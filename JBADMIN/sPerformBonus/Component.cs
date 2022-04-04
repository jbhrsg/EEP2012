using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using JBTool;
using Newtonsoft;
using Newtonsoft.Json;
using TheExcelFileImport;
using System.IO;
namespace sPerformBonus
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
        //取得部門代號
        public object[] GetUserOrgNOs(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;
        }
       
        //Excel匯入
        //新增前
        private void ucPerfBonusDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucPerfBonusDetails_CompnnentValidate();    //驗證
        }
        //將績效明細金額寫到薪資補扣發
        public object PutBonusAmtToSalary_Enrich(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();
            DataRow dr = (DataRow)objParam[0];
            string PerfBonusNO = dr["PerfBonusNO"].ToString();
            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                IDbTransaction trans = conn.BeginTransaction();
                string sql = "EXEC dbo.procPutBonusAmtToSalary_Enrich '" + PerfBonusNO + "','" + UserID + "'";
                this.ExecuteSql(sql, conn, trans);
                trans.Commit();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return ret;
        }
        //【檢查欄位】
        private string[] ucPerfBonusDetails_ValidateField = new string[] { "EmpID", "EmpName", "BonusAmt"};
        //【驗證】
        private void ucPerfBonusDetails_CompnnentValidate()
        {
            //抓驗證欄位
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucPerfBonusDetails_ValidateField)
            {
                InputContent[aString] = ucPerfBonusDetails.GetFieldCurrentValue(aString);
            }
            //資料驗證
            string ErrorMsg = ucPerfBonusDetails_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);
        }
        //【資料驗證】
        private string ucPerfBonusDetails_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EmpID", DisplayName = "員工工號", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EmpName", DisplayName = "員工姓名", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BonusAmt", DisplayName = "獎金金額", IsUserCheck = true });
            //aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "AdjustAmt", DisplayName = "調整金額", IsUserCheck = true });
            //aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "AdjustNote", DisplayName = "調整原因", IsUserCheck = true });
            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }


        #region =================================ServerMethod======================================

        //前台呼叫【驗證】
        public object[] DataValidate(object[] objParam)
        {

            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //抓驗證欄位
            var Validate_Input = Parameter_Input.Where(m => ucPerfBonusDetails_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //資料驗證
            string ErrorMsg = ucPerfBonusDetails_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
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
                //檔案
                var aMemoryStream = (MemoryStream)HandlerHelper.DeserializeObject(objParam[0].ToString());

                //SheetIndex
                var SheetIndex = (int)(objParam[1]);

                //標題
                var titleObject = (Dictionary<string, object>)HandlerHelper.DeserializeObject(objParam[2].ToString());

                //參數
                var ContentObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(objParam[3].ToString());

                //匯入作業結果
                var aExcelFileImportResult = new ExcelFileImport
                {
                    FileStream = aMemoryStream,
                    SheetIndex = SheetIndex,
                    HealderParameter = titleObject,
                    //
                    PerfBonusNO = ContentObject["PerfBonusNO"].ToString(),
                    //industry = ContentObject["NameC"].ToString(),
                    theDataModule = this,
                    UserID = SrvUtils.GetValue("_usercode", this)[1].ToString(),
                    //CreateMan = SrvUtils.GetValue("_usercode", this)[1].ToString()
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
        /// <summary>Combobox用資料</summary>
        public class ComboboxField
        {
            public string text { get; set; }

            public string value { get; set; }

            public bool selected { get; set; }

            public ComboboxField()
            {
                selected = false;
            }
        }
        //檢查調整金額,提供給Flow作判斷
        public object[] GetAdjustAmt(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            //decimal AdjustAmt = 0;
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0];
            string PerfBonusNO = dr["PerfBonusNO"].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select sum(Isnull(AdjustAmt,0)) as AdjustAmt from PerfBonusDetails where PerfBonusNO ='" + PerfBonusNO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal AdjustAmt = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                //ret[1] = AdjustAmt;
                //ret[1] = AdjustAmt;
                if (AdjustAmt != 0)
                {
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }

        private void ucPerfBonusMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucPerfBonusMaster.SetFieldValue("CreateDate", DateTime.Now);
        }
        public object[] CheckIsDuplicateApply(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string Org_NOParent = parm[0];
                string PerfBonusYM = parm[1];
                string UserID = parm[2];
                string sql = "Select Top 1 ISNULL(dbo.funReturnIsDuplicateApply('" + Org_NOParent + "','" + PerfBonusYM + "'),0) AS STR FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                js = dsTemp.Tables[0].Rows[0]["STR"].ToString();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
     }
}
