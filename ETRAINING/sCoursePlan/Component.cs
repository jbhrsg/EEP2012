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

namespace sCoursePlan
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

        private void ucCoursePlanRecord_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCoursePlanRecord.SetFieldValue("CreateDate", DateTime.Now);
            ucCoursePlanRecord.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucCoursePlanRecord_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCoursePlanRecord.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //展開計畫
        public object[] procSetUpCoursePlan(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string year= parm[0].ToString();
            string planid = parm[1].ToString();
            string spreadtype = parm[2].ToString();
            string userid = parm[3].ToString();
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
                string sql = "EXEC dbo.procSetUpCoursePlan '" + planid + "','" + year + "','" + spreadtype + "','"+userid+"'";
                this.ExecuteSql(sql, connection, transaction);
                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented縮排 將資料轉換成Json格式
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
            return new object[] { 0, true };
        }
        //刪除計畫
        public object[] procDeleteCoursePlan(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string PlanID = parm[0].ToString();
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
                string sql = "EXEC dbo.procDeleteCoursePlan " + PlanID ;
                this.ExecuteSql(sql, connection, transaction);
                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented縮排 將資料轉換成Json格式
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
            return new object[] { 0, true };
        }
        //檢查課程計畫是否可刪除 
        public object[] CheckDelCoursePlan(object[] objParam)
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
                string PlanID = objParam[0].ToString();
                string sql = "SELECT dbo.funReturnNumCoursePlanOpen(PlanID) AS CNT FROM CoursePlanRecord WHERE PlanID = '" + PlanID + "'";
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

        public object[] GetCoursePlanStudentList(object[] objParam)
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
                string CourseID = parm[0];
                string StartDate = parm[1];
                string EndDate = parm[2];
                string UserID =  parm[3];
                string sql = "EXEC dbo.procDisplayCoursePlanStudentList '" + CourseID + "','" + StartDate + "','" + EndDate + "','"+UserID+"'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
            return new object[] { 0, true }; ;
        }
        public object[] DeleteCoursePlanStudentList(object[] objParam)
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
                string sql = "Delete From CoursePlanStudents Where UserID='" +  UserID +"'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
            return new object[] { 0, true }; ;
        }
        //取得課程計畫課程資料,傳入計畫代號
        public object[] GetCourseListCoursePlan(object[] objParam)
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
                string PlanID = parm[0];
                string Type = parm[1];
                string sql = "EXEC dbo.procDisplayCourseListCoursePlanByType '" + PlanID + "','" + Type + "'";
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
        private void ucCoursePlanDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCoursePlanDetails.SetFieldValue("CreateDate", DateTime.Now);
            ucCoursePlanDetails.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucCoursePlanDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCoursePlanDetails.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
    }
}
