using System;
using System.Data;

namespace ChartTools
{
    public class ChartFieldsCollection : InfoOwnerCollection
    {
        public ChartFieldsCollection(Object aOwner, Type aItemType)
            : base(aOwner, typeof(ChartField))
        {
        }

        public DataSet DsForDD = new DataSet();
        public new ChartField this[int index]
        {
            get
            {
                return (ChartField)InnerList[index];
            }
            set
            {
                if (index > -1 && index < Count)
                {
                    if (value is ChartField)
                    {
                        //ԭ����Collection����Ϊ0
                        ((ChartField)InnerList[index]).Collection = null;
                        InnerList[index] = value;
                        //Collection����Ϊthis
                        ((ChartField)InnerList[index]).Collection = this;
                    }
                }
            }
        }
    }
}
