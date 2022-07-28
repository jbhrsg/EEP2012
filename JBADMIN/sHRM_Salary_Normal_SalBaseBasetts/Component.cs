using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Srvtools;
using JBTool;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using TheExcelFileImport1;

namespace _HRM_Salary_Normal_SalBaseBasetts
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucHRM_SALARY_SALBASE_BASETTS, log_HRM_SALARY_SALBASE_BASETTS, "SALBASE_BASETTS_ID");      //LogInfo修正
            IndentityLogInfoPatch.SysFieldAttr(ucHRM_SALARY_SALBASE_BASETTS);                                                       //FieldAttr修正
        }

        public Component(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucHRM_SALARY_SALBASE_BASETTS, log_HRM_SALARY_SALBASE_BASETTS, "SALBASE_BASETTS_ID");      //LogInfo修正
            IndentityLogInfoPatch.SysFieldAttr(ucHRM_SALARY_SALBASE_BASETTS);                                                       //FieldAttr修正
        }

        //新增前
        private void ucHRM_SALARY_SALBASE_BASETTS_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHRM_SALARY_SALBASE_BASETTS_CompnnentValidate();    //驗證
        }

        //修改前
        private void ucHRM_SALARY_SALBASE_BASETTS_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHRM_SALARY_SALBASE_BASETTS_CompnnentValidate();    //驗證
        }

        //【檢查欄位】
        private string[] ucHRM_SALARY_SALBASE_BASETTS_ValidateField = new string[] { "SALBASE_BASETTS_ID", "EMPLOYEE_ID", "EFFECT_DATE", "SALARY_ID", "AMT_Decode" };

        //【驗證】
        private void ucHRM_SALARY_SALBASE_BASETTS_CompnnentValidate()
        {
            //抓驗證欄位
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucHRM_SALARY_SALBASE_BASETTS_ValidateField)
            {
                InputContent[aString] = ucHRM_SALARY_SALBASE_BASETTS.GetFieldCurrentValue(aString);
            }

            //資料驗證
            string ErrorMsg = ucHRM_SALARY_SALBASE_BASETTS_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //邏輯驗證
            ErrorMsg = ucHRM_SALARY_SALBASE_BASETTS_ServerValidate(ucHRM_SALARY_SALBASE_BASETTS.conn, ucHRM_SALARY_SALBASE_BASETTS.trans, InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //薪資數值替換(加密)
            ucHRM_SALARY_SALBASE_BASETTS.SetFieldValue("AMT", Salary.ENCODE(1, Convert.ToDecimal(InputContent["AMT_Decode"])));
        }

        //【資料驗證】
        private string ucHRM_SALARY_SALBASE_BASETTS_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SALBASE_BASETTS_ID", DisplayName = "ID", SystemCheckType = TheDictionaryCheckType.Int });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EMPLOYEE_ID", DisplayName = "員工編號" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EFFECT_DATE", DisplayName = "日期", SystemCheckType = TheDictionaryCheckType.DateTime });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SALARY_ID", DisplayName = "薪資項目", SystemCheckType = TheDictionaryCheckType.Int, IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "AMT_Decode", DisplayName = "金額", SystemCheckType = TheDictionaryCheckType.Decimal, IsUserCheck = true });

            var SalaryIDItemsTable = SQL_Tools.GetDataSet(this, cb_HRM_SALARY_SALCODE_SalaryApproval.CommandText, new ArrayList()).Tables[0];

            aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
            {
                if (aCheckData.FieldName == "SALARY_ID")
                {
                    if (!SalaryIDItemsTable.AsEnumerable().Any(m => m.Field<int>("SALARY_ID") == Convert.ToInt32(aCheckData.TheValue)))
                    {
                        aCheckData.ErrorMsg = string.Format("【{0}】設定錯誤", aCheckData.DisplayName);
                    }
                    else aCheckData.IsOK = true;
                }
                if (aCheckData.FieldName == "AMT_Decode")
                {
                    if (Convert.ToDecimal(aCheckData.TheValue) < 0)
                    {
                        aCheckData.ErrorMsg = string.Format("【{0}】設定錯誤", aCheckData.DisplayName);
                    }
                    else aCheckData.IsOK = true;
                }
            };

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }

        //【邏輯驗證】        
        private string ucHRM_SALARY_SALBASE_BASETTS_ServerValidate(IDbConnection connection, IDbTransaction transaction, Dictionary<string, object> InputContent)
        {
            //●判斷是否有重複資料在裡面了
            string SQL = @"
Select	Case When 
		Exists( 
			Select	1
			From	[dbo].[HRM_SALARY_SALBASE_BASETTS]
			Where	[EMPLOYEE_ID] = @EmployeeID and [EFFECT_DATE] = @TheDate and [SALARY_ID] = @SalaryID and SALBASE_BASETTS_ID != @ID
		) Then 1
		Else 0	 
		End [Any]
";
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@ID", InputContent["SALBASE_BASETTS_ID"]));
            Parameter.Add(new SqlParameter("@EmployeeID", InputContent["EMPLOYEE_ID"]));
            Parameter.Add(new SqlParameter("@TheDate", InputContent["EFFECT_DATE"]));
            Parameter.Add(new SqlParameter("@SalaryID", InputContent["SALARY_ID"]));
            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

            try
            {
                DataSet DataSet = this.ExecuteSql(SQL, connection, transaction, Parameter);
                int Rows = Convert.ToInt32(DataSet.Tables[0].Rows[0][0]);
                if (Rows == 1) return "【薪資項目】重複";
                return "";
            }
            catch (Exception ex) { return ex.Message; }
        }

        #region =================================ServerMethod======================================

        //前台呼叫【驗證】
        public object[] DataValidate(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //抓驗證欄位
            var Validate_Input = Parameter_Input.Where(m => ucHRM_SALARY_SALBASE_BASETTS_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //資料驗證
            string ErrorMsg = ucHRM_SALARY_SALBASE_BASETTS_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //邏輯驗證
                ErrorMsg = ucHRM_SALARY_SALBASE_BASETTS_ServerValidate(connection, transaction, Validate_Input);
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        public object[] GetOldSetting(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EMPLOYEE_ID", DisplayName = "員工編號" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EFFECT_DATE", DisplayName = "生效日期", SystemCheckType = TheDictionaryCheckType.DateTime });
            aCheckDictionary.SetFieldValue(Parameter_Input);
            aCheckDictionary.DoCheck();

            string ErrorMsg = aCheckDictionary.GetFirstErrorMsg();
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { Result = ErrorMsg }.ToJsonString() };

            string SQL = @"
