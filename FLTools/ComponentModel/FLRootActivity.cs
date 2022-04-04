using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    [XmlInclude(typeof(FLStandActivity))]
    [XmlInclude(typeof(FLApproveActivity))]
    [XmlInclude(typeof(FLApproveBranchActivity))]
    [XmlInclude(typeof(FLIfElseActivity))]
    [XmlInclude(typeof(FLIfElseBranchActivity))]
    [XmlInclude(typeof(FLParallelActivity))]
    [XmlInclude(typeof(FLSequenceActivity))]
    [XmlInclude(typeof(FLNotifyActivity))]
    [XmlInclude(typeof(FLProcedureActivity))]
    [XmlInclude(typeof(FLValidateActivity))]
    [XmlInclude(typeof(FLHyperLinkActivity))]
    [XmlInclude(typeof(FLQueryActivity))]
    [XmlInclude(typeof(FLRejectActivity))]
    [XmlInclude(typeof(FLDetailsActivity))]
    [XmlInclude(typeof(FLSubFlowActivity))]
    [XmlInclude(typeof(FLGotoActivity))]
    public class FLRootActivity : FLActivity, IFLRootActivity
    {
        private string _presentFields;
        private string _tableName;
        private string _orgKind;
        private string _keys;
        private string _formName;
        private string _webFormName;
        private decimal _expTime;
        private decimal _urgentTime;
        private TimeUnit _timeUnit;
        private bool _notifySendMail;

        [XmlAttribute("NotifySendMail")]
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

        private string expTimeField;
        [XmlAttribute("ExpTimeField")]
        public string ExpTimeField
        {
            get { return expTimeField; }
            set { expTimeField = value; }
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

        [XmlAttribute("TableName")]
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

        private string _eepAlias;
        [XmlAttribute("EEPAlias")]
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
        [XmlAttribute("RejectProcedure")]
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
        [XmlAttribute("BodyField")]
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

        [XmlAttribute("Keys")]
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

        [XmlAttribute("PresentFields")]
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

        [XmlAttribute("OrgKind")]
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


        private bool _skipForSameUser = true;
        [XmlAttribute("SkipForSameUser")]
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

        private string _mailApproveLevel;

        [XmlAttribute("MailApproveLevel")]
        public string MailApproveLevel
        {
            get
            {
                return _mailApproveLevel;
            }
            set { _mailApproveLevel = value; }
        }

        public FLRootActivity()
        {

        }
    }
}
