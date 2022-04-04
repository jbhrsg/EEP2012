using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sERPDispatchArea
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

        private void ucERPDispatchAreaID_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPDispatchAreaID.SetFieldValue("CreateDate", DateTime.Now);
            ucERPDispatchAreaID.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucERPDispatchAreaID_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucERPDispatchAreaID.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
    }
}
