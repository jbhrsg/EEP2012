﻿namespace sERPSalesApply2
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
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.Service service1 = new Srvtools.Service();
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.Service service7 = new Srvtools.Service();
            Srvtools.Service service8 = new Srvtools.Service();
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
            this.idSalesMaster_SalesDetails = new Srvtools.InfoDataSource(this.components);
            this.SalesDetails = new Srvtools.InfoCommand(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SalesMaster = new Srvtools.InfoCommand(this.components);
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.ucSalesMaster = new Srvtools.UpdateComponent(this.components);
            this.ucSalesDetails = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMaster)).BeginInit();
            // 
            // idSalesMaster_SalesDetails
            // 
            this.idSalesMaster_SalesDetails.Detail = this.SalesDetails;
            columnItem1.FieldName = "SalesNO";
            this.idSalesMaster_SalesDetails.DetailColumns.Add(columnItem1);
            this.idSalesMaster_SalesDetails.DynamicTableName = false;
            this.idSalesMaster_SalesDetails.Master = this.SalesMaster;
            columnItem2.FieldName = "SalesNO";
            this.idSalesMaster_SalesDetails.MasterColumns.Add(columnItem2);
            // 
            // SalesDetails
            // 
            this.SalesDetails.CacheConnection = false;
            this.SalesDetails.CommandText = "SELECT dbo.[SalesDetails].*\r\n FROM dbo.[SalesDetails]";
            this.SalesDetails.CommandTimeout = 30;
            this.SalesDetails.CommandType = System.Data.CommandType.Text;
            this.SalesDetails.DynamicTableName = false;
            this.SalesDetails.EEPAlias = "JBERP";
            this.SalesDetails.EncodingAfter = null;
            this.SalesDetails.EncodingBefore = "Windows-1252";
            this.SalesDetails.EncodingConvert = null;
            this.SalesDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesNO";
            keyItem2.KeyName = "ItemNO";
            this.SalesDetails.KeyFields.Add(keyItem1);
            this.SalesDetails.KeyFields.Add(keyItem2);
            this.SalesDetails.MultiSetWhere = false;
            this.SalesDetails.Name = "SalesDetails";
            this.SalesDetails.NotificationAutoEnlist = false;
            this.SalesDetails.SecExcept = null;
            this.SalesDetails.SecFieldName = null;
            this.SalesDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesDetails.SelectPaging = false;
            this.SalesDetails.SelectTop = 0;
            this.SalesDetails.SiteControl = false;
            this.SalesDetails.SiteFieldName = null;
            this.SalesDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // SalesMaster
            // 
            this.SalesMaster.CacheConnection = false;
            this.SalesMaster.CommandText = "select dbo.SalesMaster.* from dbo.SalesMaster";
            this.SalesMaster.CommandTimeout = 30;
            this.SalesMaster.CommandType = System.Data.CommandType.Text;
            this.SalesMaster.DynamicTableName = false;
            this.SalesMaster.EEPAlias = "JBERP";
            this.SalesMaster.EncodingAfter = null;
            this.SalesMaster.EncodingBefore = "Windows-1252";
            this.SalesMaster.EncodingConvert = null;
            this.SalesMaster.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "SalesNO";
            this.SalesMaster.KeyFields.Add(keyItem3);
            this.SalesMaster.MultiSetWhere = false;
            this.SalesMaster.Name = "SalesMaster";
            this.SalesMaster.NotificationAutoEnlist = false;
            this.SalesMaster.SecExcept = null;
            this.SalesMaster.SecFieldName = null;
            this.SalesMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesMaster.SelectPaging = false;
            this.SalesMaster.SelectTop = 0;
            this.SalesMaster.SiteControl = false;
            this.SalesMaster.SiteFieldName = null;
            this.SalesMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetSalesNO";
            service1.NonLogin = false;
            service1.ServiceName = "GetSalesNO";
            service2.DelegateName = "procCallApi_Create";
            service2.NonLogin = false;
            service2.ServiceName = "procCallApi_Create";
            service3.DelegateName = "procCallApi_Cancel";
            service3.NonLogin = false;
            service3.ServiceName = "procCallApi_Cancel";
            service4.DelegateName = "procCreateReceipt";
            service4.NonLogin = false;
            service4.ServiceName = "procCreateReceipt";
            service5.DelegateName = "selectCustomerSaleType";
            service5.NonLogin = false;
            service5.ServiceName = "selectCustomerSaleType";
            service6.DelegateName = "updateSalesMasterIsActive";
            service6.NonLogin = false;
            service6.ServiceName = "updateSalesMasterIsActive";
            service7.DelegateName = "procDeleteSales";
            service7.NonLogin = false;
            service7.ServiceName = "procDeleteSales";
            service8.DelegateName = "BatchAddForDorm";
            service8.NonLogin = false;
            service8.ServiceName = "BatchAddForDorm";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            // 
            // ucSalesMaster
            // 
            this.ucSalesMaster.AutoTrans = true;
            this.ucSalesMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalesNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustomerID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesDate";
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
            fieldAttr5.DataField = "DonateMarkID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "InvoiceType";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CarrierType";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CarrierID1";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CarrierID2";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "NPOBAN";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "TaxType";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "TaxRate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "PayWayID";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "Remark";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "MailSend";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "InsGroupID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "SalesTypeID";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "IsPutInvoice";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "Employer";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CreateBy";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = "_username";
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CreateDate";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = "_sysdate";
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "LastUpdateBy";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr22.DefaultValue = "_username";
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "LastUpdateDate";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr23.DefaultValue = "_sysdate";
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "SalesNOTemp";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "IsOutPutDetails";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "FlowFlag";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = "Z";
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr1);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr2);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr3);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr4);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr5);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr6);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr7);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr8);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr9);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr10);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr11);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr12);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr13);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr14);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr15);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr16);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr17);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr18);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr19);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr20);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr21);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr22);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr23);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr24);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr25);
            this.ucSalesMaster.FieldAttrs.Add(fieldAttr26);
            this.ucSalesMaster.LogInfo = null;
            this.ucSalesMaster.Name = "ucSalesMaster";
            this.ucSalesMaster.RowAffectsCheck = true;
            this.ucSalesMaster.SelectCmd = this.SalesMaster;
            this.ucSalesMaster.SelectCmdForUpdate = null;
            this.ucSalesMaster.SendSQLCmd = true;
            this.ucSalesMaster.ServerModify = true;
            this.ucSalesMaster.ServerModifyGetMax = false;
            this.ucSalesMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesMaster.UseTranscationScope = false;
            this.ucSalesMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ucSalesDetails
            // 
            this.ucSalesDetails.AutoTrans = true;
            this.ucSalesDetails.ExceptJoin = false;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "SalesNO";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "ItemNO";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "SalesTypeID";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "FeeItemID";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "SalesTypeName";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "Quantity";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "Unit";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            fieldAttr34.CheckNull = false;
            fieldAttr34.DataField = "UnitPrice";
            fieldAttr34.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr34.DefaultValue = null;
            fieldAttr34.TrimLength = 0;
            fieldAttr34.UpdateEnable = true;
            fieldAttr34.WhereMode = true;
            fieldAttr35.CheckNull = false;
            fieldAttr35.DataField = "DType";
            fieldAttr35.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr35.DefaultValue = null;
            fieldAttr35.TrimLength = 0;
            fieldAttr35.UpdateEnable = true;
            fieldAttr35.WhereMode = true;
            fieldAttr36.CheckNull = false;
            fieldAttr36.DataField = "CreateBy";
            fieldAttr36.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr36.DefaultValue = "_username";
            fieldAttr36.TrimLength = 0;
            fieldAttr36.UpdateEnable = true;
            fieldAttr36.WhereMode = true;
            fieldAttr37.CheckNull = false;
            fieldAttr37.DataField = "CreateDate";
            fieldAttr37.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr37.DefaultValue = "_sysdate";
            fieldAttr37.TrimLength = 0;
            fieldAttr37.UpdateEnable = true;
            fieldAttr37.WhereMode = true;
            fieldAttr38.CheckNull = false;
            fieldAttr38.DataField = "LastUpdateBy";
            fieldAttr38.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr38.DefaultValue = "_username";
            fieldAttr38.TrimLength = 0;
            fieldAttr38.UpdateEnable = true;
            fieldAttr38.WhereMode = true;
            fieldAttr39.CheckNull = false;
            fieldAttr39.DataField = "LastUpdateDate";
            fieldAttr39.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr39.DefaultValue = "_sysdate";
            fieldAttr39.TrimLength = 0;
            fieldAttr39.UpdateEnable = true;
            fieldAttr39.WhereMode = true;
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr27);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr28);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr29);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr30);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr31);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr32);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr33);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr34);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr35);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr36);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr37);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr38);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr39);
            this.ucSalesDetails.LogInfo = null;
            this.ucSalesDetails.Name = "ucSalesDetails";
            this.ucSalesDetails.RowAffectsCheck = true;
            this.ucSalesDetails.SelectCmd = this.SalesDetails;
            this.ucSalesDetails.SelectCmdForUpdate = null;
            this.ucSalesDetails.SendSQLCmd = true;
            this.ucSalesDetails.ServerModify = true;
            this.ucSalesDetails.ServerModifyGetMax = false;
            this.ucSalesDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesDetails.UseTranscationScope = false;
            this.ucSalesDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.SalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMaster)).EndInit();

        }

        #endregion

        private Srvtools.InfoDataSource idSalesMaster_SalesDetails;
        private Srvtools.InfoCommand SalesDetails;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesMaster;
        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.UpdateComponent ucSalesMaster;
        private Srvtools.UpdateComponent ucSalesDetails;

    }
}
