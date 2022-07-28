using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sMedia_Report_PaperReceivablePayableList
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
                if (ReportType == "2")//彙總
                {
                    sql = "SELECT  t.NewsTypeName,p.NewsPublishName,sum([CustAmt]) as CustAmtS,sum([OfficeAmt]) as OfficeAmtS" + "\r\n";
                    sql = sql + "FROM [ERPSalesDetails] d" + "\r\n";
                    sql = sql + "LEFT JOIN ERPNewsType t on d.NewsTypeID=t.NewsTypeID" + "\r\n";
                    sql = sql + "LEFT JOIN ERPNewsPublish p on d.NewsPublishID=p.NewsPublishID" + "\r\n";
                    sql = sql + "WHERE d.IsActive=1 and d.SalesTypeID='6'" + "\r\n";

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
                        sql = sql + "and d.NewsTypeID in (" + NewsTypeID + ") " + "\r\n";
                    }
                    if (NewsAreaID != "")
                    {
                        sql = sql + "and d.NewsAreaID in (" + NewsAreaID + ") " + "\r\n";//d有DMTypeID
                    }
                    if (NewsPublishID != "")
                    {
                        sql = sql + "and d.NewsPublishID in (" + NewsPublishID + ") " + "\r\n";
                    }
                    sql = sql + "GROUP BY t.NewsTypeName,p.NewsPublishName" + "\r\n";
                    sql = sql + "ORDER BY NewsTypeName,NewsPublishName" + "\r\n";
                }
                else if (ReportType == "3")//業務
                {
                    sql = "SELECT  t.NewsTypeName,p.NewsPublishName,d.[SalesID],sum([CustAmt]) as CustAmtS,sum([OfficeAmt]) as OfficeAmtS" + "\r\n";
                    sql = sql + "FROM [ERPSalesDetails] d" + "\r\n";
                    sql = sql + "LEFT JOIN ERPNewsType t on d.NewsTypeID=t.NewsTypeID" + "\r\n";
                    sql = sql + "LEFT JOIN ERPNewsPublish p on d.NewsPublishID=p.NewsPublishID" + "\r\n";
                    sql = sql + "WHERE IsActive=1 and SalesTypeID='6'" + "\r\n";
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
                        sql = sql + "and d.NewsTypeID in (" + NewsTypeID + ") " + "\r\n";
                    }
                    if (NewsAreaID != "")
                    {
                        sql = sql + "and d.NewsAreaID in (" + NewsAreaID + ") " + "\r\n";//d有DMTypeID
                    }
                    if (NewsPublishID != "")
                    {
                        sql = sql + "and d.NewsPublishID in (" + NewsPublishID + ") " + "\r\n";
                    }
                    sql = sql + "GROUP BY t.NewsTypeName,p.NewsPublishName,d.[SalesID]" + "\r\n";
                    sql = sql + "ORDER BY NewsTypeName,NewsPublishName,SalesID" + "\r\n";
                }
                else if (ReportType == "4")//月份
                {
                    sql = "SELECT  t.NewsTypeName,p.NewsPublishName,d.[SalesID],left(CONVERT(nvarchar(10),d.[SalesDate],111),7) as SalesYM,sum([CustAmt]) as CustAmtS,sum([OfficeAmt]) as OfficeAmtS" + "\r\n";
                    sql = sql + "FROM [ERPSalesDetails] d" + "\r\n";
                    sql = sql + "LEFT JOIN ERPNewsType t on d.NewsTypeID=t.NewsTypeID" + "\r\n";
                    sql = sql + "LEFT JOIN ERPNewsPublish p on d.NewsPublishID=p.NewsPublishID" + "\r\n";
                    sql = sql + "WHERE IsActive=1 and SalesTypeID='6'" + "\r\n";
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
                        sql = sql + "and d.NewsTypeID in (" + NewsTypeID + ") " + "\r\n";
                    }
                    if (NewsAreaID != "")
                    {
                        sql = sql + "and d.NewsAreaID in (" + NewsAreaID + ") " + "\r\n";//d有DMTypeID
                    }
                    if (NewsPublishID != "")
                    {
                        sql = sql + "and d.NewsPublishID in (" + NewsPublishID + ") " + "\r\n";
                    }
                    sql = sql + "GROUP BY t.NewsTypeName,p.NewsPublishName,d.[SalesID],left(CONVERT(nvarchar(10),d.[SalesDate],111),7)" + "\r\n";
                    sql = sql + "ORDER BY NewsTypeName,NewsPublishName,SalesID,SalesYM" + "\r\n";
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
