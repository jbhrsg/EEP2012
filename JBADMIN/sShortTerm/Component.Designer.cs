namespace sShortTerm
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
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.Service service7 = new Srvtools.Service();
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ShortTerm = new Srvtools.InfoCommand(this.components);
            this.ucShortTerm = new Srvtools.UpdateComponent(this.components);
            this.View_ShortTerm = new Srvtools.InfoCommand(this.components);
            this.PayType = new Srvtools.InfoCommand(this.components);
            this.autoShortTermNO = new Srvtools.AutoNumber(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.PayTerm = new Srvtools.InfoCommand(this.components);
            this.Company = new Srvtools.InfoCommand(this.components);
            this.Vendors = new Srvtools.InfoCommand(this.components);
            this.ShortTermType = new Srvtools.InfoCommand(this.components);
            this.Employer = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            this.Sys_ToDoHis = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ShortTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTermType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sys_ToDoHis)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckRequistEnd";
            service1.NonLogin = false;
            service1.ServiceName = "CheckRequistEnd";
            service2.DelegateName = "GetEmpFlowAgentList";
            service2.NonLogin = false;
            service2.ServiceName = "GetEmpFlowAgentList";
            service3.DelegateName = "GetUserOrgNOs";
            service3.NonLogin = false;
            service3.ServiceName = "GetUserOrgNOs";
            service4.DelegateName = "PutShortTermEnd";
            service4.NonLogin = false;
            service4.ServiceName = "PutShortTermEnd";
            service5.DelegateName = "GetSignCount";
            service5.NonLogin = false;
            service5.ServiceName = "GetSignCount";
            service6.DelegateName = "GetSignNotesData";
            service6.NonLogin = false;
            service6.ServiceName = "GetSignNotesData";
            service7.DelegateName = "CheckApplyEmpIsGroupID";
            service7.NonLogin = false;
            service7.ServiceName = "CheckApplyEmpIsGroupID";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
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
            fieldAttr2.DataField = "ShortTermDescr";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "PlanPayDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ShortTermAmount";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "PayTypeID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CheckDays";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CheckTitle";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "RequestDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ShortTermDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "RequisitionNO";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Flowflag";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateBy";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr12.DefaultValue = "";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
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
            this.ucShortTerm.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucShortTerm_BeforeInsert);
            this.ucShortTerm.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucShortTerm_BeforeModify);
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
            // autoShortTermNO
            // 
            this.autoShortTermNO.Active = true;
            this.autoShortTermNO.AutoNoID = "ShortTermNO";
            this.autoShortTermNO.Description = null;
            this.autoShortTermNO.GetFixed = "GetShortTermFixed()";
            this.autoShortTermNO.isNumFill = false;
            this.autoShortTermNO.Name = "autoShortTermNO";
            this.autoShortTermNO.Number = null;
            this.autoShortTermNO.NumDig = 4;
            this.autoShortTermNO.OldVersion = false;
            this.autoShortTermNO.OverFlow = true;
            this.autoShortTermNO.StartValue = 1;
            this.autoShortTermNO.Step = 1;
            this.autoShortTermNO.TargetColumn = "ShortTermNO";
            this.autoShortTermNO.UpdateComp = this.ucShortTerm;
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
            // PayTerm
            // 
            this.PayTerm.CacheConnection = false;
            this.PayTerm.CommandText = "select PayTerm.PayTermID,PayTerm.PayTermName from PayTerm";
            this.PayTerm.CommandTimeout = 30;
            this.PayTerm.CommandType = System.Data.CommandType.Text;
            this.PayTerm.DynamicTableName = false;
            this.PayTerm.EEPAlias = null;
            this.PayTerm.EncodingAfter = null;
            this.PayTerm.EncodingBefore = "Windows-1252";
            this.PayTerm.EncodingConvert = null;
            this.PayTerm.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "PayTermID";
            this.PayTerm.KeyFields.Add(keyItem5);
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
            // ShortTermType
            // 
            this.ShortTermType.CacheConnection = false;
            this.ShortTermType.CommandText = "select ShortTermType.ShortTermTypeID,ShortTermType.ShortTermTypeName from ShortTe" +
    "rmType\r\norder by ShortTermType.ShortTermTypeID";
            this.ShortTermType.CommandTimeout = 30;
            this.ShortTermType.CommandType = System.Data.CommandType.Text;
            this.ShortTermType.DynamicTableName = false;
            this.ShortTermType.EEPAlias = null;
            this.ShortTermType.EncodingAfter = null;
            this.ShortTermType.EncodingBefore = "Windows-1252";
            this.ShortTermType.EncodingConvert = null;
            this.ShortTermType.InfoConnection = this.InfoConnection1;
            this.ShortTermType.MultiSetWhere = false;
            this.ShortTermType.Name = "ShortTermType";
            this.ShortTermType.NotificationAutoEnlist = false;
            this.ShortTermType.SecExcept = null;
            this.ShortTermType.SecFieldName = null;
            this.ShortTermType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ShortTermType.SelectPaging = false;
            this.ShortTermType.SelectTop = 0;
            this.ShortTermType.SiteControl = false;
            this.ShortTermType.SiteFieldName = null;
            this.ShortTermType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employer
            // 
            this.Employer.CacheConnection = false;
            this.Employer.CommandText = "SELECT * FROM [dbo].[View_Employer]\r\nORDER BY EmployerName";
            this.Employer.CommandTimeout = 30;
            this.Employer.CommandType = System.Data.CommandType.Text;
            this.Employer.DynamicTableName = false;
            this.Employer.EEPAlias = "";
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
            // Organization
            // 
            this.Organization.CacheConnection = false;
            this.Organization.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nWHERE (Uppe" +
    "r_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR ORG_NO=\'99999\')\r\nORDER" +
    " BY ORG_NO";
            this.Organization.CommandTimeout = 30;
            this.Organization.CommandType = System.Data.CommandType.Text;
            this.Organization.DynamicTableName = false;
            this.Organization.EEPAlias = null;
            this.Organization.EncodingAfter = null;
            this.Organization.EncodingBefore = "Windows-1252";
            this.Organization.EncodingConvert = null;
            this.Organization.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem7);
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
            // Sys_ToDoHis
            // 
            this.Sys_ToDoHis.CacheConnection = false;
            this.Sys_ToDoHis.CommandText = "SELECT * FROM View_SYS_TODOHIS_REMARK\r\nORDER BY UPDATEDATE DESC";
            this.Sys_ToDoHis.CommandTimeout = 30;
            this.Sys_ToDoHis.CommandType = System.Data.CommandType.Text;
            this.Sys_ToDoHis.DynamicTableName = false;
            this.Sys_ToDoHis.EEPAlias = null;
            this.Sys_ToDoHis.EncodingAfter = null;
            this.Sys_ToDoHis.EncodingBefore = "Windows-1252";
            this.Sys_ToDoHis.EncodingConvert = null;
            this.Sys_ToDoHis.InfoConnection = this.InfoConnection1;
            this.Sys_ToDoHis.MultiSetWhere = false;
            this.Sys_ToDoHis.Name = "Sys_ToDoHis";
            this.Sys_ToDoHis.NotificationAutoEnlist = false;
            this.Sys_ToDoHis.SecExcept = null;
            this.Sys_ToDoHis.SecFieldName = null;
            this.Sys_ToDoHis.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Sys_ToDoHis.SelectPaging = false;
            this.Sys_ToDoHis.SelectTop = 0;
            this.Sys_ToDoHis.SiteControl = false;
            this.Sys_ToDoHis.SiteFieldName = null;
            this.Sys_ToDoHis.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ShortTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTermType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sys_ToDoHis)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ShortTerm;
        private Srvtools.UpdateComponent ucShortTerm;
        private Srvtools.InfoCommand View_ShortTerm;
        private Srvtools.InfoCommand PayType;
        private Srvtools.AutoNumber autoShortTermNO;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand PayTerm;
        private Srvtools.InfoCommand Company;
        private Srvtools.InfoCommand Vendors;
        private Srvtools.InfoCommand ShortTermType;
        private Srvtools.InfoCommand Employer;
        private Srvtools.InfoCommand Organization;
        private Srvtools.InfoCommand Sys_ToDoHis;
    }
}
