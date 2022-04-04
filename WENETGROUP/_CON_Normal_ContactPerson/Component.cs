using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;

namespace _CON_Normal_ContactPerson
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_PERSON, logCON_CONTACT_PERSON, "CONTACT_PERSON_ID");  //LogInfo修正
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_PERSON);                                             //FieldAttr修正
        }

        public Component(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_PERSON, logCON_CONTACT_PERSON, "CONTACT_PERSON_ID");  //LogInfo修正
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_PERSON);                                             //FieldAttr修正
        }
    }
}
