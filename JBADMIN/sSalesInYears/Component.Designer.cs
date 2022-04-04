namespace sSalesInYears
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
            this.SalesInYears = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesInYears)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBNJB";
            // 
            // SalesInYears
            // 
            this.SalesInYears.CacheConnection = false;
            this.SalesInYears.CommandText = "SELECT * FROM VIEW_SALESINYEARS ORDER BY 交易金額 DESC";
            this.SalesInYears.CommandTimeout = 30;
            this.SalesInYears.CommandType = System.Data.CommandType.Text;
            this.SalesInYears.DynamicTableName = false;
            this.SalesInYears.EEPAlias = "JBNJB";
            this.SalesInYears.EncodingAfter = null;
            this.SalesInYears.EncodingBefore = "Windows-1252";
            this.SalesInYears.EncodingConvert = null;
            this.SalesInYears.InfoConnection = this.InfoConnection1;
            this.SalesInYears.MultiSetWhere = false;
            this.SalesInYears.Name = "SalesInYears";
            this.SalesInYears.NotificationAutoEnlist = false;
            this.SalesInYears.SecExcept = null;
            this.SalesInYears.SecFieldName = null;
            this.SalesInYears.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesInYears.SelectPaging = false;
            this.SalesInYears.SelectTop = 0;
            this.SalesInYears.SiteControl = false;
            this.SalesInYears.SiteFieldName = null;
            this.SalesInYears.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesInYears)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesInYears;
    }
}
