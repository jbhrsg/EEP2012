using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using Srvtools;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.Design;
using System.Collections;
using System.Xml;

namespace FLTools
{
    [Designer(typeof(DataSourceDesigner), typeof(IDesigner))]
    public class FLWebPreview : WebInfoBaseControl, INamingContainer
    {
        protected override void CreateChildControls()
        {
            GridView gridView = new GridView();
            gridView.ID = "GridView";
            this.Controls.Add(gridView);
            EmptyDataTemplate template = new EmptyDataTemplate();
            if (ShowEmptyInfo)
            {
                gridView.EmptyDataTemplate = template;
            }
            gridView.CssClass = CssClass;
            gridView.AutoGenerateColumns = true;
            gridView.DataSource = CreateDataSource();
            gridView.DataBind();
        }

        private DataView CreateDataSource()
        {
            Hashtable keyValues = new Hashtable();
            if (Navigator == null)
            {
                string cqs = System.Web.HttpUtility.UrlDecode(this.Parent.Page.ClientQueryString);
                cqs = cqs.ToUpper();
                if (cqs.Contains("WHERESTRING"))
                {
                    int start = cqs.IndexOf("WHERESTRING");
                    string WHERESTRING = cqs.Substring(start + 12);
                    int end = WHERESTRING.IndexOf('&');
                    WHERESTRING = end > 0 ? WHERESTRING.Substring(0, end) : WHERESTRING;
                    string[] keyValuesArray = WHERESTRING.Split(';');
                    foreach (string keyValue in keyValuesArray)
                    {
                        int splitStart = keyValue.IndexOf("=");
                        keyValues.Add(keyValue.Substring(0, splitStart), keyValue.Substring(splitStart + 1));
                    }

                    string listid = "";
                    Guid id = new Guid();
                    if (cqs.Contains("LISTID"))
                    {
                        int listidStart = cqs.IndexOf("LISTID");
                        listid = cqs.Substring(listidStart + 7);
                        int listidEnd = listid.IndexOf('&');
                        listid = listidEnd > 0 ? listid.Substring(0, listidEnd) : listid;
                        id = new Guid(listid);
                    }

                    string activityname = "";
                    if (cqs.ToUpper().Contains("FLOWPATH"))
                    {
                        int flowPathStart = cqs.IndexOf("FLOWPATH");
                        string FLOWPATH = cqs.Substring(flowPathStart + 9);
                        int flowPathEnd = FLOWPATH.IndexOf('&');
                        FLOWPATH = flowPathEnd > 0 ? FLOWPATH.Substring(0, flowPathEnd) : FLOWPATH;
                        activityname = FLOWPATH.Split(';')[1];
                    }

                    string flowFlieName = "";
                    if (cqs.ToUpper().Contains("FLOWFILENAME"))
                    {
                        int flowFlieNameStart = cqs.IndexOf("FLOWFILENAME");
                        flowFlieName = cqs.Substring(flowFlieNameStart + 13);
                        int flowFlieNameEnd = flowFlieName.IndexOf('&');
                        flowFlieName = flowFlieNameEnd > 0 ? flowFlieName.Substring(0, flowFlieNameEnd) : flowFlieName;
                    }

                    string keys = "", values = "";
                    IEnumerator enumer = keyValues.GetEnumerator();
                    while (enumer.MoveNext())
                    {
                        keys += ((DictionaryEntry)enumer.Current).Key.ToString() + ";";
                        values += ((DictionaryEntry)enumer.Current).Key.ToString() + "=" + ((DictionaryEntry)enumer.Current).Value.ToString() + ";";
                    }
                    if (keys != "" && values != "")
                    {
                        keys = keys.Substring(0, keys.LastIndexOf(';'));
                        values = values.Substring(0, values.LastIndexOf(';'));
                    }
                    string role = "";
                    if (cqs.ToUpper().Contains("SENDTOID"))
                    {
                        int roleStart = cqs.IndexOf("SENDTOID");
                        role = cqs.Substring(roleStart + 9);
                        int roleEnd = role.IndexOf('&');
                        role = role.Substring(0, roleEnd);
                    }
                    else
                    {
                        role = Srvtools.CliUtils.Roles;
                    }
                    DataSet host = new DataSet();
                    if (Page.Session["PreviewHost"] == null)
                    {
                        string sql1 = "SELECT FLOW_ID FROM SYS_TODOLIST WHERE LISTID = '" + listid + "'";
                        object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql1 });
                        if (ret1 != null && (int)ret1[0] == 0)
                        {
                            string flowID = ((DataSet)ret1[1]).Tables[0].Rows[0][0].ToString();
                            string sql2 = "SELECT FLDefinition FROM SYS_FLDefinition WHERE FLTypeId = '" + flowID + "'";
                            object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql2 });
                            if (ret2 != null && (int)ret2[0] == 0)
                            {
                                string FLDefinition = ((DataSet)ret2[1]).Tables[0].Rows[0][0].ToString();
                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(FLDefinition);
                                string tableName = xmlDoc.SelectSingleNode("FLRootActivity").Attributes["TableName"].Value;
                                string whereString = values.Replace(";", " and ");
                                string sql3 = "select * from " + tableName + " where " + whereString;
                                //object[] ret3 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql3 });
                                //if (ret3 != null && (int)ret3[0] == 0)
                                //{
                                //    host = (DataSet)ret3[1];
                                //}
                                //else
                                //{
                                //    return null;
                                //}
                                host = CliUtils.GetSqlCommand("GLModule", "ExcuteWorkFlow", new InfoDataSet(), "", CliUtils.fCurrentProject, sql3);
                             
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        host = (DataSet)this.Page.Session["PreviewHost"];
                    }
                    object[] ret = CliUtils.CallFLMethod("Preview", new object[] { id
                , new object[] { flowFlieName + ".xoml", "", activityname, host, role }, new object[] { keys, values }});
                    if ((int)ret[0] == 0 && ret[1] != null)
                    {
                        DataTable dataTable = ret[1] as DataTable;
                        dataTable.Columns.Remove("角色名");
                        return dataTable.DefaultView;
                    }
                    return null;
                }
                else
                {
                    DataTable tableEmpty = new DataTable();
                    return tableEmpty.DefaultView;
                }
            }
            else
            {
                keyValues = Navigator.GetKeyValues(false);

                if (keyValues.Count == 0)
                {
                    DataTable tableEmpty = new DataTable();
                    return tableEmpty.DefaultView;
                }
                string keys = "", values = "";
                IEnumerator enumer = keyValues.GetEnumerator();
                while (enumer.MoveNext())
                {
                    keys += ((DictionaryEntry)enumer.Current).Key.ToString() + ";";
                    values += ((DictionaryEntry)enumer.Current).Key.ToString() + "=" + ((DictionaryEntry)enumer.Current).Value.ToString() + ";";
                }
                if (keys != "" && values != "")
                {
                    keys = keys.Substring(0, keys.LastIndexOf(';'));
                    values = values.Substring(0, values.LastIndexOf(';'));
                }
                Guid id = IsApprove ? new Guid(Navigator.LISTID) : Guid.Empty;
                string activityname = IsApprove && !string.IsNullOrEmpty(Navigator.FLOWPATH) ? Navigator.FLOWPATH.ToString().Split(';')[1] : "";

                string role = DefaultRole;
                DataSet host = (DataSet)this.Page.Session["PreviewHost"];
                object[] ret = CliUtils.CallFLMethod("Preview", new object[] { id
                , new object[] { Navigator.FLOWFILENAME + ".xoml", "", activityname, host, role }, new object[] { keys, values }});
                if ((int)ret[0] == 0 && ret[1] != null)
                {
                    DataTable dataTable = ret[1] as DataTable;
                    dataTable.Columns.Remove("角色名");
                    return dataTable.DefaultView;
                }
                return null;
            }
        }

        private bool IsApprove
        {
            get
            {
                return !string.IsNullOrEmpty(Navigator.LISTID);
            }
        }

        private string navigatorID;

        public string NavigatorID
        {
            get
            {
                return navigatorID;
            }
            set
            {
                navigatorID = value;
            }
        }

        private FLWebNavigator navigator;
        [Browsable(false)]
        public FLWebNavigator Navigator
        {
            get
            {
                if (navigator == null)
                {
                    navigator = (FLWebNavigator)GetObjByID(NavigatorID);
                }
                return navigator;
            }
        }

        private string cssClass;
        public string CssClass
        {
            get
            {
                return cssClass;
            }
            set
            {
                cssClass = value;
            }
        }


        private string DefaultRole
        {
            get
            {
                if (ViewState["DefaultRole"] == null)
                {
                    string flowDesc = IsApprove ? GloFix.GetFlowDesc(Navigator.LISTID, true) : GloFix.GetFlowDesc(Navigator.FLOWFILENAME + ".xoml", false);
                    string curTime = FLTools.GloFix.DateTimeString(DateTime.Now);
                    string sql = "SELECT GROUPID,GROUPNAME FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + CliUtils.fLoginUser
                        + "')  AND ISROLE='Y' UNION SELECT ROLE_ID AS GROUPID,GROUPS.GROUPNAME  FROM SYS_ROLES_AGENT LEFT JOIN GROUPS ON SYS_ROLES_AGENT.ROLE_ID=GROUPS.GROUPID WHERE (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC='"
                        + flowDesc + "') AND AGENT='" + CliUtils.fLoginUser + "' AND START_DATE+START_TIME<='" + curTime + "' AND END_DATE+END_TIME>='" + curTime + "'";
                    object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
                    if (ret1 != null && (int)ret1[0] == 0)
                    {
                        DataTable table = ((DataSet)ret1[1]).Tables[0];
                        List<string> roles = new List<string>();
                        foreach (DataRow row in table.Rows)
                        {
                            roles.Add(row["GROUPID"].ToString());
                        }
                        if (roles.Count > 0)
                        {
                            if (roles.Contains(Navigator.SENDTOID))
                            {
                                ViewState["DefaultRole"] = Navigator.SENDTOID;
                            }
                            else
                            {
                                ViewState["DefaultRole"] = roles[0];
                            }
                        }
                    }
                }
                return (string)ViewState["DefaultRole"];
            }

        }
        private bool showEmptyInfo;
        public bool ShowEmptyInfo
        {
            get
            {
                return showEmptyInfo;
            }
            set
            {
                showEmptyInfo = value;
            }
        }
    }

    public class EmptyDataTemplate : ITemplate
    {
        public void InstantiateIn(System.Web.UI.Control container)
        {
            Label label = new Label();
            label.Text = "没有要显示的数据";
            container.Controls.Add(label);
        }
    }
}
