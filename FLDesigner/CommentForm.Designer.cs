namespace FLDesigner
{
    partial class CommentForm
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
            this.gvComment = new System.Windows.Forms.DataGridView();
            this.FLOW_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_STEP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORM_PRESENT_CT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GROUPNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvComment)).BeginInit();
            this.SuspendLayout();
            // 
            // gvComment
            // 
            this.gvComment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvComment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FLOW_DESC,
            this.S_STEP_ID,
            this.FORM_PRESENT_CT,
            this.GROUPNAME,
            this.REMARK,
            this.DateTime,
            this.STATUS});
            this.gvComment.Location = new System.Drawing.Point(12, 10);
            this.gvComment.Name = "gvComment";
            this.gvComment.RowTemplate.Height = 23;
            this.gvComment.Size = new System.Drawing.Size(610, 431);
            this.gvComment.TabIndex = 0;
            // 
            // FLOW_DESC
            // 
            this.FLOW_DESC.DataPropertyName = "FLOW_DESC";
            this.FLOW_DESC.HeaderText = "FLOW_DESC";
            this.FLOW_DESC.Name = "FLOW_DESC";
            // 
            // S_STEP_ID
            // 
            this.S_STEP_ID.DataPropertyName = "S_STEP_ID";
            this.S_STEP_ID.HeaderText = "S_STEP_ID";
            this.S_STEP_ID.Name = "S_STEP_ID";
            // 
            // FORM_PRESENT_CT
            // 
            this.FORM_PRESENT_CT.DataPropertyName = "FORM_PRESENT_CT";
            this.FORM_PRESENT_CT.HeaderText = "FORM_PRESENT_CT";
            this.FORM_PRESENT_CT.Name = "FORM_PRESENT_CT";
            // 
            // GROUPNAME
            // 
            this.GROUPNAME.DataPropertyName = "GROUPNAME";
            this.GROUPNAME.HeaderText = "GROUPNAME";
            this.GROUPNAME.Name = "GROUPNAME";
            // 
            // REMARK
            // 
            this.REMARK.DataPropertyName = "REMARK";
            this.REMARK.HeaderText = "REMARK";
            this.REMARK.Name = "REMARK";
            // 
            // DateTime
            // 
            this.DateTime.DataPropertyName = "DATETIME";
            this.DateTime.HeaderText = "DATETIME";
            this.DateTime.Name = "DateTime";
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.Name = "STATUS";
            // 
            // CommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 455);
            this.Controls.Add(this.gvComment);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CommentForm";
            this.Load += new System.EventHandler(this.CommentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvComment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLOW_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_STEP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORM_PRESENT_CT;
        private System.Windows.Forms.DataGridViewTextBoxColumn GROUPNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARK;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
    }
}