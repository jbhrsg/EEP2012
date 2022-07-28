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
            ////------------------------------------��r------------------------------------------------
            ////�u�@�a�I
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
            string ProLicenses = parm[7].ToString();//�ҷӸ��
            string JobCompany = parm[8].ToString();//�g�����q�W��
            string Keyword = parm[9].ToString();//����j�M
            int Andor = int.Parse(parm[10].ToString());

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
                string SQL = "exec procSearchREC_User '" + NameC + "','" + Gender + "','" + Age1 + "','" + Age2 + "','" + EduID + "','" + DutyAreas + "','" + CurAddress + "','" + ProLicenses + "','" + JobCompany + "','" + Keyword + "'," + Andor;
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
                string SQL = "exec procReportREC_UserRecommendResume '" + UserID + "','" + JobID + "','" + AutoKey + "','" + username + "'," + iType + ",'" + sDyItem + "'";
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
