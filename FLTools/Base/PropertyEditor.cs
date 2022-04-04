using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms.Design;

namespace FLTools.Base
{
    public class PropertyEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            object comp = context.Instance;

            if (editorService != null && comp != null)
            {
                string propertyName = context.PropertyDescriptor.Name;
                string methodName = "Get" + propertyName + "Items";

                Type type = comp.GetType();
                MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
                if (methodInfo == null)
                {
                    return null;
                }

                object v = methodInfo.Invoke(comp, null);
                string[] items = (string[])v;
                StringListSelector selector = new StringListSelector(editorService, items);

                string strValue = (string)value;
                if (selector.Execute(ref strValue)) value = strValue;
                return (string)value;
            }
            else
            {
                return null;
            }
        }
    }
}
