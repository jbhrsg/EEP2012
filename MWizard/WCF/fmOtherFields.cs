using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MWizard.WCF
{
    public partial class fmOtherFields : Form
    {
        private System.Windows.Forms.ComboBox.ObjectCollection comboObjectCollection;
        public String strCheckedItems;
        public fmOtherFields()
        {
            InitializeComponent();
        }

        public fmOtherFields(System.Windows.Forms.ComboBox.ObjectCollection oc, String checkedItems)
        {
            InitializeComponent();
            comboObjectCollection = oc;
            strCheckedItems = checkedItems;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            strCheckedItems = String.Empty;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++ )
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    strCheckedItems += this.checkedListBox1.Items[i].ToString() + ";";
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void fmOtherFields_Load(object sender, EventArgs e)
        {
            String[] checkedItems = new String[1];
            if (!String.IsNullOrEmpty(this.strCheckedItems))
                checkedItems = this.strCheckedItems.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            this.checkedListBox1.Items.Clear();
            foreach (object obj in comboObjectCollection)
            {
                bool isChecked = false;
                foreach (String item in checkedItems)
                {
                    if (item == obj.ToString())
                    {
                        isChecked = true;
                        break;
                    }
                }
                this.checkedListBox1.Items.Add(obj.ToString(), isChecked);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            ChangeCheckListBox(true);
        }

        private void btnAllCancel_Click(object sender, EventArgs e)
        {
            ChangeCheckListBox(false);
        }

        private void btnAntiSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, !this.checkedListBox1.GetItemChecked(i));
            }
        }

        private void ChangeCheckListBox(bool isChecked)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, isChecked);
            }
        }
    }
}
