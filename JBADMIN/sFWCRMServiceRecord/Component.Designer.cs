namespace sFWCRMServiceRecord
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMServiceRecordMaster = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMServiceRecordMaster = new Srvtools.UpdateComponent(this.components);
            this.FWCRMServiceRecordDetails = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMServiceRecordDetails = new Srvtools.UpdateComponent(this.components);
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails = new Srvtools.InfoDataSource(this.components);
            this.View_FWCRMServiceRecordMaster = new Srvtools.InfoCommand(this.components);
            this.infoCompanyID = new Srvtools.InfoCommand(this.components);
            this.infoEmployerID = new Srvtools.InfoCommand(this.components);
            this.infoNationalityID = new Srvtools.InfoCommand(this.components);
            this.autoRecordNo = new Srvtools.AutoNumber(this.components);
            this.infoEmployeeID = new Srvtools.InfoCommand(this.components);
            this.infoFWCRMRecordType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMServiceRecordMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMServiceRecordDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_FWCRMServiceRecordMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCompanyID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmployerID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoNationalityID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmployeeID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMRecordType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetEmployer";
            service1.NonLogin = false;
            service1.ServiceName = "GetEmployer";
            service2.DelegateName = "getEmployeeData";
            service2.NonLogin = false;
            service2.ServiceName = "getEmployeeData";
            service3.DelegateName = "ReportFWCRMServiceRecord";
            service3.NonLogin = false;
            service3.ServiceName = "ReportFWCRMServiceRecord";
            service4.DelegateName = "GetNational";
            service4.NonLogin = false;
            service4.ServiceName = "GetNational";
            service5.DelegateName = "AddISODocument";
            service5.NonLogin = false;
            service5.ServiceName = "AddISODocument";
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
            // FWCRMServiceRecordMaster
            // 
            this.FWCRMServiceRecordMaster.CacheConnection = false;
            this.FWCRMServiceRecordMaster.CommandText = resources.GetString("FWCRMServiceRecordMaster.CommandText");
            this.FWCRMServiceRecordMaster.CommandTimeout = 30;
            this.FWCRMServiceRecordMaster.CommandType = System.Data.CommandType.Text;
            this.FWCRMServiceRecordMaster.DynamicTableName = false;
            this.FWCRMServiceRecordMaster.EEPAlias = null;
            this.FWCRMServiceRecordMaster.EncodingAfter = null;
            this.FWCRMServiceRecordMaster.EncodingBefore = "Windows-1252";
            this.FWCRMServiceRecordMaster.EncodingConvert = null;
            this.FWCRMServiceRecordMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "RecordNo";
            this.FWCRMServiceRecordMaster.KeyFields.Add(keyItem1);
            this.FWCRMServiceRecordMaster.MultiSetWhere = false;
            this.FWCRMServiceRecordMaster.Name = "FWCRMServiceRecordMaster";
            this.FWCRMServiceRecordMaster.NotificationAutoEnlist = false;
            this.FWCRMServiceRecordMaster.SecExcept = null;
            this.FWCRMServiceRecordMaster.SecFieldName = null;
            this.FWCRMServiceRecordMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMServiceRecordMaster.SelectPaging = false;
            this.FWCRMServiceRecordMaster.SelectTop = 0;
            this.FWCRMServiceRecordMaster.SiteControl = false;
            this.FWCRMServiceRecordMaster.SiteFieldName = null;
            this.FWCRMServiceRecordMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMServiceRecordMaster
            // 
            this.ucFWCRMServiceRecordMaster.AutoTrans = true;
            this.ucFWCRMServiceRecordMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "Autokey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CompanyID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EmployerID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "NationalityID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Remark";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "LastUpdateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr6);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr7);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr8);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr9);
            this.ucFWCRMServiceRecordMaster.LogInfo = null;
            this.ucFWCRMServiceRecordMaster.Name = "ucFWCRMServiceRecordMaster";
            this.ucFWCRMServiceRecordMaster.RowAffectsCheck = true;
            this.ucFWCRMServiceRecordMaster.SelectCmd = this.FWCRMServiceRecordMaster;
            this.ucFWCRMServiceRecordMaster.SelectCmdForUpdate = null;
            this.ucFWCRMServiceRecordMaster.SendSQLCmd = true;
            this.ucFWCRMServiceRecordMaster.ServerModify = true;
            this.ucFWCRMServiceRecordMaster.ServerModifyGetMax = false;
            this.ucFWCRMServiceRecordMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMServiceRecordMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMServiceRecordMaster.UseTranscationScope = false;
            this.ucFWCRMServiceRecordMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucFWCRMServiceRecordMaster.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucFWCRMServiceRecordMaster_BeforeInsert);
            this.ucFWCRMServiceRecordMaster.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucFWCRMServiceRecordMaster_BeforeModify);
            // 
            // FWCRMServiceRecordDetails
            // 
            this.FWCRMServiceRecordDetails.CacheConnection = false;
            this.FWCRMServiceRecordDetails.CommandText = resources.GetString("FWCRMServiceRecordDetails.CommandText");
            this.FWCRMServiceRecordDetails.CommandTimeout = 30;
            this.FWCRMServiceRecordDetails.CommandType = System.Data.CommandType.Text;
            this.FWCRMServiceRecordDetails.DynamicTableName = false;
            this.FWCRMServiceRecordDetails.EEPAlias = null;
            this.FWCRMServiceRecordDetails.EncodingAfter = null;
            this.FWCRMServiceRecordDetails.EncodingBefore = "Windows-1252";
            this.FWCRMServiceRecordDetails.EncodingConvert = null;
            this.FWCRMServiceRecordDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "iAutokey";
            this.FWCRMServiceRecordDetails.KeyFields.Add(keyItem2);
            this.FWCRMServiceRecordDetails.MultiSetWhere = false;
            this.FWCRMServiceRecordDetails.Name = "FWCRMServiceRecordDetails";
            this.FWCRMServiceRecordDetails.NotificationAutoEnlist = false;
            this.FWCRMServiceRecordDetails.SecExcept = null;
            this.FWCRMServiceRecordDetails.SecFieldName = null;
            this.FWCRMServiceRecordDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMServiceRecordDetails.SelectPaging = false;
            this.FWCRMServiceRecordDetails.SelectTop = 0;
            this.FWCRMServiceRecordDetails.SiteControl = false;
            this.FWCRMServiceRecordDetails.SiteFieldName = null;
            this.FWCRMServiceRecordDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMServiceRecordDetails
            // 
            this.ucFWCRMServiceRecordDetails.AutoTrans = true;
            this.ucFWCRMServiceRecordDetails.ExceptJoin = false;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Autokey";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "MasterKey";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "EmployeeID";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LastUpdateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr10);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr11);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr12);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr13);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr14);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr15);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr16);
            this.ucFWCRMServiceRecordDetails.LogInfo = null;
            this.ucFWCRMServiceRecordDetails.Name = "ucFWCRMServiceRecordDetails";
            this.ucFWCRMServiceRecordDetails.RowAffectsCheck = true;
            this.ucFWCRMServiceRecordDetails.SelectCmd = this.FWCRMServiceRecordDetails;
            this.ucFWCRMServiceRecordDetails.SelectCmdForUpdate = null;
            this.ucFWCRMServiceRecordDetails.SendSQLCmd = true;
            this.ucFWCRMServiceRecordDetails.ServerModify = true;
            this.ucFWCRMServiceRecordDetails.ServerModifyGetMax = false;
            this.ucFWCRMServiceRecordDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMServiceRecordDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMServiceRecordDetails.UseTranscationScope = false;
            this.ucFWCRMServiceRecordDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucFWCRMServiceRecordDetails.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucFWCRMServiceRecordDetails_BeforeInsert);
            this.ucFWCRMServiceRecordDetails.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucFWCRMServiceRecordDetails_BeforeModify);
            // 
            // idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails
            // 
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.Detail = this.FWCRMServiceRecordDetails;
            columnItem1.FieldName = "RecordNo";
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.DetailColumns.Add(columnItem1);
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.DynamicTableName = false;
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.Master = this.FWCRMServiceRecordMaster;
            columnItem2.FieldName = "RecordNo";
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.MasterColumns.Add(columnItem2);
            // 
            // View_FWCRMServiceRecordMaster
            // 
            this.View_FWCRMServiceRecordMaster.CacheConnection = false;
            this.View_FWCRMServiceRecordMaster.CommandText = "SELECT * FROM dbo.[FWCRMServiceRecordMaster]";
            this.View_FWCRMServiceRecordMaster.CommandTimeout = 30;
            this.View_FWCRMServiceRecordMaster.CommandType = System.Data.CommandType.Text;
            this.View_FWCRMServiceRecordMaster.DynamicTableName = false;
            this.View_FWCRMServiceRecordMaster.EEPAlias = null;
            this.View_FWCRMServiceRecordMaster.EncodingAfter = null;
            this.View_FWCRMServiceRecordMaster.EncodingBefore = "Windows-1252";
            this.View_FWCRMServiceRecordMaster.EncodingConvert = null;
            this.View_FWCRMServiceRecordMaster.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "Autokey";
            this.View_FWCRMServiceRecordMaster.KeyFields.Add(keyItem3);
            this.View_FWCRMServiceRecordMaster.MultiSetWhere = false;
            this.View_FWCRMServiceRecordMaster.Name = "View_FWCRMServiceRecordMaster";
            this.View_FWCRMServiceRecordMaster.NotificationAutoEnlist = false;
            this.View_FWCRMServiceRecordMaster.SecExcept = null;
            this.View_FWCRMServiceRecordMaster.SecFieldName = null;
            this.View_FWCRMServiceRecordMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_FWCRMServiceRecordMaster.SelectPaging = false;
            this.View_FWCRMServiceRecordMaster.SelectTop = 0;
            this.View_FWCRMServiceRecordMaster.SiteControl = false;
            this.View_FWCRMServiceRecordMaster.SiteFieldName = null;
            this.View_FWCRMServiceRecordMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCompanyID
            // 
            this.infoCompanyID.CacheConnection = false;
            this.infoCompanyID.CommandText = "select CompanyID,CompName\r\nFrom FWCRMCompany";
            this.infoCompanyID.CommandTimeout = 30;
            this.infoCompanyID.CommandType = System.Data.CommandType.Text;
            this.infoCompanyID.DynamicTableName = false;
            this.infoCompanyID.EEPAlias = "";
            this.infoCompanyID.EncodingAfter = null;
            this.infoCompanyID.EncodingBefore = "Windows-1252";
            this.infoCompanyID.EncodingConvert = null;
            this.infoCompanyID.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "CompanyID";
            this.infoCompanyID.KeyFields.Add(keyItem4);
            this.infoCompanyID.MultiSetWhere = false;
            this.infoCompanyID.Name = "infoCompanyID";
            this.infoCompanyID.NotificationAutoEnlist = false;
            this.infoCompanyID.SecExcept = null;
            this.infoCompanyID.SecFieldName = null;
            this.infoCompanyID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCompanyID.SelectPaging = false;
            this.infoCompanyID.SelectTop = 0;
            this.infoCompanyID.SiteControl = false;
            this.infoCompanyID.SiteFieldName = null;
            this.infoCompanyID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoEmployerID
            // 
            this.infoEmployerID.CacheConnection = false;
            this.infoEmployerID.CommandText = resources.GetString("infoEmployerID.CommandText");
            this.infoEmployerID.CommandTimeout = 30;
            this.infoEmployerID.CommandType = System.Data.CommandType.Text;
            this.infoEmployerID.DynamicTableName = false;
            this.infoEmployerID.EEPAlias = "";
            this.infoEmployerID.EncodingAfter = null;
            this.infoEmployerID.EncodingBefore = "Windows-1252";
            this.infoEmployerID.EncodingConvert = null;
            this.infoEmployerID.InfoConnection = this.InfoConnection1;
            this.infoEmployerID.MultiSetWhere = false;
            this.infoEmployerID.Name = "infoEmployerID";
            this.infoEmployerID.NotificationAutoEnlist = false;
            this.infoEmployerID.SecExcept = null;
            this.infoEmployerID.SecFieldName = null;
            this.infoEmployerID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoEmployerID.SelectPaging = false;
            this.infoEmployerID.SelectTop = 0;
            this.infoEmployerID.SiteControl = false;
            this.infoEmployerID.SiteFieldName = null;
            this.infoEmployerID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoNationalityID
            // 
            this.infoNationalityID.CacheConnection = false;
            this.infoNationalityID.CommandText = "select ListID,ListContent \r\nfrom [192.168.1.41].FWCRM.dbo.ReferenceTable wh" +
    "ere ListCategory=\'NationalityID\'\r\n";
            this.infoNationalityID.CommandTimeout = 30;
            this.infoNationalityID.CommandType = System.Data.CommandType.Text;
            this.infoNationalityID.DynamicTableName = false;
            this.infoNationalityID.EEPAlias = "";
            this.infoNationalityID.EncodingAfter = null;
            this.infoNationalityID.EncodingBefore = "Windows-1252";
            this.infoNationalityID.EncodingConvert = null;
            this.infoNationalityID.InfoConnection = this.InfoConnection1;
            this.infoNationalityID.MultiSetWhere = false;
            this.infoNationalityID.Name = "infoNationalityID";
            this.infoNationalityID.NotificationAutoEnlist = false;
            this.infoNationalityID.SecExcept = null;
            this.infoNationalityID.SecFieldName = null;
            this.infoNationalityID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoNationalityID.SelectPaging = false;
            this.infoNationalityID.SelectTop = 0;
            this.infoNationalityID.SiteControl = false;
            this.infoNationalityID.SiteFieldName = null;
            this.infoNationalityID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoRecordNo
            // 
            this.autoRecordNo.Active = true;
            this.autoRecordNo.AutoNoID = "RecordNo";
            this.autoRecordNo.Description = null;
            this.autoRecordNo.GetFixed = "RecordNoFixed()";
            this.autoRecordNo.isNumFill = false;
            this.autoRecordNo.Name = "autoRecordNo";
            this.autoRecordNo.Number = null;
            this.autoRecordNo.NumDig = 4;
            this.autoRecordNo.OldVersion = false;
            this.autoRecordNo.OverFlow = true;
            this.autoRecordNo.StartValue = 1;
            this.autoRecordNo.Step = 1;
            this.autoRecordNo.TargetColumn = "RecordNo";
            this.autoRecordNo.UpdateComp = this.ucFWCRMServiceRecordMaster;
            // 
            // infoEmployeeID
            // 
            this.infoEmployeeID.CacheConnection = false;
            this.infoEmployeeID.CommandText = "select * from View_FWCRMServiceRecordEmployee";
            this.infoEmployeeID.CommandTimeout = 30;
            this.infoEmployeeID.CommandType = System.Data.CommandType.Text;
            this.infoEmployeeID.DynamicTableName = false;
            this.infoEmployeeID.EEPAlias = "";
            this.infoEmployeeID.EncodingAfter = null;
            this.infoEmployeeID.EncodingBefore = "Windows-1252";
            this.infoEmployeeID.EncodingConvert = null;
            this.infoEmployeeID.InfoConnection = this.InfoConnection1;
            this.infoEmployeeID.MultiSetWhere = false;
            this.infoEmployeeID.Name = "infoEmployeeID";
            this.infoEmployeeID.NotificationAutoEnlist = false;
            this.infoEmployeeID.SecExcept = null;
            this.infoEmployeeID.SecFieldName = null;
            this.infoEmployeeID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoEmployeeID.SelectPaging = false;
            this.infoEmployeeID.SelectTop = 0;
            this.infoEmployeeID.SiteControl = false;
            this.infoEmployeeID.SiteFieldName = null;
            this.infoEmployeeID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoFWCRMRecordType
            // 
            this.infoFWCRMRecordType.CacheConnection = false;
            this.infoFWCRMRecordType.CommandText = "select RecordTypeID,RecordTypeName\r\nFrom FWCRMRecordType";
            this.infoFWCRMRecordType.CommandTimeout = 30;
            this.infoFWCRMRecordType.CommandType = System.Data.CommandType.Text;
            this.infoFWCRMRecordType.DynamicTableName = false;
            this.infoFWCRMRecordType.EEPAlias = "";
            this.infoFWCRMRecordType.EncodingAfter = null;
            this.infoFWCRMRecordType.EncodingBefore = "Windows-1252";
            this.infoFWCRMRecordType.EncodingConvert = null;
            this.infoFWCRMRecordType.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "RecordTypeID";
            this.infoFWCRMRecordType.KeyFields.Add(keyItem5);
            this.infoFWCRMRecordType.MultiSetWhere = false;
            this.infoFWCRMRecordType.Name = "infoFWCRMRecordType";
            this.infoFWCRMRecordType.NotificationAutoEnlist = false;
            this.infoFWCRMRecordType.SecExcept = null;
            this.infoFWCRMRecordType.SecFieldName = null;
            this.infoFWCRMRecordType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoFWCRMRecordType.SelectPaging = false;
            this.infoFWCRMRecordType.SelectTop = 0;
            this.infoFWCRMRecordType.SiteControl = false;
            this.infoFWCRMRecordType.SiteFieldName = null;
            this.infoFWCRMRecordType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMServiceRecordMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMServiceRecordDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_FWCRMServiceRecordMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCompanyID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmployerID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoNationalityID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmployeeID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMRecordType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMServiceRecordMaster;
        private Srvtools.UpdateComponent ucFWCRMServiceRecordMaster;
        private Srvtools.InfoCommand FWCRMServiceRecordDetails;
        private Srvtools.UpdateComponent ucFWCRMServiceRecordDetails;
        private Srvtools.InfoDataSource idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails;
        private Srvtools.InfoCommand View_FWCRMServiceRecordMaster;
        private Srvtools.InfoCommand infoCompanyID;
        private Srvtools.InfoCommand infoEmployerID;
        private Srvtools.InfoCommand infoNationalityID;
        private Srvtools.AutoNumber autoRecordNo;
        private Srvtools.InfoCommand infoEmployeeID;
        private Srvtools.InfoCommand infoFWCRMRecordType;
    }
}
