namespace sglOftenUsedEntryM
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
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
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glOftenUsedEntryM = new Srvtools.InfoCommand(this.components);
            this.ucglOftenUsedEntryM = new Srvtools.UpdateComponent(this.components);
            this.glOftenUsedEntryD = new Srvtools.InfoCommand(this.components);
            this.ucglOftenUsedEntryD = new Srvtools.UpdateComponent(this.components);
            this.idglOftenUsedEntryM_glOftenUsedEntryD = new Srvtools.InfoDataSource(this.components);
            this.autoAutoKey = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glOftenUsedEntryM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glOftenUsedEntryD)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // glOftenUsedEntryM
            // 
            this.glOftenUsedEntryM.CacheConnection = false;
            this.glOftenUsedEntryM.CommandText = resources.GetString("glOftenUsedEntryM.CommandText");
            this.glOftenUsedEntryM.CommandTimeout = 30;
            this.glOftenUsedEntryM.CommandType = System.Data.CommandType.Text;
            this.glOftenUsedEntryM.DynamicTableName = false;
            this.glOftenUsedEntryM.EEPAlias = "JBADMIN";
            this.glOftenUsedEntryM.EncodingAfter = null;
            this.glOftenUsedEntryM.EncodingBefore = "Windows-1252";
            this.glOftenUsedEntryM.EncodingConvert = null;
            this.glOftenUsedEntryM.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.glOftenUsedEntryM.KeyFields.Add(keyItem1);
            this.glOftenUsedEntryM.MultiSetWhere = false;
            this.glOftenUsedEntryM.Name = "glOftenUsedEntryM";
            this.glOftenUsedEntryM.NotificationAutoEnlist = false;
            this.glOftenUsedEntryM.SecExcept = null;
            this.glOftenUsedEntryM.SecFieldName = null;
            this.glOftenUsedEntryM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glOftenUsedEntryM.SelectPaging = false;
            this.glOftenUsedEntryM.SelectTop = 0;
            this.glOftenUsedEntryM.SiteControl = false;
            this.glOftenUsedEntryM.SiteFieldName = null;
            this.glOftenUsedEntryM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglOftenUsedEntryM
            // 
            this.ucglOftenUsedEntryM.AutoTrans = true;
            this.ucglOftenUsedEntryM.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "UsedName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CompanyID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "VoucherType";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucglOftenUsedEntryM.FieldAttrs.Add(fieldAttr1);
            this.ucglOftenUsedEntryM.FieldAttrs.Add(fieldAttr2);
            this.ucglOftenUsedEntryM.FieldAttrs.Add(fieldAttr3);
            this.ucglOftenUsedEntryM.FieldAttrs.Add(fieldAttr4);
            this.ucglOftenUsedEntryM.FieldAttrs.Add(fieldAttr5);
            this.ucglOftenUsedEntryM.FieldAttrs.Add(fieldAttr6);
            this.ucglOftenUsedEntryM.LogInfo = null;
            this.ucglOftenUsedEntryM.Name = "ucglOftenUsedEntryM";
            this.ucglOftenUsedEntryM.RowAffectsCheck = true;
            this.ucglOftenUsedEntryM.SelectCmd = this.glOftenUsedEntryM;
            this.ucglOftenUsedEntryM.SelectCmdForUpdate = null;
            this.ucglOftenUsedEntryM.SendSQLCmd = true;
            this.ucglOftenUsedEntryM.ServerModify = true;
            this.ucglOftenUsedEntryM.ServerModifyGetMax = false;
            this.ucglOftenUsedEntryM.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglOftenUsedEntryM.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglOftenUsedEntryM.UseTranscationScope = false;
            this.ucglOftenUsedEntryM.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // glOftenUsedEntryD
            // 
            this.glOftenUsedEntryD.CacheConnection = false;
            this.glOftenUsedEntryD.CommandText = resources.GetString("glOftenUsedEntryD.CommandText");
            this.glOftenUsedEntryD.CommandTimeout = 30;
            this.glOftenUsedEntryD.CommandType = System.Data.CommandType.Text;
            this.glOftenUsedEntryD.DynamicTableName = false;
            this.glOftenUsedEntryD.EEPAlias = "JBADMIN";
            this.glOftenUsedEntryD.EncodingAfter = null;
            this.glOftenUsedEntryD.EncodingBefore = "Windows-1252";
            this.glOftenUsedEntryD.EncodingConvert = null;
            this.glOftenUsedEntryD.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "iAutoKey";
            this.glOftenUsedEntryD.KeyFields.Add(keyItem2);
            this.glOftenUsedEntryD.MultiSetWhere = false;
            this.glOftenUsedEntryD.Name = "glOftenUsedEntryD";
            this.glOftenUsedEntryD.NotificationAutoEnlist = false;
            this.glOftenUsedEntryD.SecExcept = null;
            this.glOftenUsedEntryD.SecFieldName = null;
            this.glOftenUsedEntryD.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glOftenUsedEntryD.SelectPaging = false;
            this.glOftenUsedEntryD.SelectTop = 0;
            this.glOftenUsedEntryD.SiteControl = false;
            this.glOftenUsedEntryD.SiteFieldName = null;
            this.glOftenUsedEntryD.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglOftenUsedEntryD
            // 
            this.ucglOftenUsedEntryD.AutoTrans = true;
            this.ucglOftenUsedEntryD.ExceptJoin = false;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AutoKey";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AutoKeyM";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Item";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "BorrowLendType";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Acno";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "SubAcno";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CostCenterID";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "DescribeID";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "Describe";
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
            fieldAttr17.DataField = "LastUpdateBy";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "LastUpdateDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr7);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr8);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr9);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr10);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr11);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr12);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr13);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr14);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr15);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr16);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr17);
            this.ucglOftenUsedEntryD.FieldAttrs.Add(fieldAttr18);
            this.ucglOftenUsedEntryD.LogInfo = null;
            this.ucglOftenUsedEntryD.Name = "ucglOftenUsedEntryD";
            this.ucglOftenUsedEntryD.RowAffectsCheck = true;
            this.ucglOftenUsedEntryD.SelectCmd = this.glOftenUsedEntryD;
            this.ucglOftenUsedEntryD.SelectCmdForUpdate = null;
            this.ucglOftenUsedEntryD.SendSQLCmd = true;
            this.ucglOftenUsedEntryD.ServerModify = true;
            this.ucglOftenUsedEntryD.ServerModifyGetMax = false;
            this.ucglOftenUsedEntryD.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglOftenUsedEntryD.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglOftenUsedEntryD.UseTranscationScope = false;
            this.ucglOftenUsedEntryD.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idglOftenUsedEntryM_glOftenUsedEntryD
            // 
            this.idglOftenUsedEntryM_glOftenUsedEntryD.Detail = this.glOftenUsedEntryD;
            columnItem1.FieldName = "AutoKey";
            this.idglOftenUsedEntryM_glOftenUsedEntryD.DetailColumns.Add(columnItem1);
            this.idglOftenUsedEntryM_glOftenUsedEntryD.DynamicTableName = false;
            this.idglOftenUsedEntryM_glOftenUsedEntryD.Master = this.glOftenUsedEntryM;
            columnItem2.FieldName = "AutoKey";
            this.idglOftenUsedEntryM_glOftenUsedEntryD.MasterColumns.Add(columnItem2);
            // 
            // autoAutoKey
            // 
            this.autoAutoKey.Active = true;
            this.autoAutoKey.AutoNoID = "UsedEntryAutoKey";
            this.autoAutoKey.Description = null;
            this.autoAutoKey.GetFixed = "";
            this.autoAutoKey.isNumFill = false;
            this.autoAutoKey.Name = "autoAutoKey";
            this.autoAutoKey.Number = null;
            this.autoAutoKey.NumDig = 8;
            this.autoAutoKey.OldVersion = false;
            this.autoAutoKey.OverFlow = true;
            this.autoAutoKey.StartValue = 1;
            this.autoAutoKey.Step = 1;
            this.autoAutoKey.TargetColumn = "AutoKey";
            this.autoAutoKey.UpdateComp = this.ucglOftenUsedEntryM;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glOftenUsedEntryM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glOftenUsedEntryD)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glOftenUsedEntryM;
        private Srvtools.UpdateComponent ucglOftenUsedEntryM;
        private Srvtools.InfoCommand glOftenUsedEntryD;
        private Srvtools.UpdateComponent ucglOftenUsedEntryD;
        private Srvtools.InfoDataSource idglOftenUsedEntryM_glOftenUsedEntryD;
        private Srvtools.AutoNumber autoAutoKey;
    }
}
