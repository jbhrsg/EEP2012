namespace sR_SalesDetails
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
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.Service service7 = new Srvtools.Service();
            Srvtools.Service service8 = new Srvtools.Service();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.infoCustomersAll = new Srvtools.InfoCommand(this.components);
            this.infoSalesMan = new Srvtools.InfoCommand(this.components);
            this.infoCustArea = new Srvtools.InfoCommand(this.components);
            this.ERPSalesDetails = new Srvtools.InfoCommand(this.components);
            this.ERPSalesDetails2 = new Srvtools.InfoCommand(this.components);
            this.infoSalesType = new Srvtools.InfoCommand(this.components);
            this.infoERPNewsType = new Srvtools.InfoCommand(this.components);
            this.infoAcceptDateData = new Srvtools.InfoCommand(this.components);
            this.infoSalesTypeAll = new Srvtools.InfoCommand(this.components);
            this.infoInvoiceList = new Srvtools.InfoCommand(this.components);
            this.infoCreateBy = new Srvtools.InfoCommand(this.components);
            this.ERPCustomerToDoNotes = new Srvtools.InfoCommand(this.components);
            this.infoSalesType2 = new Srvtools.InfoCommand(this.components);
            this.infoStats = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomersAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAcceptDateData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesTypeAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoInvoiceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCreateBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomerToDoNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesType2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoStats)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "ReportSalesDetails";
            service1.NonLogin = false;
            service1.ServiceName = "ReportSalesDetails";
            service2.DelegateName = "ReportSalesDetails2";
            service2.NonLogin = false;
            service2.ServiceName = "ReportSalesDetails2";
            service3.DelegateName = "ReportSalesDetails22";
            service3.NonLogin = false;
            service3.ServiceName = "ReportSalesDetails22";
            service4.DelegateName = "ReportSalesDetails3";
            service4.NonLogin = false;
            service4.ServiceName = "ReportSalesDetails3";
            service5.DelegateName = "ReportSalesDetails222";
            service5.NonLogin = false;
            service5.ServiceName = "ReportSalesDetails222";
            service6.DelegateName = "ReportInvoiceList";
            service6.NonLogin = false;
            service6.ServiceName = "ReportInvoiceList";
            service7.DelegateName = "ReportCustomerToDoNotes";
            service7.NonLogin = false;
            service7.ServiceName = "ReportCustomerToDoNotes";
            service8.DelegateName = "SalesDetailsCountRepeat";
            service8.NonLogin = false;
            service8.ServiceName = "SalesDetailsCountRepeat";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // infoCustomersAll
            // 
            this.infoCustomersAll.CacheConnection = false;
            this.infoCustomersAll.CommandText = "select \'\' as CustNO,\'\' as SalesID,\'==不拘==\' as CustShortName\r\nunion all\r\nselect to" +
    "p 10 CustNO,SalesID,CustNO+\' : \'+CustShortName as CustShortName\r\nfrom View_ERPCu" +
    "stomers\r\nwhere 1=1\r\norder by CustNO";
            this.infoCustomersAll.CommandTimeout = 30;
            this.infoCustomersAll.CommandType = System.Data.CommandType.Text;
            this.infoCustomersAll.DynamicTableName = false;
            this.infoCustomersAll.EEPAlias = null;
            this.infoCustomersAll.EncodingAfter = null;
            this.infoCustomersAll.EncodingBefore = "Windows-1252";
            this.infoCustomersAll.EncodingConvert = null;
            this.infoCustomersAll.InfoConnection = this.InfoConnection1;
            this.infoCustomersAll.MultiSetWhere = false;
            this.infoCustomersAll.Name = "infoCustomersAll";
            this.infoCustomersAll.NotificationAutoEnlist = false;
            this.infoCustomersAll.SecExcept = null;
            this.infoCustomersAll.SecFieldName = null;
            this.infoCustomersAll.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustomersAll.SelectPaging = false;
            this.infoCustomersAll.SelectTop = 0;
            this.infoCustomersAll.SiteControl = false;
            this.infoCustomersAll.SiteFieldName = null;
            this.infoCustomersAll.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesMan
            // 
            this.infoSalesMan.CacheConnection = false;
            this.infoSalesMan.CommandText = "select  \'\' as SalesEmployeeID,\'\' as SalesID,\'==不拘==\' as SalesName\r\nunion all\r\nsel" +
    "ect  SalesEmployeeID,SalesID,SalesName+\'-\'+SalesID\r\nfrom ERPSalesMan\r\nwhere IsMe" +
    "dia=1\r\norder by SalesEmployeeID";
            this.infoSalesMan.CommandTimeout = 30;
            this.infoSalesMan.CommandType = System.Data.CommandType.Text;
            this.infoSalesMan.DynamicTableName = false;
            this.infoSalesMan.EEPAlias = null;
            this.infoSalesMan.EncodingAfter = null;
            this.infoSalesMan.EncodingBefore = "Windows-1252";
            this.infoSalesMan.EncodingConvert = null;
            this.infoSalesMan.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalseNO";
            this.infoSalesMan.KeyFields.Add(keyItem1);
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
            // infoCustArea
            // 
            this.infoCustArea.CacheConnection = false;
            this.infoCustArea.CommandText = "select \'\' as CustAreaID,\'==不拘==\' as CustAreaName\r\nunion all\r\nselect CustAreaID,Cu" +
    "stAreaName\r\nfrom ERPCustArea\r\nwhere 1=1\r\norder by CustAreaID";
            this.infoCustArea.CommandTimeout = 30;
            this.infoCustArea.CommandType = System.Data.CommandType.Text;
            this.infoCustArea.DynamicTableName = false;
            this.infoCustArea.EEPAlias = null;
            this.infoCustArea.EncodingAfter = null;
            this.infoCustArea.EncodingBefore = "Windows-1252";
            this.infoCustArea.EncodingConvert = null;
            this.infoCustArea.InfoConnection = this.InfoConnection1;
            this.infoCustArea.MultiSetWhere = false;
            this.infoCustArea.Name = "infoCustArea";
            this.infoCustArea.NotificationAutoEnlist = false;
            this.infoCustArea.SecExcept = null;
            this.infoCustArea.SecFieldName = null;
            this.infoCustArea.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustArea.SelectPaging = false;
            this.infoCustArea.SelectTop = 0;
            this.infoCustArea.SiteControl = false;
            this.infoCustArea.SiteFieldName = null;
            this.infoCustArea.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ERPSalesDetails
            // 
            this.ERPSalesDetails.CacheConnection = false;
            this.ERPSalesDetails.CommandText = resources.GetString("ERPSalesDetails.CommandText");
            this.ERPSalesDetails.CommandTimeout = 30;
            this.ERPSalesDetails.CommandType = System.Data.CommandType.Text;
            this.ERPSalesDetails.DynamicTableName = false;
            this.ERPSalesDetails.EEPAlias = null;
            this.ERPSalesDetails.EncodingAfter = null;
            this.ERPSalesDetails.EncodingBefore = "Windows-1252";
            this.ERPSalesDetails.EncodingConvert = null;
            this.ERPSalesDetails.InfoConnection = this.InfoConnection1;
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
            // ERPSalesDetails2
            // 
            this.ERPSalesDetails2.CacheConnection = false;
            this.ERPSalesDetails2.CommandText = resources.GetString("ERPSalesDetails2.CommandText");
            this.ERPSalesDetails2.CommandTimeout = 30;
            this.ERPSalesDetails2.CommandType = System.Data.CommandType.Text;
            this.ERPSalesDetails2.DynamicTableName = false;
            this.ERPSalesDetails2.EEPAlias = null;
            this.ERPSalesDetails2.EncodingAfter = null;
            this.ERPSalesDetails2.EncodingBefore = "Windows-1252";
            this.ERPSalesDetails2.EncodingConvert = null;
            this.ERPSalesDetails2.InfoConnection = this.InfoConnection1;
            this.ERPSalesDetails2.MultiSetWhere = false;
            this.ERPSalesDetails2.Name = "ERPSalesDetails2";
            this.ERPSalesDetails2.NotificationAutoEnlist = false;
            this.ERPSalesDetails2.SecExcept = null;
            this.ERPSalesDetails2.SecFieldName = null;
            this.ERPSalesDetails2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesDetails2.SelectPaging = false;
            this.ERPSalesDetails2.SelectTop = 0;
            this.ERPSalesDetails2.SiteControl = false;
            this.ERPSalesDetails2.SiteFieldName = null;
            this.ERPSalesDetails2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesType
            // 
            this.infoSalesType.CacheConnection = false;
            this.infoSalesType.CommandText = "select SalesTypeID,SalesTypeName\r\nfrom ERPSalesType\r\nwhere isActive=1";
            this.infoSalesType.CommandTimeout = 30;
            this.infoSalesType.CommandType = System.Data.CommandType.Text;
            this.infoSalesType.DynamicTableName = false;
            this.infoSalesType.EEPAlias = null;
            this.infoSalesType.EncodingAfter = null;
            this.infoSalesType.EncodingBefore = "Windows-1252";
            this.infoSalesType.EncodingConvert = null;
            this.infoSalesType.InfoConnection = this.InfoConnection1;
            this.infoSalesType.MultiSetWhere = false;
            this.infoSalesType.Name = "infoSalesType";
            this.infoSalesType.NotificationAutoEnlist = false;
            this.infoSalesType.SecExcept = null;
            this.infoSalesType.SecFieldName = null;
            this.infoSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesType.SelectPaging = false;
            this.infoSalesType.SelectTop = 0;
            this.infoSalesType.SiteControl = false;
            this.infoSalesType.SiteFieldName = null;
            this.infoSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoERPNewsType
            // 
            this.infoERPNewsType.CacheConnection = false;
            this.infoERPNewsType.CommandText = "select \'\' as NewsTypeID,\'==不拘==\' as NewsTypeName\r\nunion all\r\nselect NewsTypeID,Ne" +
    "wsTypeID+\' : \'+NewsTypeName as NewsTypeName \r\nfrom ERPNewsType\r\norder by NewsTyp" +
    "eID";
            this.infoERPNewsType.CommandTimeout = 30;
            this.infoERPNewsType.CommandType = System.Data.CommandType.Text;
            this.infoERPNewsType.DynamicTableName = false;
            this.infoERPNewsType.EEPAlias = null;
            this.infoERPNewsType.EncodingAfter = null;
            this.infoERPNewsType.EncodingBefore = "Windows-1252";
            this.infoERPNewsType.EncodingConvert = null;
            this.infoERPNewsType.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "NewsTypeNO";
            this.infoERPNewsType.KeyFields.Add(keyItem2);
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
            // infoAcceptDateData
            // 
            this.infoAcceptDateData.CacheConnection = false;
            this.infoAcceptDateData.CommandText = resources.GetString("infoAcceptDateData.CommandText");
            this.infoAcceptDateData.CommandTimeout = 30;
            this.infoAcceptDateData.CommandType = System.Data.CommandType.Text;
            this.infoAcceptDateData.DynamicTableName = false;
            this.infoAcceptDateData.EEPAlias = null;
            this.infoAcceptDateData.EncodingAfter = null;
            this.infoAcceptDateData.EncodingBefore = "Windows-1252";
            this.infoAcceptDateData.EncodingConvert = null;
            this.infoAcceptDateData.InfoConnection = this.InfoConnection1;
            this.infoAcceptDateData.MultiSetWhere = false;
            this.infoAcceptDateData.Name = "infoAcceptDateData";
            this.infoAcceptDateData.NotificationAutoEnlist = false;
            this.infoAcceptDateData.SecExcept = null;
            this.infoAcceptDateData.SecFieldName = null;
            this.infoAcceptDateData.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoAcceptDateData.SelectPaging = false;
            this.infoAcceptDateData.SelectTop = 0;
            this.infoAcceptDateData.SiteControl = false;
            this.infoAcceptDateData.SiteFieldName = null;
            this.infoAcceptDateData.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesTypeAll
            // 
            this.infoSalesTypeAll.CacheConnection = false;
            this.infoSalesTypeAll.CommandText = "select  \'\' as SalesTypeID,\'==不拘==\' as SalesTypeName\r\nunion all\r\nselect SalesTypeI" +
    "D,SalesTypeName\r\nfrom ERPSalesType\r\nwhere isActive=1";
            this.infoSalesTypeAll.CommandTimeout = 30;
            this.infoSalesTypeAll.CommandType = System.Data.CommandType.Text;
            this.infoSalesTypeAll.DynamicTableName = false;
            this.infoSalesTypeAll.EEPAlias = null;
            this.infoSalesTypeAll.EncodingAfter = null;
            this.infoSalesTypeAll.EncodingBefore = "Windows-1252";
            this.infoSalesTypeAll.EncodingConvert = null;
            this.infoSalesTypeAll.InfoConnection = this.InfoConnection1;
            this.infoSalesTypeAll.MultiSetWhere = false;
            this.infoSalesTypeAll.Name = "infoSalesTypeAll";
            this.infoSalesTypeAll.NotificationAutoEnlist = false;
            this.infoSalesTypeAll.SecExcept = null;
            this.infoSalesTypeAll.SecFieldName = null;
            this.infoSalesTypeAll.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesTypeAll.SelectPaging = false;
            this.infoSalesTypeAll.SelectTop = 0;
            this.infoSalesTypeAll.SiteControl = false;
            this.infoSalesTypeAll.SiteFieldName = null;
            this.infoSalesTypeAll.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoInvoiceList
            // 
            this.infoInvoiceList.CacheConnection = false;
            this.infoInvoiceList.CommandText = resources.GetString("infoInvoiceList.CommandText");
            this.infoInvoiceList.CommandTimeout = 30;
            this.infoInvoiceList.CommandType = System.Data.CommandType.Text;
            this.infoInvoiceList.DynamicTableName = false;
            this.infoInvoiceList.EEPAlias = null;
            this.infoInvoiceList.EncodingAfter = null;
            this.infoInvoiceList.EncodingBefore = "Windows-1252";
            this.infoInvoiceList.EncodingConvert = null;
            this.infoInvoiceList.InfoConnection = this.InfoConnection1;
            this.infoInvoiceList.MultiSetWhere = false;
            this.infoInvoiceList.Name = "infoInvoiceList";
            this.infoInvoiceList.NotificationAutoEnlist = false;
            this.infoInvoiceList.SecExcept = null;
            this.infoInvoiceList.SecFieldName = null;
            this.infoInvoiceList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoInvoiceList.SelectPaging = false;
            this.infoInvoiceList.SelectTop = 0;
            this.infoInvoiceList.SiteControl = false;
            this.infoInvoiceList.SiteFieldName = null;
            this.infoInvoiceList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCreateBy
            // 
            this.infoCreateBy.CacheConnection = false;
            this.infoCreateBy.CommandText = "select distinct CreateBy\r\nfrom View_CustomerToDoNotes\r\norder by CreateBy\r\n";
            this.infoCreateBy.CommandTimeout = 30;
            this.infoCreateBy.CommandType = System.Data.CommandType.Text;
            this.infoCreateBy.DynamicTableName = false;
            this.infoCreateBy.EEPAlias = null;
            this.infoCreateBy.EncodingAfter = null;
            this.infoCreateBy.EncodingBefore = "Windows-1252";
            this.infoCreateBy.EncodingConvert = null;
            this.infoCreateBy.InfoConnection = this.InfoConnection1;
            this.infoCreateBy.MultiSetWhere = false;
            this.infoCreateBy.Name = "infoCreateBy";
            this.infoCreateBy.NotificationAutoEnlist = false;
            this.infoCreateBy.SecExcept = null;
            this.infoCreateBy.SecFieldName = null;
            this.infoCreateBy.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCreateBy.SelectPaging = false;
            this.infoCreateBy.SelectTop = 0;
            this.infoCreateBy.SiteControl = false;
            this.infoCreateBy.SiteFieldName = null;
            this.infoCreateBy.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ERPCustomerToDoNotes
            // 
            this.ERPCustomerToDoNotes.CacheConnection = false;
            this.ERPCustomerToDoNotes.CommandText = resources.GetString("ERPCustomerToDoNotes.CommandText");
            this.ERPCustomerToDoNotes.CommandTimeout = 30;
            this.ERPCustomerToDoNotes.CommandType = System.Data.CommandType.Text;
            this.ERPCustomerToDoNotes.DynamicTableName = false;
            this.ERPCustomerToDoNotes.EEPAlias = null;
            this.ERPCustomerToDoNotes.EncodingAfter = null;
            this.ERPCustomerToDoNotes.EncodingBefore = "Windows-1252";
            this.ERPCustomerToDoNotes.EncodingConvert = null;
            this.ERPCustomerToDoNotes.InfoConnection = this.InfoConnection1;
            this.ERPCustomerToDoNotes.MultiSetWhere = false;
            this.ERPCustomerToDoNotes.Name = "ERPCustomerToDoNotes";
            this.ERPCustomerToDoNotes.NotificationAutoEnlist = false;
            this.ERPCustomerToDoNotes.SecExcept = null;
            this.ERPCustomerToDoNotes.SecFieldName = null;
            this.ERPCustomerToDoNotes.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPCustomerToDoNotes.SelectPaging = false;
            this.ERPCustomerToDoNotes.SelectTop = 0;
            this.ERPCustomerToDoNotes.SiteControl = false;
            this.ERPCustomerToDoNotes.SiteFieldName = null;
            this.ERPCustomerToDoNotes.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesType2
            // 
            this.infoSalesType2.CacheConnection = false;
            this.infoSalesType2.CommandText = "select SalesTypeID,SalesTypeName\r\nfrom ERPSalesType\r\nwhere SalesTypeID in (\'1\',\'6" +
    "\',\'31\')";
            this.infoSalesType2.CommandTimeout = 30;
            this.infoSalesType2.CommandType = System.Data.CommandType.Text;
            this.infoSalesType2.DynamicTableName = false;
            this.infoSalesType2.EEPAlias = null;
            this.infoSalesType2.EncodingAfter = null;
            this.infoSalesType2.EncodingBefore = "Windows-1252";
            this.infoSalesType2.EncodingConvert = null;
            this.infoSalesType2.InfoConnection = this.InfoConnection1;
            this.infoSalesType2.MultiSetWhere = false;
            this.infoSalesType2.Name = "infoSalesType2";
            this.infoSalesType2.NotificationAutoEnlist = false;
            this.infoSalesType2.SecExcept = null;
            this.infoSalesType2.SecFieldName = null;
            this.infoSalesType2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesType2.SelectPaging = false;
            this.infoSalesType2.SelectTop = 0;
            this.infoSalesType2.SiteControl = false;
            this.infoSalesType2.SiteFieldName = null;
            this.infoSalesType2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoStats
            // 
            this.infoStats.CacheConnection = false;
            this.infoStats.CommandText = "select 1 as ID,\'新客戶\' as sName\r\nunion \r\nselect 2 as ID,\'1-2年間\'\r\nunion \r\nselect 3 a" +
    "s ID,\'久未刊\'";
            this.infoStats.CommandTimeout = 30;
            this.infoStats.CommandType = System.Data.CommandType.Text;
            this.infoStats.DynamicTableName = false;
            this.infoStats.EEPAlias = null;
            this.infoStats.EncodingAfter = null;
            this.infoStats.EncodingBefore = "Windows-1252";
            this.infoStats.EncodingConvert = null;
            this.infoStats.InfoConnection = this.InfoConnection1;
            this.infoStats.MultiSetWhere = false;
            this.infoStats.Name = "infoStats";
            this.infoStats.NotificationAutoEnlist = false;
            this.infoStats.SecExcept = null;
            this.infoStats.SecFieldName = null;
            this.infoStats.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoStats.SelectPaging = false;
            this.infoStats.SelectTop = 0;
            this.infoStats.SiteControl = false;
            this.infoStats.SiteFieldName = null;
            this.infoStats.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomersAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAcceptDateData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesTypeAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoInvoiceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCreateBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomerToDoNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesType2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoStats)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand infoCustomersAll;
        private Srvtools.InfoCommand infoSalesMan;
        private Srvtools.InfoCommand infoCustArea;
        private Srvtools.InfoCommand ERPSalesDetails;
        private Srvtools.InfoCommand ERPSalesDetails2;
        private Srvtools.InfoCommand infoSalesType;
        private Srvtools.InfoCommand infoERPNewsType;
        private Srvtools.InfoCommand infoAcceptDateData;
        private Srvtools.InfoCommand infoSalesTypeAll;
        private Srvtools.InfoCommand infoInvoiceList;
        private Srvtools.InfoCommand infoCreateBy;
        private Srvtools.InfoCommand ERPCustomerToDoNotes;
        private Srvtools.InfoCommand infoSalesType2;
        private Srvtools.InfoCommand infoStats;
    }
}
