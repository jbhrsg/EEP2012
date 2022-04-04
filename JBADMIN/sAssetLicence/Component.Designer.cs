namespace sAssetLicence
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.AssetLicence = new Srvtools.InfoCommand(this.components);
            this.ucAssetLicence = new Srvtools.UpdateComponent(this.components);
            this.View_AssetLicence = new Srvtools.InfoCommand(this.components);
            this.LicenceType = new Srvtools.InfoCommand(this.components);
            this.LicenceName = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLicence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetLicence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenceName)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // AssetLicence
            // 
            this.AssetLicence.CacheConnection = false;
            this.AssetLicence.CommandText = "SELECT dbo.AssetLicence.* ,\r\n             dbo.funReturnLicenceAsset(AssetLiceType" +
    ",AssetLiceCode)AS AssignTo\r\nFROM dbo.AssetLicence\r\nORDER BY AssetLiceType,AssetL" +
    "iceTypeName";
            this.AssetLicence.CommandTimeout = 30;
            this.AssetLicence.CommandType = System.Data.CommandType.Text;
            this.AssetLicence.DynamicTableName = false;
            this.AssetLicence.EEPAlias = null;
            this.AssetLicence.EncodingAfter = null;
            this.AssetLicence.EncodingBefore = "Windows-1252";
            this.AssetLicence.EncodingConvert = null;
            this.AssetLicence.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AssetLiceID";
            this.AssetLicence.KeyFields.Add(keyItem1);
            this.AssetLicence.MultiSetWhere = false;
            this.AssetLicence.Name = "AssetLicence";
            this.AssetLicence.NotificationAutoEnlist = false;
            this.AssetLicence.SecExcept = null;
            this.AssetLicence.SecFieldName = null;
            this.AssetLicence.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetLicence.SelectPaging = false;
            this.AssetLicence.SelectTop = 0;
            this.AssetLicence.SiteControl = false;
            this.AssetLicence.SiteFieldName = null;
            this.AssetLicence.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucAssetLicence
            // 
            this.ucAssetLicence.AutoTrans = true;
            this.ucAssetLicence.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AssetLiceID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "AssetLiceType";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "AssetLiceDescr";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "AssetLiceCode";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "AssetLiceDueDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AssetLiceNotes";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateBy";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr7.DefaultValue = "_username";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucAssetLicence.FieldAttrs.Add(fieldAttr1);
            this.ucAssetLicence.FieldAttrs.Add(fieldAttr2);
            this.ucAssetLicence.FieldAttrs.Add(fieldAttr3);
            this.ucAssetLicence.FieldAttrs.Add(fieldAttr4);
            this.ucAssetLicence.FieldAttrs.Add(fieldAttr5);
            this.ucAssetLicence.FieldAttrs.Add(fieldAttr6);
            this.ucAssetLicence.FieldAttrs.Add(fieldAttr7);
            this.ucAssetLicence.FieldAttrs.Add(fieldAttr8);
            this.ucAssetLicence.LogInfo = null;
            this.ucAssetLicence.Name = "ucAssetLicence";
            this.ucAssetLicence.RowAffectsCheck = true;
            this.ucAssetLicence.SelectCmd = this.AssetLicence;
            this.ucAssetLicence.SelectCmdForUpdate = null;
            this.ucAssetLicence.SendSQLCmd = true;
            this.ucAssetLicence.ServerModify = true;
            this.ucAssetLicence.ServerModifyGetMax = false;
            this.ucAssetLicence.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucAssetLicence.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucAssetLicence.UseTranscationScope = false;
            this.ucAssetLicence.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucAssetLicence.BeforeApply += new Srvtools.UpdateComponentBeforeApplyEventHandler(this.ucAssetLicence_BeforeApply);
            this.ucAssetLicence.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucAssetLicence_BeforeInsert);
            // 
            // View_AssetLicence
            // 
            this.View_AssetLicence.CacheConnection = false;
            this.View_AssetLicence.CommandText = "SELECT * FROM dbo.[AssetLicence]";
            this.View_AssetLicence.CommandTimeout = 30;
            this.View_AssetLicence.CommandType = System.Data.CommandType.Text;
            this.View_AssetLicence.DynamicTableName = false;
            this.View_AssetLicence.EEPAlias = null;
            this.View_AssetLicence.EncodingAfter = null;
            this.View_AssetLicence.EncodingBefore = "Windows-1252";
            this.View_AssetLicence.EncodingConvert = null;
            this.View_AssetLicence.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AssetLiceID";
            this.View_AssetLicence.KeyFields.Add(keyItem2);
            this.View_AssetLicence.MultiSetWhere = false;
            this.View_AssetLicence.Name = "View_AssetLicence";
            this.View_AssetLicence.NotificationAutoEnlist = false;
            this.View_AssetLicence.SecExcept = null;
            this.View_AssetLicence.SecFieldName = null;
            this.View_AssetLicence.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_AssetLicence.SelectPaging = false;
            this.View_AssetLicence.SelectTop = 0;
            this.View_AssetLicence.SiteControl = false;
            this.View_AssetLicence.SiteFieldName = null;
            this.View_AssetLicence.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // LicenceType
            // 
            this.LicenceType.CacheConnection = false;
            this.LicenceType.CommandText = "SELECT Distinct  AssetLiceType FROM AssetLicenceType";
            this.LicenceType.CommandTimeout = 30;
            this.LicenceType.CommandType = System.Data.CommandType.Text;
            this.LicenceType.DynamicTableName = false;
            this.LicenceType.EEPAlias = null;
            this.LicenceType.EncodingAfter = null;
            this.LicenceType.EncodingBefore = "Windows-1252";
            this.LicenceType.EncodingConvert = null;
            this.LicenceType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AssetLiceTypeNO";
            this.LicenceType.KeyFields.Add(keyItem3);
            this.LicenceType.MultiSetWhere = false;
            this.LicenceType.Name = "LicenceType";
            this.LicenceType.NotificationAutoEnlist = false;
            this.LicenceType.SecExcept = null;
            this.LicenceType.SecFieldName = null;
            this.LicenceType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.LicenceType.SelectPaging = false;
            this.LicenceType.SelectTop = 0;
            this.LicenceType.SiteControl = false;
            this.LicenceType.SiteFieldName = null;
            this.LicenceType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // LicenceName
            // 
            this.LicenceName.CacheConnection = false;
            this.LicenceName.CommandText = "SELECT *  FROM AssetLicenceType\r\nORDER BY SORT";
            this.LicenceName.CommandTimeout = 30;
            this.LicenceName.CommandType = System.Data.CommandType.Text;
            this.LicenceName.DynamicTableName = false;
            this.LicenceName.EEPAlias = null;
            this.LicenceName.EncodingAfter = null;
            this.LicenceName.EncodingBefore = "Windows-1252";
            this.LicenceName.EncodingConvert = null;
            this.LicenceName.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AssetLiceTypeNO";
            this.LicenceName.KeyFields.Add(keyItem4);
            this.LicenceName.MultiSetWhere = false;
            this.LicenceName.Name = "LicenceName";
            this.LicenceName.NotificationAutoEnlist = false;
            this.LicenceName.SecExcept = null;
            this.LicenceName.SecFieldName = null;
            this.LicenceName.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.LicenceName.SelectPaging = false;
            this.LicenceName.SelectTop = 0;
            this.LicenceName.SiteControl = false;
            this.LicenceName.SiteFieldName = null;
            this.LicenceName.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLicence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetLicence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenceName)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand AssetLicence;
        private Srvtools.UpdateComponent ucAssetLicence;
        private Srvtools.InfoCommand View_AssetLicence;
        private Srvtools.InfoCommand LicenceType;
        private Srvtools.InfoCommand LicenceName;
    }
}
