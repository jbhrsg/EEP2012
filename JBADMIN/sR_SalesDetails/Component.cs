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
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Data.SqlClient;
using System.Collections;


namespace sR_SalesDetails
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
        //�X�Z�έp��
        public object[] ReportSalesDetails(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = (DateTime.Parse(parm[0].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[1].ToString())).ToString("yyyy/MM/dd");
            string SalesEmployeeID = parm[2].ToString();
            string SalesTypeID = parm[3].ToString();
            string NewsTypeID = parm[4].ToString();
            string CustNO = parm[5].ToString();
            string Sort = parm[6].ToString();
            string iType = parm[7].ToString();//�e�{����	1�q�� 2���Z
            string IsAcceptePaper = parm[8].ToString();//�D�� 0���T�{, 1�q�l��, 2�ȥ�, 3����

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //���s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportERPSalseDetails '" + SDate + "','" + EDate + "','" + SalesEmployeeID + "','" + SalesTypeID + "','" + NewsTypeID + "','" + CustNO + "'," + Sort + "," + iType + "," + IsAcceptePaper;
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
        //��~�B�έp��
        public object[] ReportSalesDetails2(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = (DateTime.Parse(parm[0].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[1].ToString())).ToString("yyyy/MM/dd");
            string SalesEmployeeID = parm[2].ToString();
            string SalesTypeID = parm[3].ToString();
            string CustNO = parm[4].ToString();           
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //���s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportERPSalseDetails2 '" + SDate + "','" + EDate + "','" + SalesEmployeeID + "','" + SalesTypeID + "','" + CustNO + "'";
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
        //��~�B�έp��-by��
        public object[] ReportSalesDetails22(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = (DateTime.Parse(parm[0].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[1].ToString())).ToString("yyyy/MM/dd");
            string SalesEmployeeID = parm[2].ToString();
            string SalesTypeID = parm[3].ToString();
            string CustNO = parm[4].ToString();
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //���s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportERPSalseDetails22 '" + SDate + "','" + EDate + "','" + SalesEmployeeID + "','" + SalesTypeID + "','" + CustNO + "'";
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
        //��~�B�έp��-by��
        public object[] ReportSalesDetails222(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = (DateTime.Parse(parm[0].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[1].ToString())).ToString("yyyy/MM/dd");
            string SalesEmployeeID = parm[2].ToString();
            string SalesTypeID = parm[3].ToString();
            string CustNO = parm[4].ToString();
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //���s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportERPSalseDetails222 '" + SDate + "','" + EDate + "','" + SalesEmployeeID + "','" + SalesTypeID + "','" + CustNO + "'";
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
        //�P��Ʀ�]
        public object[] ReportSalesDetails3(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = (DateTime.Parse(parm[0].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[1].ToString())).ToString("yyyy/MM/dd");
            string SalesEmployeeID = parm[2].ToString();
            string SalesTypeID = parm[3].ToString();
            string CustNO = parm[4].ToString();
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //���s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportERPSalseDetails3 '" + SDate + "','" + EDate + "','" + SalesEmployeeID + "','" + SalesTypeID + "','" + CustNO + "'" ;
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

        //�o�������
        public object[] ReportInvoiceList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceYM = parm[0].ToString();
            string SalesID = parm[1].ToString();
            string SalesTypeID = parm[2].ToString();
            string CustNO = parm[3].ToString();
            string IsTransSys = parm[4].ToString();
            string Type = parm[5].ToString();

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //���s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportInvoiceList '" + InvoiceYM + "','" + SalesID + "','" + SalesTypeID + "','" + CustNO + "'," + IsTransSys + "," + Type;
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

    }
}