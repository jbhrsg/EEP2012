namespace sERP_Normal_SalesAbnormalApply
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
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr26 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr27 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SalesAbnormalApply = new Srvtools.InfoCommand(this.components);
            this.ucSalesAbnormalApply = new Srvtools.UpdateComponent(this.components);
            this.View_SalesAbnormalApply = new Srvtools.InfoCommand(this.components);
            this.SalesNO = new Srvtools.InfoCommand(this.components);
            this.SYS_ORG = new Srvtools.InfoCommand(this.components);
            this.Users = new Srvtools.InfoCommand(this.components);
            this.SalesItem1 = new Srvtools.InfoCommand(this.components);
            this.SalesException = new Srvtools.InfoCommand(this.components);
            this.SalesExceptDealType = new Srvtools.InfoCommand(this.components);
            this.SAANO = new Srvtools.AutoNumber(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesAbnormalApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesAbnormalApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ORG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesException)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesExceptDealType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetUserOrgNOs";
            service1.NonLogin = false;
            service1.ServiceName = "GetUserOrgNOs";
            service2.DelegateName = "InsertWarrantMasterDetails";
            service2.NonLogin = false;
            service2.ServiceName = "InsertWarrantMasterDetails";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // SalesAbnormalApply
            // 
            this.SalesAbnormalApply.CacheConnection = false;
            this.SalesAbnormalApply.CommandText = "SELECT dbo.[SalesAbnormalApply].* FROM dbo.[SalesAbnormalApply]";
            this.SalesAbnormalApply.CommandTimeout = 30;
            this.SalesAbnormalApply.CommandType = System.Data.CommandType.Text;
            this.SalesAbnormalApply.DynamicTableName = false;
            this.SalesAbnormalApply.EEPAlias = "JBERP";
            this.SalesAbnormalApply.EncodingAfter = null;
            this.SalesAbnormalApply.EncodingBefore = "Windows-1252";
            this.SalesAbnormalApply.EncodingConvert = null;
            this.SalesAbnormalApply.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ApplyNO";
            this.SalesAbnormalApply.KeyFields.Add(keyItem1);
            this.SalesAbnormalApply.MultiSetWhere = false;
            this.SalesAbnormalApply.Name = "SalesAbnormalApply";
            this.SalesAbnormalApply.NotificationAutoEnlist = false;
            this.SalesAbnormalApply.SecExcept = null;
            this.SalesAbnormalApply.SecFieldName = null;
            this.SalesAbnormalApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesAbnormalApply.SelectPaging = false;
            this.SalesAbnormalApply.SelectTop = 0;
            this.SalesAbnormalApply.SiteControl = false;
            this.SalesAbnormalApply.SiteFieldName = null;
            this.SalesAbnormalApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesAbnormalApply
            // 
            this.ucSalesAbnormalApply.AutoTrans = true;
            this.ucSalesAbnormalApply.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ApplyNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SalesNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ApplyEmpID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ApplyDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ApplyOrg_NO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AbnormalType";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "SalesTypeID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CustomerID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "InvoiceNO";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Contact";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "TaxNO";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "SalesID";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "SumAmount";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "SalesTax";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "SalesTotal";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "SalesAmount";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "AbnormalReasonID";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "AbnormalReason";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "DealTypeID";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "DealType";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "Flowflag";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "WarrantAmount";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "IsWarrant";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "CreateBy";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = "_username";
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "CreateDate";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = "_sysdate";
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "Org_NOParent";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "WarrantField";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr1);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr2);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr3);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr4);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr5);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr6);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr7);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr8);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr9);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr10);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr11);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr12);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr13);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr14);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr15);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr16);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr17);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr18);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr19);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr20);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr21);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr22);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr23);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr24);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr25);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr26);
            this.ucSalesAbnormalApply.FieldAttrs.Add(fieldAttr27);
            this.ucSalesAbnormalApply.LogInfo = null;
            this.ucSalesAbnormalApply.Name = "ucSalesAbnormalApply";
            this.ucSalesAbnormalApply.RowAffectsCheck = true;
            this.ucSalesAbnormalApply.SelectCmd = this.SalesAbnormalApply;
            this.ucSalesAbnormalApply.SelectCmdForUpdate = null;
            this.ucSalesAbnormalApply.SendSQLCmd = true;
            this.ucSalesAbnormalApply.ServerModify = true;
            this.ucSalesAbnormalApply.ServerModifyGetMax = false;
            this.ucSalesAbnormalApply.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesAbnormalApply.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesAbnormalApply.UseTranscationScope = false;
            this.ucSalesAbnormalApply.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_SalesAbnormalApply
            // 
            this.View_SalesAbnormalApply.CacheConnection = false;
            this.View_SalesAbnormalApply.CommandText = "SELECT * FROM dbo.[SalesAbnormalApply]";
            this.View_SalesAbnormalApply.CommandTimeout = 30;
            this.View_SalesAbnormalApply.CommandType = System.Data.CommandType.Text;
            this.View_SalesAbnormalApply.DynamicTableName = false;
            this.View_SalesAbnormalApply.EEPAlias = "JBERP";
            this.View_SalesAbnormalApply.EncodingAfter = null;
            this.View_SalesAbnormalApply.EncodingBefore = "Windows-1252";
            this.View_SalesAbnormalApply.EncodingConvert = null;
            this.View_SalesAbnormalApply.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ApplyNO";
            this.View_SalesAbnormalApply.KeyFields.Add(keyItem2);
            this.View_SalesAbnormalApply.MultiSetWhere = false;
            this.View_SalesAbnormalApply.Name = "View_SalesAbnormalApply";
            this.View_SalesAbnormalApply.NotificationAutoEnlist = false;
            this.View_SalesAbnormalApply.SecExcept = null;
            this.View_SalesAbnormalApply.SecFieldName = null;
            this.View_SalesAbnormalApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesAbnormalApply.SelectPaging = false;
            this.View_SalesAbnormalApply.SelectTop = 0;
            this.View_SalesAbnormalApply.SiteControl = false;
            this.View_SalesAbnormalApply.SiteFieldName = null;
            this.View_SalesAbnormalApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesNO
            // 
            this.SalesNO.CacheConnection = false;
            this.SalesNO.CommandText = "select top 50 * from View_SalesAbnormalApply_SalesNO order by SalesNO desc";
            this.SalesNO.CommandTimeout = 30;
            this.SalesNO.CommandType = System.Data.CommandType.Text;
            this.SalesNO.DynamicTableName = false;
            this.SalesNO.EEPAlias = "JBERP";
            this.SalesNO.EncodingAfter = null;
            this.SalesNO.EncodingBefore = "Windows-1252";
            this.SalesNO.EncodingConvert = null;
            this.SalesNO.InfoConnection = this.InfoConnection1;
            this.SalesNO.MultiSetWhere = false;
            this.SalesNO.Name = "SalesNO";
            this.SalesNO.NotificationAutoEnlist = false;
            this.SalesNO.SecExcept = null;
            this.SalesNO.SecFieldName = null;
            this.SalesNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesNO.SelectPaging = false;
            this.SalesNO.SelectTop = 0;
            this.SalesNO.SiteControl = false;
            this.SalesNO.SiteFieldName = null;
            this.SalesNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SYS_ORG
            // 
            this.SYS_ORG.CacheConnection = false;
            this.SYS_ORG.CommandText = "SELECT  [ORG_NO],[ORG_DESC],[COSTCENTERID]\r\n  FROM EIPHRSYS.dbo.[SYS_ORG]";
            this.SYS_ORG.CommandTimeout = 30;
            this.SYS_ORG.CommandType = System.Data.CommandType.Text;
            this.SYS_ORG.DynamicTableName = false;
            this.SYS_ORG.EEPAlias = "EIPHRSYS";
            this.SYS_ORG.EncodingAfter = null;
            this.SYS_ORG.EncodingBefore = "Windows-1252";
            this.SYS_ORG.EncodingConvert = null;
            this.SYS_ORG.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ORG_NO";
            this.SYS_ORG.KeyFields.Add(keyItem3);
            this.SYS_ORG.MultiSetWhere = false;
            this.SYS_ORG.Name = "SYS_ORG";
            this.SYS_ORG.NotificationAutoEnlist = false;
            this.SYS_ORG.SecExcept = null;
            this.SYS_ORG.SecFieldName = null;
            this.SYS_ORG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SYS_ORG.SelectPaging = false;
            this.SYS_ORG.SelectTop = 0;
            this.SYS_ORG.SiteControl = false;
            this.SYS_ORG.SiteFieldName = null;
            this.SYS_ORG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Users
            // 
            this.Users.CacheConnection = false;
            this.Users.CommandText = "SELECT USERID,USERNAME FROM  [EIPHRSYS].[dbo].[USERS]\r\nwhere users.[DESCRIPTION]=" +
    "\'JB\'";
            this.Users.CommandTimeout = 30;
            this.Users.CommandType = System.Data.CommandType.Text;
            this.Users.DynamicTableName = false;
            this.Users.EEPAlias = "EIPHRSYS";
            this.Users.EncodingAfter = null;
            this.Users.EncodingBefore = "Windows-1252";
            this.Users.EncodingConvert = null;
            this.Users.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "USERID";
            this.Users.KeyFields.Add(keyItem4);
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
            // SalesItem1
            // 
            this.SalesItem1.CacheConnection = false;
            this.SalesItem1.CommandText = "select ERPSalesItem.SalesItemID,ERPSalesItem.SalesItemName,ERPSalesItem.SalesItem" +
    "Type from [JBADMIN].dbo.ERPSalesItem\r\nwhere ERPSalesItem.SalesItemType=\'-\'\r\norde" +
    "r by ERPSalesItem.SalesItemID";
            this.SalesItem1.CommandTimeout = 30;
            this.SalesItem1.CommandType = System.Data.CommandType.Text;
            this.SalesItem1.DynamicTableName = false;
            this.SalesItem1.EEPAlias = "";
            this.SalesItem1.EncodingAfter = null;
            this.SalesItem1.EncodingBefore = "Windows-1252";
            this.SalesItem1.EncodingConvert = null;
            this.SalesItem1.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "SalesItemID";
            this.SalesItem1.KeyFields.Add(keyItem5);
            this.SalesItem1.MultiSetWhere = false;
            this.SalesItem1.Name = "SalesItem1";
            this.SalesItem1.NotificationAutoEnlist = false;
            this.SalesItem1.SecExcept = null;
            this.SalesItem1.SecFieldName = null;
            this.SalesItem1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesItem1.SelectPaging = false;
            this.SalesItem1.SelectTop = 0;
            this.SalesItem1.SiteControl = false;
            this.SalesItem1.SiteFieldName = null;
            this.SalesItem1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesException
            // 
            this.SalesException.CacheConnection = false;
            this.SalesException.CommandText = "select ERPSalesException.* from JBADMIN.dbo.ERPSalesException";
            this.SalesException.CommandTimeout = 30;
            this.SalesException.CommandType = System.Data.CommandType.Text;
            this.SalesException.DynamicTableName = false;
            this.SalesException.EEPAlias = null;
            this.SalesException.EncodingAfter = null;
            this.SalesException.EncodingBefore = "Windows-1252";
            this.SalesException.EncodingConvert = null;
            this.SalesException.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "SalesExceptionNO";
            this.SalesException.KeyFields.Add(keyItem6);
            this.SalesException.MultiSetWhere = false;
            this.SalesException.Name = "SalesException";
            this.SalesException.NotificationAutoEnlist = false;
            this.SalesException.SecExcept = null;
            this.SalesException.SecFieldName = null;
            this.SalesException.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesException.SelectPaging = false;
            this.SalesException.SelectTop = 0;
            this.SalesException.SiteControl = false;
            this.SalesException.SiteFieldName = null;
            this.SalesException.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesExceptDealType
            // 
            this.SalesExceptDealType.CacheConnection = false;
            this.SalesExceptDealType.CommandText = "select ERPSalesExceptDealType.* from JBADMIN.dbo.ERPSalesExceptDealType";
            this.SalesExceptDealType.CommandTimeout = 30;
            this.SalesExceptDealType.CommandType = System.Data.CommandType.Text;
            this.SalesExceptDealType.DynamicTableName = false;
            this.SalesExceptDealType.EEPAlias = null;
            this.SalesExceptDealType.EncodingAfter = null;
            this.SalesExceptDealType.EncodingBefore = "Windows-1252";
            this.SalesExceptDealType.EncodingConvert = null;
            this.SalesExceptDealType.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "ExceptDealTypeNO";
            this.SalesExceptDealType.KeyFields.Add(keyItem7);
            this.SalesExceptDealType.MultiSetWhere = false;
            this.SalesExceptDealType.Name = "SalesExceptDealType";
            this.SalesExceptDealType.NotificationAutoEnlist = false;
            this.SalesExceptDealType.SecExcept = null;
            this.SalesExceptDealType.SecFieldName = null;
            this.SalesExceptDealType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesExceptDealType.SelectPaging = false;
            this.SalesExceptDealType.SelectTop = 0;
            this.SalesExceptDealType.SiteControl = false;
            this.SalesExceptDealType.SiteFieldName = null;
            this.SalesExceptDealType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SAANO
            // 
            this.SAANO.Active = true;
            this.SAANO.AutoNoID = "SAANO";
            this.SAANO.Description = null;
            this.SAANO.GetFixed = "SalesAbnormalApplyNO_GetFixed()";
            this.SAANO.isNumFill = false;
            this.SAANO.Name = "SAANO";
            this.SAANO.Number = null;
            this.SAANO.NumDig = 5;
            this.SAANO.OldVersion = false;
            this.SAANO.OverFlow = true;
            this.SAANO.StartValue = 1;
            this.SAANO.Step = 1;
            this.SAANO.TargetColumn = "ApplyNO";
            this.SAANO.UpdateComp = this.ucSalesAbnormalApply;
            // 
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = resources.GetString("Customer.CommandText");
            this.Customer.CommandTimeout = 30;
            this.Customer.CommandType = System.Data.CommandType.Text;
            this.Customer.DynamicTableName = false;
            this.Customer.EEPAlias = "JBERP";
            this.Customer.EncodingAfter = null;
            this.Customer.EncodingBefore = "Windows-1252";
            this.Customer.EncodingConvert = null;
            this.Customer.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "CustomerID";
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesAbnormalApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesAbnormalApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ORG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesException)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesExceptDealType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesAbnormalApply;
        private Srvtools.UpdateComponent ucSalesAbnormalApply;
        private Srvtools.InfoCommand View_SalesAbnormalApply;
        private Srvtools.InfoCommand SalesNO;
        private Srvtools.InfoCommand SYS_ORG;
        private Srvtools.InfoCommand Users;
        private Srvtools.InfoCommand SalesItem1;
        private Srvtools.InfoCommand SalesException;
        private Srvtools.InfoCommand SalesExceptDealType;
        private Srvtools.AutoNumber SAANO;
        private Srvtools.InfoCommand Customer;
    }
}
