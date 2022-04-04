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
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem5 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem6 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CashTakeBackMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashAgainBillType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgainstBill)).BeginInit();
            // 
            // serviceManager1
            // 
            service5.DelegateName = "GetEmpFlowAgentList";
            service5.NonLogin = false;
            service5.ServiceName = "GetEmpFlowAgentList";
            service6.DelegateName = "GetUserOrgNOs";
            service6.NonLogin = false;
            service6.ServiceName = "GetUserOrgNOs";
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
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
            keyItem6.KeyName = "CashTakeBackNO";
            this.CashTakeBackMaster.KeyFields.Add(keyItem6);
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
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CashTakeBackNO";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ApplyEmpID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ApplyDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "ApplyOrg_NO";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Org_NOParent";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "ApplyNotes";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "AgainBillType";
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
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr8);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr9);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr10);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr11);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr12);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr13);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr14);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr15);
            this.ucCashTakeBackMaster.FieldAttrs.Add(fieldAttr16);
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
            this.CashTakeBackDetails.CommandText = "SELECT dbo.[CashTakeBackDetails].* FROM dbo.[CashTakeBackDetails]";
            this.CashTakeBackDetails.CommandTimeout = 30;
            this.CashTakeBackDetails.CommandType = System.Data.CommandType.Text;
            this.CashTakeBackDetails.DynamicTableName = false;
            this.CashTakeBackDetails.EEPAlias = null;
            this.CashTakeBackDetails.EncodingAfter = null;
            this.CashTakeBackDetails.EncodingBefore = "Windows-1252";
            this.CashTakeBackDetails.EncodingConvert = null;
            this.CashTakeBackDetails.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "CashTakeBackNO";
            keyItem8.KeyName = "ItemNO";
            this.CashTakeBackDetails.KeyFields.Add(keyItem7);
            this.CashTakeBackDetails.KeyFields.Add(keyItem8);
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
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CashTakeBackNO";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "ItemNO";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "ShortTermNO";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "Currency";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "Amount";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "CreateBy";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "CreateDate";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr17);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr18);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr19);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr20);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr21);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr22);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr23);
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
            columnItem5.FieldName = "CashTakeBackNO";
            this.idCashTakeBackMaster_CashTakeBackDetails.DetailColumns.Add(columnItem5);
            this.idCashTakeBackMaster_CashTakeBackDetails.DynamicTableName = false;
            this.idCashTakeBackMaster_CashTakeBackDetails.Master = this.CashTakeBackMaster;
            columnItem6.FieldName = "CashTakeBackNO";
            this.idCashTakeBackMaster_CashTakeBackDetails.MasterColumns.Add(columnItem6);
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
            keyItem1.KeyName = "CashTakeBackNO";
            this.View_CashTakeBackMaster.KeyFields.Add(keyItem1);
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
            keyItem2.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem2);
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
            keyItem3.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem3);
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
            this.AgainstBill.CommandText = "SELECT  1 AS BillType,\r\n              APPLYORG_NO,\r\n              SHORTTERMNO,\r\n " +
    "             SHORTTERMGIST\r\nFROM  SHORTTERM\r\nWHERE ISSETTLEACCOUNT=0";
            this.AgainstBill.CommandTimeout = 30;
            this.AgainstBill.CommandType = System.Data.CommandType.Text;
            this.AgainstBill.DynamicTableName = false;
            this.AgainstBill.EEPAlias = null;
            this.AgainstBill.EncodingAfter = null;
            this.AgainstBill.EncodingBefore = "Windows-1252";
            this.AgainstBill.EncodingConvert = null;
            this.AgainstBill.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "SHORTTERMNO";
            this.AgainstBill.KeyFields.Add(keyItem4);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CashTakeBackMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashAgainBillType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgainstBill)).EndInit();

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
    }
}
