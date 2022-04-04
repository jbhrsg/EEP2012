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
using System.IO;
using System.Xml;


namespace sJCS2EmpAttendance
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
        public object[] CheckEmpID(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JCS2");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string EmpID = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM JCS2.dbo.Employee WHERE EmpID = '" + EmpID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Newtonsoft.Json.Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection("JCS2", connection);
            }
            return new object[] { 0, js };
        }
        public object[] GetEmpListPeriod(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JCS2");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string DateS  = parm[0];
                string DateE  = parm[1];
                string sql = "  SELECT EMPID,NAMEC FROM Employee WHERE EmpID IN (SELECT EmpID FROM  EmpCards WHERE CalDate BETWEEN'"+ DateS + "' AND '" + DateE + "')";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Newtonsoft.Json.Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JCS2", connection);
            }
            return new object[] { 0, js }; ;

        }
        public object[] GetLastEmpID(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JCS2");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT isnull(right('000'+ltrim(str(Max(EmpID)+1,4)),3),'001') AS EmpID FROM Employee";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Newtonsoft.Json.Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JCS2", connection);
            }
            return new object[] { 0, js }; ;

        }


        private void ucEmployee_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucEmployee.SetFieldValue("CreateDate", DateTime.Now);
            ucEmployee.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
      
       
    }
}
