namespace sForm_SecurityDepositWarrant
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SecurityDepositWarrant = new Srvtools.InfoCommand(this.components);
            this.ucSecurityDepositWarrant = new Srvtools.UpdateComponent(this.components);
            this.View_SecurityDepositWarrant = new Srvtools.InfoCommand(this.components);
            this.DepositProperty = new Srvtools.InfoCommand(this.components);
            this.VenderCustomer = new Srvtools.InfoCommand(this.components);
            this.InOutWay = new Srvtools.InfoCommand(this.components);
            this.AccountType = new Srvtools.InfoCommand(this.components);
            this.AccountTitle = new Srvtools.InfoCommand(this.components);
            this.DepositNO = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.Flowflag = new Srvtools.InfoCommand(this.components);
            this.Users = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityDepositWarrant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SecurityDepositWarrant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepositProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VenderCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InOutWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepositNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Flowflag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Users)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "MakeRequisition";
            service1.NonLogin = false;
            service1.ServiceName = "MakeRequisition";
            service2.DelegateName = "SelectRequisition";
            service2.NonLogin = false;
            service2.ServiceName = "SelectRequisition";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // SecurityDepositWarrant
            // 
            this.SecurityDepositWarrant.CacheConnection = false;
            this.SecurityDepositWarrant.CommandText = "SELECT dbo.[SecurityDepositWarrant].* FROM dbo.[SecurityDepositWarrant]";
            this.SecurityDepositWarrant.CommandTimeout = 30;
            this.SecurityDepositWarrant.CommandType = System.Data.CommandType.Text;
            this.SecurityDepositWarrant.DynamicTableName = false;
            this.SecurityDepositWarrant.EEPAlias = null;
            this.SecurityDepositWarrant.EncodingAfter = null;
            this.SecurityDepositWarrant.EncodingBefore = "Windows-1252";
            this.SecurityDepositWarrant.EncodingConvert = null;
            this.SecurityDepositWarrant.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "DepositWarrantNO";
            this.SecurityDepositWarrant.KeyFields.Add(keyItem1);
            this.SecurityDepositWarrant.MultiSetWhere = false;
            this.SecurityDepositWarrant.Name = "SecurityDepositWarrant";
            this.SecurityDepositWarrant.NotificationAutoEnlist = false;
            this.SecurityDepositWarrant.SecExcept = null;
            this.SecurityDepositWarrant.SecFieldName = null;
            this.SecurityDepositWarrant.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SecurityDepositWarrant.SelectPaging = false;
            this.SecurityDepositWarrant.SelectTop = 0;
            this.SecurityDepositWarrant.SiteControl = false;
            this.SecurityDepositWarrant.SiteFieldName = null;
            this.SecurityDepositWarrant.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSecurityDepositWarrant
            // 
            this.ucSecurityDepositWarrant.AutoTrans = true;
            this.ucSecurityDepositWarrant.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "DepositWarrantNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "DepositNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "DepositProperty";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CusSupplier";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "DepositAmount";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "WarrantAmount";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "OtherWarrantAmount";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Notes";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "InOutDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "InOutWay";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CusSupplierAccName";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "VoucherNO";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = "_usercode";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = "_sysdate";
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr15.DefaultValue = "_usercode";
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LastUpdateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr16.DefaultValue = "_sysdate";
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "RequisitionNO";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr1);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr2);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr3);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr4);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr5);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr6);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr7);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr8);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr9);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr10);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr11);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr12);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr13);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr14);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr15);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr16);
            this.ucSecurityDepositWarrant.FieldAttrs.Add(fieldAttr17);
            this.ucSecurityDepositWarrant.LogInfo = null;
            this.ucSecurityDepositWarrant.Name = "ucSecurityDepositWarrant";
            this.ucSecurityDepositWarrant.RowAffectsCheck = true;
            this.ucSecurityDepositWarrant.SelectCmd = this.SecurityDepositWarrant;
            this.ucSecurityDepositWarrant.SelectCmdForUpdate = null;
            this.ucSecurityDepositWarrant.SendSQLCmd = true;
            this.ucSecurityDepositWarrant.ServerModify = true;
            this.ucSecurityDepositWarrant.ServerModifyGetMax = false;
            this.ucSecurityDepositWarrant.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSecurityDepositWarrant.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSecurityDepositWarrant.UseTranscationScope = false;
            this.ucSecurityDepositWarrant.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucSecurityDepositWarrant.AfterInsert += new Srvtools.UpdateComponentAfterInsertEventHandler(this.ucSecurityDepositWarrant_AfterInsert);
            // 
            // View_SecurityDepositWarrant
            // 
            this.View_SecurityDepositWarrant.CacheConnection = false;
            this.View_SecurityDepositWarrant.CommandText = "SELECT * FROM dbo.[SecurityDepositWarrant]";
            this.View_SecurityDepositWarrant.CommandTimeout = 30;
            this.View_SecurityDepositWarrant.CommandType = System.Data.CommandType.Text;
            this.View_SecurityDepositWarrant.DynamicTableName = false;
            this.View_SecurityDepositWarrant.EEPAlias = null;
            this.View_SecurityDepositWarrant.EncodingAfter = null;
            this.View_SecurityDepositWarrant.EncodingBefore = "Windows-1252";
            this.View_SecurityDepositWarrant.EncodingConvert = null;
            this.View_SecurityDepositWarrant.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DepositWarrantNO";
            this.View_SecurityDepositWarrant.KeyFields.Add(keyItem2);
            this.View_SecurityDepositWarrant.MultiSetWhere = false;
            this.View_SecurityDepositWarrant.Name = "View_SecurityDepositWarrant";
            this.View_SecurityDepositWarrant.NotificationAutoEnlist = false;
            this.View_SecurityDepositWarrant.SecExcept = null;
            this.View_SecurityDepositWarrant.SecFieldName = null;
            this.View_SecurityDepositWarrant.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SecurityDepositWarrant.SelectPaging = false;
            this.View_SecurityDepositWarrant.SelectTop = 0;
            this.View_SecurityDepositWarrant.SiteControl = false;
            this.View_SecurityDepositWarrant.SiteFieldName = null;
            this.View_SecurityDepositWarrant.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DepositProperty
            // 
            this.DepositProperty.CacheConnection = false;
            this.DepositProperty.CommandText = "select * from(\r\nselect \'1\' as PropertyValue,\'存出\' as PropertyName\r\nunion\r\nselect \'" +
    "2\' as PropertyValue,\'存入\' as PropertyName\r\n) temp";
            this.DepositProperty.CommandTimeout = 30;
            this.DepositProperty.CommandType = System.Data.CommandType.Text;
            this.DepositProperty.DynamicTableName = false;
            this.DepositProperty.EEPAlias = null;
            this.DepositProperty.EncodingAfter = null;
            this.DepositProperty.EncodingBefore = "Windows-1252";
            this.DepositProperty.EncodingConvert = null;
            this.DepositProperty.InfoConnection = this.InfoConnection1;
            this.DepositProperty.MultiSetWhere = false;
            this.DepositProperty.Name = "DepositProperty";
            this.DepositProperty.NotificationAutoEnlist = false;
            this.DepositProperty.SecExcept = null;
            this.DepositProperty.SecFieldName = null;
            this.DepositProperty.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DepositProperty.SelectPaging = false;
            this.DepositProperty.SelectTop = 0;
            this.DepositProperty.SiteControl = false;
            this.DepositProperty.SiteFieldName = null;
            this.DepositProperty.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VenderCustomer
            // 
            this.VenderCustomer.CacheConnection = false;
            this.VenderCustomer.CommandText = resources.GetString("VenderCustomer.CommandText");
            this.VenderCustomer.CommandTimeout = 30;
            this.VenderCustomer.CommandType = System.Data.CommandType.Text;
            this.VenderCustomer.DynamicTableName = false;
            this.VenderCustomer.EEPAlias = null;
            this.VenderCustomer.EncodingAfter = null;
            this.VenderCustomer.EncodingBefore = "Windows-1252";
            this.VenderCustomer.EncodingConvert = null;
            this.VenderCustomer.InfoConnection = this.InfoConnection1;
            this.VenderCustomer.MultiSetWhere = false;
            this.VenderCustomer.Name = "VenderCustomer";
            this.VenderCustomer.NotificationAutoEnlist = false;
            this.VenderCustomer.SecExcept = null;
            this.VenderCustomer.SecFieldName = null;
            this.VenderCustomer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VenderCustomer.SelectPaging = false;
            this.VenderCustomer.SelectTop = 0;
            this.VenderCustomer.SiteControl = false;
            this.VenderCustomer.SiteFieldName = null;
            this.VenderCustomer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InOutWay
            // 
            this.InOutWay.CacheConnection = false;
            this.InOutWay.CommandText = "select * from(\r\nselect \'1\' as InOutWayValue,\'現金\' as InOutWayName\r\nunion\r\nselect \'" +
    "2\' as InOutWayValue,\'匯款\' as InOutWayName\r\nunion\r\nselect \'3\' as InOutWayValue,\'支票" +
    "\' as InOutWayName\r\n) temp";
            this.InOutWay.CommandTimeout = 30;
            this.InOutWay.CommandType = System.Data.CommandType.Text;
            this.InOutWay.DynamicTableName = false;
            this.InOutWay.EEPAlias = null;
            this.InOutWay.EncodingAfter = null;
            this.InOutWay.EncodingBefore = "Windows-1252";
            this.InOutWay.EncodingConvert = null;
            this.InOutWay.InfoConnection = this.InfoConnection1;
            this.InOutWay.MultiSetWhere = false;
            this.InOutWay.Name = "InOutWay";
            this.InOutWay.NotificationAutoEnlist = false;
            this.InOutWay.SecExcept = null;
            this.InOutWay.SecFieldName = null;
            this.InOutWay.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InOutWay.SelectPaging = false;
            this.InOutWay.SelectTop = 0;
            this.InOutWay.SiteControl = false;
            this.InOutWay.SiteFieldName = null;
            this.InOutWay.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccountType
            // 
            this.AccountType.CacheConnection = false;
            this.AccountType.CommandText = "SELECT DISTINCT AccountType  FROM  ACCOUNTTITLE";
            this.AccountType.CommandTimeout = 30;
            this.AccountType.CommandType = System.Data.CommandType.Text;
            this.AccountType.DynamicTableName = false;
            this.AccountType.EEPAlias = null;
            this.AccountType.EncodingAfter = null;
            this.AccountType.EncodingBefore = "Windows-1252";
            this.AccountType.EncodingConvert = null;
            this.AccountType.InfoConnection = this.InfoConnection1;
            this.AccountType.MultiSetWhere = false;
            this.AccountType.Name = "AccountType";
            this.AccountType.NotificationAutoEnlist = false;
            this.AccountType.SecExcept = null;
            this.AccountType.SecFieldName = null;
            this.AccountType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AccountType.SelectPaging = false;
            this.AccountType.SelectTop = 0;
            this.AccountType.SiteControl = false;
            this.AccountType.SiteFieldName = null;
            this.AccountType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccountTitle
            // 
            this.AccountTitle.CacheConnection = false;
            this.AccountTitle.CommandText = "select AccountTitle.AccountID,AccountTitle.AccountName,AccountTitle.LimitCostCent" +
    "ers  from AccountTitle  order by  AccountName\r\n";
            this.AccountTitle.CommandTimeout = 30;
            this.AccountTitle.CommandType = System.Data.CommandType.Text;
            this.AccountTitle.DynamicTableName = false;
            this.AccountTitle.EEPAlias = null;
            this.AccountTitle.EncodingAfter = null;
            this.AccountTitle.EncodingBefore = "Windows-1252";
            this.AccountTitle.EncodingConvert = null;
            this.AccountTitle.InfoConnection = this.InfoConnection1;
            this.AccountTitle.MultiSetWhere = false;
            this.AccountTitle.Name = "AccountTitle";
            this.AccountTitle.NotificationAutoEnlist = false;
            this.AccountTitle.SecExcept = null;
            this.AccountTitle.SecFieldName = null;
            this.AccountTitle.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AccountTitle.SelectPaging = false;
            this.AccountTitle.SelectTop = 0;
            this.AccountTitle.SiteControl = false;
            this.AccountTitle.SiteFieldName = null;
            this.AccountTitle.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DepositNO
            // 
            this.DepositNO.CacheConnection = false;
            this.DepositNO.CommandText = resources.GetString("DepositNO.CommandText");
            this.DepositNO.CommandTimeout = 30;
            this.DepositNO.CommandType = System.Data.CommandType.Text;
            this.DepositNO.DynamicTableName = false;
            this.DepositNO.EEPAlias = null;
            this.DepositNO.EncodingAfter = null;
            this.DepositNO.EncodingBefore = "Windows-1252";
            this.DepositNO.EncodingConvert = null;
            this.DepositNO.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "DepositNO";
            this.DepositNO.KeyFields.Add(keyItem3);
            this.DepositNO.MultiSetWhere = false;
            this.DepositNO.Name = "DepositNO";
            this.DepositNO.NotificationAutoEnlist = false;
            this.DepositNO.SecExcept = null;
            this.DepositNO.SecFieldName = null;
            this.DepositNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DepositNO.SelectPaging = false;
            this.DepositNO.SelectTop = 0;
            this.DepositNO.SiteControl = false;
            this.DepositNO.SiteFieldName = null;
            this.DepositNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "DepositWarrantNO";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "DepositWarrantNOGetFixed()";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 5;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "DepositWarrantNO";
            this.autoNumber1.UpdateComp = this.ucSecurityDepositWarrant;
            // 
            // Flowflag
            // 
            this.Flowflag.CacheConnection = false;
            this.Flowflag.CommandText = resources.GetString("Flowflag.CommandText");
            this.Flowflag.CommandTimeout = 30;
            this.Flowflag.CommandType = System.Data.CommandType.Text;
            this.Flowflag.DynamicTableName = false;
            this.Flowflag.EEPAlias = "";
            this.Flowflag.EncodingAfter = null;
            this.Flowflag.EncodingBefore = "Windows-1252";
            this.Flowflag.EncodingConvert = null;
            this.Flowflag.InfoConnection = this.InfoConnection1;
            this.Flowflag.MultiSetWhere = false;
            this.Flowflag.Name = "Flowflag";
            this.Flowflag.NotificationAutoEnlist = false;
            this.Flowflag.SecExcept = null;
            this.Flowflag.SecFieldName = null;
            this.Flowflag.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Flowflag.SelectPaging = false;
            this.Flowflag.SelectTop = 0;
            this.Flowflag.SiteControl = false;
            this.Flowflag.SiteFieldName = null;
            this.Flowflag.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Users
            // 
            this.Users.CacheConnection = false;
            this.Users.CommandText = "SELECT USERID,USERNAME FROM  EIPHRSYS.dbo.[USERS] where [DESCRIPTION]=\'JB\'\r\n\r\n";
            this.Users.CommandTimeout = 30;
            this.Users.CommandType = System.Data.CommandType.Text;
            this.Users.DynamicTableName = false;
            this.Users.EEPAlias = "";
            this.Users.EncodingAfter = null;
            this.Users.EncodingBefore = "Windows-1252";
            this.Users.EncodingConvert = null;
            this.Users.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "USERID";
            this.Users.KeyFields.Add(keyItem4);
            this.Users.MultiSetWhere = false;
            this.Users.Name = "Users";
            this.Users.NotificationAutoEnlist = false;
            this.Users.SecExcept = null;
            this.Users.SecFieldName = null;
            this.Users.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Users.SelectPaging = false;
            this.Users.SelectTop = 0;
            this.Users.SiteControl = false;
            this.Users.SiteFieldName = null;
            this.Users.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityDepositWarrant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SecurityDepositWarrant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepositProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VenderCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InOutWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepositNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Flowflag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Users)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SecurityDepositWarrant;
        private Srvtools.UpdateComponent ucSecurityDepositWarrant;
        private Srvtools.InfoCommand View_SecurityDepositWarrant;
        private Srvtools.InfoCommand DepositProperty;
        private Srvtools.InfoCommand VenderCustomer;
        private Srvtools.InfoCommand InOutWay;
        private Srvtools.InfoCommand AccountType;
        private Srvtools.InfoCommand AccountTitle;
        private Srvtools.InfoCommand DepositNO;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand Flowflag;
        private Srvtools.InfoCommand Users;
    }
}
