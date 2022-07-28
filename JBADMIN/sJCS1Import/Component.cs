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


namespace sJCS1Import
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
        private void ucRoomerUpdate_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucRoomerUpdate_CompnnentValidate();    //驗證
        }
        //【檢查欄位】
        private string[] ucRoomerUpdate_ValidateField = new string[] { "CustNo", "CostCenter", "CustClass"};

        //【驗證】
        private void ucRoomerUpdate_CompnnentValidate()
        {
            //抓驗證欄位
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucRoomerUpdate_ValidateField)
            {
                InputContent[aString] = ucRoomerUpdate.GetFieldCurrentValue(aString);
            }

            //資料驗證
            string ErrorMsg = ucRoomerUpdate_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);
           
        }

        //【資料驗證】
        private string ucRoomerUpdate_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CustNo", DisplayName = "員工號碼", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CostCenter", DisplayName = "成本中心", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CustClass", DisplayName = "班別", IsUserCheck = true });  

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
            var Validate_Input = Parameter_Input.Where(m => ucRoomerUpdate_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //資料驗證
            string ErrorMsg = ucRoomerUpdate_ClientValidate(Validate_Input);
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
                var voucherObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(objParam[3].ToString());                

                //匯入作業結果
                var aExcelFileImportResult = new ExcelFileImport
                {
                    FileStream = aMemoryStream,
                    SheetIndex = SheetIndex,
                    HealderParameter = titleObject,
                    CustomerID = voucherObject["CustomerID"].ToString(),
                    theDataModule = this,
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
