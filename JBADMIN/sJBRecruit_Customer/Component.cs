using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;

namespace sJBRecruit_Customer
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

        private void ucCustomer_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //���ѱ��O�]���ק￤���ζm��ϥu���b�P�f�Ȥ��Ʒs�W�ק�ɤ~�|���CustomerAddress_A
            //string Addr_Country = ucCustomer.GetFieldCurrentValue("Addr_Country").ToString();
            //string Addr_City = ucCustomer.GetFieldCurrentValue("Addr_City").ToString();
            //string sql = "select ListID from ListTable where ListCategory = 'Area' and ListContent='" + Addr_Country + Addr_City + "'";
            //DataTable dt=ExecuteSql(sql, ucCustomer.conn, ucCustomer.trans).Tables[0];
            //if (dt.Rows.Count > 0) { 
            //    ucCustomer.SetFieldValue("CustomerAddress_A",dt.Rows[0][0]);
            //}
        }
    }
}
