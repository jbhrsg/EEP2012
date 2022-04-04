using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;

namespace FLDesigner
{
    [XmlType("workflowActivityType")]
    [Serializable]
    public sealed class FLActivityInfo
    {
        private string assemblyName;
        private string typeName;

        [XmlAttribute]
        public string Assembly
        {
            get
            {
                return this.assemblyName;
            }
            set
            {
                this.assemblyName = value;
            }
        }

        [XmlAttribute]
        public string TypeName
        {
            get
            {
                return this.typeName;
            }

            set
            {
                this.typeName = value;
            }
        }
    }


    public sealed class FLActivityInfoSectionHandler : IConfigurationSectionHandler
    {
        object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
        {
            List<FLActivityInfo> workflowActivityTypes = new List<FLActivityInfo>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(FLActivityInfo));
            foreach (XmlNode workflowActivityTypeNode in section.ChildNodes)
            {
                FLActivityInfo workflowActivityType = xmlSerializer.Deserialize(new XmlNodeReader(workflowActivityTypeNode)) as FLActivityInfo;
                if (workflowActivityType != null)
                    workflowActivityTypes.Add(workflowActivityType);
            }

            return workflowActivityTypes;
        }
    }
}
