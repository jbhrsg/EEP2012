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

        //請款單起單
        public object[] MakeRequisition(object[] objParam)
        {
            string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string role_id="";
            role_id=GetRoleID(user_id);
            
            object[] ret = new object[] { 0, 0 };
            string str = objParam[0].ToString();
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                if (role_id == "") { throw new Exception("沒有角色代號");}
                if (str == "自動編號") {str=SelectRequisition1(); }
                EEPRemoteModule ep = new EEPRemoteModule();
                ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml",//流程文件名字，包含完整路徑。//"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml"
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml",
                    string.Empty,////空白即可，系統使用
                    0,//是否為重要申請
                    0,//是否為緊急申請
                    "",//提交意見說明
                    role_id,//申請者的RoleID(角色編號)
                    "sRequisition.Requisition",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                    0,//系統使用
                    "0",//組織類別編號ex:0公司組織、1福利委員會
                    "" //附件
                },
                new object[]{
                    "RequisitionNO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "RequisitionNO ='"+ str +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
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

        //取請款單單號
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

        //取請款單單號
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

        //求roleID
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
            //請款單是來自哪個保證金單
            string DepositNO = ucSecurityDeposit.GetFieldCurrentValue("DepositNO").ToString();
            string RequisitionNO = ucSecurityDeposit.GetFieldCurrentValue("RequisitionNO").ToString();
            string sql = "update Requisition set SourceBillNO='" + DepositNO + "' where RequisitionNO='"+RequisitionNO+"'";
            this.ExecuteCommand(sql, ucSecurityDeposit.conn, ucSecurityDeposit.trans);
        }

        //自動起單
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            //string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string DepositWarrantNO = "";

            //1.抓RoleID
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //抓主管角色ID
                string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + userid + "' and ORG_DESC !='福委會'";
                //抓部門的角色ID
                string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + userid + "'";
                DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)//是主管
                {
                    RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                    ds.Dispose();
                }
                else
                {//不是主管
                    ds = this.ExecuteSql(sql1, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//是部門下屬
                    {
                        RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                    }
                    ds.Dispose();
                }
                transaction.Commit(); // 確認交易
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

            //2.抓DepositWarrantNO
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
                    transaction.Commit(); // 確認交易
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

            //3.起單
            if (RoleID != "" && DepositWarrantNO != "")//有RoleID，有DepositWarrantNO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_SecurityDepositWarrant.xoml",
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_SecurityDepositWarrant.xoml",
                    string.Empty,////空白即可，系統使用
                    0,//是否為重要申請
                    0,//是否為緊急申請
                    "",//提交意見說明
                    RoleID,//申請者的RoleID(角色編號)
                    "sForm_SecurityDepositWarrant.SecurityDepositWarrant",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                    0,//系統使用
                    "0",//組織類別編號ex:0公司組織、1福利委員會
                    "" //附件
                },
                new object[]{
                    "DepositWarrantNO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "DepositWarrantNO='"+ DepositWarrantNO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
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
                transaction.Commit(); // 確認交易
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
