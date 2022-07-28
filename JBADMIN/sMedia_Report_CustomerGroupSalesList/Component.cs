using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;


namespace sMedia_Report_CustomerGroupSalesList
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
            string CustNO = parm[0];
            string SalesID = parm[1];
            string SalesDateFrom = parm[2];
            string SalesDateTo = parm[3];
            string SalesTypeID = parm[4];
            string DMTypeID = parm[5];
            string ViewAreaID = parm[6];
            string ReportType = parm[7];
            string NewsAreaID = parm[8];
            string LetterType = parm[9];

            if (SalesID != "")
            {
                string[] arrSalesID = SalesID.Split('*');
                SalesID = string.Join(",", arrSalesID);
            }
            if (SalesTypeID != "")
            {
                string[] arrSalesTypeID = SalesTypeID.Split('*');
                SalesTypeID = string.Join(",", arrSalesTypeID);
            }
            if (DMTypeID != "")
            {
                string[] arrDMTypeID = DMTypeID.Split('*');
                DMTypeID = string.Join(",", arrDMTypeID);
            }
            if (ViewAreaID != "")
            {
                string[] arrViewAreaID = ViewAreaID.Split('*');
                ViewAreaID = string.Join(",", arrViewAreaID);
            }
            if (NewsAreaID != "")
            {
                string[] arrNewsAreaID = NewsAreaID.Split('*');
                NewsAreaID = string.Join(",", arrNewsAreaID);
            }

            string sql = "";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                if (ReportType == "1")//客戶別銷貨明細表
                {
                    sql = "SELECT d.[CustNO],d.[SalesID],d.[DMTypeID],d.[SalesDate],d.[GrantTypeID]" + "\r\n";
                    sql = sql + ",d.[CustPrice],d.[SalesQty],d.[CustAmt],d.[Commission],m.CustShortName,a.ViewAreaName" + "\r\n";
sql = sql + ",case when c.IsPutInvoice=1 or c.IsPutPaperInvoice=1 then 0.05*d.[CustAmt] else 0 end as TaxAmount" + "\r\n";
sql = sql + ",case when c.IsPutInvoice=1 or c.IsPutPaperInvoice=1 then 1.05*d.[CustAmt] else d.[CustAmt] end as TotalAmount" + "\r\n";
sql = sql + ",id.InvoiceNO,id.InvoiceDate" + "\r\n";//發票號碼
                    sql = sql + "FROM [dbo].[ERPSalesDetails] d" + "\r\n";
                    sql = sql + "left join [dbo].[ERPSalesMaster] m on d.SalesMasterNO=m.SalesMasterNO" + "\r\n";
                    sql = sql + "left join [dbo].[ERPViewArea] a on d.ViewAreaID=a.ViewAreaID" + "\r\n";
                    sql = sql + "left join [dbo].[ERPCustomers] c on d.CustNO=c.CustNO" + "\r\n";
                    sql = sql + "left join (" + "\r\n";
                    sql = sql + "select id.*,sm.CustNO,sm.WantedInvoiceYM " + "\r\n";
sql = sql + "from [JBERP].[dbo].[InvoiceDetails] id" + "\r\n";
sql = sql + "left join [JBERP].[dbo].[SalesMaster] sm on sm.SalesNO=id.SalesNO" + "\r\n";
sql = sql + "where id.IsActive=1 and sm.CustNO is not null" + "\r\n";
sql = sql + ") id on d.ovInvoice=1 and id.CustNO=d.CustNO" + "\r\n";
sql = sql + "and id.WantedInvoiceYM=d.InvoiceYM and id.SalesTypeID=d.SalesTypeID and id.InvoiceNO=d.InvoiceNO" + "\r\n";
//left(CONVERT(nvarchar(10),id.InvoiceDate,111),7)
                    sql = sql + "where d.IsActive=1" + "\r\n";
                    if (CustNO != "")
                    {
                        sql = sql + "and m.CustNO ='" + CustNO + "'" + "\r\n";
                    }
                    if (SalesID != "")
                    {
                        sql = sql + "and d.SalesID in (" + SalesID + ") " + "\r\n";
                    }
                    if (SalesDateFrom != "")
                    {
                        sql = sql + "and d.SalesDate >='" + SalesDateFrom + "'" + "\r\n";
                    }
                    if (SalesDateTo != "")
                    {
                        sql = sql + "and d.SalesDate <='" + SalesDateTo + "'" + "\r\n";
                    }
                    if (SalesTypeID != "")
                    {
                        sql = sql + "and m.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";//d有SalesTypeID
                    }
                    if (DMTypeID != "")
                    {
                        sql = sql + "and m.DMTypeID in (" + DMTypeID + ") " + "\r\n";//d有DMTypeID,區域
                    }
                    if (ViewAreaID != "")
                    {
                        sql = sql + "and d.ViewAreaID in (" + ViewAreaID + ") " + "\r\n";
                    }
                    if (NewsAreaID != "")
                    {
                        sql = sql + "and d.NewsAreaID in (" + NewsAreaID + ") " + "\r\n";
                    }
                    if (LetterType != "")
                    {
                        sql = sql + "and c.LetterType = '" + LetterType + "'" + "\r\n";
                    }
                    sql = sql + "order by a.ViewAreaName,d.[CustNO],d.[DMTypeID],d.[SalesMasterNO],d.[SalesDate]" + "\r\n";
                }
                else if (ReportType == "2")//客戶別銷貨彙總表
                {

                    sql = sql + "SELECT m.SalesMasterNO,m.[CustNO],m.[CustShortName],m.[TotalSalesQty],m.[CustAmt]" + "\r\n";//,t.jb_name
                    sql = sql + ",id.InvoiceNO,id.InvoiceDate" + "\r\n";
                    sql = sql + "FROM [dbo].[ERPSalesMaster] m" + "\r\n";
                    sql = sql + "left join  [dbo].[ERPCustomers] c on m.CustNO=c.CustNO" + "\r\n";
                    //sql = sql + "left join  [dbo].[View_njbjb_type] t on c.IndustryID=t.jb_type" + "\r\n";
                    sql = sql + "left join  [dbo].[ERPSalesDetails] d on m.SalesMasterNO=d.SalesMasterNO" + "\r\n";
sql = sql + "left join (" + "\r\n";
sql = sql + "select id.*,sm.CustNO,sm.WantedInvoiceYM " + "\r\n";
sql = sql + "from [JBERP].[dbo].[InvoiceDetails] id" + "\r\n";
sql = sql + "left join [JBERP].[dbo].[SalesMaster] sm on sm.SalesNO=id.SalesNO" + "\r\n";
sql = sql + "where id.IsActive=1 and sm.CustNO is not null" + "\r\n";
sql = sql + ") id on d.ovInvoice=1 and id.CustNO=d.CustNO and id.WantedInvoiceYM=d.InvoiceYM and id.SalesTypeID=d.SalesTypeID and id.InvoiceNO=d.InvoiceNO" + "\r\n";
//left(CONVERT(nvarchar(10),id.InvoiceDate,111),7)=d.InvoiceYM
                    sql = sql + "where m.IsActive=1" + "\r\n";

                    if (CustNO != "")
                    {
                        sql = sql + "and m.CustNO ='" + CustNO + "'" + "\r\n";
                    }
                    if (SalesID != "")
                    {
                        sql = sql + "and d.SalesID in (" + SalesID + ") " + "\r\n";
                    }
                    if (SalesDateFrom != "")
                    {
                        sql = sql + "and d.SalesDate >='" + SalesDateFrom + "'" + "\r\n";
                    }
                    if (SalesDateTo != "")
                    {
                        sql = sql + "and d.SalesDate <='" + SalesDateTo + "'" + "\r\n";
                    }
                    if (SalesTypeID != "")
                    {
                        sql = sql + "and m.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
                    }
                    if (DMTypeID != "")
                    {
                        sql = sql + "and m.DMTypeID in (" + DMTypeID + ") " + "\r\n";
                    }
                    if (ViewAreaID != "")
                    {
                        sql = sql + "and d.ViewAreaID in (" + ViewAreaID + ") " + "\r\n";
                    }
                    if (NewsAreaID != "")
                    {
                        sql = sql + "and d.NewsAreaID in (" + NewsAreaID + ") " + "\r\n";
                    }
                    if (LetterType != "")
                    {
                        sql = sql + "and c.LetterType = '" + LetterType + "'" + "\r\n";
                    }
                    sql = sql + "group by m.SalesMasterNO,m.[CustNO],m.[CustShortName],m.[TotalSalesQty],m.[CustAmt]" + "\r\n";//,t.jb_name
                    sql = sql + ",id.InvoiceNO,id.InvoiceDate" + "\r\n";
                    //sql = sql + "order by  [SalesTypeID],t.[InvoiceTypeName],[SalesID]" + "\r\n";
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
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js };
        }
    }
}
