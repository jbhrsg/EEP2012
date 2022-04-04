namespace sPettyCash
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem9 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem10 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.PettyCash = new Srvtools.InfoCommand(this.components);
            this.ucPettyCash = new Srvtools.UpdateComponent(this.components);
            this.View_PettyCash = new Srvtools.InfoCommand(this.components);
            this.ProofType = new Srvtools.InfoCommand(this.components);
            this.AccountTitle = new Srvtools.InfoCommand(this.components);
            this.CostCenter = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            this.autoPettyCashID = new Srvtools.AutoNumber(this.components);
            this.PayType = new Srvtools.InfoCommand(this.components);
            this.AccountType = new Srvtools.InfoCommand(this.components);
            this.BudgetBase = new Srvtools.InfoCommand(this.components);
            this.YearMonth = new Srvtools.InfoCommand(this.components);
            this.VoucherYear = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PettyCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PettyCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProofType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BudgetBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoucherYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetEmpFlowAgentList";
            service1.NonLogin = false;
            service1.ServiceName = "GetEmpFlowAgentList";
            service2.DelegateName = "GetUserOrgNOs";
            service2.NonLogin = false;
            service2.ServiceName = "GetUserOrgNOs";
            service3.DelegateName = "GetUserOrgNO";
            service3.NonLogin = false;
            service3.ServiceName = "GetUserOrgNO";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // PettyCash
            // 
            this.PettyCash.CacheConnection = false;
            this.PettyCash.CommandText = "SELECT dbo.[PettyCash].* FROM dbo.[PettyCash]";
            this.PettyCash.CommandTimeout = 30;
            this.PettyCash.CommandType = System.Data.CommandType.Text;
            this.PettyCash.DynamicTableName = false;
            this.PettyCash.EEPAlias = null;
            this.PettyCash.EncodingAfter = null;
            this.PettyCash.EncodingBefore = "Windows-1252";
            this.PettyCash.EncodingConvert = null;
            this.PettyCash.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "PettyCashID";
            this.PettyCash.KeyFields.Add(keyItem1);
            this.PettyCash.MultiSetWhere = false;
            this.PettyCash.Name = "PettyCash";
            this.PettyCash.NotificationAutoEnlist = false;
            this.PettyCash.SecExcept = null;
            this.PettyCash.SecFieldName = null;
            this.PettyCash.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PettyCash.SelectPaging = false;
            this.PettyCash.SelectTop = 0;
            this.PettyCash.SiteControl = false;
            this.PettyCash.SiteFieldName = null;
            this.PettyCash.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPettyCash
            // 
            this.ucPettyCash.AutoTrans = true;
            this.ucPettyCash.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "PettyCashID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ApplyDate";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ApplyOrg_NO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CostCenterID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ProofTypeID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ProofNO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AccountNotes";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "PettyCashAmt";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "PettyCashTax";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "PayTypeID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "AccountYM";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "AccountID";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "Flowflag";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateBy";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = "_username";
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
            this.ucPettyCash.FieldAttrs.Add(fieldAttr1);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr2);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr3);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr4);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr5);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr6);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr7);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr8);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr9);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr10);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr11);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr12);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr13);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr14);
            this.ucPettyCash.FieldAttrs.Add(fieldAttr15);
            this.ucPettyCash.LogInfo = null;
            this.ucPettyCash.Name = "ucPettyCash";
            this.ucPettyCash.RowAffectsCheck = true;
            this.ucPettyCash.SelectCmd = this.PettyCash;
            this.ucPettyCash.SelectCmdForUpdate = null;
            this.ucPettyCash.SendSQLCmd = true;
            this.ucPettyCash.ServerModify = true;
            this.ucPettyCash.ServerModifyGetMax = false;
            this.ucPettyCash.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPettyCash.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPettyCash.UseTranscationScope = false;
            this.ucPettyCash.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucPettyCash.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucPettyCash_BeforeInsert);
            // 
            // View_PettyCash
            // 
            this.View_PettyCash.CacheConnection = false;
            this.View_PettyCash.CommandText = "SELECT * FROM dbo.[PettyCash]";
            this.View_PettyCash.CommandTimeout = 30;
            this.View_PettyCash.CommandType = System.Data.CommandType.Text;
            this.View_PettyCash.DynamicTableName = false;
            this.View_PettyCash.EEPAlias = null;
            this.View_PettyCash.EncodingAfter = null;
            this.View_PettyCash.EncodingBefore = "Windows-1252";
            this.View_PettyCash.EncodingConvert = null;
            this.View_PettyCash.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "PettyCashID";
            this.View_PettyCash.KeyFields.Add(keyItem2);
            this.View_PettyCash.MultiSetWhere = false;
            this.View_PettyCash.Name = "View_PettyCash";
            this.View_PettyCash.NotificationAutoEnlist = false;
            this.View_PettyCash.SecExcept = null;
            this.View_PettyCash.SecFieldName = null;
            this.View_PettyCash.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_PettyCash.SelectPaging = false;
            this.View_PettyCash.SelectTop = 0;
            this.View_PettyCash.SiteControl = false;
            this.View_PettyCash.SiteFieldName = null;
            this.View_PettyCash.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ProofType
            // 
            this.ProofType.CacheConnection = false;
            this.ProofType.CommandText = "select ProofType.ProofTypeID,ProofType.ProofTypeName from ProofType\r\nwhere proofT" +
    "ypeID<>0 AND proofTypeID<>4";
            this.ProofType.CommandTimeout = 30;
            this.ProofType.CommandType = System.Data.CommandType.Text;
            this.ProofType.DynamicTableName = false;
            this.ProofType.EEPAlias = null;
            this.ProofType.EncodingAfter = null;
            this.ProofType.EncodingBefore = "Windows-1252";
            this.ProofType.EncodingConvert = null;
            this.ProofType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ProofTypeID";
            this.ProofType.KeyFields.Add(keyItem3);
            this.ProofType.MultiSetWhere = false;
            this.ProofType.Name = "ProofType";
            this.ProofType.NotificationAutoEnlist = false;
            this.ProofType.SecExcept = null;
            this.ProofType.SecFieldName = null;
            this.ProofType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ProofType.SelectPaging = false;
            this.ProofType.SelectTop = 0;
            this.ProofType.SiteControl = false;
            this.ProofType.SiteFieldName = null;
            this.ProofType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccountTitle
            // 
            this.AccountTitle.CacheConnection = false;
            this.AccountTitle.CommandText = "select AccountTitle.AccountID,AccountTitle.AccountName,AccountTitle.PayTypeID,\r\nA" +
    "ccountTitle.LimitCostCenters\r\nfrom AccountTitle\r\nwhere IsPettyCash=1\r\norder by  " +
    "AccountName";
            this.AccountTitle.CommandTimeout = 30;
            this.AccountTitle.CommandType = System.Data.CommandType.Text;
            this.AccountTitle.DynamicTableName = false;
            this.AccountTitle.EEPAlias = null;
            this.AccountTitle.EncodingAfter = null;
            this.AccountTitle.EncodingBefore = "Windows-1252";
            this.AccountTitle.EncodingConvert = null;
            this.AccountTitle.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AccountID";
            this.AccountTitle.KeyFields.Add(keyItem4);
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
            // CostCenter
            // 
            this.CostCenter.CacheConnection = false;
            this.CostCenter.CommandText = "select glCostCenter.CostCenterID,\r\nglCostCenter.CostCenterName from glCostCenter " +
    "where IsActive=1\r\norder by  glCostCenter.CostCenterID";
            this.CostCenter.CommandTimeout = 30;
            this.CostCenter.CommandType = System.Data.CommandType.Text;
            this.CostCenter.DynamicTableName = false;
            this.CostCenter.EEPAlias = null;
            this.CostCenter.EncodingAfter = null;
            this.CostCenter.EncodingBefore = "Windows-1252";
            this.CostCenter.EncodingConvert = null;
            this.CostCenter.InfoConnection = this.InfoConnection1;
            this.CostCenter.MultiSetWhere = false;
            this.CostCenter.Name = "CostCenter";
            this.CostCenter.NotificationAutoEnlist = false;
            this.CostCenter.SecExcept = null;
            this.CostCenter.SecFieldName = null;
            this.CostCenter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CostCenter.SelectPaging = false;
            this.CostCenter.SelectTop = 0;
            this.CostCenter.SiteControl = false;
            this.CostCenter.SiteFieldName = null;
            this.CostCenter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = resources.GetString("Employee.CommandText");
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem5);
            this.Employee.MultiSetWhere = false;
            this.Employee.Name = "Employee";
            this.Employee.NotificationAutoEnlist = false;
            this.Employee.SecExcept = null;
            this.Employee.SecFieldName = null;
            this.Employee.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Employee.SelectPaging = false;
            this.Employee.SelectTop = 0;
            this.Employee.SiteControl = false;
            this.Employee.SiteFieldName = null;
            this.Employee.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Organization
            // 
            this.Organization.CacheConnection = false;
            this.Organization.CommandText = resources.GetString("Organization.CommandText");
            this.Organization.CommandTimeout = 30;
            this.Organization.CommandType = System.Data.CommandType.Text;
            this.Organization.DynamicTableName = false;
            this.Organization.EEPAlias = null;
            this.Organization.EncodingAfter = null;
            this.Organization.EncodingBefore = "Windows-1252";
            this.Organization.EncodingConvert = null;
            this.Organization.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem6);
            this.Organization.MultiSetWhere = false;
            this.Organization.Name = "Organization";
            this.Organization.NotificationAutoEnlist = false;
            this.Organization.SecExcept = null;
            this.Organization.SecFieldName = null;
            this.Organization.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Organization.SelectPaging = false;
            this.Organization.SelectTop = 0;
            this.Organization.SiteControl = false;
            this.Organization.SiteFieldName = null;
            this.Organization.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoPettyCashID
            // 
            this.autoPettyCashID.Active = true;
            this.autoPettyCashID.AutoNoID = "PettyCashID";
            this.autoPettyCashID.Description = null;
            this.autoPettyCashID.GetFixed = "P";
            this.autoPettyCashID.isNumFill = false;
            this.autoPettyCashID.Name = "autoPettyCashID";
            this.autoPettyCashID.Number = null;
            this.autoPettyCashID.NumDig = 5;
            this.autoPettyCashID.OldVersion = false;
            this.autoPettyCashID.OverFlow = true;
            this.autoPettyCashID.StartValue = 1;
            this.autoPettyCashID.Step = 1;
            this.autoPettyCashID.TargetColumn = "PettyCashID";
            this.autoPettyCashID.UpdateComp = this.ucPettyCash;
            // 
            // PayType
            // 
            this.PayType.CacheConnection = false;
            this.PayType.CommandText = "select PayType.PayTypeID,PayType.PayTypeName from PayType";
            this.PayType.CommandTimeout = 30;
            this.PayType.CommandType = System.Data.CommandType.Text;
            this.PayType.DynamicTableName = false;
            this.PayType.EEPAlias = null;
            this.PayType.EncodingAfter = null;
            this.PayType.EncodingBefore = "Windows-1252";
            this.PayType.EncodingConvert = null;
            this.PayType.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "PayTypeID";
            this.PayType.KeyFields.Add(keyItem7);
            this.PayType.MultiSetWhere = false;
            this.PayType.Name = "PayType";
            this.PayType.NotificationAutoEnlist = false;
            this.PayType.SecExcept = null;
            this.PayType.SecFieldName = null;
            this.PayType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PayType.SelectPaging = false;
            this.PayType.SelectTop = 0;
            this.PayType.SiteControl = false;
            this.PayType.SiteFieldName = null;
            this.PayType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccountType
            // 
            this.AccountType.CacheConnection = false;
            this.AccountType.CommandText = "SELECT * FROM glBudgetType Where BudgetType=2 OR BudgetType=3\r\n\r\n";
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
            // BudgetBase
            // 
            this.BudgetBase.CacheConnection = false;
            this.BudgetBase.CommandText = "SELECT AutoKey,CostCenterID,AcSubno,AcnoName+\' (\'+Acno_S+SubAcno_S+\')\' AS AcnoNam" +
    "e,BudgetType,Acno_S,SubAcno_S\r\nFROM glYearBudgetBase ORDER BY CostCenterID,AcSub" +
    "no\r\n";
            this.BudgetBase.CommandTimeout = 30;
            this.BudgetBase.CommandType = System.Data.CommandType.Text;
            this.BudgetBase.DynamicTableName = false;
            this.BudgetBase.EEPAlias = null;
            this.BudgetBase.EncodingAfter = null;
            this.BudgetBase.EncodingBefore = "Windows-1252";
            this.BudgetBase.EncodingConvert = null;
            this.BudgetBase.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "InsGroupID";
            this.BudgetBase.KeyFields.Add(keyItem8);
            this.BudgetBase.MultiSetWhere = false;
            this.BudgetBase.Name = "BudgetBase";
            this.BudgetBase.NotificationAutoEnlist = false;
            this.BudgetBase.SecExcept = null;
            this.BudgetBase.SecFieldName = null;
            this.BudgetBase.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.BudgetBase.SelectPaging = false;
            this.BudgetBase.SelectTop = 0;
            this.BudgetBase.SiteControl = false;
            this.BudgetBase.SiteFieldName = null;
            this.BudgetBase.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // YearMonth
            // 
            this.YearMonth.CacheConnection = false;
            this.YearMonth.CommandText = resources.GetString("YearMonth.CommandText");
            this.YearMonth.CommandTimeout = 30;
            this.YearMonth.CommandType = System.Data.CommandType.Text;
            this.YearMonth.DynamicTableName = false;
            this.YearMonth.EEPAlias = null;
            this.YearMonth.EncodingAfter = null;
            this.YearMonth.EncodingBefore = "Windows-1252";
            this.YearMonth.EncodingConvert = null;
            this.YearMonth.InfoConnection = this.InfoConnection1;
            this.YearMonth.MultiSetWhere = false;
            this.YearMonth.Name = "YearMonth";
            this.YearMonth.NotificationAutoEnlist = false;
            this.YearMonth.SecExcept = null;
            this.YearMonth.SecFieldName = null;
            this.YearMonth.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.YearMonth.SelectPaging = false;
            this.YearMonth.SelectTop = 0;
            this.YearMonth.SiteControl = false;
            this.YearMonth.SiteFieldName = null;
            this.YearMonth.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VoucherYear
            // 
            this.VoucherYear.CacheConnection = false;
            this.VoucherYear.CommandText = "SELECT  TOP 2 VoucherYear  FROM glYearBudget\r\nGROUP BY VoucherYear ORDER BY Vouch" +
    "erYear DESC\r\n";
            this.VoucherYear.CommandTimeout = 30;
            this.VoucherYear.CommandType = System.Data.CommandType.Text;
            this.VoucherYear.DynamicTableName = false;
            this.VoucherYear.EEPAlias = null;
            this.VoucherYear.EncodingAfter = null;
            this.VoucherYear.EncodingBefore = "Windows-1252";
            this.VoucherYear.EncodingConvert = null;
            this.VoucherYear.InfoConnection = this.InfoConnection1;
            keyItem9.KeyName = "InsGroupID";
            this.VoucherYear.KeyFields.Add(keyItem9);
            this.VoucherYear.MultiSetWhere = false;
            this.VoucherYear.Name = "VoucherYear";
            this.VoucherYear.NotificationAutoEnlist = false;
            this.VoucherYear.SecExcept = null;
            this.VoucherYear.SecFieldName = null;
            this.VoucherYear.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VoucherYear.SelectPaging = false;
            this.VoucherYear.SelectTop = 0;
            this.VoucherYear.SiteControl = false;
            this.VoucherYear.SiteFieldName = null;
            this.VoucherYear.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "SELECT  InsGroupID AS CompanyID,InsGroupShortName AS CompanyName\r\nFROM InsGroup W" +
    "here IsActive = 1\r\nORDER BY  InsGroupID\r\n\r\n";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = null;
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.InfoConnection1;
            keyItem10.KeyName = "InsGroupID";
            this.InsGroup.KeyFields.Add(keyItem10);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PettyCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PettyCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProofType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BudgetBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoucherYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand PettyCash;
        private Srvtools.UpdateComponent ucPettyCash;
        private Srvtools.InfoCommand View_PettyCash;
        private Srvtools.InfoCommand ProofType;
        private Srvtools.InfoCommand AccountTitle;
        private Srvtools.InfoCommand CostCenter;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand Organization;
        private Srvtools.AutoNumber autoPettyCashID;
        private Srvtools.InfoCommand PayType;
        private Srvtools.InfoCommand AccountType;
        private Srvtools.InfoCommand BudgetBase;
        private Srvtools.InfoCommand YearMonth;
        private Srvtools.InfoCommand VoucherYear;
        private Srvtools.InfoCommand InsGroup;
    }
}
