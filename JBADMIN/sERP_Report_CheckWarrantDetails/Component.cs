using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_CheckWarrantDetails
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
                sql = "select  cd.CustomerID as CompanyCustomerID,c.ShortName,cd.WarrantDate,cd.Amount,wd.InvoiceNO" + "\r\n";
                sql = sql + ",id.InvoiceDate,wd.RecAmount,(ISNULL(wd.OthAmount,0)+ISNULL(wd.RebAmount,0)+ISNULL(wd.RetAmount,0)+ISNULL(wd.BadAmount,0)) as OthAmount,cd.CheckDueDate,cd.CheckNO,b.BankName as BankBranchName,cd.WarrantNO,ca.CheckAccountName as InsGroupID,left(c.CustomerName,4) as CustomerName,c.TelNO,s.SalesTypeName" + "\r\n";
                    sql = sql + "from CheckDetails cd" + "\r\n";
                    sql = sql + "left join WarrantDetails wd on wd.WarrantNO=cd.WarrantNO" + "\r\n";
                    sql = sql + "left join InvoiceDetails id on id.InvoiceNO=wd.InvoiceNO" + "\r\n";
                    sql = sql + "left join WarrantMaster wm on wm.WarrantNO=cd.WarrantNO" + "\r\n";
                    sql = sql + "left join Customer c on c.CustomerID=cd.CustomerID" + "\r\n";
                    sql = sql + "left join JBADMIN.dbo.Bank b on b.BankID=cd.BankID" + "\r\n";//b.BankBranchID=cd.BankBranchID and b.BankRootID=cd.BankRootID
                    sql = sql + "left join CheckAccount ca on cd.AccountID=ca.CheckAccountID" + "\r\n";
                    sql = sql + "left join SalesType s on s.SalesTypeID=id.SalesTypeID" + "\r\n";
                    sql = sql + "where wm.PayWayID='5'" + "\r\n";//支票
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
                    if (AccountID != "")
                    {
                        sql = sql + "and cd.AccountID in (" + AccountID + ") " + "\r\n";
                    }
                    sql = sql + "order by cd.InsGroupID,cd.CustomerID,cd.WarrantDate" + "\r\n";

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
