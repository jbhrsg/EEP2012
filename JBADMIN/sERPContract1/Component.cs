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
        //自動起單
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string ContractKey = "";
            
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
                else {//不是主管
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

            //2.抓ContractKey
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
            if (RoleID != "" && ContractKey != "")//有RoleID，有ContractKey
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Contract1.xoml",
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Contract1.xoml",
                    string.Empty,////空白即可，系統使用
                    0,//是否為重要申請
                    0,//是否為緊急申請
                    "",//提交意見說明
                    RoleID,//申請者的RoleID(角色編號)
                    "sERPContract.ERPContract",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                    0,//系統使用
                    "0",//組織類別編號ex:0公司組織、1福利委員會
                    "" //附件
                },
                new object[]{
                    "ContractKey",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "ContractKey='"+ ContractKey +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
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
                transaction.Commit(); // 確認交易
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
        


        //續約作廢須把父單的continueflag改回null，如此父單的"續聘"按鈕才會出現
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
                    transaction.Commit(); // 確認交易
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
        //        //產生ContractNO的後5碼
        //        string sql = "select top 1 ContractNO from [JBADMIN].[dbo].[ERPContract] where ContractNO!='' order by [ContractNO] desc";
        //        DataSet ds = this.ExecuteSql(sql, connection, transaction);
        //        string ContractNO = string.Empty;
        //        ContractNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["ContractNO"].ToString();
        //        string suffix = "";
        //        if (ContractNO != "")//有紀錄
        //        {
        //            if (ContractNO.Substring(0, 4) == DateTime.Today.Year.ToString("0000"))//同年份
        //            {
        //                suffix = (Convert.ToInt32(ContractNO.Substring(4, 5))+ 1).ToString("00000");;//同年同月份才能加1
        //            }
        //            else//不同年份
        //            {
        //                suffix = "00001";
        //            }
        //        }
        //        else//沒紀錄
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

        //沒用到
        public object SetContractNO(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow drDara = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            var ContractKey = drDara["ContractKey"].ToString();

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //產生ContractNO的後5碼
                string sql = "select top 1 ContractNO from [JBADMIN].[dbo].[ERPContract] where ContractNO!='' order by ContractNO desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                string ContractNO = string.Empty;
                ContractNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["ContractNO"].ToString();
                string suffix = "";
                if (ContractNO != "")//有紀錄
                {
                    if (ContractNO.Substring(0, 4) == DateTime.Today.Year.ToString("0000"))//同年份
                    {
                        suffix = (Convert.ToInt32(ContractNO.Substring(4, 5)) + 1).ToString("00000"); ;//同年份才能加1
                    }
                    else//不同年份
                    {
                        suffix = "00001";
                    }
                }
                else//沒紀錄
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

        //是否為外勞部
        public object[] IsForeignDept(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            //DataRow drDara = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            //var OverTimeNO = drDara["OverTimeNO"].ToString();
            string[] strParam = objParam[0].ToString().Split(',');
            string userid = strParam[0];
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var detailSql = "";
                //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //string userName = SrvGL.GetUserName(userid);
                detailSql = "SELECT count(USERID) as counts FROM [EIPHRSYS].[dbo].[USERGROUPS] ug left join GROUPS g on ug.GROUPID=g.groupid left join SYS_ORGROLES orgr on orgr.ROLE_ID=ug.GROUPID left join [SYS_ORG] org on org.ORG_NO=orgr.ORG_NO where (org.ORG_NO like '107%' or GROUPNAME like '%外勞%') and userid='" + userid + "'";
                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                string counts = ds.Tables[0].Rows[0]["counts"].ToString();
                transaction.Commit(); // 確認交易
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
            return ret; // 傳回值: 無
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
                //產生ContractNO的後5碼
                string sql = "select top 1 ContractNO from [JBADMIN].[dbo].[ERPContract] where ContractNO!='' order by [ContractNO] desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                
                ContractNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["ContractNO"].ToString();
                string suffix = "";
                if (ContractNO != "")//有紀錄
                {
                    if (ContractNO.Substring(0, 4) == DateTime.Today.Year.ToString("0000"))//同年份
                    {
                        suffix = (Convert.ToInt32(ContractNO.Substring(4, 5)) + 1).ToString("00000"); ;//同年同月份才能加1
                    }
                    else//不同年份
                    {
                        suffix = "00001";
                    }
                }
                else//沒紀錄
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
            int flowDirection = (int)objParam[1];//1前進 2退回
            if (flowDirection == 1)
            {
                string js = string.Empty;
                DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string sql = "select top 1 ContractAlterNO  FROM [JBADMIN].[dbo].[ERPContractAlter] order by ContractAlterNO desc";//取最大編號
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ContractAlterNO = ds.Tables[0].Rows[0][0].ToString();
                    string year = ContractAlterNO.Substring(3, 4);
                    if (DateTime.Now.Year.ToString() == year)//同年累加
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
