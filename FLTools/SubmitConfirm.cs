using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    public partial class SubmitConfirm : Form
    {

        public SubmitConfirm()
        {
            InitializeComponent();
            this.dialogSave.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //this.dialogSave.InitialDirectory = @"C:\Documents and Settings\taoweijia\桌面\wfdownload";
        }

        internal string listId = "";
        internal string provider = "";
        internal string keys = "";
        internal string values = "";
        internal string flowFileName = "";
        internal string flowPath = "";
        internal string currentFLState = "";
        internal FLNavigatorOperate operate;
        internal bool isFlowImportant = false;
        internal bool isFlowUrgent = false;
        internal string status = "";
        internal bool organizationControl = false;
        internal bool multiStepReturn = false;
        internal string sendToId = "";
        internal List<string> attachments = new List<string>();
        private bool sucess = false;
        string[] lstStatus = null;
        internal DataSet host;

        string paramAttach()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string attach in this.attachments)
            {
                if (!string.IsNullOrEmpty(attach))
                {
                    builder.AppendFormat(";{0}", attach);
                }
            }
            return builder.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.gbResult.Visible = true;
            CliUtils.fFlowSelectedRole = (this.cmbRole.Items.Count > 0) ? this.cmbRole.SelectedValue.ToString() : "";
            string suggest = this.txtSuggest.Text, role = (this.cmbRole.Items.Count > 0) ? this.cmbRole.SelectedValue.ToString() : "";
            int isImport = this.chkImportant.Checked ? 1 : 0, isUrgent = this.chkUrgent.Checked ? 1 : 0;

            string[] fLActivities = flowPath.Split(';');
            object[] objParams = null;
            switch (operate)
            {
                case FLNavigatorOperate.Submit:
                    if (currentFLState != "Continue")
                    {
                        string org = "";
                        if (organizationControl)
                        {
                            org = this.cmbOrg.SelectedValue.ToString();
                        }
                        objParams = CliUtils.CallFLMethod("Submit", new object[] { null, new object[] { flowFileName + ".xoml", "", isImport, isUrgent, suggest, role, provider, 0, org, this.paramAttach() }, new object[] { keys, values } });
                        if (Convert.ToInt16(objParams[0]) == 0)
                        {
                            if (objParams.Length > 3)
                            {
                                listId = objParams[2].ToString();
                                flowPath = objParams[3].ToString() + ";" + objParams[3].ToString();
                            }
                            if (objParams[1].ToString() == "512F4277-0D41-441c-BF16-D96B04580C2E")
                            {
                                this.label2.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "HasRejected");
                            }
                            else if (objParams[1].ToString() == "60585C77-60E1-4e6f-A2E2-3BBBAD6B4C9E")
                            {
                                this.label2.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "RunOver");
                            }
                            else
                            {
                                this.lblSend.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "Send");
                                this.label2.Text = GloFix.ShowMessage(objParams[1].ToString(), false);
                            }
                            sucess = true;
                        }
                        else
                        {
                            if (Convert.ToInt16(objParams[0]) == 2)
                                this.label2.Text = objParams[1].ToString();
                            sucess = false;
                        }
                    }
                    else
                    {
                        objParams = CliUtils.CallFLMethod("Approve", new object[] { new Guid(listId), new object[] { fLActivities[0], fLActivities[1], isImport, isUrgent, suggest, role, provider, 0, "", this.paramAttach() }, new object[] { keys, values } });
                        if (Convert.ToInt16(objParams[0]) == 0)
                        {
                            if (objParams[1].ToString() == "60585C77-60E1-4e6f-A2E2-3BBBAD6B4C9E")
                            {
                                this.label2.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "RunOver");
                            }
                            else if (objParams[1].ToString() == "512F4277-0D41-441c-BF16-D96B04580C2E")
                            {
                                this.label2.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "HasRejected");
                            }
                            else
                            {
                                this.lblSend.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "Send");
                                this.label2.Text = GloFix.ShowMessage(objParams[1].ToString(), false);
                            }
                            sucess = true;
                        }
                        else
                        {
                            if (Convert.ToInt16(objParams[0]) == 2)
                                this.label2.Text = objParams[1].ToString();
                            sucess = false;
                        }
                    }
                    break;
                case FLNavigatorOperate.Approve:
                    if (status == "A" || status == "AA")
                    {
                        objParams = CliUtils.CallFLMethod("PlusReturn", new object[] { new Guid(listId), new object[] { fLActivities[0], fLActivities[1], isImport, isUrgent, suggest, role, provider, 0, "", this.paramAttach() }, new object[] { keys, values } });
                    }
                    else
                    {
                        objParams = CliUtils.CallFLMethod("Approve", new object[] { new Guid(listId), new object[] { fLActivities[0], fLActivities[1], isImport, isUrgent, suggest, role, provider, 0, "", this.paramAttach() }, new object[] { keys, values } });
                    }
                    if (Convert.ToInt16(objParams[0]) == 0)
                    {
                        if (objParams[1].ToString() == "60585C77-60E1-4e6f-A2E2-3BBBAD6B4C9E")
                        {
                            this.label2.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "RunOver");
                        }
                        else if (objParams[1].ToString() == "512F4277-0D41-441c-BF16-D96B04580C2E")
                        {
                            this.label2.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLNavigator", "HasRejected");
                        }
                        else
                        {
                            if (objParams[1].ToString() == "")
                            {
                                string wait = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "WaitMessage", false);

                                string sql = "select SENDTO_KIND, SENDTO_ID from SYS_TODOLIST where LISTID='" + listId + "' and STATUS <> 'F'";
                                DataTable dtOthers = null;
                                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
                                if (ret1 != null && (int)ret1[0] == 0)
                                {
                                    dtOthers = ((DataSet)ret1[1]).Tables[0];
                                }
                                string sendToIds = "";
                                foreach (DataRow row in dtOthers.Rows)
                                {
                                    string sendtokind = row["SENDTO_KIND"].ToString();
                                    string sendtoid = row["SENDTO_ID"].ToString();
                                    if (sendtokind == "1")
                                    {
                                        sendToIds += sendtoid + ";";
                                    }
                                    else if (sendtokind == "2")
                                    {
                                        sendToIds += sendtoid + ":UserId;";
                                    }
                                }
                                if (sendToIds != "")
                                {
                                    this.label2.Text = GloFix.ShowParallelMessage(sendToIds);
                                }
                            }
                            else
                            {
                                this.lblSend.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "Send");
                                this.label2.Text = GloFix.ShowMessage(objParams[1].ToString(), false);
                            }
                        }
                        sucess = true;
                    }
                    else
                    {
                        if (Convert.ToInt16(objParams[0]) == 2)
                            this.label2.Text = objParams[1].ToString();
                        sucess = false;
                    }
                    break;
                case FLNavigatorOperate.Return:
                    if (status == "A" || status == "AA")
                    {
                        objParams = CliUtils.CallFLMethod("PlusReturn2", new object[] { new Guid(listId), new object[] { fLActivities[0], fLActivities[1], isImport, isUrgent, suggest, role, provider, 0, "", this.paramAttach() }, new object[] { keys, values } });
                    }
                    else if (!this.multiStepReturn)
                    {
                        objParams = CliUtils.CallFLMethod("Return", new object[] { new Guid(listId), new object[] { fLActivities[0], fLActivities[1], isImport, isUrgent, suggest, role, provider, 0, "", this.paramAttach() }, new object[] { keys, values } });
                    }
                    else
                    {
                        if (this.cmbRetunStep.SelectedIndex == 0)
                        {
                            objParams = CliUtils.CallFLMethod("Return", new object[] { new Guid(listId), new object[] { fLActivities[0], fLActivities[1], isImport, isUrgent, suggest, role, provider, 0, "", this.paramAttach() }, new object[] { keys, values } });
                        }
                        else
                        {
                            string retToStep = this.cmbRetunStep.Text;
                            objParams = CliUtils.CallFLMethod("Return2", new object[] { new Guid(listId), new object[] { retToStep, fLActivities[1], isImport, isUrgent, suggest, role, provider, 0, "", this.paramAttach() }, new object[] { keys, values } });
                        }
                    }

                    if (Convert.ToInt16(objParams[0]) == 0)
                    {
                        string s = objParams[1].ToString();
                        if (s == "B4DAF3A4-AAE8-4b51-A391-B52E46305E9F")
                        {
                            this.label2.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "FLWebNavigator", "ReturnToEnd");
                        }
                        else
                        {
                            // 返回的格式为：C912D847-1825-458a-8CB5-E680FACA42AF:001;002，冒号后面的就是要等待的角色，多个角色用分号分隔。
                            if (objParams[1].ToString().StartsWith("C912D847-1825-458a-8CB5-E680FACA42AF"))
                            {
                                this.label2.Text = GloFix.ShowMessage3(objParams[1].ToString().Split(':')[1], false);
                            }
                            else if (objParams[1].ToString() == "")
                            {
                                string wait = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "WaitMessage", false);

                                string sql = "select SENDTO_KIND, SENDTO_ID from SYS_TODOLIST where LISTID='" + listId + "' and STATUS <> 'F'";
                                DataTable dtOthers = null;
                                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
                                if (ret1 != null && (int)ret1[0] == 0)
                                {
                                    dtOthers = ((DataSet)ret1[1]).Tables[0];
                                }
                                string sendToIds = "";
                                foreach (DataRow row in dtOthers.Rows)
                                {
                                    string sendtokind = row["SENDTO_KIND"].ToString();
                                    string sendtoid = row["SENDTO_ID"].ToString();
                                    if (sendtokind == "1")
                                    {
                                        sendToIds += sendtoid + ";";
                                    }
                                    else if (sendtokind == "2")
                                    {
                                        sendToIds += sendtoid + ":UserId;";
                                    }
                                }
                                if (sendToIds != "")
                                {
                                    this.label2.Text = GloFix.ShowParallelMessage(sendToIds);
                                }
                            }
                            else
                            {
                                this.lblSend.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "Send");
                                this.label2.Text = GloFix.ShowMessage(objParams[1].ToString(), false);
                            }
                        }
                        sucess = true;
                    }
                    else
                    {
                        if (Convert.ToInt16(objParams[0]) == 2)
                            this.label2.Text = objParams[1].ToString();
                        sucess = false;
                    }
                    break;
            }
        }

        private void SubmitConfirm_Load(object sender, EventArgs e)
        {
            lstStatus = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLDesigner", "FLDesigner", "Item3").Split(',');
            SetLanguage(CliUtils.fClientLang);
            string sql = "SELECT S_STEP_ID,USER_ID,USERNAME,STATUS,UPDATE_DATE,UPDATE_TIME,REMARK FROM SYS_TODOHIS Where (LISTID = '" + listId + "') ORDER BY UPDATE_DATE,UPDATE_TIME";
            DataTable tabHis = null;
            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                tabHis = ((DataSet)ret1[1]).Tables[0];
            }
            this.dgvHis.AutoGenerateColumns = false;
            this.dgvHis.DataSource = tabHis;
            Binding binding = new Binding("Text", tabHis, "REMARK");
            this.txtRemark.DataBindings.Add(binding);

            string curTime = GloFix.DateTimeString(DateTime.Now);
            string curUser = CliUtils.fLoginUser;
            string flowDesc = "";
            switch (operate)
            {
                case FLNavigatorOperate.Submit:
                    if (currentFLState != "Continue")
                        flowDesc = GloFix.GetFlowDesc(flowFileName + ".xoml", false);
                    else
                        flowDesc = GloFix.GetFlowDesc(listId, true);
                    break;
                case FLNavigatorOperate.Approve:
                case FLNavigatorOperate.Return:
                    flowDesc = GloFix.GetFlowDesc(listId, true);
                    break;
            }

            sql = "SELECT GROUPID,GROUPNAME FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + curUser + "')  AND ISROLE='Y' UNION SELECT ROLE_ID AS GROUPID,GROUPS.GROUPNAME  FROM SYS_ROLES_AGENT LEFT JOIN GROUPS ON SYS_ROLES_AGENT.ROLE_ID=GROUPS.GROUPID WHERE (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC='" + flowDesc + "') AND AGENT='" + curUser + "' AND START_DATE+START_TIME<='" + curTime + "' AND END_DATE+END_TIME>='" + curTime + "'";
            object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (ret2 != null && (int)ret2[0] == 0)
            {
                this.cmbRole.DataSource = ((DataSet)ret2[1]).Tables[0];
                this.cmbRole.ValueMember = "GROUPID";
                this.cmbRole.DisplayMember = "GROUPNAME";
                if (!string.IsNullOrEmpty(sendToId))
                {
                    string roleSQL = String.Format("SELECT ROLE_ID  FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = '{0}' AND SYS_TODOHIS.USER_ID ='{1}' ORDER BY UPDATE_DATE, UPDATE_TIME", listId, curUser);
                    object[] ret3 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { roleSQL });
                    if (ret3 != null && (int)ret3[0] == 0)
                    {
                        DataSet roleDataSet = (DataSet)ret3[1];
                        if (roleDataSet.Tables[0].Rows.Count > 0)
                        {
                            var role = roleDataSet.Tables[0].Rows[0]["ROLE_ID"].ToString();
                            this.cmbRole.SelectedValue = role;
                        }
                    }
                    else
                    {
                        try
                        {
                            this.cmbRole.SelectedValue = sendToId;
                        }
                        catch { }
                    }
                }
                else if (CliUtils.fFlowSelectedRole != "")
                    this.cmbRole.SelectedValue = CliUtils.fFlowSelectedRole;
                else
                {
                    this.cmbRole.SelectedIndex = 0;
                }

            }

            this.buttonPreview.Enabled = (operate != FLNavigatorOperate.Return);

            if (operate == FLNavigatorOperate.Submit && currentFLState != "Continue" && organizationControl)
            {
                this.lblOrg.Visible = this.cmbOrg.Visible = true;
                sql = "select * from SYS_ORGKIND";
                object[] ret3 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
                if (ret3 != null && (int)ret3[0] == 0)
                {
                    this.cmbOrg.DataSource = ((DataSet)ret3[1]).Tables[0];
                    this.cmbOrg.ValueMember = "ORG_KIND";
                    this.cmbOrg.DisplayMember = "KIND_DESC";
                }
            }
            else
                this.lblOrg.Visible = this.cmbOrg.Visible = false;

            if (operate == FLNavigatorOperate.Return && multiStepReturn && (status != "A" && status != "AA"))
            {
                this.lblReturnStep.Visible = this.cmbRetunStep.Visible = true;
                object[] objParams = CliUtils.CallFLMethod("GetFLPathList", new object[] { new Guid(listId) });
                if (Convert.ToInt16(objParams[0]) == 0 && objParams[1] != null)
                {
                    List<string> src = new List<string>();
                    string[] UITexts = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "SubmitConfirm", "UIText").Split(',');
                    src.Add("<" + UITexts[11] + ">");
                    src.AddRange((string[])objParams[1]);
                    this.cmbRetunStep.DataSource = src;
                }
                else
                {
                    this.lblReturnStep.Visible = this.cmbRetunStep.Visible = false;
                    multiStepReturn = false;
                }
            }
            else
                this.lblReturnStep.Visible = this.cmbRetunStep.Visible = false;

            this.chkImportant.Checked = isFlowImportant;
            this.chkUrgent.Checked = isFlowUrgent;

            this.GenAttachments();
        }

        void GenAttachments()
        {
            if (this.attachments.Count >= 0)
            {
                this.gbDownload.Controls.Clear();
                int y = 15;
                for (int i = 0; i < this.attachments.Count; i++)
                {
                    LinkLabel lbl = new LinkLabel();
                    lbl.Text = this.attachments[i];
                    this.ttAttachment.SetToolTip(lbl, this.attachments[i]);
                    lbl.Width = 158;
                    lbl.Height = 15;
                    if (i % 3 == 0)
                    {
                        lbl.Location = new Point(5, y);
                    }
                    else if (i % 3 == 1)
                    {
                        lbl.Location = new Point(163, y);
                    }
                    else
                    {
                        lbl.Location = new Point(321, y);
                        y += 15;
                    }
                    lbl.LinkClicked += new LinkLabelLinkClickedEventHandler(lbl_LinkClicked);
                    this.gbDownload.Controls.Add(lbl);
                }
            }
        }

        void lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lbl = sender as LinkLabel;
            this.dialogSave.FileName = lbl.Text;
            if (this.dialogSave.ShowDialog() == DialogResult.OK)
            {
                string xpath = "{0}appSettings/{0}add[@key='FlowFilesBySolutions']";
                object[] oConfig = CliUtils.CallMethod("GLModule", "GetWebSiteConfig", new object[] { xpath });
                if (oConfig != null && (int)oConfig[0] == 0)
                {
                    string srv = oConfig[1].ToString() + @"WorkflowFiles\" + lbl.Text;
                    if ((bool)oConfig[2])
                    {
                        srv = string.Format(@"{0}WorkflowFiles\{1}\{2}", oConfig[1].ToString(), CliUtils.fCurrentProject, lbl.Text);
                    }


                    string fileName = dialogSave.FileName; //string.Format(@"{0}\Temp\{1}", Application.StartupPath, lbl.Text);
                    CliUtils.DownLoad(srv, fileName);
                    System.Diagnostics.Process.Start(fileName);



                    //string clt = this.dialogSave.FileName;
                    //CliUtils.DownLoad(srv, clt);
                }
            }
        }

        private void SetLanguage(SYS_LANGUAGE language)
        {
            string[] UITexts = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "SubmitConfirm", "UIText").Split(',');
            if (UITexts.Length >= 12)
            {
                this.gbContainer.Text = UITexts[0];
                this.chkImportant.Text = UITexts[1];
                this.chkUrgent.Text = UITexts[2];
                this.tabSuggest.Text = UITexts[3];
                this.tabHis.Text = UITexts[4];
                this.lblRole.Text = UITexts[5];
                this.btnOK.Text = UITexts[6];
                this.btnCancel.Text = UITexts[7];
                this.lblOrg.Text = UITexts[9];
                this.lblReturnStep.Text = UITexts[10];
                this.btnUploadFiles.Text = UITexts[12];
                this.btnClose.Text = UITexts[13];
                this.buttonPreview.Text = UITexts[14];
            }
            string[] UIHisTexts = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "SubmitConfirm", "HisUIText").Split(',');
            if (UIHisTexts.Length >= 5)
            {
                this.columnUserId.HeaderText = UIHisTexts[0];
                this.columnUserName.HeaderText = UIHisTexts[1];
                this.columnUpdateDate.HeaderText = UIHisTexts[2];
                this.columnUpdateTime.HeaderText = UIHisTexts[3];
                this.columnStepId.HeaderText = UIHisTexts[5];
                this.label1.Text = UIHisTexts[4];
                this.columnStatus.HeaderText = UIHisTexts[6];
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubmitConfirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sucess)
                this.DialogResult = DialogResult.OK;
        }

        private void dgvHis_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                Rectangle newRect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                using (Brush gridBrush = new SolidBrush(dgvHis.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor), selectionBackColorBrush = new SolidBrush(e.CellStyle.SelectionBackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        // Erase the cell.
                        if (dgvHis.Rows[e.RowIndex].Selected)
                        {
                            e.Graphics.FillRectangle(selectionBackColorBrush, e.CellBounds);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        }

                        // Draw the grid lines (only the right and bottom lines;
                        // DataGridView takes care of the others).
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                    }
                }
                using (Brush foreColorBrush = new SolidBrush(e.CellStyle.ForeColor), selectionForeColorBrush = new SolidBrush(e.CellStyle.SelectionForeColor))
                {
                    string formatValue = "";
                    switch (e.Value.ToString())
                    {
                        case "Z":
                            formatValue = lstStatus[0];
                            break;
                        case "N":
                            formatValue = lstStatus[1];
                            break;
                        case "NR":
                            formatValue = lstStatus[2];
                            break;
                        case "NF":
                            formatValue = lstStatus[3];
                            break;
                        case "X":
                            formatValue = lstStatus[4];
                            break;
                        case "A":
                            formatValue = lstStatus[5];
                            break;
                    }

                    if (dgvHis.Rows[e.RowIndex].Selected)
                    {
                        e.Graphics.DrawString(formatValue, e.CellStyle.Font, selectionForeColorBrush, e.CellBounds.X + 2, e.CellBounds.Y + 4);
                    }
                    else
                    {
                        e.Graphics.DrawString(formatValue, e.CellStyle.Font, foreColorBrush, e.CellBounds.X + 2, e.CellBounds.Y + 4);
                    }
                    e.Handled = true;
                }
            }
        }

        private void btnUploadFiles_Click(object sender, EventArgs e)
        {
            UploadFilesForm frmUpload = new UploadFilesForm();
            frmUpload.attachments = this.attachments;
            if (frmUpload.ShowDialog() == DialogResult.OK)
            {
                this.attachments = frmUpload.attachments;
                GenAttachments();
            }
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            Guid id = operate == FLNavigatorOperate.Approve ? new Guid(listId) : Guid.Empty;
            string activityname = operate == FLNavigatorOperate.Approve ? flowPath.Split(';')[1] : "";
            string role = (this.cmbRole.Items.Count > 0) ? this.cmbRole.SelectedValue.ToString() : "";
            object[] ret = CliUtils.CallFLMethod("Preview", new object[] { id
                , new object[] { flowFileName + ".xoml", "", activityname, host, role }, new object[] { keys, values }});
            if ((int)ret[0] == 0 && ret[1] != null)
            {
                if (ret[1] is byte[])
                {
                    string fileName = Application.StartupPath + "\\Preview.jpg";
                    System.IO.File.WriteAllBytes(fileName, (byte[])ret[1]);
                    System.Diagnostics.Process.Start(fileName);
                }
                else if (ret[1] is DataTable)
                {
                    Form form = new Form();
                    form.Text = "Preview";
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowInTaskbar = false;
                    form.Size = new Size(600, 400);

                    DataGridView gridview = new DataGridView();
                    gridview.AutoGenerateColumns = true;
                    gridview.DataSource = ret[1] as DataTable;
                    gridview.Dock = DockStyle.Fill;
                    gridview.ReadOnly = true;
                    gridview.AllowUserToAddRows = false;

                    form.Controls.Add(gridview);

                    form.ShowDialog();
                }
            }
        }
    }
}