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

        //請款單起單
        public object[] MakeRequisition(object[] objParam)
        {
            string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string role_id = "";
            role_id = GetRoleID(user_id);

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
                if (role_id == "") { throw new Exception("沒有角色代號"); }
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

        //求roleID
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
            //請款單是來自哪個保證金沖銷單
            string DepositWarrantNO = ucSecurityDepositWarrant.GetFieldCurrentValue("DepositWarrantNO").ToString();
            string RequisitionNO = ucSecurityDepositWarrant.GetFieldCurrentValue("RequisitionNO").ToString();
            string sql = "update Requisition set SourceBillNO='" + DepositWarrantNO + "' where RequisitionNO='" + RequisitionNO + "'";
            this.ExecuteCommand(sql, ucSecurityDepositWarrant.conn, ucSecurityDepositWarrant.trans);
        }
    }
}
