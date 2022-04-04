using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLIfElseBranchActivity : FLActivity, IFLIfElseBranchActivity , IControlFL
    {
        private string _condition;
 
        public FLIfElseBranchActivity()
        {
            _condition = string.Empty;
        }

        [XmlAttribute("Condition")]
        public string Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                _condition = value;
            }
        }
    }
}
