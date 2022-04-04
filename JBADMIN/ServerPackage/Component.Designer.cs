namespace ServerPackage
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Bank = new Srvtools.InfoCommand(this.components);
            this.ucBank = new Srvtools.UpdateComponent(this.components);
            this.View_Bank = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Bank)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JCS";
            // 
            // Bank
            // 
            this.Bank.CacheConnection = false;
            this.Bank.CommandText = "SELECT dbo.[Bank].* FROM dbo.[Bank]";
            this.Bank.CommandTimeout = 30;
            this.Bank.CommandType = System.Data.CommandType.Text;
            this.Bank.DynamicTableName = false;
            this.Bank.EEPAlias = null;
            this.Bank.EncodingAfter = null;
            this.Bank.EncodingBefore = "Windows-1252";
            this.Bank.EncodingConvert = null;
            this.Bank.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "BankID";
            this.Bank.KeyFields.Add(keyItem1);
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
            // ucBank
            // 
            this.ucBank.AutoTrans = true;
            this.ucBank.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "BankID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "BankNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "BankBranchNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "BankName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "IsRemit";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucBank.FieldAttrs.Add(fieldAttr1);
            this.ucBank.FieldAttrs.Add(fieldAttr2);
            this.ucBank.FieldAttrs.Add(fieldAttr3);
            this.ucBank.FieldAttrs.Add(fieldAttr4);
            this.ucBank.FieldAttrs.Add(fieldAttr5);
            this.ucBank.FieldAttrs.Add(fieldAttr6);
            this.ucBank.FieldAttrs.Add(fieldAttr7);
            this.ucBank.LogInfo = null;
            this.ucBank.Name = null;
            this.ucBank.RowAffectsCheck = true;
            this.ucBank.SelectCmd = this.Bank;
            this.ucBank.SelectCmdForUpdate = null;
            this.ucBank.SendSQLCmd = true;
            this.ucBank.ServerModify = true;
            this.ucBank.ServerModifyGetMax = false;
            this.ucBank.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucBank.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucBank.UseTranscationScope = false;
            this.ucBank.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_Bank
            // 
            this.View_Bank.CacheConnection = false;
            this.View_Bank.CommandText = "SELECT * FROM dbo.[Bank]";
            this.View_Bank.CommandTimeout = 30;
            this.View_Bank.CommandType = System.Data.CommandType.Text;
            this.View_Bank.DynamicTableName = false;
            this.View_Bank.EEPAlias = null;
            this.View_Bank.EncodingAfter = null;
            this.View_Bank.EncodingBefore = "Windows-1252";
            this.View_Bank.EncodingConvert = null;
            this.View_Bank.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "BankID";
            this.View_Bank.KeyFields.Add(keyItem2);
            this.View_Bank.MultiSetWhere = false;
            this.View_Bank.Name = "View_Bank";
            this.View_Bank.NotificationAutoEnlist = false;
            this.View_Bank.SecExcept = null;
            this.View_Bank.SecFieldName = null;
            this.View_Bank.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Bank.SelectPaging = false;
            this.View_Bank.SelectTop = 0;
            this.View_Bank.SiteControl = false;
            this.View_Bank.SiteFieldName = null;
            this.View_Bank.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Bank)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Bank;
        private Srvtools.UpdateComponent ucBank;
        private Srvtools.InfoCommand View_Bank;
    }
}
