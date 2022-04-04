namespace sCustomerDealQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            this.ucCustomer = new Srvtools.UpdateComponent(this.components);
            this.View_Customer = new Srvtools.InfoCommand(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            this.SalesTypeDateList = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTypeDateList)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetGridDataSalesTypeDate";
            service1.NonLogin = false;
            service1.ServiceName = "GetGridDataSalesTypeDate";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
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
            // ucCustomer
            // 
            this.ucCustomer.AutoTrans = true;
            this.ucCustomer.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CustomerID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustomerName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ShortName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "TelNO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "TelNOTmp";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "FaxNO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Addr_Country";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Addr_City";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Addr_Desc";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ZIPCode";
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
            fieldAttr12.DataField = "DonateMark";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "NPOBAN";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CustomerTypeID";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "PersonInCharge";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "Employer";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "ARCNO";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "CreateBy";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "CreateDate";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "LastUpdateBy";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "LastUpdateDate";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            this.ucCustomer.FieldAttrs.Add(fieldAttr1);
            this.ucCustomer.FieldAttrs.Add(fieldAttr2);
            this.ucCustomer.FieldAttrs.Add(fieldAttr3);
            this.ucCustomer.FieldAttrs.Add(fieldAttr4);
            this.ucCustomer.FieldAttrs.Add(fieldAttr5);
            this.ucCustomer.FieldAttrs.Add(fieldAttr6);
            this.ucCustomer.FieldAttrs.Add(fieldAttr7);
            this.ucCustomer.FieldAttrs.Add(fieldAttr8);
            this.ucCustomer.FieldAttrs.Add(fieldAttr9);
            this.ucCustomer.FieldAttrs.Add(fieldAttr10);
            this.ucCustomer.FieldAttrs.Add(fieldAttr11);
            this.ucCustomer.FieldAttrs.Add(fieldAttr12);
            this.ucCustomer.FieldAttrs.Add(fieldAttr13);
            this.ucCustomer.FieldAttrs.Add(fieldAttr14);
            this.ucCustomer.FieldAttrs.Add(fieldAttr15);
            this.ucCustomer.FieldAttrs.Add(fieldAttr16);
            this.ucCustomer.FieldAttrs.Add(fieldAttr17);
            this.ucCustomer.FieldAttrs.Add(fieldAttr18);
            this.ucCustomer.FieldAttrs.Add(fieldAttr19);
            this.ucCustomer.FieldAttrs.Add(fieldAttr20);
            this.ucCustomer.FieldAttrs.Add(fieldAttr21);
            this.ucCustomer.LogInfo = null;
            this.ucCustomer.Name = "ucCustomer";
            this.ucCustomer.RowAffectsCheck = true;
            this.ucCustomer.SelectCmd = this.Customer;
            this.ucCustomer.SelectCmdForUpdate = null;
            this.ucCustomer.SendSQLCmd = true;
            this.ucCustomer.ServerModify = true;
            this.ucCustomer.ServerModifyGetMax = false;
            this.ucCustomer.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCustomer.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCustomer.UseTranscationScope = false;
            this.ucCustomer.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_Customer
            // 
            this.View_Customer.CacheConnection = false;
            this.View_Customer.CommandText = "SELECT * FROM dbo.[Customer]";
            this.View_Customer.CommandTimeout = 30;
            this.View_Customer.CommandType = System.Data.CommandType.Text;
            this.View_Customer.DynamicTableName = false;
            this.View_Customer.EEPAlias = null;
            this.View_Customer.EncodingAfter = null;
            this.View_Customer.EncodingBefore = "Windows-1252";
            this.View_Customer.EncodingConvert = null;
            this.View_Customer.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CustomerID";
            this.View_Customer.KeyFields.Add(keyItem2);
            this.View_Customer.MultiSetWhere = false;
            this.View_Customer.Name = "View_Customer";
            this.View_Customer.NotificationAutoEnlist = false;
            this.View_Customer.SecExcept = null;
            this.View_Customer.SecFieldName = null;
            this.View_Customer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Customer.SelectPaging = false;
            this.View_Customer.SelectTop = 0;
            this.View_Customer.SiteControl = false;
            this.View_Customer.SiteFieldName = null;
            this.View_Customer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "Select  SalesTypeID, SalesTypeName from salestype";
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
            // SalesTypeDateList
            // 
            this.SalesTypeDateList.CacheConnection = false;
            this.SalesTypeDateList.CommandText = "SELECT SalesTypeID,SalesDate,0 AS SalesCount\r\n FROM InvoiceDetails WHERE 1=2";
            this.SalesTypeDateList.CommandTimeout = 30;
            this.SalesTypeDateList.CommandType = System.Data.CommandType.Text;
            this.SalesTypeDateList.DynamicTableName = false;
            this.SalesTypeDateList.EEPAlias = "JBERP";
            this.SalesTypeDateList.EncodingAfter = null;
            this.SalesTypeDateList.EncodingBefore = "Windows-1252";
            this.SalesTypeDateList.EncodingConvert = null;
            this.SalesTypeDateList.InfoConnection = this.InfoConnection1;
            this.SalesTypeDateList.MultiSetWhere = false;
            this.SalesTypeDateList.Name = "SalesTypeDateList";
            this.SalesTypeDateList.NotificationAutoEnlist = false;
            this.SalesTypeDateList.SecExcept = null;
            this.SalesTypeDateList.SecFieldName = null;
            this.SalesTypeDateList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesTypeDateList.SelectPaging = false;
            this.SalesTypeDateList.SelectTop = 0;
            this.SalesTypeDateList.SiteControl = false;
            this.SalesTypeDateList.SiteFieldName = null;
            this.SalesTypeDateList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTypeDateList)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Customer;
        private Srvtools.UpdateComponent ucCustomer;
        private Srvtools.InfoCommand View_Customer;
        private Srvtools.InfoCommand SalesType;
        private Srvtools.InfoCommand SalesTypeDateList;
    }
}
