using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel;
using FLCore;
using System.ComponentModel;
using FLCore.Base;
using System.Data;
using FLTools.Base;
using System.Drawing;
using System.Drawing.Design;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.Design;
using System.Reflection;
using Srvtools;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLProcedure), "Resources.FLProcedure.png")]
    public class FLProcedure : Activity, IFLProcedureActivity, INonEventWaiting, ISupportSetServerDll
    {
        private string _moduleName;
        private string _methodName;
        private bool _errorLog;
        private string _errorToRole;
        private string _serverDll;

        public FLProcedure()
            : this(string.Empty)
        {
        }

        public FLProcedure(string name)
            : base(name)
        {
        }

        #region Properties

        [Editor(typeof(ModuleNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ModuleName
        {
            get
            {
                return _moduleName;
            }
            set
            {
                _moduleName = value;
            }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string MethodName
        {
            get
            {
                return _methodName;
            }
            set
            {
                _methodName = value;
            }
        }

        [Browsable(false)]
        public string ServerDll
        {
            get
            {
                return _serverDll;
            }
            set
            {
                _serverDll = value;
            }
        }

        public bool ErrorLog
        {
            get
            {
                return _errorLog;
            }
            set
            {
                _errorLog = value;
            }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ErrorToRole
        {
            get
            {
                return _errorToRole;
            }
            set
            {
                _errorToRole = value;
            }
        }

        #endregion

        //public string[] GetErrorToRoleItems()
        //{
        //    CompositeActivity root = this.Parent;
        //    while (root != null && !(root is ISupportSetConnectionString) && root.Parent != null)
        //    {
        //        root = root.Parent;
        //    }

        //    if (root != null && root is ISupportSetConnectionString)
        //    {
        //        DbConnectionType connectionType = ((ISupportSetConnectionString)root).ConnectionType;
        //        string connectionString = ((ISupportSetConnectionString)root).ConnectionString;
        //        IDbConnection connection = Global.AllocateConnection(connectionType, connectionString);
        //        string wherePart = string.Empty;

        //        return Global.GetRoles(connection, wherePart);
        //    }
        //    return null;
        //}

        public string[] GetErrorToRoleItems()
        {
            object[] objs = CliUtils.CallMethod("GLModule", "GetRoles", null);
            if (objs[0].ToString() == "0")
            {
                return (string[])objs[1];
            }
            else
            {
                return null;
            }
        }

        public string[] GetMethodNameItems()
        {
            List<string> list = new List<string>();

            if (string.IsNullOrEmpty(_serverDll) || string.IsNullOrEmpty(_moduleName))
            {
                return null;
            }

            Assembly a = Assembly.LoadFrom(_serverDll);

            FileInfo fileInfo = new FileInfo(_serverDll);
            string s = fileInfo.Name.Split('.')[0];

            Type type = a.GetType(string.Format("{0}.Component", s));
            if (type == null)
            {
                return null;
            }

            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach(MethodInfo method in methods)
            {
                list.Add(method.Name);
            }

            return list.ToArray();
        }
    }

    public class ModuleNameEditor : UITypeEditor
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

            string path = EEPRegistry.Server;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = path;
            dialog.Filter = "*.dll | *.dll";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string s = dialog.FileName;

                ISupportSetServerDll supportSetServerDll = (ISupportSetServerDll)comp;
                supportSetServerDll.ServerDll = s;


                FileInfo fileInfo = new FileInfo(s);
                string s2 = fileInfo.Name.Split('.')[0];

                strValue = s2;
            }

            return strValue;
        }
    }
}
