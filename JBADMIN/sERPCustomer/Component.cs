using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using System.Data.SqlClient; 
using Newtonsoft;
using Newtonsoft.Json;
using System.Configuration;

namespace sERPCustomer
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
        private void ucERPPayKind_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            
            ucERPPayKind.SetFieldValue("CreateDate", DateTime.Now);
        }
        private void ucERPPayKind_BeforeUpdate(object sender, UpdateComponentBeforeInsertEventArgs e)
        {

            ucERPPayKind.SetFieldValue("UpateDate", DateTime.Now);
        }
        public object[] CheckCustNO(object[] objParam)
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
                string CustNO = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM [60.250.52.106,1433].JBADMIN.dbo.ERPCustomers WHERE CustNO = '" + CustNO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

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
        }
        //
        public object[] CheckDelCustNO(object[] objParam)
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
                string CustNO = objParam[0].ToString();
                string sql = "SELECT dbo.funReturnIsDeleteCust(CustNO) AS CNT FROM [60.250.52.106,1433].JBADMIN.dbo.ERPCustomers WHERE CustNO = '" + CustNO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
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
        }
        public object[] CheckPayKind(object[] objParam)
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
                string CustNO = parm[0];
                string SalesTypeID = parm[1];
                string PayTypeID = parm[2];
                string sql = "SELECT COUNT(*) AS CNT FROM [60.250.52.106,1433].JBADMIN.dbo.ERPPayKind WHERE CustNO = '" + CustNO + "' AND SalesTypeID='" + SalesTypeID + "' AND PayTypeID='" + PayTypeID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

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
        }
        //
        public object[]  procSyncContact(object[] objParam)
        {
            string [] parm = objParam[0].ToString().Split(',');
            string ConType = parm[0].ToString(); 
            string Conb = parm[1].ToString();
            string CustNO = parm[2].ToString();
            string Center_ID = parm[3].ToString();
            string UserID = parm[4].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procSyncContact '" + ConType + "','" + Conb + "','" + CustNO + "','" + Center_ID + "','" + UserID + "'"; 
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

        private void ucCustNotes_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCustNotes.SetFieldValue("CreateDate", DateTime.Now);
        }
        //新增客戶備註
        public object[] procAddERPCustomerToDoNotes(object[] objParam)
        {
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            string[] parm = objParam[0].ToString().Split('*');
            string CustNO = parm[0].ToString();
            string NextCallDate = parm[1].ToString();
            string NextCallTime = parm[2].ToString();
            string UserName = parm[3].ToString();
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";
            conn = new SqlConnection(connetionString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                sql = "EXEC [60.250.52.106,1433].JBADMIN.dbo.procInsertERPCustomerToDoNotes '" + CustNO + "','" + NextCallDate + "','" + NextCallTime + "','" + UserName + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, true };
        }
     }
}
