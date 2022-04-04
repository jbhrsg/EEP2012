using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;

namespace sAssetQueryByID
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
        public object[] GetAssetDataByID(object[] objParam)
        {
            string sql = null;
            string js = string.Empty;
            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            IDbTransaction transaction = conn.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string AssetID = parm[0];
                sql = "EXEC DBO.procDispAssetDataByID '" + AssetID + "'";
                DataSet ds = this.ExecuteSql(sql, conn, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                //return new object[] { 0, false };
            }
            finally
            {
                //transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);

            }
            return new object[] { 0, js };
        }
    }
}
