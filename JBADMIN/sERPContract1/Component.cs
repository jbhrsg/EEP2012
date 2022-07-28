using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using Newtonsoft.Json;
using System.Data;
using System.Collections;


namespace sERPContract
{
    //using EFClientTools;
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
        //�۰ʰ_��
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string ContractKey = "";
            
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
                else {//���O�D��
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

            //2.��ContractKey
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
                    string sql = "SELECT Top 1 ContractKey FROM [JBADMIN].[dbo].[ERPContract] ORDER BY ContractKey DESC ";// 
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    ContractKey = ds.Tables[0].Rows[0]["ContractKey"].ToString();
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
            if (RoleID != "" && ContractKey != "")//��RoleID�A��ContractKey
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Contract1.xoml",
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Contract1.xoml",
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    RoleID,//�ӽЪ̪�RoleID(����s��)
                    "sERPContract.ERPContract",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                },
                new object[]{
                    "ContractKey",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "ContractKey='"+ ContractKey +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                }
                    });
                    UpdateParentContract(ParentKey);
                    ret[1] = true;
                }
                catch
                {
                    if (ContractKey != "") DeleteNewestContract(ContractKey);
                    ret[1] = false;
                    return ret;
                }
            }
            else
            {
                if (ContractKey != "") DeleteNewestContract(ContractKey);
                ret[1] = false;
            }
            return ret;
        }

        //public object[] DeleteNewestContract(object[] objParam) { 
        
        //}
        public void DeleteNewestContract(string ContractKey)
        {
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Delete From [JBADMIN].[dbo].[ERPContract] where ContractKey='" + ContractKey+"'";
                int EffectRow = this.ExecuteCommand(sql, connection, transaction);
                transaction.Commit(); // �T�{���
            }
            catch(Exception e)
            {
                transaction.Rollback();
                throw (e);
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
        }

        public void UpdateParentContract(string ParentKey)
        {
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "UPDATE [JBADMIN].[dbo].[ERPContract] SET ContinueFlag=1 where ContractKey='" + ParentKey+"'";
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
        


        //����@�o������檺continueflag��^null�A�p�����檺"��u"���s�~�|�X�{
        public object[] FlowReject(object[] objParam)
        {
            DataRow dr = (DataRow)objParam[0];
            string ParentKey = dr["ParentKey"].ToString();
            object[] ret = new object[] { 0, 0 };
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "UPDATE [JBADMIN].[dbo].[ERPContract] SET ContinueFlag=null where ContractKey='"+ ParentKey+"'";
                int EffectRow = this.ExecuteCommand(sql, connection, transaction);
                if (EffectRow != 0)
                {
                    transaction.Commit(); // �T�{���
                }
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;
        }

        //public object[] GetContractNO(object[] objParam)
        //{
        //    object[] ret = new object[] { 0, 0 };
        //    //string[] aParam = objParam[0].ToString().Split(',');
        //    //string ContractKey = aParam[0].ToString();
        //    IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
        //    if (connection.State != ConnectionState.Open) { connection.Open(); }
        //    IDbTransaction transaction = connection.BeginTransaction();
        //    try
        //    {
        //        //����ContractNO����5�X
        //        string sql = "select top 1 ContractNO from [JBADMIN].[dbo].[ERPContract] where ContractNO!='' order by [ContractNO] desc";
        //        DataSet ds = this.ExecuteSql(sql, connection, transaction);
        //        string ContractNO = string.Empty;
        //        ContractNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["ContractNO"].ToString();
        //        string suffix = "";
        //        if (ContractNO != "")//������
        //        {
        //            if (ContractNO.Substring(0, 4) == DateTime.Today.Year.ToString("0000"))//�P�~��
        //            {
        //                suffix = (Convert.ToInt32(ContractNO.Substring(4, 5))+ 1).ToString("00000");;//�P�~�P����~��[1
        //            }
        //            else//���P�~��
        //            {
        //                suffix = "00001";
        //            }
        //        }
        //        else//�S����
        //        {
        //            suffix = "00001";
        //        }
                
        //        ContractNO = DateTime.Today.Year.ToString("0000") + suffix;

        //        transaction.Commit();
        //        ret[1] = ContractNO;
        //    }
        //    catch
        //    {
        //        transaction.Rollback();
        //        ret[1] = false;
        //        return ret;
        //    }
        //    finally
        //    {
        //        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
        //    }
        //    return ret;
        //}

        public object[] GetGroupName(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT CENTER_CNAME FROM [JBADMIN].[dbo].[ERPContractGroupUser] u inner join [JBADMIN].[dbo].[ERPContractGroup] g on u.CENTER_ID=g.CENTER_ID where userid ='" + userid + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                var js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;
        }

        //�S�Ψ�
        public object SetContractNO(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            var ContractKey = drDara["ContractKey"].ToString();

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //����ContractNO����5�X
                string sql = "select top 1 ContractNO from [JBADMIN].[dbo].[ERPContract] where ContractNO!='' order by ContractNO desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                string ContractNO = string.Empty;
                ContractNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["ContractNO"].ToString();
                string suffix = "";
                if (ContractNO != "")//������
                {
                    if (ContractNO.Substring(0, 4) == DateTime.Today.Year.ToString("0000"))//�P�~��
                    {
                        suffix = (Convert.ToInt32(ContractNO.Substring(4, 5)) + 1).ToString("00000"); ;//�P�~���~��[1
                    }
                    else//���P�~��
                    {
                        suffix = "00001";
                    }
                }
                else//�S����
                {
                    suffix = "00001";
                }

                ContractNO = DateTime.Today.Year.ToString("0000") + suffix;

                string sql1 = "update [JBADMIN].[dbo].[ERPContract] set ContractNO='" + ContractNO + "' where ContractKey='" + ContractKey+"'";
                this.ExecuteSql(sql1, connection, transaction);
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
            return ret;
        }

        //�O�_���~�ҳ�
        public object[] IsForeignDept(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            //DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            //var OverTimeNO = drDara["OverTimeNO"].ToString();
            string[] strParam = objParam[0].ToString().Split(',');
            string userid = strParam[0];
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
                var detailSql = "";
                //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //string userName = SrvGL.GetUserName(userid);
                detailSql = "SELECT count(USERID) as counts FROM [EIPHRSYS].[dbo].[USERGROUPS] ug left join GROUPS g on ug.GROUPID=g.groupid left join SYS_ORGROLES orgr on orgr.ROLE_ID=ug.GROUPID left join [SYS_ORG] org on org.ORG_NO=orgr.ORG_NO where (org.ORG_NO like '107%' or GROUPNAME like '%�~��%') and userid='" + userid + "'";
                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                string counts = ds.Tables[0].Rows[0]["counts"].ToString();
                transaction.Commit(); // �T�{���
                string js = JsonConvert.SerializeObject(counts, Formatting.Indented);
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection("EIPHRSYS", connection);
            }
            return ret; // �Ǧ^��: �L
        }


        public string GetContractNO1()
        {
            //object[] ret = new object[] { 0, 0 };
            //string[] aParam = objParam[0].ToString().Split(',');
            //string ContractKey = aParam[0].ToString();
            string ContractNO = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //����ContractNO����5�X
                string sql = "select top 1 ContractNO from [JBADMIN].[dbo].[ERPContract] where ContractNO!='' order by [ContractNO] desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                
                ContractNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["ContractNO"].ToString();
                string suffix = "";
                if (ContractNO != "")//������
                {
                    if (ContractNO.Substring(0, 4) == DateTime.Today.Year.ToString("0000"))//�P�~��
                    {
                        suffix = (Convert.ToInt32(ContractNO.Substring(4, 5)) + 1).ToString("00000"); ;//�P�~�P����~��[1
                    }
                    else//���P�~��
                    {
                        suffix = "00001";
                    }
                }
                else//�S����
                {
                    suffix = "00001";
                }

                ContractNO = DateTime.Today.Year.ToString("0000") + suffix;

                transaction.Commit();
                //ret[1] = ContractNO;
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
                //ret[1] = false;
                //return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            //return ret;
            return ContractNO;
        }

        private void ucERPContract_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPContract.SetFieldValue("ContractNO", GetContractNO1());
        }

        public object[] InsertERPContractAlter(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            int flowDirection = (int)objParam[1];//1�e�i 2�h�^
            if (flowDirection == 1)
            {
                string js = string.Empty;
                DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                    string ContractNO = dr["ContractNO"].ToString();
                    string BeginDate = string.Format("{0:yyyy/MM/dd}", dr["BeginDate"]);
                    string EndDate = string.Format("{0:yyyy/MM/dd}", dr["EndDate"]);
                    string PhysicalContractNO = dr["PhysicalContractNO"].ToString();
                    string RemindDays = dr["RemindDays"].ToString();
                    string GuarantyEndDate = string.Format("{0:yyyy/MM/dd}", dr["GuarantyEndDate"]);
                    
                    string CreateBy = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    string NewContractAlterNO = GetContractAlterNO();

                    string sql = "Insert Into ERPContractAlter  (ContractAlterNO,ContractNO,BeginDate,EndDate,PhysicalContractNO,RemindDays,GuarantyEndDate,CreateBy,CreateDate) values ('" + NewContractAlterNO + "','" + ContractNO + "','" + BeginDate + "','" + EndDate + "','" + PhysicalContractNO + "','" + RemindDays + "','" + GuarantyEndDate + "','" + CreateBy + "',GETDATE())";
                    this.ExecuteCommand(sql, connection, transaction);
                    transaction.Commit();
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }
            return ret;

        }

        public string GetContractAlterNO()
        {
            string NewContractAlterNO = "";
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
                string sql = "select top 1 ContractAlterNO  FROM [JBADMIN].[dbo].[ERPContractAlter] order by ContractAlterNO desc";//���̤j�s��
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ContractAlterNO = ds.Tables[0].Rows[0][0].ToString();
                    string year = ContractAlterNO.Substring(3, 4);
                    if (DateTime.Now.Year.ToString() == year)//�P�~�֥[
                    {
                        int number = int.Parse(ContractAlterNO.Substring(7, 5)) + 1;
                        NewContractAlterNO = "COA" + year + number.ToString().PadLeft(5, '0');
                    }
                    else
                    {
                        NewContractAlterNO = "COA" + DateTime.Now.Year.ToString() + "00001";
                    }
                }
                else {
                    NewContractAlterNO = "COA" + DateTime.Now.Year.ToString() + "00001";
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return NewContractAlterNO;
        }
    }
}
