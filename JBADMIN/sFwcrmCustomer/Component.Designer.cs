namespace sFwcrmCustomer
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Employer = new Srvtools.InfoCommand(this.components);
            this.infoConnection3JS = new Srvtools.InfoConnection(this.components);
            this.ucEmployer = new Srvtools.UpdateComponent(this.components);
            this.CountryCity = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.infoFee = new Srvtools.InfoCommand(this.components);
            this.infoSalesId = new Srvtools.InfoCommand(this.components);
            this.infoSalaryDate = new Srvtools.InfoCommand(this.components);
            this.infoFeeJS = new Srvtools.InfoCommand(this.components);
            this.infoSalesIdJS = new Srvtools.InfoCommand(this.components);
            this.infoSalaryDateJS = new Srvtools.InfoCommand(this.components);
            this.infoConnection3JSCare = new Srvtools.InfoConnection(this.components);
            this.ucEmployerJS = new Srvtools.UpdateComponent(this.components);
            this.EmployerJS = new Srvtools.InfoCommand(this.components);
            this.EmployerJSCare = new Srvtools.InfoCommand(this.components);
            this.ucEmployerJSCare = new Srvtools.UpdateComponent(this.components);
            this.infoFeeJSCare = new Srvtools.InfoCommand(this.components);
            this.infoSalesIdJSCare = new Srvtools.InfoCommand(this.components);
            this.infoSalaryDateJSCare = new Srvtools.InfoCommand(this.components);
            this.HUT_CustomerContactRecord = new Srvtools.InfoCommand(this.components);
            this.ucHUT_CustomerContactRecord = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection3JS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountryCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalaryDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFeeJS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesIdJS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalaryDateJS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection3JSCare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployerJS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployerJSCare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFeeJSCare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesIdJSCare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalaryDateJSCare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_CustomerContactRecord)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "FWCRM";
            // 
            // Employer
            // 
            this.Employer.CacheConnection = false;
            this.Employer.CommandText = resources.GetString("Employer.CommandText");
            this.Employer.CommandTimeout = 30;
            this.Employer.CommandType = System.Data.CommandType.Text;
            this.Employer.DynamicTableName = false;
            this.Employer.EEPAlias = "FWCRM";
            this.Employer.EncodingAfter = null;
            this.Employer.EncodingBefore = "Windows-1252";
            this.Employer.EncodingConvert = null;
            this.Employer.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.Employer.KeyFields.Add(keyItem1);
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
            // infoConnection3JS
            // 
            this.infoConnection3JS.EEPAlias = "FWCRMJS";
            // 
            // ucEmployer
            // 
            this.ucEmployer.AutoTrans = true;
            this.ucEmployer.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CustAreaID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustAreaName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            this.ucEmployer.FieldAttrs.Add(fieldAttr1);
            this.ucEmployer.FieldAttrs.Add(fieldAttr2);
            this.ucEmployer.LogInfo = null;
            this.ucEmployer.Name = "ucEmployer";
            this.ucEmployer.RowAffectsCheck = true;
            this.ucEmployer.SelectCmd = this.Employer;
            this.ucEmployer.SelectCmdForUpdate = null;
            this.ucEmployer.SendSQLCmd = true;
            this.ucEmployer.ServerModify = true;
            this.ucEmployer.ServerModifyGetMax = false;
            this.ucEmployer.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmployer.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmployer.UseTranscationScope = false;
            this.ucEmployer.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucEmployer.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucEmployer_BeforeModify);
            this.ucEmployer.AfterModify += new Srvtools.UpdateComponentAfterModifyEventHandler(this.ucEmployer_AfterModify);
            // 
            // CountryCity
            // 
            this.CountryCity.CacheConnection = false;
            this.CountryCity.CommandText = "select AutoKey,Country,City,ZIPCode from CountryCity";
            this.CountryCity.CommandTimeout = 30;
            this.CountryCity.CommandType = System.Data.CommandType.Text;
            this.CountryCity.DynamicTableName = false;
            this.CountryCity.EEPAlias = "JBERP";
            this.CountryCity.EncodingAfter = null;
            this.CountryCity.EncodingBefore = "Windows-1252";
            this.CountryCity.EncodingConvert = null;
            this.CountryCity.InfoConnection = this.infoConnection2;
            keyItem2.KeyName = "AutoKey";
            this.CountryCity.KeyFields.Add(keyItem2);
            this.CountryCity.MultiSetWhere = false;
            this.CountryCity.Name = "CountryCity";
            this.CountryCity.NotificationAutoEnlist = false;
            this.CountryCity.SecExcept = null;
            this.CountryCity.SecFieldName = null;
            this.CountryCity.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CountryCity.SelectPaging = false;
            this.CountryCity.SelectTop = 0;
            this.CountryCity.SiteControl = false;
            this.CountryCity.SiteFieldName = null;
            this.CountryCity.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBERP";
            // 
            // infoFee
            // 
            this.infoFee.CacheConnection = false;
            this.infoFee.CommandText = "select FeeID as ID,FeeName as Name\r\n\t\t\tfrom feeitem\r\n\t\twhere IsActive=1 \r\n\t\torder" +
    " by FeeName";
            this.infoFee.CommandTimeout = 30;
            this.infoFee.CommandType = System.Data.CommandType.Text;
            this.infoFee.DynamicTableName = false;
            this.infoFee.EEPAlias = "FWCRM";
            this.infoFee.EncodingAfter = null;
            this.infoFee.EncodingBefore = "Windows-1252";
            this.infoFee.EncodingConvert = null;
            this.infoFee.InfoConnection = this.InfoConnection1;
            this.infoFee.MultiSetWhere = false;
            this.infoFee.Name = "infoFee";
            this.infoFee.NotificationAutoEnlist = false;
            this.infoFee.SecExcept = null;
            this.infoFee.SecFieldName = null;
            this.infoFee.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoFee.SelectPaging = false;
            this.infoFee.SelectTop = 0;
            this.infoFee.SiteControl = false;
            this.infoFee.SiteFieldName = null;
            this.infoFee.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesId
            // 
            this.infoSalesId.CacheConnection = false;
            this.infoSalesId.CommandText = "select  AutoKey as ID, AttendantName as Name\r\n\t\tfrom Attendant where IsActive=1 \r" +
    "\n\t\torder by Name";
            this.infoSalesId.CommandTimeout = 30;
            this.infoSalesId.CommandType = System.Data.CommandType.Text;
            this.infoSalesId.DynamicTableName = false;
            this.infoSalesId.EEPAlias = "FWCRM";
            this.infoSalesId.EncodingAfter = null;
            this.infoSalesId.EncodingBefore = "Windows-1252";
            this.infoSalesId.EncodingConvert = null;
            this.infoSalesId.InfoConnection = this.InfoConnection1;
            this.infoSalesId.MultiSetWhere = false;
            this.infoSalesId.Name = "infoSalesId";
            this.infoSalesId.NotificationAutoEnlist = false;
            this.infoSalesId.SecExcept = null;
            this.infoSalesId.SecFieldName = null;
            this.infoSalesId.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesId.SelectPaging = false;
            this.infoSalesId.SelectTop = 0;
            this.infoSalesId.SiteControl = false;
            this.infoSalesId.SiteFieldName = null;
            this.infoSalesId.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalaryDate
            // 
            this.infoSalaryDate.CacheConnection = false;
            this.infoSalaryDate.CommandText = " Select AutoKey,ListCategory,ListID,ListContent\r\n  From ReferenceTable\r\n  Where L" +
    "istCategory=\'SalaryDate\' AND IsActive=1";
            this.infoSalaryDate.CommandTimeout = 30;
            this.infoSalaryDate.CommandType = System.Data.CommandType.Text;
            this.infoSalaryDate.DynamicTableName = false;
            this.infoSalaryDate.EEPAlias = "FWCRM";
            this.infoSalaryDate.EncodingAfter = null;
            this.infoSalaryDate.EncodingBefore = "Windows-1252";
            this.infoSalaryDate.EncodingConvert = null;
            this.infoSalaryDate.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.infoSalaryDate.KeyFields.Add(keyItem3);
            this.infoSalaryDate.MultiSetWhere = false;
            this.infoSalaryDate.Name = "infoSalaryDate";
            this.infoSalaryDate.NotificationAutoEnlist = false;
            this.infoSalaryDate.SecExcept = null;
            this.infoSalaryDate.SecFieldName = null;
            this.infoSalaryDate.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalaryDate.SelectPaging = false;
            this.infoSalaryDate.SelectTop = 0;
            this.infoSalaryDate.SiteControl = false;
            this.infoSalaryDate.SiteFieldName = null;
            this.infoSalaryDate.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoFeeJS
            // 
            this.infoFeeJS.CacheConnection = false;
            this.infoFeeJS.CommandText = "select FeeID as ID,FeeName as Name\r\n\t\t\tfrom feeitem\r\n\t\twhere IsActive=1 \r\n\t\torder" +
    " by FeeName";
            this.infoFeeJS.CommandTimeout = 30;
            this.infoFeeJS.CommandType = System.Data.CommandType.Text;
            this.infoFeeJS.DynamicTableName = false;
            this.infoFeeJS.EEPAlias = "FWCRMJS";
            this.infoFeeJS.EncodingAfter = null;
            this.infoFeeJS.EncodingBefore = "Windows-1252";
            this.infoFeeJS.EncodingConvert = null;
            this.infoFeeJS.InfoConnection = this.infoConnection3JS;
            this.infoFeeJS.MultiSetWhere = false;
            this.infoFeeJS.Name = "infoFeeJS";
            this.infoFeeJS.NotificationAutoEnlist = false;
            this.infoFeeJS.SecExcept = null;
            this.infoFeeJS.SecFieldName = null;
            this.infoFeeJS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoFeeJS.SelectPaging = false;
            this.infoFeeJS.SelectTop = 0;
            this.infoFeeJS.SiteControl = false;
            this.infoFeeJS.SiteFieldName = null;
            this.infoFeeJS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesIdJS
            // 
            this.infoSalesIdJS.CacheConnection = false;
            this.infoSalesIdJS.CommandText = "select  AutoKey as ID, AttendantName as Name\r\n\t\tfrom Attendant where IsActive=1 \r" +
    "\n\t\torder by Name";
            this.infoSalesIdJS.CommandTimeout = 30;
            this.infoSalesIdJS.CommandType = System.Data.CommandType.Text;
            this.infoSalesIdJS.DynamicTableName = false;
            this.infoSalesIdJS.EEPAlias = "FWCRMJS";
            this.infoSalesIdJS.EncodingAfter = null;
            this.infoSalesIdJS.EncodingBefore = "Windows-1252";
            this.infoSalesIdJS.EncodingConvert = null;
            this.infoSalesIdJS.InfoConnection = this.infoConnection3JS;
            this.infoSalesIdJS.MultiSetWhere = false;
            this.infoSalesIdJS.Name = "infoSalesIdJS";
            this.infoSalesIdJS.NotificationAutoEnlist = false;
            this.infoSalesIdJS.SecExcept = null;
            this.infoSalesIdJS.SecFieldName = null;
            this.infoSalesIdJS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesIdJS.SelectPaging = false;
            this.infoSalesIdJS.SelectTop = 0;
            this.infoSalesIdJS.SiteControl = false;
            this.infoSalesIdJS.SiteFieldName = null;
            this.infoSalesIdJS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalaryDateJS
            // 
            this.infoSalaryDateJS.CacheConnection = false;
            this.infoSalaryDateJS.CommandText = " Select AutoKey,ListCategory,ListID,ListContent\r\n  From ReferenceTable\r\n  Where L" +
    "istCategory=\'SalaryDate\' AND IsActive=1";
            this.infoSalaryDateJS.CommandTimeout = 30;
            this.infoSalaryDateJS.CommandType = System.Data.CommandType.Text;
            this.infoSalaryDateJS.DynamicTableName = false;
            this.infoSalaryDateJS.EEPAlias = "FWCRMJS";
            this.infoSalaryDateJS.EncodingAfter = null;
            this.infoSalaryDateJS.EncodingBefore = "Windows-1252";
            this.infoSalaryDateJS.EncodingConvert = null;
            this.infoSalaryDateJS.InfoConnection = this.infoConnection3JS;
            keyItem4.KeyName = "AutoKey";
            this.infoSalaryDateJS.KeyFields.Add(keyItem4);
            this.infoSalaryDateJS.MultiSetWhere = false;
            this.infoSalaryDateJS.Name = "infoSalaryDateJS";
            this.infoSalaryDateJS.NotificationAutoEnlist = false;
            this.infoSalaryDateJS.SecExcept = null;
            this.infoSalaryDateJS.SecFieldName = null;
            this.infoSalaryDateJS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalaryDateJS.SelectPaging = false;
            this.infoSalaryDateJS.SelectTop = 0;
            this.infoSalaryDateJS.SiteControl = false;
            this.infoSalaryDateJS.SiteFieldName = null;
            this.infoSalaryDateJS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection3JSCare
            // 
            this.infoConnection3JSCare.EEPAlias = "FWCRMJSCare";
            // 
            // ucEmployerJS
            // 
            this.ucEmployerJS.AutoTrans = true;
            this.ucEmployerJS.ExceptJoin = false;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CustAreaID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CustAreaName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucEmployerJS.FieldAttrs.Add(fieldAttr3);
            this.ucEmployerJS.FieldAttrs.Add(fieldAttr4);
            this.ucEmployerJS.LogInfo = null;
            this.ucEmployerJS.Name = "ucEmployerJS";
            this.ucEmployerJS.RowAffectsCheck = true;
            this.ucEmployerJS.SelectCmd = this.EmployerJS;
            this.ucEmployerJS.SelectCmdForUpdate = null;
            this.ucEmployerJS.SendSQLCmd = true;
            this.ucEmployerJS.ServerModify = true;
            this.ucEmployerJS.ServerModifyGetMax = false;
            this.ucEmployerJS.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmployerJS.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmployerJS.UseTranscationScope = false;
            this.ucEmployerJS.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucEmployerJS.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucEmployerJS_BeforeModify);
            this.ucEmployerJS.AfterModify += new Srvtools.UpdateComponentAfterModifyEventHandler(this.ucEmployerJS_AfterModify);
            // 
            // EmployerJS
            // 
            this.EmployerJS.CacheConnection = false;
            this.EmployerJS.CommandText = resources.GetString("EmployerJS.CommandText");
            this.EmployerJS.CommandTimeout = 30;
            this.EmployerJS.CommandType = System.Data.CommandType.Text;
            this.EmployerJS.DynamicTableName = false;
            this.EmployerJS.EEPAlias = "FWCRMJS";
            this.EmployerJS.EncodingAfter = null;
            this.EmployerJS.EncodingBefore = "Windows-1252";
            this.EmployerJS.EncodingConvert = null;
            this.EmployerJS.InfoConnection = this.infoConnection3JS;
            keyItem5.KeyName = "AutoKey";
            this.EmployerJS.KeyFields.Add(keyItem5);
            this.EmployerJS.MultiSetWhere = false;
            this.EmployerJS.Name = "EmployerJS";
            this.EmployerJS.NotificationAutoEnlist = false;
            this.EmployerJS.SecExcept = null;
            this.EmployerJS.SecFieldName = null;
            this.EmployerJS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EmployerJS.SelectPaging = false;
            this.EmployerJS.SelectTop = 0;
            this.EmployerJS.SiteControl = false;
            this.EmployerJS.SiteFieldName = null;
            this.EmployerJS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // EmployerJSCare
            // 
            this.EmployerJSCare.CacheConnection = false;
            this.EmployerJSCare.CommandText = "SELECT  *\r\nfrom View_FwcrmEmployerJSCare\r\norder by IsActive desc,LastUpdateDate d" +
    "esc";
            this.EmployerJSCare.CommandTimeout = 30;
            this.EmployerJSCare.CommandType = System.Data.CommandType.Text;
            this.EmployerJSCare.DynamicTableName = false;
            this.EmployerJSCare.EEPAlias = "FWCRMJSCare";
            this.EmployerJSCare.EncodingAfter = null;
            this.EmployerJSCare.EncodingBefore = "Windows-1252";
            this.EmployerJSCare.EncodingConvert = null;
            this.EmployerJSCare.InfoConnection = this.infoConnection3JSCare;
            keyItem6.KeyName = "AutoKey";
            this.EmployerJSCare.KeyFields.Add(keyItem6);
            this.EmployerJSCare.MultiSetWhere = false;
            this.EmployerJSCare.Name = "EmployerJSCare";
            this.EmployerJSCare.NotificationAutoEnlist = false;
            this.EmployerJSCare.SecExcept = null;
            this.EmployerJSCare.SecFieldName = null;
            this.EmployerJSCare.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EmployerJSCare.SelectPaging = false;
            this.EmployerJSCare.SelectTop = 0;
            this.EmployerJSCare.SiteControl = false;
            this.EmployerJSCare.SiteFieldName = null;
            this.EmployerJSCare.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucEmployerJSCare
            // 
            this.ucEmployerJSCare.AutoTrans = true;
            this.ucEmployerJSCare.ExceptJoin = false;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CustAreaID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CustAreaName";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucEmployerJSCare.FieldAttrs.Add(fieldAttr5);
            this.ucEmployerJSCare.FieldAttrs.Add(fieldAttr6);
            this.ucEmployerJSCare.LogInfo = null;
            this.ucEmployerJSCare.Name = "ucEmployerJSCare";
            this.ucEmployerJSCare.RowAffectsCheck = true;
            this.ucEmployerJSCare.SelectCmd = this.EmployerJSCare;
            this.ucEmployerJSCare.SelectCmdForUpdate = null;
            this.ucEmployerJSCare.SendSQLCmd = true;
            this.ucEmployerJSCare.ServerModify = true;
            this.ucEmployerJSCare.ServerModifyGetMax = false;
            this.ucEmployerJSCare.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmployerJSCare.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmployerJSCare.UseTranscationScope = false;
            this.ucEmployerJSCare.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucEmployerJSCare.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucEmployerJSCare_BeforeModify);
            this.ucEmployerJSCare.AfterModify += new Srvtools.UpdateComponentAfterModifyEventHandler(this.ucEmployerJSCare_AfterModify);
            // 
            // infoFeeJSCare
            // 
            this.infoFeeJSCare.CacheConnection = false;
            this.infoFeeJSCare.CommandText = "select FeeID as ID,FeeName as Name\r\n\t\t\tfrom feeitem\r\n\t\twhere IsActive=1 \r\n\t\torder" +
    " by FeeName";
            this.infoFeeJSCare.CommandTimeout = 30;
            this.infoFeeJSCare.CommandType = System.Data.CommandType.Text;
            this.infoFeeJSCare.DynamicTableName = false;
            this.infoFeeJSCare.EEPAlias = "FWCRMJSCare";
            this.infoFeeJSCare.EncodingAfter = null;
            this.infoFeeJSCare.EncodingBefore = "Windows-1252";
            this.infoFeeJSCare.EncodingConvert = null;
            this.infoFeeJSCare.InfoConnection = this.infoConnection3JSCare;
            this.infoFeeJSCare.MultiSetWhere = false;
            this.infoFeeJSCare.Name = "infoFeeJSCare";
            this.infoFeeJSCare.NotificationAutoEnlist = false;
            this.infoFeeJSCare.SecExcept = null;
            this.infoFeeJSCare.SecFieldName = null;
            this.infoFeeJSCare.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoFeeJSCare.SelectPaging = false;
            this.infoFeeJSCare.SelectTop = 0;
            this.infoFeeJSCare.SiteControl = false;
            this.infoFeeJSCare.SiteFieldName = null;
            this.infoFeeJSCare.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesIdJSCare
            // 
            this.infoSalesIdJSCare.CacheConnection = false;
            this.infoSalesIdJSCare.CommandText = "select  AutoKey as ID, AttendantName as Name\r\n\t\tfrom Attendant where IsActive=1 \r" +
    "\n\t\torder by Name";
            this.infoSalesIdJSCare.CommandTimeout = 30;
            this.infoSalesIdJSCare.CommandType = System.Data.CommandType.Text;
            this.infoSalesIdJSCare.DynamicTableName = false;
            this.infoSalesIdJSCare.EEPAlias = "FWCRMJSCare";
            this.infoSalesIdJSCare.EncodingAfter = null;
            this.infoSalesIdJSCare.EncodingBefore = "Windows-1252";
            this.infoSalesIdJSCare.EncodingConvert = null;
            this.infoSalesIdJSCare.InfoConnection = this.infoConnection3JSCare;
            this.infoSalesIdJSCare.MultiSetWhere = false;
            this.infoSalesIdJSCare.Name = "infoSalesIdJSCare";
            this.infoSalesIdJSCare.NotificationAutoEnlist = false;
            this.infoSalesIdJSCare.SecExcept = null;
            this.infoSalesIdJSCare.SecFieldName = null;
            this.infoSalesIdJSCare.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesIdJSCare.SelectPaging = false;
            this.infoSalesIdJSCare.SelectTop = 0;
            this.infoSalesIdJSCare.SiteControl = false;
            this.infoSalesIdJSCare.SiteFieldName = null;
            this.infoSalesIdJSCare.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalaryDateJSCare
            // 
            this.infoSalaryDateJSCare.CacheConnection = false;
            this.infoSalaryDateJSCare.CommandText = " Select AutoKey,ListCategory,ListID,ListContent\r\n  From ReferenceTable\r\n  Where L" +
    "istCategory=\'SalaryDate\' AND IsActive=1";
            this.infoSalaryDateJSCare.CommandTimeout = 30;
            this.infoSalaryDateJSCare.CommandType = System.Data.CommandType.Text;
            this.infoSalaryDateJSCare.DynamicTableName = false;
            this.infoSalaryDateJSCare.EEPAlias = "FWCRMJSCare";
            this.infoSalaryDateJSCare.EncodingAfter = null;
            this.infoSalaryDateJSCare.EncodingBefore = "Windows-1252";
            this.infoSalaryDateJSCare.EncodingConvert = null;
            this.infoSalaryDateJSCare.InfoConnection = this.infoConnection3JSCare;
            keyItem7.KeyName = "AutoKey";
            this.infoSalaryDateJSCare.KeyFields.Add(keyItem7);
            this.infoSalaryDateJSCare.MultiSetWhere = false;
            this.infoSalaryDateJSCare.Name = "infoSalaryDateJSCare";
            this.infoSalaryDateJSCare.NotificationAutoEnlist = false;
            this.infoSalaryDateJSCare.SecExcept = null;
            this.infoSalaryDateJSCare.SecFieldName = null;
            this.infoSalaryDateJSCare.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalaryDateJSCare.SelectPaging = false;
            this.infoSalaryDateJSCare.SelectTop = 0;
            this.infoSalaryDateJSCare.SiteControl = false;
            this.infoSalaryDateJSCare.SiteFieldName = null;
            this.infoSalaryDateJSCare.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_CustomerContactRecord
            // 
            this.HUT_CustomerContactRecord.CacheConnection = false;
            this.HUT_CustomerContactRecord.CommandText = "select  *,dbo.funReturnContactLogsViewer(ShareTo) as ShareToName  from ContactLog" +
    "s\r\nwhere SalesKindID=\'S35\'\r\norder by ContactDate desc";
            this.HUT_CustomerContactRecord.CommandTimeout = 30;
            this.HUT_CustomerContactRecord.CommandType = System.Data.CommandType.Text;
            this.HUT_CustomerContactRecord.DynamicTableName = false;
            this.HUT_CustomerContactRecord.EEPAlias = "JBERP";
            this.HUT_CustomerContactRecord.EncodingAfter = null;
            this.HUT_CustomerContactRecord.EncodingBefore = "Windows-1252";
            this.HUT_CustomerContactRecord.EncodingConvert = null;
            this.HUT_CustomerContactRecord.InfoConnection = this.infoConnection2;
            keyItem8.KeyName = "AutoKey";
            this.HUT_CustomerContactRecord.KeyFields.Add(keyItem8);
            this.HUT_CustomerContactRecord.MultiSetWhere = false;
            this.HUT_CustomerContactRecord.Name = "HUT_CustomerContactRecord";
            this.HUT_CustomerContactRecord.NotificationAutoEnlist = false;
            this.HUT_CustomerContactRecord.SecExcept = null;
            this.HUT_CustomerContactRecord.SecFieldName = null;
            this.HUT_CustomerContactRecord.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_CustomerContactRecord.SelectPaging = false;
            this.HUT_CustomerContactRecord.SelectTop = 0;
            this.HUT_CustomerContactRecord.SiteControl = false;
            this.HUT_CustomerContactRecord.SiteFieldName = null;
            this.HUT_CustomerContactRecord.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_CustomerContactRecord
            // 
            this.ucHUT_CustomerContactRecord.AutoTrans = true;
            this.ucHUT_CustomerContactRecord.ExceptJoin = false;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CustID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "UpdateDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "UpdateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr7);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr8);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr9);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr10);
            this.ucHUT_CustomerContactRecord.FieldAttrs.Add(fieldAttr11);
            this.ucHUT_CustomerContactRecord.LogInfo = null;
            this.ucHUT_CustomerContactRecord.Name = "ucHUT_CustomerContactRecord";
            this.ucHUT_CustomerContactRecord.RowAffectsCheck = true;
            this.ucHUT_CustomerContactRecord.SelectCmd = this.HUT_CustomerContactRecord;
            this.ucHUT_CustomerContactRecord.SelectCmdForUpdate = null;
            this.ucHUT_CustomerContactRecord.SendSQLCmd = true;
            this.ucHUT_CustomerContactRecord.ServerModify = true;
            this.ucHUT_CustomerContactRecord.ServerModifyGetMax = true;
            this.ucHUT_CustomerContactRecord.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_CustomerContactRecord.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_CustomerContactRecord.UseTranscationScope = false;
            this.ucHUT_CustomerContactRecord.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection3JS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountryCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalaryDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFeeJS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesIdJS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalaryDateJS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection3JSCare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployerJS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployerJSCare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFeeJSCare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesIdJSCare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalaryDateJSCare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_CustomerContactRecord)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Employer;
        private Srvtools.UpdateComponent ucEmployer;
        private Srvtools.InfoCommand CountryCity;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand infoFee;
        private Srvtools.InfoCommand infoSalesId;
        private Srvtools.InfoCommand infoSalaryDate;
        private Srvtools.InfoConnection infoConnection3JS;
        private Srvtools.InfoCommand infoFeeJS;
        private Srvtools.InfoCommand infoSalesIdJS;
        private Srvtools.InfoCommand infoSalaryDateJS;
        private Srvtools.InfoConnection infoConnection3JSCare;
        private Srvtools.UpdateComponent ucEmployerJS;
        private Srvtools.InfoCommand EmployerJS;
        private Srvtools.InfoCommand EmployerJSCare;
        private Srvtools.UpdateComponent ucEmployerJSCare;
        private Srvtools.InfoCommand infoFeeJSCare;
        private Srvtools.InfoCommand infoSalesIdJSCare;
        private Srvtools.InfoCommand infoSalaryDateJSCare;
        private Srvtools.InfoCommand HUT_CustomerContactRecord;
        private Srvtools.UpdateComponent ucHUT_CustomerContactRecord;
    }
}
