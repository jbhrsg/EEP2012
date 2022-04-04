using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLSequenceActivity : FLActivity, IFLSequenceActivity, IControlFL
    {
        private FLDirection _flDirection;

        public FLSequenceActivity()
        {
            _flDirection = FLDirection.Waiting;
        }

        [XmlAttribute("FLDirection")]
        public FLDirection FLDirection
        {
            get
            {
                return _flDirection;
            }
        }

        public void SetFLDirection(FLDirection flDirection)
        {
            _flDirection = flDirection;
        }
    }
}
