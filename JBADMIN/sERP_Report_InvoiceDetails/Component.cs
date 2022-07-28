using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_InvoiceDetails
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
            string InsGroupID = parm[0];
            string SalesTypeID = parm[1];
            string SalesID = parm[2];

            string InvoiceDateFrom = parm[3];
            string InvoiceDateTo = parm[4];
            string CustomerID = parm[5];
            string InvoiceTypeID = parm[6];
            string ReportType = parm[7];

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
                if (ReportType == "1")
                {
                    sql = "SELECT st.SalesTypeID,st.SalesTypeID+'-'+st.SalesTypeName as SalesTypeName,t.InvoiceTypeName,sp.SalesName as [SalesID],i.[InvoiceNO],i.[InvoiceDate] ,i.[CustomerID],c.ShortName" + "\r\n";
                    sql = sql + ",i.[SalesAmount],i.[SalesTax],i.[SalesTotal] FROM [InvoiceDetails] i" + "\r\n";
                    sql = sql + " left join [Customer] c on c.CustomerID=i.CustomerID" + "\r\n";
                    sql = sql + " left join [InvoiceType] t on t.InvoiceTypeID=i.QInvoiceType" + "\r\n";
                    sql = sql + " left join [SalesType] st on st.SalesTypeID=i.[SalesTypeID]" + "\r\n";
                    sql = sql + " left join [SalesPerson] sp on sp.SalesID=i.[SalesID]" + "\r\n";
                    sql = sql + "where i.[IsActive]=1" + "\r\n";
                    if (InsGroupID != "")
                    {
                        sql = sql + "and i.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    }
                    if (SalesTypeID != "")
                    {
                        sql = sql + "and i.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
                    }
                    if (SalesID != "")
                    {
                        sql = sql + "and i.SalesID in (" + SalesID + ")" + "\r\n";
                    }
                    if (InvoiceDateFrom != "")
                    {
                        sql = sql + "and i.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
                    }
                    if (InvoiceDateTo != "")
                    {
                        sql = sql + "and i.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
                    }
                    if (CustomerID != "")
                    {
                        sql = sql + "and i.CustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (InvoiceTypeID != "")
                    {
                        sql = sql + "and i.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
                    }
                    sql = sql + "order by  [SalesTypeID],t.[InvoiceTypeName],[SalesID],i.[InvoiceNO] ,i.[InvoiceDate] ,i.[CustomerID]" + "\r\n";
                }
                else if (ReportType == "2")
                {
                    //sql = sql + "SELECT st.SalesTypeName as [SalesTypeID],t.InvoiceTypeName,sp.SalesName as [SalesID],nt.NewsTypeName,sum(i.[SalesAmount]) as [SalesAmountSum]," + "\r\n";
                    //sql = sql + "sum(i.[SalesTax]) as [SalesTaxSum],sum(i.[SalesTotal]) as [SalesTotalSum] " + "\r\n";
                    //sql = sql + "FROM [InvoiceDetails] i left join [InvoiceType] t on t.InvoiceTypeID=i.QInvoiceType " + "\r\n";
                    //sql = sql + "left join [SalesType] st on st.SalesTypeID=i.[SalesTypeID]" + "\r\n";
                    //sql = sql + "left join [SalesPerson] sp on sp.SalesID=i.[SalesID]" + "\r\n";
                    //sql = sql + "left join [SalesMaster] sm on sm.SalesNO=i.[SalesNO]" + "\r\n";
                    //sql = sql + "left join [JBADMIN].[dbo].[ERPNewsType] nt on nt.NewsTypeID=sm.NewsTypeID" + "\r\n";
                    //sql = sql + "where i.[IsActive]=1" + "\r\n";
                    //if (InsGroupID != "")
                    //{
                    //    sql = sql + "and i.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    //}
                    //if (SalesTypeID != "")
                    //{
                    //    sql = sql + "and i.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
                    //}
                    //if (SalesID != "")
                    //{
                    //    sql = sql + "and i.SalesID in (" + SalesID + ")" + "\r\n";
                    //}
                    //if (InvoiceDateFrom != "")
                    //{
                    //    sql = sql + "and i.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
                    //}
                    //if (InvoiceDateTo != "")
                    //{
                    //    sql = sql + "and i.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
                    //}
                    //if (CustomerID != "")
                    //{
                    //    sql = sql + "and i.CustomerID = '" + CustomerID + "'" + "\r\n";
                    //}
                    //if (InvoiceTypeID != "")
                    //{
                    //    sql = sql + "and i.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
                    //}
                    //sql = sql + "group by st.SalesTypeName,t.InvoiceTypeName,sp.SalesName,nt.NewsTypeName" + "\r\n";
                    //sql = sql + "order by  [SalesTypeID],t.[InvoiceTypeName],[SalesID],[NewsTypeName]" + "\r\n";

sql = sql + "select * from(" + "\r\n";
//依銷貨類別、單據別、業務、報別做彙總(報紙)(三聯)
sql = sql + "SELECT" + "\r\n";
sql = sql + "st.SalesTypeID,st.SalesTypeID+'-'+st.SalesTypeName as SalesTypeName,t.InvoiceTypeName,sp.SalesName as [SalesID]" + "\r\n";
sql = sql + ",nt.NewsTypeName,sum(esd.SumCustAmt) as SalesAmountSum,sum(SumCustTax) as SalesTaxSum,sum(esd.SumCustAmt)+sum(SumCustTax) as SalesTotalSum" + "\r\n";//ROUND((sum(esd.SumCustAmt)*0.05),0)
sql = sql + "FROM [InvoiceDetails] i " + "\r\n";
sql = sql + "left join [InvoiceType] t on t.InvoiceTypeID=i.QInvoiceType" + "\r\n";
sql = sql + "left join [SalesType] st on st.SalesTypeID=i.[SalesTypeID]" + "\r\n";
sql = sql + "left join [SalesPerson] sp on sp.SalesID=i.[SalesID]" + "\r\n";
sql = sql + "left join [SalesMaster] sm on sm.SalesNO=i.[SalesNO]" + "\r\n";

sql = sql + "left join(" + "\r\n";//銷貨類別、客戶、發票年月、報別做彙總
sql = sql + "select SalesTypeID,CustNO,InvoiceYM,sum(CustAmt) as SumCustAmt,ROUND(sum(CustAmt)*0.05,0) as SumCustTax ,NewsTypeID" + "\r\n";
sql = sql + "from(" + "\r\n";
sql = sql + "select esd.SalesTypeID,esd.CustNO,esd.InvoiceYM,esd.NewsTypeID,esd.CustAmt,esd.SalesMasterNO" + "\r\n";
sql = sql + "from [JBADMIN].[dbo].[ERPSalesDetails] esd" + "\r\n";
sql = sql + "where  ovInvoice='1'" + "\r\n";
sql = sql + ") temp" + "\r\n";
sql = sql + "group by SalesTypeID,CustNO,InvoiceYM,NewsTypeID" + "\r\n";
sql = sql + ") esd on esd.InvoiceYM=i.InvoiceYM and esd.SalesTypeID=i.SalesTypeID and esd.CustNO=sm.CustNO" + "\r\n";//目前只假設同一家客戶在同個月份只開一種單據(二聯or三聯or收據)

sql = sql + "left join [JBADMIN].[dbo].[ERPNewsType] nt on nt.NewsTypeID=esd.NewsTypeID" + "\r\n";
sql = sql + "where i.[IsActive]=1 and i.SalesTypeID ='6' and i.QInvoiceType ='98'" + "\r\n";

if (InsGroupID != "")
{
    sql = sql + "and i.InsGroupID in (" + InsGroupID + ") " + "\r\n";
}
if (SalesTypeID != "")
{
    sql = sql + "and i.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
}
if (SalesID != "")
{
    sql = sql + "and i.SalesID in (" + SalesID + ")" + "\r\n";
}
if (InvoiceDateFrom != "")
{
    sql = sql + "and i.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
}
if (InvoiceDateTo != "")
{
    sql = sql + "and i.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
}
if (CustomerID != "")
{
    sql = sql + "and i.CustomerID = '" + CustomerID + "'" + "\r\n";
}
if (InvoiceTypeID != "")
{
    sql = sql + "and i.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
}
sql = sql + "group by st.SalesTypeID,st.SalesTypeName,t.InvoiceTypeName,sp.SalesName,nt.NewsTypeName" + "\r\n";

sql = sql + "union" + "\r\n";

//依銷貨類別、單據別、業務、報別做彙總(報紙)(二聯)
sql = sql + "SELECT" + "\r\n";
sql = sql + "st.SalesTypeID,st.SalesTypeID+'-'+st.SalesTypeName as SalesTypeName,t.InvoiceTypeName,sp.SalesName as [SalesID]" + "\r\n";
sql = sql + ",nt.NewsTypeName,sum(esd.SumCustAmt) as SalesAmountSum,0 as SalesTaxSum,sum(esd.SumCustAmt) as SalesTotalSum" + "\r\n";
sql = sql + "FROM [InvoiceDetails] i " + "\r\n";
sql = sql + "left join [InvoiceType] t on t.InvoiceTypeID=i.QInvoiceType" + "\r\n";
sql = sql + "left join [SalesType] st on st.SalesTypeID=i.[SalesTypeID]" + "\r\n";
sql = sql + "left join [SalesPerson] sp on sp.SalesID=i.[SalesID]" + "\r\n";
sql = sql + "left join [SalesMaster] sm on sm.SalesNO=i.[SalesNO]" + "\r\n";

sql = sql + "left join(" + "\r\n";//銷貨類別、客戶、發票年月、報別做彙總
sql = sql + "select SalesTypeID,CustNO,InvoiceYM,sum(CustAmt) as SumCustAmt,NewsTypeID" + "\r\n";
sql = sql + "from(" + "\r\n";
sql = sql + "select esd.SalesTypeID,esd.CustNO,esd.InvoiceYM,esd.NewsTypeID,esd.CustAmt,esd.SalesMasterNO" + "\r\n";
sql = sql + "from [JBADMIN].[dbo].[ERPSalesDetails] esd" + "\r\n";
sql = sql + "where  ovInvoice='1'" + "\r\n";
sql = sql + ") temp" + "\r\n";
sql = sql + "group by SalesTypeID,CustNO,InvoiceYM,NewsTypeID" + "\r\n";
sql = sql + ") esd on esd.InvoiceYM=i.InvoiceYM and esd.SalesTypeID=i.SalesTypeID and esd.CustNO=sm.CustNO" + "\r\n";//目前只假設同一家客戶在同個月份只開一種單據(二聯or三聯or收據)。目前發票join媒體銷貨明細只能靠這三個欄位

sql = sql + "left join [JBADMIN].[dbo].[ERPNewsType] nt on nt.NewsTypeID=esd.NewsTypeID" + "\r\n";
sql = sql + "where i.[IsActive]=1 and i.SalesTypeID ='6' and i.QInvoiceType ='99'" + "\r\n";

if (InsGroupID != "")
{
    sql = sql + "and i.InsGroupID in (" + InsGroupID + ") " + "\r\n";
}
if (SalesTypeID != "")
{
    sql = sql + "and i.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
}
if (SalesID != "")
{
    sql = sql + "and i.SalesID in (" + SalesID + ")" + "\r\n";
}
if (InvoiceDateFrom != "")
{
    sql = sql + "and i.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
}
if (InvoiceDateTo != "")
{
    sql = sql + "and i.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
}
if (CustomerID != "")
{
    sql = sql + "and i.CustomerID = '" + CustomerID + "'" + "\r\n";
}
if (InvoiceTypeID != "")
{
    sql = sql + "and i.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
}
sql = sql + "group by st.SalesTypeID,st.SalesTypeName,t.InvoiceTypeName,sp.SalesName,nt.NewsTypeName" + "\r\n";

sql = sql + "union" + "\r\n";

//依銷貨類別、單據別、業務、報別做彙總(報紙)(收據)
sql = sql + "SELECT" + "\r\n";
sql = sql + "st.SalesTypeID,st.SalesTypeID+'-'+st.SalesTypeName as SalesTypeName,t.InvoiceTypeName,sp.SalesName as [SalesID],nt.NewsTypeName,sum(i.[SalesAmount]) as [SalesAmountSum]," + "\r\n";
sql = sql + "sum(i.[SalesTax]) as [SalesTaxSum],sum(i.[SalesTotal]) as [SalesTotalSum]" + "\r\n";
sql = sql + "FROM [InvoiceDetails] i " + "\r\n";
sql = sql + "left join [InvoiceType] t on t.InvoiceTypeID=i.QInvoiceType" + "\r\n";
sql = sql + "left join [SalesType] st on st.SalesTypeID=i.[SalesTypeID]" + "\r\n";
sql = sql + "left join [SalesPerson] sp on sp.SalesID=i.[SalesID]" + "\r\n";
sql = sql + "left join [SalesMaster] sm on sm.SalesNO=i.[SalesNO]" + "\r\n";
sql = sql + "left join [JBADMIN].[dbo].[ERPNewsType] nt on nt.NewsTypeID=sm.NewsTypeID" + "\r\n";
sql = sql + "where i.[IsActive]=1 and i.QInvoiceType='97' and i.SalesTypeID ='6'" + "\r\n";

if (InsGroupID != "")
{
    sql = sql + "and i.InsGroupID in (" + InsGroupID + ") " + "\r\n";
}
if (SalesTypeID != "")
{
    sql = sql + "and i.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
}
if (SalesID != "")
{
    sql = sql + "and i.SalesID in (" + SalesID + ")" + "\r\n";
}
if (InvoiceDateFrom != "")
{
    sql = sql + "and i.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
}
if (InvoiceDateTo != "")
{
    sql = sql + "and i.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
}
if (CustomerID != "")
{
    sql = sql + "and i.CustomerID = '" + CustomerID + "'" + "\r\n";
}
if (InvoiceTypeID != "")
{
    sql = sql + "and i.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
}

sql = sql + "group by st.SalesTypeID,st.SalesTypeName,t.InvoiceTypeName,sp.SalesName,nt.NewsTypeName" + "\r\n";

sql = sql + "union" + "\r\n";

//依銷貨類別、單據別、業務、報別做彙總(其他銷貨類別)
sql = sql + "SELECT" + "\r\n";
sql = sql + "st.SalesTypeID,st.SalesTypeID+'-'+st.SalesTypeName as SalesTypeName,t.InvoiceTypeName,sp.SalesName as [SalesID],nt.NewsTypeName,sum(i.[SalesAmount]) as [SalesAmountSum]," + "\r\n";
sql = sql + "sum(i.[SalesTax]) as [SalesTaxSum],sum(i.[SalesTotal]) as [SalesTotalSum]" + "\r\n";
sql = sql + "FROM [InvoiceDetails] i " + "\r\n";
sql = sql + "left join [InvoiceType] t on t.InvoiceTypeID=i.QInvoiceType" + "\r\n";
sql = sql + "left join [SalesType] st on st.SalesTypeID=i.[SalesTypeID]" + "\r\n";
sql = sql + "left join [SalesPerson] sp on sp.SalesID=i.[SalesID]" + "\r\n";
sql = sql + "left join [SalesMaster] sm on sm.SalesNO=i.[SalesNO]" + "\r\n";
sql = sql + "left join [JBADMIN].[dbo].[ERPNewsType] nt on nt.NewsTypeID=sm.NewsTypeID" + "\r\n";
sql = sql + "where i.[IsActive]=1 and i.SalesTypeID !='6'" + "\r\n";

if (InsGroupID != "")
{
    sql = sql + "and i.InsGroupID in (" + InsGroupID + ") " + "\r\n";
}
if (SalesTypeID != "")
{
    sql = sql + "and i.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
}
if (SalesID != "")
{
    sql = sql + "and i.SalesID in (" + SalesID + ")" + "\r\n";
}
if (InvoiceDateFrom != "")
{
    sql = sql + "and i.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
}
if (InvoiceDateTo != "")
{
    sql = sql + "and i.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
}
if (CustomerID != "")
{
    sql = sql + "and i.CustomerID = '" + CustomerID + "'" + "\r\n";
}
if (InvoiceTypeID != "")
{
    sql = sql + "and i.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
}

sql = sql + "group by st.SalesTypeID,st.SalesTypeName,t.InvoiceTypeName,sp.SalesName,nt.NewsTypeName" + "\r\n";

sql = sql + ") UnionTable" + "\r\n";

sql = sql + "order by  [SalesTypeID],[InvoiceTypeName],[SalesID],[NewsTypeName]" + "\r\n";

                }
                else if (ReportType == "3")
                {
                    sql = sql + "SELECT st.SalesTypeID,st.SalesTypeID+'-'+st.SalesTypeName as SalesTypeName,sum([SalesAmount]) as [SalesAmountSum]," + "\r\n";
                    sql = sql + "sum([SalesTax]) as [SalesTaxSum],sum([SalesTotal]) as [SalesTotalSum] " + "\r\n";
                    sql = sql + "FROM [InvoiceDetails] i" + "\r\n";
                    sql = sql + "left join [SalesType] st on st.SalesTypeID=i.[SalesTypeID]" + "\r\n";
                    sql = sql + "where [IsActive]=1" + "\r\n";
                    if (InsGroupID != "")
                    {
                        sql = sql + "and i.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    }
                    if (SalesTypeID != "")
                    {
                        sql = sql + "and i.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
                    }
                    if (SalesID != "")
                    {
                        sql = sql + "and i.SalesID in (" + SalesID + ")" + "\r\n";
                    }
                    if (InvoiceDateFrom != "")
                    {
                        sql = sql + "and i.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
                    }
                    if (InvoiceDateTo != "")
                    {
                        sql = sql + "and i.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
                    }
                    if (CustomerID != "")
                    {
                        sql = sql + "and i.CustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (InvoiceTypeID != "")
                    {
                        sql = sql + "and i.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
                    }
                    sql = sql + "group by st.SalesTypeID,st.SalesTypeName" + "\r\n";
                    sql = sql + "order by  [SalesTypeID]" + "\r\n";
                }
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch(Exception ex)
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
