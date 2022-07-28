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
        //�P�f���Ӹ�ƭץ�=>���O,�ϰ�,�o���~��,����,���Z,�X�Z�Ƶ�,PDF�ɦW
        public object[] UpdateSalesDetails(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string NewData = parm[1];//�s����
            string ItemSeq = parm[2];
            string iClass = parm[3];//�ק諸���
            string TransSys = parm[4];//�O�_�פJ

            string js = string.Empty;

            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            string sq2 = null;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//���n����
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
