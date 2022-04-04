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

namespace _CON_Code_Center
{
    public partial class Component : DataModule
    {
        public const int G_HeadRowIndex = 0;

        public Component()
        {
            InitializeComponent();
        }

        public Component(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public object[] checkCenterCname(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string centerCname = parm[0];

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
                string sql = " select count(*) as cnt from CON_CENTER where ltrim(CENTER_CNAME) = '" + centerCname + "'";

                DataSet dsCON_CENTER = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsCON_CENTER.Tables[0].Rows[0]["cnt"].ToString();

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

        public object[] getAuthorityDialogData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string centerID = parm[0];
           
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

                var sql = "";

                sql = "select USERID,USERNAME,'N' AS IS_SELECTED from USERS " + "\r\n";
                sql = sql + "where USERID NOT IN (select USERID from CON_CENTER_AUTHORITY where CENTER_ID = " + centerID + ")" + "\r\n";
                sql = sql + "union " + "\r\n";
                sql = sql + "select USERID,USERNAME,'Y' AS IS_SELECTED from USERS" + "\r\n";
                sql = sql + "where USERID IN (select USERID from CON_CENTER_AUTHORITY where CENTER_ID = " + centerID + ")" + "\r\n";
                sql = sql + "ORDER BY 1" + "\r\n";

                DataSet dsSharecode = this.ExecuteSql(sql, connection, transaction);

                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(dsSharecode.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        public object[] updateCetnerAuthorityData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string centerID = parm[0];

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
                var sql = "";
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select USERNAME from USERS where USERID= '" + userid + "'";
                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
                dsUSERS.Dispose();

                sql = "";

                sql = sql + "delete from CON_CENTER_AUTHORITY WHERE CENTER_ID = " + centerID + "\r\n"; ;

                if (parm[1] != "")
                {
                    for (var i = 1; i <= parm.Length - 1; i++)
                    {
                        if (parm[i] != "")
                        {
                            sql = sql + "insert into CON_CENTER_AUTHORITY" + "\r\n";
                            sql = sql + "select " + centerID + "," + "'" + parm[i] + "','";
                            sql = sql + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                        }
                    }
                }

                this.ExecuteSql(sql, connection, transaction);
                dsUSERS.Dispose();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }

        private void ucCON_CENTER_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucCON_CENTER.conn, ucCON_CENTER.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            var command = ucCON_CENTER.conn.CreateCommand();
            command.CommandText = "SELECT SCOPE_IDENTITY()";
            command.Transaction = ucCON_CENTER.trans;
            int newID = Convert.ToInt32(command.ExecuteScalar());

            var dataset = (DataSet)ucCON_CENTER.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CENTER);
            var table = (string)ucCON_CENTER.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CENTER);
            DataTable dt = ucCON_CENTER.GetSchema();

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
                logInfo_CON_CENTER.Log(dataset.Tables[table].Rows[i], dt, ucCON_CENTER.conn, ucCON_CENTER.trans, ucCON_CENTER.SelectCmd.KeyFields);
            }
        }

        private void ucCON_CENTER_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucCON_CENTER.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CENTER);
            var table = (string)ucCON_CENTER.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CENTER);
            DataTable dt = ucCON_CENTER.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_CENTER.Log(dataset.Tables[table].Rows[i], dt, ucCON_CENTER.conn, ucCON_CENTER.trans, ucCON_CENTER.SelectCmd.KeyFields);
            }
        }

        private void ucCON_CENTER_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucCON_CENTER.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CENTER);
            var table = (string)ucCON_CENTER.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CENTER);
            DataTable dt = ucCON_CENTER.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_CENTER.Log(dataset.Tables[table].Rows[i], dt, ucCON_CENTER.conn, ucCON_CENTER.trans, ucCON_CENTER.SelectCmd.KeyFields);
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
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CENTER_CNAME", DisplayName = "���ߤ���W��" });

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
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CENTER_CNAME", DisplayName = "���ߤ���W��", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CENTER_SEQ", DisplayName = "�Ƨ�", IsRequired = false, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Int });

                aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
                {
                    aCheckData.IsOK = false;
                    if (aCheckData.FieldName == "CENTER_CNAME")
                    {
                        var aCenterData = ValidData.CenterData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aCenterData == null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                        }
                        else aCheckData.ErrorMsg = string.Format("�i{0}�j��Ƥw�s�b", aCheckData.DisplayName);
                    }
                };

                foreach (DataRow aRow in aTable.Rows)
                {
                    Dictionary<string, object> InputContent = new Dictionary<string, object>();
                    foreach (DataColumn theColumn in aTable.Columns)
                    {
                        InputContent.Add(theColumn.Caption, aRow[theColumn.Caption]);
                        if (theColumn.ColumnName == "CENTER_CNAME")
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
                        //���ߤ���W��
                        string sql = "CENTER_CNAME = '" + aRow["CENTER_CNAME"].ToString() + "'";
                        if (aTable.Select(sql).Count() > 1)
                        {
                            aRow.SetColumnError("CENTER_CNAME", "���ߤ���W�٭���");
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

        //����������ҭӬ������
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

            DataSet DataSet = this.ExecuteSql(sql, connection, transaction);

            aAns.CenterData = DataSet.Tables[0].AsEnumerable().Select(m => new CenterData { ID = m.Field<int>("CENTER_ID"), Name = m.Field<string>("CENTER_CNAME") }).ToList();

            return aAns;
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
                    sql = sql + "insert into CON_CENTER (";
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

        //--------------------------------------������ҮɭԪ����-------------
        public class DataTableCellValidateData
        {
            public List<CenterData> CenterData { get; set; }

            public DataTableCellValidateData()
            {
                CenterData = new List<CenterData>();
            }
        }

        //���ߥN�X
        public class CenterData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class ImportData
        {
            public string CENTER_CNAME { get; set; }
            public string CENTER_ENAME { get; set; }
            public string CENTER_SEQ { get; set; }
        }
    }
}
