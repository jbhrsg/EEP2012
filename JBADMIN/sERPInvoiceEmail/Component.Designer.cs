namespace sERPInvoiceEmail
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.InvoiceDetails = new Srvtools.InfoCommand(this.components);
            this.ucInvoiceDetails = new Srvtools.UpdateComponent(this.components);
            this.View_InvoiceDetails = new Srvtools.InfoCommand(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            this.SalesPerson = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.InvoiceYM = new Srvtools.InfoCommand(this.components);
            this.EmailLogsList = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_InvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceYM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmailLogsList)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetInvoiceByInvoiceNO";
            service1.NonLogin = false;
            service1.ServiceName = "GetInvoiceByInvoiceNO";
            service2.DelegateName = "CreateInvoiceLogs";
            service2.NonLogin = false;
            service2.ServiceName = "CreateInvoiceLogs";
            service3.DelegateName = "GetGridDataEmailLogs";
            service3.NonLogin = false;
            service3.ServiceName = "GetGridDataEmailLogs";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // InvoiceDetails
            // 
            this.InvoiceDetails.CacheConnection = false;
            this.InvoiceDetails.CommandText = "SELECT  *,\'\' AS STR,0 AS IsSend FROM View_ERPInvoiceEmail  order by  InvoiceDate " +
    "Desc\r\n\r\n";
            this.InvoiceDetails.CommandTimeout = 30;
            this.InvoiceDetails.CommandType = System.Data.CommandType.Text;
            this.InvoiceDetails.DynamicTableName = false;
            this.InvoiceDetails.EEPAlias = "JBERP";
            this.InvoiceDetails.EncodingAfter = null;
            this.InvoiceDetails.EncodingBefore = "Windows-1252";
            this.InvoiceDetails.EncodingConvert = null;
            this.InvoiceDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "InvoiceNO";
            this.InvoiceDetails.KeyFields.Add(keyItem1);
            this.InvoiceDetails.MultiSetWhere = false;
            this.InvoiceDetails.Name = "InvoiceDetails";
            this.InvoiceDetails.NotificationAutoEnlist = false;
            this.InvoiceDetails.SecExcept = null;
            this.InvoiceDetails.SecFieldName = null;
            this.InvoiceDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InvoiceDetails.SelectPaging = false;
            this.InvoiceDetails.SelectTop = 0;
            this.InvoiceDetails.SiteControl = false;
            this.InvoiceDetails.SiteFieldName = null;
            this.InvoiceDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucInvoiceDetails
            // 
            this.ucInvoiceDetails.AutoTrans = true;
            this.ucInvoiceDetails.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "InvoiceNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "RandNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "InsGroupID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "SalesTypeID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SalesDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "InvoiceDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ARDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "SalesAmount";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "SalesTax";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "SalesTotal";
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
            fieldAttr13.DataField = "TaxRate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "Employer";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CustomerID";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "InvoiceTypeID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "QInvoiceType";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "IsActive";
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
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "LastUpdateBy";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "LastUpdateDate";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "InvoiceYM";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr1);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr2);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr3);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr4);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr5);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr6);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr7);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr8);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr9);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr10);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr11);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr12);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr13);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr14);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr15);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr16);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr17);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr18);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr19);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr20);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr21);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr22);
            this.ucInvoiceDetails.FieldAttrs.Add(fieldAttr23);
            this.ucInvoiceDetails.LogInfo = null;
            this.ucInvoiceDetails.Name = "ucInvoiceDetails";
            this.ucInvoiceDetails.RowAffectsCheck = true;
            this.ucInvoiceDetails.SelectCmd = this.InvoiceDetails;
            this.ucInvoiceDetails.SelectCmdForUpdate = null;
            this.ucInvoiceDetails.SendSQLCmd = true;
            this.ucInvoiceDetails.ServerModify = true;
            this.ucInvoiceDetails.ServerModifyGetMax = false;
            this.ucInvoiceDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucInvoiceDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucInvoiceDetails.UseTranscationScope = false;
            this.ucInvoiceDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_InvoiceDetails
            // 
            this.View_InvoiceDetails.CacheConnection = false;
            this.View_InvoiceDetails.CommandText = "SELECT * FROM dbo.[InvoiceDetails]";
            this.View_InvoiceDetails.CommandTimeout = 30;
            this.View_InvoiceDetails.CommandType = System.Data.CommandType.Text;
            this.View_InvoiceDetails.DynamicTableName = false;
            this.View_InvoiceDetails.EEPAlias = null;
            this.View_InvoiceDetails.EncodingAfter = null;
            this.View_InvoiceDetails.EncodingBefore = "Windows-1252";
            this.View_InvoiceDetails.EncodingConvert = null;
            this.View_InvoiceDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "InvoiceNO";
            this.View_InvoiceDetails.KeyFields.Add(keyItem2);
            this.View_InvoiceDetails.MultiSetWhere = false;
            this.View_InvoiceDetails.Name = "View_InvoiceDetails";
            this.View_InvoiceDetails.NotificationAutoEnlist = false;
            this.View_InvoiceDetails.SecExcept = null;
            this.View_InvoiceDetails.SecFieldName = null;
            this.View_InvoiceDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_InvoiceDetails.SelectPaging = false;
            this.View_InvoiceDetails.SelectTop = 0;
            this.View_InvoiceDetails.SiteControl = false;
            this.View_InvoiceDetails.SiteFieldName = null;
            this.View_InvoiceDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = resources.GetString("SalesType.CommandText");
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = "JBERP";
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "SalesID";
            keyItem4.KeyName = "SalesTypeID";
            this.SalesType.KeyFields.Add(keyItem3);
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
            // SalesPerson
            // 
            this.SalesPerson.CacheConnection = false;
            this.SalesPerson.CommandText = "select SalesPerson.SalesID,SalesPerson.SalesName \r\nfrom SalesPerson\r\nwhere SalesI" +
    "D in (Select SalesID From InvoiceDetails Group By SalesID)\r\norder by SalesID";
            this.SalesPerson.CommandTimeout = 30;
            this.SalesPerson.CommandType = System.Data.CommandType.Text;
            this.SalesPerson.DynamicTableName = false;
            this.SalesPerson.EEPAlias = "JBERP";
            this.SalesPerson.EncodingAfter = null;
            this.SalesPerson.EncodingBefore = "Windows-1252";
            this.SalesPerson.EncodingConvert = null;
            this.SalesPerson.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "SalesID";
            this.SalesPerson.KeyFields.Add(keyItem5);
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
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "select InsGroupID,ShortName from JBADMIN.DBO.InsGroup where IsActive=1";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = "JBERP";
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "InsGroupID";
            this.InsGroup.KeyFields.Add(keyItem6);
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
            // InvoiceYM
            // 
            this.InvoiceYM.CacheConnection = false;
            this.InvoiceYM.CommandText = "SELECT  distinct  InvoiceYM  FROM InvoiceDetails ORDER BY InvoiceYM DESC";
            this.InvoiceYM.CommandTimeout = 30;
            this.InvoiceYM.CommandType = System.Data.CommandType.Text;
            this.InvoiceYM.DynamicTableName = false;
            this.InvoiceYM.EEPAlias = "JBERP";
            this.InvoiceYM.EncodingAfter = null;
            this.InvoiceYM.EncodingBefore = "Windows-1252";
            this.InvoiceYM.EncodingConvert = null;
            this.InvoiceYM.InfoConnection = this.InfoConnection1;
            this.InvoiceYM.MultiSetWhere = false;
            this.InvoiceYM.Name = "InvoiceYM";
            this.InvoiceYM.NotificationAutoEnlist = false;
            this.InvoiceYM.SecExcept = null;
            this.InvoiceYM.SecFieldName = null;
            this.InvoiceYM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InvoiceYM.SelectPaging = false;
            this.InvoiceYM.SelectTop = 0;
            this.InvoiceYM.SiteControl = false;
            this.InvoiceYM.SiteFieldName = null;
            this.InvoiceYM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // EmailLogsList
            // 
            this.EmailLogsList.CacheConnection = false;
            this.EmailLogsList.CommandText = "Select  ToMail,ToName,CreateBy,CreateDate \r\nFrom InvoiceEmailLog where 1=2";
            this.EmailLogsList.CommandTimeout = 30;
            this.EmailLogsList.CommandType = System.Data.CommandType.Text;
            this.EmailLogsList.DynamicTableName = false;
            this.EmailLogsList.EEPAlias = "JBERP";
            this.EmailLogsList.EncodingAfter = null;
            this.EmailLogsList.EncodingBefore = "Windows-1252";
            this.EmailLogsList.EncodingConvert = null;
            this.EmailLogsList.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "SalesID";
            this.EmailLogsList.KeyFields.Add(keyItem7);
            this.EmailLogsList.MultiSetWhere = false;
            this.EmailLogsList.Name = "EmailLogsList";
            this.EmailLogsList.NotificationAutoEnlist = false;
            this.EmailLogsList.SecExcept = null;
            this.EmailLogsList.SecFieldName = null;
            this.EmailLogsList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EmailLogsList.SelectPaging = false;
            this.EmailLogsList.SelectTop = 0;
            this.EmailLogsList.SiteControl = false;
            this.EmailLogsList.SiteFieldName = null;
            this.EmailLogsList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_InvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceYM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmailLogsList)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand InvoiceDetails;
        private Srvtools.UpdateComponent ucInvoiceDetails;
        private Srvtools.InfoCommand View_InvoiceDetails;
        private Srvtools.InfoCommand SalesType;
        private Srvtools.InfoCommand SalesPerson;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoCommand InvoiceYM;
        private Srvtools.InfoCommand EmailLogsList;
    }
}
