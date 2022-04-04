namespace sMediaPostCount
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
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.PeriodArea = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodArea)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // PeriodArea
            // 
            this.PeriodArea.CacheConnection = false;
            this.PeriodArea.CommandText = "SELECT  AreaName,Sum(Qty) AS QTY\r\nFROM View_MediaPeriodArea\r\nGROUP BY AreaName";
            this.PeriodArea.CommandTimeout = 30;
            this.PeriodArea.CommandType = System.Data.CommandType.Text;
            this.PeriodArea.DynamicTableName = false;
            this.PeriodArea.EEPAlias = null;
            this.PeriodArea.EncodingAfter = null;
            this.PeriodArea.EncodingBefore = "Windows-1252";
            this.PeriodArea.EncodingConvert = null;
            this.PeriodArea.InfoConnection = this.InfoConnection1;
            this.PeriodArea.MultiSetWhere = false;
            this.PeriodArea.Name = "PeriodArea";
            this.PeriodArea.NotificationAutoEnlist = false;
            this.PeriodArea.SecExcept = null;
            this.PeriodArea.SecFieldName = null;
            this.PeriodArea.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PeriodArea.SelectPaging = false;
            this.PeriodArea.SelectTop = 0;
            this.PeriodArea.SiteControl = false;
            this.PeriodArea.SiteFieldName = null;
            this.PeriodArea.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodArea)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand PeriodArea;
    }
}
