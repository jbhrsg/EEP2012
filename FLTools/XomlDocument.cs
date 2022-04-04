using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Collections;

/// <summary>
/// Summary description for XomlDocument
/// </summary>
public class XomlDocument : XmlDocument
{
    internal const string XMLN_PREFIX = "xmlns";
    internal const string X_PREFIX = "x";
    internal const string P1_PREFIX = "p1";
    internal const string FLTOOLS_PREFIX = "ns0";

    internal const string XMLN_URL = "http://schemas.microsoft.com/winfx/2006/xaml/workflow";
    internal const string XMLN_X_URL = "http://schemas.microsoft.com/winfx/2006/xaml";
    internal const string XMLN_P1_URL = "http://schemas.microsoft.com/winfx/2006/xaml";
    internal const string XMLN_FLTOOLS_URL = "clr-namespace:FLTools;Assembly=FLTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

    public XomlDocument()
        : base()
    {
        AppendDeclaration();

        DocumentActivity FLSequentialWorkflowActivity = CreateFLActivity("FLSequentialWorkflow");

        FLSequentialWorkflowActivity.Name = "FLSequentialWorkflow";
        FLSequentialWorkflowActivity.Description = "FLSequentialWorkflow1";

        string XMLN_X_PREFIX = string.Format("{0}:{1}", XMLN_PREFIX, X_PREFIX);
        string XMLN_P1_PREFIX = string.Format("{0}:{1}", XMLN_PREFIX, P1_PREFIX);
        string XMLN_FLTOOLS_PREFIX = string.Format("{0}:{1}", XMLN_PREFIX, FLTOOLS_PREFIX);

        FLSequentialWorkflowActivity[XMLN_PREFIX] = XMLN_URL;
        FLSequentialWorkflowActivity[XMLN_X_PREFIX] = XMLN_X_URL;
        FLSequentialWorkflowActivity[XMLN_P1_PREFIX] = XMLN_P1_URL;
        FLSequentialWorkflowActivity[XMLN_FLTOOLS_PREFIX] = XMLN_FLTOOLS_URL;
        this.AppendActivity(FLSequentialWorkflowActivity);
    }

    public DocumentActivity RootActivity
    {
        get
        {
            return new DocumentActivity(this.DocumentElement);
        }
    }

    public void AppendDeclaration()
    {
        this.AppendChild(this.CreateXmlDeclaration("1.0", "utf-8", null));//<?xml version="1.0" encoding="utf-8"?>
    }

    public DocumentActivity CreateActivity(string typeName)
    {
        return CreateActivity(null, typeName, XMLN_URL);
    }

    public DocumentActivity CreateFLActivity(string typeName)
    {
        return CreateActivity(FLTOOLS_PREFIX, typeName, XMLN_FLTOOLS_URL);
    }

    public DocumentActivity CreateActivity(string prefix, string typeName, string uri)
    {
        XmlNode xmlNode = this.CreateElement(prefix, typeName, uri);
        return new DocumentActivity(xmlNode);
    }

    public DocumentActivity AppendActivity(DocumentActivity activity)
    {
        XmlNode node = this.AppendChild(activity.RelatedNode);
        return activity;
    }

    public void RemoveActivity(DocumentActivity activity)
    {
        this.RemoveChild(activity.RelatedNode);
    }
}

public class DocumentActivity
{

    private XmlNode relatedNode = null;
    public XmlNode RelatedNode
    {
        get
        {
            return relatedNode;
        }
    }

    internal DocumentActivity(XmlNode xmlNode)
    {
        if (xmlNode == null)
        {
            throw new ArgumentNullException("xmlNode");
        }
        relatedNode = xmlNode;
    }

    public DocumentActivity ParentActivity
    {
        get
        {
            if (RelatedNode.ParentNode != null && !RelatedNode.ParentNode.Equals(RelatedNode.OwnerDocument))
            {
                return new DocumentActivity(RelatedNode.ParentNode);
            }
            return null;
        }
    }

    public List<DocumentActivity> ChildActivities
    {
        get
        {
            List<DocumentActivity> childs = new List<DocumentActivity>();
            foreach (XmlNode node in RelatedNode.ChildNodes)
            {
                childs.Add(new DocumentActivity(node));
            }
            return childs;
        }
    }

    public string Type
    {
        get
        {
            return RelatedNode.LocalName;
        }
    }

    public string Name
    {
        get
        {
            return this[string.Format("{0}:Name", XomlDocument.X_PREFIX)];
        }
        set
        {
            XmlAttribute attribute = RelatedNode.Attributes[string.Format("{0}:Name", XomlDocument.X_PREFIX)];
            if (attribute == null)
            {
                attribute = RelatedNode.OwnerDocument.CreateAttribute(XomlDocument.X_PREFIX, "Name", XomlDocument.XMLN_X_URL);
                RelatedNode.Attributes.Append(attribute);
            }
            this[string.Format("{0}:Name", XomlDocument.X_PREFIX)] = value;
        }
    }

    public string Description
    {
        get
        {
            return this["Description"];
        }
        set
        {
            this["Description"] = value;
        }
    }

    public string this[string key]
    {
        get
        {
            XmlAttribute attribute = RelatedNode.Attributes[key];
            return attribute == null ? null : attribute.Value;
        }
        set
        {
            XmlAttribute attribute = RelatedNode.Attributes[key];
            if (attribute == null)
            {
                attribute = RelatedNode.OwnerDocument.CreateAttribute(key);
                RelatedNode.Attributes.Append(attribute);
            }
            if (string.IsNullOrEmpty(value))
            {
                attribute.Value = string.Format("{{{0}:Null}}", XomlDocument.X_PREFIX);
            }
            else
            {
                attribute.Value = value;
            }
        }
    }

    public DocumentActivity AppendChildActivity(DocumentActivity activity)
    {
        XmlNode node = RelatedNode.AppendChild(activity.RelatedNode);
        return activity;
    }

    public void RemoveChildActivity(DocumentActivity activity)
    {
        RelatedNode.RemoveChild(activity.RelatedNode);
    }

    public override string ToString()
    {
        return Name;
    }
}