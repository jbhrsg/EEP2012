namespace FLDesigner
{
    partial class FormVersion
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxApplication = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelApplication = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBoxSrvtools = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelFLTools = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonAssembly = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplication)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSrvtools)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxApplication);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelApplication);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application:";
            // 
            // pictureBoxApplication
            // 
            this.pictureBoxApplication.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxApplication.Name = "pictureBoxApplication";
            this.pictureBoxApplication.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxApplication.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxApplication.TabIndex = 2;
            this.pictureBoxApplication.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 45);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:\r\nVersion:\r\nDescription:";
            // 
            // labelApplication
            // 
            this.labelApplication.AutoSize = true;
            this.labelApplication.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplication.Location = new System.Drawing.Point(130, 20);
            this.labelApplication.Name = "labelApplication";
            this.labelApplication.Size = new System.Drawing.Size(0, 15);
            this.labelApplication.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBoxSrvtools);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.labelFLTools);
            this.groupBox2.Location = new System.Drawing.Point(10, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 70);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FLTools:";
            // 
            // pictureBoxSrvtools
            // 
            this.pictureBoxSrvtools.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxSrvtools.Name = "pictureBoxSrvtools";
            this.pictureBoxSrvtools.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxSrvtools.TabIndex = 3;
            this.pictureBoxSrvtools.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 45);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name:\r\nVersion:\r\nDescription:";
            // 
            // labelFLTools
            // 
            this.labelFLTools.AutoSize = true;
            this.labelFLTools.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFLTools.Location = new System.Drawing.Point(130, 20);
            this.labelFLTools.Name = "labelFLTools";
            this.labelFLTools.Size = new System.Drawing.Size(0, 15);
            this.labelFLTools.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(205, 162);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonAssembly
            // 
            this.buttonAssembly.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAssembly.Location = new System.Drawing.Point(12, 162);
            this.buttonAssembly.Name = "buttonAssembly";
            this.buttonAssembly.Size = new System.Drawing.Size(170, 23);
            this.buttonAssembly.TabIndex = 3;
            this.buttonAssembly.Text = "View Other Assembly Info";
            this.buttonAssembly.UseVisualStyleBackColor = true;
            this.buttonAssembly.Click += new System.EventHandler(this.buttonAssembly_Click);
            // 
            // FormVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 197);
            this.Controls.Add(this.buttonAssembly);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVersion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormVersion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplication)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSrvtools)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelApplication;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelFLTools;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonAssembly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxApplication;
        private System.Windows.Forms.PictureBox pictureBoxSrvtools;
    }
}