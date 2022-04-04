using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FLTools
{
    public partial class FLWizardEditorDialog : Form
    {
        public FLWizardEditorDialog()
        {
            InitializeComponent();
        }

        internal Dictionary<string, bool> Fields = new Dictionary<string,bool>();
        internal IDesignerHost DesignerHost = null;
        internal FLWizard Component = null;

        private void FLWizardEditorDialog_Load(object sender, EventArgs e)
        {
            if (this.Component == null || this.Component.BindingObject == null)
            {
                MessageBox.Show("please set properties!");
                this.Close();
                return;
            }
            else
            {
                if (this.Fields.Count > 0)
                {
                    foreach (KeyValuePair<string, bool> pair in this.Fields)
                    {
                        this.lstFields.Items.Add(pair.Key, pair.Value);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.Component.BindingObject;
            foreach (object item in this.lstFields.Items)
            {
                string field = item.ToString();
                if (this.lstFields.CheckedItems.Contains(item))
                {
                    if (!ContainsColumn(dgv.Columns, field))
                    {
                        DataGridViewTextBoxColumn column = this.DesignerHost.CreateComponent(
                            typeof(DataGridViewTextBoxColumn), 
                            string.Format("col{0}", field)) as DataGridViewTextBoxColumn;
                        PropertyDescriptor pdHeaderText = TypeDescriptor.GetProperties(column)["HeaderText"];
                        pdHeaderText.SetValue(column, field);
                        PropertyDescriptor pdDataPropertyName = TypeDescriptor.GetProperties(column)["DataPropertyName"];
                        pdDataPropertyName.SetValue(column, field);
                        dgv.Columns.Add(column);
                    }
                }
                else
                {
                    if (ContainsColumn(dgv.Columns, field))
                    {
                        RemoveColumn(dgv.Columns, field);
                    }
                }
            }
            this.Close();
        }

        private bool ContainsColumn(DataGridViewColumnCollection collection, string bindingField)
        {
            foreach (DataGridViewColumn column in collection)
            {
                if (string.Compare(column.DataPropertyName, bindingField, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveColumn(DataGridViewColumnCollection collection, string bindingField)
        {
            int rmvIndex = -1;
            foreach (DataGridViewColumn column in collection)
            {
                if (string.Compare(column.DataPropertyName, bindingField, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    rmvIndex = column.Index;
                    break;
                }
            }
            if (rmvIndex != -1)
            {
                this.DesignerHost.DestroyComponent(collection[rmvIndex]);
                collection.RemoveAt(rmvIndex);
            }
        }
    }
}
