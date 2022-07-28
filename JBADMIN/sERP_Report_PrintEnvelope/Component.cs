using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_PrintEnvelope
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

        public object[] GetCustomerSaleType(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string CustomerID = parm[0];
            string SalesTypeID = parm[1];
            List<string> whereList=new  List<string>();
            string whereString = "";
            string[] arrCustomerID = CustomerID.Split('*');
            string[] arrSalesTypeID = SalesTypeID.Split('*');

            for (int i = 0; i < arrCustomerID.Length; i++)
            {
                whereList.Add("cst.CustomerID ='" + arrCustomerID[i] + "' and  cst.SalesTypeID ='" + arrSalesTypeID[i] + "'");
            }
            whereString=String.Join(" or ", whereList.ToArray());
            
                
                

            string sql = "";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = sql + "SELECT c.[CustomerName],c.[TelNO],c.[Addr_Desc],c.[ZIPCode],cst.AccountClerk" + "\r\n";
                sql = sql + "FROM [JBERP].[dbo].[CustomerSaleType] cst" + "\r\n";
                sql = sql + "left join [JBERP].[dbo].[Customer] c  on cst.CustomerID=c.CustomerID" + "\r\n";

                sql = sql + "where  " + whereString + "\r\n";

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
    }
}
