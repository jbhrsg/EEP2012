namespace FLTools
{
    partial class FLNavigatorVisibleControlsEditorDialog
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
            System.Windows.Forms.Button btnMoveAllFromVisibleToInVisible;
            this.lbxVisibleControls = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbxInVisibleControls = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMoveFromVisibleToInVisible = new System.Windows.Forms.Button();
            this.btnMoveFromInVisibleToVisible = new System.Windows.Forms.Button();
            this.btnMoveAllFromInVisibleToVisible = new System.Windows.Forms.Button();
            btnMoveAllFromVisibleToInVisible = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMoveAllFromVisibleToInVisible
            // 
            btnMoveAllFromVisibleToInVisible.Location = new System.Drawing.Point(239, 54);
            btnMoveAllFromVisibleToInVisible.Name = "btnMoveAllFromVisibleToInVisible";
            btnMoveAllFromVisibleToInVisible.Size = new System.Drawing.Size(45, 25);
            btnMoveAllFromVisibleToInVisible.TabIndex = 6;
            btnMoveAllFromVisibleToInVisible.Text = ">>";
            btnMoveAllFromVisibleToInVisible.UseVisualStyleBackColor = true;
            btnMoveAllFromVisibleToInVisible.Click += new System.EventHandler(this.btnMoveAllFromVisibleToInVisible_Click);
            // 
            // lbxVisibleControls
            // 
            this.lbxVisibleControls.FormattingEnabled = true;
            this.lbxVisibleControls.ItemHeight = 12;
            this.lbxVisibleControls.Location = new System.Drawing.Point(11, 33);
            this.lbxVisibleControls.Name = "lbxVisibleControls";
            this.lbxVisibleControls.Size = new System.Drawing.Size(215, 244);
            this.lbxVisibleControls.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "visible controls";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(321, 297);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(418, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lbxInVisibleControls
            // 
            this.lbxInVisibleControls.FormattingEnabled = true;
            this.lbxInVisibleControls.ItemHeight = 12;
            this.lbxInVisibleControls.Location = new System.Drawing.Point(296, 33);
            this.lbxInVisibleControls.Name = "lbxInVisibleControls";
            this.lbxInVisibleControls.Size = new System.Drawing.Size(215, 244);
            this.lbxInVisibleControls.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "invisible controls";
            // 
            // btnMoveFromVisibleToInVisible
            // 
            this.btnMoveFromVisibleToInVisible.Location = new System.Drawing.Point(239, 85);
            this.btnMoveFromVisibleToInVisible.Name = "btnMoveFromVisibleToInVisible";
            this.btnMoveFromVisibleToInVisible.Size = new System.Drawing.Size(45, 25);
            this.btnMoveFromVisibleToInVisible.TabIndex = 7;
            this.btnMoveFromVisibleToInVisible.Text = ">";
            this.btnMoveFromVisibleToInVisible.UseVisualStyleBackColor = true;
            this.btnMoveFromVisibleToInVisible.Click += new System.EventHandler(this.btnMoveFromVisibleToInVisible_Click);
            // 
            // btnMoveFromInVisibleToVisible
            // 
            this.btnMoveFromInVisibleToVisible.Location = new System.Drawing.Point(239, 137);
            this.btnMoveFromInVisibleToVisible.Name = "btnMoveFromInVisibleToVisible";
            this.btnMoveFromInVisibleToVisible.Size = new System.Drawing.Size(45, 25);
            this.btnMoveFromInVisibleToVisible.TabIndex = 8;
            this.btnMoveFromInVisibleToVisible.Text = "<";
            this.btnMoveFromInVisibleToVisible.UseVisualStyleBackColor = true;
            this.btnMoveFromInVisibleToVisible.Click += new System.EventHandler(this.btnMoveFromInVisibleToVisible_Click);
            // 
            // btnMoveAllFromInVisibleToVisible
            // 
            this.btnMoveAllFromInVisibleToVisible.Location = new System.Drawing.Point(239, 168);
            this.btnMoveAllFromInVisibleToVisible.Name = "btnMoveAllFromInVisibleToVisible";
            this.btnMoveAllFromInVisibleToVisible.Size = new System.Drawing.Size(45, 25);
            this.btnMoveAllFromInVisibleToVisible.TabIndex = 9;
            this.btnMoveAllFromInVisibleToVisible.Text = "<<";
            this.btnMoveAllFromInVisibleToVisible.UseVisualStyleBackColor = true;
            this.btnMoveAllFromInVisibleToVisible.Click += new System.EventHandler(this.btnMoveAllFromInVisibleToVisible_Click);
            // 
            // FLNavigatorVisibleControlsEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 332);
            this.Controls.Add(this.btnMoveAllFromInVisibleToVisible);
            this.Controls.Add(this.btnMoveFromInVisibleToVisible);
            this.Controls.Add(this.btnMoveFromVisibleToInVisible);
            this.Controls.Add(btnMoveAllFromVisibleToInVisible);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbxInVisibleControls);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxVisibleControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FLNavigatorVisibleControlsEditorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visible Controls Editor";
            this.Load += new System.EventHandler(this.VisibleControlsEditorDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxVisibleControls;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lbxInVisibleControls;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMoveFromVisibleToInVisible;
        private System.Windows.Forms.Button btnMoveFromInVisibleToVisible;
        private System.Windows.Forms.Button btnMoveAllFromInVisibleToVisible;
    }
}