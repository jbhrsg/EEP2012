namespace rPettyCashRepoSortByAccountID
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.PettyCash = new Srvtools.InfoCommand(this.components);
            this.ucPettyCash = new Srvtools.UpdateComponent(this.components);
            this.View_PettyCash = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PettyCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PettyCash)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // PettyCash
            // 
            this.PettyCash.CacheConnection = false;
            this.PettyCash.CommandText = "SELECT dbo.[PettyCash].* FROM dbo.[PettyCash]";
            this.PettyCash.CommandTimeout = 30;
            this.PettyCash.CommandType = System.Data.CommandType.Text;
            this.PettyCash.DynamicTableName = false;
            this.PettyCash.EEPAlias = null;
            this.PettyCash.EncodingAfter = null;
            this.PettyCash.EncodingBefore = "Windows-1252";
            this.PettyCash.InfoConnection = this.InfoConnection1;
            this.PettyCash.MultiSetWhere = false;
            this.PettyCash.Name = "PettyCash";
            this.PettyCash.NotificationAutoEnlist = false;
            this.PettyCash.SecExcept = null;
            this.PettyCash.SecFieldName = null;
            this.PettyCash.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PettyCash.SelectPaging = false;
            this.PettyCash.SelectTop = 0;
            this.PettyCash.SiteControl = false;
            this.PettyCash.SiteFieldName = null;
            this.PettyCash.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPettyCash
            // 
            this.ucPettyCash.AutoTrans = true;
            this.ucPettyCash.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "PettyCashID";
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
            fieldAttr4.DataField = "ApplyOrg_NO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CostCenterID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ProofTypeID";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ProofNO";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AccountNotes";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "PettyCashAmt";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "PettyCashTax";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "PayTypeID";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "AccountYM";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "AccountID";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "Flowflag";
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
            this.ucPettyCash.FieldAttrs.Add(fieldAttr1);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr2);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr3);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr4);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr5);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr6);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr7);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr8);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr9);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr10);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr11);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr12);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr13);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr14);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr15);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr16);
            this.ucPettyCash.LogInfo = null;
            this.ucPettyCash.Name = null;
            this.ucPettyCash.RowAffectsCheck = true;
            this.ucPettyCash.SelectCmd = this.PettyCash;
            this.ucPettyCash.SelectCmdForUpdate = null;
            this.ucPettyCash.ServerModify = true;
            this.ucPettyCash.ServerModifyGetMax = false;
            this.ucPettyCash.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPettyCash.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPettyCash.UseTranscationScope = false;
            this.ucPettyCash.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_PettyCash
            // 
            this.View_PettyCash.CacheConnection = false;
            this.View_PettyCash.CommandText = "SELECT * FROM dbo.[PettyCash]";
            this.View_PettyCash.CommandTimeout = 30;
            this.View_PettyCash.CommandType = System.Data.CommandType.Text;
            this.View_PettyCash.DynamicTableName = false;
            this.View_PettyCash.EEPAlias = null;
            this.View_PettyCash.EncodingAfter = null;
            this.View_PettyCash.EncodingBefore = "Windows-1252";
            this.View_PettyCash.InfoConnection = this.InfoConnection1;
            this.View_PettyCash.MultiSetWhere = false;
            this.View_PettyCash.Name = "View_PettyCash";
            this.View_PettyCash.NotificationAutoEnlist = false;
            this.View_PettyCash.SecExcept = null;
            this.View_PettyCash.SecFieldName = null;
            this.View_PettyCash.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_PettyCash.SelectPaging = false;
            this.View_PettyCash.SelectTop = 0;
            this.View_PettyCash.SiteControl = false;
            this.View_PettyCash.SiteFieldName = null;
            this.View_PettyCash.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PettyCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PettyCash)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand PettyCash;
        private Srvtools.UpdateComponent ucPettyCash;
        private Srvtools.InfoCommand View_PettyCash;
    }
}
