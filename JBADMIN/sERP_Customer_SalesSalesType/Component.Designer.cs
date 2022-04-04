namespace sERP_Customer_SalesSalesType
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem9 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem10 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem11 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SalesPerson = new Srvtools.InfoCommand(this.components);
            this.ucSalesPerson = new Srvtools.UpdateComponent(this.components);
            this.SalesSalesType = new Srvtools.InfoCommand(this.components);
            this.ucSalesSalesType = new Srvtools.UpdateComponent(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            this.View_SalesPerson = new Srvtools.InfoCommand(this.components);
            this.View_SalesSalesType = new Srvtools.InfoCommand(this.components);
            this.View_SalesType = new Srvtools.InfoCommand(this.components);
            this.Users = new Srvtools.InfoCommand(this.components);
            this.dataGrid2 = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.QSalesPerson = new Srvtools.InfoCommand(this.components);
            this.SalesTypeTree = new Srvtools.InfoCommand(this.components);
            this.CustomerType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QSalesPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTypeTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "SaveSalesSaleType";
            service1.NonLogin = false;
            service1.ServiceName = "SaveSalesSaleType";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // SalesPerson
            // 
            this.SalesPerson.CacheConnection = false;
            this.SalesPerson.CommandText = "SELECT dbo.[SalesPerson].*,\r\n\'\' as SalesTypeID,\r\nJBERP.dbo.funReturnSalesSaleType" +
    "(SalesID) As SalesSaleTypeIDS\r\nFROM dbo.[SalesPerson]  \r\norder by SalesID";
            this.SalesPerson.CommandTimeout = 30;
            this.SalesPerson.CommandType = System.Data.CommandType.Text;
            this.SalesPerson.DynamicTableName = false;
            this.SalesPerson.EEPAlias = "JBERP";
            this.SalesPerson.EncodingAfter = null;
            this.SalesPerson.EncodingBefore = "Windows-1252";
            this.SalesPerson.EncodingConvert = null;
            this.SalesPerson.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesID";
            this.SalesPerson.KeyFields.Add(keyItem1);
            this.SalesPerson.MultiSetWhere = false;
            this.SalesPerson.Name = "SalesPerson";
            this.SalesPerson.NotificationAutoEnlist = false;
            this.SalesPerson.SecExcept = null;
            this.SalesPerson.SecFieldName = null;
            this.SalesPerson.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesPerson.SelectPaging = false;
            this.SalesPerson.SelectTop = 0;
            this.SalesPerson.SiteControl = false;
            this.SalesPerson.SiteFieldName = null;
            this.SalesPerson.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesPerson
            // 
            this.ucSalesPerson.AutoTrans = true;
            this.ucSalesPerson.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SalesID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateBy";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = "_username";
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = "_sysdate";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LastUpdateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = "_username";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = "_sysdate";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucSalesPerson.FieldAttrs.Add(fieldAttr1);
            this.ucSalesPerson.FieldAttrs.Add(fieldAttr2);
            this.ucSalesPerson.FieldAttrs.Add(fieldAttr3);
            this.ucSalesPerson.FieldAttrs.Add(fieldAttr4);
            this.ucSalesPerson.FieldAttrs.Add(fieldAttr5);
            this.ucSalesPerson.FieldAttrs.Add(fieldAttr6);
            this.ucSalesPerson.FieldAttrs.Add(fieldAttr7);
            this.ucSalesPerson.LogInfo = null;
            this.ucSalesPerson.Name = "ucSalesPerson";
            this.ucSalesPerson.RowAffectsCheck = true;
            this.ucSalesPerson.SelectCmd = this.SalesPerson;
            this.ucSalesPerson.SelectCmdForUpdate = null;
            this.ucSalesPerson.SendSQLCmd = true;
            this.ucSalesPerson.ServerModify = true;
            this.ucSalesPerson.ServerModifyGetMax = false;
            this.ucSalesPerson.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesPerson.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesPerson.UseTranscationScope = false;
            this.ucSalesPerson.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // SalesSalesType
            // 
            this.SalesSalesType.CacheConnection = false;
            this.SalesSalesType.CommandText = resources.GetString("SalesSalesType.CommandText");
            this.SalesSalesType.CommandTimeout = 30;
            this.SalesSalesType.CommandType = System.Data.CommandType.Text;
            this.SalesSalesType.DynamicTableName = false;
            this.SalesSalesType.EEPAlias = "JBERP";
            this.SalesSalesType.EncodingAfter = null;
            this.SalesSalesType.EncodingBefore = "Windows-1252";
            this.SalesSalesType.EncodingConvert = null;
            this.SalesSalesType.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SalesID";
            keyItem3.KeyName = "SalesTypeID";
            this.SalesSalesType.KeyFields.Add(keyItem2);
            this.SalesSalesType.KeyFields.Add(keyItem3);
            this.SalesSalesType.MultiSetWhere = false;
            this.SalesSalesType.Name = "SalesSalesType";
            this.SalesSalesType.NotificationAutoEnlist = false;
            this.SalesSalesType.SecExcept = null;
            this.SalesSalesType.SecFieldName = null;
            this.SalesSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesSalesType.SelectPaging = false;
            this.SalesSalesType.SelectTop = 0;
            this.SalesSalesType.SiteControl = false;
            this.SalesSalesType.SiteFieldName = null;
            this.SalesSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesSalesType
            // 
            this.ucSalesSalesType.AutoTrans = true;
            this.ucSalesSalesType.ExceptJoin = false;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AutoKey";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "SalesID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "SalesTypeID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = "_username";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = "_sysdate";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            this.ucSalesSalesType.FieldAttrs.Add(fieldAttr8);
            this.ucSalesSalesType.FieldAttrs.Add(fieldAttr9);
            this.ucSalesSalesType.FieldAttrs.Add(fieldAttr10);
            this.ucSalesSalesType.FieldAttrs.Add(fieldAttr11);
            this.ucSalesSalesType.FieldAttrs.Add(fieldAttr12);
            this.ucSalesSalesType.LogInfo = null;
            this.ucSalesSalesType.Name = "ucSalesSalesType";
            this.ucSalesSalesType.RowAffectsCheck = true;
            this.ucSalesSalesType.SelectCmd = this.SalesSalesType;
            this.ucSalesSalesType.SelectCmdForUpdate = null;
            this.ucSalesSalesType.SendSQLCmd = true;
            this.ucSalesSalesType.ServerModify = true;
            this.ucSalesSalesType.ServerModifyGetMax = false;
            this.ucSalesSalesType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesSalesType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesSalesType.UseTranscationScope = false;
            this.ucSalesSalesType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "SELECT SalesTypeID,SalesTypeID+\'-\'+SalesTypeName as SalesTypeName FROM dbo.[Sales" +
    "Type]";
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = "JBERP";
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AutoKey";
            this.SalesType.KeyFields.Add(keyItem4);
            this.SalesType.MultiSetWhere = false;
            this.SalesType.Name = "SalesType";
            this.SalesType.NotificationAutoEnlist = false;
            this.SalesType.SecExcept = null;
            this.SalesType.SecFieldName = null;
            this.SalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesType.SelectPaging = false;
            this.SalesType.SelectTop = 0;
            this.SalesType.SiteControl = false;
            this.SalesType.SiteFieldName = null;
            this.SalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_SalesPerson
            // 
            this.View_SalesPerson.CacheConnection = false;
            this.View_SalesPerson.CommandText = "SELECT * FROM dbo.[SalesPerson]";
            this.View_SalesPerson.CommandTimeout = 30;
            this.View_SalesPerson.CommandType = System.Data.CommandType.Text;
            this.View_SalesPerson.DynamicTableName = false;
            this.View_SalesPerson.EEPAlias = "JBERP";
            this.View_SalesPerson.EncodingAfter = null;
            this.View_SalesPerson.EncodingBefore = "Windows-1252";
            this.View_SalesPerson.EncodingConvert = null;
            this.View_SalesPerson.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "AutoKey";
            this.View_SalesPerson.KeyFields.Add(keyItem5);
            this.View_SalesPerson.MultiSetWhere = false;
            this.View_SalesPerson.Name = "View_SalesPerson";
            this.View_SalesPerson.NotificationAutoEnlist = false;
            this.View_SalesPerson.SecExcept = null;
            this.View_SalesPerson.SecFieldName = null;
            this.View_SalesPerson.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesPerson.SelectPaging = false;
            this.View_SalesPerson.SelectTop = 0;
            this.View_SalesPerson.SiteControl = false;
            this.View_SalesPerson.SiteFieldName = null;
            this.View_SalesPerson.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_SalesSalesType
            // 
            this.View_SalesSalesType.CacheConnection = false;
            this.View_SalesSalesType.CommandText = "SELECT * FROM dbo.[SalesSalesType]";
            this.View_SalesSalesType.CommandTimeout = 30;
            this.View_SalesSalesType.CommandType = System.Data.CommandType.Text;
            this.View_SalesSalesType.DynamicTableName = false;
            this.View_SalesSalesType.EEPAlias = "JBERP";
            this.View_SalesSalesType.EncodingAfter = null;
            this.View_SalesSalesType.EncodingBefore = "Windows-1252";
            this.View_SalesSalesType.EncodingConvert = null;
            this.View_SalesSalesType.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "AutoKey";
            this.View_SalesSalesType.KeyFields.Add(keyItem6);
            this.View_SalesSalesType.MultiSetWhere = false;
            this.View_SalesSalesType.Name = "View_SalesSalesType";
            this.View_SalesSalesType.NotificationAutoEnlist = false;
            this.View_SalesSalesType.SecExcept = null;
            this.View_SalesSalesType.SecFieldName = null;
            this.View_SalesSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesSalesType.SelectPaging = false;
            this.View_SalesSalesType.SelectTop = 0;
            this.View_SalesSalesType.SiteControl = false;
            this.View_SalesSalesType.SiteFieldName = null;
            this.View_SalesSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_SalesType
            // 
            this.View_SalesType.CacheConnection = false;
            this.View_SalesType.CommandText = "SELECT * FROM dbo.[SalesType]";
            this.View_SalesType.CommandTimeout = 30;
            this.View_SalesType.CommandType = System.Data.CommandType.Text;
            this.View_SalesType.DynamicTableName = false;
            this.View_SalesType.EEPAlias = "JBERP";
            this.View_SalesType.EncodingAfter = null;
            this.View_SalesType.EncodingBefore = "Windows-1252";
            this.View_SalesType.EncodingConvert = null;
            this.View_SalesType.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "AutoKey";
            this.View_SalesType.KeyFields.Add(keyItem7);
            this.View_SalesType.MultiSetWhere = false;
            this.View_SalesType.Name = "View_SalesType";
            this.View_SalesType.NotificationAutoEnlist = false;
            this.View_SalesType.SecExcept = null;
            this.View_SalesType.SecFieldName = null;
            this.View_SalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesType.SelectPaging = false;
            this.View_SalesType.SelectTop = 0;
            this.View_SalesType.SiteControl = false;
            this.View_SalesType.SiteFieldName = null;
            this.View_SalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Users
            // 
            this.Users.CacheConnection = false;
            this.Users.CommandText = "SELECT  [USERID],[USERNAME]\r\nFROM [USERS] where description=\'JB\'";
            this.Users.CommandTimeout = 30;
            this.Users.CommandType = System.Data.CommandType.Text;
            this.Users.DynamicTableName = false;
            this.Users.EEPAlias = "EIPHRSYS";
            this.Users.EncodingAfter = null;
            this.Users.EncodingBefore = "Windows-1252";
            this.Users.EncodingConvert = null;
            this.Users.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "USERID";
            this.Users.KeyFields.Add(keyItem8);
            this.Users.MultiSetWhere = false;
            this.Users.Name = "Users";
            this.Users.NotificationAutoEnlist = false;
            this.Users.SecExcept = null;
            this.Users.SecFieldName = null;
            this.Users.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Users.SelectPaging = false;
            this.Users.SelectTop = 0;
            this.Users.SiteControl = false;
            this.Users.SiteFieldName = null;
            this.Users.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // dataGrid2
            // 
            this.dataGrid2.CacheConnection = false;
            this.dataGrid2.CommandText = "SELECT \'\' as SalesID,SalesTypeID,SalesTypeID+\'-\'+SalesTypeName as SalesTypeName F" +
    "ROM dbo.[SalesType]";
            this.dataGrid2.CommandTimeout = 30;
            this.dataGrid2.CommandType = System.Data.CommandType.Text;
            this.dataGrid2.DynamicTableName = false;
            this.dataGrid2.EEPAlias = "JBERP";
            this.dataGrid2.EncodingAfter = null;
            this.dataGrid2.EncodingBefore = "Windows-1252";
            this.dataGrid2.EncodingConvert = null;
            this.dataGrid2.InfoConnection = this.InfoConnection1;
            keyItem9.KeyName = "AutoKey";
            this.dataGrid2.KeyFields.Add(keyItem9);
            this.dataGrid2.MultiSetWhere = false;
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.NotificationAutoEnlist = false;
            this.dataGrid2.SecExcept = null;
            this.dataGrid2.SecFieldName = null;
            this.dataGrid2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.dataGrid2.SelectPaging = false;
            this.dataGrid2.SelectTop = 0;
            this.dataGrid2.SiteControl = false;
            this.dataGrid2.SiteFieldName = null;
            this.dataGrid2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "select InsGroupID,ShortName from InsGroup";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = "JBADMIN";
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.infoConnection2;
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
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
            // 
            // QSalesPerson
            // 
            this.QSalesPerson.CacheConnection = false;
            this.QSalesPerson.CommandText = "SELECT SalesID,SalesID+\'-\'+SalesName as SalesName FROM dbo.[SalesPerson] order by" +
    " SalesID";
            this.QSalesPerson.CommandTimeout = 30;
            this.QSalesPerson.CommandType = System.Data.CommandType.Text;
            this.QSalesPerson.DynamicTableName = false;
            this.QSalesPerson.EEPAlias = "JBERP";
            this.QSalesPerson.EncodingAfter = null;
            this.QSalesPerson.EncodingBefore = "Windows-1252";
            this.QSalesPerson.EncodingConvert = null;
            this.QSalesPerson.InfoConnection = this.InfoConnection1;
            keyItem10.KeyName = "SalesID";
            this.QSalesPerson.KeyFields.Add(keyItem10);
            this.QSalesPerson.MultiSetWhere = false;
            this.QSalesPerson.Name = "QSalesPerson";
            this.QSalesPerson.NotificationAutoEnlist = false;
            this.QSalesPerson.SecExcept = null;
            this.QSalesPerson.SecFieldName = null;
            this.QSalesPerson.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.QSalesPerson.SelectPaging = false;
            this.QSalesPerson.SelectTop = 0;
            this.QSalesPerson.SiteControl = false;
            this.QSalesPerson.SiteFieldName = null;
            this.QSalesPerson.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesTypeTree
            // 
            this.SalesTypeTree.CacheConnection = false;
            this.SalesTypeTree.CommandText = resources.GetString("SalesTypeTree.CommandText");
            this.SalesTypeTree.CommandTimeout = 30;
            this.SalesTypeTree.CommandType = System.Data.CommandType.Text;
            this.SalesTypeTree.DynamicTableName = false;
            this.SalesTypeTree.EEPAlias = "JBERP";
            this.SalesTypeTree.EncodingAfter = null;
            this.SalesTypeTree.EncodingBefore = "Windows-1252";
            this.SalesTypeTree.EncodingConvert = null;
            this.SalesTypeTree.InfoConnection = this.InfoConnection1;
            this.SalesTypeTree.MultiSetWhere = false;
            this.SalesTypeTree.Name = "SalesTypeTree";
            this.SalesTypeTree.NotificationAutoEnlist = false;
            this.SalesTypeTree.SecExcept = null;
            this.SalesTypeTree.SecFieldName = null;
            this.SalesTypeTree.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesTypeTree.SelectPaging = false;
            this.SalesTypeTree.SelectTop = 0;
            this.SalesTypeTree.SiteControl = false;
            this.SalesTypeTree.SiteFieldName = null;
            this.SalesTypeTree.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CustomerType
            // 
            this.CustomerType.CacheConnection = false;
            this.CustomerType.CommandText = "SELECT dbo.[SalesPerson].*,\r\n\'\' as SalesTypeID,\r\nJBERP.dbo.funReturnSalesSaleType" +
    "(SalesID) As SalesSaleTypeIDS\r\nFROM dbo.[SalesPerson]  \r\norder by SalesID";
            this.CustomerType.CommandTimeout = 30;
            this.CustomerType.CommandType = System.Data.CommandType.Text;
            this.CustomerType.DynamicTableName = false;
            this.CustomerType.EEPAlias = "JBERP";
            this.CustomerType.EncodingAfter = null;
            this.CustomerType.EncodingBefore = "Windows-1252";
            this.CustomerType.EncodingConvert = null;
            this.CustomerType.InfoConnection = this.InfoConnection1;
            keyItem11.KeyName = "SalesID";
            this.CustomerType.KeyFields.Add(keyItem11);
            this.CustomerType.MultiSetWhere = false;
            this.CustomerType.Name = "CustomerType";
            this.CustomerType.NotificationAutoEnlist = false;
            this.CustomerType.SecExcept = null;
            this.CustomerType.SecFieldName = null;
            this.CustomerType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CustomerType.SelectPaging = false;
            this.CustomerType.SelectTop = 0;
            this.CustomerType.SiteControl = false;
            this.CustomerType.SiteFieldName = null;
            this.CustomerType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QSalesPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTypeTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesPerson;
        private Srvtools.UpdateComponent ucSalesPerson;
        private Srvtools.InfoCommand SalesSalesType;
        private Srvtools.UpdateComponent ucSalesSalesType;
        private Srvtools.InfoCommand SalesType;
        private Srvtools.InfoCommand View_SalesPerson;
        private Srvtools.InfoCommand View_SalesSalesType;
        private Srvtools.InfoCommand View_SalesType;
        private Srvtools.InfoCommand Users;
        private Srvtools.InfoCommand dataGrid2;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand QSalesPerson;
        private Srvtools.InfoCommand SalesTypeTree;
        private Srvtools.InfoCommand CustomerType;
    }
}
