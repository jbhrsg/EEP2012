namespace sDeviceUse
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.DeviceUse = new Srvtools.InfoCommand(this.components);
            this.ucDeviceUse = new Srvtools.UpdateComponent(this.components);
            this.autoUseNO = new Srvtools.AutoNumber(this.components);
            this.infoDeviceMaster = new Srvtools.InfoCommand(this.components);
            this.infoDeviceItems = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceUse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDeviceMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDeviceItems)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckTimeOver";
            service1.NonLogin = false;
            service1.ServiceName = "CheckTimeOver";
            service2.DelegateName = "GetUserOrgNOs";
            service2.NonLogin = false;
            service2.ServiceName = "GetUserOrgNOs";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // DeviceUse
            // 
            this.DeviceUse.CacheConnection = false;
            this.DeviceUse.CommandText = resources.GetString("DeviceUse.CommandText");
            this.DeviceUse.CommandTimeout = 30;
            this.DeviceUse.CommandType = System.Data.CommandType.Text;
            this.DeviceUse.DynamicTableName = false;
            this.DeviceUse.EEPAlias = null;
            this.DeviceUse.EncodingAfter = null;
            this.DeviceUse.EncodingBefore = "Windows-1252";
            this.DeviceUse.EncodingConvert = null;
            this.DeviceUse.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "UseNO";
            this.DeviceUse.KeyFields.Add(keyItem1);
            this.DeviceUse.MultiSetWhere = false;
            this.DeviceUse.Name = "DeviceUse";
            this.DeviceUse.NotificationAutoEnlist = false;
            this.DeviceUse.SecExcept = null;
            this.DeviceUse.SecFieldName = null;
            this.DeviceUse.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DeviceUse.SelectPaging = false;
            this.DeviceUse.SelectTop = 0;
            this.DeviceUse.SiteControl = false;
            this.DeviceUse.SiteFieldName = null;
            this.DeviceUse.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucDeviceUse
            // 
            this.ucDeviceUse.AutoTrans = true;
            this.ucDeviceUse.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "UseNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ApplyEmpID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ApplyDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "DeviceItemsID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "StaTime";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "EndTime";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "OutLine";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "IsActive";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateBy";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "LastUpdateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "LastUpdateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "ORG_NO";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr1);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr2);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr3);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr4);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr5);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr6);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr7);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr8);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr9);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr10);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr11);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr12);
            this.ucDeviceUse.FieldAttrs.Add(fieldAttr13);
            this.ucDeviceUse.LogInfo = null;
            this.ucDeviceUse.Name = "ucDeviceUse";
            this.ucDeviceUse.RowAffectsCheck = true;
            this.ucDeviceUse.SelectCmd = this.DeviceUse;
            this.ucDeviceUse.SelectCmdForUpdate = null;
            this.ucDeviceUse.SendSQLCmd = true;
            this.ucDeviceUse.ServerModify = true;
            this.ucDeviceUse.ServerModifyGetMax = false;
            this.ucDeviceUse.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucDeviceUse.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucDeviceUse.UseTranscationScope = false;
            this.ucDeviceUse.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucDeviceUse.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucDeviceUse_BeforeInsert);
            this.ucDeviceUse.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucDeviceUse_BeforeModify);
            // 
            // autoUseNO
            // 
            this.autoUseNO.Active = true;
            this.autoUseNO.AutoNoID = "UseNO";
            this.autoUseNO.Description = null;
            this.autoUseNO.GetFixed = "L";
            this.autoUseNO.isNumFill = false;
            this.autoUseNO.Name = "autoUseNO";
            this.autoUseNO.Number = null;
            this.autoUseNO.NumDig = 8;
            this.autoUseNO.OldVersion = false;
            this.autoUseNO.OverFlow = true;
            this.autoUseNO.StartValue = 1;
            this.autoUseNO.Step = 1;
            this.autoUseNO.TargetColumn = "UseNO";
            this.autoUseNO.UpdateComp = this.ucDeviceUse;
            // 
            // infoDeviceMaster
            // 
            this.infoDeviceMaster.CacheConnection = false;
            this.infoDeviceMaster.CommandText = "select DeviceMaster.DeviceMasterID,DeviceMaster.DeviceMasterName from DeviceMaste" +
    "r\r\nwhere DeviceMaster.DeviceMasterID!=1\r\norder by DeviceMaster.DeviceMasterID";
            this.infoDeviceMaster.CommandTimeout = 30;
            this.infoDeviceMaster.CommandType = System.Data.CommandType.Text;
            this.infoDeviceMaster.DynamicTableName = false;
            this.infoDeviceMaster.EEPAlias = null;
            this.infoDeviceMaster.EncodingAfter = null;
            this.infoDeviceMaster.EncodingBefore = "Windows-1252";
            this.infoDeviceMaster.EncodingConvert = null;
            this.infoDeviceMaster.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DeviceMasterID";
            this.infoDeviceMaster.KeyFields.Add(keyItem2);
            this.infoDeviceMaster.MultiSetWhere = false;
            this.infoDeviceMaster.Name = "infoDeviceMaster";
            this.infoDeviceMaster.NotificationAutoEnlist = false;
            this.infoDeviceMaster.SecExcept = null;
            this.infoDeviceMaster.SecFieldName = null;
            this.infoDeviceMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoDeviceMaster.SelectPaging = false;
            this.infoDeviceMaster.SelectTop = 0;
            this.infoDeviceMaster.SiteControl = false;
            this.infoDeviceMaster.SiteFieldName = null;
            this.infoDeviceMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoDeviceItems
            // 
            this.infoDeviceItems.CacheConnection = false;
            this.infoDeviceItems.CommandText = "select DeviceItems.DeviceItemsID,DeviceItems.DeviceItemsName from DeviceItems\r\nwh" +
    "ere DeviceItems.DeviceMasterID!=1 and IsAllowedUse=1\r\norder by DeviceItems.Devic" +
    "eMasterID";
            this.infoDeviceItems.CommandTimeout = 30;
            this.infoDeviceItems.CommandType = System.Data.CommandType.Text;
            this.infoDeviceItems.DynamicTableName = false;
            this.infoDeviceItems.EEPAlias = null;
            this.infoDeviceItems.EncodingAfter = null;
            this.infoDeviceItems.EncodingBefore = "Windows-1252";
            this.infoDeviceItems.EncodingConvert = null;
            this.infoDeviceItems.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "DeviceItemsID";
            this.infoDeviceItems.KeyFields.Add(keyItem3);
            this.infoDeviceItems.MultiSetWhere = false;
            this.infoDeviceItems.Name = "infoDeviceItems";
            this.infoDeviceItems.NotificationAutoEnlist = false;
            this.infoDeviceItems.SecExcept = null;
            this.infoDeviceItems.SecFieldName = null;
            this.infoDeviceItems.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoDeviceItems.SelectPaging = false;
            this.infoDeviceItems.SelectTop = 0;
            this.infoDeviceItems.SiteControl = false;
            this.infoDeviceItems.SiteFieldName = null;
            this.infoDeviceItems.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceUse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDeviceMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDeviceItems)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand DeviceUse;
        private Srvtools.UpdateComponent ucDeviceUse;
        private Srvtools.AutoNumber autoUseNO;
        private Srvtools.InfoCommand infoDeviceMaster;
        private Srvtools.InfoCommand infoDeviceItems;
    }
}
