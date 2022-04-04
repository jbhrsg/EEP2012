namespace sShortTermClose
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
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ShortTerm = new Srvtools.InfoCommand(this.components);
            this.ucShortTerm = new Srvtools.UpdateComponent(this.components);
            this.View_ShortTerm = new Srvtools.InfoCommand(this.components);
            this.ShortTermMinusDetails = new Srvtools.InfoCommand(this.components);
            this.Employer = new Srvtools.InfoCommand(this.components);
            this.PayType = new Srvtools.InfoCommand(this.components);
            this.Vendors = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.Applyer = new Srvtools.InfoCommand(this.components);
            this.ShortTermNO = new Srvtools.InfoCommand(this.components);
            this.Company = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ShortTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTermMinusDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Applyer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTermNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "PutShortTermEnd";
            service1.NonLogin = false;
            service1.ServiceName = "PutShortTermEnd";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ShortTerm
            // 
            this.ShortTerm.CacheConnection = false;
            this.ShortTerm.CommandText = resources.GetString("ShortTerm.CommandText");
            this.ShortTerm.CommandTimeout = 30;
            this.ShortTerm.CommandType = System.Data.CommandType.Text;
            this.ShortTerm.DynamicTableName = false;
            this.ShortTerm.EEPAlias = null;
            this.ShortTerm.EncodingAfter = null;
            this.ShortTerm.EncodingBefore = "Windows-1252";
            this.ShortTerm.EncodingConvert = null;
            this.ShortTerm.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ShortTermNO";
            this.ShortTerm.KeyFields.Add(keyItem1);
            this.ShortTerm.MultiSetWhere = false;
            this.ShortTerm.Name = "ShortTerm";
            this.ShortTerm.NotificationAutoEnlist = false;
            this.ShortTerm.SecExcept = null;
            this.ShortTerm.SecFieldName = null;
            this.ShortTerm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ShortTerm.SelectPaging = false;
            this.ShortTerm.SelectTop = 0;
            this.ShortTerm.SiteControl = false;
            this.ShortTerm.SiteFieldName = null;
            this.ShortTerm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucShortTerm
            // 
            this.ucShortTerm.AutoTrans = true;
            this.ucShortTerm.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ShortTermNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ShortTermGist";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ShortTermDescr";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "PlanPayDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ShortTermAmount";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "PayTypeID";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CheckDays";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CheckTitle";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "RequestDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ShortTermDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "RequisitionNO";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Flowflag";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "EmployeeID";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "ApplyOrg_NO";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "Org_NOParent";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CompanyID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "PayTo";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "IsSettleAccount";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "SettleAccountDate";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "EmployerID";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "ShortTermTypeID";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "CostNotes";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "CreateBy";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "CreateDate";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            this.ucShortTerm.FieldAttrs.Add(fieldAttr1);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr2);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr3);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr4);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr5);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr6);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr7);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr8);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr9);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr10);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr11);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr12);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr13);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr14);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr15);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr16);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr17);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr18);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr19);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr20);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr21);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr22);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr23);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr24);
            this.ucShortTerm.LogInfo = null;
            this.ucShortTerm.Name = "ucShortTerm";
            this.ucShortTerm.RowAffectsCheck = true;
            this.ucShortTerm.SelectCmd = this.ShortTerm;
            this.ucShortTerm.SelectCmdForUpdate = null;
            this.ucShortTerm.SendSQLCmd = true;
            this.ucShortTerm.ServerModify = true;
            this.ucShortTerm.ServerModifyGetMax = false;
            this.ucShortTerm.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucShortTerm.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucShortTerm.UseTranscationScope = false;
            this.ucShortTerm.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ShortTerm
            // 
            this.View_ShortTerm.CacheConnection = false;
            this.View_ShortTerm.CommandText = "SELECT * FROM dbo.[ShortTerm]";
            this.View_ShortTerm.CommandTimeout = 30;
            this.View_ShortTerm.CommandType = System.Data.CommandType.Text;
            this.View_ShortTerm.DynamicTableName = false;
            this.View_ShortTerm.EEPAlias = null;
            this.View_ShortTerm.EncodingAfter = null;
            this.View_ShortTerm.EncodingBefore = "Windows-1252";
            this.View_ShortTerm.EncodingConvert = null;
            this.View_ShortTerm.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ShortTermNO";
            this.View_ShortTerm.KeyFields.Add(keyItem2);
            this.View_ShortTerm.MultiSetWhere = false;
            this.View_ShortTerm.Name = "View_ShortTerm";
            this.View_ShortTerm.NotificationAutoEnlist = false;
            this.View_ShortTerm.SecExcept = null;
            this.View_ShortTerm.SecFieldName = null;
            this.View_ShortTerm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ShortTerm.SelectPaging = false;
            this.View_ShortTerm.SelectTop = 0;
            this.View_ShortTerm.SiteControl = false;
            this.View_ShortTerm.SiteFieldName = null;
            this.View_ShortTerm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ShortTermMinusDetails
            // 
            this.ShortTermMinusDetails.CacheConnection = false;
            this.ShortTermMinusDetails.CommandText = "SELECT * FROM View_ShortTermMinusDetails";
            this.ShortTermMinusDetails.CommandTimeout = 30;
            this.ShortTermMinusDetails.CommandType = System.Data.CommandType.Text;
            this.ShortTermMinusDetails.DynamicTableName = false;
            this.ShortTermMinusDetails.EEPAlias = null;
            this.ShortTermMinusDetails.EncodingAfter = null;
            this.ShortTermMinusDetails.EncodingBefore = "Windows-1252";
            this.ShortTermMinusDetails.EncodingConvert = null;
            this.ShortTermMinusDetails.InfoConnection = this.InfoConnection1;
            this.ShortTermMinusDetails.MultiSetWhere = false;
            this.ShortTermMinusDetails.Name = "ShortTermMinusDetails";
            this.ShortTermMinusDetails.NotificationAutoEnlist = false;
            this.ShortTermMinusDetails.SecExcept = null;
            this.ShortTermMinusDetails.SecFieldName = null;
            this.ShortTermMinusDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ShortTermMinusDetails.SelectPaging = false;
            this.ShortTermMinusDetails.SelectTop = 0;
            this.ShortTermMinusDetails.SiteControl = false;
            this.ShortTermMinusDetails.SiteFieldName = null;
            this.ShortTermMinusDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employer
            // 
            this.Employer.CacheConnection = false;
            this.Employer.CommandText = "SELECT  EmployerID,\r\n              SUBSTRING(EmployerName,1,4) AS EmployerName\r\nF" +
    "ROM  [60.250.52.106,1433].fwcrm.dbo.Employer\r\nOrder by EmployerName";
            this.Employer.CommandTimeout = 30;
            this.Employer.CommandType = System.Data.CommandType.Text;
            this.Employer.DynamicTableName = false;
            this.Employer.EEPAlias = "JBADMIN";
            this.Employer.EncodingAfter = null;
            this.Employer.EncodingBefore = "Windows-1252";
            this.Employer.EncodingConvert = null;
            this.Employer.InfoConnection = this.InfoConnection1;
            this.Employer.MultiSetWhere = false;
            this.Employer.Name = "Employer";
            this.Employer.NotificationAutoEnlist = false;
            this.Employer.SecExcept = null;
            this.Employer.SecFieldName = null;
            this.Employer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Employer.SelectPaging = false;
            this.Employer.SelectTop = 0;
            this.Employer.SiteControl = false;
            this.Employer.SiteFieldName = null;
            this.Employer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PayType
            // 
            this.PayType.CacheConnection = false;
            this.PayType.CommandText = "select PayType.PayTypeID,\r\n            PayType.PayTypeName\r\nfrom PayType";
            this.PayType.CommandTimeout = 30;
            this.PayType.CommandType = System.Data.CommandType.Text;
            this.PayType.DynamicTableName = false;
            this.PayType.EEPAlias = null;
            this.PayType.EncodingAfter = null;
            this.PayType.EncodingBefore = "Windows-1252";
            this.PayType.EncodingConvert = null;
            this.PayType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "PayTypeID";
            this.PayType.KeyFields.Add(keyItem3);
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
            // Vendors
            // 
            this.Vendors.CacheConnection = false;
            this.Vendors.CommandText = "select Vendors.VendID,\r\n          Vendors.VendShortName \r\nfrom Vendors\r\n\r\norder b" +
    "y Vendors.VendShortName";
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
            keyItem4.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem4);
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
            // Applyer
            // 
            this.Applyer.CacheConnection = false;
            this.Applyer.CommandText = "SELECT DISTINCT  EmployeeID,\r\n(SELECT USERNAME FROM EIPHRSYS.DBO.USERS WHERE USER" +
    "ID= SHORTTERM.EMPLOYEEID) AS EmployeeName\r\nFROM  SHORTTERM\r\nWHERE  FLOWFLAG=\'Z\'";
            this.Applyer.CommandTimeout = 30;
            this.Applyer.CommandType = System.Data.CommandType.Text;
            this.Applyer.DynamicTableName = false;
            this.Applyer.EEPAlias = null;
            this.Applyer.EncodingAfter = null;
            this.Applyer.EncodingBefore = "Windows-1252";
            this.Applyer.EncodingConvert = null;
            this.Applyer.InfoConnection = this.InfoConnection1;
            this.Applyer.MultiSetWhere = false;
            this.Applyer.Name = "Applyer";
            this.Applyer.NotificationAutoEnlist = false;
            this.Applyer.SecExcept = null;
            this.Applyer.SecFieldName = null;
            this.Applyer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Applyer.SelectPaging = false;
            this.Applyer.SelectTop = 0;
            this.Applyer.SiteControl = false;
            this.Applyer.SiteFieldName = null;
            this.Applyer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ShortTermNO
            // 
            this.ShortTermNO.CacheConnection = false;
            this.ShortTermNO.CommandText = "SELECT  DISTINCT SHORTTERMNO  FROM SHORTTERM\r\nWHERE  flowflag=\'Z\'\r\nORDER BY   SHO" +
    "RTTERMNO";
            this.ShortTermNO.CommandTimeout = 30;
            this.ShortTermNO.CommandType = System.Data.CommandType.Text;
            this.ShortTermNO.DynamicTableName = false;
            this.ShortTermNO.EEPAlias = null;
            this.ShortTermNO.EncodingAfter = null;
            this.ShortTermNO.EncodingBefore = "Windows-1252";
            this.ShortTermNO.EncodingConvert = null;
            this.ShortTermNO.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "SHORTTERMNO";
            this.ShortTermNO.KeyFields.Add(keyItem5);
            this.ShortTermNO.MultiSetWhere = false;
            this.ShortTermNO.Name = "ShortTermNO";
            this.ShortTermNO.NotificationAutoEnlist = false;
            this.ShortTermNO.SecExcept = null;
            this.ShortTermNO.SecFieldName = null;
            this.ShortTermNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ShortTermNO.SelectPaging = false;
            this.ShortTermNO.SelectTop = 0;
            this.ShortTermNO.SiteControl = false;
            this.ShortTermNO.SiteFieldName = null;
            this.ShortTermNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Company
            // 
            this.Company.CacheConnection = false;
            this.Company.CommandText = "select Company.CompanyID,Company.CompanyName\r\n from Company\r\norder by companyID";
            this.Company.CommandTimeout = 30;
            this.Company.CommandType = System.Data.CommandType.Text;
            this.Company.DynamicTableName = false;
            this.Company.EEPAlias = null;
            this.Company.EncodingAfter = null;
            this.Company.EncodingBefore = "Windows-1252";
            this.Company.EncodingConvert = null;
            this.Company.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "CompanyID";
            this.Company.KeyFields.Add(keyItem6);
            this.Company.MultiSetWhere = false;
            this.Company.Name = "Company";
            this.Company.NotificationAutoEnlist = false;
            this.Company.SecExcept = null;
            this.Company.SecFieldName = null;
            this.Company.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Company.SelectPaging = false;
            this.Company.SelectTop = 0;
            this.Company.SiteControl = false;
            this.Company.SiteFieldName = null;
            this.Company.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ShortTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTermMinusDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Applyer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTermNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ShortTerm;
        private Srvtools.UpdateComponent ucShortTerm;
        private Srvtools.InfoCommand View_ShortTerm;
        private Srvtools.InfoCommand ShortTermMinusDetails;
        private Srvtools.InfoCommand Employer;
        private Srvtools.InfoCommand PayType;
        private Srvtools.InfoCommand Vendors;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand Applyer;
        private Srvtools.InfoCommand ShortTermNO;
        private Srvtools.InfoCommand Company;
    }
}
