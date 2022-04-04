using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLValidateActivity : FLActivity , IFLValidateActivity
    {
        private string _expression;

        public FLValidateActivity()
        {
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

        #region IFLValidateActivity Members

        private string _message;
        [XmlAttribute("Message")]
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
