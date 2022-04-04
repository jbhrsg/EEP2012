using System;
using System.Collections.Generic;
using System.Text;
using FLTools.Base;

namespace FLCore
{
    [Serializable]
    public class ApproveRightCollection : InfoSerializableCollection
    {
        public ApproveRightCollection()
        {
        }

        public ApproveRightCollection(Type itemType)
            : base(typeof(ApproveRight))
        {
        }

        public new ApproveRight this[int index]
        {
            get
            {
                return (ApproveRight)InnerList[index];
            }
            set
            {
                if (index > -1 && index < Count)
                {
                    if (value is ApproveRight)
                    {
                        InnerList[index] = value;
                    }
                }
            }
        }
    }
}
