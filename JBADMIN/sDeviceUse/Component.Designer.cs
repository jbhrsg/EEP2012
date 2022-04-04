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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.DeviceUse = new Srvtools.InfoCommand(this.components);
            this.ucDeviceUse = new Srvtools.UpdateComponent(this.components);
            this.View_DeviceUse = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceUse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_DeviceUse)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // DeviceUse
            // 
            this.DeviceUse.CacheConnection = false;
            this.DeviceUse.CommandText = "SELECT dbo.[DeviceUse].* FROM dbo.[DeviceUse]";
            this.DeviceUse.CommandTimeout = 30;
            this.DeviceUse.CommandType = System.Data.CommandType.Text;
            this.DeviceUse.DynamicTableName = false;
            this.DeviceUse.EEPAlias = null;
            this.DeviceUse.EncodingAfter = null;
            this.DeviceUse.EncodingBefore = "Windows-1252";
            this.DeviceUse.InfoConnection = this.InfoConnection1;
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
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LastUpdateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "LastUpdateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
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
            this.ucDeviceUse.LogInfo = null;
            this.ucDeviceUse.Name = "ucDeviceUse";
            this.ucDeviceUse.RowAffectsCheck = true;
            this.ucDeviceUse.SelectCmd = this.DeviceUse;
            this.ucDeviceUse.SelectCmdForUpdate = null;
            this.ucDeviceUse.ServerModify = true;
            this.ucDeviceUse.ServerModifyGetMax = false;
            this.ucDeviceUse.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucDeviceUse.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucDeviceUse.UseTranscationScope = false;
            this.ucDeviceUse.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_DeviceUse
            // 
            this.View_DeviceUse.CacheConnection = false;
            this.View_DeviceUse.CommandText = "SELECT * FROM dbo.[DeviceUse]";
            this.View_DeviceUse.CommandTimeout = 30;
            this.View_DeviceUse.CommandType = System.Data.CommandType.Text;
            this.View_DeviceUse.DynamicTableName = false;
            this.View_DeviceUse.EEPAlias = null;
            this.View_DeviceUse.EncodingAfter = null;
            this.View_DeviceUse.EncodingBefore = "Windows-1252";
            this.View_DeviceUse.InfoConnection = this.InfoConnection1;
            this.View_DeviceUse.MultiSetWhere = false;
            this.View_DeviceUse.Name = "View_DeviceUse";
            this.View_DeviceUse.NotificationAutoEnlist = false;
            this.View_DeviceUse.SecExcept = null;
            this.View_DeviceUse.SecFieldName = null;
            this.View_DeviceUse.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_DeviceUse.SelectPaging = false;
            this.View_DeviceUse.SelectTop = 0;
            this.View_DeviceUse.SiteControl = false;
            this.View_DeviceUse.SiteFieldName = null;
            this.View_DeviceUse.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceUse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_DeviceUse)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand DeviceUse;
        private Srvtools.UpdateComponent ucDeviceUse;
        private Srvtools.InfoCommand View_DeviceUse;
    }
}
