using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel;
using FLCore;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using Srvtools;
using FLTools.Base;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLSubFlow), "Resources.FLSubFlow.png")]
    public class FLSubFlow : Activity, IFLSubFlowActivity
    {
        #region IFLSubFlowActivity Members

        private string _xomlName;
        private bool _includeFirstActivity;

        public bool IncludeFirstActivity
        {
            get
            {
                return _includeFirstActivity;
            }
            set
            {
                _includeFirstActivity = value;
            }
        }

        [Editor(typeof(FLTools.FLSubFlow.XomlNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string XomlName
        {
            get
            {
                return _xomlName;
            }
            set
            {
                _xomlName = value;
            }
        }



        private string _xomlField;
        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string XomlField
        {
            get
            {
                return _xomlField;
            }
            set
            {
                _xomlField = value;
            }
        }

        public string[] GetXomlFieldItems()
        {
            CompositeActivity root = this.Parent;
            while (root != null && !(root is ISupportSetConnectionString) && root.Parent != null)
            {
                root = root.Parent;
            }

            string tableName = ((IFLRootActivity)root).TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }

            object[] objs = CliUtils.CallMethod("GLModule", "GetRefRoles", new object[] { tableName });
            if (objs[0].ToString() == "0")
            {
                return (string[])objs[1];
            }
            else
            {
                return null;
            }
        }


        #endregion

        private class XomlNameEditor : UITypeEditor
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
                dialog.InitialDirectory = path;
                dialog.Filter = "*.xoml | *.xoml";
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string s = dialog.FileName;
                    FileInfo fileInfo = new FileInfo(s);

                    strValue = fileInfo.Name;
                }

                return strValue;
            }
        }
    }
}
