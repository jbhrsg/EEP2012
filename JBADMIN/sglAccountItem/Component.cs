using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using Newtonsoft.Json;

namespace sglAccountItem
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

        public object[] CheckMasterDelete(object[] objParam)
        {
            //取得IssueTypeID是否有在IssueJob中被使用到
            string ClassID = (string)objParam[0];
            string js = string.Empty;
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
                string sql = " select count(*) as iCount from glAccountItem where ClassID=" + ClassID;
                DataSet dsType = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = dsType.Tables[0].Rows[0]["iCount"].ToString();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //檢查科目+明細不可以重複-------------------------------------------------------------
        public object[] ReturnAcnoSubAcnoCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CompanyID = parm[0].ToString();
            string AcnoSubAcno = parm[1].ToString();
            string js = string.Empty;
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

                string sql = "SELECT COUNT(*) AS CNT from glAccountItem where CompanyID= "+CompanyID+" and Acno+SubAcno='" + AcnoSubAcno + "'";

                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                int cnt = int.Parse(dsTemp.Tables[0].Rows[0]["cnt"].ToString());

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }


    }
}
