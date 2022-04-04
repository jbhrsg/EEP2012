namespace EEPNetFLClient
{
    partial class FLowDesingerForm
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
            this.desinger = new FLDesignerCore.FLDesigner();
            this.SuspendLayout();
            // 
            // desinger
            // 
            this.desinger._ContextMenu = null;
            this.desinger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.desinger.Location = new System.Drawing.Point(0, 0);
            this.desinger.Name = "desinger";
            this.desinger.Size = new System.Drawing.Size(600, 556);
            this.desinger.TabIndex = 2;
            this.desinger._DoubleClick += new FLDesignerCore.FLDesigner._DoubleClickEventHandler(this.desinger__DoubleClick);
            // 
            // FLowDesingerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 556);
            this.Controls.Add(this.desinger);
            this.Name = "FLowDesingerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FLowDesingerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FLowDesingerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FLDesignerCore.FLDesigner desinger;
    }
}