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

namespace sERP_Setting_SalesKindSalesType
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
        //�x�s�P�f�D�����P�f���O
        public object[] SaveKindSaleType(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string SalesKindID = parm[0].ToString();
            string SalesTypeIDs = parm[1].ToString();
            string userid = parm[2].ToString();
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
                string sql = "EXEC JBERP.dbo.procSaveKindSaleType '" + SalesKindID + "','" + SalesTypeIDs + "','" + userid + "'";
                this.ExecuteSql(sql, connection, transaction);

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
            return new object[] { 0, true };
        }

        private void ucSalesKind_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucSalesKind.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucSalesKind_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucSalesKind.SetFieldValue("CreateDate", DateTime.Now);
            ucSalesKind.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
    }
}
