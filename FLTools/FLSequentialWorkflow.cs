using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Workflow.Activities;
using Microsoft.Win32;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Xml;
using Srvtools;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data;
using System.Workflow.ComponentModel;
using FLCore;
using FLTools.Base;
using FLCore.Base;
using System.Reflection;

namespace FLTools
{
    [ToolboxItem(false)]
    [Serializable]
    public partial class FLSequentialWorkflow : SequentialWorkflowActivity, IFLRootActivity, ISupportSetClientDll//, ISupportSetConnectionString
    {
        private string _keys;
        private string _presentFields;
        private string _tableName;
        private DbConnectionType _connectionType;
        private string _eepAlias;
        private string _connecitonString = null;
        //private string _serverPath;
        private string _clientDll;
        private string _orgKind;
        private string _formName;
        private string _webFormName;
        private decimal _expTime;
        private decimal _urgentTime;
        private TimeUnit _timeUnit;
        private bool _notifySendMail;
        private string _mailApproveLevel;


        public static string __ConnectionString;
        public static DbConnectionType __ConnectionType;

        public FLSequentialWorkflow()
        {
            _timeUnit = TimeUnit.Day;
            _urgentTime = 0;
            _expTime = 0;
            _orgKind = "0";
            _connectionType = DbConnectionType.SqlClient;
            //_keys = new KeyItems(this, typeof(KeyItem));
        }

        public bool NotifySendMail
        {
            get
            {
                return _notifySendMail;
            }
            set
            {
                _notifySendMail = value;
            }
        }

        public decimal ExpTime
        {
            get
            {
                return _expTime;
            }
            set
            {
                _expTime = value;
            }
        }

        private string expTimeField;
        public string ExpTimeField
        {
            get { return expTimeField; }
            set { expTimeField = value; }
        }

        public decimal UrgentTime
        {
            get
            {
                return _urgentTime;
            }
            set
            {
                _urgentTime = value;
            }
        }

