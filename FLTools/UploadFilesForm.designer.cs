namespace FLTools
{
    partial class UploadFilesForm
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
            this.dialogFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lstUploadFiles = new System.Windows.Forms.ListBox();
            this.gbExistFiles = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gbExistFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogFile
            // 
            this.dialogFile.Multiselect = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpload);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.lstUploadFiles);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 210);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "add files";
            // 
            // btnUpload
            // 
            this.btnUpload.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUpload.Location = new System.Drawing.Point(304, 58);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(69, 23);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(304, 17);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(69, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lstUploadFiles
            // 
            this.lstUploadFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstUploadFiles.FormattingEnabled = true;
            this.lstUploadFiles.HorizontalScrollbar = true;
            this.lstUploadFiles.ItemHeight = 12;
            this.lstUploadFiles.Location = new System.Drawing.Point(3, 17);
            this.lstUploadFiles.Name = "lstUploadFiles";
            this.lstUploadFiles.Size = new System.Drawing.Size(290, 184);
            this.lstUploadFiles.TabIndex = 0;
            // 
            // gbExistFiles
            // 
            this.gbExistFiles.Controls.Add(this.btnDelete);
            this.gbExistFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbExistFiles.Location = new System.Drawing.Point(0, 210);
            this.gbExistFiles.Name = "gbExistFiles";
            this.gbExistFiles.Size = new System.Drawing.Size(385, 145);
            this.gbExistFiles.TabIndex = 1;
            this.gbExistFiles.TabStop = false;
            this.gbExistFiles.Text = "files";
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDelete.Location = new System.Drawing.Point(310, 116);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // UploadFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 355);
            this.Controls.Add(this.gbExistFiles);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadFilesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "upload files";
            this.Load += new System.EventHandler(this.UploadFilesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.gbExistFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dialogFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ListBox lstUploadFiles;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.GroupBox gbExistFiles;
        private System.Windows.Forms.Button btnDelete;
    }
}