using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_InvoiceDetails_WarrantDetails
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

        public object[] GetReportInvoiceDetails(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string CustomerID = parm[0];
            string SalesDateFrom = parm[1];
            string SalesDateTo = parm[2];
            string WarrantDateFrom = parm[3];
            string WarrantDateTo = parm[4];
            string SalesTypeID = parm[5];
            string SalesID = parm[6];
            string ReportType = parm[7];
            //string PayWayID = parm[8];
            string InsGroupID = parm[8];

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
            //if (PayWayID != "")
            //{
            //    string[] arrPayWayID = PayWayID.Split('*');
            //    PayWayID = string.Join(",", arrPayWayID);
            //}
            if (InsGroupID != "")
            {
                string[] arrInsGroupID = InsGroupID.Split('*');
                InsGroupID = string.Join(",", arrInsGroupID);
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
                sql = "SELECT id.[CustomerID],c.ShortName,id.[InvoiceNO],id.[SalesDate]" + "\r\n";
                sql = sql + ",id.[SalesTotal],id.[ARDate],id.[SalesID],id.[SalesTypeID]" + "\r\n";//,p.PayWayName//一個發票可對應很多收款單，收款方式也不一樣
                sql = sql + ",ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                if (WarrantDateFrom != "")
                {
                    sql = sql + "and wm.WarrantDate >= '" + WarrantDateFrom + "'" + "\r\n";//這區間的所收到的款
                }
                if (WarrantDateTo != "")
                {
                    sql = sql + "and wm.WarrantDate <= '" + WarrantDateTo + "'" + "\r\n";
                }
                sql = sql + "),0) as AcceptedAmount" + "\r\n";
                sql = sql + ",SalesTotal - ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                if (WarrantDateFrom != "")
                {
                    sql = sql + "and wm.WarrantDate >= '" + WarrantDateFrom + "'" + "\r\n";//這區間的未收到的款
                }
                if (WarrantDateTo != "")
                {
                    sql = sql + "and wm.WarrantDate <= '" + WarrantDateTo + "'" + "\r\n";
                }
                sql = sql + "),0) as UncollectedAmount" + "\r\n";
                sql = sql + "FROM [InvoiceDetails] id" + "\r\n";
                sql = sql + "left join Customer c on c.CustomerID=id.CustomerID" + "\r\n";
                //sql = sql + "left join WarrantDetails wd on wd.InvoiceNO=id.InvoiceNO" + "\r\n";
                //sql = sql + "left join WarrantMaster wm on wm.WarrantNO=wd.WarrantNO" + "\r\n";
                //sql = sql + "left join PayWay p on p.PayWayID=wm.PayWayID" + "\r\n";//一個發票可對應很多收款單，收款方式也不一樣
                sql = sql + "where id.IsActive=1 " + "\r\n";

                if (InsGroupID != "")
                {
                    sql = sql + "and id.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                }
                if (CustomerID != "")
                {
                    sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
                }
                if (SalesDateFrom != "")
                {
                    sql = sql + "and id.SalesDate >='" + SalesDateFrom + "'" + "\r\n";
                }
                if (SalesDateTo != "")
                {
                    sql = sql + "and id.SalesDate <='" + SalesDateTo + "'" + "\r\n";
                }

                //if (WarrantDateFrom != "")
                //{
                //    sql = sql + "and wm.WarrantDate >= '" + WarrantDateFrom + "'" + "\r\n";
                //}
                //if (WarrantDateTo != "")
                //{
                //    sql = sql + "and wm.WarrantDate <= '" + WarrantDateTo + "'" + "\r\n";
                //}

                if (SalesTypeID != "")
                {
                    sql = sql + "and id.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
                }
                if (SalesID != "")
                {
                    sql = sql + "and id.SalesID in (" + SalesID + ") " + "\r\n";
                }
                //if (PayWayID != "")
                //{
                //    sql = sql + "and wm.PayWayID in (" + PayWayID + ") " + "\r\n";
                //}
                if (ReportType == "2")//未收的發票明細
                {
                    sql = sql + "and id.SalesTotal !=ISNULL(" + "\r\n";
                    sql = sql + "(select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    if (WarrantDateFrom != "")
                    {
                        sql = sql + "and wm.WarrantDate >= '" + WarrantDateFrom + "'" + "\r\n";//這區間的收到的款
                    }
                    if (WarrantDateTo != "")
                    {
                        sql = sql + "and wm.WarrantDate <= '" + WarrantDateTo + "'" + "\r\n";
                    }
                    sql = sql + "),0)" + "\r\n";
                }
                if (ReportType == "3")//已收的發票明細
                {
                    sql = sql + "and id.SalesTotal =ISNULL(" + "\r\n";
                    sql = sql + "(select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    if (WarrantDateFrom != "")
                    {
                        sql = sql + "and wm.WarrantDate >= '" + WarrantDateFrom + "'" + "\r\n";//這區間的收到的款
                    }
                    if (WarrantDateTo != "")
                    {
                        sql = sql + "and wm.WarrantDate <= '" + WarrantDateTo + "'" + "\r\n";
                    }
                    sql = sql + "),0)" + "\r\n";
                }
                sql = sql + "order by  id.[CustomerID],id.[SalesDate]" + "\r\n";

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
