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
        //�R��
        public object[] DeleteActivityContact(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AUTOKEY = parm[0].ToString();
            string USERID = parm[1].ToString();
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
