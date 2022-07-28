using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sFWCRMOrders
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
        //訂單號碼=> ex: FWA1050001----民國年+流水碼(4)
        public string OrderNoFixed()
        {
            int OrderYear = int.Parse(ucFWCRMOrders.GetFieldCurrentValue("OrderYear").ToString());

            DateTime datetime = DateTime.Today;
            return "FWO" + (OrderYear - 1911).ToString().Trim();
        }
        //選擇來源訂單=>帶出訂單資訊
        public object[] BindFWCRMOrders(object[] objParam)
        {
            string OrderNo = (string)objParam[0];
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
                string sql = "select distinct f.OrderNo,d.Item,d.PlanIndate,d.Gender,d.org_okno,d.Notes," + "\r\n";
                   sql = sql + "SUM(d.PersonQtyOriginal) as PersonQtyOriginal,f.NationalityID,f.CreateBy," + "\r\n";
                   sql = sql + "(select Isnull(sum(PersonQty),0) from FWCRMIndateCheck where OrderNo=f.OrderNo) as PersonQtyFinal" + "\r\n";
                   sql = sql + " from FWCRMOrders f inner join FWCRMOrdersDetails d on f.OrderNo=d.OrderNo" + "\r\n";
                   sql = sql + " where f.OrderNo='" + OrderNo + "' " + "\r\n";
                   sql = sql + " group by f.OrderNo,f.NationalityID,f.CreateBy,d.Item,d.PlanIndate,d.Gender,d.org_okno,d.Notes " + "\r\n";
                   //sql = sql + " having SUM(d.PersonQtyOriginal)-SUM(d.PersonQtyFinal)!=0 " + "\r\n";
                   sql = sql + " order by f.OrderNo desc " + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
            return new object[] { 0, js };
        }

        
        //檢查挑工進度維護筆數
        public object[] ReturnStickStatusCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string OrderNo = parm[0];
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
                string sql = " select COUNT(*) as cnt from FWCRMStickStatus where OrderNo='" + OrderNo + "'";

                DataSet dsFWCRMStickStatus = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRMStickStatus.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }    
        private void ucFWCRMIndateCheck_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucFWCRMIndateCheck.SetFieldValue("LastUpdateDate", DateTime.Now);//欄位賦值
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucFWCRMIndateCheck.SetFieldValue("LastUpdateBy", LoginUser);//欄位賦值
        }

        //產生訂單報表
        public object[] ReportOrders(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string OrderNo = parm[0];
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
                string sql = "select m.OrderNo,m.OrderType,m.WorkNo,m.EmployerID,r.EmployerName,m.FromOrderNo, " + "\r\n";
                sql = sql + "m.NationalityID,dbo.funReturnReferenceData('nationality',m.NationalityID) as NationalityText, " + "\r\n";
                sql = sql + "d.Item,d.PlanIndate,d.PersonQtyOriginal,d.sgn_type,d.sgn_no,d.org_okno,d.Notes, " + "\r\n";
                sql = sql + "d.Gender,case d.Gender when 1 then '女' else '男' end as GenderText,WorkTime, " + "\r\n";
                sql = sql + "case WorkTime when 1 then '3年' when 3 then '3年' when 6 then '3年' else WorkTimeReason end as WorkTimeReason,IsOnSite,OnSiteDays," + "\r\n";
                sql = sql + "dbo.funReturnFWCRMHotelFee('" + OrderNo + "',1) as OnSiteDescribe," + "\r\n";
                sql = sql + "dbo.funReturnFWCRMHotelFee('" + OrderNo + "',2) as sTakeType " + "\r\n";
                sql = sql + "from FWCRMOrders m  " + "\r\n";
                sql = sql + "inner join FWCRMOrdersDetails d on m.OrderNo=d.OrderNo " + "\r\n";
                sql = sql + "inner join dbo.View_FWCRMEmployer r on m.EmployerID=r.EmployerID " + "\r\n";
                sql = sql + " where m.OrderNo='" + OrderNo + "'" + "\r\n";
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

        //flow 是否有函號
        public object flowCheckOrg_okno(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string OrderNo = dr["OrderNo"].ToString();

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
                string sql = " select COUNT(*) as cnt from FWCRMOrdersDetails where OrderNo='" + OrderNo + "' and (Isnull(org_okno,'') ='申請中' or Isnull(org_okno,'') ='')";

                DataSet dsFWCRM= this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                if (cnt == "0")
                    ret[1] = true;//繼續流程
                else
                    ret[1] = false;
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

        //flow 檢查仲介
        public object flowChecksup_no(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string OrderNo = dr["OrderNo"].ToString();

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
                string sql = " select COUNT(*) as cnt from FWCRMOrders where OrderNo='" + OrderNo + "' and Isnull(sup_no,'') =''";

                DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                if (cnt == "0")
                    ret[1] = true;//繼續流程
                else
                    ret[1] = false;
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

        //flow 是否更改國外仲介
        public object flowChecksupno(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string OrderNo = dr["OrderNo"].ToString();

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
                string sql = " select COUNT(*) as cnt from FWCRMOrders where OrderNo='" + OrderNo + "' and Isnull(sup_no,'')!=Isnull(sup_noOld,'')";

                DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                if (cnt == "1")
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

        //flow 業務結案確認
        public object flowCheckClose(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string CloseDate = dr["CloseDate"].ToString();

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
                if (CloseDate != "")
                    ret[1] = true;
                else
                    ret[1] = false;
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

        private void ucFWCRMOrders_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            //DateTime datetime = DateTime.Today;
            //ucFWCRMOrders.SetFieldValue("OrderYear", datetime.Year);//欄位賦值

        }

        private void ucFWCRMOrders_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            //修改國外仲介	
            string sup_noOld = ucFWCRMOrders.GetFieldOldValue("sup_no").ToString(); ;//修改前
            string sup_no= ucFWCRMOrders.GetFieldCurrentValue("sup_no").ToString(); ;//修改後
            if (sup_noOld != sup_no)
            {
                //不一樣才要賦值
                ucFWCRMOrders.SetFieldValue("sup_noOld", sup_noOld);//欄位賦值
            }
        }

      




    }
}
