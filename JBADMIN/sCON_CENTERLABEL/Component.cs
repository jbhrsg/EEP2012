using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sCON_CENTERLABEL
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

        private void ucCON_CENTERLABEL_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_CENTERLABEL.SetFieldValue("CREATE_DATE", DateTime.Now);
        }
    }
}
