namespace sERPSalseDetailsImport
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ucCompany = new Srvtools.UpdateComponent(this.components);
            this.infoInvoiceList = new Srvtools.InfoCommand(this.components);
            this.infoSalesType = new Srvtools.InfoCommand(this.components);
            this.infoSalesMan = new Srvtools.InfoCommand(this.components);
            this.infoCustomersAll = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoInvoiceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomersAll)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "ImportSalesFromERPSalesMaster";
            service1.NonLogin = false;
            service1.ServiceName = "ImportSalesFromERPSalesMaster";
            service2.DelegateName = "SalseDetailsAutoExcel";
            service2.NonLogin = false;
            service2.ServiceName = "SalseDetailsAutoExcel";
            service3.DelegateName = "UpdateCustomerInvoiceData";
            service3.NonLogin = false;
            service3.ServiceName = "UpdateCustomerInvoiceData";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ucCompany
            // 
            this.ucCompany.AutoTrans = true;
            this.ucCompany.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CompanyID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CompanyName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateBy";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucCompany.FieldAttrs.Add(fieldAttr1);
            this.ucCompany.FieldAttrs.Add(fieldAttr2);
            this.ucCompany.FieldAttrs.Add(fieldAttr3);
            this.ucCompany.FieldAttrs.Add(fieldAttr4);
            this.ucCompany.LogInfo = null;
            this.ucCompany.Name = "ucCompany";
            this.ucCompany.RowAffectsCheck = true;
            this.ucCompany.SelectCmd = null;
            this.ucCompany.SelectCmdForUpdate = null;
            this.ucCompany.SendSQLCmd = true;
            this.ucCompany.ServerModify = true;
            this.ucCompany.ServerModifyGetMax = false;
            this.ucCompany.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCompany.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCompany.UseTranscationScope = false;
            this.ucCompany.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoInvoiceList
            // 
            this.infoInvoiceList.CacheConnection = false;
            this.infoInvoiceList.CommandText = resources.GetString("infoInvoiceList.CommandText");
            this.infoInvoiceList.CommandTimeout = 30;
            this.infoInvoiceList.CommandType = System.Data.CommandType.Text;
            this.infoInvoiceList.DynamicTableName = false;
            this.infoInvoiceList.EEPAlias = null;
            this.infoInvoiceList.EncodingAfter = null;
            this.infoInvoiceList.EncodingBefore = "Windows-1252";
            this.infoInvoiceList.EncodingConvert = null;
            this.infoInvoiceList.InfoConnection = this.InfoConnection1;
            this.infoInvoiceList.MultiSetWhere = false;
            this.infoInvoiceList.Name = "infoInvoiceList";
            this.infoInvoiceList.NotificationAutoEnlist = false;
            this.infoInvoiceList.SecExcept = null;
            this.infoInvoiceList.SecFieldName = null;
            this.infoInvoiceList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoInvoiceList.SelectPaging = false;
            this.infoInvoiceList.SelectTop = 0;
            this.infoInvoiceList.SiteControl = false;
            this.infoInvoiceList.SiteFieldName = null;
            this.infoInvoiceList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesType
            // 
            this.infoSalesType.CacheConnection = false;
            this.infoSalesType.CommandText = "select SalesTypeID,SalesTypeName\r\nfrom ERPSalesType\r\nwhere SalesTypeID in(\'1\',\'6\'" +
    ",\'31\')";
            this.infoSalesType.CommandTimeout = 30;
            this.infoSalesType.CommandType = System.Data.CommandType.Text;
            this.infoSalesType.DynamicTableName = false;
            this.infoSalesType.EEPAlias = null;
            this.infoSalesType.EncodingAfter = null;
            this.infoSalesType.EncodingBefore = "Windows-1252";
            this.infoSalesType.EncodingConvert = null;
            this.infoSalesType.InfoConnection = this.InfoConnection1;
            this.infoSalesType.MultiSetWhere = false;
            this.infoSalesType.Name = "infoSalesType";
            this.infoSalesType.NotificationAutoEnlist = false;
            this.infoSalesType.SecExcept = null;
            this.infoSalesType.SecFieldName = null;
            this.infoSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesType.SelectPaging = false;
            this.infoSalesType.SelectTop = 0;
            this.infoSalesType.SiteControl = false;
            this.infoSalesType.SiteFieldName = null;
            this.infoSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoSalesMan
            // 
            this.infoSalesMan.CacheConnection = false;
            this.infoSalesMan.CommandText = "select  p.oldSalesID as SalesID,p.SalesName+\'-\'+p.oldSalesID as SalesName\r\nfrom E" +
    "RPSalesMan m\r\n\tinner join JBERP.dbo.SalesPerson p on m.SalesID=p.oldSalesID\r\nwhe" +
    "re m.IsMedia=1\r\norder by p.SalesID";
            this.infoSalesMan.CommandTimeout = 30;
            this.infoSalesMan.CommandType = System.Data.CommandType.Text;
            this.infoSalesMan.DynamicTableName = false;
            this.infoSalesMan.EEPAlias = null;
            this.infoSalesMan.EncodingAfter = null;
            this.infoSalesMan.EncodingBefore = "Windows-1252";
            this.infoSalesMan.EncodingConvert = null;
            this.infoSalesMan.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalseNO";
            this.infoSalesMan.KeyFields.Add(keyItem1);
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
            // infoCustomersAll
            // 
            this.infoCustomersAll.CacheConnection = false;
            this.infoCustomersAll.CommandText = "select top 10 CustNO,SalesID,CustNO+\' : \'+CustShortName as CustShortName\r\nfrom Vi" +
    "ew_ERPCustomers\r\nwhere 1=1\r\norder by CustNO";
            this.infoCustomersAll.CommandTimeout = 30;
            this.infoCustomersAll.CommandType = System.Data.CommandType.Text;
            this.infoCustomersAll.DynamicTableName = false;
            this.infoCustomersAll.EEPAlias = null;
            this.infoCustomersAll.EncodingAfter = null;
            this.infoCustomersAll.EncodingBefore = "Windows-1252";
            this.infoCustomersAll.EncodingConvert = null;
            this.infoCustomersAll.InfoConnection = this.InfoConnection1;
            this.infoCustomersAll.MultiSetWhere = false;
            this.infoCustomersAll.Name = "infoCustomersAll";
            this.infoCustomersAll.NotificationAutoEnlist = false;
            this.infoCustomersAll.SecExcept = null;
            this.infoCustomersAll.SecFieldName = null;
            this.infoCustomersAll.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustomersAll.SelectPaging = false;
            this.infoCustomersAll.SelectTop = 0;
            this.infoCustomersAll.SiteControl = false;
            this.infoCustomersAll.SiteFieldName = null;
            this.infoCustomersAll.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoInvoiceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomersAll)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.UpdateComponent ucCompany;
        private Srvtools.InfoCommand infoInvoiceList;
        private Srvtools.InfoCommand infoSalesType;
        private Srvtools.InfoCommand infoSalesMan;
        private Srvtools.InfoCommand infoCustomersAll;
    }
}
