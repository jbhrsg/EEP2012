using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_CashTransferWarrantDetails
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
            string CustomerID = parm[0];
            string WarrantDateFrom = parm[1];
            string WarrantDateTo = parm[2];
            string ReportType = parm[3];
            string InsGroupID = parm[4];

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
                if (ReportType == "1")
                {
                    sql = "select wm.CompanyCustomerID,c.ShortName,wm.WarrantDate,wd.WarrantNO,wd.ItemNO as WarrantItemNO,ISNULL(wd.[RecAmount],0) as SalesTotal" + "\r\n";//id.SalesTotal
                    sql = sql + ",wm.CashNO,wm.ItemNO as CashItemNO,id.InvoiceDate,wd.InvoiceNO" + "\r\n";
                    sql = sql + ",ISNULL(wd.[RecAmount],0)+ISNULL(wd.[OthAmount],0)+ISNULL(wd.[RebAmount],0)+ISNULL(wd.[RetAmount],0)+ISNULL(wd.[BadAmount],0) as AcceptedAmount" + "\r\n";
                    sql = sql + ",p.PayWayName,left(c.CustomerName,4) as CustomerName,c.TelNO,s.SalesTypeName" + "\r\n";
                    sql = sql + "from WarrantDetails wd" + "\r\n";
                    sql = sql + "left join InvoiceDetails id on id.InvoiceNO=wd.InvoiceNO" + "\r\n";
                    sql = sql + "left join WarrantMaster wm on wm.WarrantNO=wd.WarrantNO" + "\r\n";
                    sql = sql + "left join Customer c on c.CustomerID= wm.CompanyCustomerID" + "\r\n";
                    sql = sql + "left join PayWayForWarrant p on p.PayWayID=wm.PayWayID" + "\r\n";
                    sql = sql + "left join SalesType s on s.SalesTypeID=id.SalesTypeID" + "\r\n";
                    sql = sql + "where id.IsActive=1 and wm.PayWayID in ('2','1')" + "\r\n";
                    if (InsGroupID != "")
                    {
                        sql = sql + "and wm.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    }
                    if (CustomerID != "")
                    {
                        sql = sql + "and wm.CompanyCustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (WarrantDateFrom != "")
                    {
                        sql = sql + "and wm.WarrantDate >='" + WarrantDateFrom + "'" + "\r\n";
                    }
                    if (WarrantDateTo != "")
                    {
                        sql = sql + "and wm.WarrantDate <='" + WarrantDateTo + "'" + "\r\n";
                    }
                    sql = sql + "order by wm.CompanyCustomerID,wm.WarrantDate,wd.WarrantNO,wd.ItemNO" + "\r\n";
                }
                else if (ReportType == "2")
                {
                    sql = "select wm.CashNO,wm.ItemNO as CashItemNO,wm.CompanyCustomerID,c.ShortName,wm.WarrantDate,wd.WarrantNO,wd.ItemNO as WarrantItemNO,ISNULL(wd.[RecAmount],0) as SalesTotal" + "\r\n";//id.SalesTotal
                    sql = sql + ",id.InvoiceDate,wd.InvoiceNO" + "\r\n";
                    sql = sql + ",ISNULL(wd.[RecAmount],0)+ISNULL(wd.[OthAmount],0)+ISNULL(wd.[RebAmount],0)+ISNULL(wd.[RetAmount],0)+ISNULL(wd.[BadAmount],0) as AcceptedAmount" + "\r\n";
                    sql = sql + ",p.PayWayName,left(c.CustomerName,4) as CustomerName,c.TelNO,s.SalesTypeName" + "\r\n";
                    sql = sql + "from WarrantDetails wd" + "\r\n";
                    sql = sql + "left join InvoiceDetails id on id.InvoiceNO=wd.InvoiceNO" + "\r\n";
                    sql = sql + "left join WarrantMaster wm on wm.WarrantNO=wd.WarrantNO" + "\r\n";
                    sql = sql + "left join Customer c on c.CustomerID= wm.CompanyCustomerID" + "\r\n";
                    sql = sql + "left join PayWayForWarrant p on p.PayWayID=wm.PayWayID" + "\r\n";
                    sql = sql + "left join SalesType s on s.SalesTypeID=id.SalesTypeID" + "\r\n";
                    sql = sql + "where id.IsActive=1 and wm.PayWayID in ('2','1')" + "\r\n";
                    if (InsGroupID != "")
                    {
                        sql = sql + "and wm.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    }
                    if (CustomerID != "")
                    {
                        sql = sql + "and wm.CompanyCustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (WarrantDateFrom != "")
                    {
                        sql = sql + "and wm.WarrantDate >='" + WarrantDateFrom + "'" + "\r\n";
                    }
                    if (WarrantDateTo != "")
                    {
                        sql = sql + "and wm.WarrantDate <='" + WarrantDateTo + "'" + "\r\n";
                    }
                    sql = sql + "order by wm.CashNO,wm.ItemNO,wm.CompanyCustomerID,wm.WarrantDate,wd.WarrantNO,wd.ItemNO" + "\r\n";
                }
                
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
