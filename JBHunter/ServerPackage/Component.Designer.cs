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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_Customer = new Srvtools.InfoCommand(this.components);
            this.ucHUT_Customer = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_Customer = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Customer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Customer)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_Customer
            // 
            this.HUT_Customer.CacheConnection = false;
            this.HUT_Customer.CommandText = "SELECT [HUT_Customer].[CustID],[HUT_Customer].[CustTaxNo],[HUT_Customer].[CustNam" +
    "e],[HUT_Customer].[CustShortName] FROM [HUT_Customer]";
            this.HUT_Customer.CommandTimeout = 30;
            this.HUT_Customer.CommandType = System.Data.CommandType.Text;
            this.HUT_Customer.DynamicTableName = false;
            this.HUT_Customer.EEPAlias = null;
            this.HUT_Customer.EncodingAfter = null;
            this.HUT_Customer.EncodingBefore = "Windows-1252";
            this.HUT_Customer.InfoConnection = this.InfoConnection1;
            this.HUT_Customer.MultiSetWhere = false;
            this.HUT_Customer.Name = "HUT_Customer";
            this.HUT_Customer.NotificationAutoEnlist = false;
            this.HUT_Customer.SecExcept = null;
            this.HUT_Customer.SecFieldName = null;
            this.HUT_Customer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_Customer.SelectPaging = false;
            this.HUT_Customer.SelectTop = 0;
            this.HUT_Customer.SiteControl = false;
            this.HUT_Customer.SiteFieldName = null;
            this.HUT_Customer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_Customer
            // 
            this.ucHUT_Customer.AutoTrans = true;
            this.ucHUT_Customer.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CustID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustTaxNo";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CustName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CustShortName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_Customer.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_Customer.LogInfo = null;
            this.ucHUT_Customer.Name = null;
            this.ucHUT_Customer.RowAffectsCheck = true;
            this.ucHUT_Customer.SelectCmd = this.HUT_Customer;
            this.ucHUT_Customer.ServerModify = true;
            this.ucHUT_Customer.ServerModifyGetMax = false;
            this.ucHUT_Customer.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_Customer.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_Customer.UseTranscationScope = false;
            this.ucHUT_Customer.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_Customer
            // 
            this.View_HUT_Customer.CacheConnection = false;
            this.View_HUT_Customer.CommandText = "SELECT * FROM [HUT_Customer]";
            this.View_HUT_Customer.CommandTimeout = 30;
            this.View_HUT_Customer.CommandType = System.Data.CommandType.Text;
            this.View_HUT_Customer.DynamicTableName = false;
            this.View_HUT_Customer.EEPAlias = null;
            this.View_HUT_Customer.EncodingAfter = null;
            this.View_HUT_Customer.EncodingBefore = "Windows-1252";
            this.View_HUT_Customer.InfoConnection = this.InfoConnection1;
            this.View_HUT_Customer.MultiSetWhere = false;
            this.View_HUT_Customer.Name = "View_HUT_Customer";
            this.View_HUT_Customer.NotificationAutoEnlist = false;
            this.View_HUT_Customer.SecExcept = null;
            this.View_HUT_Customer.SecFieldName = null;
            this.View_HUT_Customer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_Customer.SelectPaging = false;
            this.View_HUT_Customer.SelectTop = 0;
            this.View_HUT_Customer.SiteControl = false;
            this.View_HUT_Customer.SiteFieldName = null;
            this.View_HUT_Customer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Customer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Customer)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_Customer;
        private Srvtools.UpdateComponent ucHUT_Customer;
        private Srvtools.InfoCommand View_HUT_Customer;
    }
}
