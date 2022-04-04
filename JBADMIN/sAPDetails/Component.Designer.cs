namespace sAPDetails
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
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
            Srvtools.FieldAttr fieldAttr37 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr38 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr39 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr40 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr41 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr42 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr43 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr44 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr45 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr46 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr47 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr48 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr49 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr50 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.APDetails = new Srvtools.InfoCommand(this.components);
            this.ucAPDetails = new Srvtools.UpdateComponent(this.components);
            this.View_APDetails = new Srvtools.InfoCommand(this.components);
            this.APType = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.glCostCenter = new Srvtools.InfoCommand(this.components);
            this.Vendors = new Srvtools.InfoCommand(this.components);
            this.POPayType = new Srvtools.InfoCommand(this.components);
            this.BudgetAccount = new Srvtools.InfoCommand(this.components);
            this.Vendors1 = new Srvtools.InfoCommand(this.components);
            this.PayWay = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.APDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_APDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.APType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.POPayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BudgetAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayWay)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // APDetails
            // 
            this.APDetails.CacheConnection = false;
            this.APDetails.CommandText = resources.GetString("APDetails.CommandText");
            this.APDetails.CommandTimeout = 30;
            this.APDetails.CommandType = System.Data.CommandType.Text;
            this.APDetails.DynamicTableName = false;
            this.APDetails.EEPAlias = null;
            this.APDetails.EncodingAfter = null;
            this.APDetails.EncodingBefore = "Windows-1252";
            this.APDetails.EncodingConvert = null;
            this.APDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.APDetails.KeyFields.Add(keyItem2);
            this.APDetails.MultiSetWhere = false;
            this.APDetails.Name = "APDetails";
            this.APDetails.NotificationAutoEnlist = false;
            this.APDetails.SecExcept = null;
            this.APDetails.SecFieldName = null;
            this.APDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.APDetails.SelectPaging = false;
            this.APDetails.SelectTop = 0;
            this.APDetails.SiteControl = false;
            this.APDetails.SiteFieldName = null;
            this.APDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucAPDetails
            // 
            this.ucAPDetails.AutoTrans = true;
            this.ucAPDetails.ExceptJoin = false;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "AutoKey";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "APNO";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "APTypeID";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "InsGroupID";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "CostCenterID";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "AccountType";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "AccountID";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "BillNO";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            fieldAttr34.CheckNull = false;
            fieldAttr34.DataField = "ItemNO";
            fieldAttr34.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr34.DefaultValue = null;
            fieldAttr34.TrimLength = 0;
            fieldAttr34.UpdateEnable = true;
            fieldAttr34.WhereMode = true;
            fieldAttr35.CheckNull = false;
            fieldAttr35.DataField = "APDescr";
            fieldAttr35.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr35.DefaultValue = null;
            fieldAttr35.TrimLength = 0;
            fieldAttr35.UpdateEnable = true;
            fieldAttr35.WhereMode = true;
            fieldAttr36.CheckNull = false;
            fieldAttr36.DataField = "APDate";
            fieldAttr36.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr36.DefaultValue = null;
            fieldAttr36.TrimLength = 0;
            fieldAttr36.UpdateEnable = true;
            fieldAttr36.WhereMode = true;
            fieldAttr37.CheckNull = false;
            fieldAttr37.DataField = "APQty";
            fieldAttr37.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr37.DefaultValue = null;
            fieldAttr37.TrimLength = 0;
            fieldAttr37.UpdateEnable = true;
            fieldAttr37.WhereMode = true;
            fieldAttr38.CheckNull = false;
            fieldAttr38.DataField = "APAcceptDate";
            fieldAttr38.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr38.DefaultValue = null;
            fieldAttr38.TrimLength = 0;
            fieldAttr38.UpdateEnable = true;
            fieldAttr38.WhereMode = true;
            fieldAttr39.CheckNull = false;
            fieldAttr39.DataField = "APPrice";
            fieldAttr39.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr39.DefaultValue = null;
            fieldAttr39.TrimLength = 0;
            fieldAttr39.UpdateEnable = true;
            fieldAttr39.WhereMode = true;
            fieldAttr40.CheckNull = false;
            fieldAttr40.DataField = "APAmount";
            fieldAttr40.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr40.DefaultValue = null;
            fieldAttr40.TrimLength = 0;
            fieldAttr40.UpdateEnable = true;
            fieldAttr40.WhereMode = true;
            fieldAttr41.CheckNull = false;
            fieldAttr41.DataField = "APTax";
            fieldAttr41.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr41.DefaultValue = null;
            fieldAttr41.TrimLength = 0;
            fieldAttr41.UpdateEnable = true;
            fieldAttr41.WhereMode = true;
            fieldAttr42.CheckNull = false;
            fieldAttr42.DataField = "ProofNO";
            fieldAttr42.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr42.DefaultValue = null;
            fieldAttr42.TrimLength = 0;
            fieldAttr42.UpdateEnable = true;
            fieldAttr42.WhereMode = true;
            fieldAttr43.CheckNull = false;
            fieldAttr43.DataField = "PayTo";
            fieldAttr43.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr43.DefaultValue = null;
            fieldAttr43.TrimLength = 0;
            fieldAttr43.UpdateEnable = true;
            fieldAttr43.WhereMode = true;
            fieldAttr44.CheckNull = false;
            fieldAttr44.DataField = "PlanPayDate";
            fieldAttr44.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr44.DefaultValue = null;
            fieldAttr44.TrimLength = 0;
            fieldAttr44.UpdateEnable = true;
            fieldAttr44.WhereMode = true;
            fieldAttr45.CheckNull = false;
            fieldAttr45.DataField = "DebtorDays";
            fieldAttr45.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr45.DefaultValue = null;
            fieldAttr45.TrimLength = 0;
            fieldAttr45.UpdateEnable = true;
            fieldAttr45.WhereMode = true;
            fieldAttr46.CheckNull = false;
            fieldAttr46.DataField = "Remit";
            fieldAttr46.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr46.DefaultValue = null;
            fieldAttr46.TrimLength = 0;
            fieldAttr46.UpdateEnable = true;
            fieldAttr46.WhereMode = true;
            fieldAttr47.CheckNull = false;
            fieldAttr47.DataField = "CreateBy";
            fieldAttr47.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr47.DefaultValue = null;
            fieldAttr47.TrimLength = 0;
            fieldAttr47.UpdateEnable = true;
            fieldAttr47.WhereMode = true;
            fieldAttr48.CheckNull = false;
            fieldAttr48.DataField = "CreateDate";
            fieldAttr48.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr48.DefaultValue = null;
            fieldAttr48.TrimLength = 0;
            fieldAttr48.UpdateEnable = true;
            fieldAttr48.WhereMode = true;
            fieldAttr49.CheckNull = false;
            fieldAttr49.DataField = "LastUpdateBy";
            fieldAttr49.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr49.DefaultValue = null;
            fieldAttr49.TrimLength = 0;
            fieldAttr49.UpdateEnable = true;
            fieldAttr49.WhereMode = true;
            fieldAttr50.CheckNull = false;
            fieldAttr50.DataField = "LastUpdateDate";
            fieldAttr50.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr50.DefaultValue = null;
            fieldAttr50.TrimLength = 0;
            fieldAttr50.UpdateEnable = true;
            fieldAttr50.WhereMode = true;
            this.ucAPDetails.FieldAttrs.Add(fieldAttr26);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr27);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr28);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr29);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr30);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr31);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr32);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr33);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr34);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr35);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr36);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr37);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr38);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr39);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr40);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr41);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr42);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr43);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr44);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr45);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr46);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr47);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr48);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr49);
            this.ucAPDetails.FieldAttrs.Add(fieldAttr50);
            this.ucAPDetails.LogInfo = null;
            this.ucAPDetails.Name = "ucAPDetails";
            this.ucAPDetails.RowAffectsCheck = true;
            this.ucAPDetails.SelectCmd = this.APDetails;
            this.ucAPDetails.SelectCmdForUpdate = null;
            this.ucAPDetails.SendSQLCmd = true;
            this.ucAPDetails.ServerModify = true;
            this.ucAPDetails.ServerModifyGetMax = false;
            this.ucAPDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucAPDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucAPDetails.UseTranscationScope = false;
            this.ucAPDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_APDetails
            // 
            this.View_APDetails.CacheConnection = false;
            this.View_APDetails.CommandText = "SELECT * FROM dbo.[APDetails]";
            this.View_APDetails.CommandTimeout = 30;
            this.View_APDetails.CommandType = System.Data.CommandType.Text;
            this.View_APDetails.DynamicTableName = false;
            this.View_APDetails.EEPAlias = null;
            this.View_APDetails.EncodingAfter = null;
            this.View_APDetails.EncodingBefore = "Windows-1252";
            this.View_APDetails.EncodingConvert = null;
            this.View_APDetails.InfoConnection = this.InfoConnection1;
            this.View_APDetails.MultiSetWhere = false;
            this.View_APDetails.Name = "View_APDetails";
            this.View_APDetails.NotificationAutoEnlist = false;
            this.View_APDetails.SecExcept = null;
            this.View_APDetails.SecFieldName = null;
            this.View_APDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_APDetails.SelectPaging = false;
            this.View_APDetails.SelectTop = 0;
            this.View_APDetails.SiteControl = false;
            this.View_APDetails.SiteFieldName = null;
            this.View_APDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // APType
            // 
            this.APType.CacheConnection = false;
            this.APType.CommandText = "SELECT APTypeID,APTypeName    FROM APType";
            this.APType.CommandTimeout = 30;
            this.APType.CommandType = System.Data.CommandType.Text;
            this.APType.DynamicTableName = false;
            this.APType.EEPAlias = null;
            this.APType.EncodingAfter = null;
            this.APType.EncodingBefore = "Windows-1252";
            this.APType.EncodingConvert = null;
            this.APType.InfoConnection = this.InfoConnection1;
            this.APType.MultiSetWhere = false;
            this.APType.Name = "APType";
            this.APType.NotificationAutoEnlist = false;
            this.APType.SecExcept = null;
            this.APType.SecFieldName = null;
            this.APType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.APType.SelectPaging = false;
            this.APType.SelectTop = 0;
            this.APType.SiteControl = false;
            this.APType.SiteFieldName = null;
            this.APType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "SELECT [InsGroupID] ,[InsGroupShortName]\r\n  FROM [JBADMIN].[dbo].[InsGroup] where" +
    " IsActive=1";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = null;
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.InfoConnection1;
            this.InsGroup.MultiSetWhere = false;
            this.InsGroup.Name = "InsGroup";
            this.InsGroup.NotificationAutoEnlist = false;
            this.InsGroup.SecExcept = null;
            this.InsGroup.SecFieldName = null;
            this.InsGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InsGroup.SelectPaging = false;
            this.InsGroup.SelectTop = 0;
            this.InsGroup.SiteControl = false;
            this.InsGroup.SiteFieldName = null;
            this.InsGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // glCostCenter
            // 
            this.glCostCenter.CacheConnection = false;
            this.glCostCenter.CommandText = "SELECT [CostCenterID],[CostCenterName] FROM [JBADMIN].[dbo].[glCostCenter]\r\n";
            this.glCostCenter.CommandTimeout = 30;
            this.glCostCenter.CommandType = System.Data.CommandType.Text;
            this.glCostCenter.DynamicTableName = false;
            this.glCostCenter.EEPAlias = "";
            this.glCostCenter.EncodingAfter = null;
            this.glCostCenter.EncodingBefore = "Windows-1252";
            this.glCostCenter.EncodingConvert = null;
            this.glCostCenter.InfoConnection = this.InfoConnection1;
            this.glCostCenter.MultiSetWhere = false;
            this.glCostCenter.Name = "glCostCenter";
            this.glCostCenter.NotificationAutoEnlist = false;
            this.glCostCenter.SecExcept = null;
            this.glCostCenter.SecFieldName = null;
            this.glCostCenter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glCostCenter.SelectPaging = false;
            this.glCostCenter.SelectTop = 0;
            this.glCostCenter.SiteControl = false;
            this.glCostCenter.SiteFieldName = null;
            this.glCostCenter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Vendors
            // 
            this.Vendors.CacheConnection = false;
            this.Vendors.CommandText = "Select  VendID,VendName,VendShortName,VendAccountName  From Vendors\r\norder by  Ve" +
    "ndID\r\n              ";
            this.Vendors.CommandTimeout = 30;
            this.Vendors.CommandType = System.Data.CommandType.Text;
            this.Vendors.DynamicTableName = false;
            this.Vendors.EEPAlias = null;
            this.Vendors.EncodingAfter = null;
            this.Vendors.EncodingBefore = "Windows-1252";
            this.Vendors.EncodingConvert = null;
            this.Vendors.InfoConnection = this.InfoConnection1;
            this.Vendors.MultiSetWhere = false;
            this.Vendors.Name = "Vendors";
            this.Vendors.NotificationAutoEnlist = false;
            this.Vendors.SecExcept = null;
            this.Vendors.SecFieldName = null;
            this.Vendors.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Vendors.SelectPaging = false;
            this.Vendors.SelectTop = 0;
            this.Vendors.SiteControl = false;
            this.Vendors.SiteFieldName = null;
            this.Vendors.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // POPayType
            // 
            this.POPayType.CacheConnection = false;
            this.POPayType.CommandText = "SELECT POPAYTYPEID,POPAYTYPENAME FROM POPAYTYPE";
            this.POPayType.CommandTimeout = 30;
            this.POPayType.CommandType = System.Data.CommandType.Text;
            this.POPayType.DynamicTableName = false;
            this.POPayType.EEPAlias = null;
            this.POPayType.EncodingAfter = null;
            this.POPayType.EncodingBefore = "Windows-1252";
            this.POPayType.EncodingConvert = null;
            this.POPayType.InfoConnection = this.InfoConnection1;
            this.POPayType.MultiSetWhere = false;
            this.POPayType.Name = "POPayType";
            this.POPayType.NotificationAutoEnlist = false;
            this.POPayType.SecExcept = null;
            this.POPayType.SecFieldName = null;
            this.POPayType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.POPayType.SelectPaging = false;
            this.POPayType.SelectTop = 0;
            this.POPayType.SiteControl = false;
            this.POPayType.SiteFieldName = null;
            this.POPayType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // BudgetAccount
            // 
            this.BudgetAccount.CacheConnection = false;
            this.BudgetAccount.CommandText = "SELECT AcSubno,AcnoName FROM glyearbudget";
            this.BudgetAccount.CommandTimeout = 30;
            this.BudgetAccount.CommandType = System.Data.CommandType.Text;
            this.BudgetAccount.DynamicTableName = false;
            this.BudgetAccount.EEPAlias = null;
            this.BudgetAccount.EncodingAfter = null;
            this.BudgetAccount.EncodingBefore = "Windows-1252";
            this.BudgetAccount.EncodingConvert = null;
            this.BudgetAccount.InfoConnection = this.InfoConnection1;
            this.BudgetAccount.MultiSetWhere = false;
            this.BudgetAccount.Name = "BudgetAccount";
            this.BudgetAccount.NotificationAutoEnlist = false;
            this.BudgetAccount.SecExcept = null;
            this.BudgetAccount.SecFieldName = null;
            this.BudgetAccount.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.BudgetAccount.SelectPaging = false;
            this.BudgetAccount.SelectTop = 0;
            this.BudgetAccount.SiteControl = false;
            this.BudgetAccount.SiteFieldName = null;
            this.BudgetAccount.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Vendors1
            // 
            this.Vendors1.CacheConnection = false;
            this.Vendors1.CommandText = "Select  VendID,VendName,VendShortName  From Vendors\r\nWhere  (VendID IN (SELECT Ve" +
    "ndID  From  APDetails Group By VendID  ))\r\n              ";
            this.Vendors1.CommandTimeout = 30;
            this.Vendors1.CommandType = System.Data.CommandType.Text;
            this.Vendors1.DynamicTableName = false;
            this.Vendors1.EEPAlias = null;
            this.Vendors1.EncodingAfter = null;
            this.Vendors1.EncodingBefore = "Windows-1252";
            this.Vendors1.EncodingConvert = null;
            this.Vendors1.InfoConnection = this.InfoConnection1;
            this.Vendors1.MultiSetWhere = false;
            this.Vendors1.Name = "Vendors1";
            this.Vendors1.NotificationAutoEnlist = false;
            this.Vendors1.SecExcept = null;
            this.Vendors1.SecFieldName = null;
            this.Vendors1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Vendors1.SelectPaging = false;
            this.Vendors1.SelectTop = 0;
            this.Vendors1.SiteControl = false;
            this.Vendors1.SiteFieldName = null;
            this.Vendors1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PayWay
            // 
            this.PayWay.CacheConnection = false;
            this.PayWay.CommandText = "SELECT PAYWAYID,PAYWAYNAME  \r\nFROM JBERP.DBO.PAYWAY\r\nORDER BY PAYWAYID";
            this.PayWay.CommandTimeout = 30;
            this.PayWay.CommandType = System.Data.CommandType.Text;
            this.PayWay.DynamicTableName = false;
            this.PayWay.EEPAlias = null;
            this.PayWay.EncodingAfter = null;
            this.PayWay.EncodingBefore = "Windows-1252";
            this.PayWay.EncodingConvert = null;
            this.PayWay.InfoConnection = this.InfoConnection1;
            this.PayWay.MultiSetWhere = false;
            this.PayWay.Name = "PayWay";
            this.PayWay.NotificationAutoEnlist = false;
            this.PayWay.SecExcept = null;
            this.PayWay.SecFieldName = null;
            this.PayWay.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PayWay.SelectPaging = false;
            this.PayWay.SelectTop = 0;
            this.PayWay.SiteControl = false;
            this.PayWay.SiteFieldName = null;
            this.PayWay.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.APDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_APDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.APType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.POPayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BudgetAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayWay)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand APDetails;
        private Srvtools.UpdateComponent ucAPDetails;
        private Srvtools.InfoCommand View_APDetails;
        private Srvtools.InfoCommand APType;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoCommand glCostCenter;
        private Srvtools.InfoCommand Vendors;
        private Srvtools.InfoCommand POPayType;
        private Srvtools.InfoCommand BudgetAccount;
        private Srvtools.InfoCommand Vendors1;
        private Srvtools.InfoCommand PayWay;
    }
}
