namespace sglVoucher
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
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glVoucher = new Srvtools.InfoCommand(this.components);
            this.ucglVoucher = new Srvtools.UpdateComponent(this.components);
            this.autoVoucherNo = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glVoucher)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBGL";
            // 
            // glVoucher
            // 
            this.glVoucher.CacheConnection = false;
            this.glVoucher.CommandText = "SELECT dbo.[glVoucher].* FROM dbo.[glVoucher]";
            this.glVoucher.CommandTimeout = 30;
            this.glVoucher.CommandType = System.Data.CommandType.Text;
            this.glVoucher.DynamicTableName = false;
            this.glVoucher.EEPAlias = "JBGL";
            this.glVoucher.EncodingAfter = null;
            this.glVoucher.EncodingBefore = "Windows-1252";
            this.glVoucher.EncodingConvert = null;
            this.glVoucher.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.glVoucher.KeyFields.Add(keyItem1);
            this.glVoucher.MultiSetWhere = false;
            this.glVoucher.Name = "glVoucher";
            this.glVoucher.NotificationAutoEnlist = false;
            this.glVoucher.SecExcept = null;
            this.glVoucher.SecFieldName = null;
            this.glVoucher.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glVoucher.SelectPaging = false;
            this.glVoucher.SelectTop = 0;
            this.glVoucher.SiteControl = false;
            this.glVoucher.SiteFieldName = null;
            this.glVoucher.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglVoucher
            // 
            this.ucglVoucher.AutoTrans = true;
            this.ucglVoucher.ExceptJoin = false;
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
            fieldAttr3.DataField = "VoucherYear";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "VoucherNo";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "VoucherDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "VoucherType";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Item";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "BorrowLendType";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Acno";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "SubAcno";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CostCenterID";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Describe";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "Amt";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "SourseType";
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
            this.ucglVoucher.FieldAttrs.Add(fieldAttr1);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr2);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr3);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr4);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr5);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr6);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr7);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr8);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr9);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr10);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr11);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr12);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr13);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr14);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr15);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr16);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr17);
            this.ucglVoucher.FieldAttrs.Add(fieldAttr18);
            this.ucglVoucher.LogInfo = null;
            this.ucglVoucher.Name = "ucglVoucher";
            this.ucglVoucher.RowAffectsCheck = true;
            this.ucglVoucher.SelectCmd = this.glVoucher;
            this.ucglVoucher.SelectCmdForUpdate = null;
            this.ucglVoucher.SendSQLCmd = true;
            this.ucglVoucher.ServerModify = true;
            this.ucglVoucher.ServerModifyGetMax = false;
            this.ucglVoucher.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglVoucher.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglVoucher.UseTranscationScope = false;
            this.ucglVoucher.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // autoVoucherNo
            // 
            this.autoVoucherNo.Active = true;
            this.autoVoucherNo.AutoNoID = "VoucherNo";
            this.autoVoucherNo.Description = null;
            this.autoVoucherNo.GetFixed = "VoucherNoFixed()";
            this.autoVoucherNo.isNumFill = false;
            this.autoVoucherNo.Name = "autoVoucherNo";
            this.autoVoucherNo.Number = null;
            this.autoVoucherNo.NumDig = 3;
            this.autoVoucherNo.OldVersion = false;
            this.autoVoucherNo.OverFlow = true;
            this.autoVoucherNo.StartValue = 1;
            this.autoVoucherNo.Step = 1;
            this.autoVoucherNo.TargetColumn = "VoucherNo";
            this.autoVoucherNo.UpdateComp = this.ucglVoucher;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glVoucher)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glVoucher;
        private Srvtools.UpdateComponent ucglVoucher;
        private Srvtools.AutoNumber autoVoucherNo;
    }
}
