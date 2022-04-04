namespace FLTools
{
    partial class SubmitConfirm
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
            this.components = new System.ComponentModel.Container();
            this.gbContainer = new System.Windows.Forms.GroupBox();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabSuggest = new System.Windows.Forms.TabPage();
            this.btnUploadFiles = new System.Windows.Forms.Button();
            this.gbDownload = new System.Windows.Forms.GroupBox();
            this.cmbRetunStep = new System.Windows.Forms.ComboBox();
            this.lblReturnStep = new System.Windows.Forms.Label();
            this.lblOrg = new System.Windows.Forms.Label();
            this.cmbOrg = new System.Windows.Forms.ComboBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtSuggest = new System.Windows.Forms.TextBox();
            this.tabHis = new System.Windows.Forms.TabPage();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvHis = new System.Windows.Forms.DataGridView();
            this.columnStepId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panCheckBox = new System.Windows.Forms.Panel();
            this.chkImportant = new System.Windows.Forms.CheckBox();
            this.chkUrgent = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.lblSend = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.ttAttachment = new System.Windows.Forms.ToolTip(this.components);
            this.dialogSave = new System.Windows.Forms.SaveFileDialog();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.gbContainer.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.tabSuggest.SuspendLayout();
            this.tabHis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHis)).BeginInit();
            this.panCheckBox.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbContainer
            // 
            this.gbContainer.Controls.Add(this.tabCtrl);
            this.gbContainer.Controls.Add(this.panCheckBox);
            this.gbContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbContainer.Location = new System.Drawing.Point(0, 0);
            this.gbContainer.Name = "gbContainer";
            this.gbContainer.Size = new System.Drawing.Size(503, 381);
            this.gbContainer.TabIndex = 0;
            this.gbContainer.TabStop = false;
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tabSuggest);
            this.tabCtrl.Controls.Add(this.tabHis);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(3, 88);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(497, 290);
            this.tabCtrl.TabIndex = 4;
            // 
            // tabSuggest
            // 
            this.tabSuggest.Controls.Add(this.btnUploadFiles);
            this.tabSuggest.Controls.Add(this.gbDownload);
            this.tabSuggest.Controls.Add(this.cmbRetunStep);
            this.tabSuggest.Controls.Add(this.lblReturnStep);
            this.tabSuggest.Controls.Add(this.lblOrg);
            this.tabSuggest.Controls.Add(this.cmbOrg);
            this.tabSuggest.Controls.Add(this.cmbRole);
            this.tabSuggest.Controls.Add(this.lblRole);
            this.tabSuggest.Controls.Add(this.txtSuggest);
            this.tabSuggest.Location = new System.Drawing.Point(4, 21);
            this.tabSuggest.Name = "tabSuggest";
            this.tabSuggest.Padding = new System.Windows.Forms.Padding(3);
            this.tabSuggest.Size = new System.Drawing.Size(489, 265);
            this.tabSuggest.TabIndex = 0;
            this.tabSuggest.UseVisualStyleBackColor = true;
            // 
            // btnUploadFiles
            // 
            this.btnUploadFiles.Location = new System.Drawing.Point(397, 148);
            this.btnUploadFiles.Name = "btnUploadFiles";
            this.btnUploadFiles.Size = new System.Drawing.Size(86, 23);
            this.btnUploadFiles.TabIndex = 0;
            this.btnUploadFiles.Text = "upload files";
            this.btnUploadFiles.UseVisualStyleBackColor = true;
            this.btnUploadFiles.Click += new System.EventHandler(this.btnUploadFiles_Click);
            // 
            // gbDownload
            // 
            this.gbDownload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbDownload.Location = new System.Drawing.Point(3, 174);
            this.gbDownload.Name = "gbDownload";
            this.gbDownload.Size = new System.Drawing.Size(483, 88);
            this.gbDownload.TabIndex = 9;
            this.gbDownload.TabStop = false;
            this.gbDownload.Text = "download files";
            // 
            // cmbRetunStep
            // 
            this.cmbRetunStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRetunStep.FormattingEnabled = true;
            this.cmbRetunStep.Location = new System.Drawing.Point(119, 148);
            this.cmbRetunStep.Name = "cmbRetunStep";
            this.cmbRetunStep.Size = new System.Drawing.Size(135, 20);
            this.cmbRetunStep.TabIndex = 8;
            // 
            // lblReturnStep
            // 
            this.lblReturnStep.Location = new System.Drawing.Point(2, 146);
            this.lblReturnStep.Name = "lblReturnStep";
            this.lblReturnStep.Size = new System.Drawing.Size(112, 25);
            this.lblReturnStep.TabIndex = 7;
            this.lblReturnStep.Text = "lblReturnStep";
            this.lblReturnStep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrg
            // 
            this.lblOrg.Location = new System.Drawing.Point(260, 121);
            this.lblOrg.Name = "lblOrg";
            this.lblOrg.Size = new System.Drawing.Size(93, 12);
            this.lblOrg.TabIndex = 6;
            this.lblOrg.Text = "lblOrg";
            this.lblOrg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbOrg
            // 
            this.cmbOrg.FormattingEnabled = true;
            this.cmbOrg.Location = new System.Drawing.Point(359, 118);
            this.cmbOrg.Name = "cmbOrg";
            this.cmbOrg.Size = new System.Drawing.Size(124, 20);
            this.cmbOrg.TabIndex = 3;
            // 
            // cmbRole
            // 
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(119, 118);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(135, 20);
            this.cmbRole.TabIndex = 2;
            // 
            // lblRole
            // 
            this.lblRole.Location = new System.Drawing.Point(5, 121);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(108, 17);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "lblRole";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSuggest
            // 
            this.txtSuggest.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSuggest.Location = new System.Drawing.Point(3, 3);
            this.txtSuggest.Multiline = true;
            this.txtSuggest.Name = "txtSuggest";
            this.txtSuggest.Size = new System.Drawing.Size(483, 111);
            this.txtSuggest.TabIndex = 0;
            // 
            // tabHis
            // 
            this.tabHis.Controls.Add(this.txtRemark);
            this.tabHis.Controls.Add(this.label1);
            this.tabHis.Controls.Add(this.dgvHis);
            this.tabHis.Location = new System.Drawing.Point(4, 21);
            this.tabHis.Name = "tabHis";
            this.tabHis.Padding = new System.Windows.Forms.Padding(3);
            this.tabHis.Size = new System.Drawing.Size(489, 265);
            this.tabHis.TabIndex = 1;
            this.tabHis.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.SystemColors.Window;
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Location = new System.Drawing.Point(357, 15);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ReadOnly = true;
            this.txtRemark.Size = new System.Drawing.Size(129, 247);
            this.txtRemark.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(357, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "REMARK";
            // 
            // dgvHis
            // 
            this.dgvHis.AllowUserToAddRows = false;
            this.dgvHis.AllowUserToDeleteRows = false;
            this.dgvHis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnStepId,
            this.columnUserId,
            this.columnUserName,
            this.columnStatus,
            this.columnUpdateDate,
            this.columnUpdateTime});
            this.dgvHis.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvHis.Location = new System.Drawing.Point(3, 3);
            this.dgvHis.Name = "dgvHis";
            this.dgvHis.ReadOnly = true;
            this.dgvHis.RowHeadersVisible = false;
            this.dgvHis.RowTemplate.Height = 23;
            this.dgvHis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHis.Size = new System.Drawing.Size(354, 259);
            this.dgvHis.TabIndex = 0;
            this.dgvHis.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvHis_CellPainting);
            // 
            // columnStepId
            // 
            this.columnStepId.DataPropertyName = "S_STEP_ID";
            this.columnStepId.HeaderText = "S_STEP_ID";
            this.columnStepId.Name = "columnStepId";
            this.columnStepId.ReadOnly = true;
            this.columnStepId.Width = 80;
            // 
            // columnUserId
            // 
            this.columnUserId.DataPropertyName = "USER_ID";
            this.columnUserId.HeaderText = "USER_ID";
            this.columnUserId.Name = "columnUserId";
            this.columnUserId.ReadOnly = true;
            this.columnUserId.Width = 60;
            // 
            // columnUserName
            // 
            this.columnUserName.DataPropertyName = "USERNAME";
            this.columnUserName.HeaderText = "USERNAME";
            this.columnUserName.Name = "columnUserName";
            this.columnUserName.ReadOnly = true;
            this.columnUserName.Width = 70;
            // 
            // columnStatus
            // 
            this.columnStatus.DataPropertyName = "STATUS";
            this.columnStatus.HeaderText = "STATUS";
            this.columnStatus.Name = "columnStatus";
            this.columnStatus.ReadOnly = true;
            this.columnStatus.Width = 60;
            // 
            // columnUpdateDate
            // 
            this.columnUpdateDate.DataPropertyName = "UPDATE_DATE";
            this.columnUpdateDate.HeaderText = "UPDATE_DATE";
            this.columnUpdateDate.Name = "columnUpdateDate";
            this.columnUpdateDate.ReadOnly = true;
            this.columnUpdateDate.Width = 80;
            // 
            // columnUpdateTime
            // 
            this.columnUpdateTime.DataPropertyName = "UPDATE_TIME";
            this.columnUpdateTime.HeaderText = "UPDATE_TIME";
            this.columnUpdateTime.Name = "columnUpdateTime";
            this.columnUpdateTime.ReadOnly = true;
            this.columnUpdateTime.Width = 60;
            // 
            // panCheckBox
            // 
            this.panCheckBox.Controls.Add(this.chkImportant);
            this.panCheckBox.Controls.Add(this.chkUrgent);
            this.panCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.panCheckBox.Location = new System.Drawing.Point(3, 17);
            this.panCheckBox.Name = "panCheckBox";
            this.panCheckBox.Size = new System.Drawing.Size(497, 71);
            this.panCheckBox.TabIndex = 3;
            // 
            // chkImportant
            // 
            this.chkImportant.AutoSize = true;
            this.chkImportant.Location = new System.Drawing.Point(19, 14);
            this.chkImportant.Name = "chkImportant";
            this.chkImportant.Size = new System.Drawing.Size(15, 14);
            this.chkImportant.TabIndex = 0;
            this.chkImportant.UseVisualStyleBackColor = true;
            // 
            // chkUrgent
            // 
            this.chkUrgent.AutoSize = true;
            this.chkUrgent.Location = new System.Drawing.Point(19, 43);
            this.chkUrgent.Name = "chkUrgent";
            this.chkUrgent.Size = new System.Drawing.Size(15, 14);
            this.chkUrgent.TabIndex = 1;
            this.chkUrgent.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(98, 387);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(212, 387);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(55, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(368, 49);
            this.label2.TabIndex = 3;
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.lblSend);
            this.gbResult.Controls.Add(this.btnClose);
            this.gbResult.Controls.Add(this.label2);
            this.gbResult.Location = new System.Drawing.Point(7, 416);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(493, 72);
            this.gbResult.TabIndex = 4;
            this.gbResult.TabStop = false;
            this.gbResult.Visible = false;
            // 
            // lblSend
            // 
            this.lblSend.ForeColor = System.Drawing.Color.Red;
            this.lblSend.Location = new System.Drawing.Point(6, 17);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(49, 49);
            this.lblSend.TabIndex = 5;
            this.lblSend.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(429, 43);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(58, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // buttonPreview
            // 
            this.buttonPreview.Location = new System.Drawing.Point(326, 387);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(75, 23);
            this.buttonPreview.TabIndex = 5;
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // SubmitConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 490);
            this.Controls.Add(this.buttonPreview);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubmitConfirm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubmitConfirm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubmitConfirm_FormClosing);
            this.Load += new System.EventHandler(this.SubmitConfirm_Load);
            this.gbContainer.ResumeLayout(false);
            this.tabCtrl.ResumeLayout(false);
            this.tabSuggest.ResumeLayout(false);
            this.tabSuggest.PerformLayout();
            this.tabHis.ResumeLayout(false);
            this.tabHis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHis)).EndInit();
            this.panCheckBox.ResumeLayout(false);
            this.panCheckBox.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbContainer;
        private System.Windows.Forms.CheckBox chkUrgent;
        private System.Windows.Forms.CheckBox chkImportant;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabSuggest;
        private System.Windows.Forms.TabPage tabHis;
        private System.Windows.Forms.Panel panCheckBox;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvHis;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.Label lblOrg;
        private System.Windows.Forms.ComboBox cmbOrg;
        private System.Windows.Forms.ComboBox cmbRetunStep;
        private System.Windows.Forms.Label lblReturnStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnStepId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUpdateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUpdateTime;
        private System.Windows.Forms.GroupBox gbDownload;
        private System.Windows.Forms.Button btnUploadFiles;
        private System.Windows.Forms.ToolTip ttAttachment;
        private System.Windows.Forms.SaveFileDialog dialogSave;
        private System.Windows.Forms.Button buttonPreview;
        public System.Windows.Forms.TextBox txtSuggest;
    }
}