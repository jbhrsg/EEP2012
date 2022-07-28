namespace sMedia_Report_Customer
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPCustomers = new Srvtools.InfoCommand(this.components);
            this.ERPCustomersQ = new Srvtools.InfoCommand(this.components);
            this.InfoConnection2 = new Srvtools.InfoConnection(this.components);
            this.SalesMan = new Srvtools.InfoCommand(this.components);
            this.Industry = new Srvtools.InfoCommand(this.components);
            this.CustArea = new Srvtools.InfoCommand(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomersQ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Industry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetReportData";
            service1.NonLogin = false;
            service1.ServiceName = "GetReportData";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBDBNJB";
            // 
            // ERPCustomers
            // 
            this.ERPCustomers.CacheConnection = false;
            this.ERPCustomers.CommandText = resources.GetString("ERPCustomers.CommandText");
            this.ERPCustomers.CommandTimeout = 30;
            this.ERPCustomers.CommandType = System.Data.CommandType.Text;
            this.ERPCustomers.DynamicTableName = false;
            this.ERPCustomers.EEPAlias = "JBDBNJB";
            this.ERPCustomers.EncodingAfter = null;
            this.ERPCustomers.EncodingBefore = "Windows-1252";
            this.ERPCustomers.EncodingConvert = null;
            this.ERPCustomers.InfoConnection = this.InfoConnection1;
            this.ERPCustomers.MultiSetWhere = false;
            this.ERPCustomers.Name = "ERPCustomers";
            this.ERPCustomers.NotificationAutoEnlist = false;
            this.ERPCustomers.SecExcept = null;
            this.ERPCustomers.SecFieldName = null;
            this.ERPCustomers.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPCustomers.SelectPaging = false;
            this.ERPCustomers.SelectTop = 0;
            this.ERPCustomers.SiteControl = false;
            this.ERPCustomers.SiteFieldName = null;
            this.ERPCustomers.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ERPCustomersQ
            // 
            this.ERPCustomersQ.CacheConnection = false;
            this.ERPCustomersQ.CommandText = " SELECT top 30 CustNO,CustName\r\n FROM dbo.ERPCustomers";
            this.ERPCustomersQ.CommandTimeout = 30;
            this.ERPCustomersQ.CommandType = System.Data.CommandType.Text;
            this.ERPCustomersQ.DynamicTableName = false;
            this.ERPCustomersQ.EEPAlias = "JBDBNJB";
            this.ERPCustomersQ.EncodingAfter = null;
            this.ERPCustomersQ.EncodingBefore = "Windows-1252";
            this.ERPCustomersQ.EncodingConvert = null;
            this.ERPCustomersQ.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CustNO";
            this.ERPCustomersQ.KeyFields.Add(keyItem1);
            this.ERPCustomersQ.MultiSetWhere = false;
            this.ERPCustomersQ.Name = "ERPCustomersQ";
            this.ERPCustomersQ.NotificationAutoEnlist = false;
            this.ERPCustomersQ.SecExcept = null;
            this.ERPCustomersQ.SecFieldName = null;
            this.ERPCustomersQ.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPCustomersQ.SelectPaging = false;
            this.ERPCustomersQ.SelectTop = 0;
            this.ERPCustomersQ.SiteControl = false;
            this.ERPCustomersQ.SiteFieldName = null;
            this.ERPCustomersQ.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InfoConnection2
            // 
            this.InfoConnection2.EEPAlias = "JBADMIN";
            // 
            // SalesMan
            // 
            this.SalesMan.CacheConnection = false;
            this.SalesMan.CommandText = resources.GetString("SalesMan.CommandText");
            this.SalesMan.CommandTimeout = 30;
            this.SalesMan.CommandType = System.Data.CommandType.Text;
            this.SalesMan.DynamicTableName = false;
            this.SalesMan.EEPAlias = "";
            this.SalesMan.EncodingAfter = null;
            this.SalesMan.EncodingBefore = "Windows-1252";
            this.SalesMan.EncodingConvert = null;
            this.SalesMan.InfoConnection = this.InfoConnection2;
            this.SalesMan.MultiSetWhere = false;
            this.SalesMan.Name = "SalesMan";
            this.SalesMan.NotificationAutoEnlist = false;
            this.SalesMan.SecExcept = null;
            this.SalesMan.SecFieldName = null;
            this.SalesMan.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesMan.SelectPaging = false;
            this.SalesMan.SelectTop = 0;
            this.SalesMan.SiteControl = false;
            this.SalesMan.SiteFieldName = null;
            this.SalesMan.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Industry
            // 
            this.Industry.CacheConnection = false;
            this.Industry.CommandText = "SELECT * FROM View_Industry ORDER BY JB_NAME";
            this.Industry.CommandTimeout = 30;
            this.Industry.CommandType = System.Data.CommandType.Text;
            this.Industry.DynamicTableName = false;
            this.Industry.EEPAlias = "JBDBNJB";
            this.Industry.EncodingAfter = null;
            this.Industry.EncodingBefore = "Windows-1252";
            this.Industry.EncodingConvert = null;
            this.Industry.InfoConnection = this.InfoConnection1;
            this.Industry.MultiSetWhere = false;
            this.Industry.Name = "Industry";
            this.Industry.NotificationAutoEnlist = false;
            this.Industry.SecExcept = null;
            this.Industry.SecFieldName = null;
            this.Industry.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Industry.SelectPaging = false;
            this.Industry.SelectTop = 0;
            this.Industry.SiteControl = false;
            this.Industry.SiteFieldName = null;
            this.Industry.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CustArea
            // 
            this.CustArea.CacheConnection = false;
            this.CustArea.CommandText = "select ERPCustArea.CustAreaID,ERPCustArea.CustAreaName from ERPCustArea";
            this.CustArea.CommandTimeout = 30;
            this.CustArea.CommandType = System.Data.CommandType.Text;
            this.CustArea.DynamicTableName = false;
            this.CustArea.EEPAlias = null;
            this.CustArea.EncodingAfter = null;
            this.CustArea.EncodingBefore = "Windows-1252";
            this.CustArea.EncodingConvert = null;
            this.CustArea.InfoConnection = this.InfoConnection2;
            this.CustArea.MultiSetWhere = false;
            this.CustArea.Name = "CustArea";
            this.CustArea.NotificationAutoEnlist = false;
            this.CustArea.SecExcept = null;
            this.CustArea.SecFieldName = null;
            this.CustArea.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CustArea.SelectPaging = false;
            this.CustArea.SelectTop = 0;
            this.CustArea.SiteControl = false;
            this.CustArea.SiteFieldName = null;
            this.CustArea.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "select ERPSalesType.SalesTypeID,\r\n           RTRIM(ERPSalesType.SalesTypeID)+\'-\'+" +
    "ERPSalesType.SalesTypeName  AS  SalesTypeName\r\nfrom ERPSalesType\r\n";
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = null;
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection2;
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomersQ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Industry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPCustomers;
        private Srvtools.InfoCommand ERPCustomersQ;
        private Srvtools.InfoConnection InfoConnection2;
        private Srvtools.InfoCommand SalesMan;
        private Srvtools.InfoCommand Industry;
        private Srvtools.InfoCommand CustArea;
        private Srvtools.InfoCommand SalesType;
    }
}
