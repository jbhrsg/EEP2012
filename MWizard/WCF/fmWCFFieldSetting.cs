using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;
using System.Data.Common;
using System.Linq;
using System.Collections; 

namespace MWizard
{
    public partial class fmWCFFieldSetting : Form
    {
        private DataSet dsSYS_REFVAL = new DataSet();
        private DataSet dsSYS_REFVAL_D1 = new DataSet();
        private DataSet dsSYS_REFVAL_D2 = new DataSet();
        private DataSet dsSYS_REFVAL_D3 = new DataSet();
        private ListView FListView;
        private TBlockFieldItem FSelectedBlockFieldItem;
        private ListViewItem FSelectedListViewItem;
        private Boolean FDisplayValue = false;
        private TWizardType FWizardType;
        private ListViewColumnSorter lvwColumnSorter;

        public fmWCFFieldSetting(ListView aListView, TWizardType aWizardType)
        {
            InitializeComponent();
            FListView = aListView;
            FSelectedBlockFieldItem = null;
            FSelectedListViewItem = null;
            FDisplayValue = false;
            FWizardType = aWizardType;
            InitData();

            lvwColumnSorter = new ListViewColumnSorter();
            this.lvFields.ListViewItemSorter = lvwColumnSorter;
        }

        private void InitData()
        {
            if (FWizardType == TWizardType.wtWinForm)
            {
                cbControlType.Items.Clear();
                cbControlType.Items.Add("TextBox");
                cbControlType.Items.Add("ComboBox");
                cbControlType.Items.Add("RefValBox");
                cbControlType.Items.Add("DateTimeBox");
                cbControlType.Items.Add("CheckBox");
            }

            lvFields.Items.Clear();
            foreach (ListViewItem ViewItem in FListView.Items)
            {
                TBlockFieldItem aFieldItem = (TBlockFieldItem)ViewItem.Tag;
                ListViewItem NewItem = new ListViewItem();
                NewItem.Text = aFieldItem.DataField;
                NewItem.SubItems.Add(aFieldItem.Description);
                NewItem.SubItems.Add(aFieldItem.CheckNull);
                NewItem.SubItems.Add(aFieldItem.DefaultValue);
                NewItem.SubItems.Add(aFieldItem.RefValNo);
                NewItem.SubItems.Add(aFieldItem.QueryMode);
                NewItem.SubItems.Add(aFieldItem.EditMask);
                NewItem.Tag = aFieldItem;
                lvFields.Items.Add(NewItem);
            }
        }

        public Boolean ShowRefValForm(String[] Params)
        {
            FDisplayValue = true;
            //tbCaption.Text = Params[0];
            //cbCheckNull.Text = Params[1];
            //if (cbCheckNull.Text == "")
            //    cbCheckNull.Text = "N";
            //tbDefaultValue.Text = Params[2];
            //cbControlType.Text = Params[3];
            //if (cbControlType.Text == "")
            //    cbControlType.Text = "TextBox";
            //cbRefValNo.Text = Params[4];
            //cbTableName.Text = Params[5];
            //cbDataTextField.Text = Params[6];
            //cbDataValueField.Text = Params[7];
            FDisplayValue = false;

            DialogResult DR = this.ShowDialog();
            if (DR == DialogResult.OK)
            {
                //Params[0] = tbCaption.Text;
                //Params[1] = cbCheckNull.Text;
                //Params[2] = tbDefaultValue.Text;
                //Params[3] = cbControlType.Text;
                //Params[4] = cbRefValNo.Text;
                //Params[5] = cbTableName.Text;
                //Params[6] = cbDataTextField.Text;
                //Params[7] = cbDataValueField.Text;
                return true;
            }
            else
                return false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /* 糤Common砞﹚┮ぃ浪琩
            if (lbTableName.Text == "")
                throw new Exception("Please select a table name !!");
            if (lvFieldName.SelectedItems == null)
                throw new Exception("Please select a field name !!");
             */ 
            DialogResult = DialogResult.OK;
        }

