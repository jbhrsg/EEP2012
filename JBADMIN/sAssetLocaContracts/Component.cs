using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sAssetLocaContracts
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

        private void ucAssetLocaContracts_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucAssetLocaContracts.SetFieldValue("CreateDate", DateTime.Now);
            ucAssetLocaContracts.SetFieldValue("LastUpdateDate", DateTime.Now);

        }

        private void ucAssetLocaContracts_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucAssetLocaContracts.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
    }
}
