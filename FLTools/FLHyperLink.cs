using System.Workflow.ComponentModel;
using FLCore;
using FLTools.Base;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Design;
using Microsoft.Win32;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLHyperLink), "Resources.FLHyperLink.png")]
    public class FLHyperLink : Activity, INonEventWaiting, IFLHyperLinkActivity
    {
        #region properites
        private string _formName;
        [Editor(typeof(FormNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FormName
        {
            get
            {
                return _formName;
            }
            set
            {
                _formName = value;
            }
        }

        private string _webFormName;
        [Editor(typeof(WebFormNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string WebFormName
        {
            get
            {
                return _webFormName;
            }
            set
            {
                _webFormName = value;
            }
        }

        private string _linkFlow;
        [Editor(typeof(LinkFlowEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string LinkFlow
        {
            get
            {
                return _linkFlow;
            }
            set
            {
                _linkFlow = value;
            }
        }

        private string _parameters;
        public string Parameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }
        #endregion
    }


    public class LinkFlowEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            string strValue = (string)value;

            string path = EEPRegistry.Server;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = path + @"\Workflow\";
            dialog.Filter = "*.xoml | *.xoml";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string s = dialog.FileName;
                FileInfo fileInfo = new FileInfo(s);

                string s1 = fileInfo.Directory.Name;
                string s2 = fileInfo.Name;

                strValue = s1 + @"\" + s2;
            }

            return strValue;
        }
    }
}
