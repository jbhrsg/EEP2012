namespace sRVoucherList
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
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.Service service7 = new Srvtools.Service();
            Srvtools.Service service8 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glVoucherList = new Srvtools.InfoCommand(this.components);
            this.infoglVoucherType = new Srvtools.InfoCommand(this.components);
            this.infoglCostCenter = new Srvtools.InfoCommand(this.components);
            this.infoglVoucherType2 = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glVoucherList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglVoucherType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglCostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglVoucherType2)).BeginInit();
            // 
            // serviceManager1
            // 
            service5.DelegateName = "ReportglVoucherList";
            service5.NonLogin = false;
            service5.ServiceName = "ReportglVoucherList";
            service6.DelegateName = "ReportProfitList";
            service6.NonLogin = false;
            service6.ServiceName = "ReportProfitList";
            service7.DelegateName = "ReportAssetDebt";
            service7.NonLogin = false;
            service7.ServiceName = "ReportAssetDebt";
            service8.DelegateName = "ReportEstimateProfit";
            service8.NonLogin = false;
            service8.ServiceName = "ReportEstimateProfit";
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // glVoucherList
            // 
            this.glVoucherList.CacheConnection = false;
            this.glVoucherList.CommandText = resources.GetString("glVoucherList.CommandText");
            this.glVoucherList.CommandTimeout = 30;
            this.glVoucherList.CommandType = System.Data.CommandType.Text;
            this.glVoucherList.DynamicTableName = false;
            this.glVoucherList.EEPAlias = "";
            this.glVoucherList.EncodingAfter = null;
            this.glVoucherList.EncodingBefore = "Windows-1252";
            this.glVoucherList.EncodingConvert = null;
            this.glVoucherList.InfoConnection = this.InfoConnection1;
            this.glVoucherList.MultiSetWhere = false;
            this.glVoucherList.Name = "glVoucherList";
            this.glVoucherList.NotificationAutoEnlist = false;
            this.glVoucherList.SecExcept = null;
            this.glVoucherList.SecFieldName = null;
            this.glVoucherList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glVoucherList.SelectPaging = false;
            this.glVoucherList.SelectTop = 0;
            this.glVoucherList.SiteControl = false;
            this.glVoucherList.SiteFieldName = null;
            this.glVoucherList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoglVoucherType
            // 
            this.infoglVoucherType.CacheConnection = false;
            this.infoglVoucherType.CommandText = "SELECT distinct VoucherType\r\nFROM glVoucherTypeUnion\r\n\r\n\r\n\r\n";
            this.infoglVoucherType.CommandTimeout = 30;
            this.infoglVoucherType.CommandType = System.Data.CommandType.Text;
            this.infoglVoucherType.DynamicTableName = false;
            this.infoglVoucherType.EEPAlias = "";
            this.infoglVoucherType.EncodingAfter = null;
            this.infoglVoucherType.EncodingBefore = "Windows-1252";
            this.infoglVoucherType.EncodingConvert = null;
            this.infoglVoucherType.InfoConnection = this.InfoConnection1;
            this.infoglVoucherType.MultiSetWhere = false;
            this.infoglVoucherType.Name = "infoglVoucherType";
            this.infoglVoucherType.NotificationAutoEnlist = false;
            this.infoglVoucherType.SecExcept = null;
            this.infoglVoucherType.SecFieldName = null;
            this.infoglVoucherType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoglVoucherType.SelectPaging = false;
            this.infoglVoucherType.SelectTop = 0;
            this.infoglVoucherType.SiteControl = false;
            this.infoglVoucherType.SiteFieldName = null;
            this.infoglVoucherType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoglCostCenter
            // 
            this.infoglCostCenter.CacheConnection = false;
            this.infoglCostCenter.CommandText = "select 0 as AutoKey,\'\' as CostCenterID,\'==不拘==\' as CostCenterName\r\nunion all\r\nSEL" +
    "ECT AutoKey,CostCenterID,CostCenterName\r\nFROM glCostCenter\r\n";
            this.infoglCostCenter.CommandTimeout = 30;
            this.infoglCostCenter.CommandType = System.Data.CommandType.Text;
            this.infoglCostCenter.DynamicTableName = false;
            this.infoglCostCenter.EEPAlias = "";
            this.infoglCostCenter.EncodingAfter = null;
            this.infoglCostCenter.EncodingBefore = "Windows-1252";
            this.infoglCostCenter.EncodingConvert = null;
            this.infoglCostCenter.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.infoglCostCenter.KeyFields.Add(keyItem2);
            this.infoglCostCenter.MultiSetWhere = false;
            this.infoglCostCenter.Name = "infoglCostCenter";
            this.infoglCostCenter.NotificationAutoEnlist = false;
            this.infoglCostCenter.SecExcept = null;
            this.infoglCostCenter.SecFieldName = null;
            this.infoglCostCenter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoglCostCenter.SelectPaging = false;
            this.infoglCostCenter.SelectTop = 0;
            this.infoglCostCenter.SiteControl = false;
            this.infoglCostCenter.SiteFieldName = null;
            this.infoglCostCenter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoglVoucherType2
            // 
            this.infoglVoucherType2.CacheConnection = false;
            this.infoglVoucherType2.CommandText = "SELECT distinct VoucherType as VoucherType\r\nFROM glVoucherTypeUnion\r\nunion all\r\ns" +
    "elect \'A+B\'\r\n\r\n\r\n\r\n";
            this.infoglVoucherType2.CommandTimeout = 30;
            this.infoglVoucherType2.CommandType = System.Data.CommandType.Text;
            this.infoglVoucherType2.DynamicTableName = false;
            this.infoglVoucherType2.EEPAlias = "";
            this.infoglVoucherType2.EncodingAfter = null;
            this.infoglVoucherType2.EncodingBefore = "Windows-1252";
            this.infoglVoucherType2.EncodingConvert = null;
            this.infoglVoucherType2.InfoConnection = this.InfoConnection1;
            this.infoglVoucherType2.MultiSetWhere = false;
            this.infoglVoucherType2.Name = "infoglVoucherType2";
            this.infoglVoucherType2.NotificationAutoEnlist = false;
            this.infoglVoucherType2.SecExcept = null;
            this.infoglVoucherType2.SecFieldName = null;
            this.infoglVoucherType2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoglVoucherType2.SelectPaging = false;
            this.infoglVoucherType2.SelectTop = 0;
            this.infoglVoucherType2.SiteControl = false;
            this.infoglVoucherType2.SiteFieldName = null;
            this.infoglVoucherType2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glVoucherList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglVoucherType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglCostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglVoucherType2)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glVoucherList;
        private Srvtools.InfoCommand infoglVoucherType;
        private Srvtools.InfoCommand infoglCostCenter;
        private Srvtools.InfoCommand infoglVoucherType2;
    }
}
