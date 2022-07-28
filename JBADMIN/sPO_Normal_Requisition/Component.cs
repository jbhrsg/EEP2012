using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sPO_Normal_Requisition
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

        public string ReturnGetFixed()
        {
            string Year =DateTime.Now.Year.ToString();
            return "PO" + Year;
        }

        //�۰ʰ_��
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            //string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string PONO = "";

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

            //2.��PONO
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
                    string sql = "SELECT Top 1 [PONO] FROM [JBADMIN].[dbo].[POMaster] ORDER BY PONO DESC ";// 
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    PONO = ds.Tables[0].Rows[0]["PONO"].ToString();
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
            if (RoleID != "" && PONO != "")//��RoleID�A��PONO
            {
                try
                {
                EEPRemoteModule ep = new EEPRemoteModule();
                ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                    null,
                    new object[]{
                    "C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_Requisition.xoml",
                    //"C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_Requisition.xoml",
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    RoleID,//�ӽЪ̪�RoleID(����s��)
                    "sPO_Normal_Requisition.POMaster",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                    },
                    new object[]{
                    "PONO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "PONO='"+ PONO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                    }
                });
                ret[1] = true;
                }
                catch
                {
                    ret[1] = false;
                    return ret;
                }
            }
            
            return ret;
        }

        //���o��´�s��(�p�G�ۤv���b��´��)�B���ݥD�ު���´�s��
        public object[] GetUserOrgNOs(object[] objParam)
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                //�ϥΪ̲�´�F�Y�S�ϥΪ̲�´(�ϥΪ̬O��´�̳��D��)�A�h���Һ޲�´
                //�ϥΪ̲�´�F�Y�S�b�ϥΪ̲�´(�ϥΪ̬O��´�̳��D��)�A�h���Һ޲�´���W�h��´
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
