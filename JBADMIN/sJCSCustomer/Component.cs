using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sJCSCustomer
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

        private void ucEmployer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployer.SetFieldValue("LastUpdateDate", DateTime.Now);//逆结
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployer.SetFieldValue("LastupdateBy", LoginUser);//逆结
        }

        private void ucEmployer1_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployer1.SetFieldValue("LastUpdateDate", DateTime.Now);//逆结
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployer1.SetFieldValue("LastupdateBy", LoginUser);//逆结
        }

        private void ucEmployer2_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployer2.SetFieldValue("LastUpdateDate", DateTime.Now);//逆结
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployer2.SetFieldValue("LastupdateBy", LoginUser);//逆结
        }
    }
}
