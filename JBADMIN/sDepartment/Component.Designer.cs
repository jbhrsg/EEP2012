namespace sDepartment
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
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
            this.SYS_ORG.CommandText = resources.GetString("SYS_ORG.CommandText");
            this.SYS_ORG.CommandTimeout = 30;
            this.SYS_ORG.CommandType = System.Data.CommandType.Text;
            this.SYS_ORG.DynamicTableName = false;
            this.SYS_ORG.EEPAlias = null;
            this.SYS_ORG.EncodingAfter = null;
            this.SYS_ORG.EncodingBefore = "Windows-1252";
            this.SYS_ORG.EncodingConvert = null;
            this.SYS_ORG.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ORG_NO";
            this.SYS_ORG.KeyFields.Add(keyItem3);
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
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "ORG_NO";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "ORG_DESC";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "ORG_KIND";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "UPPER_ORG";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "ORG_MAN";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LEVEL_NO";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "ORG_TREE";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "END_ORG";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "ORG_FULLNAME";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "COSTCENTERID";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr11);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr12);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr13);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr14);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr15);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr16);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr17);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr18);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr19);
            this.ucSYS_ORG.FieldAttrs.Add(fieldAttr20);
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
            keyItem1.KeyName = "ORG_NO";
            this.View_SYS_ORG.KeyFields.Add(keyItem1);
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
