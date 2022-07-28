using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;
using JBTool;

namespace sJOB0800
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
        // 取得點擊統計資訊
        public object[] GePublishingRecordInfo(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string sCust = parm[2];
            string aAccount = parm[3];
            //建立資料庫連結

            string sLoginDB = "JOB0800";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //敦緯內網
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";
                string SQL = " exec procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";//localhost

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
        // 取得點擊統計資訊-內容
        public object[] GePublishingRecordInfoData(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string sCust = parm[2];
            string aAccount = parm[3];
            string iType = parm[4];
            //建立資料庫連結

            string sLoginDB = "JOB0800";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //敦緯內網
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";
                string SQL = " exec procDisplayPublishingRecordData '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "','" + iType + "'";

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
        // 取得廣告統計
        public object[] GePublishingCount(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string IndustryId = parm[2];
            string CityId = parm[3];
            string TownId = parm[4];
            //建立資料庫連結

            string sLoginDB = "JOB0800";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //敦緯內網
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";
                string SQL = " exec procDisplayPublishingCount 1,'" + SDate + "','" + EDate + "','','" + IndustryId + "','" + CityId + "','" + TownId + "','','',''";

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
        // 取得點擊統計資訊-內容
        public object[] GePublishingCountData(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string IndustryId = parm[2];
            string CityId = parm[3];
            string TownId = parm[4];
            string TeamID = "";
            string iYear = parm[5];
            string iMonth = parm[6];
            string SalesID = parm[7];
           
            if (SalesID == "null")
            {
                SalesID = "0";
            }
            //建立資料庫連結

            string sLoginDB = "JOB0800";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //敦緯內網
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";
                string SQL = " exec procDisplayPublishingCount 2,'" + SDate + "','" + EDate + "','" + TeamID + "','" + IndustryId + "','" + CityId + "','" + TownId + "','" + iYear+"','"+iMonth+"','"+SalesID+"'";

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

        // 取得有效廣告刊登資訊
        public object[] GePublishingList(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];            
            //建立資料庫連結

            string sLoginDB = "JOB0800";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //敦緯內網
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingList '" + SDate + "','" + EDate  + "'";
                string SQL = " exec procDisplayPublishingList '" + SDate + "','" + EDate + "'";

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

        // 有效廣告刊登資訊 - 匯出Excel下載
        public object[] PublishingListAutoExcel(object[] objParam)
        {
            //var ParameterInput = TheJsonResult.GetParameterObj(objParam);
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];

            string js = string.Empty;

            string sLoginDB = "JOB0800";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
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
                //敦緯內網
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingList '" + SDate + "','" + EDate  + "'";
                string SQL = " exec procDisplayPublishingListExcel '" + SDate + "','" + EDate + "'";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };           
        }


        public object[] PublishingAutoExcel(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string IndustryId = parm[2];
            string CityId = parm[3];
            string TownId = parm[4];

            //建立資料庫連結
            string sLoginDB = "JOB0800";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
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
                string SQL = " exec procDisplayPublishingCount 3,'" + SDate + "','" + EDate + "','','" + IndustryId + "','" + CityId + "','" + TownId + "','','',''";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };

        }

        // 取得fb的貼文資訊
        public object[] ProcessPublishingJobFB(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string IsMark = parm[0];
            string MarkDate = "";
            if (parm[1].ToString() != "")
            {
                 MarkDate=DateTime.Parse(parm[1].ToString()).ToShortDateString();
            }
            string cCode = parm[2];
            string siAutoKey = parm[3];
            string Type = parm[4];

            //建立資料庫連結

            string sLoginDB = "JOB0800";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //Type 1查詢,3標記
                string SQL = " exec procProcessPublishingJobFB " + Type + ",'" + cCode + "','" + IsMark + "','" + MarkDate + "','" + siAutoKey + "'";//localhost
                if (Type == "1")
                {
                    DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                    //// Indented縮排 將資料轉換成Json格式
                    js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                }
                else
                {
                    this.ExecuteSql(SQL, connection, transaction);
                }
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
        // 將fb的貼文資訊匯出Excel , Type = 2匯出Excel
        public object[] PublishingJobFBExcel(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string IsMark = parm[0];
            string siAutoKey = parm[1];

            //建立資料庫連結
            string sLoginDB = "JOB0800";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {//Type 1查詢,2匯出Excel,3標記
                string SQL = " exec procProcessPublishingJobFB 2,'','','','" + siAutoKey + "'";

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





    }
}
