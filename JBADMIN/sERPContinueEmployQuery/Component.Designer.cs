namespace sERPContinueEmployQuery
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPContinueEmployDetail = new Srvtools.InfoCommand(this.components);
            this.ucERPContinueEmployDetail = new Srvtools.UpdateComponent(this.components);
            this.View_ERPContinueEmployDetail = new Srvtools.InfoCommand(this.components);
            this.Employer = new Srvtools.InfoCommand(this.components);
            this.DueYearMonth = new Srvtools.InfoCommand(this.components);
            this.UserName = new Srvtools.InfoCommand(this.components);
            this.FlowFlag = new Srvtools.InfoCommand(this.components);
            this.Gender = new Srvtools.InfoCommand(this.components);
            this.Country = new Srvtools.InfoCommand(this.components);
            this.SalesID = new Srvtools.InfoCommand(this.components);
            this.CreateYearMonth = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContinueEmployDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContinueEmployDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DueYearMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlowFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Country)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreateYearMonth)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPContinueEmployDetail
            // 
            this.ERPContinueEmployDetail.CacheConnection = false;
            this.ERPContinueEmployDetail.CommandText = "SELECT * from [JBADMIN].dbo.[ERPContinueEmployDetail] \r\n";
            this.ERPContinueEmployDetail.CommandTimeout = 30;
            this.ERPContinueEmployDetail.CommandType = System.Data.CommandType.Text;
            this.ERPContinueEmployDetail.DynamicTableName = false;
            this.ERPContinueEmployDetail.EEPAlias = null;
            this.ERPContinueEmployDetail.EncodingAfter = null;
            this.ERPContinueEmployDetail.EncodingBefore = "Windows-1252";
            this.ERPContinueEmployDetail.EncodingConvert = null;
            this.ERPContinueEmployDetail.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            keyItem2.KeyName = "ContinueEmployNO";
            this.ERPContinueEmployDetail.KeyFields.Add(keyItem1);
            this.ERPContinueEmployDetail.KeyFields.Add(keyItem2);
            this.ERPContinueEmployDetail.MultiSetWhere = false;
            this.ERPContinueEmployDetail.Name = "ERPContinueEmployDetail";
            this.ERPContinueEmployDetail.NotificationAutoEnlist = false;
            this.ERPContinueEmployDetail.SecExcept = null;
            this.ERPContinueEmployDetail.SecFieldName = null;
            this.ERPContinueEmployDetail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPContinueEmployDetail.SelectPaging = false;
            this.ERPContinueEmployDetail.SelectTop = 0;
            this.ERPContinueEmployDetail.SiteControl = false;
            this.ERPContinueEmployDetail.SiteFieldName = null;
            this.ERPContinueEmployDetail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPContinueEmployDetail
            // 
            this.ucERPContinueEmployDetail.AutoTrans = true;
            this.ucERPContinueEmployDetail.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ContinueEmployNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "LaborName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Gender";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Country";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ImmigrationDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "DueDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CEConfirmNO";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "IsRecontract";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Transfer";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "ReturnHome";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Employer";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "BackPot";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LetterClass";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LetterNO";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr1);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr2);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr3);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr4);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr5);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr6);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr7);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr8);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr9);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr10);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr11);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr12);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr13);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr14);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr15);
            this.ucERPContinueEmployDetail.LogInfo = null;
            this.ucERPContinueEmployDetail.Name = "ucERPContinueEmployDetail";
            this.ucERPContinueEmployDetail.RowAffectsCheck = true;
            this.ucERPContinueEmployDetail.SelectCmd = this.ERPContinueEmployDetail;
            this.ucERPContinueEmployDetail.SelectCmdForUpdate = null;
            this.ucERPContinueEmployDetail.SendSQLCmd = true;
            this.ucERPContinueEmployDetail.ServerModify = true;
            this.ucERPContinueEmployDetail.ServerModifyGetMax = false;
            this.ucERPContinueEmployDetail.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPContinueEmployDetail.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPContinueEmployDetail.UseTranscationScope = false;
            this.ucERPContinueEmployDetail.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ERPContinueEmployDetail
            // 
            this.View_ERPContinueEmployDetail.CacheConnection = false;
            this.View_ERPContinueEmployDetail.CommandText = "SELECT * from View_ERPContinueEmployMasterDetail where FlowFlag <>\'X\' ORDER BY Du" +
    "eDate ASC";
            this.View_ERPContinueEmployDetail.CommandTimeout = 30;
            this.View_ERPContinueEmployDetail.CommandType = System.Data.CommandType.Text;
            this.View_ERPContinueEmployDetail.DynamicTableName = false;
            this.View_ERPContinueEmployDetail.EEPAlias = null;
            this.View_ERPContinueEmployDetail.EncodingAfter = null;
            this.View_ERPContinueEmployDetail.EncodingBefore = "Windows-1252";
            this.View_ERPContinueEmployDetail.EncodingConvert = null;
            this.View_ERPContinueEmployDetail.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ContinueEmployNO";
            keyItem4.KeyName = "AutoKey";
            this.View_ERPContinueEmployDetail.KeyFields.Add(keyItem3);
            this.View_ERPContinueEmployDetail.KeyFields.Add(keyItem4);
            this.View_ERPContinueEmployDetail.MultiSetWhere = false;
            this.View_ERPContinueEmployDetail.Name = "View_ERPContinueEmployDetail";
            this.View_ERPContinueEmployDetail.NotificationAutoEnlist = false;
            this.View_ERPContinueEmployDetail.SecExcept = null;
            this.View_ERPContinueEmployDetail.SecFieldName = null;
            this.View_ERPContinueEmployDetail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPContinueEmployDetail.SelectPaging = false;
            this.View_ERPContinueEmployDetail.SelectTop = 0;
            this.View_ERPContinueEmployDetail.SiteControl = false;
            this.View_ERPContinueEmployDetail.SiteFieldName = null;
            this.View_ERPContinueEmployDetail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employer
            // 
            this.Employer.CacheConnection = false;
            this.Employer.CommandText = "SELECT distinct Employer from View_ERPContinueEmployMasterDetail\r\nWHERE     FlowF" +
    "lag <> \'X\'";
            this.Employer.CommandTimeout = 30;
            this.Employer.CommandType = System.Data.CommandType.Text;
            this.Employer.DynamicTableName = false;
            this.Employer.EEPAlias = null;
            this.Employer.EncodingAfter = null;
            this.Employer.EncodingBefore = "Windows-1252";
            this.Employer.EncodingConvert = null;
            this.Employer.InfoConnection = this.InfoConnection1;
            this.Employer.MultiSetWhere = false;
            this.Employer.Name = "Employer";
            this.Employer.NotificationAutoEnlist = false;
            this.Employer.SecExcept = null;
            this.Employer.SecFieldName = null;
            this.Employer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Employer.SelectPaging = false;
            this.Employer.SelectTop = 0;
            this.Employer.SiteControl = false;
            this.Employer.SiteFieldName = null;
            this.Employer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DueYearMonth
            // 
            this.DueYearMonth.CacheConnection = false;
            this.DueYearMonth.CommandText = "SELECT distinct DueYearMonth from View_ERPContinueEmployMasterDetail\r\nWHERE     F" +
    "lowFlag <> \'X\' order by DueYearMonth desc";
            this.DueYearMonth.CommandTimeout = 30;
            this.DueYearMonth.CommandType = System.Data.CommandType.Text;
            this.DueYearMonth.DynamicTableName = false;
            this.DueYearMonth.EEPAlias = null;
            this.DueYearMonth.EncodingAfter = null;
            this.DueYearMonth.EncodingBefore = "Windows-1252";
            this.DueYearMonth.EncodingConvert = null;
            this.DueYearMonth.InfoConnection = this.InfoConnection1;
            this.DueYearMonth.MultiSetWhere = false;
            this.DueYearMonth.Name = "DueYearMonth";
            this.DueYearMonth.NotificationAutoEnlist = false;
            this.DueYearMonth.SecExcept = null;
            this.DueYearMonth.SecFieldName = null;
            this.DueYearMonth.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DueYearMonth.SelectPaging = false;
            this.DueYearMonth.SelectTop = 0;
            this.DueYearMonth.SiteControl = false;
            this.DueYearMonth.SiteFieldName = null;
            this.DueYearMonth.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // UserName
            // 
            this.UserName.CacheConnection = false;
            this.UserName.CommandText = "select userid,username from [EIPHRSYS].[dbo].[USERS]";
            this.UserName.CommandTimeout = 30;
            this.UserName.CommandType = System.Data.CommandType.Text;
            this.UserName.DynamicTableName = false;
            this.UserName.EEPAlias = null;
            this.UserName.EncodingAfter = null;
            this.UserName.EncodingBefore = "Windows-1252";
            this.UserName.EncodingConvert = null;
            this.UserName.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "userid";
            this.UserName.KeyFields.Add(keyItem5);
            this.UserName.MultiSetWhere = false;
            this.UserName.Name = "UserName";
            this.UserName.NotificationAutoEnlist = false;
            this.UserName.SecExcept = null;
            this.UserName.SecFieldName = null;
            this.UserName.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UserName.SelectPaging = false;
            this.UserName.SelectTop = 0;
            this.UserName.SiteControl = false;
            this.UserName.SiteFieldName = null;
            this.UserName.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // FlowFlag
            // 
            this.FlowFlag.CacheConnection = false;
            this.FlowFlag.CommandText = "select * from FlowStatus";
            this.FlowFlag.CommandTimeout = 30;
            this.FlowFlag.CommandType = System.Data.CommandType.Text;
            this.FlowFlag.DynamicTableName = false;
            this.FlowFlag.EEPAlias = null;
            this.FlowFlag.EncodingAfter = null;
            this.FlowFlag.EncodingBefore = "Windows-1252";
            this.FlowFlag.EncodingConvert = null;
            this.FlowFlag.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "FlowFlag";
            this.FlowFlag.KeyFields.Add(keyItem6);
            this.FlowFlag.MultiSetWhere = false;
            this.FlowFlag.Name = "FlowFlag";
            this.FlowFlag.NotificationAutoEnlist = false;
            this.FlowFlag.SecExcept = null;
            this.FlowFlag.SecFieldName = null;
            this.FlowFlag.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FlowFlag.SelectPaging = false;
            this.FlowFlag.SelectTop = 0;
            this.FlowFlag.SiteControl = false;
            this.FlowFlag.SiteFieldName = null;
            this.FlowFlag.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Gender
            // 
            this.Gender.CacheConnection = false;
            this.Gender.CommandText = "SELECT distinct Gender from View_ERPContinueEmployMasterDetail\r\nWHERE     FlowFla" +
    "g <> \'X\'";
            this.Gender.CommandTimeout = 30;
            this.Gender.CommandType = System.Data.CommandType.Text;
            this.Gender.DynamicTableName = false;
            this.Gender.EEPAlias = null;
            this.Gender.EncodingAfter = null;
            this.Gender.EncodingBefore = "Windows-1252";
            this.Gender.EncodingConvert = null;
            this.Gender.InfoConnection = this.InfoConnection1;
            this.Gender.MultiSetWhere = false;
            this.Gender.Name = "Gender";
            this.Gender.NotificationAutoEnlist = false;
            this.Gender.SecExcept = null;
            this.Gender.SecFieldName = null;
            this.Gender.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Gender.SelectPaging = false;
            this.Gender.SelectTop = 0;
            this.Gender.SiteControl = false;
            this.Gender.SiteFieldName = null;
            this.Gender.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Country
            // 
            this.Country.CacheConnection = false;
            this.Country.CommandText = "SELECT distinct Country from View_ERPContinueEmployMasterDetail\r\nWHERE     FlowFl" +
    "ag <> \'X\'";
            this.Country.CommandTimeout = 30;
            this.Country.CommandType = System.Data.CommandType.Text;
            this.Country.DynamicTableName = false;
            this.Country.EEPAlias = null;
            this.Country.EncodingAfter = null;
            this.Country.EncodingBefore = "Windows-1252";
            this.Country.EncodingConvert = null;
            this.Country.InfoConnection = this.InfoConnection1;
            this.Country.MultiSetWhere = false;
            this.Country.Name = "Country";
            this.Country.NotificationAutoEnlist = false;
            this.Country.SecExcept = null;
            this.Country.SecFieldName = null;
            this.Country.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Country.SelectPaging = false;
            this.Country.SelectTop = 0;
            this.Country.SiteControl = false;
            this.Country.SiteFieldName = null;
            this.Country.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesID
            // 
            this.SalesID.CacheConnection = false;
            this.SalesID.CommandText = "SELECT distinct u.USERNAME,v.SalesID from View_ERPContinueEmployMasterDetail v\r\ni" +
    "nner join [EIPHRSYS].[dbo].[USERS] u on u.USERID=v.SalesID\r\nWHERE     FlowFlag <" +
    "> \'X\'";
            this.SalesID.CommandTimeout = 30;
            this.SalesID.CommandType = System.Data.CommandType.Text;
            this.SalesID.DynamicTableName = false;
            this.SalesID.EEPAlias = null;
            this.SalesID.EncodingAfter = null;
            this.SalesID.EncodingBefore = "Windows-1252";
            this.SalesID.EncodingConvert = null;
            this.SalesID.InfoConnection = this.InfoConnection1;
            this.SalesID.MultiSetWhere = false;
            this.SalesID.Name = "SalesID";
            this.SalesID.NotificationAutoEnlist = false;
            this.SalesID.SecExcept = null;
            this.SalesID.SecFieldName = null;
            this.SalesID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesID.SelectPaging = false;
            this.SalesID.SelectTop = 0;
            this.SalesID.SiteControl = false;
            this.SalesID.SiteFieldName = null;
            this.SalesID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CreateYearMonth
            // 
            this.CreateYearMonth.CacheConnection = false;
            this.CreateYearMonth.CommandText = "SELECT distinct CreateYearMonth from View_ERPContinueEmployMasterDetail\r\nWHERE   " +
    "  FlowFlag <> \'X\' order by CreateYearMonth desc";
            this.CreateYearMonth.CommandTimeout = 30;
            this.CreateYearMonth.CommandType = System.Data.CommandType.Text;
            this.CreateYearMonth.DynamicTableName = false;
            this.CreateYearMonth.EEPAlias = null;
            this.CreateYearMonth.EncodingAfter = null;
            this.CreateYearMonth.EncodingBefore = "Windows-1252";
            this.CreateYearMonth.EncodingConvert = null;
            this.CreateYearMonth.InfoConnection = this.InfoConnection1;
            this.CreateYearMonth.MultiSetWhere = false;
            this.CreateYearMonth.Name = "CreateYearMonth";
            this.CreateYearMonth.NotificationAutoEnlist = false;
            this.CreateYearMonth.SecExcept = null;
            this.CreateYearMonth.SecFieldName = null;
            this.CreateYearMonth.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CreateYearMonth.SelectPaging = false;
            this.CreateYearMonth.SelectTop = 0;
            this.CreateYearMonth.SiteControl = false;
            this.CreateYearMonth.SiteFieldName = null;
            this.CreateYearMonth.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContinueEmployDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContinueEmployDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DueYearMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlowFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Country)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreateYearMonth)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPContinueEmployDetail;
        private Srvtools.UpdateComponent ucERPContinueEmployDetail;
        private Srvtools.InfoCommand View_ERPContinueEmployDetail;
        private Srvtools.InfoCommand Employer;
        private Srvtools.InfoCommand DueYearMonth;
        private Srvtools.InfoCommand UserName;
        private Srvtools.InfoCommand FlowFlag;
        private Srvtools.InfoCommand Gender;
        private Srvtools.InfoCommand Country;
        private Srvtools.InfoCommand SalesID;
        private Srvtools.InfoCommand CreateYearMonth;
    }
}
