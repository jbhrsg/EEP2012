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
namespace sVendors
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

        private void ucVendors_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucVendors.SetFieldValue("CreateDate", DateTime.Now);
            ucVendors.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucVendors_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucVendors.SetFieldValue("LastUpdateDate", DateTime.Now); 
        }
        public object[] CheckDelVendors(object[] objParam)
        {
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
                string VendID = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM Requisition WHERE (PayTo) = '" + VendID + "'";
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