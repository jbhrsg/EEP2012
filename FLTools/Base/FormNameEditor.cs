using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Reflection;
using System.Workflow.ComponentModel;
using FLCore;
using Srvtools;
using System.Windows.Forms;
using System.IO;

namespace FLTools.Base
{
    public class FormNameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            string strValue = (string)value;

            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            object comp = context.Instance;

            if (editorService != null && comp != null)
            {
                Activity obj = (Activity)comp;
                if (!(obj is ISupportSetClientDll))
                {
                    obj = (Activity)obj.Parent;
                    while (obj != null && !(obj is ISupportSetConnectionString) && obj.Parent != null)
                    {
                        obj = obj.Parent;
                    }
                }

                ISupportSetClientDll supportSetClientDll = (ISupportSetClientDll)obj;
                string clientDll = supportSetClientDll.ClientDll;

                SelectFNForm dialog = new SelectFNForm(strValue, clientDll);
                editorService.ShowDialog(dialog);

                if (strValue != dialog.FormName)
                {
                    strValue = dialog.FormName;
                    supportSetClientDll.ClientDll = dialog.ClientDll;
                }

                dialog.Dispose();

                return strValue;
            }
            else
            {
                return strValue;
            }
        }
    }
}