Select	T.EFFECT_DATE,
		C.SALARY_CODE,
		C.SALARY_CNAME,
		C.SALARY_ENAME,
		[dbo].[Decode](T.AMT) as Num
From	[dbo].[dtHRM_SALARY_SALBASE_BASETTS_DateAndMan](@EmployeeID,@TheDate) as T Left Join
		[dbo].[HRM_SALARY_SALCODE]	as C on T.SALARY_ID = C.SALARY_ID
            ";

            ArrayList Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@EmployeeID", Parameter_Input["EMPLOYEE_ID"]));
            Parameter.Add(new SqlParameter("@TheDate", Parameter_Input["EFFECT_DATE"]));
            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

            DataSet DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
            if (DataSet == null || DataSet.Tables.Count == 0 || DataSet.Tables[0].Rows.Count == 0) return new object[] { 0, new TheJsonResult { ErrorMsg = "找不到相關資料" }.ToJsonString() };
            return new object[] { 0, new TheJsonResult { IsOK = true, Result = DataSet.Tables[0] }.ToJsonString() };
        }

        #endregion

        //Excel檔案匯入
        public object[] ExcelFileImport(object[] objParam)
        {

            //回傳
            var theResult = new Dictionary<string, object>();
            theResult.Add("IsOK", false);
            theResult.Add("Msg", "錯誤了唷");

            try
            {
                //參數
                var Parameter = (Dictionary<string, object>)HandlerHelper.DeserializeObject(objParam[0].ToString());

                //檔案
                var aMemoryStream = (MemoryStream)HandlerHelper.DeserializeObject(objParam[1].ToString());

                //匯入作業結果
                var aExcelFileImportResult = new ExcelFileImport
                {
                    FileStream = aMemoryStream,
                    HealderParameter = Parameter,
                    theDataModule = this,
                    CreateMan = SrvUtils.GetValue("_username", this)[1].ToString()
                }.GetTheFileImportResult();

                theResult["IsOK"] = aExcelFileImportResult.IsOK;
                theResult["Msg"] = aExcelFileImportResult.ErrorMsg;
                if (aExcelFileImportResult.Result != null) theResult["File"] = (MemoryStream)aExcelFileImportResult.Result;
            }
            catch (TheUserDefinedException ex)
            {
                theResult["IsOK"] = false;
                theResult["Msg"] = ex.Message;
            }
            catch (Exception)
            {
                theResult["IsOK"] = false;
                theResult["Msg"] = "執行錯誤";
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
        }
    }
}
