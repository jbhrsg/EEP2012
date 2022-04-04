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
        //取得傳入部門代號組織樹狀結構
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
        

        //起單 for 工作紀錄單新增後
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            string WRNO = aParam[1].ToString();
            string RoleID = "";
            //string WRNO = "";

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

            //2.抓WRNO
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
            //        transaction.Commit(); // 確認交易
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

            //3.起單
            if (RoleID != "" && WRNO != "")//有RoleID，有WRNO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                    null,
                    new object[]{
                    "C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\JBERP_WorkRecordApply.xoml",
                    //"C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\JBERP_WorkRecordApply.xoml",
                    string.Empty,////空白即可，系統使用
                    0,//是否為重要申請
                    0,//是否為緊急申請
                    "",//提交意見說明
                    RoleID,//申請者的RoleID(角色編號)
                    "sJBERP_WorkRecordApply.WRMaster",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                    0,//系統使用
                    "0",//組織類別編號ex:0公司組織、1福利委員會
                    "" //附件
                    },
                    new object[]{
                    "WRNO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "WRNO='"+ WRNO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
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
