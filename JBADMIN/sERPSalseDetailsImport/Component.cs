using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JBTool;
using Newtonsoft.Json;
using Srvtools;

namespace sERPSalseDetailsImport
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

        //	�C��P�f��g�J�P�f���--1�d��,2�ץXExcel,3�g�P�f
        public object[] ImportSalesFromERPSalesMaster(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceYM = parm[0];
            string SalesID = parm[1];
            string SalesTypeID = parm[2];
            string CustNO = parm[3];
            int IsEmail = int.Parse(parm[4].ToString());
            string Type = parm[5];//1�d��,2�ץXExcel,3�g�P�f
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;

            //SqlConnection conn;
            //string connetionString = null;
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBERP;User ID=sa;Password=NBV2mXzr";
            ////connetionString = "Data Source=192.168.1.41;Initial Catalog=JBADMIN0129;User ID=sa;Password=8421JB1021";//Test

            //conn = new SqlConnection(connetionString);
            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = conn.BeginTransaction();

            try
            {

                string SQL = " exec JBERP.dbo.procImportSalesFromERPSalesMaster '" + InvoiceYM + "','" + SalesID + "','" + SalesTypeID + "','" + CustNO + "',"+IsEmail+","+ Type + ",'" + username + "'" + "\r\n";

                if (Type != "3") //1�d��,2�ץXExcel,3�g�P�f
                {
                    DataSet ds = this.ExecuteSql(SQL, conn, transaction);
                    //// Indented�Y�� �N����ഫ��Json�榡
                    js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                }
                else
                {

                    this.ExecuteSql(SQL, conn, transaction);
                }

                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, js };
        }
        //�C��P�f => �ץXExcel
        public object[] SalseDetailsAutoExcel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceYM = parm[0];
            string SalesID = parm[1];
            string SalesTypeID = parm[2];
            string CustNO = parm[3];
            string Type = parm[4];//1�d��,2�g�P�f
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

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

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = " exec JBERP.dbo.procImportSalesFromERPSalesMaster '" + InvoiceYM + "','" + SalesID + "','" + SalesTypeID + "','" + CustNO + "'," + Type + ",'" + username + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));

                theResult.Add("IsOK", true);
                theResult.Add("Msg", "���~�T��");
                theResult.Add("FileName", "�o�O�@���ɮ�.xls");

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
        }

        //���o����=> �P�B�ק�P�f�Ȥ᪺�}�o����T----1�d��,2�ק�
        public object[] UpdateCustomerInvoiceData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceYM = parm[0];
            string iType = parm[1]; 
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
                string sql = " exec JBERP.dbo.procUpdateCustomerInvoiceData '" + InvoiceYM + "'," + iType;

                if (iType == "1") //1�d��,2�ק�
                {
                    DataSet InvoiceYMPoint = this.ExecuteSql(sql, connection, transaction);
                    //// Indented�Y�� �N����ഫ��Json�榡
                    js = InvoiceYMPoint.Tables[0].Rows[0]["iCount"].ToString();

                    //DataSet ds = this.ExecuteSql(SQL, conn, transaction);
                    ////// Indented�Y�� �N����ഫ��Json�榡
                    //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

                }
                else
                {
                    this.ExecuteSql(sql, connection, transaction);
                }

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
