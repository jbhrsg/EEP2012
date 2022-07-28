using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

using JBTool;


namespace sJBePortalEmp
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

        private void ucEmpBase_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string EmpNum = ucEmpBase.GetFieldCurrentValue("EmpNum").ToString();
            string DeptCode = ucEmpBase.GetFieldCurrentValue("DeptCode").ToString();
            string inDate = DateTime.Parse(ucEmpBase.GetFieldCurrentValue("inDate").ToString()).ToShortDateString();
            string outDate = DateTime.Parse(ucEmpBase.GetFieldCurrentValue("outDate").ToString()).ToShortDateString();

            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            connetionString = "Data Source=192.168.1.41;Initial Catalog=JBePortal;User ID=jb;Password=8VU4QTby55tbzR4A";

            conn = new SqlConnection(connetionString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                sql = "EXEC procInsertEmpBaseData '" + EmpNum + "','" + DeptCode + "','" + inDate + "','" + outDate + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                conn.Dispose();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
        }

        private void ucEmpBase_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            string EmpNum = ucEmpBase.GetFieldCurrentValue("EmpNum").ToString();
            string DeptCode = ucEmpBase.GetFieldCurrentValue("DeptCode").ToString();
            string inDate = DateTime.Parse(ucEmpBase.GetFieldCurrentValue("inDate").ToString()).ToShortDateString();
            string outDate = DateTime.Parse(ucEmpBase.GetFieldCurrentValue("outDate").ToString()).ToShortDateString();

            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            connetionString = "Data Source=192.168.1.41;Initial Catalog=JBePortal;User ID=jb;Password=8VU4QTby55tbzR4A";

            conn = new SqlConnection(connetionString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                sql = "EXEC procUpdateEmpBaseData '" + EmpNum + "','" + DeptCode + "','" + inDate + "','" + outDate + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                conn.Dispose();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
        }

        public object[] EmpBaseModify(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmpNum = parm[0];
            string DeptCode = parm[1];
            string inDate = DateTime.Parse(parm[2].ToString()).ToShortDateString();
            string outDate = DateTime.Parse(parm[3].ToString()).ToShortDateString();

            string js = string.Empty;

            string sLoginDB = "JBePortal";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " exec procUpdateEmpBaseData '" + EmpNum + "','" + DeptCode + "','" + inDate + "','" + outDate + "'";
                DataSet sCustNO = this.ExecuteSql(sql, connection, transaction);
                // Indented�Y�� �N����ഫ��Json�榡
                js = sCustNO.Tables[0].Rows[0]["Error"].ToString();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {

                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };

        }

        //�ˬd�u���O�_�s�b
        public object[] checkEmpNumCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmpNum = parm[0];           
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
                string sql = " select count(*) as iCount from [192.168.1.41].JBePortal.dbo.EmpBase where EmpNum='" + EmpNum + "'";
                DataSet sCustNO = this.ExecuteSql(sql, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = sCustNO.Tables[0].Rows[0]["iCount"].ToString();
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
        //�ˬd�b���O�_�s�b
        public object[] checkAccountCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string Account = parm[0];
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
                string sql = " select count(*) as iCount2 from [192.168.1.41].JBePortal.dbo.EmpBase where Account='" + Account + "'";
                DataSet sCustNO = this.ExecuteSql(sql, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = sCustNO.Tables[0].Rows[0]["iCount2"].ToString();
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


    }
}
