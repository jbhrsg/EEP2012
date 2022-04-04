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

namespace sERPCustomerLite
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
        private void ucERPCustomers_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            
            ucERPCustomers.SetFieldValue("CreateDate", DateTime.Now);
            ucERPCustomers.SetFieldValue("LastUpdateDate", DateTime.Now);
            ucERPCustomers.SetFieldValue("PostedDate", DateTime.Now);
        }

        private void ucERPCustomers_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucERPCustomers.SetFieldValue("CreateDate", DateTime.Now);
            ucERPCustomers.SetFieldValue("LastUpdateDate", DateTime.Now);
            ucERPCustomers.SetFieldValue("PostedDate", DateTime.Now);
        }
        public object[] CheckCustNO(object[] objParam)
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
                string CustNO = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM [60.250.52.106,1433].JBADMIN.dbo.ERPCustomers WHERE CustNO = '" + CustNO + "'";
                //string sql = "SELECT COUNT(*) AS CNT FROM [192.168.1.38].JBADMIN.dbo.ERPCustomers WHERE CustNO = '" + CustNO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
    }
}
