namespace sDevice
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.DeviceMaster = new Srvtools.InfoCommand(this.components);
            this.ucDeviceMaster = new Srvtools.UpdateComponent(this.components);
            this.DeviceItems = new Srvtools.InfoCommand(this.components);
            this.ucDeviceItems = new Srvtools.UpdateComponent(this.components);
            this.View_DeviceMaster = new Srvtools.InfoCommand(this.components);
            this.View_DeviceItems = new Srvtools.InfoCommand(this.components);
            this.DeviceMasterID = new Srvtools.AutoNumber(this.components);
            this.DeviceItemsID = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_DeviceMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_DeviceItems)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelMaster";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelMaster";
            service2.DelegateName = "CheckDelItems";
            service2.NonLogin = false;
            service2.ServiceName = "CheckDelItems";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // DeviceMaster
            // 
            this.DeviceMaster.CacheConnection = false;
            this.DeviceMaster.CommandText = "SELECT dbo.[DeviceMaster].* FROM dbo.[DeviceMaster]\r\nOrder by DeviceMasterID";
            this.DeviceMaster.CommandTimeout = 30;
            this.DeviceMaster.CommandType = System.Data.CommandType.Text;
            this.DeviceMaster.DynamicTableName = false;
            this.DeviceMaster.EEPAlias = null;
            this.DeviceMaster.EncodingAfter = null;
            this.DeviceMaster.EncodingBefore = "Windows-1252";
            this.DeviceMaster.EncodingConvert = null;
            this.DeviceMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "DeviceMasterID";
            this.DeviceMaster.KeyFields.Add(keyItem1);
            this.DeviceMaster.MultiSetWhere = false;
            this.DeviceMaster.Name = "DeviceMaster";
            this.DeviceMaster.NotificationAutoEnlist = false;
            this.DeviceMaster.SecExcept = null;
            this.DeviceMaster.SecFieldName = null;
            this.DeviceMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DeviceMaster.SelectPaging = false;
            this.DeviceMaster.SelectTop = 0;
            this.DeviceMaster.SiteControl = false;
            this.DeviceMaster.SiteFieldName = null;
            this.DeviceMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucDeviceMaster
            // 
            this.ucDeviceMaster.AutoTrans = true;
            this.ucDeviceMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "DeviceMasterID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "DeviceMasterName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateBy";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = "_usercode";
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "LastUpdateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = "_usercode";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LastUpdateDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucDeviceMaster.FieldAttrs.Add(fieldAttr1);
            this.ucDeviceMaster.FieldAttrs.Add(fieldAttr2);
            this.ucDeviceMaster.FieldAttrs.Add(fieldAttr3);
            this.ucDeviceMaster.FieldAttrs.Add(fieldAttr4);
            this.ucDeviceMaster.FieldAttrs.Add(fieldAttr5);
            this.ucDeviceMaster.FieldAttrs.Add(fieldAttr6);
            this.ucDeviceMaster.LogInfo = null;
            this.ucDeviceMaster.Name = "ucDeviceMaster";
            this.ucDeviceMaster.RowAffectsCheck = true;
            this.ucDeviceMaster.SelectCmd = this.DeviceMaster;
            this.ucDeviceMaster.SelectCmdForUpdate = null;
            this.ucDeviceMaster.SendSQLCmd = true;
            this.ucDeviceMaster.ServerModify = true;
            this.ucDeviceMaster.ServerModifyGetMax = false;
            this.ucDeviceMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucDeviceMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucDeviceMaster.UseTranscationScope = false;
            this.ucDeviceMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucDeviceMaster.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucDeviceMaster_BeforeInsert);
            this.ucDeviceMaster.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucDeviceMaster_BeforeModify);
            // 
            // DeviceItems
            // 
            this.DeviceItems.CacheConnection = false;
            this.DeviceItems.CommandText = "SELECT dbo.[DeviceItems].* FROM dbo.[DeviceItems] \r\nORDER BY DeviceItemsID";
            this.DeviceItems.CommandTimeout = 30;
            this.DeviceItems.CommandType = System.Data.CommandType.Text;
            this.DeviceItems.DynamicTableName = false;
            this.DeviceItems.EEPAlias = null;
            this.DeviceItems.EncodingAfter = null;
            this.DeviceItems.EncodingBefore = "Windows-1252";
            this.DeviceItems.EncodingConvert = null;
            this.DeviceItems.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DeviceItemsID";
            this.DeviceItems.KeyFields.Add(keyItem2);
            this.DeviceItems.MultiSetWhere = false;
            this.DeviceItems.Name = "DeviceItems";
            this.DeviceItems.NotificationAutoEnlist = false;
            this.DeviceItems.SecExcept = null;
            this.DeviceItems.SecFieldName = null;
            this.DeviceItems.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DeviceItems.SelectPaging = false;
            this.DeviceItems.SelectTop = 0;
            this.DeviceItems.SiteControl = false;
            this.DeviceItems.SiteFieldName = null;
            this.DeviceItems.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucDeviceItems
            // 
            this.ucDeviceItems.AutoTrans = true;
            this.ucDeviceItems.ExceptJoin = false;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "DeviceItemsID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "DeviceMasterID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "DeviceItemsName";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "DeviceLocation";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "DeviceNotes";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "IsAllowedUse";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LastUpdateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr7);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr8);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr9);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr10);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr11);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr12);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr13);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr14);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr15);
            this.ucDeviceItems.FieldAttrs.Add(fieldAttr16);
            this.ucDeviceItems.LogInfo = null;
            this.ucDeviceItems.Name = "ucDeviceItems";
            this.ucDeviceItems.RowAffectsCheck = true;
            this.ucDeviceItems.SelectCmd = this.DeviceItems;
            this.ucDeviceItems.SelectCmdForUpdate = null;
            this.ucDeviceItems.SendSQLCmd = true;
            this.ucDeviceItems.ServerModify = true;
            this.ucDeviceItems.ServerModifyGetMax = false;
            this.ucDeviceItems.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucDeviceItems.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucDeviceItems.UseTranscationScope = false;
            this.ucDeviceItems.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucDeviceItems.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucDeviceItems_BeforeInsert);
            this.ucDeviceItems.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucDeviceItems_BeforeModify);
            // 
            // View_DeviceMaster
            // 
            this.View_DeviceMaster.CacheConnection = false;
            this.View_DeviceMaster.CommandText = "SELECT * FROM dbo.[DeviceMaster]";
            this.View_DeviceMaster.CommandTimeout = 30;
            this.View_DeviceMaster.CommandType = System.Data.CommandType.Text;
            this.View_DeviceMaster.DynamicTableName = false;
            this.View_DeviceMaster.EEPAlias = null;
            this.View_DeviceMaster.EncodingAfter = null;
            this.View_DeviceMaster.EncodingBefore = "Windows-1252";
            this.View_DeviceMaster.EncodingConvert = null;
            this.View_DeviceMaster.InfoConnection = this.InfoConnection1;
            this.View_DeviceMaster.MultiSetWhere = false;
            this.View_DeviceMaster.Name = "View_DeviceMaster";
            this.View_DeviceMaster.NotificationAutoEnlist = false;
            this.View_DeviceMaster.SecExcept = null;
            this.View_DeviceMaster.SecFieldName = null;
            this.View_DeviceMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_DeviceMaster.SelectPaging = false;
            this.View_DeviceMaster.SelectTop = 0;
            this.View_DeviceMaster.SiteControl = false;
            this.View_DeviceMaster.SiteFieldName = null;
            this.View_DeviceMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_DeviceItems
            // 
            this.View_DeviceItems.CacheConnection = false;
            this.View_DeviceItems.CommandText = "SELECT * FROM dbo.[DeviceItems]";
            this.View_DeviceItems.CommandTimeout = 30;
            this.View_DeviceItems.CommandType = System.Data.CommandType.Text;
            this.View_DeviceItems.DynamicTableName = false;
            this.View_DeviceItems.EEPAlias = null;
            this.View_DeviceItems.EncodingAfter = null;
            this.View_DeviceItems.EncodingBefore = "Windows-1252";
            this.View_DeviceItems.EncodingConvert = null;
            this.View_DeviceItems.InfoConnection = this.InfoConnection1;
            this.View_DeviceItems.MultiSetWhere = false;
            this.View_DeviceItems.Name = "View_DeviceItems";
            this.View_DeviceItems.NotificationAutoEnlist = false;
            this.View_DeviceItems.SecExcept = null;
            this.View_DeviceItems.SecFieldName = null;
            this.View_DeviceItems.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_DeviceItems.SelectPaging = false;
            this.View_DeviceItems.SelectTop = 0;
            this.View_DeviceItems.SiteControl = false;
            this.View_DeviceItems.SiteFieldName = null;
            this.View_DeviceItems.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DeviceMasterID
            // 
            this.DeviceMasterID.Active = true;
            this.DeviceMasterID.AutoNoID = "DeviceMasterID";
            this.DeviceMasterID.Description = null;
            this.DeviceMasterID.GetFixed = "";
            this.DeviceMasterID.isNumFill = false;
            this.DeviceMasterID.Name = "DeviceMasterID";
            this.DeviceMasterID.Number = null;
            this.DeviceMasterID.NumDig = 3;
            this.DeviceMasterID.OldVersion = false;
            this.DeviceMasterID.OverFlow = true;
            this.DeviceMasterID.StartValue = 1;
            this.DeviceMasterID.Step = 1;
            this.DeviceMasterID.TargetColumn = "DeviceMasterID";
            this.DeviceMasterID.UpdateComp = this.ucDeviceMaster;
            // 
            // DeviceItemsID
            // 
            this.DeviceItemsID.Active = true;
            this.DeviceItemsID.AutoNoID = "DeviceItemsID";
            this.DeviceItemsID.Description = null;
            this.DeviceItemsID.GetFixed = "";
            this.DeviceItemsID.isNumFill = false;
            this.DeviceItemsID.Name = "DeviceItemsID";
            this.DeviceItemsID.Number = null;
            this.DeviceItemsID.NumDig = 3;
            this.DeviceItemsID.OldVersion = false;
            this.DeviceItemsID.OverFlow = true;
            this.DeviceItemsID.StartValue = 1;
            this.DeviceItemsID.Step = 1;
            this.DeviceItemsID.TargetColumn = "DeviceItemsID";
            this.DeviceItemsID.UpdateComp = this.ucDeviceItems;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_DeviceMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_DeviceItems)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand DeviceMaster;
        private Srvtools.UpdateComponent ucDeviceMaster;
        private Srvtools.InfoCommand DeviceItems;
        private Srvtools.UpdateComponent ucDeviceItems;
        private Srvtools.InfoCommand View_DeviceMaster;
        private Srvtools.InfoCommand View_DeviceItems;
        private Srvtools.AutoNumber DeviceMasterID;
        private Srvtools.AutoNumber DeviceItemsID;
    }
}
