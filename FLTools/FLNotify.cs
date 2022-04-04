using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using FLCore;
using System.Drawing.Design;
using System.Data;
using System.Xml.Serialization;
using FLTools.Base;
using FLCore.Base;
using Srvtools;
using System.Windows.Forms;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLNotify), "Resources.FLNotify.png")]
    public class FLNotify : Activity, ISupportSendToKind, IFLNotifyActivity, INonEventWaiting
    {
        private string _formName;
        private string _webFormName;
        private SendToKind _sendToKind;
        private string _sendToField;
        private string _sendToRole;
        private string _parameters;
        private int _expTime;
        private int _urgentTime;
        private TimeUnit _timeUnit;
        private bool _sendEmail;
        private string _userId;
        private string _roleId;

        public FLNotify()
            : this(string.Empty)
        {
        }

        public FLNotify(string name)
            : base(name)
        {
            _formName = string.Empty;
            _webFormName = string.Empty;
        }

        #region Properties

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
                return FLNavigatorMode.Notify;
            }
            set
            {
            }
        }

        [ReadOnly(true)]
        public NavigatorMode NavigatorMode
        {
            get
            {
                return  NavigatorMode.Normal;
            }
            set
            {
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
                _sendToKind = value;
                if (_sendToKind == SendToKind.Applicate || _sendToKind == SendToKind.Manager)
                {
                    SendToRole = string.Empty;
                    SendToUser = string.Empty;
                    SendToField = string.Empty;
                }
                else if (_sendToKind == SendToKind.Role || _sendToKind == SendToKind.User)
                {
                    SendToField = string.Empty;
                }
                else if (_sendToKind == SendToKind.RefRole || _sendToKind == SendToKind.RefUser)
                {
                    SendToRole = string.Empty;
                }
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

        private string _sendToUser;
        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string SendToUser
        {
            get
            {
                return _sendToUser;
            }
            set
            {
                _sendToUser = value;
            }
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

        public int ExpTime
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

        public int UrgentTime
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

        [ReadOnly(true)]
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        [ReadOnly(true)]
        public string RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
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
        //        string tableName = ((IFLRootActivity)root).TableName;
        //        IDbConnection connection = Global.AllocateConnection(connectionType, connectionString);

        //        string wherePart = string.Empty;

        //        if (SendToKind == SendToKind.RefRole)
        //        {
        //            return Global.GetRefRoles(connection, tableName);
        //        }
        //    }
        //    return null;
        //}

        //public string[] GetSendToRoleItems()
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

        //        if (SendToKind == SendToKind.Role)
        //        {
        //            return Global.GetRoles(connection, wherePart);
        //        }
        //    }
        //    return null;
        //}

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

        public string[] GetSendToRoleItems()
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

        public string[] GetSendToUserItems()
        {
            object[] objs = CliUtils.CallMethod("GLModule", "GetAllUsers", null);
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(ds.Tables[0].Rows[i]["USERID"].ToString());
                }
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
