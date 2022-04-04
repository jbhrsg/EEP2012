using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using Srvtools;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace FLTools
{
    internal class NavigatorConverterDesigner : ComponentDesigner
    {
        public NavigatorConverterDesigner()
            : base()
        {
            DesignerVerb createVerb = new DesignerVerb("Set Default", new EventHandler(OnCreate));
            this.Verbs.Add(createVerb);
        }

        public void OnCreate(object sender, EventArgs e)
        {
            if (this.Component != null && this.Component is NavigatorConverter)
            {
                NavigatorConverter converter = (NavigatorConverter)this.Component;
                if (converter.FlowNavigator != null)
                {
                    IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
                    if (!containsItem("flowSeparator", converter.FlowNavigator))
                    {
                        ToolStripSeparator separator1 = host.CreateComponent(typeof(ToolStripSeparator), "flowSeparator") as ToolStripSeparator;
                        converter.FlowNavigator.Items.Add(separator1);
                    }
                    if (!containsItem("toolStripSubmitItem", converter.FlowNavigator))
                    {
                        ToolStripButton submitItem = CreatestripButton(host, "toolStripSubmitItem", "Submit") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(submitItem);
                        converter.FlowNavigator.SubmitItem = submitItem;
                    }
                    if (!containsItem("toolStripApproveItem", converter.FlowNavigator))
                    {
                        ToolStripButton approveItem = CreatestripButton(host, "toolStripApproveItem", "Approve") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(approveItem);
                        converter.FlowNavigator.ApproveItem = approveItem;
                    }
                    if (!containsItem("toolStripReturnItem", converter.FlowNavigator))
                    {
                        ToolStripButton returnItem = CreatestripButton(host, "toolStripReturnItem", "Return") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(returnItem);
                        converter.FlowNavigator.ReturnItem = returnItem;
                    }
                    if (!containsItem("toolStripRejectItem", converter.FlowNavigator))
                    {
                        ToolStripButton rejectItem = CreatestripButton(host, "toolStripRejectItem", "Reject") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(rejectItem);
                        converter.FlowNavigator.RejectItem = rejectItem;
                    }
                    if (!containsItem("toolStripNotifyItem", converter.FlowNavigator))
                    {
                        ToolStripButton notifyItem = CreatestripButton(host, "toolStripNotifyItem", "Notify") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(notifyItem);
                        converter.FlowNavigator.NotifyItem = notifyItem;
                    }
                    if (!containsItem("toolStripFlowDeleteItem", converter.FlowNavigator))
                    {
                        ToolStripButton deleteItem = CreatestripButton(host, "toolStripFlowDeleteItem", "FlowDelete") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(deleteItem);
                        converter.FlowNavigator.FlowDeleteItem = deleteItem;
                    }
                    if (!containsItem("toolStripPlusItem", converter.FlowNavigator))
                    {
                        ToolStripButton plusItem = CreatestripButton(host, "toolStripPlusItem", "Plus") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(plusItem);
                        converter.FlowNavigator.PlusItem = plusItem;
                    }
                    if (!containsItem("toolStripPauseItem", converter.FlowNavigator))
                    {
                        ToolStripButton pauseItem = CreatestripButton(host, "toolStripPauseItem", "Pause") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(pauseItem);
                        converter.FlowNavigator.PauseItem = pauseItem;
                    }
                    if (!containsItem("toolStripCommentItem", converter.FlowNavigator))
                    {
                        ToolStripButton commentItem = CreatestripButton(host, "toolStripCommentItem", "Comment") as ToolStripButton;
                        converter.FlowNavigator.Items.Add(commentItem);
                        converter.FlowNavigator.CommentItem = commentItem;
                    }

                    converter.FlowNavigator.InitializeFlowStates();
                }
            }
        }

        private bool containsItem(string itemName, FLNavigator navigator)
        {
            foreach (ToolStripItem item in navigator.Items)
            {
                if (item.Name == itemName)
                    return true;
            }
            return false;
        }

        private ToolStripItem CreatestripButton(IDesignerHost host, string buttonName, string buttonText)
        {
            Assembly assembly = this.Component.GetType().Assembly;
            ToolStripButton item = host.CreateComponent(typeof(ToolStripButton), buttonName) as ToolStripButton;
            Stream s = assembly.GetManifestResourceStream("FLTools.FLNavigator." + buttonText + ".png");
            if (s != null)
            {
                Bitmap bitmap = new Bitmap(s);
                bitmap.MakeTransparent(Color.Magenta);
                item.Image = bitmap;
                item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            }
            else
            {
                item.DisplayStyle = ToolStripItemDisplayStyle.Text;
            }
            item.Text = buttonText;

            return item;
        }
    }

    [ToolboxItem(true)]
    [Designer(typeof(NavigatorConverterDesigner), typeof(IDesigner))]
    public class NavigatorConverter : InfoBaseComp
    {
        public NavigatorConverter()
        { }

        private FLNavigator _flowNavigator;
        public FLNavigator FlowNavigator
        {
            get
            {
                return _flowNavigator;
            }
            set
            {
                _flowNavigator = value;
            }
        }
    }
}
