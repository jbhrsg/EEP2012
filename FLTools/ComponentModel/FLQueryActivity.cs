using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLQueryActivity : FLActivity, IFLQueryActivity
    {
        private string _parameters;

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
    }
}
