namespace sIssueJob
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.Service service1 = new Srvtools.Service();
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.IssueJob = new Srvtools.InfoCommand(this.components);
            this.ucIssueJob = new Srvtools.UpdateComponent(this.components);
            this.IssueBelong = new Srvtools.InfoCommand(this.components);
            this.ucIssueBelong = new Srvtools.UpdateComponent(this.components);
            this.IssueType = new Srvtools.InfoCommand(this.components);
            this.ucIssueType = new Srvtools.UpdateComponent(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.autoNumber2 = new Srvtools.AutoNumber(this.components);
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.autoIssueJobNO = new Srvtools.AutoNumber(this.components);
            this.GROUPS = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.ORG = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueBelong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ORG)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // IssueJob
            // 
            this.IssueJob.CacheConnection = false;
            this.IssueJob.CommandText = "SELECT dbo.[IssueJob].* FROM dbo.[IssueJob]";
            this.IssueJob.CommandTimeout = 30;
            this.IssueJob.CommandType = System.Data.CommandType.Text;
            this.IssueJob.DynamicTableName = false;
            this.IssueJob.EEPAlias = "JBADMIN";
            this.IssueJob.EncodingAfter = null;
            this.IssueJob.EncodingBefore = "Windows-1252";
            this.IssueJob.EncodingConvert = null;
            this.IssueJob.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "IssueJobNO";
            this.IssueJob.KeyFields.Add(keyItem1);
            this.IssueJob.MultiSetWhere = false;
            this.IssueJob.Name = "IssueJob";
            this.IssueJob.NotificationAutoEnlist = false;
            this.IssueJob.SecExcept = null;
            this.IssueJob.SecFieldName = null;
            this.IssueJob.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.IssueJob.SelectPaging = false;
            this.IssueJob.SelectTop = 0;
            this.IssueJob.SiteControl = false;
            this.IssueJob.SiteFieldName = null;
            this.IssueJob.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucIssueJob
            // 
            this.ucIssueJob.AutoTrans = true;
            this.ucIssueJob.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "IssueJobNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "IssueJobDate";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "IssueBelongID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "IssueTypeID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "IssueDescr";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Flowflag";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "EmployeeID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = "_username";
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
            fieldAttr10.DataField = "IsTransfer";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Cost";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "UrgentLevel";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "ORG_NO";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucIssueJob.FieldAttrs.Add(fieldAttr1);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr2);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr3);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr4);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr5);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr6);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr7);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr8);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr9);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr10);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr11);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr12);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr13);
            this.ucIssueJob.LogInfo = null;
            this.ucIssueJob.Name = "ucIssueJob";
            this.ucIssueJob.RowAffectsCheck = true;
            this.ucIssueJob.SelectCmd = this.IssueJob;
            this.ucIssueJob.SelectCmdForUpdate = null;
            this.ucIssueJob.SendSQLCmd = true;
            this.ucIssueJob.ServerModify = true;
            this.ucIssueJob.ServerModifyGetMax = false;
            this.ucIssueJob.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucIssueJob.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucIssueJob.UseTranscationScope = false;
            this.ucIssueJob.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucIssueJob.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucIssueJob_BeforeInsert);
            // 
            // IssueBelong
            // 
            this.IssueBelong.CacheConnection = false;
            this.IssueBelong.CommandText = "SELECT dbo.[IssueBelong].* FROM dbo.[IssueBelong]";
            this.IssueBelong.CommandTimeout = 30;
            this.IssueBelong.CommandType = System.Data.CommandType.Text;
            this.IssueBelong.DynamicTableName = false;
            this.IssueBelong.EEPAlias = "JBADMIN";
            this.IssueBelong.EncodingAfter = null;
            this.IssueBelong.EncodingBefore = "Windows-1252";
            this.IssueBelong.EncodingConvert = null;
            this.IssueBelong.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "IssueBelongID";
            this.IssueBelong.KeyFields.Add(keyItem2);
            this.IssueBelong.MultiSetWhere = false;
            this.IssueBelong.Name = "IssueBelong";
            this.IssueBelong.NotificationAutoEnlist = false;
            this.IssueBelong.SecExcept = null;
            this.IssueBelong.SecFieldName = null;
            this.IssueBelong.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.IssueBelong.SelectPaging = false;
            this.IssueBelong.SelectTop = 0;
            this.IssueBelong.SiteControl = false;
            this.IssueBelong.SiteFieldName = null;
            this.IssueBelong.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucIssueBelong
            // 
            this.ucIssueBelong.AutoTrans = true;
            this.ucIssueBelong.ExceptJoin = false;
            this.ucIssueBelong.LogInfo = null;
            this.ucIssueBelong.Name = "ucIssueBelong";
            this.ucIssueBelong.RowAffectsCheck = true;
            this.ucIssueBelong.SelectCmd = this.IssueBelong;
            this.ucIssueBelong.SelectCmdForUpdate = null;
            this.ucIssueBelong.SendSQLCmd = true;
            this.ucIssueBelong.ServerModify = true;
            this.ucIssueBelong.ServerModifyGetMax = false;
            this.ucIssueBelong.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucIssueBelong.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucIssueBelong.UseTranscationScope = false;
            this.ucIssueBelong.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // IssueType
            // 
            this.IssueType.CacheConnection = false;
            this.IssueType.CommandText = "SELECT dbo.[IssueType].* FROM dbo.[IssueType]";
            this.IssueType.CommandTimeout = 30;
            this.IssueType.CommandType = System.Data.CommandType.Text;
            this.IssueType.DynamicTableName = false;
            this.IssueType.EEPAlias = "JBADMIN";
            this.IssueType.EncodingAfter = null;
            this.IssueType.EncodingBefore = "Windows-1252";
            this.IssueType.EncodingConvert = null;
            this.IssueType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "IssueTypeID";
            this.IssueType.KeyFields.Add(keyItem3);
            this.IssueType.MultiSetWhere = false;
            this.IssueType.Name = "IssueType";
            this.IssueType.NotificationAutoEnlist = false;
            this.IssueType.SecExcept = null;
            this.IssueType.SecFieldName = null;
            this.IssueType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.IssueType.SelectPaging = false;
            this.IssueType.SelectTop = 0;
            this.IssueType.SiteControl = false;
            this.IssueType.SiteFieldName = null;
            this.IssueType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucIssueType
            // 
            this.ucIssueType.AutoTrans = true;
            this.ucIssueType.ExceptJoin = false;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "IssueTypeID";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "IssueBelongID";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "IssueTypeName";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CreateBy";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "CreateDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.ucIssueType.FieldAttrs.Add(fieldAttr14);
            this.ucIssueType.FieldAttrs.Add(fieldAttr15);
            this.ucIssueType.FieldAttrs.Add(fieldAttr16);
            this.ucIssueType.FieldAttrs.Add(fieldAttr17);
            this.ucIssueType.FieldAttrs.Add(fieldAttr18);
            this.ucIssueType.LogInfo = null;
            this.ucIssueType.Name = "ucIssueType";
            this.ucIssueType.RowAffectsCheck = true;
            this.ucIssueType.SelectCmd = this.IssueType;
            this.ucIssueType.SelectCmdForUpdate = null;
            this.ucIssueType.SendSQLCmd = true;
            this.ucIssueType.ServerModify = true;
            this.ucIssueType.ServerModifyGetMax = false;
            this.ucIssueType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucIssueType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucIssueType.UseTranscationScope = false;
            this.ucIssueType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "IssueBelongID";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "";
            this.autoNumber1.isNumFill = true;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 1;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "IssueBelongID";
            this.autoNumber1.UpdateComp = this.ucIssueBelong;
            // 
            // autoNumber2
            // 
            this.autoNumber2.Active = true;
            this.autoNumber2.AutoNoID = "IssueTypeID";
            this.autoNumber2.Description = null;
            this.autoNumber2.GetFixed = "";
            this.autoNumber2.isNumFill = true;
            this.autoNumber2.Name = "autoNumber2";
            this.autoNumber2.Number = null;
            this.autoNumber2.NumDig = 1;
            this.autoNumber2.OldVersion = false;
            this.autoNumber2.OverFlow = true;
            this.autoNumber2.StartValue = 1;
            this.autoNumber2.Step = 1;
            this.autoNumber2.TargetColumn = "IssueTypeID";
            this.autoNumber2.UpdateComp = this.ucIssueType;
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckMasterDelete";
            service1.NonLogin = false;
            service1.ServiceName = "CheckMasterDelete";
            service2.DelegateName = "CheckDetailDelete";
            service2.NonLogin = false;
            service2.ServiceName = "CheckDetailDelete";
            service3.DelegateName = "CheckCloseDate";
            service3.NonLogin = false;
            service3.ServiceName = "CheckCloseDate";
            service4.DelegateName = "GetUserOrgNOs";
            service4.NonLogin = false;
            service4.ServiceName = "GetUserOrgNOs";
            service5.DelegateName = "Call_funReturnEmpBossID";
            service5.NonLogin = false;
            service5.ServiceName = "Call_funReturnEmpBossID";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            // 
            // autoIssueJobNO
            // 
            this.autoIssueJobNO.Active = true;
            this.autoIssueJobNO.AutoNoID = "IssueJobNO";
            this.autoIssueJobNO.Description = null;
            this.autoIssueJobNO.GetFixed = "GetIssueJobFixed()";
            this.autoIssueJobNO.isNumFill = false;
            this.autoIssueJobNO.Name = "autoIssueJobNO";
            this.autoIssueJobNO.Number = null;
            this.autoIssueJobNO.NumDig = 4;
            this.autoIssueJobNO.OldVersion = false;
            this.autoIssueJobNO.OverFlow = true;
            this.autoIssueJobNO.StartValue = 1;
            this.autoIssueJobNO.Step = 1;
            this.autoIssueJobNO.TargetColumn = "IssueJobNO";
            this.autoIssueJobNO.UpdateComp = this.ucIssueJob;
            // 
            // GROUPS
            // 
            this.GROUPS.CacheConnection = false;
            this.GROUPS.CommandText = resources.GetString("GROUPS.CommandText");
            this.GROUPS.CommandTimeout = 30;
            this.GROUPS.CommandType = System.Data.CommandType.Text;
            this.GROUPS.DynamicTableName = false;
            this.GROUPS.EEPAlias = "EIPHRSYS";
            this.GROUPS.EncodingAfter = null;
            this.GROUPS.EncodingBefore = "Windows-1252";
            this.GROUPS.EncodingConvert = null;
            this.GROUPS.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "GROUPID";
            this.GROUPS.KeyFields.Add(keyItem4);
            this.GROUPS.MultiSetWhere = false;
            this.GROUPS.Name = "GROUPS";
            this.GROUPS.NotificationAutoEnlist = false;
            this.GROUPS.SecExcept = null;
            this.GROUPS.SecFieldName = null;
            this.GROUPS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.GROUPS.SelectPaging = false;
            this.GROUPS.SelectTop = 0;
            this.GROUPS.SiteControl = false;
            this.GROUPS.SiteFieldName = null;
            this.GROUPS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = resources.GetString("Employee.CommandText");
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem5);
            this.Employee.MultiSetWhere = false;
            this.Employee.Name = "Employee";
            this.Employee.NotificationAutoEnlist = false;
            this.Employee.SecExcept = null;
            this.Employee.SecFieldName = null;
            this.Employee.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Employee.SelectPaging = false;
            this.Employee.SelectTop = 0;
            this.Employee.SiteControl = false;
            this.Employee.SiteFieldName = null;
            this.Employee.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ORG
            // 
            this.ORG.CacheConnection = false;
            this.ORG.CommandText = resources.GetString("ORG.CommandText");
            this.ORG.CommandTimeout = 30;
            this.ORG.CommandType = System.Data.CommandType.Text;
            this.ORG.DynamicTableName = false;
            this.ORG.EEPAlias = "EIPHRSYS";
            this.ORG.EncodingAfter = null;
            this.ORG.EncodingBefore = "Windows-1252";
            this.ORG.EncodingConvert = null;
            this.ORG.InfoConnection = this.InfoConnection1;
            this.ORG.MultiSetWhere = false;
            this.ORG.Name = "ORG";
            this.ORG.NotificationAutoEnlist = false;
            this.ORG.SecExcept = null;
            this.ORG.SecFieldName = null;
            this.ORG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ORG.SelectPaging = false;
            this.ORG.SelectTop = 0;
            this.ORG.SiteControl = false;
            this.ORG.SiteFieldName = null;
            this.ORG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueBelong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ORG)).EndInit();

        }

        #endregion

        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand IssueJob;
        private Srvtools.UpdateComponent ucIssueJob;
        private Srvtools.InfoCommand IssueBelong;
        private Srvtools.UpdateComponent ucIssueBelong;
        private Srvtools.InfoCommand IssueType;
        private Srvtools.UpdateComponent ucIssueType;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.AutoNumber autoNumber2;
        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.AutoNumber autoIssueJobNO;
        private Srvtools.InfoCommand GROUPS;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand ORG;
    }
}
