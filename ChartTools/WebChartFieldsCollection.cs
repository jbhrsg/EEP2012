using System;
using Srvtools;
using System.Data;

namespace ChartTools
{
    public class WebChartFieldsCollection : InfoOwnerCollection
    {
        public WebChartFieldsCollection(Object aOwner, Type aItemType)
            : base(aOwner, typeof(WebChartField))
        {
        }

        public DataSet DsForDD = new DataSet();
        public new WebChartField this[int index]
        {
            get
            {
                return (WebChartField)InnerList[index];
            }
            set
            {
                if (index > -1 && index < Count)
                {
                    if (value is WebChartField)
                    {
                        //ԭ����Collection����Ϊ0
                        ((WebChartField)InnerList[index]).Collection = null;
                        InnerList[index] = value;
                        //Collection����Ϊthis
                        ((WebChartField)InnerList[index]).Collection = this;
                    }
                }
            }
        }
    }
}
