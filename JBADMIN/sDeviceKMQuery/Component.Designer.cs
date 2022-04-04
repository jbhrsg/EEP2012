namespace sDeviceKMQuery
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.DeviceKMQuery = new Srvtools.InfoCommand(this.components);
            this.ucDeviceKMQuery = new Srvtools.UpdateComponent(this.components);
            this.View_DeviceKMQuery = new Srvtools.InfoCommand(this.components);
            this.DeviceItem = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceKMQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_DeviceKMQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceItem)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetDeviceKMRate";
            service1.NonLogin = false;
            service1.ServiceName = "GetDeviceKMRate";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // DeviceKMQuery
            // 
            this.DeviceKMQuery.CacheConnection = false;
            this.DeviceKMQuery.CommandText = "SELECT A.* ,B.DeviceItemsName AS DeviceName,C.CostCenterName\r\nFROM DeviceKMQuery " +
    "A,DeviceItems B,GlCostCenter C\r\nWHERE A.DeviceItemsID=B.DeviceItemsID AND A.Cost" +
    "CenterID=C.CostCenterID";
            this.DeviceKMQuery.CommandTimeout = 30;
            this.DeviceKMQuery.CommandType = System.Data.CommandType.Text;
            this.DeviceKMQuery.DynamicTableName = false;
            this.DeviceKMQuery.EEPAlias = null;
            this.DeviceKMQuery.EncodingAfter = null;
            this.DeviceKMQuery.EncodingBefore = "Windows-1252";
            this.DeviceKMQuery.EncodingConvert = null;
            this.DeviceKMQuery.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.DeviceKMQuery.KeyFields.Add(keyItem1);
            this.DeviceKMQuery.MultiSetWhere = false;
            this.DeviceKMQuery.Name = "DeviceKMQuery";
            this.DeviceKMQuery.NotificationAutoEnlist = false;
            this.DeviceKMQuery.SecExcept = null;
            this.DeviceKMQuery.SecFieldName = null;
            this.DeviceKMQuery.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DeviceKMQuery.SelectPaging = false;
            this.DeviceKMQuery.SelectTop = 0;
            this.DeviceKMQuery.SiteControl = false;
            this.DeviceKMQuery.SiteFieldName = null;
            this.DeviceKMQuery.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucDeviceKMQuery
            // 
            this.ucDeviceKMQuery.AutoTrans = true;
            this.ucDeviceKMQuery.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "UserID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "DeviceItemsID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CostCenterID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "KMs";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "KMRate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucDeviceKMQuery.FieldAttrs.Add(fieldAttr1);
            this.ucDeviceKMQuery.FieldAttrs.Add(fieldAttr2);
            this.ucDeviceKMQuery.FieldAttrs.Add(fieldAttr3);
            this.ucDeviceKMQuery.FieldAttrs.Add(fieldAttr4);
            this.ucDeviceKMQuery.FieldAttrs.Add(fieldAttr5);
            this.ucDeviceKMQuery.FieldAttrs.Add(fieldAttr6);
            this.ucDeviceKMQuery.LogInfo = null;
            this.ucDeviceKMQuery.Name = "ucDeviceKMQuery";
            this.ucDeviceKMQuery.RowAffectsCheck = true;
            this.ucDeviceKMQuery.SelectCmd = this.DeviceKMQuery;
            this.ucDeviceKMQuery.SelectCmdForUpdate = null;
            this.ucDeviceKMQuery.SendSQLCmd = true;
            this.ucDeviceKMQuery.ServerModify = true;
            this.ucDeviceKMQuery.ServerModifyGetMax = false;
            this.ucDeviceKMQuery.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucDeviceKMQuery.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucDeviceKMQuery.UseTranscationScope = false;
            this.ucDeviceKMQuery.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_DeviceKMQuery
            // 
            this.View_DeviceKMQuery.CacheConnection = false;
            this.View_DeviceKMQuery.CommandText = "SELECT * FROM dbo.[DeviceKMQuery]";
            this.View_DeviceKMQuery.CommandTimeout = 30;
            this.View_DeviceKMQuery.CommandType = System.Data.CommandType.Text;
            this.View_DeviceKMQuery.DynamicTableName = false;
            this.View_DeviceKMQuery.EEPAlias = null;
            this.View_DeviceKMQuery.EncodingAfter = null;
            this.View_DeviceKMQuery.EncodingBefore = "Windows-1252";
            this.View_DeviceKMQuery.EncodingConvert = null;
            this.View_DeviceKMQuery.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.View_DeviceKMQuery.KeyFields.Add(keyItem2);
            this.View_DeviceKMQuery.MultiSetWhere = false;
            this.View_DeviceKMQuery.Name = "View_DeviceKMQuery";
            this.View_DeviceKMQuery.NotificationAutoEnlist = false;
            this.View_DeviceKMQuery.SecExcept = null;
            this.View_DeviceKMQuery.SecFieldName = null;
            this.View_DeviceKMQuery.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_DeviceKMQuery.SelectPaging = false;
            this.View_DeviceKMQuery.SelectTop = 0;
            this.View_DeviceKMQuery.SiteControl = false;
            this.View_DeviceKMQuery.SiteFieldName = null;
            this.View_DeviceKMQuery.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DeviceItem
            // 
            this.DeviceItem.CacheConnection = false;
            this.DeviceItem.CommandText = "SELECT DeviceItemsID,DeviceItemsName FROM DeviceItems\r\nWHERE DeviceItemsID IN (5," +
    "7)\r\nORDER BY  DeviceItemsID";
            this.DeviceItem.CommandTimeout = 30;
            this.DeviceItem.CommandType = System.Data.CommandType.Text;
            this.DeviceItem.DynamicTableName = false;
            this.DeviceItem.EEPAlias = null;
            this.DeviceItem.EncodingAfter = null;
            this.DeviceItem.EncodingBefore = "Windows-1252";
            this.DeviceItem.EncodingConvert = null;
            this.DeviceItem.InfoConnection = this.InfoConnection1;
            this.DeviceItem.MultiSetWhere = false;
            this.DeviceItem.Name = "DeviceItem";
            this.DeviceItem.NotificationAutoEnlist = false;
            this.DeviceItem.SecExcept = null;
            this.DeviceItem.SecFieldName = null;
            this.DeviceItem.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DeviceItem.SelectPaging = false;
            this.DeviceItem.SelectTop = 0;
            this.DeviceItem.SiteControl = false;
            this.DeviceItem.SiteFieldName = null;
            this.DeviceItem.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceKMQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_DeviceKMQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceItem)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand DeviceKMQuery;
        private Srvtools.UpdateComponent ucDeviceKMQuery;
        private Srvtools.InfoCommand View_DeviceKMQuery;
        private Srvtools.InfoCommand DeviceItem;
    }
}
