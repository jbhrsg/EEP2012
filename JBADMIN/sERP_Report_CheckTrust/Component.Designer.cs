namespace sERP_Report_CheckTrust
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CheckDetails = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.BankAccount = new Srvtools.InfoCommand(this.components);
            this.CheckAccount = new Srvtools.InfoCommand(this.components);
            this.Action = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BankAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Action)).BeginInit();
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
            this.CheckDetails.CommandText = resources.GetString("CheckDetails.CommandText");
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
            keyItem3.KeyName = "InsGroupID";
            this.InsGroup.KeyFields.Add(keyItem3);
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
            // BankAccount
            // 
            this.BankAccount.CacheConnection = false;
            this.BankAccount.CommandText = "select AccountID,AccountName from BankAccount";
            this.BankAccount.CommandTimeout = 30;
            this.BankAccount.CommandType = System.Data.CommandType.Text;
            this.BankAccount.DynamicTableName = false;
            this.BankAccount.EEPAlias = "JBERP";
            this.BankAccount.EncodingAfter = null;
            this.BankAccount.EncodingBefore = "Windows-1252";
            this.BankAccount.EncodingConvert = null;
            this.BankAccount.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AccountID";
            this.BankAccount.KeyFields.Add(keyItem4);
            this.BankAccount.MultiSetWhere = false;
            this.BankAccount.Name = "BankAccount";
            this.BankAccount.NotificationAutoEnlist = false;
            this.BankAccount.SecExcept = null;
            this.BankAccount.SecFieldName = null;
            this.BankAccount.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.BankAccount.SelectPaging = false;
            this.BankAccount.SelectTop = 0;
            this.BankAccount.SiteControl = false;
            this.BankAccount.SiteFieldName = null;
            this.BankAccount.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CheckAccount
            // 
            this.CheckAccount.CacheConnection = false;
            this.CheckAccount.CommandText = "select CheckAccount.CheckAccountID,CheckAccount.CheckAccountName from CheckAccoun" +
    "t";
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
            // 
            // Action
            // 
            this.Action.CacheConnection = false;
            this.Action.CommandText = resources.GetString("Action.CommandText");
            this.Action.CommandTimeout = 30;
            this.Action.CommandType = System.Data.CommandType.Text;
            this.Action.DynamicTableName = false;
            this.Action.EEPAlias = null;
            this.Action.EncodingAfter = null;
            this.Action.EncodingBefore = "Windows-1252";
            this.Action.EncodingConvert = null;
            this.Action.InfoConnection = this.InfoConnection1;
            this.Action.MultiSetWhere = false;
            this.Action.Name = "Action";
            this.Action.NotificationAutoEnlist = false;
            this.Action.SecExcept = null;
            this.Action.SecFieldName = null;
            this.Action.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Action.SelectPaging = false;
            this.Action.SelectTop = 0;
            this.Action.SiteControl = false;
            this.Action.SiteFieldName = null;
            this.Action.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BankAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Action)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CheckDetails;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand BankAccount;
        private Srvtools.InfoCommand CheckAccount;
        private Srvtools.InfoCommand Action;
    }
}
