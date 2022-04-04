namespace FLTools.Base
{
    partial class SelectFNForm
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
            this.btnChooseDll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientDll = new System.Windows.Forms.TextBox();
            this.lbxFormNames = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChooseDll
            // 
            this.btnChooseDll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseDll.Location = new System.Drawing.Point(248, 27);
            this.btnChooseDll.Name = "btnChooseDll";
            this.btnChooseDll.Size = new System.Drawing.Size(32, 23);
            this.btnChooseDll.TabIndex = 0;
            this.btnChooseDll.Text = "...";
            this.btnChooseDll.UseVisualStyleBackColor = true;
            this.btnChooseDll.Click += new System.EventHandler(this.btnChooseDll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose dll:";
            // 
            // txtClientDll
            // 
            this.txtClientDll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientDll.Location = new System.Drawing.Point(12, 29);
            this.txtClientDll.Name = "txtClientDll";
            this.txtClientDll.Size = new System.Drawing.Size(230, 21);
            this.txtClientDll.TabIndex = 2;
            this.txtClientDll.TextChanged += new System.EventHandler(this.txtClientDll_TextChanged);
            // 
            // lbxFormNames
            // 
            this.lbxFormNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxFormNames.FormattingEnabled = true;
            this.lbxFormNames.ItemHeight = 12;
            this.lbxFormNames.Location = new System.Drawing.Point(12, 72);
            this.lbxFormNames.Name = "lbxFormNames";
            this.lbxFormNames.Size = new System.Drawing.Size(268, 160);
            this.lbxFormNames.TabIndex = 3;
            this.lbxFormNames.DoubleClick += new System.EventHandler(this.lbxFormNames_DoubleClick);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(137, 243);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(216, 243);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SelectFNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 280);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbxFormNames);
            this.Controls.Add(this.txtClientDll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChooseDll);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectFNForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select FormName";
            this.Load += new System.EventHandler(this.SelectFNForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChooseDll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClientDll;
        private System.Windows.Forms.ListBox lbxFormNames;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
    }
}