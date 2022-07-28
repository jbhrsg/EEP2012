using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sJCS1
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
        //平鎮宿舍銷貨報表
        public object[] procReportJCS1BillData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YM = parm[0].ToString();
            string CustID = parm[1].ToString();          

            string js = string.Empty;
            string sLoginDB = "JCS1";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportJCS1BillData '" + YM + "','" + CustID + "'";
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }
        //平鎮宿舍電子發票匯出Excel格式
        public object[] procReportJCS1Electronic(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YM = parm[0].ToString();
            string CustID = parm[1].ToString();
            string InvoiceType = parm[2].ToString();
            string IsElec = parm[3].ToString();

            string js = string.Empty;
            string sLoginDB = "JCS1";
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
                string SQL = "exec procReportJCS1Electronic '" + YM + "','" + CustID + "','" + InvoiceType + "','" + IsElec + "'";
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



    }
}
