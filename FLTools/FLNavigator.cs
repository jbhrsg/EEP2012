using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using Srvtools;

namespace FLTools
{
    public class FLNavigator : InfoNavigator, ISupportInitialize, IGetValues, INavigatorSecurity
    {
        public FLNavigator()
        {
            _flActive = true;
            _states = new FLDataStateCollection(this, typeof(StateItem));
            _flStates = new FLNavigatorStateCollection(this, typeof(FLNavigatorStateItem));
        }

        private const int WM_ShowWindow = 0x0018;

        private string _listId = "";
        private string _flNavMode = "";
        private string _navMode = "";
        private string _flowFileName = "";
        private string _whereString = "";
        //private string _currentActivity = "";
        //private string _nextActivity = "";
        private string _flowPath = "";
        private bool _isImport = false;
        private bool _isUrgent = false;
        private string _plusApprove = "";
        private string _status = "";
        private bool _multiStepReturn = false;
        private string _sendToId = "";
        private string _attachments = "";

        bool LoadCompleted = false;
        #region ISupportInitialize
        void ISupportInitialize.BeginInit()
        {
            base.BeginInit();
        }

        void ISupportInitialize.EndInit()
        {
            this.OnBeforeEndInit();
            base.EndInit();
            this.OnAfterEndInit();
            LoadCompleted = true;
        }

        protected override void OnBeforeEndInit()
        {
            base.OnBeforeEndInit();
            if (this.GetServerText)
            {
                string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "Srvtools", "InfoNavigator", "NavText");
                string[] texts = message.Split(';');
                if (this.SubmitItem != null)
                    this.SubmitItem.Text = texts[16];
                if (this.ApproveItem != null)
                    this.ApproveItem.Text = texts[17];
                if (this.ReturnItem != null)
                    this.ReturnItem.Text = texts[18];
                if (this.RejectItem != null)
                    this.RejectItem.Text = texts[19];
                if (this.NotifyItem != null)
                    this.NotifyItem.Text = texts[20];
                if (this.FlowDeleteItem != null)
                    this.FlowDeleteItem.Text = texts[21];
                if (this.PlusItem != null)
                    this.PlusItem.Text = texts[22];
                if (this.PauseItem != null)
                    this.PauseItem.Text = texts[23];
                if (this.CommentItem != null)
                    this.CommentItem.Text = texts[24];
            }
        }

        protected override void OnAfterEndInit()
        {
            base.OnAfterEndInit();
            if (this.FLActive)
            {
                // FlowItems
                if (this.SubmitItem != null)
                {
                    this.SubmitItem.Click += new EventHandler(SubmitItem_Click);
                }
                if (this.ApproveItem != null)
                {
                    this.ApproveItem.Click += new EventHandler(ApproveItem_Click);
                }
                if (this.ReturnItem != null)
                {
                    this.ReturnItem.Click += new EventHandler(ReturnItem_Click);
                }
                if (this.RejectItem != null)
                {
                    this.RejectItem.Click += new EventHandler(RejectItem_Click);
                }
                if (this.NotifyItem != null)
                {
                    this.NotifyItem.Click += new EventHandler(NotifyItem_Click);
                }
                if (this.FlowDeleteItem != null)
                {
                    this.FlowDeleteItem.Click += new EventHandler(FlowDeleteItem_Click);
                }
                if (this.PlusItem != null)
                {
                    this.PlusItem.Click += new EventHandler(PlusItem_Click);
                }
                if (this.PauseItem != null)
                {
                    this.PauseItem.Click += new EventHandler(PauseItem_Click);
                }
                if (this.CommentItem != null)
                {
                    this.CommentItem.Click += new EventHandler(CommentItem_Click);
                }
            }
        }
        #endregion

        public override void InitializeStates()
        {
            #region States
            foreach (StateItem stateItem in this.States)
            {
                if (!stateItem.EnabledControlsEdited)
                {
                    if (stateItem.EnabledControls != null)
                    {
                        stateItem.EnabledControls.Clear();
                    }
                    switch (stateItem.StateText)
                    {
                        case "Initial":
                        case "Browsed":
                            foreach (ToolStripItem item in this.Items)
                            {
                                if (!(item is ToolStripSeparator) && !IsFlowNavigatorItem(item)
                                    && item.Name != null && item.Name.Trim() != "")
                                {
                                    if (this.OKItem != item
                                        && this.CancelItem != item
                                        && this.ApplyItem != item
                                        && this.AbortItem != item)
                                    {
                                        AddItemToList(stateItem.EnabledControls, item);
                                    }
                                }
                            }
                            break;
                        case "Inserting":
                        case "Editing":
                            AddItemToList(stateItem.EnabledControls, this.OKItem);
                            AddItemToList(stateItem.EnabledControls, this.CancelItem);
                            AddItemToList(stateItem.EnabledControls, this.ApplyItem);
                            AddItemToList(stateItem.EnabledControls, this.AbortItem);
                            break;
                        case "Changing":
                            foreach (ToolStripItem item in this.Items)
                            {
                                if (!(item is ToolStripSeparator) && !IsFlowNavigatorItem(item)
                                    && item.Name != null && item.Name.Trim() != "")
                                {
                                    if (this.OKItem != item
                                        && this.CancelItem != item
                                        && this.ViewRefreshItem != item
                                        && this.ViewQueryItem != item
                                        && this.PrintItem != item
                                        && this.ExportItem != item)
                                    {
                                        AddItemToList(stateItem.EnabledControls, item);
                                    }
                                }
                            }
                            break;
                        case "Insert":
                            AddItemToList(stateItem.EnabledControls, this.AddNewItem);
                            break;
                        case "Modify":
                            AddItemToList(stateItem.EnabledControls, this.EditItem);
                            AddItemToList(stateItem.EnabledControls, this.PrintItem);
                            break;
                        case "Inquery":
                            AddItemToList(stateItem.EnabledControls, this.ViewQueryItem);
                            break;
                        case "Prepare":
                            AddItemToList(stateItem.EnabledControls, this.EditItem);
                            AddItemToList(stateItem.EnabledControls, this.ViewQueryItem);
                            break;
                        case "Applying":
                        case "Querying":
                        case "Printing":
                        case "Normal":
                            break;
                    }
                }
            }
            #endregion

            if (this.FLActive)
            {
                InitializeFlowStates();
            }
        }

        public void InitializeFlowStates()
        {
            #region FLNavigatorStates
            foreach (FLNavigatorStateItem stateItem in this.FLStates)
            {
                if (!stateItem.VisibleControlsEdited)
                {
                    stateItem.VisibleControls.Clear();
                    switch (stateItem.StateText)
                    {
                        case "Submit":
                            AddItemToList(stateItem.VisibleControls, this.SubmitItem);
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                            AddItemToList(stateItem.VisibleControls, this.PauseItem);
                            break;
                        case "Approve":
                            AddItemToList(stateItem.VisibleControls, this.ApproveItem);
                            AddItemToList(stateItem.VisibleControls, this.ReturnItem);
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                            if(_plusApprove == "1")
                                AddItemToList(stateItem.VisibleControls, this.PlusItem);
                            AddItemToList(stateItem.VisibleControls, this.CommentItem);
                            break;
                        case "Return":
                            AddItemToList(stateItem.VisibleControls, this.SubmitItem);
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                            AddItemToList(stateItem.VisibleControls, this.RejectItem);
                            break;
                        case "Continue":
                            AddItemToList(stateItem.VisibleControls, this.SubmitItem);
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                           // AddItemToList(stateItem.VisibleControls, this.PlusItem);
                            AddItemToList(stateItem.VisibleControls, this.CommentItem);
                            break;
                        case "Inquery":
                            AddItemToList(stateItem.VisibleControls, this.CommentItem);
                            break;
                        case "None":
                            AddItemToList(stateItem.VisibleControls, this.CommentItem);
                            break;
                        case "Notify":
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                            AddItemToList(stateItem.VisibleControls, this.FlowDeleteItem);
                            AddItemToList(stateItem.VisibleControls, this.CommentItem);
                            break;
                        case "Plus":
                            AddItemToList(stateItem.VisibleControls, this.ApproveItem);
                            AddItemToList(stateItem.VisibleControls, this.ReturnItem);
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                            AddItemToList(stateItem.VisibleControls, this.CommentItem);
                            if (getParams("STATUS").Equals("AA"))
                            {
                                AddItemToList(stateItem.VisibleControls, this.PlusItem);
                            }
                            break;
                        case "Lock":
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                            break;
                        case "FSubmit":
                            AddItemToList(stateItem.VisibleControls, this.SubmitItem);
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                            AddItemToList(stateItem.VisibleControls, this.RejectItem);
                            break;
                        case "RSubmit":
                            AddItemToList(stateItem.VisibleControls, this.SubmitItem);
                            //AddItemToList(stateItem.VisibleControls, this.ReturnItem);
                            AddItemToList(stateItem.VisibleControls, this.NotifyItem);
                            AddItemToList(stateItem.VisibleControls, this.RejectItem);
                            AddItemToList(stateItem.VisibleControls, this.CommentItem);
                            break;
                    }
                }
            }
            #endregion
        }

