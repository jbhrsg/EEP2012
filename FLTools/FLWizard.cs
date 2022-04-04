using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    [ToolboxItem(true)]
    [Designer(typeof(FLWizardDesigner), typeof(IDesigner))]
    public class FLWizard : InfoBaseComp
    {
        public FLWizard()
        {
            _formCollection = new Srvtools.FormCollection(this, typeof(FormItem));
        }

        private ESqlMode _sqlMode = ESqlMode.ToDoList;
        private bool _active;
        private DataGridView _bindingObject;
        private Srvtools.FormCollection _formCollection;
        public string Filter = "";

        #region Properties
        [Category("InfoLight")]
        public ESqlMode SqlMode
        {
            get
            {
                return _sqlMode;
            }
            set
            {
                _sqlMode = value;
            }
        }

        [Category("InfoLight")]
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }

        [Category("InfoLight")]
        public DataGridView BindingObject
        {
            get
            {
                return _bindingObject;
            }
            set
            {
                _bindingObject = value;
            }
        }
        #endregion

        public virtual void Refresh()
        {
            BindToGrid(GetList("", 2, false, null));
        }

        public virtual void Refresh(string filter)
        {
            BindToGrid(GetList(filter, 2, false, null));
        }

        public virtual void Refresh(int delayLevel, bool delayIgnoreWeekends, List<string> extDates)
        {
            BindToGrid(GetList("", delayLevel, delayIgnoreWeekends, extDates));
        }

        public void CustomerGetList()
        {
            BindToGrid(GetList("", 2, false, null));
        }

        public void CustomerGetList(string filter)
        {
            BindToGrid(GetList(filter, 2, false, null));
        }

        public void CustomerGetList(int delayLevel, bool delayIgnoreWeekends, List<string> extDates)
        {
            BindToGrid(GetList("", delayLevel, delayIgnoreWeekends, extDates));
        }

        private DataTable GetList(string filter, int delayLevel, bool delayIgnoreWeekends, List<string> delayExtDates)
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
                return tab;
            }
            return null;
        }

        private void BindToGrid(DataTable tab)
        {
            if (tab == null) return;
            this.BindingObject.DataSource = tab;
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

        public void OpenFlowDetail(object row)
        {
            if (row != null && row is DataRowView)
            {
                DataRowView rowView = row as DataRowView;

                string packageForm = rowView["FORM_NAME"].ToString();
                string ItemParam = GetItemParam(rowView);
                StringDictionary FLParams = GetFormParam(rowView);

                string package = packageForm.Substring(0, packageForm.IndexOf('.'));
                string form = packageForm.Substring(packageForm.IndexOf('.') + 1);

                if (this.OwnerComp != null && this.OwnerComp is IShowForm)
                {
                    IShowForm iform = (IShowForm)this.OwnerComp;
                    iform.showForm(package, form, ItemParam, FLParams);
                }
            }
        }

        //public virtual void Open()
        //{
        //    if (this.BindingObject != null && this.BindingObject.SelectedRows.Count == 1)
        //    {
        //        DataRowView rowView = (DataRowView)this.BindingObject.SelectedRows[0].DataBoundItem;
        //        string formName = rowView["FORM_NAME"].ToString();
        //        string ItemParam = GetItemParam(rowView);
        //        StringDictionary FLParams = GetFormParam(rowView);

        //        string PackageName = formName.Substring(0, formName.IndexOf('.'));
        //        string FormName = formName.Substring(formName.IndexOf('.') + 1);

        //        if (this.OwnerComp != null && this.OwnerComp is IShowForm)
        //        {
        //            IShowForm form = (IShowForm)this.OwnerComp;
        //            form.showForm(PackageName, FormName, ItemParam, FLParams);
        //        }
        //    }
        //}

        public virtual void DeleteNotify()
        {
            if (this.SqlMode == ESqlMode.Notify && this.BindingObject != null && this.BindingObject.SelectedRows.Count == 1)
            {
                DataRowView rowView = (DataRowView)this.BindingObject.SelectedRows[0].DataBoundItem;
                string listId = rowView["LISTID"].ToString();
                string flowPath = rowView["FLOWPATH"].ToString();
                object[] objParams = CliUtils.CallFLMethod("DeleteNotify", new object[] { listId, flowPath });
                if (Convert.ToInt16(objParams[0]) == 0)
                {
                    MessageBox.Show(SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowDeleted"));
                }
                else if (Convert.ToInt16(objParams[0]) == 2)
                {
                    MessageBox.Show(objParams[1].ToString());
                }
            }
            Refresh();
        }

        public virtual bool Approve()
        {
            return GloOperate(FLNavigatorOperate.Approve);
        }

        public virtual bool Reject()
        {
            if (this.SqlMode == ESqlMode.ToDoList && this.BindingObject != null && this.BindingObject.SelectedRows.Count == 1)
            {
                DataRowView rowView = (DataRowView)this.BindingObject.SelectedRows[0].DataBoundItem;
                string listId = rowView["LISTID"].ToString();
                string keys = rowView["FORM_KEYS"].ToString();
                string values = rowView["FORM_PRESENTATION"].ToString();
                values = values.Replace("'", "''");
                object[] objParams = CliUtils.CallFLMethod("Reject", new object[] { new Guid(listId), new object[] { keys, values } });
                if (Convert.ToInt16(objParams[0]) == 0)
                    return true;
            }
            return false;
        }

        public virtual bool Return()
        {
            return Return("");
        }

        public virtual bool Return(string stepid)
        {
            return GloOperate(FLNavigatorOperate.Return, stepid);
        }

        private bool GloOperate(FLNavigatorOperate operate)
        {
            return GloOperate(operate, "");
        }

        private bool GloOperate(FLNavigatorOperate operate, string stepid)
        {
            bool sucess = false;
            if (this.SqlMode == ESqlMode.ToDoList && this.BindingObject != null && this.BindingObject.SelectedRows.Count == 1)
            {
                DataRowView rowView = (DataRowView)(DataRowView)this.BindingObject.SelectedRows[0].DataBoundItem;
                SubmitConfirm frmSubmitConfirm = new SubmitConfirm();
                frmSubmitConfirm.listId = rowView["LISTID"].ToString();
                frmSubmitConfirm.flowPath = rowView["FLOWPATH"].ToString();
                frmSubmitConfirm.operate = operate;
                frmSubmitConfirm.provider = rowView["PROVIDER_NAME"].ToString();
                frmSubmitConfirm.keys = rowView["FORM_KEYS"].ToString();
                frmSubmitConfirm.values = rowView["FORM_PRESENTATION"].ToString().Replace("'", "''");
                frmSubmitConfirm.isFlowImportant = (rowView["FLOWIMPORTANT"].ToString() == "1");
                frmSubmitConfirm.isFlowUrgent = (rowView["FLOWURGENT"].ToString() == "1");
                frmSubmitConfirm.multiStepReturn = (rowView["MULTISTEPRETURN"].ToString() == "1");
                if (rowView["ATTACHMENTS"] != null && rowView["ATTACHMENTS"].ToString() != "")
                {
                    string[] attachments = rowView["ATTACHMENTS"].ToString().Split(';');
                    List<string> lstAttachments = new List<string>();
                    foreach (string attach in attachments)
                    {
                        if (attach != "")
                        {
                            lstAttachments.Add(attach);
                        }
                    }
                    frmSubmitConfirm.attachments = lstAttachments;
                }

                sucess = (frmSubmitConfirm.ShowDialog() == DialogResult.OK);
            }
            else if (this.SqlMode == ESqlMode.ToDoHis && operate == FLNavigatorOperate.Return && this.BindingObject != null && this.BindingObject.SelectedRows.Count == 1)
            {
                DataRowView rowView = (DataRowView)this.BindingObject.SelectedRows[0].DataBoundItem;
                string listId = rowView["LISTID"].ToString();
                string sql = "SELECT * FROM SYS_TODOHIS WHERE LISTID='" + listId + "' AND D_STEP_ID='" + stepid + "' ORDER BY UPDATE_TIME DESC";
                DataSet ds = null;

                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    ds = (DataSet)ret1[1];
                }
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show(SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLRuntime", "InstanceManager", "NotRetakePleaseRefresh"));
                        return sucess;
                    }

                    DataRow row = ds.Tables[0].Rows[0];
                    string currentFLActivityName = row["S_STEP_ID"].ToString();
                    string keys = row["FORM_KEYS"].ToString();
                    string keyvalues = row["FORM_PRESENTATION"].ToString();
                    keyvalues = keyvalues.Replace("'", "''");
                    object[] objParams = CliUtils.CallFLMethod("Retake", new object[] { new Guid(listId), new object[] { currentFLActivityName, "" }, new object[] { keys, keyvalues } });
                    if (Convert.ToInt16(objParams[0]) == 0)
                    {
                        sucess = true;
                        MessageBox.Show(SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "RetakeSucess"));
                    }
                    else if (Convert.ToInt16(objParams[0]) == 2)
                    {
                        MessageBox.Show(objParams[1].ToString());
                    }
                }
            }
            return sucess;
        }

        private string GetItemParam(DataRowView oriRow)
        {
            if (oriRow.DataView.Table.Columns.Contains("PARAMETERS"))
                return oriRow["PARAMETERS"].ToString();
            return "";
        }

        private StringDictionary GetFormParam(DataRowView rowView)
        {
            StringDictionary dicFormParam = new StringDictionary();
            dicFormParam.Add("LISTID", rowView["LISTID"].ToString());
            dicFormParam.Add("WHERESTRING", rowView["FORM_PRESENTATION"].ToString());
            string flNavMode = "", navMode = "", plusRoles = "";
            switch (this.SqlMode)
            {
                case ESqlMode.ToDoList:
                case ESqlMode.Notify:
                    flNavMode = rowView["FLNAVIGATOR_MODE"].ToString();
                    navMode = rowView["NAVIGATOR_MODE"].ToString();
                    plusRoles = rowView["PLUSROLES"].ToString().Trim();
                    if (!string.IsNullOrEmpty(plusRoles))
                        flNavMode = "8";
                    dicFormParam.Add("FLOWPATH", rowView["FLOWPATH"].ToString());
                    dicFormParam.Add("SENDTOID", rowView["SENDTO_ID"].ToString());
                    dicFormParam.Add("ISIMPORTANT", rowView["FLOWIMPORTANT"].ToString());
                    dicFormParam.Add("ISURGENT", rowView["FLOWURGENT"].ToString());
                    dicFormParam.Add("PLUSAPPROVE", rowView["PLUSAPPROVE"].ToString());
                    dicFormParam.Add("STATUS", rowView["STATUS"].ToString().Trim().ToUpper());
                    dicFormParam.Add("MULTISTEPRETURN", rowView["MULTISTEPRETURN"].ToString());
                    break;
                case ESqlMode.ToDoHis:
                    dicFormParam.Add("FLOWPATH", rowView["FLOWPATH"].ToString());
                    flNavMode = "6";
                    navMode = "0";
                    break;
                case ESqlMode.Delay:
                case ESqlMode.FlowRunOver:
                    flNavMode = "6";
                    navMode = "0";
                    break;
            }
            dicFormParam.Add("FLNAVMODE", flNavMode);
            dicFormParam.Add("NAVMODE", navMode);
            dicFormParam.Add("ATTACHMENTS", rowView["ATTACHMENTS"].ToString());
            return dicFormParam;
        }
    }
}
