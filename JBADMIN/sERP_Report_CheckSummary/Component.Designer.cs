namespace sERP_Report_CheckSummary
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CheckDetails = new Srvtools.InfoCommand(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.CheckAccount = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckAccount)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetReportData";
            service1.NonLogin = false;
            service1.ServiceName = "GetReportData";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // CheckDetails
            // 
            this.CheckDetails.CacheConnection = false;
            this.CheckDetails.CommandText = "SELECT top 1 WarrantDate,CustomerID,InsGroupID,AccountID FROM dbo.[CheckDetails]";
            this.CheckDetails.CommandTimeout = 30;
            this.CheckDetails.CommandType = System.Data.CommandType.Text;
            this.CheckDetails.DynamicTableName = false;
            this.CheckDetails.EEPAlias = "JBERP";
            this.CheckDetails.EncodingAfter = null;
            this.CheckDetails.EncodingBefore = "Windows-1252";
            this.CheckDetails.EncodingConvert = null;
            this.CheckDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "WarrantNO";
            keyItem2.KeyName = "ItemNO";
            this.CheckDetails.KeyFields.Add(keyItem1);
            this.CheckDetails.KeyFields.Add(keyItem2);
            this.CheckDetails.MultiSetWhere = false;
            this.CheckDetails.Name = "CheckDetails";
            this.CheckDetails.NotificationAutoEnlist = false;
            this.CheckDetails.SecExcept = null;
            this.CheckDetails.SecFieldName = null;
            this.CheckDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CheckDetails.SelectPaging = false;
            this.CheckDetails.SelectTop = 0;
            this.CheckDetails.SiteControl = false;
            this.CheckDetails.SiteFieldName = null;
            this.CheckDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = "SELECT CustomerID,ShortName FROM [Customer] where CustomerTypeID=\'1\' and Customer" +
    "ID is not null and  ShortName  is not null";
            this.Customer.CommandTimeout = 30;
            this.Customer.CommandType = System.Data.CommandType.Text;
            this.Customer.DynamicTableName = false;
            this.Customer.EEPAlias = "JBERP";
            this.Customer.EncodingAfter = null;
            this.Customer.EncodingBefore = "Windows-1252";
            this.Customer.EncodingConvert = null;
            this.Customer.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CustomerID";
            this.Customer.KeyFields.Add(keyItem3);
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
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "select InsGroupID,ShortName from InsGroup where IsActive=1";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = "JBADMIN";
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.infoConnection2;
            keyItem4.KeyName = "InsGroupID";
            this.InsGroup.KeyFields.Add(keyItem4);
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
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
            // 
            // CheckAccount
            // 
            this.CheckAccount.CacheConnection = false;
            this.CheckAccount.CommandText = "SELECT [CheckAccountID]\r\n      ,[CheckAccountName]\r\n  FROM [JBERP].[dbo].[CheckAc" +
    "count]";
            this.CheckAccount.CommandTimeout = 30;
            this.CheckAccount.CommandType = System.Data.CommandType.Text;
            this.CheckAccount.DynamicTableName = false;
            this.CheckAccount.EEPAlias = "JBERP";
            this.CheckAccount.EncodingAfter = null;
            this.CheckAccount.EncodingBefore = "Windows-1252";
            this.CheckAccount.EncodingConvert = null;
            this.CheckAccount.InfoConnection = this.InfoConnection1;
            this.CheckAccount.MultiSetWhere = false;
            this.CheckAccount.Name = "CheckAccount";
            this.CheckAccount.NotificationAutoEnlist = false;
            this.CheckAccount.SecExcept = null;
            this.CheckAccount.SecFieldName = null;
            this.CheckAccount.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CheckAccount.SelectPaging = false;
            this.CheckAccount.SelectTop = 0;
            this.CheckAccount.SiteControl = false;
            this.CheckAccount.SiteFieldName = null;
            this.CheckAccount.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckAccount)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CheckDetails;
        private Srvtools.InfoCommand Customer;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand CheckAccount;
    }
}
