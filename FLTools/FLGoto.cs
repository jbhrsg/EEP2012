using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Workflow.ComponentModel;
using FLCore;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLGoto), "Resources.FLGoto.png")]
    public class FLGoto : Activity, IFLGotoActivity, INonEventWaiting
    {
        #region IFLGoto Members

        private string activityName;
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
