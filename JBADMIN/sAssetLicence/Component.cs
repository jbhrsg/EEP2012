using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sAssetLicence
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

        private void ucAssetLicence_BeforeApply(object sender, UpdateComponentBeforeApplyEventArgs e)
        {
            ucAssetLicence.SetFieldValue("CreateDate", DateTime.Now);
        }

        private void ucAssetLicence_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucAssetLicence.SetFieldValue("CreateDate", DateTime.Now);
        }
    }
}
