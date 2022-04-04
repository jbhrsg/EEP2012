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

namespace _CON_Normal_ContactMemo
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_MEMO, logInfo_CON_CONTACT_MEMO, "CONTACT_MEMO_ID");  //LogInfo�ץ�
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_MEMO);                                             //FieldAttr�ץ�
        }

        public Component(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_MEMO, logInfo_CON_CONTACT_MEMO, "CONTACT_MEMO_ID");  //LogInfo�ץ�
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_MEMO);                                             //FieldAttr�ץ�
        }

        public const int G_HeadRowIndex = 0;

        //�s�W�e
        private void ucCON_CONTACT_MEMO_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_CONTACT_MEMO_CompnnentValidate();    //����
        }

        //�ק�e
        private void ucCON_CONTACT_MEMO_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCON_CONTACT_MEMO_CompnnentValidate();    //����
        }

        //�i�ˬd���j
        private string[] ucCON_CONTACT_MEMO_ValidateField = new string[] { "CONTACT_MEMO_ID", "CONTACT_ID", "MEMO_DATE", "MEMO_CONTENT", "MEMO_USER" };

        //�i���ҡj
        private void ucCON_CONTACT_MEMO_CompnnentValidate()
        {
            //���������
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucCON_CONTACT_MEMO_ValidateField)
            {
                InputContent[aString] = ucCON_CONTACT_MEMO.GetFieldCurrentValue(aString);
            }

            //�������
            string ErrorMsg = ucCON_CONTACT_MEMO_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //�޿�����
            ErrorMsg = ucCON_CONTACT_MEMO_ServerValidate(ucCON_CONTACT_MEMO.conn, ucCON_CONTACT_MEMO.trans, InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);
        }

        //�i������ҡj
        private string ucCON_CONTACT_MEMO_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            //aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_MEMO_ID", DisplayName = "KEY" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "�p���HID" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_DATE", DisplayName = "���O���" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_CONTENT", DisplayName = "���O���e" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_USER", DisplayName = "���O�H��" });

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }

        //�i�޿����ҡj        
        private string ucCON_CONTACT_MEMO_ServerValidate(IDbConnection connection, IDbTransaction transaction, Dictionary<string, object> InputContent)
        {
            //���P�_�O�_�����Ƹ�Ʀb�̭��F
            return "";
        }

        //�e�x�I�s�i���ҡj
        public object[] DataValidate(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //���������
            var Validate_Input = Parameter_Input.Where(m => ucCON_CONTACT_MEMO_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //�������
            string ErrorMsg = ucCON_CONTACT_MEMO_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //�޿�����
                ErrorMsg = ucCON_CONTACT_MEMO_ServerValidate(connection, transaction, Validate_Input);
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        //-----------------------------------�W�Ǹ��-------------------------------------
        public object[] FileUpload(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

                var aResult = new { FileName = "", };

                //�i��J���ҡj�G�e�x��DataForm��J����
                var aCheckDictionary = FileUploadFormValidate(Parameter_Input);
                string ErrorMsg = aCheckDictionary.GetFirstErrorMsg();
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

                //�ɮ׸��|
                //string FilePathName = string.Format("../JQWebClient/{0}", Parameter_Input["FilePathName"]);
                string FilePathName = Parameter_Input["FilePathName"].ToString();

                //�i���oColumnName�j�G�N�e�x��J������ഫ��ColumnName
                var aHeadList = FileUploadGetHeadList(Parameter_Input);

                //�i���ơj�GColumnName���X���s��DataTable                
                DataTable FileData = NPOIHelper.GetDataTable(FilePathName, aHeadList, G_HeadRowIndex);
                if (FileData == null) throw new Exception("�i���Ū���jError");

                //�i������ҡj�B�i����ഫ�j�G����Ʈw�O�_���o�ӼƭȡA���N�N�ƭ��ഫ(Code->ID)
                if (!DataTableCellValidate(FileData)) throw new Exception("�i������ҡjError");
                if (FileData.HasErrors)
                {
                    if (!NPOIHelper.SetErrorMemo(FileData, FilePathName, aHeadList, G_HeadRowIndex)) throw new Exception("�i�ɮ׽s��jError");
                    return new object[] { 0, new TheJsonResult { ErrorMsg = "������Ҧ����~", Result = Parameter_Input["FilePathName"] }.ToJsonString() };
                }

                //�i�W�h���ҡj�G��������
                if (!DataTableDataValidate(FileData)) throw new Exception("�i������ҡjError");
                if (FileData.HasErrors)
                {
                    if (!NPOIHelper.SetErrorMemo(FileData, FilePathName, aHeadList, G_HeadRowIndex)) throw new Exception("�i�ɮ׽s��jError");
                    return new object[] { 0, new TheJsonResult { ErrorMsg = "������Ҧ����~", Result = Parameter_Input["FilePathName"] }.ToJsonString() };
                }

                //�g�J
                if (!DataTableDataInsert(FileData)) throw new Exception("�i��Ƽg�J�jError");

                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };

            }
            catch (Exception ex)
            {
                return new object[] { 0, new TheJsonResult { ErrorMsg = "������~" }.ToJsonString() };
            }

        }

        //��J����
        private TheDictionaryCheck FileUploadFormValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "FilePathName", DisplayName = "�ɮ׸��|" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "�p���H���" });

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary;
        }

        //���oColumnName
        private Dictionary<string, int> FileUploadGetHeadList(Dictionary<string, object> Parameter_Input)
        {
            Dictionary<string, int> aHeadList = new Dictionary<string, int>();

            foreach (var aProperty in typeof(ImportData).GetProperties())
            {
                if (Parameter_Input[aProperty.Name].ToString() != "")
                    aHeadList[aProperty.Name] = Convert.ToInt32(Parameter_Input[aProperty.Name]);
            }
            return aHeadList;
        }

        //--------------------------------------�������(���K����ഫ)----------------------------
        private bool DataTableCellValidate(DataTable aTable)
        {
            bool codeFlag = false;

            try
            {
                var ValidData = GetValidData();

                TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "�p���H���", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_DATE", DisplayName = "���O���", IsRequired = true, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_CONTENT", DisplayName = "���O���e", IsRequired = true, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_USER", DisplayName = "���O�H��", IsRequired = true, IsUserCheck = false });
                
                aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
                {
                    aCheckData.IsOK = false;
                    if (aCheckData.FieldName == "CONTACT_ID")
                    {
                        var aContactData = ValidData.ContactData.FirstOrDefault(m => m.Cellphone == aCheckData.TheValue.ToString());
                        if (aContactData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aContactData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("�i{0}�j�S���۹����p���H������", aCheckData.DisplayName);
                    }
                };

                foreach (DataRow aRow in aTable.Rows)
                {
                    Dictionary<string, object> InputContent = new Dictionary<string, object>();
                    foreach (DataColumn theColumn in aTable.Columns)
                    {
                        InputContent.Add(theColumn.Caption, aRow[theColumn.Caption]);
                        if (theColumn.ColumnName == "CONTACT_ID")
                            codeFlag = true;
                    }
                    aCheckDictionary.SetFieldValue(InputContent);
                    aCheckDictionary.DoCheck();

                    aCheckDictionary.CheckData.ForEach(delegate(TheDictionaryCheckData aCheckData)
                    {
                        if (aCheckData.TheValue != null)
                        {
                            if (!aCheckData.IsOK)
                                aRow.SetColumnError(aCheckData.FieldName, aCheckData.ErrorMsg);
                            else
                                aRow[aCheckData.FieldName] = aCheckData.TheResult;
                        }
                    });

                    //if (codeFlag)
                    //{
                    //    //�p���H�m�W
                    //    string sql = "CONTACT_NAME = '" + aRow["CONTACT_NAME"].ToString() + "'";
                    //    if (aTable.Select(sql).Count() > 1)
                    //    {
                    //        aRow.SetColumnError("CONTACT_NAME", "�p���H�m�W����");
                    //        aRow.RowError = "���ҿ��~";
                    //    }
                    //}

                    aRow.RowError = aRow.HasErrors ? "���ҿ��~" : "";
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //���o������ҭӬ������
        private DataTableCellValidateData GetValidData()
        {
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            DataTableCellValidateData aAns = new DataTableCellValidateData();

            string sql = "select CONTACT_ID, CONTACT_NAME, CONTACT_CELLPHONE from CON_CONTACT" + "\n\r";

            DataSet DataSet = this.ExecuteSql(sql, connection, transaction);

            aAns.ContactData = DataSet.Tables[0].AsEnumerable().Select(m => new ContactData { ID = m.Field<int>("CONTACT_ID"), Name = m.Field<string>("CONTACT_NAME"), Cellphone = m.Field<string>("CONTACT_CELLPHONE") }).ToList();
            return aAns;
        }

        //--------------------------------------������ҮɭԪ����-------------
        public class DataTableCellValidateData
        {
            public List<ContactData> ContactData { get; set; }
            
            public DataTableCellValidateData()
            {
                ContactData = new List<ContactData>();
            }
        }

        //---------------------------------------�������-----------------------------------------
        private bool DataTableDataValidate(DataTable aTable)
        {
            return true;
        }

        //---------------------------------------�g�J��Ʈw---------------------------------------
        private bool DataTableDataInsert(DataTable aTable)
        {
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string sql = "";
            int columnCount = 0;

            try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select USERNAME from USERS where USERID= '" + userid + "'";
                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

                sql = "";
                foreach (DataRow aRow in aTable.Rows)
                {
                    columnCount = aRow.ItemArray.Length;
                    sql = sql + "insert into CON_CONTACT_MEMO (";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + column.ColumnName + ",";
                    }
                    sql = sql + " CREATE_MAN,CREATE_DATE,UPDATE_MAN,UPDATE_DATE)";
                    sql = sql + " select '";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + (aRow[column].ToString().Trim() == "" ? "null" : aRow[column].ToString().Trim()) + "','";
                    }
                    sql = sql + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                }
                sql = sql.Replace("'null'", "null");
                this.ExecuteSql(sql, connection, transaction);
                dsUSERS.Dispose();
                transaction.Commit();
                return true;
            }
            catch { transaction.Rollback(); return false; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        //�p���H���
        public class ContactData
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Cellphone { get; set; }
        }

        public class ImportData
        {
            public int CONTACT_ID { get; set; }
            public string MEMO_DATE { get; set; }
            public string MEMO_CONTENT { get; set; }
            public string MEMO_USER { get; set; }
        }
    }
}
