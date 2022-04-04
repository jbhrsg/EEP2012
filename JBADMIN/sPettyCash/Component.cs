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


namespace sPettyCash
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

        private void ucPettyCash_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucPettyCash.SetFieldValue("CreateDate", DateTime.Now);
        }
        public object[] GetEmpFlowAgentList(object[] objParam)
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
                string Flow = parm[1];
                string sql = "SELECT dbo.funRetrunEmpFlowAgentList('" + UserID + "','" + Flow + "') AS ReturnStr FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = ds.Tables[0].Rows[0]["ReturnStr"].ToString(); ;

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
        public object[] GetUserOrgNO(object[] objParam)
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
                string sql = "SELECT dbo.funReturnEmpOrgNO('" + UserID + "') AS UserOrgNO FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
        public object[] GetUserOrgNOs(object[] objParam)
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
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent,dbo.funReturnEmpOrgNOL2CostCenter('" + UserID + "')  AS OrgCostCenter  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
        //public object IsSignWithNotes(object[] objParam)
        //{
        //    object[] ret = new object[] { 0, 0 };
        //    DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
        //    string PettyCashID = dr["PettyCashID"].ToString();
        //    //�إ߸�Ʈw�s��
        //    IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
        //    //��s�u���A������open�ɡA�}�ҳs��
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }
        //    //�}�ltransaction
        //    IDbTransaction transaction = connection.BeginTransaction();
        //    try
        //    {
        //        string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='PettyCashID=''" + PettyCashID + "'''";
        //        DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
        //        string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
        //        transaction.Commit();
        //        //Indented�Y�� �N����ഫ��Json�榡
        //        if (cnt == "0")
        //            ret[1] = false;//�~��y�{
        //        else
        //            ret[1] = true;
        //    }
        //    catch
        //    {
        //        transaction.Rollback();
        //    }
        //    finally
        //    {
        //        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
        //    }
        //    return ret; // �Ǧ^��: �L
        //}
        //public object[] GetSignCount(object[] objParam)
        //{
        //    object[] ret = new object[] { 0, 0 };
        //    IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }
        //    IDbTransaction transaction = connection.BeginTransaction();
        //    try
        //    {
        //        string[] parm = objParam[0].ToString().Split(',');
        //        string PettyCashID = parm[0];
        //        string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='PettyCashID=''" + PettyCashID + "'''";
        //        DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
        //        string cnt = dsTemp.Tables[0].Rows[0]["CNT"].ToString();
        //        if (cnt == "0")
        //            ret[1] = 0;
        //        else
        //            ret[1] = 1;
        //    }
        //    catch
        //    {
        //        transaction.Rollback();
        //    }
        //    finally
        //    {
        //        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
        //    }
        //    return ret;
        //}
       
    }
}
