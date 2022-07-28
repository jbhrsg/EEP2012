using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace _HRM_COMPANY_JOBFront
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

        private void ucHRM_COMPANY_JOBFront_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHRM_COMPANY_JOBFront.SetFieldValue("CreateDate", DateTime.Now);
            ucHRM_COMPANY_JOBFront.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucHRM_COMPANY_JOBFront_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHRM_COMPANY_JOBFront.SetFieldValue("LastUpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHRM_COMPANY_JOBFront.SetFieldValue("LastUpdateBy", username);

        }
        public object[] UpdateRecReference(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string COMPANY_JOB_ID = parm[0];

            string js = string.Empty;

            string sLoginDB = "JBHRIS_DISPATCH";
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
                string SQL = "exec procSyncMutiselectTextFieldHRM_COMPANY_JOBFront " + COMPANY_JOB_ID ;
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
        //================================================================================================================================//
        //�d�ߤ��i���
        public object[] HRM_COMPANY_JOBFrontQuery(object[] objParam)
        {
            //string Keyword = (string)objParam[0].ToString().Trim();
            string[] parm = objParam[0].ToString().Split('*');
            string FrontName = parm[0].ToString();//¾�ʦW��
            string SalesTeam = parm[1].ToString();//�A�Ȧa��
            string ServiceConsultants = parm[2].ToString();//�A�ȤH��
            string IsActiveDate = parm[3].ToString();//�B�z���A
            string JobID = parm[4].ToString();//¾�ʥN��      
            string Keyword = parm[5].ToString();//����j�M
            int Andor = int.Parse(parm[6].ToString());
            int iOrderby = int.Parse(parm[7].ToString());

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCH";
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
                string SQL = "exec procSearchHRM_COMPANY_JOBFront '" + FrontName + "','" + SalesTeam + "','" + ServiceConsultants + "','" + IsActiveDate + "','" + JobID + "','" + Keyword + "'," + Andor + "," + iOrderby;
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
        //¾�ʿ����έp
        public object[] DutyAreaClassCountQuery(object[] objParam)
        {
           

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCH";
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
                string SQL = "exec procSearchHRM_COMPANY_JOBDutyAreaClassCount ";
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
        private void ucHRM_COMPANY_JOBFrontFeatured_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHRM_COMPANY_JOBFrontFeatured.SetFieldValue("CreateDate", DateTime.Now);

        }
        //�ˬd���¾�ʬ��X��---------------------------------------------------------------------------
        public object[] ReturnFeaturedCount(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "JBHRIS_DISPATCH";
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

                string sql = "SELECT COUNT(*) AS CNT from HRM_COMPANY_JOBFrontFeatured ";
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
       



    }
}
