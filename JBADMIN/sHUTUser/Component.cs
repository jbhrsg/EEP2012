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
        //�i���s��=> ex: U10900001--����~+��+��+4�X�y����
        public string HunterUserIDFixed()
        {           
            string VoucherNoTitle = "U";           
            DateTime dDate = DateTime.Now;
            return VoucherNoTitle + (dDate.Year - 1911).ToString().Trim();
        }

        //�i�����
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
        //�u�@�g��
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
        //�y���O
        private void ucHUT_UserLang_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_UserLang.SetFieldValue("CreateDate", DateTime.Now);
            ucHUT_UserLang.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucHUT_UserLang_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_UserLang.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //--------------------���ͬ������@-----------------------------------------
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
        //�u�@�g��-���~���O�R���ˬd
        public object[] CheckMasterDelete(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                int CategoryID = int.Parse(objParam[0].ToString());
                string sql = "SELECT COUNT(*) AS CNT from [Hunter].dbo.HUT_UserCareer where CategoryID = " + CategoryID;
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //===============================�i���ɮ�==============================================================
        private void ucHUT_UserFile_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_UserFile.SetFieldValue("UpdateDate", DateTime.Now);
        }

        //-----------�R��¾���ɮ�--------------------------
        public object[] DelUserFile(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string cnt = "0";
                string[] parm = objParam[0].ToString().Split(',');
                string AutoKey = parm[0];
                string UserFile = parm[1];//�s����

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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //================================================================================================================================//
        //�d�߼i�����
        public object[] UsersQuery(object[] objParam)
        {
            //string Keyword = (string)objParam[0].ToString().Trim();
            string[] parm = objParam[0].ToString().Split('*');
            string Age1 = parm[0].ToString();//�~��
            string Age2 = parm[1].ToString();
            string EduID = parm[2].ToString();//�Ǿ�
            string LangID = parm[3].ToString();//�y��
            string LangLevel = parm[4].ToString();//�{��
            string GoodTools = parm[5].ToString();//�q���ޯ�
            string LicenseQA = parm[6].ToString();//�ҷӸ��
            string ExpDutyArea = parm[7].ToString();//����u�@�a
            string ExpCategory = parm[8].ToString();//���沣�~
            string ExpJobType = parm[9].ToString();//����¾��
            string ComName = parm[10].ToString();//�g�����q�W��
            string DutyTitle = parm[11].ToString();//�g��¾��
            string CategoryID = parm[12].ToString();//���~���O
            string DutyContent = parm[13].ToString();//�g���u�@���e
            string NameC = parm[14].ToString();//�H�~�m�W
            string Keyword = parm[15].ToString();//����j�M
            int Andor = int.Parse(parm[16].ToString());
            int blacklist = int.Parse(parm[17].ToString());//�¦W��
            string UpdateDay = parm[18].ToString();//��s���=>1�̷s,3�T�餺,7�@�P��,14�G�P��,30�@�Ӥ뤺,0����

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
                string SQL = "exec procSearchUsers N'" + NameC + "','" + Age1 + "','" + Age2 + "','" + EduID + "','" + LangID + "','" + LangLevel + "','" + GoodTools + "','" + LicenseQA + "','" +
                        ExpDutyArea + "','" + ExpCategory + "','" + ExpJobType + "','" + ComName + "','" + DutyTitle + "','" + CategoryID + "','" + DutyContent + "','" + Keyword + "'," + Andor + "," + blacklist + ",'" + UpdateDay +"'";
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

        //���˼i�� ����--------------------------------------------------------------------------------
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
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
                string SQL = "exec procReportRecommendResume '" + UserID + "','" + JobID + "','" + AutoKey + "','" + username + "'," + iType;
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
        //�дک��Ӫ� ����--------------------------------------------------------------------------------
        public object[] procReportPleasePay(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string UserID = parm[0].ToString();
            string JobID = parm[1].ToString();
            string AutoKey = parm[2].ToString();

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
                string SQL = "exec procReportPleasePay '" + UserID + "','" + JobID + "','" + AutoKey + "','" + username + "'";
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
        //���ק@�~�i�� ����--------------------------------------------------------------------------------
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
            //�إ߸�Ʈw�s��
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
                string SQL = "exec procReportJobSchedule '" + Type + "','" + CustID + "','" + JobName + "','" + SalesTeamID + "','" + JobStatus + "','" + HunterID + "','" + HunterIDAssist + "','" +
                    SDate + "','" + EDate + "','" + iDay1 + "','" + iDay2 + "','" + SADate + "','" + EADate + "','" + AssignID + "','" + AssignHunterID + "'";

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

        //�P�f�`�� ����--------------------------------------------------------------------------------
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
            //�إ߸�Ʈw�s��
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
                string SQL = "exec procReportSalesInvoice '" + SDate + "','" + EDate + "','" + CustID + "','" + SalesTeamID + "','" + HunterID + "','" + AssignHunterID + "'";

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

        //���׹w���禬 ����--------------------------------------------------------------------------------
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
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
                string SQL = "exec procReportRevenue '" + Type + "','" + CustID + "','" + JobName + "','" + HunterID + "','" + SalesTeamID + "','"
                    + SADate + "','" + EADate + "','" + DraftS + "','" + DraftE + "','" + AssignHunterID + "'";

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
        //���׹w���禬 Grid--------------------------------------------------------------------------------
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
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
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
                string SQL = "exec procDisplayRevenue '" + Type + "','" + CustID + "','" + JobName + "','" + HunterID + "','" + SalesTeamID + "','"
                    + SADate + "','" + EADate + "','" + DraftS + "','" + DraftE + "','" + AssignHunterID + "'," + Class;

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
        //���׹w���禬 �ץX
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
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "Hunter";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();

            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = "exec procDisplayRevenue '" + Type + "','" + CustID + "','" + JobName + "','" + HunterID + "','" + SalesTeamID + "','"
                    + SADate + "','" + EADate + "','" + DraftS + "','" + DraftE + "','" + AssignHunterID + "'," + Class;

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();


                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));

                theResult.Add("IsOK", true);
                theResult.Add("Msg", "���~�T��");
                theResult.Add("FileName", "�o�O�@���ɮ�.xls");

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
        //���˧@�~�ˬd=>�D���˵���---------------------------------------------------------------------------
        public object[] ReturnJobAssignLogsCount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "Hunter";
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

                string sql = "SELECT COUNT(*) AS CNT from HUT_JobAssignLogs where JobID = " + JobID + " and UserID='" + UserID + "' and AssignID=1";
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
        //���˧@�~�ˬd=>�D<�������O�_������������---------------------------------------------------------------------------
        public object[] ReturnJobAssignLogsAdmitCount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "Hunter";
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
                string dDate = parm[2].ToString();//������ //1����,2����,3����,4������,5����,6������,7��¾

                string sql = "SELECT COUNT(*) AS CNT from HUT_JobAssignLogs where JobID = " + JobID + " and UserID='" + UserID + "' and AssignID=3 and AssignTime<='" + dDate+"'";
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
        //��¾����Y�j�������+�O�ҤѼ�-1�A�n�ˬd����Ŀ�h�O���---------------------------------------------------------------------------
        public object[] ReturnJobAssignLogsAssureDayCount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "Hunter";
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
                DateTime LeaveDate =  DateTime.Parse(parm[2].ToString());//��¾��� //1����,2����,3����,4������,5����,6������,7��¾

                string sql = "SELECT Dateadd(day,AssureDay,AssignTime)-1 as AssureDate from HUT_JobAssignLogs where JobID = " + JobID + " and UserID='" + UserID + "' and AssignID=5 and AssignTime<='" + LeaveDate.ToShortDateString() + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                DateTime AssureDate = DateTime.Parse(dsTemp.Tables[0].Rows[0]["AssureDate"].ToString());
                int cnt = 0;
                if (LeaveDate > AssureDate)
                {
                    cnt = 1;
                }

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
        //�i���ˬd=>�ˬd�q��1 or �q��2���i�H����---------------------------------------------------------------------------
        public object[] ReturnUserCount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "Hunter";
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
        //���o�n�J�̬O�_�ݩ󤤰������U��
        public object[] ReturnHunterCount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "Hunter";
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
                string UserName = objParam[0].ToString().Trim();
                string sql = "SELECT count(*) as cnt from HUT_Hunter where HunterName like '%" + UserName + "%'";
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
        //���o�n�J��ܪ���T(�s�W¾��+����¾��+�s�W�i��)
        public object[] ReturnCreateInfo(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "Hunter";
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
                string SQL = "exec procDisplayCreateInfo ";

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

        private void ucHUT_JobAssignLogs_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //�P�f���J�ӽ�(�~�ȤH������g)
            string Autokey = ucHUT_JobAssignLogs.GetFieldCurrentValue("Autokey").ToString();//�y����     
            string SalesID = ucHUT_JobAssignLogs.GetFieldCurrentValue("SalesID").ToString();//�~�ȭ��u�N��    
            string InvoiceYM = ucHUT_JobAssignLogs.GetFieldCurrentValue("InvoiceYM").ToString();//�o���~��  

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

         
        }
        //�g�P�f���J���
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

                string sql = "exec procImportSalesFromHunter '" + SalesID + "','" + InvoiceYM + "'," + Autokey + ",'" + username + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string SalesNO = dsTemp.Tables[0].Rows[0]["SalesNO"].ToString();
                string RoleID = dsTemp.Tables[0].Rows[0]["RoleID"].ToString();
                transaction.Commit();

                //�۰ʰ_��P�f���J�ӽ�
                if (SalesID != "" && SalesNO != "")//��SalesID�A��SalesNO
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
            /*������user�n�J�Auserid�ШϥΥ��p�g*/
            string username = SrvGL.GetUserName(SalesID.ToLower());
            object[] cInfo = new object[] { this.ClientInfo[0], -1, 1, "" };
            SrvGL.LogUser(SalesID, username, "", 1);   //�����n�J 
            ((object[])cInfo[0])[1] = SalesID;

                EEPRemoteModule ep = new EEPRemoteModule();
                object[] ret1 = ep.CallFLMethod(cInfo, "Submit", new object[]{
                    null,
                        new object[]{
                        "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\ERPSalesApply.xoml",
                        //"D:\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\ERPSalesApply.xoml",
                        string.Empty,////�ťէY�i�A�t�Ψϥ�
                        0,//�O�_�����n�ӽ�
                        0,//�O�_�����ӽ�
                        "",//����N������
                        RoleID,//"1060053",//�ӽЪ̪�RoleID(����s��)
                        "sERPSalesApply.SalesMaster",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                        0,//�t�Ψϥ�
                        "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                        Attach //����
                        },
                    new object[]{
                    "SalesNO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "SalesNO='"+ SalesNO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                    }
                });
                SrvGL.LogUser(SalesID, username, "", -1);  //�����n�X
            
        }
        //���˧@�~�����d�� ����--------------------------------------------------------------------------------
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
                string SQL = "exec procDisplayJobAssignLogsRemind 1,'" + CustID + "','" + AssignID + "','" + SalesTeamID + "','" + AssignHunterID + "','" + 
                     SalesID + "','" + SDate + "','" + EDate +"'";

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







    }
}
