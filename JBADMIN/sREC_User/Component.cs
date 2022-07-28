using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace sREC_User
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

        private void ucRECUser_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucRECUser.SetFieldValue("CreateDate", DateTime.Now);
            ucRECUser.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucRECUser_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucRECUser.SetFieldValue("LastUpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucRECUser.SetFieldValue("LastUpdateBy", username);
        }
        private void ucRECUser_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            ////------------------------------------文字------------------------------------------------
            ////工作地點
            //string DutyAreasIDs = ucRECUser.GetFieldCurrentValue("DutyAreasIDs").ToString();
            //string DutyAreas = GetRecReference("DutyAreas", DutyAreasIDs);
            //ucRECUser.SetFieldValue("DutyAreas", DutyAreas);

        }
        public object[] UpdateRecReference(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string UserID = parm[0];

            string js = string.Empty;

            string sLoginDB = "Hunter";
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
            {
                //修改文字
                string SQL = "exec procSyncMutiselectTextFieldAll '" + UserID+"'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

            }
            catch
            {
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        public string GetRecReference(string sTable, string sVal)
        {
            string sContents = "";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
            {
                string sql = "exec procReturnRecReference '" + sTable + "','"+sVal+"'" + "\r\n";

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
                sContents = ds.Tables[0].ToString();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return sContents;
        }
        //--------------------面談紀錄維護-----------------------------------------
        private void ucREC_UserContactRecord_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucREC_UserContactRecord.SetFieldValue("UpdateDate", DateTime.Now);
        }

        private void ucREC_UserContactRecord_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucREC_UserContactRecord.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucREC_UserContactRecord.SetFieldValue("UpdateBy", username);

        }
        //工作經驗
        private void ucREC_UserCareer_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucREC_UserCareer.SetFieldValue("CreateDate", DateTime.Now);
            ucREC_UserCareer.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucREC_UserCareer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucREC_UserCareer.SetFieldValue("LastUpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucREC_UserCareer.SetFieldValue("LastUpdateby", username);
        }
        //================================================================================================================================//
        //查詢履歷資料
        public object[] RECUsersQuery(object[] objParam)
        {
            //string Keyword = (string)objParam[0].ToString().Trim();
            string[] parm = objParam[0].ToString().Split('*');
            string NameC = parm[0].ToString();//人才姓名/身分證/履歷編號
            string Gender = parm[1].ToString();//性別
            string Age1 = parm[2].ToString();//年齡
            string Age2 = parm[3].ToString();
            string EduID = parm[4].ToString();//學歷
            string DutyAreas = parm[5].ToString();//工作地點
            string CurAddress = parm[6].ToString();//現居地址
            string ProLicenses = parm[7].ToString();//證照資格
            string JobCompany = parm[8].ToString();//經歷公司名稱
            string Keyword = parm[9].ToString();//全文搜尋
            int Andor = int.Parse(parm[10].ToString());

            string js = string.Empty;
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
            {
                string SQL = "exec procSearchREC_User '" + NameC + "','" + Gender + "','" + Age1 + "','" + Age2 + "','" + EduID + "','" + DutyAreas + "','" + CurAddress + "','" + ProLicenses + "','" + JobCompany + "','" + Keyword + "'," + Andor;
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
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //--------------------------------寫入招募系統--------------------------------
        public object[] InsertUserbyREC_User(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string UserID = parm[0];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;

            string sLoginDB = "JBRecruit";
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
            {
                string SQL = "exec procInsertUserbyREC_User '" + UserID + "','" + username+"'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

            }
            catch
            {
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //推薦履歷 報表--------------------------------------------------------------------------------
        public object[] procReportRecommendResume(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string UserID = parm[0].ToString();
            string JobID = parm[1].ToString();
            string AutoKey = parm[2].ToString();
            int iType = int.Parse(parm[3].ToString());
            string sDyItem = parm[4].ToString();

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
            {
                string SQL = "exec procReportREC_UserRecommendResume '" + UserID + "','" + JobID + "','" + AutoKey + "','" + username + "'," + iType + ",'" + sDyItem + "'";
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
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        


       

    }
}
