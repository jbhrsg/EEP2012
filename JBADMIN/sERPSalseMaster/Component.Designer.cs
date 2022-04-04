namespace sERPSalseMaster
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalseMaster = new Srvtools.InfoCommand(this.components);
            this.ucERPSalseMaster = new Srvtools.UpdateComponent(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.infoSalesType = new Srvtools.InfoCommand(this.components);
            this.infoCustomers = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalseMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalseMaster
            // 
            this.ERPSalseMaster.CacheConnection = false;
            this.ERPSalseMaster.CommandText = resources.GetString("ERPSalseMaster.CommandText");
            this.ERPSalseMaster.CommandTimeout = 30;
            this.ERPSalseMaster.CommandType = System.Data.CommandType.Text;
            this.ERPSalseMaster.DynamicTableName = false;
            this.ERPSalseMaster.EEPAlias = null;
            this.ERPSalseMaster.EncodingAfter = null;
            this.ERPSalseMaster.EncodingBefore = "Windows-1252";
            this.ERPSalseMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ItemSeq";
            keyItem2.KeyName = "SalesMasterNO";
            this.ERPSalseMaster.KeyFields.Add(keyItem1);
            this.ERPSalseMaster.KeyFields.Add(keyItem2);
            this.ERPSalseMaster.MultiSetWhere = false;
            this.ERPSalseMaster.Name = "ERPSalseMaster";
            this.ERPSalseMaster.NotificationAutoEnlist = false;
            this.ERPSalseMaster.SecExcept = null;
            this.ERPSalseMaster.SecFieldName = null;
            this.ERPSalseMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalseMaster.SelectPaging = false;
            this.ERPSalseMaster.SelectTop = 0;
            this.ERPSalseMaster.SiteControl = false;
            this.ERPSalseMaster.SiteFieldName = null;
            this.ERPSalseMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalseMaster
            // 
            this.ucERPSalseMaster.AutoTrans = true;
            this.ucERPSalseMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalseMasterNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SalseID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesTypeID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "StdDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "EndDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SalseAmt";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "NewTypeID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "NewsAreaID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "NewsPublishID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr1);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr2);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr3);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr4);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr5);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr6);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr7);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr8);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr9);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr10);
            this.ucERPSalseMaster.FieldAttrs.Add(fieldAttr11);
            this.ucERPSalseMaster.LogInfo = null;
            this.ucERPSalseMaster.Name = "ucERPSalseMaster";
            this.ucERPSalseMaster.RowAffectsCheck = true;
            this.ucERPSalseMaster.SelectCmd = this.ERPSalseMaster;
            this.ucERPSalseMaster.SelectCmdForUpdate = null;
            this.ucERPSalseMaster.ServerModify = true;
            this.ucERPSalseMaster.ServerModifyGetMax = false;
            this.ucERPSalseMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalseMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalseMaster.UseTranscationScope = false;
            this.ucERPSalseMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "SalseMasterNO";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "";
            this.autoNumber1.isNumFill = true;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 1;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "SalseMasterNO";
            this.autoNumber1.UpdateComp = this.ucERPSalseMaster;
            // 
            // infoSalesType
            // 
            this.infoSalesType.CacheConnection = false;
            this.infoSalesType.CommandText = "select ERPSalesType.* from ERPSalesType";
            this.infoSalesType.CommandTimeout = 30;
            this.infoSalesType.CommandType = System.Data.CommandType.Text;
            this.infoSalesType.DynamicTableName = false;
            this.infoSalesType.EEPAlias = null;
            this.infoSalesType.EncodingAfter = null;
            this.infoSalesType.EncodingBefore = "Windows-1252";
            this.infoSalesType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "SalesTypeNO";
            this.infoSalesType.KeyFields.Add(keyItem3);
            this.infoSalesType.MultiSetWhere = false;
            this.infoSalesType.Name = "infoSalesType";
            this.infoSalesType.NotificationAutoEnlist = false;
            this.infoSalesType.SecExcept = null;
            this.infoSalesType.SecFieldName = null;
            this.infoSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesType.SelectPaging = false;
            this.infoSalesType.SelectTop = 0;
            this.infoSalesType.SiteControl = false;
            this.infoSalesType.SiteFieldName = null;
            this.infoSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCustomers
            // 
            this.infoCustomers.CacheConnection = false;
            this.infoCustomers.CommandText = "select ERPCustomers.* from ERPCustomers";
            this.infoCustomers.CommandTimeout = 30;
            this.infoCustomers.CommandType = System.Data.CommandType.Text;
            this.infoCustomers.DynamicTableName = false;
            this.infoCustomers.EEPAlias = null;
            this.infoCustomers.EncodingAfter = null;
            this.infoCustomers.EncodingBefore = "Windows-1252";
            this.infoCustomers.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "CustNO";
            this.infoCustomers.KeyFields.Add(keyItem4);
            this.infoCustomers.MultiSetWhere = false;
            this.infoCustomers.Name = "infoCustomers";
            this.infoCustomers.NotificationAutoEnlist = false;
            this.infoCustomers.SecExcept = null;
            this.infoCustomers.SecFieldName = null;
            this.infoCustomers.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustomers.SelectPaging = false;
            this.infoCustomers.SelectTop = 0;
            this.infoCustomers.SiteControl = false;
            this.infoCustomers.SiteFieldName = null;
            this.infoCustomers.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalseMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomers)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalseMaster;
        private Srvtools.UpdateComponent ucERPSalseMaster;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand infoSalesType;
        private Srvtools.InfoCommand infoCustomers;
    }
}
