using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
namespace sERPSalesInvoices
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
        //���o
        public object[] GetSalesNO(object[] objParam)
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
                string sql = "SELECT TOP 1 JBERP.dbo.funGetSalesNO() as SalesNO From EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js }; ;

        }

        //--------begin---------�}�ߧ@�o���(�q�}�o���B���ڡB��}�o��)�B�@�o�P�f---------------------

        //�}�߹q�}�o��
        public object[] procCallApi_Create(object[] objParam)
        {
            string invoicexml = string.Empty;
            string js = string.Empty;
            string sql = string.Empty;
            string ok = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string InsGroupID = parm[0].ToString();
            string SalesNO = parm[1].ToString();
            string hastax = parm[2].ToString();
            string rentid = parm[3].ToString();
            string source = parm[4].ToString();
            string UserID = parm[5].ToString();
            string IsOutPutDetails = parm[6].ToString();
            string SalesTypeID = parm[7].ToString();
            string tmpInvoiceNO = parm[8].ToString();
            string InvoiceNO = "";
            string message="";//�H�D�^�Ǫ��r��

            string ihastax = "0";
            if (hastax == "true" || hastax == "TRUE" || hastax == "True" || hastax == "1")
            {
                ihastax = "1";
            }

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "Select Top 1 ISNULL(JBERP.dbo.funReturnSalesNOXMLManySingle('" + SalesNO + "'),'') AS XMLFile FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                invoicexml = ds.Tables[0].Rows[0]["XMLFile"].ToString();
                CetusAPI.InvoiceAPIService o = new CetusAPI.InvoiceAPIService();
                message = o.CreateInvoiceV3(invoicexml, ihastax, rentid, source);
                if (message.Length == 15)//�^�Ǧ��o�����X
                {
                    InvoiceNO = message.Substring(0, 10);
                }
                js = message;
                //string message = "MA12618689;1104";
                //�g�J�o���ɡF�s�WAPI�W�Ǭ����F��s�P�f�D��UploadCode�F1,6,31�g�^�C��P�f����ovInvoice=1
                sql = "EXEC JBERP.dbo.procCreateInvoice '" + message + "','" + SalesNO + "','" + invoicexml + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit(); //��ϥ� transaction ��,�ݼW�[��Command
            }
            catch (Exception ex)
            {
               js = ex.Message;
               transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }


            if (message.Length == 15)//�^�Ǧ��o�����X
            {
                //-------01,02 �^�g�~�Ҩt�θ�� �o�����X; 1,6,31 �C��P�f �g�Ȥ��� �̪�����,1 �D�~�����,31 �g�������,6 ���ȥ����-------            SqlCommand cmd;
                SqlCommand cmd;
                SqlConnection conn;
                string connetionString = null;
                string sq2 = null;
                connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//���n����
                //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";

                conn = new SqlConnection(connetionString);
                //conn = (SqlConnection)AllocateConnection("JBADMIN");
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    if (SalesTypeID == "01")
                    {
                        //�^��o���N��
                        sq2 = "EXEC [60.250.52.106,1433].FWCRM.dbo.procUpdateInvoiceDataInvoiceNO '" + tmpInvoiceNO + "','" + InvoiceNO + "','" + UserID + "'";
                    }
                    else if (SalesTypeID == "02")
                    {
                        //�^��o���N��
                        sq2 = "EXEC [60.250.52.106,1433].FWCRMJS.dbo.procUpdateInvoiceDataInvoiceNO '" + tmpInvoiceNO + "','" + InvoiceNO + "','" + UserID + "'";
                    }
                    else if (SalesTypeID == "47")//�޲z�A�ȶO-�ǫH�a�A
                    {
                        //�^��o���N��
                        sq2 = "EXEC [60.250.52.106,1433].FWCRMJSCare.dbo.procUpdateInvoiceDataInvoiceNO '" + tmpInvoiceNO + "','" + InvoiceNO + "','" + UserID + "'";
                    }
                     else if (SalesTypeID == "48")//�޲z�A�ȶO-�ǳ��a�A
                    {
                        //�^��o���N��
                        sq2 = "EXEC [60.250.52.106,1433].FWCRMCare.dbo.procUpdateInvoiceDataInvoiceNO '" + tmpInvoiceNO + "','" + InvoiceNO + "','" + UserID + "'";
                    }
                    else if (SalesTypeID == "1" || SalesTypeID == "6" || SalesTypeID == "31")
                    {
                        //��sERPCustomers�̷s�����
                        sq2 = "EXEC dbo.procUpdateInvoiceDataERPCustomers '" + SalesNO + "'";
                    }
                    cmd = new SqlCommand(sq2, conn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //transaction.Rollback();
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    ReleaseConnection("JBADMIN", conn);
                }
            }

            return new object[] { 0, js };
        }

        //�}�ߦ���
        public object[] procCreateReceipt(object[] objParam)
        {
            string js = string.Empty;
            string ok = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string InsGroupID = parm[0].ToString();
            string SalesNO = parm[1].ToString();
            string UserID = parm[2].ToString();
            string SalesTypeID = parm[3].ToString();
            string sql1 = string.Empty;
            string js1 = string.Empty;
            string js2 = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //�g�J�o���ɡF��s�P�f�D��UploadCode�F1,6,31�g�^�C��P�f����ovInvoice=1
                sql = "EXEC JBERP.dbo.procCreateReceipt '" + InsGroupID + "','" + SalesNO + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit(); //��ϥ� transaction ��,�ݼW�[��Command
                //�ˬd�ӾP�f�渹���P�f���B�B���ڪ��B�O�_�ۦP�A���P�|����
                sql1 = "select id.SalesAmount as idSalesAmount"+"\r\n";
                sql1 = sql1+",(select ISNULL(SUM(sd.Quantity*sd.UnitPrice),0) From JBERP.dbo.SalesDetails sd where sd.SalesNO=id.SalesNO) as sdSalesAmount"+"\r\n";
                sql1 = sql1 + "From JBERP.dbo.InvoiceDetails id" + "\r\n";
                sql1 = sql1+"where id.SalesNO='" + SalesNO + "'"+"\r\n";
                DataTable dt = this.ExecuteSql(sql1, connection, transaction).Tables[0];
                js1 = dt.Rows[0]["idSalesAmount"].ToString();//= Newtonsoft.Json.JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
                js2 = dt.Rows[0]["sdSalesAmount"].ToString();
                js = "RS"+"*"+js1+"*"+js2;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }

            //��s�C��Ȥ᪺�̪�����
            //------ 1,6,31 �C��P�f �g�Ȥ��� �̪�����,1 �D�~�����,31 �g�������,6 ���ȥ����-------            SqlCommand cmd;
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sq2 = null;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//���n����
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=jbdbsql;Password=J3554436B";

            conn = new SqlConnection(connetionString);
            //conn = (SqlConnection)AllocateConnection("JBADMIN");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                if (SalesTypeID == "1" || SalesTypeID == "6" || SalesTypeID == "31")
                {
                    //��sERPCustomers�̷s�����
                    sq2 = "EXEC dbo.procUpdateInvoiceDataERPCustomers '" + SalesNO + "'";
                }
                cmd = new SqlCommand(sq2, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                ReleaseConnection("JBADMIN", conn);
            }

            return new object[] { 0, js };
        }

        //�P�f���J�ӽЬy�{�����A�ק�P�f�D��.IsActive=1(�Y����}�o���A�h�g�J�o����)
        public object[] updateSalesMasterIsActive(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0];
            string SalesNO = dr["SalesNO"].ToString();
            string SalesTypeID = dr["SalesTypeID"].ToString();//for��s�C��Ȥ᪺�̪�����

            string CreateInvoiceType = dr["CreateInvoiceType"].ToString();//�}�o������1.�q�} 2.��}
            string HandWriteInvoiceNO = dr["HandWriteInvoiceNO"].ToString();//��}�o�����X
            string WantedInvoiceYM = dr["WantedInvoiceYM"].ToString();//�o�����}�~��

            string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "Update [SalesMaster] Set IsActive=1 where SalesNO='" + SalesNO + "'" + "\r\n";
                this.ExecuteCommand(sql, connection, transaction);

                if (CreateInvoiceType == "2" && HandWriteInvoiceNO != "" && WantedInvoiceYM != "")
                {
                    //�g�J�o���ɡF��s�P�f�D��UploadCode�F1,6,31�g�^�C��P�f����ovInvoice=1
                    this.ExecuteCommand("exec procCreateInvoice_HandWriteInvoice '" + SalesNO + "','" + UserID + "'", connection, transaction);
                }

                transaction.Commit();
                ret[1] = true;
            }
            catch (Exception ex)
            {
                ret[1] = false;
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }


            //��s�C��Ȥ᪺�̪�����
            //------ 1,6,31 �C��P�f �g�Ȥ��� �̪�����,1 �D�~�����,31 �g�������,6 ���ȥ����-------            SqlCommand cmd;
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sq2 = null;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//���n����
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=jbdbsql;Password=J3554436B";
            conn = new SqlConnection(connetionString);
            //conn = (SqlConnection)AllocateConnection("JBADMIN");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                if (SalesTypeID == "1" || SalesTypeID == "6" || SalesTypeID == "31")
                {
                    //��sERPCustomers�̷s�����
                    sq2 = "EXEC dbo.procUpdateInvoiceDataERPCustomers '" + SalesNO + "'";
                }
                cmd = new SqlCommand(sq2, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                ReleaseConnection("JBADMIN", conn);
            }


            return ret;
        }

        //�@�o�q�}�o��
        public object[] procCallApi_Cancel(object[] objParam)
        {
            string invoicexml = string.Empty;
            string js = string.Empty;
            string ok = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split('*');

            string InsGroupID = parm[0].ToString();
            string SalesNO = parm[1].ToString();
            string InvoiceNO = parm[2].ToString();
            string rentid = parm[3].ToString();
            string source = parm[4].ToString();
            string ReturnTaxNumber = parm[5].ToString(); //�o���@�o�帹
            string ReturnRemark = parm[6].ToString();    //�o���@�o��]
            string UserID = parm[7].ToString();
            string InvoiceDate = parm[8].ToString();
            //string SalesTypeID = parm[8].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Select Top 1 ISNULL(JBERP.dbo.funReturnInvoiceReturnXML('" + InvoiceNO + "','" + InvoiceDate + "','" + ReturnTaxNumber + "','" + ReturnRemark + "' ),'') AS XMLFile FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                
                invoicexml = ds.Tables[0].Rows[0]["XMLFile"].ToString();
                CetusAPI.InvoiceAPIService o = new CetusAPI.InvoiceAPIService();

                string message = o.CancelInvoiceNoCheck(invoicexml, rentid, source);
                //��s�o����IsActive=0�F1,6,31�C��P�f�g���}�o��(ovInvoice=0)�F�s�WAPI�W�Ǭ�����APIUploadLogs�F��s�P�f�D��UploadCode
                sql = "EXEC JBERP.dbo.procCancelInvoice '" + message + "','" + InsGroupID + "','" + SalesNO + "','" + InvoiceNO + "','" + invoicexml + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                js = message;

                //string message = "C0";
                //sql = "EXEC JBERP.dbo.procCancelInvoice '" + "C0" + "','" + InsGroupID + "','" + SalesNO + "','" + InvoiceNO + "','" + invoicexml + "','" + UserID + "'";
                //this.ExecuteSql(sql, connection, transaction);
                //js = message;

                transaction.Commit(); //��ϥ� transaction ��,�ݼW�[��Command
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                js = ex.ToString();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }

        //�@�o����
        public object[] Receipt_Cancel(object[] objParam)
        {
            string js = string.Empty;
            string ok = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            
            string SalesNO = parm[0].ToString();
            string InvoiceNO = parm[1].ToString();
            string UserID = parm[2].ToString();

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //��s�o����IsActive=0�F1,6,31�C��P�f�g���}�o��(ovInvoice=0)�F��s�P�f�D��UploadCode=C00
                string sql = "EXEC JBERP.dbo.procCancelReceipt '" + UserID + "','" + InvoiceNO + "','" + SalesNO + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit(); //��ϥ� transaction ��,�ݼW�[��Command
                js = "C00";
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }

        //�@�o��}�o��
        public object[] HandWriteInvoice_Cancel(object[] objParam)
        {
            string js = string.Empty;
            string ok = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');

            string SalesNO = parm[0].ToString();
            string InvoiceNO = parm[1].ToString();
            string UserID = parm[2].ToString();

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //��s�o����IsActive=0�F1,6,31�C��P�f�g���}�o��(ovInvoice=0)�F��s�P�f�D��UploadCode=C000
                string sql = "EXEC JBERP.dbo.procCancelInvoice_HandWriteInvoice '" + UserID + "','" + InvoiceNO + "','" + SalesNO + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit(); //��ϥ� transaction ��,�ݼW�[��Command
                js = "C000";
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }

        //�@�o�P�f
        public object[] procDeleteSales(object[] objParam)
        {
            string js = string.Empty;
            js = "OK";
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string SalesNO = parm[0].ToString();
            string UserID = parm[1].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var sql = "EXEC JBERP.dbo.procDeleteSales '" + SalesNO + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
            }
            catch
            {
                js = "Fail";
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }

        //--------end---------�}�ߧ@�o���(�q�}�o���B��}�o���B����)�B�@�o�P�f---------------------

        //�Ȥ�P�f���O
        public object[] selectCustomerSaleType(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            string dat = DateTime.Now.ToString("yyyy/MM/dd");
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string CustomerID = parm[0].ToString();
            string SalesTypeID = parm[1].ToString();
            string InvoiceType = parm[2].ToString();
            
            string DomID = CustomerID.Substring(0, 1);  //�J�٥N��
            string CustID = string.Empty;
            if (DomID != "C")//�ثe�}�Y���OC�����O�J�٪��Ы�
            {
                CustID = CustomerID.Substring(1, 8); //�J�٩ЫȥN��
            }
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                if (DomID != "C")//�ثe�}�Y���OC�����O�J�٪��ЫȡA���F��^�o�Ыȥثe�����D
                {
                    sql = "SELECT SalesID,TaxType,PayWay,BalanceDate,DebtorDays,AccountClerk,EmailAddress,QInvoiceType,dbo.funReturnRoomerEmployer(" + "'" + CustID + "'" + "," + "'" + dat + "'" + "," + DomID + ") AS Employer," + "\r\n";
                }
                else {
                    sql = "SELECT SalesID,TaxType,PayWay,BalanceDate,DebtorDays,AccountClerk,EmailAddress,QInvoiceType,null AS Employer," + "\r\n";
                }
                sql = sql + "dbo.funReturnInvoiceRemark(" + "'" + InvoiceType + "'" + "," + "'" + CustomerID +"'"+ ") AS Remark,TaxType" + "\r\n";
                sql = sql + ",skst.SalesKindID" + "\r\n";
                sql = sql + "FROM [CustomerSaleType]" + "\r\n";
                sql = sql + "left join [SalesKindSalesType] skst on skst.SalesTypeID=[CustomerSaleType].SalesTypeID" + "\r\n";
                sql = sql + "where CustomerID='" + CustomerID + "' and [CustomerSaleType].SalesTypeID='" + SalesTypeID + "'" + "\r\n";
                DataTable dt=this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = Newtonsoft.Json.JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
                transaction.Commit(); //��ϥ� transaction ��,�ݼW�[��Command
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

        //�Ƶ�&�νs
        public object[] selectRemarkAndTaxNO(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string CustomerID = parm[0].ToString();//�Ȥ�ID
            string InvoiceType = parm[1].ToString();//������O

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "select dbo.funReturnInvoiceRemark(" + "'" + InvoiceType + "'" + "," + "'" + CustomerID + "'" + ") AS Remark,TaxNO from Customer where CustomerID='" + CustomerID + "'" + "\r\n";
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

        //�妸�s�W�J�٤G�p�P�f(�����g�J�P�f�D�׻P����)NotUse
        public object[] BatchAddForDorm(object[] objParam)
        {
            string js = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string InsGroupID = parm[0].ToString();
            string CustomerID = parm[1].ToString();
            string UserName = parm[2].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var sql = "EXEC JBERP.dbo.procBatchInsertSalesForDorm_NotUse '" + InsGroupID + "','" + CustomerID + "','" + UserName + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];//�^��@@Error(0�O���`)
                js=JsonConvert.SerializeObject(tb, Formatting.Indented);
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

        //�妸�s�W�J�٤G�p�P�f��Temp(�ĤG��)
        public object[] BatchInsertTemp(object[] objParam)
        {
            string js = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string InsGroupID = parm[0].ToString();
            string CustomerID = parm[1].ToString();
            string UserName = parm[2].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var sql = "EXEC procBatchInsertSalesTempForDorm '" + InsGroupID + "','" + CustomerID + "','" + UserName + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];//�^��@@Error(0�O���`)
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

        public object[] DeleteSalesDetailsMasterTemp(object[] objParam)
        {
            bool js;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var sql = "delete from SalesDetailsMasterTemp";
                this.ExecuteCommand(sql, connection, transaction);
                js = true;
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

        //(�ĤG��)
        public object[] BatchInsertSales(object[] objParam)
        {
            string js = string.Empty;
            string sql = "";

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC procBatchInsertSalesForDorm";
                //this.ExecuteCommand(sql, connection, transaction);
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];//�^��@@Error(0�O���`)
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch (Exception e)
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

        public object[] UpdateSalesDetailsMasterTemp(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string salesNOs = parm[0].ToString();
            string itemNOs = parm[1].ToString();
            string unitPrice = parm[2].ToString().Trim();
            string quantity = parm[3].ToString().Trim();
            string feeItemID = parm[4].ToString().Trim();
            string salesTypeName = parm[5].ToString().Trim();

            string[] arrSalesNO = salesNOs.Split('*');
            string[] arrItemNO = itemNOs.Split('*');
            List<string> lstColumns=new List<string>();
            string strColumns;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                for (int i = 0; i < arrSalesNO.Length; i++)
                {

                    lstColumns.Clear();
                    strColumns = "";
                    //this.ExecuteCommand(sql, connection, transaction);
                    IDbCommand cmd = connection.CreateCommand();
                    //cmd.CommandText = sql;
                    cmd.CommandTimeout = 90;
                    cmd.Transaction = transaction;

                    IDbDataParameter param = cmd.CreateParameter();
                    param.ParameterName = "@SalesNO";
                    param.Value = arrSalesNO[i];
                    cmd.Parameters.Add(param);

                    IDbDataParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "@ItemNO";
                    param1.Value = arrItemNO[i];
                    cmd.Parameters.Add(param1);

                    if (unitPrice != "")
                    {
                        IDbDataParameter param2 = cmd.CreateParameter();
                        param2.ParameterName = "@UnitPrice";
                        param2.Value = unitPrice;
                        cmd.Parameters.Add(param2);
                        lstColumns.Add("UnitPrice=@UnitPrice");
                    }
                    if (quantity != "")
                    {
                        IDbDataParameter param3 = cmd.CreateParameter();
                        param3.ParameterName = "@Quantity";
                        param3.Value = quantity;
                        cmd.Parameters.Add(param3);
                        lstColumns.Add("Quantity=@Quantity");
                    }
                    if (feeItemID != "")
                    {
                        IDbDataParameter param4 = cmd.CreateParameter();
                        param4.ParameterName = "@FeeItemID";
                        param4.Value = feeItemID;
                        cmd.Parameters.Add(param4);
                        lstColumns.Add("FeeItemID=@FeeItemID");
                    }
                    if (salesTypeName != "")
                    {
                        IDbDataParameter param5 = cmd.CreateParameter();
                        param5.ParameterName = "@SalesTypeName";
                        param5.Value = salesTypeName;
                        cmd.Parameters.Add(param5);
                        lstColumns.Add("SalesTypeName=@SalesTypeName");
                    }

                    strColumns=string.Join(",",lstColumns.ToArray());
                    sql = @"Update [SalesDetailsMasterTemp] Set " + strColumns + " where SalesNO=@SalesNO and ItemNO=@ItemNO";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit(); //��ϥ� transaction ��,�ݼW�[��Command
            }
            catch(Exception e)
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[]{0, js};
        }

        public object[] SelectRoomerFeeItems(object[] objParam)
        {
            string js = string.Empty;
            string[] param = objParam[0].ToString().Split(',');
            string insGroupID = param[0];//���q�O
            string companyCustomerID = param[1];//�������q�Ȥ�

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //���odatatable
                string sql = "select * from  dbo.funReturnSalesDetails1 ('" + insGroupID + "','" + companyCustomerID + "')";
                DataTable tbInvoiceDetails = this.ExecuteSql(sql, connection, transaction).Tables[0];
                //�নjs�榡string
                js = JsonConvert.SerializeObject(tbInvoiceDetails, Formatting.Indented);
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
        
        private void ucSalesMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucSalesMaster.SetFieldValue("CreateDate", DateTime.Now);
            ucSalesMaster.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucSalesMaster_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucSalesMaster.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        //�s�W��ƨ�P�f�D��(�ĤT��)
        public void ucSalesDetails1_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string ItemNO = ucSalesDetails1.GetFieldCurrentValue("ItemNO").ToString();

            string SalesNO = ucSalesDetails1.GetFieldCurrentValue("SalesNO").ToString();
            string SalesTypeID = ucSalesDetails1.GetFieldCurrentValue("SalesTypeID").ToString();
            
            string CustomerID = ucSalesDetails1.GetFieldCurrentValue("CustomerID").ToString();
            string SalesDate = DateTime.Parse(ucSalesDetails1.GetFieldCurrentValue("SalesDate").ToString()).ToString("yyyy/MM/dd");//�h���W�ȤU��
            string SalesID = ucSalesDetails1.GetFieldCurrentValue("SalesID").ToString();
            string TaxType = ucSalesDetails1.GetFieldCurrentValue("TaxType").ToString();
            string PayWayID = ucSalesDetails1.GetFieldCurrentValue("PayWayID").ToString();
            string Remark = ucSalesDetails1.GetFieldCurrentValue("Remark").ToString();
            string InsGroupID = ucSalesDetails1.GetFieldCurrentValue("InsGroupID").ToString();
            string SalesKindID = ucSalesDetails1.GetFieldCurrentValue("SalesKindID").ToString();
            string Employer = ucSalesDetails1.GetFieldCurrentValue("Employer").ToString();
            string BalanceDate = ucSalesDetails1.GetFieldCurrentValue("BalanceDate").ToString();
            string DebtorDays = ucSalesDetails1.GetFieldCurrentValue("DebtorDays").ToString();
            string EmailAddress = ucSalesDetails1.GetFieldCurrentValue("EmailAddress").ToString();

            string sql0 = "select count(SalesNO) from dbo.SalesMaster where SalesNO='" + SalesNO + "'";
            string result = this.ExecuteSql(sql0, ucSalesDetails1.conn, ucSalesDetails1.trans).Tables[0].Rows[0][0].ToString();

            if (result == "0")
            {
                string sql = "insert into dbo.SalesMaster(" + "\r\n";
                sql = sql + "[SalesNO],[SalesDate],[InvoiceType],[QInvoiceType],[CustomerID],[SalesTypeID],[SalesID],[BalanceDate],[DebtorDays],[Employer],[EmailAddress],[TaxRate],[TaxType],[IsActive],[IsOutPutDetails],[DonateMarkID],[FlowFlag],[UploadCode],[CreateBy],[CreateDate],[LastUpdateBy],[LastUpdateDate],[InsGroupID],[MailSend],[IsHasTax],[SalesKindID],[Remark]) values " + "\r\n";
                sql = sql + "('" + SalesNO + "'," + "convert(datetime,'" + SalesDate + "', 111)" + ",'07','99','" + CustomerID + "','" + SalesTypeID + "','" + SalesID + "'," + BalanceDate + "," + DebtorDays + ",'" + Employer + "','" + EmailAddress + "','0.05','" + TaxType + "',1,0,2,'Z','00','allen',GETDATE(),'allen',GETDATE(),'" + InsGroupID + "',0,0,'" + SalesKindID + "','" + Remark + "')" + "\r\n";
                this.ExecuteCommand(sql, ucSalesDetails1.conn, ucSalesDetails1.trans);
            }

        }

        //��Amount
        public void ucSalesDetails1_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e){
            int UnitPrice = (ucSalesDetails1.GetFieldCurrentValue("UnitPrice").ToString() != "") ? Convert.ToInt16(ucSalesDetails1.GetFieldCurrentValue("UnitPrice")) : 0;
            int Quantity = (ucSalesDetails1.GetFieldCurrentValue("Quantity").ToString() != "") ? Convert.ToInt16(ucSalesDetails1.GetFieldCurrentValue("Quantity")) : 0;
            ucSalesDetails1.SetFieldValue("Amount", UnitPrice * Quantity);
        }

        //���ϥΪ̲�´�s��
        public object[] GetUserOrgNOs(object[] objParam)
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
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
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

        //�ˬd���o�����L�b���ک��Ӹ�Ƹ�
        public object[] CheckInvoiceNOIsInWarrantDetails(object[] objParam)
        {
            string js = string.Empty;
            string[] param = objParam[0].ToString().Split(',');
            string InvoiceNO = param[0];

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select count(WarrantNO) as Counts from WarrantDetails where InvoiceNO='"+InvoiceNO+"'"+"\r\n";

                DataTable tbInvoiceDetails = this.ExecuteSql(sql, connection, transaction).Tables[0];
                //�নjs�榡string
                js = JsonConvert.SerializeObject(tbInvoiceDetails, Formatting.Indented);
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
    }
}
