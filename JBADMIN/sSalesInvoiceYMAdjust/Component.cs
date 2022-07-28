using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sSalesInvoiceYMAdjust
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
        //銷貨明細資料修正=>版別,區域,發票年月,單位數,見刊,出刊備註,PDF檔名
        public object[] UpdateSalesDetails(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string NewData = parm[1];//新的值
            string ItemSeq = parm[2];
            string iClass = parm[3];//修改的欄位
            string TransSys = parm[4];//是否匯入

            string js = string.Empty;

            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            string sq2 = null;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//敦緯內網
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";

            conn = new SqlConnection(connetionString);
            //IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                sql = "exec procUpdateERPSalseDetailsInvoiceYM '" + SalesMasterNO + "','" + ItemSeq + "','" + iClass + "','" + NewData + "'";
                //sql = "EXEC [60.250.52.107,3225].JBADMIN.dbo.procUpdateERPSalseDetailsIsActive '" + SalesMasterNO + "','" + ItemSeq + "','" + sType + "','" + username + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();            
              
            }
            catch
            {
            }
            finally
            {
                conn.Dispose();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, js };
        }

        //不開發票之銷貨明細匯入行政系統
        public object[] InsertSalesDetailsYetImport(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string ItemSeq = parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());            
            string js = string.Empty;

            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            object[] ret = new object[] { 0, 0 };

            connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=sa;Password=J3554436B";
            conn = new SqlConnection(connetionString);


            //當連線狀態不等於open時，開啟連結
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //開始transaction
            //IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = "exec procInsertERPSalesDetailsYetImport '" + SalesMasterNO + "','" + ItemSeq + "','" + username + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);

            }
            return ret;

        }

    }
}
