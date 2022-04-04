using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace FLTools.Base
{
    public class WebFormNameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            string strValue = (string)value;

            string path = EEPRegistry.WebClient;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = path;
            dialog.Filter = "*.aspx|*.aspx|Silverlight Form|*.xaml";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string s = dialog.FileName;
                FileInfo fileInfo = new FileInfo(s);

                string s1 = fileInfo.Directory.Name;
                string s2 = String.Empty;
                if (fileInfo.Name.ToLower().EndsWith(".xaml"))
                {
                    s1 = fileInfo.Directory.Parent.Name + "." + s1;
                    s2 = fileInfo.Name;
                }
                else
                {
                    s2 = fileInfo.Name.Split('.')[0];
                }

                strValue = s1 + "." + s2;
            }

            return strValue;
        }
    }
}
