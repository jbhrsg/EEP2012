namespace s0800PublishingJob
{
    partial class Component
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.cmdPublishingJob = new Srvtools.InfoCommand(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cmdPublishingJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            // 
            // cmdPublishingJob
            // 
            this.cmdPublishingJob.CacheConnection = false;
            this.cmdPublishingJob.CommandText = "select * from PublishingJob";
            this.cmdPublishingJob.CommandTimeout = 30;
            this.cmdPublishingJob.CommandType = System.Data.CommandType.Text;
            this.cmdPublishingJob.DynamicTableName = false;
            this.cmdPublishingJob.EEPAlias = "JB0800";
            this.cmdPublishingJob.EncodingAfter = null;
            this.cmdPublishingJob.EncodingBefore = "Windows-1252";
            this.cmdPublishingJob.EncodingConvert = null;
            this.cmdPublishingJob.InfoConnection = this.InfoConnection1;
            this.cmdPublishingJob.MultiSetWhere = false;
            this.cmdPublishingJob.Name = "cmdPublishingJob";
            this.cmdPublishingJob.NotificationAutoEnlist = false;
            this.cmdPublishingJob.SecExcept = null;
            this.cmdPublishingJob.SecFieldName = null;
            this.cmdPublishingJob.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.cmdPublishingJob.SelectPaging = false;
            this.cmdPublishingJob.SelectTop = 0;
            this.cmdPublishingJob.SiteControl = false;
            this.cmdPublishingJob.SiteFieldName = null;
            this.cmdPublishingJob.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JB0800";
            ((System.ComponentModel.ISupportInitialize)(this.cmdPublishingJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoCommand cmdPublishingJob;
        private Srvtools.InfoConnection InfoConnection1;
    }
}
