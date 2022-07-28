namespace sMedia_Report_CustomerGroupSalesSummary
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
            this.ERPSalesMaster = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesMaster)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalesMaster
            // 
            this.ERPSalesMaster.CacheConnection = false;
            this.ERPSalesMaster.CommandText = "select \'\' as CustTelNO\r\n ,\'\' as SalesID\r\n ,\'\' as SalesDate\r\n ,\'\' as SalesTypeID\r\n" +
    ",\'\' as DMTypeID\r\n,\'\' as CustAreaID\r\n,\'\' as ReportType";
            this.ERPSalesMaster.CommandTimeout = 30;
            this.ERPSalesMaster.CommandType = System.Data.CommandType.Text;
            this.ERPSalesMaster.DynamicTableName = false;
            this.ERPSalesMaster.EEPAlias = null;
            this.ERPSalesMaster.EncodingAfter = null;
            this.ERPSalesMaster.EncodingBefore = "Windows-1252";
            this.ERPSalesMaster.EncodingConvert = null;
            this.ERPSalesMaster.InfoConnection = this.InfoConnection1;
            this.ERPSalesMaster.MultiSetWhere = false;
            this.ERPSalesMaster.Name = "ERPSalesMaster";
            this.ERPSalesMaster.NotificationAutoEnlist = false;
            this.ERPSalesMaster.SecExcept = null;
            this.ERPSalesMaster.SecFieldName = null;
            this.ERPSalesMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesMaster.SelectPaging = false;
            this.ERPSalesMaster.SelectTop = 0;
            this.ERPSalesMaster.SiteControl = false;
            this.ERPSalesMaster.SiteFieldName = null;
            this.ERPSalesMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesMaster)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesMaster;
    }
}
