using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sForm_SecurityDepositWarrant
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

        public string DepositWarrantNOGetFixed()
        {
            string Year = DateTime.Now.Year.ToString();
            return "DEW" + Year;
        }

        //�дڳ�_��
        public object[] MakeRequisition(object[] objParam)
        {
            string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string role_id = "";
            role_id = GetRoleID(user_id);

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
                if (role_id == "") { throw new Exception("�S������N��"); }
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
                    role_id,//�ӽЪ̪�RoleID(����s��)
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
            catch (Exception ex)
            {
                transaction.Rollback();
                throw (ex);
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }

        //���дڳ�渹
        public object[] SelectRequisition(object[] objParam)
        {
            string js = string.Empty;
            //string js2 = string.Empty;
            //string str = objParam[0].ToString();
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT top 1 RequisitionNO  FROM [Requisition] WHERE  (Flowflag IS NOT NULL and Flowflag !='X') order by RequisitionNO desc";// [SourceBillNO] ='" + str + "'
                DataSet ds = this.ExecuteSql(sql, connection, transaction);//SUM([RequisitAmt]) AS SumR,COUNT([RequisitAmt]) AS CountR
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
            return new object[] { 0, js };
        }

        //�DroleID
        private string GetRoleID(string UserID)
        {
            string RoleID = "";

            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select top 1 ug.GROUPID from EIPHRSYS.dbo.USERGROUPS ug left join EIPHRSYS.dbo.GROUPS g on g.GROUPID=ug.GROUPID where USERID = '" + UserID + "' and ISROLE='Y'";// 
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RoleID = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }

            return RoleID;
        }

        private void ucSecurityDepositWarrant_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            //�дڳ�O�Ӧۭ��ӫO�Ҫ��R�P��
            string DepositWarrantNO = ucSecurityDepositWarrant.GetFieldCurrentValue("DepositWarrantNO").ToString();
            string RequisitionNO = ucSecurityDepositWarrant.GetFieldCurrentValue("RequisitionNO").ToString();
            string sql = "update Requisition set SourceBillNO='" + DepositWarrantNO + "' where RequisitionNO='" + RequisitionNO + "'";
            this.ExecuteCommand(sql, ucSecurityDepositWarrant.conn, ucSecurityDepositWarrant.trans);
        }
    }
}
