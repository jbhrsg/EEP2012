namespace sJobQuery
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.JobQuery = new Srvtools.InfoCommand(this.components);
            this.QueryResult = new Srvtools.InfoCommand(this.components);
            this.AssignStep = new Srvtools.InfoCommand(this.components);
            this.JobAssignNew = new Srvtools.InfoCommand(this.components);
            this.ucHUT_JobAssignNew = new Srvtools.UpdateComponent(this.components);
            this.AssignNotes = new Srvtools.InfoCommand(this.components);
            this.ucHUT_AssignNotes = new Srvtools.UpdateComponent(this.components);
            this.idHUT_JobAssignNew_HUT_AssignNotes = new Srvtools.InfoDataSource(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.AssignLogs = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssignStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobAssignNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssignNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssignLogs)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "AddMenu";
            service1.NonLogin = false;
            service1.ServiceName = "AddMenu";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // JobQuery
            // 
            this.JobQuery.CacheConnection = false;
            this.JobQuery.CommandText = resources.GetString("JobQuery.CommandText");
            this.JobQuery.CommandTimeout = 30;
            this.JobQuery.CommandType = System.Data.CommandType.Text;
            this.JobQuery.DynamicTableName = false;
            this.JobQuery.EEPAlias = "Hunter";
            this.JobQuery.EncodingAfter = null;
            this.JobQuery.EncodingBefore = "Windows-1252";
            this.JobQuery.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "JobID";
            this.JobQuery.KeyFields.Add(keyItem1);
            this.JobQuery.MultiSetWhere = false;
            this.JobQuery.Name = "JobQuery";
            this.JobQuery.NotificationAutoEnlist = false;
            this.JobQuery.SecExcept = null;
            this.JobQuery.SecFieldName = null;
            this.JobQuery.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.JobQuery.SelectPaging = false;
            this.JobQuery.SelectTop = 0;
            this.JobQuery.SiteControl = false;
            this.JobQuery.SiteFieldName = null;
            this.JobQuery.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // QueryResult
            // 
            this.QueryResult.CacheConnection = false;
            this.QueryResult.CommandText = resources.GetString("QueryResult.CommandText");
            this.QueryResult.CommandTimeout = 30;
            this.QueryResult.CommandType = System.Data.CommandType.Text;
            this.QueryResult.DynamicTableName = false;
            this.QueryResult.EEPAlias = "Hunter";
            this.QueryResult.EncodingAfter = null;
            this.QueryResult.EncodingBefore = "Windows-1252";
            this.QueryResult.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "UserID";
            this.QueryResult.KeyFields.Add(keyItem2);
            this.QueryResult.MultiSetWhere = false;
            this.QueryResult.Name = "QueryResult";
            this.QueryResult.NotificationAutoEnlist = false;
            this.QueryResult.SecExcept = null;
            this.QueryResult.SecFieldName = null;
            this.QueryResult.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.QueryResult.SelectPaging = false;
            this.QueryResult.SelectTop = 0;
            this.QueryResult.SiteControl = false;
            this.QueryResult.SiteFieldName = null;
            this.QueryResult.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AssignStep
            // 
            this.AssignStep.CacheConnection = false;
            this.AssignStep.CommandText = "select HUT_ZAssignStep.* from HUT_ZAssignStep";
            this.AssignStep.CommandTimeout = 30;
            this.AssignStep.CommandType = System.Data.CommandType.Text;
            this.AssignStep.DynamicTableName = false;
            this.AssignStep.EEPAlias = "Hunter";
            this.AssignStep.EncodingAfter = null;
            this.AssignStep.EncodingBefore = "Windows-1252";
            this.AssignStep.InfoConnection = this.InfoConnection1;
            this.AssignStep.MultiSetWhere = false;
            this.AssignStep.Name = "AssignStep";
            this.AssignStep.NotificationAutoEnlist = false;
            this.AssignStep.SecExcept = null;
            this.AssignStep.SecFieldName = null;
            this.AssignStep.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssignStep.SelectPaging = false;
            this.AssignStep.SelectTop = 0;
            this.AssignStep.SiteControl = false;
            this.AssignStep.SiteFieldName = null;
            this.AssignStep.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // JobAssignNew
            // 
            this.JobAssignNew.CacheConnection = false;
            this.JobAssignNew.CommandText = resources.GetString("JobAssignNew.CommandText");
            this.JobAssignNew.CommandTimeout = 30;
            this.JobAssignNew.CommandType = System.Data.CommandType.Text;
            this.JobAssignNew.DynamicTableName = false;
            this.JobAssignNew.EEPAlias = null;
            this.JobAssignNew.EncodingAfter = null;
            this.JobAssignNew.EncodingBefore = "Windows-1252";
            this.JobAssignNew.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AssignNO";
            this.JobAssignNew.KeyFields.Add(keyItem3);
            this.JobAssignNew.MultiSetWhere = false;
            this.JobAssignNew.Name = "JobAssignNew";
            this.JobAssignNew.NotificationAutoEnlist = false;
            this.JobAssignNew.SecExcept = null;
            this.JobAssignNew.SecFieldName = null;
            this.JobAssignNew.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.JobAssignNew.SelectPaging = false;
            this.JobAssignNew.SelectTop = 0;
            this.JobAssignNew.SiteControl = false;
            this.JobAssignNew.SiteFieldName = null;
            this.JobAssignNew.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_JobAssignNew
            // 
            this.ucHUT_JobAssignNew.AutoTrans = true;
            this.ucHUT_JobAssignNew.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AssignNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "UserID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "JobID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "AssignID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "AssignTime";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LastUpdateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucHUT_JobAssignNew.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_JobAssignNew.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_JobAssignNew.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_JobAssignNew.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_JobAssignNew.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_JobAssignNew.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_JobAssignNew.FieldAttrs.Add(fieldAttr7);
            this.ucHUT_JobAssignNew.LogInfo = null;
            this.ucHUT_JobAssignNew.Name = "ucHUT_JobAssignNew";
            this.ucHUT_JobAssignNew.RowAffectsCheck = true;
            this.ucHUT_JobAssignNew.SelectCmd = this.JobAssignNew;
            this.ucHUT_JobAssignNew.ServerModify = true;
            this.ucHUT_JobAssignNew.ServerModifyGetMax = false;
            this.ucHUT_JobAssignNew.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_JobAssignNew.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_JobAssignNew.UseTranscationScope = false;
            this.ucHUT_JobAssignNew.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHUT_JobAssignNew.AfterDelete += new Srvtools.UpdateComponentAfterDeleteEventHandler(this.ucHUT_JobAssignNew_AfterDelete);
            // 
            // AssignNotes
            // 
            this.AssignNotes.CacheConnection = false;
            this.AssignNotes.CommandText = "SELECT [HUT_AssignNotes].* FROM [HUT_AssignNotes]";
            this.AssignNotes.CommandTimeout = 30;
            this.AssignNotes.CommandType = System.Data.CommandType.Text;
            this.AssignNotes.DynamicTableName = false;
            this.AssignNotes.EEPAlias = null;
            this.AssignNotes.EncodingAfter = null;
            this.AssignNotes.EncodingBefore = "Windows-1252";
            this.AssignNotes.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AutoKey";
            this.AssignNotes.KeyFields.Add(keyItem4);
            this.AssignNotes.MultiSetWhere = false;
            this.AssignNotes.Name = "AssignNotes";
            this.AssignNotes.NotificationAutoEnlist = false;
            this.AssignNotes.SecExcept = null;
            this.AssignNotes.SecFieldName = null;
            this.AssignNotes.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssignNotes.SelectPaging = false;
            this.AssignNotes.SelectTop = 0;
            this.AssignNotes.SiteControl = false;
            this.AssignNotes.SiteFieldName = null;
            this.AssignNotes.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_AssignNotes
            // 
            this.ucHUT_AssignNotes.AutoTrans = true;
            this.ucHUT_AssignNotes.ExceptJoin = false;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AssignNO";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "JobID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "AssignNotes";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            this.ucHUT_AssignNotes.FieldAttrs.Add(fieldAttr8);
            this.ucHUT_AssignNotes.FieldAttrs.Add(fieldAttr9);
            this.ucHUT_AssignNotes.FieldAttrs.Add(fieldAttr10);
            this.ucHUT_AssignNotes.FieldAttrs.Add(fieldAttr11);
            this.ucHUT_AssignNotes.FieldAttrs.Add(fieldAttr12);
            this.ucHUT_AssignNotes.LogInfo = null;
            this.ucHUT_AssignNotes.Name = "ucHUT_AssignNotes";
            this.ucHUT_AssignNotes.RowAffectsCheck = true;
            this.ucHUT_AssignNotes.SelectCmd = this.AssignNotes;
            this.ucHUT_AssignNotes.ServerModify = true;
            this.ucHUT_AssignNotes.ServerModifyGetMax = true;
            this.ucHUT_AssignNotes.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_AssignNotes.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_AssignNotes.UseTranscationScope = false;
            this.ucHUT_AssignNotes.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idHUT_JobAssignNew_HUT_AssignNotes
            // 
            this.idHUT_JobAssignNew_HUT_AssignNotes.Detail = this.AssignNotes;
            columnItem1.FieldName = "AssignNO";
            this.idHUT_JobAssignNew_HUT_AssignNotes.DetailColumns.Add(columnItem1);
            this.idHUT_JobAssignNew_HUT_AssignNotes.DynamicTableName = false;
            this.idHUT_JobAssignNew_HUT_AssignNotes.Master = this.JobAssignNew;
            columnItem2.FieldName = "AssignNO";
            this.idHUT_JobAssignNew_HUT_AssignNotes.MasterColumns.Add(columnItem2);
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "NotesAutoKey";
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
            this.autoNumber1.TargetColumn = "AutoKey";
            this.autoNumber1.UpdateComp = this.ucHUT_AssignNotes;
            // 
            // AssignLogs
            // 
            this.AssignLogs.CacheConnection = false;
            this.AssignLogs.CommandText = resources.GetString("AssignLogs.CommandText");
            this.AssignLogs.CommandTimeout = 30;
            this.AssignLogs.CommandType = System.Data.CommandType.Text;
            this.AssignLogs.DynamicTableName = false;
            this.AssignLogs.EEPAlias = null;
            this.AssignLogs.EncodingAfter = null;
            this.AssignLogs.EncodingBefore = "Windows-1252";
            this.AssignLogs.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "AssignNO";
            this.AssignLogs.KeyFields.Add(keyItem5);
            this.AssignLogs.MultiSetWhere = false;
            this.AssignLogs.Name = "AssignLogs";
            this.AssignLogs.NotificationAutoEnlist = false;
            this.AssignLogs.SecExcept = null;
            this.AssignLogs.SecFieldName = null;
            this.AssignLogs.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssignLogs.SelectPaging = false;
            this.AssignLogs.SelectTop = 0;
            this.AssignLogs.SiteControl = false;
            this.AssignLogs.SiteFieldName = null;
            this.AssignLogs.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssignStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobAssignNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssignNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssignLogs)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand JobQuery;
        private Srvtools.InfoCommand QueryResult;
        private Srvtools.InfoCommand AssignStep;
        private Srvtools.InfoCommand JobAssignNew;
        private Srvtools.UpdateComponent ucHUT_JobAssignNew;
        private Srvtools.InfoCommand AssignNotes;
        private Srvtools.UpdateComponent ucHUT_AssignNotes;
        private Srvtools.InfoDataSource idHUT_JobAssignNew_HUT_AssignNotes;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand AssignLogs;
    }
}
