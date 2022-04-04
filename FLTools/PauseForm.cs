using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    public partial class PauseForm : Form
    {
        public PauseForm()
        {
            InitializeComponent();
        }

        private string _orgKind = "";
        public string OrgKind
        {
            get { return _orgKind; }
        }

        private void PauseForm_Load(object sender, EventArgs e)
        {
            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "select * from SYS_ORGKIND" });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                this.cmbOrgKind.DataSource = ((DataSet)ret1[1]).Tables[0];
                this.cmbOrgKind.ValueMember = "ORG_KIND";
                this.cmbOrgKind.DisplayMember = "KIND_DESC";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this._orgKind = this.cmbOrgKind.SelectedValue.ToString();
        }
    }
}