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
    public partial class FLWebNavigatorVisibleControlsEditorDialog : Form
    {
        public string VisibleControls;
        private FLWebNavigator Navigator = null;

        public FLWebNavigatorVisibleControlsEditorDialog(string visibleControls, FLWebNavigator navigator)
        {
            InitializeComponent();

            //foreach (string ctrl in visibleControls)
            //{
            //    VisibleControls.Add(ctrl);
            //}
            VisibleControls = visibleControls;

            Navigator = navigator;
        }

        private bool IsFlowNavigatorItem(ControlItem item)
        {
            if (item.Name != "Submit"
                && item.Name != "Approve"
                && item.Name != "Return"
                && item.Name != "Reject"
                && item.Name != "Notify"
                && item.Name != "FlowDelete"
                && item.Name != "Plus"
                && item.Name != "Pause"
                && item.Name != "Comment")
            {
                return false;
            }
            return true;
        }

        private bool containsItem(string itemName)
        {
            if (VisibleControls != null && VisibleControls != "")
            {
                string[] vcs = VisibleControls.Split(';');
                foreach (string vc in vcs)
                {
                    if (vc == itemName)
                        return true;
                }
            }
            return false;
        }

        private void VisibleControlsEditorDialog_Load(object sender, EventArgs e)
        {
            foreach (ControlItem item in Navigator.NavControls)
            {
                if (item.Name != null && item.Name.Trim() != ""
                    && IsFlowNavigatorItem(item))
                {
                    if (containsItem(item.Name))
                    {
                        this.lbxVisibleControls.Items.Add(item.Name);
                    }
                    else
                    {
                        this.lbxInVisibleControls.Items.Add(item.Name);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            VisibleControls = "";
            foreach (object obj in this.lbxVisibleControls.Items)
            {
                VisibleControls += obj.ToString() + ";";
            }
            VisibleControls = VisibleControls.Substring(0, VisibleControls.LastIndexOf(';'));
        }

        private void btnMoveAllFromVisibleToInVisible_Click(object sender, EventArgs e)
        {
            for (int index = this.lbxVisibleControls.Items.Count - 1; index >= 0; --index)
            {
                this.lbxInVisibleControls.Items.Add(this.lbxVisibleControls.Items[index]);
                this.lbxVisibleControls.Items.RemoveAt(index);
            }
        }

        private void btnMoveAllFromInVisibleToVisible_Click(object sender, EventArgs e)
        {
            for (int index = this.lbxInVisibleControls.Items.Count - 1; index >= 0; --index)
            {
                this.lbxVisibleControls.Items.Add(this.lbxInVisibleControls.Items[index]);
                this.lbxInVisibleControls.Items.RemoveAt(index);
            }
        }

        private void btnMoveFromVisibleToInVisible_Click(object sender, EventArgs e)
        {
            int index = this.lbxVisibleControls.SelectedIndex;
            if (index != -1)
            {
                this.lbxInVisibleControls.Items.Add(this.lbxVisibleControls.Items[index]);
                this.lbxVisibleControls.Items.RemoveAt(index);

                if (index < this.lbxVisibleControls.Items.Count)
                {
                    this.lbxVisibleControls.SelectedIndex = index;
                }
                else
                {
                    this.lbxVisibleControls.SelectedIndex = this.lbxVisibleControls.Items.Count - 1;
                }

                this.lbxInVisibleControls.SelectedIndex = this.lbxInVisibleControls.Items.Count - 1;
            }
        }

        private void btnMoveFromInVisibleToVisible_Click(object sender, EventArgs e)
        {
            int index = this.lbxInVisibleControls.SelectedIndex;
            if (index != -1)
            {
                this.lbxVisibleControls.Items.Add(this.lbxInVisibleControls.Items[index]);
                this.lbxInVisibleControls.Items.RemoveAt(index);

                if (index < this.lbxInVisibleControls.Items.Count)
                {
                    this.lbxInVisibleControls.SelectedIndex = index;
                }
                else
                {
                    this.lbxInVisibleControls.SelectedIndex = this.lbxInVisibleControls.Items.Count - 1;
                }
                this.lbxVisibleControls.SelectedIndex = this.lbxVisibleControls.Items.Count - 1;
            }
        }

        private void lbxVisibleControls_DoubleClick(object sender, EventArgs e)
        {
            this.btnMoveFromVisibleToInVisible.PerformClick();
        }

        private void lbxInVisibleControls_DoubleClick(object sender, EventArgs e)
        {
            this.btnMoveFromInVisibleToVisible.PerformClick();
        }
    }
}