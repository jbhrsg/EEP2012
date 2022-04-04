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
        //寫入暫借款
        public object PostToShortTerm(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
              int FlowDirection = (int)objParam[1];
                  if (FlowDirection == 1)// 正向簽核
                       {
                           //取得使用者姓名
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
        //寫入暫借款
        public object UpdateShortTermToEnd (object[] objParam)
        {
            //取得使用者姓名
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
                //Indented縮排 將資料轉換成Json格式
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
            //開始transaction
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
        //起單-請款單
        public object[] BillingRequisitionFromShortTermMaster(object[] objParam)
        {
            DataRow dr = (DataRow)objParam[0];
            IDbConnection connection;
            IDbTransaction transaction;
            object[] res = SrvUtils.GetValue("_username", this);
            string CreateBy = res[1].ToString();
            string ShortTermNO = dr["ShortTermNO"].ToString();
            int flowDirection = (int)objParam[1];//1前進 2退回
            if (flowDirection == 1)
            {
                //抓出要入設備異動單的交貨明細到DataTable裡
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

                //全部新增成功，再起單(起單前抓起單申請者者職稱代號、剛新增的資產異動單號TranNO(where 請購單號 和 未起單))

                //1.抓RoleID
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
                    //抓主管角色ID
                    string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + EmployeeID + "' and ORG_DESC !='福委會'";
                    //抓部門的角色ID
                    string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + EmployeeID + "'";
                    DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//是主管
                    {
                        RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                        ds.Dispose();
                    }
                    //不是主管
                    else
                    {
                        ds = this.ExecuteSql(sql1, connection, transaction);
                        if (ds.Tables[0].Rows.Count > 0)//是部屬
                        {
                            RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                        }
                        ds.Dispose();
                    }
                    transaction.Commit(); // 確認交易
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ReleaseConnection("EIPHRSYS", connection);
                }

                //2.抓TranNO
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
                            //起單
                            EEPRemoteModule ep = new EEPRemoteModule();
                            object[] ret = ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                            null,
                            new object[]{
                                "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml",
                                string.Empty,////空白即可，系統使用
                                0,//是否為重要申請
                                0,//是否為緊急申請
                                "",//提交意見說明
                                RoleID,//申請者的RoleID(角色編號)
                                "sRequisition.Requisition",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                                0,//系統使用
                                "0",//組織類別編號ex:0公司組織、1福利委員會
                                "" //附件
                            },
                            new object[]{
                                "RequisitionNO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                                "RequisitionNO = '"+ RequisitionNO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
                            }
                            });

                            if (ret[0].ToString() != "0")//起單不成功，0是成功
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