        private bool IsFlowNavigatorItem(ToolStripItem item)
        {
            if (item != this.SubmitItem
                && item != this.ApproveItem
                && item != this.ReturnItem
                && item != this.RejectItem
                && item != this.NotifyItem
                && item != this.FlowDeleteItem
                && item != this.PlusItem
                && item != this.PauseItem
                && item != this.CommentItem)
            {
                return false;
            }
            return true;
        }

        private void setFLOperation(string fstateText, string stateText)
        {
            // Initial,Browsed,Inserting,Editing,Applying,Changing,Querying,Printing|||||Normal,Insert,Modify,Inquery,Prepare
            bool canFLOperation = !(stateText == "Inserting" || stateText == "Editing" || stateText == "Applying"
                || stateText == "Changing" || stateText == "Querying" || stateText == "Printing" // 原来的States
                //|| stateText == "Insert" || stateText == "Modify" || stateText == "Inquary" || stateText == "Prepare" // flow新加的States
                );

            foreach (FLNavigatorStateItem stateItem in this.FLStates)
            {
                if (stateItem.StateText == fstateText)
                {
                    foreach (ToolStripItem toolItem in this.Items)
                    {
                        if (stateItem.VisibleControls != null)
                        {
                            if (stateItem.VisibleControls.Contains(toolItem.Name))
                            {
                                toolItem.Enabled = canFLOperation;
                            }
                        }
                    }
                    break;
                }
            }
            //if (this.HideItemStates)
            //{
            //    this.SubmitItem.Enabled = !isInFlStates;
            //    this.ApproveItem.Enabled = !isInFlStates;
            //    this.ReturnItem.Enabled = !isInFlStates;
            //    this.RejectItem.Enabled = !isInFlStates;
            //    this.NotifyItem.Enabled = !isInFlStates;
            //    this.FlowDeleteItem.Enabled = !isInFlStates;
            //    this.PlusItem.Enabled = !isInFlStates;
            //    this.PauseItem.Enabled = !isInFlStates;
            //    this.CommentItem.Enabled = !isInFlStates;
            //}
            //else
            //{
            //    this.SubmitItem.Visible = !isInFlStates;
            //    this.ApproveItem.Visible = !isInFlStates;
            //    this.ReturnItem.Visible = !isInFlStates;
            //    this.RejectItem.Visible = !isInFlStates;
            //    this.NotifyItem.Visible = !isInFlStates;
            //    this.FlowDeleteItem.Visible = !isInFlStates;
            //    this.PlusItem.Visible = !isInFlStates;
            //    this.PauseItem.Visible = !isInFlStates;
            //    this.CommentItem.Visible = !isInFlStates;
            //}
        }

        protected override void OnLayoutCompleted(EventArgs e)
        {
            base.OnLayoutCompleted(e);
            if (LoadCompleted && !this.DesignMode)
            {
                this.FindForm().Load += new EventHandler(FLNavigator_Load);
                LoadCompleted = false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (!this.DesignMode && m.Msg == WM_ShowWindow)
            {
                this.SetState("Initial");
            }
            base.WndProc2(ref m);
        }

        void FLNavigator_Load(object sender, EventArgs e)
        {
            LoadNavigator();
        }

        public void LoadNavigator()
        {
            setParams();
            InitializeStates();
            if (this.FLActive)
            {
                if (this.BindingSource != null)
                {
                    object objDs = this.BindingSource.GetDataSource();
                    if (objDs != null && objDs is InfoDataSet)
                    {
                        if (_whereString != "")
                        {
                            string[] listWhere = _whereString.Split(';');
                          
                            string sql = DBUtils.GetCommandText(this.BindingSource);
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
                            ((InfoDataSet)objDs).SetWhere(strWhere);
                        }
                    }
                }
            }
            InitStates(_flNavMode, _navMode);
        }

        private void setParams()
        {
            //0:tree; 1:desinger; 2:todolist
            _listId = getParams("LISTID"); //2
            _flNavMode = getParams("FLNAVMODE");//1,2
            _navMode = getParams("NAVMODE");//1,2
            _flowFileName = getParams("FLOWFILENAME").ToUpper();//1
            if (!string.IsNullOrEmpty(_flowFileName) && _flowFileName.IndexOf(@"EEPNETSERVER\WORKFLOW\") != -1)
            {
                _flowFileName = _flowFileName.Substring(_flowFileName.IndexOf(@"EEPNETSERVER\WORKFLOW\") + 22);
                if (!string.IsNullOrEmpty(_flowFileName))
                {
                    object[] ret1 = CliUtils.CallMethod("GLModule", "GetServerPath", null);
                    if (ret1 != null && (int)ret1[0] == 0)
                        _flowFileName = (string)ret1[1] + "\\WorkFlow\\" + _flowFileName;
                }
            }
            _whereString = getParams("WHERESTRING");//2
            //_currentActivity = getParams("CURRENTACTIVITY");//2
            //_nextActivity = getParams("NEXTACTIVITY");//2
            if (getParams("ISIMPORTANT") == "1")
                _isImport = true;//2
            if (getParams("ISURGENT") == "1")
                _isUrgent = true;//2
            _flowPath = getParams("FLOWPATH");//2
            _plusApprove = getParams("PLUSAPPROVE"); //2
            _status = getParams("STATUS"); //2
            if (getParams("MULTISTEPRETURN") == "1")
                _multiStepReturn = true; //2
            _sendToId = getParams("SENDTOID"); //2
            _attachments = getParams("ATTACHMENTS"); //2

            if (_flowFileName != "" || _listId != "")
                return;

            if (string.IsNullOrEmpty(_flowFileName))
            {
                string name = getUserParam("FLOWFILENAME");//0
                if (!string.IsNullOrEmpty(name))
                {
                    object[] ret1 = CliUtils.CallMethod("GLModule", "GetServerPath", null);
                    if (ret1 != null && (int)ret1[0] == 0)
                        _flowFileName = (string)ret1[1] + "\\WorkFlow\\" + name;
                }
                else
                {
                    _flowFileName = name;
                }
            }
            if (string.IsNullOrEmpty(_flNavMode))
                _flNavMode = getUserParam("FLNAVMODE");//0
            if (string.IsNullOrEmpty(_navMode))
                _navMode = getUserParam("NAVMODE");//0
        }

        private bool isFlowConditionReqired()
        {
            if (string.IsNullOrEmpty(_listId) && string.IsNullOrEmpty(_flowFileName))
            {
                MessageBox.Show(SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowNotDefine"));
                return false;
            }
            return true;
        }

        private string getParams(string key)
        {
            string flow = "";
            Form form = this.FindForm();
            if (form != null && form is InfoForm)
            {
                StringDictionary paramters = ((InfoForm)form).fLItemParamters;
                if (paramters != null && paramters.ContainsKey(key))
                {
                    flow = paramters[key];
                }
            }
            return flow;
        }

        private string getUserParam(string paramName)
        {
            Form form = this.FindForm();
            if (form != null && form is InfoForm && ((InfoForm)form).ItemParamters != null)
            {
                string[] pas = ((InfoForm)form).ItemParamters.Split(';');
                foreach (string pa in pas)
                {
                    string[] paSplit = pa.Split('=');
                    if (pa.IndexOf(paramName) != -1 && paSplit[0] == paramName)
                    {
                        return paSplit[1];
                    }
                }
            }
            return "";
        }

        private void InitStates(string flNavigatorMode, string navigatorMode)
        {
            if (flNavigatorMode != "" && navigatorMode != "")
            {
                string flMode = "", mode = "";
                switch (flNavigatorMode)
                {
                    case "0":
                        flMode = "Submit";
                        if (this._listId != "")
                            flMode = "Approve";
                        if (this._status == "NF")
                            flMode = "FSubmit";
                        else if (this._status == "NR")
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
                }
                //if (this._status == "NF")
                //    flMode = "FSubmit";
                //else if (this._status == "NR")
                //    flMode = "RSubmit";
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
                    case "4": //Prepare初始化的时候当做
                        mode = "Inquery";
                        break;
                }
                if (mode != "")
                {
                    this.SetState(mode);
                }
            }
        }

        protected override void DoOK(object sender)
        {
            base.DoOK(sender);
            if (this.CurrentState == "Changing" || this.CurrentState == "Editing" || this.CurrentState == "Inserting")
                return;
            CHMode("ok");
        }

        protected override void DoCancel()
        {
            base.DoCancel();
            CHMode("cancel");
        }

        protected override void DoApply()
        {
            base.DoApply();
            if (this.CurrentState == "Changing" || this.CurrentState == "Editing" || this.CurrentState == "Inserting")
                return;
            CHMode("apply");
            if (this.AutoSubmit)
            {
                if (!string.IsNullOrEmpty(this.CurrentFLState) && this.CurrentFLState.Equals("Approve"))
                {
                    if (Approve()) remData(true);
                }
                else
                {
                    if (Submit()) remData(true);
                }
            }
        }

        protected override void DoAbort()
        {
            base.DoAbort();
            CHMode("abort");
        }

        private void CHMode(string callInMethod)
        {
            string mode = _navMode;
            switch (mode)
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
            }
            if (mode != "")
            {
                this.SetState(mode);
                if (this.BindingSource.Count > 0)
                {
                    object obj = this.BindingSource.GetDataSource(true);
                    if (obj != null && obj is DataSet)
                    {
                        DataSet ds = obj as DataSet;
                        if (!ds.HasChanges())
                        {
                            if (mode == "Insert")
                            {
                                this.DeleteItem.Enabled = true;
                            }
                            this.EditItem.Enabled = true;
                        }
                    }
                    this.PrintItem.Enabled = true;
                }
            }
        }

        private string GloWhereString = "";

        private void remData(bool remAll)
        {
            object obj = this.BindingSource.GetDataSource();
            object objRow = this.BindingSource.Current;
            if (obj != null && obj is InfoDataSet)
            {
                InfoDataSet ds = obj as InfoDataSet;
                string ws = "";
                if (remAll)
                {
                    ds.SetWhere("1=0");
                }
                else
                {
                    if (objRow != null && objRow is DataRowView)
                    {
                        string sql = DBUtils.GetCommandText(this.BindingSource);
                        DataRowView rowView = objRow as DataRowView;
                        foreach (string key in ds.GetKeyFields())
                        {
                            if (GloFix.IsNumeric(rowView.DataView.Table.Columns[key].DataType))
                            {
                                ws += CliUtils.GetTableNameForColumn(sql, key) +"<>" + rowView[key] + " and ";
                            }
                            else
                            {
                                ws += CliUtils.GetTableNameForColumn(sql, key) + "<>'" + rowView[key] + "' and ";
                            }
                        }
                        if (ws != "")
                        {
                            GloWhereString = ws + GloWhereString;
                            if(GloWhereString.EndsWith(" and "))
                                GloWhereString = GloWhereString.Substring(0, GloWhereString.LastIndexOf(" and "));
                        }
                    }
                    ds.SetWhere(GloWhereString);
                }
            }
        }

        #region Click
        void SubmitItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("Submit");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                if (Submit()) remData(true);
                OnAfterItemClick(new AfterItemClickEventArgs("Submit"));
            }
        }

        void ApproveItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("Approve");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                if (Approve()) remData(true);
                OnAfterItemClick(new AfterItemClickEventArgs("Approve"));
            }
        }

