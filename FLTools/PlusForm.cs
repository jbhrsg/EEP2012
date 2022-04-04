using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    public partial class PlusForm : Form
    {
        public PlusForm()
        {
            InitializeComponent();
        }

        DataTable tabRoles = null;
        internal bool sucess = false;
        internal string flowPath = "";
        internal string listId = "";
        internal string provider = "";
        internal string keys = "";
        internal string values = "";
        internal string sendToId = "";
        internal int isImportant = 0;
        internal int isUrgent = 0;
        internal string attachments = string.Empty;
        internal List<string> secRoles = new List<string>();

        private void PlusForm_Load(object sender, EventArgs e)
        {
            SetLanguage(CliUtils.fClientLang);

            string sqlRoles = "select GROUPID, GROUPNAME, ISROLE from GROUPS where ISROLE='Y'";
            if (this.secRoles.Count > 0)
            {
                sqlRoles += " and GROUPID in (";
                for (int i = 0; i < this.secRoles.Count; i++)
                {
                    if (i == this.secRoles.Count - 1)
                        sqlRoles += "'" + this.secRoles[i] + "'";
                    else
                        sqlRoles += "'" + this.secRoles[i] + "',";
                }
                sqlRoles += ")";
            }
            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlRoles });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                tabRoles = ((DataSet)ret1[1]).Tables[0];
            }
            DataColumn colDisRole = new DataColumn("DISROLE", typeof(string), "GROUPID+'('+GROUPNAME+')'");
            tabRoles.Columns.Add(colDisRole);
            this.lstRolesFrom.DataSource = tabRoles;
            this.lstRolesFrom.ValueMember = "GROUPID";
            this.lstRolesFrom.DisplayMember = "DISROLE";
        }

        private void SetLanguage(SYS_LANGUAGE language)
        {
            string[] UITexts = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "NotifyForm", "UIText").Split(',');
            if (UITexts.Length >= 7)
            {
                this.gbRoles.Text = UITexts[1];
                this.gbNotify.Text = UITexts[2];
                this.label1.Text = UITexts[3];
                this.label2.Text = UITexts[4];
                this.btnOK.Text = UITexts[5];
                this.btnCancel.Text = UITexts[6];

                this.label4.Text = UITexts[8];
                this.label5.Text = UITexts[9];
                this.btnSearch.Text = UITexts[10];
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.gbResult.Visible = true;
            string roles = "", message = this.txtNotify.Text;
            string[] fLActivities = flowPath.Split(';');
            foreach (string role in this.lstRolesTo.Items)
            {
                if (role.IndexOf('(') != -1)
                    roles += role.Substring(0, role.IndexOf('(')) + ";";
                else
                    roles += role + ";";
            }
            object[] objParams = CliUtils.CallFLMethod("PlusApprove", new object[] { new Guid(listId), new object[] { fLActivities[1], fLActivities[1], isImportant, isUrgent, message, sendToId, provider, 0, roles, attachments }, new object[] { keys, values } });
            if (Convert.ToInt16(objParams[0]) == 0)
            {
                string sendToIds = roles.Substring(0, roles.LastIndexOf(';'));
                this.label3.Text = GloFix.ShowPlusMessage(sendToIds);
                sucess = true;
            }
            else
            {
                if (Convert.ToInt16(objParams[0]) == 2)
                    this.label3.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "FailToPlus");
                sucess = false;
            }
        }

        private void btnRolesLR_Click(object sender, EventArgs e)
        {
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRowView rowView in this.lstRolesFrom.SelectedItems)
            {
                this.lstRolesTo.Items.Add(rowView["DISROLE"]);
                rows.Add(rowView.Row);
            }
            foreach (DataRow row in rows)
            {
                tabRoles.Rows.Remove(row);
            }
        }

        private void btnRolesRL_Click(object sender, EventArgs e)
        {
            List<string> senders = new List<string>();
            foreach (string send in this.lstRolesTo.SelectedItems)
            {
                string[] senderInfos = send.Split(new char[] { '(', ')' });
                DataRow row = tabRoles.NewRow();
                row["GROUPID"] = senderInfos[0];
                row["GROUPNAME"] = senderInfos[1];
                row["ISROLE"] = "Y";
                tabRoles.Rows.Add(row);
                senders.Add(send);
            }
            foreach (string send in senders)
            {
                this.lstRolesTo.Items.Remove(send);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PlusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sucess)
                this.DialogResult = DialogResult.OK;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlRoles = "select GROUPID, GROUPNAME, ISROLE from GROUPS where ISROLE='Y'";
            if (this.secRoles.Count > 0)
            {
                sqlRoles += " and GROUPID in (";
                for (int i = 0; i < this.secRoles.Count; i++)
                {
                    if (i == this.secRoles.Count - 1)
                        sqlRoles += "'" + this.secRoles[i] + "'";
                    else
                        sqlRoles += "'" + this.secRoles[i] + "',";
                }
                sqlRoles += ")";
            }
            if (!string.IsNullOrEmpty(this.txtSearchRoleId.Text))
            {
                sqlRoles += string.Format(" and groupid='{0}'", this.txtSearchRoleId.Text);
            }
            if (!string.IsNullOrEmpty(this.txtSearchRoleName.Text))
            {
                sqlRoles += string.Format(" and groupname like '%{0}%'", this.txtSearchRoleName.Text);
            }
            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlRoles });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                tabRoles = ((DataSet)ret1[1]).Tables[0];
                List<DataRow> rows = new List<DataRow>();
                foreach (string item in lstRolesTo.Items)
                {
                    string[] senderInfos = item.Split(new char[] { '(', ')' });
                    string groupID = senderInfos[0];
                    foreach (DataRow row in tabRoles.Rows)
                    {
                        if (row["GROUPID"].Equals(senderInfos[0]))
                        {
                            rows.Add(row);
                            break;
                        }
                    }
                }
                foreach (DataRow row in rows)
                {
                    tabRoles.Rows.Remove(row);
                }
            }
            DataColumn colDisRole = new DataColumn("DISROLE", typeof(string), "GROUPID+'('+GROUPNAME+')'");
            tabRoles.Columns.Add(colDisRole);
            this.lstRolesFrom.DataSource = tabRoles;
            this.lstRolesFrom.ValueMember = "GROUPID";
            this.lstRolesFrom.DisplayMember = "DISROLE";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void gbRoles_Enter(object sender, EventArgs e)
        {

        }
    }
}