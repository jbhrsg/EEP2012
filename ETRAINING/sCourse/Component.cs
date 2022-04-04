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

namespace sCourse
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

        private void ucCourse_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCourse.SetFieldValue("CreateDate", DateTime.Now);
            ucCourse.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucCourse_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCourse.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //將傳入的講師字串轉存入Table StudentsCourses
        public object[] SetStudentsCourses(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string courseid = parm[0].ToString();
            string teacherids = parm[1].ToString();
            string userid = parm[2].ToString();
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
                string sql = "EXEC  dbo.procSetCourseTeacherIDs '" + courseid + "','" + teacherids + "','" + userid + "'";
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
        //檢查課程是否可刪除 
        public object[] CheckDelCourseID(object[] objParam)
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
                string CourseID = parm[0].ToString();
                string Dt = parm[1].ToString();
                string sql = "SELECT dbo.funReturnIsDeleteCourseID(CourseID,'" + Dt + "') AS CNT FROM Course WHERE CourseID = '" + CourseID + "'";
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

        public object[] GetCourseJobIDStr(object[] objParam)
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
                string sql = "SELECT dbo.funReturnCourseJobIDs('" + CourseID + "') AS ReturnStr FROM Course WHERE CourseID='" + CourseID + "'";
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

        public object[] GetCoursePath(object[] objParam)
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
                string sql = "SELECT dbo.funReturnCourseNameWithLevel('" + CourseID + "') AS ReturnStr FROM Course WHERE CourseID='" + CourseID + "'";
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
        //將加入的職能名單寫入課程職能JobNeedCourse
        public object[] procAddJobNeedCourseByJob(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CourseID = parm[0].ToString();
            string CourseFrequency = parm[1].ToString();
            string CourseFinishDays1 = parm[2].ToString();
            string CourseFinishDays2 = parm[3].ToString();
            string StartDate = parm[4].ToString();
            string EndDate = parm[5].ToString();
            string JobIDs = parm[6].ToString();
            string UserID = parm[7].ToString();
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
                string sql = "EXEC procAddJobNeedCourseByJob '" + CourseID + "','" + CourseFrequency + "','" + CourseFinishDays1 + "','" + CourseFinishDays2 + "','" + StartDate + "','" + EndDate + "','" + JobIDs + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
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
        public object[] procUpdateJobNeedCourse(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseID = parm[0].ToString();
            string UserID = parm[1].ToString();
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
                string sql = "EXEC procUpdateJobNeedCourseByCourse '" + CourseID + "','" + UserID + "'";
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

        private void ucJobNeedCourse_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucJobNeedCourse.SetFieldValue("CreateDate", DateTime.Now);
            ucJobNeedCourse.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
    }
}
