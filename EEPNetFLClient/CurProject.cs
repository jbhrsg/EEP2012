using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;

namespace EEPNetFLClient
{
    public partial class CurProject : Form
    {
        private frmClientMain MainForm;
        public CurProject(frmClientMain MainForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
        }

        private void CurProject_Load(object sender, EventArgs e)
        {
            this.infoCmbSolution.SelectedValue = CliUtils.fCurrentProject;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int length = MainForm.MdiChildren.Length;
            for (int i = 0; i < length; i++)
            {
                MainForm.MdiChildren[0].Close();
                if (i + MainForm.MdiChildren.Length >= length)
                {
                    return;
                }
            }

            //CliUtils.fCurrentProject = this.infoCmbSolution.SelectedValue.ToString();
            this.MainForm.cmbSolution.SelectedValue = this.infoCmbSolution.SelectedValue;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}