namespace sERPSalesToDoList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalesMaster = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesMaster = new Srvtools.UpdateComponent(this.components);
            this.ERPSalesDetails = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesDetails = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalesMaster
            // 
            this.ERPSalesMaster.CacheConnection = false;
            this.ERPSalesMaster.CommandText = resources.GetString("ERPSalesMaster.CommandText");
            this.ERPSalesMaster.CommandTimeout = 30;
            this.ERPSalesMaster.CommandType = System.Data.CommandType.Text;
            this.ERPSalesMaster.DynamicTableName = false;
            this.ERPSalesMaster.EEPAlias = null;
            this.ERPSalesMaster.EncodingAfter = null;
            this.ERPSalesMaster.EncodingBefore = "Windows-1252";
            this.ERPSalesMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesMasterNO";
            this.ERPSalesMaster.KeyFields.Add(keyItem1);
            this.ERPSalesMaster.MultiSetWhere = false;
            this.ERPSalesMaster.Name = "ERPSalesMaster";
            this.ERPSalesMaster.NotificationAutoEnlist = false;
            this.ERPSalesMaster.SecExcept = null;
            this.ERPSalesMaster.SecFieldName = null;
            this.ERPSalesMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesMaster.SelectPaging = false;
            this.ERPSalesMaster.SelectTop = 0;
            this.ERPSalesMaster.SiteControl = false;
            this.ERPSalesMaster.SiteFieldName = null;
            this.ERPSalesMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalesMaster
            // 
            this.ucERPSalesMaster.AutoTrans = true;
            this.ucERPSalesMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalesMasterNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "KeepDays";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "KeepDaysAlert";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucERPSalesMaster.FieldAttrs.Add(fieldAttr1);
            this.ucERPSalesMaster.FieldAttrs.Add(fieldAttr2);
            this.ucERPSalesMaster.FieldAttrs.Add(fieldAttr3);
            this.ucERPSalesMaster.FieldAttrs.Add(fieldAttr4);
            this.ucERPSalesMaster.LogInfo = null;
            this.ucERPSalesMaster.Name = "ucERPSalesMaster";
            this.ucERPSalesMaster.RowAffectsCheck = true;
            this.ucERPSalesMaster.SelectCmd = this.ERPSalesMaster;
            this.ucERPSalesMaster.SelectCmdForUpdate = null;
            this.ucERPSalesMaster.ServerModify = true;
            this.ucERPSalesMaster.ServerModifyGetMax = false;
            this.ucERPSalesMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalesMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalesMaster.UseTranscationScope = false;
            this.ucERPSalesMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
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
            this.ERPSalesDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SalesMasterNO";
            keyItem3.KeyName = "ItemSeq";
            this.ERPSalesDetails.KeyFields.Add(keyItem2);
            this.ERPSalesDetails.KeyFields.Add(keyItem3);
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
            // ucERPSalesDetails
            // 
            this.ucERPSalesDetails.AutoTrans = true;
            this.ucERPSalesDetails.ExceptJoin = false;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "SalesMasterNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ItemSeq";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CustNO";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "SalesDescr";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "SalesDescrDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "SalesDescrAlert";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr5);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr6);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr7);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr8);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr9);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr10);
            this.ucERPSalesDetails.LogInfo = null;
            this.ucERPSalesDetails.Name = "ucERPSalesDetails";
            this.ucERPSalesDetails.RowAffectsCheck = true;
            this.ucERPSalesDetails.SelectCmd = this.ERPSalesDetails;
            this.ucERPSalesDetails.SelectCmdForUpdate = null;
            this.ucERPSalesDetails.ServerModify = true;
            this.ucERPSalesDetails.ServerModifyGetMax = false;
            this.ucERPSalesDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalesDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalesDetails.UseTranscationScope = false;
            this.ucERPSalesDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesMaster;
        private Srvtools.UpdateComponent ucERPSalesMaster;
        private Srvtools.InfoCommand ERPSalesDetails;
        private Srvtools.UpdateComponent ucERPSalesDetails;
    }
}
