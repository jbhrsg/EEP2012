namespace sForm_SecurityDeposit
{
    partial class s
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
            Srvtools.InfoCommand VenderCustomer1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s));
            Srvtools.Service service1 = new Srvtools.Service();
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
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
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.SecurityDeposit = new Srvtools.InfoCommand(this.components);
            this.View_SecurityDeposit = new Srvtools.InfoCommand(this.components);
            this.ContractNO = new Srvtools.InfoCommand(this.components);
            this.DepositProperty = new Srvtools.InfoCommand(this.components);
            this.VenderCustomer = new Srvtools.InfoCommand(this.components);
            this.InOutWay = new Srvtools.InfoCommand(this.components);
            this.ucSecurityDeposit = new Srvtools.UpdateComponent(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.AccountType = new Srvtools.InfoCommand(this.components);
            this.AccountTitle = new Srvtools.InfoCommand(this.components);
            VenderCustomer1 = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(VenderCustomer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityDeposit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SecurityDeposit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepositProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VenderCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InOutWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTitle)).BeginInit();
            // 
            // VenderCustomer1
            // 
            VenderCustomer1.CacheConnection = false;
            VenderCustomer1.CommandText = resources.GetString("VenderCustomer1.CommandText");
            VenderCustomer1.CommandTimeout = 30;
            VenderCustomer1.CommandType = System.Data.CommandType.Text;
            VenderCustomer1.DynamicTableName = false;
            VenderCustomer1.EEPAlias = null;
            VenderCustomer1.EncodingAfter = null;
            VenderCustomer1.EncodingBefore = "Windows-1252";
            VenderCustomer1.EncodingConvert = null;
            VenderCustomer1.InfoConnection = this.InfoConnection1;
            VenderCustomer1.MultiSetWhere = false;
            VenderCustomer1.Name = "VenderCustomer1";
            VenderCustomer1.NotificationAutoEnlist = false;
            VenderCustomer1.SecExcept = null;
            VenderCustomer1.SecFieldName = null;
            VenderCustomer1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            VenderCustomer1.SelectPaging = false;
            VenderCustomer1.SelectTop = 0;
            VenderCustomer1.SiteControl = false;
            VenderCustomer1.SiteFieldName = null;
            VenderCustomer1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // serviceManager1
            // 
            service1.DelegateName = "MakeRequisition";
            service1.NonLogin = false;
            service1.ServiceName = "MakeRequisition";
            service2.DelegateName = "SelectRequisition";
            service2.NonLogin = false;
            service2.ServiceName = "SelectRequisition";
            service3.DelegateName = "FlowStartUp";
            service3.NonLogin = false;
            service3.ServiceName = "FlowStartUp";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // SecurityDeposit
            // 
            this.SecurityDeposit.CacheConnection = false;
            this.SecurityDeposit.CommandText = "SELECT * FROM dbo.[SecurityDeposit] \r\n";
            this.SecurityDeposit.CommandTimeout = 30;
            this.SecurityDeposit.CommandType = System.Data.CommandType.Text;
            this.SecurityDeposit.DynamicTableName = false;
            this.SecurityDeposit.EEPAlias = null;
            this.SecurityDeposit.EncodingAfter = null;
            this.SecurityDeposit.EncodingBefore = "Windows-1252";
            this.SecurityDeposit.EncodingConvert = null;
            this.SecurityDeposit.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "DepositNO";
            this.SecurityDeposit.KeyFields.Add(keyItem1);
            this.SecurityDeposit.MultiSetWhere = false;
            this.SecurityDeposit.Name = "SecurityDeposit";
            this.SecurityDeposit.NotificationAutoEnlist = false;
            this.SecurityDeposit.SecExcept = null;
            this.SecurityDeposit.SecFieldName = null;
            this.SecurityDeposit.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SecurityDeposit.SelectPaging = false;
            this.SecurityDeposit.SelectTop = 0;
            this.SecurityDeposit.SiteControl = false;
            this.SecurityDeposit.SiteFieldName = null;
            this.SecurityDeposit.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_SecurityDeposit
            // 
            this.View_SecurityDeposit.CacheConnection = false;
            this.View_SecurityDeposit.CommandText = "SELECT * FROM dbo.[SecurityDeposit]";
            this.View_SecurityDeposit.CommandTimeout = 30;
            this.View_SecurityDeposit.CommandType = System.Data.CommandType.Text;
            this.View_SecurityDeposit.DynamicTableName = false;
            this.View_SecurityDeposit.EEPAlias = null;
            this.View_SecurityDeposit.EncodingAfter = null;
            this.View_SecurityDeposit.EncodingBefore = "Windows-1252";
            this.View_SecurityDeposit.EncodingConvert = null;
            this.View_SecurityDeposit.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DepositNO";
            this.View_SecurityDeposit.KeyFields.Add(keyItem2);
            this.View_SecurityDeposit.MultiSetWhere = false;
            this.View_SecurityDeposit.Name = "View_SecurityDeposit";
            this.View_SecurityDeposit.NotificationAutoEnlist = false;
            this.View_SecurityDeposit.SecExcept = null;
            this.View_SecurityDeposit.SecFieldName = null;
            this.View_SecurityDeposit.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SecurityDeposit.SelectPaging = false;
            this.View_SecurityDeposit.SelectTop = 0;
            this.View_SecurityDeposit.SiteControl = false;
            this.View_SecurityDeposit.SiteFieldName = null;
            this.View_SecurityDeposit.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ContractNO
            // 
            this.ContractNO.CacheConnection = false;
            this.ContractNO.CommandText = resources.GetString("ContractNO.CommandText");
            this.ContractNO.CommandTimeout = 30;
            this.ContractNO.CommandType = System.Data.CommandType.Text;
            this.ContractNO.DynamicTableName = false;
            this.ContractNO.EEPAlias = null;
            this.ContractNO.EncodingAfter = null;
            this.ContractNO.EncodingBefore = "Windows-1252";
            this.ContractNO.EncodingConvert = null;
            this.ContractNO.InfoConnection = this.InfoConnection1;
            this.ContractNO.MultiSetWhere = false;
            this.ContractNO.Name = "ContractNO";
            this.ContractNO.NotificationAutoEnlist = false;
            this.ContractNO.SecExcept = null;
            this.ContractNO.SecFieldName = null;
            this.ContractNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ContractNO.SelectPaging = false;
            this.ContractNO.SelectTop = 0;
            this.ContractNO.SiteControl = false;
            this.ContractNO.SiteFieldName = null;
            this.ContractNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DepositProperty
            // 
            this.DepositProperty.CacheConnection = false;
            this.DepositProperty.CommandText = "select \'1\' as PropertyValue,\'存出\' as PropertyName\r\nunion\r\nselect \'2\' as PropertyVa" +
    "lue,\'存入\' as PropertyName";
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
            this.InOutWay.CommandText = "select \'1\' as InOutWayValue,\'現金\' as InOutWayName\r\nunion\r\nselect \'2\' as InOutWayVa" +
    "lue,\'匯款\' as InOutWayName\r\nunion\r\nselect \'3\' as InOutWayValue,\'支票\' as InOutWayNam" +
    "e";
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
            // ucSecurityDeposit
            // 
            this.ucSecurityDeposit.AutoTrans = true;
            this.ucSecurityDeposit.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "DepositNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ContractNO";
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
            fieldAttr6.DataField = "Notes";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "InOutDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "InOutWay";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CusSupplierAccName";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "VoucherNO";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = "_usercode";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = "_sysdate";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr13.DefaultValue = "_usercode";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpdateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr14.DefaultValue = "_sysdate";
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
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr1);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr2);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr3);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr4);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr5);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr6);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr7);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr8);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr9);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr10);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr11);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr12);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr13);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr14);
            this.ucSecurityDeposit.FieldAttrs.Add(fieldAttr15);
            this.ucSecurityDeposit.LogInfo = null;
            this.ucSecurityDeposit.Name = "ucSecurityDeposit";
            this.ucSecurityDeposit.RowAffectsCheck = true;
            this.ucSecurityDeposit.SelectCmd = this.SecurityDeposit;
            this.ucSecurityDeposit.SelectCmdForUpdate = null;
            this.ucSecurityDeposit.SendSQLCmd = true;
            this.ucSecurityDeposit.ServerModify = true;
            this.ucSecurityDeposit.ServerModifyGetMax = false;
            this.ucSecurityDeposit.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSecurityDeposit.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSecurityDeposit.UseTranscationScope = false;
            this.ucSecurityDeposit.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucSecurityDeposit.AfterInsert += new Srvtools.UpdateComponentAfterInsertEventHandler(this.ucSecurityDeposit_AfterInsert);
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "SecurityDepositNO";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "DepositNOGetFixed()";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 5;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "DepositNO";
            this.autoNumber1.UpdateComp = this.ucSecurityDeposit;
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
            ((System.ComponentModel.ISupportInitialize)(VenderCustomer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityDeposit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SecurityDeposit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepositProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VenderCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InOutWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTitle)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SecurityDeposit;
        private Srvtools.InfoCommand View_SecurityDeposit;
        private Srvtools.InfoCommand ContractNO;
        private Srvtools.InfoCommand DepositProperty;
        private Srvtools.InfoCommand VenderCustomer;
        private Srvtools.InfoCommand InOutWay;
        private Srvtools.UpdateComponent ucSecurityDeposit;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand AccountType;
        private Srvtools.InfoCommand AccountTitle;
    }
}
