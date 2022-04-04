namespace sAssetLocation
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
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.AssetLocation = new Srvtools.InfoCommand(this.components);
            this.ucAssetLocation = new Srvtools.UpdateComponent(this.components);
            this.AssetLocaContDetails = new Srvtools.InfoCommand(this.components);
            this.ucAssetLocaContDetails = new Srvtools.UpdateComponent(this.components);
            this.idAssetLocation_AssetLocaContDetails = new Srvtools.InfoDataSource(this.components);
            this.View_AssetLocation = new Srvtools.InfoCommand(this.components);
            this.OwnerList = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLocaContDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OwnerList)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // AssetLocation
            // 
            this.AssetLocation.CacheConnection = false;
            this.AssetLocation.CommandText = resources.GetString("AssetLocation.CommandText");
            this.AssetLocation.CommandTimeout = 30;
            this.AssetLocation.CommandType = System.Data.CommandType.Text;
            this.AssetLocation.DynamicTableName = false;
            this.AssetLocation.EEPAlias = null;
            this.AssetLocation.EncodingAfter = null;
            this.AssetLocation.EncodingBefore = "Windows-1252";
            this.AssetLocation.EncodingConvert = null;
            this.AssetLocation.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AssetLocaID";
            this.AssetLocation.KeyFields.Add(keyItem1);
            this.AssetLocation.MultiSetWhere = false;
            this.AssetLocation.Name = "AssetLocation";
            this.AssetLocation.NotificationAutoEnlist = false;
            this.AssetLocation.SecExcept = null;
            this.AssetLocation.SecFieldName = null;
            this.AssetLocation.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetLocation.SelectPaging = false;
            this.AssetLocation.SelectTop = 0;
            this.AssetLocation.SiteControl = false;
            this.AssetLocation.SiteFieldName = null;
            this.AssetLocation.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucAssetLocation
            // 
            this.ucAssetLocation.AutoTrans = true;
            this.ucAssetLocation.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AssetLocaID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "AssetLocaName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "AssetLocaAddr";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "AssetLocaNotes";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "AssetLocaEffectDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr6.DefaultValue = "_username";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucAssetLocation.FieldAttrs.Add(fieldAttr1);
            this.ucAssetLocation.FieldAttrs.Add(fieldAttr2);
            this.ucAssetLocation.FieldAttrs.Add(fieldAttr3);
            this.ucAssetLocation.FieldAttrs.Add(fieldAttr4);
            this.ucAssetLocation.FieldAttrs.Add(fieldAttr5);
            this.ucAssetLocation.FieldAttrs.Add(fieldAttr6);
            this.ucAssetLocation.FieldAttrs.Add(fieldAttr7);
            this.ucAssetLocation.LogInfo = null;
            this.ucAssetLocation.Name = "ucAssetLocation";
            this.ucAssetLocation.RowAffectsCheck = true;
            this.ucAssetLocation.SelectCmd = this.AssetLocation;
            this.ucAssetLocation.SelectCmdForUpdate = null;
            this.ucAssetLocation.SendSQLCmd = true;
            this.ucAssetLocation.ServerModify = true;
            this.ucAssetLocation.ServerModifyGetMax = false;
            this.ucAssetLocation.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucAssetLocation.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucAssetLocation.UseTranscationScope = false;
            this.ucAssetLocation.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucAssetLocation.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucAssetLocation_BeforeInsert);
            // 
            // AssetLocaContDetails
            // 
            this.AssetLocaContDetails.CacheConnection = false;
            this.AssetLocaContDetails.CommandText = "SELECT dbo.[AssetLocaContDetails].* FROM dbo.[AssetLocaContDetails]";
            this.AssetLocaContDetails.CommandTimeout = 30;
            this.AssetLocaContDetails.CommandType = System.Data.CommandType.Text;
            this.AssetLocaContDetails.DynamicTableName = false;
            this.AssetLocaContDetails.EEPAlias = null;
            this.AssetLocaContDetails.EncodingAfter = null;
            this.AssetLocaContDetails.EncodingBefore = "Windows-1252";
            this.AssetLocaContDetails.EncodingConvert = null;
            this.AssetLocaContDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AssetLocaContractsNO";
            this.AssetLocaContDetails.KeyFields.Add(keyItem2);
            this.AssetLocaContDetails.MultiSetWhere = false;
            this.AssetLocaContDetails.Name = "AssetLocaContDetails";
            this.AssetLocaContDetails.NotificationAutoEnlist = false;
            this.AssetLocaContDetails.SecExcept = null;
            this.AssetLocaContDetails.SecFieldName = null;
            this.AssetLocaContDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetLocaContDetails.SelectPaging = false;
            this.AssetLocaContDetails.SelectTop = 0;
            this.AssetLocaContDetails.SiteControl = false;
            this.AssetLocaContDetails.SiteFieldName = null;
            this.AssetLocaContDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucAssetLocaContDetails
            // 
            this.ucAssetLocaContDetails.AutoTrans = true;
            this.ucAssetLocaContDetails.ExceptJoin = false;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AssetLocaContractsNO";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "AssetLocaID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LocaContID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "AssetLocaEffectDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateBy";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = "_username";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucAssetLocaContDetails.FieldAttrs.Add(fieldAttr8);
            this.ucAssetLocaContDetails.FieldAttrs.Add(fieldAttr9);
            this.ucAssetLocaContDetails.FieldAttrs.Add(fieldAttr10);
            this.ucAssetLocaContDetails.FieldAttrs.Add(fieldAttr11);
            this.ucAssetLocaContDetails.FieldAttrs.Add(fieldAttr12);
            this.ucAssetLocaContDetails.FieldAttrs.Add(fieldAttr13);
            this.ucAssetLocaContDetails.LogInfo = null;
            this.ucAssetLocaContDetails.Name = "ucAssetLocaContDetails";
            this.ucAssetLocaContDetails.RowAffectsCheck = true;
            this.ucAssetLocaContDetails.SelectCmd = this.AssetLocaContDetails;
            this.ucAssetLocaContDetails.SelectCmdForUpdate = null;
            this.ucAssetLocaContDetails.SendSQLCmd = true;
            this.ucAssetLocaContDetails.ServerModify = true;
            this.ucAssetLocaContDetails.ServerModifyGetMax = false;
            this.ucAssetLocaContDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucAssetLocaContDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucAssetLocaContDetails.UseTranscationScope = false;
            this.ucAssetLocaContDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucAssetLocaContDetails.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucAssetLocaContDetails_BeforeInsert);
            // 
            // idAssetLocation_AssetLocaContDetails
            // 
            this.idAssetLocation_AssetLocaContDetails.Detail = this.AssetLocaContDetails;
            columnItem1.FieldName = "AssetLocaID";
            this.idAssetLocation_AssetLocaContDetails.DetailColumns.Add(columnItem1);
            this.idAssetLocation_AssetLocaContDetails.DynamicTableName = false;
            this.idAssetLocation_AssetLocaContDetails.Master = this.AssetLocation;
            columnItem2.FieldName = "AssetLocaID";
            this.idAssetLocation_AssetLocaContDetails.MasterColumns.Add(columnItem2);
            // 
            // View_AssetLocation
            // 
            this.View_AssetLocation.CacheConnection = false;
            this.View_AssetLocation.CommandText = "SELECT * FROM dbo.[AssetLocation]";
            this.View_AssetLocation.CommandTimeout = 30;
            this.View_AssetLocation.CommandType = System.Data.CommandType.Text;
            this.View_AssetLocation.DynamicTableName = false;
            this.View_AssetLocation.EEPAlias = null;
            this.View_AssetLocation.EncodingAfter = null;
            this.View_AssetLocation.EncodingBefore = "Windows-1252";
            this.View_AssetLocation.EncodingConvert = null;
            this.View_AssetLocation.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AssetLocaID";
            this.View_AssetLocation.KeyFields.Add(keyItem3);
            this.View_AssetLocation.MultiSetWhere = false;
            this.View_AssetLocation.Name = "View_AssetLocation";
            this.View_AssetLocation.NotificationAutoEnlist = false;
            this.View_AssetLocation.SecExcept = null;
            this.View_AssetLocation.SecFieldName = null;
            this.View_AssetLocation.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_AssetLocation.SelectPaging = false;
            this.View_AssetLocation.SelectTop = 0;
            this.View_AssetLocation.SiteControl = false;
            this.View_AssetLocation.SiteFieldName = null;
            this.View_AssetLocation.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // OwnerList
            // 
            this.OwnerList.CacheConnection = false;
            this.OwnerList.CommandText = "SELECT USERID,USERNAME FROM EIPHRSYS.DBO.USERS \r\nWHERE DESCRIPTION=\'JB\'\r\nORDER BY" +
    " USERNAME";
            this.OwnerList.CommandTimeout = 30;
            this.OwnerList.CommandType = System.Data.CommandType.Text;
            this.OwnerList.DynamicTableName = false;
            this.OwnerList.EEPAlias = null;
            this.OwnerList.EncodingAfter = null;
            this.OwnerList.EncodingBefore = "Windows-1252";
            this.OwnerList.EncodingConvert = null;
            this.OwnerList.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "USERID";
            this.OwnerList.KeyFields.Add(keyItem4);
            this.OwnerList.MultiSetWhere = false;
            this.OwnerList.Name = "OwnerList";
            this.OwnerList.NotificationAutoEnlist = false;
            this.OwnerList.SecExcept = null;
            this.OwnerList.SecFieldName = null;
            this.OwnerList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.OwnerList.SelectPaging = false;
            this.OwnerList.SelectTop = 0;
            this.OwnerList.SiteControl = false;
            this.OwnerList.SiteFieldName = null;
            this.OwnerList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLocaContDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OwnerList)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand AssetLocation;
        private Srvtools.UpdateComponent ucAssetLocation;
        private Srvtools.InfoCommand AssetLocaContDetails;
        private Srvtools.UpdateComponent ucAssetLocaContDetails;
        private Srvtools.InfoDataSource idAssetLocation_AssetLocaContDetails;
        private Srvtools.InfoCommand View_AssetLocation;
        private Srvtools.InfoCommand OwnerList;
    }
}
