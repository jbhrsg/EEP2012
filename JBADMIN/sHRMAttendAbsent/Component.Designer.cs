namespace sHRMAttendAbsent
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
            Srvtools.Service service1 = new Srvtools.Service();
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.Service service7 = new Srvtools.Service();
            Srvtools.Service service8 = new Srvtools.Service();
            Srvtools.Service service9 = new Srvtools.Service();
            Srvtools.Service service10 = new Srvtools.Service();
            Srvtools.Service service11 = new Srvtools.Service();
            Srvtools.Service service12 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HRMAttendAbsentApply = new Srvtools.InfoCommand(this.components);
            this.ucHRMAttendAbsentApply = new Srvtools.UpdateComponent(this.components);
            this.auto_AbsentMinusID = new Srvtools.AutoNumber(this.components);
            this.infoHRM_ATTEND_HOLIDAY = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.infoHRM_BASE_BASE = new Srvtools.InfoCommand(this.components);
            this.infoABSENT_PLUS = new Srvtools.InfoCommand(this.components);
            this.infoAgentEmployeeID = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendAbsentApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_ATTEND_HOLIDAY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_BASE_BASE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoABSENT_PLUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAgentEmployeeID)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // HRMAttendAbsentApply
            // 
            this.HRMAttendAbsentApply.CacheConnection = false;
            this.HRMAttendAbsentApply.CommandText = "SELECT dbo.[HRMAttendAbsentApply].* FROM dbo.[HRMAttendAbsentApply]";
            this.HRMAttendAbsentApply.CommandTimeout = 30;
            this.HRMAttendAbsentApply.CommandType = System.Data.CommandType.Text;
            this.HRMAttendAbsentApply.DynamicTableName = false;
            this.HRMAttendAbsentApply.EEPAlias = null;
            this.HRMAttendAbsentApply.EncodingAfter = null;
            this.HRMAttendAbsentApply.EncodingBefore = "Windows-1252";
            this.HRMAttendAbsentApply.EncodingConvert = null;
            this.HRMAttendAbsentApply.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AbsentMinusID";
            this.HRMAttendAbsentApply.KeyFields.Add(keyItem1);
            this.HRMAttendAbsentApply.MultiSetWhere = false;
            this.HRMAttendAbsentApply.Name = "HRMAttendAbsentApply";
            this.HRMAttendAbsentApply.NotificationAutoEnlist = false;
            this.HRMAttendAbsentApply.SecExcept = null;
            this.HRMAttendAbsentApply.SecFieldName = null;
            this.HRMAttendAbsentApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRMAttendAbsentApply.SelectPaging = false;
            this.HRMAttendAbsentApply.SelectTop = 0;
            this.HRMAttendAbsentApply.SiteControl = false;
            this.HRMAttendAbsentApply.SiteFieldName = null;
            this.HRMAttendAbsentApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHRMAttendAbsentApply
            // 
            this.ucHRMAttendAbsentApply.AutoTrans = true;
            this.ucHRMAttendAbsentApply.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AbsentMinusID";
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
            fieldAttr3.DataField = "BeginDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "EndDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "BeginTime";
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
            fieldAttr7.DataField = "AbsentDateTimeBegin";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AbsentDateTimeEnd";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "HolidayID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "TotalHours";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "flowflag";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateBy";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr1);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr2);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr3);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr4);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr5);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr6);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr7);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr8);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr9);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr10);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr11);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr12);
            this.ucHRMAttendAbsentApply.FieldAttrs.Add(fieldAttr13);
            this.ucHRMAttendAbsentApply.LogInfo = null;
            this.ucHRMAttendAbsentApply.Name = "ucHRMAttendAbsentApply";
            this.ucHRMAttendAbsentApply.RowAffectsCheck = true;
            this.ucHRMAttendAbsentApply.SelectCmd = this.HRMAttendAbsentApply;
            this.ucHRMAttendAbsentApply.SelectCmdForUpdate = null;
            this.ucHRMAttendAbsentApply.SendSQLCmd = true;
            this.ucHRMAttendAbsentApply.ServerModify = true;
            this.ucHRMAttendAbsentApply.ServerModifyGetMax = false;
            this.ucHRMAttendAbsentApply.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHRMAttendAbsentApply.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHRMAttendAbsentApply.UseTranscationScope = false;
            this.ucHRMAttendAbsentApply.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHRMAttendAbsentApply.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHRMAttendAbsentApply_BeforeInsert);
            // 
            // auto_AbsentMinusID
            // 
            this.auto_AbsentMinusID.Active = true;
            this.auto_AbsentMinusID.AutoNoID = "AbsentMinusID";
            this.auto_AbsentMinusID.Description = "";
            this.auto_AbsentMinusID.GetFixed = "";
            this.auto_AbsentMinusID.isNumFill = false;
            this.auto_AbsentMinusID.Name = "auto_AbsentMinusID";
            this.auto_AbsentMinusID.Number = null;
            this.auto_AbsentMinusID.NumDig = 10;
            this.auto_AbsentMinusID.OldVersion = false;
            this.auto_AbsentMinusID.OverFlow = true;
            this.auto_AbsentMinusID.StartValue = 1;
            this.auto_AbsentMinusID.Step = 1;
            this.auto_AbsentMinusID.TargetColumn = "AbsentMinusID";
            this.auto_AbsentMinusID.UpdateComp = this.ucHRMAttendAbsentApply;
            // 
            // infoHRM_ATTEND_HOLIDAY
            // 
            this.infoHRM_ATTEND_HOLIDAY.CacheConnection = false;
            this.infoHRM_ATTEND_HOLIDAY.CommandText = "SELECT HOLIDAY_ID,HOLIDAY_CODE,HOLIDAY_CNAME,HOLIDAY_FLAG       \r\nFROM  HRM_ATTEN" +
    "D_HOLIDAY\r\nwhere HOLIDAY_KIND_ID!=1";
            this.infoHRM_ATTEND_HOLIDAY.CommandTimeout = 30;
            this.infoHRM_ATTEND_HOLIDAY.CommandType = System.Data.CommandType.Text;
            this.infoHRM_ATTEND_HOLIDAY.DynamicTableName = false;
            this.infoHRM_ATTEND_HOLIDAY.EEPAlias = "JBHR_EEP";
            this.infoHRM_ATTEND_HOLIDAY.EncodingAfter = null;
            this.infoHRM_ATTEND_HOLIDAY.EncodingBefore = "Windows-1252";
            this.infoHRM_ATTEND_HOLIDAY.EncodingConvert = null;
            this.infoHRM_ATTEND_HOLIDAY.InfoConnection = this.infoConnection2;
            keyItem2.KeyName = "HOLIDAY_ID";
            this.infoHRM_ATTEND_HOLIDAY.KeyFields.Add(keyItem2);
            this.infoHRM_ATTEND_HOLIDAY.MultiSetWhere = false;
            this.infoHRM_ATTEND_HOLIDAY.Name = "infoHRM_ATTEND_HOLIDAY";
            this.infoHRM_ATTEND_HOLIDAY.NotificationAutoEnlist = false;
            this.infoHRM_ATTEND_HOLIDAY.SecExcept = null;
            this.infoHRM_ATTEND_HOLIDAY.SecFieldName = null;
            this.infoHRM_ATTEND_HOLIDAY.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoHRM_ATTEND_HOLIDAY.SelectPaging = false;
            this.infoHRM_ATTEND_HOLIDAY.SelectTop = 0;
            this.infoHRM_ATTEND_HOLIDAY.SiteControl = false;
            this.infoHRM_ATTEND_HOLIDAY.SiteFieldName = null;
            this.infoHRM_ATTEND_HOLIDAY.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBHR_EEP";
            // 
            // serviceManager1
            // 
            service1.DelegateName = "getDeptInfo";
            service1.NonLogin = false;
            service1.ServiceName = "getDeptInfo";
            service2.DelegateName = "checkAbsentHours";
            service2.NonLogin = false;
            service2.ServiceName = "checkAbsentHours";
            service3.DelegateName = "checkAbsentRestHours";
            service3.NonLogin = false;
            service3.ServiceName = "checkAbsentRestHours";
            service4.DelegateName = "checkAbsentData";
            service4.NonLogin = false;
            service4.ServiceName = "checkAbsentData";
            service5.DelegateName = "getSalaryYYMM";
            service5.NonLogin = false;
            service5.ServiceName = "getSalaryYYMM";
            service6.DelegateName = "procInsertHRM_ATTEND_ABSENT_MINUS";
            service6.NonLogin = false;
            service6.ServiceName = "procInsertHRM_ATTEND_ABSENT_MINUS";
            service7.DelegateName = "flowCheckHours";
            service7.NonLogin = false;
            service7.ServiceName = "flowCheckHours";
            service8.DelegateName = "checkPhysiologyleavesID";
            service8.NonLogin = false;
            service8.ServiceName = "checkPhysiologyleavesID";
            service9.DelegateName = "GetHolidayData";
            service9.NonLogin = false;
            service9.ServiceName = "GetHolidayData";
            service10.DelegateName = "checkOnData";
            service10.NonLogin = false;
            service10.ServiceName = "checkOnData";
            service11.DelegateName = "checkHolidaySex";
            service11.NonLogin = false;
            service11.ServiceName = "checkHolidaySex";
            service12.DelegateName = "getEmployeeID";
            service12.NonLogin = false;
            service12.ServiceName = "getEmployeeID";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            this.serviceManager1.ServiceCollection.Add(service9);
            this.serviceManager1.ServiceCollection.Add(service10);
            this.serviceManager1.ServiceCollection.Add(service11);
            this.serviceManager1.ServiceCollection.Add(service12);
            // 
            // infoHRM_BASE_BASE
            // 
            this.infoHRM_BASE_BASE.CacheConnection = false;
            this.infoHRM_BASE_BASE.CommandText = "SELECT  EMPLOYEE_ID,EMPLOYEE_CODE,NAME_C       \r\nFROM   HRM_BASE_BASE";
            this.infoHRM_BASE_BASE.CommandTimeout = 30;
            this.infoHRM_BASE_BASE.CommandType = System.Data.CommandType.Text;
            this.infoHRM_BASE_BASE.DynamicTableName = false;
            this.infoHRM_BASE_BASE.EEPAlias = "JBHR_EEP";
            this.infoHRM_BASE_BASE.EncodingAfter = null;
            this.infoHRM_BASE_BASE.EncodingBefore = "Windows-1252";
            this.infoHRM_BASE_BASE.EncodingConvert = null;
            this.infoHRM_BASE_BASE.InfoConnection = this.infoConnection2;
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
            // infoABSENT_PLUS
            // 
            this.infoABSENT_PLUS.CacheConnection = false;
            this.infoABSENT_PLUS.CommandText = resources.GetString("infoABSENT_PLUS.CommandText");
            this.infoABSENT_PLUS.CommandTimeout = 30;
            this.infoABSENT_PLUS.CommandType = System.Data.CommandType.Text;
            this.infoABSENT_PLUS.DynamicTableName = false;
            this.infoABSENT_PLUS.EEPAlias = "JBHR_EEP";
            this.infoABSENT_PLUS.EncodingAfter = null;
            this.infoABSENT_PLUS.EncodingBefore = "Windows-1252";
            this.infoABSENT_PLUS.EncodingConvert = null;
            this.infoABSENT_PLUS.InfoConnection = this.infoConnection2;
            this.infoABSENT_PLUS.MultiSetWhere = false;
            this.infoABSENT_PLUS.Name = "infoABSENT_PLUS";
            this.infoABSENT_PLUS.NotificationAutoEnlist = false;
            this.infoABSENT_PLUS.SecExcept = null;
            this.infoABSENT_PLUS.SecFieldName = null;
            this.infoABSENT_PLUS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoABSENT_PLUS.SelectPaging = false;
            this.infoABSENT_PLUS.SelectTop = 0;
            this.infoABSENT_PLUS.SiteControl = false;
            this.infoABSENT_PLUS.SiteFieldName = null;
            this.infoABSENT_PLUS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoAgentEmployeeID
            // 
            this.infoAgentEmployeeID.CacheConnection = false;
            this.infoAgentEmployeeID.CommandText = "SELECT  EMPLOYEE_ID,EMPLOYEE_CODE,NAME_C  \r\nFrom [dtHRM_BaseAndBasetts_Employed](" +
    "GetDate())";
            this.infoAgentEmployeeID.CommandTimeout = 30;
            this.infoAgentEmployeeID.CommandType = System.Data.CommandType.Text;
            this.infoAgentEmployeeID.DynamicTableName = false;
            this.infoAgentEmployeeID.EEPAlias = "JBHR_EEP";
            this.infoAgentEmployeeID.EncodingAfter = null;
            this.infoAgentEmployeeID.EncodingBefore = "Windows-1252";
            this.infoAgentEmployeeID.EncodingConvert = null;
            this.infoAgentEmployeeID.InfoConnection = this.infoConnection2;
            keyItem3.KeyName = "EMPLOYEE_ID";
            this.infoAgentEmployeeID.KeyFields.Add(keyItem3);
            this.infoAgentEmployeeID.MultiSetWhere = false;
            this.infoAgentEmployeeID.Name = "infoAgentEmployeeID";
            this.infoAgentEmployeeID.NotificationAutoEnlist = false;
            this.infoAgentEmployeeID.SecExcept = null;
            this.infoAgentEmployeeID.SecFieldName = null;
            this.infoAgentEmployeeID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoAgentEmployeeID.SelectPaging = false;
            this.infoAgentEmployeeID.SelectTop = 0;
            this.infoAgentEmployeeID.SiteControl = false;
            this.infoAgentEmployeeID.SiteFieldName = null;
            this.infoAgentEmployeeID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendAbsentApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_ATTEND_HOLIDAY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_BASE_BASE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoABSENT_PLUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAgentEmployeeID)).EndInit();

        }

        #endregion

        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HRMAttendAbsentApply;
        private Srvtools.UpdateComponent ucHRMAttendAbsentApply;
        private Srvtools.AutoNumber auto_AbsentMinusID;
        private Srvtools.InfoCommand infoHRM_ATTEND_HOLIDAY;
        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoCommand infoHRM_BASE_BASE;
        private Srvtools.InfoCommand infoABSENT_PLUS;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand infoAgentEmployeeID;
    }
}
