using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;

namespace sERPSalesPaper
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

        public object[] UpdateSalesPaperName(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string str = objParam[0].ToString();
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
                //string sql = "SELECT sum(AirfareVisafee) as TotalAFVF FROM JBADMIN.DBO.BizTravelDetails_Accom WHERE TvlNo='" + str + "'";
                //DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //string js = ds.Tables[0].Rows[0]["TotalAFVF"].ToString();
                //string sql1 = "UPDATE [JBADMIN].[dbo].ERPSalesPaper SET SalesType1_A=" + "20161006.png" + " WHERE PaperDate=" + "2016-10-06";
                string sql1 = "insert into [JBADMIN].[dbo].ERPSalesPaper (PaperDate,SalesType1_A) values ('2016-10-06','20161006.png');";
                this.ExecuteSql(sql1, connection, transaction);
                transaction.Commit();
                ret[1] = true;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;  
        
        }
    }
}
