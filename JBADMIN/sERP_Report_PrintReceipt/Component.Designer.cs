namespace sERP_Report_PrintReceipt
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.InvoiceDetails = new Srvtools.InfoCommand(this.components);
            this.ucInvoiceDetails = new Srvtools.UpdateComponent(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            this.InvoiceNO = new Srvtools.InfoCommand(this.components);
            this.SalesPerson = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPerson)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetReceiptFromInvoiceDetails";
            service1.NonLogin = false;
            service1.ServiceName = "GetReceiptFromInvoiceDetails";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // InvoiceDetails
            // 
            this.InvoiceDetails.CacheConnection = false;
            this.InvoiceDetails.CommandText = "SELECT top 1 \'\' as InvoiceNO,\'\' as CustomerID,\'\' as InvoiceDate,\'\' as SalesTypeID" +
    ",\'\' as SalesID from InvoiceDetails";
            this.InvoiceDetails.CommandTimeout = 30;
            this.InvoiceDetails.CommandType = System.Data.CommandType.Text;
            this.InvoiceDetails.DynamicTableName = false;
            this.InvoiceDetails.EEPAlias = "JBERP";
            this.InvoiceDetails.EncodingAfter = null;
            this.InvoiceDetails.EncodingBefore = "Windows-1252";
            this.InvoiceDetails.EncodingConvert = null;
            this.InvoiceDetails.InfoConnection = this.InfoConnection1;
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
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = "SELECT CustomerID,ShortName,TelNO FROM [Customer] where CustomerID is not null an" +
    "d  ShortName  is not null";
            this.Customer.CommandTimeout = 30;
            this.Customer.CommandType = System.Data.CommandType.Text;
            this.Customer.DynamicTableName = false;
            this.Customer.EEPAlias = "JBERP";
            this.Customer.EncodingAfter = null;
            this.Customer.EncodingBefore = "Windows-1252";
            this.Customer.EncodingConvert = null;
            this.Customer.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CustomerID";
            this.Customer.KeyFields.Add(keyItem1);
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
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "select SalesType.SalesTypeID,SalesType.SalesTypeName from SalesType where SalesTy" +
    "peID in(\'1\',\'31\',\'6\')  order by SalesTypeID";
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = "JBERP";
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection1;
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
            // InvoiceNO
            // 
            this.InvoiceNO.CacheConnection = false;
            this.InvoiceNO.CommandText = "SELECT top 30  InvoiceNO from InvoiceDetails where IsActive=1 and QInvoiceType=\'9" +
    "7\'";
            this.InvoiceNO.CommandTimeout = 30;
            this.InvoiceNO.CommandType = System.Data.CommandType.Text;
            this.InvoiceNO.DynamicTableName = false;
            this.InvoiceNO.EEPAlias = "JBERP";
            this.InvoiceNO.EncodingAfter = null;
            this.InvoiceNO.EncodingBefore = "Windows-1252";
            this.InvoiceNO.EncodingConvert = null;
            this.InvoiceNO.InfoConnection = this.InfoConnection1;
            this.InvoiceNO.MultiSetWhere = false;
            this.InvoiceNO.Name = "InvoiceNO";
            this.InvoiceNO.NotificationAutoEnlist = false;
            this.InvoiceNO.SecExcept = null;
            this.InvoiceNO.SecFieldName = null;
            this.InvoiceNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InvoiceNO.SelectPaging = false;
            this.InvoiceNO.SelectTop = 0;
            this.InvoiceNO.SiteControl = false;
            this.InvoiceNO.SiteFieldName = null;
            this.InvoiceNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesPerson
            // 
            this.SalesPerson.CacheConnection = false;
            this.SalesPerson.CommandText = "select SalesPerson.SalesID,SalesPerson.SalesName from SalesPerson";
            this.SalesPerson.CommandTimeout = 30;
            this.SalesPerson.CommandType = System.Data.CommandType.Text;
            this.SalesPerson.DynamicTableName = false;
            this.SalesPerson.EEPAlias = "JBERP";
            this.SalesPerson.EncodingAfter = null;
            this.SalesPerson.EncodingBefore = "Windows-1252";
            this.SalesPerson.EncodingConvert = null;
            this.SalesPerson.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SalesID";
            this.SalesPerson.KeyFields.Add(keyItem2);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesPerson)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand InvoiceDetails;
        private Srvtools.UpdateComponent ucInvoiceDetails;
        private Srvtools.InfoCommand Customer;
        private Srvtools.InfoCommand SalesType;
        private Srvtools.InfoCommand InvoiceNO;
        private Srvtools.InfoCommand SalesPerson;
    }
}
