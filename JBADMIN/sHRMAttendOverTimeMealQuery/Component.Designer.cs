namespace sHRMAttendOverTimeMealQuery
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HRMAttendOverTimeApplyMaster = new Srvtools.InfoCommand(this.components);
            this.ucHRMAttendOverTimeApplyMaster = new Srvtools.UpdateComponent(this.components);
            this.View_HRMAttendOverTimeApplyMaster = new Srvtools.InfoCommand(this.components);
            this.EmployeeText = new Srvtools.InfoCommand(this.components);
            this.EmployeeID = new Srvtools.InfoCommand(this.components);
            this.UpdateDateYM = new Srvtools.InfoCommand(this.components);
            this.HRMAttendOverTimeMealApplyType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendOverTimeApplyMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HRMAttendOverTimeApplyMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDateYM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendOverTimeMealApplyType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "ReportDelayLunch";
            service1.NonLogin = false;
            service1.ServiceName = "ReportDelayLunch";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // HRMAttendOverTimeApplyMaster
            // 
            this.HRMAttendOverTimeApplyMaster.CacheConnection = false;
            this.HRMAttendOverTimeApplyMaster.CommandText = resources.GetString("HRMAttendOverTimeApplyMaster.CommandText");
            this.HRMAttendOverTimeApplyMaster.CommandTimeout = 30;
            this.HRMAttendOverTimeApplyMaster.CommandType = System.Data.CommandType.Text;
            this.HRMAttendOverTimeApplyMaster.DynamicTableName = false;
            this.HRMAttendOverTimeApplyMaster.EEPAlias = null;
            this.HRMAttendOverTimeApplyMaster.EncodingAfter = null;
            this.HRMAttendOverTimeApplyMaster.EncodingBefore = "Windows-1252";
            this.HRMAttendOverTimeApplyMaster.EncodingConvert = null;
            this.HRMAttendOverTimeApplyMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "OverTimeNO";
            keyItem2.KeyName = "ItemSeq";
            this.HRMAttendOverTimeApplyMaster.KeyFields.Add(keyItem1);
            this.HRMAttendOverTimeApplyMaster.KeyFields.Add(keyItem2);
            this.HRMAttendOverTimeApplyMaster.MultiSetWhere = false;
            this.HRMAttendOverTimeApplyMaster.Name = "HRMAttendOverTimeApplyMaster";
            this.HRMAttendOverTimeApplyMaster.NotificationAutoEnlist = false;
            this.HRMAttendOverTimeApplyMaster.SecExcept = null;
            this.HRMAttendOverTimeApplyMaster.SecFieldName = null;
            this.HRMAttendOverTimeApplyMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRMAttendOverTimeApplyMaster.SelectPaging = false;
            this.HRMAttendOverTimeApplyMaster.SelectTop = 0;
            this.HRMAttendOverTimeApplyMaster.SiteControl = false;
            this.HRMAttendOverTimeApplyMaster.SiteFieldName = null;
            this.HRMAttendOverTimeApplyMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHRMAttendOverTimeApplyMaster
            // 
            this.ucHRMAttendOverTimeApplyMaster.AutoTrans = true;
            this.ucHRMAttendOverTimeApplyMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "OverTimeNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "EmployeeID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "OverTimeDeptID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "OverTimeRoteID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "MasterTotalHours";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Memo";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "flowflag";
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
            fieldAttr10.DataField = "EmployeeText";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr1);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr2);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr3);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr4);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr5);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr6);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr7);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr8);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr9);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr10);
            this.ucHRMAttendOverTimeApplyMaster.LogInfo = null;
            this.ucHRMAttendOverTimeApplyMaster.Name = "ucHRMAttendOverTimeApplyMaster";
            this.ucHRMAttendOverTimeApplyMaster.RowAffectsCheck = true;
            this.ucHRMAttendOverTimeApplyMaster.SelectCmd = this.HRMAttendOverTimeApplyMaster;
            this.ucHRMAttendOverTimeApplyMaster.SelectCmdForUpdate = null;
            this.ucHRMAttendOverTimeApplyMaster.SendSQLCmd = true;
            this.ucHRMAttendOverTimeApplyMaster.ServerModify = true;
            this.ucHRMAttendOverTimeApplyMaster.ServerModifyGetMax = false;
            this.ucHRMAttendOverTimeApplyMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHRMAttendOverTimeApplyMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHRMAttendOverTimeApplyMaster.UseTranscationScope = false;
            this.ucHRMAttendOverTimeApplyMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HRMAttendOverTimeApplyMaster
            // 
            this.View_HRMAttendOverTimeApplyMaster.CacheConnection = false;
            this.View_HRMAttendOverTimeApplyMaster.CommandText = "select * from View_HRMAttendOverTimeMasterDetail";
            this.View_HRMAttendOverTimeApplyMaster.CommandTimeout = 30;
            this.View_HRMAttendOverTimeApplyMaster.CommandType = System.Data.CommandType.Text;
            this.View_HRMAttendOverTimeApplyMaster.DynamicTableName = false;
            this.View_HRMAttendOverTimeApplyMaster.EEPAlias = null;
            this.View_HRMAttendOverTimeApplyMaster.EncodingAfter = null;
            this.View_HRMAttendOverTimeApplyMaster.EncodingBefore = "Windows-1252";
            this.View_HRMAttendOverTimeApplyMaster.EncodingConvert = null;
            this.View_HRMAttendOverTimeApplyMaster.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "OverTimeNO";
            keyItem4.KeyName = "ItemSeq";
            this.View_HRMAttendOverTimeApplyMaster.KeyFields.Add(keyItem3);
            this.View_HRMAttendOverTimeApplyMaster.KeyFields.Add(keyItem4);
            this.View_HRMAttendOverTimeApplyMaster.MultiSetWhere = false;
            this.View_HRMAttendOverTimeApplyMaster.Name = "View_HRMAttendOverTimeApplyMaster";
            this.View_HRMAttendOverTimeApplyMaster.NotificationAutoEnlist = false;
            this.View_HRMAttendOverTimeApplyMaster.SecExcept = null;
            this.View_HRMAttendOverTimeApplyMaster.SecFieldName = null;
            this.View_HRMAttendOverTimeApplyMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HRMAttendOverTimeApplyMaster.SelectPaging = false;
            this.View_HRMAttendOverTimeApplyMaster.SelectTop = 0;
            this.View_HRMAttendOverTimeApplyMaster.SiteControl = false;
            this.View_HRMAttendOverTimeApplyMaster.SiteFieldName = null;
            this.View_HRMAttendOverTimeApplyMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // EmployeeText
            // 
            this.EmployeeText.CacheConnection = false;
            this.EmployeeText.CommandText = "select distinct EmployeeText,EmployeeID  from HRMAttendOverTimeApplyMaster\r\n wher" +
    "e  Rtrim(Ltrim(EmployeeText)) not in(\'001\',\'003\',\'005\')\r\n order by EmployeeID as" +
    "c";
            this.EmployeeText.CommandTimeout = 30;
            this.EmployeeText.CommandType = System.Data.CommandType.Text;
            this.EmployeeText.DynamicTableName = false;
            this.EmployeeText.EEPAlias = null;
            this.EmployeeText.EncodingAfter = null;
            this.EmployeeText.EncodingBefore = "Windows-1252";
            this.EmployeeText.EncodingConvert = null;
            this.EmployeeText.InfoConnection = this.InfoConnection1;
            this.EmployeeText.MultiSetWhere = false;
            this.EmployeeText.Name = "EmployeeText";
            this.EmployeeText.NotificationAutoEnlist = false;
            this.EmployeeText.SecExcept = null;
            this.EmployeeText.SecFieldName = null;
            this.EmployeeText.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EmployeeText.SelectPaging = false;
            this.EmployeeText.SelectTop = 0;
            this.EmployeeText.SiteControl = false;
            this.EmployeeText.SiteFieldName = null;
            this.EmployeeText.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // EmployeeID
            // 
            this.EmployeeID.CacheConnection = false;
            this.EmployeeID.CommandText = "select distinct EmployeeID from HRMAttendOverTimeApplyMaster order by EmployeeID " +
    "asc";
            this.EmployeeID.CommandTimeout = 30;
            this.EmployeeID.CommandType = System.Data.CommandType.Text;
            this.EmployeeID.DynamicTableName = false;
            this.EmployeeID.EEPAlias = null;
            this.EmployeeID.EncodingAfter = null;
            this.EmployeeID.EncodingBefore = "Windows-1252";
            this.EmployeeID.EncodingConvert = null;
            this.EmployeeID.InfoConnection = this.InfoConnection1;
            this.EmployeeID.MultiSetWhere = false;
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.NotificationAutoEnlist = false;
            this.EmployeeID.SecExcept = null;
            this.EmployeeID.SecFieldName = null;
            this.EmployeeID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EmployeeID.SelectPaging = false;
            this.EmployeeID.SelectTop = 0;
            this.EmployeeID.SiteControl = false;
            this.EmployeeID.SiteFieldName = null;
            this.EmployeeID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // UpdateDateYM
            // 
            this.UpdateDateYM.CacheConnection = false;
            this.UpdateDateYM.CommandText = resources.GetString("UpdateDateYM.CommandText");
            this.UpdateDateYM.CommandTimeout = 30;
            this.UpdateDateYM.CommandType = System.Data.CommandType.Text;
            this.UpdateDateYM.DynamicTableName = false;
            this.UpdateDateYM.EEPAlias = null;
            this.UpdateDateYM.EncodingAfter = null;
            this.UpdateDateYM.EncodingBefore = "Windows-1252";
            this.UpdateDateYM.EncodingConvert = null;
            this.UpdateDateYM.InfoConnection = this.InfoConnection1;
            this.UpdateDateYM.MultiSetWhere = false;
            this.UpdateDateYM.Name = "UpdateDateYM";
            this.UpdateDateYM.NotificationAutoEnlist = false;
            this.UpdateDateYM.SecExcept = null;
            this.UpdateDateYM.SecFieldName = null;
            this.UpdateDateYM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UpdateDateYM.SelectPaging = false;
            this.UpdateDateYM.SelectTop = 0;
            this.UpdateDateYM.SiteControl = false;
            this.UpdateDateYM.SiteFieldName = null;
            this.UpdateDateYM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HRMAttendOverTimeMealApplyType
            // 
            this.HRMAttendOverTimeMealApplyType.CacheConnection = false;
            this.HRMAttendOverTimeMealApplyType.CommandText = "select * from HRMAttendOverTimeMealApplyType";
            this.HRMAttendOverTimeMealApplyType.CommandTimeout = 30;
            this.HRMAttendOverTimeMealApplyType.CommandType = System.Data.CommandType.Text;
            this.HRMAttendOverTimeMealApplyType.DynamicTableName = false;
            this.HRMAttendOverTimeMealApplyType.EEPAlias = null;
            this.HRMAttendOverTimeMealApplyType.EncodingAfter = null;
            this.HRMAttendOverTimeMealApplyType.EncodingBefore = "Windows-1252";
            this.HRMAttendOverTimeMealApplyType.EncodingConvert = null;
            this.HRMAttendOverTimeMealApplyType.InfoConnection = this.InfoConnection1;
            this.HRMAttendOverTimeMealApplyType.MultiSetWhere = false;
            this.HRMAttendOverTimeMealApplyType.Name = "HRMAttendOverTimeMealApplyType";
            this.HRMAttendOverTimeMealApplyType.NotificationAutoEnlist = false;
            this.HRMAttendOverTimeMealApplyType.SecExcept = null;
            this.HRMAttendOverTimeMealApplyType.SecFieldName = null;
            this.HRMAttendOverTimeMealApplyType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRMAttendOverTimeMealApplyType.SelectPaging = false;
            this.HRMAttendOverTimeMealApplyType.SelectTop = 0;
            this.HRMAttendOverTimeMealApplyType.SiteControl = false;
            this.HRMAttendOverTimeMealApplyType.SiteFieldName = null;
            this.HRMAttendOverTimeMealApplyType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendOverTimeApplyMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HRMAttendOverTimeApplyMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateDateYM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendOverTimeMealApplyType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HRMAttendOverTimeApplyMaster;
        private Srvtools.UpdateComponent ucHRMAttendOverTimeApplyMaster;
        private Srvtools.InfoCommand View_HRMAttendOverTimeApplyMaster;
        private Srvtools.InfoCommand EmployeeText;
        private Srvtools.InfoCommand EmployeeID;
        private Srvtools.InfoCommand UpdateDateYM;
        private Srvtools.InfoCommand HRMAttendOverTimeMealApplyType;
    }
}
