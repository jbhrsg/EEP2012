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
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_FILE, logInfo_CON_CONTACT_FILE, "CONTACT_FILE_ID");  //LogInfo�ץ�
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_FILE);     
        }

        public Component(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_FILE, logInfo_CON_CONTACT_FILE, "CONTACT_FILE_ID");  //LogInfo�ץ�
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_FILE);              
        }

        public const int G_HeadRowIndex = 0;

        private void ucCON_CONTACT_FILE_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_CONTACT_FILE_CompnnentValidate();    //����
        }

        private void ucCON_CONTACT_FILE_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCON_CONTACT_FILE_CompnnentValidate();    //����
        }

        //�i�ˬd���j
        private string[] ucCON_CONTACT_FILE_ValidateField = new string[] { "CONTACT_FILE_ID", "CONTACT_ID", "CONTACT_FILE"};

        //�i���ҡj
        private void ucCON_CONTACT_FILE_CompnnentValidate()
        {
            //���������
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucCON_CONTACT_FILE_ValidateField)
            {
                InputContent[aString] = ucCON_CONTACT_FILE.GetFieldCurrentValue(aString);
            }

            //�������
            string ErrorMsg = ucCON_CONTACT_FILE_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //�޿�����
            ErrorMsg = ucCON_CONTACT_FILE_ServerValidate(ucCON_CONTACT_FILE.conn, ucCON_CONTACT_FILE.trans, InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);
        }

        //�i������ҡj
        private string ucCON_CONTACT_FILE_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            //aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_MEMO_ID", DisplayName = "KEY" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "�p���HID" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_FILE", DisplayName = "�W���ɮ�" });
            
            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }

        //�i�޿����ҡj        
        private string ucCON_CONTACT_FILE_ServerValidate(IDbConnection connection, IDbTransaction transaction, Dictionary<string, object> InputContent)
        {
            //���P�_�O�_�����Ƹ�Ʀb�̭��F
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
                if (Rows == 1) return "�i�W���ɮסj����";
                return "";
            }
            catch (Exception ex) { return ex.Message; }
        }

        //�e�x�I�s�i���ҡj
        public object[] DataValidate(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //���������
            var Validate_Input = Parameter_Input.Where(m => ucCON_CONTACT_FILE_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //�������
            string ErrorMsg = ucCON_CONTACT_FILE_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //�޿�����
                ErrorMsg = ucCON_CONTACT_FILE_ServerValidate(connection, transaction, Validate_Input);
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }
    }
}
