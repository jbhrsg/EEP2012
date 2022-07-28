using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using Newtonsoft.Json;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace _HRM_REC_User_Management2
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

        public object[] UpdateRecReference(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string UserID = parm[0];

            string js = string.Empty;

            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                string SQL = "exec procSyncMutiselectTextFieldAll '" + UserID + "'";
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

            //------------------------------------------------同步資料------------------------------------------------
            //建立資料庫連結
            //IDbConnection connection2 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB2 = "JBRecruit";
            string Loginuserid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(Loginuserid.ToLower());

            IDbConnection connection2 = (IDbConnection)AllocateConnection(sLoginDB2);

            //當連線狀態不等於open時，開啟連結
            if (connection2.State != ConnectionState.Open)
            {
                connection2.Open();
            }
            //開始transaction
            IDbTransaction transaction2 = connection2.BeginTransaction();
            try
            {   //修改已寫狀態
                string sql = "exec procUpdateUserbyREC_User '" + UserID + "','" + username+"'";
                this.ExecuteSql(sql, connection2, transaction2);
            }
            catch
            {
                transaction2.Rollback();
            }
            finally
            {
                transaction2.Commit();
                ReleaseConnection(sLoginDB2, connection2);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection2);
            }

            return new object[] { 0, js };
        }
        public string GetRecReference(string sTable, string sVal)
        {
            string sContents = "";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                string sql = "exec procReturnRecReference '" + sTable + "','" + sVal + "'" + "\r\n";

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
        //--------------------健保眷屬維護-----------------------------------------
        private void ucREC_UserFamily_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            //身分證判定性別 2女=>F , 1男=>M
            string UserFamilyIdno = ucREC_UserFamily.GetFieldCurrentValue("UserFamilyIdno").ToString();
            string UserFamilySex = "";
            if (UserFamilyIdno.Substring(1, 1) == "1")
            {
                UserFamilySex = "M";
            }
            else UserFamilySex = "F";
            ucREC_UserFamily.SetFieldValue("UserFamilySex", UserFamilySex);

            ucREC_UserFamily.SetFieldValue("CreateDate", DateTime.Now);
            ucREC_UserFamily.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucREC_UserFamily_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            string UserFamilyIdno = ucREC_UserFamily.GetFieldCurrentValue("UserFamilyIdno").ToString();
            string UserFamilySex = "";
            if (UserFamilyIdno.Substring(1, 1) == "1")
            {
                UserFamilySex = "M";
            }
            else UserFamilySex = "F";
            ucREC_UserFamily.SetFieldValue("UserFamilySex", UserFamilySex);

            ucREC_UserFamily.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucREC_UserFamily.SetFieldValue("UpdateBy", username);
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

        //今日新增筆數呈現---------------------------------------------------------------------------
        public object[] ReturnREC_UserAddNowCount(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
            string sLoginDB = "JBHRIS_DISPATCHTest";
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

                string sql = "exec procReturnREC_UserAddNowCount";

                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string sCountInfo = dsTemp.Tables[0].Rows[0]["sCountInfo"].ToString().Trim();

                ////Indented縮排 將資料轉換成Json格式
                js = sCountInfo;//JsonConvert.SerializeObject(sCountInfo, Formatting.Indented);
                transaction.Commit();

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
            string AssignJob = parm[7].ToString();//派任職缺
            string ProLicenses = parm[8].ToString();//證照資格
            string JobCompany = parm[9].ToString();//經歷公司名稱
            string SalesTeam = parm[10].ToString();//服務地區
            string ServiceConsultants = parm[11].ToString();//服務人員
            string Status = parm[12].ToString();//處理狀態
            string SDate = parm[13].ToString();//修改日期
            string EDate = parm[14].ToString();//
            string SCDate = parm[15].ToString();//建立日期
            string ECDate = parm[16].ToString();//

            string PFMemberID = parm[17].ToString();//推薦樁腳
            string IsPilefoot = parm[18].ToString();//是否樁腳

            string ShowCount = parm[19].ToString();//顯示筆數
            string Keyword = parm[20].ToString();//全文搜尋
            int Andor = int.Parse(parm[21].ToString());

            string js = string.Empty;
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                string SQL = "exec procSearchREC_User '" + NameC + "','" + Gender + "','" + Age1 + "','" + Age2 + "','" + EduID + "','" + DutyAreas + "','" + CurAddress + "','" + AssignJob + "','" + ProLicenses + "','" + JobCompany + "','" + SalesTeam + "','" + ServiceConsultants + "','" + Status + "','" +
                   SDate + "','"+ EDate + "','" +SCDate + "','"+ ECDate + "','" + PFMemberID + "','" + IsPilefoot + "','"+ ShowCount + "','" + Keyword + "'," + Andor;
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
        //履歷檢查=>檢查身分證字號不可以重複---------------------------------------------------------------------------
        public object[] ReturnUserCount(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
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
                string[] parm = objParam[0].ToString().Split(',');
                string PID = parm[0].ToString();

                string sql = "SELECT COUNT(*) AS CNT from [User] where IDNumber='" + PID + "'";

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

        //人才刪除作業------------------------------------------------------------------------
        public object[] DeleteREC_UserAbount(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                //string[] parm = objParam[0].ToString().Split(',');
                string UserID = objParam[0].ToString();

                string SQL = "Delete from REC_User where UserID='" + UserID + "' Delete from REC_UserCareer where UserID='" + UserID + "' Delete from REC_UserFamily where UserID='" + UserID + "'";
                this.ExecuteSql(SQL, connection, transaction);
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


        ////派任紀錄刪除=>刪除最新一筆(取消刪除動作並執行新指令)   
        public object[] JobAssignLogsIsActive(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AutoKey = (string)parm[0];
           
            string js = string.Empty;

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                string SQL = "update REC_JobAssignLogs set IsActive=0,LastUpdateBy='" + LoginUser + "',LastUpdateDate=getdate() from REC_JobAssignLogs where AutoKey=" + AutoKey ;
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
                ReleaseConnection(sLoginDB, connection);

            }
            return new object[] { 0, js };
        }
        //求得最新一筆的紀錄(派任狀態、派任日期)---------------------------------------------------------------------------
        public object[] ReturnNewJobAssignLogs(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                string AssignTime = parm[2].ToString();
                string sql = "";
                if (AssignTime == "1900/01/01")//新增
                {
                    sql = "SELECT top 1 AssignID,Convert(nvarchar(10),AssignTime,111) as AssignTime from REC_JobAssignLogs where IsActive=1 and JobID = " + JobID + " and UserID='" + UserID + "' order by AssignTime desc";
                }
                else//編輯
                {
                    sql = "SELECT top 1 AssignID,Convert(nvarchar(10),AssignTime,111) as AssignTime from REC_JobAssignLogs where IsActive=1 and JobID = " + JobID + " and UserID='" + UserID + "' and AssignTime < '" + AssignTime + "' order by AssignTime desc";
                }

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //int cnt = int.Parse(dsTemp.Tables[0].Rows[0]["AssignID"].ToString());

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
                transaction.Commit();
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        //新增派任
        private void ucRec_JobAssignLogs_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucRec_JobAssignLogs.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucRec_JobAssignLogs_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            ////發信
            //SendAssignMail(aUserID[i], aNameC[i], aEmail[i], AssignMail);
        }

        //將人才群加入派任紀錄
        public object[] AddJobAssignLogsMore(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string sUserID = (string)parm[0];
            string sNameC = (string)parm[1];
            string sEmail = (string)parm[2];

            string CustID = (string)parm[3];
            string JobID = (string)parm[4];
            string RecommendCust = (string)parm[5];
            string RecID = (string)parm[6];
            string AssignID = (string)parm[7];
            string AssignTime = (string)parm[8];
            string AssignContent = (string)parm[9];
            string bAssignMail = (string)parm[10];
            string AssignMail = (string)parm[11];

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                string SQL = "exec procInsertJobAssignLogsMore '" + sUserID + "','" + CustID + "'," + JobID + ",'" + RecommendCust + "'," + RecID + "," + AssignID + ",'" + AssignTime + "','" + AssignContent + "'," + bAssignMail + ",'" + AssignMail + "','" + CreateBy + "'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

                if (bAssignMail == "1")
                {
                    //發信
                    string[] aUserID = sUserID.Split(',');
                    string[] aNameC = sNameC.Split(',');
                    string[] aEmail = sEmail.Split(',');

                    for (int i = 0; i < aUserID.Length; i++)
                    {
                        if (aUserID[i].Trim() != "")
                        {
                            SendAssignMail(aUserID[i], aNameC[i], aEmail[i], AssignMail);

                        }
                    }
                }
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
        //-------------------------------------------------------------------------------------------------------------------------------------

        //待報到寄送Email
        public void SendAssignMail(string UserID, string NameC, string Email, string AssignMail)
        {
            //設定Email相關資訊
            string MailServerName = "smtp.office365.com";       //MailServerName
            int iPort = 25;                                     //MailServer 阜號
            bool IsUseDefaultCredentials = true;
            bool IsSSL = true;
            string FromID = "service@jbjob.com.tw";             //寄件者帳號
            string FromPW = "shjnmmvgmvtgswxy";                 //寄件者密碼

            string FromName = "傑報人力資源顧問有限公司";
            string Subject = "錄取通知信";
            string Body = GetAssignMailBody(NameC, AssignMail);
            //寄出eMail
            //測試
            Email = "rebecca@jbjob.com.tw";
            bool SendOK = SendMail(MailServerName, iPort, FromID, IsUseDefaultCredentials, IsSSL, FromID, FromPW, Email, Subject, Body, FromName);
            if (SendOK)
            {
                //建立資料庫連結
                //IDbConnection connection2 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                string sLoginDB2 = "JBHRIS_DISPATCHTest";
                IDbConnection connection2 = (IDbConnection)AllocateConnection(sLoginDB2);

                //當連線狀態不等於open時，開啟連結
                if (connection2.State != ConnectionState.Open)
                {
                    connection2.Open();
                }
                //開始transaction
                IDbTransaction transaction2 = connection2.BeginTransaction();
                try
                {   //寫入發送紀錄
                    string sql = "EXEC procInsertREC_UserEmailLogs '" + Email + "','" + NameC + "','" + UserID + "',''," + 3;
                    this.ExecuteSql(sql, connection2, transaction2);
                }
                catch
                {
                    transaction2.Rollback();
                }
                finally
                {
                    transaction2.Commit();
                    ReleaseConnection(sLoginDB2, connection2);
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection2);
                }
            }

        }
        //發送email- 單筆
        public object[] AssignLogsToMail(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string UserID = (string)parm[0];
            string NameC = (string)parm[1];
            string Email = (string)parm[2];
            string AssignMail = (string)parm[3];

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
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

                SendAssignMail(UserID, NameC, Email, AssignMail);
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
        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------

        //設定服務人員Email,發信內容
        string GetAssignMailBody(string NameC, string AssignMail)
        {
            string strToBody = "";
            DateTime dt = DateTime.Now;
            strToBody += "<script type='text/css'>a.button {-webkit-appearance: button;-moz-appearance: button;appearance: button;text-decoration: none;color: initial;}</script>" +
                           "<table border='0' width='92%' align='left'>" +
                           "<tr><td align='left'><a  href='http://www.jbjob.com.tw/'><img src='http://www.jbhr.com.tw/jqwebclient/Files/JBERP_SalesPaper/logo.png'  alt='傑報人力資源服務集團' class='fusion-logo-1x fusion-standard-logo'></a>" +
                           "<tr><td align='left'><br/>Hello，<b>" + NameC.Trim() + "</b>您好：</td><tr>" +
                           "<tr><td align='left'><br/></td></tr>" +
                           "<tr><td align='left'><label style='color:black'>" + AssignMail.Replace("\n", "<br/>").Replace(" ", "&nbsp;") + "</label></td></tr>" +
                           "<tr><td align='left'><br/></td></tr>" +
                           "<tr><td align='left'><a href='https://www.jbhr.com.tw/jqweb2015/RWDLOGON.aspx'><label style='color:blue'><b>登入連結</b></label><alt='傑報人力資源服務集團'></a>" +
                           "<tr><td align='left'>發送時間:" + dt.ToString() + "</td></tr>" +

                           "</table>";
            return strToBody;
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
                string SQL = "exec procInsertUserbyREC_User '" + UserID + "','" + username + "'";
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

            //建立資料庫連結
            //IDbConnection connection2 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB2 = "JBHRIS_DISPATCHTest";
            IDbConnection connection2 = (IDbConnection)AllocateConnection(sLoginDB2);

            //當連線狀態不等於open時，開啟連結
            if (connection2.State != ConnectionState.Open)
            {
                connection2.Open();
            }
            //開始transaction
            IDbTransaction transaction2 = connection2.BeginTransaction();
            try
            {   //修改已寫狀態
                string sql = "exec procUpdateREC_UserIsRec '" + UserID + "'";
                this.ExecuteSql(sql, connection2, transaction2);
            }
            catch
            {
                transaction2.Rollback();
            }
            finally
            {
                transaction2.Commit();
                ReleaseConnection(sLoginDB2, connection2);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection2);
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
            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                string SQL = "exec procReportREC_UserRecommendResume2 '" + UserID + "','" + JobID + "','" + AutoKey + "','" + username + "'," + iType + ",'" + sDyItem + "'";
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
        //取得登入者是否是主管
        public object[] ReturnIsManager(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
            string sLoginDB = "JBHRIS_DISPATCHTest";
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
                string UserID = objParam[0].ToString().Trim();
                string sql = "SELECT count(*) as cnt from REC_Consultants where EmpID='" + UserID + "' and IsManager=1";
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
        //刪除事件-----------------------------------------------
        private void ucRECUser_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            //刪除 USERS 與 USERSGROUP
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            string MemberID = ucRECUser.GetFieldOldValue("MemberID").ToString();

            //------------------------------------------------------------------------------------------------------------------

            try
            {
                string SQL = "delete from USERS where USERID='" + MemberID + "' delete from USERGROUPS where USERID='" + MemberID + "'";
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
                ReleaseConnection(sLoginDB, connection);
            }


        }
        //判斷是否指定服務人員=>發email
        private void ucRECUser_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //服務人員
            string oServiceConsultants = ucRECUser.GetFieldOldValue("ServiceConsultants").ToString();
            string ServiceConsultants = ucRECUser.GetFieldCurrentValue("ServiceConsultants").ToString();
            if (oServiceConsultants != ServiceConsultants && ServiceConsultants!="")
            {

                string ConsultantName = "";
                string ConsultantEmail = "";

                //建立資料庫連結
                //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                string sLoginDB = "JBHRIS_DISPATCHTest";
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
                    string sql = "select ConsultantName,ConsultantEmail from REC_Consultants where ID= " + ServiceConsultants;
                    DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                    ConsultantName = dsTemp.Tables[0].Rows[0]["ConsultantName"].ToString();
                    ConsultantEmail = dsTemp.Tables[0].Rows[0]["ConsultantEmail"].ToString();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Commit();
                    ReleaseConnection(sLoginDB, connection);
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }

                //設定Email相關資訊
                string MailServerName = "smtp.office365.com";       //MailServerName
                int iPort = 25;                                     //MailServer 阜號
                bool IsUseDefaultCredentials = true;
                bool IsSSL = true;
                string FromID = "service@jbjob.com.tw";             //寄件者帳號
                string FromPW = "shjnmmvgmvtgswxy";                 //寄件者密碼
                string To = ConsultantEmail; //         //收信者

                string UserID = ucRECUser.GetFieldCurrentValue("UserID").ToString();
                string MemberID = ucRECUser.GetFieldCurrentValue("MemberID").ToString();
                string NameC = ucRECUser.GetFieldCurrentValue("NameC").ToString();
                string MobileNo = ucRECUser.GetFieldCurrentValue("MobileNo").ToString();
                string Email = ucRECUser.GetFieldCurrentValue("Email").ToString();
                string Consultants = ucRECUser.GetFieldCurrentValue("ServiceConsultants").ToString();
                string FromName = "傑報人力資源顧問有限公司";
                string Subject = NameC + "投遞履歷通知信";
                string Body = GetMailBody(NameC, MobileNo, Email, ConsultantName);
                //寄出eMail
                bool SendOK = SendMail(MailServerName, iPort, FromID, IsUseDefaultCredentials, IsSSL, FromID, FromPW, To, Subject, Body, FromName);
                if (SendOK)
                {
                    //建立資料庫連結
                    //IDbConnection connection2 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                    string sLoginDB2 = "JBHRIS_DISPATCHTest";
                    IDbConnection connection2 = (IDbConnection)AllocateConnection(sLoginDB2);

                    //當連線狀態不等於open時，開啟連結
                    if (connection2.State != ConnectionState.Open)
                    {
                        connection2.Open();
                    }
                    //開始transaction
                    IDbTransaction transaction2 = connection2.BeginTransaction();
                    try
                    {   //寫入發送紀錄
                        string sql = "EXEC procInsertREC_UserEmailLogs '" + To + "','" + ConsultantName + "','" + UserID + "','" + MemberID + "'," +2;
                        this.ExecuteSql(sql, connection2, transaction2);
                    }
                    catch
                    {
                        transaction2.Rollback();
                    }
                    finally
                    {
                        transaction2.Commit();
                        ReleaseConnection(sLoginDB2, connection2);
                        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection2);
                    }
                }

            }
        }
    
       //寄送服務人員Email
        public bool SendMail(string sMailServerName, int iPort, string sFrom, bool bIsUseDefaultCredentials, bool bSSL, string sFromID, string sFromPW, string sTo, string sSubject, string sBody,string sFromName)
        {
           try
            {
                    //.Net FrameWork 支援TLS1.1 1.2通訊協定
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                    //string sFromName = "Service";
                    string [] aTo = sTo.Split(',');
                    string[] aToName = new string[(aTo.Length)];
                    for (int i = 0; i < aTo.Length; i++){
                        if (aTo[i].Trim() != ""){
                            aToName[i] = aTo[i].Substring(0,aTo[i].IndexOf('@') - 0);
                        }
                    }
                    MailMessage message = new MailMessage();
                    for (int i = 0; i < aToName.Length; i++){
                        message.To.Add(new MailAddress(aTo[i],aToName[i],Encoding.Default));
                    }
                    message.From = new MailAddress(sFrom, sFromName, Encoding.Default);
                    message.Subject = sSubject;
                    message.Body = sBody;
                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.High;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.SubjectEncoding = System.Text.Encoding.Default;
                    SmtpClient mailClient = new SmtpClient(sMailServerName);
                    mailClient.ServicePoint.MaxIdleTime = Convert.ToInt32(5000); //0.5 sec
                    mailClient.Port = iPort;
                    mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //mailClient.Timeout = 10000;
                    mailClient.EnableSsl = bSSL;
                    if (bIsUseDefaultCredentials)
                    {
                        mailClient.UseDefaultCredentials = true;
                        mailClient.Credentials = new System.Net.NetworkCredential(sFromID, sFromPW);
                    }
                    else
                        mailClient.UseDefaultCredentials = false;
                    mailClient.Send(message);
                    mailClient.Dispose();
                    return true;
                }
             catch (Exception ex)
            {
                return false;
            }

        }
       //設定服務人員Email,發信內容
        string GetMailBody(string NameC, string MobileNo, string Email, string ConsultantName)
        {
            string strToBody = "";
            DateTime dt = DateTime.Now;
            strToBody += "<script type='text/css'>a.button {-webkit-appearance: button;-moz-appearance: button;appearance: button;text-decoration: none;color: initial;}</script>" +
                           "<table border='0' width='92%' align='left'>" +
                           "<tr><td align='left'><a  href='http://www.jbjob.com.tw/'><img src='http://www.jbhr.com.tw/jqwebclient/Files/JBERP_SalesPaper/logo.png'  alt='傑報人力資源服務集團' class='fusion-logo-1x fusion-standard-logo'></a>" +
                           "<tr><td align='left'><br/>Hello，<b>" + ConsultantName.Trim() + "</b>您好：</td><tr>" +
                           "<tr><td align='left'><br/></td></tr>" +
                           "<tr><td align='left'>以下是求職者的資料，請立即主動聯繫對方唷！</td></tr>" +
                           "<tr><td align='left'>人才姓名：<label style='color:red'>" + NameC.Trim() + "</label></td></tr>" +
                           "<tr><td align='left'>行動電話：<label style='color:red'>" + MobileNo.Trim() + "</label></td></tr>" +
                           "<tr><td align='left'>郵件信箱：<label style='color:red'>" + Email.Trim() + "</label></td></tr>" +
                           "<tr><td align='left'><a href='https://www.jbhr.com.tw/jqweb2015/RWDLOGON.aspx'><label style='color:blue'><b>登入連結</b></label><alt='傑報人力資源服務集團'></a>" +
                           "<tr><td align='left'>發送時間:" + dt.ToString() + "</td></tr>" +

                           "</table>";
            return strToBody;
        }



        
        



    }
}
