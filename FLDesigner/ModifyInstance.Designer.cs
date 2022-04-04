namespace FLDesigner
{
    partial class ModifyInstance
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
            this.btnModify = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.gvInstances = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemComment = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemForward = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBack = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReject = new System.Windows.Forms.ToolStripMenuItem();
            this.txtXomlFile = new System.Windows.Forms.TextBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDescription = new System.Windows.Forms.ComboBox();
            this.Modify = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LISTID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLOW_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_STEP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USERNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORM_PRESENT_CT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SENDTO_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLOWPATH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORM_KEYS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORM_PRESENTATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvInstances)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModify.Location = new System.Drawing.Point(15, 499);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Xoml File:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "*.xoml|*.xoml";
            // 
            // gvInstances
            // 
            this.gvInstances.AllowUserToAddRows = false;
            this.gvInstances.AllowUserToDeleteRows = false;
            this.gvInstances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvInstances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvInstances.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Modify,
            this.LISTID,
            this.DateTime,
            this.FLOW_DESC,
            this.D_STEP_ID,
            this.USERNAME,
            this.FORM_PRESENT_CT,
            this.SENDTO_NAME,
            this.REMARK,
            this.FLOWPATH,
            this.STATUS,
            this.FORM_KEYS,
            this.FORM_PRESENTATION});
            this.gvInstances.ContextMenuStrip = this.contextMenuStrip1;
            this.gvInstances.Location = new System.Drawing.Point(15, 97);
            this.gvInstances.Name = "gvInstances";
            this.gvInstances.RowTemplate.Height = 23;
            this.gvInstances.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvInstances.Size = new System.Drawing.Size(701, 376);
            this.gvInstances.TabIndex = 12;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemComment,
            this.menuItemForward,
            this.menuItemBack,
            this.menuItemReject});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(120, 92);
            // 
            // menuItemComment
            // 
            this.menuItemComment.Name = "menuItemComment";
            this.menuItemComment.Size = new System.Drawing.Size(119, 22);
            this.menuItemComment.Text = "Comment";
            this.menuItemComment.Click += new System.EventHandler(this.menuItemComment_Click);
            // 
            // menuItemForward
            // 
            this.menuItemForward.Name = "menuItemForward";
            this.menuItemForward.Size = new System.Drawing.Size(119, 22);
            this.menuItemForward.Text = "Forward";
            this.menuItemForward.Click += new System.EventHandler(this.menuItemForward_Click);
            // 
            // menuItemBack
            // 
            this.menuItemBack.Name = "menuItemBack";
            this.menuItemBack.Size = new System.Drawing.Size(119, 22);
            this.menuItemBack.Text = "Back";
            this.menuItemBack.Click += new System.EventHandler(this.menuItemBack_Click);
            // 
            // menuItemReject
            // 
            this.menuItemReject.Name = "menuItemReject";
            this.menuItemReject.Size = new System.Drawing.Size(119, 22);
            this.menuItemReject.Text = "Reject";
            this.menuItemReject.Click += new System.EventHandler(this.menuItemReject_Click);
            // 
            // txtXomlFile
            // 
            this.txtXomlFile.Location = new System.Drawing.Point(96, 16);
            this.txtXomlFile.Name = "txtXomlFile";
            this.txtXomlFile.ReadOnly = true;
            this.txtXomlFile.Size = new System.Drawing.Size(347, 21);
            this.txtXomlFile.TabIndex = 3;
            // 
            // btnForward
            // 
            this.btnForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnForward.Location = new System.Drawing.Point(96, 499);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 23);
            this.btnForward.TabIndex = 13;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(178, 499);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 14;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnReject
            // 
            this.btnReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReject.Location = new System.Drawing.Point(260, 499);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 23);
            this.btnReject.TabIndex = 15;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(449, 52);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "Description:";
            // 
            // cmbDescription
            // 
            this.cmbDescription.FormattingEnabled = true;
            this.cmbDescription.Location = new System.Drawing.Point(96, 55);
            this.cmbDescription.Name = "cmbDescription";
            this.cmbDescription.Size = new System.Drawing.Size(347, 20);
            this.cmbDescription.TabIndex = 19;
            // 
            // Modify
            // 
            this.Modify.Frozen = true;
            this.Modify.HeaderText = "";
            this.Modify.Name = "Modify";
            this.Modify.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Modify.Width = 50;
            // 
            // LISTID
            // 
            this.LISTID.DataPropertyName = "LISTID";
            this.LISTID.HeaderText = "LISTID";
            this.LISTID.Name = "LISTID";
            this.LISTID.Width = 250;
            // 
            // DateTime
            // 
            this.DateTime.DataPropertyName = "DATETIME";
            this.DateTime.HeaderText = "DATETIME";
            this.DateTime.Name = "DateTime";
            this.DateTime.Width = 150;
            // 
            // FLOW_DESC
            // 
            this.FLOW_DESC.DataPropertyName = "FLOW_DESC";
            this.FLOW_DESC.HeaderText = "FLOW_DESC";
            this.FLOW_DESC.Name = "FLOW_DESC";
            // 
            // D_STEP_ID
            // 
            this.D_STEP_ID.DataPropertyName = "D_STEP_ID";
            this.D_STEP_ID.HeaderText = "D_STEP_ID";
            this.D_STEP_ID.Name = "D_STEP_ID";
            // 
            // USERNAME
            // 
            this.USERNAME.DataPropertyName = "USERNAME";
            this.USERNAME.HeaderText = "USERNAME";
            this.USERNAME.Name = "USERNAME";
            // 
            // FORM_PRESENT_CT
            // 
            this.FORM_PRESENT_CT.DataPropertyName = "FORM_PRESENT_CT";
            this.FORM_PRESENT_CT.HeaderText = "FORM_PRESENT_CT";
            this.FORM_PRESENT_CT.Name = "FORM_PRESENT_CT";
            this.FORM_PRESENT_CT.Width = 200;
            // 
            // SENDTO_NAME
            // 
            this.SENDTO_NAME.DataPropertyName = "SENDTO_NAME";
            this.SENDTO_NAME.HeaderText = "SENDTO_NAME";
            this.SENDTO_NAME.Name = "SENDTO_NAME";
            // 
            // REMARK
            // 
            this.REMARK.DataPropertyName = "REMARK";
            this.REMARK.HeaderText = "REMARK";
            this.REMARK.Name = "REMARK";
            // 
            // FLOWPATH
            // 
            this.FLOWPATH.DataPropertyName = "FLOWPATH";
            this.FLOWPATH.HeaderText = "FLOWPATH";
            this.FLOWPATH.Name = "FLOWPATH";
            this.FLOWPATH.Visible = false;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.Name = "STATUS";
            this.STATUS.Visible = false;
            // 
            // FORM_KEYS
            // 
            this.FORM_KEYS.DataPropertyName = "FORM_KEYS";
            this.FORM_KEYS.HeaderText = "FORM_KEYS";
            this.FORM_KEYS.Name = "FORM_KEYS";
            this.FORM_KEYS.Visible = false;
            // 
            // FORM_PRESENTATION
            // 
            this.FORM_PRESENTATION.DataPropertyName = "FORM_PRESENTATION";
            this.FORM_PRESENTATION.HeaderText = "FORM_PRESENTATION";
            this.FORM_PRESENTATION.Name = "FORM_PRESENTATION";
            this.FORM_PRESENTATION.Visible = false;
            // 
            // ModifyInstance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 545);
            this.Controls.Add(this.cmbDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gvInstances);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.txtXomlFile);
            this.Controls.Add(this.btnModify);
            this.Name = "ModifyInstance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modify Instance";
            this.Load += new System.EventHandler(this.Instance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvInstances)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtXomlFile;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemComment;
        private System.Windows.Forms.ToolStripMenuItem menuItemForward;
        private System.Windows.Forms.ToolStripMenuItem menuItemBack;
        private System.Windows.Forms.ToolStripMenuItem menuItemReject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDescription;
        private System.Windows.Forms.DataGridView gvInstances;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Modify;
        private System.Windows.Forms.DataGridViewTextBoxColumn LISTID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLOW_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_STEP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn USERNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORM_PRESENT_CT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SENDTO_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARK;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLOWPATH;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORM_KEYS;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORM_PRESENTATION;

    }
}