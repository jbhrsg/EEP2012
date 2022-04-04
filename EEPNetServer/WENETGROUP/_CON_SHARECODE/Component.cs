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

namespace _CON_SHARECODE
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

        public object[] checkName(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string fieldName = parm[0];
            string name = parm[1];

            string js = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select count(*) as cnt from CON_SHARECODE where FIELDNAME = '" + fieldName + "' and ltrim(NAME) = '" + name + "'";

                DataSet dsCON_SHARECODE = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsCON_SHARECODE.Tables[0].Rows[0]["cnt"].ToString();

                //Indented縮排 將資料轉換成Json格式
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

        public object[] getDialogData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string type = parm[0];
            string listData = "";
            if (parm[1] != "")
            {
                for (var i = 1; i <= parm.Length - 1; i++)
                {
                    if (i == parm.Length - 1)
                        listData = listData + "'" + parm[i] + "'";
                    else
                        listData = listData + "'" + parm[i] + "', ";
                }
            }

            string js = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                var user = GetClientInfo(ClientInfoType.LoginUser);
                var sql = "";

                switch (type)
                {
                    case "SKILL":
                        if (listData != "")
                        {
                            sql = "select CODE_ID,NAME,SORT,'N' AS IS_SELECTED from CON_SHARECODE " + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_SKILL' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " AND CODE_ID NOT IN (" + listData + ")" + "\n\r";
                            sql = sql + " union " + "\n\r";
                            sql = sql + " select CODE_ID,NAME,SORT,'Y' AS IS_SELECTED from CON_SHARECODE" + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_SKILL' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " AND CODE_ID IN (" + listData + ")" + "\n\r";
                            sql = sql + " ORDER BY SORT" + "\n\r";
                        }
                        else
                        {
                            sql = "select CODE_ID,NAME,SORT,'N' AS IS_SELECTED from CON_SHARECODE " + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_SKILL' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " ORDER BY SORT" + "\n\r";
                        }
                        break;
                    case "HOBBY":
                        if (listData != "")
                        {
                            sql = "select CODE_ID,NAME,SORT,'N' AS IS_SELECTED from CON_SHARECODE " + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_HOBBY' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " AND CODE_ID NOT IN (" + listData + ")" + "\n\r";
                            sql = sql + " union " + "\n\r";
                            sql = sql + " select CODE_ID,NAME,SORT,'Y' AS IS_SELECTED from CON_SHARECODE" + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_HOBBY' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " AND CODE_ID IN (" + listData + ")" + "\n\r";
                            sql = sql + " ORDER BY SORT" + "\n\r";
                        }
                        else
                        {
                            sql = "select CODE_ID,NAME,SORT,'N' AS IS_SELECTED from CON_SHARECODE " + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_HOBBY' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " ORDER BY SORT" + "\n\r";
                        }
                        break;
                    default:

                        break;
                }

                DataSet dsSharecode = this.ExecuteSql(sql, connection, transaction);

                //Indented縮排 將資料轉換成Json格式
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

        private void ucCON_SHARECODE_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucCON_SHARECODE.conn, ucCON_SHARECODE.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            var command = ucCON_SHARECODE.conn.CreateCommand();
            command.CommandText = "SELECT SCOPE_IDENTITY()";
            command.Transaction = ucCON_SHARECODE.trans;
            int newID = Convert.ToInt32(command.ExecuteScalar());

            var dataset = (DataSet)ucCON_SHARECODE.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_SHARECODE);
            var table = (string)ucCON_SHARECODE.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_SHARECODE);
            DataTable dt = ucCON_SHARECODE.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                if (dataset.Tables[table].Rows[i].RowState == DataRowState.Added)
                {
                    dataset.Tables[table].Rows[i]["CODE_ID"] = newID;
                    dataset.Tables[table].Rows[i]["CREATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["UPDATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["CREATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataset.Tables[table].Rows[i]["UPDATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                logInfo_CON_SHARECODE.Log(dataset.Tables[table].Rows[i], dt, ucCON_SHARECODE.conn, ucCON_SHARECODE.trans, ucCON_SHARECODE.SelectCmd.KeyFields);
            }
        }

        private void ucCON_SHARECODE_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucCON_SHARECODE.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_SHARECODE);
            var table = (string)ucCON_SHARECODE.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_SHARECODE);
            DataTable dt = ucCON_SHARECODE.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_SHARECODE.Log(dataset.Tables[table].Rows[i], dt, ucCON_SHARECODE.conn, ucCON_SHARECODE.trans, ucCON_SHARECODE.SelectCmd.KeyFields);
            }
        }

        private void ucCON_SHARECODE_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucCON_SHARECODE.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_SHARECODE);
            var table = (string)ucCON_SHARECODE.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_SHARECODE);
            DataTable dt = ucCON_SHARECODE.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_SHARECODE.Log(dataset.Tables[table].Rows[i], dt, ucCON_SHARECODE.conn, ucCON_SHARECODE.trans, ucCON_SHARECODE.SelectCmd.KeyFields);
            }
        }
    }
}
