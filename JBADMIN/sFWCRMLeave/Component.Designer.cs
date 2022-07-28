namespace sFWCRMLeave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
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
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMLeave = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMLeave = new Srvtools.UpdateComponent(this.components);
            this.FWCRMLeaveDetails = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMLeaveDetails = new Srvtools.UpdateComponent(this.components);
            this.idFWCRMLeave_FWCRMLeaveDetails = new Srvtools.InfoDataSource(this.components);
            this.infoFWCRMCurrency = new Srvtools.InfoCommand(this.components);
            this.infoFWCRMAirline = new Srvtools.InfoCommand(this.components);
            this.infosup = new Srvtools.InfoCommand(this.components);
            this.glCompany = new Srvtools.InfoCommand(this.components);
            this.autoLeaveNo = new Srvtools.AutoNumber(this.components);
            this.infoAirTime = new Srvtools.InfoCommand(this.components);
            this.infoNationalityID = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMLeave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMLeaveDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMAirline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infosup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAirTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoNationalityID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetrEmployerID";
            service1.NonLogin = false;
            service1.ServiceName = "GetrEmployerID";
            service2.DelegateName = "GetrEmployeeID";
            service2.NonLogin = false;
            service2.ServiceName = "GetrEmployeeID";
            service3.DelegateName = "GetEmployeeIDData";
            service3.NonLogin = false;
            service3.ServiceName = "GetEmployeeIDData";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMLeave
            // 
            this.FWCRMLeave.CacheConnection = false;
            this.FWCRMLeave.CommandText = "SELECT dbo.[FWCRMLeave].* FROM dbo.[FWCRMLeave]";
            this.FWCRMLeave.CommandTimeout = 30;
            this.FWCRMLeave.CommandType = System.Data.CommandType.Text;
            this.FWCRMLeave.DynamicTableName = false;
            this.FWCRMLeave.EEPAlias = null;
            this.FWCRMLeave.EncodingAfter = null;
            this.FWCRMLeave.EncodingBefore = "Windows-1252";
            this.FWCRMLeave.EncodingConvert = null;
            this.FWCRMLeave.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "LeaveNo";
            this.FWCRMLeave.KeyFields.Add(keyItem1);
            this.FWCRMLeave.MultiSetWhere = false;
            this.FWCRMLeave.Name = "FWCRMLeave";
            this.FWCRMLeave.NotificationAutoEnlist = false;
            this.FWCRMLeave.SecExcept = null;
            this.FWCRMLeave.SecFieldName = null;
            this.FWCRMLeave.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMLeave.SelectPaging = false;
            this.FWCRMLeave.SelectTop = 0;
            this.FWCRMLeave.SiteControl = false;
            this.FWCRMLeave.SiteFieldName = null;
            this.FWCRMLeave.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMLeave
            // 
            this.ucFWCRMLeave.AutoTrans = true;
            this.ucFWCRMLeave.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "LeaveNo";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "sup_no";
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
            fieldAttr4.DataField = "InfoDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucFWCRMLeave.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMLeave.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMLeave.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMLeave.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMLeave.LogInfo = null;
            this.ucFWCRMLeave.Name = "ucFWCRMLeave";
            this.ucFWCRMLeave.RowAffectsCheck = true;
            this.ucFWCRMLeave.SelectCmd = this.FWCRMLeave;
            this.ucFWCRMLeave.SelectCmdForUpdate = null;
            this.ucFWCRMLeave.SendSQLCmd = true;
            this.ucFWCRMLeave.ServerModify = true;
            this.ucFWCRMLeave.ServerModifyGetMax = false;
            this.ucFWCRMLeave.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMLeave.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMLeave.UseTranscationScope = false;
            this.ucFWCRMLeave.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // FWCRMLeaveDetails
            // 
            this.FWCRMLeaveDetails.CacheConnection = false;
            this.FWCRMLeaveDetails.CommandText = resources.GetString("FWCRMLeaveDetails.CommandText");
            this.FWCRMLeaveDetails.CommandTimeout = 30;
            this.FWCRMLeaveDetails.CommandType = System.Data.CommandType.Text;
            this.FWCRMLeaveDetails.DynamicTableName = false;
            this.FWCRMLeaveDetails.EEPAlias = null;
            this.FWCRMLeaveDetails.EncodingAfter = null;
            this.FWCRMLeaveDetails.EncodingBefore = "Windows-1252";
            this.FWCRMLeaveDetails.EncodingConvert = null;
            this.FWCRMLeaveDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "Autokey";
            this.FWCRMLeaveDetails.KeyFields.Add(keyItem2);
            this.FWCRMLeaveDetails.MultiSetWhere = false;
            this.FWCRMLeaveDetails.Name = "FWCRMLeaveDetails";
            this.FWCRMLeaveDetails.NotificationAutoEnlist = false;
            this.FWCRMLeaveDetails.SecExcept = null;
            this.FWCRMLeaveDetails.SecFieldName = null;
            this.FWCRMLeaveDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMLeaveDetails.SelectPaging = false;
            this.FWCRMLeaveDetails.SelectTop = 0;
            this.FWCRMLeaveDetails.SiteControl = false;
            this.FWCRMLeaveDetails.SiteFieldName = null;
            this.FWCRMLeaveDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMLeaveDetails
            // 
            this.ucFWCRMLeaveDetails.AutoTrans = true;
            this.ucFWCRMLeaveDetails.ExceptJoin = false;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Autokey";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LeaveNo";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "EmployeeID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Gender";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "EffectDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "RenewDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "LeaveDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Airline";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "AirTime";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "Arrears";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "Refund";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "iCurrency";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "flowflag";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "UserID";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "CreateBy";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CreateDate";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr6);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr7);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr8);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr9);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr10);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr11);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr12);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr13);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr14);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr15);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr16);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr17);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr18);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr19);
            this.ucFWCRMLeaveDetails.FieldAttrs.Add(fieldAttr20);
            this.ucFWCRMLeaveDetails.LogInfo = null;
            this.ucFWCRMLeaveDetails.Name = "ucFWCRMLeaveDetails";
            this.ucFWCRMLeaveDetails.RowAffectsCheck = true;
            this.ucFWCRMLeaveDetails.SelectCmd = this.FWCRMLeaveDetails;
            this.ucFWCRMLeaveDetails.SelectCmdForUpdate = null;
            this.ucFWCRMLeaveDetails.SendSQLCmd = true;
            this.ucFWCRMLeaveDetails.ServerModify = true;
            this.ucFWCRMLeaveDetails.ServerModifyGetMax = false;
            this.ucFWCRMLeaveDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMLeaveDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMLeaveDetails.UseTranscationScope = false;
            this.ucFWCRMLeaveDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idFWCRMLeave_FWCRMLeaveDetails
            // 
            this.idFWCRMLeave_FWCRMLeaveDetails.Detail = this.FWCRMLeaveDetails;
            columnItem1.FieldName = "LeaveNo";
            this.idFWCRMLeave_FWCRMLeaveDetails.DetailColumns.Add(columnItem1);
            this.idFWCRMLeave_FWCRMLeaveDetails.DynamicTableName = false;
            this.idFWCRMLeave_FWCRMLeaveDetails.Master = this.FWCRMLeave;
            columnItem2.FieldName = "LeaveNo";
            this.idFWCRMLeave_FWCRMLeaveDetails.MasterColumns.Add(columnItem2);
            // 
            // infoFWCRMCurrency
            // 
            this.infoFWCRMCurrency.CacheConnection = false;
            this.infoFWCRMCurrency.CommandText = "SELECT * FROM dbo.[FWCRMCurrency]";
            this.infoFWCRMCurrency.CommandTimeout = 30;
            this.infoFWCRMCurrency.CommandType = System.Data.CommandType.Text;
            this.infoFWCRMCurrency.DynamicTableName = false;
            this.infoFWCRMCurrency.EEPAlias = null;
            this.infoFWCRMCurrency.EncodingAfter = null;
            this.infoFWCRMCurrency.EncodingBefore = "Windows-1252";
            this.infoFWCRMCurrency.EncodingConvert = null;
            this.infoFWCRMCurrency.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CurrencyID";
            this.infoFWCRMCurrency.KeyFields.Add(keyItem3);
            this.infoFWCRMCurrency.MultiSetWhere = false;
            this.infoFWCRMCurrency.Name = "infoFWCRMCurrency";
            this.infoFWCRMCurrency.NotificationAutoEnlist = false;
            this.infoFWCRMCurrency.SecExcept = null;
            this.infoFWCRMCurrency.SecFieldName = null;
            this.infoFWCRMCurrency.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoFWCRMCurrency.SelectPaging = false;
            this.infoFWCRMCurrency.SelectTop = 0;
            this.infoFWCRMCurrency.SiteControl = false;
            this.infoFWCRMCurrency.SiteFieldName = null;
            this.infoFWCRMCurrency.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoFWCRMAirline
            // 
            this.infoFWCRMAirline.CacheConnection = false;
            this.infoFWCRMAirline.CommandText = "SELECT * FROM dbo.[FWCRMAirline]";
            this.infoFWCRMAirline.CommandTimeout = 30;
            this.infoFWCRMAirline.CommandType = System.Data.CommandType.Text;
            this.infoFWCRMAirline.DynamicTableName = false;
            this.infoFWCRMAirline.EEPAlias = null;
            this.infoFWCRMAirline.EncodingAfter = null;
            this.infoFWCRMAirline.EncodingBefore = "Windows-1252";
            this.infoFWCRMAirline.EncodingConvert = null;
            this.infoFWCRMAirline.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AirlineID";
            this.infoFWCRMAirline.KeyFields.Add(keyItem4);
            this.infoFWCRMAirline.MultiSetWhere = false;
            this.infoFWCRMAirline.Name = "infoFWCRMAirline";
            this.infoFWCRMAirline.NotificationAutoEnlist = false;
            this.infoFWCRMAirline.SecExcept = null;
            this.infoFWCRMAirline.SecFieldName = null;
            this.infoFWCRMAirline.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoFWCRMAirline.SelectPaging = false;
            this.infoFWCRMAirline.SelectTop = 0;
            this.infoFWCRMAirline.SiteControl = false;
            this.infoFWCRMAirline.SiteFieldName = null;
            this.infoFWCRMAirline.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infosup
            // 
            this.infosup.CacheConnection = false;
            this.infosup.CommandText = resources.GetString("infosup.CommandText");
            this.infosup.CommandTimeout = 30;
            this.infosup.CommandType = System.Data.CommandType.Text;
            this.infosup.DynamicTableName = false;
            this.infosup.EEPAlias = null;
            this.infosup.EncodingAfter = null;
            this.infosup.EncodingBefore = "Windows-1252";
            this.infosup.EncodingConvert = null;
            this.infosup.InfoConnection = this.InfoConnection1;
            this.infosup.MultiSetWhere = false;
            this.infosup.Name = "infosup";
            this.infosup.NotificationAutoEnlist = false;
            this.infosup.SecExcept = null;
            this.infosup.SecFieldName = null;
            this.infosup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infosup.SelectPaging = false;
            this.infosup.SelectTop = 0;
            this.infosup.SiteControl = false;
            this.infosup.SiteFieldName = null;
            this.infosup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // glCompany
            // 
            this.glCompany.CacheConnection = false;
            this.glCompany.CommandText = "SELECT CompanyID,CompanyName\r\nFROM glCompany\r\nwhere CompanyID in(2,4)";
            this.glCompany.CommandTimeout = 30;
            this.glCompany.CommandType = System.Data.CommandType.Text;
            this.glCompany.DynamicTableName = false;
            this.glCompany.EEPAlias = "";
            this.glCompany.EncodingAfter = null;
            this.glCompany.EncodingBefore = "Windows-1252";
            this.glCompany.EncodingConvert = null;
            this.glCompany.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "CompanyID";
            this.glCompany.KeyFields.Add(keyItem5);
            this.glCompany.MultiSetWhere = false;
            this.glCompany.Name = "glCompany";
            this.glCompany.NotificationAutoEnlist = false;
            this.glCompany.SecExcept = null;
            this.glCompany.SecFieldName = null;
            this.glCompany.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glCompany.SelectPaging = false;
            this.glCompany.SelectTop = 0;
            this.glCompany.SiteControl = false;
            this.glCompany.SiteFieldName = null;
            this.glCompany.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoLeaveNo
            // 
            this.autoLeaveNo.Active = true;
            this.autoLeaveNo.AutoNoID = "LeaveNo";
            this.autoLeaveNo.Description = null;
            this.autoLeaveNo.GetFixed = "LeaveNoFixed()";
            this.autoLeaveNo.isNumFill = false;
            this.autoLeaveNo.Name = "autoLeaveNo";
            this.autoLeaveNo.Number = null;
            this.autoLeaveNo.NumDig = 2;
            this.autoLeaveNo.OldVersion = false;
            this.autoLeaveNo.OverFlow = true;
            this.autoLeaveNo.StartValue = 1;
            this.autoLeaveNo.Step = 1;
            this.autoLeaveNo.TargetColumn = "LeaveNo";
            this.autoLeaveNo.UpdateComp = this.ucFWCRMLeave;
            // 
            // infoAirTime
            // 
            this.infoAirTime.CacheConnection = false;
            this.infoAirTime.CommandText = "SELECT \'上午\' as sAirTime,1 as AirTimeID\r\nunion\r\nSELECT \'下午\' as sAirTime,2 as AirTi" +
    "meID\r\nunion\r\nSELECT \'晚上\' as sAirTime,3 as AirTimeID\r\norder by AirTimeID";
            this.infoAirTime.CommandTimeout = 30;
            this.infoAirTime.CommandType = System.Data.CommandType.Text;
            this.infoAirTime.DynamicTableName = false;
            this.infoAirTime.EEPAlias = "";
            this.infoAirTime.EncodingAfter = null;
            this.infoAirTime.EncodingBefore = "Windows-1252";
            this.infoAirTime.EncodingConvert = null;
            this.infoAirTime.InfoConnection = this.InfoConnection1;
            this.infoAirTime.MultiSetWhere = false;
            this.infoAirTime.Name = "infoAirTime";
            this.infoAirTime.NotificationAutoEnlist = false;
            this.infoAirTime.SecExcept = null;
            this.infoAirTime.SecFieldName = null;
            this.infoAirTime.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoAirTime.SelectPaging = false;
            this.infoAirTime.SelectTop = 0;
            this.infoAirTime.SiteControl = false;
            this.infoAirTime.SiteFieldName = null;
            this.infoAirTime.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoNationalityID
            // 
            this.infoNationalityID.CacheConnection = false;
            this.infoNationalityID.CommandText = "SELECT ListID,ListContent\r\nFROM ReferenceTable\r\nwhere ListCategory=\'NationalityID" +
    "\'";
            this.infoNationalityID.CommandTimeout = 30;
            this.infoNationalityID.CommandType = System.Data.CommandType.Text;
            this.infoNationalityID.DynamicTableName = false;
            this.infoNationalityID.EEPAlias = "FWCRM";
            this.infoNationalityID.EncodingAfter = null;
            this.infoNationalityID.EncodingBefore = "Windows-1252";
            this.infoNationalityID.EncodingConvert = null;
            this.infoNationalityID.InfoConnection = this.infoConnection2;
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
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "FWCRM";
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMLeave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMLeaveDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoFWCRMAirline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infosup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAirTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoNationalityID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMLeave;
        private Srvtools.UpdateComponent ucFWCRMLeave;
        private Srvtools.InfoCommand FWCRMLeaveDetails;
        private Srvtools.UpdateComponent ucFWCRMLeaveDetails;
        private Srvtools.InfoDataSource idFWCRMLeave_FWCRMLeaveDetails;
        private Srvtools.InfoCommand infoFWCRMCurrency;
        private Srvtools.InfoCommand infoFWCRMAirline;
        private Srvtools.InfoCommand infosup;
        private Srvtools.InfoCommand glCompany;
        private Srvtools.AutoNumber autoLeaveNo;
        private Srvtools.InfoCommand infoAirTime;
        private Srvtools.InfoCommand infoNationalityID;
        private Srvtools.InfoConnection infoConnection2;
    }
}
