namespace sERPOrderFlowM
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPOrderFlowM = new Srvtools.InfoCommand(this.components);
            this.ucERPOrderFlowM = new Srvtools.UpdateComponent(this.components);
            this.ERPOrderFlowD = new Srvtools.InfoCommand(this.components);
            this.ucERPOrderFlowD = new Srvtools.UpdateComponent(this.components);
            this.idERPOrderFlowM_ERPOrderFlowD = new Srvtools.InfoDataSource(this.components);
            this.View_ERPOrderFlowM = new Srvtools.InfoCommand(this.components);
            this.Org = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPOrderFlowM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPOrderFlowD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPOrderFlowM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Org)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPOrderFlowM
            // 
            this.ERPOrderFlowM.CacheConnection = false;
            this.ERPOrderFlowM.CommandText = "SELECT dbo.[ERPOrderFlowM].* FROM dbo.[ERPOrderFlowM]";
            this.ERPOrderFlowM.CommandTimeout = 30;
            this.ERPOrderFlowM.CommandType = System.Data.CommandType.Text;
            this.ERPOrderFlowM.DynamicTableName = false;
            this.ERPOrderFlowM.EEPAlias = null;
            this.ERPOrderFlowM.EncodingAfter = null;
            this.ERPOrderFlowM.EncodingBefore = "Windows-1252";
            this.ERPOrderFlowM.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "OrderFlowNO";
            this.ERPOrderFlowM.KeyFields.Add(keyItem1);
            this.ERPOrderFlowM.MultiSetWhere = false;
            this.ERPOrderFlowM.Name = "ERPOrderFlowM";
            this.ERPOrderFlowM.NotificationAutoEnlist = false;
            this.ERPOrderFlowM.SecExcept = null;
            this.ERPOrderFlowM.SecFieldName = null;
            this.ERPOrderFlowM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPOrderFlowM.SelectPaging = false;
            this.ERPOrderFlowM.SelectTop = 0;
            this.ERPOrderFlowM.SiteControl = false;
            this.ERPOrderFlowM.SiteFieldName = null;
            this.ERPOrderFlowM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPOrderFlowM
            // 
            this.ucERPOrderFlowM.AutoTrans = true;
            this.ucERPOrderFlowM.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "OrderFlowNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ORG_NO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "OrderFlowName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            this.ucERPOrderFlowM.FieldAttrs.Add(fieldAttr1);
            this.ucERPOrderFlowM.FieldAttrs.Add(fieldAttr2);
            this.ucERPOrderFlowM.FieldAttrs.Add(fieldAttr3);
            this.ucERPOrderFlowM.LogInfo = null;
            this.ucERPOrderFlowM.Name = "ucERPOrderFlowM";
            this.ucERPOrderFlowM.RowAffectsCheck = true;
            this.ucERPOrderFlowM.SelectCmd = this.ERPOrderFlowM;
            this.ucERPOrderFlowM.SelectCmdForUpdate = null;
            this.ucERPOrderFlowM.ServerModify = true;
            this.ucERPOrderFlowM.ServerModifyGetMax = false;
            this.ucERPOrderFlowM.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPOrderFlowM.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPOrderFlowM.UseTranscationScope = false;
            this.ucERPOrderFlowM.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ERPOrderFlowD
            // 
            this.ERPOrderFlowD.CacheConnection = false;
            this.ERPOrderFlowD.CommandText = "SELECT dbo.[ERPOrderFlowD].* FROM dbo.[ERPOrderFlowD]";
            this.ERPOrderFlowD.CommandTimeout = 30;
            this.ERPOrderFlowD.CommandType = System.Data.CommandType.Text;
            this.ERPOrderFlowD.DynamicTableName = false;
            this.ERPOrderFlowD.EEPAlias = null;
            this.ERPOrderFlowD.EncodingAfter = null;
            this.ERPOrderFlowD.EncodingBefore = "Windows-1252";
            this.ERPOrderFlowD.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "OrderFlowNO";
            keyItem3.KeyName = "SeqNO";
            this.ERPOrderFlowD.KeyFields.Add(keyItem2);
            this.ERPOrderFlowD.KeyFields.Add(keyItem3);
            this.ERPOrderFlowD.MultiSetWhere = false;
            this.ERPOrderFlowD.Name = "ERPOrderFlowD";
            this.ERPOrderFlowD.NotificationAutoEnlist = false;
            this.ERPOrderFlowD.SecExcept = null;
            this.ERPOrderFlowD.SecFieldName = null;
            this.ERPOrderFlowD.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPOrderFlowD.SelectPaging = false;
            this.ERPOrderFlowD.SelectTop = 0;
            this.ERPOrderFlowD.SiteControl = false;
            this.ERPOrderFlowD.SiteFieldName = null;
            this.ERPOrderFlowD.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPOrderFlowD
            // 
            this.ucERPOrderFlowD.AutoTrans = true;
            this.ucERPOrderFlowD.ExceptJoin = false;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "OrderFlowNO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "SeqNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SeqName";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "SeqOutCome";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "SalesRate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucERPOrderFlowD.FieldAttrs.Add(fieldAttr4);
            this.ucERPOrderFlowD.FieldAttrs.Add(fieldAttr5);
            this.ucERPOrderFlowD.FieldAttrs.Add(fieldAttr6);
            this.ucERPOrderFlowD.FieldAttrs.Add(fieldAttr7);
            this.ucERPOrderFlowD.FieldAttrs.Add(fieldAttr8);
            this.ucERPOrderFlowD.LogInfo = null;
            this.ucERPOrderFlowD.Name = "ucERPOrderFlowD";
            this.ucERPOrderFlowD.RowAffectsCheck = true;
            this.ucERPOrderFlowD.SelectCmd = this.ERPOrderFlowD;
            this.ucERPOrderFlowD.SelectCmdForUpdate = null;
            this.ucERPOrderFlowD.ServerModify = true;
            this.ucERPOrderFlowD.ServerModifyGetMax = false;
            this.ucERPOrderFlowD.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPOrderFlowD.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPOrderFlowD.UseTranscationScope = false;
            this.ucERPOrderFlowD.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idERPOrderFlowM_ERPOrderFlowD
            // 
            this.idERPOrderFlowM_ERPOrderFlowD.Detail = this.ERPOrderFlowD;
            columnItem1.FieldName = "OrderFlowNO";
            this.idERPOrderFlowM_ERPOrderFlowD.DetailColumns.Add(columnItem1);
            this.idERPOrderFlowM_ERPOrderFlowD.DynamicTableName = false;
            this.idERPOrderFlowM_ERPOrderFlowD.Master = this.ERPOrderFlowM;
            columnItem2.FieldName = "OrderFlowNO";
            this.idERPOrderFlowM_ERPOrderFlowD.MasterColumns.Add(columnItem2);
            // 
            // View_ERPOrderFlowM
            // 
            this.View_ERPOrderFlowM.CacheConnection = false;
            this.View_ERPOrderFlowM.CommandText = "SELECT * FROM dbo.[ERPOrderFlowM]";
            this.View_ERPOrderFlowM.CommandTimeout = 30;
            this.View_ERPOrderFlowM.CommandType = System.Data.CommandType.Text;
            this.View_ERPOrderFlowM.DynamicTableName = false;
            this.View_ERPOrderFlowM.EEPAlias = null;
            this.View_ERPOrderFlowM.EncodingAfter = null;
            this.View_ERPOrderFlowM.EncodingBefore = "Windows-1252";
            this.View_ERPOrderFlowM.InfoConnection = this.InfoConnection1;
            this.View_ERPOrderFlowM.MultiSetWhere = false;
            this.View_ERPOrderFlowM.Name = "View_ERPOrderFlowM";
            this.View_ERPOrderFlowM.NotificationAutoEnlist = false;
            this.View_ERPOrderFlowM.SecExcept = null;
            this.View_ERPOrderFlowM.SecFieldName = null;
            this.View_ERPOrderFlowM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPOrderFlowM.SelectPaging = false;
            this.View_ERPOrderFlowM.SelectTop = 0;
            this.View_ERPOrderFlowM.SiteControl = false;
            this.View_ERPOrderFlowM.SiteFieldName = null;
            this.View_ERPOrderFlowM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Org
            // 
            this.Org.CacheConnection = false;
            this.Org.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nWHERE (Uppe" +
    "r_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR ORG_NO=\'99999\')\r\nORDER" +
    " BY ORG_NO";
            this.Org.CommandTimeout = 30;
            this.Org.CommandType = System.Data.CommandType.Text;
            this.Org.DynamicTableName = false;
            this.Org.EEPAlias = null;
            this.Org.EncodingAfter = null;
            this.Org.EncodingBefore = "Windows-1252";
            this.Org.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ORG_NO";
            this.Org.KeyFields.Add(keyItem4);
            this.Org.MultiSetWhere = false;
            this.Org.Name = "Org";
            this.Org.NotificationAutoEnlist = false;
            this.Org.SecExcept = null;
            this.Org.SecFieldName = null;
            this.Org.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Org.SelectPaging = false;
            this.Org.SelectTop = 0;
            this.Org.SiteControl = false;
            this.Org.SiteFieldName = null;
            this.Org.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPOrderFlowM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPOrderFlowD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPOrderFlowM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Org)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPOrderFlowM;
        private Srvtools.UpdateComponent ucERPOrderFlowM;
        private Srvtools.InfoCommand ERPOrderFlowD;
        private Srvtools.UpdateComponent ucERPOrderFlowD;
        private Srvtools.InfoDataSource idERPOrderFlowM_ERPOrderFlowD;
        private Srvtools.InfoCommand View_ERPOrderFlowM;
        private Srvtools.InfoCommand Org;
    }
}
