namespace sCashTakeBackMaster
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem9 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CashTakeBackMaster = new Srvtools.InfoCommand(this.components);
            this.ucCashTakeBackMaster = new Srvtools.UpdateComponent(this.components);
            this.CashTakeBackDetails = new Srvtools.InfoCommand(this.components);
            this.ucCashTakeBackDetails = new Srvtools.UpdateComponent(this.components);
            this.idCashTakeBackMaster_CashTakeBackDetails = new Srvtools.InfoDataSource(this.components);
            this.View_CashTakeBackMaster = new Srvtools.InfoCommand(this.components);
            this.Currency = new Srvtools.InfoCommand(this.components);
            this.CashAgainBillType = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            this.autoCashTakeBackNO = new Srvtools.AutoNumber(this.components);
            this.AgainstBill = new Srvtools.InfoCommand(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            this.CashTakeBackType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CashTakeBackMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashAgainBillType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgainstBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetEmpFlowAgentList";
            service1.NonLogin = false;
            service1.ServiceName = "GetEmpFlowAgentList";
            service2.DelegateName = "GetUserOrgNOs";
            service2.NonLogin = false;
            service2.ServiceName = "GetUserOrgNOs";
            service3.DelegateName = "PutFeeToShortTermMinusDetails";
            service3.NonLogin = false;
            service3.ServiceName = "PutFeeToShortTermMinusDetails";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // CashTakeBackMaster
            // 
            this.CashTakeBackMaster.CacheConnection = false;
            this.CashTakeBackMaster.CommandText = "SELECT dbo.[CashTakeBackMaster].* FROM dbo.[CashTakeBackMaster]";
            this.CashTakeBackMaster.CommandTimeout = 30;
            this.CashTakeBackMaster.CommandType = System.Data.CommandType.Text;
            this.CashTakeBackMaster.DynamicTableName = false;
            this.CashTakeBackMaster.EEPAlias = null;
            this.CashTakeBackMaster.EncodingAfter = null;
            this.CashTakeBackMaster.EncodingBefore = "Windows-1252";
            this.CashTakeBackMaster.EncodingConvert = null;
            this.CashTakeBackMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CashTakeBackNO";
            this.CashTakeBackMaster.KeyFields.Add(keyItem1);
            this.CashTakeBackMaster.MultiSetWhere = false;
            this.CashTakeBackMaster.Name = "CashTakeBackMaster";
            this.CashTakeBackMaster.NotificationAutoEnlist = false;
            this.CashTakeBackMaster.SecExcept = null;
            this.CashTakeBackMaster.SecFieldName = null;
            this.CashTakeBackMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CashTakeBackMaster.SelectPaging = false;
            this.CashTakeBackMaster.SelectTop = 0;
            this.CashTakeBackMaster.SiteControl = false;
            this.CashTakeBackMaster.SiteFieldName = null;
            this.CashTakeBackMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCashTakeBackMaster
            // 
            this.ucCashTakeBackMaster.AutoTrans = true;
            this.ucCashTakeBackMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CashTakeBackNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ApplyEmpID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ApplyDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ApplyOrg_NO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Org_NOParent";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ApplyNotes";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AgainBillType";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = "_username";
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
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr1);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr2);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr3);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr4);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr5);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr6);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr7);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr8);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr9);
            this.ucCashTakeBackMaster.LogInfo = null;
            this.ucCashTakeBackMaster.Name = "ucCashTakeBackMaster";
            this.ucCashTakeBackMaster.RowAffectsCheck = true;
            this.ucCashTakeBackMaster.SelectCmd = this.CashTakeBackMaster;
            this.ucCashTakeBackMaster.SelectCmdForUpdate = null;
            this.ucCashTakeBackMaster.SendSQLCmd = true;
            this.ucCashTakeBackMaster.ServerModify = true;
            this.ucCashTakeBackMaster.ServerModifyGetMax = false;
            this.ucCashTakeBackMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCashTakeBackMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCashTakeBackMaster.UseTranscationScope = false;
            this.ucCashTakeBackMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCashTakeBackMaster.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCashTakeBackMaster_BeforeInsert);
            this.ucCashTakeBackMaster.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCashTakeBackMaster_BeforeModify);
            // 
            // CashTakeBackDetails
            // 
            this.CashTakeBackDetails.CacheConnection = false;
            this.CashTakeBackDetails.CommandText = "SELECT dbo.[CashTakeBackDetails].* ,C.ShortName\r\nFROM dbo.[CashTakeBackDetails]\r\n" +
    "LEFT OUTER JOIN JBERP.DBO.Customer AS C ON dbo.[CashTakeBackDetails].CustomerID=" +
    "C.CustomerID\r\nORDER BY ITEMNO";
            this.CashTakeBackDetails.CommandTimeout = 30;
            this.CashTakeBackDetails.CommandType = System.Data.CommandType.Text;
            this.CashTakeBackDetails.DynamicTableName = false;
            this.CashTakeBackDetails.EEPAlias = null;
            this.CashTakeBackDetails.EncodingAfter = null;
            this.CashTakeBackDetails.EncodingBefore = "Windows-1252";
            this.CashTakeBackDetails.EncodingConvert = null;
            this.CashTakeBackDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CashTakeBackNO";
            keyItem3.KeyName = "ItemNO";
            this.CashTakeBackDetails.KeyFields.Add(keyItem2);
            this.CashTakeBackDetails.KeyFields.Add(keyItem3);
            this.CashTakeBackDetails.MultiSetWhere = false;
            this.CashTakeBackDetails.Name = "CashTakeBackDetails";
            this.CashTakeBackDetails.NotificationAutoEnlist = false;
            this.CashTakeBackDetails.SecExcept = null;
            this.CashTakeBackDetails.SecFieldName = null;
            this.CashTakeBackDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CashTakeBackDetails.SelectPaging = false;
            this.CashTakeBackDetails.SelectTop = 0;
            this.CashTakeBackDetails.SiteControl = false;
            this.CashTakeBackDetails.SiteFieldName = null;
            this.CashTakeBackDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCashTakeBackDetails
            // 
            this.ucCashTakeBackDetails.AutoTrans = true;
            this.ucCashTakeBackDetails.ExceptJoin = false;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CashTakeBackNO";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "ItemNO";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "ShortTermNO";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "Currency";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "Amount";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = "_username";
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CreateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr10);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr11);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr12);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr13);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr14);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr15);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr16);
            this.ucCashTakeBackDetails.LogInfo = null;
            this.ucCashTakeBackDetails.Name = "ucCashTakeBackDetails";
            this.ucCashTakeBackDetails.RowAffectsCheck = true;
            this.ucCashTakeBackDetails.SelectCmd = this.CashTakeBackDetails;
            this.ucCashTakeBackDetails.SelectCmdForUpdate = null;
            this.ucCashTakeBackDetails.SendSQLCmd = true;
            this.ucCashTakeBackDetails.ServerModify = true;
            this.ucCashTakeBackDetails.ServerModifyGetMax = false;
            this.ucCashTakeBackDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCashTakeBackDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCashTakeBackDetails.UseTranscationScope = false;
            this.ucCashTakeBackDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCashTakeBackDetails.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCashTakeBackDetails_BeforeInsert);
            this.ucCashTakeBackDetails.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCashTakeBackDetails_BeforeModify);
            // 
            // idCashTakeBackMaster_CashTakeBackDetails
            // 
            this.idCashTakeBackMaster_CashTakeBackDetails.Detail = this.CashTakeBackDetails;
            columnItem1.FieldName = "CashTakeBackNO";
            this.idCashTakeBackMaster_CashTakeBackDetails.DetailColumns.Add(columnItem1);
            this.idCashTakeBackMaster_CashTakeBackDetails.DynamicTableName = false;
            this.idCashTakeBackMaster_CashTakeBackDetails.Master = this.CashTakeBackMaster;
            columnItem2.FieldName = "CashTakeBackNO";
            this.idCashTakeBackMaster_CashTakeBackDetails.MasterColumns.Add(columnItem2);
            // 
            // View_CashTakeBackMaster
            // 
            this.View_CashTakeBackMaster.CacheConnection = false;
            this.View_CashTakeBackMaster.CommandText = "SELECT * FROM dbo.[CashTakeBackMaster]";
            this.View_CashTakeBackMaster.CommandTimeout = 30;
            this.View_CashTakeBackMaster.CommandType = System.Data.CommandType.Text;
            this.View_CashTakeBackMaster.DynamicTableName = false;
            this.View_CashTakeBackMaster.EEPAlias = null;
            this.View_CashTakeBackMaster.EncodingAfter = null;
            this.View_CashTakeBackMaster.EncodingBefore = "Windows-1252";
            this.View_CashTakeBackMaster.EncodingConvert = null;
            this.View_CashTakeBackMaster.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "CashTakeBackNO";
            this.View_CashTakeBackMaster.KeyFields.Add(keyItem4);
            this.View_CashTakeBackMaster.MultiSetWhere = false;
            this.View_CashTakeBackMaster.Name = "View_CashTakeBackMaster";
            this.View_CashTakeBackMaster.NotificationAutoEnlist = false;
            this.View_CashTakeBackMaster.SecExcept = null;
            this.View_CashTakeBackMaster.SecFieldName = null;
            this.View_CashTakeBackMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CashTakeBackMaster.SelectPaging = false;
            this.View_CashTakeBackMaster.SelectTop = 0;
            this.View_CashTakeBackMaster.SiteControl = false;
            this.View_CashTakeBackMaster.SiteFieldName = null;
            this.View_CashTakeBackMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Currency
            // 
            this.Currency.CacheConnection = false;
            this.Currency.CommandText = "select Currency.CurrencyType,Currency.CurrencyName,Currency.Sort from Currency\r\no" +
    "rder by sort";
            this.Currency.CommandTimeout = 30;
            this.Currency.CommandType = System.Data.CommandType.Text;
            this.Currency.DynamicTableName = false;
            this.Currency.EEPAlias = null;
            this.Currency.EncodingAfter = null;
            this.Currency.EncodingBefore = "Windows-1252";
            this.Currency.EncodingConvert = null;
            this.Currency.InfoConnection = this.InfoConnection1;
            this.Currency.MultiSetWhere = false;
            this.Currency.Name = "Currency";
            this.Currency.NotificationAutoEnlist = false;
            this.Currency.SecExcept = null;
            this.Currency.SecFieldName = null;
            this.Currency.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Currency.SelectPaging = false;
            this.Currency.SelectTop = 0;
            this.Currency.SiteControl = false;
            this.Currency.SiteFieldName = null;
            this.Currency.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CashAgainBillType
            // 
            this.CashAgainBillType.CacheConnection = false;
            this.CashAgainBillType.CommandText = "select CashAgainBillType.AgainBillType,CashAgainBillType.AgainBillName\r\n from Cas" +
    "hAgainBillType\r\nwhere IsCashTakeBackItem=1";
            this.CashAgainBillType.CommandTimeout = 30;
            this.CashAgainBillType.CommandType = System.Data.CommandType.Text;
            this.CashAgainBillType.DynamicTableName = false;
            this.CashAgainBillType.EEPAlias = null;
            this.CashAgainBillType.EncodingAfter = null;
            this.CashAgainBillType.EncodingBefore = "Windows-1252";
            this.CashAgainBillType.EncodingConvert = null;
            this.CashAgainBillType.InfoConnection = this.InfoConnection1;
            this.CashAgainBillType.MultiSetWhere = false;
            this.CashAgainBillType.Name = "CashAgainBillType";
            this.CashAgainBillType.NotificationAutoEnlist = false;
            this.CashAgainBillType.SecExcept = null;
            this.CashAgainBillType.SecFieldName = null;
            this.CashAgainBillType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CashAgainBillType.SelectPaging = false;
            this.CashAgainBillType.SelectTop = 0;
            this.CashAgainBillType.SiteControl = false;
            this.CashAgainBillType.SiteFieldName = null;
            this.CashAgainBillType.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            // autoCashTakeBackNO
            // 
            this.autoCashTakeBackNO.Active = true;
            this.autoCashTakeBackNO.AutoNoID = "CashTakeBackNO";
            this.autoCashTakeBackNO.Description = null;
            this.autoCashTakeBackNO.GetFixed = "GetCashTakeBackFixed()";
            this.autoCashTakeBackNO.isNumFill = false;
            this.autoCashTakeBackNO.Name = "autoCashTakeBackNO";
            this.autoCashTakeBackNO.Number = null;
            this.autoCashTakeBackNO.NumDig = 4;
            this.autoCashTakeBackNO.OldVersion = false;
            this.autoCashTakeBackNO.OverFlow = true;
            this.autoCashTakeBackNO.StartValue = 1;
            this.autoCashTakeBackNO.Step = 1;
            this.autoCashTakeBackNO.TargetColumn = "CashTakeBackNO";
            this.autoCashTakeBackNO.UpdateComp = this.ucCashTakeBackMaster;
            // 
            // AgainstBill
            // 
            this.AgainstBill.CacheConnection = false;
            this.AgainstBill.CommandText = resources.GetString("AgainstBill.CommandText");
            this.AgainstBill.CommandTimeout = 30;
            this.AgainstBill.CommandType = System.Data.CommandType.Text;
            this.AgainstBill.DynamicTableName = false;
            this.AgainstBill.EEPAlias = null;
            this.AgainstBill.EncodingAfter = null;
            this.AgainstBill.EncodingBefore = "Windows-1252";
            this.AgainstBill.EncodingConvert = null;
            this.AgainstBill.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "SHORTTERMNO";
            this.AgainstBill.KeyFields.Add(keyItem7);
            this.AgainstBill.MultiSetWhere = false;
            this.AgainstBill.Name = "AgainstBill";
            this.AgainstBill.NotificationAutoEnlist = false;
            this.AgainstBill.SecExcept = null;
            this.AgainstBill.SecFieldName = null;
            this.AgainstBill.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AgainstBill.SelectPaging = false;
            this.AgainstBill.SelectTop = 0;
            this.AgainstBill.SiteControl = false;
            this.AgainstBill.SiteFieldName = null;
            this.AgainstBill.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = " SELECT CustomerID,ShortName,Employer\r\n FROM JBERP.DBO.CUSTOMER  ORDER BY  SHORTN" +
    "AME";
            this.Customer.CommandTimeout = 30;
            this.Customer.CommandType = System.Data.CommandType.Text;
            this.Customer.DynamicTableName = false;
            this.Customer.EEPAlias = null;
            this.Customer.EncodingAfter = null;
            this.Customer.EncodingBefore = "Windows-1252";
            this.Customer.EncodingConvert = null;
            this.Customer.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "SHORTTERMNO";
            this.Customer.KeyFields.Add(keyItem8);
            this.Customer.MultiSetWhere = false;
            this.Customer.Name = "Customer";
            this.Customer.NotificationAutoEnlist = false;
            this.Customer.SecExcept = null;
            this.Customer.SecFieldName = null;
            this.Customer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Customer.SelectPaging = false;
            this.Customer.SelectTop = 0;
            this.Customer.SiteControl = false;
            this.Customer.SiteFieldName = null;
            this.Customer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CashTakeBackType
            // 
            this.CashTakeBackType.CacheConnection = false;
            this.CashTakeBackType.CommandText = " SELECT * FROM  CashTakeBackType ORDER BY CashTakeBackType";
            this.CashTakeBackType.CommandTimeout = 30;
            this.CashTakeBackType.CommandType = System.Data.CommandType.Text;
            this.CashTakeBackType.DynamicTableName = false;
            this.CashTakeBackType.EEPAlias = null;
            this.CashTakeBackType.EncodingAfter = null;
            this.CashTakeBackType.EncodingBefore = "Windows-1252";
            this.CashTakeBackType.EncodingConvert = null;
            this.CashTakeBackType.InfoConnection = this.InfoConnection1;
            keyItem9.KeyName = "SHORTTERMNO";
            this.CashTakeBackType.KeyFields.Add(keyItem9);
            this.CashTakeBackType.MultiSetWhere = false;
            this.CashTakeBackType.Name = "CashTakeBackType";
            this.CashTakeBackType.NotificationAutoEnlist = false;
            this.CashTakeBackType.SecExcept = null;
            this.CashTakeBackType.SecFieldName = null;
            this.CashTakeBackType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CashTakeBackType.SelectPaging = false;
            this.CashTakeBackType.SelectTop = 0;
            this.CashTakeBackType.SiteControl = false;
            this.CashTakeBackType.SiteFieldName = null;
            this.CashTakeBackType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CashTakeBackMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashAgainBillType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgainstBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CashTakeBackMaster;
        private Srvtools.UpdateComponent ucCashTakeBackMaster;
        private Srvtools.InfoCommand CashTakeBackDetails;
        private Srvtools.UpdateComponent ucCashTakeBackDetails;
        private Srvtools.InfoDataSource idCashTakeBackMaster_CashTakeBackDetails;
        private Srvtools.InfoCommand View_CashTakeBackMaster;
        private Srvtools.InfoCommand Currency;
        private Srvtools.InfoCommand CashAgainBillType;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand Organization;
        private Srvtools.AutoNumber autoCashTakeBackNO;
        private Srvtools.InfoCommand AgainstBill;
        private Srvtools.InfoCommand Customer;
        private Srvtools.InfoCommand CashTakeBackType;
    }
}
