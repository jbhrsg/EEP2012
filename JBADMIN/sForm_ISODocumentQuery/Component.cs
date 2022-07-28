using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;


namespace sForm_ISODocumentQuery
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

        public string DocNOGetFixed()
        {
            string Year = DateTime.Now.Year.ToString();
            return "DOC" + Year;
        }

        //�۰ʰ_��(�S�Ψ�A�]��Form_ISODocumentModify.xoml��page��Form_ISODocument.aspx)
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            //string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string DocNO = "";

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

            //2.��DocNO
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
                    string sql = "SELECT Top 1 DocNO FROM [JBADMIN].[dbo].[ISODocument] ORDER BY DocNO DESC ";// 
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    DocNO = ds.Tables[0].Rows[0]["DocNO"].ToString();
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
            if (RoleID != "" && DocNO != "")//��RoleID�A��DocNO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_ISODocumentModify.xoml",
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_ISODocumentModify.xoml",
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    RoleID,//�ӽЪ̪�RoleID(����s��)
                    "sForm_ISODocument.ISODocument",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                },
                new object[]{
                    "DocNO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "DocNO='"+ DocNO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                }
                    });
                    //UpdateParentContract(ParentKey);
                    ret[1] = true;
                }
                catch
                {
                    if (DocNO != "") DeleteNewestISODocument(DocNO);
                    ret[1] = false;
                    return ret;
                }
            }
            else
            {
                if (DocNO != "") DeleteNewestISODocument(DocNO);
                ret[1] = false;
            }
            return ret;
        }
        //(�S�Ψ�A�]���۰ʰ_��S��)
        public void DeleteNewestISODocument(string DocNO)
        {
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Delete From [JBADMIN].[dbo].[ISODocument] where DocNO='" + DocNO + "'";
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

        //�^�Ǧ����O�_�b�y�{��
        public object[] IsProcess(object[] objParam)
        {
            string[] aParam = objParam[0].ToString().Split(',');
            string DocPaperNO = aParam[0].ToString();
            //string js = string.Empty;
            string IsProcessing = "0";
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
                string sql = "SELECT DocNO from ISODocument  where left(DocPaperNO,8)='" + DocPaperNO.Substring(0,8) + "' and FlowFlag in ('N','P')";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IsProcessing = "y";
                }else {
                    IsProcessing = "n";
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
            return new object[] { 0, IsProcessing };
        }

        //���o��´�s��
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
                string sql = "SELECT dbo.funReturnEmpOrgNOAllandL2('" + UserID + "') AS OrgNOs FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                //funReturnEmpOrgNOAll--�Ǧ^�ϥΪ̩Ҧ��������N��eg:10760,10700,
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

        //�ϥΪ�ID��W��
        public object[] ReturnUserNames(object[] objParam)
        {
            string UserIDs = objParam[0].ToString();
            //string js = string.Empty;
            string UserNames = "";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select dbo.funReturnUserNames('" + UserIDs + "') as UserNames";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UserNames = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("EIPHRSYS", connection);
            }
            return new object[] { 0, UserNames };
        }

        //������ID��W��
        public object[] ReturnOrgNames(object[] objParam)
        {
            string OrgIDs = objParam[0].ToString();
            //string js = string.Empty;
            string OrgNames = "";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select dbo.funReturnOrgNames('" + OrgIDs + "') as OrgNames";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    OrgNames = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("EIPHRSYS", connection);
            }
            return new object[] { 0, OrgNames };
        }

        private void ucISODocumentForm_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            try
            {
                string year = DateTime.Now.Year.ToString().Substring(2, 2);
                string DocNO = ucISODocumentForm.GetFieldCurrentValue("DocNO").ToString();
                string FirstDocNO = "";
                string sql2 = "SELECT FirstDocNO from ISODocument where DocNO='" + DocNO + "'";
                DataSet ds2 = this.ExecuteSql(sql2, ucISODocumentForm.conn, ucISODocumentForm.trans);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    FirstDocNO = ds2.Tables[0].Rows[0][0].ToString();
                }
                string FormNO = "";
                //string ISOEndCode = "01";
                string sql0 = "SELECT top 1 FormNO from ISODocumentForm where FirstDocNO='" + FirstDocNO + "' order by FormNO desc";
                DataSet ds = this.ExecuteSql(sql0, ucISODocumentForm.conn, ucISODocumentForm.trans);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string year1;
                    string FormNO1 = ds.Tables[0].Rows[0][0].ToString();
                    year1 = FormNO1.Substring(0, 2);
                    if (year1 == year)
                    {
                        FormNO = (Int32.Parse(FormNO1.Substring(2, 4)) + 1).ToString("0000");
                        FormNO = year + FormNO;
                    }
                    else
                    {
                        FormNO = year + "0001";
                    }
                }
                else
                {
                    FormNO = year + "0001";
                }

                ucISODocumentForm.SetFieldValue("FormNO", FormNO);
                ucISODocumentForm.SetFieldValue("FirstDocNO", FirstDocNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
