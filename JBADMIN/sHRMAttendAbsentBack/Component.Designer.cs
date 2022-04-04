namespace sHRMAttendAbsentBack
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
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HRMAttendAbsentBackApply = new Srvtools.InfoCommand(this.components);
            this.ucHRMAttendAbsentBackApply = new Srvtools.UpdateComponent(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.infoHRM_ATTEND_ABSENT_MINUS = new Srvtools.InfoCommand(this.components);
            this.auto_AbsentBackID = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendAbsentBackApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_ATTEND_ABSENT_MINUS)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "procDeleteHRM_ATTEND_ABSENT_MINUS";
            service1.NonLogin = false;
            service1.ServiceName = "procDeleteHRM_ATTEND_ABSENT_MINUS";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // HRMAttendAbsentBackApply
            // 
            this.HRMAttendAbsentBackApply.CacheConnection = false;
            this.HRMAttendAbsentBackApply.CommandText = "select * from HRMAttendAbsentBackApply ";
            this.HRMAttendAbsentBackApply.CommandTimeout = 30;
            this.HRMAttendAbsentBackApply.CommandType = System.Data.CommandType.Text;
            this.HRMAttendAbsentBackApply.DynamicTableName = false;
            this.HRMAttendAbsentBackApply.EEPAlias = null;
            this.HRMAttendAbsentBackApply.EncodingAfter = null;
            this.HRMAttendAbsentBackApply.EncodingBefore = "Windows-1252";
            this.HRMAttendAbsentBackApply.EncodingConvert = null;
            this.HRMAttendAbsentBackApply.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AbsentBackID";
            this.HRMAttendAbsentBackApply.KeyFields.Add(keyItem1);
            this.HRMAttendAbsentBackApply.MultiSetWhere = false;
            this.HRMAttendAbsentBackApply.Name = "HRMAttendAbsentBackApply";
            this.HRMAttendAbsentBackApply.NotificationAutoEnlist = false;
            this.HRMAttendAbsentBackApply.SecExcept = null;
            this.HRMAttendAbsentBackApply.SecFieldName = null;
            this.HRMAttendAbsentBackApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRMAttendAbsentBackApply.SelectPaging = false;
            this.HRMAttendAbsentBackApply.SelectTop = 0;
            this.HRMAttendAbsentBackApply.SiteControl = false;
            this.HRMAttendAbsentBackApply.SiteFieldName = null;
            this.HRMAttendAbsentBackApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHRMAttendAbsentBackApply
            // 
            this.ucHRMAttendAbsentBackApply.AutoTrans = true;
            this.ucHRMAttendAbsentBackApply.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AbsentBackID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "AbsentMinusID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EmployeeText";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "HolidayText";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Memo";
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
            this.ucHRMAttendAbsentBackApply.FieldAttrs.Add(fieldAttr1);
            this.ucHRMAttendAbsentBackApply.FieldAttrs.Add(fieldAttr2);
            this.ucHRMAttendAbsentBackApply.FieldAttrs.Add(fieldAttr3);
            this.ucHRMAttendAbsentBackApply.FieldAttrs.Add(fieldAttr4);
            this.ucHRMAttendAbsentBackApply.FieldAttrs.Add(fieldAttr5);
            this.ucHRMAttendAbsentBackApply.FieldAttrs.Add(fieldAttr6);
            this.ucHRMAttendAbsentBackApply.FieldAttrs.Add(fieldAttr7);
            this.ucHRMAttendAbsentBackApply.FieldAttrs.Add(fieldAttr8);
            this.ucHRMAttendAbsentBackApply.LogInfo = null;
            this.ucHRMAttendAbsentBackApply.Name = "ucHRMAttendAbsentBackApply";
            this.ucHRMAttendAbsentBackApply.RowAffectsCheck = true;
            this.ucHRMAttendAbsentBackApply.SelectCmd = this.HRMAttendAbsentBackApply;
            this.ucHRMAttendAbsentBackApply.SelectCmdForUpdate = null;
            this.ucHRMAttendAbsentBackApply.SendSQLCmd = true;
            this.ucHRMAttendAbsentBackApply.ServerModify = true;
            this.ucHRMAttendAbsentBackApply.ServerModifyGetMax = false;
            this.ucHRMAttendAbsentBackApply.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHRMAttendAbsentBackApply.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHRMAttendAbsentBackApply.UseTranscationScope = false;
            this.ucHRMAttendAbsentBackApply.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBHR_EEP";
            // 
            // infoHRM_ATTEND_ABSENT_MINUS
            // 
            this.infoHRM_ATTEND_ABSENT_MINUS.CacheConnection = false;
            this.infoHRM_ATTEND_ABSENT_MINUS.CommandText = "select * from HRM_ATTEND_ABSENT_MINUS m\r\n\tinner join HRM_ATTEND_HOLIDAY h on m.HO" +
    "LIDAY_ID=h.HOLIDAY_ID\r\nwhere m.SALARY_YYMM>=Left(CONVERT(nvarchar(10),GETDATE()," +
    "112),6)\r\norder by m.BEGIN_DATE desc";
            this.infoHRM_ATTEND_ABSENT_MINUS.CommandTimeout = 30;
            this.infoHRM_ATTEND_ABSENT_MINUS.CommandType = System.Data.CommandType.Text;
            this.infoHRM_ATTEND_ABSENT_MINUS.DynamicTableName = false;
            this.infoHRM_ATTEND_ABSENT_MINUS.EEPAlias = "JBHR_EEP";
            this.infoHRM_ATTEND_ABSENT_MINUS.EncodingAfter = null;
            this.infoHRM_ATTEND_ABSENT_MINUS.EncodingBefore = "Windows-1252";
            this.infoHRM_ATTEND_ABSENT_MINUS.EncodingConvert = null;
            this.infoHRM_ATTEND_ABSENT_MINUS.InfoConnection = this.infoConnection2;
            keyItem2.KeyName = "ABSENT_MINUS_ID";
            this.infoHRM_ATTEND_ABSENT_MINUS.KeyFields.Add(keyItem2);
            this.infoHRM_ATTEND_ABSENT_MINUS.MultiSetWhere = false;
            this.infoHRM_ATTEND_ABSENT_MINUS.Name = "infoHRM_ATTEND_ABSENT_MINUS";
            this.infoHRM_ATTEND_ABSENT_MINUS.NotificationAutoEnlist = false;
            this.infoHRM_ATTEND_ABSENT_MINUS.SecExcept = null;
            this.infoHRM_ATTEND_ABSENT_MINUS.SecFieldName = null;
            this.infoHRM_ATTEND_ABSENT_MINUS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoHRM_ATTEND_ABSENT_MINUS.SelectPaging = false;
            this.infoHRM_ATTEND_ABSENT_MINUS.SelectTop = 0;
            this.infoHRM_ATTEND_ABSENT_MINUS.SiteControl = false;
            this.infoHRM_ATTEND_ABSENT_MINUS.SiteFieldName = null;
            this.infoHRM_ATTEND_ABSENT_MINUS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // auto_AbsentBackID
            // 
            this.auto_AbsentBackID.Active = true;
            this.auto_AbsentBackID.AutoNoID = "AbsentBackID";
            this.auto_AbsentBackID.Description = "";
            this.auto_AbsentBackID.GetFixed = "";
            this.auto_AbsentBackID.isNumFill = false;
            this.auto_AbsentBackID.Name = "auto_AbsentBackID";
            this.auto_AbsentBackID.Number = null;
            this.auto_AbsentBackID.NumDig = 10;
            this.auto_AbsentBackID.OldVersion = false;
            this.auto_AbsentBackID.OverFlow = true;
            this.auto_AbsentBackID.StartValue = 1;
            this.auto_AbsentBackID.Step = 1;
            this.auto_AbsentBackID.TargetColumn = "AbsentBackID";
            this.auto_AbsentBackID.UpdateComp = this.ucHRMAttendAbsentBackApply;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRMAttendAbsentBackApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHRM_ATTEND_ABSENT_MINUS)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HRMAttendAbsentBackApply;
        private Srvtools.UpdateComponent ucHRMAttendAbsentBackApply;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand infoHRM_ATTEND_ABSENT_MINUS;
        private Srvtools.AutoNumber auto_AbsentBackID;
    }
}
