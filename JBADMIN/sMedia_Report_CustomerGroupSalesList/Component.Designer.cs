namespace sMedia_Report_CustomerGroupSalesList
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
            this.QueryColumn = new Srvtools.InfoCommand(this.components);
            this.infoERPDMType = new Srvtools.InfoCommand(this.components);
            this.infoERPViewArea = new Srvtools.InfoCommand(this.components);
            this.infoERPSalesType = new Srvtools.InfoCommand(this.components);
            this.infoCustomers = new Srvtools.InfoCommand(this.components);
            this.infoSalesMan = new Srvtools.InfoCommand(this.components);
            this.infoERPNewsArea = new Srvtools.InfoCommand(this.components);
            this.ERPLetterType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPDMType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPViewArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPLetterType)).BeginInit();
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
            // QueryColumn
            // 
            this.QueryColumn.CacheConnection = false;
            this.QueryColumn.CommandText = "SELECT top 1 \'\' as CustNO ,\'\' as SalesID ,\'\' as SalesDate ,\'\' as SalesTypeID,\'\' a" +
    "s DMTypeID,\'\' as ViewAreaID ,\'\' as ReportType,\'\' as NewsAreaID,\'\' as LetterType " +
    " from ERPSalesMaster";
            this.QueryColumn.CommandTimeout = 30;
            this.QueryColumn.CommandType = System.Data.CommandType.Text;
            this.QueryColumn.DynamicTableName = false;
            this.QueryColumn.EEPAlias = null;
            this.QueryColumn.EncodingAfter = null;
            this.QueryColumn.EncodingBefore = "Windows-1252";
            this.QueryColumn.EncodingConvert = null;
            this.QueryColumn.InfoConnection = this.InfoConnection1;
            this.QueryColumn.MultiSetWhere = false;
            this.QueryColumn.Name = "QueryColumn";
            this.QueryColumn.NotificationAutoEnlist = false;
            this.QueryColumn.SecExcept = null;
            this.QueryColumn.SecFieldName = null;
            this.QueryColumn.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.QueryColumn.SelectPaging = false;
            this.QueryColumn.SelectTop = 0;
            this.QueryColumn.SiteControl = false;
            this.QueryColumn.SiteFieldName = null;
            this.QueryColumn.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoERPDMType
            // 
            this.infoERPDMType.CacheConnection = false;
            this.infoERPDMType.CommandText = "select DMTypeNO,DMTypeID,Cast(DMTypeID as nvarchar(5))+\' : \'+DMTypeName as DMType" +
    "Name from ERPDMType\r\nwhere IsActive=1";
            this.infoERPDMType.CommandTimeout = 30;
            this.infoERPDMType.CommandType = System.Data.CommandType.Text;
            this.infoERPDMType.DynamicTableName = false;
            this.infoERPDMType.EEPAlias = null;
            this.infoERPDMType.EncodingAfter = null;
            this.infoERPDMType.EncodingBefore = "Windows-1252";
            this.infoERPDMType.EncodingConvert = null;
            this.infoERPDMType.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "DMTypeNO";
            this.infoERPDMType.KeyFields.Add(keyItem1);
            this.infoERPDMType.MultiSetWhere = false;
            this.infoERPDMType.Name = "infoERPDMType";
            this.infoERPDMType.NotificationAutoEnlist = false;
            this.infoERPDMType.SecExcept = null;
            this.infoERPDMType.SecFieldName = null;
            this.infoERPDMType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoERPDMType.SelectPaging = false;
            this.infoERPDMType.SelectTop = 0;
            this.infoERPDMType.SiteControl = false;
            this.infoERPDMType.SiteFieldName = null;
            this.infoERPDMType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoERPViewArea
            // 
            this.infoERPViewArea.CacheConnection = false;
            this.infoERPViewArea.CommandText = "select ViewAreaNO,ViewAreaID,ViewAreaName from ERPViewArea";
            this.infoERPViewArea.CommandTimeout = 30;
            this.infoERPViewArea.CommandType = System.Data.CommandType.Text;
            this.infoERPViewArea.DynamicTableName = false;
            this.infoERPViewArea.EEPAlias = null;
            this.infoERPViewArea.EncodingAfter = null;
            this.infoERPViewArea.EncodingBefore = "Windows-1252";
            this.infoERPViewArea.EncodingConvert = null;
            this.infoERPViewArea.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ViewAreaNO";
            this.infoERPViewArea.KeyFields.Add(keyItem2);
            this.infoERPViewArea.MultiSetWhere = false;
            this.infoERPViewArea.Name = "infoERPViewArea";
            this.infoERPViewArea.NotificationAutoEnlist = false;
            this.infoERPViewArea.SecExcept = null;
            this.infoERPViewArea.SecFieldName = null;
            this.infoERPViewArea.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoERPViewArea.SelectPaging = false;
            this.infoERPViewArea.SelectTop = 0;
            this.infoERPViewArea.SiteControl = false;
            this.infoERPViewArea.SiteFieldName = null;
            this.infoERPViewArea.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoERPSalesType
            // 
            this.infoERPSalesType.CacheConnection = false;
            this.infoERPSalesType.CommandText = "select SalesTypeNO,SalesTypeID,Cast(SalesTypeID as nvarchar(5))+\' : \'+SalesTypeNa" +
    "me as SalesTypeName from ERPSalesType\r\nwhere IsActive=1 and SalesTypeID in (\'1\'," +
    "\'6\',\'31\')\r\n";
            this.infoERPSalesType.CommandTimeout = 30;
            this.infoERPSalesType.CommandType = System.Data.CommandType.Text;
            this.infoERPSalesType.DynamicTableName = false;
            this.infoERPSalesType.EEPAlias = null;
            this.infoERPSalesType.EncodingAfter = null;
            this.infoERPSalesType.EncodingBefore = "Windows-1252";
            this.infoERPSalesType.EncodingConvert = null;
            this.infoERPSalesType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "SalesTypeNO";
            this.infoERPSalesType.KeyFields.Add(keyItem3);
            this.infoERPSalesType.MultiSetWhere = false;
            this.infoERPSalesType.Name = "infoERPSalesType";
            this.infoERPSalesType.NotificationAutoEnlist = false;
            this.infoERPSalesType.SecExcept = null;
            this.infoERPSalesType.SecFieldName = null;
            this.infoERPSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoERPSalesType.SelectPaging = false;
            this.infoERPSalesType.SelectTop = 0;
            this.infoERPSalesType.SiteControl = false;
            this.infoERPSalesType.SiteFieldName = null;
            this.infoERPSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCustomers
            // 
            this.infoCustomers.CacheConnection = false;
            this.infoCustomers.CommandText = "select top 500 CustNO,CustName,CustTelNO\r\nfrom DBO.ERPCustomers\r\n\r\n";
            this.infoCustomers.CommandTimeout = 30;
            this.infoCustomers.CommandType = System.Data.CommandType.Text;
            this.infoCustomers.DynamicTableName = false;
            this.infoCustomers.EEPAlias = "";
            this.infoCustomers.EncodingAfter = null;
            this.infoCustomers.EncodingBefore = "Windows-1252";
            this.infoCustomers.EncodingConvert = null;
            this.infoCustomers.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "CustNO";
            this.infoCustomers.KeyFields.Add(keyItem4);
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
            keyItem5.KeyName = "SalseNO";
            this.infoSalesMan.KeyFields.Add(keyItem5);
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
            keyItem6.KeyName = "NewsAreaNO";
            this.infoERPNewsArea.KeyFields.Add(keyItem6);
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
            // 
            // ERPLetterType
            // 
            this.ERPLetterType.CacheConnection = false;
            this.ERPLetterType.CommandText = "select * from ERPLetterType order by LetterTypeID";
            this.ERPLetterType.CommandTimeout = 30;
            this.ERPLetterType.CommandType = System.Data.CommandType.Text;
            this.ERPLetterType.DynamicTableName = false;
            this.ERPLetterType.EEPAlias = null;
            this.ERPLetterType.EncodingAfter = null;
            this.ERPLetterType.EncodingBefore = "Windows-1252";
            this.ERPLetterType.EncodingConvert = null;
            this.ERPLetterType.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "LetterTypeNO";
            this.ERPLetterType.KeyFields.Add(keyItem7);
            this.ERPLetterType.MultiSetWhere = false;
            this.ERPLetterType.Name = "ERPLetterType";
            this.ERPLetterType.NotificationAutoEnlist = false;
            this.ERPLetterType.SecExcept = null;
            this.ERPLetterType.SecFieldName = null;
            this.ERPLetterType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPLetterType.SelectPaging = false;
            this.ERPLetterType.SelectTop = 0;
            this.ERPLetterType.SiteControl = false;
            this.ERPLetterType.SiteFieldName = null;
            this.ERPLetterType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPDMType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPViewArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPNewsArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPLetterType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand QueryColumn;
        private Srvtools.InfoCommand infoERPDMType;
        private Srvtools.InfoCommand infoERPViewArea;
        private Srvtools.InfoCommand infoERPSalesType;
        private Srvtools.InfoCommand infoCustomers;
        private Srvtools.InfoCommand infoSalesMan;
        private Srvtools.InfoCommand infoERPNewsArea;
        private Srvtools.InfoCommand ERPLetterType;
    }
}
