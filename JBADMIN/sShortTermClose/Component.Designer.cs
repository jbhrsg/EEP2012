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
            Srvtools.Service service2 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
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
            service2.DelegateName = "PutShortTermEnd";
            service2.NonLogin = false;
            service2.ServiceName = "PutShortTermEnd";
            this.serviceManager1.ServiceCollection.Add(service2);
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
            this.ShortTerm.EEPAlias = "";
            this.ShortTerm.EncodingAfter = null;
            this.ShortTerm.EncodingBefore = "Windows-1252";
            this.ShortTerm.EncodingConvert = null;
            this.ShortTerm.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "ShortTermNO";
            this.ShortTerm.KeyFields.Add(keyItem7);
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
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "ShortTermNO";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "ShortTermGist";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "ShortTermDescr";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "PlanPayDate";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "ShortTermAmount";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "PayTypeID";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "CheckDays";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "CheckTitle";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "RequestDate";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            fieldAttr34.CheckNull = false;
            fieldAttr34.DataField = "ShortTermDate";
            fieldAttr34.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr34.DefaultValue = null;
            fieldAttr34.TrimLength = 0;
            fieldAttr34.UpdateEnable = true;
            fieldAttr34.WhereMode = true;
            fieldAttr35.CheckNull = false;
            fieldAttr35.DataField = "RequisitionNO";
            fieldAttr35.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr35.DefaultValue = null;
            fieldAttr35.TrimLength = 0;
            fieldAttr35.UpdateEnable = true;
            fieldAttr35.WhereMode = true;
            fieldAttr36.CheckNull = false;
            fieldAttr36.DataField = "Flowflag";
            fieldAttr36.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr36.DefaultValue = null;
            fieldAttr36.TrimLength = 0;
            fieldAttr36.UpdateEnable = true;
            fieldAttr36.WhereMode = true;
            fieldAttr37.CheckNull = false;
            fieldAttr37.DataField = "EmployeeID";
            fieldAttr37.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr37.DefaultValue = null;
            fieldAttr37.TrimLength = 0;
            fieldAttr37.UpdateEnable = true;
            fieldAttr37.WhereMode = true;
            fieldAttr38.CheckNull = false;
            fieldAttr38.DataField = "ApplyOrg_NO";
            fieldAttr38.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr38.DefaultValue = null;
            fieldAttr38.TrimLength = 0;
            fieldAttr38.UpdateEnable = true;
            fieldAttr38.WhereMode = true;
            fieldAttr39.CheckNull = false;
            fieldAttr39.DataField = "Org_NOParent";
            fieldAttr39.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr39.DefaultValue = null;
            fieldAttr39.TrimLength = 0;
            fieldAttr39.UpdateEnable = true;
            fieldAttr39.WhereMode = true;
            fieldAttr40.CheckNull = false;
            fieldAttr40.DataField = "CompanyID";
            fieldAttr40.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr40.DefaultValue = null;
            fieldAttr40.TrimLength = 0;
            fieldAttr40.UpdateEnable = true;
            fieldAttr40.WhereMode = true;
            fieldAttr41.CheckNull = false;
            fieldAttr41.DataField = "PayTo";
            fieldAttr41.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr41.DefaultValue = null;
            fieldAttr41.TrimLength = 0;
            fieldAttr41.UpdateEnable = true;
            fieldAttr41.WhereMode = true;
            fieldAttr42.CheckNull = false;
            fieldAttr42.DataField = "IsSettleAccount";
            fieldAttr42.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr42.DefaultValue = null;
            fieldAttr42.TrimLength = 0;
            fieldAttr42.UpdateEnable = true;
            fieldAttr42.WhereMode = true;
            fieldAttr43.CheckNull = false;
            fieldAttr43.DataField = "SettleAccountDate";
            fieldAttr43.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr43.DefaultValue = null;
            fieldAttr43.TrimLength = 0;
            fieldAttr43.UpdateEnable = true;
            fieldAttr43.WhereMode = true;
            fieldAttr44.CheckNull = false;
            fieldAttr44.DataField = "EmployerID";
            fieldAttr44.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr44.DefaultValue = null;
            fieldAttr44.TrimLength = 0;
            fieldAttr44.UpdateEnable = true;
            fieldAttr44.WhereMode = true;
            fieldAttr45.CheckNull = false;
            fieldAttr45.DataField = "ShortTermTypeID";
            fieldAttr45.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr45.DefaultValue = null;
            fieldAttr45.TrimLength = 0;
            fieldAttr45.UpdateEnable = true;
            fieldAttr45.WhereMode = true;
            fieldAttr46.CheckNull = false;
            fieldAttr46.DataField = "CostNotes";
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
            this.ucShortTerm.FieldAttrs.Add(fieldAttr25);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr26);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr27);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr28);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr29);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr30);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr31);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr32);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr33);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr34);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr35);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr36);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr37);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr38);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr39);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr40);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr41);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr42);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr43);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr44);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr45);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr46);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr47);
            this.ucShortTerm.FieldAttrs.Add(fieldAttr48);
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
            keyItem1.KeyName = "ShortTermNO";
            this.View_ShortTerm.KeyFields.Add(keyItem1);
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
            this.ShortTermMinusDetails.EEPAlias = "";
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
    "ROM  [192.168.1.41].fwcrm.dbo.Employer\r\nOrder by EmployerName";
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
            keyItem2.KeyName = "PayTypeID";
            this.PayType.KeyFields.Add(keyItem2);
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
            keyItem3.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem3);
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
            this.Applyer.CommandText = "SELECT  EmployeeID,\r\n(SELECT USERNAME FROM EIPHRSYS.DBO.USERS WHERE USERID= SHORT" +
    "TERM.EMPLOYEEID) AS EmployeeName\r\nFROM  SHORTTERM\r\nWHERE  FLOWFLAG=\'Z\'\r\nGROUP BY" +
    " EmployeeID";
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
            keyItem4.KeyName = "SHORTTERMNO";
            this.ShortTermNO.KeyFields.Add(keyItem4);
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
            keyItem5.KeyName = "CompanyID";
            this.Company.KeyFields.Add(keyItem5);
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
