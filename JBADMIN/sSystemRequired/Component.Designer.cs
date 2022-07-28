namespace sSystemRequired
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem9 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SystemRequired = new Srvtools.InfoCommand(this.components);
            this.ucSystemRequired = new Srvtools.UpdateComponent(this.components);
            this.View_SystemRequired = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.SystemType = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            this.GroupMang = new Srvtools.InfoCommand(this.components);
            this.ProjectLeader = new Srvtools.InfoCommand(this.components);
            this.autoSysRequiredNo = new Srvtools.AutoNumber(this.components);
            this.DevelopTechnology = new Srvtools.InfoCommand(this.components);
            this.EvaluationResult = new Srvtools.InfoCommand(this.components);
            this.Checker = new Srvtools.InfoCommand(this.components);
            this.UsersGROUPS = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SystemRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SystemRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SystemType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupMang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectLeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevelopTechnology)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EvaluationResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Checker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGROUPS)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetUserOrgNOs";
            service1.NonLogin = false;
            service1.ServiceName = "GetUserOrgNOs";
            service2.DelegateName = "NoFixed";
            service2.NonLogin = false;
            service2.ServiceName = "NoFixed";
            service3.DelegateName = "GetOrgMang";
            service3.NonLogin = false;
            service3.ServiceName = "GetOrgMang";
            service4.DelegateName = "GetOrgMangID";
            service4.NonLogin = false;
            service4.ServiceName = "GetOrgMangID";
            service5.DelegateName = "PresidentSign";
            service5.NonLogin = false;
            service5.ServiceName = "PresidentSign";
            service6.DelegateName = "GetCheckId";
            service6.NonLogin = false;
            service6.ServiceName = "GetCheckId";
            service7.DelegateName = "GetSystemRequired";
            service7.NonLogin = false;
            service7.ServiceName = "GetSystemRequired";
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
            // SystemRequired
            // 
            this.SystemRequired.CacheConnection = false;
            this.SystemRequired.CommandText = "SELECT dbo.[SystemRequired].* FROM dbo.[SystemRequired]";
            this.SystemRequired.CommandTimeout = 30;
            this.SystemRequired.CommandType = System.Data.CommandType.Text;
            this.SystemRequired.DynamicTableName = false;
            this.SystemRequired.EEPAlias = null;
            this.SystemRequired.EncodingAfter = null;
            this.SystemRequired.EncodingBefore = "Windows-1252";
            this.SystemRequired.EncodingConvert = null;
            this.SystemRequired.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SysRequiredNo";
            this.SystemRequired.KeyFields.Add(keyItem1);
            this.SystemRequired.MultiSetWhere = false;
            this.SystemRequired.Name = "SystemRequired";
            this.SystemRequired.NotificationAutoEnlist = false;
            this.SystemRequired.SecExcept = null;
            this.SystemRequired.SecFieldName = null;
            this.SystemRequired.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SystemRequired.SelectPaging = false;
            this.SystemRequired.SelectTop = 0;
            this.SystemRequired.SiteControl = false;
            this.SystemRequired.SiteFieldName = null;
            this.SystemRequired.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSystemRequired
            // 
            this.ucSystemRequired.AutoTrans = true;
            this.ucSystemRequired.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SysRequiredNo";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ApplyDate";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ApplyEmpID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ApplyEmpName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ApplyOrg_NO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "RequiredOrg_NO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "NewProject";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Description";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Attachment1";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Attachment2";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Attachment3";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "ProjectLeader";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "EstimatedDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CompledDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "Checker";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CheckDescr";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CheckDate";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "CreateBy";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "CreateDate";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "flowflag";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr1);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr2);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr3);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr4);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr5);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr6);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr7);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr8);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr9);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr10);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr11);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr12);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr13);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr14);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr15);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr16);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr17);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr18);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr19);
            this.ucSystemRequired.FieldAttrs.Add(fieldAttr20);
            this.ucSystemRequired.LogInfo = null;
            this.ucSystemRequired.Name = "ucSystemRequired";
            this.ucSystemRequired.RowAffectsCheck = true;
            this.ucSystemRequired.SelectCmd = this.SystemRequired;
            this.ucSystemRequired.SelectCmdForUpdate = null;
            this.ucSystemRequired.SendSQLCmd = true;
            this.ucSystemRequired.ServerModify = true;
            this.ucSystemRequired.ServerModifyGetMax = false;
            this.ucSystemRequired.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSystemRequired.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSystemRequired.UseTranscationScope = false;
            this.ucSystemRequired.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucSystemRequired.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucSystemRequiredBeforeInsert);
            // 
            // View_SystemRequired
            // 
            this.View_SystemRequired.CacheConnection = false;
            this.View_SystemRequired.CommandText = "SELECT * FROM dbo.[SystemRequired]";
            this.View_SystemRequired.CommandTimeout = 30;
            this.View_SystemRequired.CommandType = System.Data.CommandType.Text;
            this.View_SystemRequired.DynamicTableName = false;
            this.View_SystemRequired.EEPAlias = null;
            this.View_SystemRequired.EncodingAfter = null;
            this.View_SystemRequired.EncodingBefore = "Windows-1252";
            this.View_SystemRequired.EncodingConvert = null;
            this.View_SystemRequired.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SysRequiredNo";
            this.View_SystemRequired.KeyFields.Add(keyItem2);
            this.View_SystemRequired.MultiSetWhere = false;
            this.View_SystemRequired.Name = "View_SystemRequired";
            this.View_SystemRequired.NotificationAutoEnlist = false;
            this.View_SystemRequired.SecExcept = null;
            this.View_SystemRequired.SecFieldName = null;
            this.View_SystemRequired.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SystemRequired.SelectPaging = false;
            this.View_SystemRequired.SelectTop = 0;
            this.View_SystemRequired.SiteControl = false;
            this.View_SystemRequired.SiteFieldName = null;
            this.View_SystemRequired.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            keyItem3.KeyName = "EMPLOYEEID";
            this.Employee.KeyFields.Add(keyItem3);
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
            // SystemType
            // 
            this.SystemType.CacheConnection = false;
            this.SystemType.CommandText = "select Code,CodeNmae from CodeFile  where TableName=\'SystemType\' and UseState=\'1\'" +
    "";
            this.SystemType.CommandTimeout = 30;
            this.SystemType.CommandType = System.Data.CommandType.Text;
            this.SystemType.DynamicTableName = false;
            this.SystemType.EEPAlias = null;
            this.SystemType.EncodingAfter = null;
            this.SystemType.EncodingBefore = "Windows-1252";
            this.SystemType.EncodingConvert = null;
            this.SystemType.InfoConnection = this.InfoConnection1;
            this.SystemType.MultiSetWhere = false;
            this.SystemType.Name = "SystemType";
            this.SystemType.NotificationAutoEnlist = false;
            this.SystemType.SecExcept = null;
            this.SystemType.SecFieldName = null;
            this.SystemType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SystemType.SelectPaging = false;
            this.SystemType.SelectTop = 0;
            this.SystemType.SiteControl = false;
            this.SystemType.SiteFieldName = null;
            this.SystemType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Organization
            // 
            this.Organization.CacheConnection = false;
            this.Organization.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nWHERE (Uppe" +
    "r_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR ORG_NO=\'99999\')\r\nORDER" +
    " BY ORG_NO";
            this.Organization.CommandTimeout = 30;
            this.Organization.CommandType = System.Data.CommandType.Text;
            this.Organization.DynamicTableName = false;
            this.Organization.EEPAlias = null;
            this.Organization.EncodingAfter = null;
            this.Organization.EncodingBefore = "Windows-1252";
            this.Organization.EncodingConvert = null;
            this.Organization.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem4);
            this.Organization.MultiSetWhere = false;
            this.Organization.Name = "Organization";
            this.Organization.NotificationAutoEnlist = false;
            this.Organization.SecExcept = null;
            this.Organization.SecFieldName = null;
            this.Organization.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Organization.SelectPaging = false;
            this.Organization.SelectTop = 0;
            this.Organization.SiteControl = false;
            this.Organization.SiteFieldName = null;
            this.Organization.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // GroupMang
            // 
            this.GroupMang.CacheConnection = false;
            this.GroupMang.CommandText = "SELECT * FROM View_UsersGROUPS WHERE  1=0";
            this.GroupMang.CommandTimeout = 30;
            this.GroupMang.CommandType = System.Data.CommandType.Text;
            this.GroupMang.DynamicTableName = false;
            this.GroupMang.EEPAlias = null;
            this.GroupMang.EncodingAfter = null;
            this.GroupMang.EncodingBefore = "Windows-1252";
            this.GroupMang.EncodingConvert = null;
            this.GroupMang.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "USERID";
            keyItem6.KeyName = "GROUPID";
            this.GroupMang.KeyFields.Add(keyItem5);
            this.GroupMang.KeyFields.Add(keyItem6);
            this.GroupMang.MultiSetWhere = false;
            this.GroupMang.Name = "GroupMang";
            this.GroupMang.NotificationAutoEnlist = false;
            this.GroupMang.SecExcept = null;
            this.GroupMang.SecFieldName = null;
            this.GroupMang.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.GroupMang.SelectPaging = false;
            this.GroupMang.SelectTop = 0;
            this.GroupMang.SiteControl = false;
            this.GroupMang.SiteFieldName = null;
            this.GroupMang.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ProjectLeader
            // 
            this.ProjectLeader.CacheConnection = false;
            this.ProjectLeader.CommandText = resources.GetString("ProjectLeader.CommandText");
            this.ProjectLeader.CommandTimeout = 30;
            this.ProjectLeader.CommandType = System.Data.CommandType.Text;
            this.ProjectLeader.DynamicTableName = false;
            this.ProjectLeader.EEPAlias = null;
            this.ProjectLeader.EncodingAfter = null;
            this.ProjectLeader.EncodingBefore = "Windows-1252";
            this.ProjectLeader.EncodingConvert = null;
            this.ProjectLeader.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "EMPLOYEEID";
            this.ProjectLeader.KeyFields.Add(keyItem7);
            this.ProjectLeader.MultiSetWhere = false;
            this.ProjectLeader.Name = "ProjectLeader";
            this.ProjectLeader.NotificationAutoEnlist = false;
            this.ProjectLeader.SecExcept = null;
            this.ProjectLeader.SecFieldName = null;
            this.ProjectLeader.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ProjectLeader.SelectPaging = false;
            this.ProjectLeader.SelectTop = 0;
            this.ProjectLeader.SiteControl = false;
            this.ProjectLeader.SiteFieldName = null;
            this.ProjectLeader.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoSysRequiredNo
            // 
            this.autoSysRequiredNo.Active = true;
            this.autoSysRequiredNo.AutoNoID = "autoSysRequiredNo";
            this.autoSysRequiredNo.Description = null;
            this.autoSysRequiredNo.GetFixed = "NoFixed()";
            this.autoSysRequiredNo.isNumFill = false;
            this.autoSysRequiredNo.Name = "autoSysRequiredNo";
            this.autoSysRequiredNo.Number = null;
            this.autoSysRequiredNo.NumDig = 4;
            this.autoSysRequiredNo.OldVersion = false;
            this.autoSysRequiredNo.OverFlow = true;
            this.autoSysRequiredNo.StartValue = 1;
            this.autoSysRequiredNo.Step = 1;
            this.autoSysRequiredNo.TargetColumn = "SysRequiredNo";
            this.autoSysRequiredNo.UpdateComp = this.ucSystemRequired;
            // 
            // DevelopTechnology
            // 
            this.DevelopTechnology.CacheConnection = false;
            this.DevelopTechnology.CommandText = "select Code,CodeNmae from CodeFile  where TableName=\'DevelopTechnology\' and UseSt" +
    "ate=\'1\'";
            this.DevelopTechnology.CommandTimeout = 30;
            this.DevelopTechnology.CommandType = System.Data.CommandType.Text;
            this.DevelopTechnology.DynamicTableName = false;
            this.DevelopTechnology.EEPAlias = null;
            this.DevelopTechnology.EncodingAfter = null;
            this.DevelopTechnology.EncodingBefore = "Windows-1252";
            this.DevelopTechnology.EncodingConvert = null;
            this.DevelopTechnology.InfoConnection = this.InfoConnection1;
            this.DevelopTechnology.MultiSetWhere = false;
            this.DevelopTechnology.Name = "DevelopTechnology";
            this.DevelopTechnology.NotificationAutoEnlist = false;
            this.DevelopTechnology.SecExcept = null;
            this.DevelopTechnology.SecFieldName = null;
            this.DevelopTechnology.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DevelopTechnology.SelectPaging = false;
            this.DevelopTechnology.SelectTop = 0;
            this.DevelopTechnology.SiteControl = false;
            this.DevelopTechnology.SiteFieldName = null;
            this.DevelopTechnology.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // EvaluationResult
            // 
            this.EvaluationResult.CacheConnection = false;
            this.EvaluationResult.CommandText = "select Code,CodeNmae from CodeFile  where TableName=\'EvaluationResult\' and UseSta" +
    "te=\'1\'";
            this.EvaluationResult.CommandTimeout = 30;
            this.EvaluationResult.CommandType = System.Data.CommandType.Text;
            this.EvaluationResult.DynamicTableName = false;
            this.EvaluationResult.EEPAlias = null;
            this.EvaluationResult.EncodingAfter = null;
            this.EvaluationResult.EncodingBefore = "Windows-1252";
            this.EvaluationResult.EncodingConvert = null;
            this.EvaluationResult.InfoConnection = this.InfoConnection1;
            this.EvaluationResult.MultiSetWhere = false;
            this.EvaluationResult.Name = "EvaluationResult";
            this.EvaluationResult.NotificationAutoEnlist = false;
            this.EvaluationResult.SecExcept = null;
            this.EvaluationResult.SecFieldName = null;
            this.EvaluationResult.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EvaluationResult.SelectPaging = false;
            this.EvaluationResult.SelectTop = 0;
            this.EvaluationResult.SiteControl = false;
            this.EvaluationResult.SiteFieldName = null;
            this.EvaluationResult.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Checker
            // 
            this.Checker.CacheConnection = false;
            this.Checker.CommandText = resources.GetString("Checker.CommandText");
            this.Checker.CommandTimeout = 30;
            this.Checker.CommandType = System.Data.CommandType.Text;
            this.Checker.DynamicTableName = false;
            this.Checker.EEPAlias = null;
            this.Checker.EncodingAfter = null;
            this.Checker.EncodingBefore = "Windows-1252";
            this.Checker.EncodingConvert = null;
            this.Checker.InfoConnection = this.InfoConnection1;
            this.Checker.MultiSetWhere = false;
            this.Checker.Name = "Checker";
            this.Checker.NotificationAutoEnlist = false;
            this.Checker.SecExcept = null;
            this.Checker.SecFieldName = null;
            this.Checker.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Checker.SelectPaging = false;
            this.Checker.SelectTop = 0;
            this.Checker.SiteControl = false;
            this.Checker.SiteFieldName = null;
            this.Checker.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // UsersGROUPS
            // 
            this.UsersGROUPS.CacheConnection = false;
            this.UsersGROUPS.CommandText = resources.GetString("UsersGROUPS.CommandText");
            this.UsersGROUPS.CommandTimeout = 30;
            this.UsersGROUPS.CommandType = System.Data.CommandType.Text;
            this.UsersGROUPS.DynamicTableName = false;
            this.UsersGROUPS.EEPAlias = null;
            this.UsersGROUPS.EncodingAfter = null;
            this.UsersGROUPS.EncodingBefore = "Windows-1252";
            this.UsersGROUPS.EncodingConvert = null;
            this.UsersGROUPS.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "USERID";
            keyItem9.KeyName = "GroupID";
            this.UsersGROUPS.KeyFields.Add(keyItem8);
            this.UsersGROUPS.KeyFields.Add(keyItem9);
            this.UsersGROUPS.MultiSetWhere = false;
            this.UsersGROUPS.Name = "UsersGROUPS";
            this.UsersGROUPS.NotificationAutoEnlist = false;
            this.UsersGROUPS.SecExcept = null;
            this.UsersGROUPS.SecFieldName = null;
            this.UsersGROUPS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UsersGROUPS.SelectPaging = false;
            this.UsersGROUPS.SelectTop = 0;
            this.UsersGROUPS.SiteControl = false;
            this.UsersGROUPS.SiteFieldName = null;
            this.UsersGROUPS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SystemRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SystemRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SystemType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupMang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectLeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevelopTechnology)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EvaluationResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Checker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGROUPS)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SystemRequired;
        private Srvtools.UpdateComponent ucSystemRequired;
        private Srvtools.InfoCommand View_SystemRequired;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand SystemType;
        private Srvtools.InfoCommand Organization;
        private Srvtools.InfoCommand GroupMang;
        private Srvtools.InfoCommand ProjectLeader;
        private Srvtools.AutoNumber autoSysRequiredNo;
        private Srvtools.InfoCommand DevelopTechnology;
        private Srvtools.InfoCommand EvaluationResult;
        private Srvtools.InfoCommand Checker;
        private Srvtools.InfoCommand UsersGROUPS;
    }
}
