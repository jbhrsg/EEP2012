using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;

namespace sERPSalesEmail
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
        public object[] UpdateERPSalesEmail(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam=objParam[0].ToString().Split(',');
            string sCustNO = aParam[0].ToString();
            string soInvoiceYM = aParam[1].ToString();
            string sCustShortName = aParam[2].ToString();
            string sContactA = aParam[3].ToString();
            string sContactAMail = aParam[4].ToString();
            string sbilldeal = aParam[5].ToString();
            string sbilldealemail = aParam[6].ToString();
            string sSalesName = aParam[7].ToString();
            //char[] delimiter = new char[]{'*'};
            string[] aCustNO = sCustNO.Split('*');
            string[] aoInvoiceYM = soInvoiceYM.Split('*');
            string[] aCustShortName = sCustShortName.Split('*');
            string[] aContactA = sContactA.Split('*');
            string[] aContactAMail = sContactAMail.Split('*');
            string[] abilldeal = sbilldeal.Split('*');
            string[] abilldealemail = sbilldealemail.Split('*');
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            string[] aSalesName = sSalesName.Split('*');

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql1;
                string thisDay = DateTime.Today.ToString("yyyy-MM-dd");
                string body = string.Empty;
                for (int i = 0; i < aCustNO.Length; i++)
                {
                    if ((aContactA[i].Trim() != "" && aContactA[i].Trim() != null) && (abilldeal[i].Trim() != "" && abilldeal[i].Trim() != null)){//'" + "allen@jbjob.com.tw" + "*" + "mandy@jbjob.com.tw" + "*" + "jommy@jbjob.com.tw" + "'
                        sql1 = "INSERT INTO [dbo].[ERPSalesEmail] ([ToMail],[ToName],[FromName],[FromDate],[FromType],[CreateBy],[CreateDate],[CustNO],[InvoiceYM],[EmailBody],[SalesName])  VALUES ('" + aContactAMail[i].Trim() + "*" + abilldealemail[i].Trim() + "','" + aContactA[i].Replace("'", "").Replace(@"""", "").Trim() + "、" + abilldeal[i].Replace("'", "").Replace(@"""", "").Trim() + "','" + LoginUser.Replace("'", "").Replace(@"""", "") + "','" + thisDay + "','1','" + LoginUser.Replace("'", "").Replace(@"""", "") + "','" + thisDay + "','" + aCustNO[i] + "','" + aoInvoiceYM[i] + "','" + body + "','" + aSalesName[i] + "')";
                    }
                    else if ((aContactA[i].Trim() == "" || aContactA[i].Trim() == null) && (abilldeal[i].Trim() != "" && abilldeal[i].Trim() != null)){
                        sql1 = "INSERT INTO [dbo].[ERPSalesEmail] ([ToMail],[ToName],[FromName],[FromDate],[FromType],[CreateBy],[CreateDate],[CustNO],[InvoiceYM],[EmailBody],[SalesName])  VALUES ('" + aContactAMail[i].Trim() + "*" + abilldealemail[i].Trim() + "','" + abilldeal[i].Replace("'", "").Replace(@"""", "").Trim() + "','" + LoginUser.Replace("'", "").Replace(@"""", "") + "','" + thisDay + "','1','" + LoginUser.Replace("'", "").Replace(@"""", "") + "','" + thisDay + "','" + aCustNO[i] + "','" + aoInvoiceYM[i] + "','" + body + "','" + aSalesName[i] + "')";
                    }
                    else{
                        sql1 = "INSERT INTO [dbo].[ERPSalesEmail] ([ToMail],[ToName],[FromName],[FromDate],[FromType],[CreateBy],[CreateDate],[CustNO],[InvoiceYM],[EmailBody],[SalesName])  VALUES ('" + aContactAMail[i].Trim() + "*" + abilldealemail[i].Trim() + "','" + aContactA[i].Replace("'", "").Replace(@"""", "").Trim() + "','" + LoginUser.Replace("'", "").Replace(@"""", "") + "','" + thisDay + "','1','" + LoginUser.Replace("'", "").Replace(@"""", "") + "','" + thisDay + "','" + aCustNO[i] + "','" + aoInvoiceYM[i] + "','" + body + "','" + aSalesName[i] + "')";
                    }
                    
                    this.ExecuteSql(sql1, connection, transaction);
                }
                transaction.Commit();
                ret[1] = true;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;  
        }
    }
}
