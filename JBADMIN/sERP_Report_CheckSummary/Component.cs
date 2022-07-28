using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_CheckSummary
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
            string InsGroupID = parm[1];
            string WarrantDateFrom = parm[2];
            string WarrantDateTo = parm[3];
            string AccountID = parm[4];

            if (InsGroupID != "")
            {
                string[] arrInsGroupID = InsGroupID.Split('*');
                InsGroupID = string.Join(",", arrInsGroupID);
            }
            if (AccountID != "")
            {
                string[] arrAccountID = AccountID.Split('*');
                AccountID = string.Join(",", arrAccountID);
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
                if (AccountID == "")
                {
                    sql = "select CheckNO,WarrantDate,CheckDueDate,Amount,cd.CustomerID,c.ShortName,wd.InvoiceNO,cd.InsGroupID,ca.CheckAccountName from CheckDetails cd" + "\r\n";
                    sql = sql + "left join WarrantDetails wd on wd.WarrantNO=cd.WarrantNO" + "\r\n";
                    sql = sql + "left join Customer c on c.CustomerID=cd.CustomerID" + "\r\n";
                    sql = sql + "left join CheckAccount ca on cd.AccountID=ca.CheckAccountID" + "\r\n";
                    sql = sql + "where CheckDueDate > cast(GETDATE() as date)" + "\r\n";
                    if (CustomerID != "")
                    {
                        sql = sql + "and cd.CustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (InsGroupID != "")
                    {
                        sql = sql + "and cd.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    }
                    if (WarrantDateFrom != "")
                    {
                        sql = sql + "and cd.WarrantDate >='" + WarrantDateFrom + "'" + "\r\n";
                    }
                    if (WarrantDateTo != "")
                    {
                        sql = sql + "and cd.WarrantDate <='" + WarrantDateTo + "'" + "\r\n";
                    }
                    //sql = sql + "order by cd.InsGroupID,cd.CustomerID,cd.WarrantDate" + "\r\n";
                }
                else if (AccountID != "")
                {
                    sql = "select WarrantDate,CheckDueDate,Amount from CheckDetails" + "\r\n";
                    sql = sql + "where AccountID in (" + AccountID + ") " + "\r\n";
                    if (CustomerID != "")
                    {
                        sql = sql + "and CustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (InsGroupID != "")
                    {
                        sql = sql + "and InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    }
                    if (WarrantDateFrom != "")
                    {
                        sql = sql + "and WarrantDate >='" + WarrantDateFrom + "'" + "\r\n";
                    }
                    if (WarrantDateTo != "")
                    {
                        sql = sql + "and WarrantDate <='" + WarrantDateTo + "'" + "\r\n";
                    }
                    sql = sql + "order by WarrantDate asc" + "\r\n";
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
