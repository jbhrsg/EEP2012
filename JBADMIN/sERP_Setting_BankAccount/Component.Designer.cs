namespace sERP_Setting_BankAccount
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.BankAccount = new Srvtools.InfoCommand(this.components);
            this.ucBankAccount = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BankAccount)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // BankAccount
            // 
            this.BankAccount.CacheConnection = false;
            this.BankAccount.CommandText = "SELECT dbo.[BankAccount].* FROM dbo.[BankAccount] order by AutoKey desc";
            this.BankAccount.CommandTimeout = 30;
            this.BankAccount.CommandType = System.Data.CommandType.Text;
            this.BankAccount.DynamicTableName = false;
            this.BankAccount.EEPAlias = "JBERP";
            this.BankAccount.EncodingAfter = null;
            this.BankAccount.EncodingBefore = "Windows-1252";
            this.BankAccount.EncodingConvert = null;
            this.BankAccount.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AccountID";
            this.BankAccount.KeyFields.Add(keyItem1);
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
            // ucBankAccount
            // 
            this.ucBankAccount.AutoTrans = true;
            this.ucBankAccount.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "AccountID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "AccountName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "BankID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "BankAccount";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AccountM";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AccountS";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = "_username";
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = "_sysdate";
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LastUpdateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr10.DefaultValue = "_username";
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "LastUpdateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr11.DefaultValue = "_sysdate";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            this.ucBankAccount.FieldAttrs.Add(fieldAttr1);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr2);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr3);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr4);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr5);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr6);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr7);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr8);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr9);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr10);
            this.ucBankAccount.FieldAttrs.Add(fieldAttr11);
            this.ucBankAccount.LogInfo = null;
            this.ucBankAccount.Name = "ucBankAccount";
            this.ucBankAccount.RowAffectsCheck = true;
            this.ucBankAccount.SelectCmd = this.BankAccount;
            this.ucBankAccount.SelectCmdForUpdate = null;
            this.ucBankAccount.SendSQLCmd = true;
            this.ucBankAccount.ServerModify = true;
            this.ucBankAccount.ServerModifyGetMax = false;
            this.ucBankAccount.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucBankAccount.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucBankAccount.UseTranscationScope = false;
            this.ucBankAccount.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BankAccount)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand BankAccount;
        private Srvtools.UpdateComponent ucBankAccount;
    }
}
