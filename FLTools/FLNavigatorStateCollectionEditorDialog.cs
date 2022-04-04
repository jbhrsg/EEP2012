using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FLTools
{
    public partial class FLNavigatorStateCollectionEditorDialog : Form
    {
        public FLNavigatorStateCollection Collection = null;

        public FLNavigatorStateCollectionEditorDialog(FLNavigatorStateCollection collection)
        {
            InitializeComponent();

            Collection = new FLNavigatorStateCollection(collection.Owner, collection.ItemType);
            foreach (FLNavigatorStateItem stateItem in collection)
            {
                if (stateItem.StateText == "Approve"
                    || stateItem.StateText == "Continue"
                    || stateItem.StateText == "Inquery"
                    || stateItem.StateText == "Notify"
                    || stateItem.StateText == "Return"
                    || stateItem.StateText == "Submit"
                    || stateItem.StateText == "None"
                    || stateItem.StateText == "Plus"
                    || stateItem.StateText == "Lock"
                    || stateItem.StateText == "RSubmit"
                    || stateItem.StateText == "FSubmit")
                {
                    foreach (FLNavigatorStateItem item in Collection)
                    {
                        if (item.StateText == stateItem.StateText)
                        {
                            item.Collection = Collection;
                            foreach (string ctrlName in stateItem.VisibleControls)
                            {
                                item.VisibleControls.Add(ctrlName);
                            }
                            item.Name = stateItem.Name;
                            item.Description = stateItem.Description;
                            break;
                        }
                    }
                }
                else
                {
                    FLNavigatorStateItem item = new FLNavigatorStateItem();
                    Collection.Add(item);

                    item.Collection = Collection;
                    foreach (string ctrlName in stateItem.VisibleControls)
                    {
                        item.VisibleControls.Add(ctrlName);
                    }
                    item.Name = stateItem.Name;
                    item.StateText = stateItem.StateText;
                    item.Description = stateItem.Description;
                }
            }
        }

        private void StateCollectionEditorDialog_Load(object sender, EventArgs e)
        {
            RefreshStateItem();
            this.lbxStates.SelectedIndex = 0;
        }

        private void RefreshStateItem()
        {
            lbxStates.Items.Clear();
            foreach (FLNavigatorStateItem stateItem in Collection)
            {
                lbxStates.Items.Add(stateItem.StateText);
            }
        }

        private void lbxStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbxStates.SelectedIndex;
            if (index != -1)
            {
                this.pgStateItem.SelectedObject = this.Collection.GetItem(index);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = this.lbxStates.SelectedIndex;
            if (index != -1)
            {
                if (this.lbxStates.Items[index].ToString() == "Approve"
                        || this.lbxStates.Items[index].ToString() == "Continue"
                        || this.lbxStates.Items[index].ToString() == "Inquery"
                        || this.lbxStates.Items[index].ToString() == "Notify"
                        || this.lbxStates.Items[index].ToString() == "Return"
                        || this.lbxStates.Items[index].ToString() == "Submit"
                        || this.lbxStates.Items[index].ToString() == "None"
                        || this.lbxStates.Items[index].ToString() == "Plus"
                        || this.lbxStates.Items[index].ToString() == "Lock"
                        || this.lbxStates.Items[index].ToString() == "RSubmit"
                        || this.lbxStates.Items[index].ToString() == "FSubmit")
                {
                    MessageBox.Show("Default FLNavigatorStateItem can not be removed");
                    return;
                }
                this.lbxStates.Items.RemoveAt(index);
                Collection.RemoveAt(index);
                if (index < this.lbxStates.Items.Count)
                {
                    this.lbxStates.SelectedIndex = index;
                }
                else
                {
                    this.lbxStates.SelectedIndex = this.lbxStates.Items.Count - 1;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FLNavigatorStateItem stateItem = new FLNavigatorStateItem();

            // Determine StateText
            bool stateTextExists = true;
            int loopCounter = 0;
            while (stateTextExists)
            {
                loopCounter++;
                stateTextExists = false;
                foreach (FLNavigatorStateItem si in Collection)
                {
                    if (si.StateText == "State" + loopCounter.ToString())
                    {
                        stateTextExists = true;
                        break;
                    }
                }
            }
            stateItem.StateText = "State" + loopCounter.ToString();
            stateItem.Description = stateItem.StateText;

            Collection.Add(stateItem);
            this.lbxStates.Items.Add(stateItem.StateText);
            this.lbxStates.SelectedIndex = this.lbxStates.Items.Count - 1;
        }

    }
}