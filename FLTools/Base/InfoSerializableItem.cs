using System;
using System.Collections.Generic;
using System.Text;

namespace FLTools.Base
{
    [Serializable]
    public abstract class InfoSerializableItem
    {
        public abstract string Name
        {
            get;
            set;
        }
    }
}
