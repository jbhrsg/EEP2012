using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sAssetLocation
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

        private void ucAssetLocation_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucAssetLocation.SetFieldValue("CreateDate", DateTime.Now);
        }

        private void ucAssetLocaContDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucAssetLocaContDetails.SetFieldValue("CreateDate", DateTime.Now);
        }
    }
}
