namespace sCostCenter
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
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CostCenter = new Srvtools.InfoCommand(this.components);
            this.ucCostCenter = new Srvtools.UpdateComponent(this.components);
            this.View_CostCenter = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CostCenter)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelCostCenter";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelCostCenter";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // CostCenter
            // 
            this.CostCenter.CacheConnection = false;
            this.CostCenter.CommandText = "SELECT dbo.[CostCenter].* FROM dbo.[CostCenter]\r\norder by  CostCenterID";
            this.CostCenter.CommandTimeout = 30;
            this.CostCenter.CommandType = System.Data.CommandType.Text;
            this.CostCenter.DynamicTableName = false;
            this.CostCenter.EEPAlias = null;
            this.CostCenter.EncodingAfter = null;
            this.CostCenter.EncodingBefore = "Windows-1252";
            this.CostCenter.EncodingConvert = null;
            this.CostCenter.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.CostCenter.KeyFields.Add(keyItem1);
            this.CostCenter.MultiSetWhere = false;
            this.CostCenter.Name = "CostCenter";
            this.CostCenter.NotificationAutoEnlist = false;
            this.CostCenter.SecExcept = null;
            this.CostCenter.SecFieldName = null;
            this.CostCenter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CostCenter.SelectPaging = false;
            this.CostCenter.SelectTop = 0;
            this.CostCenter.SiteControl = false;
            this.CostCenter.SiteFieldName = null;
            this.CostCenter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCostCenter
            // 
            this.ucCostCenter.AutoTrans = true;
            this.ucCostCenter.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CostCenterID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CostCenterName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Org_NO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateBy";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = "_USERNAME";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucCostCenter.FieldAttrs.Add(fieldAttr1);
            this.ucCostCenter.FieldAttrs.Add(fieldAttr2);
            this.ucCostCenter.FieldAttrs.Add(fieldAttr3);
            this.ucCostCenter.FieldAttrs.Add(fieldAttr4);
            this.ucCostCenter.FieldAttrs.Add(fieldAttr5);
            this.ucCostCenter.FieldAttrs.Add(fieldAttr6);
            this.ucCostCenter.FieldAttrs.Add(fieldAttr7);
            this.ucCostCenter.FieldAttrs.Add(fieldAttr8);
            this.ucCostCenter.LogInfo = null;
            this.ucCostCenter.Name = "ucCostCenter";
            this.ucCostCenter.RowAffectsCheck = true;
            this.ucCostCenter.SelectCmd = this.CostCenter;
            this.ucCostCenter.SelectCmdForUpdate = null;
            this.ucCostCenter.SendSQLCmd = true;
            this.ucCostCenter.ServerModify = true;
            this.ucCostCenter.ServerModifyGetMax = false;
            this.ucCostCenter.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCostCenter.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCostCenter.UseTranscationScope = false;
            this.ucCostCenter.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCostCenter.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCostCenter_BeforeInsert);
            this.ucCostCenter.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCostCenter_BeforeModify);
            // 
            // View_CostCenter
            // 
            this.View_CostCenter.CacheConnection = false;
            this.View_CostCenter.CommandText = "SELECT * FROM dbo.[CostCenter]";
            this.View_CostCenter.CommandTimeout = 30;
            this.View_CostCenter.CommandType = System.Data.CommandType.Text;
            this.View_CostCenter.DynamicTableName = false;
            this.View_CostCenter.EEPAlias = null;
            this.View_CostCenter.EncodingAfter = null;
            this.View_CostCenter.EncodingBefore = "Windows-1252";
            this.View_CostCenter.EncodingConvert = null;
            this.View_CostCenter.InfoConnection = this.InfoConnection1;
            this.View_CostCenter.MultiSetWhere = false;
            this.View_CostCenter.Name = "View_CostCenter";
            this.View_CostCenter.NotificationAutoEnlist = false;
            this.View_CostCenter.SecExcept = null;
            this.View_CostCenter.SecFieldName = null;
            this.View_CostCenter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CostCenter.SelectPaging = false;
            this.View_CostCenter.SelectTop = 0;
            this.View_CostCenter.SiteControl = false;
            this.View_CostCenter.SiteFieldName = null;
            this.View_CostCenter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CostCenter)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CostCenter;
        private Srvtools.UpdateComponent ucCostCenter;
        private Srvtools.InfoCommand View_CostCenter;
    }
}
