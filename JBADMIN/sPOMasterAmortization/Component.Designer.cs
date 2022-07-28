namespace sPOMasterAmortization
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
            Srvtools.Service service1 = new Srvtools.Service();
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.POMasterAmortization = new Srvtools.InfoCommand(this.components);
            this.ucPOMasterAmortization = new Srvtools.UpdateComponent(this.components);
            this.POMasterAmortizationV = new Srvtools.InfoCommand(this.components);
            this.ucPOMasterAmortizationV = new Srvtools.UpdateComponent(this.components);
            this.idPOMasterAmortization_POMasterAmortizationV = new Srvtools.InfoDataSource(this.components);
            this.infoPOMaster = new Srvtools.InfoCommand(this.components);
            this.infoAssetMaster = new Srvtools.InfoCommand(this.components);
            this.infoglVoucher = new Srvtools.InfoCommand(this.components);
            this.infoglAccountItem = new Srvtools.InfoCommand(this.components);
            this.glCompany = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.POMasterAmortization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.POMasterAmortizationV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPOMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAssetMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglAccountItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCompany)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "UpdatePOMasterAmortizationVIsActive";
            service1.NonLogin = false;
            service1.ServiceName = "UpdatePOMasterAmortizationVIsActive";
            service2.DelegateName = "procInsertPOMasterVoucherM";
            service2.NonLogin = false;
            service2.ServiceName = "procInsertPOMasterVoucherM";
            service3.DelegateName = "AmortizationAutoExcel";
            service3.NonLogin = false;
            service3.ServiceName = "AmortizationAutoExcel";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // POMasterAmortization
            // 
            this.POMasterAmortization.CacheConnection = false;
            this.POMasterAmortization.CommandText = resources.GetString("POMasterAmortization.CommandText");
            this.POMasterAmortization.CommandTimeout = 30;
            this.POMasterAmortization.CommandType = System.Data.CommandType.Text;
            this.POMasterAmortization.DynamicTableName = false;
            this.POMasterAmortization.EEPAlias = null;
            this.POMasterAmortization.EncodingAfter = null;
            this.POMasterAmortization.EncodingBefore = "Windows-1252";
            this.POMasterAmortization.EncodingConvert = null;
            this.POMasterAmortization.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.POMasterAmortization.KeyFields.Add(keyItem1);
            this.POMasterAmortization.MultiSetWhere = false;
            this.POMasterAmortization.Name = "POMasterAmortization";
            this.POMasterAmortization.NotificationAutoEnlist = false;
            this.POMasterAmortization.SecExcept = null;
            this.POMasterAmortization.SecFieldName = null;
            this.POMasterAmortization.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.POMasterAmortization.SelectPaging = false;
            this.POMasterAmortization.SelectTop = 0;
            this.POMasterAmortization.SiteControl = false;
            this.POMasterAmortization.SiteFieldName = null;
            this.POMasterAmortization.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPOMasterAmortization
            // 
            this.ucPOMasterAmortization.AutoTrans = true;
            this.ucPOMasterAmortization.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "PONO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "AssetID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "AssetName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "AssetQty";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AssetUnit";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AssetGetDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AssetGetAmt";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "UsefulYears";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ScrapValue";
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
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr1);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr2);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr3);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr4);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr5);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr6);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr7);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr8);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr9);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr10);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr11);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr12);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr13);
            this.ucPOMasterAmortization.FieldAttrs.Add(fieldAttr14);
            this.ucPOMasterAmortization.LogInfo = null;
            this.ucPOMasterAmortization.Name = "ucPOMasterAmortization";
            this.ucPOMasterAmortization.RowAffectsCheck = true;
            this.ucPOMasterAmortization.SelectCmd = this.POMasterAmortization;
            this.ucPOMasterAmortization.SelectCmdForUpdate = null;
            this.ucPOMasterAmortization.SendSQLCmd = true;
            this.ucPOMasterAmortization.ServerModify = true;
            this.ucPOMasterAmortization.ServerModifyGetMax = false;
            this.ucPOMasterAmortization.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPOMasterAmortization.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPOMasterAmortization.UseTranscationScope = false;
            this.ucPOMasterAmortization.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucPOMasterAmortization.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucPOMasterAmortization_BeforeInsert);
            this.ucPOMasterAmortization.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucPOMasterAmortization_BeforeModify);
            // 
            // POMasterAmortizationV
            // 
            this.POMasterAmortizationV.CacheConnection = false;
            this.POMasterAmortizationV.CommandText = "select  * from View_POMasterAmortizationV";
            this.POMasterAmortizationV.CommandTimeout = 30;
            this.POMasterAmortizationV.CommandType = System.Data.CommandType.Text;
            this.POMasterAmortizationV.DynamicTableName = false;
            this.POMasterAmortizationV.EEPAlias = null;
            this.POMasterAmortizationV.EncodingAfter = null;
            this.POMasterAmortizationV.EncodingBefore = "Windows-1252";
            this.POMasterAmortizationV.EncodingConvert = null;
            this.POMasterAmortizationV.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.POMasterAmortizationV.KeyFields.Add(keyItem2);
            this.POMasterAmortizationV.MultiSetWhere = false;
            this.POMasterAmortizationV.Name = "POMasterAmortizationV";
            this.POMasterAmortizationV.NotificationAutoEnlist = false;
            this.POMasterAmortizationV.SecExcept = null;
            this.POMasterAmortizationV.SecFieldName = null;
            this.POMasterAmortizationV.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.POMasterAmortizationV.SelectPaging = false;
            this.POMasterAmortizationV.SelectTop = 0;
            this.POMasterAmortizationV.SiteControl = false;
            this.POMasterAmortizationV.SiteFieldName = null;
            this.POMasterAmortizationV.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPOMasterAmortizationV
            // 
            this.ucPOMasterAmortizationV.AutoTrans = true;
            this.ucPOMasterAmortizationV.ExceptJoin = false;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "AutoKey";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "MAutoKey";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "PONO";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "VoucherNo";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "CreateBy";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CreateDate";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "LastUpdateBy";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "LastUpdateDate";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            this.ucPOMasterAmortizationV.FieldAttrs.Add(fieldAttr15);
            this.ucPOMasterAmortizationV.FieldAttrs.Add(fieldAttr16);
            this.ucPOMasterAmortizationV.FieldAttrs.Add(fieldAttr17);
            this.ucPOMasterAmortizationV.FieldAttrs.Add(fieldAttr18);
            this.ucPOMasterAmortizationV.FieldAttrs.Add(fieldAttr19);
            this.ucPOMasterAmortizationV.FieldAttrs.Add(fieldAttr20);
            this.ucPOMasterAmortizationV.FieldAttrs.Add(fieldAttr21);
            this.ucPOMasterAmortizationV.FieldAttrs.Add(fieldAttr22);
            this.ucPOMasterAmortizationV.LogInfo = null;
            this.ucPOMasterAmortizationV.Name = "ucPOMasterAmortizationV";
            this.ucPOMasterAmortizationV.RowAffectsCheck = true;
            this.ucPOMasterAmortizationV.SelectCmd = this.POMasterAmortizationV;
            this.ucPOMasterAmortizationV.SelectCmdForUpdate = null;
            this.ucPOMasterAmortizationV.SendSQLCmd = true;
            this.ucPOMasterAmortizationV.ServerModify = true;
            this.ucPOMasterAmortizationV.ServerModifyGetMax = false;
            this.ucPOMasterAmortizationV.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPOMasterAmortizationV.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPOMasterAmortizationV.UseTranscationScope = false;
            this.ucPOMasterAmortizationV.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idPOMasterAmortization_POMasterAmortizationV
            // 
            this.idPOMasterAmortization_POMasterAmortizationV.Detail = this.POMasterAmortizationV;
            columnItem1.FieldName = "MAutoKey";
            this.idPOMasterAmortization_POMasterAmortizationV.DetailColumns.Add(columnItem1);
            this.idPOMasterAmortization_POMasterAmortizationV.DynamicTableName = false;
            this.idPOMasterAmortization_POMasterAmortizationV.Master = this.POMasterAmortization;
            columnItem2.FieldName = "AutoKey";
            this.idPOMasterAmortization_POMasterAmortizationV.MasterColumns.Add(columnItem2);
            // 
            // infoPOMaster
            // 
            this.infoPOMaster.CacheConnection = false;
            this.infoPOMaster.CommandText = "select p.PONO,p.Description,sum(d.PurPrice) as PurPrice\r\nfrom POMaster p\r\n\tinner " +
    "join PODelivery d on p.PONO=d.PONO\r\n\tgroup by p.PONO,p.Description";
            this.infoPOMaster.CommandTimeout = 30;
            this.infoPOMaster.CommandType = System.Data.CommandType.Text;
            this.infoPOMaster.DynamicTableName = false;
            this.infoPOMaster.EEPAlias = null;
            this.infoPOMaster.EncodingAfter = null;
            this.infoPOMaster.EncodingBefore = "Windows-1252";
            this.infoPOMaster.EncodingConvert = null;
            this.infoPOMaster.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "PONO";
            this.infoPOMaster.KeyFields.Add(keyItem3);
            this.infoPOMaster.MultiSetWhere = false;
            this.infoPOMaster.Name = "infoPOMaster";
            this.infoPOMaster.NotificationAutoEnlist = false;
            this.infoPOMaster.SecExcept = null;
            this.infoPOMaster.SecFieldName = null;
            this.infoPOMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoPOMaster.SelectPaging = false;
            this.infoPOMaster.SelectTop = 0;
            this.infoPOMaster.SiteControl = false;
            this.infoPOMaster.SiteFieldName = null;
            this.infoPOMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoAssetMaster
            // 
            this.infoAssetMaster.CacheConnection = false;
            this.infoAssetMaster.CommandText = "select a.AssetID,a.AssetName,a.AssetGetDate,a.AssetSpecs\r\nfrom AssetMaster a\r\n\t";
            this.infoAssetMaster.CommandTimeout = 30;
            this.infoAssetMaster.CommandType = System.Data.CommandType.Text;
            this.infoAssetMaster.DynamicTableName = false;
            this.infoAssetMaster.EEPAlias = null;
            this.infoAssetMaster.EncodingAfter = null;
            this.infoAssetMaster.EncodingBefore = "Windows-1252";
            this.infoAssetMaster.EncodingConvert = null;
            this.infoAssetMaster.InfoConnection = this.InfoConnection1;
            this.infoAssetMaster.MultiSetWhere = false;
            this.infoAssetMaster.Name = "infoAssetMaster";
            this.infoAssetMaster.NotificationAutoEnlist = false;
            this.infoAssetMaster.SecExcept = null;
            this.infoAssetMaster.SecFieldName = null;
            this.infoAssetMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoAssetMaster.SelectPaging = false;
            this.infoAssetMaster.SelectTop = 0;
            this.infoAssetMaster.SiteControl = false;
            this.infoAssetMaster.SiteFieldName = null;
            this.infoAssetMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoglVoucher
            // 
            this.infoglVoucher.CacheConnection = false;
            this.infoglVoucher.CommandText = "select top 100 * from View_AmortizationglVoucher\r\norder by VoucherDate desc";
            this.infoglVoucher.CommandTimeout = 30;
            this.infoglVoucher.CommandType = System.Data.CommandType.Text;
            this.infoglVoucher.DynamicTableName = false;
            this.infoglVoucher.EEPAlias = null;
            this.infoglVoucher.EncodingAfter = null;
            this.infoglVoucher.EncodingBefore = "Windows-1252";
            this.infoglVoucher.EncodingConvert = null;
            this.infoglVoucher.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "VoucherNo";
            this.infoglVoucher.KeyFields.Add(keyItem4);
            this.infoglVoucher.MultiSetWhere = false;
            this.infoglVoucher.Name = "infoglVoucher";
            this.infoglVoucher.NotificationAutoEnlist = false;
            this.infoglVoucher.SecExcept = null;
            this.infoglVoucher.SecFieldName = null;
            this.infoglVoucher.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoglVoucher.SelectPaging = false;
            this.infoglVoucher.SelectTop = 0;
            this.infoglVoucher.SiteControl = false;
            this.infoglVoucher.SiteFieldName = null;
            this.infoglVoucher.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoglAccountItem
            // 
            this.infoglAccountItem.CacheConnection = false;
            this.infoglAccountItem.CommandText = "SELECT CompanyID,CAcnoSubAcno,AcnoName\r\nFROM View_glAccountItem\r\norder by Company" +
    "ID,CAcnoSubAcno";
            this.infoglAccountItem.CommandTimeout = 30;
            this.infoglAccountItem.CommandType = System.Data.CommandType.Text;
            this.infoglAccountItem.DynamicTableName = false;
            this.infoglAccountItem.EEPAlias = null;
            this.infoglAccountItem.EncodingAfter = null;
            this.infoglAccountItem.EncodingBefore = "Windows-1252";
            this.infoglAccountItem.EncodingConvert = null;
            this.infoglAccountItem.InfoConnection = this.InfoConnection1;
            this.infoglAccountItem.MultiSetWhere = false;
            this.infoglAccountItem.Name = "infoglAccountItem";
            this.infoglAccountItem.NotificationAutoEnlist = false;
            this.infoglAccountItem.SecExcept = null;
            this.infoglAccountItem.SecFieldName = null;
            this.infoglAccountItem.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoglAccountItem.SelectPaging = false;
            this.infoglAccountItem.SelectTop = 0;
            this.infoglAccountItem.SiteControl = false;
            this.infoglAccountItem.SiteFieldName = null;
            this.infoglAccountItem.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // glCompany
            // 
            this.glCompany.CacheConnection = false;
            this.glCompany.CommandText = "SELECT CompanyID,Cast(CompanyID as nvarchar(1))+\'-\'+CompanyName as CompanyName\r\nF" +
    "ROM glCompany";
            this.glCompany.CommandTimeout = 30;
            this.glCompany.CommandType = System.Data.CommandType.Text;
            this.glCompany.DynamicTableName = false;
            this.glCompany.EEPAlias = "";
            this.glCompany.EncodingAfter = null;
            this.glCompany.EncodingBefore = "Windows-1252";
            this.glCompany.EncodingConvert = null;
            this.glCompany.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "CompanyID";
            this.glCompany.KeyFields.Add(keyItem5);
            this.glCompany.MultiSetWhere = false;
            this.glCompany.Name = "glCompany";
            this.glCompany.NotificationAutoEnlist = false;
            this.glCompany.SecExcept = null;
            this.glCompany.SecFieldName = null;
            this.glCompany.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glCompany.SelectPaging = false;
            this.glCompany.SelectTop = 0;
            this.glCompany.SiteControl = false;
            this.glCompany.SiteFieldName = null;
            this.glCompany.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.POMasterAmortization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.POMasterAmortizationV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPOMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAssetMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglAccountItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCompany)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand POMasterAmortization;
        private Srvtools.UpdateComponent ucPOMasterAmortization;
        private Srvtools.InfoCommand POMasterAmortizationV;
        private Srvtools.UpdateComponent ucPOMasterAmortizationV;
        private Srvtools.InfoDataSource idPOMasterAmortization_POMasterAmortizationV;
        private Srvtools.InfoCommand infoPOMaster;
        private Srvtools.InfoCommand infoAssetMaster;
        private Srvtools.InfoCommand infoglVoucher;
        private Srvtools.InfoCommand infoglAccountItem;
        private Srvtools.InfoCommand glCompany;
    }
}
