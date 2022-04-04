using System.Diagnostics;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using FLCore;
using System.Xml.Serialization;
using FLTools.Base;
using System.Drawing;
using System.ComponentModel;

namespace FLTools
{
    [ToolboxBitmap(typeof(FLValidate), "Resources.FLValidate.png")]
    public class FLValidate : Activity, IFLValidateActivity, INonEventWaiting
    {
        private string _expression;

        public FLValidate()
            : base(string.Empty)
        {
        }

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

        #region IFLValidateActivity Members

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        #endregion
    }
}
