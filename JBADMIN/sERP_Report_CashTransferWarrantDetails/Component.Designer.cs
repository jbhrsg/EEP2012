namespace sERP_Report_CashTransferWarrantDetails
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
            Srvtools.Service service2 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.WarrantDetails = new Srvtools.InfoCommand(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WarrantDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            // 
            // serviceManager1
            // 
            service2.DelegateName = "GetReportData";
            service2.NonLogin = false;
            service2.ServiceName = "GetReportData";
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // WarrantDetails
            // 
            this.WarrantDetails.CacheConnection = false;
            this.WarrantDetails.CommandText = resources.GetString("WarrantDetails.CommandText");
            this.WarrantDetails.CommandTimeout = 30;
            this.WarrantDetails.CommandType = System.Data.CommandType.Text;
            this.WarrantDetails.DynamicTableName = false;
            this.WarrantDetails.EEPAlias = "JBERP";
            this.WarrantDetails.EncodingAfter = null;
            this.WarrantDetails.EncodingBefore = "Windows-1252";
            this.WarrantDetails.EncodingConvert = null;
            this.WarrantDetails.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "WarrantNO";
            keyItem5.KeyName = "ItemNO";
            this.WarrantDetails.KeyFields.Add(keyItem4);
            this.WarrantDetails.KeyFields.Add(keyItem5);
            this.WarrantDetails.MultiSetWhere = false;
            this.WarrantDetails.Name = "WarrantDetails";
            this.WarrantDetails.NotificationAutoEnlist = false;
            this.WarrantDetails.SecExcept = null;
            this.WarrantDetails.SecFieldName = null;
            this.WarrantDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.WarrantDetails.SelectPaging = false;
            this.WarrantDetails.SelectTop = 0;
            this.WarrantDetails.SiteControl = false;
            this.WarrantDetails.SiteFieldName = null;
            this.WarrantDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = "SELECT CustomerID,ShortName FROM [Customer] where CustomerTypeID=\'1\' and Customer" +
    "ID is not null and  ShortName  is not null";
            this.Customer.CommandTimeout = 30;
            this.Customer.CommandType = System.Data.CommandType.Text;
            this.Customer.DynamicTableName = false;
            this.Customer.EEPAlias = "JBERP";
            this.Customer.EncodingAfter = null;
            this.Customer.EncodingBefore = "Windows-1252";
            this.Customer.EncodingConvert = null;
            this.Customer.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CustomerID";
            this.Customer.KeyFields.Add(keyItem1);
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
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
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
            this.InsGroup.InfoConnection = this.infoConnection2;
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WarrantDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand WarrantDetails;
        private Srvtools.InfoCommand Customer;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand InsGroup;
    }
}
