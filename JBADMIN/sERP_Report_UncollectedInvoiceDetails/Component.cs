using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_UncollectedInvoiceDetails
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

        public object[] GetReportUncollectedInvoiceDetails(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string InsGroupID = parm[0];
            string SalesTypeID = parm[1];
            string SalesID = parm[2];

            string SalesDateFrom = parm[3];
            string SalesDateTo = parm[4];
            string CustomerID = parm[5];
            string ARDate = parm[6];
            string ReportType = parm[7];
            string InvoiceDateFrom = parm[8];
            string InvoiceDateTo = parm[9];

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
                if (ReportType == "1" || ReportType == "2" || ReportType == "3" || ReportType=="4")
                {
                    sql = "SELECT  id.[InvoiceNO],id.[SalesNO],id.[InsGroupID],id.[SalesTypeID],id.[SalesDate],id.[InvoiceDate],id.[ARDate]" + "\r\n";
                    sql = sql + ",sm.[BalanceDate],sm.[DebtorDays],id.[SalesAmount],id.[SalesTax],id.[SalesTotal],id.[SalesID],sp.SalesName" + "\r\n";
                    sql = sql + ",id.[TaxRate],id.[Employer],id.[CustomerID],c.TelNO,c.ShortName+(case when id.Employer is not null and id.Employer!='' then'('+LEFT(c1.ShortName,2)+')' else'' end) as ShortName" + "\r\n";
                    sql = sql + ",id.[IsActive],id.[InvoiceTypeID]" + "\r\n";
                    sql = sql + ",id.[CreateBy],id.[CreateDate],id.[LastUpdateBy],id.[LastUpdateDate],sm.MailSend,pt.PayTypeName" + "\r\n";
                    sql = sql + ",ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    if (ARDate != "")
                    {
                        sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//此日截止時的收款金額
                    }
                    sql = sql + "),0) as AcceptedAmount" + "\r\n";
                    sql = sql + ",SalesTotal - ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    if (ARDate != "")
                    {
                        sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//此日截止時的未收款金額
                    }
                    sql = sql + "),0) as UncollectedAmount" + "\r\n";

                    if (ReportType == "4")
                    {
                        //橫的刊登日期(子查詢)
                        //sql = sql + ",(select convert(varchar,SalesDate,111)+'、' from JBADMIN.dbo.[ERPSalesDetails] " + "\r\n";
                        //sql = sql + "where ovInvoice=1 and CustNO=sm.CustNO and InvoiceYM=left(CONVERT(nvarchar(10),id.InvoiceDate,111),7)" + "\r\n";
                        //sql = sql + "and SalesTypeID=id.SalesTypeID FOR XML PATH('')) as MediaSalesDate" + "\r\n";
                        
                        //直的刊登日期(left join 媒體銷貨明細)
                        sql = sql + ",convert(varchar,msd.SalesDate,111) as MediaSalesDate" + "\r\n";
                    }

                    sql = sql + "FROM [InvoiceDetails] id" + "\r\n";
                    sql = sql + "left join Customer c on c.CustomerID=id.CustomerID" + "\r\n";
                    sql = sql + "left join Customer c1 on c1.CustomerID=id.Employer" + "\r\n";
                    sql = sql + "left join SalesMaster sm on sm.SalesNO=id.SalesNO" + "\r\n";
                    sql = sql + "left join SalesPerson sp on id.SalesID=sp.SalesID" + "\r\n";
sql = sql + "left join JBADMIN.dbo.ERPPayKind pk on pk.CustNO=sm.CustNO and id.SalesTypeID=pk.SalesTypeID" + "\r\n";
sql = sql + "left join JBADMIN.dbo.ERPPayType pt on pt.PayTypeID=pk.PayTypeID" + "\r\n";

                    //直的刊登日期
                    if (ReportType == "4")
                    {
                        sql = sql + "left join JBADMIN.dbo.[ERPSalesDetails] msd on msd.ovInvoice=1 and msd.CustNO=sm.CustNO" + "\r\n";
                        sql = sql + "and msd.InvoiceYM=sm.WantedInvoiceYM and msd.SalesTypeID=id.SalesTypeID and msd.InvoiceNO=id.InvoiceNO" + "\r\n";
                    }//left(CONVERT(nvarchar(10),id.InvoiceDate,111),7)

                    sql = sql + "where id.IsActive=1 " + "\r\n";
                    
                    if(ReportType=="4"){
                        sql = sql + "and sm.CustNO is not null ";
                    }

                    sql = sql + "and id.SalesTotal !=ISNULL(" + "\r\n";
                    sql = sql + "(select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    if (ARDate != "")
                    {
                        sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//此日截止時的未完全收到款的發票
                    }
                    sql = sql + "),0)" + "\r\n";

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
                    if (SalesDateFrom != "")
                    {
                        sql = sql + "and id.SalesDate >='" + SalesDateFrom + "'" + "\r\n";
                    }
                    if (SalesDateTo != "")
                    {
                        sql = sql + "and id.SalesDate <='" + SalesDateTo + "'" + "\r\n";
                    }
                    if (CustomerID != "")
                    {
                        sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (InvoiceDateFrom != "")
                    {
                        sql = sql + "and id.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
                    }
                    if (InvoiceDateTo != "")
                    {
                        sql = sql + "and id.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
                    }
                    //if (ARDate != "")
                    //{
                    //    sql = sql + "and id.InvoiceDate <= '" + ARDate + "'" + "\r\n";//發票日期
                    //}
                    if (ReportType == "1" )
                    {
                        sql = sql + "order by  id.[SalesID],id.[InvoiceDate],id.[InvoiceNO],id.[CustomerID]" + "\r\n";
                    }
                    else if (ReportType == "2")// || ReportType == "4"  橫的刊登日期
                    {
                        sql = sql + "order by  id.[SalesID],id.[CustomerID],id.[InvoiceDate],id.[InvoiceNO],id.[SalesDate]" + "\r\n";
                    }
                    else if (ReportType == "3")
                    {
                        sql = sql + "order by  id.[SalesID],id.[CustomerID],id.[ARDate]" + "\r\n";
                    }
                    else if (ReportType == "4")//直的刊登日期
                    {
                        sql = sql + "order by  id.[SalesID],id.[CustomerID],id.[InvoiceDate],id.[InvoiceNO],id.[SalesDate],MediaSalesDate" + "\r\n";
                    }
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
