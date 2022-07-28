using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using Newtonsoft.Json;
using JBTool;

namespace sPOMasterAmortization
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

        //失效傳票
        public object[] UpdatePOMasterAmortizationVIsActive(object[] objParam)
        {
            string AutoKey = objParam[0].ToString();
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

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
                string SQL = "exec procUpdatePOMasterAmortizationVIsActive " + AutoKey + ",'" + username+"'";
                this.ExecuteSql(SQL, connection, transaction);
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

        private void ucPOMasterAmortization_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucPOMasterAmortization.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucPOMasterAmortization_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucPOMasterAmortization.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        //查詢攤銷項目 或 新增攤銷項目到暫存的傳票檔
        public object[] procInsertPOMasterVoucherM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];
            string CompanyID = parm[1];
            string POAutoKey = parm[2];

            string Type = parm[3];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

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
                string SQL = " exec procInsertPOMasterVoucherM '" + CompanyID + "','" + YearMonth + "','" + POAutoKey.Trim() + "','" + userid + "','" + username + "'," + Type + "\r\n";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
        //月份攤銷清單 => 匯出Excel
        public object[] AmortizationAutoExcel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];
            string CompanyID = parm[1];
            string Type = parm[2];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

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

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = " exec procInsertPOMasterVoucherM '" + CompanyID + "','" + YearMonth + "','" + userid + "','" + username + "'," + Type + "\r\n";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));

                theResult.Add("IsOK", true);
                theResult.Add("Msg", "錯誤訊息");
                theResult.Add("FileName", "這是一個檔案.xls");

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
        }





    }
}
