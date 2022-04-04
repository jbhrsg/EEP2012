using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using FLCore;
using System.Xml.Serialization;
using FLTools.Base;
using System.Drawing;
using FLCore.Base;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using Srvtools;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLApprove), "Resources.FLApprove.png")]
    public partial class FLApprove : Activity, IEventWaiting, IFLApproveActivity
    {
        private string _formName;
        private string _webFormName;
        private NavigatorMode _navigatorMode;
        private string _parameters;
        private decimal _expTime;
        private decimal _urgentTime;
        private TimeUnit _timeUnit;
        private DateTime _invokedDateTime;
        private ApproveRightCollection _approveRights;
        private string _sendToId;
        private DateTime _executedTime = new DateTime();
        private bool _isUrgent = false;
        private bool _sendEmail;
        private SendToKind _sendToKind;
        private string _sendToField;
        private bool _plusApprove;
        private bool _delayAutoApprove;

        public FLApprove()
            : this(string.Empty)
        {
        }

        public FLApprove(string name)
            : base(name)
        {
            _invokedDateTime = DateTime.MinValue;
            _approveRights = new ApproveRightCollection();
            _formName = string.Empty;
            _webFormName = string.Empty;
            _sendToKind = SendToKind.Manager;
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

        [Editor(typeof(FLApproveSendToKindEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public SendToKind SendToKind
        {
            get
            {
                return _sendToKind;
            }
            set
            {
                if (value != SendToKind.Manager && value != SendToKind.RefManager && value != SendToKind.ApplicateManager)
                {
                    MessageBox.Show("This value musts be Manager perhaps RefManager");
                }
                else
                {
                    if (value == SendToKind.Manager)
                    {
                        _sendToField = string.Empty;
                    }
                    _sendToKind = value;
                }
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

        [Browsable(false)]
        public string SendToRole
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }

        [Browsable(false)]
        public string SendToUser
        {
            get { return string.Empty; }
            set { }
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ApproveRightCollection ApproveRights
        {
            get
            {
                return _approveRights;
            }
            set
            {
                _approveRights = value;
            }
        }

        #endregion

        public List<IFLApproveBranchActivity> GetApproveRights()
        {
            List<IFLApproveBranchActivity> list = new List<IFLApproveBranchActivity>();
            foreach (object a in ApproveRights)
            {
                list.Add((IFLApproveBranchActivity)a);
            }
            return list;
        }

        protected override void OnActivityExecutionContextUnload(IServiceProvider provider)
        {
            base.OnActivityExecutionContextUnload(provider);
        }

        [Browsable(false)]
        public DateTime InvokedDateTime
        {
            get
            {
                return _invokedDateTime;
            }
            set
            {
                _invokedDateTime = value;
            }
        }

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
    }


    public class FLApproveSendToKindEditor : UITypeEditor
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
                string[] items = new string[] { SendToKind.Manager.ToString(), SendToKind.RefManager.ToString() };
                FLTools.Base.StringListSelector selector = new FLTools.Base.StringListSelector(editorService, items);

                string strValue = (string)value;
                if (selector.Execute(ref strValue)) value = strValue;
                if (value.ToString() == SendToKind.Manager.ToString())
                {
                    return SendToKind.Manager;
                }
                else
                {
                    return SendToKind.RefManager;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
