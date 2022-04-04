using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;


namespace sPO_POType
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

        private void ucPOType_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucPOType.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucPOType_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucPOType.SetFieldValue("CreateDate", DateTime.Now);
            ucPOType.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
    }
}
