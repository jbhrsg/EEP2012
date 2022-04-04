namespace sJCSCustomer
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
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
            Srvtools.KeyItem keyItem9 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem10 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection = new Srvtools.InfoConnection(this.components);
            this.infoConnection0 = new Srvtools.InfoConnection(this.components);
            this.CountryCity = new Srvtools.InfoCommand(this.components);
            this.Customers = new Srvtools.InfoCommand(this.components);
            this.ucEmployer = new Srvtools.UpdateComponent(this.components);
            this.infoLimitTime = new Srvtools.InfoCommand(this.components);
            this.infoSalesId = new Srvtools.InfoCommand(this.components);
            this.infoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Customers1 = new Srvtools.InfoCommand(this.components);
            this.ucEmployer1 = new Srvtools.UpdateComponent(this.components);
            this.infoSalesId1 = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.Customers2 = new Srvtools.InfoCommand(this.components);
            this.ucEmployer2 = new Srvtools.UpdateComponent(this.components);
            this.infoSalesId2 = new Srvtools.InfoCommand(this.components);
            this.infoCommand1 = new Srvtools.InfoCommand(this.components);
            this.infoConnection3 = new Srvtools.InfoConnection(this.components);
            this.Customers3 = new Srvtools.InfoCommand(this.components);
            this.ucEmployer3 = new Srvtools.UpdateComponent(this.components);
            this.infoSalesId3 = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountryCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoLimitTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCommand1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId3)).BeginInit();
            // 
            // InfoConnection
            // 
            this.InfoConnection.EEPAlias = "JCS";
            // 
            // infoConnection0
            // 
            this.infoConnection0.EEPAlias = "JBERP";
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
            this.CountryCity.InfoConnection = this.infoConnection0;
            keyItem1.KeyName = "AutoKey";
            this.CountryCity.KeyFields.Add(keyItem1);
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
            // Customers
            // 
            this.Customers.CacheConnection = false;
            this.Customers.CommandText = "SELECT Customers.*,(select SalesName from Sales where SalesID=Customers.SalesID) " +
    "as SalesName\r\nfrom Customers\r\norder by IsActive desc,LastUpdateDate desc";
            this.Customers.CommandTimeout = 30;
            this.Customers.CommandType = System.Data.CommandType.Text;
            this.Customers.DynamicTableName = false;
            this.Customers.EEPAlias = "JCS";
            this.Customers.EncodingAfter = null;
            this.Customers.EncodingBefore = "Windows-1252";
            this.Customers.EncodingConvert = null;
            this.Customers.InfoConnection = this.InfoConnection;
            keyItem2.KeyName = "CustomerID";
            this.Customers.KeyFields.Add(keyItem2);
            this.Customers.MultiSetWhere = false;
            this.Customers.Name = "Customers";
            this.Customers.NotificationAutoEnlist = false;
            this.Customers.SecExcept = null;
            this.Customers.SecFieldName = null;
            this.Customers.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Customers.SelectPaging = false;
            this.Customers.SelectTop = 0;
            this.Customers.SiteControl = false;
            this.Customers.SiteFieldName = null;
            this.Customers.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            this.ucEmployer.SelectCmd = this.Customers;
            this.ucEmployer.SelectCmdForUpdate = null;
            this.ucEmployer.SendSQLCmd = true;
            this.ucEmployer.ServerModify = true;
            this.ucEmployer.ServerModifyGetMax = false;
            this.ucEmployer.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmployer.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmployer.UseTranscationScope = false;
            this.ucEmployer.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucEmployer.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucEmployer_BeforeModify);
            // 
            // infoLimitTime
            // 
            this.infoLimitTime.CacheConnection = false;
            this.infoLimitTime.CommandText = resources.GetString("infoLimitTime.CommandText");
            this.infoLimitTime.CommandTimeout = 30;
            this.infoLimitTime.CommandType = System.Data.CommandType.Text;
            this.infoLimitTime.DynamicTableName = false;
            this.infoLimitTime.EEPAlias = "JBERP";
            this.infoLimitTime.EncodingAfter = null;
            this.infoLimitTime.EncodingBefore = "Windows-1252";
            this.infoLimitTime.EncodingConvert = null;
            this.infoLimitTime.InfoConnection = this.infoConnection0;
            this.infoLimitTime.MultiSetWhere = false;
            this.infoLimitTime.Name = "infoLimitTime";
            this.infoLimitTime.NotificationAutoEnlist = false;
            this.infoLimitTime.SecExcept = null;
            this.infoLimitTime.SecFieldName = null;
            this.infoLimitTime.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoLimitTime.SelectPaging = false;
            this.infoLimitTime.SelectTop = 0;
            this.infoLimitTime.SiteControl = false;
            this.infoLimitTime.SiteFieldName = null;
            this.infoLimitTime.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesId
            // 
            this.infoSalesId.CacheConnection = false;
            this.infoSalesId.CommandText = "select  SalesID as ID, SalesName as Name\r\n\t\tfrom Sales\r\n\t\twhere IsActive = 1\r\n\t\to" +
    "rder by ID";
            this.infoSalesId.CommandTimeout = 30;
            this.infoSalesId.CommandType = System.Data.CommandType.Text;
            this.infoSalesId.DynamicTableName = false;
            this.infoSalesId.EEPAlias = "JCS";
            this.infoSalesId.EncodingAfter = null;
            this.infoSalesId.EncodingBefore = "Windows-1252";
            this.infoSalesId.EncodingConvert = null;
            this.infoSalesId.InfoConnection = this.InfoConnection;
            keyItem3.KeyName = "ID";
            this.infoSalesId.KeyFields.Add(keyItem3);
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
            // infoConnection1
            // 
            this.infoConnection1.EEPAlias = "JCS1";
            // 
            // Customers1
            // 
            this.Customers1.CacheConnection = false;
            this.Customers1.CommandText = "SELECT Customers.*,(select SalesName from Sales where SalesID=Customers.SalesID) " +
    "as SalesName\r\nfrom Customers\r\norder by IsActive desc,LastUpdateDate desc";
            this.Customers1.CommandTimeout = 30;
            this.Customers1.CommandType = System.Data.CommandType.Text;
            this.Customers1.DynamicTableName = false;
            this.Customers1.EEPAlias = "JCS1";
            this.Customers1.EncodingAfter = null;
            this.Customers1.EncodingBefore = "Windows-1252";
            this.Customers1.EncodingConvert = null;
            this.Customers1.InfoConnection = this.infoConnection1;
            keyItem4.KeyName = "CustomerID";
            this.Customers1.KeyFields.Add(keyItem4);
            this.Customers1.MultiSetWhere = false;
            this.Customers1.Name = "Customers1";
            this.Customers1.NotificationAutoEnlist = false;
            this.Customers1.SecExcept = null;
            this.Customers1.SecFieldName = null;
            this.Customers1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Customers1.SelectPaging = false;
            this.Customers1.SelectTop = 0;
            this.Customers1.SiteControl = false;
            this.Customers1.SiteFieldName = null;
            this.Customers1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucEmployer1
            // 
            this.ucEmployer1.AutoTrans = true;
            this.ucEmployer1.ExceptJoin = false;
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
            this.ucEmployer1.FieldAttrs.Add(fieldAttr3);
            this.ucEmployer1.FieldAttrs.Add(fieldAttr4);
            this.ucEmployer1.LogInfo = null;
            this.ucEmployer1.Name = "ucEmployer1";
            this.ucEmployer1.RowAffectsCheck = true;
            this.ucEmployer1.SelectCmd = this.Customers1;
            this.ucEmployer1.SelectCmdForUpdate = null;
            this.ucEmployer1.SendSQLCmd = true;
            this.ucEmployer1.ServerModify = true;
            this.ucEmployer1.ServerModifyGetMax = false;
            this.ucEmployer1.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmployer1.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmployer1.UseTranscationScope = false;
            this.ucEmployer1.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucEmployer1.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucEmployer1_BeforeModify);
            // 
            // infoSalesId1
            // 
            this.infoSalesId1.CacheConnection = false;
            this.infoSalesId1.CommandText = "select  SalesID as ID, SalesName as Name\r\n\t\tfrom Sales\r\n\t\twhere IsActive = 1\r\n\t\to" +
    "rder by ID";
            this.infoSalesId1.CommandTimeout = 30;
            this.infoSalesId1.CommandType = System.Data.CommandType.Text;
            this.infoSalesId1.DynamicTableName = false;
            this.infoSalesId1.EEPAlias = "JCS1";
            this.infoSalesId1.EncodingAfter = null;
            this.infoSalesId1.EncodingBefore = "Windows-1252";
            this.infoSalesId1.EncodingConvert = null;
            this.infoSalesId1.InfoConnection = this.infoConnection1;
            keyItem5.KeyName = "ID";
            this.infoSalesId1.KeyFields.Add(keyItem5);
            this.infoSalesId1.MultiSetWhere = false;
            this.infoSalesId1.Name = "infoSalesId1";
            this.infoSalesId1.NotificationAutoEnlist = false;
            this.infoSalesId1.SecExcept = null;
            this.infoSalesId1.SecFieldName = null;
            this.infoSalesId1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesId1.SelectPaging = false;
            this.infoSalesId1.SelectTop = 0;
            this.infoSalesId1.SiteControl = false;
            this.infoSalesId1.SiteFieldName = null;
            this.infoSalesId1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JCS2";
            // 
            // Customers2
            // 
            this.Customers2.CacheConnection = false;
            this.Customers2.CommandText = "SELECT Customers.*,(select SalesName from Sales where SalesID=Customers.SalesID) " +
    "as SalesName\r\nfrom Customers\r\norder by IsActive desc,LastUpdateDate desc";
            this.Customers2.CommandTimeout = 30;
            this.Customers2.CommandType = System.Data.CommandType.Text;
            this.Customers2.DynamicTableName = false;
            this.Customers2.EEPAlias = "JCS2";
            this.Customers2.EncodingAfter = null;
            this.Customers2.EncodingBefore = "Windows-1252";
            this.Customers2.EncodingConvert = null;
            this.Customers2.InfoConnection = this.infoConnection2;
            keyItem6.KeyName = "CustomerID";
            this.Customers2.KeyFields.Add(keyItem6);
            this.Customers2.MultiSetWhere = false;
            this.Customers2.Name = "Customers2";
            this.Customers2.NotificationAutoEnlist = false;
            this.Customers2.SecExcept = null;
            this.Customers2.SecFieldName = null;
            this.Customers2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Customers2.SelectPaging = false;
            this.Customers2.SelectTop = 0;
            this.Customers2.SiteControl = false;
            this.Customers2.SiteFieldName = null;
            this.Customers2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucEmployer2
            // 
            this.ucEmployer2.AutoTrans = true;
            this.ucEmployer2.ExceptJoin = false;
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
            this.ucEmployer2.FieldAttrs.Add(fieldAttr5);
            this.ucEmployer2.FieldAttrs.Add(fieldAttr6);
            this.ucEmployer2.LogInfo = null;
            this.ucEmployer2.Name = "ucEmployer2";
            this.ucEmployer2.RowAffectsCheck = true;
            this.ucEmployer2.SelectCmd = this.Customers2;
            this.ucEmployer2.SelectCmdForUpdate = null;
            this.ucEmployer2.SendSQLCmd = true;
            this.ucEmployer2.ServerModify = true;
            this.ucEmployer2.ServerModifyGetMax = false;
            this.ucEmployer2.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmployer2.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmployer2.UseTranscationScope = false;
            this.ucEmployer2.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucEmployer2.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucEmployer2_BeforeModify);
            // 
            // infoSalesId2
            // 
            this.infoSalesId2.CacheConnection = false;
            this.infoSalesId2.CommandText = "select  SalesID as ID, SalesName as Name\r\n\t\tfrom Sales\r\n\t\twhere IsActive = 1\r\n\t\to" +
    "rder by ID";
            this.infoSalesId2.CommandTimeout = 30;
            this.infoSalesId2.CommandType = System.Data.CommandType.Text;
            this.infoSalesId2.DynamicTableName = false;
            this.infoSalesId2.EEPAlias = "JCS2";
            this.infoSalesId2.EncodingAfter = null;
            this.infoSalesId2.EncodingBefore = "Windows-1252";
            this.infoSalesId2.EncodingConvert = null;
            this.infoSalesId2.InfoConnection = this.infoConnection2;
            keyItem7.KeyName = "ID";
            this.infoSalesId2.KeyFields.Add(keyItem7);
            this.infoSalesId2.MultiSetWhere = false;
            this.infoSalesId2.Name = "infoSalesId2";
            this.infoSalesId2.NotificationAutoEnlist = false;
            this.infoSalesId2.SecExcept = null;
            this.infoSalesId2.SecFieldName = null;
            this.infoSalesId2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesId2.SelectPaging = false;
            this.infoSalesId2.SelectTop = 0;
            this.infoSalesId2.SiteControl = false;
            this.infoSalesId2.SiteFieldName = null;
            this.infoSalesId2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCommand1
            // 
            this.infoCommand1.CacheConnection = false;
            this.infoCommand1.CommandText = "select  SalesID as ID, SalesName as Name\r\n\t\tfrom Sales\r\n\t\twhere IsActive = 1\r\n\t\to" +
    "rder by ID";
            this.infoCommand1.CommandTimeout = 30;
            this.infoCommand1.CommandType = System.Data.CommandType.Text;
            this.infoCommand1.DynamicTableName = false;
            this.infoCommand1.EEPAlias = "JCS1";
            this.infoCommand1.EncodingAfter = null;
            this.infoCommand1.EncodingBefore = "Windows-1252";
            this.infoCommand1.EncodingConvert = null;
            this.infoCommand1.InfoConnection = this.infoConnection1;
            keyItem8.KeyName = "ID";
            this.infoCommand1.KeyFields.Add(keyItem8);
            this.infoCommand1.MultiSetWhere = false;
            this.infoCommand1.Name = "infoCommand1";
            this.infoCommand1.NotificationAutoEnlist = false;
            this.infoCommand1.SecExcept = null;
            this.infoCommand1.SecFieldName = null;
            this.infoCommand1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCommand1.SelectPaging = false;
            this.infoCommand1.SelectTop = 0;
            this.infoCommand1.SiteControl = false;
            this.infoCommand1.SiteFieldName = null;
            this.infoCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection3
            // 
            this.infoConnection3.EEPAlias = "JCS3";
            // 
            // Customers3
            // 
            this.Customers3.CacheConnection = false;
            this.Customers3.CommandText = "SELECT Customers.*,(select SalesName from Sales where SalesID=Customers.SalesID) " +
    "as SalesName\r\nfrom Customers\r\norder by IsActive desc,LastUpdateDate desc";
            this.Customers3.CommandTimeout = 30;
            this.Customers3.CommandType = System.Data.CommandType.Text;
            this.Customers3.DynamicTableName = false;
            this.Customers3.EEPAlias = "JCS3";
            this.Customers3.EncodingAfter = null;
            this.Customers3.EncodingBefore = "Windows-1252";
            this.Customers3.EncodingConvert = null;
            this.Customers3.InfoConnection = this.infoConnection3;
            keyItem9.KeyName = "CustomerID";
            this.Customers3.KeyFields.Add(keyItem9);
            this.Customers3.MultiSetWhere = false;
            this.Customers3.Name = "Customers3";
            this.Customers3.NotificationAutoEnlist = false;
            this.Customers3.SecExcept = null;
            this.Customers3.SecFieldName = null;
            this.Customers3.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Customers3.SelectPaging = false;
            this.Customers3.SelectTop = 0;
            this.Customers3.SiteControl = false;
            this.Customers3.SiteFieldName = null;
            this.Customers3.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucEmployer3
            // 
            this.ucEmployer3.AutoTrans = true;
            this.ucEmployer3.ExceptJoin = false;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CustAreaID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CustAreaName";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucEmployer3.FieldAttrs.Add(fieldAttr7);
            this.ucEmployer3.FieldAttrs.Add(fieldAttr8);
            this.ucEmployer3.LogInfo = null;
            this.ucEmployer3.Name = "updateComponent1";
            this.ucEmployer3.RowAffectsCheck = true;
            this.ucEmployer3.SelectCmd = this.Customers3;
            this.ucEmployer3.SelectCmdForUpdate = null;
            this.ucEmployer3.SendSQLCmd = true;
            this.ucEmployer3.ServerModify = true;
            this.ucEmployer3.ServerModifyGetMax = false;
            this.ucEmployer3.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmployer3.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmployer3.UseTranscationScope = false;
            this.ucEmployer3.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoSalesId3
            // 
            this.infoSalesId3.CacheConnection = false;
            this.infoSalesId3.CommandText = "select  SalesID as ID, SalesName as Name\r\n\t\tfrom Sales\r\n\t\twhere IsActive = 1\r\n\t\to" +
    "rder by ID";
            this.infoSalesId3.CommandTimeout = 30;
            this.infoSalesId3.CommandType = System.Data.CommandType.Text;
            this.infoSalesId3.DynamicTableName = false;
            this.infoSalesId3.EEPAlias = "JCS3";
            this.infoSalesId3.EncodingAfter = null;
            this.infoSalesId3.EncodingBefore = "Windows-1252";
            this.infoSalesId3.EncodingConvert = null;
            this.infoSalesId3.InfoConnection = this.infoConnection3;
            keyItem10.KeyName = "ID";
            this.infoSalesId3.KeyFields.Add(keyItem10);
            this.infoSalesId3.MultiSetWhere = false;
            this.infoSalesId3.Name = "infoSalesId3";
            this.infoSalesId3.NotificationAutoEnlist = false;
            this.infoSalesId3.SecExcept = null;
            this.infoSalesId3.SecFieldName = null;
            this.infoSalesId3.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesId3.SelectPaging = false;
            this.infoSalesId3.SelectTop = 0;
            this.infoSalesId3.SiteControl = false;
            this.infoSalesId3.SiteFieldName = null;
            this.infoSalesId3.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountryCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoLimitTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCommand1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesId3)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection;
        private Srvtools.InfoConnection infoConnection0;
        private Srvtools.InfoCommand CountryCity;
        private Srvtools.InfoCommand Customers;
        private Srvtools.UpdateComponent ucEmployer;
        private Srvtools.InfoCommand infoLimitTime;
        private Srvtools.InfoCommand infoSalesId;
        private Srvtools.InfoConnection infoConnection1;
        private Srvtools.InfoCommand Customers1;
        private Srvtools.UpdateComponent ucEmployer1;
        private Srvtools.InfoCommand infoSalesId1;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand Customers2;
        private Srvtools.UpdateComponent ucEmployer2;
        private Srvtools.InfoCommand infoSalesId2;
        private Srvtools.InfoCommand infoCommand1;
        private Srvtools.InfoConnection infoConnection3;
        private Srvtools.InfoCommand Customers3;
        private Srvtools.UpdateComponent ucEmployer3;
        private Srvtools.InfoCommand infoSalesId3;
    }
}
