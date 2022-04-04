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

namespace sERPSalesMan
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
        private void ucERPSalesMan_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPSalesMan.SetFieldValue("CreateDate", DateTime.Now);
            ucERPSalesMan.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucERPSalesMan_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucERPSalesMan.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
         public object[] CheckSalesID(object[] objParam)
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
                string SalesID = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM ERPSalesMan WHERE SalesID = '" + SalesID + "'";
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
      
    }
}