        private void cbControlType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbControlType.Text == "ComboBox")
            {
                gbComboBox.Enabled = true;
            }
            else
            {
                tbRemoteName.Text = String.Empty;
                tbTableName.Text = String.Empty;
                cbDataTextField.Text = String.Empty;
                cbDataValueField.Text = String.Empty;
                gbComboBox.Enabled = false;
            }
        }

        private void SetValue()
        {
            if (FSelectedBlockFieldItem == null)
                return;
            FSelectedBlockFieldItem.Description = tbCaption.Text;
            FSelectedBlockFieldItem.CheckNull = cbCheckNull.Text;
            FSelectedBlockFieldItem.DefaultValue = tbDefaultValue.Text;
            FSelectedBlockFieldItem.ControlType = cbControlType.Text;
            //FSelectedBlockFieldItem.ComboRemoteName = tbRemoteName.Text;
            FSelectedBlockFieldItem.ComboEntityName = tbTableName.Text;
            FSelectedBlockFieldItem.ComboTextField = cbDataTextField.Text;
            FSelectedBlockFieldItem.ComboValueField = cbDataValueField.Text;
            FSelectedBlockFieldItem.QueryMode = cbQueryMode.Text;
            FSelectedBlockFieldItem.EditMask = tbEditMask.Text;

            FSelectedListViewItem.SubItems[1].Text = FSelectedBlockFieldItem.Description;
            FSelectedListViewItem.SubItems[2].Text = FSelectedBlockFieldItem.CheckNull;
            FSelectedListViewItem.SubItems[3].Text = FSelectedBlockFieldItem.DefaultValue;
            FSelectedListViewItem.SubItems[4].Text = FSelectedBlockFieldItem.RefValNo;
            FSelectedListViewItem.SubItems[5].Text = FSelectedBlockFieldItem.QueryMode;
            FSelectedListViewItem.SubItems[6].Text = FSelectedBlockFieldItem.EditMask;

        }

        private void DisplayValue()
        {
            if (FSelectedBlockFieldItem == null)
                return;
            cbControlType.Text = "TextBox";
            tbCaption.Text = FSelectedBlockFieldItem.Description;
            cbCheckNull.Text = FSelectedBlockFieldItem.CheckNull;
            tbDefaultValue.Text = FSelectedBlockFieldItem.DefaultValue;
            cbControlType.Text = FSelectedBlockFieldItem.ControlType;
            //tbRemoteName.Text = FSelectedBlockFieldItem.ComboRemoteName;
            tbTableName.Text = FSelectedBlockFieldItem.ComboEntityName;
            cbDataTextField.Text = FSelectedBlockFieldItem.ComboTextField;
            cbDataValueField.Text = FSelectedBlockFieldItem.ComboValueField;
            cbQueryMode.Text = FSelectedBlockFieldItem.QueryMode;
            tbEditMask.Text = FSelectedBlockFieldItem.EditMask;
            //if (cbCheckNull.Text == "" || cbCheckNull.Text == null)
            //    cbCheckNull.Text = "N";
            if (cbControlType.Text == "" || cbControlType.Text == null)
                cbControlType.Text = "TextBox";
            if (cbQueryMode.Text == null || cbQueryMode.Text == "")
                cbQueryMode.Text = "None";
        }

        private void lvFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFields.SelectedItems.Count == 1)
            {
                ListViewItem aViewItem = lvFields.SelectedItems[0];
                FSelectedListViewItem = aViewItem;
                FSelectedBlockFieldItem = (TBlockFieldItem)aViewItem.Tag;
                FDisplayValue = true;
                DisplayValue();
                FDisplayValue = false;
            }
        }

        private void tbCaption_TextChanged(object sender, EventArgs e)
        {
            if (!FDisplayValue)
                SetValue();
        }

        private void lvFields_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 检查点击的列是不是现在的排序列.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwColumnSorter.OrderOfSort == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.OrderOfSort = System.Windows.Forms.SortOrder.Ascending;
            }

            // 用新的排序方法对ListView排序
            (sender as ListView).Sort();
        }

        private void tbTableName_TextChanged(object sender, EventArgs e)
        {
            if (tbTableName.Text != String.Empty)
            {
                //String strRemoteName = tbRemoteName.Text;
                //String strTableName = tbTableName.Text;
                //EFClientTools.Web.EFDataSource aEFDataSource = new EFClientTools.Web.EFDataSource();
                //aEFDataSource.RemoteName = strRemoteName;
                //aEFDataSource.DataMember = strTableName;
                //Type tEntityType = aEFDataSource.GetEntityType();
                //StringBuilder[] fileds = tEntityType.GetProperties().Where(p => (ExtTools.GloFix.IsPrimitive(p.PropertyType))).Select(p => new StringBuilder(p.Name)).ToArray();
                //cbDataTextField.Items.Clear();
                //cbDataTextField.Items.AddRange(fileds);
                //cbDataValueField.Items.Clear();
                //cbDataValueField.Items.AddRange(fileds);
            }
        }
    }
}
