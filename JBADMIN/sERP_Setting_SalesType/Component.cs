using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sERP_Setting_SalesType
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

        private void ucSalesType_BeforeApply(object sender, UpdateComponentBeforeApplyEventArgs e)
        {
            ucSalesType.SetFieldValue("CreateDate", DateTime.Now);
        }

        private void ucSalesType_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucSalesType.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
    }
}
