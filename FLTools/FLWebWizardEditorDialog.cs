using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
#if VS90
using WebDevPage = Microsoft.VisualWebDeveloper.Interop.WebDeveloperPage;
#endif

namespace FLTools
{
    public partial class FLWebWizardEditorDialog : Form
    {
        public FLWebWizardEditorDialog()
        {
            InitializeComponent();
        }
#if VS90
        internal WebDevPage.DesignerDocument DesignerDocument = null;
#endif
        internal Dictionary<string, bool> Fields = new Dictionary<string, bool>();
        internal FLWebWizard Component = null;

        private void FLWebWizardEditorDialog_Load(object sender, EventArgs e)
        {
            if (this.Component == null || string.IsNullOrEmpty(this.Component.BindingObjectID))
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

#if VS90
        private void SetColumnValue(WebDevPage.IHTMLElement gridElement, string field)
        {
            string collXml = string.Format("<asp:BoundField DataField='{0}' HeaderText='{0}'/>", field);
            string html = gridElement.innerHTML;
            int insertPos = 0;
            int indexBegin = this.IndexOfBeginTag(html, "Columns");
            int length;
            if (indexBegin > 0)
            {
                insertPos = this.IndexOfEndTag(html, "Columns", out length);
            }
            else
            {
                collXml = "<Columns>" + collXml + "</Columns>";
            }
            gridElement.innerHTML = html.Insert(insertPos, collXml);
        }
#endif

        private int IndexOfBeginTag(string html, string tag)
        {
            Match mc = Regex.Match(html, string.Format(@"<{0}\s*>", tag));
            if (mc.Success)
            {
                return mc.Index;
            }
            else
            {
                return -1;
            }
        }

        private int IndexOfEndTag(string html, string tag)
        {
            int length;
            return IndexOfEndTag(html, tag, out length);
        }

        private int IndexOfEndTag(string html, string tag, out int length)
        {
            Match mc = Regex.Match(html, string.Format(@"</{0}\s*>", tag), RegexOptions.RightToLeft);
            if (mc.Success)
            {
                length = mc.Length;
                return mc.Index;
            }
            else
            {
                length = 0;
                return -1;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
#if VS90
            WebDevPage.IHTMLElement gridElement = DesignerDocument.webControls.item(Component.BindingObjectID, 0) as WebDevPage.IHTMLElement;
            gridElement.innerHTML = "";
            foreach (object item in this.lstFields.Items)
            {
                if (this.lstFields.CheckedItems.Contains(item))
                {
                    this.SetColumnValue(gridElement, item.ToString());
                }
            }
            this.Close();
#endif
        }
    }
}
