//using sRequisition;
using sBizTravel;
using Srvtools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace sBizTravel
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
        public string GetFixed(){
            DateTime today1 = DateTime.Today;
            return "TRV" + ((today1.Year).ToString()).Trim();
        }
        //�۰ʰ_��
        public object[] MakeRequisition(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string str = objParam[0].ToString();
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
            EEPRemoteModule ep = new EEPRemoteModule();
            ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml",//�y�{���W�r�A�]�t������|�C//"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml"
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml",
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    "1030051",//�ӽЪ̪�RoleID(����s��)
                    "sRequisition.Requisition",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                },
                new object[]{
                    "RequisitionNO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "RequisitionNO ='"+ str +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                }
            });
            ret[1] = true;
            
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;
            
        }

        public object[] SelectRequisition(object[] objParam)
        {
            string js = string.Empty;
            //string js2 = string.Empty;
            string str = objParam[0].ToString();
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try{
                string sql = "SELECT SUM([RequisitAmt]) AS SumR,COUNT([RequisitAmt]) AS CountR FROM [JBADMIN].[dbo].[Requisition] WHERE [SourceBillNO] ='" + str + "' and (Flowflag IS NOT NULL and Flowflag !='X')";// 
            DataSet ds = this.ExecuteSql(sql, connection, transaction);
            js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            //js1 = ds.Tables[0].Rows[0]["SumR"].ToString();
            //js2 = ds.Tables[0].Rows[0]["CountR"].ToString();
            }
            catch{
                transaction.Rollback();
            }
            finally{
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js};
        }

        public object[] UpdateBizTravel(object[] objParam) {
            object[] ret = new object[] { 0, 0 };
            string str = objParam[0].ToString();
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
                string sql = "SELECT sum(AirfareVisafee) as TotalAFVF FROM JBADMIN.DBO.BizTravelDetails_Accom WHERE TvlNo='" + str + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                string js = ds.Tables[0].Rows[0]["TotalAFVF"].ToString();
                string sql1 = "UPDATE BizTravelMaster SET TotalAFVF=" + js + " WHERE TvlNo='" + str + "'";
                this.ExecuteSql(sql1, connection, transaction);
                transaction.Commit();
                ret[1] = true;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;  
        }

        public object[] IsSignWithNotes(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string TvlNo = dr["TvlNo"].ToString();
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
                string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='TvlNo=''" + TvlNo + "'''";
                DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                if (cnt == "0")
                    ret[1] = false;//�~��y�{
                else
                    ret[1] = true;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }

        public object[] GetSignCount(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string TvlNo = parm[0];
                string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='TvlNo=''" + TvlNo + "'''";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["CNT"].ToString();
                if (cnt == "0")
                    ret[1] = 0;
                else
                    ret[1] = 1;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;
        }

        public object[] GetSignNotesData(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string TvlNo = parm[0];
                string sql = "SELECT S_STEP_ID,USERNAME,REMARK,UPDATEDATE FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='TvlNo=''" + TvlNo + "''' ORDER BY UPDATEDATE ASC";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;
        }
    }
}
