using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sglVoucher
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
        //傳票編號=> ex: FWA1050001--A1050108010--A+民國年+月+日+3碼流水號=>A1051025006
        public string OrderNoFixed()
        {
            DateTime datetime = DateTime.Today;
            return "A" + datetime.ToShortDateString();//(datetime.Year - 1911).ToString().Trim();
        }
    }
}