        void ReturnItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("Return");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                if (Return()) remData(true);
                OnAfterItemClick(new AfterItemClickEventArgs("Return"));
            }
        }

        void RejectItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("Reject");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowRejectConfirm");
                if (MessageBox.Show(message, "reject work flow...", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (Reject()) remData(true);
                    OnAfterItemClick(new AfterItemClickEventArgs("Reject"));
                }
            }
        }

        void NotifyItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("Notify");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                if (!Notify())
                    MessageBox.Show("notify failed!");
                OnAfterItemClick(new AfterItemClickEventArgs("Notify"));
            }
        }

        void FlowDeleteItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("FlowDelete");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                FlowDelete();
                remData(true);
                OnAfterItemClick(new AfterItemClickEventArgs("FlowDelete"));
            }
        }

        void PlusItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("Plus");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                if (Plus()) remData(true);
                OnAfterItemClick(new AfterItemClickEventArgs("Plus"));
            }
        }

        void PauseItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("Pause");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                if (Pause()) remData(true);
                OnAfterItemClick(new AfterItemClickEventArgs("Pause"));
            }
        }

        void CommentItem_Click(object sender, EventArgs e)
        {
            if (!isFlowConditionReqired()) return;
            if (!hasApplyOrAbort()) return;
            BeforeItemClickEventArgs argsbeforeclick = new BeforeItemClickEventArgs("Comment");
            OnBeforeItemClick(argsbeforeclick);
            if (!argsbeforeclick.Cancel)
            {
                Comment();
                OnAfterItemClick(new AfterItemClickEventArgs("Comment"));
            }
        }

        private bool hasApplyOrAbort()
        {
            object obj = this.BindingSource.GetDataSource(true);
            if (obj != null && obj is DataSet)
            {
                DataSet ds = obj as DataSet;
                string cstate = this.GetCurrentState();
                if (ds.GetChanges() != null || (cstate != "Initial" && cstate != "Browsed" && !GloFix.IsFlowState(cstate)))
                {
                    MessageBox.Show(SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "ApplyFirst"));
                    return false;
                }
            }
            return true;
        }

        public bool Submit()
        {
            return GloOperate(FLNavigatorOperate.Submit);
        }

        public bool Approve()
        {
            return GloOperate(FLNavigatorOperate.Approve);
        }

        public bool Return()
        {
            return GloOperate(FLNavigatorOperate.Return);
        }

        public bool Reject()
        {
            if (string.IsNullOrEmpty(_listId))
            {
                string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "IsListIdNull", true);
                MessageBox.Show(message);
                return false;
            }
            string provider = "", keys = "", values = "";
            object dataSource = this.BindingSource.GetDataSource();
            if (dataSource != null && dataSource is InfoDataSet)
            {
                InfoDataSet ds = (InfoDataSet)dataSource;
                provider = ds.RemoteName;
                object objCurrent = this.BindingSource.Current;
                if (objCurrent != null && objCurrent is DataRowView)
                {
                    ArrayList lstKeys = ds.GetKeyFields();
                    if (lstKeys.Count > 0)
                    {
                        DataRowView rowView = (DataRowView)objCurrent;
                        foreach (string key in lstKeys)
                        {
                            keys += key + ";";
                            if (GloFix.IsNumeric(rowView[key].GetType()))
                            {
                                values += key + " = " + rowView[key].ToString() + ";";
                            }
                            else
                            {
                                values += key + " = ''" + rowView[key].ToString() + "'';";
                            }
                        }
                        if (keys != "")
                        {
                            keys = keys.Substring(0, keys.LastIndexOf(';'));
                            values = values.Substring(0, values.LastIndexOf(';'));
                        }
                    }
                }
                else
                {
                    string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", true);
                    MessageBox.Show(selDataMessage);
                    return false;
                }
            }
            else
                return false;
            string[] activities = _flowPath.Split(';');
            object[] objParams = CliUtils.CallFLMethod("Reject", new object[] { new Guid(_listId), new object[] { activities[0], activities[1], this.FLRejectNotify ? "1" : "0", provider }, new object[] { keys, values } });
            if (Convert.ToInt16(objParams[0]) == 0)
            {
                object objSource = this.BindingSource.GetDataSource();
                if (objSource != null && objSource is InfoDataSet)
                {
                    InfoDataSet ds = objSource as InfoDataSet;
                    ds.SetWhere("1=0");
                }
                return true;
            }
            else
                return false;
        }

        public bool Notify()
        {
            if (string.IsNullOrEmpty(_listId)) return false;
            NotifyForm frmNotify = new NotifyForm();
            string keys = "", values = "";
            frmNotify.flowPath = _flowPath;
            frmNotify.listId = _listId;
            if (this.FLNotifySecControl)
                SetSecUsersAndRoles(frmNotify);
            object dataSource = this.BindingSource.GetDataSource();
            if (dataSource != null && dataSource is InfoDataSet)
            {
                InfoDataSet ds = (InfoDataSet)dataSource;
                frmNotify.provider = ds.RemoteName;
                object objCurrent = this.BindingSource.Current;
                if (objCurrent != null && objCurrent is DataRowView)
                {
                    ArrayList lstKeys = ds.GetKeyFields();
                    if (lstKeys.Count > 0)
                    {
                        DataRowView rowView = (DataRowView)objCurrent;
                        foreach (string key in lstKeys)
                        {
                            keys += key + ";";
                            if (GloFix.IsNumeric(rowView[key].GetType()))
                            {
                                values += key + " = " + rowView[key].ToString() + ";";
                            }
                            else
                            {
                                values += key + " = ''" + rowView[key].ToString() + "'';";
                            }
                        }
                        if (keys != "")
                        {
                            frmNotify.keys = keys.Substring(0, keys.LastIndexOf(';'));
                            frmNotify.values = values.Substring(0, values.LastIndexOf(';'));
                        }
                    }
                }
                else
                {
                    string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", true);
                    MessageBox.Show(selDataMessage);
                    return false;
                }
            }
            bool b = frmNotify.ShowDialog() == DialogResult.OK;
            return b;
        }

        public void FlowDelete()
        {
            object[] objParams = CliUtils.CallFLMethod("DeleteNotify", new object[] { this._listId, this._flowPath });
            if (Convert.ToInt16(objParams[0]) == 0)
            {
                string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "FlowDeleted");
                MessageBox.Show(message);
            }
            else if (Convert.ToInt16(objParams[0]) == 2)
            {
                MessageBox.Show(objParams[1].ToString());
            }
        }

        public bool Plus()
        {
            PlusForm frmPlus = new PlusForm();
            string keys = "", values = "";
            frmPlus.flowPath = _flowPath;
            frmPlus.listId = _listId;
            frmPlus.sendToId = _sendToId;
            frmPlus.isImportant = _isImport ? 1 : 0;
            frmPlus.isUrgent = _isUrgent ? 1 : 0;
            frmPlus.attachments = this._attachments;
            if (this.FLNotifySecControl)
                SetSecUsersAndRoles(frmPlus);
            object dataSource = this.BindingSource.GetDataSource();
            if (dataSource != null && dataSource is InfoDataSet)
            {
                InfoDataSet ds = (InfoDataSet)dataSource;
                frmPlus.provider = ds.RemoteName;
                object objCurrent = this.BindingSource.Current;
                if (objCurrent != null && objCurrent is DataRowView)
                {
                    ArrayList lstKeys = ds.GetKeyFields();
                    if (lstKeys.Count > 0)
                    {
                        DataRowView rowView = (DataRowView)objCurrent;
                        foreach (string key in lstKeys)
                        {
                            keys += key + ";";
                            if (GloFix.IsNumeric(rowView[key].GetType()))
                            {
                                values += key + " = " + rowView[key].ToString() + ";";
                            }
                            else
                            {
                                values += key + " = ''" + rowView[key].ToString() + "'';";
                            }
                        }
                        if (keys != "")
                        {
                            frmPlus.keys = keys.Substring(0, keys.LastIndexOf(';'));
                            frmPlus.values = values.Substring(0, values.LastIndexOf(';'));
                        }
                    }
                }
                else
                {
                    string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", true);
                    MessageBox.Show(selDataMessage);
                    return false;
                }
            }
            bool b = frmPlus.ShowDialog() == DialogResult.OK;
            return b;
        }

        public bool Pause()
        {
            bool sucess = false;
            string provider = "", keys = "", values = "";
            object dataSource = this.BindingSource.GetDataSource();
            if (dataSource != null && dataSource is InfoDataSet)
            {
                InfoDataSet ds = (InfoDataSet)dataSource;
                provider = ds.RemoteName;
                object objCurrent = this.BindingSource.Current;
                if (objCurrent != null && objCurrent is DataRowView)
                {
                    ArrayList lstKeys = ds.GetKeyFields();
                    if (lstKeys.Count > 0)
                    {
                        DataRowView rowView = (DataRowView)objCurrent;
                        foreach (string key in lstKeys)
                        {
                            keys += key + ";";
                            if (GloFix.IsNumeric(rowView[key].GetType()))
                            {
                                values += key + " = " + rowView[key].ToString() + ";";
                            }
                            else
                            {
                                values += key + " = ''" + rowView[key].ToString() + "'';";
                            }
                        }
                        if (keys != "")
                        {
                            keys = keys.Substring(0, keys.LastIndexOf(';'));
                            values = values.Substring(0, values.LastIndexOf(';'));
                        }
                    }
                }
                else
                {
                    string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", false);
                    MessageBox.Show(selDataMessage);
                    return false;
                }
            }
            else
                return false;
            string orgKind = "";
            if (this.OrganizationControl)
            {
                PauseForm frmPause = new PauseForm();
                if (DialogResult.OK == frmPause.ShowDialog())
                {
                    orgKind = frmPause.OrgKind;
                }
                else
                    return false;
            }
            object[] objParams = CliUtils.CallFLMethod("Pause", new object[] { null, new object[] { _flowFileName + ".xoml", "", 0, 0, "", "", provider, 0, orgKind, "" }, new object[] { keys, values } });
            if (Convert.ToInt16(objParams[0]) == 0)
            {
                sucess = true;
                MessageBox.Show(SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "PauseSucceed", false));
            }
            else
            {
                sucess = false;
                MessageBox.Show(objParams[1].ToString());
            }
            return sucess;
        }

        public void Comment()
        {
            Comment frmComment = new Comment();
            frmComment.listId = _listId;
            string[] attachments = _attachments.Split(';');
            List<string> lstAttachments = new List<string>();
            foreach (string attach in attachments)
            {
                if (attach != "")
                {
                    lstAttachments.Add(attach);
                }
            }
            frmComment.attachments = lstAttachments;
            frmComment.ShowDialog();
        }


        private string remarkField;

        public string RemarkField
        {
            get { return remarkField; }
            set { remarkField = value; }
        }

        private bool GloOperate(FLNavigatorOperate operate)
        {
            if ((_status == "NF" || _status == "NR") && operate == FLNavigatorOperate.Submit)
                operate = FLNavigatorOperate.Approve;
            bool sucess = false;
            string keys = "", values = "";
            SubmitConfirm frmSubmitConfirm = new SubmitConfirm();
            frmSubmitConfirm.listId = _listId;
            frmSubmitConfirm.flowFileName = _flowFileName;
            frmSubmitConfirm.flowPath = _flowPath;
            frmSubmitConfirm.operate = operate;
            frmSubmitConfirm.currentFLState = CurrentFLState;
            frmSubmitConfirm.isFlowImportant = _isImport;
            frmSubmitConfirm.isFlowUrgent = _isUrgent;
            frmSubmitConfirm.status = _status;
            frmSubmitConfirm.sendToId = _sendToId;
            frmSubmitConfirm.organizationControl = _organizationControl;
            frmSubmitConfirm.multiStepReturn = _multiStepReturn;
            if (!string.IsNullOrEmpty(RemarkField) && this.BindingSource.Current != null && this.BindingSource.Current is DataRowView)
            {
                frmSubmitConfirm.txtSuggest.Text = (this.BindingSource.Current as DataRowView)[RemarkField].ToString();
            }

            if (!string.IsNullOrEmpty(_attachments))
            {
                string[] attachments = _attachments.Split(';');
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

            object dataSource = this.BindingSource.GetDataSource();
            if (dataSource != null && dataSource is InfoDataSet)
            {
                InfoDataSet ds = (InfoDataSet)dataSource;
                frmSubmitConfirm.provider = ds.RemoteName;
                object objCurrent = this.BindingSource.Current;
              
                if (objCurrent != null && objCurrent is DataRowView)
                {
                    ArrayList lstKeys = ds.GetKeyFields();
                    if (lstKeys.Count > 0)
                    {
                        DataRowView rowView = (DataRowView)objCurrent;
                        DataSet host = ds.RealDataSet.Clone();
                        host.Tables[0].Rows.Add(rowView.Row.ItemArray);
                        frmSubmitConfirm.host = host;
                        foreach (string key in lstKeys)
                        {
                            keys += key + ";";
                            if (GloFix.IsNumeric(rowView[key].GetType()))
                            {
                                values += key + "=" + rowView[key].ToString() + ";";
                            }
                            else
                            {
                                values += key + "=''" + rowView[key].ToString() + "'';";
                            }
                        }
                        if (keys != "")
                        {
                            frmSubmitConfirm.keys = keys.Substring(0, keys.LastIndexOf(';'));
                            frmSubmitConfirm.values = values.Substring(0, values.LastIndexOf(';'));
                        }
                    }
                }
                else
                {
                    string selDataMessage = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "SelectData", false);
                    MessageBox.Show(selDataMessage);
                    return false;
                }
            }
            if (frmSubmitConfirm.ShowDialog() == DialogResult.OK)
            {
                _flowPath = frmSubmitConfirm.flowPath;
                _listId = frmSubmitConfirm.listId;
                Form parentForm = this.FindForm().ParentForm;
                if (parentForm != null)
                {
                    RefreshWizard(parentForm, "wizToDoList");
                    RefreshWizard(parentForm, "wizToDoHis");
                    RefreshWizard(parentForm, "wizNotify");
                }
                sucess = true;
                if (CurrentState == "Insert")
                {
                    CurrentStateItem = null;
                    this.SetState("Insert");
                }
                else if (CurrentState == "Modify")
                {
                    CurrentStateItem = null;
                    this.SetState("Normal");
                }
                else if (CurrentState == "Prepare")
                {
                    CurrentStateItem = null;
                    this.SetState("Inquery");
                }
                else if (CurrentState == "Inquery")
                {
                    CurrentStateItem = null;
                    this.SetState("Inquery");
                }
            }
            else
            {
                sucess = false;
            }
            return sucess;
        }
        #endregion

        #region Properties
        /*private bool _multiStepReturn;
        [Category("Infolight")]
        public bool MultiStepReturn
        {
            get
            {
                return _multiStepReturn;
            }
            set
            {
                _multiStepReturn = value;
            }
        }*/

        private bool _flRejectNotify = false;
        [Category("Infolight")]
        [DefaultValue(false)]
        public bool FLRejectNotify
        {
            get
            {
                return _flRejectNotify;
            }
            set
            {
                _flRejectNotify = value;
            }
        }

        private bool _autoSubmit = false;
        [Category("Infolight")]
        [DefaultValue(false)]
        public bool AutoSubmit
        {
            get
            {
                return _autoSubmit;
            }
            set
            {
                _autoSubmit = value;
            }
        }

        private string _dbAlias;
        [Category("Infolight")]
        [Editor(typeof(GetAlias), typeof(System.Drawing.Design.UITypeEditor))]
        public string DBAlias
        {
            get
            {
                return _dbAlias;
            }
            set
            {
                _dbAlias = value;
            }
        }

        private string _menuId;
        [Category("Infolight")]
        [Editor(typeof(GetMenuID), typeof(System.Drawing.Design.UITypeEditor))]
        public string MenuId
        {
            get
            {
                return _menuId;
            }
            set
            {
                _menuId = value;
            }
        }

        private bool _flActive;
        [Category("Infolight")]
        public bool FLActive
        {
            get
            {
                return _flActive;
            }
            set
            {
                _flActive = value;
            }
        }

        private FLDataStateCollection _states;
        [Category("Infolight")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new FLDataStateCollection States
        {
            get
            {
                return _states;
            }
            set
            {
                _states = value;
            }
        }

        private FLNavigatorStateCollection _flStates;
        [Category("Infolight")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FLNavigatorStateCollection FLStates
        {
            get
            {
                return _flStates;
            }
            set
            {
                _flStates = value;
            }
        }

        private ToolStripButton _submitItem;
        [Category("FlowItems")]
        public ToolStripButton SubmitItem
        {
            get
            {
                return _submitItem;
            }
            set
            {
                _submitItem = value;
            }
        }

        private ToolStripButton _approveItem;
        [Category("FlowItems")]
        public ToolStripButton ApproveItem
        {
            get
            {
                return _approveItem;
            }
            set
            {
                _approveItem = value;
            }
        }

        private ToolStripButton _returnItem;
        [Category("FlowItems")]
        public ToolStripButton ReturnItem
        {
            get
            {
                return _returnItem;
            }
            set
            {
                _returnItem = value;
            }
        }

        private ToolStripButton _rejectItem;
        [Category("FlowItems")]
        public ToolStripButton RejectItem
        {
            get
            {
                return _rejectItem;
            }
            set
            {
                _rejectItem = value;
            }
        }

        private ToolStripButton _notifyItem;
        [Category("FlowItems")]
        public ToolStripButton NotifyItem
        {
            get
            {
                return _notifyItem;
            }
            set
            {
                _notifyItem = value;
            }
        }

        private ToolStripButton _flowDeleteItem;
        [Category("FlowItems")]
        public ToolStripButton FlowDeleteItem
        {
            get
            {
                return _flowDeleteItem;
            }
            set
            {
                _flowDeleteItem = value;
            }
        }

        private ToolStripButton _plusItem;
        [Category("FlowItems")]
        public ToolStripButton PlusItem
        {
            get
            {
                return _plusItem;
            }
            set
            {
                _plusItem = value;
            }
        }

        private ToolStripButton _pauseItem;
        [Category("FlowItems")]
        public ToolStripButton PauseItem
        {
            get
            {
                return _pauseItem;
            }
            set
            {
                _pauseItem = value;
            }
        }

        private ToolStripButton _commentItem;
        [Category("FlowItems")]
        public ToolStripButton CommentItem
        {
            get
            {
                return _commentItem;
            }
            set
            {
                _commentItem = value;
            }
        }

        private string _flowStepField;
        [Category("FlowData")]
        [Editor(typeof(FieldNameEditor), typeof(UITypeEditor))]
        public string FlowStepField
        {
            get
            {
                return _flowStepField;
            }
            set
            {
                _flowStepField = value;
            }
        }

        private InfoBindingSource _flowBindingSource;
        [Category("FlowData")]
        public InfoBindingSource FlowBindingSource
        {
            get
            {
                return _flowBindingSource;
            }
            set
            {
                _flowBindingSource = value;
            }
        }

        private bool _important;
        [Category("Infolight")]
        public bool Important
        {
            get
            {
                return _important;
            }
            set
            {
                _important = value;
            }
        }

        private bool _urgent;
        [Category("Infolight")]
        public bool Urgent
        {
            get
            {
                return _urgent;
            }
            set
            {
                _urgent = value;
            }
        }

        private bool _applySubmit;
        [Category("Infolight")]
        public bool ApplySubmit
        {
            get
            {
                return _applySubmit;
            }
            set
            {
                _applySubmit = value;
            }
        }

        private string _flowID;
        [Category("Infolight")]
        public string FlowID
        {
            get
            {
                return _flowID;
            }
            set
            {
                _flowID = value;
            }
        }

        private string _stepID;
        [Category("Infolight")]
        public string StepID
        {
            get
            {
                return _stepID;
            }
            set
            {
                _stepID = value;
            }
        }

        private InfoStatusStrip _refStatusStrip;
        [Category("Infolight")]
        public InfoStatusStrip RefStatusStrip
        {
            get
            {
                return _refStatusStrip;
            }
            set
            {
                _refStatusStrip = value;
            }
        }

        private bool _flNotifyOff;
        [Category("Infolight")]
        public bool FLNotifyOff
        {
            get
            {
                return _flNotifyOff;
            }
            set
            {
                _flNotifyOff = value;
            }
        }

        private bool _flNotifySecControl;
        [Category("Infolight")]
        public bool FLNotifySecControl
        {
            get
            {
                return _flNotifySecControl;
            }
            set
            {
                _flNotifySecControl = value;
            }
        }

        private string _currentFLState;
        [Browsable(false)]
        public string CurrentFLState
        {
            get { return _currentFLState; }
        }

        private bool _organizationControl = false;
        [Category("Infolight")]
        public bool OrganizationControl
        {
            get
            {
                return _organizationControl;
            }
            set
            {
                _organizationControl = value;
            }
        }


        #endregion

        public void RefreshWizard(Form parentForm, string wizName)
        {
            Type type = parentForm.GetType();
            FieldInfo wizInfo = type.GetField(wizName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (wizInfo != null)
            {
                FLWizard wiz = wizInfo.GetValue(parentForm) as FLWizard;
                if (wiz != null)
                {
                    wiz.Refresh();
                }
            }
        }

        public new string[] GetValues(string sKind)
        {
            string[] retList = null;
            List<string> values = new List<string>();
            if (sKind.ToLower().Equals("flowstepfield"))
            {
                DataTable table = this.GetBindingTable();
                if (table != null)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        values.Add(column.ColumnName);
                    }
                }
            }
            if (values.Count > 0)
            {
                retList = values.ToArray();
            }
            return retList;
        }

        protected virtual DataTable GetBindingTable()
        {
            if (this.FlowBindingSource == null)
                return null;
            object obj = this.FlowBindingSource.GetDataSource();
            string tabName = this.FlowBindingSource.GetTableName();
            if (obj != null && obj is InfoDataSet)
            {
                InfoDataSet infoDs = (InfoDataSet)obj;
                return infoDs.RealDataSet.Tables[tabName];
            }
            return null;
        }

        public override void AddStandardItems()
        {
            base.AddStandardItems();
            if (this.FLActive)
                AddFlowStandardItems();
        }

        internal void AddFlowStandardItems()
        {
            AddFlowStandardItems(false);
        }

        internal void AddFlowStandardItems(bool isConverter)
        {
            Assembly assembly = this.GetType().Assembly;
            string resDir = this.ResourceDir();
            ToolStripSeparator separator1 = new ToolStripSeparator();
            separator1.Name = "separator1";
            #region FlowItem
            // submit
            Bitmap bitmapSubmit = new Bitmap(assembly.GetManifestResourceStream(resDir + "Submit.png"));
            bitmapSubmit.MakeTransparent(Color.Magenta);
            this.SubmitItem = new ToolStripButton();
            this.SubmitItem.Name = "toolStripSubmitItem";
            this.SubmitItem.Text = "submit";
            this.SubmitItem.Image = bitmapSubmit;
            this.SubmitItem.DisplayStyle = ToolStripItemDisplayStyle.Image;

            // approve
            Bitmap bitmapApprove = new Bitmap(assembly.GetManifestResourceStream(resDir + "Approve.png"));
            bitmapApprove.MakeTransparent(Color.Magenta);
            this.ApproveItem = new ToolStripButton();
            this.ApproveItem.Name = "toolStripApproveItem";
            this.ApproveItem.Text = "approve";
            this.ApproveItem.Image = bitmapApprove;
            this.ApproveItem.DisplayStyle = ToolStripItemDisplayStyle.Image;

            // return
            Bitmap bitmapReturn = new Bitmap(assembly.GetManifestResourceStream(resDir + "Return.png"));
            bitmapReturn.MakeTransparent(Color.Magenta);
            this.ReturnItem = new ToolStripButton();
            this.ReturnItem.Name = "toolStripReturnItem";
            this.ReturnItem.Text = "return";
            this.ReturnItem.Image = bitmapReturn;
            this.ReturnItem.DisplayStyle = ToolStripItemDisplayStyle.Image;

            // reject
            Bitmap bitmapReject = new Bitmap(assembly.GetManifestResourceStream(resDir + "Reject.png"));
            bitmapReject.MakeTransparent(Color.Magenta);
            this.RejectItem = new ToolStripButton();
            this.RejectItem.Name = "toolStripRejectItem";
            this.RejectItem.Text = "reject";
            this.RejectItem.Image = bitmapReject;
            this.RejectItem.DisplayStyle = ToolStripItemDisplayStyle.Image;

            // notify
            Bitmap bitmapNotify = new Bitmap(assembly.GetManifestResourceStream(resDir + "Notify.png"));
            bitmapNotify.MakeTransparent(Color.Magenta);
            this.NotifyItem = new ToolStripButton();
            this.NotifyItem.Name = "toolStripNotifyItem";
            this.NotifyItem.Text = "notify";
            this.NotifyItem.Image = bitmapNotify;
            this.NotifyItem.DisplayStyle = ToolStripItemDisplayStyle.Image;

            // flowdelete
            Bitmap bitmapFlowDelete = new Bitmap(assembly.GetManifestResourceStream(resDir + "FlowDelete.png"));
            bitmapFlowDelete.MakeTransparent(Color.Magenta);
            this.FlowDeleteItem = new ToolStripButton();
            this.FlowDeleteItem.Name = "toolStripFlowDeleteItem";
            this.FlowDeleteItem.Text = "delete";
            this.FlowDeleteItem.Image = bitmapFlowDelete;
            this.FlowDeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;

            //plus
            Bitmap bitmapPlus = new Bitmap(assembly.GetManifestResourceStream(resDir + "Plus.png"));
            bitmapPlus.MakeTransparent(Color.Magenta);
            this.PlusItem = new ToolStripButton();
            this.PlusItem.Name = "toolStripPlusItem";
            this.PlusItem.Text = "plus";
            this.PlusItem.Image = bitmapPlus;
            this.PlusItem.DisplayStyle = ToolStripItemDisplayStyle.Image;

            //pause
            Bitmap bitmapPause = new Bitmap(assembly.GetManifestResourceStream(resDir + "Pause.png"));
            bitmapPause.MakeTransparent(Color.Magenta);
            this.PauseItem = new ToolStripButton();
            this.PauseItem.Name = "toolStripPauseItem";
            this.PauseItem.Text = "pause";
            this.PauseItem.Image = bitmapPause;
            this.PauseItem.DisplayStyle = ToolStripItemDisplayStyle.Image;

            //comment
            Bitmap bitmapComment = new Bitmap(assembly.GetManifestResourceStream(resDir + "Comment.png"));
            bitmapComment.MakeTransparent(Color.Magenta);
            this.CommentItem = new ToolStripButton();
            this.CommentItem.Name = "toolStripCommentItem";
            this.CommentItem.Text = "comment";
            this.CommentItem.Image = bitmapComment;
            this.CommentItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            #endregion

            ToolStripItem[] itemArray = new ToolStripItem[10] { 
                separator1,
                this.SubmitItem, 
                this.ApproveItem,
                this.ReturnItem,
                this.NotifyItem,
                this.RejectItem,
                this.FlowDeleteItem,
                this.PlusItem,
                this.PauseItem,
                this.CommentItem};
            this.Items.AddRange(itemArray);
        }

        protected override string ResourceDir()
        {
            return "FLTools.FLNavigator.";
        }

        public void SetFLState(string flStateText)
        {
            if (this.FLActive)
            {
                InnerSetFLState(flStateText);
                _currentFLState = flStateText;
            }
            else
            {
                InnerSetFLState("None");
                _currentFLState = "None";
            }

            if (this.FLNotifyOff)
            {
                this.NotifyItem.Visible = false;
            }
        }

        protected FLNavigatorStateItem PreviousFLStateItem = null;
        protected FLNavigatorStateItem CurrentFLStateItem = null;
        protected virtual void InnerSetFLState(string flStateText)
        {
            foreach (FLNavigatorStateItem stateItem in this.FLStates)
            {
                if (stateItem.StateText == flStateText)
                {
                    PreviousFLStateItem = CurrentFLStateItem;
                    CurrentFLStateItem = stateItem;
                    if ((PreviousFLStateItem == null) || (PreviousFLStateItem != CurrentFLStateItem))
                    {
                        foreach (ToolStripItem toolItem in this.Items)
                        {
                            if (stateItem.VisibleControls != null)
                            {
                                if (stateItem.VisibleControls.Contains(toolItem.Name))
                                {
                                    toolItem.Visible = true;
                                }
                                else if (this.IsFlowNavigatorItem(toolItem))
                                {
                                    toolItem.Visible = false;
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }

        public string GetCurrentFLState()
        {
            if (this.FLActive)
                return "";
            return CurrentFLState;
        }

        protected override void InnerSetState(string stateText, bool raiseStateChangedEvent)
        {
            foreach (StateItem stateItem in States)
            {
                if (stateItem.StateText == stateText)
                {
                    if (this.StatusStrip != null && this.StatusStrip.ShowNavigatorStatus == true)
                    {
                        string strState = "";
                        if (this.GetServerText)
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "Srvtools", "InfoNavigator", "NavStates");
                            string[] states = message.Split(';');
                            foreach (StateItem item in States)
                            {
                                switch (item.StateText)
                                {
                                    case "Initial":
                                        item.Description = states[0];
                                        break;
                                    case "Browsed":
                                        item.Description = states[1];
                                        break;
                                    case "Inserting":
                                        item.Description = states[2];
                                        break;
                                    case "Editing":
                                        item.Description = states[3];
                                        break;
                                    case "Applying":
                                        item.Description = states[4];
                                        break;
                                    case "Changing":
                                        item.Description = states[5];
                                        break;
                                    case "Querying":
                                        item.Description = states[6];
                                        break;
                                    case "Printing":
                                        item.Description = states[7];
                                        break;
                                }
                            }
                        }
                        foreach (StateItem item in States)
                        {
                            if (item.StateText == stateText)
                            {
                                if (item.Description != null)
                                {
                                    strState = item.Description;
                                }
                                else
                                {
                                    strState = item.StateText;
                                }
                                break;
                            }
                        }
                        StatusStrip.SetNavigatorStatus(strState);
                    }

                    PrevousStateItem = CurrentStateItem;
                    CurrentStateItem = stateItem;
                    if ((PrevousStateItem == null) || (PrevousStateItem != CurrentStateItem))
                    {
                        foreach (ToolStripItem toolItem in this.Items)
                        {
                            if (stateItem.EnabledControls != null)
                            {
                                if (stateItem.EnabledControls.Contains(toolItem.Name))
                                {
                                    if (!this.HideItemStates)
                                        toolItem.Enabled = true;
                                    else
                                        toolItem.Visible = true;
                                }
                                else
                                {
                                    if (!this.IsFlowNavigatorItem(toolItem))
                                    {
                                        if (!this.HideItemStates)
                                            toolItem.Enabled = false;
                                        else
                                            toolItem.Visible = false;
                                    }
                                    else
                                    {
                                        //setFLOperation(this.CurrentFLState, stateText);
                                    }
                                }
                            }
                        }
                    }
                    if (stateText == "Browsed" || stateText == "Initial")//没有资料时将EditItem disable
                    {
                        if (ViewBindingSource != null)
                        {
                            if (ViewBindingSource.Count == 0)
                            {
                                if (this.EditItem != null)
                                {
                                    if (!this.HideItemStates)
                                        EditItem.Enabled = false;
                                    else
                                        EditItem.Visible = false;
                                }
                                if (this.DeleteItem != null)
                                {
                                    if (!this.HideItemStates)
                                        DeleteItem.Enabled = false;
                                    else
                                        DeleteItem.Visible = false;
                                }
                            }
                        }
                        else if (ViewBindingSource == null)
                        {
                            if (BindingSource != null)
                            {
                                if (BindingSource.Count == 0)
                                {
                                    if (this.EditItem != null)
                                    {
                                        if (!this.HideItemStates)
                                            EditItem.Enabled = false;
                                        else
                                            EditItem.Visible = false;
                                    }
                                    if (this.DeleteItem != null)
                                    {
                                        if (!this.HideItemStates)
                                            DeleteItem.Enabled = false;
                                        else
                                            DeleteItem.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                    if (this.DescriptionItem != null)
                    {
                        this.DescriptionItem.Text = stateItem.Description;
                    }
                    //added by lily 2009-4-29 ，配合4-21日改的程式（否則在StateChanged事件讀取CurrentState會是舊的狀態 ）
                    _CurrentState = stateText;
                    
                    // Raise StateChanged Event
                    if (raiseStateChangedEvent && PrevousStateItem != CurrentStateItem)
                    {
                        OnStateChanged(new InfoNavigatorStateChangedEventArgs(CurrentStateItem, PrevousStateItem));
                    }
                    break;
                }
            }
        }

        private void SetSecUsersAndRoles(NotifyForm form)
        {
            form.secRoles.Clear();
            form.secUsers.Clear();

            string sqlGM = string.Format(GloFix.secGroups, this.MenuId);
            DataTable tabGM = null;

            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlGM });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                tabGM = ((DataSet)ret1[1]).Tables[0];
            }
            foreach (DataRow row in tabGM.Rows)
            {
                form.secRoles.Add(row["GROUPID"].ToString());
            }
            string sqlUM = string.Format(GloFix.secUsers, this.MenuId);
            DataTable tabUM = null;

            object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlUM });
            if (ret2 != null && (int)ret2[0] == 0)
            {
                tabUM = ((DataSet)ret2[1]).Tables[0];
            }
            foreach (DataRow row in tabUM.Rows)
            {
                form.secUsers.Add(row["USERID"].ToString());
            }
        }

        private void SetSecUsersAndRoles(PlusForm form)
        {
            form.secRoles.Clear();

            string sqlGM = string.Format(GloFix.secGroups, this.MenuId); 
            DataTable tabGM = null;

            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlGM });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                tabGM = ((DataSet)ret1[1]).Tables[0];
            }
            foreach (DataRow row in tabGM.Rows)
            {
                form.secRoles.Add(row["GROUPID"].ToString());
            }
        }

        protected override void DoViewQuery()
        {
            base.DoViewQuery();
            if (_navMode == "3") //Inquery
            {
                CurrentStateItem = null;
                this.SetState("Inquery");
            }
            else if (_navMode == "4") //Prepare
            {
                CurrentStateItem = null;
                if (this.BindingSource.Count > 0)
                {
                    this.SetState("Prepare");
                }
                else
                {
                    this.SetState("Inquery");
                }
            }
            if (this.BindingSource.Count > 0)
            {
                object obj = this.BindingSource.GetDataSource(true);
                if (obj != null && obj is DataSet)
                {
                    DataTable ds = (obj as DataSet).Tables[0];
                    if (ds.Rows.Count > 1)
                    {
                        this.ViewMoveNextItem.Enabled = true;
                        this.ViewMoveLastItem.Enabled = true;
                    }
                }
                this.PrintItem.Enabled = true;
            }
        }
    }

    #region NavigatorStates
    [Editor(typeof(StateCollectionEditor), typeof(UITypeEditor))]
    public class FLNavigatorStateCollection : InfoOwnerCollection
    {
        public FLNavigatorStateCollection(Object aOwner, Type aItemType)
            : base(aOwner, typeof(InfoOwnerCollectionItem))
        {
            FLNavigatorStateItem ApproveStateItem = new FLNavigatorStateItem();
            base.Add(ApproveStateItem);
            ApproveStateItem.StateText = "Approve";

            FLNavigatorStateItem ContinueStateItem = new FLNavigatorStateItem();
            base.Add(ContinueStateItem);
            ContinueStateItem.StateText = "Continue";

            FLNavigatorStateItem InqueryStateItem = new FLNavigatorStateItem();
            base.Add(InqueryStateItem);
            InqueryStateItem.StateText = "Inquery";

            FLNavigatorStateItem NotifyStateItem = new FLNavigatorStateItem();
            base.Add(NotifyStateItem);
            NotifyStateItem.StateText = "Notify";

            FLNavigatorStateItem ReturnStateItem = new FLNavigatorStateItem();
            base.Add(ReturnStateItem);
            ReturnStateItem.StateText = "Return";

            FLNavigatorStateItem SubmitStateItem = new FLNavigatorStateItem();
            base.Add(SubmitStateItem);
            SubmitStateItem.StateText = "Submit";

            FLNavigatorStateItem NoneStateItem = new FLNavigatorStateItem();
            base.Add(NoneStateItem);
            NoneStateItem.StateText = "None";

            FLNavigatorStateItem PlusStateItem = new FLNavigatorStateItem();
            base.Add(PlusStateItem);
            PlusStateItem.StateText = "Plus";

            FLNavigatorStateItem LockStateItem = new FLNavigatorStateItem();
            base.Add(LockStateItem);
            LockStateItem.StateText = "Lock";

            FLNavigatorStateItem RSubmitStateItem = new FLNavigatorStateItem();
            base.Add(RSubmitStateItem);
            RSubmitStateItem.StateText = "RSubmit";

            FLNavigatorStateItem FSubmitStateItem = new FLNavigatorStateItem();
            base.Add(FSubmitStateItem);
            FSubmitStateItem.StateText = "FSubmit";

        }

        public new FLNavigatorStateItem this[int index]
        {
            get
            {
                return (FLNavigatorStateItem)InnerList[index];
            }
            set
            {
                if (index > -1 && index < Count)
                {
                    if (value is FLNavigatorStateItem)
                    {
                        //原来的Collection设置为0
                        ((FLNavigatorStateItem)InnerList[index]).Collection = null;
                        InnerList[index] = value;
                        //Collection设置为this
                        ((FLNavigatorStateItem)InnerList[index]).Collection = this;
                    }
                }
            }
        }

        new public void Remove(object value)
        {
            FLNavigatorStateItem stateItem = value as FLNavigatorStateItem;
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
                    || stateItem.StateText == "RSubmit"
                    || stateItem.StateText == "FSubmit")
                {
                    throw new Exception("Default FLNavigatorStateItem can not be removed");
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
                FLNavigatorStateItem stateItem = this[index];
                if (stateItem.StateText == "Approve"
                    || stateItem.StateText == "Continue"
                    || stateItem.StateText == "Inquery"
                    || stateItem.StateText == "Notify"
                    || stateItem.StateText == "Return"
                    || stateItem.StateText == "Submit"
                    || stateItem.StateText == "None"
                    || stateItem.StateText == "Plus"
                    || stateItem.StateText == "Lock"
                    || stateItem.StateText == "RSubmit"
                    || stateItem.StateText == "FSubmit")
                {
                    throw new Exception("Default FLNavigatorStateItem can not be removed");
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
            FLNavigatorStateItem stateItem = value as FLNavigatorStateItem;
            if (stateItem != null)
            {
                foreach (FLNavigatorStateItem si in this)
                {
                    if (si.StateText == stateItem.StateText)
                    {
                        si.VisibleControls = stateItem.VisibleControls;
                        si.Description = stateItem.Description;
                        si.VisibleControlsEdited = stateItem.VisibleControlsEdited;
                        return;
                    }
                }

                base.Add(stateItem);
            }
        }

        public void ClearExceptDefaultStateItem()
        {
            // The number of Default FLNavigatorStateItem is 11
            while (this.Count > 11)
            {
                base.RemoveAt(11);
            }
        }
    }

    public class FLNavigatorStateItem : InfoOwnerCollectionItem
    {
        private string _name;
        private string _stateText;
        private string _description;
        private bool _visibleControlsEdited = false;
        private List<string> _visibleControls = new List<string>();

        public FLNavigatorStateItem()
        {
            _visibleControls = new List<string>();
        }

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
                    || _stateText == "RSubmit"
                    || _stateText == "FSubmit")
                {
                    throw new Exception("Default StateText Can not be Changed");
                }
                else if (value == null || value.Trim() == "")
                {
                    throw new Exception("Empty StateText not allowed");
                }
                else
                {
                    FLNavigatorStateCollection stateCollection = this.Collection as FLNavigatorStateCollection;
                    if (stateCollection != null)
                    {
                        foreach (FLNavigatorStateItem stateItem in stateCollection)
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

        [Browsable(false)]
        public bool VisibleControlsEdited
        {
            get
            {
                return _visibleControlsEdited;
            }
            set
            {
                _visibleControlsEdited = value;
            }
        }

        [Editor(typeof(VisableControlsEditor), typeof(UITypeEditor))]
        public List<string> VisibleControls
        {
            get
            {
                return _visibleControls;
            }
            set
            {
                if (value == null || value is List<string>)
                {
                    _visibleControls = value;
                }
            }
        }
    }

    internal class StateCollectionEditor : UITypeEditor
    {
        IWindowsFormsEditorService EditorService = null;
        public StateCollectionEditor()
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
                EditorService = provider.GetService(typeof(IWindowsFormsEditorService))
                    as IWindowsFormsEditorService;
            }

            FLNavigator flNavigator = context.Instance as FLNavigator;

            if (flNavigator != null && EditorService != null)
            {
                if (value is FLNavigatorStateCollection)
                {
                    FLNavigatorStateCollectionEditorDialog editorDialog = new FLNavigatorStateCollectionEditorDialog(value as FLNavigatorStateCollection);
                    if (DialogResult.OK == EditorService.ShowDialog(editorDialog))
                    {
                        IComponentChangeService ComponentChangeService = provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;

                        object oldValue = null;
                        object newValue = null;

                        // States changed
                        PropertyDescriptor descStates = TypeDescriptor.GetProperties(flNavigator)["FLStates"];
                        ComponentChangeService.OnComponentChanging(flNavigator, descStates);
                        oldValue = value;

                        value = editorDialog.Collection;

                        newValue = value;
                        ComponentChangeService.OnComponentChanged(flNavigator, descStates, oldValue, newValue);
                    }
                }
            }

            return value;
        }
    }

    internal class VisableControlsEditor : UITypeEditor
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
            FLNavigatorStateItem stateItem = context.Instance as FLNavigatorStateItem;
            FLNavigator flNavigator = stateItem.Owner as FLNavigator;
            if (EditorService != null && flNavigator != null)
            {
                if (value is List<string>)
                {
                    FLNavigatorVisibleControlsEditorDialog editorDialog = new FLNavigatorVisibleControlsEditorDialog(value as List<string>, flNavigator);
                    if (DialogResult.OK == EditorService.ShowDialog(editorDialog))
                    {
                        stateItem.VisibleControlsEdited = true;
                        value = editorDialog.VisibleControls;
                    }
                }
            }
            return value;
        }
    }

    #endregion

    #region DataStates
    [Editor(typeof(NavigatorStateCollectionEditor), typeof(UITypeEditor))]
    public class FLDataStateCollection : InfoOwnerCollection
    {
        public FLDataStateCollection(Object aOwner, Type aItemType)
            : base(aOwner, typeof(Transaction))
        {
            StateItem InitStateItem = new StateItem();
            base.Add(InitStateItem);
            InitStateItem.StateText = "Initial";

            StateItem BrowsedStateItem = new StateItem();
            base.Add(BrowsedStateItem);
            BrowsedStateItem.StateText = "Browsed";

            StateItem InsertingStateItem = new StateItem();
            base.Add(InsertingStateItem);
            InsertingStateItem.StateText = "Inserting";

            StateItem EditingStateItem = new StateItem();
            base.Add(EditingStateItem);
            EditingStateItem.StateText = "Editing";

            StateItem ApplyingStateItem = new StateItem();
            base.Add(ApplyingStateItem);
            ApplyingStateItem.StateText = "Applying";

            StateItem ChangingStateItem = new StateItem();
            base.Add(ChangingStateItem);
            ChangingStateItem.StateText = "Changing";

            StateItem QueryingStateItem = new StateItem();
            base.Add(QueryingStateItem);
            QueryingStateItem.StateText = "Querying";

            StateItem PrintingStateItem = new StateItem();
            base.Add(PrintingStateItem);
            PrintingStateItem.StateText = "Printing";

            StateItem NormalStateItem = new StateItem();
            base.Add(NormalStateItem);
            NormalStateItem.StateText = "Normal";

            StateItem InsertStateItem = new StateItem();
            base.Add(InsertStateItem);
            InsertStateItem.StateText = "Insert";

            StateItem ModifyStateItem = new StateItem();
            base.Add(ModifyStateItem);
            ModifyStateItem.StateText = "Modify";

            StateItem InqueryStateItem = new StateItem();
            base.Add(InqueryStateItem);
            InqueryStateItem.StateText = "Inquery";

            StateItem PrepareStateItem = new StateItem();
            base.Add(PrepareStateItem);
            PrepareStateItem.StateText = "Prepare";
        }


        public new StateItem this[int index]
        {
            get
            {
                return (StateItem)InnerList[index];
            }
            set
            {
                if (index > -1 && index < Count)
                {
                    if (value is StateItem)
                    {
                        //原来的Collection设置为0
                        ((StateItem)InnerList[index]).Collection = null;
                        InnerList[index] = value;
                        //Collection设置为this
                        ((StateItem)InnerList[index]).Collection = this;
                    }
                }
            }
        }

        new public virtual void Remove(object value)
        {
            StateItem stateItem = value as StateItem;
            if (stateItem != null)
            {
                if (stateItem.StateText == "Initial"
                    || stateItem.StateText == "Browsed"
                    || stateItem.StateText == "Inserting"
                    || stateItem.StateText == "Editing"
                    || stateItem.StateText == "Applying"
                    || stateItem.StateText == "Changing"
                    || stateItem.StateText == "Querying"
                    || stateItem.StateText == "Printing"

                    || stateItem.StateText == "Normal"
                    || stateItem.StateText == "Insert"
                    || stateItem.StateText == "Modify"
                    || stateItem.StateText == "Inquery"
                    || stateItem.StateText == "Prepare")
                {
                    throw new Exception("Default StateItem can not be removed");
                }
                else
                {
                    base.Remove(value);
                }
            }
        }

        new public virtual void RemoveAt(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                StateItem stateItem = this[index];
                if (stateItem.StateText == "Initial"
                    || stateItem.StateText == "Browsed"
                    || stateItem.StateText == "Inserting"
                    || stateItem.StateText == "Editing"
                    || stateItem.StateText == "Applying"
                    || stateItem.StateText == "Changing"
                    || stateItem.StateText == "Querying"
                    || stateItem.StateText == "Printing"

                    || stateItem.StateText == "Normal"
                    || stateItem.StateText == "Insert"
                    || stateItem.StateText == "Modify"
                    || stateItem.StateText == "Inquery"
                    || stateItem.StateText == "Prepare")
                {
                    throw new Exception("Default StateItem can not be removed");
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
            StateItem stateItem = value as StateItem;
            if (stateItem != null)
            {
                foreach (StateItem si in this)
                {
                    if (si.StateText == stateItem.StateText)
                    {
                        si.EnabledControls = stateItem.EnabledControls;
                        si.Description = stateItem.Description;
                        si.EnabledControlsEdited = stateItem.EnabledControlsEdited;
                        return;
                    }
                }

                base.Add(stateItem);
            }
        }

        public virtual void ClearExceptDefaultStateItem()
        {
            // The number of Default StateItem is 13
            while (this.Count > 13)
            {
                base.RemoveAt(13);
            }
        }
    }

    internal class NavigatorStateCollectionEditor : UITypeEditor
    {
        IWindowsFormsEditorService EditorService = null;
        public NavigatorStateCollectionEditor()
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
                EditorService = provider.GetService(typeof(IWindowsFormsEditorService))
                    as IWindowsFormsEditorService;
            }

            FLNavigator flNavigator = context.Instance as FLNavigator;
            if (flNavigator != null && EditorService != null)
            {
                if (value is FLDataStateCollection)
                {
                    FLDataStateCollectionEditorDialog editorDialog = new FLDataStateCollectionEditorDialog(value as FLDataStateCollection);
                    if (DialogResult.OK == EditorService.ShowDialog(editorDialog))
                    {
                        IComponentChangeService ComponentChangeService = provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;

                        object oldValue = null;
                        object newValue = null;

                        // States changed
                        PropertyDescriptor descStates = TypeDescriptor.GetProperties(flNavigator)["States"];
                        ComponentChangeService.OnComponentChanging(flNavigator, descStates);
                        oldValue = value;

                        value = editorDialog.Collection;

                        newValue = value;
                        ComponentChangeService.OnComponentChanged(flNavigator, descStates, oldValue, newValue);
                    }
                }
            }

            return value;
        }
    }

    /*internal class EnableControlsEditor : UITypeEditor
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
            StateItem stateItem = context.Instance as StateItem;
            FLNavigator flNavigator = stateItem.Owner as FLNavigator;
            if (EditorService != null && flNavigator != null)
            {
                if (value is List<string>)
                {
                    EnabledControlsEditorDialog editorDialog = new EnabledControlsEditorDialog(value as List<string>, flNavigator);
                    if (DialogResult.OK == EditorService.ShowDialog(editorDialog))
                    {
                        stateItem.EnabledControlsEdited = true;
                        value = editorDialog.EnabledControls;
                    }
                }
            }
            return value;
        }
    }*/

    #endregion

    public class GetAlias : System.Drawing.Design.UITypeEditor
    {
        public GetAlias()
        {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            List<string> objName = new List<string>();
            if (context.Instance is INavigatorSecurity)
            {
                if (File.Exists(SystemFile.DBFile))
                {
                    XmlDocument DBXML = new XmlDocument();
                    DBXML.Load(SystemFile.DBFile);
                    XmlNode aNode = DBXML.DocumentElement.FirstChild;
                    while (aNode != null)
                    {
                        if (aNode.Name.ToUpper().Equals("DATABASE"))
                        {
                            XmlNode bNode = aNode.FirstChild;
                            while (bNode != null)
                            {
                                objName.Add(bNode.LocalName);
                                bNode = bNode.NextSibling;
                            }
                        }
                        aNode = aNode.NextSibling;
                    }
                }
            }
            IWindowsFormsEditorService EditorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (EditorService != null)
            {
                StringListSelector selector = new StringListSelector(EditorService, objName.ToArray());
                string strValue = (string)value;
                if (selector.Execute(ref strValue)) value = strValue;
            }
            return value;
        }
    }

    public class GetMenuID : System.Drawing.Design.UITypeEditor
    {
        public GetMenuID()
        {

        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            List<string> objName = new List<string>();
            if (context.Instance is INavigatorSecurity)
            {
                INavigatorSecurity sec = context.Instance as INavigatorSecurity;
                object[] param = new object[1];
                param[0] = sec.DBAlias;
                CliUtils.fLoginDB = sec.DBAlias;
                object[] myRet = CliUtils.CallMethod("GLModule", "GetMenuID", param);
                CliUtils.fLoginDB = "";
                if ((myRet != null) && (0 == (int)myRet[0]))
                {
                    ArrayList listMenuID = (ArrayList)myRet[1];
                    ArrayList listCaption = (ArrayList)myRet[2];
                    for (int i = 0; i < listMenuID.Count; i++)
                    {
                        objName.Add(listMenuID[i] + "(" + listCaption[i] + ")");
                    }
                }
            }
            IWindowsFormsEditorService EditorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (EditorService != null)
            {
                StringListSelector selector = new StringListSelector(EditorService, objName.ToArray());
                string strValue = (string)value;
                if (selector.Execute(ref strValue))
                {
                    value = strValue.Substring(0, strValue.IndexOf('('));
                }
            }
            return value;
        }
    }
}
