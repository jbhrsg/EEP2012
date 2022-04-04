using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft;
using System.Data.SqlClient; 
using Newtonsoft.Json;
namespace sShortTermMasterApply
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
        public string GetShortTermFixed()
        {
            DateTime datetime = DateTime.Today;
            return "A" + ((datetime.Year) - 1911).ToString().Trim();
        }
        public object[] GetUserOrgNOs(object[] objParam)
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
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
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
        public object[] GetEmpFlowAgentList(object[] objParam)
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
                string UserID = parm[0];
                string Flow = parm[1];
                string sql = "SELECT dbo.funRetrunEmpFlowAgentList('" + UserID + "','" + Flow + "') AS ReturnStr FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = ds.Tables[0].Rows[0]["ReturnStr"].ToString(); ;
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

        private void ucShortTermDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucShortTermDetails.SetFieldValue("CreateDate", DateTime.Now);
        }

        private void ucShortTermMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucShortTermMaster.SetFieldValue("CreateDate", DateTime.Now);
        }
        //�g�J�ȭɴ�
        public object PostToShortTerm(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
              int FlowDirection = (int)objParam[1];
                  if (FlowDirection == 1)// ���Vñ��
                       {
                           //���o�ϥΪ̩m�W
                           object[] res = SrvUtils.GetValue("_username", this);
                           string username = res[1].ToString();
                           DataRow dr = (DataRow)objParam[0];
                           string ShortTermNO = dr["ShortTermNO"].ToString();
                           IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                           if (conn.State != ConnectionState.Open)
                           {
                               conn.Open();
                           }
                           try
                           {
                               IDbTransaction trans = conn.BeginTransaction();
                               string sql = "EXEC procPutFeeToShortTerm  '" + ShortTermNO + "','" + username + "'";
                               this.ExecuteSql(sql, conn, trans);
                               trans.Commit();
                           }
                           finally
                           {
                               ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
                           }
                       }
                       return ret;

        }
        //�g�J�ȭɴ�
        public object UpdateShortTermToEnd (object[] objParam)
        {
            //���o�ϥΪ̩m�W
            object[] res = SrvUtils.GetValue("_username", this);
            string username = res[1].ToString();
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0];
            string ShortTermNO = dr["ShortTermNO"].ToString();
            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                IDbTransaction trans = conn.BeginTransaction();
                string sql = "EXEC procUpdateShortTermToEnd  '" + ShortTermNO + "','" + username + "'"; 
                this.ExecuteSql(sql, conn, trans);
                trans.Commit();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return ret;

        }
        public object[] GetSYS_TODOLISTStatus(object[] objParam)
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
                string BILLNO = parm[0];
                string UserID = parm[1];
                string sql = "SELECT STATUS AS FlowStatus FROM EIPHRSYS.dbo.SYS_TODOLIST WHERE FORM_PRESENTATION='SHORTTERMNO=''" + BILLNO + "'''";
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
        public object[] CheckSetUpFeeStatus(object[] objParam)
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
                string ShortTermNO = objParam[0].ToString();
                string sql = "SELECT COUNT(ShortTermNO) as cnt FROM [60.250.52.106,1433].FWCRM.DBO.EmployeeFees WHERE ShortTermNO = '" + ShortTermNO + "'";
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
        public object[] CheckSetUpFeeStatusBill(object[] objParam)
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
                string ShortTermNO = objParam[0].ToString();
                string sql = "SELECT COUNT(ShortTermNO) as cnt FROM [60.250.52.106,1433].FWCRM.DBO.FeeSetUpM WHERE left(ShortTermNO,8) = '" + ShortTermNO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
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
        public object[] CheckSetUpFeeStatusMonth(object[] objParam)
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
                string sYM = objParam[0].ToString();
                string sql = "EXEC dbo.procReturnCheckMonthSetUpFee  '" + sYM + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["Result"].ToString();
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
        public object[] procInsertFeeSetUpMbyEEP(object[] objParam)
        {
            SqlCommand cmd;
            SqlConnection conn;
            string connectionstr = null;
            string[] parm = objParam[0].ToString().Split(',');
            string FeeID = parm[0].ToString();
            string YearMonth = parm[1].ToString();
            string FeeSDate = parm[2].ToString();
            string FeeEDate = parm[3].ToString();
            string ShortTermAmount = parm[4].ToString();
            string ShortTermNO = parm[5].ToString();
            string DormID = parm[6].ToString();
            string UserName = parm[7].ToString();
            string js = string.Empty;
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            connectionstr = "data source = 192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";
            //connectionstr = "data source = 192.168.1.38;Initial Catalog=JBADMIN;User ID=sa;Password=NBV2mXzr";
            conn = new SqlConnection(connectionstr);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //�}�ltransaction
            //IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC [60.250.52.106,1433].FWCRM.dbo.procInsertFeeSetUpMbyEEP '" + FeeID + "','" + YearMonth + "','" + FeeSDate + "','" + FeeEDate + "','" + ShortTermAmount + "','" + ShortTermNO + "','" + DormID + "','" + UserName + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //this.ExecuteSql(sql, connection, transaction);
            }
           
            finally
            {
                //transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, true };
        }
        //�_��-�дڳ�
        public object[] BillingRequisitionFromShortTermMaster(object[] objParam)
        {
            DataRow dr = (DataRow)objParam[0];
            IDbConnection connection;
            IDbTransaction transaction;
            object[] res = SrvUtils.GetValue("_username", this);
            string CreateBy = res[1].ToString();
            string ShortTermNO = dr["ShortTermNO"].ToString();
            int flowDirection = (int)objParam[1];//1�e�i 2�h�^
            if (flowDirection == 1)
            {
                //��X�n�J�]�Ʋ��ʳ檺��f���Ө�DataTable��
                connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                transaction = connection.BeginTransaction();
                try
                {
                    string sql = "EXEC procInsertRequisitionFromShortTermMaster '" + ShortTermNO + "','"+ CreateBy + "'";
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

                //�����s�W���\�A�A�_��(�_��e��_��ӽЪ̪�¾�٥N���B��s�W���겣���ʳ渹TranNO(where ���ʳ渹 �M ���_��))

                //1.��RoleID
                string RoleID = "";
                string EmployeeID = dr["EmployeeID"].ToString();
                connection = (IDbConnection)AllocateConnection("EIPHRSYS");
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                transaction = connection.BeginTransaction();
                try
                {
                    //string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    //��D�ި���ID
                    string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + EmployeeID + "' and ORG_DESC !='�֩e�|'";
                    //�쳡��������ID
                    string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + EmployeeID + "'";
                    DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//�O�D��
                    {
                        RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                        ds.Dispose();
                    }
                    //���O�D��
                    else
                    {
                        ds = this.ExecuteSql(sql1, connection, transaction);
                        if (ds.Tables[0].Rows.Count > 0)//�O����
                        {
                            RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                        }
                        ds.Dispose();
                    }
                    transaction.Commit(); // �T�{���
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ReleaseConnection("EIPHRSYS", connection);
                }

                //2.��TranNO
                DataTable RequisitionTable = new DataTable();
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
                        string sql = "Select RequisitionNO From [Requisition] Where left(ShortTermNO,8)='" + ShortTermNO + "' and flowflag is null";// 
                        RequisitionTable = this.ExecuteSql(sql, connection, transaction).Tables[0];
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                    }
                }

                if (RoleID != "" && RequisitionTable.Rows.Count > 0)
                {
                    try
                    {
                        string RequisitionNO;
                        foreach (DataRow drow in RequisitionTable.Rows)
                        {
                            RequisitionNO = drow["RequisitionNO"].ToString();
                            //�_��
                            EEPRemoteModule ep = new EEPRemoteModule();
                            object[] ret = ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                            null,
                            new object[]{
                                "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml",
                                string.Empty,////�ťէY�i�A�t�Ψϥ�
                                0,//�O�_�����n�ӽ�
                                0,//�O�_�����ӽ�
                                "",//����N������
                                RoleID,//�ӽЪ̪�RoleID(����s��)
                                "sRequisition.Requisition",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                                0,//�t�Ψϥ�
                                "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                                "" //����
                            },
                            new object[]{
                                "RequisitionNO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                                "RequisitionNO = '"+ RequisitionNO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                            }
                            });

                            if (ret[0].ToString() != "0")//�_�椣���\�A0�O���\
                            {
                                throw new Exception(ret[0] + "," + ret[1]);
                            }
                        }//foreach
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return new object[] { 0, 0 };
       } 
      
    }
}
