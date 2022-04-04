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
        //�P�f���ӳ]���L��
        public object[] UpdateInvoiceYM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string NewYM = parm[1];
            string ItemSeq = parm[2];
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
                string SQL = "exec procUpdateERPSalseDetailsInvoiceYM '" + SalesMasterNO + "','" + NewYM + "','" + ItemSeq + "'";
                this.ExecuteSql(SQL, connection, transaction);
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

        //���}�o�����P�f���ӶפJ��F�t��
        public object[] InsertSalesDetailsYetImport(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string ItemSeq = parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());            
            string js = string.Empty;

            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            object[] ret = new object[] { 0, 0 };

            connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=sa;Password=J3554436B";
            conn = new SqlConnection(connetionString);


            //��s�u���A������open�ɡA�}�ҳs��
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //�}�ltransaction
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
