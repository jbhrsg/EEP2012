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

namespace sTeacher
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
        //
        public object[] SetTeacherCanCourseIDs(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string teacherid = parm[0].ToString();
            string courseids = parm[1].ToString();
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
                string sql = "EXEC  dbo.procSetTeacherCanCourseIDs '" + teacherid + "','" + courseids + "','" + userid + "'";
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
        public object[] SetTeacherGroupIDs(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string teacherid = parm[0].ToString();
            string groupids = parm[1].ToString();
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
                string sql = "EXEC  dbo.procSetTeacherGroupIDs '" + teacherid + "','" + groupids + "','" + userid + "'";
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
        public object[] GetTeacherID(object[] objParam)
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
                string TeacherGroupID = parm[0];
                string UserID = parm[1];
                string sql = "SELECT dbo.funReturnTeacherID('" + TeacherGroupID + "') AS ReturnStr FROM STUDENT WHERE STUDENTID='" + UserID + "'";
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
        //���o���v�̷s�i�½ҵ{�N���P�W��
        public object[] GetCourseIDsNames(object[] objParam)
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
                string TeacherID = parm[0];
                string sql = "SELECT CanTeachCourseIDs,IsNull(dbo.funReturnCourseIDsName('" + TeacherID + "'),'') as CourseIDsName FROM Teacher WHERE TeacherID='" + TeacherID + "'";
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
        private void ucTeacher_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucTeacher.SetFieldValue("CreateDate", DateTime.Now);
            ucTeacher.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucTeacher_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucTeacher.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
    }
}
