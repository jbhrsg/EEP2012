using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace FLTools
{
    public class FLRunTimeSubActivity:WebControl,INamingContainer
    {
        public FLRunTimeSubActivity()
        {
        }
        private Button btText = new Button();
        private System.Web.UI.WebControls.Image icon = new System.Web.UI.WebControls.Image();
        public string Name
        {
            get
            {
                EnsureChildControls();
                return btText.Text;
            }
            set
            {
                EnsureChildControls();
                btText.Text = value;
            }
        }

        public string Description
        {
            get
            {
                object o = ViewState["Description"];
                if (o != null)
                {
                    return (string)o;
                }
                else
                {
                    return "" ;
                }
            }
            set { ViewState["Description"] = value; }
        }

        public int Layer
        {
            get
            {
                object o = ViewState["Layer"];
                if (o != null)
                {
                    return (int)o;
                }
                else
                {
                    return -1;
                }
            }
            set { ViewState["Layer"] = value; }
        }

        public bool Selected
        {
            get { 
                object o = ViewState["Selected"];
                if (o != null)
                {
                    return (bool)o;
                }
                else
                {
                    return false;
                }
            }
            set { ViewState["Selected"] = value; }
        }

        public List<string> FLStandProperty
        {
            get
            {
                object o = ViewState["FLStandProperty"];
                if (o != null)
                {
                    return (List<string>)o;
                }
                else
                {
                    return new List<string>(); ;
                }
            }
            set { ViewState["FLStandProperty"] = value; }

        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            
            btText = new Button();
            
            btText.Text = "btText";
            btText.CommandName = "Select";
            btText.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

            icon = new System.Web.UI.WebControls.Image();
            
            icon.ImageUrl = "~/Image/FL/FLStand.png";

            this.Controls.Add(btText);
            this.Controls.Add(icon);
            ChildControlsCreated = true;
        }

        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            bool handled = false;
            if (args is CommandEventArgs)
            {
                CommandEventArgs ce = (CommandEventArgs)args;
                if (ce.CommandName == "Select")
                {
                    OnOKClick(EventArgs.Empty);                 
                    handled = true;
                }
            }
            return handled;
        }

        void setButton()
        {
            btText.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
            btText.BackColor = Color.DeepSkyBlue;
            this.Selected = true;
        }

        public void isSelected()
        {
            this.Selected = true;
            setButton();
        }

        #region event OKClick
        public event EventHandler OKClick
        {
            add { Events.AddHandler(EventOKClick, value); }
            remove { Events.RemoveHandler(EventOKClick, value); }
        }

        private static readonly object EventOKClick = new object();

        protected virtual void OnOKClick(EventArgs e)
        {
            EventHandler OKClickHandler = (EventHandler)Events[EventOKClick];
            if (OKClickHandler != null)
            {
                OKClickHandler(this, e);
            }
        }
        #endregion

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Div);//<div>
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "1");
            writer.AddAttribute(HtmlTextWriterAttribute.Bordercolor, "Gray");

            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "float: left;");
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
            writer.RenderBeginTag(HtmlTextWriterTag.Table); // <table>
            
            writer.RenderBeginTag(HtmlTextWriterTag.Tr); // <tr>
            
            writer.RenderBeginTag(HtmlTextWriterTag.Td); // <td>
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "height:20px;width:20px;");
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
            icon.RenderControl(writer);
            writer.RenderEndTag();  // </td>

            writer.RenderBeginTag(HtmlTextWriterTag.Td); // <td>
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "height:30px");
            writer.AddAttribute(HtmlTextWriterAttribute.Bgcolor, "white");
            btText.RenderControl(writer);
            writer.RenderEndTag();  // </td>

            writer.RenderEndTag();  // </tr>

            writer.RenderEndTag();  // </table>
            writer.RenderEndTag();  // </div>
        }
    }
}
