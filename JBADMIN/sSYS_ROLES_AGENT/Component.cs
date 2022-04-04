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

namespace sSYS_ROLES_AGENT
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

        private void ucSYS_ROLES_AGENT_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucSYS_ROLES_AGENT.SetFieldValue("CreateDate", DateTime.Now);
            ucSYS_ROLES_AGENT.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucSYS_ROLES_AGENT_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucSYS_ROLES_AGENT.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //���o�ϥΪ̨���M��
        public object[] GetEmpRoleStr(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpRoles(UserID) AS RoleStr FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = ds.Tables[0].Rows[0]["RoleStr"].ToString(); ;

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
        public object[] CheckRoleAgentFlow(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string ROLEID = parm[0];
                string AGENT = parm[1];
                string FLOW = parm[2];
                string sql = "SELECT COUNT(*) AS CNT FROM SYS_ROLES_AGENT WHERE ROLE_ID = '" + ROLEID + "' AND AGENT='"+AGENT+"' AND FLOW_DESC='"+FLOW+"'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

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
        }
    }
}
