namespace sCoursePlan
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
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
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CoursePlanRecord = new Srvtools.InfoCommand(this.components);
            this.ucCoursePlanRecord = new Srvtools.UpdateComponent(this.components);
            this.View_CoursePlanRecord = new Srvtools.InfoCommand(this.components);
            this.YearNO = new Srvtools.InfoCommand(this.components);
            this.SpreadType = new Srvtools.InfoCommand(this.components);
            this.CoursePlanDetails = new Srvtools.InfoCommand(this.components);
            this.ucCoursePlanDetails = new Srvtools.UpdateComponent(this.components);
            this.OpenSeason = new Srvtools.InfoCommand(this.components);
            this.CourseStudentList = new Srvtools.InfoCommand(this.components);
            this.PlanType = new Srvtools.InfoCommand(this.components);
            this.UserInfo = new Srvtools.InfoCommand(this.components);
            this.CourseList = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoursePlanRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CoursePlanRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpreadType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoursePlanDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenSeason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseStudentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseList)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "procSetUpCoursePlan";
            service1.NonLogin = false;
            service1.ServiceName = "procSetUpCoursePlan";
            service2.DelegateName = "CheckDelCoursePlan";
            service2.NonLogin = false;
            service2.ServiceName = "CheckDelCoursePlan";
            service3.DelegateName = "procDeleteCoursePlan";
            service3.NonLogin = false;
            service3.ServiceName = "procDeleteCoursePlan";
            service4.DelegateName = "GetCoursePlanStudentList";
            service4.NonLogin = false;
            service4.ServiceName = "GetCoursePlanStudentList";
            service5.DelegateName = "DeleteCoursePlanStudentList";
            service5.NonLogin = false;
            service5.ServiceName = "DeleteCoursePlanStudentList";
            service6.DelegateName = "GetCourseListCoursePlan";
            service6.NonLogin = false;
            service6.ServiceName = "GetCourseListCoursePlan";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "eTraining";
            // 
            // CoursePlanRecord
            // 
            this.CoursePlanRecord.CacheConnection = false;
            this.CoursePlanRecord.CommandText = resources.GetString("CoursePlanRecord.CommandText");
            this.CoursePlanRecord.CommandTimeout = 30;
            this.CoursePlanRecord.CommandType = System.Data.CommandType.Text;
            this.CoursePlanRecord.DynamicTableName = false;
            this.CoursePlanRecord.EEPAlias = null;
            this.CoursePlanRecord.EncodingAfter = null;
            this.CoursePlanRecord.EncodingBefore = "Windows-1252";
            this.CoursePlanRecord.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "PlanID";
            this.CoursePlanRecord.KeyFields.Add(keyItem1);
            this.CoursePlanRecord.MultiSetWhere = false;
            this.CoursePlanRecord.Name = "CoursePlanRecord";
            this.CoursePlanRecord.NotificationAutoEnlist = false;
            this.CoursePlanRecord.SecExcept = null;
            this.CoursePlanRecord.SecFieldName = null;
            this.CoursePlanRecord.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CoursePlanRecord.SelectPaging = false;
            this.CoursePlanRecord.SelectTop = 0;
            this.CoursePlanRecord.SiteControl = false;
            this.CoursePlanRecord.SiteFieldName = null;
            this.CoursePlanRecord.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCoursePlanRecord
            // 
            this.ucCoursePlanRecord.AutoTrans = true;
            this.ucCoursePlanRecord.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "PlanID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "RequestID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "PlanName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Year";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "PlanType";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "PlanRange";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "IsMain";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "IsOpen";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateBy";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = "_usercode";
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
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr11.DefaultValue = "_usercode";
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
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr1);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr2);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr3);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr4);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr5);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr6);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr7);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr8);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr9);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr10);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr11);
            this.ucCoursePlanRecord.FieldAttrs.Add(fieldAttr12);
            this.ucCoursePlanRecord.LogInfo = null;
            this.ucCoursePlanRecord.Name = "ucCoursePlanRecord";
            this.ucCoursePlanRecord.RowAffectsCheck = true;
            this.ucCoursePlanRecord.SelectCmd = this.CoursePlanRecord;
            this.ucCoursePlanRecord.SelectCmdForUpdate = null;
            this.ucCoursePlanRecord.ServerModify = true;
            this.ucCoursePlanRecord.ServerModifyGetMax = false;
            this.ucCoursePlanRecord.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCoursePlanRecord.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCoursePlanRecord.UseTranscationScope = false;
            this.ucCoursePlanRecord.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCoursePlanRecord.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCoursePlanRecord_BeforeInsert);
            this.ucCoursePlanRecord.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCoursePlanRecord_BeforeModify);
            // 
            // View_CoursePlanRecord
            // 
            this.View_CoursePlanRecord.CacheConnection = false;
            this.View_CoursePlanRecord.CommandText = "SELECT * FROM dbo.[CoursePlanRecord]";
            this.View_CoursePlanRecord.CommandTimeout = 30;
            this.View_CoursePlanRecord.CommandType = System.Data.CommandType.Text;
            this.View_CoursePlanRecord.DynamicTableName = false;
            this.View_CoursePlanRecord.EEPAlias = null;
            this.View_CoursePlanRecord.EncodingAfter = null;
            this.View_CoursePlanRecord.EncodingBefore = "Windows-1252";
            this.View_CoursePlanRecord.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "PlanID";
            this.View_CoursePlanRecord.KeyFields.Add(keyItem2);
            this.View_CoursePlanRecord.MultiSetWhere = false;
            this.View_CoursePlanRecord.Name = "View_CoursePlanRecord";
            this.View_CoursePlanRecord.NotificationAutoEnlist = false;
            this.View_CoursePlanRecord.SecExcept = null;
            this.View_CoursePlanRecord.SecFieldName = null;
            this.View_CoursePlanRecord.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CoursePlanRecord.SelectPaging = false;
            this.View_CoursePlanRecord.SelectTop = 0;
            this.View_CoursePlanRecord.SiteControl = false;
            this.View_CoursePlanRecord.SiteFieldName = null;
            this.View_CoursePlanRecord.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // YearNO
            // 
            this.YearNO.CacheConnection = false;
            this.YearNO.CommandText = "SELECT Distinct  YEAR  FROM CoursePlanRecord\r\nUNION\r\nSELECT YEAR(GETDATE())  AS Y" +
    "EAR\r\nORDER BY YEAR DESC";
            this.YearNO.CommandTimeout = 30;
            this.YearNO.CommandType = System.Data.CommandType.Text;
            this.YearNO.DynamicTableName = false;
            this.YearNO.EEPAlias = null;
            this.YearNO.EncodingAfter = null;
            this.YearNO.EncodingBefore = "Windows-1252";
            this.YearNO.InfoConnection = this.InfoConnection1;
            this.YearNO.MultiSetWhere = false;
            this.YearNO.Name = "YearNO";
            this.YearNO.NotificationAutoEnlist = false;
            this.YearNO.SecExcept = null;
            this.YearNO.SecFieldName = null;
            this.YearNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.YearNO.SelectPaging = false;
            this.YearNO.SelectTop = 0;
            this.YearNO.SiteControl = false;
            this.YearNO.SiteFieldName = null;
            this.YearNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SpreadType
            // 
            this.SpreadType.CacheConnection = false;
            this.SpreadType.CommandText = "SELECT ID ,NAME FROM LISTTABLE \r\nWHERE CLASS=\'SpreadType\'\r\nORDER BY ID";
            this.SpreadType.CommandTimeout = 30;
            this.SpreadType.CommandType = System.Data.CommandType.Text;
            this.SpreadType.DynamicTableName = false;
            this.SpreadType.EEPAlias = null;
            this.SpreadType.EncodingAfter = null;
            this.SpreadType.EncodingBefore = "Windows-1252";
            this.SpreadType.InfoConnection = this.InfoConnection1;
            this.SpreadType.MultiSetWhere = false;
            this.SpreadType.Name = "SpreadType";
            this.SpreadType.NotificationAutoEnlist = false;
            this.SpreadType.SecExcept = null;
            this.SpreadType.SecFieldName = null;
            this.SpreadType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SpreadType.SelectPaging = false;
            this.SpreadType.SelectTop = 0;
            this.SpreadType.SiteControl = false;
            this.SpreadType.SiteFieldName = null;
            this.SpreadType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CoursePlanDetails
            // 
            this.CoursePlanDetails.CacheConnection = false;
            this.CoursePlanDetails.CommandText = resources.GetString("CoursePlanDetails.CommandText");
            this.CoursePlanDetails.CommandTimeout = 30;
            this.CoursePlanDetails.CommandType = System.Data.CommandType.Text;
            this.CoursePlanDetails.DynamicTableName = false;
            this.CoursePlanDetails.EEPAlias = null;
            this.CoursePlanDetails.EncodingAfter = null;
            this.CoursePlanDetails.EncodingBefore = "Windows-1252";
            this.CoursePlanDetails.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.CoursePlanDetails.KeyFields.Add(keyItem3);
            this.CoursePlanDetails.MultiSetWhere = false;
            this.CoursePlanDetails.Name = "CoursePlanDetails";
            this.CoursePlanDetails.NotificationAutoEnlist = false;
            this.CoursePlanDetails.SecExcept = null;
            this.CoursePlanDetails.SecFieldName = null;
            this.CoursePlanDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CoursePlanDetails.SelectPaging = false;
            this.CoursePlanDetails.SelectTop = 0;
            this.CoursePlanDetails.SiteControl = false;
            this.CoursePlanDetails.SiteFieldName = null;
            this.CoursePlanDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCoursePlanDetails
            // 
            this.ucCoursePlanDetails.AutoTrans = true;
            this.ucCoursePlanDetails.ExceptJoin = false;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "AutoKey";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "ParentKey";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "PlanID";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CourseNO";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "ClassName";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "IsClass";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "IsUserDefine";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "StudentID";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "JobID";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "DeptID";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "PlanDeptIDs";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "StartDate1";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "StartDate2";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "SpreadType";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "OpenSeason";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "CreateBy";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = "_usercode";
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
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr30.DefaultValue = "_usercode";
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
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr13);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr14);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr15);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr16);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr17);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr18);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr19);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr20);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr21);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr22);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr23);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr24);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr25);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr26);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr27);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr28);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr29);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr30);
            this.ucCoursePlanDetails.FieldAttrs.Add(fieldAttr31);
            this.ucCoursePlanDetails.LogInfo = null;
            this.ucCoursePlanDetails.Name = "ucCoursePlanDetails";
            this.ucCoursePlanDetails.RowAffectsCheck = true;
            this.ucCoursePlanDetails.SelectCmd = this.CoursePlanDetails;
            this.ucCoursePlanDetails.SelectCmdForUpdate = null;
            this.ucCoursePlanDetails.ServerModify = true;
            this.ucCoursePlanDetails.ServerModifyGetMax = false;
            this.ucCoursePlanDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCoursePlanDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCoursePlanDetails.UseTranscationScope = false;
            this.ucCoursePlanDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCoursePlanDetails.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCoursePlanDetails_BeforeInsert);
            this.ucCoursePlanDetails.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCoursePlanDetails_BeforeModify);
            // 
            // OpenSeason
            // 
            this.OpenSeason.CacheConnection = false;
            this.OpenSeason.CommandText = "SELECT OPENSEASONNAME AS Season,\r\n              StartDate,\r\n              EndDate" +
    "\r\nFROM LISTOPENSEASON\r\nORDER BY OPENSEASONID";
            this.OpenSeason.CommandTimeout = 30;
            this.OpenSeason.CommandType = System.Data.CommandType.Text;
            this.OpenSeason.DynamicTableName = false;
            this.OpenSeason.EEPAlias = null;
            this.OpenSeason.EncodingAfter = null;
            this.OpenSeason.EncodingBefore = "Windows-1252";
            this.OpenSeason.InfoConnection = this.InfoConnection1;
            this.OpenSeason.MultiSetWhere = false;
            this.OpenSeason.Name = "OpenSeason";
            this.OpenSeason.NotificationAutoEnlist = false;
            this.OpenSeason.SecExcept = null;
            this.OpenSeason.SecFieldName = null;
            this.OpenSeason.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.OpenSeason.SelectPaging = false;
            this.OpenSeason.SelectTop = 0;
            this.OpenSeason.SiteControl = false;
            this.OpenSeason.SiteFieldName = null;
            this.OpenSeason.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CourseStudentList
            // 
            this.CourseStudentList.CacheConnection = false;
            this.CourseStudentList.CommandText = "SELECT cp.*  FROM CoursePlanStudents cp\r\n";
            this.CourseStudentList.CommandTimeout = 30;
            this.CourseStudentList.CommandType = System.Data.CommandType.Text;
            this.CourseStudentList.DynamicTableName = false;
            this.CourseStudentList.EEPAlias = null;
            this.CourseStudentList.EncodingAfter = null;
            this.CourseStudentList.EncodingBefore = "Windows-1252";
            this.CourseStudentList.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AutoKey";
            this.CourseStudentList.KeyFields.Add(keyItem4);
            this.CourseStudentList.MultiSetWhere = false;
            this.CourseStudentList.Name = "CourseStudentList";
            this.CourseStudentList.NotificationAutoEnlist = false;
            this.CourseStudentList.SecExcept = null;
            this.CourseStudentList.SecFieldName = null;
            this.CourseStudentList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CourseStudentList.SelectPaging = false;
            this.CourseStudentList.SelectTop = 0;
            this.CourseStudentList.SiteControl = false;
            this.CourseStudentList.SiteFieldName = null;
            this.CourseStudentList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PlanType
            // 
            this.PlanType.CacheConnection = false;
            this.PlanType.CommandText = "SELECT * FROM LISTPLANTYPE\r\nORDER BY ID";
            this.PlanType.CommandTimeout = 30;
            this.PlanType.CommandType = System.Data.CommandType.Text;
            this.PlanType.DynamicTableName = false;
            this.PlanType.EEPAlias = null;
            this.PlanType.EncodingAfter = null;
            this.PlanType.EncodingBefore = "Windows-1252";
            this.PlanType.InfoConnection = this.InfoConnection1;
            this.PlanType.MultiSetWhere = false;
            this.PlanType.Name = "PlanType";
            this.PlanType.NotificationAutoEnlist = false;
            this.PlanType.SecExcept = null;
            this.PlanType.SecFieldName = null;
            this.PlanType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PlanType.SelectPaging = false;
            this.PlanType.SelectTop = 0;
            this.PlanType.SiteControl = false;
            this.PlanType.SiteFieldName = null;
            this.PlanType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // UserInfo
            // 
            this.UserInfo.CacheConnection = false;
            this.UserInfo.CommandText = "SELECT USERID,USERNAME FROM EIPHRSYS.DBO.USERS\r\nORDER BY USERID";
            this.UserInfo.CommandTimeout = 30;
            this.UserInfo.CommandType = System.Data.CommandType.Text;
            this.UserInfo.DynamicTableName = false;
            this.UserInfo.EEPAlias = null;
            this.UserInfo.EncodingAfter = null;
            this.UserInfo.EncodingBefore = "Windows-1252";
            this.UserInfo.InfoConnection = this.InfoConnection1;
            this.UserInfo.MultiSetWhere = false;
            this.UserInfo.Name = "UserInfo";
            this.UserInfo.NotificationAutoEnlist = false;
            this.UserInfo.SecExcept = null;
            this.UserInfo.SecFieldName = null;
            this.UserInfo.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UserInfo.SelectPaging = false;
            this.UserInfo.SelectTop = 0;
            this.UserInfo.SiteControl = false;
            this.UserInfo.SiteFieldName = null;
            this.UserInfo.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CourseList
            // 
            this.CourseList.CacheConnection = false;
            this.CourseList.CommandText = "SELECT TOP 60 CourseID,CourseName  \r\nFrom Course Where  IsClass=0\r\n";
            this.CourseList.CommandTimeout = 30;
            this.CourseList.CommandType = System.Data.CommandType.Text;
            this.CourseList.DynamicTableName = false;
            this.CourseList.EEPAlias = null;
            this.CourseList.EncodingAfter = null;
            this.CourseList.EncodingBefore = "Windows-1252";
            this.CourseList.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "CourseID";
            this.CourseList.KeyFields.Add(keyItem5);
            this.CourseList.MultiSetWhere = false;
            this.CourseList.Name = "CourseList";
            this.CourseList.NotificationAutoEnlist = false;
            this.CourseList.SecExcept = null;
            this.CourseList.SecFieldName = null;
            this.CourseList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CourseList.SelectPaging = false;
            this.CourseList.SelectTop = 0;
            this.CourseList.SiteControl = false;
            this.CourseList.SiteFieldName = null;
            this.CourseList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoursePlanRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CoursePlanRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpreadType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoursePlanDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpenSeason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseStudentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseList)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CoursePlanRecord;
        private Srvtools.UpdateComponent ucCoursePlanRecord;
        private Srvtools.InfoCommand View_CoursePlanRecord;
        private Srvtools.InfoCommand YearNO;
        private Srvtools.InfoCommand SpreadType;
        private Srvtools.InfoCommand CoursePlanDetails;
        private Srvtools.UpdateComponent ucCoursePlanDetails;
        private Srvtools.InfoCommand OpenSeason;
        private Srvtools.InfoCommand CourseStudentList;
        private Srvtools.InfoCommand PlanType;
        private Srvtools.InfoCommand UserInfo;
        private Srvtools.InfoCommand CourseList;
    }
}
