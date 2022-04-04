namespace sCON_ACTIVITYMASTER
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CON_ACTIVITYMASTER = new Srvtools.InfoCommand(this.components);
            this.ucCON_ACTIVITYMASTER = new Srvtools.UpdateComponent(this.components);
            this.CON_ACTIVITYDETAILS = new Srvtools.InfoCommand(this.components);
            this.ucCON_ACTIVITYDETAILS = new Srvtools.UpdateComponent(this.components);
            this.View_CON_ACTIVITYMASTER = new Srvtools.InfoCommand(this.components);
            this.View_CON_ACTIVITYDETAILS = new Srvtools.InfoCommand(this.components);
            this.CON_ACTIVITYDETAILSTEMP = new Srvtools.InfoCommand(this.components);
            this.CENTER = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_ACTIVITYMASTER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_ACTIVITYDETAILS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_ACTIVITYMASTER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_ACTIVITYDETAILS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_ACTIVITYDETAILSTEMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CENTER)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "DeleteActivityContact";
            service1.NonLogin = false;
            service1.ServiceName = "DeleteActivityContact";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // CON_ACTIVITYMASTER
            // 
            this.CON_ACTIVITYMASTER.CacheConnection = false;
            this.CON_ACTIVITYMASTER.CommandText = resources.GetString("CON_ACTIVITYMASTER.CommandText");
            this.CON_ACTIVITYMASTER.CommandTimeout = 30;
            this.CON_ACTIVITYMASTER.CommandType = System.Data.CommandType.Text;
            this.CON_ACTIVITYMASTER.DynamicTableName = false;
            this.CON_ACTIVITYMASTER.EEPAlias = null;
            this.CON_ACTIVITYMASTER.EncodingAfter = null;
            this.CON_ACTIVITYMASTER.EncodingBefore = "Windows-1252";
            this.CON_ACTIVITYMASTER.EncodingConvert = null;
            this.CON_ACTIVITYMASTER.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ACTIVITY_ID";
            this.CON_ACTIVITYMASTER.KeyFields.Add(keyItem1);
            this.CON_ACTIVITYMASTER.MultiSetWhere = false;
            this.CON_ACTIVITYMASTER.Name = "CON_ACTIVITYMASTER";
            this.CON_ACTIVITYMASTER.NotificationAutoEnlist = false;
            this.CON_ACTIVITYMASTER.SecExcept = null;
            this.CON_ACTIVITYMASTER.SecFieldName = null;
            this.CON_ACTIVITYMASTER.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_ACTIVITYMASTER.SelectPaging = false;
            this.CON_ACTIVITYMASTER.SelectTop = 0;
            this.CON_ACTIVITYMASTER.SiteControl = false;
            this.CON_ACTIVITYMASTER.SiteFieldName = null;
            this.CON_ACTIVITYMASTER.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_ACTIVITYMASTER
            // 
            this.ucCON_ACTIVITYMASTER.AutoTrans = true;
            this.ucCON_ACTIVITYMASTER.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ACTIVITY_ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ACIIVITY_NAME";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ACTIVITY_DATE";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CREATE_MAN";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = "_username";
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CREATE_DATE";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            this.ucCON_ACTIVITYMASTER.FieldAttrs.Add(fieldAttr1);
            this.ucCON_ACTIVITYMASTER.FieldAttrs.Add(fieldAttr2);
            this.ucCON_ACTIVITYMASTER.FieldAttrs.Add(fieldAttr3);
            this.ucCON_ACTIVITYMASTER.FieldAttrs.Add(fieldAttr4);
            this.ucCON_ACTIVITYMASTER.FieldAttrs.Add(fieldAttr5);
            this.ucCON_ACTIVITYMASTER.LogInfo = null;
            this.ucCON_ACTIVITYMASTER.Name = "ucCON_ACTIVITYMASTER";
            this.ucCON_ACTIVITYMASTER.RowAffectsCheck = true;
            this.ucCON_ACTIVITYMASTER.SelectCmd = this.CON_ACTIVITYMASTER;
            this.ucCON_ACTIVITYMASTER.SelectCmdForUpdate = null;
            this.ucCON_ACTIVITYMASTER.SendSQLCmd = true;
            this.ucCON_ACTIVITYMASTER.ServerModify = true;
            this.ucCON_ACTIVITYMASTER.ServerModifyGetMax = false;
            this.ucCON_ACTIVITYMASTER.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_ACTIVITYMASTER.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_ACTIVITYMASTER.UseTranscationScope = false;
            this.ucCON_ACTIVITYMASTER.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCON_ACTIVITYMASTER.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCON_ACTIVITYMASTER_BeforeInsert);
            // 
            // CON_ACTIVITYDETAILS
            // 
            this.CON_ACTIVITYDETAILS.CacheConnection = false;
            this.CON_ACTIVITYDETAILS.CommandText = resources.GetString("CON_ACTIVITYDETAILS.CommandText");
            this.CON_ACTIVITYDETAILS.CommandTimeout = 30;
            this.CON_ACTIVITYDETAILS.CommandType = System.Data.CommandType.Text;
            this.CON_ACTIVITYDETAILS.DynamicTableName = false;
            this.CON_ACTIVITYDETAILS.EEPAlias = null;
            this.CON_ACTIVITYDETAILS.EncodingAfter = null;
            this.CON_ACTIVITYDETAILS.EncodingBefore = "Windows-1252";
            this.CON_ACTIVITYDETAILS.EncodingConvert = null;
            this.CON_ACTIVITYDETAILS.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CENTER_ID";
            keyItem3.KeyName = "CONTACT_ID";
            this.CON_ACTIVITYDETAILS.KeyFields.Add(keyItem2);
            this.CON_ACTIVITYDETAILS.KeyFields.Add(keyItem3);
            this.CON_ACTIVITYDETAILS.MultiSetWhere = false;
            this.CON_ACTIVITYDETAILS.Name = "CON_ACTIVITYDETAILS";
            this.CON_ACTIVITYDETAILS.NotificationAutoEnlist = false;
            this.CON_ACTIVITYDETAILS.SecExcept = null;
            this.CON_ACTIVITYDETAILS.SecFieldName = null;
            this.CON_ACTIVITYDETAILS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_ACTIVITYDETAILS.SelectPaging = false;
            this.CON_ACTIVITYDETAILS.SelectTop = 0;
            this.CON_ACTIVITYDETAILS.SiteControl = false;
            this.CON_ACTIVITYDETAILS.SiteFieldName = null;
            this.CON_ACTIVITYDETAILS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_ACTIVITYDETAILS
            // 
            this.ucCON_ACTIVITYDETAILS.AutoTrans = true;
            this.ucCON_ACTIVITYDETAILS.ExceptJoin = false;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AUTOKEY";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ACTIVITY_ID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CONTACT_ID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CENTER_ID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CREATE_MAN";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CREATE_DATE";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            this.ucCON_ACTIVITYDETAILS.FieldAttrs.Add(fieldAttr6);
            this.ucCON_ACTIVITYDETAILS.FieldAttrs.Add(fieldAttr7);
            this.ucCON_ACTIVITYDETAILS.FieldAttrs.Add(fieldAttr8);
            this.ucCON_ACTIVITYDETAILS.FieldAttrs.Add(fieldAttr9);
            this.ucCON_ACTIVITYDETAILS.FieldAttrs.Add(fieldAttr10);
            this.ucCON_ACTIVITYDETAILS.FieldAttrs.Add(fieldAttr11);
            this.ucCON_ACTIVITYDETAILS.LogInfo = null;
            this.ucCON_ACTIVITYDETAILS.Name = "ucCON_ACTIVITYDETAILS";
            this.ucCON_ACTIVITYDETAILS.RowAffectsCheck = true;
            this.ucCON_ACTIVITYDETAILS.SelectCmd = this.CON_ACTIVITYDETAILS;
            this.ucCON_ACTIVITYDETAILS.SelectCmdForUpdate = null;
            this.ucCON_ACTIVITYDETAILS.SendSQLCmd = true;
            this.ucCON_ACTIVITYDETAILS.ServerModify = true;
            this.ucCON_ACTIVITYDETAILS.ServerModifyGetMax = false;
            this.ucCON_ACTIVITYDETAILS.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_ACTIVITYDETAILS.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_ACTIVITYDETAILS.UseTranscationScope = false;
            this.ucCON_ACTIVITYDETAILS.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_CON_ACTIVITYMASTER
            // 
            this.View_CON_ACTIVITYMASTER.CacheConnection = false;
            this.View_CON_ACTIVITYMASTER.CommandText = "SELECT * FROM dbo.[CON_ACTIVITYMASTER]";
            this.View_CON_ACTIVITYMASTER.CommandTimeout = 30;
            this.View_CON_ACTIVITYMASTER.CommandType = System.Data.CommandType.Text;
            this.View_CON_ACTIVITYMASTER.DynamicTableName = false;
            this.View_CON_ACTIVITYMASTER.EEPAlias = null;
            this.View_CON_ACTIVITYMASTER.EncodingAfter = null;
            this.View_CON_ACTIVITYMASTER.EncodingBefore = "Windows-1252";
            this.View_CON_ACTIVITYMASTER.EncodingConvert = null;
            this.View_CON_ACTIVITYMASTER.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ACTIVITY_ID";
            this.View_CON_ACTIVITYMASTER.KeyFields.Add(keyItem4);
            this.View_CON_ACTIVITYMASTER.MultiSetWhere = false;
            this.View_CON_ACTIVITYMASTER.Name = "View_CON_ACTIVITYMASTER";
            this.View_CON_ACTIVITYMASTER.NotificationAutoEnlist = false;
            this.View_CON_ACTIVITYMASTER.SecExcept = null;
            this.View_CON_ACTIVITYMASTER.SecFieldName = null;
            this.View_CON_ACTIVITYMASTER.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CON_ACTIVITYMASTER.SelectPaging = false;
            this.View_CON_ACTIVITYMASTER.SelectTop = 0;
            this.View_CON_ACTIVITYMASTER.SiteControl = false;
            this.View_CON_ACTIVITYMASTER.SiteFieldName = null;
            this.View_CON_ACTIVITYMASTER.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_CON_ACTIVITYDETAILS
            // 
            this.View_CON_ACTIVITYDETAILS.CacheConnection = false;
            this.View_CON_ACTIVITYDETAILS.CommandText = "SELECT * FROM dbo.[CON_ACTIVITYDETAILS]";
            this.View_CON_ACTIVITYDETAILS.CommandTimeout = 30;
            this.View_CON_ACTIVITYDETAILS.CommandType = System.Data.CommandType.Text;
            this.View_CON_ACTIVITYDETAILS.DynamicTableName = false;
            this.View_CON_ACTIVITYDETAILS.EEPAlias = null;
            this.View_CON_ACTIVITYDETAILS.EncodingAfter = null;
            this.View_CON_ACTIVITYDETAILS.EncodingBefore = "Windows-1252";
            this.View_CON_ACTIVITYDETAILS.EncodingConvert = null;
            this.View_CON_ACTIVITYDETAILS.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "AUTOKEY";
            this.View_CON_ACTIVITYDETAILS.KeyFields.Add(keyItem5);
            this.View_CON_ACTIVITYDETAILS.MultiSetWhere = false;
            this.View_CON_ACTIVITYDETAILS.Name = "View_CON_ACTIVITYDETAILS";
            this.View_CON_ACTIVITYDETAILS.NotificationAutoEnlist = false;
            this.View_CON_ACTIVITYDETAILS.SecExcept = null;
            this.View_CON_ACTIVITYDETAILS.SecFieldName = null;
            this.View_CON_ACTIVITYDETAILS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CON_ACTIVITYDETAILS.SelectPaging = false;
            this.View_CON_ACTIVITYDETAILS.SelectTop = 0;
            this.View_CON_ACTIVITYDETAILS.SiteControl = false;
            this.View_CON_ACTIVITYDETAILS.SiteFieldName = null;
            this.View_CON_ACTIVITYDETAILS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CON_ACTIVITYDETAILSTEMP
            // 
            this.CON_ACTIVITYDETAILSTEMP.CacheConnection = false;
            this.CON_ACTIVITYDETAILSTEMP.CommandText = resources.GetString("CON_ACTIVITYDETAILSTEMP.CommandText");
            this.CON_ACTIVITYDETAILSTEMP.CommandTimeout = 30;
            this.CON_ACTIVITYDETAILSTEMP.CommandType = System.Data.CommandType.Text;
            this.CON_ACTIVITYDETAILSTEMP.DynamicTableName = false;
            this.CON_ACTIVITYDETAILSTEMP.EEPAlias = null;
            this.CON_ACTIVITYDETAILSTEMP.EncodingAfter = null;
            this.CON_ACTIVITYDETAILSTEMP.EncodingBefore = "Windows-1252";
            this.CON_ACTIVITYDETAILSTEMP.EncodingConvert = null;
            this.CON_ACTIVITYDETAILSTEMP.InfoConnection = this.InfoConnection1;
            this.CON_ACTIVITYDETAILSTEMP.MultiSetWhere = false;
            this.CON_ACTIVITYDETAILSTEMP.Name = "CON_ACTIVITYDETAILSTEMP";
            this.CON_ACTIVITYDETAILSTEMP.NotificationAutoEnlist = false;
            this.CON_ACTIVITYDETAILSTEMP.SecExcept = null;
            this.CON_ACTIVITYDETAILSTEMP.SecFieldName = null;
            this.CON_ACTIVITYDETAILSTEMP.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_ACTIVITYDETAILSTEMP.SelectPaging = false;
            this.CON_ACTIVITYDETAILSTEMP.SelectTop = 0;
            this.CON_ACTIVITYDETAILSTEMP.SiteControl = false;
            this.CON_ACTIVITYDETAILSTEMP.SiteFieldName = null;
            this.CON_ACTIVITYDETAILSTEMP.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CENTER
            // 
            this.CENTER.CacheConnection = false;
            this.CENTER.CommandText = "SELECT  DISTINCT CENTER_ID_FROM,\'\' AS CENTER_NAME\r\nFROM CON_ACTIVITYDETAILS";
            this.CENTER.CommandTimeout = 30;
            this.CENTER.CommandType = System.Data.CommandType.Text;
            this.CENTER.DynamicTableName = false;
            this.CENTER.EEPAlias = null;
            this.CENTER.EncodingAfter = null;
            this.CENTER.EncodingBefore = "Windows-1252";
            this.CENTER.EncodingConvert = null;
            this.CENTER.InfoConnection = this.InfoConnection1;
            this.CENTER.MultiSetWhere = false;
            this.CENTER.Name = "CENTER";
            this.CENTER.NotificationAutoEnlist = false;
            this.CENTER.SecExcept = null;
            this.CENTER.SecFieldName = null;
            this.CENTER.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CENTER.SelectPaging = false;
            this.CENTER.SelectTop = 0;
            this.CENTER.SiteControl = false;
            this.CENTER.SiteFieldName = null;
            this.CENTER.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_ACTIVITYMASTER)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_ACTIVITYDETAILS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_ACTIVITYMASTER)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_ACTIVITYDETAILS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_ACTIVITYDETAILSTEMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CENTER)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CON_ACTIVITYMASTER;
        private Srvtools.UpdateComponent ucCON_ACTIVITYMASTER;
        private Srvtools.InfoCommand CON_ACTIVITYDETAILS;
        private Srvtools.UpdateComponent ucCON_ACTIVITYDETAILS;
        private Srvtools.InfoCommand View_CON_ACTIVITYMASTER;
        private Srvtools.InfoCommand View_CON_ACTIVITYDETAILS;
        private Srvtools.InfoCommand CON_ACTIVITYDETAILSTEMP;
        private Srvtools.InfoCommand CENTER;
    }
}
