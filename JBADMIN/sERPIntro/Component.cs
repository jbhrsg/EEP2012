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
using System.Linq;

namespace sERPIntro
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
        public string GetFixed_IntroNO(){
            return "T"+DateTime.Today.Year.ToString();
        }
        //取承接部門主管工號
        public object[] SelectDepartManager(object[] objParam) {
            object[] ret = new object[] { 0, 0 };
            string Depart = objParam[0].ToString();
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
                string sql = "SELECT Top 1 USERID FROM [EIPHRSYS].[dbo].[SYS_ORG] o inner JOIN [EIPHRSYS].[dbo].[USERGROUPS] ug ON ug.GROUPID=o.ORG_MAN where ORG_DESC='" + Depart + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);//datatable轉成string
                    ret[1] = js;
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
            return ret;
        }
        //取轉介部門主管工號
        public object[] SelectIntroManager(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string IntroMan = objParam[0].ToString();
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
                string sql = "select top 1 UserManager from(" + "\r\n";
                sql = sql + "SELECT distinct u.USERID,USERNAME,ug1.USERID as UserManager FROM[EIPHRSYS].dbo.[USERS] u" + "\r\n";
                sql = sql + "left join [EIPHRSYS].[dbo].[USERGROUPS] ug on ug.USERID=u.USERID" + "\r\n";
                sql = sql + "left join [EIPHRSYS].[dbo].[SYS_ORGROLES] ro on ro.ROLE_ID=ug.GROUPID" + "\r\n";
                sql = sql + "left join [EIPHRSYS].[dbo].[SYS_ORG] o on o.ORG_NO=ro.ORG_NO" + "\r\n";
                sql = sql + "left join [EIPHRSYS].[dbo].[USERGROUPS] ug1 on ug1.GROUPID=o.ORG_MAN" + "\r\n";
                sql = sql + "where o.ORG_DESC is not null" + "\r\n";
                sql = sql + "union all" + "\r\n";
                sql = sql + "select distinct ug.USERID,USERNAME,u.USERID as UserManager from [EIPHRSYS].[dbo].[SYS_ORG] o" + "\r\n";
                sql = sql + "inner join [EIPHRSYS].[dbo].[USERGROUPS] ug on ug.GROUPID=o.ORG_MAN" + "\r\n";
                sql = sql + "inner join [EIPHRSYS].dbo.[USERS] u on ug.USERID=u.USERID" + "\r\n";
                sql = sql + "where ug.USERID not in(SELECT distinct u.USERID FROM[EIPHRSYS].dbo.[USERS] u" + "\r\n";
                sql = sql + "left join [EIPHRSYS].[dbo].[USERGROUPS] ug on ug.USERID=u.USERID" + "\r\n";
                sql = sql + "left join [EIPHRSYS].[dbo].[SYS_ORGROLES] ro on ro.ROLE_ID=ug.GROUPID" + "\r\n";
                sql = sql + "left join [EIPHRSYS].[dbo].[SYS_ORG] o on o.ORG_NO=ro.ORG_NO" + "\r\n";
                sql = sql + "where o.ORG_DESC is not null)" + "\r\n";
                sql = sql + ")  tb1 where USERID='"+ IntroMan+"'"+ "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);//datatable轉成string
                    ret[1] = js;
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
            return ret;
        }
        //自動起單
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            //string MasterNO = aParam[0].ToString();
            //string sAutoKey = aParam[1].ToString();
            string userid = aParam[0].ToString();
            string ParentIntroNO = aParam[1].ToString();
            string RoleID = "";
            string IntroNO = "";

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

            //2.抓IntroNO
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
                    string sql = "SELECT Top 1 IntroNO FROM [JBADMIN].[dbo].[ERPIntroMaster] ORDER BY IntroNO DESC ";// 
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    IntroNO = ds.Tables[0].Rows[0]["IntroNO"].ToString();
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
            if (RoleID != "" && IntroNO != "")//有RoleID，有IntroNO
            {
                try
                {
                    //for (int i = 0; i < aAutoKey.Length; i++)
                    //{
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    "C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Intro1.xoml",
                    string.Empty,////空白即可，系統使用
                    0,//是否為重要申請
                    0,//是否為緊急申請
                    "",//提交意見說明
                    RoleID,//申請者的RoleID(角色編號)
                    "sERPIntro.ERPIntroMaster",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                    0,//系統使用
                    "0",//組織類別編號ex:0公司組織、1福利委員會
                    "" //附件
                },
                new object[]{
                    "IntroNO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "IntroNO ='"+ IntroNO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
                }
                    });
                    UpdateParentContract(ParentIntroNO);
                    ret[1] = true;
                }
                catch
                {
                    if (IntroNO != "") DeleteNewestContract(IntroNO);
                    ret[1] = false;
                    return ret;
                }
            }
            else
            {
                if (IntroNO != "") DeleteNewestContract(IntroNO);
                ret[1] = false;
            }
            return ret;
        }

        public void DeleteNewestContract(string IntroNO)
        {
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Delete From [JBADMIN].[dbo].[ERPIntroMaster] where IntroNO='" + IntroNO+"'";
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
        public void UpdateParentContract(string ParentIntroNO)
        {
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "UPDATE [JBADMIN].[dbo].[ERPIntroMaster] SET ContinueFlag=1 where IntroNO='" + ParentIntroNO+"'";
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
        public object[] FlowReject(object[] objParam)
        {//.itemArray[1];[1][1]
            //object ParentKey = objParam[0];
            //string[] ar1 =ParentKey.itemArray[1];
            DataRow dr = (DataRow)objParam[0];
            string ParentIntroNO = dr["ParentIntroNO"].ToString();
            object[] ret = new object[] { 0, 0 };
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //string sql = "SELECT Top 1 ContractKey FROM [JBADMIN].[dbo].[ERPContract] ORDER BY ContractKey DESC ";// 
                //DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //ContractKey = ds.Tables[0].Rows[0][0].ToString();
                string sql = "UPDATE [JBADMIN].[dbo].[ERPIntroMaster] SET ContinueFlag=null where IntroNO='" + ParentIntroNO + "'";
                int EffectRow = this.ExecuteCommand(sql, connection, transaction);
                transaction.Commit(); // 確認交易
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
        //產生報表資訊
        public object[] ReportOrders(object[] objParam)
        {
            string IntroNO = objParam[0].ToString();
            //string IsRecontract = objParam[1].ToString();
            //string ContinueEmployNO = parm;
            //string IsRecontract;
            //if (parm1 == "1") { IsRecontract = "是"; } else if (parm1 == "0") { IsRecontract = "否"; } else { IsRecontract = ""; }
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string userName = SrvGL.GetUserName(userid);
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
                //string sql = "select m.OrderNo,m.OrderType,m.WorkNo,m.EmployerID,r.EmployerName,m.FromOrderNo, " + "\r\n";
                //sql = sql + "m.NationalityID,dbo.funReturnReferenceData('nationality',m.NationalityID) as NationalityText, " + "\r\n";
                //sql = sql + "d.Item,d.PlanIndate,d.PersonQtyOriginal,d.sgn_type,d.sgn_no,d.org_okno,d.Notes, " + "\r\n";
                //sql = sql + "d.Gender,case d.Gender when 1 then '女' else '男' end as GenderText,WorkTime,WorkTimeReason " + "\r\n";
                //sql = sql + "from FWCRMOrders m  " + "\r\n";
                //sql = sql + "inner join FWCRMOrdersDetails d on m.OrderNo=d.OrderNo " + "\r\n";
                //sql = sql + "inner join [60.250.52.107,3225].FWCRM.dbo.Employer r on m.EmployerID=r.EmployerID " + "\r\n";
                //sql = sql + " where m.OrderNo='" + OrderNo + "'" + "\r\n";

                //string sql = "SELECT m.ContinueEmployNO,c.title,CreateBy,CreateDate,d.AutoKey,l.lab_cname+''+l.lab_name as labname," + "\r\n";
                //sql = sql + "Gender,Country,ImmigrationDate,DueDate,CEConfirmNO" + "\r\n";
                //sql = sql + "FROM dbo.[ERPContinueEmployMaster] m" + "\r\n";
                //sql = sql + "inner join dbo.[ERPContinueEmployDetail]d on m.ContinueEmployNO = d.ContinueEmployNO" + "\r\n";
                //sql = sql + "inner join [192.168.1.40,1433].lab.dbo.cus c on m.Employer = c.[cus_no]" + "\r\n";
                //sql = sql + "inner join [192.168.1.40,1433].lab.dbo.lab l on d.LaborName = l.[lab_no]" + "\r\n";

                //string sql = "SELECT m.ContinueEmployNO,Employer,CreateBy,CreateDate,d.AutoKey,LaborName,Gender,Country,ImmigrationDate,DueDate,CEConfirmNO,d.IsRecontract,d.Transfer,d.ReturnHome,d.BackPot,d.LetterClass,d.LetterNO,d.TransferAg FROM dbo.[ERPContinueEmployMaster] m inner join dbo.[ERPContinueEmployDetail]d on m.ContinueEmployNO = d.ContinueEmployNO" + "\r\n";
                string sql = "SELECT  m.[IntroNO],[ParentIntroNO],[Depart],(select USERNAME from [EIPHRSYS].[dbo].[USERS] where USERID=m.DepartManager) as DepartManager,(select USERNAME from [EIPHRSYS].[dbo].[USERS] where USERID=m.IntroMan) as IntroMan,(select USERNAME from [EIPHRSYS].[dbo].[USERS] where USERID=m.IntroManager) as IntroManager,(select USERNAME from [EIPHRSYS].[dbo].[USERS] where USERID=m.UnderTaker) as UnderTaker,[CustomerDescr],[Deal],[DealDescr],[BonusAmount],[BonusReleaseDate],[BonusDescr],[FlowFlag],(select USERNAME from [EIPHRSYS].[dbo].[USERS] where USERID=m.CreateBy) as CreateBy,[CreateDate],[ContinueFlag],[AutoKey],[TrackNote] from [JBADMIN].[dbo].[ERPIntroMaster] m left join [JBADMIN].[dbo].[ERPIntroDetail] d on m.IntroNO=d.IntroNO " + "\r\n";
                sql = sql + " where m.IntroNO='" + IntroNO + "' order by d.AutoKey asc" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
    }
}
