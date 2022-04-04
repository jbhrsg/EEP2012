using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLApproveBranchActivity : FLActivity, IFLApproveBranchActivity, IEventWaiting, IEventWaitingExecute
    {
        private string _formName;
        private string _webFormName;
        private NavigatorMode _navigatorMode;
        private FLNavigatorMode _flNavigatorMode;
        private string _sendToRole;
        private string _sendToField;
        private SendToKind _sendToKind;
        private string _parameters;
        private decimal _expTime;
        private decimal _urgentTime;
        private TimeUnit _timeUnit;
        private DateTime _invokedDateTime = DateTime.MinValue;
        private string _userId;
        private string _roleId;       
        private string _grade;
        private string _expression;
        private string _parentActivity;
        private string _sendToId;
        private DateTime _executedTime;
        private bool _isUrgent;
        private bool _sendEmail;
      
        public FLApproveBranchActivity()
            : this(string.Empty)
        {
        }

        public FLApproveBranchActivity(string name)
            : base(name)
        {
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

        [XmlAttribute("Grade")]
        public string Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                _grade = value;
            }
        }

        [XmlAttribute("Expression")]
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
            }
        }

        [XmlAttribute("ParentActivity")]
        public string ParentActivity
        {
            get
            {
                return _parentActivity;
            }
            set
            {
                _parentActivity = value;
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

            base.Execute();
        }

        public void Return(string userId, string roleId, bool isUrgent)
        {
            _userId = userId;
            _roleId = roleId;
            _executedTime = DateTime.Now;
            _isUrgent = isUrgent;

            base.Return();
        }
    }
}
