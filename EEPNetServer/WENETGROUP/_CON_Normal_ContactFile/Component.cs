using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
namespace _CON_Normal_ContactFile
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_FILE, logInfo_CON_CONTACT_FILE, "CONTACT_FILE_ID");  //LogInfo修正
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_FILE);     
        }

        public Component(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_FILE, logInfo_CON_CONTACT_FILE, "CONTACT_FILE_ID");  //LogInfo修正
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_FILE);              
        }

        public const int G_HeadRowIndex = 0;

        private void ucCON_CONTACT_FILE_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_CONTACT_FILE_CompnnentValidate();    //驗證
        }

        private void ucCON_CONTACT_FILE_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCON_CONTACT_FILE_CompnnentValidate();    //驗證
        }

        //【檢查欄位】
        private string[] ucCON_CONTACT_FILE_ValidateField = new string[] { "CONTACT_FILE_ID", "CONTACT_ID", "CONTACT_FILE"};

        //【驗證】
        private void ucCON_CONTACT_FILE_CompnnentValidate()
        {
            //抓驗證欄位
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucCON_CONTACT_FILE_ValidateField)
            {
                InputContent[aString] = ucCON_CONTACT_FILE.GetFieldCurrentValue(aString);
            }

            //資料驗證
            string ErrorMsg = ucCON_CONTACT_FILE_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //邏輯驗證
            ErrorMsg = ucCON_CONTACT_FILE_ServerValidate(ucCON_CONTACT_FILE.conn, ucCON_CONTACT_FILE.trans, InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);
        }

        //【資料驗證】
        private string ucCON_CONTACT_FILE_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            //aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_MEMO_ID", DisplayName = "KEY" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "聯絡人ID" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_FILE", DisplayName = "上傳檔案" });
            
            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }

        //【邏輯驗證】        
        private string ucCON_CONTACT_FILE_ServerValidate(IDbConnection connection, IDbTransaction transaction, Dictionary<string, object> InputContent)
        {
            //●判斷是否有重複資料在裡面了
            string SQL = @"
Select	Case When 
		Exists( 
			Select	1
			From	[dbo].[CON_CONTACT_FILE]
			Where	CONTACT_ID = @CONTACT_ID and CONTACT_FILE = @CONTACT_FILE 
		) Then 1
		Else 0	 
		End [Any]
";

            try
            {
                var Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CONTACT_ID", InputContent["CONTACT_ID"]));
                Parameter.Add(new SqlParameter("@CONTACT_FILE", InputContent["CONTACT_FILE"]));
                foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                DataSet DataSet = this.ExecuteSql(SQL, connection, transaction, Parameter);
                int Rows = Convert.ToInt32(DataSet.Tables[0].Rows[0][0]);
                if (Rows == 1) return "【上傳檔案】重複";
                return "";
            }
            catch (Exception ex) { return ex.Message; }
        }

        //前台呼叫【驗證】
        public object[] DataValidate(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //抓驗證欄位
            var Validate_Input = Parameter_Input.Where(m => ucCON_CONTACT_FILE_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //資料驗證
            string ErrorMsg = ucCON_CONTACT_FILE_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //邏輯驗證
                ErrorMsg = ucCON_CONTACT_FILE_ServerValidate(connection, transaction, Validate_Input);
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }
    }
}
