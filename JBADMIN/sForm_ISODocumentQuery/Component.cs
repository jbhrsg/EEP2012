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

        //自動起單(沒用到，因為Form_ISODocumentModify.xoml的page用Form_ISODocument.aspx)
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            //string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string DocNO = "";

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

            //2.抓DocNO
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
            if (RoleID != "" && DocNO != "")//有RoleID，有DocNO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_ISODocumentModify.xoml",
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\Form_ISODocumentModify.xoml",
                    string.Empty,////空白即可，系統使用
                    0,//是否為重要申請
                    0,//是否為緊急申請
                    "",//提交意見說明
                    RoleID,//申請者的RoleID(角色編號)
                    "sForm_ISODocument.ISODocument",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                    0,//系統使用
                    "0",//組織類別編號ex:0公司組織、1福利委員會
                    "" //附件
                },
                new object[]{
                    "DocNO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "DocNO='"+ DocNO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
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
        //(沒用到，因為自動起單沒用)
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

        //回傳此文件是否在流程中
        public object[] IsProcess(object[] objParam)
        {
            string[] aParam = objParam[0].ToString().Split(',');
            string DocPaperNO = aParam[0].ToString();
            //string js = string.Empty;
            string IsProcessing = "0";
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

        //取得組織編號
        public object[] GetUserOrgNOs(object[] objParam)
        {
            string js = string.Empty;
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOAllandL2('" + UserID + "') AS OrgNOs FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                //funReturnEmpOrgNOAll--傳回使用者所有的部門代號eg:10760,10700,
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

        //使用者ID轉名稱
        public object[] ReturnUserNames(object[] objParam)
        {
            string UserIDs = objParam[0].ToString();
            //string js = string.Empty;
            string UserNames = "";
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

        //部門組ID轉名稱
        public object[] ReturnOrgNames(object[] objParam)
        {
            string OrgIDs = objParam[0].ToString();
            //string js = string.Empty;
            string OrgNames = "";
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
