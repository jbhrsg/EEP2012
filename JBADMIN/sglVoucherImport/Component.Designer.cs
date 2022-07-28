namespace sglVoucherImport
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
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.Service service1 = new Srvtools.Service();
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glVoucherDetails = new Srvtools.InfoCommand(this.components);
            this.ucglVoucherDetails = new Srvtools.UpdateComponent(this.components);
            this.TheServiceManager = new Srvtools.ServiceManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glVoucherDetails)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // glVoucherDetails
            // 
            this.glVoucherDetails.CacheConnection = false;
            this.glVoucherDetails.CommandText = resources.GetString("glVoucherDetails.CommandText");
            this.glVoucherDetails.CommandTimeout = 30;
            this.glVoucherDetails.CommandType = System.Data.CommandType.Text;
            this.glVoucherDetails.DynamicTableName = false;
            this.glVoucherDetails.EEPAlias = null;
            this.glVoucherDetails.EncodingAfter = null;
            this.glVoucherDetails.EncodingBefore = "Windows-1252";
            this.glVoucherDetails.EncodingConvert = null;
            this.glVoucherDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "VoucherNo";
            keyItem2.KeyName = "Item";
            this.glVoucherDetails.KeyFields.Add(keyItem1);
            this.glVoucherDetails.KeyFields.Add(keyItem2);
            this.glVoucherDetails.MultiSetWhere = false;
            this.glVoucherDetails.Name = "glVoucherDetails";
            this.glVoucherDetails.NotificationAutoEnlist = false;
            this.glVoucherDetails.SecExcept = null;
            this.glVoucherDetails.SecFieldName = null;
            this.glVoucherDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glVoucherDetails.SelectPaging = false;
            this.glVoucherDetails.SelectTop = 0;
            this.glVoucherDetails.SiteControl = false;
            this.glVoucherDetails.SiteFieldName = null;
            this.glVoucherDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglVoucherDetails
            // 
            this.ucglVoucherDetails.AutoTrans = true;
            this.ucglVoucherDetails.ExceptJoin = false;
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
            fieldAttr3.DataField = "VoucherID";
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
            fieldAttr5.DataField = "Item";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "BorrowLendType";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Acno";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "SubAcno";
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
            fieldAttr10.DataField = "CostCenterID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Describe";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Amt";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "AmtShow";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateBy";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LastUpdateBy";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "LastUpdateDate";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr1);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr2);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr3);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr4);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr5);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr6);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr7);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr8);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr9);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr10);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr11);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr12);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr13);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr14);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr15);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr16);
            this.ucglVoucherDetails.FieldAttrs.Add(fieldAttr17);
            this.ucglVoucherDetails.LogInfo = null;
            this.ucglVoucherDetails.Name = "ucglVoucherDetails";
            this.ucglVoucherDetails.RowAffectsCheck = true;
            this.ucglVoucherDetails.SelectCmd = this.glVoucherDetails;
            this.ucglVoucherDetails.SelectCmdForUpdate = null;
            this.ucglVoucherDetails.SendSQLCmd = true;
            this.ucglVoucherDetails.ServerModify = true;
            this.ucglVoucherDetails.ServerModifyGetMax = false;
            this.ucglVoucherDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglVoucherDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglVoucherDetails.UseTranscationScope = false;
            this.ucglVoucherDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucglVoucherDetails.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucglVoucherDetails_BeforeInsert);
            // 
            // TheServiceManager
            // 
            service1.DelegateName = "DataValidate";
            service1.NonLogin = false;
            service1.ServiceName = "DataValidate";
            service2.DelegateName = "GetOldSetting";
            service2.NonLogin = false;
            service2.ServiceName = "GetOldSetting";
            service3.DelegateName = "ExcelFileImport";
            service3.NonLogin = false;
            service3.ServiceName = "ExcelFileImport";
            this.TheServiceManager.ServiceCollection.Add(service1);
            this.TheServiceManager.ServiceCollection.Add(service2);
            this.TheServiceManager.ServiceCollection.Add(service3);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glVoucherDetails)).EndInit();

        }

        #endregion

        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glVoucherDetails;
        private Srvtools.InfoCommand cb_glVoucherDetails;
        private Srvtools.UpdateComponent ucglVoucherDetails;
        private Srvtools.ServiceManager TheServiceManager;
    }
}
