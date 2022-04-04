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
namespace sAssetMaster
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
        private void ucAssetMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucAssetMaster.SetFieldValue("CreateDate", DateTime.Now);
            ucAssetMaster.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucAssetMaster_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucAssetMaster.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucAssetBelongLogs_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucAssetBelongLogs.SetFieldValue("CreateDate", DateTime.Now);
            ucAssetBelongLogs.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucAssetBelongLogs_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucAssetBelongLogs.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //取得最新資產編號
        public object[] GetMaxAssetID(object[] objParam)
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
                string AutoID = "AssetID";
                string sql = "SELECT dbo.funReturnMaxAssetID() AS ReturnStr FROM EIPHRSYS.DBO.USERS WHERE USERID='" + UserID + "'";
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

        public object[] procPutAssetInventoryLogs(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AssetID = parm[0];
            string IsInventory = parm[1];
            string UserName = parm[2];
            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            IDbTransaction trans = conn.BeginTransaction();
            try
            {
                string sql = "EXEC procPutAssetInventoryLogs '" + AssetID + "','" + IsInventory + "','" + UserName + "'";
                this.ExecuteSql(sql, conn, trans);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();

            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, "" }; //必須有回傳值,且必須為Object 
        }
        public object[] procDeleteAssetMaster(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AssetID = parm[0];
            string UserName = parm[1];

            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            IDbTransaction trans = conn.BeginTransaction();
            try
            {
                string sql = "UPDATE  AssetMaster Set IsActive=0,LastUpdateBy=" + "'" + UserName + "'" + ",LastUpdateDate=Getdate() WHERE AssetID='" + AssetID + "'";
                this.ExecuteSql(sql, conn, trans);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();

            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, "" }; //必須有回傳值,且必須為Object 
        }
        //標註物品資產已貼標籤
        public object[] procAddAssetIDLabel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string AssetIDStr = parm[0].ToString();
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
                string sql = "EXEC procAddAssetIDLabel '" + AssetIDStr + "','" + UserID + "'";
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

        private void ucItemMaintDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucItemMaintDetails.SetFieldValue("CreateDate", DateTime.Now);
        }
   }
}
