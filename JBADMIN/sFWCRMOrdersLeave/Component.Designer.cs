namespace sFWCRMOrdersLeave
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMOrdersLeave = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMOrdersLeave = new Srvtools.UpdateComponent(this.components);
            this.View_FWCRMOrdersLeave = new Srvtools.InfoCommand(this.components);
            this.infoFWCRMAirline = new Srvtools.InfoCommand(this.components);
            this.infoFWCRMCurrency = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrdersLeave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_FWCRMOrdersLeave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMAirline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMCurrency)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMOrdersLeave
            // 
            this.FWCRMOrdersLeave.CacheConnection = false;
            this.FWCRMOrdersLeave.CommandText = "SELECT dbo.[FWCRMOrdersLeave].* FROM dbo.[FWCRMOrdersLeave]";
            this.FWCRMOrdersLeave.CommandTimeout = 30;
            this.FWCRMOrdersLeave.CommandType = System.Data.CommandType.Text;
            this.FWCRMOrdersLeave.DynamicTableName = false;
            this.FWCRMOrdersLeave.EEPAlias = null;
            this.FWCRMOrdersLeave.EncodingAfter = null;
            this.FWCRMOrdersLeave.EncodingBefore = "Windows-1252";
            this.FWCRMOrdersLeave.EncodingConvert = null;
            this.FWCRMOrdersLeave.InfoConnection = this.InfoConnection1;
            this.FWCRMOrdersLeave.MultiSetWhere = false;
            this.FWCRMOrdersLeave.Name = "FWCRMOrdersLeave";
            this.FWCRMOrdersLeave.NotificationAutoEnlist = false;
            this.FWCRMOrdersLeave.SecExcept = null;
            this.FWCRMOrdersLeave.SecFieldName = null;
            this.FWCRMOrdersLeave.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMOrdersLeave.SelectPaging = false;
            this.FWCRMOrdersLeave.SelectTop = 0;
            this.FWCRMOrdersLeave.SiteControl = false;
            this.FWCRMOrdersLeave.SiteFieldName = null;
            this.FWCRMOrdersLeave.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMOrdersLeave
            // 
            this.ucFWCRMOrdersLeave.AutoTrans = true;
            this.ucFWCRMOrdersLeave.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "LeaveNo";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "sup_no";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EmployerID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "EmployeeID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Gender";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "EffectDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "RenewDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LeaveDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Airline";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "AirDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "AirTime";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Arrears";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "Refund";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "iCurrency";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "flowflag";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "UserID";
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
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr6);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr7);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr8);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr9);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr10);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr11);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr12);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr13);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr14);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr15);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr16);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr17);
            this.ucFWCRMOrdersLeave.FieldAttrs.Add(fieldAttr18);
            this.ucFWCRMOrdersLeave.LogInfo = null;
            this.ucFWCRMOrdersLeave.Name = "ucFWCRMOrdersLeave";
            this.ucFWCRMOrdersLeave.RowAffectsCheck = true;
            this.ucFWCRMOrdersLeave.SelectCmd = this.FWCRMOrdersLeave;
            this.ucFWCRMOrdersLeave.SelectCmdForUpdate = null;
            this.ucFWCRMOrdersLeave.SendSQLCmd = true;
            this.ucFWCRMOrdersLeave.ServerModify = true;
            this.ucFWCRMOrdersLeave.ServerModifyGetMax = false;
            this.ucFWCRMOrdersLeave.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMOrdersLeave.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMOrdersLeave.UseTranscationScope = false;
            this.ucFWCRMOrdersLeave.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_FWCRMOrdersLeave
            // 
            this.View_FWCRMOrdersLeave.CacheConnection = false;
            this.View_FWCRMOrdersLeave.CommandText = "SELECT * FROM dbo.[FWCRMOrdersLeave]";
            this.View_FWCRMOrdersLeave.CommandTimeout = 30;
            this.View_FWCRMOrdersLeave.CommandType = System.Data.CommandType.Text;
            this.View_FWCRMOrdersLeave.DynamicTableName = false;
            this.View_FWCRMOrdersLeave.EEPAlias = null;
            this.View_FWCRMOrdersLeave.EncodingAfter = null;
            this.View_FWCRMOrdersLeave.EncodingBefore = "Windows-1252";
            this.View_FWCRMOrdersLeave.EncodingConvert = null;
            this.View_FWCRMOrdersLeave.InfoConnection = this.InfoConnection1;
            this.View_FWCRMOrdersLeave.MultiSetWhere = false;
            this.View_FWCRMOrdersLeave.Name = "View_FWCRMOrdersLeave";
            this.View_FWCRMOrdersLeave.NotificationAutoEnlist = false;
            this.View_FWCRMOrdersLeave.SecExcept = null;
            this.View_FWCRMOrdersLeave.SecFieldName = null;
            this.View_FWCRMOrdersLeave.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_FWCRMOrdersLeave.SelectPaging = false;
            this.View_FWCRMOrdersLeave.SelectTop = 0;
            this.View_FWCRMOrdersLeave.SiteControl = false;
            this.View_FWCRMOrdersLeave.SiteFieldName = null;
            this.View_FWCRMOrdersLeave.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoFWCRMAirline
            // 
            this.infoFWCRMAirline.CacheConnection = false;
            this.infoFWCRMAirline.CommandText = "SELECT * FROM dbo.[FWCRMAirline]";
            this.infoFWCRMAirline.CommandTimeout = 30;
            this.infoFWCRMAirline.CommandType = System.Data.CommandType.Text;
            this.infoFWCRMAirline.DynamicTableName = false;
            this.infoFWCRMAirline.EEPAlias = null;
            this.infoFWCRMAirline.EncodingAfter = null;
            this.infoFWCRMAirline.EncodingBefore = "Windows-1252";
            this.infoFWCRMAirline.EncodingConvert = null;
            this.infoFWCRMAirline.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AirlineID";
            this.infoFWCRMAirline.KeyFields.Add(keyItem1);
            this.infoFWCRMAirline.MultiSetWhere = false;
            this.infoFWCRMAirline.Name = "infoFWCRMAirline";
            this.infoFWCRMAirline.NotificationAutoEnlist = false;
            this.infoFWCRMAirline.SecExcept = null;
            this.infoFWCRMAirline.SecFieldName = null;
            this.infoFWCRMAirline.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoFWCRMAirline.SelectPaging = false;
            this.infoFWCRMAirline.SelectTop = 0;
            this.infoFWCRMAirline.SiteControl = false;
            this.infoFWCRMAirline.SiteFieldName = null;
            this.infoFWCRMAirline.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoFWCRMCurrency
            // 
            this.infoFWCRMCurrency.CacheConnection = false;
            this.infoFWCRMCurrency.CommandText = "SELECT * FROM dbo.[FWCRMCurrency]";
            this.infoFWCRMCurrency.CommandTimeout = 30;
            this.infoFWCRMCurrency.CommandType = System.Data.CommandType.Text;
            this.infoFWCRMCurrency.DynamicTableName = false;
            this.infoFWCRMCurrency.EEPAlias = null;
            this.infoFWCRMCurrency.EncodingAfter = null;
            this.infoFWCRMCurrency.EncodingBefore = "Windows-1252";
            this.infoFWCRMCurrency.EncodingConvert = null;
            this.infoFWCRMCurrency.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CurrencyID";
            this.infoFWCRMCurrency.KeyFields.Add(keyItem2);
            this.infoFWCRMCurrency.MultiSetWhere = false;
            this.infoFWCRMCurrency.Name = "infoFWCRMCurrency";
            this.infoFWCRMCurrency.NotificationAutoEnlist = false;
            this.infoFWCRMCurrency.SecExcept = null;
            this.infoFWCRMCurrency.SecFieldName = null;
            this.infoFWCRMCurrency.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoFWCRMCurrency.SelectPaging = false;
            this.infoFWCRMCurrency.SelectTop = 0;
            this.infoFWCRMCurrency.SiteControl = false;
            this.infoFWCRMCurrency.SiteFieldName = null;
            this.infoFWCRMCurrency.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrdersLeave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_FWCRMOrdersLeave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMAirline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMCurrency)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMOrdersLeave;
        private Srvtools.UpdateComponent ucFWCRMOrdersLeave;
        private Srvtools.InfoCommand View_FWCRMOrdersLeave;
        private Srvtools.InfoCommand infoFWCRMAirline;
        private Srvtools.InfoCommand infoFWCRMCurrency;
    }
}
