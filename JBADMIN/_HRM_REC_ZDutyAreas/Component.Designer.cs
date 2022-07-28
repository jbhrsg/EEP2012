namespace _HRM_REC_ZDutyAreas
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.REC_ZDutyAreasClass = new Srvtools.InfoCommand(this.components);
            this.ucREC_ZDutyAreasClass = new Srvtools.UpdateComponent(this.components);
            this.REC_ZDutyAreas = new Srvtools.InfoCommand(this.components);
            this.ucREC_ZDutyAreas = new Srvtools.UpdateComponent(this.components);
            this.idREC_ZDutyAreasClass_REC_ZDutyAreas = new Srvtools.InfoDataSource(this.components);
            this.View_REC_ZDutyAreasClass = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REC_ZDutyAreasClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REC_ZDutyAreas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_REC_ZDutyAreasClass)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckMasterDelete";
            service1.NonLogin = false;
            service1.ServiceName = "CheckMasterDelete";
            service2.DelegateName = "CheckDetailDelete";
            service2.NonLogin = false;
            service2.ServiceName = "CheckDetailDelete";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBHRIS_DISPATCH";
            // 
            // REC_ZDutyAreasClass
            // 
            this.REC_ZDutyAreasClass.CacheConnection = false;
            this.REC_ZDutyAreasClass.CommandText = "SELECT dbo.[REC_ZDutyAreasClass].* FROM dbo.[REC_ZDutyAreasClass]";
            this.REC_ZDutyAreasClass.CommandTimeout = 30;
            this.REC_ZDutyAreasClass.CommandType = System.Data.CommandType.Text;
            this.REC_ZDutyAreasClass.DynamicTableName = false;
            this.REC_ZDutyAreasClass.EEPAlias = "JBHRIS_DISPATCH";
            this.REC_ZDutyAreasClass.EncodingAfter = null;
            this.REC_ZDutyAreasClass.EncodingBefore = "Windows-1252";
            this.REC_ZDutyAreasClass.EncodingConvert = null;
            this.REC_ZDutyAreasClass.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.REC_ZDutyAreasClass.KeyFields.Add(keyItem1);
            this.REC_ZDutyAreasClass.MultiSetWhere = false;
            this.REC_ZDutyAreasClass.Name = "REC_ZDutyAreasClass";
            this.REC_ZDutyAreasClass.NotificationAutoEnlist = false;
            this.REC_ZDutyAreasClass.SecExcept = null;
            this.REC_ZDutyAreasClass.SecFieldName = null;
            this.REC_ZDutyAreasClass.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.REC_ZDutyAreasClass.SelectPaging = false;
            this.REC_ZDutyAreasClass.SelectTop = 0;
            this.REC_ZDutyAreasClass.SiteControl = false;
            this.REC_ZDutyAreasClass.SiteFieldName = null;
            this.REC_ZDutyAreasClass.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucREC_ZDutyAreasClass
            // 
            this.ucREC_ZDutyAreasClass.AutoTrans = true;
            this.ucREC_ZDutyAreasClass.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "Contents";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SortID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucREC_ZDutyAreasClass.FieldAttrs.Add(fieldAttr1);
            this.ucREC_ZDutyAreasClass.FieldAttrs.Add(fieldAttr2);
            this.ucREC_ZDutyAreasClass.FieldAttrs.Add(fieldAttr3);
            this.ucREC_ZDutyAreasClass.FieldAttrs.Add(fieldAttr4);
            this.ucREC_ZDutyAreasClass.LogInfo = null;
            this.ucREC_ZDutyAreasClass.Name = "ucREC_ZDutyAreasClass";
            this.ucREC_ZDutyAreasClass.RowAffectsCheck = true;
            this.ucREC_ZDutyAreasClass.SelectCmd = this.REC_ZDutyAreasClass;
            this.ucREC_ZDutyAreasClass.SelectCmdForUpdate = null;
            this.ucREC_ZDutyAreasClass.SendSQLCmd = true;
            this.ucREC_ZDutyAreasClass.ServerModify = true;
            this.ucREC_ZDutyAreasClass.ServerModifyGetMax = false;
            this.ucREC_ZDutyAreasClass.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucREC_ZDutyAreasClass.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucREC_ZDutyAreasClass.UseTranscationScope = false;
            this.ucREC_ZDutyAreasClass.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucREC_ZDutyAreasClass.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucREC_ZDutyAreasClass_BeforeInsert);
            this.ucREC_ZDutyAreasClass.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucREC_ZDutyAreasClass_BeforeModify);
            // 
            // REC_ZDutyAreas
            // 
            this.REC_ZDutyAreas.CacheConnection = false;
            this.REC_ZDutyAreas.CommandText = "SELECT dbo.[REC_ZDutyAreas].* FROM dbo.[REC_ZDutyAreas]";
            this.REC_ZDutyAreas.CommandTimeout = 30;
            this.REC_ZDutyAreas.CommandType = System.Data.CommandType.Text;
            this.REC_ZDutyAreas.DynamicTableName = false;
            this.REC_ZDutyAreas.EEPAlias = "JBHRIS_DISPATCH";
            this.REC_ZDutyAreas.EncodingAfter = null;
            this.REC_ZDutyAreas.EncodingBefore = "Windows-1252";
            this.REC_ZDutyAreas.EncodingConvert = null;
            this.REC_ZDutyAreas.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ID";
            this.REC_ZDutyAreas.KeyFields.Add(keyItem2);
            this.REC_ZDutyAreas.MultiSetWhere = false;
            this.REC_ZDutyAreas.Name = "REC_ZDutyAreas";
            this.REC_ZDutyAreas.NotificationAutoEnlist = false;
            this.REC_ZDutyAreas.SecExcept = null;
            this.REC_ZDutyAreas.SecFieldName = null;
            this.REC_ZDutyAreas.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.REC_ZDutyAreas.SelectPaging = false;
            this.REC_ZDutyAreas.SelectTop = 0;
            this.REC_ZDutyAreas.SiteControl = false;
            this.REC_ZDutyAreas.SiteFieldName = null;
            this.REC_ZDutyAreas.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucREC_ZDutyAreas
            // 
            this.ucREC_ZDutyAreas.AutoTrans = true;
            this.ucREC_ZDutyAreas.ExceptJoin = false;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "AutoKey";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ClassID";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Contents";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "SortID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucREC_ZDutyAreas.FieldAttrs.Add(fieldAttr5);
            this.ucREC_ZDutyAreas.FieldAttrs.Add(fieldAttr6);
            this.ucREC_ZDutyAreas.FieldAttrs.Add(fieldAttr7);
            this.ucREC_ZDutyAreas.FieldAttrs.Add(fieldAttr8);
            this.ucREC_ZDutyAreas.FieldAttrs.Add(fieldAttr9);
            this.ucREC_ZDutyAreas.LogInfo = null;
            this.ucREC_ZDutyAreas.Name = "ucREC_ZDutyAreas";
            this.ucREC_ZDutyAreas.RowAffectsCheck = true;
            this.ucREC_ZDutyAreas.SelectCmd = this.REC_ZDutyAreas;
            this.ucREC_ZDutyAreas.SelectCmdForUpdate = null;
            this.ucREC_ZDutyAreas.SendSQLCmd = true;
            this.ucREC_ZDutyAreas.ServerModify = true;
            this.ucREC_ZDutyAreas.ServerModifyGetMax = false;
            this.ucREC_ZDutyAreas.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucREC_ZDutyAreas.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucREC_ZDutyAreas.UseTranscationScope = false;
            this.ucREC_ZDutyAreas.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idREC_ZDutyAreasClass_REC_ZDutyAreas
            // 
            this.idREC_ZDutyAreasClass_REC_ZDutyAreas.Detail = this.REC_ZDutyAreas;
            columnItem1.FieldName = "ClassID";
            this.idREC_ZDutyAreasClass_REC_ZDutyAreas.DetailColumns.Add(columnItem1);
            this.idREC_ZDutyAreasClass_REC_ZDutyAreas.DynamicTableName = false;
            this.idREC_ZDutyAreasClass_REC_ZDutyAreas.Master = this.REC_ZDutyAreasClass;
            columnItem2.FieldName = "ID";
            this.idREC_ZDutyAreasClass_REC_ZDutyAreas.MasterColumns.Add(columnItem2);
            // 
            // View_REC_ZDutyAreasClass
            // 
            this.View_REC_ZDutyAreasClass.CacheConnection = false;
            this.View_REC_ZDutyAreasClass.CommandText = "SELECT * FROM dbo.[REC_ZDutyAreasClass]";
            this.View_REC_ZDutyAreasClass.CommandTimeout = 30;
            this.View_REC_ZDutyAreasClass.CommandType = System.Data.CommandType.Text;
            this.View_REC_ZDutyAreasClass.DynamicTableName = false;
            this.View_REC_ZDutyAreasClass.EEPAlias = null;
            this.View_REC_ZDutyAreasClass.EncodingAfter = null;
            this.View_REC_ZDutyAreasClass.EncodingBefore = "Windows-1252";
            this.View_REC_ZDutyAreasClass.EncodingConvert = null;
            this.View_REC_ZDutyAreasClass.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.View_REC_ZDutyAreasClass.KeyFields.Add(keyItem3);
            this.View_REC_ZDutyAreasClass.MultiSetWhere = false;
            this.View_REC_ZDutyAreasClass.Name = "View_REC_ZDutyAreasClass";
            this.View_REC_ZDutyAreasClass.NotificationAutoEnlist = false;
            this.View_REC_ZDutyAreasClass.SecExcept = null;
            this.View_REC_ZDutyAreasClass.SecFieldName = null;
            this.View_REC_ZDutyAreasClass.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_REC_ZDutyAreasClass.SelectPaging = false;
            this.View_REC_ZDutyAreasClass.SelectTop = 0;
            this.View_REC_ZDutyAreasClass.SiteControl = false;
            this.View_REC_ZDutyAreasClass.SiteFieldName = null;
            this.View_REC_ZDutyAreasClass.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REC_ZDutyAreasClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REC_ZDutyAreas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_REC_ZDutyAreasClass)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand REC_ZDutyAreasClass;
        private Srvtools.UpdateComponent ucREC_ZDutyAreasClass;
        private Srvtools.InfoCommand REC_ZDutyAreas;
        private Srvtools.UpdateComponent ucREC_ZDutyAreas;
        private Srvtools.InfoDataSource idREC_ZDutyAreasClass_REC_ZDutyAreas;
        private Srvtools.InfoCommand View_REC_ZDutyAreasClass;
    }
}
