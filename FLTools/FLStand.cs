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
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLStand), "Resources.FLStand.png")]
    public class FLStand : Activity, ISupportSendToKind, IEventWaiting, IFLStandActivity
    {
        private string _formName;
        private string _webFormName;
        private FLNavigatorMode _flNavigatorMode;
        private NavigatorMode _navigatorMode;

        private SendToKind _sendToKind;
        private string _sendToField;
        private string _sendToRole;
        private string _sendToId;

        private string _parameters;
        private decimal _expTime;
        private decimal _urgentTime;
        private TimeUnit _timeUnit;
        private DateTime _executedTime = new DateTime();
        private bool _isUrgent = false;
        private bool _sendEmail;
        private bool _plusApprove;
        private bool _delayAutoApprove;

        public FLStand()
            : this(string.Empty)
        {
        }

        public FLStand(string name)
            : base(name)
        {
            _formName = string.Empty;
            _webFormName = string.Empty;
        }

        #region Properties

        public bool DelayAutoApprove
        {
            get
            {
                return _delayAutoApprove;
            }
            set
            {
                _delayAutoApprove = value;
            }
        }

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

        public bool PlusApprove
        {
            get
            {
                return _plusApprove;
            }
            set
            {
                _plusApprove = value;
            }
        }

        private bool plusApproveReturn = true;
        public bool PlusApproveReturn
        {
            get { return plusApproveReturn; }
            set { plusApproveReturn = value; }
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

        public FLNavigatorMode FLNavigatorMode
        {
            get
            {
                return _flNavigatorMode;
            }
            set
            {
                _flNavigatorMode = value;
            }
        }

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
                if (value == SendToKind.AllRoles)
                {
                    SendToKind k = _sendToKind;
                    MessageBox.Show("FLStand not support AllRoles.");
                    _sendToKind = k;
                    return;
                }

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
            get { return _sendToUser; }
            set { _sendToUser = value; }
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
        //        string tableName = ((IFLRootActivity)root).TableName;
        //        IDbConnection connection = Global.AllocateConnection(connectionType, connectionString);

        //        string wherePart = string.Empty;

        //        if (SendToKind == SendToKind.RefRole || SendToKind == SendToKind.RefManager)
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
        //        string connectionString =((ISupportSetConnectionString)root).ConnectionString;
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

    //FLStand                 FLApprove
    //------------------------------------------
    //ProcStep                ProcStep
    //ProcName                ProcName
    //ProcDescription         ProcDescription
    //WinFormName             WinFormName
    //WebAspName              WebAspName
    //FLNavigatorMode         FLNavigatorMode
    //NavigatorMode           NavigatorMode
    //SendToKind              SendToKind
    //                        ApproveRights
    //SendToField
    //SendToRole
    //Parameters              Parameters
    //ExpTime                 ExpTime
    //UrgentTime              UrgentTime
    //TimeUnit                TimeUnit
}
