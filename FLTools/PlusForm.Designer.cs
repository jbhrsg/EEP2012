namespace FLTools
{
    partial class PlusForm
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
            this.gbRoles = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchRoleName = new System.Windows.Forms.TextBox();
            this.txtSearchRoleId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRolesLR = new System.Windows.Forms.Button();
            this.btnRolesRL = new System.Windows.Forms.Button();
            this.lstRolesTo = new System.Windows.Forms.ListBox();
            this.lstRolesFrom = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.gbNotify = new System.Windows.Forms.GroupBox();
            this.txtNotify = new System.Windows.Forms.TextBox();
            this.gbRoles.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.gbNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRoles
            // 
            this.gbRoles.Controls.Add(this.btnSearch);
            this.gbRoles.Controls.Add(this.txtSearchRoleName);
            this.gbRoles.Controls.Add(this.txtSearchRoleId);
            this.gbRoles.Controls.Add(this.label5);
            this.gbRoles.Controls.Add(this.label4);
            this.gbRoles.Controls.Add(this.btnRolesLR);
            this.gbRoles.Controls.Add(this.btnRolesRL);
            this.gbRoles.Controls.Add(this.lstRolesTo);
            this.gbRoles.Controls.Add(this.lstRolesFrom);
            this.gbRoles.Controls.Add(this.label2);
            this.gbRoles.Controls.Add(this.label1);
            this.gbRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRoles.Location = new System.Drawing.Point(0, 0);
            this.gbRoles.Name = "gbRoles";
            this.gbRoles.Size = new System.Drawing.Size(443, 218);
            this.gbRoles.TabIndex = 2;
            this.gbRoles.TabStop = false;
            this.gbRoles.Enter += new System.EventHandler(this.gbRoles_Enter);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(363, 174);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 23);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchRoleName
            // 
            this.txtSearchRoleName.Location = new System.Drawing.Point(241, 176);
            this.txtSearchRoleName.Name = "txtSearchRoleName";
            this.txtSearchRoleName.Size = new System.Drawing.Size(116, 21);
            this.txtSearchRoleName.TabIndex = 11;
            // 
            // txtSearchRoleId
            // 
            this.txtSearchRoleId.Location = new System.Drawing.Point(61, 176);
            this.txtSearchRoleId.Name = "txtSearchRoleId";
            this.txtSearchRoleId.Size = new System.Drawing.Size(116, 21);
            this.txtSearchRoleId.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "label4";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnRolesLR
            // 
            this.btnRolesLR.Location = new System.Drawing.Point(194, 73);
            this.btnRolesLR.Name = "btnRolesLR";
            this.btnRolesLR.Size = new System.Drawing.Size(54, 23);
            this.btnRolesLR.TabIndex = 6;
            this.btnRolesLR.Text = ">";
            this.btnRolesLR.UseVisualStyleBackColor = true;
            this.btnRolesLR.Click += new System.EventHandler(this.btnRolesLR_Click);
            // 
            // btnRolesRL
            // 
            this.btnRolesRL.Location = new System.Drawing.Point(194, 111);
            this.btnRolesRL.Name = "btnRolesRL";
            this.btnRolesRL.Size = new System.Drawing.Size(54, 23);
            this.btnRolesRL.TabIndex = 7;
            this.btnRolesRL.Text = "<";
            this.btnRolesRL.UseVisualStyleBackColor = true;
            this.btnRolesRL.Click += new System.EventHandler(this.btnRolesRL_Click);
            // 
            // lstRolesTo
            // 
            this.lstRolesTo.FormattingEnabled = true;
            this.lstRolesTo.ItemHeight = 12;
            this.lstRolesTo.Location = new System.Drawing.Point(260, 34);
            this.lstRolesTo.Name = "lstRolesTo";
            this.lstRolesTo.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstRolesTo.Size = new System.Drawing.Size(177, 136);
            this.lstRolesTo.TabIndex = 3;
            // 
            // lstRolesFrom
            // 
            this.lstRolesFrom.FormattingEnabled = true;
            this.lstRolesFrom.ItemHeight = 12;
            this.lstRolesFrom.Location = new System.Drawing.Point(7, 34);
            this.lstRolesFrom.Name = "lstRolesFrom";
            this.lstRolesFrom.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstRolesFrom.Size = new System.Drawing.Size(177, 136);
            this.lstRolesFrom.TabIndex = 2;
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(257, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(127, 319);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.btnClose);
            this.gbResult.Controls.Add(this.label3);
            this.gbResult.Location = new System.Drawing.Point(10, 348);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(425, 78);
            this.gbResult.TabIndex = 12;
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
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(13, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 50);
            this.label3.TabIndex = 3;
            // 
            // gbNotify
            // 
            this.gbNotify.Controls.Add(this.txtNotify);
            this.gbNotify.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbNotify.Location = new System.Drawing.Point(0, 218);
            this.gbNotify.Name = "gbNotify";
            this.gbNotify.Size = new System.Drawing.Size(443, 95);
            this.gbNotify.TabIndex = 13;
            this.gbNotify.TabStop = false;
            // 
            // txtNotify
            // 
            this.txtNotify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotify.Location = new System.Drawing.Point(3, 17);
            this.txtNotify.Multiline = true;
            this.txtNotify.Name = "txtNotify";
            this.txtNotify.Size = new System.Drawing.Size(437, 75);
            this.txtNotify.TabIndex = 0;
            // 
            // PlusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 433);
            this.Controls.Add(this.gbNotify);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbRoles);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PlusForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlusForm_FormClosing);
            this.gbRoles.ResumeLayout(false);
            this.gbRoles.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.gbNotify.ResumeLayout(false);
            this.gbNotify.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRoles;
        private System.Windows.Forms.Button btnRolesRL;
        private System.Windows.Forms.Button btnRolesLR;
        private System.Windows.Forms.ListBox lstRolesTo;
        private System.Windows.Forms.ListBox lstRolesFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbNotify;
        private System.Windows.Forms.TextBox txtNotify;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearchRoleName;
        private System.Windows.Forms.TextBox txtSearchRoleId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}