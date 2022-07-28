namespace sERPIntro
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPIntroMaster = new Srvtools.InfoCommand(this.components);
            this.ucERPIntroMaster = new Srvtools.UpdateComponent(this.components);
            this.ERPIntroDetail = new Srvtools.InfoCommand(this.components);
            this.ucERPIntroDetail = new Srvtools.UpdateComponent(this.components);
            this.idERPIntroMaster_ERPIntroDetail = new Srvtools.InfoDataSource(this.components);
            this.View_ERPIntroMaster = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.USERS = new Srvtools.InfoCommand(this.components);
            this.Depart = new Srvtools.InfoCommand(this.components);
            this.USERSORG = new Srvtools.InfoCommand(this.components);
            this.FlowFlag = new Srvtools.InfoCommand(this.components);
            this.TrueFalse = new Srvtools.InfoCommand(this.components);
            this.PrintReport = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPIntroMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPIntroDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPIntroMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERSORG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlowFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrueFalse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintReport)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetFixed_IntroNO";
            service1.NonLogin = false;
            service1.ServiceName = "GetFixed_IntroNO";
            service2.DelegateName = "SelectDepartManager";
            service2.NonLogin = false;
            service2.ServiceName = "SelectDepartManager";
            service3.DelegateName = "SelectIntroManager";
            service3.NonLogin = false;
            service3.ServiceName = "SelectIntroManager";
            service4.DelegateName = "FlowStartUp";
            service4.NonLogin = false;
            service4.ServiceName = "FlowStartUp";
            service5.DelegateName = "FlowReject";
            service5.NonLogin = false;
            service5.ServiceName = "FlowReject";
            service6.DelegateName = "ReportOrders";
            service6.NonLogin = false;
            service6.ServiceName = "ReportOrders";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPIntroMaster
            // 
            this.ERPIntroMaster.CacheConnection = false;
            this.ERPIntroMaster.CommandText = "SELECT dbo.[ERPIntroMaster].* ,\'\' as Button1 FROM dbo.[ERPIntroMaster] order by I" +
    "ntroNO desc";
            this.ERPIntroMaster.CommandTimeout = 30;
            this.ERPIntroMaster.CommandType = System.Data.CommandType.Text;
            this.ERPIntroMaster.DynamicTableName = false;
            this.ERPIntroMaster.EEPAlias = null;
            this.ERPIntroMaster.EncodingAfter = null;
            this.ERPIntroMaster.EncodingBefore = "Windows-1252";
            this.ERPIntroMaster.EncodingConvert = null;
            this.ERPIntroMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "IntroNO";
            this.ERPIntroMaster.KeyFields.Add(keyItem1);
            this.ERPIntroMaster.MultiSetWhere = false;
            this.ERPIntroMaster.Name = "ERPIntroMaster";
            this.ERPIntroMaster.NotificationAutoEnlist = false;
            this.ERPIntroMaster.SecExcept = null;
            this.ERPIntroMaster.SecFieldName = null;
            this.ERPIntroMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPIntroMaster.SelectPaging = false;
            this.ERPIntroMaster.SelectTop = 0;
            this.ERPIntroMaster.SiteControl = false;
            this.ERPIntroMaster.SiteFieldName = null;
            this.ERPIntroMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPIntroMaster
            // 
            this.ucERPIntroMaster.AutoTrans = true;
            this.ucERPIntroMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "IntroNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "Depart";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "DepartManager";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "IntroMan";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "IntroManager";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "UnderTaker";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CustomerDescr";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Deal";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "DealDescr";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "BonusAmount";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "BonusReleaseDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "BonusDescr";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "FlowFlag";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateBy";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "ParentIntroNO";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr1);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr2);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr3);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr4);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr5);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr6);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr7);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr8);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr9);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr10);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr11);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr12);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr13);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr14);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr15);
            this.ucERPIntroMaster.FieldAttrs.Add(fieldAttr16);
            this.ucERPIntroMaster.LogInfo = null;
            this.ucERPIntroMaster.Name = "ucERPIntroMaster";
            this.ucERPIntroMaster.RowAffectsCheck = true;
            this.ucERPIntroMaster.SelectCmd = this.ERPIntroMaster;
            this.ucERPIntroMaster.SelectCmdForUpdate = null;
            this.ucERPIntroMaster.SendSQLCmd = true;
            this.ucERPIntroMaster.ServerModify = true;
            this.ucERPIntroMaster.ServerModifyGetMax = false;
            this.ucERPIntroMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPIntroMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPIntroMaster.UseTranscationScope = false;
            this.ucERPIntroMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ERPIntroDetail
            // 
            this.ERPIntroDetail.CacheConnection = false;
            this.ERPIntroDetail.CommandText = "SELECT dbo.[ERPIntroDetail].* FROM dbo.[ERPIntroDetail]";
            this.ERPIntroDetail.CommandTimeout = 30;
            this.ERPIntroDetail.CommandType = System.Data.CommandType.Text;
            this.ERPIntroDetail.DynamicTableName = false;
            this.ERPIntroDetail.EEPAlias = null;
            this.ERPIntroDetail.EncodingAfter = null;
            this.ERPIntroDetail.EncodingBefore = "Windows-1252";
            this.ERPIntroDetail.EncodingConvert = null;
            this.ERPIntroDetail.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "IntroNO";
            keyItem3.KeyName = "AutoKey";
            this.ERPIntroDetail.KeyFields.Add(keyItem2);
            this.ERPIntroDetail.KeyFields.Add(keyItem3);
            this.ERPIntroDetail.MultiSetWhere = false;
            this.ERPIntroDetail.Name = "ERPIntroDetail";
            this.ERPIntroDetail.NotificationAutoEnlist = false;
            this.ERPIntroDetail.SecExcept = null;
            this.ERPIntroDetail.SecFieldName = null;
            this.ERPIntroDetail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPIntroDetail.SelectPaging = false;
            this.ERPIntroDetail.SelectTop = 0;
            this.ERPIntroDetail.SiteControl = false;
            this.ERPIntroDetail.SiteFieldName = null;
            this.ERPIntroDetail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPIntroDetail
            // 
            this.ucERPIntroDetail.AutoTrans = true;
            this.ucERPIntroDetail.ExceptJoin = false;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "IntroNO";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "AutoKey";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "TrackNote";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            this.ucERPIntroDetail.FieldAttrs.Add(fieldAttr17);
            this.ucERPIntroDetail.FieldAttrs.Add(fieldAttr18);
            this.ucERPIntroDetail.FieldAttrs.Add(fieldAttr19);
            this.ucERPIntroDetail.LogInfo = null;
            this.ucERPIntroDetail.Name = "ucERPIntroDetail";
            this.ucERPIntroDetail.RowAffectsCheck = true;
            this.ucERPIntroDetail.SelectCmd = this.ERPIntroDetail;
            this.ucERPIntroDetail.SelectCmdForUpdate = null;
            this.ucERPIntroDetail.SendSQLCmd = true;
            this.ucERPIntroDetail.ServerModify = true;
            this.ucERPIntroDetail.ServerModifyGetMax = false;
            this.ucERPIntroDetail.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPIntroDetail.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPIntroDetail.UseTranscationScope = false;
            this.ucERPIntroDetail.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idERPIntroMaster_ERPIntroDetail
            // 
            this.idERPIntroMaster_ERPIntroDetail.Detail = this.ERPIntroDetail;
            columnItem1.FieldName = "IntroNO";
            this.idERPIntroMaster_ERPIntroDetail.DetailColumns.Add(columnItem1);
            this.idERPIntroMaster_ERPIntroDetail.DynamicTableName = false;
            this.idERPIntroMaster_ERPIntroDetail.Master = this.ERPIntroMaster;
            columnItem2.FieldName = "IntroNO";
            this.idERPIntroMaster_ERPIntroDetail.MasterColumns.Add(columnItem2);
            // 
            // View_ERPIntroMaster
            // 
            this.View_ERPIntroMaster.CacheConnection = false;
            this.View_ERPIntroMaster.CommandText = "SELECT * FROM dbo.[ERPIntroMaster]";
            this.View_ERPIntroMaster.CommandTimeout = 30;
            this.View_ERPIntroMaster.CommandType = System.Data.CommandType.Text;
            this.View_ERPIntroMaster.DynamicTableName = false;
            this.View_ERPIntroMaster.EEPAlias = null;
            this.View_ERPIntroMaster.EncodingAfter = null;
            this.View_ERPIntroMaster.EncodingBefore = "Windows-1252";
            this.View_ERPIntroMaster.EncodingConvert = null;
            this.View_ERPIntroMaster.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "IntroNO";
            this.View_ERPIntroMaster.KeyFields.Add(keyItem4);
            this.View_ERPIntroMaster.MultiSetWhere = false;
            this.View_ERPIntroMaster.Name = "View_ERPIntroMaster";
            this.View_ERPIntroMaster.NotificationAutoEnlist = false;
            this.View_ERPIntroMaster.SecExcept = null;
            this.View_ERPIntroMaster.SecFieldName = null;
            this.View_ERPIntroMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPIntroMaster.SelectPaging = false;
            this.View_ERPIntroMaster.SelectTop = 0;
            this.View_ERPIntroMaster.SiteControl = false;
            this.View_ERPIntroMaster.SiteFieldName = null;
            this.View_ERPIntroMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "AutoNO1";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "GetFixed_IntroNO()";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 5;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "IntroNO";
            this.autoNumber1.UpdateComp = this.ucERPIntroMaster;
            // 
            // USERS
            // 
            this.USERS.CacheConnection = false;
            this.USERS.CommandText = "SELECT USERID,USERNAME FROM[EIPHRSYS].dbo.[USERS]\r\nWHERE DESCRIPTION=\'JB\' \r\nORDER" +
    " BY  USERID";
            this.USERS.CommandTimeout = 30;
            this.USERS.CommandType = System.Data.CommandType.Text;
            this.USERS.DynamicTableName = false;
            this.USERS.EEPAlias = null;
            this.USERS.EncodingAfter = null;
            this.USERS.EncodingBefore = "Windows-1252";
            this.USERS.EncodingConvert = null;
            this.USERS.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "USERID";
            this.USERS.KeyFields.Add(keyItem5);
            this.USERS.MultiSetWhere = false;
            this.USERS.Name = "USERS";
            this.USERS.NotificationAutoEnlist = false;
            this.USERS.SecExcept = null;
            this.USERS.SecFieldName = null;
            this.USERS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.USERS.SelectPaging = false;
            this.USERS.SelectTop = 0;
            this.USERS.SiteControl = false;
            this.USERS.SiteFieldName = null;
            this.USERS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Depart
            // 
            this.Depart.CacheConnection = false;
            this.Depart.CommandText = "SELECT ORG_DESC,USERID FROM [EIPHRSYS].[dbo].[SYS_ORG] o inner JOIN [EIPHRSYS].[d" +
    "bo].[USERGROUPS] ug ON ug.GROUPID=o.ORG_MAN where ORG_DESC  not in (\'福委會\')";
            this.Depart.CommandTimeout = 30;
            this.Depart.CommandType = System.Data.CommandType.Text;
            this.Depart.DynamicTableName = false;
            this.Depart.EEPAlias = null;
            this.Depart.EncodingAfter = null;
            this.Depart.EncodingBefore = "Windows-1252";
            this.Depart.EncodingConvert = null;
            this.Depart.InfoConnection = this.InfoConnection1;
            this.Depart.MultiSetWhere = false;
            this.Depart.Name = "Depart";
            this.Depart.NotificationAutoEnlist = false;
            this.Depart.SecExcept = null;
            this.Depart.SecFieldName = null;
            this.Depart.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Depart.SelectPaging = false;
            this.Depart.SelectTop = 0;
            this.Depart.SiteControl = false;
            this.Depart.SiteFieldName = null;
            this.Depart.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // USERSORG
            // 
            this.USERSORG.CacheConnection = false;
            this.USERSORG.CommandText = resources.GetString("USERSORG.CommandText");
            this.USERSORG.CommandTimeout = 30;
            this.USERSORG.CommandType = System.Data.CommandType.Text;
            this.USERSORG.DynamicTableName = false;
            this.USERSORG.EEPAlias = null;
            this.USERSORG.EncodingAfter = null;
            this.USERSORG.EncodingBefore = "Windows-1252";
            this.USERSORG.EncodingConvert = null;
            this.USERSORG.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "USERID";
            this.USERSORG.KeyFields.Add(keyItem6);
            this.USERSORG.MultiSetWhere = true;
            this.USERSORG.Name = "USERSORG";
            this.USERSORG.NotificationAutoEnlist = false;
            this.USERSORG.SecExcept = null;
            this.USERSORG.SecFieldName = null;
            this.USERSORG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.USERSORG.SelectPaging = false;
            this.USERSORG.SelectTop = 0;
            this.USERSORG.SiteControl = false;
            this.USERSORG.SiteFieldName = null;
            this.USERSORG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // FlowFlag
            // 
            this.FlowFlag.CacheConnection = false;
            this.FlowFlag.CommandText = "select \'Z\' as Code,\'結案\' as Name\r\nunion\r\nselect \'P\' as Code,\'新申請\' as Name\r\nunion\r\n" +
    "select \'N\' as Code,\'流程中\' as Name\r\nunion\r\nselect \'X\' as Code,\'作廢\' as Name";
            this.FlowFlag.CommandTimeout = 30;
            this.FlowFlag.CommandType = System.Data.CommandType.Text;
            this.FlowFlag.DynamicTableName = false;
            this.FlowFlag.EEPAlias = null;
            this.FlowFlag.EncodingAfter = null;
            this.FlowFlag.EncodingBefore = "Windows-1252";
            this.FlowFlag.EncodingConvert = null;
            this.FlowFlag.InfoConnection = this.InfoConnection1;
            this.FlowFlag.MultiSetWhere = false;
            this.FlowFlag.Name = "FlowFlag";
            this.FlowFlag.NotificationAutoEnlist = false;
            this.FlowFlag.SecExcept = null;
            this.FlowFlag.SecFieldName = null;
            this.FlowFlag.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FlowFlag.SelectPaging = false;
            this.FlowFlag.SelectTop = 0;
            this.FlowFlag.SiteControl = false;
            this.FlowFlag.SiteFieldName = null;
            this.FlowFlag.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // TrueFalse
            // 
            this.TrueFalse.CacheConnection = false;
            this.TrueFalse.CommandText = "select 0 as Code,\'否\' as Name\r\nunion\r\nselect 1 as Code,\'是\' as Name";
            this.TrueFalse.CommandTimeout = 30;
            this.TrueFalse.CommandType = System.Data.CommandType.Text;
            this.TrueFalse.DynamicTableName = false;
            this.TrueFalse.EEPAlias = null;
            this.TrueFalse.EncodingAfter = null;
            this.TrueFalse.EncodingBefore = "Windows-1252";
            this.TrueFalse.EncodingConvert = null;
            this.TrueFalse.InfoConnection = this.InfoConnection1;
            this.TrueFalse.MultiSetWhere = false;
            this.TrueFalse.Name = "TrueFalse";
            this.TrueFalse.NotificationAutoEnlist = false;
            this.TrueFalse.SecExcept = null;
            this.TrueFalse.SecFieldName = null;
            this.TrueFalse.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.TrueFalse.SelectPaging = false;
            this.TrueFalse.SelectTop = 0;
            this.TrueFalse.SiteControl = false;
            this.TrueFalse.SiteFieldName = null;
            this.TrueFalse.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PrintReport
            // 
            this.PrintReport.CacheConnection = false;
            this.PrintReport.CommandText = resources.GetString("PrintReport.CommandText");
            this.PrintReport.CommandTimeout = 30;
            this.PrintReport.CommandType = System.Data.CommandType.Text;
            this.PrintReport.DynamicTableName = false;
            this.PrintReport.EEPAlias = null;
            this.PrintReport.EncodingAfter = null;
            this.PrintReport.EncodingBefore = "Windows-1252";
            this.PrintReport.EncodingConvert = null;
            this.PrintReport.InfoConnection = this.InfoConnection1;
            this.PrintReport.MultiSetWhere = false;
            this.PrintReport.Name = "PrintReport";
            this.PrintReport.NotificationAutoEnlist = false;
            this.PrintReport.SecExcept = null;
            this.PrintReport.SecFieldName = null;
            this.PrintReport.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PrintReport.SelectPaging = false;
            this.PrintReport.SelectTop = 0;
            this.PrintReport.SiteControl = false;
            this.PrintReport.SiteFieldName = null;
            this.PrintReport.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPIntroMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPIntroDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPIntroMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERSORG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlowFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrueFalse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintReport)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPIntroMaster;
        private Srvtools.UpdateComponent ucERPIntroMaster;
        private Srvtools.InfoCommand ERPIntroDetail;
        private Srvtools.UpdateComponent ucERPIntroDetail;
        private Srvtools.InfoDataSource idERPIntroMaster_ERPIntroDetail;
        private Srvtools.InfoCommand View_ERPIntroMaster;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand USERS;
        private Srvtools.InfoCommand Depart;
        private Srvtools.InfoCommand USERSORG;
        private Srvtools.InfoCommand FlowFlag;
        private Srvtools.InfoCommand TrueFalse;
        private Srvtools.InfoCommand PrintReport;
    }
}
