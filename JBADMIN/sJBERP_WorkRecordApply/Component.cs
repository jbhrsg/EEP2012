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

namespace sJBERP_WorkRecordApply
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

        public string autoNumber1GetFixed()
        {
            //string year = DateTime.Now.Year.ToString();
            //string month = DateTime.Now.Month.ToString("d2");
            return "WR";
        }
        //���o�ǤJ�����N����´�𪬵��c
        public object[] GetUserOrg(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "EXEC [dbo].[procGetOrgNOUnderUser]  '" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js }; ;

        }


        public object[] GetNextDayPlan(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string WorkDate = parm[1];
                string sql = "select  top 1 ISNULL(NextDayPlan,'') as NextDayPlan from WRMaster where USERID='" + UserID + "' and WorkDate < '" + WorkDate + "' order by WorkDate desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js }; ;

        }
        

        //�_�� for �u�@������s�W��
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            string WRNO = aParam[1].ToString();
            string RoleID = "";
            //string WRNO = "";

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

            //2.��WRNO
            //if (RoleID != "")
            //{
            //    connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //    if (connection.State != ConnectionState.Open)
            //    {
            //        connection.Open();
            //    }
            //    transaction = connection.BeginTransaction();
            //    try
            //    {
            //        string sql = "SELECT Top 1 [WRNO] FROM [JBADMIN].[dbo].[WRMaster] ORDER BY WRNO DESC ";// 
            //        DataSet ds = this.ExecuteSql(sql, connection, transaction);
            //        WRNO = ds.Tables[0].Rows[0]["WRNO"].ToString();
            //        transaction.Commit(); // �T�{���
            //    }
            //    catch
            //    {
            //        ret[1] = false;
            //        return ret;
            //    }
            //    finally
            //    {
            //        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            //    }
            //}

            //3.�_��
            if (RoleID != "" && WRNO != "")//��RoleID�A��WRNO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                    null,
                    new object[]{
                    "C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\JBERP_WorkRecordApply.xoml",
                    //"C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\JBERP_WorkRecordApply.xoml",
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    RoleID,//�ӽЪ̪�RoleID(����s��)
                    "sJBERP_WorkRecordApply.WRMaster",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                    },
                    new object[]{
                    "WRNO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "WRNO='"+ WRNO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                    }
                });
                    ret[1] = true;
                }
                catch (Exception ex)
                {
                    ret[1] = false;
                    //return ret;
                    throw ex;
                }
            }

            return ret;
        }


        
    }
}
