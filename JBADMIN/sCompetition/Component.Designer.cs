namespace sCompetition
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Competition = new Srvtools.InfoCommand(this.components);
            this.ucCompetition = new Srvtools.UpdateComponent(this.components);
            this.dataFormReal_Department_cb = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.SYS_ORG = new Srvtools.InfoCommand(this.components);
            this.Competition2 = new Srvtools.InfoCommand(this.components);
            this.ucCompetition2 = new Srvtools.UpdateComponent(this.components);
            this.autoNumber2 = new Srvtools.AutoNumber(this.components);
            this.Year_Query = new Srvtools.InfoCommand(this.components);
            this.Quarter_Query = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Competition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataFormReal_Department_cb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ORG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Competition2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year_Query)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Quarter_Query)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // Competition
            // 
            this.Competition.CacheConnection = false;
            this.Competition.CommandText = "SELECT dbo.[Competition].* FROM dbo.[Competition] \r\norder by Year desc,Quarter de" +
    "sc ,AutoKey";
            this.Competition.CommandTimeout = 30;
            this.Competition.CommandType = System.Data.CommandType.Text;
            this.Competition.DynamicTableName = false;
            this.Competition.EEPAlias = null;
            this.Competition.EncodingAfter = null;
            this.Competition.EncodingBefore = "Windows-1252";
            this.Competition.EncodingConvert = null;
            this.Competition.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "Year";
            keyItem2.KeyName = "Department";
            keyItem3.KeyName = "Quarter";
            this.Competition.KeyFields.Add(keyItem1);
            this.Competition.KeyFields.Add(keyItem2);
            this.Competition.KeyFields.Add(keyItem3);
            this.Competition.MultiSetWhere = false;
            this.Competition.Name = "Competition";
            this.Competition.NotificationAutoEnlist = false;
            this.Competition.SecExcept = null;
            this.Competition.SecFieldName = null;
            this.Competition.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Competition.SelectPaging = false;
            this.Competition.SelectTop = 0;
            this.Competition.SiteControl = false;
            this.Competition.SiteFieldName = null;
            this.Competition.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCompetition
            // 
            this.ucCompetition.AutoTrans = true;
            this.ucCompetition.ExceptJoin = false;
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
            fieldAttr3.DataField = "Quarter";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Department";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "QuarterTarget";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "4m";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "5m";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "6m";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Unit";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucCompetition.FieldAttrs.Add(fieldAttr1);
            this.ucCompetition.FieldAttrs.Add(fieldAttr2);
            this.ucCompetition.FieldAttrs.Add(fieldAttr3);
            this.ucCompetition.FieldAttrs.Add(fieldAttr4);
            this.ucCompetition.FieldAttrs.Add(fieldAttr5);
            this.ucCompetition.FieldAttrs.Add(fieldAttr6);
            this.ucCompetition.FieldAttrs.Add(fieldAttr7);
            this.ucCompetition.FieldAttrs.Add(fieldAttr8);
            this.ucCompetition.FieldAttrs.Add(fieldAttr9);
            this.ucCompetition.LogInfo = null;
            this.ucCompetition.Name = "ucCompetition";
            this.ucCompetition.RowAffectsCheck = false;
            this.ucCompetition.SelectCmd = this.Competition;
            this.ucCompetition.SelectCmdForUpdate = null;
            this.ucCompetition.SendSQLCmd = true;
            this.ucCompetition.ServerModify = true;
            this.ucCompetition.ServerModifyGetMax = false;
            this.ucCompetition.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCompetition.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCompetition.UseTranscationScope = false;
            this.ucCompetition.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // dataFormReal_Department_cb
            // 
            this.dataFormReal_Department_cb.CacheConnection = false;
            this.dataFormReal_Department_cb.CommandText = "SELECT Department,Unit  FROM dbo.[Competition]  where Year =\'2020\' and Quarter=\'3" +
    "\'";
            this.dataFormReal_Department_cb.CommandTimeout = 30;
            this.dataFormReal_Department_cb.CommandType = System.Data.CommandType.Text;
            this.dataFormReal_Department_cb.DynamicTableName = false;
            this.dataFormReal_Department_cb.EEPAlias = null;
            this.dataFormReal_Department_cb.EncodingAfter = null;
            this.dataFormReal_Department_cb.EncodingBefore = "Windows-1252";
            this.dataFormReal_Department_cb.EncodingConvert = null;
            this.dataFormReal_Department_cb.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "Year";
            keyItem5.KeyName = "Department";
            this.dataFormReal_Department_cb.KeyFields.Add(keyItem4);
            this.dataFormReal_Department_cb.KeyFields.Add(keyItem5);
            this.dataFormReal_Department_cb.MultiSetWhere = false;
            this.dataFormReal_Department_cb.Name = "dataFormReal_Department_cb";
            this.dataFormReal_Department_cb.NotificationAutoEnlist = false;
            this.dataFormReal_Department_cb.SecExcept = null;
            this.dataFormReal_Department_cb.SecFieldName = null;
            this.dataFormReal_Department_cb.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.dataFormReal_Department_cb.SelectPaging = false;
            this.dataFormReal_Department_cb.SelectTop = 0;
            this.dataFormReal_Department_cb.SiteControl = false;
            this.dataFormReal_Department_cb.SiteFieldName = null;
            this.dataFormReal_Department_cb.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "CompetitionAutokey";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 10;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "AutoKey";
            this.autoNumber1.UpdateComp = this.ucCompetition;
            // 
            // SYS_ORG
            // 
            this.SYS_ORG.CacheConnection = false;
            this.SYS_ORG.CommandText = resources.GetString("SYS_ORG.CommandText");
            this.SYS_ORG.CommandTimeout = 30;
            this.SYS_ORG.CommandType = System.Data.CommandType.Text;
            this.SYS_ORG.DynamicTableName = false;
            this.SYS_ORG.EEPAlias = null;
            this.SYS_ORG.EncodingAfter = null;
            this.SYS_ORG.EncodingBefore = "Windows-1252";
            this.SYS_ORG.EncodingConvert = null;
            this.SYS_ORG.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "ORG_NO";
            this.SYS_ORG.KeyFields.Add(keyItem6);
            this.SYS_ORG.MultiSetWhere = false;
            this.SYS_ORG.Name = "SYS_ORG";
            this.SYS_ORG.NotificationAutoEnlist = false;
            this.SYS_ORG.SecExcept = null;
            this.SYS_ORG.SecFieldName = null;
            this.SYS_ORG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SYS_ORG.SelectPaging = false;
            this.SYS_ORG.SelectTop = 0;
            this.SYS_ORG.SiteControl = false;
            this.SYS_ORG.SiteFieldName = null;
            this.SYS_ORG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Competition2
            // 
            this.Competition2.CacheConnection = false;
            this.Competition2.CommandText = resources.GetString("Competition2.CommandText");
            this.Competition2.CommandTimeout = 30;
            this.Competition2.CommandType = System.Data.CommandType.Text;
            this.Competition2.DynamicTableName = false;
            this.Competition2.EEPAlias = null;
            this.Competition2.EncodingAfter = null;
            this.Competition2.EncodingBefore = "Windows-1252";
            this.Competition2.EncodingConvert = null;
            this.Competition2.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "Date";
            keyItem8.KeyName = "Department";
            this.Competition2.KeyFields.Add(keyItem7);
            this.Competition2.KeyFields.Add(keyItem8);
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
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "AutoKey";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Date";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Month";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "Department";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "Real";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = "_username";
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CreateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = "_sysdate";
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "LastUpdateBy";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr17.DefaultValue = "_username";
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "LastUpdateDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr18.DefaultValue = "_sysdate";
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.ucCompetition2.FieldAttrs.Add(fieldAttr10);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr11);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr12);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr13);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr14);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr15);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr16);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr17);
            this.ucCompetition2.FieldAttrs.Add(fieldAttr18);
            this.ucCompetition2.LogInfo = null;
            this.ucCompetition2.Name = "ucCompetition2";
            this.ucCompetition2.RowAffectsCheck = false;
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
            // autoNumber2
            // 
            this.autoNumber2.Active = true;
            this.autoNumber2.AutoNoID = "Competition2Autokey";
            this.autoNumber2.Description = null;
            this.autoNumber2.GetFixed = "";
            this.autoNumber2.isNumFill = false;
            this.autoNumber2.Name = "autoNumber2";
            this.autoNumber2.Number = null;
            this.autoNumber2.NumDig = 10;
            this.autoNumber2.OldVersion = false;
            this.autoNumber2.OverFlow = true;
            this.autoNumber2.StartValue = 1;
            this.autoNumber2.Step = 1;
            this.autoNumber2.TargetColumn = "AutoKey";
            this.autoNumber2.UpdateComp = this.ucCompetition2;
            // 
            // Year_Query
            // 
            this.Year_Query.CacheConnection = false;
            this.Year_Query.CommandText = "SELECT distinct dbo.[Competition].Year FROM dbo.[Competition] \r\norder by Year des" +
    "c";
            this.Year_Query.CommandTimeout = 30;
            this.Year_Query.CommandType = System.Data.CommandType.Text;
            this.Year_Query.DynamicTableName = false;
            this.Year_Query.EEPAlias = null;
            this.Year_Query.EncodingAfter = null;
            this.Year_Query.EncodingBefore = "Windows-1252";
            this.Year_Query.EncodingConvert = null;
            this.Year_Query.InfoConnection = this.InfoConnection1;
            this.Year_Query.MultiSetWhere = false;
            this.Year_Query.Name = "Year_Query";
            this.Year_Query.NotificationAutoEnlist = false;
            this.Year_Query.SecExcept = null;
            this.Year_Query.SecFieldName = null;
            this.Year_Query.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Year_Query.SelectPaging = false;
            this.Year_Query.SelectTop = 0;
            this.Year_Query.SiteControl = false;
            this.Year_Query.SiteFieldName = null;
            this.Year_Query.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Quarter_Query
            // 
            this.Quarter_Query.CacheConnection = false;
            this.Quarter_Query.CommandText = "SELECT distinct dbo.[Competition].Quarter FROM dbo.[Competition] \r\norder by Quart" +
    "er";
            this.Quarter_Query.CommandTimeout = 30;
            this.Quarter_Query.CommandType = System.Data.CommandType.Text;
            this.Quarter_Query.DynamicTableName = false;
            this.Quarter_Query.EEPAlias = null;
            this.Quarter_Query.EncodingAfter = null;
            this.Quarter_Query.EncodingBefore = "Windows-1252";
            this.Quarter_Query.EncodingConvert = null;
            this.Quarter_Query.InfoConnection = this.InfoConnection1;
            this.Quarter_Query.MultiSetWhere = false;
            this.Quarter_Query.Name = "Quarter_Query";
            this.Quarter_Query.NotificationAutoEnlist = false;
            this.Quarter_Query.SecExcept = null;
            this.Quarter_Query.SecFieldName = null;
            this.Quarter_Query.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Quarter_Query.SelectPaging = false;
            this.Quarter_Query.SelectTop = 0;
            this.Quarter_Query.SiteControl = false;
            this.Quarter_Query.SiteFieldName = null;
            this.Quarter_Query.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Competition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataFormReal_Department_cb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ORG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Competition2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year_Query)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Quarter_Query)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Competition;
        private Srvtools.UpdateComponent ucCompetition;
        private Srvtools.InfoCommand dataFormReal_Department_cb;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand SYS_ORG;
        private Srvtools.InfoCommand Competition2;
        private Srvtools.UpdateComponent ucCompetition2;
        private Srvtools.AutoNumber autoNumber2;
        private Srvtools.InfoCommand Year_Query;
        private Srvtools.InfoCommand Quarter_Query;
    }
}
