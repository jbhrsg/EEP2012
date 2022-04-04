namespace sShortTermRepoAcct
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ShortTerm = new Srvtools.InfoCommand(this.components);
            this.ucShortTerm = new Srvtools.UpdateComponent(this.components);
            this.View_ShortTerm = new Srvtools.InfoCommand(this.components);
            this.Company = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ShortTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ShortTerm
            // 
            this.ShortTerm.CacheConnection = false;
            this.ShortTerm.CommandText = resources.GetString("ShortTerm.CommandText");
            this.ShortTerm.CommandTimeout = 30;
            this.ShortTerm.CommandType = System.Data.CommandType.Text;
            this.ShortTerm.DynamicTableName = false;
            this.ShortTerm.EEPAlias = null;
            this.ShortTerm.EncodingAfter = null;
            this.ShortTerm.EncodingBefore = "Windows-1252";
            this.ShortTerm.InfoConnection = this.InfoConnection1;
            this.ShortTerm.MultiSetWhere = false;
            this.ShortTerm.Name = "ShortTerm";
            this.ShortTerm.NotificationAutoEnlist = false;
            this.ShortTerm.SecExcept = null;
            this.ShortTerm.SecFieldName = null;
            this.ShortTerm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ShortTerm.SelectPaging = false;
            this.ShortTerm.SelectTop = 0;
            this.ShortTerm.SiteControl = false;
            this.ShortTerm.SiteFieldName = null;
            this.ShortTerm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucShortTerm
            // 
            this.ucShortTerm.AutoTrans = true;
            this.ucShortTerm.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ShortTermNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ShortTermGist";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ShortTermDescr";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "PlanPayDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ShortTermAmount";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "PayTypeID";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CheckDays";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CheckTitle";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "RequestDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ShortTermDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "RequisitionNO";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Flowflag";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "EmployeeID";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CompanyID";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "PayTo";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "IsSettleAccount";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "SettleAccountDate";
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
            this.ucShortTerm.FieldAttrs.Add(fieldAttr1);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr2);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr3);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr4);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr5);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr6);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr7);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr8);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr9);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr10);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr11);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr12);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr13);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr14);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr15);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr16);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr17);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr18);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr19);
            this.ucShortTerm.LogInfo = null;
            this.ucShortTerm.Name = "ucShortTerm";
            this.ucShortTerm.RowAffectsCheck = true;
            this.ucShortTerm.SelectCmd = this.ShortTerm;
            this.ucShortTerm.SelectCmdForUpdate = null;
            this.ucShortTerm.ServerModify = true;
            this.ucShortTerm.ServerModifyGetMax = false;
            this.ucShortTerm.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucShortTerm.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucShortTerm.UseTranscationScope = false;
            this.ucShortTerm.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ShortTerm
            // 
            this.View_ShortTerm.CacheConnection = false;
            this.View_ShortTerm.CommandText = "SELECT * FROM dbo.[ShortTerm]";
            this.View_ShortTerm.CommandTimeout = 30;
            this.View_ShortTerm.CommandType = System.Data.CommandType.Text;
            this.View_ShortTerm.DynamicTableName = false;
            this.View_ShortTerm.EEPAlias = null;
            this.View_ShortTerm.EncodingAfter = null;
            this.View_ShortTerm.EncodingBefore = "Windows-1252";
            this.View_ShortTerm.InfoConnection = this.InfoConnection1;
            this.View_ShortTerm.MultiSetWhere = false;
            this.View_ShortTerm.Name = "View_ShortTerm";
            this.View_ShortTerm.NotificationAutoEnlist = false;
            this.View_ShortTerm.SecExcept = null;
            this.View_ShortTerm.SecFieldName = null;
            this.View_ShortTerm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ShortTerm.SelectPaging = false;
            this.View_ShortTerm.SelectTop = 0;
            this.View_ShortTerm.SiteControl = false;
            this.View_ShortTerm.SiteFieldName = null;
            this.View_ShortTerm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Company
            // 
            this.Company.CacheConnection = false;
            this.Company.CommandText = "select Company.CompanyID,Company.CompanyName from Company";
            this.Company.CommandTimeout = 30;
            this.Company.CommandType = System.Data.CommandType.Text;
            this.Company.DynamicTableName = false;
            this.Company.EEPAlias = null;
            this.Company.EncodingAfter = null;
            this.Company.EncodingBefore = "Windows-1252";
            this.Company.InfoConnection = this.InfoConnection1;
            this.Company.MultiSetWhere = false;
            this.Company.Name = "Company";
            this.Company.NotificationAutoEnlist = false;
            this.Company.SecExcept = null;
            this.Company.SecFieldName = null;
            this.Company.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Company.SelectPaging = false;
            this.Company.SelectTop = 0;
            this.Company.SiteControl = false;
            this.Company.SiteFieldName = null;
            this.Company.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ShortTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ShortTerm;
        private Srvtools.UpdateComponent ucShortTerm;
        private Srvtools.InfoCommand View_ShortTerm;
        private Srvtools.InfoCommand Company;
    }
}
