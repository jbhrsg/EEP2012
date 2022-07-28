namespace sEstimateProfit
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
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.Service service1 = new Srvtools.Service();
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.EstimateProfit = new Srvtools.InfoCommand(this.components);
            this.ucEstimateProfit = new Srvtools.UpdateComponent(this.components);
            this.TheServiceManager = new Srvtools.ServiceManager(this.components);
            this.infoAcnoAll = new Srvtools.InfoCommand(this.components);
            this.infoglCostCenter = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EstimateProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAcnoAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglCostCenter)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // EstimateProfit
            // 
            this.EstimateProfit.CacheConnection = false;
            this.EstimateProfit.CommandText = "SELECT e.*,g.AcnoName\r\nFROM EstimateProfit e\r\n\tinner join glAccountItem g on g.Co" +
    "mpanyID=1 and e.AcnoAll=g.Acno+g.SubAcno\r\norder by e.LastUpdateDate desc,e.AutoK" +
    "ey desc";
            this.EstimateProfit.CommandTimeout = 30;
            this.EstimateProfit.CommandType = System.Data.CommandType.Text;
            this.EstimateProfit.DynamicTableName = false;
            this.EstimateProfit.EEPAlias = null;
            this.EstimateProfit.EncodingAfter = null;
            this.EstimateProfit.EncodingBefore = "Windows-1252";
            this.EstimateProfit.EncodingConvert = null;
            this.EstimateProfit.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.EstimateProfit.KeyFields.Add(keyItem1);
            this.EstimateProfit.MultiSetWhere = false;
            this.EstimateProfit.Name = "EstimateProfit";
            this.EstimateProfit.NotificationAutoEnlist = false;
            this.EstimateProfit.SecExcept = null;
            this.EstimateProfit.SecFieldName = null;
            this.EstimateProfit.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EstimateProfit.SelectPaging = false;
            this.EstimateProfit.SelectTop = 0;
            this.EstimateProfit.SiteControl = false;
            this.EstimateProfit.SiteFieldName = null;
            this.EstimateProfit.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucEstimateProfit
            // 
            this.ucEstimateProfit.AutoTrans = true;
            this.ucEstimateProfit.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CompanyID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "VoucherID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "VoucherNo";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Item";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "BorrowLendType";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Acno";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "SubAcno";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "DescribeID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CostCenterID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Describe";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Amt";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "AmtShow";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateBy";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LastUpdateBy";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "LastUpdateDate";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr1);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr2);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr3);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr4);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr5);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr6);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr7);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr8);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr9);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr10);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr11);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr12);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr13);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr14);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr15);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr16);
            this.ucEstimateProfit.FieldAttrs.Add(fieldAttr17);
            this.ucEstimateProfit.LogInfo = null;
            this.ucEstimateProfit.Name = "ucEstimateProfit";
            this.ucEstimateProfit.RowAffectsCheck = true;
            this.ucEstimateProfit.SelectCmd = this.EstimateProfit;
            this.ucEstimateProfit.SelectCmdForUpdate = null;
            this.ucEstimateProfit.SendSQLCmd = true;
            this.ucEstimateProfit.ServerModify = true;
            this.ucEstimateProfit.ServerModifyGetMax = false;
            this.ucEstimateProfit.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEstimateProfit.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEstimateProfit.UseTranscationScope = false;
            this.ucEstimateProfit.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucEstimateProfit.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucEstimateProfit_BeforeInsert);
            this.ucEstimateProfit.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucEstimateProfit_BeforeModify);
            // 
            // TheServiceManager
            // 
            service1.DelegateName = "DataValidate";
            service1.NonLogin = false;
            service1.ServiceName = "DataValidate";
            service2.DelegateName = "GetOldSetting";
            service2.NonLogin = false;
            service2.ServiceName = "GetOldSetting";
            service3.DelegateName = "ExcelFileImport";
            service3.NonLogin = false;
            service3.ServiceName = "ExcelFileImport";
            this.TheServiceManager.ServiceCollection.Add(service1);
            this.TheServiceManager.ServiceCollection.Add(service2);
            this.TheServiceManager.ServiceCollection.Add(service3);
            // 
            // infoAcnoAll
            // 
            this.infoAcnoAll.CacheConnection = false;
            this.infoAcnoAll.CommandText = "SELECT AcnoSubAcno,AcnoName\r\nFROM View_glAccountItem\r\nwhere CompanyID=1\r\norder by" +
    " AcnoSubAcno\r\n\r\n";
            this.infoAcnoAll.CommandTimeout = 30;
            this.infoAcnoAll.CommandType = System.Data.CommandType.Text;
            this.infoAcnoAll.DynamicTableName = false;
            this.infoAcnoAll.EEPAlias = "";
            this.infoAcnoAll.EncodingAfter = null;
            this.infoAcnoAll.EncodingBefore = "Windows-1252";
            this.infoAcnoAll.EncodingConvert = null;
            this.infoAcnoAll.InfoConnection = this.InfoConnection1;
            this.infoAcnoAll.MultiSetWhere = false;
            this.infoAcnoAll.Name = "infoAcnoAll";
            this.infoAcnoAll.NotificationAutoEnlist = false;
            this.infoAcnoAll.SecExcept = null;
            this.infoAcnoAll.SecFieldName = null;
            this.infoAcnoAll.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoAcnoAll.SelectPaging = false;
            this.infoAcnoAll.SelectTop = 0;
            this.infoAcnoAll.SiteControl = false;
            this.infoAcnoAll.SiteFieldName = null;
            this.infoAcnoAll.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoglCostCenter
            // 
            this.infoglCostCenter.CacheConnection = false;
            this.infoglCostCenter.CommandText = "SELECT AutoKey,CostCenterID,CostCenterID+\':\'+CostCenterName as CostCenterName\r\nFR" +
    "OM glCostCenter\r\nwhere CostCenterID!=\'000\' and CostCenterID!=\'999\'";
            this.infoglCostCenter.CommandTimeout = 30;
            this.infoglCostCenter.CommandType = System.Data.CommandType.Text;
            this.infoglCostCenter.DynamicTableName = false;
            this.infoglCostCenter.EEPAlias = "";
            this.infoglCostCenter.EncodingAfter = null;
            this.infoglCostCenter.EncodingBefore = "Windows-1252";
            this.infoglCostCenter.EncodingConvert = null;
            this.infoglCostCenter.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.infoglCostCenter.KeyFields.Add(keyItem2);
            this.infoglCostCenter.MultiSetWhere = false;
            this.infoglCostCenter.Name = "infoglCostCenter";
            this.infoglCostCenter.NotificationAutoEnlist = false;
            this.infoglCostCenter.SecExcept = null;
            this.infoglCostCenter.SecFieldName = null;
            this.infoglCostCenter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoglCostCenter.SelectPaging = false;
            this.infoglCostCenter.SelectTop = 0;
            this.infoglCostCenter.SiteControl = false;
            this.infoglCostCenter.SiteFieldName = null;
            this.infoglCostCenter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EstimateProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAcnoAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoglCostCenter)).EndInit();

        }

        #endregion

        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand EstimateProfit;
        private Srvtools.InfoCommand cb_glVoucherDetails;
        private Srvtools.UpdateComponent ucEstimateProfit;
        private Srvtools.ServiceManager TheServiceManager;
        private Srvtools.InfoCommand infoAcnoAll;
        private Srvtools.InfoCommand infoglCostCenter;
    }
}
