using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient; 

namespace sPO_Setting_Item
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

        //判斷類別名稱是否重複
        public object[] CheckDuplicate_ItemTypeName(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string ItemTypeName = aParam[0].Trim();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  [ItemTypeName] FROM [ItemType] where ItemTypeName='" + ItemTypeName + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js };
        }

        //判斷物品名稱是否重複
        public object[] CheckDuplicate_ItemName(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string ItemName = aParam[0].Trim();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  [ItemName] FROM [Item] where ItemName='" + ItemName + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js };
        }

        //傳回該類別的物品名稱(判斷該類有無物品)
        public object[] CheckItems(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string ItemTypeID = aParam[0].Trim();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  [ItemID] FROM [Item] where ItemTypeID='" + ItemTypeID + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js };
        }
        public object[] procInsertItemTypeAcnoByMany(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string ItemTypeID = parm[0].ToString();
            string UserID = parm[1].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC dbo.procInsertItemTypeAcnoByMany '" + ItemTypeID + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented縮排 將資料轉換成Json格式
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
        public object[] procSyncglYearBudgetBase(object[] objParam)
        {
            SqlCommand cmd;
            SqlConnection conn;
            string connectionstr = null;
            string sql = null;
            string[] parm = objParam[0].ToString().Split(',');
            string CostCenterID = parm[0].ToString();
            string AcSubno = parm[1].ToString();
            string AcnoName = parm[2].ToString();
            string BudgetType = parm[3].ToString();
            string Acno_S = parm[4].ToString();
            string SubAcno_S = parm[5].ToString();
            string UserID = parm[6].ToString();
            conn = new SqlConnection(connectionstr);
            IDbConnection connection = (IDbConnection)AllocateConnection(("JBADMIN"));
            string js = string.Empty;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC JBADMIN.dbo.procSyncglYearBudgetBase '" + CostCenterID + "','" + AcSubno + "','" + AcnoName + "','" + BudgetType + "','"+Acno_S +"','"+SubAcno_S+"','"+UserID+"'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
                ReleaseConnection("JBADMIN", conn);
            }
            return new object[] { 0, js };
        }
        //檢查物品類別+成本中心+會科代號是否重複
        public object[] CheckItemCostAcSubno(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string ItemTypeNO = parm[0];
                string CostCenterID = parm[1];
                string AcSubno = parm[2];
                string UserID = parm[3];
                string sql = "Select Top 1 ISNULL(dbo.funReturnIsExistItemTypeCostCenterIDAcno('" + ItemTypeNO + "','" + CostCenterID + "','" + AcSubno + "'),0) AS STR FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                js = dsTemp.Tables[0].Rows[0]["STR"].ToString();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };

        }

        private void ucItemTypeAcno_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucItemTypeAcno.SetFieldValue("CreateDate", DateTime.Now);
        }
    }
}
