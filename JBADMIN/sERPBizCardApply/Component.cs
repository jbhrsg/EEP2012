using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERPBizCardApply
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
        public String GetBizCardNOPrefix(){
            DateTime td = DateTime.Today;
            return "BC" + ((td.Year).ToString()).Trim();
        }
        public object[] GetLastApply(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };//第二個元素放json字串ex:{.....} or [.....]
            //DataRow drDara = (DataRow)objParam[0]; // 法1
            //var OverTimeNO = drDara["OverTimeNO"].ToString();
            string[] strParam = objParam[0].ToString().Split(',');// 法2
            string Cname = strParam[0];
            //string aDate = strParam[1];
            //string bDate = strParam[2];
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
                var detailSql = "";
                //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //string userName = SrvGL.GetUserName(userid);
                detailSql = "SELECT top 1 * FROM dbo.[ERPBizCardApply] where Cname='"+Cname+"' order by CreateDate desc";
                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                //string counts = ds.Tables[0].Rows[0]["counts"].ToString();//DataSet,DataTable,DataRow
                transaction.Commit(); // 確認交易
                string js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // 傳回值: 無
        }

        public object[] GetUserID(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };//第二個元素放json字串ex:{.....} or [.....]
            //DataRow drDara = (DataRow)objParam[0]; // 法1
            //var OverTimeNO = drDara["OverTimeNO"].ToString();
            string[] strParam = objParam[0].ToString().Split(',');// 法2
            string Cname = strParam[0];
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var detailSql = "";
                //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //string userName = SrvGL.GetUserName(userid);
                detailSql = "SELECT [USERID] FROM [EIPHRSYS].[dbo].[USERS] where DESCRIPTION ='JB' and USERNAME='" + Cname + "'";
                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                transaction.Commit(); // 確認交易
                string js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("EIPHRSYS", connection);
            }
            return ret; // 傳回值: 無
        }

        public object[] Update_ERPBizCardApply(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string sCardNO = aParam[0].ToString();
            string[] aCardNO = sCardNO.Split('*');
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transactionaCustNO
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql1;
                for (int i = 0; i < aCardNO.Length; i++)
                {
                    sql1 = "update ERPBizCardApply set IsPrint='是' where BizCardNO='"+aCardNO[i]+"'";
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