        public TimeUnit TimeUnit
        {
            get
            {
                return _timeUnit;
            }
            set
            {
                _timeUnit = value;
            }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string OrgKind
        {
            get
            {
                return _orgKind;
            }
            set
            {
                _orgKind = value;
            }
        }

        [Browsable(false)]
        public string ConnectionString
        {
            get
            {
                return _connecitonString;
            }
        }

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

        private bool _skipForSameUser = true;
        public bool SkipForSameUser
        {
            get
            {
                return _skipForSameUser;
            }
            set
            {
                _skipForSameUser = value;
            }
        }

        [Browsable(false)]
        public DbConnectionType ConnectionType
        {
            get
            {
                return _connectionType;
            }
            set
            {
                _connectionType = value;
                __ConnectionType = _connectionType;
            }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string EEPAlias
        {
            get
            {
                return _eepAlias;
            }
            set
            {
                _eepAlias = value;
            }
        }

        private string _rejectProcedure;
        public string RejectProcedure
        {
            get
            {
                return _rejectProcedure;
            }
            set
            {
                _rejectProcedure = value;
            }
        }

        private string _bodyField;
        public string BodyField
        {
            get
            {
                return _bodyField;
            }
            set
            {
                _bodyField = value;
            }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
            }
        }


        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(KeysEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Keys
        {
            get
            {
                return _keys;
            }
            set
            {
                _keys = value;
            }
        }

        [Editor(typeof(KeysEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string PresentFields
        {
            get
            {
                return _presentFields;
            }
            set
            {
                _presentFields = value;
            }
        }

        public string MailApproveLevel
        {
            get
            {
                return _mailApproveLevel;
            }
            set { _mailApproveLevel = value; }
        }

        private string GetPwdString(string password)
        {
            string sRet = "";
            for (int i = 0; i < password.Length; i++)
            {
                sRet = sRet + (char)(((int)(password[password.Length - 1 - i])) ^ password.Length);
            }
            return sRet;
        }

        //public string[] GetOrgKindItems()
        //{
        //    DbConnectionType connectionType = ConnectionType;
        //    string connectionString = ConnectionString;
        //    IDbConnection connection = Global.AllocateConnection(connectionType, connectionString);

        //    string wherePart = string.Empty;

        //    return Global.GetOrgKind(connection, wherePart);
        //}

        //private string GetConnectionString(string alias)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(SystemFile.DBFile);

        //    XmlNode node = xmlDoc.FirstChild.FirstChild.SelectSingleNode(alias);

        //    string DbString = "";
        //    string Pwd = "";
        //    if (node != null)
        //    {
        //        DbString = node.Attributes["String"].Value.Trim();
        //        Pwd = GetPwdString(node.Attributes["Password"].Value.Trim());
        //    }
        //    if (DbString.Length > 0 && Pwd.Length > 0)
        //    {
        //        if (DbString[DbString.Length - 1] != ';')
        //            DbString = DbString + ";Password=" + Pwd;
        //        else
        //            DbString = DbString + "Password=" + Pwd;
        //    }

        //    string value = "1";
        //    if (node != null)
        //    {
        //        value = node.Attributes["Type"].Value;
        //    }

        //    return DbString;
        //}

        //private void ResetConnectionString()
        //{
        //    if (_eepAlias == null || _eepAlias == string.Empty)
        //    {
        //        _connecitonString = string.Empty;
        //    }
        //    else
        //    {
        //        _connecitonString = GetConnectionString(_eepAlias);
        //    }

        //    __ConnectionString = _connecitonString;
        //}

        public string[] GetEEPAliasItems()
        {
            List<String> aliasList = new List<String>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(SystemFile.DBFile);

            XmlNodeList nodeList = xmlDoc.FirstChild.FirstChild.ChildNodes;
            foreach (XmlNode n in nodeList)
            {
                aliasList.Add(n.Name);
            }

            return aliasList.ToArray();
        }

        // ------------------------------------------------------------

        // ------------------------------------------------------------
        // TableName property editor.
//        public string[] GetTableNameItems()
//        {
//            if (_connecitonString == null || _connecitonString == string.Empty)
//            {
//                return null;
//            }

//            IDbConnection connection = null;
//            string connecitonString = GetConnectionString(_eepAlias);
//            List<String> tablesList = new List<string>();
//            String sQL = "";

//            if (_connectionType == DbConnectionType.SqlClient)
//            {
//                connection = new SqlConnection(_connecitonString);

//                sQL = "select @@version as version";
//                IDbCommand cmd0 = Global.AllocateCommand(connection, sQL);
//                if (connection.State == ConnectionState.Closed)
//                    connection.Open();

//                Object o = cmd0.ExecuteScalar();
//                connection.Close();
//                if (o.ToString().ToLower().IndexOf("microsoft sql server 2005") >= 0)
//                {
//                    sQL = @"select (
//                            case when b.name != 'dbo' then 
//	                            case when (Charindex(' ',Rtrim(Ltrim(b.name)),0) > 0) then
//		                            '[' + b.[name] + ']'
//	                            else
//		                            b.[name]
//	                            end
//	                            + '.' +
//	                            case when (Charindex(' ',Rtrim(Ltrim(a.name)),0) != 0) then
//		                            '[' + a.[name] + ']'
//	                            else
//		                            a.[name]
//	                            end
//                            else 
//	                            case when (Charindex(' ',Rtrim(Ltrim(a.name)),0) != 0) then
//		                            '[' + a.[name] + ']'
//	                            else
//		                            a.[name]
//	                            end
//                            end
//                        )as name from sysobjects a,sys.schemas b where a.uid=b.schema_id and a.xtype in ('u','U','v','V') order by a.[name]";
//                }
//                else
//                {
//                    sQL = @"select(
//                            case when (Charindex(' ',Rtrim(Ltrim(name)),0) != 0) then
//		                        '[' + [name] + ']'
//	                        else
//		                        [name]
//                            end
//                            ) as name from sysobjects where xtype in ('u','U','v','V')  order by [name]";
//                }
//            }
//            else if (_connectionType == DbConnectionType.Odbc)
//            {
//                connection = new OdbcConnection(_connecitonString);
//                sQL = "select * from systables where (tabtype = 'T' or tabtype = 'V') and tabid >= 100 order by tabname";
//            }
//            else if (_connectionType == DbConnectionType.OracleClient)
//            {
//                connection = new OracleConnection(_connecitonString);
//                sQL = "SELECT * FROM USER_OBJECTS WHERE OBJECT_TYPE = 'TABLE' OR OBJECT_TYPE = 'VIEW'order by OBJECT_NAME";
//            }
//            else if (_connectionType == DbConnectionType.OleDb)
//            {
//                connection = new OleDbConnection(_connecitonString);
//                sQL = "select * from sysobjects where xtype in ('u','U','v','V')  order by [name]";
//            }
//            else if (_connectionType == DbConnectionType.MySQL)
//            {
//                String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);

//                Assembly assembly = Assembly.LoadFrom(s);
//                connection = (IDbConnection)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlConnection");
//                connection.ConnectionString = _connecitonString;
//                sQL = "show tables;";
//            }

//            IDbCommand cmd = Global.AllocateCommand(connection, sQL);

//            if (connection.State == ConnectionState.Closed)
//            {
//                connection.Open();
//            }

//            IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
//            while (reader.Read())
//            {
//                if (connection is SqlConnection)
//                    tablesList.Add(reader["name"].ToString());
//                else if (connection is OdbcConnection)
//                    tablesList.Add(reader["tabname"].ToString());
//                else if (connection is OracleConnection)
//                    tablesList.Add(reader["OBJECT_NAME"].ToString());
//                else if (connection is OleDbConnection)
//                    tablesList.Add(reader["name"].ToString());
//                else if (connection.ToString() == "MySql.Data.MySqlClient.MySqlConnection")
//                {
//                    tablesList.Add(reader["Tables_in_mysqleep"].ToString());
//                }
//            }
//            connection.Close();

//            return tablesList.ToArray();
//        }



        //public string GetConnectionString()
        //{
        //    if (_eepAlias == null || _eepAlias != string.Empty)
        //    {
        //        return GetConnectionString(_eepAlias);
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}
        // ------------------------------------------------------------

        public string[] GetTableNameItems()
        {
            object[] objs = CliUtils.CallMethod("GLModule", "GetTableNames", null);
            if (objs[0].ToString() == "0")
            {
                return (string[])objs[1];
            }
            else
            {
                return null;
            }
        }

        public string[] GetOrgKindItems()
        {
            object[] objs = CliUtils.CallMethod("GLModule", "GetOrgKinds", null);
            if (objs[0].ToString() == "0")
            {
                return (string[])objs[1];
            }
            else
            {
                return null;
            }
        }

        [Browsable(false)]
        public string ClientDll
        {
            get
            {
                return _clientDll;
            }
            set
            {
                _clientDll = value;
            }
        }
    }

    //public class KeyItems : InfoOwnerCollection
    //{
    //    public KeyItems(object owner, Type aItemType)
    //        : base(owner, typeof(KeyItem))
    //    {

    //    }

    //    new public KeyItem this[int index]
    //    {
    //        get
    //        {
    //            return (KeyItem)InnerList[index];
    //        }
    //        set
    //        {
    //            if (index > -1 && index < Count)
    //                if (value is KeyItem)
    //                {
    //                    //原来的Collection设置为0
    //                    ((KeyItem)InnerList[index]).Collection = null;
    //                    InnerList[index] = value;
    //                    //Collection设置为this
    //                    ((KeyItem)InnerList[index]).Collection = this;
    //                }

    //        }
    //    }
    //}

    //[Serializable]
    //public class KeyItem : InfoOwnerCollectionItem, IGetValues
    //{
    //    private string _keyName = "";
    //    [Browsable(true)]
    //    [Editor(typeof(KeyNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
    //    public string KeyName
    //    {
    //        get
    //        {
    //            return _keyName;
    //        }
    //        set
    //        {
    //            _keyName = value;
    //        }
    //    }

    //    [Browsable(false)]
    //    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //    public override string Name
    //    {
    //        get
    //        {
    //            return KeyName;
    //        }

    //        set
    //        {
    //            KeyName = value;
    //        }
    //    }

    //    public string[] GetValues(string kind)
    //    {
    //        CompositeActivity root = (CompositeActivity)this.Owner;
    //        while (root != null && !(root is ISupportSetConnectionString) && root.Parent != null)
    //        {
    //            root = root.Parent;
    //        }

    //        if (root != null && root is ISupportSetConnectionString && root is IFLRootActivity)
    //        {
    //            DbConnectionType connectionType = ((ISupportSetConnectionString)root).ConnectionType;
    //            string connectionString = ((ISupportSetConnectionString)root).ConnectionString;
    //            string tableName = ((IFLRootActivity)root).TableName;
    //            IDbConnection connection = Global.AllocateConnection(connectionType, connectionString);

    //            string wherePart = string.Empty;

    //            return Global.GetRefRoles(connection, tableName);
    //        }
    //        return null;
    //    }

    //    //pending...
    //}

    //public class KeyNameEditor : System.Drawing.Design.UITypeEditor
    //{
    //    public KeyNameEditor()
    //    {
    //    }

    //    // Indicates whether the UITypeEditor provides a form-based (modal) dialog,
    //    // drop down dialog, or no UI outside of the properties window.
    //    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    //    public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
    //    {
    //        return UITypeEditorEditStyle.DropDown;
    //    }

    //    // Displays the UI for value selection.
    //    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    //    public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
    //    {
    //        // Uses the IWindowsFormsEditorService to display a
    //        // drop-down UI in the Properties window.
    //        IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
    //        IGetValues aItem = (IGetValues)context.Instance;
    //        if (edSvc != null)
    //        {
    //            FLTools.Base.StringListSelector mySelector = new FLTools.Base.StringListSelector(edSvc, aItem.GetValues(context.PropertyDescriptor.Name));
    //            string strValue = (string)value;
    //            if (mySelector.Execute(ref strValue)) value = strValue;
    //        }

    //        return value;
    //    }
    //}

    public class KeysEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            object comp = context.Instance;

            if (editorService != null && comp != null)
            {
                string strValue = (string)value;

                KeysEditorForm form = new KeysEditorForm(strValue, (IFLRootActivity)comp);
                form.ShowDialog();
                value = form.Keys;

                return (string)value;
            }
            else
            {
                return null;
            }
        }
    }
}
