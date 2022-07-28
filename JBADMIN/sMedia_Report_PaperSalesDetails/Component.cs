using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sMedia_Report_PaperSalesDetails
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
            string NewsTypeID = parm[4];
            string NewsAreaID = parm[5];
            string NewsPublishID = parm[6];
            string ReportType = parm[7];
            string GrantTypeID = parm[8];

            if (SalesID != "")
            {
                string[] arrSalesID = SalesID.Split('*');
                SalesID = string.Join(",", arrSalesID);
            }
            if (NewsTypeID != "")
            {
                string[] arrNewsTypeID = NewsTypeID.Split('*');
                NewsTypeID = string.Join(",", arrNewsTypeID);
            }
            if (NewsAreaID != "")
            {
                string[] arrNewsAreaID = NewsAreaID.Split('*');
                NewsAreaID = string.Join(",", arrNewsAreaID);
            }
            if (NewsPublishID != "")
            {
                string[] arrNewsPublishID = NewsPublishID.Split('*');
                NewsPublishID = string.Join(",", arrNewsPublishID);
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
                if (ReportType == "1" || ReportType == "2" || ReportType == "4" || ReportType == "5")//日期，報別，客戶
                {
                    sql = "SELECT d.[SalesDate],d.[SalesID],d.[CustNO],m.[CustShortName],d.[Sections],d.[OfficeLines],d.[CustLines],d.[NewsTypeID]" + "\r\n";
                    sql = sql + ",d.[NewsAreaID],d.[OfficeAmt],d.[CustPrice],d.[CustAmt],d.[NewsPublishID]" + "\r\n";
                    sql = sql + "FROM [ERPSalesDetails] d" + "\r\n";
                    sql = sql + "left join [ERPSalesMaster] m on d.SalesMasterNO=m.SalesMasterNO" + "\r\n";
                    sql = sql + "where d.IsActive=1 and d.SalesTypeID='6'" + "\r\n";
                    if (CustNO != "")
                    {
                        sql = sql + "and d.CustNO ='" + CustNO + "'" + "\r\n";
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
                    if (NewsTypeID != "")
                    {
                        sql = sql + "and d.NewsTypeID in (" + NewsTypeID + ") " + "\r\n";//d有DMTypeID
                    }
                    if (NewsAreaID != "")
                    {
                        sql = sql + "and d.NewsAreaID in (" + NewsAreaID + ") " + "\r\n";
                    }
                    if (NewsPublishID != "")
                    {
                        sql = sql + "and d.NewsPublishID in (" + NewsPublishID + ") " + "\r\n";
                    }
                    if (GrantTypeID == "1")
                    {
                        sql = sql + "and d.OfficeAmt !=0" + "\r\n";
                    }
                    
                    if(ReportType == "4")
                    sql = sql + "order by d.SalesDate,d.NewsTypeID,d.CustNO" + "\r\n";
                    else if (ReportType == "5")
                    {
                        sql = sql + "order by d.SalesID,d.CustNO,d.NewsTypeID,d.SalesDate" + "\r\n";
                    }
                    else sql = sql + "order by d.SalesDate" + "\r\n";
                }
                else if (ReportType == "3")//發稿
                {
                    sql = "SELECT d.[SalesDate],d.[SalesID],d.[CustNO],m.[CustShortName],d.[Sections],d.[OfficeLines],d.[CustLines],d.[NewsTypeID]" + "\r\n";
                    sql = sql + ",d.[NewsAreaID],d.[OfficeAmt],d.[CustPrice],d.[CustAmt],d.[NewsPublishID],p.NewsPublishName" + "\r\n";
                    sql = sql + "FROM [ERPSalesDetails] d" + "\r\n";
                    sql = sql + "left join [ERPSalesMaster] m on d.SalesMasterNO=m.SalesMasterNO" + "\r\n";
                    sql = sql + "left join [ERPNewsPublish] p on d.NewsPublishID=p.NewsPublishID" + "\r\n";
                    sql = sql + "where d.IsActive=1 and d.SalesTypeID='6'" + "\r\n";
                    if (CustNO != "")
                    {
                        sql = sql + "and d.CustNO ='" + CustNO + "'" + "\r\n";
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
                    if (NewsTypeID != "")
                    {
                        sql = sql + "and d.NewsTypeID in (" + NewsTypeID + ") " + "\r\n";//d有DMTypeID
                    }
                    if (NewsAreaID != "")
                    {
                        sql = sql + "and d.NewsAreaID in (" + NewsAreaID + ") " + "\r\n";
                    }
                    if (NewsPublishID != "")
                    {
                        sql = sql + "and d.NewsPublishID in (" + NewsPublishID + ") " + "\r\n";
                    }
                    if (GrantTypeID == "1")
                    {
                        sql = sql + "and d.OfficeAmt !=0" + "\r\n";
                    }
                    sql = sql + "order by d.NewsPublishID,d.SalesDate,d.NewsTypeID" + "\r\n";
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
