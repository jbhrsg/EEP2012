using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace sCustomersJobs
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

        //===============================�Ȥ�==============================================================
        private void ucHUT_Customer_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_Customer.SetFieldValue("CreateDate", DateTime.Now);
            ucHUT_Customer.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucHUT_Customer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_Customer.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        public object[] CheckDelCustomer(object[] objParam)
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
                string CustID = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM HUT_CONTRACT WHERE (CUSTID) = '" + CustID + "'";
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

        //===============================�Ȥ�q�X����==============================================================
        private void ucHUT_CustomerContactRecord_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_CustomerContactRecord.SetFieldValue("UpdateDate", DateTime.Now);
        }

        private void ucHUT_CustomerContactRecord_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_CustomerContactRecord.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_CustomerContactRecord.SetFieldValue("UpdateBy", username);
        }
        //===============================�Ȥ��p���H==============================================================
        private void ucHUT_CustomerContactPerson_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_CustomerContactPerson.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_CustomerContactPerson.SetFieldValue("UpdateBy", username);
        }
        private void ucHUT_CustomerContactPerson_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_CustomerContactPerson.SetFieldValue("UpdateDate", DateTime.Now);
        }
        //===============================�Ȥ��ɮ׺޲z==============================================================
        private void ucHUT_CustomerFile_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_CustomerFile.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_CustomerFile.SetFieldValue("UpdateBy", username);
        }
        //===============================�Ȥ��ɮקR��==============================================================
        public object[] DelCustomerFile(object[] objParam)
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
                string CustFile = parm[1];//�s����

                string sql = "Delete [Hunter].dbo.HUT_CustomerFile where AutoKey=" + AutoKey;
                this.ExecuteCommand(sql, connection, transaction);

                string sourcePath = @"\\192.168.10.70\c$\inetpub\wwwroot\JQWebClient_JBHR_SP7\Files\Hunter\Customer";
                string destFile = System.IO.Path.Combine(sourcePath, CustFile);

                if (System.IO.File.Exists(destFile))
                {
                    System.IO.File.Delete(destFile);
                }
                else
                {
                    cnt = "1";
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
        //--------------------------------------------------------------------------------------------------

        //===============================¾��==============================================================
        private void ucHUT_Job_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            string CustID = ucHUT_Job.GetFieldCurrentValue("sCustID").ToString();
            ucHUT_Job.SetFieldValue("CustID", CustID);

            ucHUT_Job.SetFieldValue("CreateDate", DateTime.Now);
            ucHUT_Job.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucHUT_Job_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_Job.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //===============================NAR�ݨD���R����==============================================================
        private void ucHUT_JobRequirementRecord_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            string CustID = ucHUT_Job.GetFieldCurrentValue("CustID").ToString();
            ucHUT_JobRequirementRecord.SetFieldValue("CustID", CustID);
            ucHUT_JobRequirementRecord.SetFieldValue("UpdateDate", DateTime.Now);
        }

        private void ucHUT_JobRequirementRecord_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            string CustID = ucHUT_Job.GetFieldCurrentValue("CustID").ToString();
            ucHUT_JobRequirementRecord.SetFieldValue("CustID", CustID);

            ucHUT_JobRequirementRecord.SetFieldValue("UpdateDate", DateTime.Now);
        }
        //===============================¾���pô����==============================================================
        private void ucHUT_JobContactRecord_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {         
            ucHUT_JobContactRecord.SetFieldValue("UpdateDate", DateTime.Now);
        }
        private void ucHUT_JobContactRecord_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {          
            ucHUT_JobContactRecord.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_JobContactRecord.SetFieldValue("UpdateBy", username);

        }
        //===============================¾���p���H==============================================================
        private void ucHUT_JobContactPerson_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            string CustID = ucHUT_Job.GetFieldCurrentValue("CustID").ToString();
            ucHUT_JobContactPerson.SetFieldValue("CustID", CustID);
            ucHUT_JobContactPerson.SetFieldValue("UpdateDate", DateTime.Now);
        }

        private void ucHUT_JobContactPerson_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            string CustID = ucHUT_Job.GetFieldCurrentValue("CustID").ToString();
            ucHUT_JobContactPerson.SetFieldValue("CustID", CustID);

            ucHUT_JobContactPerson.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_JobContactPerson.SetFieldValue("UpdateBy", username);

        }
        //===============================¾�ʶ}���ʳ]�w==============================================================
        private void ucHUT_JobDateLog_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {           
            ucHUT_JobDateLog.SetFieldValue("UpdateDate", DateTime.Now);
        }

        private void ucHUT_JobDateLog_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {         
            ucHUT_JobDateLog.SetFieldValue("UpdateDate", DateTime.Now);
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            ucHUT_JobDateLog.SetFieldValue("UpdateBy", username);
        }

        //===============================¾���ɮ�==============================================================
        private void ucHUT_JobFile_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            string CustID = ucHUT_Job.GetFieldCurrentValue("CustID").ToString();
            ucHUT_JobFile.SetFieldValue("CustID", CustID);
            ucHUT_JobFile.SetFieldValue("UpdateDate", DateTime.Now);
        }
        
        //-----------�R��¾���ɮ�--------------------------
        public object[] DelJobFile(object[] objParam)
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
                string CustFile = parm[1];//�s����

                string sql = "Delete [Hunter].dbo.HUT_jobFile where AutoKey=" + AutoKey;
                this.ExecuteCommand(sql, connection, transaction);
                string sourcePath = @"\\192.168.10.70\c$\inetpub\wwwroot\JQWebClient_JBHR_SP7\Files\Hunter\Job";
                string destFile = System.IO.Path.Combine(sourcePath, CustFile);

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

        //-----------NAR����=>������--------------------------
        public object[] RequirementRecordIsActive(object[] objParam)
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
                string AutoKey = objParam[0].ToString();

                string sql = "Update [Hunter].dbo.HUT_JobRequirementRecord set IsActive=0 where AutoKey=" + AutoKey;
                this.ExecuteCommand(sql, connection, transaction);

                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //NAR ����--------------------------------------------------------------------------------
        public object[] procReportJobNAR(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            int JobID = int.Parse(parm[0].ToString());
            int iType = int.Parse(parm[1].ToString());
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
                string SQL = "exec procReportHunterJobNAR " + JobID + "," + iType;
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }


        //¾�ʳq��--------------------------------------------------------------------------------
        public object[] procReportJobRequire(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CustID = parm[0].ToString();
            string JobName = parm[1].ToString();
            string HunterID = parm[2].ToString();
            string SalesTeamID = parm[3].ToString();
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
                string SQL = "exec procReportJobRequire '" + CustID + "','" + JobName + "','" + HunterID + "','" + SalesTeamID+"'";
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }




        //���o�q��s���T�w�X
        public string GetOrderFixed()
        {
            DateTime datetime = DateTime.Today;
            return "HTPO" + ((datetime.Year) - 1911).ToString().Trim();
        }

        
        //�N¾�ʻy�媬�A��s Table JobLang
        private void IPDJobEng(string UpdateType,int JobID)
        {
            string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();
            if (!JobID.Equals(null))
            {
                string sql = "EXEC dbo.procUpdateJobLang  " + JobID.ToString() + ",'"+ UpdateType+ "' ," + UserID;
                this.ExecuteCommand(sql, ucHUT_CustomerContactRecord.conn, ucHUT_CustomerContactRecord.trans);
            }
        }
        //�N¾�ʼi���q���H��s Table HUT_JobNotify
        private void IPDJobNotify(string UpdateType, int JobID,string CreateBy) 
        {
            string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();
            if (!JobID.Equals(null))
            {
                string sql = "EXEC procUpdateJobNotify  " + JobID.ToString() + ",'" + UpdateType + "' ," + CreateBy;
                this.ExecuteCommand(sql, ucHUT_CustomerContactRecord.conn, ucHUT_CustomerContactRecord.trans);
            }
        }

        

       

        

       
       

        

        

      

        

       

      

      

        

        
     
     }
}
