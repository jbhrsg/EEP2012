using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using JBTool;

namespace sHUTUser
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
        //履歷編號=> ex: U10900001--民國年+月+日+4碼流水號
        public string HunterUserIDFixed()
        {           
            string VoucherNoTitle = "U";           
            DateTime dDate = DateTime.Now;
            return VoucherNoTitle + (dDate.Year - 1911).ToString().Trim();
        }

        //履歷資料
        private void ucHUT_User_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_User.SetFieldValue("CreateDate", DateTime.Now);
            ucHUT_User.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucHUT_User_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_User.SetFieldValue("LastUpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_User.SetFieldValue("LastUpdateBy", username);
        }
        //工作經驗
        private void ucHUT_UserCareer_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_UserCareer.SetFieldValue("CreateDate", DateTime.Now);
            ucHUT_UserCareer.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucHUT_UserCareer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_UserCareer.SetFieldValue("LastUpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_UserCareer.SetFieldValue("LastUpdateby", username);
        }
        //語文能力
        private void ucHUT_UserLang_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_UserLang.SetFieldValue("CreateDate", DateTime.Now);
            ucHUT_UserLang.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucHUT_UserLang_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_UserLang.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //--------------------面談紀錄維護-----------------------------------------
        private void ucHUT_UserContactRecord_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_UserContactRecord.SetFieldValue("UpdateDate", DateTime.Now);
        }
        private void ucHUT_UserContactRecord_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_UserContactRecord.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_UserContactRecord.SetFieldValue("UpdateBy", username);
        }
        //================================================================================================================================//
        //工作經驗-產業類別刪除檢查
        public object[] CheckMasterDelete(object[] objParam)
        {
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
                int CategoryID = int.Parse(objParam[0].ToString());
                string sql = "SELECT COUNT(*) AS CNT from [Hunter].dbo.HUT_UserCareer where CategoryID = " + CategoryID;
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

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
        //===============================履歷檔案==============================================================
        private void ucHUT_UserFile_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_UserFile.SetFieldValue("UpdateDate", DateTime.Now);
        }

        //-----------刪除職缺檔案--------------------------
        public object[] DelUserFile(object[] objParam)
        {
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
                string cnt = "0";
                string[] parm = objParam[0].ToString().Split(',');
                string AutoKey = parm[0];
                string UserFile = parm[1];//新的值

                string sql = "Delete [Hunter].dbo.HUT_UserFile where AutoKey=" + AutoKey;
                this.ExecuteCommand(sql, connection, transaction);
                string sourcePath = @"\\192.168.10.70\c$\inetpub\wwwroot\JQWebClient_JBHR_SP7\Files\Hunter\User";
                string destFile = System.IO.Path.Combine(sourcePath, UserFile);

                if (System.IO.File.Exists(destFile))
                {
                    System.IO.File.Delete(destFile);
                }
                else
                {
                    cnt = "1";
                    //cnt = destFile;
                }
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
        //================================================================================================================================//
        //查詢履歷資料
        public object[] UsersQuery(object[] objParam)
        {
            //string Keyword = (string)objParam[0].ToString().Trim();
            string[] parm = objParam[0].ToString().Split('*');
            string Age1 = parm[0].ToString();//年齡
            string Age2 = parm[1].ToString();
            string EduID = parm[2].ToString();//學歷
            string LangID = parm[3].ToString();//語言
            string LangLevel = parm[4].ToString();//程度
            string GoodTools = parm[5].ToString();//電腦技能
            string LicenseQA = parm[6].ToString();//證照資格
            string ExpDutyArea = parm[7].ToString();//期望工作地
            string ExpCategory = parm[8].ToString();//期望產業
            string ExpJobType = parm[9].ToString();//期望職務
            string ComName = parm[10].ToString();//經歷公司名稱
            string DutyTitle = parm[11].ToString();//經歷職稱
            string CategoryID = parm[12].ToString();//產業類別
            string DutyContent = parm[13].ToString();//經歷工作內容
            string NameC = parm[14].ToString();//人才姓名
            string Keyword = parm[15].ToString();//全文搜尋
            int Andor = int.Parse(parm[16].ToString());
            int blacklist = int.Parse(parm[17].ToString());//黑名單
            string UpdateDay = parm[18].ToString();//更新日期=>1最新,3三日內,7一周內,14二周內,30一個月內,0不拘

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
                string SQL = "exec procSearchUsers N'" + NameC + "','" + Age1 + "','" + Age2 + "','" + EduID + "','" + LangID + "','" + LangLevel + "','" + GoodTools + "','" + LicenseQA + "','" +
                        ExpDutyArea + "','" + ExpCategory + "','" + ExpJobType + "','" + ComName + "','" + DutyTitle + "','" + CategoryID + "','" + DutyContent + "','" + Keyword + "'," + Andor + "," + blacklist + ",'" + UpdateDay +"'";
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

        //推薦履歷 報表--------------------------------------------------------------------------------
        public object[] procReportRecommendResume(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string UserID =parm[0].ToString();
            string JobID = parm[1].ToString();
            string AutoKey = parm[2].ToString();

            int iType = int.Parse(parm[3].ToString());

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
                string SQL = "exec procReportRecommendResume '" + UserID + "','" + JobID + "','" + AutoKey + "','" + username + "'," + iType;
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
        //請款明細表 報表--------------------------------------------------------------------------------
        public object[] procReportPleasePay(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string UserID = parm[0].ToString();
            string JobID = parm[1].ToString();
            string AutoKey = parm[2].ToString();

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
                string SQL = "exec procReportPleasePay '" + UserID + "','" + JobID + "','" + AutoKey + "','" + username + "'";
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
        //執案作業進度 報表--------------------------------------------------------------------------------
        public object[] procReportJobSchedule(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string Type = parm[0].ToString();
            string CustID = parm[1].ToString();
            string JobName = parm[2].ToString();
            string SalesTeamID = parm[3].ToString();
            string JobStatus = parm[4].ToString();
            string HunterID = parm[5].ToString();
            string HunterIDAssist = parm[6].ToString();
            string SDate = parm[7].ToString();
            string EDate = parm[8].ToString();
            string iDay1 = parm[9].ToString();
            string iDay2 = parm[10].ToString();
            string SADate = parm[11].ToString();
            string EADate = parm[12].ToString();
            string AssignID = parm[13].ToString();
            string AssignHunterID = parm[14].ToString();
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //建立資料庫連結
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
                string SQL = "exec procReportJobSchedule '" + Type + "','" + CustID + "','" + JobName + "','" + SalesTeamID + "','" + JobStatus + "','" + HunterID + "','" + HunterIDAssist + "','" +
                    SDate + "','" + EDate + "','" + iDay1 + "','" + iDay2 + "','" + SADate + "','" + EADate + "','" + AssignID + "','" + AssignHunterID + "'";

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

        //銷貨總表 報表--------------------------------------------------------------------------------
        public object[] ReportSalesInvoice(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0].ToString();
            string EDate = parm[1].ToString();
            string CustID = parm[2].ToString();
            string SalesTeamID = parm[3].ToString();
            string HunterID = parm[4].ToString();
            string AssignHunterID = parm[5].ToString();
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //建立資料庫連結
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
                string SQL = "exec procReportSalesInvoice '" + SDate + "','" + EDate + "','" + CustID + "','" + SalesTeamID + "','" + HunterID + "','" + AssignHunterID + "'";

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

        //執案預估營收 報表--------------------------------------------------------------------------------
        public object[] procReportRevenue(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string Type = parm[0].ToString();
            string CustID = parm[1].ToString();
            string JobName = parm[2].ToString();
            string HunterID = parm[3].ToString();
            string SalesTeamID = parm[4].ToString();
            string SADate = parm[5].ToString();
            string EADate = parm[6].ToString();
            string DraftS = parm[7].ToString();
            string DraftE = parm[8].ToString();
            string AssignHunterID = parm[9].ToString();

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
                string SQL = "exec procReportRevenue '" + Type + "','" + CustID + "','" + JobName + "','" + HunterID + "','" + SalesTeamID + "','"
                    + SADate + "','" + EADate + "','" + DraftS + "','" + DraftE + "','" + AssignHunterID + "'";

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
        //執案預估營收 Grid--------------------------------------------------------------------------------
        public object[] procDisplayRevenue(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string Type = parm[0].ToString();
            string CustID = parm[1].ToString();
            string JobName = parm[2].ToString();
            string HunterID = parm[3].ToString();
            string SalesTeamID = parm[4].ToString();
            string SADate = parm[5].ToString();
            string EADate = parm[6].ToString();
            string DraftS = parm[7].ToString();
            string DraftE = parm[8].ToString();
            string AssignHunterID = parm[9].ToString();
            string Class = parm[10].ToString();

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
                string SQL = "exec procDisplayRevenue '" + Type + "','" + CustID + "','" + JobName + "','" + HunterID + "','" + SalesTeamID + "','"
                    + SADate + "','" + EADate + "','" + DraftS + "','" + DraftE + "','" + AssignHunterID + "'," + Class;

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
        //執案預估營收 匯出
        public object[] procExcelRevenue(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string Type = parm[0].ToString();
            string CustID = parm[1].ToString();
            string JobName = parm[2].ToString();
            string HunterID = parm[3].ToString();
            string SalesTeamID = parm[4].ToString();
            string SADate = parm[5].ToString();
            string EADate = parm[6].ToString();
            string DraftS = parm[7].ToString();
            string DraftE = parm[8].ToString();
            string AssignHunterID = parm[9].ToString();
            string Class = parm[10].ToString();

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

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = "exec procDisplayRevenue '" + Type + "','" + CustID + "','" + JobName + "','" + HunterID + "','" + SalesTeamID + "','"
                    + SADate + "','" + EADate + "','" + DraftS + "','" + DraftE + "','" + AssignHunterID + "'," + Class;

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
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };

        }
        //推薦作業檢查=>求推薦筆數---------------------------------------------------------------------------
        public object[] ReturnJobAssignLogsCount(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
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
                string[] parm = objParam[0].ToString().Split(',');
                int JobID = int.Parse(parm[0].ToString());
                string UserID = parm[1].ToString();

                string sql = "SELECT COUNT(*) AS CNT from HUT_JobAssignLogs where JobID = " + JobID + " and UserID='" + UserID + "' and AssignID=1";
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
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //推薦作業檢查=>求<報到日期是否有錄取的紀錄---------------------------------------------------------------------------
        public object[] ReturnJobAssignLogsAdmitCount(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
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
                string[] parm = objParam[0].ToString().Split(',');
                int JobID = int.Parse(parm[0].ToString());
                string UserID = parm[1].ToString();
                string dDate = parm[2].ToString();//報到日期 //1推薦,2面試,3錄取,4未錄取,5報到,6未報到,7離職

                string sql = "SELECT COUNT(*) AS CNT from HUT_JobAssignLogs where JobID = " + JobID + " and UserID='" + UserID + "' and AssignID=3 and AssignTime<='" + dDate+"'";
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
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //離職日期若大於報到日期+保證天數-1，要檢查不能勾選退費欄位---------------------------------------------------------------------------
        public object[] ReturnJobAssignLogsAssureDayCount(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
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
                string[] parm = objParam[0].ToString().Split(',');
                int JobID = int.Parse(parm[0].ToString());
                string UserID = parm[1].ToString();
                DateTime LeaveDate =  DateTime.Parse(parm[2].ToString());//離職日期 //1推薦,2面試,3錄取,4未錄取,5報到,6未報到,7離職

                string sql = "SELECT Dateadd(day,AssureDay,AssignTime)-1 as AssureDate from HUT_JobAssignLogs where JobID = " + JobID + " and UserID='" + UserID + "' and AssignID=5 and AssignTime<='" + LeaveDate.ToShortDateString() + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                DateTime AssureDate = DateTime.Parse(dsTemp.Tables[0].Rows[0]["AssureDate"].ToString());
                int cnt = 0;
                if (LeaveDate > AssureDate)
                {
                    cnt = 1;
                }

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
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //履歷檢查=>檢查電話1 or 電話2不可以重複---------------------------------------------------------------------------
        public object[] ReturnUserCount(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
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
                string[] parm = objParam[0].ToString().Split(',');
                string MobileNo1 = parm[0].ToString();
                string MobileNo2 = parm[1].ToString();
                string UserID = parm[2].ToString();
                
                string sql = "SELECT COUNT(*) AS CNT from HUT_User where 1=1";
                if (UserID != "")
                {
                    sql = sql + " and UserID!='" + UserID + "'";
                }
                if (MobileNo1 != "")
                {
                    sql = sql + " and ( MobileNo1='" + MobileNo1 + "'";
                }
                if (MobileNo2 != "")
                {
                    if (MobileNo1 == "")
                    {
                        sql = sql + " and MobileNo2='" + MobileNo2 + "'";
                    }
                    else
                    {
                        sql = sql + " or MobileNo2='" + MobileNo2 + "')";
                    }
                }
                else
                {
                    sql = sql + ")";
                }
               
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
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //取得登入者是否屬於中高階的顧問
        public object[] ReturnHunterCount(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
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
                string UserName = objParam[0].ToString().Trim();
                string sql = "SELECT count(*) as cnt from HUT_Hunter where HunterName like '%" + UserName + "%'";
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
                ReleaseConnection(sLoginDB, connection);

                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //取得登入顯示的資訊(新增職缺+關閉職缺+新增履歷)
        public object[] ReturnCreateInfo(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
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
                string SQL = "exec procDisplayCreateInfo ";

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

        private void ucHUT_JobAssignLogs_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //銷貨收入申請(業務人員有填寫)
            string Autokey = ucHUT_JobAssignLogs.GetFieldCurrentValue("Autokey").ToString();//流水號     
            string SalesID = ucHUT_JobAssignLogs.GetFieldCurrentValue("SalesID").ToString();//業務員工代號    
            string InvoiceYM = ucHUT_JobAssignLogs.GetFieldCurrentValue("InvoiceYM").ToString();//發票年月  

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

         
        }
        //寫銷貨收入資料
        public object[] InsertSalesMaster(object[] objParam)
        {
            string js = string.Empty;

            string[] parm = objParam[0].ToString().Split(',');
            string Autokey = parm[0].ToString();
            string SalesID = parm[1].ToString();
            string InvoiceYM = parm[2].ToString();
            string Attach = parm[3].ToString();
            
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string sLoginDB = "JBERP";
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

                string sql = "exec procImportSalesFromHunter '" + SalesID + "','" + InvoiceYM + "'," + Autokey + ",'" + username + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string SalesNO = dsTemp.Tables[0].Rows[0]["SalesNO"].ToString();
                string RoleID = dsTemp.Tables[0].Rows[0]["RoleID"].ToString();
                transaction.Commit();

                //自動起單銷貨收入申請
                if (SalesID != "" && SalesNO != "")//有SalesID，有SalesNO
                {
                    RunSalesMaster(SalesID, SalesNO, RoleID,Attach);
                }
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        public void RunSalesMaster(string SalesID, string SalesNO, string RoleID, string Attach)
        {
            /*模擬該user登入，userid請使用全小寫*/
            string username = SrvGL.GetUserName(SalesID.ToLower());
            object[] cInfo = new object[] { this.ClientInfo[0], -1, 1, "" };
            SrvGL.LogUser(SalesID, username, "", 1);   //模擬登入 
            ((object[])cInfo[0])[1] = SalesID;

                EEPRemoteModule ep = new EEPRemoteModule();
                object[] ret1 = ep.CallFLMethod(cInfo, "Submit", new object[]{
                    null,
                        new object[]{
                        "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\ERPSalesApply.xoml",
                        //"D:\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\ERPSalesApply.xoml",
                        string.Empty,////空白即可，系統使用
                        0,//是否為重要申請
                        0,//是否為緊急申請
                        "",//提交意見說明
                        RoleID,//"1060053",//申請者的RoleID(角色編號)
                        "sERPSalesApply.SalesMaster",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                        0,//系統使用
                        "0",//組織類別編號ex:0公司組織、1福利委員會
                        Attach //附件
                        },
                    new object[]{
                    "SalesNO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "SalesNO='"+ SalesNO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
                    }
                });
                SrvGL.LogUser(SalesID, username, "", -1);  //模擬登出
            
        }
        //推薦作業提醒查詢 報表--------------------------------------------------------------------------------
        public object[] JobAssignLogsRemind(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CustID = parm[0].ToString();
            string AssignID = parm[1].ToString();
            string SalesTeamID = parm[2].ToString();
            string AssignHunterID = parm[3].ToString();
            string SalesID = parm[4].ToString();
            string SDate = parm[5].ToString();
            string EDate = parm[6].ToString();
         
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
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
                string SQL = "exec procDisplayJobAssignLogsRemind 1,'" + CustID + "','" + AssignID + "','" + SalesTeamID + "','" + AssignHunterID + "','" + 
                     SalesID + "','" + SDate + "','" + EDate +"'";

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
