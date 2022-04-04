using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    [XmlInclude(typeof(FLStandActivity))]
    [XmlInclude(typeof(FLApproveActivity))]
    [XmlInclude(typeof(FLApproveBranchActivity))]
    [XmlInclude(typeof(FLParallelActivity))]
    [XmlInclude(typeof(FLSequenceActivity))]
    public class FLDetailsActivity : FLActivity, IFLDetailsActivity, IControlFL
    {
        private string _detailsTableName;
        private string _relationKeys;
        private string _parallelField;
        private string _sendToField;
        private SendToKind _sendToKind;
        private string _sendToMasterField;
        private decimal _parallelRate;
        private ParallelMode _parallelMode;

        private string _formName;
        private string _webFormName;
        private NavigatorMode _navigatorMode;
        private string _sendToRole;
        private string _sendToId;
        private string _parameters;
        private decimal _expTime;
        private decimal _urgentTime;
        private TimeUnit _timeUnit;
        private DateTime _executedTime = new DateTime();
        private bool _isUrgent = false;
        private bool _sendEmail;

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
                return FLNavigatorMode.Approve;
            }
            set
            {
            }
        }

        [XmlAttribute("FLNavigatorField")]
        public string FLNavigatorField { get; set; }

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

        [XmlAttribute("ParallelMode")]
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

        [XmlAttribute("SendToMasterField")]
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

        [XmlAttribute("DetailsTableName")]
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

        [XmlAttribute("RelationKeys")]
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

        [XmlAttribute("ParallelField")]
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

        private string _extApproveID;
        [XmlAttribute("ExtApproveID")]
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
        [XmlAttribute("ExtGroupField")]
        public string ExtGroupField
        {
            get { return _extGroupField; }
            set { _extGroupField = value; }
        }

        private string _extValueField;
        [XmlAttribute("ExtValueField")]
        public string ExtValueField
        {
            get { return _extValueField; }
            set { _extValueField = value; }
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

        [XmlAttribute("ParallelRate")]
        public decimal ParallelRate
        {
            get
            {
                return _parallelRate;
            }
            set
            {
                _parallelRate = value;
            }
        }

        public override void Return()
        {
            base.Return();
        }

        #region IFLPlusApprove Members


        private bool plusApprove;
        [XmlAttribute("PlusApprove")]
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
        [XmlAttribute("PlusApproveReturn")]
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
