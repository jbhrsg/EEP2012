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
using JBTool;
namespace sglYearBudget
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
        public object[] GetAccitemM(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string VoucherYear = parm[0];
                string CostCenterID = parm[1];
                string UserID = parm[2];
                string sql = "EXEC [dbo].[procReturnAccitemByCostCenter] '" + VoucherYear + "', '" + CostCenterID + "','" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js }; ;

        }
        public object[] GetCostCenter(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "EXEC [dbo].[procReturnCostCenterByUser]  '" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js }; ;
        }
        //依條件取得預算科目累計金額明細
        public object[] GetGridDataDynamic(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string BudgetYear = parm[0].ToString();
            string EndDate = parm[1].ToString();
            string CostCenterID = parm[2].ToString();
            string Acno = parm[3].ToString();
            string AcnoS = parm[4].ToString();
            string AcnoE = parm[5].ToString();
            string UserID = parm[6].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC dbo.procGetglYearBudgetDetails '" + BudgetYear + "','" + EndDate + "','" + CostCenterID + "','" + Acno + "','"+  AcnoS +"','"+ AcnoE +"','"+ UserID + "'";
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
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, true };
        }

        //媒體銷貨 => 匯出Excel
        public object[] GetGridDataDynamicExcel(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string BudgetYear = parm[0].ToString();
            string EndDate = parm[1].ToString();
            string CostCenterID = parm[2].ToString();
            string Acno = parm[3].ToString();
            string AcnoS = parm[4].ToString();
            string AcnoE = parm[5].ToString();
            string UserID = parm[6].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            var theResult = new Dictionary<string, object>();
            try
            {
                sql = "EXEC dbo.procGetglYearBudgetDetailsExcel '" + BudgetYear + "','" + EndDate + "','" + CostCenterID + "','" + Acno + "','" + AcnoS + "','" + AcnoE + "','" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));
                theResult.Add("IsOK", true);
                theResult.Add("Msg", "錯誤訊息");
                theResult.Add("FileName", "這是一個檔案.xls");
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
        }

        private void ucglYearBudget_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglYearBudget.SetFieldValue("CreateDate", DateTime.Now);
            ucglYearBudget.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucglYearBudget_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucglYearBudget.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        public object[] GetAuditorList(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string Category = parm[0];
                string sql = "SELECT CategoryValue FROM SYS_Variable WHERE Category='" + Category + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = ds.Tables[0].Rows[0]["CategoryValue"].ToString(); ;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js }; ;

        }
    }
     
}
