namespace sIssueJobQuery
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.IssueJob = new Srvtools.InfoCommand(this.components);
            this.ucIssueJob = new Srvtools.UpdateComponent(this.components);
            this.View_IssueJob = new Srvtools.InfoCommand(this.components);
            this.GROUPS = new Srvtools.InfoCommand(this.components);
            this.USERS = new Srvtools.InfoCommand(this.components);
            this.Applier = new Srvtools.InfoCommand(this.components);
            this.ORG = new Srvtools.InfoCommand(this.components);
            this.IssueType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_IssueJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Applier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ORG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueType)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // IssueJob
            // 
            this.IssueJob.CacheConnection = false;
            this.IssueJob.CommandText = resources.GetString("IssueJob.CommandText");
            this.IssueJob.CommandTimeout = 30;
            this.IssueJob.CommandType = System.Data.CommandType.Text;
            this.IssueJob.DynamicTableName = false;
            this.IssueJob.EEPAlias = null;
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
            fieldAttr10.DataField = "EstimationDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CheckDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CheckDescr";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CloseDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CloseDescr";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CheckScore";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "ServeEmployeeID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
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
            this.ucIssueJob.FieldAttrs.Add(fieldAttr14);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr15);
            this.ucIssueJob.FieldAttrs.Add(fieldAttr16);
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
            // 
            // View_IssueJob
            // 
            this.View_IssueJob.CacheConnection = false;
            this.View_IssueJob.CommandText = resources.GetString("View_IssueJob.CommandText");
            this.View_IssueJob.CommandTimeout = 30;
            this.View_IssueJob.CommandType = System.Data.CommandType.Text;
            this.View_IssueJob.DynamicTableName = false;
            this.View_IssueJob.EEPAlias = "JBADMIN";
            this.View_IssueJob.EncodingAfter = null;
            this.View_IssueJob.EncodingBefore = "Windows-1252";
            this.View_IssueJob.EncodingConvert = null;
            this.View_IssueJob.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "IssueJobNO";
            this.View_IssueJob.KeyFields.Add(keyItem2);
            this.View_IssueJob.MultiSetWhere = false;
            this.View_IssueJob.Name = "View_IssueJob";
            this.View_IssueJob.NotificationAutoEnlist = false;
            this.View_IssueJob.SecExcept = null;
            this.View_IssueJob.SecFieldName = null;
            this.View_IssueJob.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_IssueJob.SelectPaging = false;
            this.View_IssueJob.SelectTop = 0;
            this.View_IssueJob.SiteControl = false;
            this.View_IssueJob.SiteFieldName = null;
            this.View_IssueJob.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // GROUPS
            // 
            this.GROUPS.CacheConnection = false;
            this.GROUPS.CommandText = "SELECT g.GROUPID,g.GROUPNAME FROM [JBADMIN].[dbo].[IssueJob] as ij\r\njoin [EIPHRSY" +
    "S].[dbo].[GROUPS] as g on ij.IssueBelongID = g.GROUPID\r\nwhere g.GROUPID IS NOT N" +
    "ULL\r\ngroup by g.GROUPID,g.GROUPNAME";
            this.GROUPS.CommandTimeout = 30;
            this.GROUPS.CommandType = System.Data.CommandType.Text;
            this.GROUPS.DynamicTableName = false;
            this.GROUPS.EEPAlias = "EIPHRSYS";
            this.GROUPS.EncodingAfter = null;
            this.GROUPS.EncodingBefore = "Windows-1252";
            this.GROUPS.EncodingConvert = null;
            this.GROUPS.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "GROUPID";
            this.GROUPS.KeyFields.Add(keyItem3);
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
            // USERS
            // 
            this.USERS.CacheConnection = false;
            this.USERS.CommandText = resources.GetString("USERS.CommandText");
            this.USERS.CommandTimeout = 30;
            this.USERS.CommandType = System.Data.CommandType.Text;
            this.USERS.DynamicTableName = false;
            this.USERS.EEPAlias = "EIPHRSYS";
            this.USERS.EncodingAfter = null;
            this.USERS.EncodingBefore = "Windows-1252";
            this.USERS.EncodingConvert = null;
            this.USERS.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "USERID";
            keyItem5.KeyName = "GROUPID";
            this.USERS.KeyFields.Add(keyItem4);
            this.USERS.KeyFields.Add(keyItem5);
            this.USERS.MultiSetWhere = false;
            this.USERS.Name = "USERS";
            this.USERS.NotificationAutoEnlist = false;
            this.USERS.SecExcept = null;
            this.USERS.SecFieldName = null;
            this.USERS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.USERS.SelectPaging = false;
            this.USERS.SelectTop = 0;
            this.USERS.SiteControl = false;
            this.USERS.SiteFieldName = null;
            this.USERS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Applier
            // 
            this.Applier.CacheConnection = false;
            this.Applier.CommandText = "SELECT distinct EmployeeID,CreateBy FROM [IssueJob]\r\nwhere CreateBy !=\'\' and Empl" +
    "oyeeID!=\'\'";
            this.Applier.CommandTimeout = 30;
            this.Applier.CommandType = System.Data.CommandType.Text;
            this.Applier.DynamicTableName = false;
            this.Applier.EEPAlias = "JBADMIN";
            this.Applier.EncodingAfter = null;
            this.Applier.EncodingBefore = "Windows-1252";
            this.Applier.EncodingConvert = null;
            this.Applier.InfoConnection = this.InfoConnection1;
            this.Applier.MultiSetWhere = false;
            this.Applier.Name = "Applier";
            this.Applier.NotificationAutoEnlist = false;
            this.Applier.SecExcept = null;
            this.Applier.SecFieldName = null;
            this.Applier.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Applier.SelectPaging = false;
            this.Applier.SelectTop = 0;
            this.Applier.SiteControl = false;
            this.Applier.SiteFieldName = null;
            this.Applier.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            // 
            // IssueType
            // 
            this.IssueType.CacheConnection = false;
            this.IssueType.CommandText = "SELECT IssueTypeID,IssueBelongID,IssueTypeName FROM dbo.[IssueType]\r\nORDER BY Iss" +
    "ueBelongID";
            this.IssueType.CommandTimeout = 30;
            this.IssueType.CommandType = System.Data.CommandType.Text;
            this.IssueType.DynamicTableName = false;
            this.IssueType.EEPAlias = "JBADMIN";
            this.IssueType.EncodingAfter = null;
            this.IssueType.EncodingBefore = "Windows-1252";
            this.IssueType.EncodingConvert = null;
            this.IssueType.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "IssueTypeID";
            this.IssueType.KeyFields.Add(keyItem6);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_IssueJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Applier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ORG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssueType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand IssueJob;
        private Srvtools.UpdateComponent ucIssueJob;
        private Srvtools.InfoCommand View_IssueJob;
        private Srvtools.InfoCommand GROUPS;
        private Srvtools.InfoCommand USERS;
        private Srvtools.InfoCommand Applier;
        private Srvtools.InfoCommand ORG;
        private Srvtools.InfoCommand IssueType;
    }
}
