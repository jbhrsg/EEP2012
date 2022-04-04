using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sCON_CENTER
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

        private void ucCON_CENTER_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_CENTER.SetFieldValue("CREATE_DATE", DateTime.Now);
            ucCON_CENTER.SetFieldValue("UPDATE_DATE", DateTime.Now);
        }

        private void ucCON_CENTER_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCON_CENTER.SetFieldValue("UPDATE_DATE", DateTime.Now);
        }

        private void ucCON_CENTER_AUTHORITY_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_CENTER_AUTHORITY.SetFieldValue("CREATE_DATE", DateTime.Now);
            ucCON_CENTER_AUTHORITY.SetFieldValue("UPDATE_DATE", DateTime.Now);
        }

        private void ucCON_CENTER_AUTHORITY_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCON_CENTER_AUTHORITY.SetFieldValue("UPDATE_DATE", DateTime.Now);
        }
    }
}
