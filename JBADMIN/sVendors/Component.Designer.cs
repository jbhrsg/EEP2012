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
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
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
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Vendors = new Srvtools.InfoCommand(this.components);
            this.ucVendors = new Srvtools.UpdateComponent(this.components);
            this.View_Vendors = new Srvtools.InfoCommand(this.components);
            this.autoVendID = new Srvtools.AutoNumber(this.components);
            this.PayTerm = new Srvtools.InfoCommand(this.components);
            this.Bank = new Srvtools.InfoCommand(this.components);
            this.ItemType = new Srvtools.InfoCommand(this.components);
            this.ItemTypeTree = new Srvtools.InfoCommand(this.components);
            this.ItemUnit = new Srvtools.InfoCommand(this.components);
            this.VendLevel = new Srvtools.InfoCommand(this.components);
            this.VendProperty = new Srvtools.InfoCommand(this.components);
            this.VendGrade = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Vendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTypeTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendGrade)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelVendors";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelVendors";
            service2.DelegateName = "SaveVendorItemType";
            service2.NonLogin = false;
            service2.ServiceName = "SaveVendorItemType";
            service3.DelegateName = "CheckVendNameIsExist";
            service3.NonLogin = false;
            service3.ServiceName = "CheckVendNameIsExist";
            service4.DelegateName = "GetMaxVendorID";
            service4.NonLogin = false;
            service4.ServiceName = "GetMaxVendorID";
            service5.DelegateName = "PutVendorEval";
            service5.NonLogin = false;
            service5.ServiceName = "PutVendorEval";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // Vendors
            // 
            this.Vendors.CacheConnection = false;
            this.Vendors.CommandText = resources.GetString("Vendors.CommandText");
            this.Vendors.CommandTimeout = 30;
            this.Vendors.CommandType = System.Data.CommandType.Text;
            this.Vendors.DynamicTableName = false;
            this.Vendors.EEPAlias = null;
            this.Vendors.EncodingAfter = null;
            this.Vendors.EncodingBefore = "Windows-1252";
            this.Vendors.EncodingConvert = null;
            this.Vendors.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "VendID";
            this.Vendors.KeyFields.Add(keyItem1);
            this.Vendors.MultiSetWhere = false;
            this.Vendors.Name = "Vendors";
            this.Vendors.NotificationAutoEnlist = false;
            this.Vendors.SecExcept = null;
            this.Vendors.SecFieldName = "";
            this.Vendors.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Vendors.SelectPaging = false;
            this.Vendors.SelectTop = 0;
            this.Vendors.SiteControl = false;
            this.Vendors.SiteFieldName = null;
            this.Vendors.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            fieldAttr15.DataField = "VendLicense";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "VendGradeID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "LastEvalDate";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "CreateBy";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = "";
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "CreateDate";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "LastUpdateBy";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr20.DefaultValue = "";
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "LastUpdateDate";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
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
            this.ucVendors.FieldAttrs.Add(fieldAttr19);
            this.ucVendors.FieldAttrs.Add(fieldAttr20);
            this.ucVendors.FieldAttrs.Add(fieldAttr21);
            this.ucVendors.LogInfo = null;
            this.ucVendors.Name = "ucVendors";
            this.ucVendors.RowAffectsCheck = true;
            this.ucVendors.SelectCmd = this.Vendors;
            this.ucVendors.SelectCmdForUpdate = null;
            this.ucVendors.SendSQLCmd = true;
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
            this.View_Vendors.EncodingConvert = null;
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
            this.autoVendID.Active = false;
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
            this.PayTerm.CommandText = "select PayTerm.* from PayTerm \r\norder by PayTermName";
            this.PayTerm.CommandTimeout = 30;
            this.PayTerm.CommandType = System.Data.CommandType.Text;
            this.PayTerm.DynamicTableName = false;
            this.PayTerm.EEPAlias = null;
            this.PayTerm.EncodingAfter = null;
            this.PayTerm.EncodingBefore = "Windows-1252";
            this.PayTerm.EncodingConvert = null;
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
            this.Bank.EncodingConvert = null;
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
            // ItemType
            // 
            this.ItemType.CacheConnection = false;
            this.ItemType.CommandText = "SELECT  ItemTypeID,ItemTypeName\r\nFROM ItemType Order by ItemTypeID";
            this.ItemType.CommandTimeout = 30;
            this.ItemType.CommandType = System.Data.CommandType.Text;
            this.ItemType.DynamicTableName = false;
            this.ItemType.EEPAlias = null;
            this.ItemType.EncodingAfter = null;
            this.ItemType.EncodingBefore = "Windows-1252";
            this.ItemType.EncodingConvert = null;
            this.ItemType.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "ItemTypeID";
            this.ItemType.KeyFields.Add(keyItem5);
            this.ItemType.MultiSetWhere = false;
            this.ItemType.Name = "ItemType";
            this.ItemType.NotificationAutoEnlist = false;
            this.ItemType.SecExcept = null;
            this.ItemType.SecFieldName = null;
            this.ItemType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ItemType.SelectPaging = false;
            this.ItemType.SelectTop = 0;
            this.ItemType.SiteControl = false;
            this.ItemType.SiteFieldName = null;
            this.ItemType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ItemTypeTree
            // 
            this.ItemTypeTree.CacheConnection = false;
            this.ItemTypeTree.CommandText = "SELECT  \'Root\' as ID,null as ParentID,\'物品類別\' AS Name,\'True\' as IsClass\r\nUnion \r\nS" +
    "ELECT  ItemTypeID AS ID,\'Root\' AS ParentID,ItemTypeName as Name,\'False\' as IsCla" +
    "ss   \r\nFROM ItemType";
            this.ItemTypeTree.CommandTimeout = 30;
            this.ItemTypeTree.CommandType = System.Data.CommandType.Text;
            this.ItemTypeTree.DynamicTableName = false;
            this.ItemTypeTree.EEPAlias = null;
            this.ItemTypeTree.EncodingAfter = null;
            this.ItemTypeTree.EncodingBefore = "Windows-1252";
            this.ItemTypeTree.EncodingConvert = null;
            this.ItemTypeTree.InfoConnection = this.InfoConnection1;
            this.ItemTypeTree.MultiSetWhere = false;
            this.ItemTypeTree.Name = "ItemTypeTree";
            this.ItemTypeTree.NotificationAutoEnlist = false;
            this.ItemTypeTree.SecExcept = null;
            this.ItemTypeTree.SecFieldName = null;
            this.ItemTypeTree.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ItemTypeTree.SelectPaging = false;
            this.ItemTypeTree.SelectTop = 0;
            this.ItemTypeTree.SiteControl = false;
            this.ItemTypeTree.SiteFieldName = null;
            this.ItemTypeTree.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ItemUnit
            // 
            this.ItemUnit.CacheConnection = false;
            this.ItemUnit.CommandText = "SELECT  Unit FROM Item  Where Unit<>\'\' Group By Unit";
            this.ItemUnit.CommandTimeout = 30;
            this.ItemUnit.CommandType = System.Data.CommandType.Text;
            this.ItemUnit.DynamicTableName = false;
            this.ItemUnit.EEPAlias = null;
            this.ItemUnit.EncodingAfter = null;
            this.ItemUnit.EncodingBefore = "Windows-1252";
            this.ItemUnit.EncodingConvert = null;
            this.ItemUnit.InfoConnection = this.InfoConnection1;
            this.ItemUnit.MultiSetWhere = false;
            this.ItemUnit.Name = "ItemUnit";
            this.ItemUnit.NotificationAutoEnlist = false;
            this.ItemUnit.SecExcept = null;
            this.ItemUnit.SecFieldName = null;
            this.ItemUnit.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ItemUnit.SelectPaging = false;
            this.ItemUnit.SelectTop = 0;
            this.ItemUnit.SiteControl = false;
            this.ItemUnit.SiteFieldName = null;
            this.ItemUnit.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VendLevel
            // 
            this.VendLevel.CacheConnection = false;
            this.VendLevel.CommandText = resources.GetString("VendLevel.CommandText");
            this.VendLevel.CommandTimeout = 30;
            this.VendLevel.CommandType = System.Data.CommandType.Text;
            this.VendLevel.DynamicTableName = false;
            this.VendLevel.EEPAlias = null;
            this.VendLevel.EncodingAfter = null;
            this.VendLevel.EncodingBefore = "Windows-1252";
            this.VendLevel.EncodingConvert = null;
            this.VendLevel.InfoConnection = this.InfoConnection1;
            this.VendLevel.MultiSetWhere = false;
            this.VendLevel.Name = "VendLevel";
            this.VendLevel.NotificationAutoEnlist = false;
            this.VendLevel.SecExcept = null;
            this.VendLevel.SecFieldName = null;
            this.VendLevel.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VendLevel.SelectPaging = false;
            this.VendLevel.SelectTop = 0;
            this.VendLevel.SiteControl = false;
            this.VendLevel.SiteFieldName = null;
            this.VendLevel.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VendProperty
            // 
            this.VendProperty.CacheConnection = false;
            this.VendProperty.CommandText = "SELECT ID,Content,ContentGrid FROM VendProperTY\r\nORDER BY ID";
            this.VendProperty.CommandTimeout = 30;
            this.VendProperty.CommandType = System.Data.CommandType.Text;
            this.VendProperty.DynamicTableName = false;
            this.VendProperty.EEPAlias = null;
            this.VendProperty.EncodingAfter = null;
            this.VendProperty.EncodingBefore = "Windows-1252";
            this.VendProperty.EncodingConvert = null;
            this.VendProperty.InfoConnection = this.InfoConnection1;
            this.VendProperty.MultiSetWhere = false;
            this.VendProperty.Name = "VendProperty";
            this.VendProperty.NotificationAutoEnlist = false;
            this.VendProperty.SecExcept = null;
            this.VendProperty.SecFieldName = null;
            this.VendProperty.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VendProperty.SelectPaging = false;
            this.VendProperty.SelectTop = 0;
            this.VendProperty.SiteControl = false;
            this.VendProperty.SiteFieldName = null;
            this.VendProperty.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VendGrade
            // 
            this.VendGrade.CacheConnection = false;
            this.VendGrade.CommandText = "SELECT ID,Content,ContentGrid FROM VendGrade\r\nORDER BY ID";
            this.VendGrade.CommandTimeout = 30;
            this.VendGrade.CommandType = System.Data.CommandType.Text;
            this.VendGrade.DynamicTableName = false;
            this.VendGrade.EEPAlias = null;
            this.VendGrade.EncodingAfter = null;
            this.VendGrade.EncodingBefore = "Windows-1252";
            this.VendGrade.EncodingConvert = null;
            this.VendGrade.InfoConnection = this.InfoConnection1;
            this.VendGrade.MultiSetWhere = false;
            this.VendGrade.Name = "VendGrade";
            this.VendGrade.NotificationAutoEnlist = false;
            this.VendGrade.SecExcept = null;
            this.VendGrade.SecFieldName = null;
            this.VendGrade.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VendGrade.SelectPaging = false;
            this.VendGrade.SelectTop = 0;
            this.VendGrade.SiteControl = false;
            this.VendGrade.SiteFieldName = null;
            this.VendGrade.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Vendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTypeTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendGrade)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Vendors;
        private Srvtools.UpdateComponent ucVendors;
        private Srvtools.InfoCommand View_Vendors;
        private Srvtools.AutoNumber autoVendID;
        private Srvtools.InfoCommand PayTerm;
        private Srvtools.InfoCommand Bank;
        private Srvtools.InfoCommand ItemType;
        private Srvtools.InfoCommand ItemTypeTree;
        private Srvtools.InfoCommand ItemUnit;
        private Srvtools.InfoCommand VendLevel;
        private Srvtools.InfoCommand VendProperty;
        private Srvtools.InfoCommand VendGrade;
    }
}
