using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Normal_CheckDetailsAction
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

        public object[] UpdateCheckDetails_Trust(object[] objParam)
        {
            string[] param = objParam[0].ToString().Split(',');
            string WarrantNOs = param[0];
            string ItemNOs = param[1];
            string TrustDate = param[2];
            string TrustAccountID = param[3];
            string[] arrWarrantNO = WarrantNOs.Split('*');
            string[] arrItemNO = ItemNOs.Split('*');
            int total = 0;
            string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string user_name = SrvGL.GetUserName(user_id);

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                for (int i = 0; i < arrWarrantNO.Count(); i++)
                {
                    string sql = "update CheckDetails set [TrustDate]='" + TrustDate + "',[TrustAccountID]='" + TrustAccountID + "',[ActionCode]=1,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where WarrantNO='" + arrWarrantNO[i] + "' and ItemNO='" + arrItemNO[i] + "'" + "\r\n";
                    total = total+this.ExecuteCommand(sql, connection, transaction);
                }
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
            return new object[] { 0, total};
        }

        public object[] UpdateCheckDetails_Cash(object[] objParam)
        {
            string[] param = objParam[0].ToString().Split(',');
            string WarrantNOs = param[0];
            string ItemNOs = param[1];
            string CashDate = param[2];

            string[] arrWarrantNO = WarrantNOs.Split('*');
            string[] arrItemNO = ItemNOs.Split('*');
            int total = 0;
            string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string user_name = SrvGL.GetUserName(user_id);

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                for (int i = 0; i < arrWarrantNO.Count(); i++)
                {
                    string sql = "update CheckDetails set [CashDate]='" + CashDate + "',[ActionCode]=2,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where WarrantNO='" + arrWarrantNO[i] + "' and ItemNO='" + arrItemNO[i] + "'" + "\r\n";
                    total = total + this.ExecuteCommand(sql, connection, transaction);
                }
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
            return new object[] { 0, total };
        }

        public object[] UpdateCheckDetails_Return(object[] objParam)
        {
            string[] param = objParam[0].ToString().Split(',');
            string WarrantNOs = param[0];
            string ItemNOs = param[1];
            string ReturnDate = param[2];
            string ReturnNotes = param[3];
            string[] arrWarrantNO = WarrantNOs.Split('*');
            string[] arrItemNO = ItemNOs.Split('*');
            int total = 0;
            string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string user_name = SrvGL.GetUserName(user_id);

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                for (int i = 0; i < arrWarrantNO.Count(); i++)
                {
                    string sql = "update CheckDetails set [ReturnDate]='" + ReturnDate + "',[ReturnNotes]='" + ReturnNotes + "',[ActionCode]=3,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where WarrantNO='" + arrWarrantNO[i] + "' and ItemNO='" + arrItemNO[i] + "'" + "\r\n";
                    total = total + this.ExecuteCommand(sql, connection, transaction);
                }
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
            return new object[] { 0, total };
        }
    }
}
