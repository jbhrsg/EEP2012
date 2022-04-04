namespace FLTools
{
    partial class Comment
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
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvHis = new System.Windows.Forms.DataGridView();
            this.columnStepId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dialogSave = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHis)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.SystemColors.Window;
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Location = new System.Drawing.Point(494, 12);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ReadOnly = true;
            this.txtRemark.Size = new System.Drawing.Size(193, 309);
            this.txtRemark.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(494, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
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
            this.dgvHis.Location = new System.Drawing.Point(0, 0);
            this.dgvHis.Name = "dgvHis";
            this.dgvHis.ReadOnly = true;
            this.dgvHis.RowHeadersVisible = false;
            this.dgvHis.RowTemplate.Height = 23;
            this.dgvHis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHis.Size = new System.Drawing.Size(494, 321);
            this.dgvHis.TabIndex = 3;
            this.dgvHis.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvHis_CellPainting);
            // 
            // columnStepId
            // 
            this.columnStepId.DataPropertyName = "S_STEP_ID";
            this.columnStepId.HeaderText = "S_STEP_ID";
            this.columnStepId.Name = "columnStepId";
            this.columnStepId.ReadOnly = true;
            // 
            // columnUserId
            // 
            this.columnUserId.DataPropertyName = "USER_ID";
            this.columnUserId.HeaderText = "USER_ID";
            this.columnUserId.Name = "columnUserId";
            this.columnUserId.ReadOnly = true;
            this.columnUserId.Width = 80;
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
            this.columnUpdateTime.Width = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 321);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "attachments";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtRemark);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dgvHis);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 321);
            this.panel1.TabIndex = 7;
            // 
            // Comment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 421);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Comment";
            this.Text = "Comment";
            this.Load += new System.EventHandler(this.Comment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHis)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvHis;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnStepId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUpdateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUpdateTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SaveFileDialog dialogSave;
    }
}