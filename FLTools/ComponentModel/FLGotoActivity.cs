using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLGotoActivity : FLActivity, IFLGotoActivity, INonEventWaiting
    {
        #region IFLGoto Members

        private string activityName;
        [XmlAttribute("ActivityName")]
        public string ActivityName
        {
            get
            {
                return activityName;
            }
            set
            {
                activityName = value;
            }
        }

        #endregion
    }
}
