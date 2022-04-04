using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sCON_HOBBY
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

        private void ucCON_SHARECODE_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_SHARECODE.SetFieldValue("CREATE_DATE", DateTime.Now);
            ucCON_SHARECODE.SetFieldValue("UPDATE_DATE", DateTime.Now);
        }

        private void ucCON_SHARECODE_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCON_SHARECODE.SetFieldValue("UPDATE_DATE", DateTime.Now);
        }
    }
}
