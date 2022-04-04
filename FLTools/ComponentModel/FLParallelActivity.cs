using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLParallelActivity : FLActivity, IFLParallelActivity, IControlFL
    {
        private List<string> _executedBranches;

        public FLParallelActivity()
        {
            _executedBranches = new List<string>();
        }

        [XmlAttribute("ExecutedBranches")]
        public List<string> ExecutedBranches
        {
            get
            {
                return _executedBranches;
            }
        }
    }
}
