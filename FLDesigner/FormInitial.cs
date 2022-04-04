using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FLDesigner
{
    public partial class FormInitial : Form
    {
        public FormInitial()
        {
            InitializeComponent();
        }

        const string REGISTRYNAME = "infolight\\eep.net2008";

        private void FormInitial_Load(object sender, EventArgs e)
        {
            textBoxWebClientPath.Text = Properties.Settings.Default.EFWebClientPath;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EFWebClientPath = textBoxWebClientPath.Text;
            Properties.Settings.Default.Save();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = textBoxWebClientPath.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxWebClientPath.Text = dialog.SelectedPath;
            }
        }
    }
}
