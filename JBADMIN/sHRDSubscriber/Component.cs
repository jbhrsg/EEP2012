using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sHRDSubscriber
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

        private void ucSubscriberMail_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucSubscriberMail.SetFieldValue("dataFromDate", DateTime.Now);
        }
    }
}
