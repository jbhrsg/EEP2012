using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FLTools
{
    public partial class FLWebNavigatorStateCollectionEditorDialog : Form
    {
        public FLWebNavigatorStateCollection Collection = null;

        public FLWebNavigatorStateCollectionEditorDialog(FLWebNavigatorStateCollection collection)
        {
            InitializeComponent();

            Collection = new FLWebNavigatorStateCollection(collection.Owner, collection.ItemType);
            foreach (FLWebNavigatorStateItem stateItem in collection)
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
                    || stateItem.StateText == "FSubmit"
                    || stateItem.StateText == "RSubmit")
                {
                    foreach (FLWebNavigatorStateItem item in Collection)
                    {
                        if (item.StateText == stateItem.StateText)
                        {
                            item.Collection = Collection;
                            //foreach (string ctrlName in stateItem.VisibleControls)
                            //{
                            //    item.VisibleControls.Add(ctrlName);
                            //}
                            item.VisibleControls = stateItem.VisibleControls;
                            item.Name = stateItem.Name;
                            item.Description = stateItem.Description;
                            break;
                        }
                    }
                }
                else
                {
                    FLWebNavigatorStateItem item = new FLWebNavigatorStateItem();
                    Collection.Add(item);

                    item.Collection = Collection;
                    //foreach (string ctrlName in stateItem.VisibleControls)
                    //{
                    //    item.VisibleControls.Add(ctrlName);
                    //}
                    item.VisibleControls = stateItem.VisibleControls;
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
            foreach (FLWebNavigatorStateItem stateItem in Collection)
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
                        || this.lbxStates.Items[index].ToString() == "FSubmit"
                        || this.lbxStates.Items[index].ToString() == "RSubmit")
                {
                    MessageBox.Show("Default FLWebNavigatorStateItem can not be removed");
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
            FLWebNavigatorStateItem stateItem = new FLWebNavigatorStateItem();

            // Determine StateText
            bool stateTextExists = true;
            int loopCounter = 0;
            while (stateTextExists)
            {
                loopCounter++;
                stateTextExists = false;
                foreach (FLWebNavigatorStateItem si in Collection)
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