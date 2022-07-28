using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using System.Data.Sql;

namespace sForm_ISODocument
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

        

        //�S�Ψ� �^��ISO�X
        public object[] GetISOCode(object[] objParam)
        {
            string[] aParam = objParam[0].ToString().Split(',');
            string DocPaperNOPrefix = aParam[0].ToString();
            //string js = string.Empty;
            string ISOCode = "0";
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
                string sql = "SELECT top 1 SUBSTRING(DocPaperNO,7,2) from ISODocument   where left(DocPaperNO,6)='" + DocPaperNOPrefix + "' and FlowFlag!='X' order by DocPaperNO desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ISOCode = (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString("00");
                }
                else {
                    ISOCode = "01";
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
            return new object[] { 0, ISOCode };
        }

        public object[] GetDocPaperNO(object[] objParam)
        {
            string[] aParam = objParam[0].ToString().Split(',');
            string DocNO = aParam[0].ToString();
            //string js = string.Empty;
            string DocPaperNO = "0";
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
                string sql = "SELECT DocPaperNO from ISODocument   where DocNO='" + DocNO + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                DocPaperNO = ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, DocPaperNO };
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

        //�۰ʰ_��(�׭q��)
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
                    "C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_ISODocumentModify.xoml",
                    //"C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_ISODocumentModify.xoml",
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
        //(�׭q��)
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

        //�s�W���A�A�ܧ�DocPaperNO�ΰO���Ĥ@���s��
        private void ucISODocument_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {string IsModify = ucISODocument.GetFieldCurrentValue("IsModify").ToString();
         if (IsModify == "n")
         {
            try
            {

                string FirstNO = ucISODocument.GetFieldCurrentValue("FirstNO").ToString();
                string SecondNO = ucISODocument.GetFieldCurrentValue("SecondNO").ToString();
                string DocPropertyNO = ucISODocument.GetFieldCurrentValue("DocPropertyNO").ToString();
                string DocName = ucISODocument.GetFieldCurrentValue("DocName").ToString();
                string DocNO = ucISODocument.GetFieldCurrentValue("DocNO").ToString();
                if (SecondNO == "")
                {
                    SecondNO = FirstNO;
                }

                string ISOEndCode = "01";
                string sql0 = "SELECT top 1 SUBSTRING(DocPaperNO,7,2) from ISODocument   where DocNO!='" + DocNO + "' and left(DocPaperNO,6)='" + FirstNO + SecondNO + "-" + DocPropertyNO + "' and FlowFlag!='X' order by DocPaperNO desc";
                DataSet ds = this.ExecuteSql(sql0, ucISODocument.conn, ucISODocument.trans);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ISOEndCode = this.ExecuteSql(sql0, ucISODocument.conn, ucISODocument.trans).Tables[0].Rows[0][0].ToString();
                    ISOEndCode = (Int32.Parse(ISOEndCode) + 1).ToString("00");
                }

                string DocPaperNOPrefix = FirstNO + SecondNO + "-" + DocPropertyNO + ISOEndCode + "-" + DocName;// +"-" + "01" + "-" + DateTime.Now.ToString("yyyyMMdd");
                string sql = "update ISODocument set DocPaperNO='" + DocPaperNOPrefix + "',FirstDocNO='" + DocNO + "' where DocNO='" + DocNO + "'" + "\r\n";
                this.ExecuteCommand(sql, ucISODocument.conn, ucISODocument.trans);
            }
            catch (Exception ex)
            {
                throw ex;
            }
         }
        }

        //�ק窱�A�A�ܧ�DocPaperNO
        private void ucISODocument_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {

         string IsModify = ucISODocument.GetFieldCurrentValue("IsModify").ToString();
         if (IsModify == "n")
         {
            string DocNO = ucISODocument.GetFieldCurrentValue("DocNO").ToString();
            string sql3 = "SELECT top 1 D_STEP_ID FROM EIPHRSYS.dbo.SYS_TODOLIST WHERE FORM_PRESENTATION = 'DocNO=''" + DocNO + "''' order by update_date desc,update_time desc";
            DataTable tb = this.ExecuteSql(sql3, ucISODocument.conn, ucISODocument.trans).Tables[0];
            string d_STEP_ID = "";
            if (tb.Rows.Count > 0)
            {
                d_STEP_ID = tb.Rows[0][0].ToString();//���d
            }
            //�]�wDocPaperNO ���ȥ��s��
            if (d_STEP_ID == "��󭺦��ӽ�")
            {
                string FirstNO = ucISODocument.GetFieldCurrentValue("FirstNO").ToString();
                string SecondNO = ucISODocument.GetFieldCurrentValue("SecondNO").ToString();
                string DocPropertyNO = ucISODocument.GetFieldCurrentValue("DocPropertyNO").ToString();
                string DocName = ucISODocument.GetFieldCurrentValue("DocName").ToString();
                //string DocNO = ucISODocument.GetFieldCurrentValue("DocNO").ToString();
                if (SecondNO == "")
                {
                    SecondNO = FirstNO;
                }

                string ISOEndCode = "01";
                string sql0 = "SELECT top 1 SUBSTRING(DocPaperNO,7,2) from ISODocument   where DocNO!='" + DocNO + "' and left(DocPaperNO,6)='" + FirstNO + SecondNO + "-" + DocPropertyNO + "' and FlowFlag!='X' order by DocPaperNO desc";
                DataSet ds = this.ExecuteSql(sql0, ucISODocument.conn, ucISODocument.trans);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ISOEndCode = this.ExecuteSql(sql0, ucISODocument.conn, ucISODocument.trans).Tables[0].Rows[0][0].ToString();
                    ISOEndCode = (Int32.Parse(ISOEndCode) + 1).ToString("00");
                }

                string DocPaperNOPrefix = FirstNO + SecondNO + "-" + DocPropertyNO + ISOEndCode + "-" + DocName;// +"-" + "01" + "-" + DateTime.Now.ToString("yyyyMMdd");
                string sql = "update ISODocument set DocPaperNO='" + DocPaperNOPrefix + "' where DocNO='" + DocNO + "'" + "\r\n";
                this.ExecuteCommand(sql, ucISODocument.conn, ucISODocument.trans);
            }
            //�]�wDocPaperNO ���ȥ��s��
            else if (d_STEP_ID == "�D�޼f���v��")
            {
                string DocPaperNO = ucISODocument.GetFieldCurrentValue("DocPaperNO").ToString();
                List<string> DocPaperNOList = DocPaperNO.Split('-').ToList();
                if (DocPaperNOList.Count >= 5)
                {
                    DocPaperNOList.RemoveRange(DocPaperNOList.Count - 2, 2);
                    DocPaperNO = String.Join("-", DocPaperNOList.ToArray());
                }


                //string CreateDate = ucISODocument.GetFieldCurrentValue("CreateDate").ToString();
                //string[] CreateDateArr = CreateDate.Substring(0, CreateDate.IndexOf('��') - 2).Split('/');
                //string CreateDate1 = "";
                //CreateDate1 = CreateDate1 + CreateDateArr[0];
                //CreateDate1 = CreateDate1 + (("0" + CreateDateArr[1]).Substring(("0" + CreateDateArr[1]).Length - 2, 2));
                //CreateDate1 = CreateDate1 + (("0" + CreateDateArr[2]).Substring(("0" + CreateDateArr[2]).Length - 2, 2));

                string CreateDate1 = "";
                string sqlc = "SELECT CreateDate from ISODocument WHERE DocNO='" + DocNO + "'";
                DataTable tbc = this.ExecuteSql(sqlc, ucISODocument.conn, ucISODocument.trans).Tables[0];
                if (tbc.Rows.Count > 0)
                {
                    CreateDate1 = tbc.Rows[0].Field<DateTime>(0).ToString("yyyyMMdd");
                }

                string DocPaperNO1 = DocPaperNO + "-" + "01" + "-" + CreateDate1;  //CreateDate.Format({0:s});
                string sql = "update ISODocument set DocPaperNO='" + DocPaperNO1 + "',IsLast='y' where DocNO='" + DocNO + "'" + "\r\n";
                this.ExecuteCommand(sql, ucISODocument.conn, ucISODocument.trans);
            }
         }
         else if (IsModify == "y")
         {
            string DocNO = ucISODocument.GetFieldCurrentValue("DocNO").ToString();
            string sql3 = "SELECT top 1 D_STEP_ID FROM EIPHRSYS.dbo.SYS_TODOLIST WHERE FORM_PRESENTATION = 'DocNO=''" + DocNO + "''' order by update_date desc,update_time desc";
            DataTable tb = this.ExecuteSql(sql3, ucISODocument.conn, ucISODocument.trans).Tables[0];
            string d_STEP_ID = "";
            if (tb.Rows.Count > 0)
            {
                d_STEP_ID = tb.Rows[0][0].ToString();//���d
            }
            //�]�wDocPaperNO ���ȥ��s��
            if (d_STEP_ID == "�D�޼f���v��")
            {
                try
                {
                    string DocPaperNO = ucISODocument.GetFieldCurrentValue("DocPaperNO").ToString();
                    List<string> DocPaperNOList = DocPaperNO.Split('-').ToList();
                    if (DocPaperNOList.Count >= 5)
                    {
                        DocPaperNOList.RemoveRange(DocPaperNOList.Count - 2, 2);
                        DocPaperNO = String.Join("-", DocPaperNOList.ToArray());
                    }


                    //string CreateDate = ucISODocument.GetFieldCurrentValue("CreateDate").ToString();
                    //string[] CreateDateArr = CreateDate.Substring(0, CreateDate.IndexOf('��') - 2).Split('/');
                    //string CreateDate1 = "";
                    //CreateDate1 = CreateDate1 + CreateDateArr[0];
                    //CreateDate1 = CreateDate1 + (("0" + CreateDateArr[1]).Substring(("0" + CreateDateArr[1]).Length - 2, 2));
                    //CreateDate1 = CreateDate1 + (("0" + CreateDateArr[2]).Substring(("0" + CreateDateArr[2]).Length - 2, 2));

                    string CreateDate1 = "";
                    string sqlc = "SELECT CreateDate from ISODocument WHERE DocNO='" + DocNO + "'";
                    DataTable tbc = this.ExecuteSql(sqlc, ucISODocument.conn, ucISODocument.trans).Tables[0];
                    if (tbc.Rows.Count > 0)
                    {
                        CreateDate1 = tbc.Rows[0].Field<DateTime>(0).ToString("yyyyMMdd");
                    }


                    string Version = "";
                    string sql0 = "SELECT  top 1 left(right(DocPaperNO,11),2) from ISODocument  where left(DocPaperNO,8)='" + DocPaperNO.Substring(0, 8) + "' and FlowFlag!='X' order by DocPaperNO desc";
                    DataSet ds = this.ExecuteSql(sql0, ucISODocument.conn, ucISODocument.trans);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Version = this.ExecuteSql(sql0, ucISODocument.conn, ucISODocument.trans).Tables[0].Rows[0][0].ToString();
                        Version = (Int32.Parse(Version) + 1).ToString("00");
                    }

                    if (CreateDate1 == "" || Version == "") {
                        throw new Exception("���:" + CreateDate1 + "�B����:" + Version);
                    }

                    string DocPaperNO1 = DocPaperNO + "-" + Version + "-" + CreateDate1;
                    string sql = "update ISODocument set DocPaperNO='" + DocPaperNO1 + "',IsLast='y' where DocNO='" + DocNO + "'" + "\r\n";
                    this.ExecuteCommand(sql, ucISODocument.conn, ucISODocument.trans);

                    sql = "update ISODocument set IsLast='n' where DocNO!='" + DocNO + "' and left(DocPaperNO,8)='" + DocPaperNO.Substring(0, 8) + "'" + "\r\n";
                    this.ExecuteCommand(sql, ucISODocument.conn, ucISODocument.trans);
                }
                catch (Exception ex) { throw ex; }
            }
         }
        }
    }
}
