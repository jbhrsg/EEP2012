using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data.SqlClient; 
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;
using JBTool;

namespace sJCSOutQuery
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
        public object[] GetRoomerInOutData(object[] objParam)
        {
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            string js = string.Empty;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JCS;User ID=sa;Password=NBV2mXzr";
            //connetionString = "Data Source=192.168.1.17\\SQL2016;Initial Catalog=JCS;User ID=sa;Password=SQLADMIN";
            conn = new SqlConnection(connetionString);
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //開始transaction
            IDbTransaction transaction = conn.BeginTransaction();

            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string CustNO = parm[0];
                string CurrentDate = parm[1];
                string ControlTime = parm[2];
                string ReportType = parm[3];   //報表類別
                switch (ReportType)
                {
                    //傑誠宿舍
                    case "1":
                           sql = "EXEC  JCS.DBO.procDisplayRoomerInOutOverLimitTime '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    //長安宿舍
                    case "2":
                          sql = "EXEC  JCS2.DBO.procDisplayRoomerInOutOverLimitTime '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    //平鎮宿舍24小時未刷卡異常
                    case "3":
                          sql = "EXEC  JCS1.DBO.procDisplayRoomerInOutOverLimitTime '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    //平鎮宿舍
                    case "6":
                          sql = "EXEC  JCS1.DBO.procDisplayRoomerInOutOnLimitTimeNew '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    //台茂宿舍
                    case "8":
                        //sql = "EXEC  JCS4.DBO.procDisplayRoomerInOutOnLimitTimeNew '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                          sql = "EXEC  JCS4.DBO.procDisplayRoomerInOutOverLimitTime '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    //睿能
                    case "9":
                        sql   = "EXEC  JCS2.DBO.procDisplayRoomerInOutOnLimitTimeNewJ0017 '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                }
                DataSet ds = this.ExecuteSql(sql, conn, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                //return new object[] { 0, false };
            }
            finally
            {
                //transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);

            }
            return new object[] { 0, js };
        }

        public object[] RoomerInOutAutoExcel(object[] objParam)
        {
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            string js = string.Empty;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JCS;User ID=sa;Password=NBV2mXzr";
            conn = new SqlConnection(connetionString);
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //開始transaction
            IDbTransaction transaction = conn.BeginTransaction();
            var theResult = new Dictionary<string, object>();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string CustNO = parm[0];
                string CurrentDate = parm[1];
                string ControlTime = parm[2];
                string DoomID = parm[3];   //宿舍代號
                switch (DoomID)
                {
                    //傑誠宿舍
                    case "1":
                        sql = "EXEC JCS.DBO.procDisplayRoomerInOutOverLimitTime '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    //長安宿舍
                    case "2":
                        sql = "EXEC JCS2.DBO.procDisplayRoomerInOutOverLimitTime '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    //平鎮宿舍
                    case "6":
                        sql = "EXEC JCS1.DBO.procDisplayRoomerInOutOnLimitTimeNew '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    //台茂宿舍
                    case "8":
                        sql = "EXEC JCS4.DBO.procDisplayRoomerInOutOverLimitTime '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                    case "9":
                        sql = "EXEC  JCS2.DBO.procDisplayRoomerInOutOnLimitTimeNewJ0017 '" + CustNO + "','" + CurrentDate + "','" + ControlTime + "'";
                        break;
                }
                DataSet ds = this.ExecuteSql(sql, conn, transaction);
                //// Indented縮排 將資料轉換成Json格式
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
          
        }



    }
}
