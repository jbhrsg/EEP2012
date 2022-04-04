namespace sAssetType
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
            this.AssetType = new Srvtools.InfoCommand(this.components);
            this.ucAssetType = new Srvtools.UpdateComponent(this.components);
            this.View_AssetType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetType)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // AssetType
            // 
            this.AssetType.CacheConnection = false;
            this.AssetType.CommandText = "SELECT dbo.[AssetType].* \r\nFROM dbo.[AssetType]";
            this.AssetType.CommandTimeout = 30;
            this.AssetType.CommandType = System.Data.CommandType.Text;
            this.AssetType.DynamicTableName = false;
            this.AssetType.EEPAlias = null;
            this.AssetType.EncodingAfter = null;
            this.AssetType.EncodingBefore = "Windows-1252";
            this.AssetType.InfoConnection = this.InfoConnection1;
            this.AssetType.MultiSetWhere = false;
            this.AssetType.Name = "AssetType";
            this.AssetType.NotificationAutoEnlist = false;
            this.AssetType.SecExcept = null;
            this.AssetType.SecFieldName = null;
            this.AssetType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetType.SelectPaging = false;
            this.AssetType.SelectTop = 0;
            this.AssetType.SiteControl = false;
            this.AssetType.SiteFieldName = null;
            this.AssetType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucAssetType
            // 
            this.ucAssetType.AutoTrans = true;
            this.ucAssetType.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AssetTypeID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "AssetTypeName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateBy";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucAssetType.FieldAttrs.Add(fieldAttr1);
            this.ucAssetType.FieldAttrs.Add(fieldAttr2);
            this.ucAssetType.FieldAttrs.Add(fieldAttr3);
            this.ucAssetType.FieldAttrs.Add(fieldAttr4);
            this.ucAssetType.LogInfo = null;
            this.ucAssetType.Name = "ucAssetType";
            this.ucAssetType.RowAffectsCheck = true;
            this.ucAssetType.SelectCmd = this.AssetType;
            this.ucAssetType.SelectCmdForUpdate = null;
            this.ucAssetType.ServerModify = true;
            this.ucAssetType.ServerModifyGetMax = false;
            this.ucAssetType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucAssetType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucAssetType.UseTranscationScope = false;
            this.ucAssetType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_AssetType
            // 
            this.View_AssetType.CacheConnection = false;
            this.View_AssetType.CommandText = "SELECT * FROM dbo.[AssetType]";
            this.View_AssetType.CommandTimeout = 30;
            this.View_AssetType.CommandType = System.Data.CommandType.Text;
            this.View_AssetType.DynamicTableName = false;
            this.View_AssetType.EEPAlias = null;
            this.View_AssetType.EncodingAfter = null;
            this.View_AssetType.EncodingBefore = "Windows-1252";
            this.View_AssetType.InfoConnection = this.InfoConnection1;
            this.View_AssetType.MultiSetWhere = false;
            this.View_AssetType.Name = "View_AssetType";
            this.View_AssetType.NotificationAutoEnlist = false;
            this.View_AssetType.SecExcept = null;
            this.View_AssetType.SecFieldName = null;
            this.View_AssetType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_AssetType.SelectPaging = false;
            this.View_AssetType.SelectTop = 0;
            this.View_AssetType.SiteControl = false;
            this.View_AssetType.SiteFieldName = null;
            this.View_AssetType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand AssetType;
        private Srvtools.UpdateComponent ucAssetType;
        private Srvtools.InfoCommand View_AssetType;
    }
}
