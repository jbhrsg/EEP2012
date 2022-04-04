using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;

namespace sCON_ACTIVITYMASTER
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
        //刪除
        public object[] DeleteActivityContact(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AUTOKEY = parm[0].ToString();
            string USERID = parm[1].ToString();
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
                string sql = "Exec procDeleteActivityContact '" + AUTOKEY + "','" + USERID + "'";
                this.ExecuteSql(sql, connection, transaction);
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
            return new object[] { 0, true };
        }

        private void ucCON_ACTIVITYMASTER_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_ACTIVITYMASTER.SetFieldValue("CREATE_DATE", DateTime.Now);
        }
    }
   
}
