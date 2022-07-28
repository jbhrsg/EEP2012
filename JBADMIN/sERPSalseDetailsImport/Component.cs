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

        //	媒體銷貨單寫入銷貨資料--1查詢,2匯出Excel,3寫銷貨
        public object[] ImportSalesFromERPSalesMaster(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceYM = parm[0];
            string SalesID = parm[1];
            string SalesTypeID = parm[2];
            string CustNO = parm[3];
            int IsEmail = int.Parse(parm[4].ToString());
            string Type = parm[5];//1查詢,2匯出Excel,3寫銷貨
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

            //開始transaction
            IDbTransaction transaction = conn.BeginTransaction();

            try
            {

                string SQL = " exec JBERP.dbo.procImportSalesFromERPSalesMaster '" + InvoiceYM + "','" + SalesID + "','" + SalesTypeID + "','" + CustNO + "',"+IsEmail+","+ Type + ",'" + username + "'" + "\r\n";

                if (Type != "3") //1查詢,2匯出Excel,3寫銷貨
                {
                    DataSet ds = this.ExecuteSql(SQL, conn, transaction);
                    //// Indented縮排 將資料轉換成Json格式
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
        //媒體銷貨 => 匯出Excel
        public object[] SalseDetailsAutoExcel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceYM = parm[0];
            string SalesID = parm[1];
            string SalesTypeID = parm[2];
            string CustNO = parm[3];
            string Type = parm[4];//1查詢,2寫銷貨
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

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

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = " exec JBERP.dbo.procImportSalesFromERPSalesMaster '" + InvoiceYM + "','" + SalesID + "','" + SalesTypeID + "','" + CustNO + "'," + Type + ",'" + username + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));

                theResult.Add("IsOK", true);
                theResult.Add("Msg", "錯誤訊息");
                theResult.Add("FileName", "這是一個檔案.xls");

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

        //取得筆數=> 同步修改銷貨客戶的開發票資訊----1查詢,2修改
        public object[] UpdateCustomerInvoiceData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string InvoiceYM = parm[0];
            string iType = parm[1]; 
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
                string sql = " exec JBERP.dbo.procUpdateCustomerInvoiceData '" + InvoiceYM + "'," + iType;

                if (iType == "1") //1查詢,2修改
                {
                    DataSet InvoiceYMPoint = this.ExecuteSql(sql, connection, transaction);
                    //// Indented縮排 將資料轉換成Json格式
                    js = InvoiceYMPoint.Tables[0].Rows[0]["iCount"].ToString();

                    //DataSet ds = this.ExecuteSql(SQL, conn, transaction);
                    ////// Indented縮排 將資料轉換成Json格式
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
