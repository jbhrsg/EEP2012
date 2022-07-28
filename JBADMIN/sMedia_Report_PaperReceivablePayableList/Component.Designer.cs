namespace sMedia_Report_PaperReceivablePayableList
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalesDetails = new Srvtools.InfoCommand(this.components);
            this.infoCustomers = new Srvtools.InfoCommand(this.components);
            this.infoSalesMan = new Srvtools.InfoCommand(this.components);
            this.infoERPNewsPublish = new Srvtools.InfoCommand(this.components);
            this.infoERPNewsType = new Srvtools.InfoCommand(this.components);
            this.infoERPNewsArea = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsPublish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsArea)).BeginInit();
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
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalesDetails
            // 
            this.ERPSalesDetails.CacheConnection = false;
            this.ERPSalesDetails.CommandText = "SELECT top 1  \'\' as CustNO ,\'\' as SalesID ,\'\' as SalesDate,\'\' as [NewsTypeID],\'\' " +
    "as [NewsAreaID],\'\' as ReportType,\'\' as NewsPublishID\r\nFROM dbo.[ERPSalesDetails]" +
    "";
            this.ERPSalesDetails.CommandTimeout = 30;
            this.ERPSalesDetails.CommandType = System.Data.CommandType.Text;
            this.ERPSalesDetails.DynamicTableName = false;
            this.ERPSalesDetails.EEPAlias = null;
            this.ERPSalesDetails.EncodingAfter = null;
            this.ERPSalesDetails.EncodingBefore = "Windows-1252";
            this.ERPSalesDetails.EncodingConvert = null;
            this.ERPSalesDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesMasterNO";
            keyItem2.KeyName = "ItemSeq";
            this.ERPSalesDetails.KeyFields.Add(keyItem1);
            this.ERPSalesDetails.KeyFields.Add(keyItem2);
            this.ERPSalesDetails.MultiSetWhere = false;
            this.ERPSalesDetails.Name = "ERPSalesDetails";
            this.ERPSalesDetails.NotificationAutoEnlist = false;
            this.ERPSalesDetails.SecExcept = null;
            this.ERPSalesDetails.SecFieldName = null;
            this.ERPSalesDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesDetails.SelectPaging = false;
            this.ERPSalesDetails.SelectTop = 0;
            this.ERPSalesDetails.SiteControl = false;
            this.ERPSalesDetails.SiteFieldName = null;
            this.ERPSalesDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCustomers
            // 
            this.infoCustomers.CacheConnection = false;
            this.infoCustomers.CommandText = "select CustNO,CustName,CustTelNO\r\nfrom DBO.ERPCustomers\r\n\r\n";
            this.infoCustomers.CommandTimeout = 30;
            this.infoCustomers.CommandType = System.Data.CommandType.Text;
            this.infoCustomers.DynamicTableName = false;
            this.infoCustomers.EEPAlias = "";
            this.infoCustomers.EncodingAfter = null;
            this.infoCustomers.EncodingBefore = "Windows-1252";
            this.infoCustomers.EncodingConvert = null;
            this.infoCustomers.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CustNO";
            this.infoCustomers.KeyFields.Add(keyItem3);
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
            // infoSalesMan
            // 
            this.infoSalesMan.CacheConnection = false;
            this.infoSalesMan.CommandText = "select  SalesEmployeeID,SalesID,SalesName+\'-\'+SalesID as SalesName\r\nfrom ERPSales" +
    "Man\r\nwhere IsMedia=1\r\norder by SalesEmployeeID";
            this.infoSalesMan.CommandTimeout = 30;
            this.infoSalesMan.CommandType = System.Data.CommandType.Text;
            this.infoSalesMan.DynamicTableName = false;
            this.infoSalesMan.EEPAlias = null;
            this.infoSalesMan.EncodingAfter = null;
            this.infoSalesMan.EncodingBefore = "Windows-1252";
            this.infoSalesMan.EncodingConvert = null;
            this.infoSalesMan.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "SalseNO";
            this.infoSalesMan.KeyFields.Add(keyItem4);
            this.infoSalesMan.MultiSetWhere = false;
            this.infoSalesMan.Name = "infoSalesMan";
            this.infoSalesMan.NotificationAutoEnlist = false;
            this.infoSalesMan.SecExcept = null;
            this.infoSalesMan.SecFieldName = null;
            this.infoSalesMan.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesMan.SelectPaging = false;
            this.infoSalesMan.SelectTop = 0;
            this.infoSalesMan.SiteControl = false;
            this.infoSalesMan.SiteFieldName = null;
            this.infoSalesMan.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoERPNewsPublish
            // 
            this.infoERPNewsPublish.CacheConnection = false;
            this.infoERPNewsPublish.CommandText = "select NewsPublishNO,NewsPublishID,NewsPublishID+\' : \'+NewsPublishName as NewsPub" +
    "lishName\r\n from ERPNewsPublish\r\norder by NewsPublishID";
            this.infoERPNewsPublish.CommandTimeout = 30;
            this.infoERPNewsPublish.CommandType = System.Data.CommandType.Text;
            this.infoERPNewsPublish.DynamicTableName = false;
            this.infoERPNewsPublish.EEPAlias = null;
            this.infoERPNewsPublish.EncodingAfter = null;
            this.infoERPNewsPublish.EncodingBefore = "Windows-1252";
            this.infoERPNewsPublish.EncodingConvert = null;
            this.infoERPNewsPublish.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "NewsPublishNO";
            this.infoERPNewsPublish.KeyFields.Add(keyItem5);
            this.infoERPNewsPublish.MultiSetWhere = false;
            this.infoERPNewsPublish.Name = "infoERPNewsPublish";
            this.infoERPNewsPublish.NotificationAutoEnlist = false;
            this.infoERPNewsPublish.SecExcept = null;
            this.infoERPNewsPublish.SecFieldName = null;
            this.infoERPNewsPublish.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoERPNewsPublish.SelectPaging = false;
            this.infoERPNewsPublish.SelectTop = 0;
            this.infoERPNewsPublish.SiteControl = false;
            this.infoERPNewsPublish.SiteFieldName = null;
            this.infoERPNewsPublish.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoERPNewsType
            // 
            this.infoERPNewsType.CacheConnection = false;
            this.infoERPNewsType.CommandText = "select NewsTypeNO,NewsTypeID,NewsTypeID+\' : \'+NewsTypeName as NewsTypeName \r\nfrom" +
    " ERPNewsType\r\norder by NewsTypeID";
            this.infoERPNewsType.CommandTimeout = 30;
            this.infoERPNewsType.CommandType = System.Data.CommandType.Text;
            this.infoERPNewsType.DynamicTableName = false;
            this.infoERPNewsType.EEPAlias = null;
            this.infoERPNewsType.EncodingAfter = null;
            this.infoERPNewsType.EncodingBefore = "Windows-1252";
            this.infoERPNewsType.EncodingConvert = null;
            this.infoERPNewsType.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "NewsTypeNO";
            this.infoERPNewsType.KeyFields.Add(keyItem6);
            this.infoERPNewsType.MultiSetWhere = false;
            this.infoERPNewsType.Name = "infoERPNewsType";
            this.infoERPNewsType.NotificationAutoEnlist = false;
            this.infoERPNewsType.SecExcept = null;
            this.infoERPNewsType.SecFieldName = null;
            this.infoERPNewsType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoERPNewsType.SelectPaging = false;
            this.infoERPNewsType.SelectTop = 0;
            this.infoERPNewsType.SiteControl = false;
            this.infoERPNewsType.SiteFieldName = null;
            this.infoERPNewsType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoERPNewsArea
            // 
            this.infoERPNewsArea.CacheConnection = false;
            this.infoERPNewsArea.CommandText = "select NewsAreaNO,NewsAreaID,NewsAreaID+\' : \'+NewsAreaName as NewsAreaName\r\nfrom " +
    "ERPNewsArea\r\norder by NewsAreaID";
            this.infoERPNewsArea.CommandTimeout = 30;
            this.infoERPNewsArea.CommandType = System.Data.CommandType.Text;
            this.infoERPNewsArea.DynamicTableName = false;
            this.infoERPNewsArea.EEPAlias = null;
            this.infoERPNewsArea.EncodingAfter = null;
            this.infoERPNewsArea.EncodingBefore = "Windows-1252";
            this.infoERPNewsArea.EncodingConvert = null;
            this.infoERPNewsArea.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "NewsAreaNO";
            this.infoERPNewsArea.KeyFields.Add(keyItem7);
            this.infoERPNewsArea.MultiSetWhere = false;
            this.infoERPNewsArea.Name = "infoERPNewsArea";
            this.infoERPNewsArea.NotificationAutoEnlist = false;
            this.infoERPNewsArea.SecExcept = null;
            this.infoERPNewsArea.SecFieldName = null;
            this.infoERPNewsArea.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoERPNewsArea.SelectPaging = false;
            this.infoERPNewsArea.SelectTop = 0;
            this.infoERPNewsArea.SiteControl = false;
            this.infoERPNewsArea.SiteFieldName = null;
            this.infoERPNewsArea.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsPublish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsArea)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesDetails;
        private Srvtools.InfoCommand infoCustomers;
        private Srvtools.InfoCommand infoSalesMan;
        private Srvtools.InfoCommand infoERPNewsPublish;
        private Srvtools.InfoCommand infoERPNewsType;
        private Srvtools.InfoCommand infoERPNewsArea;
    }
}
