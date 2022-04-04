using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sInvoiceVoids
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

        private void ucERPInvoiceVoidApply_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPInvoiceVoidApply.SetFieldValue("CreateDate", DateTime.Now);
        }
    }
}
