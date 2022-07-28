namespace sERP_Normal_CheckDetailsAction
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CheckDetails = new Srvtools.InfoCommand(this.components);
            this.ucCheckDetails = new Srvtools.UpdateComponent(this.components);
            this.Customer = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.Bank = new Srvtools.InfoCommand(this.components);
            this.BankAccount = new Srvtools.InfoCommand(this.components);
            this.Action = new Srvtools.InfoCommand(this.components);
            this.CheckAccount = new Srvtools.InfoCommand(this.components);
            this.Customers = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BankAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Action)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "UpdateCheckDetails_Trust";
            service1.NonLogin = false;
            service1.ServiceName = "UpdateCheckDetails_Trust";
            service2.DelegateName = "UpdateCheckDetails_Cash";
            service2.NonLogin = false;
            service2.ServiceName = "UpdateCheckDetails_Cash";
            service3.DelegateName = "UpdateCheckDetails_Return";
            service3.NonLogin = false;
            service3.ServiceName = "UpdateCheckDetails_Return";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
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
            // ucCheckDetails
            // 
            this.ucCheckDetails.AutoTrans = true;
            this.ucCheckDetails.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "WarrantNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ItemNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "InsGroupID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "WarrantDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CheckNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CheckDueDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Amount";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "BankRootID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "BankBranchID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Bourse";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "BankID";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CheckAccount";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "AccountID";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CustomerID";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "TrustDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "TrustAccountID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CashDate";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "ReturnDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "ReturnNotes";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CreateBy";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CreateDate";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "LastUpdateBy";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr22.DefaultValue = "_username";
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "LastUpdateDate";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr23.DefaultValue = "_sysdate";
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr1);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr2);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr3);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr4);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr5);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr6);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr7);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr8);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr9);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr10);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr11);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr12);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr13);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr14);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr15);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr16);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr17);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr18);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr19);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr20);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr21);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr22);
            this.ucCheckDetails.FieldAttrs.Add(fieldAttr23);
            this.ucCheckDetails.LogInfo = null;
            this.ucCheckDetails.Name = "ucCheckDetails";
            this.ucCheckDetails.RowAffectsCheck = true;
            this.ucCheckDetails.SelectCmd = this.CheckDetails;
            this.ucCheckDetails.SelectCmdForUpdate = null;
            this.ucCheckDetails.SendSQLCmd = true;
            this.ucCheckDetails.ServerModify = true;
            this.ucCheckDetails.ServerModifyGetMax = false;
            this.ucCheckDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCheckDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCheckDetails.UseTranscationScope = false;
            this.ucCheckDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // Customer
            // 
            this.Customer.CacheConnection = false;
            this.Customer.CommandText = "select top 20 CustomerID,CustomerName,ShortName from Customer\r\nwhere CustomerType" +
    "ID=\'1\'";
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
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
            // 
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "select InsGroupID,InsGroupName,ShortName from InsGroup where IsActive=1";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = "JBADMIN";
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.infoConnection2;
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
            // Bank
            // 
            this.Bank.CacheConnection = false;
            this.Bank.CommandText = "SELECT *\r\n  FROM JBADMIN.dbo.[Bank] where [BankBranchNO] is not null and [BankBra" +
    "nchNO] !=\'\'";
            this.Bank.CommandTimeout = 30;
            this.Bank.CommandType = System.Data.CommandType.Text;
            this.Bank.DynamicTableName = false;
            this.Bank.EEPAlias = "JBADMIN";
            this.Bank.EncodingAfter = null;
            this.Bank.EncodingBefore = "Windows-1252";
            this.Bank.EncodingConvert = null;
            this.Bank.InfoConnection = this.InfoConnection1;
            this.Bank.MultiSetWhere = false;
            this.Bank.Name = "Bank";
            this.Bank.NotificationAutoEnlist = false;
            this.Bank.SecExcept = null;
            this.Bank.SecFieldName = null;
            this.Bank.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Bank.SelectPaging = false;
            this.Bank.SelectTop = 0;
            this.Bank.SiteControl = false;
            this.Bank.SiteFieldName = null;
            this.Bank.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // BankAccount
            // 
            this.BankAccount.CacheConnection = false;
            this.BankAccount.CommandText = "SELECT \r\n      [AccountID]\r\n      ,[AccountName]\r\n      ,[BankID]\r\n      ,[BankAc" +
    "count]\r\n  FROM  [BankAccount]";
            this.BankAccount.CommandTimeout = 30;
            this.BankAccount.CommandType = System.Data.CommandType.Text;
            this.BankAccount.DynamicTableName = false;
            this.BankAccount.EEPAlias = "JBERP";
            this.BankAccount.EncodingAfter = null;
            this.BankAccount.EncodingBefore = "Windows-1252";
            this.BankAccount.EncodingConvert = null;
            this.BankAccount.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AutoKey";
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
            // Action
            // 
            this.Action.CacheConnection = false;
            this.Action.CommandText = "select 0 as ActionCode,\'可託收\' as ActionName\r\nunion\r\nselect 1 as ActionCode,\'可兌現\' a" +
    "s ActionName\r\nunion\r\nselect 2 as ActionCode,\'可退票\' as ActionName\r\nunion\r\nselect 3" +
    " as ActionCode,\'已退票\' as ActionName";
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
            // 
            // CheckAccount
            // 
            this.CheckAccount.CacheConnection = false;
            this.CheckAccount.CommandText = "SELECT  [CheckAccountID]\r\n      ,[CheckAccountName]\r\n  FROM CheckAccount";
            this.CheckAccount.CommandTimeout = 30;
            this.CheckAccount.CommandType = System.Data.CommandType.Text;
            this.CheckAccount.DynamicTableName = false;
            this.CheckAccount.EEPAlias = "JBERP";
            this.CheckAccount.EncodingAfter = null;
            this.CheckAccount.EncodingBefore = "Windows-1252";
            this.CheckAccount.EncodingConvert = null;
            this.CheckAccount.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "AutoKey";
            this.CheckAccount.KeyFields.Add(keyItem5);
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
            // Customers
            // 
            this.Customers.CacheConnection = false;
            this.Customers.CommandText = "select CustomerID,CustomerName,ShortName from Customer\r\n";
            this.Customers.CommandTimeout = 30;
            this.Customers.CommandType = System.Data.CommandType.Text;
            this.Customers.DynamicTableName = false;
            this.Customers.EEPAlias = "JBERP";
            this.Customers.EncodingAfter = null;
            this.Customers.EncodingBefore = "Windows-1252";
            this.Customers.EncodingConvert = null;
            this.Customers.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "CustomerID";
            this.Customers.KeyFields.Add(keyItem6);
            this.Customers.MultiSetWhere = false;
            this.Customers.Name = "Customers";
            this.Customers.NotificationAutoEnlist = false;
            this.Customers.SecExcept = null;
            this.Customers.SecFieldName = null;
            this.Customers.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Customers.SelectPaging = false;
            this.Customers.SelectTop = 0;
            this.Customers.SiteControl = false;
            this.Customers.SiteFieldName = null;
            this.Customers.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BankAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Action)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CheckDetails;
        private Srvtools.UpdateComponent ucCheckDetails;
        private Srvtools.InfoCommand Customer;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoCommand Bank;
        private Srvtools.InfoCommand BankAccount;
        private Srvtools.InfoCommand Action;
        private Srvtools.InfoCommand CheckAccount;
        private Srvtools.InfoCommand Customers;
    }
}
