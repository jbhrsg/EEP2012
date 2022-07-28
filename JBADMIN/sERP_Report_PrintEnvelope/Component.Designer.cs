namespace sERP_Report_PrintEnvelope
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
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            this.View_Customer = new Srvtools.InfoCommand(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            this.QCustomerID = new Srvtools.InfoCommand(this.components);
            this.QTelNO = new Srvtools.InfoCommand(this.components);
            this.ucCustomer = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QCustomerID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QTelNO)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetCustomerSaleType";
            service1.NonLogin = false;
            service1.ServiceName = "GetCustomerSaleType";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = resources.GetString("Customer.CommandText");
            this.Customer.CommandTimeout = 30;
            this.Customer.CommandType = System.Data.CommandType.Text;
            this.Customer.DynamicTableName = false;
            this.Customer.EEPAlias = "JBERP";
            this.Customer.EncodingAfter = null;
            this.Customer.EncodingBefore = "Windows-1252";
            this.Customer.EncodingConvert = null;
            this.Customer.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CustomerID";
            keyItem2.KeyName = "SalesTypeID";
            this.Customer.KeyFields.Add(keyItem1);
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
            // View_Customer
            // 
            this.View_Customer.CacheConnection = false;
            this.View_Customer.CommandText = "SELECT * FROM dbo.[Customer]";
            this.View_Customer.CommandTimeout = 30;
            this.View_Customer.CommandType = System.Data.CommandType.Text;
            this.View_Customer.DynamicTableName = false;
            this.View_Customer.EEPAlias = "JBERP";
            this.View_Customer.EncodingAfter = null;
            this.View_Customer.EncodingBefore = "Windows-1252";
            this.View_Customer.EncodingConvert = null;
            this.View_Customer.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CustomerID";
            this.View_Customer.KeyFields.Add(keyItem3);
            this.View_Customer.MultiSetWhere = false;
            this.View_Customer.Name = "View_Customer";
            this.View_Customer.NotificationAutoEnlist = false;
            this.View_Customer.SecExcept = null;
            this.View_Customer.SecFieldName = null;
            this.View_Customer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Customer.SelectPaging = false;
            this.View_Customer.SelectTop = 0;
            this.View_Customer.SiteControl = false;
            this.View_Customer.SiteFieldName = null;
            this.View_Customer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "SELECT SalesTypeID,SalesTypeID+\'-\'+SalesTypeName as SalesTypeName  FROM dbo.Sales" +
    "Type order by SalesTypeID";
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = "JBERP";
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "SalesTypeID";
            this.SalesType.KeyFields.Add(keyItem4);
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
            // QCustomerID
            // 
            this.QCustomerID.CacheConnection = false;
            this.QCustomerID.CommandText = "SELECT  distinct   top 30 [CustomerID],[CustomerName]\r\n  FROM [Customer]\r\n";
            this.QCustomerID.CommandTimeout = 30;
            this.QCustomerID.CommandType = System.Data.CommandType.Text;
            this.QCustomerID.DynamicTableName = false;
            this.QCustomerID.EEPAlias = "JBERP";
            this.QCustomerID.EncodingAfter = null;
            this.QCustomerID.EncodingBefore = "Windows-1252";
            this.QCustomerID.EncodingConvert = null;
            this.QCustomerID.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "CustomerID";
            this.QCustomerID.KeyFields.Add(keyItem5);
            this.QCustomerID.MultiSetWhere = false;
            this.QCustomerID.Name = "QCustomerID";
            this.QCustomerID.NotificationAutoEnlist = false;
            this.QCustomerID.SecExcept = null;
            this.QCustomerID.SecFieldName = null;
            this.QCustomerID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.QCustomerID.SelectPaging = false;
            this.QCustomerID.SelectTop = 0;
            this.QCustomerID.SiteControl = false;
            this.QCustomerID.SiteFieldName = null;
            this.QCustomerID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // QTelNO
            // 
            this.QTelNO.CacheConnection = false;
            this.QTelNO.CommandText = "SELECT  distinct  top 30  [TelNO]\r\n  FROM [Customer] \r\nwhere TelNO!=\'\' and TelNO " +
    "is not null";
            this.QTelNO.CommandTimeout = 30;
            this.QTelNO.CommandType = System.Data.CommandType.Text;
            this.QTelNO.DynamicTableName = false;
            this.QTelNO.EEPAlias = "JBERP";
            this.QTelNO.EncodingAfter = null;
            this.QTelNO.EncodingBefore = "Windows-1252";
            this.QTelNO.EncodingConvert = null;
            this.QTelNO.InfoConnection = this.InfoConnection1;
            this.QTelNO.MultiSetWhere = false;
            this.QTelNO.Name = "QTelNO";
            this.QTelNO.NotificationAutoEnlist = false;
            this.QTelNO.SecExcept = null;
            this.QTelNO.SecFieldName = null;
            this.QTelNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.QTelNO.SelectPaging = false;
            this.QTelNO.SelectTop = 0;
            this.QTelNO.SiteControl = false;
            this.QTelNO.SiteFieldName = null;
            this.QTelNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCustomer
            // 
            this.ucCustomer.AutoTrans = true;
            this.ucCustomer.ExceptJoin = false;
            this.ucCustomer.LogInfo = null;
            this.ucCustomer.Name = "ucCustomer";
            this.ucCustomer.RowAffectsCheck = true;
            this.ucCustomer.SelectCmd = this.Customer;
            this.ucCustomer.SelectCmdForUpdate = null;
            this.ucCustomer.SendSQLCmd = true;
            this.ucCustomer.ServerModify = true;
            this.ucCustomer.ServerModifyGetMax = false;
            this.ucCustomer.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCustomer.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCustomer.UseTranscationScope = false;
            this.ucCustomer.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QCustomerID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QTelNO)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Customer;
        private Srvtools.InfoCommand View_Customer;
        private Srvtools.InfoCommand SalesType;
        private Srvtools.InfoCommand QCustomerID;
        private Srvtools.InfoCommand QTelNO;
        private Srvtools.UpdateComponent ucCustomer;
    }
}
