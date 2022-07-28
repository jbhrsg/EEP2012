using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using TheExcelFileImport;
using Newtonsoft.Json;

namespace sglVoucherImport
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
        }

        public Component(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        //新增前
        private void ucglVoucherDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglVoucherDetails_CompnnentValidate();    //驗證
        }
        ////修改前
        //private void ucglVoucherDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        //{
        //    ucglVoucherDetails_CompnnentValidate();    //驗證
        //}

        //【檢查欄位】
        private string[] ucglVoucherDetails_ValidateField = new string[] {"BorrowLendType", "Acno", "SubAcno", "CostCenterID", "Describe", "Amt" };

        //【驗證】
        private void ucglVoucherDetails_CompnnentValidate()
        {
            //抓驗證欄位
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucglVoucherDetails_ValidateField)
            {
                InputContent[aString] = ucglVoucherDetails.GetFieldCurrentValue(aString);
            }

            //資料驗證
            string ErrorMsg = ucglVoucherDetails_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //邏輯驗證
            ErrorMsg = ucglVoucherDetails_ServerValidate(ucglVoucherDetails.conn, ucglVoucherDetails.trans, InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //薪資數值替換(加密)
            ucglVoucherDetails.SetFieldValue("AMT", Salary.ENCODE(1, Convert.ToDecimal(InputContent["AMT_Decode"])));
        }

        //【資料驗證】
        private string ucglVoucherDetails_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "VoucherDate", DisplayName = "傳票日期", SystemCheckType = TheDictionaryCheckType.DateTime, IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BorrowLendType", DisplayName = "借貸", SystemCheckType = TheDictionaryCheckType.Int, IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Acno", DisplayName = "科目", SystemCheckType = TheDictionaryCheckType.Int, IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SubAcno", DisplayName = "科目明細",  IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Describe", DisplayName = "內容", IsUserCheck = true });

            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Amt", DisplayName = "金額", SystemCheckType = TheDictionaryCheckType.Int, IsUserCheck = true });

            var SalaryIDItemsTable = SQL_Tools.GetDataSet(this, cb_glVoucherDetails.CommandText, new ArrayList()).Tables[0];

            aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
            {
                if (aCheckData.FieldName == "Acno")
                {
                    if (!SalaryIDItemsTable.AsEnumerable().Any(m => m.Field<int>("Acno") == Convert.ToInt32(aCheckData.TheValue)))
                    {
                        aCheckData.ErrorMsg = string.Format("【{0}】設定錯誤", aCheckData.DisplayName);
                    }
                    else aCheckData.IsOK = true;
                }
                if (aCheckData.FieldName == "Amt")
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
        private string ucglVoucherDetails_ServerValidate(IDbConnection connection, IDbTransaction transaction, Dictionary<string, object> InputContent)
        {
            //●判斷是否有此 公司別之科目與明細
            string SQL = @"
select COUNT(*) as iCount from glAccountItem where CompanyID=@CompanyID
    and Acno=@Acno and SubAcno=@SubAcno 
";
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@CompanyID", InputContent["CompanyID"]));
            Parameter.Add(new SqlParameter("@Acno", InputContent["Acno"]));
            Parameter.Add(new SqlParameter("@SubAcno", InputContent["SubAcno"]));
            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

            try
            {
                DataSet DataSet = this.ExecuteSql(SQL, connection, transaction, Parameter);
                int Rows = Convert.ToInt32(DataSet.Tables[0].Rows[0][0]);
                if (Rows == 0) return "【公司別之科目與明細】不存在";
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
            var Validate_Input = Parameter_Input.Where(m => ucglVoucherDetails_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //資料驗證
            string ErrorMsg = ucglVoucherDetails_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //邏輯驗證
                ErrorMsg = ucglVoucherDetails_ServerValidate(connection, transaction, Validate_Input);
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        //public object[] GetOldSetting(object[] objParam)
        //{
//            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

//            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
//            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EMPLOYEE_ID", DisplayName = "員工編號" });
//            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EFFECT_DATE", DisplayName = "生效日期", SystemCheckType = TheDictionaryCheckType.DateTime });
//            aCheckDictionary.SetFieldValue(Parameter_Input);
//            aCheckDictionary.DoCheck();

//            string ErrorMsg = aCheckDictionary.GetFirstErrorMsg();
//            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { Result = ErrorMsg }.ToJsonString() };

//            string SQL = @"
//Select	T.EFFECT_DATE,
//		C.SALARY_CODE,
//		C.SALARY_CNAME,
//		C.SALARY_ENAME,
//		[dbo].[Decode](T.AMT) as Num
//From	[dbo].[dtHRM_SALARY_SALBASE_BASETTS_DateAndMan](@EmployeeID,@TheDate) as T Left Join
//		[dbo].[HRM_SALARY_SALCODE]	as C on T.SALARY_ID = C.SALARY_ID
//            ";

//            ArrayList Parameter = new ArrayList();
//            Parameter.Add(new SqlParameter("@EmployeeID", Parameter_Input["EMPLOYEE_ID"]));
//            Parameter.Add(new SqlParameter("@TheDate", Parameter_Input["EFFECT_DATE"]));
//            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

//            DataSet DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
//            if (DataSet == null || DataSet.Tables.Count == 0 || DataSet.Tables[0].Rows.Count == 0) return new object[] { 0, new TheJsonResult { ErrorMsg = "找不到相關資料" }.ToJsonString() };
//            return new object[] { 0, new TheJsonResult { IsOK = true, Result = DataSet.Tables[0] }.ToJsonString() };
        //}

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
                //檔案
                var aMemoryStream = (MemoryStream)HandlerHelper.DeserializeObject(objParam[0].ToString());

                //SheetIndex
                var SheetIndex = (int)(objParam[1]);

                //標題
                var titleObject = (Dictionary<string, object>)HandlerHelper.DeserializeObject(objParam[2].ToString());

                //參數
                var voucherObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(objParam[3].ToString());                
               
                //匯入作業結果
                var aExcelFileImportResult = new ExcelFileImport
                {
                    FileStream = aMemoryStream,
                    SheetIndex = SheetIndex,
                    HealderParameter = titleObject,
                    CompanyID = Convert.ToInt32(voucherObject["CompanyID"]),
                    VoucherID = Convert.ToInt32(voucherObject["VoucherID"]),
                    theDataModule = this,
                    UserID = SrvUtils.GetValue("_usercode", this)[1].ToString(),
                    CreateMan = SrvUtils.GetValue("_username", this)[1].ToString()
                }.GetTheFileImportResult();

                theResult["IsOK"] = aExcelFileImportResult.IsOK;
                theResult["Msg"] = aExcelFileImportResult.ErrorMsg;
                if (!aExcelFileImportResult.IsOK && aExcelFileImportResult.Result != null) theResult["File"] = (MemoryStream)aExcelFileImportResult.Result;
                
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
