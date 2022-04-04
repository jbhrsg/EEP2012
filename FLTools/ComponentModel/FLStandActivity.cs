using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;
using System.Xml;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLStandActivity : FLActivity, IFLStandActivity , IEventWaiting, IEventWaitingExecute, ISupportFLDetailsActivity
    {
        private string _formName;
        private string _webFormName;
        private FLNavigatorMode _flNavigatorMode;
        private NavigatorMode _navigatorMode;
        private SendToKind _sendToKind;
        private string _sendToField;
        private string _sendToRole;
        private string _parameters;
        private decimal _expTime;
        private decimal _urgentTime;
        private TimeUnit _timeUnit;
        private string _userId;
        private string _roleId;
        private string _sendToId;
        private DateTime _executedTime;
        private bool _isUrgent;
        private bool _sendEmail;
        private bool _plusApprove;
        private bool _delayAutoApprove;
        private string _sendToId2;

        public FLStandActivity()
            : this(string.Empty)
        {
            _sendToId2 = string.Empty;
        }

        public FLStandActivity(string name)
            : base(name)
        {
            _sendToId2 = string.Empty;
        }

        [XmlAttribute("DelayAutoApprove")]
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

        [XmlAttribute("SendToId2")]
        public string SendToId2
        {
            get
            {
                return _sendToId2;
            }
            set
            {
                _sendToId2 = value;
            }
        }

        [XmlAttribute("PlusApprove")]
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
        [XmlAttribute("PlusApproveReturn")]
        public bool PlusApproveReturn
        {
            get { return plusApproveReturn; }
            set { plusApproveReturn = value; }
        }

        [XmlAttribute("SendEmail")]
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

        [XmlAttribute("FormName")]
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

        [XmlAttribute("WebFormName")]
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

        [XmlAttribute("FLNavigatorMode")]
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

        [XmlAttribute("NavigatorMode")]
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

        [XmlAttribute("SendToKind")]
        public SendToKind SendToKind
        {
            get
            {
                return _sendToKind;
            }
            set
            {
                _sendToKind = value;
            }
        }

        [XmlAttribute("SendToField")]
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

        [XmlAttribute("SendToRole")]
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
        [XmlAttribute("SendToUser")]
        public string SendToUser
        {
            get { return _sendToUser; }
            set { _sendToUser = value; }
        }

        [XmlAttribute("SendToId")]
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

        [XmlAttribute("Parameters")]
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

        [XmlAttribute("ExpTime")]
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

        [XmlAttribute("UrgentTime")]
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

        [XmlAttribute("TimeUnit")]
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

        [XmlAttribute("UserId")]
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

        [XmlAttribute("RoleId")]
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

        [XmlAttribute("ExecutedTime")]
        public DateTime ExecutedTime
        {
            get
            {
                return _executedTime;
            }
        }

        [XmlAttribute("IsUrgent")]
        public bool IsUrgent
        {
            get
            {
                return _isUrgent;
            }
        }

        public void Execute(string userId, string roleId, bool isUrgent)
        {
            _userId = userId;
            _roleId = roleId;
            _executedTime = DateTime.Now;
            _isUrgent = isUrgent;
            ExecutionStatus = FLActivityExecutionStatus.Executed;
        }

        public void Return(string userId, string roleId, bool isUrgent)
        {
            _userId = userId;
            _roleId = roleId;
            _executedTime = DateTime.Now;
            _isUrgent = isUrgent;
            ExecutionStatus = FLActivityExecutionStatus.Returned;
        }
    }
}
