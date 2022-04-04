using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    public partial class NotifyForm : Form
    {
        public NotifyForm()
        {
            InitializeComponent();
        }

        DataTable tabUsers = null;
        DataTable tabRoles = null;
        private void NotifyForm_Load(object sender, EventArgs e)
        {
            SetLanguage(CliUtils.fClientLang);
            string sqlUsers = "select USERID, USERNAME from USERS order by USERID";
            if (this.secUsers.Count > 0)
            {
                sqlUsers += " where USERID in (";
                for (int i = 0; i < this.secUsers.Count; i++)
                {
                    if (i == this.secUsers.Count - 1)
                        sqlUsers += "'" + this.secUsers[i] + "'";
                    else
                        sqlUsers += "'" + this.secUsers[i] + "',";
                }
                sqlUsers += ")";
            }
            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlUsers });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                tabUsers = ((DataSet)ret1[1]).Tables[0];
            }
            DataColumn colDisUser = new DataColumn("DISUSER", typeof(string), "USERID+'('+USERNAME+')'");
            tabUsers.Columns.Add(colDisUser);
            this.lstUsersFrom.DataSource = tabUsers;
            this.lstUsersFrom.ValueMember = "USERID";
            this.lstUsersFrom.DisplayMember = "DISUSER";

            string sqlRoles = "select GROUPID, GROUPNAME, ISROLE from GROUPS where ISROLE='Y' order by GROUPID";
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
            object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sqlRoles });
            if (ret2 != null && (int)ret2[0] == 0)
            {
                tabRoles = ((DataSet)ret2[1]).Tables[0];
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
                this.gbUsers.Text = UITexts[0];
                this.gbRoles.Text = UITexts[1];
                this.gbNotify.Text = UITexts[2];
                this.label1.Text = UITexts[3];
                this.label4.Text = UITexts[3];
                this.label2.Text = UITexts[4];
                this.label3.Text = UITexts[4];
                this.btnOK.Text = UITexts[5];
                this.btnCancel.Text = UITexts[6];
            }
        }

        private void btnUsersLR_Click(object sender, EventArgs e)
        {
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRowView rowView in this.lstUsersFrom.SelectedItems)
            {
                this.lstUsersTo.Items.Add(rowView["DISUSER"]);
                rows.Add(rowView.Row);
            }
            foreach (DataRow row in rows)
            {
                tabUsers.Rows.Remove(row);
            }
        }

        private void btnUsersRL_Click(object sender, EventArgs e)
        {
            List<string> senders = new List<string>();
            foreach (string send in this.lstUsersTo.SelectedItems)
            {
                string[] senderInfos = send.Split(new char[] { '(', ')' });
                DataRow row = tabUsers.NewRow();
                row["USERID"] = senderInfos[0];
                row["USERNAME"] = senderInfos[1];
                tabUsers.Rows.Add(row);
                senders.Add(send);
            }
            foreach (string send in senders)
            {
                this.lstUsersTo.Items.Remove(send);
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.gbResult.Visible = true;
            string users = "", roles = "", message = this.txtNotify.Text;
            string[] fLActivities = flowPath.Split(';');
            foreach (string user in this.lstUsersTo.Items)
            {
                if (user.IndexOf('(') != -1)
                    users += user.Substring(0, user.IndexOf('(')) + ":UserId;";
                else
                    users += user + ":UserId;";
            }
            foreach (string role in this.lstRolesTo.Items)
            {
                if (role.IndexOf('(') != -1)
                    roles += role.Substring(0, role.IndexOf('(')) + ";";
                else
                    roles += role + ";";
            }
            object[] objParams = CliUtils.CallFLMethod("Notify", new object[] { new Guid(listId), new object[] { fLActivities[1], fLActivities[1], 0, 0, message, "", provider, 0, users + roles, "" }, new object[] { keys, values } });
            if (Convert.ToInt16(objParams[0]) == 0)
            {
                string sendToIds = users + roles;
                sendToIds = sendToIds.Substring(0, sendToIds.LastIndexOf(';'));
                this.label5.Text = GloFix.ShowNotifyMessage(sendToIds);
                sucess = true;
            }
            else
            {
                if(Convert.ToInt16(objParams[0]) == 2)
                    this.label5.Text = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "FailToNotify");
                sucess = false;
            }
        }

        internal bool sucess = false;
        internal string keys = "";
        internal string values = "";
        internal string provider = "";
        internal string flowPath = "";
        internal string listId = "";
        internal List<string> secUsers = new List<string>();
        internal List<string> secRoles = new List<string>();

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotifyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sucess)
                this.DialogResult = DialogResult.OK;
        }
    }
}