namespace sDept
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SYS_ORG = new Srvtools.InfoCommand(this.components);
            this.ucSYS_ORG = new Srvtools.UpdateComponent(this.components);
            this.View_SYS_ORG = new Srvtools.InfoCommand(this.components);
            this.CostCenter = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ORG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SYS_ORG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenter)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "EIPHRSYS";
            // 
            // SYS_ORG
            // 
            this.SYS_ORG.CacheConnection = false;
            this.SYS_ORG.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG,CostCenterID\r\nFROM EIPHRSYS.dbo.sys_org" +
    "\r\nWHERE (Upper_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR ORG_NO=\'9" +
    "9999\')\r\nORDER BY ORG_NO";
            this.SYS_ORG.CommandTimeout = 30;
            this.SYS_ORG.CommandType = System.Data.CommandType.Text;
            this.SYS_ORG.DynamicTableName = false;
            this.SYS_ORG.EEPAlias = null;
            this.SYS_ORG.EncodingAfter = null;
            this.SYS_ORG.EncodingBefore = "Windows-1252";
            this.SYS_ORG.EncodingConvert = null;
            this.SYS_ORG.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ORG_NO";
            this.SYS_ORG.KeyFields.Add(keyItem1);
            this.SYS_ORG.MultiSetWhere = false;
            this.SYS_ORG.Name = "SYS_ORG";
            this.SYS_ORG.NotificationAutoEnlist = false;
            this.SYS_ORG.SecExcept = null;
            this.SYS_ORG.SecFieldName = null;
            this.SYS_ORG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SYS_ORG.SelectPaging = false;
            this.SYS_ORG.SelectTop = 0;
            this.SYS_ORG.SiteControl = false;
            this.SYS_ORG.SiteFieldName = null;
            this.SYS_ORG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSYS_ORG
            // 
            this.ucSYS_ORG.AutoTrans = true;
            this.ucSYS_ORG.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ORG_NO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ORG_DESC";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ORG_KIND";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "UPPER_ORG";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ORG_MAN";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LEVEL_NO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ORG_TREE";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "END_ORG";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ORG_FULLNAME";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "COSTCENTERID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr1);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr2);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr3);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr4);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr5);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr6);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr7);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr8);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr9);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr10);
            this.ucSYS_ORG.LogInfo = null;
            this.ucSYS_ORG.Name = "ucSYS_ORG";
            this.ucSYS_ORG.RowAffectsCheck = true;
            this.ucSYS_ORG.SelectCmd = this.SYS_ORG;
            this.ucSYS_ORG.SelectCmdForUpdate = null;
            this.ucSYS_ORG.SendSQLCmd = true;
            this.ucSYS_ORG.ServerModify = true;
            this.ucSYS_ORG.ServerModifyGetMax = false;
            this.ucSYS_ORG.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSYS_ORG.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSYS_ORG.UseTranscationScope = false;
            this.ucSYS_ORG.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_SYS_ORG
            // 
            this.View_SYS_ORG.CacheConnection = false;
            this.View_SYS_ORG.CommandText = "SELECT * FROM dbo.[SYS_ORG]";
            this.View_SYS_ORG.CommandTimeout = 30;
            this.View_SYS_ORG.CommandType = System.Data.CommandType.Text;
            this.View_SYS_ORG.DynamicTableName = false;
            this.View_SYS_ORG.EEPAlias = null;
            this.View_SYS_ORG.EncodingAfter = null;
            this.View_SYS_ORG.EncodingBefore = "Windows-1252";
            this.View_SYS_ORG.EncodingConvert = null;
            this.View_SYS_ORG.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ORG_NO";
            this.View_SYS_ORG.KeyFields.Add(keyItem2);
            this.View_SYS_ORG.MultiSetWhere = false;
            this.View_SYS_ORG.Name = "View_SYS_ORG";
            this.View_SYS_ORG.NotificationAutoEnlist = false;
            this.View_SYS_ORG.SecExcept = null;
            this.View_SYS_ORG.SecFieldName = null;
            this.View_SYS_ORG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SYS_ORG.SelectPaging = false;
            this.View_SYS_ORG.SelectTop = 0;
            this.View_SYS_ORG.SiteControl = false;
            this.View_SYS_ORG.SiteFieldName = null;
            this.View_SYS_ORG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CostCenter
            // 
            this.CostCenter.CacheConnection = false;
            this.CostCenter.CommandText = "SELECT AutoKey,CostCenterID,CostCenterName\r\nFROM JBADMIN.DBO.COSTCENTER\r\nWHERE IS" +
    "Active=1 ORDER BY  CostCenterID";
            this.CostCenter.CommandTimeout = 30;
            this.CostCenter.CommandType = System.Data.CommandType.Text;
            this.CostCenter.DynamicTableName = false;
            this.CostCenter.EEPAlias = null;
            this.CostCenter.EncodingAfter = null;
            this.CostCenter.EncodingBefore = "Windows-1252";
            this.CostCenter.EncodingConvert = null;
            this.CostCenter.InfoConnection = this.InfoConnection1;
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ORG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SYS_ORG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenter)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SYS_ORG;
        private Srvtools.UpdateComponent ucSYS_ORG;
        private Srvtools.InfoCommand View_SYS_ORG;
        private Srvtools.InfoCommand CostCenter;
    }
}
