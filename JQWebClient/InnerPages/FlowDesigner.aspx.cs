using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Win32;
using System.Xml;
using System.IO;
using System.Text;

public partial class InnerPages_FlowDesigner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string fileName = this.Request.QueryString["FlowFileName"];
            this.imageMapDesigner.ImageUrl = "~/Image/" + fileName + ".jpg";

            setVS(fileName);
        }
    }

    private string ConvertUserParameters(string userParameters)
    {
        StringBuilder builder = new StringBuilder();
        string[] lstUserParameters = userParameters.Split(';');
        foreach (string userParameter in lstUserParameters)
        {
            if (!string.IsNullOrEmpty(userParameter) && userParameter.IndexOf('=') != -1)
            {
                string key = userParameter.Substring(0, userParameter.IndexOf('=') + 1);
                string value = HttpUtility.UrlEncode(userParameter.Substring(userParameter.IndexOf('=') + 1));
                builder.Append("&" + key + value);
            }
        }
        return builder.ToString();
    }

    private void setVS(string fileName)
    {
        string xmlFile = this.Request.PhysicalApplicationPath + "Image\\" + fileName + ".xml";
        if (File.Exists(xmlFile))
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            XmlNode nRoot = doc.SelectSingleNode("FlowDetails");
            if (nRoot != null)
            {
                //string flowFileName = "", fullFlowFileName = "", formName = "", flowNavMode = "", navMode = "", paramters = "";
                string flowFileName = "", formName = "", flowNavMode = "", navMode = "", paramters = "";

                XmlNode nFormName = nRoot.SelectSingleNode("FormName");
                if (nFormName != null) formName = nFormName.InnerText;
                XmlNode nFlowNavMode = nRoot.SelectSingleNode("FlowNavMode");
                if (nFlowNavMode != null) flowNavMode = nFlowNavMode.InnerText;
                XmlNode nNavMode = nRoot.SelectSingleNode("NavMode");
                if (nNavMode != null) navMode = nNavMode.InnerText;
                XmlNode nFlowFileName = nRoot.SelectSingleNode("FlowFileName");
                if (nFlowFileName != null)
                {
                    flowFileName = nFlowFileName.InnerText.ToUpper();
                    //fullFlowFileName = nFlowFileName.InnerText;
                    if (!string.IsNullOrEmpty(flowFileName) && flowFileName.IndexOf(@"WORKFLOW\") != -1)
                    {
                        flowFileName = flowFileName.Substring(flowFileName.IndexOf(@"WORKFLOW\") + 9);
                    }
                }
                XmlNode nParamters = nRoot.SelectSingleNode("Parameters");
                if (nParamters != null) paramters = ConvertUserParameters(nParamters.InnerText);
                XmlNode nImageWidth = nRoot.SelectSingleNode("ImageWidth");
                if (nImageWidth != null)
                {
                    if (!string.IsNullOrEmpty(formName))
                    {
                        RectangleHotSpot hot = new RectangleHotSpot();
                        hot.HotSpotMode = HotSpotMode.Navigate;
                        hot.Top = 71;
                        hot.Bottom = 111;
                        hot.Left = Convert.ToInt32(nImageWidth.InnerText) / 2 - 65;
                        hot.Right = Convert.ToInt32(nImageWidth.InnerText) / 2 + 65;
                        formName = formName.Replace('.', '/');
                        //hot.NavigateUrl = "~/" + formName + ".aspx?FLOWFILENAME=" + HttpUtility.UrlEncode(flowFileName) + "&FULLFLOWFILENAME=" + HttpUtility.UrlEncode(fullFlowFileName) + "&NAVMODE=" + navMode + "&FLNAVMODE=" + flowNavMode + paramters;
                        hot.NavigateUrl = "~/" + formName + ".aspx?FLOWFILENAME=" + HttpUtility.UrlEncode(flowFileName) + "&NAVMODE=" + navMode + "&FLNAVMODE=" + flowNavMode + paramters;
                        this.imageMapDesigner.HotSpots.Add(hot);
                    }
                }

                XmlNode nFLHyperLink = nRoot.SelectSingleNode("FLHyperLink");
                if (nFLHyperLink != null)
                {
                    XmlNode node = nFLHyperLink.FirstChild;
                    while (node != null)
                    {
                        RectangleHotSpot hot = new RectangleHotSpot();
                        hot.HotSpotMode = HotSpotMode.Navigate;
                        string linkFlow = node.Attributes["LinkFlow"].Value;
                        string linkFormName = node.Attributes["WebFormName"].Value;
                        string linkParameters = ConvertUserParameters(node.Attributes["Parameters"].Value);

                        if (!String.IsNullOrEmpty(linkFlow))
                        {
                            string xmlFile2 = this.Request.PhysicalApplicationPath + "Image\\" + linkFlow.Split(new char[] { '\\', '.' })[1] + ".xml";
                            XmlDocument doc2 = new XmlDocument();
                            doc2.Load(xmlFile2);
                            XmlNode nRoot2 = doc2.SelectSingleNode("FlowDetails");
                            if (nRoot2 != null)
                            {
                                string flowFileName2 = "", fullFlowFileName2 = "", formName2 = "", flowNavMode2 = "", navMode2 = "", paramters2 = "";
                                XmlNode nFormName2 = nRoot2.SelectSingleNode("FormName");
                                if (nFormName2 != null) formName2 = nFormName2.InnerText;
                                XmlNode nFlowNavMode2 = nRoot2.SelectSingleNode("FlowNavMode");
                                if (nFlowNavMode2 != null) flowNavMode2 = nFlowNavMode2.InnerText;
                                XmlNode nNavMode2 = nRoot2.SelectSingleNode("NavMode");
                                if (nNavMode2 != null) navMode2 = nNavMode2.InnerText;
                                XmlNode nFlowFileName2 = nRoot2.SelectSingleNode("FlowFileName");
                                if (nFlowFileName2 != null)
                                {
                                    flowFileName2 = nFlowFileName2.InnerText.ToUpper();
                                    fullFlowFileName2 = nFlowFileName2.InnerText;
                                    if (!string.IsNullOrEmpty(flowFileName2) && flowFileName2.IndexOf(@"WORKFLOW\") != -1)
                                    {
                                        flowFileName2 = flowFileName2.Substring(flowFileName2.IndexOf(@"WORKFLOW\") + 9);
                                    }
                                }
                                XmlNode nParamters2 = nRoot2.SelectSingleNode("Parameters");
                                if (nParamters2 != null) paramters2 = ConvertUserParameters(nParamters2.InnerText);

                                if (linkFlow != null && linkFlow != "")
                                {
                                    if (linkFlow.IndexOf('\\') != -1)
                                        linkFlow = linkFlow.Substring(linkFlow.IndexOf('\\') + 1);
                                    formName2 = formName2.Replace('.', '/');
                                    hot.NavigateUrl = "~/InnerPages/FlowDesigner.aspx?FLOWFILENAME=" + linkFlow.Split('.')[0] + "&FULLFLOWFILENAME=" + HttpUtility.UrlEncode(fullFlowFileName2) + "&NAVMODE=" + navMode2 + "&FLNAVMODE=" + flowNavMode2 + paramters2;
                                    //hot.NavigateUrl = "~/" + formName2 + ".aspx?FLOWFILENAME=" + HttpUtility.UrlEncode(flowFileName2) + "&FULLFLOWFILENAME=" + HttpUtility.UrlEncode(fullFlowFileName2) + "&NAVMODE=" + navMode2 + "&FLNAVMODE=" + flowNavMode2 + paramters2;
                                    //hot.NavigateUrl = this.Request.FilePath + "?FlowFileName=" + linkFlow + "&FULLFLOWFILENAME=" + HttpUtility.UrlEncode(fullFlowFileName2) + "&NAVMODE=" + navMode2 + "&FLNAVMODE=" + flowNavMode2 + paramters2;
                                }
                                else if (linkFormName != null && linkFormName != "")
                                {
                                    linkFormName = linkFormName.Replace('.', '/');
                                    hot.NavigateUrl = "~/" + linkFormName + ".aspx?FLOWFILENAME=" + HttpUtility.UrlEncode(flowFileName) + linkParameters;
                                }
                                if (node.Attributes["Top"] == null)
                                {
                                    hot.Top = 71;
                                    hot.Bottom = 111;
                                    hot.Left = Convert.ToInt32(nImageWidth.InnerText) / 2 - 65;
                                    hot.Right = Convert.ToInt32(nImageWidth.InnerText) / 2 + 65;
                                }
                                else
                                {
                                    hot.Top = Convert.ToInt32(node.Attributes["Top"].Value);
                                    hot.Bottom = Convert.ToInt32(node.Attributes["Bottom"].Value);
                                    hot.Left = Convert.ToInt32(node.Attributes["Left"].Value);
                                    hot.Right = Convert.ToInt32(node.Attributes["Right"].Value);
                                }
                                this.imageMapDesigner.HotSpots.Add(hot);
                                node = node.NextSibling;
                            }
                        }
                        else if (!String.IsNullOrEmpty(linkFormName))
                        {
                            linkFormName = linkFormName.Replace('.', '/');
                            hot.NavigateUrl = "~/" + linkFormName + ".aspx?" + linkParameters;

                            if (node.Attributes["Top"] == null)
                            {
                                hot.Top = 71;
                                hot.Bottom = 111;
                                hot.Left = Convert.ToInt32(nImageWidth.InnerText) / 2 - 65;
                                hot.Right = Convert.ToInt32(nImageWidth.InnerText) / 2 + 65;
                            }
                            else
                            {
                                hot.Top = Convert.ToInt32(node.Attributes["Top"].Value);
                                hot.Bottom = Convert.ToInt32(node.Attributes["Bottom"].Value);
                                hot.Left = Convert.ToInt32(node.Attributes["Left"].Value);
                                hot.Right = Convert.ToInt32(node.Attributes["Right"].Value);
                            }
                            this.imageMapDesigner.HotSpots.Add(hot);
                            node = node.NextSibling;
                        }
                    }
                }

                XmlNode nFLQuery = nRoot.SelectSingleNode("FLQuery");
                if (nFLQuery != null)
                {
                    XmlNode node = nFLQuery.FirstChild;
                    while (node != null)
                    {
                        RectangleHotSpot hot = new RectangleHotSpot();
                        hot.HotSpotMode = HotSpotMode.Navigate;
                        string queryFormName = node.Attributes["WebFormName"].Value;
                        queryFormName = queryFormName.Replace('.', '/');
                        string queryParameters = ConvertUserParameters(node.Attributes["Parameters"].Value);
                        hot.NavigateUrl = "~/" + queryFormName + ".aspx?FLOWFILENAME=" + HttpUtility.UrlEncode(flowFileName) + "&NAVMODE=3&FLNAVMODE=4" + queryParameters;
                        hot.Top = Convert.ToInt32(node.Attributes["Top"].Value);
                        hot.Bottom = Convert.ToInt32(node.Attributes["Bottom"].Value);
                        hot.Left = Convert.ToInt32(node.Attributes["Left"].Value);
                        hot.Right = Convert.ToInt32(node.Attributes["Right"].Value);
                        this.imageMapDesigner.HotSpots.Add(hot);
                        node = node.NextSibling;
                    }
                }
            }
        }
    }
}
