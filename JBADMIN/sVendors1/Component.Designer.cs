namespace sVendors
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
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Vendors1 = new Srvtools.InfoCommand(this.components);
            this.ucVendors = new Srvtools.UpdateComponent(this.components);
            this.View_Vendors = new Srvtools.InfoCommand(this.components);
            this.autoVendID = new Srvtools.AutoNumber(this.components);
            this.PayTerm = new Srvtools.InfoCommand(this.components);
            this.Bank = new Srvtools.InfoCommand(this.components);
            this.VendType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Vendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelVendors";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelVendors";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // Vendors1
            // 
            this.Vendors1.CacheConnection = false;
            this.Vendors1.CommandText = resources.GetString("Vendors1.CommandText");
            this.Vendors1.CommandTimeout = 30;
            this.Vendors1.CommandType = System.Data.CommandType.Text;
            this.Vendors1.DynamicTableName = false;
            this.Vendors1.EEPAlias = null;
            this.Vendors1.EncodingAfter = null;
            this.Vendors1.EncodingBefore = "Windows-1252";
            this.Vendors1.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "VendID";
            this.Vendors1.KeyFields.Add(keyItem1);
            this.Vendors1.MultiSetWhere = false;
            this.Vendors1.Name = "Vendors1";
            this.Vendors1.NotificationAutoEnlist = false;
            this.Vendors1.SecExcept = null;
            this.Vendors1.SecFieldName = "";
            this.Vendors1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Vendors1.SelectPaging = false;
            this.Vendors1.SelectTop = 0;
            this.Vendors1.SiteControl = false;
            this.Vendors1.SiteFieldName = null;
            this.Vendors1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucVendors
            // 
            this.ucVendors.AutoTrans = true;
            this.ucVendors.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "VendID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "VendName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "VendShortName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ContactName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ContactTelArea";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ContactTel";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ContactTelExt";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "PayTermDays";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ContactEmail";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ContactMobile";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "VendBank";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "VendAccount";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "VendorNotes";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "VendTypeID";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = "_username";
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
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr17.DefaultValue = "_username";
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "LastUpdateDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.ucVendors.FieldAttrs.Add(fieldAttr1);
            this.ucVendors.FieldAttrs.Add(fieldAttr2);
            this.ucVendors.FieldAttrs.Add(fieldAttr3);
            this.ucVendors.FieldAttrs.Add(fieldAttr4);
            this.ucVendors.FieldAttrs.Add(fieldAttr5);
            this.ucVendors.FieldAttrs.Add(fieldAttr6);
            this.ucVendors.FieldAttrs.Add(fieldAttr7);
            this.ucVendors.FieldAttrs.Add(fieldAttr8);
            this.ucVendors.FieldAttrs.Add(fieldAttr9);
            this.ucVendors.FieldAttrs.Add(fieldAttr10);
            this.ucVendors.FieldAttrs.Add(fieldAttr11);
            this.ucVendors.FieldAttrs.Add(fieldAttr12);
            this.ucVendors.FieldAttrs.Add(fieldAttr13);
            this.ucVendors.FieldAttrs.Add(fieldAttr14);
            this.ucVendors.FieldAttrs.Add(fieldAttr15);
            this.ucVendors.FieldAttrs.Add(fieldAttr16);
            this.ucVendors.FieldAttrs.Add(fieldAttr17);
            this.ucVendors.FieldAttrs.Add(fieldAttr18);
            this.ucVendors.LogInfo = null;
            this.ucVendors.Name = "ucVendors";
            this.ucVendors.RowAffectsCheck = true;
            this.ucVendors.SelectCmd = this.Vendors1;
            this.ucVendors.SelectCmdForUpdate = null;
            this.ucVendors.ServerModify = true;
            this.ucVendors.ServerModifyGetMax = false;
            this.ucVendors.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucVendors.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucVendors.UseTranscationScope = false;
            this.ucVendors.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucVendors.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucVendors_BeforeInsert);
            this.ucVendors.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucVendors_BeforeModify);
            // 
            // View_Vendors
            // 
            this.View_Vendors.CacheConnection = false;
            this.View_Vendors.CommandText = "SELECT * FROM dbo.[Vendors]";
            this.View_Vendors.CommandTimeout = 30;
            this.View_Vendors.CommandType = System.Data.CommandType.Text;
            this.View_Vendors.DynamicTableName = false;
            this.View_Vendors.EEPAlias = null;
            this.View_Vendors.EncodingAfter = null;
            this.View_Vendors.EncodingBefore = "Windows-1252";
            this.View_Vendors.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "VendID";
            this.View_Vendors.KeyFields.Add(keyItem2);
            this.View_Vendors.MultiSetWhere = false;
            this.View_Vendors.Name = "View_Vendors";
            this.View_Vendors.NotificationAutoEnlist = false;
            this.View_Vendors.SecExcept = null;
            this.View_Vendors.SecFieldName = null;
            this.View_Vendors.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Vendors.SelectPaging = false;
            this.View_Vendors.SelectTop = 0;
            this.View_Vendors.SiteControl = false;
            this.View_Vendors.SiteFieldName = null;
            this.View_Vendors.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoVendID
            // 
            this.autoVendID.Active = true;
            this.autoVendID.AutoNoID = "VendID";
            this.autoVendID.Description = null;
            this.autoVendID.GetFixed = "V";
            this.autoVendID.isNumFill = false;
            this.autoVendID.Name = "autoVendID";
            this.autoVendID.Number = null;
            this.autoVendID.NumDig = 4;
            this.autoVendID.OldVersion = false;
            this.autoVendID.OverFlow = true;
            this.autoVendID.StartValue = 1;
            this.autoVendID.Step = 1;
            this.autoVendID.TargetColumn = "VendID";
            this.autoVendID.UpdateComp = this.ucVendors;
            // 
            // PayTerm
            // 
            this.PayTerm.CacheConnection = false;
            this.PayTerm.CommandText = "select PayTerm.* from PayTerm";
            this.PayTerm.CommandTimeout = 30;
            this.PayTerm.CommandType = System.Data.CommandType.Text;
            this.PayTerm.DynamicTableName = false;
            this.PayTerm.EEPAlias = null;
            this.PayTerm.EncodingAfter = null;
            this.PayTerm.EncodingBefore = "Windows-1252";
            this.PayTerm.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "PayTermID";
            this.PayTerm.KeyFields.Add(keyItem3);
            this.PayTerm.MultiSetWhere = false;
            this.PayTerm.Name = "PayTerm";
            this.PayTerm.NotificationAutoEnlist = false;
            this.PayTerm.SecExcept = null;
            this.PayTerm.SecFieldName = null;
            this.PayTerm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PayTerm.SelectPaging = false;
            this.PayTerm.SelectTop = 0;
            this.PayTerm.SiteControl = false;
            this.PayTerm.SiteFieldName = null;
            this.PayTerm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Bank
            // 
            this.Bank.CacheConnection = false;
            this.Bank.CommandText = "select Bank.BankID,Bank.BankName,Bank.IsRemit,Bank.BankNO,Bank.BankBranchNO from " +
    "Bank order by Bank.BankNO,Bank.BankBranchNO";
            this.Bank.CommandTimeout = 30;
            this.Bank.CommandType = System.Data.CommandType.Text;
            this.Bank.DynamicTableName = false;
            this.Bank.EEPAlias = null;
            this.Bank.EncodingAfter = null;
            this.Bank.EncodingBefore = "Windows-1252";
            this.Bank.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "BankID";
            this.Bank.KeyFields.Add(keyItem4);
            this.Bank.MultiSetWhere = false;
            this.Bank.Name = "Bank";
            this.Bank.NotificationAutoEnlist = false;
            this.Bank.SecExcept = null;
            this.Bank.SecFieldName = null;
            this.Bank.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Bank.SelectPaging = false;
            this.Bank.SelectTop = 0;
            this.Bank.SiteControl = false;
            this.Bank.SiteFieldName = null;
            this.Bank.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VendType
            // 
            this.VendType.CacheConnection = false;
            this.VendType.CommandText = "select VendType.VendTypeID,VendType.VendTypeName from VendType\r\nOrder by VendType" +
    ".VendTypeName";
            this.VendType.CommandTimeout = 30;
            this.VendType.CommandType = System.Data.CommandType.Text;
            this.VendType.DynamicTableName = false;
            this.VendType.EEPAlias = null;
            this.VendType.EncodingAfter = null;
            this.VendType.EncodingBefore = "Windows-1252";
            this.VendType.InfoConnection = this.InfoConnection1;
            this.VendType.MultiSetWhere = false;
            this.VendType.Name = "VendType";
            this.VendType.NotificationAutoEnlist = false;
            this.VendType.SecExcept = null;
            this.VendType.SecFieldName = null;
            this.VendType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VendType.SelectPaging = false;
            this.VendType.SelectTop = 0;
            this.VendType.SiteControl = false;
            this.VendType.SiteFieldName = null;
            this.VendType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Vendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Vendors1;
        private Srvtools.UpdateComponent ucVendors;
        private Srvtools.InfoCommand View_Vendors;
        private Srvtools.AutoNumber autoVendID;
        private Srvtools.InfoCommand PayTerm;
        private Srvtools.InfoCommand Bank;
        private Srvtools.InfoCommand VendType;
    }
}
