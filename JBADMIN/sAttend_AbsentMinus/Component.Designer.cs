namespace sAttend_AbsentMinus
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
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection2 = new Srvtools.InfoConnection(this.components);
            this.HRM_ATTEND_ABSENT_MINUS = new Srvtools.InfoCommand(this.components);
            this.ucHRM_ATTEND_ABSENT_MINUS = new Srvtools.UpdateComponent(this.components);
            this.autoNumber_ABSENT_MINUS_ID = new Srvtools.AutoNumber(this.components);
            this.infoHRM_BASE_BASE = new Srvtools.InfoCommand(this.components);
            this.infoHRM_ATTEND_HOLIDAY = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_ATTEND_ABSENT_MINUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_BASE_BASE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_ATTEND_HOLIDAY)).BeginInit();
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
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            // 
            // InfoConnection2
            // 
            this.InfoConnection2.EEPAlias = "JBADMIN";
            // 
            // HRM_ATTEND_ABSENT_MINUS
            // 
            this.HRM_ATTEND_ABSENT_MINUS.CacheConnection = false;
            this.HRM_ATTEND_ABSENT_MINUS.CommandText = resources.GetString("HRM_ATTEND_ABSENT_MINUS.CommandText");
            this.HRM_ATTEND_ABSENT_MINUS.CommandTimeout = 30;
            this.HRM_ATTEND_ABSENT_MINUS.CommandType = System.Data.CommandType.Text;
            this.HRM_ATTEND_ABSENT_MINUS.DynamicTableName = false;
            this.HRM_ATTEND_ABSENT_MINUS.EEPAlias = "JBADMIN";
            this.HRM_ATTEND_ABSENT_MINUS.EncodingAfter = null;
            this.HRM_ATTEND_ABSENT_MINUS.EncodingBefore = "Windows-1252";
            this.HRM_ATTEND_ABSENT_MINUS.InfoConnection = this.InfoConnection2;
            keyItem1.KeyName = "ABSENT_MINUS_ID";
            this.HRM_ATTEND_ABSENT_MINUS.KeyFields.Add(keyItem1);
            this.HRM_ATTEND_ABSENT_MINUS.MultiSetWhere = false;
            this.HRM_ATTEND_ABSENT_MINUS.Name = "HRM_ATTEND_ABSENT_MINUS";
            this.HRM_ATTEND_ABSENT_MINUS.NotificationAutoEnlist = false;
            this.HRM_ATTEND_ABSENT_MINUS.SecExcept = null;
            this.HRM_ATTEND_ABSENT_MINUS.SecFieldName = null;
            this.HRM_ATTEND_ABSENT_MINUS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRM_ATTEND_ABSENT_MINUS.SelectPaging = false;
            this.HRM_ATTEND_ABSENT_MINUS.SelectTop = 0;
            this.HRM_ATTEND_ABSENT_MINUS.SiteControl = false;
            this.HRM_ATTEND_ABSENT_MINUS.SiteFieldName = null;
            this.HRM_ATTEND_ABSENT_MINUS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHRM_ATTEND_ABSENT_MINUS
            // 
            this.ucHRM_ATTEND_ABSENT_MINUS.AutoTrans = true;
            this.ucHRM_ATTEND_ABSENT_MINUS.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ABSENT_MINUS_ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "EMPLOYEE_ID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "BEGIN_DATE";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "END_DATE";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "BEGIN_TIME";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "END_TIME";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ABSENT_DATE_TIME_BEGIN";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ABSENT_DATE_TIME_END";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "HOLIDAY_ID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "TOTAL_HOURS";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "TOTAL_DAY";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "SALARY_YYMM";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "MEMO";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "ABSENT_NO";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "NOT_ALLOW_MODIFY";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "NOT_CALCULATE";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "SYSCREATE";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "FLOWFLAG";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "CREATE_MAN";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = "_username";
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CREATE_DATE";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = "_sysdate";
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "UPDATE_MAN";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr21.DefaultValue = "_username";
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "UPDATE_DATE";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr22.DefaultValue = "_sysdate";
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr1);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr2);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr3);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr4);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr5);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr6);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr7);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr8);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr9);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr10);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr11);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr12);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr13);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr14);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr15);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr16);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr17);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr18);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr19);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr20);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr21);
            this.ucHRM_ATTEND_ABSENT_MINUS.FieldAttrs.Add(fieldAttr22);
            this.ucHRM_ATTEND_ABSENT_MINUS.LogInfo = null;
            this.ucHRM_ATTEND_ABSENT_MINUS.Name = "ucHRM_ATTEND_ABSENT_MINUS";
            this.ucHRM_ATTEND_ABSENT_MINUS.RowAffectsCheck = true;
            this.ucHRM_ATTEND_ABSENT_MINUS.SelectCmd = this.HRM_ATTEND_ABSENT_MINUS;
            this.ucHRM_ATTEND_ABSENT_MINUS.SelectCmdForUpdate = null;
            this.ucHRM_ATTEND_ABSENT_MINUS.ServerModify = true;
            this.ucHRM_ATTEND_ABSENT_MINUS.ServerModifyGetMax = true;
            this.ucHRM_ATTEND_ABSENT_MINUS.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHRM_ATTEND_ABSENT_MINUS.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHRM_ATTEND_ABSENT_MINUS.UseTranscationScope = false;
            this.ucHRM_ATTEND_ABSENT_MINUS.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHRM_ATTEND_ABSENT_MINUS.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHRM_ATTEND_ABSENT_MINUS_BeforeInsert);
            // 
            // autoNumber_ABSENT_MINUS_ID
            // 
            this.autoNumber_ABSENT_MINUS_ID.Active = false;
            this.autoNumber_ABSENT_MINUS_ID.AutoNoID = "ABSENT_MINUS_ID";
            this.autoNumber_ABSENT_MINUS_ID.Description = "HRM_ATTEND_ABSENT_MINUS.ABSENT_MINUS_ID";
            this.autoNumber_ABSENT_MINUS_ID.GetFixed = "";
            this.autoNumber_ABSENT_MINUS_ID.isNumFill = false;
            this.autoNumber_ABSENT_MINUS_ID.Name = "autoNumber_ABSENT_MINUS_ID";
            this.autoNumber_ABSENT_MINUS_ID.Number = null;
            this.autoNumber_ABSENT_MINUS_ID.NumDig = 10;
            this.autoNumber_ABSENT_MINUS_ID.OldVersion = false;
            this.autoNumber_ABSENT_MINUS_ID.OverFlow = true;
            this.autoNumber_ABSENT_MINUS_ID.StartValue = 1;
            this.autoNumber_ABSENT_MINUS_ID.Step = 1;
            this.autoNumber_ABSENT_MINUS_ID.TargetColumn = "ABSENT_MINUS_ID";
            this.autoNumber_ABSENT_MINUS_ID.UpdateComp = this.ucHRM_ATTEND_ABSENT_MINUS;
            // 
            // infoHRM_BASE_BASE
            // 
            this.infoHRM_BASE_BASE.CacheConnection = false;
            this.infoHRM_BASE_BASE.CommandText = "SELECT EMPLOYEE_ID,EMPLOYEE_CODE,NAME_C       \r\nFROM   JBHR_EEP.dbo.HRM_BASE_BASE" +
    "";
            this.infoHRM_BASE_BASE.CommandTimeout = 30;
            this.infoHRM_BASE_BASE.CommandType = System.Data.CommandType.Text;
            this.infoHRM_BASE_BASE.DynamicTableName = false;
            this.infoHRM_BASE_BASE.EEPAlias = "JBADMIN";
            this.infoHRM_BASE_BASE.EncodingAfter = null;
            this.infoHRM_BASE_BASE.EncodingBefore = "Windows-1252";
            this.infoHRM_BASE_BASE.InfoConnection = this.InfoConnection2;
            keyItem2.KeyName = "EMPLOYEE_ID";
            this.infoHRM_BASE_BASE.KeyFields.Add(keyItem2);
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
            // infoHRM_ATTEND_HOLIDAY
            // 
            this.infoHRM_ATTEND_HOLIDAY.CacheConnection = false;
            this.infoHRM_ATTEND_HOLIDAY.CommandText = "SELECT HOLIDAY_ID,HOLIDAY_CODE,HOLIDAY_CNAME,HOLIDAY_FLAG       \r\nFROM   JBHR_EEP" +
    ".dbo.HRM_ATTEND_HOLIDAY";
            this.infoHRM_ATTEND_HOLIDAY.CommandTimeout = 30;
            this.infoHRM_ATTEND_HOLIDAY.CommandType = System.Data.CommandType.Text;
            this.infoHRM_ATTEND_HOLIDAY.DynamicTableName = false;
            this.infoHRM_ATTEND_HOLIDAY.EEPAlias = "JBADMIN";
            this.infoHRM_ATTEND_HOLIDAY.EncodingAfter = null;
            this.infoHRM_ATTEND_HOLIDAY.EncodingBefore = "Windows-1252";
            this.infoHRM_ATTEND_HOLIDAY.InfoConnection = this.InfoConnection2;
            keyItem3.KeyName = "HOLIDAY_ID";
            this.infoHRM_ATTEND_HOLIDAY.KeyFields.Add(keyItem3);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_ATTEND_ABSENT_MINUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_BASE_BASE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_ATTEND_HOLIDAY)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection2;
        private Srvtools.InfoCommand HRM_ATTEND_ABSENT_MINUS;
        private Srvtools.UpdateComponent ucHRM_ATTEND_ABSENT_MINUS;
        private Srvtools.AutoNumber autoNumber_ABSENT_MINUS_ID;
        private Srvtools.InfoCommand infoHRM_BASE_BASE;
        private Srvtools.InfoCommand infoHRM_ATTEND_HOLIDAY;

    }
}
