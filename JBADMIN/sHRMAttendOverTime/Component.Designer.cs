namespace sHRMAttendOverTime
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HRMAttendOverTimeApplyMaster = new Srvtools.InfoCommand(this.components);
            this.ucHRMAttendOverTimeApplyMaster = new Srvtools.UpdateComponent(this.components);
            this.HRMAttendOverTimeApplyDetails = new Srvtools.InfoCommand(this.components);
            this.ucHRMAttendOverTimeApplyDetails = new Srvtools.UpdateComponent(this.components);
            this.idHRMAttendOverTimeApplyMaster_HRMAttendOverTimeApplyDetails = new Srvtools.InfoDataSource(this.components);
            this.autoOverTimeNO = new Srvtools.AutoNumber(this.components);
            this.infoHRM_BASE_BASE = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.infoOverTimeCauseID = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendOverTimeApplyMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendOverTimeApplyDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_BASE_BASE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoOverTimeCauseID)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "getDeptInfo";
            service1.NonLogin = false;
            service1.ServiceName = "getDeptInfo";
            service2.DelegateName = "checkOvertimeHours";
            service2.NonLogin = false;
            service2.ServiceName = "checkOvertimeHours";
            service3.DelegateName = "checkOvertimeData";
            service3.NonLogin = false;
            service3.ServiceName = "checkOvertimeData";
            service4.DelegateName = "procInsertHRMAttendOverTimeApplyMaster";
            service4.NonLogin = false;
            service4.ServiceName = "procInsertHRMAttendOverTimeApplyMaster";
            service5.DelegateName = "getOFF_TIME";
            service5.NonLogin = false;
            service5.ServiceName = "getOFF_TIME";
            service6.DelegateName = "checkOnData";
            service6.NonLogin = false;
            service6.ServiceName = "checkOnData";
            service7.DelegateName = "getEmployeeID";
            service7.NonLogin = false;
            service7.ServiceName = "getEmployeeID";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // HRMAttendOverTimeApplyMaster
            // 
            this.HRMAttendOverTimeApplyMaster.CacheConnection = false;
            this.HRMAttendOverTimeApplyMaster.CommandText = "SELECT dbo.[HRMAttendOverTimeApplyMaster].*,\'\' as DEPT_Name,\'\' as ROTE_Name FROM " +
    "dbo.[HRMAttendOverTimeApplyMaster]";
            this.HRMAttendOverTimeApplyMaster.CommandTimeout = 30;
            this.HRMAttendOverTimeApplyMaster.CommandType = System.Data.CommandType.Text;
            this.HRMAttendOverTimeApplyMaster.DynamicTableName = false;
            this.HRMAttendOverTimeApplyMaster.EEPAlias = null;
            this.HRMAttendOverTimeApplyMaster.EncodingAfter = null;
            this.HRMAttendOverTimeApplyMaster.EncodingBefore = "Windows-1252";
            this.HRMAttendOverTimeApplyMaster.EncodingConvert = null;
            this.HRMAttendOverTimeApplyMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "OverTimeNO";
            this.HRMAttendOverTimeApplyMaster.KeyFields.Add(keyItem1);
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
            fieldAttr6.DataField = "flowflag";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateBy";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr1);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr2);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr3);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr4);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr5);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr6);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr7);
            this.ucHRMAttendOverTimeApplyMaster.FieldAttrs.Add(fieldAttr8);
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
            this.ucHRMAttendOverTimeApplyMaster.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHRMAttendOverTimeApplyMaster_BeforeInsert);
            // 
            // HRMAttendOverTimeApplyDetails
            // 
            this.HRMAttendOverTimeApplyDetails.CacheConnection = false;
            this.HRMAttendOverTimeApplyDetails.CommandText = "SELECT dbo.[HRMAttendOverTimeApplyDetails].* FROM dbo.[HRMAttendOverTimeApplyDeta" +
    "ils]";
            this.HRMAttendOverTimeApplyDetails.CommandTimeout = 30;
            this.HRMAttendOverTimeApplyDetails.CommandType = System.Data.CommandType.Text;
            this.HRMAttendOverTimeApplyDetails.DynamicTableName = false;
            this.HRMAttendOverTimeApplyDetails.EEPAlias = null;
            this.HRMAttendOverTimeApplyDetails.EncodingAfter = null;
            this.HRMAttendOverTimeApplyDetails.EncodingBefore = "Windows-1252";
            this.HRMAttendOverTimeApplyDetails.EncodingConvert = null;
            this.HRMAttendOverTimeApplyDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ItemSeq";
            keyItem3.KeyName = "OverTimeNO";
            this.HRMAttendOverTimeApplyDetails.KeyFields.Add(keyItem2);
            this.HRMAttendOverTimeApplyDetails.KeyFields.Add(keyItem3);
            this.HRMAttendOverTimeApplyDetails.MultiSetWhere = false;
            this.HRMAttendOverTimeApplyDetails.Name = "HRMAttendOverTimeApplyDetails";
            this.HRMAttendOverTimeApplyDetails.NotificationAutoEnlist = false;
            this.HRMAttendOverTimeApplyDetails.SecExcept = null;
            this.HRMAttendOverTimeApplyDetails.SecFieldName = null;
            this.HRMAttendOverTimeApplyDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRMAttendOverTimeApplyDetails.SelectPaging = false;
            this.HRMAttendOverTimeApplyDetails.SelectTop = 0;
            this.HRMAttendOverTimeApplyDetails.SiteControl = false;
            this.HRMAttendOverTimeApplyDetails.SiteFieldName = null;
            this.HRMAttendOverTimeApplyDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHRMAttendOverTimeApplyDetails
            // 
            this.ucHRMAttendOverTimeApplyDetails.AutoTrans = true;
            this.ucHRMAttendOverTimeApplyDetails.ExceptJoin = false;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ItemSeq";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "OverTimeNO";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "OverTimeDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "BeginTime";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "OverTimeDateTimeBegin";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "EndTime";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "OverTimeDateTimeEnd";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "OverTimeHours";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "RestHours";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "TotalHours";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "OverTimeCauseID";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CreateBy";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CreateDate";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr9);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr10);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr11);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr12);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr13);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr14);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr15);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr16);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr17);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr18);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr19);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr20);
            this.ucHRMAttendOverTimeApplyDetails.FieldAttrs.Add(fieldAttr21);
            this.ucHRMAttendOverTimeApplyDetails.LogInfo = null;
            this.ucHRMAttendOverTimeApplyDetails.Name = "ucHRMAttendOverTimeApplyDetails";
            this.ucHRMAttendOverTimeApplyDetails.RowAffectsCheck = true;
            this.ucHRMAttendOverTimeApplyDetails.SelectCmd = this.HRMAttendOverTimeApplyDetails;
            this.ucHRMAttendOverTimeApplyDetails.SelectCmdForUpdate = null;
            this.ucHRMAttendOverTimeApplyDetails.SendSQLCmd = true;
            this.ucHRMAttendOverTimeApplyDetails.ServerModify = true;
            this.ucHRMAttendOverTimeApplyDetails.ServerModifyGetMax = false;
            this.ucHRMAttendOverTimeApplyDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHRMAttendOverTimeApplyDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHRMAttendOverTimeApplyDetails.UseTranscationScope = false;
            this.ucHRMAttendOverTimeApplyDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHRMAttendOverTimeApplyDetails.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHRMAttendOverTimeApplyDetails_BeforeInsert);
            // 
            // idHRMAttendOverTimeApplyMaster_HRMAttendOverTimeApplyDetails
            // 
            this.idHRMAttendOverTimeApplyMaster_HRMAttendOverTimeApplyDetails.Detail = this.HRMAttendOverTimeApplyDetails;
            columnItem1.FieldName = "OverTimeNO";
            this.idHRMAttendOverTimeApplyMaster_HRMAttendOverTimeApplyDetails.DetailColumns.Add(columnItem1);
            this.idHRMAttendOverTimeApplyMaster_HRMAttendOverTimeApplyDetails.DynamicTableName = false;
            this.idHRMAttendOverTimeApplyMaster_HRMAttendOverTimeApplyDetails.Master = this.HRMAttendOverTimeApplyMaster;
            columnItem2.FieldName = "OverTimeNO";
            this.idHRMAttendOverTimeApplyMaster_HRMAttendOverTimeApplyDetails.MasterColumns.Add(columnItem2);
            // 
            // autoOverTimeNO
            // 
            this.autoOverTimeNO.Active = true;
            this.autoOverTimeNO.AutoNoID = "OverTimeNO";
            this.autoOverTimeNO.Description = null;
            this.autoOverTimeNO.GetFixed = "OverTimeNOFixed()";
            this.autoOverTimeNO.isNumFill = false;
            this.autoOverTimeNO.Name = "autoOverTimeNO";
            this.autoOverTimeNO.Number = null;
            this.autoOverTimeNO.NumDig = 5;
            this.autoOverTimeNO.OldVersion = false;
            this.autoOverTimeNO.OverFlow = true;
            this.autoOverTimeNO.StartValue = 1;
            this.autoOverTimeNO.Step = 1;
            this.autoOverTimeNO.TargetColumn = "OverTimeNO";
            this.autoOverTimeNO.UpdateComp = this.ucHRMAttendOverTimeApplyMaster;
            // 
            // infoHRM_BASE_BASE
            // 
            this.infoHRM_BASE_BASE.CacheConnection = false;
            this.infoHRM_BASE_BASE.CommandText = "SELECT EMPLOYEE_ID,EMPLOYEE_CODE,NAME_C       \r\nFROM  HRM_BASE_BASE";
            this.infoHRM_BASE_BASE.CommandTimeout = 30;
            this.infoHRM_BASE_BASE.CommandType = System.Data.CommandType.Text;
            this.infoHRM_BASE_BASE.DynamicTableName = false;
            this.infoHRM_BASE_BASE.EEPAlias = "JBHR_EEP";
            this.infoHRM_BASE_BASE.EncodingAfter = null;
            this.infoHRM_BASE_BASE.EncodingBefore = "Windows-1252";
            this.infoHRM_BASE_BASE.EncodingConvert = null;
            this.infoHRM_BASE_BASE.InfoConnection = this.infoConnection2;
            keyItem4.KeyName = "EMPLOYEE_ID";
            this.infoHRM_BASE_BASE.KeyFields.Add(keyItem4);
            this.infoHRM_BASE_BASE.MultiSetWhere = false;
            this.infoHRM_BASE_BASE.Name = "infoHRM_BASE_BASE";
            this.infoHRM_BASE_BASE.NotificationAutoEnlist = false;
            this.infoHRM_BASE_BASE.SecExcept = null;
            this.infoHRM_BASE_BASE.SecFieldName = null;
            this.infoHRM_BASE_BASE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoHRM_BASE_BASE.SelectPaging = false;
            this.infoHRM_BASE_BASE.SelectTop = 0;
            this.infoHRM_BASE_BASE.SiteControl = false;
            this.infoHRM_BASE_BASE.SiteFieldName = null;
            this.infoHRM_BASE_BASE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBHR_EEP";
            // 
            // infoOverTimeCauseID
            // 
            this.infoOverTimeCauseID.CacheConnection = false;
            this.infoOverTimeCauseID.CommandText = "SELECT distinct OverTimeCauseID\r\nFROM   HRMAttendOverTimeApplyDetails\r\nwhere Over" +
    "TimeCauseID is not null";
            this.infoOverTimeCauseID.CommandTimeout = 30;
            this.infoOverTimeCauseID.CommandType = System.Data.CommandType.Text;
            this.infoOverTimeCauseID.DynamicTableName = false;
            this.infoOverTimeCauseID.EEPAlias = "";
            this.infoOverTimeCauseID.EncodingAfter = null;
            this.infoOverTimeCauseID.EncodingBefore = "Windows-1252";
            this.infoOverTimeCauseID.EncodingConvert = null;
            this.infoOverTimeCauseID.InfoConnection = this.InfoConnection1;
            this.infoOverTimeCauseID.MultiSetWhere = false;
            this.infoOverTimeCauseID.Name = "infoOverTimeCauseID";
            this.infoOverTimeCauseID.NotificationAutoEnlist = false;
            this.infoOverTimeCauseID.SecExcept = null;
            this.infoOverTimeCauseID.SecFieldName = null;
            this.infoOverTimeCauseID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoOverTimeCauseID.SelectPaging = false;
            this.infoOverTimeCauseID.SelectTop = 0;
            this.infoOverTimeCauseID.SiteControl = false;
            this.infoOverTimeCauseID.SiteFieldName = null;
            this.infoOverTimeCauseID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendOverTimeApplyMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendOverTimeApplyDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_BASE_BASE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoOverTimeCauseID)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HRMAttendOverTimeApplyMaster;
        private Srvtools.UpdateComponent ucHRMAttendOverTimeApplyMaster;
        private Srvtools.InfoCommand HRMAttendOverTimeApplyDetails;
        private Srvtools.UpdateComponent ucHRMAttendOverTimeApplyDetails;
        private Srvtools.InfoDataSource idHRMAttendOverTimeApplyMaster_HRMAttendOverTimeApplyDetails;
        private Srvtools.AutoNumber autoOverTimeNO;
        private Srvtools.InfoCommand infoHRM_BASE_BASE;
        private Srvtools.InfoCommand infoOverTimeCauseID;
        private Srvtools.InfoConnection infoConnection2;
    }
}
