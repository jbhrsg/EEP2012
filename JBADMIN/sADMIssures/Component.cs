using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sADMIssures
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

        private void ucAdmIssures_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucAdmIssures.SetFieldValue("CreateDate", DateTime.Now);
        }

        private void ucAdmIssures_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucAdmIssures.SetFieldValue("CreateDate", DateTime.Now);
        }
    }
}
