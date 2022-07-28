using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_PrintReceipt
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

        public object[] GetReceiptFromInvoiceDetails(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string SalesTypeID = parm[0];


            string InvoiceDateFrom = parm[1];
            string InvoiceDateTo = parm[2];
            string CustomerID = parm[3];
            string InvoiceNO = parm[4];
            string SalesID = parm[5];

            if (SalesTypeID != "")
            {
                string[] arrSalesTypeID = SalesTypeID.Split('*');
                SalesTypeID = string.Join(",", arrSalesTypeID);
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
//sql = sql + "select SalesDateList" + "\r\n";

//sql = sql + ",NewsTypeName,NewsAreaName,InvoiceDate" + "\r\n";

//sql = sql + ",sum(CustAmt) as OfficeAmtSum,CustomerID,CustomerName,Addr_Desc,TaxNO,SalesTotal,TelNO,InvoiceNO,SalesTypeName from(" + "\r\n";
//sql = sql + "select " + "\r\n";
//sql = sql + "STUFF((SELECT ',' + right(convert(nvarchar(10),et.SalesDate,111),5)" + "\r\n";
//sql = sql + "FROM JBADMIN.dbo.[ERPSalesDetails] et" + "\r\n";
//sql = sql + "WHERE et.CustNO=vc.CustNO and et.SalesTypeID=id.SalesTypeID and et.InvoiceYM=id.InvoiceYM and et.ovInvoice=1 and et.NewsTypeID=msd.NewsTypeID" + "\r\n";
//sql = sql + "FOR XML PATH('') ), 1, 1, '') as SalesDateList" + "\r\n";
//sql = sql + ",mnt.NewsTypeName" + "\r\n";
//sql = sql + ",mna.NewsAreaName" + "\r\n";
//sql = sql + ",msd.CustAmt,id.CustomerID,c.CustomerName,c.Addr_Desc,sm.TaxNO,id.SalesTotal" + "\r\n";
//sql = sql + ",c.TelNO,id.InvoiceNO,st.SalesTypeName,id.InvoiceDate" + "\r\n";
//sql = sql + "from InvoiceDetails id" + "\r\n";
//sql = sql + "left join Customer c on id.CustomerID=c.CustomerID" + "\r\n";
//sql = sql + "inner join SalesMaster sm on id.SalesNO=sm.SalesNO" + "\r\n";
//sql = sql + "inner join View_Cust vc on id.CustomerID = vc.ERPCustomerID" + "\r\n";//為了下步搭橋
//sql = sql + "inner join JBADMIN.dbo.[ERPSalesDetails] msd on vc.CustNO=msd.CustNO and id.SalesTypeID=msd.SalesTypeID and id.InvoiceYM=msd.InvoiceYM and msd.ovInvoice=1" + "\r\n";//已開發票
//sql = sql + "left join JBADMIN.dbo.[ERPNewsType] mnt on msd.NewsTypeID=mnt.NewsTypeID" + "\r\n";
//sql = sql + "left join JBADMIN.dbo.[ERPNewsArea] mna on msd.NewsAreaID=mna.NewsAreaID" + "\r\n";
//sql = sql + "left join  dbo.SalesType st on id.SalesTypeID=st.SalesTypeID" + "\r\n";
//sql = sql + "where id.IsActive=1 and id.[QInvoiceType]='97'" + "\r\n";
//                    if (SalesTypeID != "")
//                    {
//                        sql = sql + "and id.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
//                    }
                    
//                    if (InvoiceDateFrom != "")
//                    {
//                        sql = sql + "and id.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
//                    }
//                    if (InvoiceDateTo != "")
//                    {
//                        sql = sql + "and id.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
//                    }
//                    if (CustomerID != "")
//                    {
//                        sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
//                    }
//                    if (InvoiceNO != "")
//                    {
//                        sql = sql + "and id.InvoiceNO = '" + InvoiceNO + "'" + "\r\n";
//                    }
//                    if (SalesID != "")
//                    {
//                        sql = sql + "and id.SalesID = '" + SalesID + "'" + "\r\n";
//                    }
//                    sql = sql + ") temp group by CustomerID,CustomerName,Addr_Desc,TaxNO,SalesTotal,TelNO,InvoiceNO,SalesDateList,NewsTypeName,NewsAreaName,SalesTypeName,InvoiceDate" + "\r\n";


                sql = sql + "select id.CustomerID,c.[CustName] as CustomerName,c.[CustAddr] as Addr_Desc,sm.TaxNO,id.SalesTotal,c.[CustNO] as TelNO,InvoiceNO,id.InvoiceDate,st.SalesTypeName" + "\r\n";//c.Addr_Desc
sql = sql + ",sd.SalesTypeName as SalesTypeName1,mnt.NewsTypeName,mna.NewsAreaName,sd.Amount,sd.Sections,sd.CustLines" + "\r\n";
sql = sql + "from InvoiceDetails id" + "\r\n";
sql = sql + "inner join SalesMaster sm on id.SalesNO=sm.SalesNO" + "\r\n";
sql = sql + "left join SalesDetails sd on sm.SalesNO=sd.SalesNO" + "\r\n";
sql = sql + "left join JBADMIN.dbo.[ERPCustomers] c on id.CustomerID=c.[ERPCustomerID]" + "\r\n";//Customer，一般收據是join Customer，但此廣告費收據只有媒體用故join [ERPCustomers]
sql = sql + "left join JBADMIN.dbo.[ERPNewsType] mnt on sm.NewsTypeID=mnt.NewsTypeID" + "\r\n";
sql = sql + "left join JBADMIN.dbo.[ERPNewsArea] mna on sd.NewsAreaID=mna.NewsAreaID" + "\r\n";
sql = sql + "left join  dbo.SalesType st on id.SalesTypeID=st.SalesTypeID" + "\r\n";
sql = sql + "where id.IsActive=1 and id.[QInvoiceType]='97'" + "\r\n";
if (SalesTypeID != "")
{
    sql = sql + "and id.SalesTypeID in (" + SalesTypeID + ") " + "\r\n";
}

if (InvoiceDateFrom != "")
{
    sql = sql + "and id.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
}
if (InvoiceDateTo != "")
{
    sql = sql + "and id.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
}
if (CustomerID != "")
{
    sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
}
if (InvoiceNO != "")
{
    sql = sql + "and id.InvoiceNO = '" + InvoiceNO + "'" + "\r\n";
}
if (SalesID != "")
{
    sql = sql + "and id.SalesID = '" + SalesID + "'" + "\r\n";
}                
sql = sql + "order by TelNO asc" + "\r\n";
                //sql = sql + "order by  i.[InsGroupID],[SalesTypeID],t.[InvoiceTypeName],[SalesID],i.[InvoiceNO] ,i.[InvoiceDate] ,i.[CustomerID]" + "\r\n";
                //}
                DataSet ds = this.ExecuteSql(sql, connection, transaction);


                ds.Tables[0].Columns.Add("SalesTotalChinese", typeof(string));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                    ds.Tables[0].Rows[i]["SalesTotalChinese"] = IntToChinese(Convert.ToInt32(ds.Tables[0].Rows[i]["SalesTotal"]), true);
                }
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

        public string IntToChinese(long value, bool unitEnable)
        {
            string chinese;
            string[] number = { "零", "壹", "貳", "叁", "肆", "伍", "陸", "柒", "捌", "玖" };
            string[] unit = { "", "拾", "佰", "仟", "萬", "拾", "佰", "仟", "億", "拾", "佰", "仟", "兆", "拾", "佰", "仟" };
            string temp = value.ToString();
            chinese = "";
            //if (value > 9999999999999999)
            //{
            //    return "-1";
            //}
            //if (temp.Substring(0, 1).Equals("-"))
            //{
            //    chinese = "負的";
            //    temp = temp.Substring(1, temp.Length - 1);
            //}
            //if (temp.Substring(0, 1).Equals("+"))
            //{
            //    chinese = "正的";
            //    temp = temp.Substring(1, temp.Length - 1);
            //}

            for (int i = 0; i < temp.Length; i++)
            {
                chinese = chinese + number[Convert.ToInt32(temp.Substring(i, 1))];
                if (unitEnable.Equals(true))
                    chinese = chinese + unit[(temp.Length - (i + 1)) % 16];
            }

            return chinese;
        }
    }
}
