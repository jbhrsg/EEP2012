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

namespace sCourseOpen
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
        public object[] GetYearAndPlan(object[] objParam)
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
                string para = parm[0];
                string sql = "SELECT  TOP 1 Year,PlanID FROM CoursePlanRecord ORDER BY YEAR DESC";
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
        //�p�e�ҵ{�}��
        public object[] procCoursePlanOpen(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string Year = parm[0].ToString();
            string PlanID = parm[1].ToString();
            string CourseID = parm[2].ToString();
            string SpreadType = parm[3].ToString();
            string UserID = parm[4].ToString();
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
                string sql = "EXEC dbo.procCoursePlanOpen '" + Year + "','" + PlanID + "','" + CourseID + "','" + SpreadType + "','" + UserID + "'";
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
        //�}�ҽҵ{�[�}
        public object[] procCoursePlanOpenAdd(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();
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
                string sql = "EXEC dbo.procCoursePlanOpenAdd '" + CourseOpenID + "','" + UserID + "'";
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
        //�T�{�}��
        public object[] procCoursePlanOpenBookBill(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();
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
                string sql = "EXEC dbo.procCoursePlanOpenBookBill '" + CourseOpenID + "','" + UserID + "'";
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
        //�ҵ{����
        public object[] procCoursePlanOpenEnd(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();
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
                string sql = "EXEC dbo.procCoursePlanOpenEnd'" + CourseOpenID + "','" + UserID + "'";
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
        //�N�[�J���ǭ��W��g�J�ҵ{�W�ҦW��CourseBookBill
        public object[] procAddCourseOpenBookBill(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CourseOpenID = parm[0].ToString();
            string StudentIDs = parm[1].ToString();
            string UserID = parm[2].ToString();
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
                string sql = "EXEC procAddCourseOpenBookBill '" + CourseOpenID + "','"+StudentIDs + "','"+ UserID + "'";
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
        //ñ��ï(Master)
        public object[] ReportCourseOpenRecord(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();           
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
                string SQL = "SELECT CourseOpenID,CourseName,CourseID,Convert(nvarchar(10),CourseStartDate,111) As CourseStartDate,"+
                "Convert(nvarchar(10),CourseEndDate,111) As CourseEndDate,CourseHours," +
                "(SELECT CourseOutline  FROM COURSE Where CourseID=CourseOpenRecord.CourseID) AS CourseOutLine,"+
                "(SELECT StudentName From Student where StudentID=CourseOpenRecord.CreateBy) As OpenUser,"+
                "dbo.funReturnListData('CourseOpenType', CourseOpenRecord.CourseOpenType) as TextCourseOpenType,"+
                "dbo.funReturnListData('CourseOpenStatus',CourseOpenRecord.CourseOpenStatus) as TextCourseOpenStatus,"+
                "(SELECT NAME FROM ListCourseType Where ID=CourseOpenRecord.CourseType) AS CourseType,"+
                "(SELECT NAME FROM CourseLocation Where ID=CourseOpenRecord.CourseLocationID) As CourseLocation,"+
                "dbo.funReturnTeacherNames(" + CourseOpenID + ") As TeacherName1," +
                "(SELECT TEACHERNAME FROM Teacher Where TeacherID=CourseOpenRecord.TeacherID1) As TeacherName2,"+
                "(SELECT NAME FROM ListTestMethod Where ID=CourseOpenRecord.CourseTestMethod)  As TestMethod "+
                "FROM CourseOpenRecord where CourseOpenID=" + CourseOpenID;
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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
        //ñ��ï(Detail)
        public object[] ReportCourseOpenBookBill(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();
            string Group = parm[1].ToString();//0��������,1������(���ɪť�),2�ť�ñ���           
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
                string SQL = "exec ReportCourseOpenBookBill " + CourseOpenID + "," + Group;
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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

        public object[] GetPlanOpenCourseStudents(object[] objParam)
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
                string PlanID = parm[0];
                string CourseID = parm[1];
                string UserID = parm[2];
                string sql = "SELECT dbo.funReturnPlanOpenCourseStudents('" + PlanID + "','"+CourseID+"') AS ReturnStr FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
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
        //���o�}�Ҵ����
        public object[] GetOpenCourseExamID(object[] objParam)
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
                string CourseOpenID = parm[0];
                string TestMethod = parm[1];
                string sql = "EXEC dbo.procDisplayExamPaperforCourseUpdate '" + CourseOpenID + "','" + TestMethod + "'";
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
        //���o�}�ұЧ�
        public object[] GetOpenCourseDataID(object[] objParam)
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
                string Type = parm[0];
                string CourseOpenID = parm[1];
                string sql = "EXEC dbo.procDisplayCourseDataforOpenCourse '" + Type + "','" + CourseOpenID + "'";
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
        //�}�ҽҵ{�}�Ҫ��A�]������
        public object[] SetOpenCourseExam(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();
            string CourseOpenStatus = parm[1].ToString();
            string IsSendEmail = parm[2].ToString();
            string UserID = parm[3].ToString();
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
                string sql = "EXEC dbo.procUpdateCourseOpenRecordStatus '" + CourseOpenID + "','" + CourseOpenStatus + "','" + IsSendEmail + "','" + UserID + "'";
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
        //�R���}�Ҭ����W�ҤH���W��
        public object[] DeleteCourseBookBill(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();
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
                string sql = "Exec procDeleteCourseOpen '" + CourseOpenID+"'" ;
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
        //�]�G����
        public object[] PlanCourseOpenStop(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();
            string UserID = parm[1].ToString();
            string IsSendMail = parm[2].ToString();
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
                string sql = "Exec procSetPlanCourseOpenStop '" + CourseOpenID + "','" + UserID + "'," + IsSendMail;
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
        //�ҵ{����
        public object[] procCourseOpenClose(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CourseOpenID = parm[0].ToString();
            string CourseOpenStatus = parm[1].ToString();
            string IsSendMail = parm[2].ToString();
            string UserID = parm[3].ToString();
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
                string sql = "Exec procUpdateCourseOpenRecordStatus '" + CourseOpenID + "','" + CourseOpenStatus + "','" + IsSendMail + "','" + UserID + "'";
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
        //�ˬd�O�_�i�ҵ{�}�ҩΥ[�}�ҵ{
        public object[] CheckIsOpenCourse(object[] objParam)
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
                string PlanID = parm[1].ToString();
                string sql = "SELECT dbo.funReturnIsOpenCourse(CourseID,'" + PlanID + "') AS CNT FROM Course WHERE CourseID = '" + CourseID + "'";
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

    }
   
}
