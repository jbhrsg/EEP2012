namespace sJCS1
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
            Srvtools.Service service2 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.BillPayMaster = new Srvtools.InfoCommand(this.components);
            this.infoCustomers = new Srvtools.InfoCommand(this.components);
            this.InfoConnJCS = new Srvtools.InfoConnection(this.components);
            this.infoConnection = new Srvtools.InfoConnection(this.components);
            this.infoDepositElec = new Srvtools.InfoCommand(this.components);
            this.infoCust = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BillPayMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnJCS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDepositElec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCust)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "procReportJCS1BillData";
            service1.NonLogin = false;
            service1.ServiceName = "procReportJCS1BillData";
            service2.DelegateName = "procReportJCS1Electronic";
            service2.NonLogin = false;
            service2.ServiceName = "procReportJCS1Electronic";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // BillPayMaster
            // 
            this.BillPayMaster.CacheConnection = false;
            this.BillPayMaster.CommandText = resources.GetString("BillPayMaster.CommandText");
            this.BillPayMaster.CommandTimeout = 30;
            this.BillPayMaster.CommandType = System.Data.CommandType.Text;
            this.BillPayMaster.DynamicTableName = false;
            this.BillPayMaster.EEPAlias = "JCS1";
            this.BillPayMaster.EncodingAfter = null;
            this.BillPayMaster.EncodingBefore = "Windows-1252";
            this.BillPayMaster.EncodingConvert = null;
            keyItem1.KeyName = "BillPayID";
            this.BillPayMaster.KeyFields.Add(keyItem1);
            this.BillPayMaster.MultiSetWhere = false;
            this.BillPayMaster.Name = "BillPayMaster";
            this.BillPayMaster.NotificationAutoEnlist = false;
            this.BillPayMaster.SecExcept = null;
            this.BillPayMaster.SecFieldName = null;
            this.BillPayMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.BillPayMaster.SelectPaging = false;
            this.BillPayMaster.SelectTop = 0;
            this.BillPayMaster.SiteControl = false;
            this.BillPayMaster.SiteFieldName = null;
            this.BillPayMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCustomers
            // 
            this.infoCustomers.CacheConnection = false;
            this.infoCustomers.CommandText = "SELECT CustomerShortName,CustomerID \r\nFROM Customers where IsActive=1";
            this.infoCustomers.CommandTimeout = 30;
            this.infoCustomers.CommandType = System.Data.CommandType.Text;
            this.infoCustomers.DynamicTableName = false;
            this.infoCustomers.EEPAlias = "JCS1";
            this.infoCustomers.EncodingAfter = null;
            this.infoCustomers.EncodingBefore = "Windows-1252";
            this.infoCustomers.EncodingConvert = null;
            this.infoCustomers.InfoConnection = this.InfoConnJCS;
            this.infoCustomers.MultiSetWhere = false;
            this.infoCustomers.Name = "infoCustomers";
            this.infoCustomers.NotificationAutoEnlist = false;
            this.infoCustomers.SecExcept = null;
            this.infoCustomers.SecFieldName = null;
            this.infoCustomers.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustomers.SelectPaging = false;
            this.infoCustomers.SelectTop = 0;
            this.infoCustomers.SiteControl = false;
            this.infoCustomers.SiteFieldName = null;
            this.infoCustomers.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InfoConnJCS
            // 
            this.InfoConnJCS.EEPAlias = "JCS1";
            // 
            // infoConnection
            // 
            this.infoConnection.EEPAlias = "JBADMIN";
            // 
            // infoDepositElec
            // 
            this.infoDepositElec.CacheConnection = false;
            this.infoDepositElec.CommandText = resources.GetString("infoDepositElec.CommandText");
            this.infoDepositElec.CommandTimeout = 30;
            this.infoDepositElec.CommandType = System.Data.CommandType.Text;
            this.infoDepositElec.DynamicTableName = false;
            this.infoDepositElec.EEPAlias = "JBADMIN";
            this.infoDepositElec.EncodingAfter = null;
            this.infoDepositElec.EncodingBefore = "Windows-1252";
            this.infoDepositElec.EncodingConvert = null;
            this.infoDepositElec.InfoConnection = this.infoConnection;
            this.infoDepositElec.MultiSetWhere = false;
            this.infoDepositElec.Name = "infoDepositElec";
            this.infoDepositElec.NotificationAutoEnlist = false;
            this.infoDepositElec.SecExcept = null;
            this.infoDepositElec.SecFieldName = null;
            this.infoDepositElec.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoDepositElec.SelectPaging = false;
            this.infoDepositElec.SelectTop = 0;
            this.infoDepositElec.SiteControl = false;
            this.infoDepositElec.SiteFieldName = null;
            this.infoDepositElec.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCust
            // 
            this.infoCust.CacheConnection = false;
            this.infoCust.CommandText = resources.GetString("infoCust.CommandText");
            this.infoCust.CommandTimeout = 30;
            this.infoCust.CommandType = System.Data.CommandType.Text;
            this.infoCust.DynamicTableName = false;
            this.infoCust.EEPAlias = "JCS1";
            this.infoCust.EncodingAfter = null;
            this.infoCust.EncodingBefore = "Windows-1252";
            this.infoCust.EncodingConvert = null;
            keyItem2.KeyName = "RoomerID";
            this.infoCust.KeyFields.Add(keyItem2);
            this.infoCust.MultiSetWhere = false;
            this.infoCust.Name = "infoCust";
            this.infoCust.NotificationAutoEnlist = false;
            this.infoCust.SecExcept = null;
            this.infoCust.SecFieldName = null;
            this.infoCust.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCust.SelectPaging = false;
            this.infoCust.SelectTop = 0;
            this.infoCust.SiteControl = false;
            this.infoCust.SiteFieldName = null;
            this.infoCust.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.BillPayMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnJCS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDepositElec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCust)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoCommand BillPayMaster;
        private Srvtools.InfoCommand infoCustomers;
        private Srvtools.InfoConnection infoConnection;
        private Srvtools.InfoCommand infoDepositElec;
        private Srvtools.InfoCommand infoCust;
        private Srvtools.InfoConnection InfoConnJCS;
    }
}
