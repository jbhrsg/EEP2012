namespace sCompetitionOutput1
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Competition1 = new Srvtools.InfoCommand(this.components);
            this.ucCompetition1 = new Srvtools.UpdateComponent(this.components);
            this.Competition2 = new Srvtools.InfoCommand(this.components);
            this.ucCompetition2 = new Srvtools.UpdateComponent(this.components);
            this.View_Competition1 = new Srvtools.InfoCommand(this.components);
            this.View_Competition2 = new Srvtools.InfoCommand(this.components);
            this.RealTargetRatio = new Srvtools.InfoCommand(this.components);
            this.AccumRealTargetRatio1 = new Srvtools.InfoCommand(this.components);
            this.AccumRealTargetRatio = new Srvtools.InfoCommand(this.components);
            this.AccumRealTargetRatio2 = new Srvtools.InfoCommand(this.components);
            this.RealTargetRatio1 = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Competition1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Competition2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Competition1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Competition2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RealTargetRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccumRealTargetRatio1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccumRealTargetRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccumRealTargetRatio2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RealTargetRatio1)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // Competition1
            // 
            this.Competition1.CacheConnection = false;
            this.Competition1.CommandText = "SELECT dbo.[Competition1].* FROM dbo.[Competition1]";
            this.Competition1.CommandTimeout = 30;
            this.Competition1.CommandType = System.Data.CommandType.Text;
            this.Competition1.DynamicTableName = false;
            this.Competition1.EEPAlias = null;
            this.Competition1.EncodingAfter = null;
            this.Competition1.EncodingBefore = "Windows-1252";
            this.Competition1.EncodingConvert = null;
            this.Competition1.InfoConnection = this.InfoConnection1;
            this.Competition1.MultiSetWhere = false;
            this.Competition1.Name = "Competition1";
            this.Competition1.NotificationAutoEnlist = false;
            this.Competition1.SecExcept = null;
            this.Competition1.SecFieldName = null;
            this.Competition1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Competition1.SelectPaging = false;
            this.Competition1.SelectTop = 0;
            this.Competition1.SiteControl = false;
            this.Competition1.SiteFieldName = null;
            this.Competition1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCompetition1
            // 
            this.ucCompetition1.AutoTrans = true;
            this.ucCompetition1.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "Year";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "Month";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "QuarterTarget";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Real";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Department";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucCompetition1.FieldAttrs.Add(fieldAttr1);
            this.ucCompetition1.FieldAttrs.Add(fieldAttr2);
            this.ucCompetition1.FieldAttrs.Add(fieldAttr3);
            this.ucCompetition1.FieldAttrs.Add(fieldAttr4);
            this.ucCompetition1.FieldAttrs.Add(fieldAttr5);
            this.ucCompetition1.FieldAttrs.Add(fieldAttr6);
            this.ucCompetition1.LogInfo = null;
            this.ucCompetition1.Name = "ucCompetition1";
            this.ucCompetition1.RowAffectsCheck = true;
            this.ucCompetition1.SelectCmd = this.Competition1;
            this.ucCompetition1.SelectCmdForUpdate = null;
            this.ucCompetition1.SendSQLCmd = true;
            this.ucCompetition1.ServerModify = true;
            this.ucCompetition1.ServerModifyGetMax = false;
            this.ucCompetition1.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCompetition1.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCompetition1.UseTranscationScope = false;
            this.ucCompetition1.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // Competition2
            // 
            this.Competition2.CacheConnection = false;
            this.Competition2.CommandText = "SELECT dbo.[Competition2].* FROM dbo.[Competition2]";
            this.Competition2.CommandTimeout = 30;
            this.Competition2.CommandType = System.Data.CommandType.Text;
            this.Competition2.DynamicTableName = false;
            this.Competition2.EEPAlias = null;
            this.Competition2.EncodingAfter = null;
            this.Competition2.EncodingBefore = "Windows-1252";
            this.Competition2.EncodingConvert = null;
            this.Competition2.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "Date";
            keyItem2.KeyName = "Department";
            this.Competition2.KeyFields.Add(keyItem1);
            this.Competition2.KeyFields.Add(keyItem2);
            this.Competition2.MultiSetWhere = false;
            this.Competition2.Name = "Competition2";
            this.Competition2.NotificationAutoEnlist = false;
            this.Competition2.SecExcept = null;
            this.Competition2.SecFieldName = null;
            this.Competition2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Competition2.SelectPaging = false;
            this.Competition2.SelectTop = 0;
            this.Competition2.SiteControl = false;
            this.Competition2.SiteFieldName = null;
            this.Competition2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCompetition2
            // 
            this.ucCompetition2.AutoTrans = true;
            this.ucCompetition2.ExceptJoin = false;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AutoKey";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Date";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Year";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Month";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Department";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Real";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            this.ucCompetition2.FieldAttrs.Add(fieldAttr7);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr8);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr9);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr10);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr11);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr12);
            this.ucCompetition2.LogInfo = null;
            this.ucCompetition2.Name = "ucCompetition2";
            this.ucCompetition2.RowAffectsCheck = true;
            this.ucCompetition2.SelectCmd = this.Competition2;
            this.ucCompetition2.SelectCmdForUpdate = null;
            this.ucCompetition2.SendSQLCmd = true;
            this.ucCompetition2.ServerModify = true;
            this.ucCompetition2.ServerModifyGetMax = false;
            this.ucCompetition2.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCompetition2.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCompetition2.UseTranscationScope = false;
            this.ucCompetition2.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_Competition1
            // 
            this.View_Competition1.CacheConnection = false;
            this.View_Competition1.CommandText = "SELECT * FROM dbo.[Competition1]";
            this.View_Competition1.CommandTimeout = 30;
            this.View_Competition1.CommandType = System.Data.CommandType.Text;
            this.View_Competition1.DynamicTableName = false;
            this.View_Competition1.EEPAlias = null;
            this.View_Competition1.EncodingAfter = null;
            this.View_Competition1.EncodingBefore = "Windows-1252";
            this.View_Competition1.EncodingConvert = null;
            this.View_Competition1.InfoConnection = this.InfoConnection1;
            this.View_Competition1.MultiSetWhere = false;
            this.View_Competition1.Name = "View_Competition1";
            this.View_Competition1.NotificationAutoEnlist = false;
            this.View_Competition1.SecExcept = null;
            this.View_Competition1.SecFieldName = null;
            this.View_Competition1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Competition1.SelectPaging = false;
            this.View_Competition1.SelectTop = 0;
            this.View_Competition1.SiteControl = false;
            this.View_Competition1.SiteFieldName = null;
            this.View_Competition1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_Competition2
            // 
            this.View_Competition2.CacheConnection = false;
            this.View_Competition2.CommandText = "SELECT * FROM dbo.[Competition2]";
            this.View_Competition2.CommandTimeout = 30;
            this.View_Competition2.CommandType = System.Data.CommandType.Text;
            this.View_Competition2.DynamicTableName = false;
            this.View_Competition2.EEPAlias = null;
            this.View_Competition2.EncodingAfter = null;
            this.View_Competition2.EncodingBefore = "Windows-1252";
            this.View_Competition2.EncodingConvert = null;
            this.View_Competition2.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "Date";
            keyItem4.KeyName = "Department";
            this.View_Competition2.KeyFields.Add(keyItem3);
            this.View_Competition2.KeyFields.Add(keyItem4);
            this.View_Competition2.MultiSetWhere = false;
            this.View_Competition2.Name = "View_Competition2";
            this.View_Competition2.NotificationAutoEnlist = false;
            this.View_Competition2.SecExcept = null;
            this.View_Competition2.SecFieldName = null;
            this.View_Competition2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Competition2.SelectPaging = false;
            this.View_Competition2.SelectTop = 0;
            this.View_Competition2.SiteControl = false;
            this.View_Competition2.SiteFieldName = null;
            this.View_Competition2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // RealTargetRatio
            // 
            this.RealTargetRatio.CacheConnection = false;
            this.RealTargetRatio.CommandText = resources.GetString("RealTargetRatio.CommandText");
            this.RealTargetRatio.CommandTimeout = 30;
            this.RealTargetRatio.CommandType = System.Data.CommandType.Text;
            this.RealTargetRatio.DynamicTableName = false;
            this.RealTargetRatio.EEPAlias = null;
            this.RealTargetRatio.EncodingAfter = null;
            this.RealTargetRatio.EncodingBefore = "Windows-1252";
            this.RealTargetRatio.EncodingConvert = null;
            this.RealTargetRatio.InfoConnection = this.InfoConnection1;
            this.RealTargetRatio.MultiSetWhere = false;
            this.RealTargetRatio.Name = "RealTargetRatio";
            this.RealTargetRatio.NotificationAutoEnlist = false;
            this.RealTargetRatio.SecExcept = null;
            this.RealTargetRatio.SecFieldName = null;
            this.RealTargetRatio.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.RealTargetRatio.SelectPaging = false;
            this.RealTargetRatio.SelectTop = 0;
            this.RealTargetRatio.SiteControl = false;
            this.RealTargetRatio.SiteFieldName = null;
            this.RealTargetRatio.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccumRealTargetRatio1
            // 
            this.AccumRealTargetRatio1.CacheConnection = false;
            this.AccumRealTargetRatio1.CommandText = resources.GetString("AccumRealTargetRatio1.CommandText");
            this.AccumRealTargetRatio1.CommandTimeout = 30;
            this.AccumRealTargetRatio1.CommandType = System.Data.CommandType.Text;
            this.AccumRealTargetRatio1.DynamicTableName = false;
            this.AccumRealTargetRatio1.EEPAlias = null;
            this.AccumRealTargetRatio1.EncodingAfter = null;
            this.AccumRealTargetRatio1.EncodingBefore = "Windows-1252";
            this.AccumRealTargetRatio1.EncodingConvert = null;
            this.AccumRealTargetRatio1.InfoConnection = this.InfoConnection1;
            this.AccumRealTargetRatio1.MultiSetWhere = false;
            this.AccumRealTargetRatio1.Name = "AccumRealTargetRatio1";
            this.AccumRealTargetRatio1.NotificationAutoEnlist = false;
            this.AccumRealTargetRatio1.SecExcept = null;
            this.AccumRealTargetRatio1.SecFieldName = null;
            this.AccumRealTargetRatio1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AccumRealTargetRatio1.SelectPaging = false;
            this.AccumRealTargetRatio1.SelectTop = 0;
            this.AccumRealTargetRatio1.SiteControl = false;
            this.AccumRealTargetRatio1.SiteFieldName = null;
            this.AccumRealTargetRatio1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccumRealTargetRatio
            // 
            this.AccumRealTargetRatio.CacheConnection = false;
            this.AccumRealTargetRatio.CommandText = resources.GetString("AccumRealTargetRatio.CommandText");
            this.AccumRealTargetRatio.CommandTimeout = 30;
            this.AccumRealTargetRatio.CommandType = System.Data.CommandType.Text;
            this.AccumRealTargetRatio.DynamicTableName = false;
            this.AccumRealTargetRatio.EEPAlias = null;
            this.AccumRealTargetRatio.EncodingAfter = null;
            this.AccumRealTargetRatio.EncodingBefore = "Windows-1252";
            this.AccumRealTargetRatio.EncodingConvert = null;
            this.AccumRealTargetRatio.InfoConnection = this.InfoConnection1;
            this.AccumRealTargetRatio.MultiSetWhere = false;
            this.AccumRealTargetRatio.Name = "AccumRealTargetRatio";
            this.AccumRealTargetRatio.NotificationAutoEnlist = false;
            this.AccumRealTargetRatio.SecExcept = null;
            this.AccumRealTargetRatio.SecFieldName = null;
            this.AccumRealTargetRatio.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AccumRealTargetRatio.SelectPaging = false;
            this.AccumRealTargetRatio.SelectTop = 0;
            this.AccumRealTargetRatio.SiteControl = false;
            this.AccumRealTargetRatio.SiteFieldName = null;
            this.AccumRealTargetRatio.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccumRealTargetRatio2
            // 
            this.AccumRealTargetRatio2.CacheConnection = false;
            this.AccumRealTargetRatio2.CommandText = resources.GetString("AccumRealTargetRatio2.CommandText");
            this.AccumRealTargetRatio2.CommandTimeout = 30;
            this.AccumRealTargetRatio2.CommandType = System.Data.CommandType.Text;
            this.AccumRealTargetRatio2.DynamicTableName = false;
            this.AccumRealTargetRatio2.EEPAlias = null;
            this.AccumRealTargetRatio2.EncodingAfter = null;
            this.AccumRealTargetRatio2.EncodingBefore = "Windows-1252";
            this.AccumRealTargetRatio2.EncodingConvert = null;
            this.AccumRealTargetRatio2.InfoConnection = this.InfoConnection1;
            this.AccumRealTargetRatio2.MultiSetWhere = false;
            this.AccumRealTargetRatio2.Name = "AccumRealTargetRatio2";
            this.AccumRealTargetRatio2.NotificationAutoEnlist = false;
            this.AccumRealTargetRatio2.SecExcept = null;
            this.AccumRealTargetRatio2.SecFieldName = null;
            this.AccumRealTargetRatio2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AccumRealTargetRatio2.SelectPaging = false;
            this.AccumRealTargetRatio2.SelectTop = 0;
            this.AccumRealTargetRatio2.SiteControl = false;
            this.AccumRealTargetRatio2.SiteFieldName = null;
            this.AccumRealTargetRatio2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // RealTargetRatio1
            // 
            this.RealTargetRatio1.CacheConnection = false;
            this.RealTargetRatio1.CommandText = resources.GetString("RealTargetRatio1.CommandText");
            this.RealTargetRatio1.CommandTimeout = 30;
            this.RealTargetRatio1.CommandType = System.Data.CommandType.Text;
            this.RealTargetRatio1.DynamicTableName = false;
            this.RealTargetRatio1.EEPAlias = null;
            this.RealTargetRatio1.EncodingAfter = null;
            this.RealTargetRatio1.EncodingBefore = "Windows-1252";
            this.RealTargetRatio1.EncodingConvert = null;
            this.RealTargetRatio1.InfoConnection = this.InfoConnection1;
            this.RealTargetRatio1.MultiSetWhere = false;
            this.RealTargetRatio1.Name = "RealTargetRatio1";
            this.RealTargetRatio1.NotificationAutoEnlist = false;
            this.RealTargetRatio1.SecExcept = null;
            this.RealTargetRatio1.SecFieldName = null;
            this.RealTargetRatio1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.RealTargetRatio1.SelectPaging = false;
            this.RealTargetRatio1.SelectTop = 0;
            this.RealTargetRatio1.SiteControl = false;
            this.RealTargetRatio1.SiteFieldName = null;
            this.RealTargetRatio1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Competition1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Competition2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Competition1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Competition2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RealTargetRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccumRealTargetRatio1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccumRealTargetRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccumRealTargetRatio2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RealTargetRatio1)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Competition1;
        private Srvtools.UpdateComponent ucCompetition1;
        private Srvtools.InfoCommand Competition2;
        private Srvtools.UpdateComponent ucCompetition2;
        private Srvtools.InfoCommand View_Competition1;
        private Srvtools.InfoCommand View_Competition2;
        private Srvtools.InfoCommand RealTargetRatio;
        private Srvtools.InfoCommand AccumRealTargetRatio1;
        private Srvtools.InfoCommand AccumRealTargetRatio;
        private Srvtools.InfoCommand AccumRealTargetRatio2;
        private Srvtools.InfoCommand RealTargetRatio1;
    }
}
