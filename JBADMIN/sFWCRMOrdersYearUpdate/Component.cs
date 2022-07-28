using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sFWCRMOrdersYearUpdate
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
        //Flow�ק�q��~�צ��\=>�ק� FWCRMOrders => OrderYear     
        public object procUpdateOrderYear(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };                    

            DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string OrderNo = drDara["OrderNo"].ToString();
            string OrderYearNew = drDara["OrderYearNew"].ToString();         

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
                var sql = "";              

                sql = "update FWCRMOrders set OrderYear= '" + OrderYearNew + "' where OrderNo='" + OrderNo + "'" + "\r\n";

                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit(); // �T�{���
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L

        }


    }
}
