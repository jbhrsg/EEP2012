using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using System.Data.SqlClient; 
using Newtonsoft;
using Newtonsoft.Json;
using System.Configuration;

namespace sERPCustomer
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
        private void ucERPPayKind_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPPayKind.SetFieldValue("CreateBy", ucERPPayKind.GetFieldCurrentValue("CreateBy").ToString());
            ucERPPayKind.SetFieldValue("CreateDate", DateTime.Now);
        }
        private void ucERPPayKind_BeforeUpdate(object sender, UpdateComponentBeforeInsertEventArgs e)
        {

            ucERPPayKind.SetFieldValue("UpateDate", DateTime.Now);
        }
        public object[] CheckCustNO(object[] objParam)
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
                string CustNO = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM ERPCustomers WHERE CustNO = '" + CustNO + "'";
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
        //
        public object[] CheckDelCustNO(object[] objParam)
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
                string CustNO = objParam[0].ToString();
                string sql = "SELECT dbo.funReturnIsDeleteCust(CustNO) AS CNT FROM ERPCustomers WHERE CustNO = '" + CustNO + "'";
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
        public object[] CheckPayKind(object[] objParam)
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
                string CustNO = parm[0];
                string SalesTypeID = parm[1];
                string PayTypeID = parm[2];
                string sql = "SELECT COUNT(*) AS CNT FROM ERPPayKind WHERE CustNO = '" + CustNO + "' AND SalesTypeID='" + SalesTypeID + "' AND PayTypeID='" + PayTypeID + "'";
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
        //
        public object[]  procSyncContact(object[] objParam)
        {
            string [] parm = objParam[0].ToString().Split(',');
            string ConType = parm[0].ToString(); 
            string Conb = parm[1].ToString();
            string CustNO = parm[2].ToString();
            string Center_ID = parm[3].ToString();
            string UserID = parm[4].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procSyncContact '" + ConType + "','" + Conb + "','" + CustNO + "','" + Center_ID + "','" + UserID + "'"; 
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
            return new object[] { 0, js };
        }

        private void ucCustNotes_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCustNotes.SetFieldValue("CreateDate", DateTime.Now);
        }
        //�s�W�Ȥ�Ƶ�
        public object[] procAddERPCustomerToDoNotes(object[] objParam)
        {
            string sql = null;
            string[] parm = objParam[0].ToString().Split('*');
            string CustNO = parm[0].ToString();
            string NextCallDate = parm[1].ToString();
            string NextCallTime = parm[2].ToString();
            string UserName = parm[3].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string js = string.Empty;

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC procInsertERPCustomerToDoNotes '" + CustNO + "','" + NextCallDate + "','" + NextCallTime + "','" + UserName + "'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };

        }

        private void ucERPCustomers_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {

        }

        
        private void ucERPPayKind_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            
        }
        //�ܾP�f�Ȥ�s�W���ڤ覡
        public object[] procAddJBERPCustomerSaleType(object[] objParam)
        {
            string sql = null;
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0].ToString();
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            string js = string.Empty;

            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBERP";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC procInsertCustomerSaleType '" + CustNO + "','" + username + "'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch 
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Commit();
                //ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);

            }
            return new object[] { 0, js };

        }
        //�ܾP�f�Ȥ�ק怜�ڤ覡
        public object[] procUpdateJBERPCustomerSaleType(object[] objParam)
        {
            string sql = null;
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0].ToString();
            string PayKindNO = parm[1].ToString();
            string js = string.Empty;

            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBERP";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = "EXEC procUpdateCustomerSaleType '" + CustNO + "'," + PayKindNO;
                this.ExecuteSql(sql, connection, transaction);
            }
            catch 
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Commit();
                //ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };

        }

        ///�ܾP�f�Ȥ�R�����ڤ覡
        public void procDeleteJBERPCustomerSaleType(object[] objParam)
        {
            string sql = null;
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0].ToString();
            string SalesTypeID = parm[1].ToString();

            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBERP";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = "EXEC procDeleteCustomerSaleType '" + CustNO + "','" + SalesTypeID+"'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch 
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Commit();
                //ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);
            }
            
        }

        private void ucERPPayKind_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBERP";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            string CustNO = ucERPPayKind.GetFieldOldValue("CustNO").ToString();
            string SalesTypeID = ucERPPayKind.GetFieldOldValue("SalesTypeID").ToString();

            //------------------------------------------------------------------------------------------------------------------

            try
            {
                string SQL = "EXEC procDeleteCustomerSaleType '" + CustNO + "','" + SalesTypeID + "'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                //ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);
            }

        }




     }
}
