using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sFWCRMOrdersYearUpdate
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
        //Flow修改訂單年度成功=>修改 FWCRMOrders => OrderYear     
        public object procUpdateOrderYear(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };                    

            DataRow drDara = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string OrderNo = drDara["OrderNo"].ToString();
            string OrderYearNew = drDara["OrderYearNew"].ToString();         

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
                var sql = "";              

                sql = "update FWCRMOrders set OrderYear= '" + OrderYearNew + "' where OrderNo='" + OrderNo + "'" + "\r\n";

                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit(); // 確認交易
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // 傳回值: 無

        }


    }
}
