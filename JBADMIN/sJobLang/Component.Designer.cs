namespace sJobLang
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem9 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem10 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem11 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_JobLang = new Srvtools.InfoCommand(this.components);
            this.ucHUT_JobLang = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_JobLang = new Srvtools.InfoCommand(this.components);
            this.HUT_ZLangType = new Srvtools.InfoCommand(this.components);
            this.HUT_ZLangLevel = new Srvtools.InfoCommand(this.components);
            this.HUT_LangLicence = new Srvtools.InfoCommand(this.components);
            this.HUT_ZLangNeedType = new Srvtools.InfoCommand(this.components);
            this.HUT_JobLangTemp = new Srvtools.InfoCommand(this.components);
            this.ucHUT_JobLangTemp = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_JobLang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_JobLang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_ZLangType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_ZLangLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_LangLicence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_ZLangNeedType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_JobLangTemp)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "UpdateJobLangTemp";
            service1.NonLogin = false;
            service1.ServiceName = "UpdateJobLangTemp";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_JobLang
            // 
            this.HUT_JobLang.CacheConnection = false;
            this.HUT_JobLang.CommandText = "SELECT [HUT_JobLang].* FROM [HUT_JobLang]";
            this.HUT_JobLang.CommandTimeout = 30;
            this.HUT_JobLang.CommandType = System.Data.CommandType.Text;
            this.HUT_JobLang.DynamicTableName = false;
            this.HUT_JobLang.EEPAlias = null;
            this.HUT_JobLang.EncodingAfter = null;
            this.HUT_JobLang.EncodingBefore = "Windows-1252";
            this.HUT_JobLang.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "JobID";
            keyItem2.KeyName = "LangID";
            this.HUT_JobLang.KeyFields.Add(keyItem1);
            this.HUT_JobLang.KeyFields.Add(keyItem2);
            this.HUT_JobLang.MultiSetWhere = false;
            this.HUT_JobLang.Name = "HUT_JobLang";
            this.HUT_JobLang.NotificationAutoEnlist = false;
            this.HUT_JobLang.SecExcept = null;
            this.HUT_JobLang.SecFieldName = null;
            this.HUT_JobLang.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_JobLang.SelectPaging = false;
            this.HUT_JobLang.SelectTop = 0;
            this.HUT_JobLang.SiteControl = false;
            this.HUT_JobLang.SiteFieldName = null;
            this.HUT_JobLang.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_JobLang
            // 
            this.ucHUT_JobLang.AutoTrans = true;
            this.ucHUT_JobLang.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "JobID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "LangID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ListenLevel";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SayLevel";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ReadLevel";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "WriteLevel";
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
            this.ucHUT_JobLang.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_JobLang.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_JobLang.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_JobLang.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_JobLang.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_JobLang.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_JobLang.FieldAttrs.Add(fieldAttr7);
            this.ucHUT_JobLang.FieldAttrs.Add(fieldAttr8);
            this.ucHUT_JobLang.LogInfo = null;
            this.ucHUT_JobLang.Name = "ucHUT_JobLang";
            this.ucHUT_JobLang.RowAffectsCheck = true;
            this.ucHUT_JobLang.SelectCmd = this.HUT_JobLang;
            this.ucHUT_JobLang.SelectCmdForUpdate = null;
            this.ucHUT_JobLang.ServerModify = true;
            this.ucHUT_JobLang.ServerModifyGetMax = false;
            this.ucHUT_JobLang.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_JobLang.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_JobLang.UseTranscationScope = false;
            this.ucHUT_JobLang.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_JobLang
            // 
            this.View_HUT_JobLang.CacheConnection = false;
            this.View_HUT_JobLang.CommandText = "SELECT * FROM [HUT_JobLang]";
            this.View_HUT_JobLang.CommandTimeout = 30;
            this.View_HUT_JobLang.CommandType = System.Data.CommandType.Text;
            this.View_HUT_JobLang.DynamicTableName = false;
            this.View_HUT_JobLang.EEPAlias = null;
            this.View_HUT_JobLang.EncodingAfter = null;
            this.View_HUT_JobLang.EncodingBefore = "Windows-1252";
            this.View_HUT_JobLang.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "JobID";
            keyItem4.KeyName = "LangID";
            this.View_HUT_JobLang.KeyFields.Add(keyItem3);
            this.View_HUT_JobLang.KeyFields.Add(keyItem4);
            this.View_HUT_JobLang.MultiSetWhere = false;
            this.View_HUT_JobLang.Name = "View_HUT_JobLang";
            this.View_HUT_JobLang.NotificationAutoEnlist = false;
            this.View_HUT_JobLang.SecExcept = null;
            this.View_HUT_JobLang.SecFieldName = null;
            this.View_HUT_JobLang.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_JobLang.SelectPaging = false;
            this.View_HUT_JobLang.SelectTop = 0;
            this.View_HUT_JobLang.SiteControl = false;
            this.View_HUT_JobLang.SiteFieldName = null;
            this.View_HUT_JobLang.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_ZLangType
            // 
            this.HUT_ZLangType.CacheConnection = false;
            this.HUT_ZLangType.CommandText = "select * from HUT_ZLangType";
            this.HUT_ZLangType.CommandTimeout = 30;
            this.HUT_ZLangType.CommandType = System.Data.CommandType.Text;
            this.HUT_ZLangType.DynamicTableName = false;
            this.HUT_ZLangType.EEPAlias = "Hunter";
            this.HUT_ZLangType.EncodingAfter = null;
            this.HUT_ZLangType.EncodingBefore = "Windows-1252";
            this.HUT_ZLangType.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "LangID";
            this.HUT_ZLangType.KeyFields.Add(keyItem5);
            this.HUT_ZLangType.MultiSetWhere = false;
            this.HUT_ZLangType.Name = "HUT_ZLangType";
            this.HUT_ZLangType.NotificationAutoEnlist = false;
            this.HUT_ZLangType.SecExcept = null;
            this.HUT_ZLangType.SecFieldName = null;
            this.HUT_ZLangType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_ZLangType.SelectPaging = false;
            this.HUT_ZLangType.SelectTop = 0;
            this.HUT_ZLangType.SiteControl = false;
            this.HUT_ZLangType.SiteFieldName = null;
            this.HUT_ZLangType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_ZLangLevel
            // 
            this.HUT_ZLangLevel.CacheConnection = false;
            this.HUT_ZLangLevel.CommandText = "select HUT_ZLangLevel.* from HUT_ZLangLevel";
            this.HUT_ZLangLevel.CommandTimeout = 30;
            this.HUT_ZLangLevel.CommandType = System.Data.CommandType.Text;
            this.HUT_ZLangLevel.DynamicTableName = false;
            this.HUT_ZLangLevel.EEPAlias = "Hunter";
            this.HUT_ZLangLevel.EncodingAfter = null;
            this.HUT_ZLangLevel.EncodingBefore = "Windows-1252";
            this.HUT_ZLangLevel.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "ID";
            this.HUT_ZLangLevel.KeyFields.Add(keyItem6);
            this.HUT_ZLangLevel.MultiSetWhere = false;
            this.HUT_ZLangLevel.Name = "HUT_ZLangLevel";
            this.HUT_ZLangLevel.NotificationAutoEnlist = false;
            this.HUT_ZLangLevel.SecExcept = null;
            this.HUT_ZLangLevel.SecFieldName = null;
            this.HUT_ZLangLevel.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_ZLangLevel.SelectPaging = false;
            this.HUT_ZLangLevel.SelectTop = 0;
            this.HUT_ZLangLevel.SiteControl = false;
            this.HUT_ZLangLevel.SiteFieldName = null;
            this.HUT_ZLangLevel.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_LangLicence
            // 
            this.HUT_LangLicence.CacheConnection = false;
            this.HUT_LangLicence.CommandText = "select * from HUT_LangLicence";
            this.HUT_LangLicence.CommandTimeout = 30;
            this.HUT_LangLicence.CommandType = System.Data.CommandType.Text;
            this.HUT_LangLicence.DynamicTableName = false;
            this.HUT_LangLicence.EEPAlias = "Hunter";
            this.HUT_LangLicence.EncodingAfter = null;
            this.HUT_LangLicence.EncodingBefore = "Windows-1252";
            this.HUT_LangLicence.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "ID";
            this.HUT_LangLicence.KeyFields.Add(keyItem7);
            this.HUT_LangLicence.MultiSetWhere = false;
            this.HUT_LangLicence.Name = "HUT_LangLicence";
            this.HUT_LangLicence.NotificationAutoEnlist = false;
            this.HUT_LangLicence.SecExcept = null;
            this.HUT_LangLicence.SecFieldName = null;
            this.HUT_LangLicence.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_LangLicence.SelectPaging = false;
            this.HUT_LangLicence.SelectTop = 0;
            this.HUT_LangLicence.SiteControl = false;
            this.HUT_LangLicence.SiteFieldName = null;
            this.HUT_LangLicence.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_ZLangNeedType
            // 
            this.HUT_ZLangNeedType.CacheConnection = false;
            this.HUT_ZLangNeedType.CommandText = "select * from HUT_ZLangNeedType";
            this.HUT_ZLangNeedType.CommandTimeout = 30;
            this.HUT_ZLangNeedType.CommandType = System.Data.CommandType.Text;
            this.HUT_ZLangNeedType.DynamicTableName = false;
            this.HUT_ZLangNeedType.EEPAlias = "Hunter";
            this.HUT_ZLangNeedType.EncodingAfter = null;
            this.HUT_ZLangNeedType.EncodingBefore = "Windows-1252";
            this.HUT_ZLangNeedType.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "ID";
            this.HUT_ZLangNeedType.KeyFields.Add(keyItem8);
            this.HUT_ZLangNeedType.MultiSetWhere = false;
            this.HUT_ZLangNeedType.Name = "HUT_ZLangNeedType";
            this.HUT_ZLangNeedType.NotificationAutoEnlist = false;
            this.HUT_ZLangNeedType.SecExcept = null;
            this.HUT_ZLangNeedType.SecFieldName = null;
            this.HUT_ZLangNeedType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_ZLangNeedType.SelectPaging = false;
            this.HUT_ZLangNeedType.SelectTop = 0;
            this.HUT_ZLangNeedType.SiteControl = false;
            this.HUT_ZLangNeedType.SiteFieldName = null;
            this.HUT_ZLangNeedType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_JobLangTemp
            // 
            this.HUT_JobLangTemp.CacheConnection = false;
            this.HUT_JobLangTemp.CommandText = "SELECT * FROM [HUT_JobLangTemp]";
            this.HUT_JobLangTemp.CommandTimeout = 30;
            this.HUT_JobLangTemp.CommandType = System.Data.CommandType.Text;
            this.HUT_JobLangTemp.DynamicTableName = false;
            this.HUT_JobLangTemp.EEPAlias = null;
            this.HUT_JobLangTemp.EncodingAfter = null;
            this.HUT_JobLangTemp.EncodingBefore = "Windows-1252";
            this.HUT_JobLangTemp.InfoConnection = this.InfoConnection1;
            keyItem9.KeyName = "JobID";
            keyItem10.KeyName = "LangID";
            keyItem11.KeyName = "IPAddress";
            this.HUT_JobLangTemp.KeyFields.Add(keyItem9);
            this.HUT_JobLangTemp.KeyFields.Add(keyItem10);
            this.HUT_JobLangTemp.KeyFields.Add(keyItem11);
            this.HUT_JobLangTemp.MultiSetWhere = false;
            this.HUT_JobLangTemp.Name = "HUT_JobLangTemp";
            this.HUT_JobLangTemp.NotificationAutoEnlist = false;
            this.HUT_JobLangTemp.SecExcept = null;
            this.HUT_JobLangTemp.SecFieldName = null;
            this.HUT_JobLangTemp.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_JobLangTemp.SelectPaging = false;
            this.HUT_JobLangTemp.SelectTop = 0;
            this.HUT_JobLangTemp.SiteControl = false;
            this.HUT_JobLangTemp.SiteFieldName = null;
            this.HUT_JobLangTemp.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_JobLangTemp
            // 
            this.ucHUT_JobLangTemp.AutoTrans = true;
            this.ucHUT_JobLangTemp.ExceptJoin = false;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "JobID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LangID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "ListenLevel";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "SayLevel";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "ReadLevel";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "WriteLevel";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CreateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "IPAddress";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr9);
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr10);
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr11);
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr12);
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr13);
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr14);
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr15);
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr16);
            this.ucHUT_JobLangTemp.FieldAttrs.Add(fieldAttr17);
            this.ucHUT_JobLangTemp.LogInfo = null;
            this.ucHUT_JobLangTemp.Name = "ucHUT_JobLangTemp";
            this.ucHUT_JobLangTemp.RowAffectsCheck = true;
            this.ucHUT_JobLangTemp.SelectCmd = this.HUT_JobLangTemp;
            this.ucHUT_JobLangTemp.SelectCmdForUpdate = null;
            this.ucHUT_JobLangTemp.ServerModify = true;
            this.ucHUT_JobLangTemp.ServerModifyGetMax = false;
            this.ucHUT_JobLangTemp.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_JobLangTemp.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_JobLangTemp.UseTranscationScope = false;
            this.ucHUT_JobLangTemp.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_JobLang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_JobLang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_ZLangType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_ZLangLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_LangLicence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_ZLangNeedType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_JobLangTemp)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_JobLang;
        private Srvtools.UpdateComponent ucHUT_JobLang;
        private Srvtools.InfoCommand View_HUT_JobLang;
        private Srvtools.InfoCommand HUT_ZLangType;
        private Srvtools.InfoCommand HUT_ZLangLevel;
        private Srvtools.InfoCommand HUT_LangLicence;
        private Srvtools.InfoCommand HUT_ZLangNeedType;
        private Srvtools.InfoCommand HUT_JobLangTemp;
        private Srvtools.UpdateComponent ucHUT_JobLangTemp;
    }
}
