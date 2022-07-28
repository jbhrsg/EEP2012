using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sForm_SecurityDeposit
{
    public partial class s : DataModule
    {
        public s()
        {
            InitializeComponent();
        }

        public s(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public string DepositNOGetFixed()
        {
            string Year = DateTime.Now.Year.ToString();
            return "DE" + Year;
        }

        //�дڳ�_��
        public object[] MakeRequisition(object[] objParam)
        {
            string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string role_id="";
            role_id=GetRoleID(user_id);
            
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
                if (role_id == "") { throw new Exception("�S������N��");}
                if (str == "�۰ʽs��") {str=SelectRequisition1(); }
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
            catch(Exception ex)
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
                //js1 = ds.Tables[0].Rows[0]["SumR"].ToString();
                //js2 = ds.Tables[0].Rows[0]["CountR"].ToString();
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

        //���дڳ�渹
        private string SelectRequisition1()
        {
            string js = string.Empty;
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT top 1 RequisitionNO  FROM [Requisition] WHERE  (Flowflag IS NULL) order by RequisitionNO desc";// [SourceBillNO] ='" + str + "'
                DataSet ds = this.ExecuteSql(sql, connection, transaction);//SUM([RequisitAmt]) AS SumR,COUNT([RequisitAmt]) AS CountR
                js=ds.Tables[0].Rows[0][0].ToString();
                //js1 = ds.Tables[0].Rows[0]["SumR"].ToString();
                //js2 = ds.Tables[0].Rows[0]["CountR"].ToString();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return js;
        }

        //�DroleID
        private string GetRoleID(string UserID) {
            string RoleID = "";

            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select top 1 ug.GROUPID from EIPHRSYS.dbo.USERGROUPS ug left join EIPHRSYS.dbo.GROUPS g on g.GROUPID=ug.GROUPID where USERID = '"+UserID+"' and ISROLE='Y'";// 
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                if(ds.Tables[0].Rows.Count>0){
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

        private void ucSecurityDeposit_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            //�дڳ�O�Ӧۭ��ӫO�Ҫ���
            string DepositNO = ucSecurityDeposit.GetFieldCurrentValue("DepositNO").ToString();
            string RequisitionNO = ucSecurityDeposit.GetFieldCurrentValue("RequisitionNO").ToString();
            string sql = "update Requisition set SourceBillNO='" + DepositNO + "' where RequisitionNO='"+RequisitionNO+"'";
            this.ExecuteCommand(sql, ucSecurityDeposit.conn, ucSecurityDeposit.trans);
        }

        //�۰ʰ_��
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            //string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string DepositWarrantNO = "";

            //1.��RoleID
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //��D�ި���ID
                string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + userid + "' and ORG_DESC !='�֩e�|'";
                //�쳡��������ID
                string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + userid + "'";
                DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)//�O�D��
                {
                    RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                    ds.Dispose();
                }
                else
                {//���O�D��
                    ds = this.ExecuteSql(sql1, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//�O�����U��
                    {
                        RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                    }
                    ds.Dispose();
                }
                transaction.Commit(); // �T�{���
            }
            catch
            {
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("EIPHRSYS", connection);
            }

            //2.��DepositWarrantNO
            if (RoleID != "")
            {
                connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                transaction = connection.BeginTransaction();
                try
                {
                    string sql = "SELECT Top 1 DepositWarrantNO FROM [JBADMIN].[dbo].[SecurityDepositWarrant] ORDER BY DepositWarrantNO DESC ";// 
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    DepositWarrantNO = ds.Tables[0].Rows[0]["DepositWarrantNO"].ToString();
                    transaction.Commit(); // �T�{���
                }
                catch
                {
                    ret[1] = false;
                    return ret;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }

            //3.�_��
            if (RoleID != "" && DepositWarrantNO != "")//��RoleID�A��DepositWarrantNO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_SecurityDepositWarrant.xoml",
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_SecurityDepositWarrant.xoml",
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    RoleID,//�ӽЪ̪�RoleID(����s��)
                    "sForm_SecurityDepositWarrant.SecurityDepositWarrant",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                },
                new object[]{
                    "DepositWarrantNO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "DepositWarrantNO='"+ DepositWarrantNO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                }
                    });
                    //UpdateParentContract(ParentKey);
                    ret[1] = true;
                }
                catch
                {
                    if (DepositWarrantNO != "") DeleteNewestContract(DepositWarrantNO);
                    ret[1] = false;
                    return ret;
                }
            }
            else
            {
                if (DepositWarrantNO != "") DeleteNewestContract(DepositWarrantNO);
                ret[1] = false;
            }
            return ret;
        }

        public void DeleteNewestContract(string DepositWarrantNO)
        {
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Delete From [JBADMIN].[dbo].[SecurityDepositWarrant] where DepositWarrantNO='" + DepositWarrantNO + "'";
                int EffectRow = this.ExecuteCommand(sql, connection, transaction);
                transaction.Commit(); // �T�{���
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw (e);
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
        }
    }
}
