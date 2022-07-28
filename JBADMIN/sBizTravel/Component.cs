//using sRequisition;
using sBizTravel;
using Srvtools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace sBizTravel
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
        public string GetFixed(){
            DateTime today1 = DateTime.Today;
            return "TRV" + ((today1.Year).ToString()).Trim();
        }
        //自動起單
        public object[] MakeRequisition(object[] objParam)
        {
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
                    "1030051",//申請者的RoleID(角色編號)
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

        public object[] SelectRequisition(object[] objParam)
        {
            string js = string.Empty;
            //string js2 = string.Empty;
            string str = objParam[0].ToString();
            IDbConnection connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try{
                string sql = "SELECT SUM([RequisitAmt]) AS SumR,COUNT([RequisitAmt]) AS CountR FROM [JBADMIN].[dbo].[Requisition] WHERE [SourceBillNO] ='" + str + "' and (Flowflag IS NOT NULL and Flowflag !='X')";// 
            DataSet ds = this.ExecuteSql(sql, connection, transaction);
            js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            //js1 = ds.Tables[0].Rows[0]["SumR"].ToString();
            //js2 = ds.Tables[0].Rows[0]["CountR"].ToString();
            }
            catch{
                transaction.Rollback();
            }
            finally{
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js};
        }

        public object[] UpdateBizTravel(object[] objParam) {
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
                string sql = "SELECT sum(AirfareVisafee) as TotalAFVF FROM JBADMIN.DBO.BizTravelDetails_Accom WHERE TvlNo='" + str + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                string js = ds.Tables[0].Rows[0]["TotalAFVF"].ToString();
                string sql1 = "UPDATE BizTravelMaster SET TotalAFVF=" + js + " WHERE TvlNo='" + str + "'";
                this.ExecuteSql(sql1, connection, transaction);
                transaction.Commit();
                ret[1] = true;
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

        public object[] IsSignWithNotes(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string TvlNo = dr["TvlNo"].ToString();
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
                string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='TvlNo=''" + TvlNo + "'''";
                DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                if (cnt == "0")
                    ret[1] = false;//繼續流程
                else
                    ret[1] = true;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // 傳回值: 無
        }

        public object[] GetSignCount(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string TvlNo = parm[0];
                string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='TvlNo=''" + TvlNo + "'''";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["CNT"].ToString();
                if (cnt == "0")
                    ret[1] = 0;
                else
                    ret[1] = 1;
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

        public object[] GetSignNotesData(object[] objParam)
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
                string TvlNo = parm[0];
                string sql = "SELECT S_STEP_ID,USERNAME,REMARK,UPDATEDATE FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='TvlNo=''" + TvlNo + "''' ORDER BY UPDATEDATE ASC";
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
    }
}
