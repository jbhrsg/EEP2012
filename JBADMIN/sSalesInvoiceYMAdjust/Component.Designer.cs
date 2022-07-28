namespace sSalesInvoiceYMAdjust
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
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
            Srvtools.FieldAttr fieldAttr28 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr29 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr30 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr31 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr32 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr33 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr34 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr35 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr36 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr37 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr38 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr39 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalesDetails = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesDetails = new Srvtools.UpdateComponent(this.components);
            this.infoCustNO = new Srvtools.InfoCommand(this.components);
            this.infoSalesMan = new Srvtools.InfoCommand(this.components);
            this.infoSalesTypeAll = new Srvtools.InfoCommand(this.components);
            this.infoSalesManAll = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesTypeAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesManAll)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "UpdateSalesDetails";
            service1.NonLogin = false;
            service1.ServiceName = "UpdateSalesDetails";
            service2.DelegateName = "InsertSalesDetailsYetImport";
            service2.NonLogin = false;
            service2.ServiceName = "InsertSalesDetailsYetImport";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalesDetails
            // 
            this.ERPSalesDetails.CacheConnection = false;
            this.ERPSalesDetails.CommandText = resources.GetString("ERPSalesDetails.CommandText");
            this.ERPSalesDetails.CommandTimeout = 30;
            this.ERPSalesDetails.CommandType = System.Data.CommandType.Text;
            this.ERPSalesDetails.DynamicTableName = false;
            this.ERPSalesDetails.EEPAlias = null;
            this.ERPSalesDetails.EncodingAfter = null;
            this.ERPSalesDetails.EncodingBefore = "Windows-1252";
            this.ERPSalesDetails.EncodingConvert = null;
            this.ERPSalesDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesMasterNO";
            keyItem2.KeyName = "ItemSeq";
            this.ERPSalesDetails.KeyFields.Add(keyItem1);
            this.ERPSalesDetails.KeyFields.Add(keyItem2);
            this.ERPSalesDetails.MultiSetWhere = false;
            this.ERPSalesDetails.Name = "ERPSalesDetails";
            this.ERPSalesDetails.NotificationAutoEnlist = false;
            this.ERPSalesDetails.SecExcept = null;
            this.ERPSalesDetails.SecFieldName = null;
            this.ERPSalesDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesDetails.SelectPaging = false;
            this.ERPSalesDetails.SelectTop = 0;
            this.ERPSalesDetails.SiteControl = false;
            this.ERPSalesDetails.SiteFieldName = null;
            this.ERPSalesDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalesDetails
            // 
            this.ucERPSalesDetails.AutoTrans = true;
            this.ucERPSalesDetails.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalesMasterNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ItemSeq";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CustNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SalesID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "SalesEmployeeID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SalesTypeID";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "DMTypeID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "SalesDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "InvoiceYM";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "GrantTypeID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "SalesQty";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "SalesQtyView";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "Commission";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "PublishCount";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "PresentCount";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "PresentWNewsCount";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "SalesDescr";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "SalesDescrDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "SalesDescrAlert";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CustPrice";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CustLines";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "CustAmt";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "OfficePrice";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "OfficeLines";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "OfficeAmt";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "NewsTypeID";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "NewsAreaID";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "NewsPublishID";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "Sections";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "IsActive";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "IsSetInvoice";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "SalesOutLine";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "IsImport";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            fieldAttr34.CheckNull = false;
            fieldAttr34.DataField = "CreateBy";
            fieldAttr34.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr34.DefaultValue = null;
            fieldAttr34.TrimLength = 0;
            fieldAttr34.UpdateEnable = true;
            fieldAttr34.WhereMode = true;
            fieldAttr35.CheckNull = false;
            fieldAttr35.DataField = "CreateDate";
            fieldAttr35.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr35.DefaultValue = null;
            fieldAttr35.TrimLength = 0;
            fieldAttr35.UpdateEnable = true;
            fieldAttr35.WhereMode = true;
            fieldAttr36.CheckNull = false;
            fieldAttr36.DataField = "IsTransSys";
            fieldAttr36.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr36.DefaultValue = null;
            fieldAttr36.TrimLength = 0;
            fieldAttr36.UpdateEnable = true;
            fieldAttr36.WhereMode = true;
            fieldAttr37.CheckNull = false;
            fieldAttr37.DataField = "InvoiceYMPoint";
            fieldAttr37.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr37.DefaultValue = null;
            fieldAttr37.TrimLength = 0;
            fieldAttr37.UpdateEnable = true;
            fieldAttr37.WhereMode = true;
            fieldAttr38.CheckNull = false;
            fieldAttr38.DataField = "depositSeq";
            fieldAttr38.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr38.DefaultValue = null;
            fieldAttr38.TrimLength = 0;
            fieldAttr38.UpdateEnable = true;
            fieldAttr38.WhereMode = true;
            fieldAttr39.CheckNull = false;
            fieldAttr39.DataField = "ViewAreaID";
            fieldAttr39.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr39.DefaultValue = null;
            fieldAttr39.TrimLength = 0;
            fieldAttr39.UpdateEnable = true;
            fieldAttr39.WhereMode = true;
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr1);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr2);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr3);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr4);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr5);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr6);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr7);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr8);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr9);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr10);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr11);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr12);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr13);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr14);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr15);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr16);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr17);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr18);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr19);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr20);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr21);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr22);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr23);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr24);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr25);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr26);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr27);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr28);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr29);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr30);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr31);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr32);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr33);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr34);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr35);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr36);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr37);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr38);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr39);
            this.ucERPSalesDetails.LogInfo = null;
            this.ucERPSalesDetails.Name = "ucERPSalesDetails";
            this.ucERPSalesDetails.RowAffectsCheck = true;
            this.ucERPSalesDetails.SelectCmd = this.ERPSalesDetails;
            this.ucERPSalesDetails.SelectCmdForUpdate = null;
            this.ucERPSalesDetails.SendSQLCmd = true;
            this.ucERPSalesDetails.ServerModify = true;
            this.ucERPSalesDetails.ServerModifyGetMax = false;
            this.ucERPSalesDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalesDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalesDetails.UseTranscationScope = false;
            this.ucERPSalesDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoCustNO
            // 
            this.infoCustNO.CacheConnection = false;
            this.infoCustNO.CommandText = resources.GetString("infoCustNO.CommandText");
            this.infoCustNO.CommandTimeout = 30;
            this.infoCustNO.CommandType = System.Data.CommandType.Text;
            this.infoCustNO.DynamicTableName = false;
            this.infoCustNO.EEPAlias = null;
            this.infoCustNO.EncodingAfter = null;
            this.infoCustNO.EncodingBefore = "Windows-1252";
            this.infoCustNO.EncodingConvert = null;
            this.infoCustNO.InfoConnection = this.InfoConnection1;
            this.infoCustNO.MultiSetWhere = false;
            this.infoCustNO.Name = "infoCustNO";
            this.infoCustNO.NotificationAutoEnlist = false;
            this.infoCustNO.SecExcept = null;
            this.infoCustNO.SecFieldName = null;
            this.infoCustNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustNO.SelectPaging = false;
            this.infoCustNO.SelectTop = 0;
            this.infoCustNO.SiteControl = false;
            this.infoCustNO.SiteFieldName = null;
            this.infoCustNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesMan
            // 
            this.infoSalesMan.CacheConnection = false;
            this.infoSalesMan.CommandText = "select  SalesEmployeeID,SalesID,SalesName+\'-\'+SalesID as SalesName\r\nfrom ERPSales" +
    "Man\r\nwhere IsMedia=1\r\norder by SalesEmployeeID";
            this.infoSalesMan.CommandTimeout = 30;
            this.infoSalesMan.CommandType = System.Data.CommandType.Text;
            this.infoSalesMan.DynamicTableName = false;
            this.infoSalesMan.EEPAlias = null;
            this.infoSalesMan.EncodingAfter = null;
            this.infoSalesMan.EncodingBefore = "Windows-1252";
            this.infoSalesMan.EncodingConvert = null;
            this.infoSalesMan.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "SalseNO";
            this.infoSalesMan.KeyFields.Add(keyItem3);
            this.infoSalesMan.MultiSetWhere = false;
            this.infoSalesMan.Name = "infoSalesMan";
            this.infoSalesMan.NotificationAutoEnlist = false;
            this.infoSalesMan.SecExcept = null;
            this.infoSalesMan.SecFieldName = null;
            this.infoSalesMan.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesMan.SelectPaging = false;
            this.infoSalesMan.SelectTop = 0;
            this.infoSalesMan.SiteControl = false;
            this.infoSalesMan.SiteFieldName = null;
            this.infoSalesMan.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesTypeAll
            // 
            this.infoSalesTypeAll.CacheConnection = false;
            this.infoSalesTypeAll.CommandText = "select \'\' as SalesTypeID,\'==不拘==\' as SalesTypeName\r\nunion all\r\nselect SalesTypeID" +
    ",SalesTypeName\r\nfrom ERPSalesType\r\nwhere isActive=1";
            this.infoSalesTypeAll.CommandTimeout = 30;
            this.infoSalesTypeAll.CommandType = System.Data.CommandType.Text;
            this.infoSalesTypeAll.DynamicTableName = false;
            this.infoSalesTypeAll.EEPAlias = null;
            this.infoSalesTypeAll.EncodingAfter = null;
            this.infoSalesTypeAll.EncodingBefore = "Windows-1252";
            this.infoSalesTypeAll.EncodingConvert = null;
            this.infoSalesTypeAll.InfoConnection = this.InfoConnection1;
            this.infoSalesTypeAll.MultiSetWhere = false;
            this.infoSalesTypeAll.Name = "infoSalesTypeAll";
            this.infoSalesTypeAll.NotificationAutoEnlist = false;
            this.infoSalesTypeAll.SecExcept = null;
            this.infoSalesTypeAll.SecFieldName = null;
            this.infoSalesTypeAll.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesTypeAll.SelectPaging = false;
            this.infoSalesTypeAll.SelectTop = 0;
            this.infoSalesTypeAll.SiteControl = false;
            this.infoSalesTypeAll.SiteFieldName = null;
            this.infoSalesTypeAll.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesManAll
            // 
            this.infoSalesManAll.CacheConnection = false;
            this.infoSalesManAll.CommandText = "select \'\' as SalesEmployeeID,\'\' as SalesID,\'==不拘==\' as SalesName\r\nunion all\r\nsele" +
    "ct  SalesEmployeeID,SalesID,SalesName+\'-\'+SalesID\r\nfrom ERPSalesMan\r\nwhere IsMed" +
    "ia=1\r\norder by SalesEmployeeID";
            this.infoSalesManAll.CommandTimeout = 30;
            this.infoSalesManAll.CommandType = System.Data.CommandType.Text;
            this.infoSalesManAll.DynamicTableName = false;
            this.infoSalesManAll.EEPAlias = null;
            this.infoSalesManAll.EncodingAfter = null;
            this.infoSalesManAll.EncodingBefore = "Windows-1252";
            this.infoSalesManAll.EncodingConvert = null;
            this.infoSalesManAll.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "SalseNO";
            this.infoSalesManAll.KeyFields.Add(keyItem4);
            this.infoSalesManAll.MultiSetWhere = false;
            this.infoSalesManAll.Name = "infoSalesManAll";
            this.infoSalesManAll.NotificationAutoEnlist = false;
            this.infoSalesManAll.SecExcept = null;
            this.infoSalesManAll.SecFieldName = null;
            this.infoSalesManAll.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesManAll.SelectPaging = false;
            this.infoSalesManAll.SelectTop = 0;
            this.infoSalesManAll.SiteControl = false;
            this.infoSalesManAll.SiteFieldName = null;
            this.infoSalesManAll.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesTypeAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesManAll)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesDetails;
        private Srvtools.UpdateComponent ucERPSalesDetails;
        private Srvtools.InfoCommand infoCustNO;
        private Srvtools.InfoCommand infoSalesMan;
        private Srvtools.InfoCommand infoSalesTypeAll;
        private Srvtools.InfoCommand infoSalesManAll;
    }
}
