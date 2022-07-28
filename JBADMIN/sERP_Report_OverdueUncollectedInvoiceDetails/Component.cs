using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_OverdueUncollectedInvoiceDetails
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

        public object[] GetReportData(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string InsGroupID = parm[0];
            string SalesTypeID = parm[1];
            string SalesID = parm[2];
            string CustomerID = parm[3];
            string ARDate = parm[4];

            if (InsGroupID != "")
            {
                string[] arrInsGroupID = InsGroupID.Split('*');
                InsGroupID = string.Join(",", arrInsGroupID);
            }
            if (SalesTypeID != "")
            {
                string[] arrSalesTypeID = SalesTypeID.Split('*');
                SalesTypeID = string.Join(",", arrSalesTypeID);
            }
            if (SalesID != "")
            {
                string[] arrSalesID = SalesID.Split('*');
                SalesID = string.Join(",", arrSalesID);
            }

            string sql = "";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = "SELECT  sp.SalesName as [SalesID],id.[CustomerID]" + "\r\n";
                    sql = sql + ",c.ShortName,id.InvoiceDate,id.[InvoiceNO],id.[SalesDate],id.[ARDate],id.[SalesTotal]" + "\r\n";
                    sql = sql + ",ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 where d1.InvoiceNO=id.InvoiceNO),0) as AcceptedAmount" + "\r\n";
                    sql = sql + ",SalesTotal - ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 where d1.InvoiceNO=id.InvoiceNO),0) as UncollectedAmount" + "\r\n";
                    sql = sql + ",id.[SalesTypeID],id.[InsGroupID],'' as TrackNote,c.TelNO" + "\r\n";
                    sql = sql + "FROM [InvoiceDetails] id" + "\r\n";
                    sql = sql + "left join Customer c on c.CustomerID=id.CustomerID" + "\r\n";
                    sql = sql + "left join [SalesPerson] sp on sp.SalesID=id.[SalesID]" + "\r\n";
                    sql = sql + "where id.IsActive=1 and id.SalesTotal !=ISNULL(" + "\r\n";
                    sql = sql + "(select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 where d1.InvoiceNO=id.InvoiceNO),0)" + "\r\n";

                    if (InsGroupID != "")
                    {
                        sql = sql + "and id.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    }
                    if (SalesTypeID != "")
                    {
                        sql = sql + "and id.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
                    }
                    if (SalesID != "")
                    {
                        sql = sql + "and id.SalesID in (" + SalesID + ")" + "\r\n";
                    }
                    if (CustomerID != "")
                    {
                        sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (ARDate != "")
                    {
                        sql = sql + "and id.ARDate <= '" + ARDate + "'" + "\r\n";
                    }
                    sql = sql + "order by  id.[SalesID],id.[CustomerID],id.[InvoiceDate],id.[InvoiceNO]" + "\r\n";
                    

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
    }
}
