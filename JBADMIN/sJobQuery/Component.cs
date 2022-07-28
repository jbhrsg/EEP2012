using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections;
using System.Data.SqlClient;

namespace sJobQuery
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

        public object[] AddMenu(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string type = parm[0];           
            string JobID = parm[1];         
            string AssignID = parm[2];
            int ileng = int.Parse(parm[3]);
            string sUserID ="";
            for (var i = 4; i < 4+ileng; i++)
            {
                sUserID = sUserID + parm[i]+",";
            }
            string AssignTime = DateTime.Now.ToString("yyyy/MM/dd");
            if (type == "2")
            {
                AssignTime = DateTime.Parse(parm[5]).ToString("yyyy/MM/dd");
            }           
            DateTime CreateDate = DateTime.Now;
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

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
                string SQL = "exec procInsertJobAssignLogs '" + sUserID + "'," + JobID + "," + AssignID + ",'" + AssignTime + "','" + LoginUser + "'";
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
            }
            return new object[] { 0, js };
        }
        public object[] writeDBurl(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');            
            string UserID = (string)parm[0];
            string JobID = (string)parm[1];
            string AssignID = (string)parm[2];
            string FileUrl = (string)parm[3];
            string js = string.Empty;
            //string LoginUser = GetClientInfo(ClientInfoType.LoginUser).ToString();

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

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
            {//�R����Y�٦���ƶ��ק�HUT_JobAssignNew�����,�S���h�R��HUT_JobAssignNew���
                string SQL = "exec procInsertJobAssignLogsFile '" + UserID + "'," + JobID + "," + AssignID + ",'" + LoginUser + "','"+ FileUrl + "'";
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
            }
            return new object[] { 0, js };
        }
        public object[] userDEL(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AssignNO = (string)parm[0];
            string UserID = (string)parm[1];
            string JobID = (string)parm[2];
            string js = string.Empty;
            //string LoginUser = GetClientInfo(ClientInfoType.LoginUser).ToString();

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

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
            {//�R����Y�٦���ƶ��ק�HUT_JobAssignNew�����,�S���h�R��HUT_JobAssignNew���
                string SQL = "exec procUpdateJobAssignNew '" + AssignNO + "','" + UserID + "'," + JobID + ",'" + LoginUser + "'";
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
            }
            return new object[] { 0, js };
        }

        //�����˯�
        public object[] SearchJobFullIndex(object[] objParam)
        {
            string Keyword = (string)objParam[0];
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
                string SQL = "exec procSearchJobFullIndex " + Keyword;
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
    }
}
