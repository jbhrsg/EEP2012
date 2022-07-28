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
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.Service service4 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.infoCustomers = new Srvtools.InfoCommand(this.components);
            this.InfoConnJCS = new Srvtools.InfoConnection(this.components);
            this.infoConnection = new Srvtools.InfoConnection(this.components);
            this.infoRoomerData = new Srvtools.InfoCommand(this.components);
            this.infoDorm = new Srvtools.InfoCommand(this.components);
            this.infoInOutStatus = new Srvtools.InfoCommand(this.components);
            this.infoRoomerType = new Srvtools.InfoCommand(this.components);
            this.infoCommand1 = new Srvtools.InfoCommand(this.components);
            this.infoRoomerCard = new Srvtools.InfoCommand(this.components);
            this.infoRoomCustList = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnJCS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRoomerData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDorm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoInOutStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRoomerType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCommand1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRoomerCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRoomCustList)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "procReportRoomerList";
            service1.NonLogin = false;
            service1.ServiceName = "procReportRoomerList";
            service2.DelegateName = "procReportRoomerCardDetails";
            service2.NonLogin = false;
            service2.ServiceName = "procReportRoomerCardDetails";
            service3.DelegateName = "GetCustData";
            service3.NonLogin = false;
            service3.ServiceName = "GetCustData";
            service4.DelegateName = "procReportRoomCustList";
            service4.NonLogin = false;
            service4.ServiceName = "procReportRoomCustList";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            // 
            // infoCustomers
            // 
            this.infoCustomers.CacheConnection = false;
            this.infoCustomers.CommandText = "SELECT *\r\nFROM vAllCustomers";
            this.infoCustomers.CommandTimeout = 30;
            this.infoCustomers.CommandType = System.Data.CommandType.Text;
            this.infoCustomers.DynamicTableName = false;
            this.infoCustomers.EEPAlias = "JCS";
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
            this.InfoConnJCS.EEPAlias = "JCS";
            // 
            // infoConnection
            // 
            this.infoConnection.EEPAlias = "JBADMIN";
            // 
            // infoRoomerData
            // 
            this.infoRoomerData.CacheConnection = false;
            this.infoRoomerData.CommandText = resources.GetString("infoRoomerData.CommandText");
            this.infoRoomerData.CommandTimeout = 30;
            this.infoRoomerData.CommandType = System.Data.CommandType.Text;
            this.infoRoomerData.DynamicTableName = false;
            this.infoRoomerData.EEPAlias = "JCS1";
            this.infoRoomerData.EncodingAfter = null;
            this.infoRoomerData.EncodingBefore = "Windows-1252";
            this.infoRoomerData.EncodingConvert = null;
            this.infoRoomerData.InfoConnection = this.InfoConnJCS;
            this.infoRoomerData.MultiSetWhere = false;
            this.infoRoomerData.Name = "infoRoomerData";
            this.infoRoomerData.NotificationAutoEnlist = false;
            this.infoRoomerData.SecExcept = null;
            this.infoRoomerData.SecFieldName = null;
            this.infoRoomerData.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoRoomerData.SelectPaging = false;
            this.infoRoomerData.SelectTop = 0;
            this.infoRoomerData.SiteControl = false;
            this.infoRoomerData.SiteFieldName = null;
            this.infoRoomerData.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoDorm
            // 
            this.infoDorm.CacheConnection = false;
            this.infoDorm.CommandText = "SELECT \'傑誠宿舍\' as DormName,\'JCS\' as DormID\r\nunion\r\nSELECT \'平鎮宿舍\',\'JCS1\'\r\nunion\r\nSE" +
    "LECT \'長安宿舍\',\'JCS2\'\r\norder by DormID\r\n";
            this.infoDorm.CommandTimeout = 30;
            this.infoDorm.CommandType = System.Data.CommandType.Text;
            this.infoDorm.DynamicTableName = false;
            this.infoDorm.EEPAlias = "JCS1";
            this.infoDorm.EncodingAfter = null;
            this.infoDorm.EncodingBefore = "Windows-1252";
            this.infoDorm.EncodingConvert = null;
            this.infoDorm.InfoConnection = null;
            this.infoDorm.MultiSetWhere = false;
            this.infoDorm.Name = "infoDorm";
            this.infoDorm.NotificationAutoEnlist = false;
            this.infoDorm.SecExcept = null;
            this.infoDorm.SecFieldName = null;
            this.infoDorm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoDorm.SelectPaging = false;
            this.infoDorm.SelectTop = 0;
            this.infoDorm.SiteControl = false;
            this.infoDorm.SiteFieldName = null;
            this.infoDorm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoInOutStatus
            // 
            this.infoInOutStatus.CacheConnection = false;
            this.infoInOutStatus.CommandText = "SELECT \'不拘\' as Status,0 as StatusID\r\nunion\r\nSELECT \'進\',1\r\nunion\r\nSELECT \'出\',2\r\nor" +
    "der by StatusID";
            this.infoInOutStatus.CommandTimeout = 30;
            this.infoInOutStatus.CommandType = System.Data.CommandType.Text;
            this.infoInOutStatus.DynamicTableName = false;
            this.infoInOutStatus.EEPAlias = "JCS1";
            this.infoInOutStatus.EncodingAfter = null;
            this.infoInOutStatus.EncodingBefore = "Windows-1252";
            this.infoInOutStatus.EncodingConvert = null;
            this.infoInOutStatus.InfoConnection = null;
            this.infoInOutStatus.MultiSetWhere = false;
            this.infoInOutStatus.Name = "infoInOutStatus";
            this.infoInOutStatus.NotificationAutoEnlist = false;
            this.infoInOutStatus.SecExcept = null;
            this.infoInOutStatus.SecFieldName = null;
            this.infoInOutStatus.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoInOutStatus.SelectPaging = false;
            this.infoInOutStatus.SelectTop = 0;
            this.infoInOutStatus.SiteControl = false;
            this.infoInOutStatus.SiteFieldName = null;
            this.infoInOutStatus.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoRoomerType
            // 
            this.infoRoomerType.CacheConnection = false;
            this.infoRoomerType.CommandText = "SELECT \'所有房客\' as Type,0 as TypeID\r\nunion\r\nSELECT \'現住房客\',1\r\nunion\r\nSELECT \'遷出房客\',2" +
    "\r\norder by  TypeID";
            this.infoRoomerType.CommandTimeout = 30;
            this.infoRoomerType.CommandType = System.Data.CommandType.Text;
            this.infoRoomerType.DynamicTableName = false;
            this.infoRoomerType.EEPAlias = "JCS1";
            this.infoRoomerType.EncodingAfter = null;
            this.infoRoomerType.EncodingBefore = "Windows-1252";
            this.infoRoomerType.EncodingConvert = null;
            this.infoRoomerType.InfoConnection = null;
            this.infoRoomerType.MultiSetWhere = false;
            this.infoRoomerType.Name = "infoRoomerType";
            this.infoRoomerType.NotificationAutoEnlist = false;
            this.infoRoomerType.SecExcept = null;
            this.infoRoomerType.SecFieldName = null;
            this.infoRoomerType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoRoomerType.SelectPaging = false;
            this.infoRoomerType.SelectTop = 0;
            this.infoRoomerType.SiteControl = false;
            this.infoRoomerType.SiteFieldName = null;
            this.infoRoomerType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCommand1
            // 
            this.infoCommand1.CacheConnection = false;
            this.infoCommand1.CommandText = resources.GetString("infoCommand1.CommandText");
            this.infoCommand1.CommandTimeout = 30;
            this.infoCommand1.CommandType = System.Data.CommandType.Text;
            this.infoCommand1.DynamicTableName = false;
            this.infoCommand1.EEPAlias = "JCS1";
            this.infoCommand1.EncodingAfter = null;
            this.infoCommand1.EncodingBefore = "Windows-1252";
            this.infoCommand1.EncodingConvert = null;
            this.infoCommand1.InfoConnection = null;
            this.infoCommand1.MultiSetWhere = false;
            this.infoCommand1.Name = "infoCommand1";
            this.infoCommand1.NotificationAutoEnlist = false;
            this.infoCommand1.SecExcept = null;
            this.infoCommand1.SecFieldName = null;
            this.infoCommand1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCommand1.SelectPaging = false;
            this.infoCommand1.SelectTop = 0;
            this.infoCommand1.SiteControl = false;
            this.infoCommand1.SiteFieldName = null;
            this.infoCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoRoomerCard
            // 
            this.infoRoomerCard.CacheConnection = false;
            this.infoRoomerCard.CommandText = resources.GetString("infoRoomerCard.CommandText");
            this.infoRoomerCard.CommandTimeout = 30;
            this.infoRoomerCard.CommandType = System.Data.CommandType.Text;
            this.infoRoomerCard.DynamicTableName = false;
            this.infoRoomerCard.EEPAlias = "JCS1";
            this.infoRoomerCard.EncodingAfter = null;
            this.infoRoomerCard.EncodingBefore = "Windows-1252";
            this.infoRoomerCard.EncodingConvert = null;
            this.infoRoomerCard.InfoConnection = null;
            this.infoRoomerCard.MultiSetWhere = false;
            this.infoRoomerCard.Name = "infoRoomerCard";
            this.infoRoomerCard.NotificationAutoEnlist = false;
            this.infoRoomerCard.SecExcept = null;
            this.infoRoomerCard.SecFieldName = null;
            this.infoRoomerCard.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoRoomerCard.SelectPaging = false;
            this.infoRoomerCard.SelectTop = 0;
            this.infoRoomerCard.SiteControl = false;
            this.infoRoomerCard.SiteFieldName = null;
            this.infoRoomerCard.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoRoomCustList
            // 
            this.infoRoomCustList.CacheConnection = false;
            this.infoRoomCustList.CommandText = resources.GetString("infoRoomCustList.CommandText");
            this.infoRoomCustList.CommandTimeout = 30;
            this.infoRoomCustList.CommandType = System.Data.CommandType.Text;
            this.infoRoomCustList.DynamicTableName = false;
            this.infoRoomCustList.EEPAlias = "JCS1";
            this.infoRoomCustList.EncodingAfter = null;
            this.infoRoomCustList.EncodingBefore = "Windows-1252";
            this.infoRoomCustList.EncodingConvert = null;
            this.infoRoomCustList.InfoConnection = this.InfoConnJCS;
            this.infoRoomCustList.MultiSetWhere = false;
            this.infoRoomCustList.Name = "infoRoomCustList";
            this.infoRoomCustList.NotificationAutoEnlist = false;
            this.infoRoomCustList.SecExcept = null;
            this.infoRoomCustList.SecFieldName = null;
            this.infoRoomCustList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoRoomCustList.SelectPaging = false;
            this.infoRoomCustList.SelectTop = 0;
            this.infoRoomCustList.SiteControl = false;
            this.infoRoomCustList.SiteFieldName = null;
            this.infoRoomCustList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnJCS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRoomerData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDorm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoInOutStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRoomerType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCommand1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRoomerCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRoomCustList)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoCommand infoCustomers;
        private Srvtools.InfoConnection infoConnection;
        private Srvtools.InfoCommand infoRoomerData;
        private Srvtools.InfoCommand infoDorm;
        private Srvtools.InfoCommand infoInOutStatus;
        private Srvtools.InfoCommand infoRoomerType;
        private Srvtools.InfoCommand infoCommand1;
        private Srvtools.InfoCommand infoRoomerCard;
        private Srvtools.InfoConnection InfoConnJCS;
        private Srvtools.InfoCommand infoRoomCustList;
    }
}
