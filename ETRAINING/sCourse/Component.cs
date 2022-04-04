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
        //�N�ǤJ�����v�r����s�JTable StudentsCourses
        public object[] SetStudentsCourses(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string courseid = parm[0].ToString();
            string teacherids = parm[1].ToString();
            string userid = parm[2].ToString();
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC  dbo.procSetCourseTeacherIDs '" + courseid + "','" + teacherids + "','" + userid + "'";
                this.ExecuteSql(sql, connection, transaction);
                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
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
        //�ˬd�ҵ{�O�_�i�R�� 
        public object[] CheckDelCourseID(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');  
                string CourseID = parm[0].ToString();
                string Dt = parm[1].ToString();
                string sql = "SELECT dbo.funReturnIsDeleteCourseID(CourseID,'" + Dt + "') AS CNT FROM Course WHERE CourseID = '" + CourseID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
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
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
        //�N�[�J��¾��W��g�J�ҵ{¾��JobNeedCourse
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
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procUpdateJobNeedCourseByCourse '" + CourseID + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
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
