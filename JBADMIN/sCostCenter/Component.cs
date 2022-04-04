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
namespace sCostCenter
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
        public object[] CheckDelCostCenter(object[] objParam)
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
                string AutoKey = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM Requisition WHERE (CostCenterID) = '" + AutoKey + "'";
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

        private void ucCostCenter_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
             ucCostCenter.SetFieldValue("CreateDate", DateTime.Now);
        }

        private void ucCostCenter_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            object[] res = SrvUtils.GetValue("_username", this);
            string username = res[1].ToString();
            ucCostCenter.SetFieldValue("LastUpdateDate", DateTime.Now);
            ucCostCenter.SetFieldValue("LastUpdateBy",username);
        }
    }
}
