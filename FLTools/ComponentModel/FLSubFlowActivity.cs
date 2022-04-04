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
    public class FLSubFlowActivity: FLActivity, IFLSubFlowActivity, IControlFL
    {
        #region IFLSubFlowActivity Members

        private string _xomlName;
        private bool _includeFirstActivity;

        [XmlAttribute("IncludeFirstActivity")]
        public bool IncludeFirstActivity
        {
            get
            {
                return _includeFirstActivity;
            }
            set
            {
                _includeFirstActivity = value;
            }
        }

        [XmlAttribute("XomlName")]
        public string XomlName
        {
            get
            {
                return _xomlName;
            }
            set
            {
                _xomlName = value;
            }
        }

        private string _xomlField;
        [XmlAttribute("XomlField")]
        public string XomlField
        {
            get
            {
                return _xomlField;
            }
            set
            {
                _xomlField = value;
            }
        }

        #endregion
    }
}
