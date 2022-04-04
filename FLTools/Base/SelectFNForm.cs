using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Srvtools;
using Microsoft.Win32;

namespace FLTools.Base
{
    public partial class SelectFNForm : Form
    {
        private string _formName;
        private string _clientDll;

        private string _path;

        public SelectFNForm(string formName, string clientDll)
        {
            InitializeComponent();
            _formName = formName;
            _clientDll = clientDll;
        }

        private void SelectFNForm_Load(object sender, EventArgs e)
        {
            txtClientDll.Text = _clientDll;

            _path = EEPRegistry.Client;
        }

        private string[] GetFormNameByClientDll(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            try
            {
                FileInfo fileInfo = new FileInfo(_clientDll);
                string dllName = fileInfo.Name.Split('.')[0];

                List<string> list = new List<string>();
                Assembly assembly = Assembly.LoadFile(path);
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.BaseType == typeof(InfoForm))
                    {
                        list.Add(dllName + "." + type.Name);
                    }
                }

                return list.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void btnChooseDll_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = _path;
            dialog.Filter = "*.dll | *.dll";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtClientDll.Text = dialog.FileName;
            }
        }

        private void txtClientDll_TextChanged(object sender, EventArgs e)
        {
            _clientDll = txtClientDll.Text;

            lbxFormNames.Items.Clear();
            string[] items = GetFormNameByClientDll(txtClientDll.Text);
            if (items != null && items.Length != 0)
            {
                lbxFormNames.Items.AddRange(items);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtClientDll.Text != string.Empty && txtClientDll.Text != string.Empty && lbxFormNames.Text != string.Empty && lbxFormNames.Text != string.Empty)
            {
                _clientDll = txtClientDll.Text;
                _formName = lbxFormNames.Text;
            }
            this.Close();
        }


        private void lbxFormNames_DoubleClick(object sender, EventArgs e)
        {
            if (lbxFormNames.Text != null && lbxFormNames.Text != string.Empty)
            {
                btnOK_Click(null, null);
            }
        }

        public string FormName
        {
            get
            {
                return _formName;
            }
        }

        public string ClientDll
        {
            get
            {
                return _clientDll;
            }
        }
    }
}