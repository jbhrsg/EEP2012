namespace sERP_Report_InvoiceDetails_WarrantSummary
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
            Srvtools.Service service1 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.InvoiceDetails = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetReportInvoiceDetails";
            service1.NonLogin = false;
            service1.ServiceName = "GetReportInvoiceDetails";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // InvoiceDetails
            // 
            this.InvoiceDetails.CacheConnection = false;
            this.InvoiceDetails.CommandText = resources.GetString("InvoiceDetails.CommandText");
            this.InvoiceDetails.CommandTimeout = 30;
            this.InvoiceDetails.CommandType = System.Data.CommandType.Text;
            this.InvoiceDetails.DynamicTableName = false;
            this.InvoiceDetails.EEPAlias = "JBERP";
            this.InvoiceDetails.EncodingAfter = null;
            this.InvoiceDetails.EncodingBefore = "Windows-1252";
            this.InvoiceDetails.EncodingConvert = null;
            this.InvoiceDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "InvoiceNO";
            this.InvoiceDetails.KeyFields.Add(keyItem1);
            this.InvoiceDetails.MultiSetWhere = false;
            this.InvoiceDetails.Name = "InvoiceDetails";
            this.InvoiceDetails.NotificationAutoEnlist = false;
            this.InvoiceDetails.SecExcept = null;
            this.InvoiceDetails.SecFieldName = null;
            this.InvoiceDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InvoiceDetails.SelectPaging = false;
            this.InvoiceDetails.SelectTop = 0;
            this.InvoiceDetails.SiteControl = false;
            this.InvoiceDetails.SiteFieldName = null;
            this.InvoiceDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "select InsGroupID,ShortName from InsGroup where IsActive=1";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = "JBADMIN";
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "InsGroupID";
            this.InsGroup.KeyFields.Add(keyItem2);
            this.InsGroup.MultiSetWhere = false;
            this.InsGroup.Name = "InsGroup";
            this.InsGroup.NotificationAutoEnlist = false;
            this.InsGroup.SecExcept = null;
            this.InsGroup.SecFieldName = null;
            this.InsGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InsGroup.SelectPaging = false;
            this.InsGroup.SelectTop = 0;
            this.InsGroup.SiteControl = false;
            this.InsGroup.SiteFieldName = null;
            this.InsGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = "SELECT CustomerID,ShortName FROM [Customer] where CustomerID is not null and  Sho" +
    "rtName  is not null";
            this.Customer.CommandTimeout = 30;
            this.Customer.CommandType = System.Data.CommandType.Text;
            this.Customer.DynamicTableName = false;
            this.Customer.EEPAlias = "JBERP";
            this.Customer.EncodingAfter = null;
            this.Customer.EncodingBefore = "Windows-1252";
            this.Customer.EncodingConvert = null;
            this.Customer.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CustomerID";
            this.Customer.KeyFields.Add(keyItem3);
            this.Customer.MultiSetWhere = false;
            this.Customer.Name = "Customer";
            this.Customer.NotificationAutoEnlist = false;
            this.Customer.SecExcept = null;
            this.Customer.SecFieldName = null;
            this.Customer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Customer.SelectPaging = false;
            this.Customer.SelectTop = 0;
            this.Customer.SiteControl = false;
            this.Customer.SiteFieldName = null;
            this.Customer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand InvoiceDetails;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoCommand Customer;
    }
}
