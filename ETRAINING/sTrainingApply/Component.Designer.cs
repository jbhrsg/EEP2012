﻿namespace sTrainingApply
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
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr26 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr27 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr28 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr29 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr30 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr31 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr32 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr33 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CourseApply = new Srvtools.InfoCommand(this.components);
            this.ucCourseApply = new Srvtools.UpdateComponent(this.components);
            this.View_CourseApply = new Srvtools.InfoCommand(this.components);
            this.Student = new Srvtools.InfoCommand(this.components);
            this.StudentTitle = new Srvtools.InfoCommand(this.components);
            this.ApplyType = new Srvtools.InfoCommand(this.components);
            this.Course = new Srvtools.InfoCommand(this.components);
            this.CourseMethod = new Srvtools.InfoCommand(this.components);
            this.TrainingInstitute = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CourseApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Student)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StudentTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApplyType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Course)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingInstitute)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetApplyStudentInfo";
            service1.NonLogin = false;
            service1.ServiceName = "GetApplyStudentInfo";
            service2.DelegateName = "GetEmpFlowAgentList";
            service2.NonLogin = false;
            service2.ServiceName = "GetEmpFlowAgentList";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "eTraining";
            // 
            // CourseApply
            // 
            this.CourseApply.CacheConnection = false;
            this.CourseApply.CommandText = "SELECT dbo.CourseApply.*\r\nFROM dbo.CourseApply\r\n";
            this.CourseApply.CommandTimeout = 30;
            this.CourseApply.CommandType = System.Data.CommandType.Text;
            this.CourseApply.DynamicTableName = false;
            this.CourseApply.EEPAlias = null;
            this.CourseApply.EncodingAfter = null;
            this.CourseApply.EncodingBefore = "Windows-1252";
            this.CourseApply.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ApplyID";
            this.CourseApply.KeyFields.Add(keyItem1);
            this.CourseApply.MultiSetWhere = false;
            this.CourseApply.Name = "CourseApply";
            this.CourseApply.NotificationAutoEnlist = false;
            this.CourseApply.SecExcept = null;
            this.CourseApply.SecFieldName = null;
            this.CourseApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CourseApply.SelectPaging = false;
            this.CourseApply.SelectTop = 0;
            this.CourseApply.SiteControl = false;
            this.CourseApply.SiteFieldName = null;
            this.CourseApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCourseApply
            // 
            this.ucCourseApply.AutoTrans = true;
            this.ucCourseApply.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ApplyID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "StudentID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CCID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "PhoneExt";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Area";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "StudentTitleID";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CourseID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CourseName";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ApplyDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "TrainingPurpose";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Description";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CourseMethod";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CourseStartHour";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CourseEndHour";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "ApplyType";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "TrainingInstituteID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "TrainingInstitute";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "InstituteAddress";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "InstitutePhone";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "IsCertifiedCourse";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "TermStartDate";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "TermEndDate";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "TotalHours";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "Tuition";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "IsOverseaCourse";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "IsEnrollBySelf";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "Flowflag";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "CreateBy";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "CreateDate";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "LastUpdateBy";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "LastUpdateDate";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "SelfTuition";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "IsDeclaration";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            this.ucCourseApply.FieldAttrs.Add(fieldAttr1);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr2);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr3);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr4);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr5);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr6);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr7);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr8);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr9);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr10);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr11);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr12);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr13);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr14);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr15);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr16);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr17);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr18);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr19);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr20);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr21);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr22);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr23);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr24);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr25);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr26);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr27);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr28);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr29);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr30);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr31);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr32);
            this.ucCourseApply.FieldAttrs.Add(fieldAttr33);
            this.ucCourseApply.LogInfo = null;
            this.ucCourseApply.Name = "ucCourseApply";
            this.ucCourseApply.RowAffectsCheck = true;
            this.ucCourseApply.SelectCmd = this.CourseApply;
            this.ucCourseApply.SelectCmdForUpdate = null;
            this.ucCourseApply.ServerModify = true;
            this.ucCourseApply.ServerModifyGetMax = false;
            this.ucCourseApply.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCourseApply.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCourseApply.UseTranscationScope = false;
            this.ucCourseApply.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_CourseApply
            // 
            this.View_CourseApply.CacheConnection = false;
            this.View_CourseApply.CommandText = "SELECT * FROM dbo.[CourseApply]";
            this.View_CourseApply.CommandTimeout = 30;
            this.View_CourseApply.CommandType = System.Data.CommandType.Text;
            this.View_CourseApply.DynamicTableName = false;
            this.View_CourseApply.EEPAlias = null;
            this.View_CourseApply.EncodingAfter = null;
            this.View_CourseApply.EncodingBefore = "Windows-1252";
            this.View_CourseApply.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ApplyID";
            this.View_CourseApply.KeyFields.Add(keyItem2);
            this.View_CourseApply.MultiSetWhere = false;
            this.View_CourseApply.Name = "View_CourseApply";
            this.View_CourseApply.NotificationAutoEnlist = false;
            this.View_CourseApply.SecExcept = null;
            this.View_CourseApply.SecFieldName = null;
            this.View_CourseApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CourseApply.SelectPaging = false;
            this.View_CourseApply.SelectTop = 0;
            this.View_CourseApply.SiteControl = false;
            this.View_CourseApply.SiteFieldName = null;
            this.View_CourseApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Student
            // 
            this.Student.CacheConnection = false;
            this.Student.CommandText = resources.GetString("Student.CommandText");
            this.Student.CommandTimeout = 30;
            this.Student.CommandType = System.Data.CommandType.Text;
            this.Student.DynamicTableName = false;
            this.Student.EEPAlias = null;
            this.Student.EncodingAfter = null;
            this.Student.EncodingBefore = "Windows-1252";
            this.Student.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "StudentID";
            this.Student.KeyFields.Add(keyItem3);
            this.Student.MultiSetWhere = false;
            this.Student.Name = "Student";
            this.Student.NotificationAutoEnlist = false;
            this.Student.SecExcept = null;
            this.Student.SecFieldName = null;
            this.Student.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Student.SelectPaging = false;
            this.Student.SelectTop = 0;
            this.Student.SiteControl = false;
            this.Student.SiteFieldName = null;
            this.Student.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // StudentTitle
            // 
            this.StudentTitle.CacheConnection = false;
            this.StudentTitle.CommandText = "SELECT ID,NAME\r\nFROM LISTSTUDENTTITLE";
            this.StudentTitle.CommandTimeout = 30;
            this.StudentTitle.CommandType = System.Data.CommandType.Text;
            this.StudentTitle.DynamicTableName = false;
            this.StudentTitle.EEPAlias = null;
            this.StudentTitle.EncodingAfter = null;
            this.StudentTitle.EncodingBefore = "Windows-1252";
            this.StudentTitle.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ID";
            this.StudentTitle.KeyFields.Add(keyItem4);
            this.StudentTitle.MultiSetWhere = false;
            this.StudentTitle.Name = "StudentTitle";
            this.StudentTitle.NotificationAutoEnlist = false;
            this.StudentTitle.SecExcept = null;
            this.StudentTitle.SecFieldName = null;
            this.StudentTitle.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.StudentTitle.SelectPaging = false;
            this.StudentTitle.SelectTop = 0;
            this.StudentTitle.SiteControl = false;
            this.StudentTitle.SiteFieldName = null;
            this.StudentTitle.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ApplyType
            // 
            this.ApplyType.CacheConnection = false;
            this.ApplyType.CommandText = "SELECT * FROM ListApplyType";
            this.ApplyType.CommandTimeout = 30;
            this.ApplyType.CommandType = System.Data.CommandType.Text;
            this.ApplyType.DynamicTableName = false;
            this.ApplyType.EEPAlias = null;
            this.ApplyType.EncodingAfter = null;
            this.ApplyType.EncodingBefore = "Windows-1252";
            this.ApplyType.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "AutoKey";
            this.ApplyType.KeyFields.Add(keyItem5);
            this.ApplyType.MultiSetWhere = false;
            this.ApplyType.Name = "ApplyType";
            this.ApplyType.NotificationAutoEnlist = false;
            this.ApplyType.SecExcept = null;
            this.ApplyType.SecFieldName = null;
            this.ApplyType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ApplyType.SelectPaging = false;
            this.ApplyType.SelectTop = 0;
            this.ApplyType.SiteControl = false;
            this.ApplyType.SiteFieldName = null;
            this.ApplyType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Course
            // 
            this.Course.CacheConnection = false;
            this.Course.CommandText = resources.GetString("Course.CommandText");
            this.Course.CommandTimeout = 30;
            this.Course.CommandType = System.Data.CommandType.Text;
            this.Course.DynamicTableName = false;
            this.Course.EEPAlias = null;
            this.Course.EncodingAfter = null;
            this.Course.EncodingBefore = "Windows-1252";
            this.Course.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "CourseID";
            this.Course.KeyFields.Add(keyItem6);
            this.Course.MultiSetWhere = false;
            this.Course.Name = "Course";
            this.Course.NotificationAutoEnlist = false;
            this.Course.SecExcept = null;
            this.Course.SecFieldName = null;
            this.Course.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Course.SelectPaging = false;
            this.Course.SelectTop = 0;
            this.Course.SiteControl = false;
            this.Course.SiteFieldName = null;
            this.Course.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CourseMethod
            // 
            this.CourseMethod.CacheConnection = false;
            this.CourseMethod.CommandText = "SELECT ID,NAME\r\nFROM LISTCOURSEMETHOD\r\nORDER BY ID";
            this.CourseMethod.CommandTimeout = 30;
            this.CourseMethod.CommandType = System.Data.CommandType.Text;
            this.CourseMethod.DynamicTableName = false;
            this.CourseMethod.EEPAlias = null;
            this.CourseMethod.EncodingAfter = null;
            this.CourseMethod.EncodingBefore = "Windows-1252";
            this.CourseMethod.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "ID";
            this.CourseMethod.KeyFields.Add(keyItem7);
            this.CourseMethod.MultiSetWhere = false;
            this.CourseMethod.Name = "CourseMethod";
            this.CourseMethod.NotificationAutoEnlist = false;
            this.CourseMethod.SecExcept = null;
            this.CourseMethod.SecFieldName = null;
            this.CourseMethod.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CourseMethod.SelectPaging = false;
            this.CourseMethod.SelectTop = 0;
            this.CourseMethod.SiteControl = false;
            this.CourseMethod.SiteFieldName = null;
            this.CourseMethod.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // TrainingInstitute
            // 
            this.TrainingInstitute.CacheConnection = false;
            this.TrainingInstitute.CommandText = "SELECT ID,NAME FROM TrainingCompany\r\nORDER BY NAME";
            this.TrainingInstitute.CommandTimeout = 30;
            this.TrainingInstitute.CommandType = System.Data.CommandType.Text;
            this.TrainingInstitute.DynamicTableName = false;
            this.TrainingInstitute.EEPAlias = null;
            this.TrainingInstitute.EncodingAfter = null;
            this.TrainingInstitute.EncodingBefore = "Windows-1252";
            this.TrainingInstitute.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "ID";
            this.TrainingInstitute.KeyFields.Add(keyItem8);
            this.TrainingInstitute.MultiSetWhere = false;
            this.TrainingInstitute.Name = "TrainingInstitute";
            this.TrainingInstitute.NotificationAutoEnlist = false;
            this.TrainingInstitute.SecExcept = null;
            this.TrainingInstitute.SecFieldName = null;
            this.TrainingInstitute.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.TrainingInstitute.SelectPaging = false;
            this.TrainingInstitute.SelectTop = 0;
            this.TrainingInstitute.SiteControl = false;
            this.TrainingInstitute.SiteFieldName = null;
            this.TrainingInstitute.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CourseApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Student)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StudentTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApplyType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Course)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingInstitute)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CourseApply;
        private Srvtools.UpdateComponent ucCourseApply;
        private Srvtools.InfoCommand View_CourseApply;
        private Srvtools.InfoCommand Student;
        private Srvtools.InfoCommand StudentTitle;
        private Srvtools.InfoCommand ApplyType;
        private Srvtools.InfoCommand Course;
        private Srvtools.InfoCommand CourseMethod;
        private Srvtools.InfoCommand TrainingInstitute;
    }
}
