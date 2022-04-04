using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using System.Reflection;
using Newtonsoft;
using Newtonsoft.Json;
using JBTool;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using NPOI.SS.UserModel;

namespace _CON_Normal_Contact
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

        public const int G_HeadRowIndex = 0;

        public object[] checkContactCellphone(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string centerID = parm[0];
            string ContactCellphone = parm[1];

            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select count(*) as cnt from CON_CONTACT where ltrim(CONTACT_CELLPHONE) = '" + ContactCellphone + "'";

                DataSet dsCON_CONTACT = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsCON_CONTACT.Tables[0].Rows[0]["cnt"].ToString();

                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        //�Ⱥ޲z����ݨ�����A�þ֦��R�����\��A��D�޲z���������ϥΪ̵L�k�R��
        public object[] checkUser(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string centerID = parm[0];
            string contactName = parm[1];

            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string sql = "select count(*) as cnt from USERS where AUTOLOGIN = 'S' and USERID= '" + userid + "'";
                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsUSERS.Tables[0].Rows[0]["cnt"].ToString();

                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        private void ucCON_CONTACT_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucCON_CONTACT.conn, ucCON_CONTACT.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            var command = ucCON_CONTACT.conn.CreateCommand();
            command.CommandText = "SELECT SCOPE_IDENTITY()";
            command.Transaction = ucCON_CONTACT.trans;
            int newID = Convert.ToInt32(command.ExecuteScalar());

            var dataset = (DataSet)ucCON_CONTACT.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT);
            var table = (string)ucCON_CONTACT.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT);
            DataTable dt = ucCON_CONTACT.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                if (dataset.Tables[table].Rows[i].RowState == DataRowState.Added)
                {
                    dataset.Tables[table].Rows[i]["CENTER_ID"] = newID;
                    dataset.Tables[table].Rows[i]["CREATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["UPDATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["CREATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataset.Tables[table].Rows[i]["UPDATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                logInfo_CON_CONTACT.Log(dataset.Tables[table].Rows[i], dt, ucCON_CONTACT.conn, ucCON_CONTACT.trans, ucCON_CONTACT.SelectCmd.KeyFields);
            }
        }

        private void ucCON_CONTACT_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucCON_CONTACT.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT);
            var table = (string)ucCON_CONTACT.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT);
            DataTable dt = ucCON_CONTACT.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_CONTACT.Log(dataset.Tables[table].Rows[i], dt, ucCON_CONTACT.conn, ucCON_CONTACT.trans, ucCON_CONTACT.SelectCmd.KeyFields);
            }
        }

        private void ucCON_CONTACT_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucCON_CONTACT.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT);
            var table = (string)ucCON_CONTACT.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT);
            DataTable dt = ucCON_CONTACT.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_CONTACT.Log(dataset.Tables[table].Rows[i], dt, ucCON_CONTACT.conn, ucCON_CONTACT.trans, ucCON_CONTACT.SelectCmd.KeyFields);
            }
        }

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
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CENTER_ID", DisplayName = "���ߦW��" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_NAME", DisplayName = "�m�W" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_CELLPHONE", DisplayName = "���" });
            
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
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_NAME", DisplayName = "�m�W", IsRequired = true, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CENTER_ID", DisplayName = "���ߦW��", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_CELLPHONE", DisplayName = "���", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_EMAIL", DisplayName = "E-Mail", IsRequired = false, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Email});
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_AREA_ID", DisplayName = "�ϰ�", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_TYPE_ID", DisplayName = "����", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_TERRITORY_ID", DisplayName = "���O", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_SKILL_ID", DisplayName = "�M��", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_HOBBY_ID", DisplayName = "����", IsRequired = false, IsUserCheck = true });

                aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
                {
                    aCheckData.IsOK = false;
                    if (aCheckData.FieldName == "CONTACT_CELLPHONE")
                    {
                        var aContactData = ValidData.ContactData.FirstOrDefault(m => m.Cellphone == aCheckData.TheValue.ToString());
                        if (aContactData == null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("�i{0}�j������X����", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "CENTER_ID")
                    {
                        var aCenterData = ValidData.CenterData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aCenterData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aCenterData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("�i{0}�j�S���۹������߸��", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "CONTACT_AREA_ID")
                    {
                        var aContactAreaData = ValidData.ContactAreaData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aContactAreaData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aContactAreaData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("�i{0}�j�S���۹����ϰ���", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "CONTACT_TYPE_ID")
                    {
                        var aContactTypeData = ValidData.ContactTypeData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aContactTypeData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aContactTypeData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("�i{0}�j�S���۹����������", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "CONTACT_TERRITORY_ID")
                    {
                        var aContactTerritoryData = ValidData.ContactTerritoryData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aContactTerritoryData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aContactTerritoryData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("�i{0}�j�S���۹������O���", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "CONTACT_SKILL_ID")
                    {
                        var aContactSkillData = ValidData.ContactSkillData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aContactSkillData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aContactSkillData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("�i{0}�j�S���۹����M�����", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "CONTACT_HOBBY_ID")
                    {
                        var aContactHobbyData = ValidData.ContactHobbyData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aContactHobbyData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aContactHobbyData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("�i{0}�j�S���۹���������", aCheckData.DisplayName);
                    }
                };

                foreach (DataRow aRow in aTable.Rows)
                {
                    Dictionary<string, object> InputContent = new Dictionary<string, object>();
                    foreach (DataColumn theColumn in aTable.Columns)
                    {
                        InputContent.Add(theColumn.Caption, aRow[theColumn.Caption]);
                        if (theColumn.ColumnName == "CONTACT_CELLPHONE")
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

                    if (codeFlag)
                    {
                        //������X
                        string sql = "CONTACT_CELLPHONE = '" + aRow["CONTACT_CELLPHONE"].ToString() + "'";
                        if (aTable.Select(sql).Count() > 1)
                        {
                            aRow.SetColumnError("CONTACT_CELLPHONE", "������X����");
                            aRow.RowError = "���ҿ��~";
                        }
                    }
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

            string sql = "select CENTER_ID, CENTER_CNAME from CON_CENTER" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'CONTACT_AREA' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'CONTACT_TYPE' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'CONTACT_TERRITORY' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'CONTACT_SKILL' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'CONTACT_HOBBY' ORDER BY SORT" + "\n\r";
            sql = sql + "select CONTACT_ID, CONTACT_NAME, CONTACT_CELLPHONE from CON_CONTACT" + "\n\r";

            DataSet DataSet = this.ExecuteSql(sql, connection, transaction);

            aAns.CenterData = DataSet.Tables[0].AsEnumerable().Select(m => new CenterData { ID = m.Field<int>("CENTER_ID"), Name = m.Field<string>("CENTER_CNAME") }).ToList();
            aAns.ContactAreaData = DataSet.Tables[1].AsEnumerable().Select(m => new ContactAreaData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ContactTypeData = DataSet.Tables[2].AsEnumerable().Select(m => new ContactTypeData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ContactTerritoryData = DataSet.Tables[3].AsEnumerable().Select(m => new ContactTerritoryData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ContactSkillData = DataSet.Tables[4].AsEnumerable().Select(m => new ContactSkillData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ContactHobbyData = DataSet.Tables[5].AsEnumerable().Select(m => new ContactHobbyData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ContactData = DataSet.Tables[6].AsEnumerable().Select(m => new ContactData { ID = m.Field<int>("CONTACT_ID"), Name = m.Field<string>("CONTACT_NAME"), Cellphone = m.Field<string>("CONTACT_CELLPHONE") }).ToList();
            return aAns;
        }

        //--------------------------------------������ҮɭԪ����-------------
        public class DataTableCellValidateData
        {
            public List<ContactData> ContactData { get; set; }
            public List<CenterData> CenterData { get; set; }
            public List<ContactAreaData> ContactAreaData { get; set; }
            public List<ContactTypeData> ContactTypeData { get; set; }
            public List<ContactTerritoryData> ContactTerritoryData { get; set; }
            public List<ContactSkillData> ContactSkillData { get; set; }
            public List<ContactHobbyData> ContactHobbyData { get; set; }

            public DataTableCellValidateData()
            {
                ContactData = new List<ContactData>();
                CenterData = new List<CenterData>();
                ContactAreaData = new List<ContactAreaData>();
                ContactTypeData = new List<ContactTypeData>();
                ContactTerritoryData = new List<ContactTerritoryData>();
                ContactSkillData = new List<ContactSkillData>();
                ContactHobbyData = new List<ContactHobbyData>();
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
                    sql = sql + "insert into CON_CONTACT (";
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

        //���ߥN�X
        public class CenterData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //�ϰ�N�X
        public class ContactAreaData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //�����N�X
        public class ContactTypeData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //���O�N�X
        public class ContactTerritoryData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //�M���N�X
        public class ContactSkillData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //����N�X
        public class ContactHobbyData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class ImportData
        {
            public int CENTER_ID { get; set; }
            public string CONTACT_NAME { get; set; }
            public string CONTACT_JOB { get; set; }
            public string CONTACT_TEL { get; set; }
            public string CONTACT_CELLPHONE { get; set; }
            public string CONTACT_EMAIL { get; set; }
            public string CONTACT_ADDR { get; set; }
            public string CONTACT_MEMO { get; set; }
            public string CONTACT_MEMO1 { get; set; }
            public string CONTACT_MEMO2 { get; set; }
            public int CONTACT_AREA_ID { get; set; }
            public int CONTACT_TYPE_ID { get; set; }
            public int CONTACT_TERRITORY_ID { get; set; }
            public int CONTACT_SKILL_NAME { get; set; }
            public int CONTACT_HOBBY_NAME { get; set; }
        }
    }
}
