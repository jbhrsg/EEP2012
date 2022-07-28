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
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //�ק��r
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

            //------------------------------------------------�P�B���------------------------------------------------
            //�إ߸�Ʈw�s��
            //IDbConnection connection2 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB2 = "JBRecruit";
            string Loginuserid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(Loginuserid.ToLower());

            IDbConnection connection2 = (IDbConnection)AllocateConnection(sLoginDB2);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection2.State != ConnectionState.Open)
            {
                connection2.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction2 = connection2.BeginTransaction();
            try
            {   //�ק�w�g���A
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
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
        //--------------------���ͬ������@-----------------------------------------
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
        //--------------------���O���ݺ��@-----------------------------------------
        private void ucREC_UserFamily_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            //�����ҧP�w�ʧO 2�k=>F , 1�k=>M
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

        //�u�@�g��
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

        //����s�W���Ƨe�{---------------------------------------------------------------------------
        public object[] ReturnREC_UserAddNowCount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {

                string sql = "exec procReturnREC_UserAddNowCount";

                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string sCountInfo = dsTemp.Tables[0].Rows[0]["sCountInfo"].ToString().Trim();

                ////Indented�Y�� �N����ഫ��Json�榡
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
        //�d�߼i�����
        public object[] RECUsersQuery(object[] objParam)
        {
            //string Keyword = (string)objParam[0].ToString().Trim();
            string[] parm = objParam[0].ToString().Split('*');
            string NameC = parm[0].ToString();//�H�~�m�W/������/�i���s��
            string Gender = parm[1].ToString();//�ʧO
            string Age1 = parm[2].ToString();//�~��
            string Age2 = parm[3].ToString();
            string EduID = parm[4].ToString();//�Ǿ�
            string DutyAreas = parm[5].ToString();//�u�@�a�I
            string CurAddress = parm[6].ToString();//�{�~�a�}
            string AssignJob = parm[7].ToString();//����¾��
            string ProLicenses = parm[8].ToString();//�ҷӸ��
            string JobCompany = parm[9].ToString();//�g�����q�W��
            string SalesTeam = parm[10].ToString();//�A�Ȧa��
            string ServiceConsultants = parm[11].ToString();//�A�ȤH��
            string Status = parm[12].ToString();//�B�z���A
            string SDate = parm[13].ToString();//�ק���
            string EDate = parm[14].ToString();//
            string SCDate = parm[15].ToString();//�إߤ��
            string ECDate = parm[16].ToString();//

            string PFMemberID = parm[17].ToString();//���˼θ}
            string IsPilefoot = parm[18].ToString();//�O�_�θ}

            string ShowCount = parm[19].ToString();//��ܵ���
            string Keyword = parm[20].ToString();//����j�M
            int Andor = int.Parse(parm[21].ToString());

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procSearchREC_User '" + NameC + "','" + Gender + "','" + Age1 + "','" + Age2 + "','" + EduID + "','" + DutyAreas + "','" + CurAddress + "','" + AssignJob + "','" + ProLicenses + "','" + JobCompany + "','" + SalesTeam + "','" + ServiceConsultants + "','" + Status + "','" +
                   SDate + "','"+ EDate + "','" +SCDate + "','"+ ECDate + "','" + PFMemberID + "','" + IsPilefoot + "','"+ ShowCount + "','" + Keyword + "'," + Andor;
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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
        //�i���ˬd=>�ˬd�����Ҧr�����i�H����---------------------------------------------------------------------------
        public object[] ReturnUserCount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "JBRecruit";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string PID = parm[0].ToString();

                string sql = "SELECT COUNT(*) AS CNT from [User] where IDNumber='" + PID + "'";

                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                int cnt = int.Parse(dsTemp.Tables[0].Rows[0]["cnt"].ToString());

                //Indented�Y�� �N����ഫ��Json�榡
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

        //�H�~�R���@�~------------------------------------------------------------------------
        public object[] DeleteREC_UserAbount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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


        ////���������R��=>�R���̷s�@��(�����R���ʧ@�ð���s���O)   
        public object[] JobAssignLogsIsActive(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AutoKey = (string)parm[0];
           
            string js = string.Empty;

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
        //�D�o�̷s�@��������(�������A�B�������)---------------------------------------------------------------------------
        public object[] ReturnNewJobAssignLogs(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                int JobID = int.Parse(parm[0].ToString());
                string UserID = parm[1].ToString();
                string AssignTime = parm[2].ToString();
                string sql = "";
                if (AssignTime == "1900/01/01")//�s�W
                {
                    sql = "SELECT top 1 AssignID,Convert(nvarchar(10),AssignTime,111) as AssignTime from REC_JobAssignLogs where IsActive=1 and JobID = " + JobID + " and UserID='" + UserID + "' order by AssignTime desc";
                }
                else//�s��
                {
                    sql = "SELECT top 1 AssignID,Convert(nvarchar(10),AssignTime,111) as AssignTime from REC_JobAssignLogs where IsActive=1 and JobID = " + JobID + " and UserID='" + UserID + "' and AssignTime < '" + AssignTime + "' order by AssignTime desc";
                }

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //int cnt = int.Parse(dsTemp.Tables[0].Rows[0]["AssignID"].ToString());

                //Indented�Y�� �N����ഫ��Json�榡
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

        //�s�W����
        private void ucRec_JobAssignLogs_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucRec_JobAssignLogs.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucRec_JobAssignLogs_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            ////�o�H
            //SendAssignMail(aUserID[i], aNameC[i], aEmail[i], AssignMail);
        }

        //�N�H�~�s�[�J��������
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
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();

            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procInsertJobAssignLogsMore '" + sUserID + "','" + CustID + "'," + JobID + ",'" + RecommendCust + "'," + RecID + "," + AssignID + ",'" + AssignTime + "','" + AssignContent + "'," + bAssignMail + ",'" + AssignMail + "','" + CreateBy + "'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

                if (bAssignMail == "1")
                {
                    //�o�H
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

        //�ݳ���H�eEmail
        public void SendAssignMail(string UserID, string NameC, string Email, string AssignMail)
        {
            //�]�wEmail������T
            string MailServerName = "smtp.office365.com";       //MailServerName
            int iPort = 25;                                     //MailServer ����
            bool IsUseDefaultCredentials = true;
            bool IsSSL = true;
            string FromID = "service@jbjob.com.tw";             //�H��̱b��
            string FromPW = "shjnmmvgmvtgswxy";                 //�H��̱K�X

            string FromName = "�ǳ��H�O�귽�U�ݦ������q";
            string Subject = "�����q���H";
            string Body = GetAssignMailBody(NameC, AssignMail);
            //�H�XeMail
            //����
            Email = "rebecca@jbjob.com.tw";
            bool SendOK = SendMail(MailServerName, iPort, FromID, IsUseDefaultCredentials, IsSSL, FromID, FromPW, Email, Subject, Body, FromName);
            if (SendOK)
            {
                //�إ߸�Ʈw�s��
                //IDbConnection connection2 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                string sLoginDB2 = "JBHRIS_DISPATCHTest";
                IDbConnection connection2 = (IDbConnection)AllocateConnection(sLoginDB2);

                //��s�u���A������open�ɡA�}�ҳs��
                if (connection2.State != ConnectionState.Open)
                {
                    connection2.Open();
                }
                //�}�ltransaction
                IDbTransaction transaction2 = connection2.BeginTransaction();
                try
                {   //�g�J�o�e����
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
        //�o�eemail- �浧
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
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();

            }
            //�}�ltransaction
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

        //�]�w�A�ȤH��Email,�o�H���e
        string GetAssignMailBody(string NameC, string AssignMail)
        {
            string strToBody = "";
            DateTime dt = DateTime.Now;
            strToBody += "<script type='text/css'>a.button {-webkit-appearance: button;-moz-appearance: button;appearance: button;text-decoration: none;color: initial;}</script>" +
                           "<table border='0' width='92%' align='left'>" +
                           "<tr><td align='left'><a  href='http://www.jbjob.com.tw/'><img src='http://www.jbhr.com.tw/jqwebclient/Files/JBERP_SalesPaper/logo.png'  alt='�ǳ��H�O�귽�A�ȶ���' class='fusion-logo-1x fusion-standard-logo'></a>" +
                           "<tr><td align='left'><br/>Hello�A<b>" + NameC.Trim() + "</b>�z�n�G</td><tr>" +
                           "<tr><td align='left'><br/></td></tr>" +
                           "<tr><td align='left'><label style='color:black'>" + AssignMail.Replace("\n", "<br/>").Replace(" ", "&nbsp;") + "</label></td></tr>" +
                           "<tr><td align='left'><br/></td></tr>" +
                           "<tr><td align='left'><a href='https://www.jbhr.com.tw/jqweb2015/RWDLOGON.aspx'><label style='color:blue'><b>�n�J�s��</b></label><alt='�ǳ��H�O�귽�A�ȶ���'></a>" +
                           "<tr><td align='left'>�o�e�ɶ�:" + dt.ToString() + "</td></tr>" +

                           "</table>";
            return strToBody;
        }

        //--------------------------------�g�J�۶Ҩt��--------------------------------
        public object[] InsertUserbyREC_User(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string UserID = parm[0];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;

            string sLoginDB = "JBRecruit";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
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

            //�إ߸�Ʈw�s��
            //IDbConnection connection2 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB2 = "JBHRIS_DISPATCHTest";
            IDbConnection connection2 = (IDbConnection)AllocateConnection(sLoginDB2);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection2.State != ConnectionState.Open)
            {
                connection2.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction2 = connection2.BeginTransaction();
            try
            {   //�ק�w�g���A
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
        //���˼i�� ����--------------------------------------------------------------------------------
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
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();

            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportREC_UserRecommendResume2 '" + UserID + "','" + JobID + "','" + AutoKey + "','" + username + "'," + iType + ",'" + sDyItem + "'";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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
        //���o�n�J�̬O�_�O�D��
        public object[] ReturnIsManager(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "JBHRIS_DISPATCHTest";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string UserID = objParam[0].ToString().Trim();
                string sql = "SELECT count(*) as cnt from REC_Consultants where EmpID='" + UserID + "' and IsManager=1";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                int cnt = int.Parse(dsTemp.Tables[0].Rows[0]["cnt"].ToString());

                //Indented�Y�� �N����ഫ��Json�榡
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
        //�R���ƥ�-----------------------------------------------
        private void ucRECUser_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            //�R�� USERS �P USERSGROUP
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCHTest";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
        //�P�_�O�_���w�A�ȤH��=>�oemail
        private void ucRECUser_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //�A�ȤH��
            string oServiceConsultants = ucRECUser.GetFieldOldValue("ServiceConsultants").ToString();
            string ServiceConsultants = ucRECUser.GetFieldCurrentValue("ServiceConsultants").ToString();
            if (oServiceConsultants != ServiceConsultants && ServiceConsultants!="")
            {

                string ConsultantName = "";
                string ConsultantEmail = "";

                //�إ߸�Ʈw�s��
                //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                string sLoginDB = "JBHRIS_DISPATCHTest";
                IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

                //��s�u���A������open�ɡA�}�ҳs��
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                //�}�ltransaction
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

                //�]�wEmail������T
                string MailServerName = "smtp.office365.com";       //MailServerName
                int iPort = 25;                                     //MailServer ����
                bool IsUseDefaultCredentials = true;
                bool IsSSL = true;
                string FromID = "service@jbjob.com.tw";             //�H��̱b��
                string FromPW = "shjnmmvgmvtgswxy";                 //�H��̱K�X
                string To = ConsultantEmail; //         //���H��

                string UserID = ucRECUser.GetFieldCurrentValue("UserID").ToString();
                string MemberID = ucRECUser.GetFieldCurrentValue("MemberID").ToString();
                string NameC = ucRECUser.GetFieldCurrentValue("NameC").ToString();
                string MobileNo = ucRECUser.GetFieldCurrentValue("MobileNo").ToString();
                string Email = ucRECUser.GetFieldCurrentValue("Email").ToString();
                string Consultants = ucRECUser.GetFieldCurrentValue("ServiceConsultants").ToString();
                string FromName = "�ǳ��H�O�귽�U�ݦ������q";
                string Subject = NameC + "�뻼�i���q���H";
                string Body = GetMailBody(NameC, MobileNo, Email, ConsultantName);
                //�H�XeMail
                bool SendOK = SendMail(MailServerName, iPort, FromID, IsUseDefaultCredentials, IsSSL, FromID, FromPW, To, Subject, Body, FromName);
                if (SendOK)
                {
                    //�إ߸�Ʈw�s��
                    //IDbConnection connection2 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                    string sLoginDB2 = "JBHRIS_DISPATCHTest";
                    IDbConnection connection2 = (IDbConnection)AllocateConnection(sLoginDB2);

                    //��s�u���A������open�ɡA�}�ҳs��
                    if (connection2.State != ConnectionState.Open)
                    {
                        connection2.Open();
                    }
                    //�}�ltransaction
                    IDbTransaction transaction2 = connection2.BeginTransaction();
                    try
                    {   //�g�J�o�e����
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
    
       //�H�e�A�ȤH��Email
        public bool SendMail(string sMailServerName, int iPort, string sFrom, bool bIsUseDefaultCredentials, bool bSSL, string sFromID, string sFromPW, string sTo, string sSubject, string sBody,string sFromName)
        {
           try
            {
                    //.Net FrameWork �䴩TLS1.1 1.2�q�T��w
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
       //�]�w�A�ȤH��Email,�o�H���e
        string GetMailBody(string NameC, string MobileNo, string Email, string ConsultantName)
        {
            string strToBody = "";
            DateTime dt = DateTime.Now;
            strToBody += "<script type='text/css'>a.button {-webkit-appearance: button;-moz-appearance: button;appearance: button;text-decoration: none;color: initial;}</script>" +
                           "<table border='0' width='92%' align='left'>" +
                           "<tr><td align='left'><a  href='http://www.jbjob.com.tw/'><img src='http://www.jbhr.com.tw/jqwebclient/Files/JBERP_SalesPaper/logo.png'  alt='�ǳ��H�O�귽�A�ȶ���' class='fusion-logo-1x fusion-standard-logo'></a>" +
                           "<tr><td align='left'><br/>Hello�A<b>" + ConsultantName.Trim() + "</b>�z�n�G</td><tr>" +
                           "<tr><td align='left'><br/></td></tr>" +
                           "<tr><td align='left'>�H�U�O�D¾�̪���ơA�ХߧY�D���pô����I</td></tr>" +
                           "<tr><td align='left'>�H�~�m�W�G<label style='color:red'>" + NameC.Trim() + "</label></td></tr>" +
                           "<tr><td align='left'>��ʹq�ܡG<label style='color:red'>" + MobileNo.Trim() + "</label></td></tr>" +
                           "<tr><td align='left'>�l��H�c�G<label style='color:red'>" + Email.Trim() + "</label></td></tr>" +
                           "<tr><td align='left'><a href='https://www.jbhr.com.tw/jqweb2015/RWDLOGON.aspx'><label style='color:blue'><b>�n�J�s��</b></label><alt='�ǳ��H�O�귽�A�ȶ���'></a>" +
                           "<tr><td align='left'>�o�e�ɶ�:" + dt.ToString() + "</td></tr>" +

                           "</table>";
            return strToBody;
        }



        
        



    }
}
