using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLRejectActivity : FLActivity, IFLRejectActivity, INonEventWaiting
    {
        private string _formName;
        private string _webFormName;
        private FLNavigatorMode _flNavigatorMode;
        private NavigatorMode _navigatorMode;
        private SendToKind _sendToKind;
        private string _sendToField;
        private string _sendToRole;
        private string _parameters;
        private int _expTime;
        private int _urgentTime;
        private TimeUnit _timeUnit;
        private bool _sendEmail;

        public FLRejectActivity()
            : this(string.Empty)
        {
        }

        public FLRejectActivity(string name)
            : base(name)
        {
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

        [XmlAttribute("UrgentTime")]
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

        public static explicit operator FLNotifyActivity(FLRejectActivity flRejectActivity)
        {
            FLNotifyActivity flNotifyActivity = new FLNotifyActivity();

            flNotifyActivity.Name = flRejectActivity.Name;                     // Ä£ÄâFLRejectActivityµÄName;
            flNotifyActivity.Description = flRejectActivity.Description;
            flNotifyActivity.FormName = flRejectActivity.FormName;
            flNotifyActivity.WebFormName = flRejectActivity.WebFormName;
            flNotifyActivity.FLNavigatorMode = flRejectActivity.FLNavigatorMode;
            flNotifyActivity.NavigatorMode = flRejectActivity.NavigatorMode;
            flNotifyActivity.SendToKind = flRejectActivity.SendToKind;
            flNotifyActivity.SendToField = flRejectActivity.SendToField;
            flNotifyActivity.SendToRole = flRejectActivity.SendToRole;
            flNotifyActivity.SendToUser = flNotifyActivity.SendToUser;
            flNotifyActivity.Parameters = flRejectActivity.Parameters;
            flNotifyActivity.ExpTime = flRejectActivity.ExpTime;
            flNotifyActivity.UrgentTime = flRejectActivity.UrgentTime;
            flNotifyActivity.TimeUnit = flRejectActivity.TimeUnit;
            flNotifyActivity.SendEmail = flRejectActivity.SendEmail;

            return flNotifyActivity;
        }
    }
}
