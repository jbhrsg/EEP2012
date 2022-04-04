using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using Srvtools;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace FLTools
{
#if VS90
    [Designer(typeof(FLWebWizardDesigner), typeof(IDesigner))]
#else
    [Designer(typeof(DataSourceDesigner), typeof(IDesigner))]
#endif
    public class FLWebWizard : Control, IBaseWebControl
    {
        #region Properties
        [Category("InfoLight")]
        public ESqlMode SqlMode
        {
            get
            {
                object obj = this.ViewState["SqlMode"];
                if(obj != null)
                    return (ESqlMode)obj;
                return ESqlMode.ToDoList;
            }
            set
            {
                this.ViewState["SqlMode"] = value;
            }
        }

        [Category("InfoLight")]
        public bool Active
        {
            get
            {
                object obj = this.ViewState["Active"];
                if (obj != null)
                    return (bool)obj;
                return false;
            }
            set
            {
                this.ViewState["Active"] = value;
            }
        }

        /*[Category("InfoLight")]
        public string DataSourceID
        {
            get
            {
                object obj = this.ViewState["DataSourceID"];
                if (obj != null)
                    return (string)obj;
                return "";
            }
            set
            {
                this.ViewState["DataSourceID"] = value;
            }
        }*/

        [Category("InfoLight")]
        public string BindingObjectID
        {
            get
            {
                object obj = this.ViewState["BindingObjectID"];
                if (obj != null)
                    return (string)obj;
                return "";
            }
            set
            {
                this.ViewState["BindingObjectID"] = value;
            }
        }

        [Browsable(false)]
        public string Filter
        {
            get
            {
                object obj = this.ViewState["Filter"];
                if (obj != null)
                    return (string)obj;
                return "";
            }
            set
            {
                this.ViewState["Filter"] = value;
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.Refresh();
                OnRefresh(EventArgs.Empty);
            }
            base.OnLoad(e);
        }

        static readonly object refreshEventKey = new object();

        public event EventHandler<EventArgs> Refreshed
        {
            add { this.Events.AddHandler(refreshEventKey, value); }
            remove { this.Events.RemoveHandler(refreshEventKey, value); }
        }

        protected virtual void OnRefresh(EventArgs e)
        {
            EventHandler<EventArgs> refreshHandler = (EventHandler<EventArgs>)this.Events[refreshEventKey];
            if (refreshHandler != null)
            {
                refreshHandler(this, e);
            }
        }

        public void Refresh()
        {
            BindToGrid(GetList("", 2, false, null), "", false);
        }

        public void Refresh(string sort, string filter)
        {
            BindToGrid(GetList(filter, 2, false, null), sort, false);
        }

        public void Refresh(int delayLevel, bool delayIgnoreWeekends, List<string> extDates)
        {
            BindToGrid(GetList("", delayLevel, delayIgnoreWeekends, extDates), "", false);
        }

        public DataView GetDelayList(int delayLevel, bool delayIgnoreWeekends, List<string> extDates)
        {
            return GetList("", delayLevel, delayIgnoreWeekends, extDates);
        }

        public void CustomerGetList()
        {
            BindToGrid(GetList("", 2, false, null), "", true);
        }

        public void CustomerGetList(string filter)
        {
            BindToGrid(GetList(filter, 2, false, null), "", true);
        }

        public void CustomerGetList(int delayLevel, bool delayIgnoreWeekends, List<string> extDates)
        {
            BindToGrid(GetList("", delayLevel, delayIgnoreWeekends, extDates), "", true);
        }

        private DataView GetList(string filter, int delayLevel, bool delayIgnoreWeekends, List<string> delayExtDates)
        {
            if (!this.Active) return null;
            string user = CliUtils.fLoginUser;
            if (!string.IsNullOrEmpty(user))
            {
                DataTable tab = null;
                if (this.SqlMode == ESqlMode.AllStatist)
                {
                    DataTable tabTodolist = null;
                    string sqlTodolist = GloFix.GetFlowSql(user, ESqlMode.ToDoListStatist);
                    object[] objTodolist = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlTodolist });
                    if ((null != objTodolist) && (0 == (int)objTodolist[0])) tabTodolist = ((DataSet)objTodolist[1]).Tables[0];

                    DataTable tabTodohis = null;
                    string sqlTodohis = GloFix.GetFlowSql(user, ESqlMode.ToDoHisStatist);
                    object[] objTodohis = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlTodohis });
                    if ((null != objTodohis) && (0 == (int)objTodohis[0])) tabTodohis = ((DataSet)objTodohis[1]).Tables[0];

                    DataTable tabNotify = null;
                    string sqlNotify = GloFix.GetFlowSql(user, ESqlMode.NotifyStatist);
                    object[] objNotify = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlNotify });
                    if ((null != objNotify) && (0 == (int)objNotify[0])) tabNotify = ((DataSet)objNotify[1]).Tables[0];

                    DataTable tabDelay = null;
                    object[] param = new object[5] { user, delayLevel, delayIgnoreWeekends, delayExtDates, false };
                    object[] objDelay = CliUtils.CallMethod("GLModule", "FLOvertimeList", param);
                    if ((null != objDelay) && (0 == (int)objDelay[0])) tabDelay = (DataTable)objDelay[1];

                    tab = new DataTable();
                    tab.Columns.AddRange(
                        new DataColumn[] 
                        { 
                            new DataColumn("FLOW_DESC", typeof(string)), 
                            new DataColumn("TODOLIST_COUNT", typeof(int)), 
                            new DataColumn("TODOHIS_COUNT", typeof(int)), 
                            new DataColumn("NOTIFY_COUNT", typeof(int)),
                            new DataColumn("DELAY_COUNT", typeof(int))
                        });

                    List<string> lstFlow = allFlowList(new DataTable[] { tabTodolist, tabTodohis, tabNotify, tabDelay });

                    foreach (string flow in lstFlow)
                    {
                        DataRow newRow = tab.NewRow();
                        newRow["FLOW_DESC"] = flow;
                        DataRow[] todolistRows = tabTodolist.Select(string.Format("FLOW_DESC='{0}'", flow));
                        DataRow[] todohisRows = tabTodohis.Select(string.Format("FLOW_DESC='{0}'", flow));
                        DataRow[] notifyRows = tabNotify.Select(string.Format("FLOW_DESC='{0}'", flow));
                        DataRow[] delayRows = tabDelay.Select(string.Format("FLOW_DESC='{0}'", flow));
                        if (todolistRows.Length == 1) newRow["TODOLIST_COUNT"] = todolistRows[0][1];
                        else newRow["TODOLIST_COUNT"] = 0;
                        if (todohisRows.Length == 1) newRow["TODOHIS_COUNT"] = todohisRows[0][1];
                        else newRow["TODOHIS_COUNT"] = 0;
                        if (notifyRows.Length == 1) newRow["NOTIFY_COUNT"] = notifyRows[0][1];
                        else newRow["NOTIFY_COUNT"] = 0;
                        if (delayRows.Length == 1) newRow["DELAY_COUNT"] = delayRows[0][1];
                        else newRow["DELAY_COUNT"] = 0;

                        tab.Rows.Add(newRow);
                    }
                }
                else if (this.SqlMode == ESqlMode.Delay || this.SqlMode == ESqlMode.DelayStatist)
                {
                    object[] param = new object[5] { user, delayLevel, delayIgnoreWeekends, delayExtDates, this.SqlMode == ESqlMode.Delay };
                    object[] obj = CliUtils.CallMethod("GLModule", "FLOvertimeList", param);
                    if ((null != obj) && (0 == (int)obj[0]))
                    {
                        tab = (DataTable)obj[1];
                    }
                }
                // ToDoList,ToDoHis,Notify,FlowRunOver,ToDoListStatist,ToDoHisStatist,NotifyStatist
                else
                {
                    string sql = GloFix.GetFlowSql(user, this.SqlMode);
                    //if (this.SqlMode == ESqlMode.FlowRunOver)
                    //{
                    //    this.Filter = "";
                    //}
                    if (!string.IsNullOrEmpty(this.Filter))
                    {
                        sql = sql.Insert(sql.LastIndexOf("ORDER BY"), " AND " + this.Filter);
                    }
                    if (!string.IsNullOrEmpty(filter))
                    {
                        sql = sql.Insert(sql.LastIndexOf("ORDER BY"), " AND " + filter);
                    }
                    object[] obj = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
                    if ((null != obj) && (0 == (int)obj[0])) tab = ((DataSet)obj[1]).Tables[0];
                }
                return new DataView(tab);
            }
            return null;
        }

        private void BindToGrid(DataView view, string sort, bool isCustomerCall)
        {
            if (view == null) return;
            Control ctrl = this.Parent.FindControl(this.BindingObjectID);
            if (ctrl != null && ctrl is GridView)
            {
                GridView grid = (GridView)ctrl;
                //if (this.SqlMode == ESqlMode.FlowRunOver && grid.Columns.Count > 1 && grid.Columns[1] is TemplateField && ((TemplateField)grid.Columns[1]).SortExpression == "FLOWIMPORTANT")
                //{
                //    grid.Columns.RemoveAt(1);
                //}
                if (isCustomerCall)
                {
                    if (this.SqlMode == ESqlMode.Delay)
                    {
                        grid.AutoGenerateColumns = false;
                        grid.Columns.Clear();
                        BoundField descField = new BoundField();
                        descField.DataField = "FLOW_DESC";
                        descField.HeaderText = "FLOW_DESC";
                        grid.Columns.Add(descField);

                        BoundField stepField = new BoundField();
                        stepField.DataField = "D_STEP_ID";
                        stepField.HeaderText = "D_STEP_ID";
                        grid.Columns.Add(stepField);

                        BoundField presentField = new BoundField();
                        presentField.DataField = "FORM_PRESENT_CT";
                        presentField.HeaderText = "FORM_PRESENT_CT";
                        grid.Columns.Add(presentField);

                        BoundField remarkField = new BoundField();
                        remarkField.DataField = "REMARK";
                        remarkField.HeaderText = "REMARK";
                        grid.Columns.Add(remarkField);

                        BoundField sendtoField = new BoundField();
                        sendtoField.DataField = "SENDTO_DETAIL";
                        sendtoField.HeaderText = "SENDTO_DETAIL";
                        grid.Columns.Add(sendtoField);

                        BoundField timeField = new BoundField();
                        timeField.DataField = "UPDATE_WHOLE_TIME";
                        timeField.HeaderText = "UPDATE_WHOLE_TIME";
                        grid.Columns.Add(timeField);

                        BoundField overtimeField = new BoundField();
                        overtimeField.DataField = "OVERTIME";
                        overtimeField.HeaderText = "OVERTIME";
                        grid.Columns.Add(overtimeField);
                    }
                }
                view.Sort = sort;
                grid.DataSource = view;
                grid.DataBind();
                SetGridHeaderText(grid);
            }
        }

        private void SetGridHeaderText(GridView grid)
        {
            if (grid.HeaderRow == null) return;
            string[] toDoListCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "ToDoListColumns", true).Split(',');
            string[] overtimeCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "OvertimeColumns", true).Split(',');
            string[] statistCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "StatistColumns", true).Split(',');
            foreach (TableCell cell in grid.HeaderRow.Cells)
            {
                cell.Wrap = false;
                if (this.SqlMode == ESqlMode.Delay)
                {
                    if (grid.AllowSorting)
                    {
                        if (cell.Controls.Count == 1 && cell.Controls[0] is LinkButton)
                        {
                            LinkButton lnk = cell.Controls[0] as LinkButton;
                            if (lnk.Text == "FLOW_DESC") lnk.Text = overtimeCols[0];
                            else if (lnk.Text == "D_STEP_ID") lnk.Text = overtimeCols[1];
                            else if (lnk.Text == "FORM_PRESENT_CT") lnk.Text = overtimeCols[2];
                            else if (lnk.Text == "REMARK") lnk.Text = overtimeCols[4];
                            else if (lnk.Text == "SENDTO_DETAIL") lnk.Text = overtimeCols[3];
                            else if (lnk.Text == "UPDATE_WHOLE_TIME") lnk.Text = overtimeCols[5];
                            else if (lnk.Text == "OVERTIME") lnk.Text = overtimeCols[6];
                        }
                    }
                    else
                    {
                        if (cell.Text == "FLOW_DESC") cell.Text = overtimeCols[0];
                        else if (cell.Text == "D_STEP_ID") cell.Text = overtimeCols[1];
                        else if (cell.Text == "FORM_PRESENT_CT") cell.Text = overtimeCols[2];
                        else if (cell.Text == "REMARK") cell.Text = overtimeCols[4];
                        else if (cell.Text == "SENDTO_DETAIL") cell.Text = overtimeCols[3];
                        else if (cell.Text == "UPDATE_WHOLE_TIME") cell.Text = overtimeCols[5];
                        else if (cell.Text == "OVERTIME") cell.Text = overtimeCols[6];
                    }
                }
                else if (this.SqlMode == ESqlMode.FlowRunOver)
                {
                    if (grid.AllowSorting)
                    {
                        if (cell.Controls.Count == 1 && cell.Controls[0] is LinkButton)
                        {
                            LinkButton lnk = cell.Controls[0] as LinkButton;
                            if (lnk.Text == "FLOW_DESC") lnk.Text = toDoListCols[0];
                            else if (lnk.Text == "D_STEP_ID") lnk.Text = toDoListCols[1];
                            else if (lnk.Text == "FORM_NAME") lnk.Text = toDoListCols[8];
                            else if (lnk.Text == "WEBFORM_NAME") lnk.Text = toDoListCols[9];
                            else if (lnk.Text == "FORM_PRESENT_CT") lnk.Text = toDoListCols[2];
                            else if (lnk.Text == "REMARK") lnk.Text = toDoListCols[4];
                            else if (lnk.Text == "UPDATE_WHOLE_TIME") lnk.Text = toDoListCols[5];
                        }
                    }
                    else
                    {
                        if (cell.Text == "FLOW_DESC") cell.Text = toDoListCols[0];
                        else if (cell.Text == "D_STEP_ID") cell.Text = toDoListCols[1];
                        else if (cell.Text == "FORM_NAME") cell.Text = toDoListCols[8];
                        else if (cell.Text == "WEBFORM_NAME") cell.Text = toDoListCols[9];
                        else if (cell.Text == "FORM_PRESENT_CT") cell.Text = toDoListCols[2];
                        else if (cell.Text == "REMARK") cell.Text = toDoListCols[4];
                        else if (cell.Text == "UPDATE_WHOLE_TIME") cell.Text = toDoListCols[5];
                    }
                }
                else if (this.SqlMode == ESqlMode.ToDoListStatist || this.SqlMode == ESqlMode.ToDoHisStatist
                    || this.SqlMode == ESqlMode.NotifyStatist || this.SqlMode == ESqlMode.DelayStatist
                    || this.SqlMode == ESqlMode.AllStatist)
                {
                    if (cell.Text == "FLOW_DESC") cell.Text = statistCols[0];
                    else if (cell.Text == "TODOLIST_COUNT") cell.Text = statistCols[1];
                    else if (cell.Text == "TODOHIS_COUNT") cell.Text = statistCols[2];
                    else if (cell.Text == "NOTIFY_COUNT") cell.Text = statistCols[3];
                    else if (cell.Text == "DELAY_COUNT") cell.Text = statistCols[4]; 
                }
                else
                {
                    if (grid.AllowSorting)
                    {
                        if (cell.Controls.Count == 1 && cell.Controls[0] is LinkButton)
                        {
                            LinkButton lnk = cell.Controls[0] as LinkButton;
                            if (lnk.Text == "FLOW_DESC") lnk.Text = toDoListCols[0];
                            else if (lnk.Text == "D_STEP_ID") lnk.Text = toDoListCols[1];
                            else if (lnk.Text == "FORM_PRESENT_CT") lnk.Text = toDoListCols[2];
                            else if (lnk.Text == "USERNAME") lnk.Text = toDoListCols[3];
                            else if (lnk.Text == "REMARK") lnk.Text = toDoListCols[4];
                            else if (lnk.Text == "UPDATE_WHOLE_TIME") lnk.Text = toDoListCols[5];
                            else if (lnk.Text == "STATUS") lnk.Text = toDoListCols[6];
                            else if (lnk.Text == "SENDTO_NAME") lnk.Text = toDoListCols[7];
                        }
                    }
                    else
                    {
                        if (cell.Text == "FLOW_DESC") cell.Text = toDoListCols[0];
                        else if (cell.Text == "D_STEP_ID") cell.Text = toDoListCols[1];
                        else if (cell.Text == "FORM_PRESENT_CT") cell.Text = toDoListCols[2];
                        else if (cell.Text == "USERNAME") cell.Text = toDoListCols[3];
                        else if (cell.Text == "REMARK") cell.Text = toDoListCols[4];
                        else if (cell.Text == "UPDATE_WHOLE_TIME") cell.Text = toDoListCols[5];
                        else if (cell.Text == "STATUS") cell.Text = toDoListCols[6];
                        else if (cell.Text == "SENDTO_NAME") cell.Text = toDoListCols[7];
                    }
                }
            }
        }

        private List<string> allFlowList(DataTable[] tables)
        {
            List<string> lstFlow = new List<string>();
            foreach (DataTable table in tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!lstFlow.Contains(row["FLOW_DESC"].ToString()))
                        lstFlow.Add(row["FLOW_DESC"].ToString());
                }
            }
            return lstFlow;
        }

        public void OpenFlowDetail(string list)
        {
            if (!this.Active) return;
            DataView view = this.GetList(string.Format("LISTID='{0}'", list), 2, false, null);
            if (view.Count == 1)
            {
                DataRowView row = view[0];
                //string path, flowPath, formPresent, navMode, flMode, flImportant, flUrgent, status, plusApprove, multiStepReturn, attachments, sendToId, parameters;
                string path = row["WEBFORM_NAME"].ToString();
                if (path.IndexOf('.') != -1) path = path.Replace('.', '/');

                string formPresent = HttpUtility.UrlEncode(row["FORM_PRESENTATION"].ToString());
                string flowPath = HttpUtility.UrlEncode(row["FLOWPATH"].ToString());
                string flImportant = row["FLOWIMPORTANT"].ToString();
                string flUrgent = row["FLOWURGENT"].ToString();
                string status = row["STATUS"].ToString();
                string plusApprove = row["PLUSAPPROVE"].ToString();
                string sendToId = row["SENDTO_ID"].ToString();
                string parameters = row["PARAMETERS"].ToString();

                string urlQuery = "";
                string[] urlParam = null;
                if (this.SqlMode == ESqlMode.ToDoList || this.SqlMode == ESqlMode.Notify)
                {
                    string navMode = row["NAVIGATOR_MODE"].ToString();
                    string flMode = row["FLNAVIGATOR_MODE"].ToString();
                    string multiStepReturn = row["MULTISTEPRETURN"].ToString();
                    string attachments = HttpUtility.UrlEncode(row["ATTACHMENTS"].ToString());

                    urlQuery = "LISTID={0}&FLOWPATH={1}&WHERESTRING={2}&NAVMODE={3}&FLNAVMODE={4}&ISIMPORTANT={5}&ISURGENT={6}&STATUS={7}&PLUSAPPROVE={8}&MULTISTEPRETURN={9}&ATTACHMENTS={10}&SENDTOID={11}{12}";
                    urlParam = new string[] { list, flowPath, formPresent, navMode, flMode, flImportant, flUrgent, status, plusApprove, multiStepReturn, attachments, sendToId, parameters };
                }
                else if(this.SqlMode == ESqlMode.ToDoHis)
                {
                    string attachments = HttpUtility.UrlEncode(row["ATTACHMENTS"].ToString());

                    urlQuery = "LISTID={0}&FLOWPATH={1}&WHERESTRING={2}&ATTACHMENTS={3}&NAVMODE=0&FLNAVMODE=6{4}";
                    urlParam = new string[] { list, flowPath, formPresent, attachments, parameters };
                }
                else if (this.SqlMode == ESqlMode.FlowRunOver)
                {
                    string attachments = HttpUtility.UrlEncode(row["ATTACHMENTS"].ToString());

                    urlQuery = "LISTID={0}&WHERESTRING={1}&ATTACHMENTS={2}&NAVMODE=0&FLNAVMODE=6";
                    urlParam = new string[] { list, formPresent, attachments };
                }
                else if (this.SqlMode == ESqlMode.Delay)
                {
                    urlQuery = "LISTID={0}&WHERESTRING={1}&NAVMODE=0&FLNAVMODE=6";
                    urlParam = new string[] { list, formPresent };
                }
                this.Page.Response.Redirect(string.Format("~/" + path + ".aspx?" + urlQuery, urlParam));
            }
        }

        #region IBaseWebControl
        internal Control FindChildControl(string strid, Control ct)
        {
            if (ct.ID == strid)
            {
                return ct;
            }
            else
            {
                if (ct.HasControls())
                {
                    foreach (Control ctchild in ct.Controls)
                    {
                        Control ctrtn = FindChildControl(strid, ctchild);
                        if (ctrtn != null)
                        {
                            return ctrtn;
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }
        internal Control FindChildControl(string strid, Control ct, FindControlType type, Type ReturnControlType)
        {
            string fieldName = "ID";
            if (type == FindControlType.DataSourceID)
            {
                fieldName = "DataSourceID";
            }
            else if (type == FindControlType.BindingObject)
            {
                fieldName = "BindingObject";
            }
            else if (type == FindControlType.MasterDataSource)
            {
                fieldName = "MasterDataSource";
            }

            Type ctType = ct.GetType();
            PropertyInfo pi = ctType.GetProperty(fieldName);
            if (pi != null && pi.GetValue(ct, null) != null && pi.GetValue(ct, null).ToString() == strid && ReturnControlType.IsInstanceOfType(ct))
            {
                return ct;
            }
            else
            {
                if (ct.HasControls())
                {
                    foreach (Control ctchild in ct.Controls)
                    {
                        Control ctrtn = FindChildControl(strid, ctchild, type, ReturnControlType);
                        if (ctrtn != null)
                        {
                            return ctrtn;
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        internal Control ExtendedFindChildControl(string strid, FindControlType type, Type ReturnControlType)
        {
            Control objContentPlaceHolder = this.Page.Form.FindControl("ContentPlaceHolder1");
            if (objContentPlaceHolder != null)
            {
                return this.FindChildControl(strid, objContentPlaceHolder, type, ReturnControlType);
            }
            else
            {
                return this.FindChildControl(strid, this.Page.Form, type, ReturnControlType);
            }
        }

        public object GetObjByID(string ObjID)
        {
            if (this.Site != null)
            {
                return FindChildControl(ObjID, this.Page);
            }
            else
            {
                if (this.Page.Form != null)
                    return FindChildControl(ObjID, this.Page.Form);
                else
                    return FindChildControl(ObjID, this.Page);
            }
        }
        #endregion

        /*public virtual string OpenUrl()
        {
            //string isListNullMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "IsListIdNull", true);
            string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", true);
            string script = "javascript:alert('" + selDataMessage + "');";
            object objGrid = this.Parent.FindControl(this.BindingObjectID);
            if (objGrid != null && objGrid is GridView)
            {
                GridView grid = (GridView)objGrid;
                if (grid.SelectedRow != null)
                {
                    string FORM_NAME = grid.SelectedRow.Cells[36].Text;
                    string packageName = HttpUtility.UrlEncode(FORM_NAME.Substring(0, FORM_NAME.IndexOf('.')));
                    string formName = HttpUtility.UrlEncode(FORM_NAME.Substring(FORM_NAME.IndexOf('.') + 1));
                    string listId = grid.SelectedRow.Cells[2].Text;
                    string flowPath = HttpUtility.UrlEncode(grid.SelectedRow.Cells[38].Text);
                    string whereString = HttpUtility.UrlEncode(grid.SelectedRow.Cells[26].Text);
                    string navMode = "", flMode = "";
                    if (this.SqlMode == ESqlMode.ToDoList)
                    {
                        navMode = grid.SelectedRow.Cells[16].Text;
                        flMode = grid.SelectedRow.Cells[17].Text;
                    }
                    else if (this.SqlMode == ESqlMode.ToDoHis)
                    {
                        navMode = "0";
                        flMode = "6";
                    }
                    script = packageName + "\\" + formName + ".aspx?LISTID=" + listId + "&FLOWPATH=" + flowPath + "&WHERESTRING=" + whereString + "&NAVMODE=" + navMode + "&FLNAVMODE=" + flMode;
                }
            }
            return script;
        }

        public virtual void Approve()
        {
            string script = GloOperate(FLNavigatorOperate.Approve);
            this.Page.Response.Write("<script>window.open('" + script + "', '', 'resizable=yes,scrollbars=yes,width=500,height=500,top=200,left=200');</script>");
        }

        public virtual string ApproveUrl()
        {
            return "window.open('" + GloOperate(FLNavigatorOperate.Approve) + "', '', 'resizable=yes,scrollbars=yes,width=500,height=500,top=200,left=200');return false;";
        }

        public virtual void Return()
        {
            string script = GloOperate(FLNavigatorOperate.Return);
            this.Page.Response.Write("<script>window.open('" + script + "', '', 'resizable=yes,scrollbars=yes,width=500,height=500,top=200,left=200');</script>");
        }

        public virtual string ReturnUrl()
        {
            return "window.open('" + GloOperate(FLNavigatorOperate.Return) + "', '', 'resizable=yes,scrollbars=yes,width=500,height=500,top=200,left=200');return false;";
        }

        public bool Reject()
        {
            if (this.SqlMode == ESqlMode.ToDoList)
            {
                object objGrid = this.Parent.FindControl(this.BindingObjectID);
                if (objGrid != null && objGrid is GridView)
                {
                    GridView grid = (GridView)objGrid;
                    if (grid.SelectedRow != null)
                    {
                        string listId = grid.SelectedRow.Cells[2].Text;
                        string keys = grid.SelectedRow.Cells[25].Text;
                        string values = grid.SelectedRow.Cells[26].Text;
                        values = values.Replace("'", "''");
                        object[] objParams = CliUtils.CallFLMethod("Reject", new object[] { new Guid(listId), new object[] { keys, values } });
                        if (Convert.ToInt16(objParams[0]) == 0)
                            return true;
                    }
                    else
                    {
                        string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", true);
                        this.Page.Response.Write("<script>alert('" + selDataMessage + "');</script>");
                    }
                }
            }
            return false;
        }

        private string GloOperate(FLNavigatorOperate operate)
        {
            string isListNullMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "IsListIdNull", true);
            string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", true);
            string script = "javascript:alert('" + selDataMessage + "');";
            if (this.SqlMode == ESqlMode.ToDoList)
            {
                object objGrid = this.Parent.FindControl(this.BindingObjectID);
                if (objGrid != null && objGrid is GridView)
                {
                    GridView grid = (GridView)objGrid;
                    if (grid.SelectedRow != null)
                    {
                        string listId = grid.SelectedRow.Cells[2].Text;
                        string flowPath = grid.SelectedRow.Cells[38].Text;
                        string provider = grid.SelectedRow.Cells[29].Text;
                        string keys = grid.SelectedRow.Cells[25].Text;
                        string values = grid.SelectedRow.Cells[26].Text;
                        values = values.Replace("'", "$$$");
                        if (listId == "")
                        {
                            script = "alert('" + isListNullMessage + "')";
                        }
                        else
                        {
                            string param = "LISTID=" + listId + "&OPERATETYPE=" + operate.ToString() + "&KEYS=" + HttpUtility.UrlEncode(keys) + "&VALUES=" + HttpUtility.UrlEncode(values) + "&FLOWPATH=" + HttpUtility.UrlEncode(flowPath) + "&PROVIDER=" + HttpUtility.UrlEncode(provider);
                            script = "InnerPages/FlowSubmitConfirm.aspx?" + param;
                        }
                    }
                }
            }
            else
            {
                script = "javascript:alert('" + SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "OnlyToDoListWork", true) + "');";
            }
            return script;
        }*/
    }
}
