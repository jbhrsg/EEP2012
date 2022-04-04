using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using System.Data.SqlClient; 
using Newtonsoft.Json;
namespace sERP_Customer_Normal_Customer
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
        //���o�ϥΪ̹w�]�Ȥ��ݩʥN��
        public object[] GetUserCustomerTypeID(object[] objParam)
        {
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
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
                string UserID = parm[0];
                string sql = "SELECT ISNULL((SELECT CustomerTypeID FROM SalesPerson Where SalesID = '" + UserID + "'),0) AS CustomerTypeID ";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                js = dsTemp.Tables[0].Rows[0]["CustomerTypeID"].ToString();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                //ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };

        }
        public object[] GetCustomerID(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            //string[] aParam = objParam[0].ToString().Split(',');
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //����CustomerID����5�X
                string sql = "select top 1 CustomerID from [dbo].[Customer] where LEFT(CustomerID,1)='C' order by CustomerID desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                string CustomerID = string.Empty;
                CustomerID = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["CustomerID"].ToString();
                if (CustomerID != "")//��
                {
                    int num = Convert.ToInt32(CustomerID.Substring(1, 5));
                    CustomerID = "C"+((num + 1).ToString("00000")); ;
                }
                else//�S��
                {
                    CustomerID = "C00001";
                }
                js = Newtonsoft.Json.JsonConvert.SerializeObject(CustomerID, Newtonsoft.Json.Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
        //���o�Ȥ�֦��P�f���O
        public object[] GetCustSalesTypeID(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT SalesTypeID FROM CustomerSaleType Where CustomerID  ='" + aParam[0] + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                List<string> SalesTypeID = new List<string>();
                string SalesTypeIDs = string.Empty;
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SalesTypeID.Add("'" + ds.Tables[0].Rows[i]["SalesTypeID"].ToString() + "'");
                    }
                    SalesTypeIDs = String.Join(",", SalesTypeID);
                }
                transaction.Commit();
                ret[1] = SalesTypeIDs;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return ret;
        }
        //���o�~�ȭ����P�f���O
        public object[] GetSalesTypeID(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //����CustomerID����5�X
                string sql = "SELECT SalesTypeID FROM SalesSalesType where SalesID='"+aParam[0]+"'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                List<string> SalesTypeID=new List<string>();
                string SalesTypeIDs=string.Empty;
                if (ds.Tables[0].Rows.Count != 0) {
                    for(var i=0;i<ds.Tables[0].Rows.Count;i++){
                        SalesTypeID.Add("'"+ds.Tables[0].Rows[i]["SalesTypeID"].ToString()+"'");
                    }
                    SalesTypeIDs = String.Join(",", SalesTypeID);
                }
                transaction.Commit();
                ret[1] = SalesTypeIDs;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return ret;
        }
        //���o�~�ȭ����P�f���O
        public object[] GetSalesKindID(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //����CustomerID����5�X
                string sql = "SELECT Distinct SalesKindID FROM SalesKindSalesType where SalesTypeID IN (Select SalesTypeID FROM SalesSalesType WHERE SALESID='" + aParam[0] + "')";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                List<string> SalesKindID = new List<string>();
                string SalesKindIDs = string.Empty;
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SalesKindID.Add(ds.Tables[0].Rows[i]["SalesKindID"].ToString());
                        //SalesKindID.Add("'" + ds.Tables[0].Rows[i]["SalesKindID"].ToString() + "'");
                    }
                    SalesKindIDs = String.Join(",", SalesKindID);
                }
                transaction.Commit();
                ret[1] = SalesKindIDs;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return ret;
        }
        //�P�_�νs�O�_����
        public object[] CheckDuplicate_TaxNO(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string TaxNO = aParam[0];
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  CustomerID FROM Customer where TaxNO='"+TaxNO+"'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
        //�P�_�Ȥ�W�١B�q�ܬO�_����
        public object[] CheckDuplicate_CustomerNameAndTelNO(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string CustomerName = aParam[0];
            string TelNO = aParam[1];
            string CustomerNameShort= string.Empty;//���u�W
            if (CustomerName.Length < 4){
                CustomerNameShort = CustomerName;
            }
            else { CustomerNameShort = CustomerName.Substring(0, 4); }
            
            string TelNOShort = string.Empty;//���h���ϽX���q��
            if (TelNO.IndexOf('-', 0, 4) != -1) {
                TelNOShort = TelNO.Substring(TelNO.IndexOf('-', 0, 4) + 1, TelNO.Length - (TelNO.IndexOf('-', 0, 4) + 1));
            }
            else if (TelNO.IndexOf(')', 0, 4) != -1){
                TelNOShort = TelNO.Substring(TelNO.IndexOf(')', 0, 4) + 1, TelNO.Length - (TelNO.IndexOf(')', 0, 4) + 1));
            }
            else {TelNOShort = TelNO;}


            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  CustomerID FROM [Customer] " + "\r\n";
                sql = sql + "where CustomerName like '%" + CustomerNameShort + "%' and TelNO like '%" + TelNOShort + "%' " + "\r\n";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
        private void ucCustomer_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            //���� �Ȥ�
            string CustomerID = ucCustomer.GetFieldOldValue("CustomerID").ToString();
            //�R���Ȥ�P�f���O
            if(CustomerID !=""){
                string sql = "delete CustomerSaleType where CustomerID ='" + CustomerID + "'";
                this.ExecuteCommand(sql, ucCustomer.conn, ucCustomer.trans);
            }
        }
        private void ucCustomer_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
             ucCustomer.SetFieldValue("CreateDate", DateTime.Now);
             ucCustomer.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //�ˬd�ӫȤᦳ�L�s�b��P�f�D��
        public object[] SelectSalesMasterWhereCustomerID(object[] objParam)
        {
            string[] aParam = objParam[0].ToString().Split(',');
            string CustomerID = aParam[0];
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  CustomerID FROM SalesMaster where CustomerID='" + CustomerID + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
        //���o�ǤJ�Ȥ�N���P��~�O,�̪������,�P�������
        public object[] GetGridDataSalesTypeDate(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string CustomerID = parm[0].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC JBERP.dbo.procSalesTypeDate '" + CustomerID + "'";
                DataTable dt = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = Newtonsoft.Json.JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
                transaction.Commit(); 
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }

        private void ucContactLogs_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucContactLogs.SetFieldValue("CreateDate", DateTime.Now);
        }
        private void ucCustomer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucContactLogs.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //�̷~�ȭ��v���s�W�P�f���O/�~�����O/�P�f���O�������Ȥ���
        public object[] procAddCustomerSaleTypeBySales(object[] objParam)
        {
            SqlCommand cmd;
            SqlConnection conn;
            string connectionstr = null;
            string sql = null;
            string[] parm = objParam[0].ToString().Split('*');
            string CustomerID = parm[0].ToString();
            string SalesID = parm[1].ToString();
            string TaxType = parm[2].ToString();
            string PayWay = parm[3].ToString();
            string BalanceDate = parm[4].ToString();
            string DebtorDays = parm[5].ToString();
            string AccountClerk = parm[6].ToString();
            string EmailAddress = parm[7].ToString();
            string QInvoiceType = parm[8].ToString();
            string sUSK = parm[9].ToString();    //�ϥΪ̫��w�n��s���~�����O
            string Sync1 = parm[10].ToString();  //�O�_�P�B�P�f���O ?
            string Sync2 = parm[11].ToString();  //�O�_�P�B�~�����O ?
            string Sync3 = parm[12].ToString();  //�O�_�P�B�Ȥ��� ? 
            string UserID = parm[13].ToString(); //
            connectionstr = "data source = 192.168.10.60;Initial Catalog=JBERP;User ID=sa;Password=NBV2mXzr";
            //connectionstr = "data source = .\\SQL2016;Initial Catalog=JBERP;User ID=sa;Password=SQLADMIN";
            conn = new SqlConnection(connectionstr);
            string js = string.Empty;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //�}�ltransaction
            //IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC JBERP.DBO.procAddCustomerSaleTypeBySales '" + CustomerID + "','" + SalesID + "','" + TaxType + "','" + PayWay + "','" + BalanceDate + "','" + DebtorDays + "','" + AccountClerk + "','" + EmailAddress + "','" + QInvoiceType + "','" + sUSK + "','" + Sync1 +"','"+ Sync2 + "','" + Sync3 + "','" + UserID + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //this.ExecuteSql(sql, connection, transaction);
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
            }
            finally
            {
                //ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
                ReleaseConnection("JBERP", conn);
            }
            return new object[] { 0, js };
        }
        //�̷~�ȭ��v���s�W�P�f���O/�~�����O/�P�f���O�������Ȥ���
        public object[] procAddCustomerSaleTypeBySalesType(object[] objParam)
        {
            SqlCommand cmd;
            SqlConnection conn;
            string connectionstr = null;
            string sql = null;
            string[] parm = objParam[0].ToString().Split(',');
            string CustomerID = parm[0].ToString();
            string SalesID = parm[1].ToString();
            string SalesTypeID = parm[2].ToString();
            string UserID = parm[3].ToString();
            //connectionstr = "data source = .\\SQL2016;Initial Catalog=JBERP;User ID=sa;Password=SQLADMIN";
            connectionstr = "data source = 192.168.10.60;Initial Catalog=JBERP;User ID=sa;Password=NBV2mXzr";
            conn = new SqlConnection(connectionstr);
            //IDbConnection connection = (IDbConnection)AllocateConnection(("JBERP"));
            string js = string.Empty;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //�}�ltransaction
            //IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC JBERP.DBO.procAddCustomerSaleTypeBySalesType '" + CustomerID + "','" + SalesID + "','" + SalesTypeID + "','" + UserID + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //this.ExecuteSql(sql, connection, transaction);
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
                ReleaseConnection("JBERP", conn);
            }
            return new object[] { 0, js };
       }
        //�̫Ȥ�N����s��~�����O���A
        public object[] procUpdateDevelopLevel(object[] objParam)
        {
            //SqlCommand cmd;
            //SqlConnection conn;
            //string connectionstr = null;
            string sql = null;
            string[] parm = objParam[0].ToString().Split(',');
            string CustomerID = parm[0].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection(("JBERP"));
            string js = string.Empty;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC JBERP.DBO.procUpdateDevelopLevel '" + CustomerID + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
       
    }
}
