namespace sContract
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_Customer = new Srvtools.InfoCommand(this.components);
            this.ucHUT_Customer = new Srvtools.UpdateComponent(this.components);
            this.HUT_Contract = new Srvtools.InfoCommand(this.components);
            this.ucHUT_Contract = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_Customer = new Srvtools.InfoCommand(this.components);
            this.View_HUT_Contract = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.SYS_Users = new Srvtools.InfoCommand(this.components);
            this.HUT_Hunter = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Contract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Contract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_Users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Hunter)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelContract";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelContract";
            service2.DelegateName = "CheckContractNODul";
            service2.NonLogin = false;
            service2.ServiceName = "CheckContractNODul";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_Customer
            // 
            this.HUT_Customer.CacheConnection = false;
            this.HUT_Customer.CommandText = "SELECT [HUT_Customer].[CustID],[HUT_Customer].[CustTaxNo],[HUT_Customer].[CustNam" +
    "e],[HUT_Customer].[CustShortName] FROM [HUT_Customer]";
            this.HUT_Customer.CommandTimeout = 30;
            this.HUT_Customer.CommandType = System.Data.CommandType.Text;
            this.HUT_Customer.DynamicTableName = false;
            this.HUT_Customer.EEPAlias = null;
            this.HUT_Customer.EncodingAfter = null;
            this.HUT_Customer.EncodingBefore = "Windows-1252";
            this.HUT_Customer.InfoConnection = this.InfoConnection1;
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
            fieldAttr1.DataField = "CustID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustTaxNo";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CustName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CustShortName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_Customer.LogInfo = null;
            this.ucHUT_Customer.Name = "ucHUT_Customer";
            this.ucHUT_Customer.RowAffectsCheck = true;
            this.ucHUT_Customer.SelectCmd = this.HUT_Customer;
            this.ucHUT_Customer.SelectCmdForUpdate = null;
            this.ucHUT_Customer.ServerModify = true;
            this.ucHUT_Customer.ServerModifyGetMax = false;
            this.ucHUT_Customer.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_Customer.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_Customer.UseTranscationScope = false;
            this.ucHUT_Customer.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHUT_Customer.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHUT_Customer_BeforeInsert);
            this.ucHUT_Customer.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucHUT_Customer_BeforeModify);
            // 
            // HUT_Contract
            // 
            this.HUT_Contract.CacheConnection = false;
            this.HUT_Contract.CommandText = resources.GetString("HUT_Contract.CommandText");
            this.HUT_Contract.CommandTimeout = 30;
            this.HUT_Contract.CommandType = System.Data.CommandType.Text;
            this.HUT_Contract.DynamicTableName = false;
            this.HUT_Contract.EEPAlias = null;
            this.HUT_Contract.EncodingAfter = null;
            this.HUT_Contract.EncodingBefore = "Windows-1252";
            this.HUT_Contract.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ContractNO";
            this.HUT_Contract.KeyFields.Add(keyItem1);
            this.HUT_Contract.MultiSetWhere = false;
            this.HUT_Contract.Name = "HUT_Contract";
            this.HUT_Contract.NotificationAutoEnlist = false;
            this.HUT_Contract.SecExcept = null;
            this.HUT_Contract.SecFieldName = null;
            this.HUT_Contract.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_Contract.SelectPaging = false;
            this.HUT_Contract.SelectTop = 0;
            this.HUT_Contract.SiteControl = false;
            this.HUT_Contract.SiteFieldName = null;
            this.HUT_Contract.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_Contract
            // 
            this.ucHUT_Contract.AutoTrans = true;
            this.ucHUT_Contract.ExceptJoin = false;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ContractNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ContContent";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ContDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ConStdDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ConEndDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ContEffYear";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CustID";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateBy";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = "_username";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = "";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpdateBy";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr14.DefaultValue = "_username";
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr15.DefaultValue = "";
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr7);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr8);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr9);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr10);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr11);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr12);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr13);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr14);
            this.ucHUT_Contract.FieldAttrs.Add(fieldAttr15);
            this.ucHUT_Contract.LogInfo = null;
            this.ucHUT_Contract.Name = "ucHUT_Contract";
            this.ucHUT_Contract.RowAffectsCheck = true;
            this.ucHUT_Contract.SelectCmd = this.HUT_Contract;
            this.ucHUT_Contract.SelectCmdForUpdate = null;
            this.ucHUT_Contract.ServerModify = true;
            this.ucHUT_Contract.ServerModifyGetMax = false;
            this.ucHUT_Contract.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_Contract.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_Contract.UseTranscationScope = false;
            this.ucHUT_Contract.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHUT_Contract.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHUT_Contract_BeforeInsert);
            this.ucHUT_Contract.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucHUT_Contract_BeforeModify);
            // 
            // View_HUT_Customer
            // 
            this.View_HUT_Customer.CacheConnection = false;
            this.View_HUT_Customer.CommandText = "SELECT * FROM [HUT_Customer]";
            this.View_HUT_Customer.CommandTimeout = 30;
            this.View_HUT_Customer.CommandType = System.Data.CommandType.Text;
            this.View_HUT_Customer.DynamicTableName = false;
            this.View_HUT_Customer.EEPAlias = null;
            this.View_HUT_Customer.EncodingAfter = null;
            this.View_HUT_Customer.EncodingBefore = "Windows-1252";
            this.View_HUT_Customer.InfoConnection = this.InfoConnection1;
            this.View_HUT_Customer.MultiSetWhere = false;
            this.View_HUT_Customer.Name = "View_HUT_Customer";
            this.View_HUT_Customer.NotificationAutoEnlist = false;
            this.View_HUT_Customer.SecExcept = null;
            this.View_HUT_Customer.SecFieldName = null;
            this.View_HUT_Customer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_Customer.SelectPaging = false;
            this.View_HUT_Customer.SelectTop = 0;
            this.View_HUT_Customer.SiteControl = false;
            this.View_HUT_Customer.SiteFieldName = null;
            this.View_HUT_Customer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_HUT_Contract
            // 
            this.View_HUT_Contract.CacheConnection = false;
            this.View_HUT_Contract.CommandText = "SELECT * FROM [HUT_Contract]";
            this.View_HUT_Contract.CommandTimeout = 30;
            this.View_HUT_Contract.CommandType = System.Data.CommandType.Text;
            this.View_HUT_Contract.DynamicTableName = false;
            this.View_HUT_Contract.EEPAlias = null;
            this.View_HUT_Contract.EncodingAfter = null;
            this.View_HUT_Contract.EncodingBefore = "Windows-1252";
            this.View_HUT_Contract.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ContractNO";
            this.View_HUT_Contract.KeyFields.Add(keyItem2);
            this.View_HUT_Contract.MultiSetWhere = false;
            this.View_HUT_Contract.Name = "View_HUT_Contract";
            this.View_HUT_Contract.NotificationAutoEnlist = false;
            this.View_HUT_Contract.SecExcept = null;
            this.View_HUT_Contract.SecFieldName = null;
            this.View_HUT_Contract.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_Contract.SelectPaging = false;
            this.View_HUT_Contract.SelectTop = 0;
            this.View_HUT_Contract.SiteControl = false;
            this.View_HUT_Contract.SiteFieldName = null;
            this.View_HUT_Contract.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = false;
            this.autoNumber1.AutoNoID = "ContractNO";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "GetContFixed()";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 4;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "ContractNO";
            this.autoNumber1.UpdateComp = this.ucHUT_Contract;
            // 
            // SYS_Users
            // 
            this.SYS_Users.CacheConnection = false;
            this.SYS_Users.CommandText = "select SYS_USERS.USERID,SYS_USERS.USERNAME \r\nfrom SYS_USERS ORDER BY SYS_USERS.US" +
    "ERID";
            this.SYS_Users.CommandTimeout = 30;
            this.SYS_Users.CommandType = System.Data.CommandType.Text;
            this.SYS_Users.DynamicTableName = false;
            this.SYS_Users.EEPAlias = null;
            this.SYS_Users.EncodingAfter = null;
            this.SYS_Users.EncodingBefore = "Windows-1252";
            this.SYS_Users.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "USERID";
            this.SYS_Users.KeyFields.Add(keyItem3);
            this.SYS_Users.MultiSetWhere = false;
            this.SYS_Users.Name = "SYS_Users";
            this.SYS_Users.NotificationAutoEnlist = false;
            this.SYS_Users.SecExcept = null;
            this.SYS_Users.SecFieldName = null;
            this.SYS_Users.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SYS_Users.SelectPaging = false;
            this.SYS_Users.SelectTop = 0;
            this.SYS_Users.SiteControl = false;
            this.SYS_Users.SiteFieldName = null;
            this.SYS_Users.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_Hunter
            // 
            this.HUT_Hunter.CacheConnection = false;
            this.HUT_Hunter.CommandText = "select HUT_Hunter.ID,HUT_Hunter.HunterName from HUT_Hunter";
            this.HUT_Hunter.CommandTimeout = 30;
            this.HUT_Hunter.CommandType = System.Data.CommandType.Text;
            this.HUT_Hunter.DynamicTableName = false;
            this.HUT_Hunter.EEPAlias = null;
            this.HUT_Hunter.EncodingAfter = null;
            this.HUT_Hunter.EncodingBefore = "Windows-1252";
            this.HUT_Hunter.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ID";
            this.HUT_Hunter.KeyFields.Add(keyItem4);
            this.HUT_Hunter.MultiSetWhere = false;
            this.HUT_Hunter.Name = "HUT_Hunter";
            this.HUT_Hunter.NotificationAutoEnlist = false;
            this.HUT_Hunter.SecExcept = null;
            this.HUT_Hunter.SecFieldName = null;
            this.HUT_Hunter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_Hunter.SelectPaging = false;
            this.HUT_Hunter.SelectTop = 0;
            this.HUT_Hunter.SiteControl = false;
            this.HUT_Hunter.SiteFieldName = null;
            this.HUT_Hunter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Contract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Contract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_Users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Hunter)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_Customer;
        private Srvtools.UpdateComponent ucHUT_Customer;
        private Srvtools.InfoCommand HUT_Contract;
        private Srvtools.UpdateComponent ucHUT_Contract;
        private Srvtools.InfoCommand View_HUT_Customer;
        private Srvtools.InfoCommand View_HUT_Contract;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand SYS_Users;
        private Srvtools.InfoCommand HUT_Hunter;
    }
}
