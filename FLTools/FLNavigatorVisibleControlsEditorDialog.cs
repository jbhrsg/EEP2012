using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FLTools
{
    public partial class FLNavigatorVisibleControlsEditorDialog : Form
    {
        public List<string> VisibleControls = new List<string>();
        private BindingNavigator Navigator = null;

        public FLNavigatorVisibleControlsEditorDialog(List<string> visibleControls, BindingNavigator navigator)
        {
            InitializeComponent();

            foreach (string ctrl in visibleControls)
            {
                VisibleControls.Add(ctrl);
            }

            Navigator = navigator;
        }

        private bool IsFlowNavigatorItem(ToolStripItem item)
        {
            if (item.Name != "toolStripSubmitItem"
                && item.Name != "toolStripApproveItem"
                && item.Name != "toolStripReturnItem"
                && item.Name != "toolStripRejectItem"
                && item.Name != "toolStripNotifyItem"
                && item.Name != "toolStripFlowDeleteItem"
                && item.Name != "toolStripPlusItem"
                && item.Name != "toolStripPauseItem"
                && item.Name != "toolStripCommentItem")
            {
                return false;
            }
            return true;
        }

        private void VisibleControlsEditorDialog_Load(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in Navigator.Items)
            {
                if (!(item is ToolStripSeparator)
                    && item.Name != null && item.Name.Trim() != ""
                    && IsFlowNavigatorItem(item))
                {
                    if (VisibleControls.Contains(item.Name))
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
            VisibleControls.Clear();
            foreach (object obj in this.lbxVisibleControls.Items)
            {
                VisibleControls.Add(obj.ToString());
            }
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