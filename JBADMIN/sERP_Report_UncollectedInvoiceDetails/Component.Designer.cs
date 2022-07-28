namespace sERP_Report_UncollectedInvoiceDetails
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
            this.UncollectedInvoiceDetails = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.SalesPerson = new Srvtools.InfoCommand(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.Department = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UncollectedInvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Department)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetReportUncollectedInvoiceDetails";
            service1.NonLogin = false;
            service1.ServiceName = "GetReportUncollectedInvoiceDetails";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // UncollectedInvoiceDetails
            // 
            this.UncollectedInvoiceDetails.CacheConnection = false;
            this.UncollectedInvoiceDetails.CommandText = resources.GetString("UncollectedInvoiceDetails.CommandText");
            this.UncollectedInvoiceDetails.CommandTimeout = 30;
            this.UncollectedInvoiceDetails.CommandType = System.Data.CommandType.Text;
            this.UncollectedInvoiceDetails.DynamicTableName = false;
            this.UncollectedInvoiceDetails.EEPAlias = "JBERP";
            this.UncollectedInvoiceDetails.EncodingAfter = null;
            this.UncollectedInvoiceDetails.EncodingBefore = "Windows-1252";
            this.UncollectedInvoiceDetails.EncodingConvert = null;
            this.UncollectedInvoiceDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "InvoiceNO";
            this.UncollectedInvoiceDetails.KeyFields.Add(keyItem1);
            this.UncollectedInvoiceDetails.MultiSetWhere = false;
            this.UncollectedInvoiceDetails.Name = "UncollectedInvoiceDetails";
            this.UncollectedInvoiceDetails.NotificationAutoEnlist = false;
            this.UncollectedInvoiceDetails.SecExcept = null;
            this.UncollectedInvoiceDetails.SecFieldName = null;
            this.UncollectedInvoiceDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UncollectedInvoiceDetails.SelectPaging = false;
            this.UncollectedInvoiceDetails.SelectTop = 0;
            this.UncollectedInvoiceDetails.SiteControl = false;
            this.UncollectedInvoiceDetails.SiteFieldName = null;
            this.UncollectedInvoiceDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
            // 
            // SalesPerson
            // 
            this.SalesPerson.CacheConnection = false;
            this.SalesPerson.CommandText = resources.GetString("SalesPerson.CommandText");
            this.SalesPerson.CommandTimeout = 30;
            this.SalesPerson.CommandType = System.Data.CommandType.Text;
            this.SalesPerson.DynamicTableName = false;
            this.SalesPerson.EEPAlias = "JBERP";
            this.SalesPerson.EncodingAfter = null;
            this.SalesPerson.EncodingBefore = "Windows-1252";
            this.SalesPerson.EncodingConvert = null;
            this.SalesPerson.InfoConnection = this.InfoConnection1;
            this.SalesPerson.MultiSetWhere = false;
            this.SalesPerson.Name = "SalesPerson";
            this.SalesPerson.NotificationAutoEnlist = false;
            this.SalesPerson.SecExcept = null;
            this.SalesPerson.SecFieldName = null;
            this.SalesPerson.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesPerson.SelectPaging = false;
            this.SalesPerson.SelectTop = 0;
            this.SalesPerson.SiteControl = false;
            this.SalesPerson.SiteFieldName = null;
            this.SalesPerson.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "select SalesType.SalesTypeID,SalesType.SalesTypeName from SalesType order by Sale" +
    "sTypeID";
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = "JBERP";
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection1;
            this.SalesType.MultiSetWhere = false;
            this.SalesType.Name = "SalesType";
            this.SalesType.NotificationAutoEnlist = false;
            this.SalesType.SecExcept = null;
            this.SalesType.SecFieldName = null;
            this.SalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesType.SelectPaging = false;
            this.SalesType.SelectTop = 0;
            this.SalesType.SiteControl = false;
            this.SalesType.SiteFieldName = null;
            this.SalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = "SELECT CustomerID,ShortName,CustomerName,TelNO,TaxNO FROM [Customer] where Custom" +
    "erID is not null";
            this.Customer.CommandTimeout = 30;
            this.Customer.CommandType = System.Data.CommandType.Text;
            this.Customer.DynamicTableName = false;
            this.Customer.EEPAlias = "JBERP";
            this.Customer.EncodingAfter = null;
            this.Customer.EncodingBefore = "Windows-1252";
            this.Customer.EncodingConvert = null;
            this.Customer.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CustomerID";
            this.Customer.KeyFields.Add(keyItem2);
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
            keyItem3.KeyName = "InsGroupID";
            this.InsGroup.KeyFields.Add(keyItem3);
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
            // Department
            // 
            this.Department.CacheConnection = false;
            this.Department.CommandText = "  select * from [SYS_ORG]\r\n  where ORG_NO not in (\'201\',\'90000\',\'99999\')";
            this.Department.CommandTimeout = 30;
            this.Department.CommandType = System.Data.CommandType.Text;
            this.Department.DynamicTableName = false;
            this.Department.EEPAlias = "EIPHRSYS";
            this.Department.EncodingAfter = null;
            this.Department.EncodingBefore = "Windows-1252";
            this.Department.EncodingConvert = null;
            this.Department.InfoConnection = this.infoConnection2;
            this.Department.MultiSetWhere = false;
            this.Department.Name = "Department";
            this.Department.NotificationAutoEnlist = false;
            this.Department.SecExcept = null;
            this.Department.SecFieldName = null;
            this.Department.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Department.SelectPaging = false;
            this.Department.SelectTop = 0;
            this.Department.SiteControl = false;
            this.Department.SiteFieldName = null;
            this.Department.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UncollectedInvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Department)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand UncollectedInvoiceDetails;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand SalesPerson;
        private Srvtools.InfoCommand SalesType;
        private Srvtools.InfoCommand Customer;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoCommand Department;
    }
}
