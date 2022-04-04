using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Workflow.ComponentModel;
using FLCore;
using System.ComponentModel;
using System.Xml.Serialization;
using FLCore.Base;
using System.Data;
using FLTools.Base;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Xml;
using Microsoft.Win32;
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLDetails), "Resources.FLDetails.png")]
    public class FLDetails : Activity, IFLDetailsActivity, IEventWaiting
    {
        private string _formName;
        private string _webFormName;
        //private FLNavigatorMode _flNavigatorMode;
        private NavigatorMode _navigatorMode;

        //private SendToKind _sendToKind;
        private string _sendToField;
        private string _sendToMasterField;
        private string _sendToRole;
        private SendToKind _sendToKind = SendToKind.RefRole;
        private string _sendToId;

        private decimal _parallelRate;
        private string _parameters;
        private decimal _expTime;
        private decimal _urgentTime;
        private TimeUnit _timeUnit;
        private DateTime _executedTime = new DateTime();
        private bool _isUrgent = false;
        private bool _sendEmail;
        //private bool _plusApprove;
        private string _parallelField;
        private string _relationKeys;
        private string _detailsTableName;
        private ParallelMode _parallelMode;

        public static string __ConnectionString;
        public static DbConnectionType __ConnectionType;

        public FLDetails() : this(string.Empty)
        {
        }

        public FLDetails(string name)
            : base(name)
        {
            _formName = string.Empty;
            _webFormName = string.Empty;
            _parallelRate = 0;
        }

        #region Properties


        private bool _allowSendBack = true;
        public bool AllowSendBack
        {
            get
            {
                return _allowSendBack;
            }
            set
            {
                _allowSendBack = value;
            }
        }
        //[XmlAttribute("PlusApprove")]
        //public bool PlusApprove
        //{
        //    get
        //    {
        //        return _plusApprove;
        //    }
        //    set
        //    {
        //        _plusApprove = value;
        //    }
        //}

        public ParallelMode ParallelMode
        {
            get
            {
                return _parallelMode;
            }
            set
            {
                _parallelMode = value;
            }
        }


        public decimal ParallelRate
        {
            get
            {
                return _parallelRate;
            }
            set
            {
                if (value > 100 || value < 0)
                    MessageBox.Show("ParallelRate's value must be <100 and >0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    _parallelRate = value;
            }
        }

        [Category("Relation")]
        public string RelationKeys
        {
            get
            {
                return _relationKeys;
            }
            set
            {
                _relationKeys = value;
            }
        }

        [Category("Relation")]
        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string DetailsTableName
        {
            get
            {
                return _detailsTableName;
            }
            set
            {
                _detailsTableName = value;
            }
        }

        public bool SendEmail
        {
            get
            {
                return _sendEmail;
            }
            set
            {
                _sendEmail = value;
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

        [ReadOnly(true)]
        public FLNavigatorMode FLNavigatorMode
        {
            get
            {
                return FLNavigatorMode.Approve;
            }
            set
            {               
            }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FLNavigatorField { get; set; }

        public NavigatorMode NavigatorMode
        {
            get
            {
                return _navigatorMode;
            }
            set
            {
                _navigatorMode = value;
            }
        }

        public SendToKind SendToKind
        {
            get
            {
                return _sendToKind;
            }
            set
            {
                if (value != SendToKind.RefRole && value != SendToKind.RefUser)
                {
                    MessageBox.Show("This value musts be RefRole or RefUser");
                }
                else
                {
                    _sendToKind = value;
                }
            }
        }

        [Browsable(false)]
        public string SendToId
        {
            get
            {
                return _sendToId;
            }
            set
            {
                _sendToId = value;
            }
        }

        private string _extApproveID;
        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ExtApproveID
        {
            get
            {
                return _extApproveID;
            }
            set
            {
                _extApproveID = value;
            }
        }

        private string _extGroupField;
        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ExtGroupField
        {
            get { return _extGroupField; }
            set { _extGroupField = value; }
        }

        private string _extValueField;
        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ExtValueField
        {
            get { return _extValueField; }
            set { _extValueField = value; }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string SendToField
        {
            get
            {
                return _sendToField;
            }
            set
            {
                _sendToField = value;
            }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string SendToMasterField
        {
            get
            {
                return _sendToMasterField;
            }
            set
            {
                _sendToMasterField = value;
            }
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ParallelField
        {
            get
            {
                return _parallelField;
            }
            set
            {
                _parallelField = value;
            }
        }

        [Browsable(false)]
        public string SendToRole
        {
            get
            {
                return _sendToRole;
            }
            set
            {
                _sendToRole = value;
            }
        }

        [Browsable(false)]
        public string SendToUser
        {
            get { return string.Empty; }
            set { }
        }

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

        [Browsable(false)]
        public DateTime ExecutedTime
        {
            get
            {
                return _executedTime;
            }
        }

        [Browsable(false)]
        public bool IsUrgent
        {
            get
            {
                return _isUrgent;
            }
        }

        #endregion

        //public string[] GetSendToFieldItems()
        //{
        //    CompositeActivity root = this.Parent;
        //    while (root != null && !(root is ISupportSetConnectionString) && root.Parent != null)
        //    {
        //        root = root.Parent;
        //    }

        //    if (root != null && root is ISupportSetConnectionString && root is IFLRootActivity)
        //    {
        //        DbConnectionType connectionType = ((ISupportSetConnectionString)root).ConnectionType;
        //        string connectionString = ((ISupportSetConnectionString)root).ConnectionString;
        //        if (connectionString == null || connectionString.Length == 0 || DetailsTableName == null || DetailsTableName.Length == 0)
        //        {
        //            return null;
        //        }

        //        IDbConnection connection = Global.AllocateConnection(connectionType, connectionString);

        //        string wherePart = string.Empty;

        //        return Global.GetRefRoles(connection, DetailsTableName);
        //    }
        //    return null;
        //}

        //public string[] GetParallelFieldItems()
        //{
        //    CompositeActivity root = this.Parent;
        //    while (root != null && !(root is ISupportSetConnectionString) && root.Parent != null)
        //    {
        //        root = root.Parent;
        //    }

        //    if (root != null && root is ISupportSetConnectionString && root is IFLRootActivity)
        //    {
        //        DbConnectionType connectionType = ((ISupportSetConnectionString)root).ConnectionType;
        //        string connectionString = ((ISupportSetConnectionString)root).ConnectionString;
        //        if (connectionString == null || connectionString.Length == 0 || DetailsTableName == null || DetailsTableName.Length == 0)
        //        {
        //            return null;
        //        }

        //        IDbConnection connection = Global.AllocateConnection(connectionType, connectionString);

        //        string wherePart = string.Empty;

        //        return Global.GetRefRoles(connection, DetailsTableName);
        //    }
        //    return null;
        //}

        public string[] GetSendToMasterFieldItems()
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

        public string[] GetExtApproveIDItems()
        {
            List<string> items = new List<string>();
            object[] objs = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT DISTINCT APPROVEID FROM SYS_EXTAPPROVE" });
            if (objs != null && (int)objs[0] == 0)
            {
                foreach (DataRow row in ((DataSet)objs[1]).Tables[0].Rows)
                {
                    items.Add(row[0].ToString());
                }
            }
            return items.ToArray();
        }

        public string[] GetExtGroupFieldItems()
        {
            return GetExtValueFieldItems();
        }

        public string[] GetExtValueFieldItems()
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

        public string[] GetSendToFieldItems()
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

            if (string.IsNullOrEmpty(DetailsTableName))
            {
                return null;
            }

            object[] objs = CliUtils.CallMethod("GLModule", "GetRefRoles", new object[] { DetailsTableName });
            if (objs[0].ToString() == "0")
            {
                return (string[])objs[1];
            }
            else
            {
                return null;
            }
        }

        public string[] GetFLNavigatorFieldItems()
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

            if (string.IsNullOrEmpty(DetailsTableName))
            {
                return null;
            }

            object[] objs = CliUtils.CallMethod("GLModule", "GetRefRoles", new object[] { DetailsTableName });
            if (objs[0].ToString() == "0")
            {
                return (string[])objs[1];
            }
            else
            {
                return null;
            }
        }

        public string[] GetParallelFieldItems()
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

            if (string.IsNullOrEmpty(DetailsTableName))
            {
                return null;
            }

            object[] objs = CliUtils.CallMethod("GLModule", "GetRefRoles", new object[] { DetailsTableName });
            if (objs[0].ToString() == "0")
            {
                return (string[])objs[1];
            }
            else
            {
                return null;
            }
        }

//        public string[] GetDetailsTableNameItems()
//        {
//            CompositeActivity root = this.Parent;
//            while (root != null && !(root is ISupportSetConnectionString) && root.Parent != null)
//            {
//                root = root.Parent;
//            }

//            if (root != null && root is ISupportSetConnectionString && root is IFLRootActivity)
//            {
//                DbConnectionType connectionType = ((ISupportSetConnectionString)root).ConnectionType;
//                string connectionString = ((ISupportSetConnectionString)root).ConnectionString;

//                if (connectionString == null || connectionString.Length == 0)
//                {
//                    return null;
//                }

//                IDbConnection connection = null;
//                List<String> tablesList = new List<string>();
//                String sQL = "";

//                if (connectionType == DbConnectionType.SqlClient)
//                {
//                    connection = new SqlConnection(connectionString);

//                    sQL = "select @@version as version";
//                    IDbCommand cmd0 = Global.AllocateCommand(connection, sQL);
//                    if (connection.State == ConnectionState.Closed)
//                        connection.Open();

//                    Object o = cmd0.ExecuteScalar();
//                    connection.Close();
//                    if (o.ToString().ToLower().IndexOf("microsoft sql server 2005") >= 0)
//                    {
//                        sQL = @"select (
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
//                    }
//                    else
//                    {
//                        sQL = @"select(
//                            case when (Charindex(' ',Rtrim(Ltrim(name)),0) != 0) then
//		                        '[' + [name] + ']'
//	                        else
//		                        [name]
//                            end
//                            ) as name from sysobjects where xtype in ('u','U','v','V')  order by [name]";
//                    }
//                }
//                else if (connectionType == DbConnectionType.Odbc)
//                {
//                    connection = new OdbcConnection(connectionString);
//                    sQL = "select * from systables where (tabtype = 'T' or tabtype = 'V') and tabid >= 100 order by tabname";
//                }
//                else if (connectionType == DbConnectionType.OracleClient)
//                {
//                    connection = new OracleConnection(connectionString);
//                    sQL = "SELECT * FROM USER_OBJECTS WHERE OBJECT_TYPE = 'TABLE' OR OBJECT_TYPE = 'VIEW'order by OBJECT_NAME";
//                }
//                else if (connectionType == DbConnectionType.OleDb)
//                {
//                    connection = new OleDbConnection(connectionString);
//                    sQL = "select * from sysobjects where xtype in ('u','U','v','V')  order by [name]";
//                }

//                IDbCommand cmd = Global.AllocateCommand(connection, sQL);

//                if (connection.State == ConnectionState.Closed)
//                {
//                    connection.Open();
//                }

//                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
//                while (reader.Read())
//                {
//                    if (connection is SqlConnection)
//                        tablesList.Add(reader["name"].ToString());
//                    else if (connection is OdbcConnection)
//                        tablesList.Add(reader["tabname"].ToString());
//                    else if (connection is OracleConnection)
//                        tablesList.Add(reader["OBJECT_NAME"].ToString());
//                    else if (connection is OleDbConnection)
//                        tablesList.Add(reader["name"].ToString());
//                }
//                connection.Close();

//                return tablesList.ToArray();
//            }
//            else
//            {
//                return null;
//            }
//        }

        public string[] GetDetailsTableNameItems()
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

        #region IFLPlusApprove Members
        private bool plusApprove;
     
        public bool PlusApprove
        {
            get
            {
                return plusApprove;
            }
            set
            {
                plusApprove = value;
            }
        }

        private bool plusApproveReturn = true;
 
        public bool PlusApproveReturn
        {
            get
            {
                return plusApproveReturn;
            }
            set
            {
                plusApproveReturn = value;
            }
        }

        #endregion
    }
}
