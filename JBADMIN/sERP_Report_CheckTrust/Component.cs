using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_CheckTrust
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
            string AccountID = parm[0];
            string WarrantDateFrom = parm[1];
            string WarrantDateTo = parm[2];
            string InsGroupID = parm[3];
            string CheckDueDateFrom = parm[4];
            string CheckDueDateTo = parm[5];
            string ActionCode = parm[6];
            string TrustDateFrom = parm[7];
            string TrustDateTo = parm[8];

            List<string> WhereList = new List<string>();
            string[] WhereArr;
            string WhereString = "";

            if (AccountID != "")
            {
                string[] arrAccountID = AccountID.Split('*');
                //AccountID = string.Join(",", arrAccountID);
                AccountID="";
                for(int i=0;i<arrAccountID.Length;i++){
                    if(i==0)AccountID=AccountID+"'"+arrAccountID[i]+"'";
                    else if(i!=0){AccountID=AccountID+",'"+arrAccountID[i]+"'";}
                }
            }
            if (InsGroupID != "")
            {
                string[] arrInsGroupID = InsGroupID.Split('*');
                InsGroupID = string.Join(",", arrInsGroupID);
            }
            if (ActionCode != "")
            {
                string[] arrActionCode = ActionCode.Split('*');
                ActionCode = string.Join(",", arrActionCode);
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
                    sql = "SELECT  cd.[AccountID]+' '+ca.CheckAccountName as [AccountIDName],[CheckAccount],[CheckNO],b.BankRootName" + "\r\n";
                    sql = sql + ",b.BankName as BankBranchName,c.ShortName,[CheckDueDate],[Amount],cd.InsGroupID" + "\r\n";
                    sql = sql+"FROM [JBERP].[dbo].[CheckDetails] cd" + "\r\n";
                    sql = sql+"left join CheckAccount ca on ca.CheckAccountID=cd.AccountID" + "\r\n";
                    sql = sql+"left join JBADMIN.dbo.Bank b on b.BankID=cd.BankID" + "\r\n";
                    sql = sql+"left join Customer c on c.CustomerID=cd.CustomerID" + "\r\n";

                    //sql = sql + "where cd.ActionCode =0" + "\r\n";
                    if (AccountID != "")
                    {
                        WhereList.Add("cd.AccountID in (" + AccountID + ") ");
                    }
                    if (WarrantDateFrom != "")
                    {
                        WhereList.Add("cd.WarrantDate >='" + WarrantDateFrom + "'");
                    }
                    if (WarrantDateTo != "")
                    {
                        WhereList.Add("cd.WarrantDate <='" + WarrantDateTo + "'");
                    }
                    if (InsGroupID != "")
                    {
                        WhereList.Add("cd.InsGroupID in (" + InsGroupID + ") ");
                    }
                    if (CheckDueDateFrom != "")
                    {
                        WhereList.Add("cd.CheckDueDate >='" + CheckDueDateFrom + "'");
                    }
                    if (CheckDueDateTo != "")
                    {
                        WhereList.Add("cd.CheckDueDate <='" + CheckDueDateTo + "'");
                    }
                    if (ActionCode != "")
                    {
                        WhereList.Add("cd.ActionCode in (" + ActionCode + ") ");
                    }
                    if (TrustDateFrom != "")
                    {
                        WhereList.Add("cd.TrustDate >='" + TrustDateFrom + "'");
                    }
                    if (TrustDateTo != "")
                    {
                        WhereList.Add("cd.TrustDate <='" + TrustDateTo + "'");
                    }

                    WhereArr=WhereList.ToArray();
                    if (WhereArr.Length > 0) {
                        WhereString="where " + string.Join(" and ", WhereArr);

                    }

                    sql = sql + WhereString + "\r\n";
                    //sql = sql + "order by cd.InsGroupID,cd.CustomerID,cd.WarrantDate" + "\r\n";
                
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
