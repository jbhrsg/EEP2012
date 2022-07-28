namespace sFWCRMOrdersPeopleCount
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
            Srvtools.Service service1 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager();
            this.InfoConnection1 = new Srvtools.InfoConnection();
            this.FWCRMOrders = new Srvtools.InfoCommand();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrders)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "ReportOrdersPeopleCount";
            service1.NonLogin = false;
            service1.ServiceName = "ReportOrdersPeopleCount";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMOrders
            // 
            this.FWCRMOrders.CacheConnection = false;
            this.FWCRMOrders.CommandText = resources.GetString("FWCRMOrders.CommandText");
            this.FWCRMOrders.CommandTimeout = 30;
            this.FWCRMOrders.CommandType = System.Data.CommandType.Text;
            this.FWCRMOrders.DynamicTableName = false;
            this.FWCRMOrders.EEPAlias = null;
            this.FWCRMOrders.EncodingAfter = null;
            this.FWCRMOrders.EncodingBefore = "Windows-1252";
            this.FWCRMOrders.EncodingConvert = null;
            this.FWCRMOrders.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "OrderNo";
            this.FWCRMOrders.KeyFields.Add(keyItem1);
            this.FWCRMOrders.MultiSetWhere = false;
            this.FWCRMOrders.Name = "FWCRMOrders";
            this.FWCRMOrders.NotificationAutoEnlist = false;
            this.FWCRMOrders.SecExcept = null;
            this.FWCRMOrders.SecFieldName = null;
            this.FWCRMOrders.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMOrders.SelectPaging = false;
            this.FWCRMOrders.SelectTop = 0;
            this.FWCRMOrders.SiteControl = false;
            this.FWCRMOrders.SiteFieldName = null;
            this.FWCRMOrders.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrders)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMOrders;
    }
}
