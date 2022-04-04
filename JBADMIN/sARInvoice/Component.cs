using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;


namespace sARInvoice
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
        public object[] procCallApi_Cancel(object[] objParam)
        {
            string invoicexml = string.Empty;
            string js = string.Empty;
            string ok = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string InsGroupID = parm[0].ToString();
            string SalesNO = parm[1].ToString();
            string InvoiceNO = parm[2].ToString();
            string rentid = parm[3].ToString();
            string source = parm[4].ToString();
            string ReturnTaxNumber = parm[5].ToString(); //發票作廢文號
            string ReturnRemark = parm[6].ToString();    //發票作廢原因
            string UserID = parm[7].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Select Top 1 ISNULL(JBERP.dbo.funReturnInvoiceReturnXML('" + InvoiceNO + "','" + ReturnTaxNumber + "','" + ReturnRemark + "' ),'') AS XMLFile FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                invoicexml = ds.Tables[0].Rows[0]["XMLFile"].ToString();
                Cetustek.InvoiceAPIService o = new Cetustek.InvoiceAPIService();
                string message = o.CancelInvoiceNoCheck(invoicexml, rentid, source);
                js = message;
                //js = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.Indented);
                sql = "EXEC JBERP.dbo.procCancelInvoice '" + message + "','" + InsGroupID + "','" + SalesNO + "','" + InvoiceNO + "','" + invoicexml + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit(); //當使用 transaction 時,需增加此Command
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
    }
   
}
