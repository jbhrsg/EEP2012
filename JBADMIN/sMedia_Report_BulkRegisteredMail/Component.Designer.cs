namespace sMedia_Report_BulkRegisteredMail
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.BulkRegisteredMail = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.ucBulkRegisteredMail = new Srvtools.UpdateComponent(this.components);
            this.ERPCustomers2 = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BulkRegisteredMail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomers2)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetAllData";
            service1.NonLogin = false;
            service1.ServiceName = "GetAllData";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // BulkRegisteredMail
            // 
            this.BulkRegisteredMail.CacheConnection = false;
            this.BulkRegisteredMail.CommandText = "SELECT * FROM dbo.[BulkRegisteredMail]";
            this.BulkRegisteredMail.CommandTimeout = 30;
            this.BulkRegisteredMail.CommandType = System.Data.CommandType.Text;
            this.BulkRegisteredMail.DynamicTableName = false;
            this.BulkRegisteredMail.EEPAlias = "JBADMIN";
            this.BulkRegisteredMail.EncodingAfter = null;
            this.BulkRegisteredMail.EncodingBefore = "Windows-1252";
            this.BulkRegisteredMail.EncodingConvert = null;
            this.BulkRegisteredMail.InfoConnection = this.infoConnection2;
            keyItem1.KeyName = "CustNO";
            this.BulkRegisteredMail.KeyFields.Add(keyItem1);
            this.BulkRegisteredMail.MultiSetWhere = false;
            this.BulkRegisteredMail.Name = "BulkRegisteredMail";
            this.BulkRegisteredMail.NotificationAutoEnlist = false;
            this.BulkRegisteredMail.SecExcept = null;
            this.BulkRegisteredMail.SecFieldName = null;
            this.BulkRegisteredMail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.BulkRegisteredMail.SelectPaging = false;
            this.BulkRegisteredMail.SelectTop = 0;
            this.BulkRegisteredMail.SiteControl = false;
            this.BulkRegisteredMail.SiteFieldName = null;
            this.BulkRegisteredMail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
            // 
            // ucBulkRegisteredMail
            // 
            this.ucBulkRegisteredMail.AutoTrans = true;
            this.ucBulkRegisteredMail.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CustNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustShortName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CustAddr";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            this.ucBulkRegisteredMail.FieldAttrs.Add(fieldAttr1);
            this.ucBulkRegisteredMail.FieldAttrs.Add(fieldAttr2);
            this.ucBulkRegisteredMail.FieldAttrs.Add(fieldAttr3);
            this.ucBulkRegisteredMail.LogInfo = null;
            this.ucBulkRegisteredMail.Name = "ucBulkRegisteredMail";
            this.ucBulkRegisteredMail.RowAffectsCheck = true;
            this.ucBulkRegisteredMail.SelectCmd = this.BulkRegisteredMail;
            this.ucBulkRegisteredMail.SelectCmdForUpdate = null;
            this.ucBulkRegisteredMail.SendSQLCmd = true;
            this.ucBulkRegisteredMail.ServerModify = true;
            this.ucBulkRegisteredMail.ServerModifyGetMax = false;
            this.ucBulkRegisteredMail.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucBulkRegisteredMail.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucBulkRegisteredMail.UseTranscationScope = false;
            this.ucBulkRegisteredMail.WhereMode = Srvtools.WhereModeType.All;
            // 
            // ERPCustomers2
            // 
            this.ERPCustomers2.CacheConnection = false;
            this.ERPCustomers2.CommandText = "select  distinct  top 100 CustNO,CustShortName,CustAddr from ERPCustomers\r\norder " +
    "by CustNO";
            this.ERPCustomers2.CommandTimeout = 30;
            this.ERPCustomers2.CommandType = System.Data.CommandType.Text;
            this.ERPCustomers2.DynamicTableName = false;
            this.ERPCustomers2.EEPAlias = "";
            this.ERPCustomers2.EncodingAfter = null;
            this.ERPCustomers2.EncodingBefore = "Windows-1252";
            this.ERPCustomers2.EncodingConvert = null;
            this.ERPCustomers2.InfoConnection = this.infoConnection2;
            keyItem2.KeyName = "CustNO";
            this.ERPCustomers2.KeyFields.Add(keyItem2);
            this.ERPCustomers2.MultiSetWhere = false;
            this.ERPCustomers2.Name = "ERPCustomers2";
            this.ERPCustomers2.NotificationAutoEnlist = false;
            this.ERPCustomers2.SecExcept = null;
            this.ERPCustomers2.SecFieldName = null;
            this.ERPCustomers2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPCustomers2.SelectPaging = false;
            this.ERPCustomers2.SelectTop = 0;
            this.ERPCustomers2.SiteControl = false;
            this.ERPCustomers2.SiteFieldName = null;
            this.ERPCustomers2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.BulkRegisteredMail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomers2)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoCommand BulkRegisteredMail;
        private Srvtools.UpdateComponent ucBulkRegisteredMail;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand ERPCustomers2;
    }
}
