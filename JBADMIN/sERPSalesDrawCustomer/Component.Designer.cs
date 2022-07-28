namespace sERPSalesDrawCustomer
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalesDetails = new Srvtools.InfoCommand(this.components);
            this.infoCustomersAll = new Srvtools.InfoCommand(this.components);
            this.infoSalesMan = new Srvtools.InfoCommand(this.components);
            this.infoERPSalesType = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesDetails = new Srvtools.UpdateComponent(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.InvoiceDetails = new Srvtools.InfoCommand(this.components);
            this.InvoiceYM = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomersAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceYM)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetCustNOData";
            service1.NonLogin = false;
            service1.ServiceName = "GetCustNOData";
            this.serviceManager1.ServiceCollection.Add(service1);
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
            // infoCustomersAll
            // 
            this.infoCustomersAll.CacheConnection = false;
            this.infoCustomersAll.CommandText = resources.GetString("infoCustomersAll.CommandText");
            this.infoCustomersAll.CommandTimeout = 30;
            this.infoCustomersAll.CommandType = System.Data.CommandType.Text;
            this.infoCustomersAll.DynamicTableName = false;
            this.infoCustomersAll.EEPAlias = "JBADMIN";
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
            // infoERPSalesType
            // 
            this.infoERPSalesType.CacheConnection = false;
            this.infoERPSalesType.CommandText = "select SalesTypeNO,SalesTypeID,Cast(SalesTypeID as nvarchar(5))+\' : \'+SalesTypeNa" +
    "me as SalesTypeName from ERPSalesType\r\nwhere IsActive=1\r\n";
            this.infoERPSalesType.CommandTimeout = 30;
            this.infoERPSalesType.CommandType = System.Data.CommandType.Text;
            this.infoERPSalesType.DynamicTableName = false;
            this.infoERPSalesType.EEPAlias = null;
            this.infoERPSalesType.EncodingAfter = null;
            this.infoERPSalesType.EncodingBefore = "Windows-1252";
            this.infoERPSalesType.EncodingConvert = null;
            this.infoERPSalesType.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "SalesTypeNO";
            this.infoERPSalesType.KeyFields.Add(keyItem4);
            this.infoERPSalesType.MultiSetWhere = false;
            this.infoERPSalesType.Name = "infoERPSalesType";
            this.infoERPSalesType.NotificationAutoEnlist = false;
            this.infoERPSalesType.SecExcept = null;
            this.infoERPSalesType.SecFieldName = null;
            this.infoERPSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoERPSalesType.SelectPaging = false;
            this.infoERPSalesType.SelectTop = 0;
            this.infoERPSalesType.SiteControl = false;
            this.infoERPSalesType.SiteFieldName = null;
            this.infoERPSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalesDetails
            // 
            this.ucERPSalesDetails.AutoTrans = true;
            this.ucERPSalesDetails.ExceptJoin = false;
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
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBERP";
            // 
            // InvoiceDetails
            // 
            this.InvoiceDetails.CacheConnection = false;
            this.InvoiceDetails.CommandText = resources.GetString("InvoiceDetails.CommandText");
            this.InvoiceDetails.CommandTimeout = 30;
            this.InvoiceDetails.CommandType = System.Data.CommandType.Text;
            this.InvoiceDetails.DynamicTableName = false;
            this.InvoiceDetails.EEPAlias = "JBERP";
            this.InvoiceDetails.EncodingAfter = null;
            this.InvoiceDetails.EncodingBefore = "Windows-1252";
            this.InvoiceDetails.EncodingConvert = null;
            this.InvoiceDetails.InfoConnection = this.infoConnection2;
            keyItem5.KeyName = "InvoiceNO";
            this.InvoiceDetails.KeyFields.Add(keyItem5);
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
            // InvoiceYM
            // 
            this.InvoiceYM.CacheConnection = false;
            this.InvoiceYM.CommandText = resources.GetString("InvoiceYM.CommandText");
            this.InvoiceYM.CommandTimeout = 30;
            this.InvoiceYM.CommandType = System.Data.CommandType.Text;
            this.InvoiceYM.DynamicTableName = false;
            this.InvoiceYM.EEPAlias = null;
            this.InvoiceYM.EncodingAfter = null;
            this.InvoiceYM.EncodingBefore = "Windows-1252";
            this.InvoiceYM.EncodingConvert = null;
            this.InvoiceYM.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "SalesMasterNO";
            keyItem7.KeyName = "ItemSeq";
            this.InvoiceYM.KeyFields.Add(keyItem6);
            this.InvoiceYM.KeyFields.Add(keyItem7);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomersAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceYM)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesDetails;
        private Srvtools.InfoCommand infoCustomersAll;
        private Srvtools.InfoCommand infoSalesMan;
        private Srvtools.InfoCommand infoERPSalesType;
        private Srvtools.UpdateComponent ucERPSalesDetails;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand InvoiceDetails;
        private Srvtools.InfoCommand InvoiceYM;

    }
}
