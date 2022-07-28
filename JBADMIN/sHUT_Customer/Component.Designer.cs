namespace sHUT_Customer
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
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr26 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr27 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr28 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr29 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr30 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr31 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr32 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr33 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr34 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr35 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr36 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_Customer = new Srvtools.InfoCommand(this.components);
            this.ucHUT_Customer = new Srvtools.UpdateComponent(this.components);
            this.HUT_CustomerContactRecord = new Srvtools.InfoCommand(this.components);
            this.ucHUT_CustomerContactRecord = new Srvtools.UpdateComponent(this.components);
            this.idCustomer_ContactRecord = new Srvtools.InfoDataSource(this.components);
            this.View_glVoucherMaster = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_CustomerContactRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_glVoucherMaster)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_Customer
            // 
            this.HUT_Customer.CacheConnection = false;
            this.HUT_Customer.CommandText = "SELECT * FROM HUT_Customer";
            this.HUT_Customer.CommandTimeout = 30;
            this.HUT_Customer.CommandType = System.Data.CommandType.Text;
            this.HUT_Customer.DynamicTableName = false;
            this.HUT_Customer.EEPAlias = "Hunter";
            this.HUT_Customer.EncodingAfter = null;
            this.HUT_Customer.EncodingBefore = "Windows-1252";
            this.HUT_Customer.EncodingConvert = null;
            this.HUT_Customer.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CustID";
            this.HUT_Customer.KeyFields.Add(keyItem1);
            this.HUT_Customer.MultiSetWhere = false;
            this.HUT_Customer.Name = "HUT_Customer";
            this.HUT_Customer.NotificationAutoEnlist = false;
            this.HUT_Customer.SecExcept = null;
            this.HUT_Customer.SecFieldName = null;
            this.HUT_Customer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_Customer.SelectPaging = false;
            this.HUT_Customer.SelectTop = 0;
            this.HUT_Customer.SiteControl = false;
            this.HUT_Customer.SiteFieldName = null;
            this.HUT_Customer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_Customer
            // 
            this.ucHUT_Customer.AutoTrans = true;
            this.ucHUT_Customer.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "VoucherNo";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "VoucherNoShow";
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
            fieldAttr4.DataField = "CompanyID";
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
            fieldAttr6.DataField = "VoucherID";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "OftenUsedAcno";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "OftenUsedEntryID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "UserID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "IsOpenBill";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "IsProfit";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "IsImport";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "RequisitionNO";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "POMasterAutoKey";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "importNo";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "BorrowAmount";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "LendAmount";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr7);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr8);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr9);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr10);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr11);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr12);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr13);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr14);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr15);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr16);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr17);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr18);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr19);
            this.ucHUT_Customer.LogInfo = null;
            this.ucHUT_Customer.Name = "ucglVoucherMaster";
            this.ucHUT_Customer.RowAffectsCheck = true;
            this.ucHUT_Customer.SelectCmd = this.HUT_Customer;
            this.ucHUT_Customer.SelectCmdForUpdate = null;
            this.ucHUT_Customer.SendSQLCmd = true;
            this.ucHUT_Customer.ServerModify = true;
            this.ucHUT_Customer.ServerModifyGetMax = false;
            this.ucHUT_Customer.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_Customer.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_Customer.UseTranscationScope = false;
            this.ucHUT_Customer.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // HUT_CustomerContactRecord
            // 
            this.HUT_CustomerContactRecord.CacheConnection = false;
            this.HUT_CustomerContactRecord.CommandText = "SELECT * FROM HUT_CustomerContactRecord";
            this.HUT_CustomerContactRecord.CommandTimeout = 30;
            this.HUT_CustomerContactRecord.CommandType = System.Data.CommandType.Text;
            this.HUT_CustomerContactRecord.DynamicTableName = false;
            this.HUT_CustomerContactRecord.EEPAlias = "Hunter";
            this.HUT_CustomerContactRecord.EncodingAfter = null;
            this.HUT_CustomerContactRecord.EncodingBefore = "Windows-1252";
            this.HUT_CustomerContactRecord.EncodingConvert = null;
            this.HUT_CustomerContactRecord.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CustID";
            this.HUT_CustomerContactRecord.KeyFields.Add(keyItem2);
            this.HUT_CustomerContactRecord.MultiSetWhere = false;
            this.HUT_CustomerContactRecord.Name = "HUT_CustomerContactRecord";
            this.HUT_CustomerContactRecord.NotificationAutoEnlist = false;
            this.HUT_CustomerContactRecord.SecExcept = null;
            this.HUT_CustomerContactRecord.SecFieldName = null;
            this.HUT_CustomerContactRecord.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_CustomerContactRecord.SelectPaging = false;
            this.HUT_CustomerContactRecord.SelectTop = 0;
            this.HUT_CustomerContactRecord.SiteControl = false;
            this.HUT_CustomerContactRecord.SiteFieldName = null;
            this.HUT_CustomerContactRecord.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_CustomerContactRecord
            // 
            this.ucHUT_CustomerContactRecord.AutoTrans = true;
            this.ucHUT_CustomerContactRecord.ExceptJoin = false;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "AutoKey";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CompanyID";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "VoucherID";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "VoucherNo";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "Item";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "BorrowLendType";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "Acno";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "SubAcno";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "DescribeID";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "CostCenterID";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "Describe";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "Amt";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "AmtShow";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "CreateBy";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            fieldAttr34.CheckNull = false;
            fieldAttr34.DataField = "CreateDate";
            fieldAttr34.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr34.DefaultValue = null;
            fieldAttr34.TrimLength = 0;
            fieldAttr34.UpdateEnable = true;
            fieldAttr34.WhereMode = true;
            fieldAttr35.CheckNull = false;
            fieldAttr35.DataField = "LastUpdateBy";
            fieldAttr35.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr35.DefaultValue = null;
            fieldAttr35.TrimLength = 0;
            fieldAttr35.UpdateEnable = true;
            fieldAttr35.WhereMode = true;
            fieldAttr36.CheckNull = false;
            fieldAttr36.DataField = "LastUpdateDate";
            fieldAttr36.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr36.DefaultValue = null;
            fieldAttr36.TrimLength = 0;
            fieldAttr36.UpdateEnable = true;
            fieldAttr36.WhereMode = true;
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr20);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr21);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr22);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr23);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr24);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr25);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr26);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr27);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr28);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr29);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr30);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr31);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr32);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr33);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr34);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr35);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr36);
            this.ucHUT_CustomerContactRecord.LogInfo = null;
            this.ucHUT_CustomerContactRecord.Name = "ucglVoucherDetails";
            this.ucHUT_CustomerContactRecord.RowAffectsCheck = true;
            this.ucHUT_CustomerContactRecord.SelectCmd = this.HUT_CustomerContactRecord;
            this.ucHUT_CustomerContactRecord.SelectCmdForUpdate = null;
            this.ucHUT_CustomerContactRecord.SendSQLCmd = true;
            this.ucHUT_CustomerContactRecord.ServerModify = true;
            this.ucHUT_CustomerContactRecord.ServerModifyGetMax = false;
            this.ucHUT_CustomerContactRecord.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_CustomerContactRecord.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_CustomerContactRecord.UseTranscationScope = false;
            this.ucHUT_CustomerContactRecord.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idCustomer_ContactRecord
            // 
            this.idCustomer_ContactRecord.Detail = this.HUT_CustomerContactRecord;
            columnItem1.FieldName = "CustID";
            this.idCustomer_ContactRecord.DetailColumns.Add(columnItem1);
            this.idCustomer_ContactRecord.DynamicTableName = false;
            this.idCustomer_ContactRecord.Master = this.HUT_Customer;
            columnItem2.FieldName = "CustID";
            this.idCustomer_ContactRecord.MasterColumns.Add(columnItem2);
            // 
            // View_glVoucherMaster
            // 
            this.View_glVoucherMaster.CacheConnection = false;
            this.View_glVoucherMaster.CommandText = "SELECT * FROM dbo.[glVoucherMaster]";
            this.View_glVoucherMaster.CommandTimeout = 30;
            this.View_glVoucherMaster.CommandType = System.Data.CommandType.Text;
            this.View_glVoucherMaster.DynamicTableName = false;
            this.View_glVoucherMaster.EEPAlias = null;
            this.View_glVoucherMaster.EncodingAfter = null;
            this.View_glVoucherMaster.EncodingBefore = "Windows-1252";
            this.View_glVoucherMaster.EncodingConvert = null;
            this.View_glVoucherMaster.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "VoucherNo";
            this.View_glVoucherMaster.KeyFields.Add(keyItem3);
            this.View_glVoucherMaster.MultiSetWhere = false;
            this.View_glVoucherMaster.Name = "View_glVoucherMaster";
            this.View_glVoucherMaster.NotificationAutoEnlist = false;
            this.View_glVoucherMaster.SecExcept = null;
            this.View_glVoucherMaster.SecFieldName = null;
            this.View_glVoucherMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_glVoucherMaster.SelectPaging = false;
            this.View_glVoucherMaster.SelectTop = 0;
            this.View_glVoucherMaster.SiteControl = false;
            this.View_glVoucherMaster.SiteFieldName = null;
            this.View_glVoucherMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_CustomerContactRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_glVoucherMaster)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_Customer;
        private Srvtools.UpdateComponent ucHUT_Customer;
        private Srvtools.InfoCommand HUT_CustomerContactRecord;
        private Srvtools.UpdateComponent ucHUT_CustomerContactRecord;
        private Srvtools.InfoDataSource idCustomer_ContactRecord;
        private Srvtools.InfoCommand View_glVoucherMaster;
    }
}
