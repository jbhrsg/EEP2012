namespace sERPSalesPaper
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
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalesPaper = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesPaper = new Srvtools.UpdateComponent(this.components);
            this.View_ERPSalesPaper = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesPaper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPSalesPaper)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "UpdateSalesPaperName";
            service1.NonLogin = false;
            service1.ServiceName = "UpdateSalesPaperName";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalesPaper
            // 
            this.ERPSalesPaper.CacheConnection = false;
            this.ERPSalesPaper.CommandText = "SELECT dbo.[ERPSalesPaper].* FROM dbo.[ERPSalesPaper] ORDER BY PaperDate DESC";
            this.ERPSalesPaper.CommandTimeout = 30;
            this.ERPSalesPaper.CommandType = System.Data.CommandType.Text;
            this.ERPSalesPaper.DynamicTableName = false;
            this.ERPSalesPaper.EEPAlias = null;
            this.ERPSalesPaper.EncodingAfter = null;
            this.ERPSalesPaper.EncodingBefore = "Windows-1252";
            this.ERPSalesPaper.EncodingConvert = null;
            this.ERPSalesPaper.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "PaperDate";
            this.ERPSalesPaper.KeyFields.Add(keyItem1);
            this.ERPSalesPaper.MultiSetWhere = false;
            this.ERPSalesPaper.Name = "ERPSalesPaper";
            this.ERPSalesPaper.NotificationAutoEnlist = false;
            this.ERPSalesPaper.SecExcept = null;
            this.ERPSalesPaper.SecFieldName = null;
            this.ERPSalesPaper.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesPaper.SelectPaging = false;
            this.ERPSalesPaper.SelectTop = 0;
            this.ERPSalesPaper.SiteControl = false;
            this.ERPSalesPaper.SiteFieldName = null;
            this.ERPSalesPaper.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalesPaper
            // 
            this.ucERPSalesPaper.AutoTrans = true;
            this.ucERPSalesPaper.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalesPaperNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "PaperDate";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesType1_A";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SalesType1_B";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "SalesType1_C";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SalesType31_A";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "SalesType31_B";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucERPSalesPaper.FieldAttrs.Add(fieldAttr1);
            this.ucERPSalesPaper.FieldAttrs.Add(fieldAttr2);
            this.ucERPSalesPaper.FieldAttrs.Add(fieldAttr3);
            this.ucERPSalesPaper.FieldAttrs.Add(fieldAttr4);
            this.ucERPSalesPaper.FieldAttrs.Add(fieldAttr5);
            this.ucERPSalesPaper.FieldAttrs.Add(fieldAttr6);
            this.ucERPSalesPaper.FieldAttrs.Add(fieldAttr7);
            this.ucERPSalesPaper.LogInfo = null;
            this.ucERPSalesPaper.Name = "ucERPSalesPaper";
            this.ucERPSalesPaper.RowAffectsCheck = true;
            this.ucERPSalesPaper.SelectCmd = this.ERPSalesPaper;
            this.ucERPSalesPaper.SelectCmdForUpdate = null;
            this.ucERPSalesPaper.SendSQLCmd = true;
            this.ucERPSalesPaper.ServerModify = true;
            this.ucERPSalesPaper.ServerModifyGetMax = false;
            this.ucERPSalesPaper.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalesPaper.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalesPaper.UseTranscationScope = false;
            this.ucERPSalesPaper.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ERPSalesPaper
            // 
            this.View_ERPSalesPaper.CacheConnection = false;
            this.View_ERPSalesPaper.CommandText = "SELECT * FROM dbo.[ERPSalesPaper]";
            this.View_ERPSalesPaper.CommandTimeout = 30;
            this.View_ERPSalesPaper.CommandType = System.Data.CommandType.Text;
            this.View_ERPSalesPaper.DynamicTableName = false;
            this.View_ERPSalesPaper.EEPAlias = null;
            this.View_ERPSalesPaper.EncodingAfter = null;
            this.View_ERPSalesPaper.EncodingBefore = "Windows-1252";
            this.View_ERPSalesPaper.EncodingConvert = null;
            this.View_ERPSalesPaper.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SalesPaperNO";
            this.View_ERPSalesPaper.KeyFields.Add(keyItem2);
            this.View_ERPSalesPaper.MultiSetWhere = false;
            this.View_ERPSalesPaper.Name = "View_ERPSalesPaper";
            this.View_ERPSalesPaper.NotificationAutoEnlist = false;
            this.View_ERPSalesPaper.SecExcept = null;
            this.View_ERPSalesPaper.SecFieldName = null;
            this.View_ERPSalesPaper.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPSalesPaper.SelectPaging = false;
            this.View_ERPSalesPaper.SelectTop = 0;
            this.View_ERPSalesPaper.SiteControl = false;
            this.View_ERPSalesPaper.SiteFieldName = null;
            this.View_ERPSalesPaper.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesPaper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPSalesPaper)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesPaper;
        private Srvtools.UpdateComponent ucERPSalesPaper;
        private Srvtools.InfoCommand View_ERPSalesPaper;
    }
}
