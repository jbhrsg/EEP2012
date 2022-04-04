using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERPInvoiceEmail
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
        public object[] GetInvoiceByInvoiceNO(object[] objParam)
        {
            string js = "";
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceNO = parm[0];
            string sql = "";
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "SELECT  * " + "\r\n";
                sql = sql + "From View_ERPInvoiceEmail" + "\r\n";
                sql = sql + "Where InvoiceNO = '" + InvoiceNO + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
        //寫入電子發票發送紀錄
        public object[] CreateInvoiceLogs(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceNO = parm[0].ToString();
            string UserID = parm[1].ToString();
            string ToEmail = parm[2].ToString();
            string ToName = parm[3].ToString();
            string MessStr = parm[4].ToString(); //
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procCreateInvoiceLogs '" + InvoiceNO + "','" + UserID + "','" + ToEmail + "','" + ToName + "','" + MessStr + "'";
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
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, true };
        }
        public object[] GetGridDataEmailLogs(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceNO = parm[0].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC JBERP.dbo.procInvoiceEmailLogs '" + InvoiceNO + "'";
                DataTable dt = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = Newtonsoft.Json.JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
    }
}
