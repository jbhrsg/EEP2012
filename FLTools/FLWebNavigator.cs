using System;
using Srvtools;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.Collections;
using System.Web;
using System.Xml;
using System.Resources;
using FLCore;

namespace FLTools
{
    public class FLWebNavigator : WebNavigator, INavigatorSecurity
    {
        public FLWebNavigator()
        {
            _flStates = new FLWebNavigatorStateCollection(this, typeof(FLWebNavigatorStateItem));
        }

        #region Properties
        [Category("Infolight")]
        [DefaultValue(false)]
        public bool AutoSubmit
        {
            get
            {
                object obj = this.ViewState["AutoSubmit"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return false;
            }
            set
            {
                this.ViewState["AutoSubmit"] = value;
            }
        }

        [Category("Infolight")]
        [DefaultValue("")]
        [Editor(typeof(GetAlias), typeof(System.Drawing.Design.UITypeEditor))]
        public string DBAlias
        {
            get
            {
                object obj = this.ViewState["DBAlias"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
            set
            {
                this.ViewState["DBAlias"] = value;
            }
        }

        [Category("Infolight")]
        [Editor(typeof(GetMenuID), typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public string MenuId
        {
            get
            {
                object obj = this.ViewState["MenuId"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
            set
            {
                this.ViewState["MenuId"] = value;
            }
        }

        [Browsable(false)]
        public string PreviousFLState
        {
            get
            {
                object obj = this.ViewState["PreviousFLState"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string CurrentFLState
        {
            get
            {
                object obj = this.ViewState["CurrentFLState"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string LISTID
        {
            get
            {
                object obj = this.ViewState["LISTID"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string FLOWPATH
        {
            get
            {
                object obj = this.ViewState["FLOWPATH"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string FLOWFILENAME
        {
            get
            {
                object obj = this.ViewState["FLOWFILENAME"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string WHERESTRING
        {
            get
            {
                object obj = this.ViewState["WHERESTRING"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string NAVIGATOR_MODE
        {
            get
            {
                object obj = this.ViewState["NAVIGATOR_MODE"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string FLNAVIGATOR_MODE
        {
            get
            {
                object obj = this.ViewState["FLNAVIGATOR_MODE"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string FLOW_IMPORTANT
        {
            get
            {
                object obj = this.ViewState["FLOW_IMPORTANT"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "0";
            }
        }

        [Browsable(false)]
        public string FLOW_URGENT
        {
            get
            {
                object obj = this.ViewState["FLOW_URGENT"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "0";
            }
        }

        [Browsable(false)]
        public string STATUS
        {
            get
            {
                object obj = this.ViewState["STATUS"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string PLUSAPPROVE
        {
            get
            {
                object obj = this.ViewState["PLUSAPPROVE"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Browsable(false)]
        public string MULTISTEPRETURN
        {
            get
            {
                object obj = this.ViewState["MULTISTEPRETURN"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "0";
            }
        }

        public string ATTACHMENTS
        {
            get
            {
                object obj = this.ViewState["ATTACHMENTS"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        public string SENDTOID
        {
            get
            {
                object obj = this.ViewState["SENDTOID"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
        }

        [Category("Infolight")]
        [DefaultValue(false)]
        public bool OrganizationControl
        {
            get
            {
                object obj = this.ViewState["OrganizationControl"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return false;
            }
            set
            {
                this.ViewState["OrganizationControl"] = value;
            }
        }

        private string remarkField;

        public string RemarkField
        {
            get { return remarkField; }
            set { remarkField = value; }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Page.IsPostBack)
            {
                if (this.Page.Request.QueryString != null && this.Page.Request.QueryString.Count > 0)
                {
                    //submit
                    this.ViewState["FLOWFILENAME"] = this.getFlowFileName(this.Page.Request.QueryString["FLOWFILENAME"]);
                    //approve
                    this.ViewState["LISTID"] = this.Page.Request.QueryString["LISTID"];
                    this.ViewState["FLOWPATH"] = this.Page.Request.QueryString["FLOWPATH"];
                    string ws = this.Page.Request.QueryString["WHERESTRING"];
                    if (ws != null && ws.IndexOf("''") != -1)
                        ws = ws.Replace("''", "'");
                    this.ViewState["WHERESTRING"] = ws;
                    this.ViewState["FLOW_IMPORTANT"] = this.Page.Request.QueryString["ISIMPORTANT"];
                    this.ViewState["FLOW_URGENT"] = this.Page.Request.QueryString["ISURGENT"];
                    this.ViewState["STATUS"] = this.Page.Request.QueryString["STATUS"];
                    this.ViewState["PLUSAPPROVE"] = this.Page.Request.QueryString["PLUSAPPROVE"];
                    this.ViewState["MULTISTEPRETURN"] = this.Page.Request.QueryString["MULTISTEPRETURN"];
                    this.ViewState["ATTACHMENTS"] = this.Page.Request.QueryString["ATTACHMENTS"];
                    this.ViewState["SENDTOID"] = this.Page.Request.QueryString["SENDTOID"];
                    //both
                    this.ViewState["NAVIGATOR_MODE"] = this.Page.Request.QueryString["NAVMODE"];
                    this.ViewState["FLNAVIGATOR_MODE"] = this.Page.Request.QueryString["FLNAVMODE"];
                    this.ViewState["VDSNAME"] = string.IsNullOrEmpty(this.Page.Request.QueryString["VDSNAME"]) ? CliUtils.fCurrentProject : this.Page.Request.QueryString["VDSNAME"];
                }
                CompositeDataBoundControl bindingControl = this.GetBindingObject() as CompositeDataBoundControl;
                if (bindingControl != null)
                {
                    bindingControl.Load += new EventHandler(bindingControl_Load);
                }
            }
        }


        void bindingControl_Load(object sender, EventArgs e)
        {
            this.InitStates(this.FLNAVIGATOR_MODE, this.NAVIGATOR_MODE);
            InitFilterData();
        }

        private string getFlowFileName(string name)
        {
            if (!string.IsNullOrEmpty(name) && !name.Contains("\\WorkFlow\\"))
            {
                object[] ret1 = CliUtils.CallMethod("GLModule", "GetServerPath", null);
                if(ret1 != null && (int)ret1[0] == 0)
                    return (string)ret1[1] + "\\WorkFlow\\" + name;
            }
            return name;
        }

        protected override string getFlowUrl()
        {
            string flowFileName = ViewState["FLOWFILENAME"] == null ? "" : ViewState["FLOWFILENAME"].ToString();
            string listId = ViewState["LISTID"] == null ? "" : ViewState["LISTID"].ToString();
            string flowPath = ViewState["FLOWPATH"] == null ? "" : ViewState["FLOWPATH"].ToString();
            string whereString = ViewState["WHERESTRING"] == null ? "" : ViewState["WHERESTRING"].ToString();
            string navMode = ViewState["NAVIGATOR_MODE"] == null ? "" : ViewState["NAVIGATOR_MODE"].ToString();
            string flNavMode = ViewState["FLNAVIGATOR_MODE"] == null ? "" : ViewState["FLNAVIGATOR_MODE"].ToString();
            return "&FLOWFILENAME=" + HttpUtility.UrlEncode(flowFileName) + "&LISTID=" + HttpUtility.UrlEncode(listId) + "&FLOWPATH=" + HttpUtility.UrlEncode(flowPath) + "&WHERESTRING=" + HttpUtility.UrlEncode(whereString) + "&NAVMODE=" + navMode + "&FLNAVMODE=" + flNavMode;
        }

        private void InitStates(string flNavigatorMode, string navigatorMode)
        {
            if (flNavigatorMode != "" && navigatorMode != "" && this.FLActive)
            {
                string flMode = "", mode = "";
                switch (flNavigatorMode)
                {
                    case "0":
                        flMode = "Submit";
                        if (this.LISTID != "" && this.FLOWFILENAME == "")
                            flMode = "Approve";
                        if (this.STATUS == "NF")
                            flMode = "FSubmit";
                        else if (this.STATUS == "NR")
                            flMode = "RSubmit";
                        break;
                    case "1":
                        flMode = "Approve";
                        break;
                    case "2":
                        flMode = "Return";
                        break;
                    case "3":
                        flMode = "Notify";
                        break;
                    case "4":
                        flMode = "Inquery";
                        break;
                    case "5":
                        flMode = "Continue";
                        break;
                    case "6":
                        flMode = "None";
                        break;
                    case "7":
                        flMode = "Plus";
                        break;
                    case "8":
                        flMode = "Lock";
                        break;
                    default:
                        flMode = flNavigatorMode;
                        break;
                }
                if (flMode != "")
                {
                    this.SetFLState(flMode);
                }
                switch (navigatorMode)
                {
                    case "0":
                        mode = "Normal";
                        break;
                    case "1":
                        mode = "Insert";
                        break;
                    case "2":
                        mode = "Modify";
                        break;
                    case "3":
                        mode = "Inquery";
                        break;
                    case "4":
                        mode = "Prepare";
                        break;
                    /*以下状态为系统内定,不允许用户自行设置*/
                    case "5":
                        mode = "PreInsert";
                        break;
                    default:
                        mode = navigatorMode;
                        break;
                }
                if (mode != "")
                {
                    if (mode == "Inquery" || mode == "Prepare")
                    {
                        WebDataSource wds = this.GetDataSource() as WebDataSource;
                        if (wds != null)
                        {
                            if (wds.IsEmpty)
                            {
                                mode = "Inquery";
                            }
                            else
                            {
                                int record = wds.InnerDataSet.Tables[0].Rows.Count;
                                if (record == 1)
                                {
                                    mode = string.Format("{0}Single", mode);
                                }
                                else
                                {
                                    mode = string.Format("{0}Multi", mode);
                                }
                            }
                        }
                    }
                    this.SetNavState(mode);
                }
            }
        }

        private void InitFilterData()
        {
            if (this.WHERESTRING != "")
            {
                object dataSource = this.GetDataSource();
                if (dataSource != null && dataSource is WebDataSource)
                {
                    WebDataSource wds = dataSource as WebDataSource;

                    string[] listWhere = WHERESTRING.Split(';');

                    string sql = DBUtils.GetCommandText(wds);
                    string tableName = DBUtils.GetTableName(sql, false);

                    string strWhere = string.Empty;
                    foreach (string str in listWhere)
                    {
                        if (strWhere.Length > 0)
                        {
                            strWhere += " AND ";
                        }
                        strWhere += tableName + "." + str.Trim();
                    }

                    wds.SetWhere(strWhere);


                    object obj = this.GetObjByID(this.BindingObject);
                    if (obj != null && obj is CompositeDataBoundControl)
                    {
                        CompositeDataBoundControl dbc = obj as CompositeDataBoundControl;
                        dbc.DataBind();
                    }
                }
            }
        }

        private FLWebNavigatorStateCollection _flStates;
        [Category("Infolight")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(CollectionConverter))]
        public FLWebNavigatorStateCollection FLStates
        {
            get
            {
                return _flStates;
            }
        }

        [Category("Infolight")]
        [DefaultValue(true)]
        public bool FLActive
        {
            get
            {
                object obj = this.ViewState["FLActive"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return true;
            }
            set
            {
                this.ViewState["FLActive"] = value;
            }
        }

        [Category("Infolight")]
        [DefaultValue(false)]
        public bool FLNotifyOff
        {
            get
            {
                object obj = this.ViewState["FLNotifyOff"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return false;
            }
            set
            {
                this.ViewState["FLNotifyOff"] = value;
            }
        }

        [Category("Infolight")]
        [DefaultValue(false)]
        public bool FLNotifySecControl
        {
            get
            {
                object obj = this.ViewState["FLNotifySecControl"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return false;
            }
            set
            {
                this.ViewState["FLNotifySecControl"] = value;
            }
        }

        [Category("Infolight")]
        [DefaultValue(false)]
        public bool FLRejectNotify
        {
            get
            {
                object obj = this.ViewState["FLRejectNotify"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return false;
            }
            set
            {
                this.ViewState["FLRejectNotify"] = value;
            }
        }

        protected override void GridViewFlowCommand(CommandEventArgs e)
        {
            ExcuteCommand(e);
        }

        protected override void DetailsViewFlowCommand(CommandEventArgs e)
        {
            ExcuteCommand(e);
        }

        protected override void FormViewFlowCommand(CommandEventArgs e)
        {
            ExcuteCommand(e);
        }

        private void ExcuteCommand(CommandEventArgs e)
        {
            if (e.CommandName == "cmdFlowDelete")
            {
                FlowDelete();
            }
            else if (e.CommandName == "cmdReject")
            {
                Reject();
            }
            else if (e.CommandName == "cmdPause" && !this.OrganizationControl)
            {
                Pause();
            }
            ClientScriptManager csm = this.Page.ClientScript;
            string ref_script = "var lnk_element=window.parent.document.getElementById('lnkRefresh');if(lnk_element){window.parent.__doPostBack('lnkRefresh','');}";
            csm.RegisterStartupScript(this.GetType(), "scriptRefresh", ref_script, true);
        }

        protected override void FlowApply()
        {
            if (this.FLActive)
            {
                if (this.ViewState["CurrentNavState"].ToString() == "Insert")
                {
                    this.SetNavState("PreInsert");
                    if (this.AutoSubmit)
                    {
                        if (this.STATUS == "N")
                            CliUtils.RegisterStartupScript(this, GloScript("Approve"));
                        else
                            CliUtils.RegisterStartupScript(this, GloScript("Submit"));
                    }
                }
                else if (this.ViewState["CurrentNavState"].ToString() == "Prepare")
                {
                    WebDataSource wds = this.GetDataSource() as WebDataSource;
                    if (wds != null)
                    {
                        if (wds.IsEmpty)
                        {
                            this.SetNavState("Inquery");
                        }
                        else
                        {
                            int record = wds.InnerDataSet.Tables[0].Rows.Count;
                            if (record == 1)
                            {
                                this.SetNavState("PrepareSingle");
                            }
                            else
                            {
                                this.SetNavState("PrepareMulti");
                            }
                        }
                    }
                }
            }
        }

        protected override void FlowAbort()
        {
            if (this.FLActive)
            {
                if (this.ViewState["PreFLState"] != null)
                    this.SetNavState(this.ViewState["PreFLState"].ToString());
                else
                    this.SetNavState("PreInsert");
            }
        }

        protected override void FlowOK()
        {
            if (this.FLActive)
            {
                object dataSource = this.GetDataSource();
                if (dataSource != null && dataSource is WebDataSource)
                {
                    WebDataSource wds = dataSource as WebDataSource;
                    if (this.ViewState["CurrentNavState"].ToString() == "Insert")
                    {
                        if (wds.InnerDataSet.Tables[0].ChildRelations.Count > 0)  //is Master of the Master+Detail
                        {
                            if (!wds.InnerDataSet.HasChanges())
                            {
                                this.SetNavState("PreInsert");
                            }
                        }
                        else if (wds.AutoApply) //is Master of single
                        {
                            this.SetNavState("PreInsert");
                        }
                    }
                    else if (this.ViewState["CurrentNavState"].ToString() == "Prepare")
                    {
                        if (wds.IsEmpty)
                        {
                            this.SetNavState("Inquery");
                        }
                        else
                        {
                            int record = wds.InnerDataSet.Tables[0].Rows.Count;
                            if (record == 1)
                            {
                                this.SetNavState("PrepareSingle");
                            }
                            else
                            {
                                this.SetNavState("PrepareMulti");
                            }
                        }
                    }
                }
            }
        }

        protected override void FlowCancel()
        {
            if (this.FLActive)
            {
                if (this.ViewState["PreFLState"] != null)
                    this.SetNavState(this.ViewState["PreFLState"].ToString());
                else
                    this.SetNavState("PreInsert");
            }
        }

        //protected override

        public bool Reject()
        {
            Hashtable keyValues = GetKeyValues(false);
            string sKeys = "", sValues = "", svs = "";
            IEnumerator enumer = keyValues.GetEnumerator();
            while (enumer.MoveNext())
            {
                sKeys += ((DictionaryEntry)enumer.Current).Key.ToString() + ";";
                sValues += ((DictionaryEntry)enumer.Current).Key.ToString() + "=" + ((DictionaryEntry)enumer.Current).Value.ToString() + ";";
                svs += ((DictionaryEntry)enumer.Current).Value.ToString();
            }
            if (sKeys != "" && sValues != "")
            {
                sKeys = sKeys.Substring(0, sKeys.LastIndexOf(';'));
                sValues = sValues.Substring(0, sValues.LastIndexOf(';'));
            }
            string[] activities = this.FLOWPATH.Split(';');
            object[] objParams = CliUtils.CallFLMethod("Reject", new object[] { new Guid(this.LISTID), new object[] { activities[0], activities[1], this.FLRejectNotify ? "1" : "0", this.GetPorvider() }, new object[] { sKeys, sValues } });
            if (Convert.ToInt16(objParams[0]) == 0)
            {
                ClearData();
                return true;
            }
            else
                return false;
        }

        public bool FlowDelete()
        {
            bool ret = false;
            object[] objParams = CliUtils.CallFLMethod("DeleteNotify", new object[] { this.LISTID, this.FLOWPATH });
            ClientScriptManager csm = this.Page.ClientScript;
            if (Convert.ToInt16(objParams[0]) == 0)
            {
                ret = true;
                ClearData();
                string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowDeleted1");
                csm.RegisterClientScriptBlock(this.GetType(), "FlowDelete1", "alert('" + message + "');", true);
            }
            else if (Convert.ToInt16(objParams[0]) == 2)
            {
                csm.RegisterClientScriptBlock(this.GetType(), "FlowDelete2", "alert('" + objParams[1].ToString() + "');", true);
            }
            return ret;
        }

        private void Pause()
        {
            Hashtable keyValues = GetKeyValues(false);
            string sKeys = "", sValues = "", svs = "";
            IEnumerator enumer = keyValues.GetEnumerator();
            while (enumer.MoveNext())
            {
                sKeys += ((DictionaryEntry)enumer.Current).Key.ToString() + ";";
                sValues += ((DictionaryEntry)enumer.Current).Key.ToString() + "=" + ((DictionaryEntry)enumer.Current).Value.ToString() + ";";
                svs += ((DictionaryEntry)enumer.Current).Value.ToString();
            }
            if (sKeys != "" && sValues != "")
            {
                sKeys = sKeys.Substring(0, sKeys.LastIndexOf(';'));
                sValues = sValues.Substring(0, sValues.LastIndexOf(';'));
            }

            //$edit20100604 by navy For support ajax,彈出訊息的方式支援ajax
            //ClientScriptManager csm = this.Page.ClientScript;
            string message = "";
            if ((string.IsNullOrEmpty(sKeys) && string.IsNullOrEmpty(sValues)) || this.CurrentNavState != "PreInsert")
            {
                message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", true);
                //$edit20100604 by navy
                //csm.RegisterStartupScript(this.GetType(), "seldata", "alert('" + message + "');", true);
                ScriptHelper.RegisterStartupScript(this, "alert('" + message + "');");
                return;
            }
            //if (!string.IsNullOrEmpty(sValues) && sValues.IndexOf("''") == -1)
            //{
            //    string provider = this.GetPorvider();
            //    string module = provider.Split('.')[0];
            //    string command = provider.Split('.')[1];

            //    string sql = CliUtils.GetSqlCommandText(module, command, CliUtils.fCurrentProject).ToUpper();
            //    string s = (sql.IndexOf(" WHERE ") == -1) ? " WHERE " + sValues : " AND " + sValues;
            //    if (sql.LastIndexOf("ORDER BY") != -1)
            //    {
            //        sql = sql.Insert(sql.LastIndexOf("ORDER BY"), s);
            //    }
            //    else
            //    {
            //        sql += s;
            //    }

            //    DataTable t = CliUtils.ExecuteSql(module, command, sql, true, CliUtils.fCurrentProject).Tables[0];
            //    if (t.Rows.Count == 0)
            //        return;
            //}
            object[] objParams = CliUtils.CallFLMethod("Pause", new object[] { null, new object[] { this.FLOWFILENAME + ".xoml", "", 0, 0, "", "", this.GetPorvider(), 0, "", "" }, new object[] { sKeys, sValues } });
            if (Convert.ToInt16(objParams[0]) == 0)
            {
                message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "PauseSucceed", true);
            }
            else
            {
                message = objParams[1].ToString();
            }
            //$edit20100604 by navy
            //if (message != "" && !csm.IsClientScriptBlockRegistered("Pause"))
            //    csm.RegisterClientScriptBlock(this.GetType(), "Pause", "alert('" + message + "');", true);
            if (message != "")
                ScriptHelper.RegisterStartupScript(this, "alert('" + message + "');");
            //catch (FLException e)
            //{
            //    if (e.Type == 2)
            //    {
            //        csm.RegisterStartupScript(this.GetType(), "hasPaused", "alert('" + e.Message + "');", true);
            //    }
            //}
        }

        private void ClearData()
        {
            object dataSource = this.GetDataSource();
            if (dataSource != null && dataSource is WebDataSource)
            {
                WebDataSource wds = dataSource as WebDataSource;
                wds.SetWhere("1=0");
                object bindingObject = GetBindingObject();
                if (bindingObject != null && bindingObject is CompositeDataBoundControl)
                {
                    ((CompositeDataBoundControl)bindingObject).DataBind();
                }
            }
        }

        protected override void RenderFlowItems(HtmlTextWriter writer)
        {
            string[] ctrlTexts = { "submit", "approve", "return", "reject", "notify", "flowdelete", "plus", "pause", "comment" };
            if (this.GetServerText)
            {
            }
            #region add default controls
            //0:submit,1:approve,2:return,3:reject,4:notify,5:flowdelete,6:plus,7:pause
            bool[] vcs = new bool[9] { true, true, true, true, true, true, true, true, true };
            // Add Submit Control
            ControlItem SubmitItem = new ControlItem("Submit", ctrlTexts[0], CtrlType.Image, "../image/uipics/submit.gif", "../image/uipics/submit2.gif", "", 25, vcs[0]);
            this.NavControls.Add(SubmitItem);
            // Add Approve Control
            ControlItem ApproveItem = new ControlItem("Approve", ctrlTexts[1], CtrlType.Image, "../image/uipics/Approve.gif", "../image/uipics/Approve2.gif", "", 25, vcs[1]);
            this.NavControls.Add(ApproveItem);
            // Add Return Control
            ControlItem ReturnItem = new ControlItem("Return", ctrlTexts[2], CtrlType.Image, "../image/uipics/Return.gif", "../image/uipics/Return2.gif", "", 25, vcs[2]);
            this.NavControls.Add(ReturnItem);
            // Add Reject Control
            ControlItem RejectItem = new ControlItem("Reject", ctrlTexts[3], CtrlType.Image, "../image/uipics/Reject.gif", "../image/uipics/Reject2.gif", "", 25, vcs[3]);
            this.NavControls.Add(RejectItem);
            // Add Notify Control
            ControlItem NotifyItem = new ControlItem("Notify", ctrlTexts[4], CtrlType.Image, "../image/uipics/Notify.gif", "../image/uipics/Notify2.gif", "", 25, vcs[4]);
            this.NavControls.Add(NotifyItem);
            // Add FlowDelete Control
            ControlItem FlowDeleteItem = new ControlItem("FlowDelete", ctrlTexts[5], CtrlType.Image, "../image/uipics/FlowDelete.gif", "../image/uipics/FlowDelete2.gif", "", 25, vcs[5]);
            this.NavControls.Add(FlowDeleteItem);
            // Add Plus Control
            ControlItem plusItem = new ControlItem("Plus", ctrlTexts[6], CtrlType.Image, "../image/uipics/Plus.gif", "../image/uipics/Plus2.gif", "", 25, vcs[6]);
            this.NavControls.Add(plusItem);
            // Add Pause Control
            ControlItem pauseItem = new ControlItem("Pause", ctrlTexts[7], CtrlType.Image, "../image/uipics/Pause.gif", "../image/uipics/Pause2.gif", "", 25, vcs[7]);
            this.NavControls.Add(pauseItem);
            // Add Comment Control
            ControlItem commentItem = new ControlItem("Comment", ctrlTexts[8], CtrlType.Image, "../image/uipics/Comment.gif", "../image/uipics/Comment2.gif", "", 25, vcs[8]);
            this.NavControls.Add(commentItem);

            #endregion
            InitializeFlowStates();
        }

        private bool IsControlVisible(ControlItem item)
        {
            string ctrlName = item.ControlName;
            return IsControlVisible(ctrlName) && item.ControlVisible;
        }

        private bool IsControlVisible(string ctrlName)
        { 
            bool visible = true;
            if (ctrlName == "Submit")
            {
                switch (this.CurrentFLState)
                {
                    case "Submit":
                    case "Return":
                    case "Continue":
                    case "FSubmit":
                    case "RSubmit":
                        visible = true;
                        break;
                    case "Approve":
                    case "Inquery":
                    case "Notify":
                    case "None":
                    case "Plus":
                    case "Lock":
                        visible = false;
                        break;
                }
            }
            else if (ctrlName == "Approve")
            {
                switch (this.CurrentFLState)
                {
                    case "Approve":
                    case "Plus":
                        visible = true;
                        break;
                    case "Submit":
                    case "Return":
                    case "Continue":
                    case "Inquery":
                    case "Notify":
                    case "None":
                    case "Lock":
                    case "FSubmit":
                    case "RSubmit":
                        visible = false;
                        break;

                }
            }
            else if (ctrlName == "Return")
            {
                switch (this.CurrentFLState)
                {
                    case "Approve":
                    case "Plus":
                        visible = true;
                        break;
                    case "RSubmit":
                    case "Submit":
                    case "Return":
                    case "Continue":
                    case "Inquery":
                    case "Notify":
                    case "None":

                    case "Lock":
                    case "FSubmit":
                        visible = false;
                        break;
                }
            }
            else if (ctrlName == "Reject")
            {
                switch (this.CurrentFLState)
                {
                    case "Return":
                    case "FSubmit":
                    case "RSubmit":
                        visible = true;
                        break;
                    case "Submit":
                    case "Approve":
                    case "Continue":
                    case "Inquery":
                    case "Notify":
                    case "None":
                    case "Plus":
                    case "Lock":
                        visible = false;
                        break;
                }
            }
            else if (ctrlName == "Notify")
            {
                switch (this.CurrentFLState)
                {
                    case "Submit":
                    case "Notify":
                    case "Approve":
                    case "Return":
                    case "Continue":
                    case "Plus":
                    case "Lock":
                    case "FSubmit":
                    case "RSubmit":
                        visible = true;
                        break;
                    case "Inquery":
                    case "None":
                        visible = false;
                        break;
                }
            }
            else if (ctrlName == "FlowDelete")
            {
                switch (this.CurrentFLState)
                {
                    case "Notify":
                        visible = true;
                        break;
                    case "Submit":
                    case "Approve":
                    case "Return":
                    case "Continue":
                    case "Inquery":
                    case "None":
                    case "Plus":
                    case "Lock":
                    case "FSubmit":
                    case "RSubmit":
                        visible = false;
                        break;
                }
            }
            else if (ctrlName == "Plus")
            {
                switch (this.CurrentFLState)
                {
                    case "Approve":
                        if (this.ViewState["PLUSAPPROVE"] != null && this.ViewState["PLUSAPPROVE"].ToString() == "1")
                            visible = true;
                        else
                            visible = false;
                        break;
                    case "Submit":
                    case "Return":
                    case "Notify":
                    case "Continue":
                    case "Inquery":
                    case "None":
                    case "Lock":
                    case "FSubmit":
                    case "RSubmit":
                        visible = false;
                        break;
                    case "Plus":
                        if (this.ViewState["STATUS"] != null && this.ViewState["STATUS"].ToString() == "AA")
                            visible = true;
                        else
                            visible = false;
                        break;
                }
            }
            else if (ctrlName == "Pause")
            {
                switch (this.CurrentFLState)
                {
                    case "Submit":
                        visible = true;
                        break;
                    case "Notify":
                    case "Approve":
                    case "Return":
                    case "Continue":
                    case "Inquery":
                    case "None":
                    case "Plus":
                    case "Lock":
                    case "FSubmit":
                    case "RSubmit":
                        visible = false;
                        break;
                }
            }
            else if (ctrlName == "Comment")
            {
                switch (this.CurrentFLState)
                {
                    case "Approve":
                    case "Notify":
                    case "Inquery":
                    case "Continue":
                    case "Plus":
                    case "None":
                    case "RSubmit":
                    case "FSubmit":
                        visible = true;
                        break;
                    case "Submit":
                    case "Return":
                    case "Lock":
                        visible = false;
                        break;
                }
            }
            return visible;
        }

        protected override bool getUserSetEnabled(string ctrlName, string navState)
        {
            if (GloFix.IsFlowItem(ctrlName))
            {
                if (navState == "Editing" || navState == "Inserting")
                {
                    return false;//修改或新增时不允许流程操作
                }
                else
                {
                    return true;
                }
            }
            foreach (WebNavigatorStateItem item in this.NavStates)
            {
                if (item.StateText == navState)
                {
                    if (string.IsNullOrEmpty(item.EnableControls))
                    {
                        return IsControlEnabled(ctrlName);
                    }
                    else
                    {
                        return (item.EnableControls.IndexOf(ctrlName) != -1);
                    }
                }
            }
            return false;
        }

        private bool IsControlEnabled(string ctrlName)
        {
            bool enable = true;
            #region set default
            if (ctrlName == "First" || ctrlName == "Previous" || ctrlName == "Next" || ctrlName == "Last" || ctrlName == "Export")
            {
                switch (this.CurrentNavState)
                {
                    case "Initial":
                    case "Browsed":
                    case "Changing":
                    case "InqueryMulti":
                    case "PrepareMulti":
                        enable = true; break;
                    case "Inserting":
                    case "Editing":
                    case "Applying":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                    case "Insert":
                    case "Modify":
                    case "Inquery":
                    case "Prepare":
                    case "PreInsert":
                    case "InquerySingle":
                    case "PrepareSingle":
                        enable = false; break;
                }
            }
            else if (ctrlName == "Delete")
            {
                switch (this.CurrentNavState)
                {
                    case "Initial":
                    case "Browsed":
                    case "Changing":
                    case "PreInsert":
                        enable = true; break;
                    case "Inserting":
                    case "Editing":
                    case "Applying":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                    case "Insert":
                    case "Modify":
                    case "Inquery":
                    case "Prepare":
                    case "InqueryMulti":
                    case "PrepareMulti":
                    case "InquerySingle":
                    case "PrepareSingle":
                        enable = false; break;
                }
            }
            else if (ctrlName == "Add")
            {
                switch (this.CurrentNavState)
                {
                    case "Initial":
                    case "Browsed":
                    case "Changing":
                    case "Insert":
                        enable = true;
                        break;
                    case "Inserting":
                    case "Editing":
                    case "Applying":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                    case "Modify":
                    case "Inquery":
                    case "Prepare":
                    case "PreInsert":
                    case "InqueryMulti":
                    case "PrepareMulti":
                    case "InquerySingle":
                    case "PrepareSingle":
                        enable = false; break;
                }
            }
            else if (ctrlName == "Update")
            {
                switch (this.CurrentNavState)
                {
                    case "Initial":
                    case "Browsed":
                    case "Changing":
                    case "Modify":
                    case "Prepare":
                    case "PreInsert":
                    case "PrepareMulti":
                    case "PrepareSingle":
                        enable = true; break;
                    case "Inserting":
                    case "Editing":
                    case "Applying":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                    case "Insert":
                    case "Inquery":
                    case "InqueryMulti":
                    case "InquerySingle":
                        enable = false; break;
                }
            }
            else if (ctrlName == "OK" || ctrlName == "Cancel")
            {
                switch (this.CurrentNavState)
                {
                    case "Inserting":
                    case "Editing":
                        enable = true; break;
                    case "Initial":
                    case "Browsed":
                    case "Applying":
                    case "Changing":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                    case "Insert":
                    case "Modify":
                    case "Inquery":
                    case "Prepare":
                    case "PreInsert":
                    case "InqueryMulti":
                    case "PrepareMulti":
                    case "InquerySingle":
                    case "PrepareSingle":
                        enable = false; break;
                }
            }
            else if (ctrlName == "Apply" || ctrlName == "Abort")
            {
                switch (this.CurrentNavState)
                {
                    case "Inserting":
                    case "Editing":
                    case "Changing":
                        enable = true; break;
                    case "Initial":
                    case "Browsed":
                    case "Applying":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                    case "Insert":
                    case "Modify":
                    case "Inquery":
                    case "Prepare":
                    case "PreInsert":
                    case "InqueryMulti":
                    case "PrepareMulti":
                    case "InquerySingle":
                    case "PrepareSingle":
                        enable = false; break;
                }
            }
            else if (ctrlName == "Query")
            {
                switch (this.CurrentNavState)
                {
                    case "Initial":
                    case "Browsed":
                    case "Inquery":
                    case "Prepare":
                    case "InqueryMulti":
                    case "PrepareMulti":
                    case "InquerySingle":
                    case "PrepareSingle":
                        enable = true; break;
                    case "Inserting":
                    case "Editing":
                    case "Changing":
                    case "Applying":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                    case "Insert":
                    case "Modify":
                    case "PreInsert":
                        enable = false; break;
                }
            }
            else if (ctrlName == "Print" || ctrlName == "Export")
            {
                switch (this.CurrentNavState)
                {
                    case "Initial":
                    case "Browsed":
                    case "PreInsert":
                    case "Modify":
                    case "Prepare":
                    case "InqueryMulti":
                    case "PrepareMulti":
                    case "InquerySingle":
                    case "PrepareSingle":
                        enable = true; break;
                    case "Inserting":
                    case "Editing":
                    case "Changing":
                    case "Applying":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                    case "Insert":
                    case "Inquery":
                        enable = false; break;
                }
            }
            #endregion
            return enable;
        }

        protected override void createButton(HtmlTextWriter writer, ControlItem ctrl, WebGridView gdView)
        {
            if (!this.FLActive && GloFix.IsFlowItem(ctrl.ControlName))
                return;
            string command = "cmd" + ctrl.ControlName, text = ctrl.ControlText, imageUrl = ctrl.ImageUrl, mouseOverImageUrl = ctrl.MouseOverImageUrl, disenableImageUrl = ctrl.DisenableImageUrl;
            int size = ctrl.Size;
            bool IsVisible = IsControlVisible(ctrl);
            if (ctrl.ControlName == "Notify" && this.FLNotifyOff)
                IsVisible = false;
            bool IsEnable = this.DesignMode ? true : getUserSetEnabled(ctrl.ControlName, this.CurrentNavState);
            CtrlType ct = ctrl.ControlType;
            ClientScriptManager csm = Page.ClientScript;

            string tooltiptext = "";
            if (!this.DesignMode)
            {
                tooltiptext = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "FLWebNavigator", "ControlText", true);
                string[] arrtext = tooltiptext.Split(';');
                switch (ctrl.ControlName)
                {
                    case "First":
                        text = arrtext[0];
                        tooltiptext = arrtext[0];
                        break;
                    case "Previous":
                        text = arrtext[1];
                        tooltiptext = arrtext[1]; 
                        break;
                    case "Next":
                        text = arrtext[2];
                        tooltiptext = arrtext[2]; 
                        break;
                    case "Last":
                        text = arrtext[3];
                        tooltiptext = arrtext[3]; 
                        break;
                    case "Add":
                        text = arrtext[4];
                        tooltiptext = arrtext[4]; 
                        break;
                    case "Update":
                        text = arrtext[5];
                        tooltiptext = arrtext[5]; 
                        break;
                    case "Delete":
                        text = arrtext[6];
                        tooltiptext = arrtext[6]; 
                        break;
                    case "OK":
                        text = arrtext[7];
                        tooltiptext = arrtext[7]; 
                        break;
                    case "Cancel":
                        text = arrtext[8];
                        tooltiptext = arrtext[8]; 
                        break;
                    case "Apply":
                        text = arrtext[9];
                        tooltiptext = arrtext[9]; 
                        break;
                    case "Abort":
                        text = arrtext[10];
                        tooltiptext = arrtext[10]; 
                        break;
                    case "Query":
                        text = arrtext[11];
                        tooltiptext = arrtext[11]; 
                        break;
                    case "Print":
                        text = arrtext[12];
                        tooltiptext = arrtext[12]; 
                        break;
                    case "Export":
                        text = arrtext[13];
                        tooltiptext = arrtext[13]; 
                        break;
                    case "Submit":
                        text = arrtext[14];
                        tooltiptext = arrtext[14]; 
                        break;
                    case "Approve":
                        text = arrtext[15];
                        tooltiptext = arrtext[15]; 
                        break;
                    case "Return":
                        text = arrtext[16];
                        tooltiptext = arrtext[16]; 
                        break;
                    case "Reject":
                        text = arrtext[17];
                        tooltiptext = arrtext[17]; 
                        break;
                    case "Notify":
                        text = arrtext[18];
                        tooltiptext = arrtext[18]; 
                        break;
                    case "FlowDelete":
                        text = arrtext[19];
                        tooltiptext = arrtext[19]; 
                        break;
                    case "Plus":
                        text = arrtext[20];
                        tooltiptext = arrtext[20]; 
                        break;
                    case "Pause":
                        text = arrtext[21];
                        tooltiptext = arrtext[21]; 
                        break;
                    case "Comment":
                        text = arrtext[22];
                        tooltiptext = arrtext[22];
                        break;
                    default: 
                        tooltiptext = ctrl.ControlName; 
                        break;
                }
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            #region Button
            if (ct == CtrlType.Button && IsVisible)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + command);
                if (!isFlowConditionReqired(ctrl.ControlName))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "alert('" + SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowNotDefine") + "')");
                }
                else if (!hasApplyOrAbort(ctrl.ControlName))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "alert('" + SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "ApplyFirst") + "')");
                }
                else
                {
                    if (ctrl.ControlName == "Add")
                    {
                        if (gdView != null && gdView.EditURL != null && gdView.EditURL != "" && !gdView.OpenEditUrlInServerMode)
                        {
                            string url = gdView.getURL(WebGridView.OpenEditMode.Insert, null);
                            if (url != "")
                                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "window.open('" + url + "','','height=" + gdView.OpenEditHeight + ",width=" + gdView.OpenEditWidth + ",toolbar=no,scrollbars,resizable');return false;");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "var button = document.getElementById('" + this.ClientID + command + "');button.disabled=true;" + csm.GetPostBackEventReference(this, command));
                        }
                    }
                    else if (ctrl.ControlName == "Delete")
                    {
                        if (this.SureDelete)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "if(confirm('sure to delete?')){" + "var button = document.getElementById('" + this.ClientID + command + "');button.disabled=true;" + csm.GetPostBackEventReference(this, command) + "}");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "var button = document.getElementById('" + this.ClientID + command + "');button.disabled=true;" + csm.GetPostBackEventReference(this, command));
                        }
                    }
                    else if (ctrl.ControlName == "Submit" || ctrl.ControlName == "Approve" || ctrl.ControlName == "Return" || ctrl.ControlName == "Notify" || ctrl.ControlName == "Plus" || ctrl.ControlName == "Comment" || (ctrl.ControlName == "Pause" && this.OrganizationControl))
                    {
                        string script = GloScript(ctrl.ControlName);
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, script);
                    }
                    else if (ctrl.ControlName == "Apply" || ctrl.ControlName == "Ok" || ctrl.ControlName == "Update")
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "var button = document.getElementById('" + this.ClientID + command + "');button.disabled=true;" + csm.GetPostBackEventReference(this, command));
                        //writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "var start=new Date().getTime(); while(true) if(new Date().getTime()-start>5000) break; var aaa = document.getElementById('cmdApply');aaa.disabled=true;");
                        //writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackEventReference(this, command));
                    }
                    else
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "var button = document.getElementById('" + this.ClientID + command + "');button.disabled=true;" + csm.GetPostBackEventReference(this, command));
                    }
                }
                // render Button tag
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + size + "px; height:" + ((this.Height.Value <= 20) ? 25 : this.Height.Value) + "px; color:" + this.ForeColor.Name.Replace("ff", "#") + ";background-color:" + this.BackColor.Name.Replace("ff", "#") + ";");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, tooltiptext);// new add by ccm
                if (!IsEnable)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Button);
                writer.Write(text);
                writer.RenderEndTag();
            }
            #endregion
            #region HyperLink
            else if (ct == CtrlType.HyperLink && IsVisible)
            {
                if (!isFlowConditionReqired(ctrl.ControlName))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:alert('" + SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowNotDefine") + "')");
                }
                else if (!hasApplyOrAbort(ctrl.ControlName))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:alert('" + SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "ApplyFirst") + "')");
                }
                else
                {
                    if (ctrl.ControlName == "Add")
                    {
                        if (gdView != null && gdView.EditURL != null && gdView.EditURL != "" && !gdView.OpenEditUrlInServerMode)
                        {
                            string url = gdView.getURL(WebGridView.OpenEditMode.Insert, null);
                            if (url != "")
                                writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:window.open('" + url + "','','height=" + gdView.OpenEditHeight + ",width=" + gdView.OpenEditWidth + ",toolbar=no,scrollbars,resizable');return false;");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:" + csm.GetPostBackClientHyperlink(this, command));
                        }
                    }
                    else if (ctrl.ControlName == "Delete")
                    {
                        if (this.SureDelete)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:if(confirm('sure to delete?')){" + csm.GetPostBackClientHyperlink(this, command) + "}");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:" + csm.GetPostBackClientHyperlink(this, command));
                        }
                    }
                    else if (ctrl.ControlName == "Submit" || ctrl.ControlName == "Approve" || ctrl.ControlName == "Return" || ctrl.ControlName == "Notify" || ctrl.ControlName == "Plus" || ctrl.ControlName == "Comment" || (ctrl.ControlName == "Pause" && this.OrganizationControl))
                    {
                        string script = GloScript(ctrl.ControlName);
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:" + script + "}");
                    }
                    else
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:" + csm.GetPostBackEventReference(this, command));
                    }
                }
                // render Link tag
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + size + "px; color:" + this.ForeColor.Name.Replace("ff", "#") + ";background-color:" + this.BackColor.Name.Replace("ff", "#") + ";");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, tooltiptext);// new add by ccm
                if (!IsEnable)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(text);
                writer.RenderEndTag();
            }
            #endregion
            #region Image
            else if (ct == CtrlType.Image && imageUrl != null && imageUrl != "" && IsVisible)
            {
                if (!isFlowConditionReqired(ctrl.ControlName))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "alert('" + SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowNotDefine") + "')");
                }
                else if (!hasApplyOrAbort(ctrl.ControlName))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "alert('" + SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "ApplyFirst") + "')");
                }
                else
                {
                    if (ctrl.ControlName == "Add")
                    {
                        if (gdView != null && gdView.EditURL != null && gdView.EditURL != "" && !gdView.OpenEditUrlInServerMode)
                        {
                            string url = gdView.getURL(WebGridView.OpenEditMode.Insert, null);
                            if (url != "")
                                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "window.open('" + url + "','','height=" + gdView.OpenEditHeight + ",width=" + gdView.OpenEditWidth + ",toolbar=no,scrollbars,resizable');return false;");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackClientHyperlink(this, command));
                        }
                    }
                    else if (ctrl.ControlName == "Delete")
                    {
                        if (this.SureDelete)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "if(confirm('sure to delete?')){" + csm.GetPostBackEventReference(this, command) + "}");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackEventReference(this, command));
                        }
                    }
                    else if (ctrl.ControlName == "Submit" || ctrl.ControlName == "Approve" || ctrl.ControlName == "Return" || ctrl.ControlName == "Notify" || ctrl.ControlName == "Plus" || ctrl.ControlName == "Comment" || (ctrl.ControlName == "Pause" && this.OrganizationControl))
                    {
                        string script = GloScript(ctrl.ControlName);

                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, script);
                    }
                    else if (ctrl.ControlName == "Reject")
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "if(confirm('" + SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowRejectConfirm")
+ "')){" + csm.GetPostBackEventReference(this, command) + "}");
                    }
                    else
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackEventReference(this, command));
                    }
                }
                // render Image tag
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + size + "px;");
                if (!IsEnable)
                {
                    if (disenableImageUrl != null && disenableImageUrl != "")
                        writer.AddAttribute(HtmlTextWriterAttribute.Src, disenableImageUrl);
                    else
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Src, imageUrl);
                        writer.AddAttribute("onmouseover", "this.src='" + mouseOverImageUrl + "'");
                        writer.AddAttribute("onmouseout", "this.src='" + imageUrl + "'");
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, imageUrl);
                    writer.AddAttribute("onmouseover", "this.src='" + mouseOverImageUrl + "'");
                    writer.AddAttribute("onmouseout", "this.src='" + imageUrl + "'");
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Alt, tooltiptext);// new add by ccm
                writer.AddAttribute(HtmlTextWriterAttribute.Title, tooltiptext);
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }
            #endregion
            writer.RenderEndTag(); // </td>
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width:" + this.ControlsGap + "px;");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.RenderEndTag();
        }

        private string GloScript(string controlName)
        {
            if (this.DesignMode)
                return "";
            Hashtable keyValues = GetKeyValues(true);
            string sKeys = "", sValues = "", svs = "";
            IEnumerator enumer = keyValues.GetEnumerator();
            while (enumer.MoveNext())
            {
                sKeys += ((DictionaryEntry)enumer.Current).Key.ToString() + ";";
                sValues += ((DictionaryEntry)enumer.Current).Key.ToString() + "=" + ((DictionaryEntry)enumer.Current).Value.ToString() + ";";
                svs += ((DictionaryEntry)enumer.Current).Value.ToString();
            }
            if (sKeys != "" && sValues != "")
            {
                sKeys = sKeys.Substring(0, sKeys.LastIndexOf(';'));
                sValues = sValues.Substring(0, sValues.LastIndexOf(';'));
            }
            string isListNullMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "IsListIdNull", true);
            string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", true);
            bool isNull = true;
            foreach (char c in svs.ToCharArray())
            {
                if (c != '$')
                {
                    isNull = false;
                    break;
                }
            }
            if (isNull)
            {
                return "alert('" + selDataMessage + "');";
            }
            string provider = this.GetPorvider();
            string script = "";
            //user define parameter
            List<string> reserveredWords = new List<string>( new string[] { 
                "FLOWFILENAME", "LISTID", "FLOWPATH", "WHERESTRING", "ISIMPORTANT", "ISURGENT", "STATUS", "PLUSAPPROVE"
                ,"MULTISTEPRETURN", "ATTACHMENTS", "SENDTOID", "SENDTOID", "FLNAVMODE", "VDSNAME"
                                                });
            System.Text.StringBuilder userParameter = new System.Text.StringBuilder("&UserParam=");
            foreach (string key in this.Page.Request.QueryString.AllKeys)
            {
                if (!reserveredWords.Contains(key))
                {
                    if (userParameter.Length > 0)
                    {
                        userParameter.Append(";");
                    }
                    userParameter.Append(HttpUtility.UrlEncode(key));
                    userParameter.Append("^");
                    userParameter.Append(HttpUtility.UrlEncode(Page.Request.QueryString[key]));
                }
            }

            switch (controlName)
            {
                case "Submit":
                    #region Submit
                    if (keyValues.Count == 0)
                    {
                        script = "alert('" + selDataMessage + "')";
                    }
                    else
                    {
                        string param = "";
                        if (this.CurrentFLState == "Continue" || this.STATUS == "NF" || this.STATUS == "NR")
                        {
                            param = "LISTID=" + this.LISTID + "&OPERATETYPE=Approve&KEYS=" + HttpUtility.UrlEncode(sKeys) + "&VALUES=" + HttpUtility.UrlEncode(sValues) + "&FLOWPATH=" + HttpUtility.UrlEncode(this.FLOWPATH) + "&PROVIDER=" + HttpUtility.UrlEncode(provider) + "&ISIMPORTANT=" + this.FLOW_IMPORTANT + "&ISURGENT=" + this.FLOW_URGENT + "&STATUS=" + this.STATUS + "&ATTACHMENTS=" + HttpUtility.UrlEncode(this.ATTACHMENTS) + "&PAGEPATH=" + this.Page.Request.FilePath + "&VDSNAME=" + this.ViewState["VDSNAME"].ToString() + "&RemarkField=" + this.RemarkField;
                            param += userParameter.ToString();
                            script = "window.open(\"../InnerPages/FlowSubmitConfirm.aspx?" + param + "\", '', 'resizable=yes,scrollbars=yes,width=500,height=410,top=200,left=200');";
                        }
                        else
                        {
                            //if (this.CurrentNavState == "PreInsert" || this.CurrentNavState == "Prepare")
                            if (ContainsKeyValues())
                            {
                                param = "FLOWFILENAME=" + HttpUtility.UrlEncode(this.FLOWFILENAME) + "&OPERATETYPE=Submit&KEYS=" + HttpUtility.UrlEncode(sKeys) + "&Values=" + HttpUtility.UrlEncode(sValues) + "&PROVIDER=" + HttpUtility.UrlEncode(provider) + "&PAGEPATH=" + this.Page.Request.FilePath + "&NAVIGATOR_MODE=" + this.NAVIGATOR_MODE + "&FLNAVIGATOR_MODE=" + this.FLNAVIGATOR_MODE + "&PLUSAPPROVE=" + this.PLUSAPPROVE + "&ORGCONTROL=" + this.OrganizationControl.ToString() + "&VDSNAME=" + this.ViewState["VDSNAME"].ToString() + "&RemarkField=" + this.RemarkField;
                                param += userParameter.ToString();
                                script = "window.open(\"../InnerPages/FlowSubmitConfirm.aspx?" + param + "\", '', 'resizable=yes,scrollbars=yes,width=500,height=410,top=200,left=200');";
                            }
                            else
                            {
                                script = "alert('" + selDataMessage + "');";
                            }
                        }
                    }
                    break;
                    #endregion
                case "Approve":
                case "Return":
                    #region Approve, Return
                    if (this.LISTID == "")
                    {
                        script = "alert('" + isListNullMessage + "');";
                    }
                    else if (keyValues.Count == 0)
                    {
                        script = "alert('" + selDataMessage + "');";
                    }
                    else
                    {
                        //邮件打开加签返回PLUSAPPROVE为1
                        //if (this.PLUSAPPROVE == "1")
                        //{
                        //    this.ViewState["STATUS"] = "A";
                        //}
                        string param = "LISTID=" + this.LISTID + "&OPERATETYPE=" + controlName + "&KEYS=" + HttpUtility.UrlEncode(sKeys) + "&VALUES=" + HttpUtility.UrlEncode(sValues) + "&FLOWPATH=" + HttpUtility.UrlEncode(this.FLOWPATH) + "&PROVIDER=" + HttpUtility.UrlEncode(provider) + "&ISIMPORTANT=" + this.FLOW_IMPORTANT + "&ISURGENT=" + this.FLOW_URGENT + "&STATUS=" + this.STATUS + "&MULTISTEPRETURN=" + this.MULTISTEPRETURN + "&ATTACHMENTS=" + HttpUtility.UrlEncode(this.ATTACHMENTS) + "&SENDTOID=" + HttpUtility.UrlEncode(this.SENDTOID) + "&PAGEPATH=" + this.Page.Request.FilePath + "&VDSNAME=" + this.ViewState["VDSNAME"].ToString() + "&RemarkField=" + this.RemarkField;
                        param += userParameter.ToString();
                        script = "window.open(\"../InnerPages/FlowSubmitConfirm.aspx?" + param + "\", '', 'resizable=yes,scrollbars=yes,width=500,height=410,top=200,left=200');";
                    }
                    break;
                    #endregion
                case "Notify":
                    #region Notify
                    if (this.LISTID == "")
                    {
                        script = "alert('" + isListNullMessage + "');";
                    }
                    else if (keyValues.Count == 0)
                    {
                        script = "alert('" + selDataMessage + "');";
                    }
                    else
                    {
                        string param = "LISTID=" + this.LISTID + "&KEYS=" + HttpUtility.UrlEncode(sKeys) + "&VALUES=" + HttpUtility.UrlEncode(sValues) + "&FLOWPATH=" + HttpUtility.UrlEncode(this.FLOWPATH.Split(';')[1]) + "&PROVIDER=" + HttpUtility.UrlEncode(provider);
                        #region WebSecurity
                        if (this.FLNotifySecControl)
                        {
                            string secGroups = "", secUsers = "";
                            string menuid = this.MenuId;
                            string sqlGM = string.Format(GloFix.secGroups, menuid);
                            DataTable tabGM = null;

                            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlGM });
                            if (ret1 != null && (int)ret1[0] == 0)
                            {
                                tabGM = ((DataSet)ret1[1]).Tables[0];
                            }
                            foreach (DataRow row in tabGM.Rows)
                            {
                                secGroups += row["GROUPID"].ToString() + ";";
                            }
                            if (secGroups != "")
                            {
                                secGroups = secGroups.Substring(0, secGroups.LastIndexOf(';'));
                                param += "&SecGroups=" + HttpUtility.UrlEncode(secGroups);
                            }
                            string sqlUM = string.Format(GloFix.secUsers, menuid);
                            DataTable tabUM = null;

                            object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlUM });
                            if (ret2 != null && (int)ret2[0] == 0)
                            {
                                tabUM = ((DataSet)ret2[1]).Tables[0];
                            }
                            foreach (DataRow row in tabUM.Rows)
                            {
                                secUsers += row["USERID"].ToString() + ";";
                            }
                            if (secUsers != "")
                            {
                                secUsers = secUsers.Substring(0, secUsers.LastIndexOf(';'));
                                param += "&SecUsers=" + HttpUtility.UrlEncode(secUsers);
                            }
                        }
                        #endregion
                        script = "window.open('../InnerPages/FlowNotify.aspx?" + param + "', '', 'resizable=yes,scrollbars=yes,width=500,height=530,top=100,left=200');";
                    }
                    break;
                    #endregion
                case "Plus":
                    #region Plus
                    if (this.LISTID == "")
                    {
                        script = "alert('" + isListNullMessage + "');";
                    }
                    else if (keyValues.Count == 0)
                    {
                        script = "alert('" + selDataMessage + "');";
                    }
                    else
                    {
                        string param = "LISTID=" + this.LISTID + "&KEYS=" + HttpUtility.UrlEncode(sKeys) + "&VALUES=" + HttpUtility.UrlEncode(sValues) + "&FLOWPATH=" + HttpUtility.UrlEncode(this.FLOWPATH.Split(';')[1]) + "&PROVIDER=" + HttpUtility.UrlEncode(provider) + "&SENDTOID=" + HttpUtility.UrlEncode(this.SENDTOID) + "&ATTACHMENTS=" + HttpUtility.UrlEncode(this.ATTACHMENTS) + "&ISIMPORTANT=" + this.FLOW_IMPORTANT + "&ISURGENT=" + this.FLOW_URGENT + "&PAGEPATH=" + this.Page.Request.FilePath + "&VDSNAME=" + this.ViewState["VDSNAME"].ToString();
                        script = "window.open('../InnerPages/FlowPlus.aspx?" + param + "', '', 'resizable=yes,scrollbars=yes,width=500,height=480,top=100,left=200');";
                    }
                    break;
                    #endregion
                case "Pause":
                    #region Pause
                    string p = "FLOWFILENAME=" + HttpUtility.UrlEncode(this.FLOWFILENAME) + "&PROVIDER=" + HttpUtility.UrlEncode(provider) + "&KEYS=" + HttpUtility.UrlEncode(sKeys) + "&VALUES=" + HttpUtility.UrlEncode(sValues);
                    script = "window.open('../InnerPages/FlowPause.aspx?" + p + "', '', 'resizable=yes,scrollbars=yes,width=500,height=400,top=100,left=200');";
                    break;
                    #endregion
                case "Comment":
                    #region Comment
                    if (this.LISTID == "")
                    {
                        script = "alert('" + isListNullMessage + "');";
                    }
                    script = "window.open('../InnerPages/FlowComment.aspx?LISTID=" + this.LISTID + "&ATTACHMENTS=" + HttpUtility.UrlEncode(this.ATTACHMENTS) + "&VDSNAME=" + this.ViewState["VDSNAME"].ToString() + "', '', 'resizable=yes,scrollbars=yes,width=645,height=400,top=100,left=200');";
                    break;
                    #endregion
                case "Reject":
                    #region Reject
                    // reject append in createButton method...
                    break;
                    #endregion
            }
            return script;
        }

        public void Submit()
        {
            string script = this.GloScript("Submit");
            if (!IsControlVisible("Submit"))
            {
                script = "if(confirm('Submit operation is not suggested to do it now! Are you sure to do it constrainedly?')){" + script + "}";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "flow_submit_script", script, true);
        }

        public void Approve()
        {
            string script = this.GloScript("Approve");
            if (!IsControlVisible("Approve"))
            {
                script = "if(confirm('Approve operation is not suggested to do it now! Are you sure to do it constrainedly?')){" + script + "}";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "flow_approve_script", script, true);
        }

        public void Return()
        {
            string script = this.GloScript("Return");
            if (!IsControlVisible("Return"))
            {
                script = "if(confirm('Return operation is not suggested to do it now! Are you sure to do it constrainedly?')){" + script + "}";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "flow_return_script", script, true);
        }

        public void Plus()
        {
            string script = this.GloScript("Plus");
            if (!IsControlVisible("Plus"))
            {
                script = "if(confirm('Plus operation is not suggested to do it now! Are you sure to do it constrainedly?')){" + script + "}";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "flow_plus_script", script, true);
        }

        public void Notify()
        {
            string script = this.GloScript("Notify");
            if (!IsControlVisible("Notify"))
            {
                script = "if(confirm('Notify operation is not suggested to do it now! Are you sure to do it constrainedly?')){" + script + "}";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "flow_notify_script", script, true);
        }

        public void Comment()
        {
            string script = this.GloScript("Comment");
            if (!IsControlVisible("Comment"))
            {
                script = "if(confirm('Comment operation is not suggested to do it now! Are you sure to do it constrainedly?')){" + script + "}";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "flow_comment_script", script, true);
        }

        private bool isFlowConditionReqired(string itemName)
        {
            if (GloFix.IsFlowItem(itemName) && string.IsNullOrEmpty(this.LISTID) && string.IsNullOrEmpty(this.FLOWFILENAME))
            {
                return false;
            }
            return true;
        }

        private bool hasApplyOrAbort(string itemName)
        {
            object obj = this.GetDataSource();
            if (GloFix.IsFlowItem(itemName) && obj != null && obj is WebDataSource)
            {
                WebDataSource ds = obj as WebDataSource;
                if (ds.InnerDataSet.HasChanges())
                {
                    return false;
                }
            }
            return true;
        }

        internal Hashtable GetKeyValues(bool cs)
        {
            Hashtable keyVals = new Hashtable();
            object objData = this.GetDataItem();
            if (objData == null)
            {
                return keyVals;
            }
            object dataSource = this.GetDataSource();
            if (dataSource != null && dataSource is WebDataSource)
            {
                WebDataSource wds = dataSource as WebDataSource;
                DataRow row = (DataRow)objData;
                DataSet host = wds.InnerDataSet.Clone();
                host.Tables[0].Rows.Add(row.ItemArray);
                Page.Session["PreviewHost"] = host;
                foreach (DataColumn column in wds.PrimaryKey)
                {
                    string key = column.ColumnName;
                    //string key = keyField.ColumnName;
                    if (GloFix.IsNumeric(row[key].GetType()))
                    {
                        keyVals.Add(key, row[key].ToString());
                    }
                    else
                    {
                        if (row[key] != null && row[key].ToString() != "")
                        {
                            if (cs)
                                keyVals.Add(key, "$$$" + row[key].ToString() + "$$$");
                            else
                                keyVals.Add(key, "''" + row[key].ToString() + "''");
                        }
                    }
                }
            }
            return keyVals;
        }

        private bool ContainsKeyValues()
        {
            object objData = this.GetDataItem();
            if (objData == null) return false;
            object dataSource = this.GetDataSource();
            if (dataSource != null && dataSource is WebDataSource)
            {
                string provider = this.GetPorvider();
                string module = provider.Split('.')[0];
                string cmd = provider.Split('.')[1];
                string sql = CliUtils.GetSqlCommandText(module, cmd, CliUtils.fCurrentProject);

                WebDataSource wds = dataSource as WebDataSource;
                DataRow row = (DataRow)objData;
                foreach (DataColumn column in wds.PrimaryKey)
                {
                    string key = column.ColumnName;
                    if (GloFix.IsNumeric(row[key].GetType()))
                    {
                        sql = CliUtils.InsertWhere(sql, string.Format("{0}={1}", CliUtils.GetTableNameForColumn(sql, key), row[key].ToString()));
                    }
                    else
                    {
                        sql = CliUtils.InsertWhere(sql, string.Format("{0}='{1}'", CliUtils.GetTableNameForColumn(sql, key), row[key].ToString()));
                    }
                }

                DataTable tab = CliUtils.ExecuteSql("GLModule", "cmdRefValUse", sql, true, CliUtils.fCurrentProject).Tables[0];
                if (tab.Rows.Count > 0) return true;
            }
            return false;
        }

        private string GetPorvider()
        {
            string remoteName = "";
            object dataSource = this.GetDataSource();
            if (dataSource != null && dataSource is WebDataSource)
            {
                WebDataSource wds = (WebDataSource)dataSource;

                XmlDocument xmlDoc = new XmlDocument();
                string aspxName = this.Page.MapPath(this.Page.Request.Path);
                string resourceName = aspxName + @".vi-VN.resx";
                ResXResourceReader reader = new ResXResourceReader(resourceName);
                IDictionaryEnumerator enumerator = reader.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Key.ToString() == "WebDataSets")
                    {
                        string sXml = (string)enumerator.Value;
                        xmlDoc.LoadXml(sXml);
                        break;
                    }
                }
                if (xmlDoc != null)
                {
                    XmlNode nWDSs = xmlDoc.SelectSingleNode("WebDataSets");
                    if (nWDSs != null)
                    {
                        string webDataSetID = wds.WebDataSetID;
                        XmlNode nWDS = nWDSs.SelectSingleNode("WebDataSet[@Name='" + webDataSetID + "']");
                        if (nWDS != null)
                        {
                            XmlNode nRemoteName = nWDS.SelectSingleNode("RemoteName");
                            if (nRemoteName != null)
                                remoteName = nRemoteName.InnerText;
                        }
                    }
                }
            }
            return remoteName;
        }

        public object GetDataItem()
        {
            object bindingObject = GetBindingObject();
            if (bindingObject != null)
            {
                int index = -1;
                object objDs = this.GetDataSource();
                if (bindingObject is GridView)
                {
                    GridView grid = (GridView)bindingObject;
                    if (grid.SelectedRow != null)
                    {
                        index = grid.SelectedRow.DataItemIndex;
                    }
                }
                else if (bindingObject is DetailsView)
                {
                    DetailsView detls = (DetailsView)bindingObject;
                    index = detls.DataItemIndex;
                }
                else if (bindingObject is FormView)
                {
                    FormView form = (FormView)bindingObject;
                    index = form.DataItemIndex;
                }
                if (objDs != null && objDs is WebDataSource && index != -1)
                {
                    WebDataSource wds = (WebDataSource)objDs;
                    if (wds.InnerDataSet.Tables[0].Rows.Count == 0)
                        return null;
                    return wds.InnerDataSet.Tables[0].Rows[index];
                }
            }
            return null;
        }

        public object GetBindingObject()
        {
            return this.GetObjByID(this.BindingObject);
        }

        public object GetDataSource()
        {
            object bindingObject = GetBindingObject();
            if (bindingObject != null && bindingObject is CompositeDataBoundControl)
            {
                return this.GetObjByID(((CompositeDataBoundControl)bindingObject).DataSourceID);
            }
            return null;
        }

        public void InitializeFlowStates()
        {
            #region FLNavigatorStates
            foreach (FLWebNavigatorStateItem stateItem in this.FLStates)
            {
                stateItem.VisibleControls = "";
                switch (stateItem.StateText)
                {
                    case "Submit":
                        stateItem.VisibleControls = "Submit;Notify;Pause";
                        break;
                    case "Approve":
                        if (this.ViewState["PLUSAPPROVE"] != null && this.ViewState["PLUSAPPROVE"].ToString() == "1")
                            stateItem.VisibleControls = "Approve;Return;Notify;Plus;Comment";
                        else
                            stateItem.VisibleControls = "Approve;Return;Notify;Comment";
                        break;
                    case "Return":
                        stateItem.VisibleControls = "Submit;Reject;Notify";
                        break;
                    case "Continue":
                        stateItem.VisibleControls = "Submit;Notify;Comment";
                        break;
                    case "Inquery":
                        stateItem.VisibleControls = "Comment";
                        break;
                    case "None":
                        break;
                    case "Notify":
                        stateItem.VisibleControls = "Notify;FlowDelete;Comment";
                        break;
                    case "Plus":
                        stateItem.VisibleControls = "Approve;Notify;Comment";
                        break;
                    case "Lock":
                        stateItem.VisibleControls = "Notify";
                        break;
                    case "FSubmit":
                        stateItem.VisibleControls = "Submit;Notify";
                        break;
                    case "RSubmit":
                        stateItem.VisibleControls = "Submit;Return;Notify;Comment";
                        break;
                }
            }
            #endregion
        }

        protected override void InitializeStates()
        {
            #region NavigatorStates
            foreach (WebNavigatorStateItem stateItem in this.NavStates)
            {
                stateItem.EnableControls = "";
                switch (stateItem.StateText)
                {
                    case "Initial":
                    case "Browsed":
                        stateItem.EnableControls = "First;Previous;Next;Last;Add;Update;Delete;Query;Print;Export";
                        break;
                    case "Inserting":
                    case "Editing":
                        stateItem.EnableControls = "OK;Cancel;Apply;Abort";
                        break;
                    case "Changing":
                        stateItem.EnableControls = "First;Previous;Next;Last;Add;Update;Delete;Apply;Abort;Export";
                        break;
                    case "Applying":
                    case "Querying":
                    case "Printing":
                    case "Normal":
                        break;
                    case "Insert":
                        stateItem.EnableControls = "Add";
                        break;
                    case "Modify":
                        stateItem.EnableControls = "Update;Print;Export";
                        break;
                    case "Inquery":
                        stateItem.EnableControls = "Query";
                        break;
                    case "Prepare":
                        stateItem.EnableControls = "Query;Update;Print;Export";
                        break;
                    case "PreInsert":
                        stateItem.EnableControls = "Update;Delete;Print;Export";
                        break;
                    case "InquerySingle":
                        stateItem.EnableControls = "Query;Print;Export";
                        break;
                    case "InqueryMulti":
                        stateItem.EnableControls = "First;Previous;Next;Last;Query;Print;Export";
                        break;
                    case "PrepareSingle":
                        stateItem.EnableControls = "Update;Query;Print;Export";
                        break;
                    case "PrepareMulti":
                        stateItem.EnableControls = "Update;Previous;Next;Last;Edit;Query;Print;Export";
                        break;
                }
            }
            #endregion
        }

        public override void SetNavState(string StateText)
        {
            base.SetNavState(StateText);
            if (StateText == "Browsed" && this.FLActive)
            {
                string mode = "";
                switch (this.NAVIGATOR_MODE)
                {
                    case "0":
                        mode = "Normal";
                        break;
                    case "1":
                        mode = "Insert";
                        break;
                    case "2":
                        mode = "Modify";
                        break;
                    case "3":
                        mode = "Inquery";
                        break;
                    case "4":
                        mode = "Prepare";
                        break;
                    case "5":
                        mode = "PreInsert";
                        break;
                    case "6":
                        mode = "InquerySingle";
                        break;
                    case "7":
                        mode = "InqueryMulti";
                        break;
                    case "8":
                        mode = "PrepareSingle";
                        break;
                    case "9":
                        mode = "PrepareMulti";
                        break;
                    default:
                        mode = this.NAVIGATOR_MODE;
                        break;
                }
                if (mode != "")
                {
                    InnerSetNavState(mode);
                    this.ViewState["CurrentNavState"] = mode;
                }
            }
            //object bind_obj = this.GetBindingObject();
            //if (bind_obj != null && bind_obj is WebGridView)
            //{
            //    WebGridView grid = bind_obj as WebGridView;
            //    foreach (DataControlField field in grid.Columns)
            //    {
            //        if (field is CommandField)
            //        {
            //            if (StateText == "Normal" || StateText == "Insert" || StateText == "Prepare" || StateText == "Inquery")
            //            {
            //                ((CommandField)field).ShowDeleteButton = !grid.FLActive;
            //                ((CommandField)field).ShowEditButton = !grid.FLActive;
            //            }
            //            else if (StateText == "Modify")
            //            {
            //                ((CommandField)field).ShowDeleteButton = !grid.FLActive;
            //            }
            //            else if (StateText == "PreInsert")
            //            {
            //                ((CommandField)field).ShowDeleteButton = true;
            //                ((CommandField)field).ShowEditButton = true;
            //            }
            //        }
            //    }
            //}
        }

        public void SetFLState(string flStateText)
        {
            InnerSetFLState(flStateText);
            this.ViewState["CurrentFLState"] = flStateText;
        }

        protected virtual void InnerSetFLState(string flStateText)
        {
            foreach (FLWebNavigatorStateItem stateItem in this.FLStates)
            {
                if (stateItem.StateText == flStateText)
                {
                    this.ViewState["PreviousFLState"] = CurrentFLState;
                    this.ViewState["CurrentFLState"] = flStateText;
                    break;
                }
            }
        }

        private bool containsItem(string itemName, string visibleControls)
        {
            if (visibleControls != null && visibleControls != "")
            {
                string[] vcs = visibleControls.Split(';');
                foreach (string vc in vcs)
                {
                    if (vc == itemName)
                        return true;
                }
            }
            return false;
        }
    }

    #region NavigatorStates
    //[Editor(typeof(WebStateCollectionEditor), typeof(UITypeEditor))]
    public class FLWebNavigatorStateCollection : InfoOwnerCollection
    {
        public FLWebNavigatorStateCollection(Object aOwner, Type aItemType)
            : base(aOwner, typeof(FLWebNavigatorStateItem))
        {
#if !VS90
            FLWebNavigatorStateItem ApproveStateItem = new FLWebNavigatorStateItem();
            base.Add(ApproveStateItem);
            ApproveStateItem.StateText = "Approve";

            FLWebNavigatorStateItem ContinueStateItem = new FLWebNavigatorStateItem();
            base.Add(ContinueStateItem);
            ContinueStateItem.StateText = "Continue";

            FLWebNavigatorStateItem InqueryStateItem = new FLWebNavigatorStateItem();
            base.Add(InqueryStateItem);
            InqueryStateItem.StateText = "Inquery";

            FLWebNavigatorStateItem NotifyStateItem = new FLWebNavigatorStateItem();
            base.Add(NotifyStateItem);
            NotifyStateItem.StateText = "Notify";

            FLWebNavigatorStateItem ReturnStateItem = new FLWebNavigatorStateItem();
            base.Add(ReturnStateItem);
            ReturnStateItem.StateText = "Return";

            FLWebNavigatorStateItem SubmitStateItem = new FLWebNavigatorStateItem();
            base.Add(SubmitStateItem);
            SubmitStateItem.StateText = "Submit";

            FLWebNavigatorStateItem NoneStateItem = new FLWebNavigatorStateItem();
            base.Add(NoneStateItem);
            NoneStateItem.StateText = "None";

            FLWebNavigatorStateItem PlusStateItem = new FLWebNavigatorStateItem();
            base.Add(PlusStateItem);
            PlusStateItem.StateText = "Plus";

            FLWebNavigatorStateItem LockStateItem = new FLWebNavigatorStateItem();
            base.Add(LockStateItem);
            LockStateItem.StateText = "Lock";

            FLWebNavigatorStateItem FSubmitStateItem = new FLWebNavigatorStateItem();
            base.Add(FSubmitStateItem);
            FSubmitStateItem.StateText = "FSubmit";

            FLWebNavigatorStateItem RSubmitStateItem = new FLWebNavigatorStateItem();
            base.Add(RSubmitStateItem);
            RSubmitStateItem.StateText = "RSubmit";
#endif
        }

        public new FLWebNavigatorStateItem this[int index]
        {
            get
            {
                return (FLWebNavigatorStateItem)InnerList[index];
            }
            set
            {
                if (index > -1 && index < Count)
                {
                    if (value is FLWebNavigatorStateItem)
                    {
                        //原来的Collection设置为0
                        ((FLWebNavigatorStateItem)InnerList[index]).Collection = null;
                        InnerList[index] = value;
                        //Collection设置为this
                        ((FLWebNavigatorStateItem)InnerList[index]).Collection = this;
                    }
                }
            }
        }

        new public void Remove(object value)
        {
            FLWebNavigatorStateItem stateItem = value as FLWebNavigatorStateItem;
            if (stateItem != null)
            {
                if (stateItem.StateText == "Approve"
                    || stateItem.StateText == "Continue"
                    || stateItem.StateText == "Inquery"
                    || stateItem.StateText == "Notify"
                    || stateItem.StateText == "Return"
                    || stateItem.StateText == "Submit"
                    || stateItem.StateText == "None"
                    || stateItem.StateText == "Plus"
                    || stateItem.StateText == "Lock"
                    || stateItem.StateText == "FSubmit"
                    || stateItem.StateText == "RSubmit")
                {
                    throw new Exception("Default FLWebNavigatorStateItem can not be removed");
                }
                else
                {
                    base.Remove(value);
                }
            }
        }

        new public void RemoveAt(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                FLWebNavigatorStateItem stateItem = this[index];
                if (stateItem.StateText == "Approve"
                    || stateItem.StateText == "Continue"
                    || stateItem.StateText == "Inquery"
                    || stateItem.StateText == "Notify"
                    || stateItem.StateText == "Return"
                    || stateItem.StateText == "Submit"
                    || stateItem.StateText == "None"
                    || stateItem.StateText == "Plus"
                    || stateItem.StateText == "Lock"
                    || stateItem.StateText == "FSubmit"
                    || stateItem.StateText == "RSubmit")
                {
                    throw new Exception("Default FLWebNavigatorStateItem can not be removed");
                }
                else
                {
                    base.RemoveAt(index);
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        new public void Clear()
        {
            this.ClearExceptDefaultStateItem();
        }

        new public void Add(object value)
        {
            FLWebNavigatorStateItem stateItem = value as FLWebNavigatorStateItem;
            if (stateItem != null)
            {
                foreach (FLWebNavigatorStateItem si in this)
                {
                    if (si.StateText == stateItem.StateText)
                    {
                        si.VisibleControls = stateItem.VisibleControls;
                        si.Description = stateItem.Description;
                        //si.VisibleControlsEdited = stateItem.VisibleControlsEdited;
                        return;
                    }
                }

                base.Add(stateItem);
            }
        }

        public void ClearExceptDefaultStateItem()
        {
            // The number of Default FLNavigatorStateItem is 7
            while (this.Count > 7)
            {
                base.RemoveAt(7);
            }
        }
    }

    public class FLWebNavigatorStateItem : InfoOwnerCollectionItem
    {
        private string _name;
        private string _stateText;
        private string _description;
        //private bool _visibleControlsEdited = false;
        private string _visibleControls;

        public FLWebNavigatorStateItem()
        {
        }

        [NotifyParentProperty(true)]
        public override string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [NotifyParentProperty(true)]
        public string StateText
        {
            get
            {
                return _stateText;
            }
            set
            {
                if (_stateText == "Approve"
                    || _stateText == "Continue"
                    || _stateText == "Inquery"
                    || _stateText == "Notify"
                    || _stateText == "Return"
                    || _stateText == "Submit"
                    || _stateText == "None"
                    || _stateText == "Plus"
                    || _stateText == "Lock"
                    || _stateText == "FSubmit"
                    || _stateText == "RSubmit")
                {
                    throw new Exception("Default StateText Can not be Changed");
                }
                else if (value == null || value.Trim() == "")
                {
                    throw new Exception("Empty StateText not allowed");
                }
                else
                {
                    FLWebNavigatorStateCollection stateCollection = this.Collection as FLWebNavigatorStateCollection;
                    if (stateCollection != null)
                    {
                        foreach (FLWebNavigatorStateItem stateItem in stateCollection)
                        {
                            if (stateItem.StateText == value.Trim())
                            {
                                throw new Exception("StateText already exists");
                            }
                        }
                    }
                    _stateText = value.Trim();
                }
            }
        }

        [NotifyParentProperty(true)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        //[Browsable(false)]
        //[NotifyParentProperty(true)]
        //public bool VisibleControlsEdited
        //{
        //    get
        //    {
        //        return _visibleControlsEdited;
        //    }
        //    set
        //    {
        //        _visibleControlsEdited = value;
        //    }
        //}

        [Editor(typeof(WebVisableControlsEditor), typeof(UITypeEditor))]
        [NotifyParentProperty(true)]
        public string VisibleControls
        {
            get
            {
                return _visibleControls;
            }
            set
            {
                _visibleControls = value;
            }
        }

        public override string ToString()
        {
            return StateText;
        }
    }

    /*internal class WebStateCollectionEditor : UITypeEditor
    {
        IWindowsFormsEditorService EditorService = null;
        public WebStateCollectionEditor()
            : base()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                EditorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            }

            FLWebNavigator flNavigator = context.Instance as FLWebNavigator;

            if (flNavigator != null && EditorService != null)
            {
                if (value is FLWebNavigatorStateCollection)
                {
                    FLWebNavigatorStateCollectionEditorDialog editorDialog = new FLWebNavigatorStateCollectionEditorDialog(value as FLWebNavigatorStateCollection);
                    if (DialogResult.OK == EditorService.ShowDialog(editorDialog))
                    {
                        value = editorDialog.Collection;
                        IDesignerHost service = (IDesignerHost)context.GetService(typeof(IDesignerHost));
                        WebNavigatorDesigner designer = service.GetDesigner(flNavigator) as WebNavigatorDesigner;
                        designer.SetDirty();
                    }
                }
            }
            return value;
        }
    }*/

    internal class WebVisableControlsEditor : UITypeEditor
    {
        IWindowsFormsEditorService EditorService = null;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                EditorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            }
            FLWebNavigatorStateItem stateItem = context.Instance as FLWebNavigatorStateItem;
            FLWebNavigator flNavigator = stateItem.Owner as FLWebNavigator;
            if (EditorService != null && flNavigator != null)
            {
                FLWebNavigatorVisibleControlsEditorDialog editorDialog = new FLWebNavigatorVisibleControlsEditorDialog(value as string, flNavigator);
                if (DialogResult.OK == EditorService.ShowDialog(editorDialog))
                {
                    //stateItem.VisibleControlsEdited = true;
                    value = editorDialog.VisibleControls;
                }
            }
            return value;
        }
    }
    #endregion

    //$edit20100604 by navy For support Ajax,Code Copy Form WebNavigator.cs
    #region Register Script
    internal class ScriptHelper
    {
        public static void RegisterStartupScript(System.Web.UI.Control ctrl, string script)
        {
            RegisterStartupScript(ctrl, null, String.Empty, script);
        }

        public static void RegisterStartupScript(System.Web.UI.Control ctrl, string key, string script)
        {
            RegisterStartupScript(ctrl, null, key, script);

        }

        public static void RegisterStartupScript(System.Web.UI.Control ctrl, Page page, string key, string script)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = Guid.NewGuid().ToString();
            }

            if (page == null)
            {
                page = ctrl.Page;
            }

            System.Web.UI.Control panel = ctrl.Parent;
            while (panel != null && panel.GetType() != typeof(UpdatePanel))
            {
                panel = panel.Parent;
            }
            if (panel != null)
            {
                ScriptManager.RegisterStartupScript(panel as UpdatePanel, page.GetType(), key, script, true);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), key, "<script>" + script + "</script>");
            }
        }

        public static void ShowMessage(System.Web.UI.Control ctrl, string key, string message)
        {
            string script = "alert('" + message + "')";
            RegisterStartupScript(ctrl, key, script);
        }

        public static void ShowMessage(System.Web.UI.Control ctrl, string message)
        {
            ShowMessage(ctrl, string.Empty, message);
        }
    }
    #endregion
}
