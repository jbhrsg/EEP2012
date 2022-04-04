namespace FLTools
{
    partial class NotifyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbUsers = new System.Windows.Forms.GroupBox();
            this.btnUsersRL = new System.Windows.Forms.Button();
            this.btnUsersLR = new System.Windows.Forms.Button();
            this.lstUsersTo = new System.Windows.Forms.ListBox();
            this.lstUsersFrom = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbRoles = new System.Windows.Forms.GroupBox();
            this.btnRolesRL = new System.Windows.Forms.Button();
            this.btnRolesLR = new System.Windows.Forms.Button();
            this.lstRolesTo = new System.Windows.Forms.ListBox();
            this.lstRolesFrom = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbNotify = new System.Windows.Forms.GroupBox();
            this.txtNotify = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.gbUsers.SuspendLayout();
            this.gbRoles.SuspendLayout();
            this.gbNotify.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUsers
            // 
            this.gbUsers.Controls.Add(this.btnUsersRL);
            this.gbUsers.Controls.Add(this.btnUsersLR);
            this.gbUsers.Controls.Add(this.lstUsersTo);
            this.gbUsers.Controls.Add(this.lstUsersFrom);
            this.gbUsers.Controls.Add(this.label2);
            this.gbUsers.Controls.Add(this.label1);
            this.gbUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbUsers.Location = new System.Drawing.Point(0, 0);
            this.gbUsers.Name = "gbUsers";
            this.gbUsers.Size = new System.Drawing.Size(443, 134);
            this.gbUsers.TabIndex = 0;
            this.gbUsers.TabStop = false;
            // 
            // btnUsersRL
            // 
            this.btnUsersRL.Location = new System.Drawing.Point(195, 92);
            this.btnUsersRL.Name = "btnUsersRL";
            this.btnUsersRL.Size = new System.Drawing.Size(54, 23);
            this.btnUsersRL.TabIndex = 5;
            this.btnUsersRL.Text = "<";
            this.btnUsersRL.UseVisualStyleBackColor = true;
            this.btnUsersRL.Click += new System.EventHandler(this.btnUsersRL_Click);
            // 
            // btnUsersLR
            // 
            this.btnUsersLR.Location = new System.Drawing.Point(195, 47);
            this.btnUsersLR.Name = "btnUsersLR";
            this.btnUsersLR.Size = new System.Drawing.Size(54, 23);
            this.btnUsersLR.TabIndex = 4;
            this.btnUsersLR.Text = ">";
            this.btnUsersLR.UseVisualStyleBackColor = true;
            this.btnUsersLR.Click += new System.EventHandler(this.btnUsersLR_Click);
            // 
            // lstUsersTo
            // 
            this.lstUsersTo.FormattingEnabled = true;
            this.lstUsersTo.ItemHeight = 12;
            this.lstUsersTo.Location = new System.Drawing.Point(260, 38);
            this.lstUsersTo.Name = "lstUsersTo";
            this.lstUsersTo.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUsersTo.Size = new System.Drawing.Size(177, 88);
            this.lstUsersTo.TabIndex = 3;
            // 
            // lstUsersFrom
            // 
            this.lstUsersFrom.FormattingEnabled = true;
            this.lstUsersFrom.ItemHeight = 12;
            this.lstUsersFrom.Location = new System.Drawing.Point(7, 38);
            this.lstUsersFrom.MultiColumn = true;
            this.lstUsersFrom.Name = "lstUsersFrom";
            this.lstUsersFrom.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUsersFrom.Size = new System.Drawing.Size(177, 88);
            this.lstUsersFrom.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // gbRoles
            // 
            this.gbRoles.Controls.Add(this.btnRolesRL);
            this.gbRoles.Controls.Add(this.btnRolesLR);
            this.gbRoles.Controls.Add(this.lstRolesTo);
            this.gbRoles.Controls.Add(this.lstRolesFrom);
            this.gbRoles.Controls.Add(this.label3);
            this.gbRoles.Controls.Add(this.label4);
            this.gbRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRoles.Location = new System.Drawing.Point(0, 134);
            this.gbRoles.Name = "gbRoles";
            this.gbRoles.Size = new System.Drawing.Size(443, 130);
            this.gbRoles.TabIndex = 1;
            this.gbRoles.TabStop = false;
            // 
            // btnRolesRL
            // 
            this.btnRolesRL.Location = new System.Drawing.Point(195, 86);
            this.btnRolesRL.Name = "btnRolesRL";
            this.btnRolesRL.Size = new System.Drawing.Size(54, 23);
            this.btnRolesRL.TabIndex = 7;
            this.btnRolesRL.Text = "<";
            this.btnRolesRL.UseVisualStyleBackColor = true;
            this.btnRolesRL.Click += new System.EventHandler(this.btnRolesRL_Click);
            // 
            // btnRolesLR
            // 
            this.btnRolesLR.Location = new System.Drawing.Point(195, 48);
            this.btnRolesLR.Name = "btnRolesLR";
            this.btnRolesLR.Size = new System.Drawing.Size(54, 23);
            this.btnRolesLR.TabIndex = 6;
            this.btnRolesLR.Text = ">";
            this.btnRolesLR.UseVisualStyleBackColor = true;
            this.btnRolesLR.Click += new System.EventHandler(this.btnRolesLR_Click);
            // 
            // lstRolesTo
            // 
            this.lstRolesTo.FormattingEnabled = true;
            this.lstRolesTo.ItemHeight = 12;
            this.lstRolesTo.Location = new System.Drawing.Point(260, 34);
            this.lstRolesTo.Name = "lstRolesTo";
            this.lstRolesTo.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstRolesTo.Size = new System.Drawing.Size(177, 88);
            this.lstRolesTo.TabIndex = 3;
            // 
            // lstRolesFrom
            // 
            this.lstRolesFrom.FormattingEnabled = true;
            this.lstRolesFrom.ItemHeight = 12;
            this.lstRolesFrom.Location = new System.Drawing.Point(7, 34);
            this.lstRolesFrom.Name = "lstRolesFrom";
            this.lstRolesFrom.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstRolesFrom.Size = new System.Drawing.Size(177, 88);
            this.lstRolesFrom.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 0;
            // 
            // gbNotify
            // 
            this.gbNotify.Controls.Add(this.txtNotify);
            this.gbNotify.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbNotify.Location = new System.Drawing.Point(0, 264);
            this.gbNotify.Name = "gbNotify";
            this.gbNotify.Size = new System.Drawing.Size(443, 95);
            this.gbNotify.TabIndex = 2;
            this.gbNotify.TabStop = false;
            // 
            // txtNotify
            // 
            this.txtNotify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotify.Location = new System.Drawing.Point(3, 18);
            this.txtNotify.Multiline = true;
            this.txtNotify.Name = "txtNotify";
            this.txtNotify.Size = new System.Drawing.Size(437, 74);
            this.txtNotify.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(245, 362);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(115, 362);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.btnClose);
            this.gbResult.Controls.Add(this.label5);
            this.gbResult.Location = new System.Drawing.Point(7, 391);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(425, 78);
            this.gbResult.TabIndex = 10;
            this.gbResult.TabStop = false;
            this.gbResult.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(340, 45);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(13, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(321, 50);
            this.label5.TabIndex = 3;
            // 
            // NotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 472);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbNotify);
            this.Controls.Add(this.gbRoles);
            this.Controls.Add(this.gbUsers);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotifyForm_FormClosing);
            this.Load += new System.EventHandler(this.NotifyForm_Load);
            this.gbUsers.ResumeLayout(false);
            this.gbUsers.PerformLayout();
            this.gbRoles.ResumeLayout(false);
            this.gbRoles.PerformLayout();
            this.gbNotify.ResumeLayout(false);
            this.gbNotify.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUsers;
        private System.Windows.Forms.ListBox lstUsersTo;
        private System.Windows.Forms.ListBox lstUsersFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbRoles;
        private System.Windows.Forms.ListBox lstRolesTo;
        private System.Windows.Forms.ListBox lstRolesFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUsersRL;
        private System.Windows.Forms.Button btnUsersLR;
        private System.Windows.Forms.Button btnRolesRL;
        private System.Windows.Forms.Button btnRolesLR;
        private System.Windows.Forms.GroupBox gbNotify;
        private System.Windows.Forms.TextBox txtNotify;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label5;
    }
}