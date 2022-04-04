using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data.SqlClient; 
using System.Data;
using System.Configuration;
using Newtonsoft;
using Newtonsoft.Json;
namespace sERPSalesApplyMaster
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
        //正常銷貨過帳到Njb
        public object PostToNjbDeposit(object[] objParam)
        {
            //取得使用者登入名稱
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            object[] res = SrvUtils.GetValue("_username", this);
            string username = res[1].ToString();
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0];
            string SalesApplyNO = dr["SalesApplyNO"].ToString();
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";
            conn = new SqlConnection(connetionString);
            //IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                //IDbTransaction trans = conn.BeginTransaction();
                //執行 JB-DB\SQL2008.NJB.JBADMIN.dbo.procPostToNjbDeposit 預存程序
                sql = "EXEC [60.250.52.106,1433].JBADMIN.dbo.procPostToNjbDeposit '" + SalesApplyNO + "','" + username + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //this.ExecuteSql(sql, conn,trans);
                //trans.Commit();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return ret;

        }
        //銷貨異常過帳到Njb
        public object PostToNjbExcept(object[] objParam)
        {
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sqlstr = null;
            object[] res = SrvUtils.GetValue("_username", this);
            string username = res[1].ToString();
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0];
            string SalesApplyNO = dr["SalesApplyNO"].ToString();
            //DbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";
            conn = new SqlConnection(connetionString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                //IDbTransaction trans = conn.BeginTransaction();
                sqlstr = "EXEC [60.250.52.106,1433].JBADMIN.dbo.procPostToNjbExcept '" + SalesApplyNO + "'";
                cmd = new SqlCommand(sqlstr, conn);
                cmd.ExecuteNonQuery();
                //this.ExecuteSql(sql, conn, trans);
                //trans.Commit();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return ret;

        }
        private void ucERPSalesApplyDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPSalesApplyDetails.SetFieldValue("CreateDate", DateTime.Now);
        }
        private void ucERPSalesApplyMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPSalesApplyMaster.SetFieldValue("CreateDate", DateTime.Now);
        }
        public object[] GetEmpFlowAgentList(object[] objParam)
        {
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
        public object[] GetUserOrgNOs(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
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
        //        string SalesApplyNO = parm[0];
        //        string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='SalesApplyNO=''" + SalesApplyNO + "'''";
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
