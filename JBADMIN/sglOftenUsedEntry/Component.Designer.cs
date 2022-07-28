namespace sglOftenUsedEntry
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glOftenUsedEntry = new Srvtools.InfoCommand(this.components);
            this.ucglOftenUsedEntry = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glOftenUsedEntry)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // glOftenUsedEntry
            // 
            this.glOftenUsedEntry.CacheConnection = false;
            this.glOftenUsedEntry.CommandText = "SELECT dbo.[glOftenUsedEntry].* FROM dbo.[glOftenUsedEntry]";
            this.glOftenUsedEntry.CommandTimeout = 30;
            this.glOftenUsedEntry.CommandType = System.Data.CommandType.Text;
            this.glOftenUsedEntry.DynamicTableName = false;
            this.glOftenUsedEntry.EEPAlias = "";
            this.glOftenUsedEntry.EncodingAfter = null;
            this.glOftenUsedEntry.EncodingBefore = "Windows-1252";
            this.glOftenUsedEntry.EncodingConvert = null;
            this.glOftenUsedEntry.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.glOftenUsedEntry.KeyFields.Add(keyItem1);
            this.glOftenUsedEntry.MultiSetWhere = false;
            this.glOftenUsedEntry.Name = "glOftenUsedEntry";
            this.glOftenUsedEntry.NotificationAutoEnlist = false;
            this.glOftenUsedEntry.SecExcept = null;
            this.glOftenUsedEntry.SecFieldName = null;
            this.glOftenUsedEntry.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glOftenUsedEntry.SelectPaging = false;
            this.glOftenUsedEntry.SelectTop = 0;
            this.glOftenUsedEntry.SiteControl = false;
            this.glOftenUsedEntry.SiteFieldName = null;
            this.glOftenUsedEntry.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglOftenUsedEntry
            // 
            this.ucglOftenUsedEntry.AutoTrans = true;
            this.ucglOftenUsedEntry.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CompanyID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "VoucherType";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Item";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "BorrowLendType";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Acno";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "SubAcno";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CostCenterID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "DescribeID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Describe";
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
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpdateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr1);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr2);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr3);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr4);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr5);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr6);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr7);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr8);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr9);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr10);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr11);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr12);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr13);
            this.ucglOftenUsedEntry.FieldAttrs.Add(fieldAttr14);
            this.ucglOftenUsedEntry.LogInfo = null;
            this.ucglOftenUsedEntry.Name = "ucglOftenUsedEntry";
            this.ucglOftenUsedEntry.RowAffectsCheck = true;
            this.ucglOftenUsedEntry.SelectCmd = this.glOftenUsedEntry;
            this.ucglOftenUsedEntry.SelectCmdForUpdate = null;
            this.ucglOftenUsedEntry.SendSQLCmd = true;
            this.ucglOftenUsedEntry.ServerModify = true;
            this.ucglOftenUsedEntry.ServerModifyGetMax = false;
            this.ucglOftenUsedEntry.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglOftenUsedEntry.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglOftenUsedEntry.UseTranscationScope = false;
            this.ucglOftenUsedEntry.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glOftenUsedEntry)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glOftenUsedEntry;
        private Srvtools.UpdateComponent ucglOftenUsedEntry;
    }
}
