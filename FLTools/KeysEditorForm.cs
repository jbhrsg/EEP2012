using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;
using FLCore;

namespace FLTools
{
    public partial class KeysEditorForm : Form
    {
        private string _keys = string.Empty;
        private IFLRootActivity _root;

        public KeysEditorForm()
        {
            InitializeComponent();
        }

        public KeysEditorForm(string keys,  IFLRootActivity root)
            : this()
        {
            _keys = keys;
            _root = root;
        }

        public string Keys
        {
            get
            {
                return _keys;
            }
        }

        private void KeysEditorForm_Load(object sender, EventArgs e)
        {
            string tableName = ((IFLRootActivity)_root).TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                return;
            }

            List<string> items2 = new List<string>();
            List<string> temp = new List<string>();
            if (!string.IsNullOrEmpty(_keys) && _keys.Length > 0)
            {
                string[] ss = _keys.Split(",".ToCharArray());
                foreach (string s in ss)
                {
                    if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(s.Trim()) && !temp.Contains(s.Trim().ToUpper()))
                    {
                        items2.Add(s.Trim());
                        temp.Add(s.Trim().ToUpper());
                    }
                }
            }

            object[] objs = CliUtils.CallMethod("GLModule", "GetRefRoles", new object[] { tableName });
            if (objs[0].ToString() == "0")
            {
                string[] items1 = (string[])objs[1];
                foreach (string item in items1)
                {
                    if (!items2.Contains(item))
                    {
                        listBox1.Items.Add(item);
                    }
                }

                foreach (string item in items2)
                {
                    listBox2.Items.Add(item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string keys = string.Empty;
            foreach (object obj in listBox2.Items)
            {
                if (keys.Length > 0)
                {
                    keys += ",";
                }

                keys += obj.ToString();
            }

            _keys = keys;

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (object obj in listBox1.SelectedItems)
            {
                list.Add(obj.ToString());
            }

            foreach (string item in list)
            {
                listBox1.Items.Remove(item);

                if (!listBox2.Items.Contains(item))
                {
                    listBox2.Items.Add(item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (object obj in listBox2.SelectedItems)
            {
                list.Add(obj.ToString());
            }

            foreach (string item in list)
            {
                listBox2.Items.Remove(item);

                if (!listBox1.Items.Contains(item))
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            button2_Click(null, null);
        }
    }
}
