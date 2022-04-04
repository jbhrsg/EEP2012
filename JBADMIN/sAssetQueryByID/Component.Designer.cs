namespace sAssetQueryByID
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
            Srvtools.Service service2 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr26 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr27 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr28 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr29 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr30 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr31 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr32 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr33 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr34 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr35 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr36 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr37 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr38 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr39 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr40 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr41 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr42 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr43 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr44 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.AssetMaster = new Srvtools.InfoCommand(this.components);
            this.ucAssetMaster = new Srvtools.UpdateComponent(this.components);
            this.View_AssetMaster = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.AssetGetType = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.AssetLocate = new Srvtools.InfoCommand(this.components);
            this.ItemType = new Srvtools.InfoCommand(this.components);
            this.AssetTranType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetGetType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLocate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetTranType)).BeginInit();
            // 
            // serviceManager1
            // 
            service2.DelegateName = "GetAssetDataByID";
            service2.NonLogin = false;
            service2.ServiceName = "GetAssetDataByID";
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // AssetMaster
            // 
            this.AssetMaster.CacheConnection = false;
            this.AssetMaster.CommandText = resources.GetString("AssetMaster.CommandText");
            this.AssetMaster.CommandTimeout = 30;
            this.AssetMaster.CommandType = System.Data.CommandType.Text;
            this.AssetMaster.DynamicTableName = false;
            this.AssetMaster.EEPAlias = null;
            this.AssetMaster.EncodingAfter = null;
            this.AssetMaster.EncodingBefore = "Windows-1252";
            this.AssetMaster.EncodingConvert = null;
            this.AssetMaster.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "AssetMasterNO";
            this.AssetMaster.KeyFields.Add(keyItem6);
            this.AssetMaster.MultiSetWhere = false;
            this.AssetMaster.Name = "AssetMaster";
            this.AssetMaster.NotificationAutoEnlist = false;
            this.AssetMaster.SecExcept = null;
            this.AssetMaster.SecFieldName = null;
            this.AssetMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetMaster.SelectPaging = false;
            this.AssetMaster.SelectTop = 0;
            this.AssetMaster.SiteControl = false;
            this.AssetMaster.SiteFieldName = null;
            this.AssetMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucAssetMaster
            // 
            this.ucAssetMaster.AutoTrans = true;
            this.ucAssetMaster.ExceptJoin = false;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "AssetMasterNO";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "AssetID";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "AssetName";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "ItemTypeID";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "AssetUnit";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "AssetSpecs";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "AssetGetType";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "AssetGetDate";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "UsefulYears";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "AssetQty";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "AssetPhotoPath";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            fieldAttr34.CheckNull = false;
            fieldAttr34.DataField = "AssetPhotoPath1";
            fieldAttr34.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr34.DefaultValue = null;
            fieldAttr34.TrimLength = 0;
            fieldAttr34.UpdateEnable = true;
            fieldAttr34.WhereMode = true;
            fieldAttr35.CheckNull = false;
            fieldAttr35.DataField = "AssetPlace";
            fieldAttr35.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr35.DefaultValue = null;
            fieldAttr35.TrimLength = 0;
            fieldAttr35.UpdateEnable = true;
            fieldAttr35.WhereMode = true;
            fieldAttr36.CheckNull = false;
            fieldAttr36.DataField = "AssetNotes";
            fieldAttr36.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr36.DefaultValue = null;
            fieldAttr36.TrimLength = 0;
            fieldAttr36.UpdateEnable = true;
            fieldAttr36.WhereMode = true;
            fieldAttr37.CheckNull = false;
            fieldAttr37.DataField = "PONO";
            fieldAttr37.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr37.DefaultValue = null;
            fieldAttr37.TrimLength = 0;
            fieldAttr37.UpdateEnable = true;
            fieldAttr37.WhereMode = true;
            fieldAttr38.CheckNull = false;
            fieldAttr38.DataField = "IsAssetShare";
            fieldAttr38.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr38.DefaultValue = null;
            fieldAttr38.TrimLength = 0;
            fieldAttr38.UpdateEnable = true;
            fieldAttr38.WhereMode = true;
            fieldAttr39.CheckNull = false;
            fieldAttr39.DataField = "IsActive";
            fieldAttr39.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr39.DefaultValue = null;
            fieldAttr39.TrimLength = 0;
            fieldAttr39.UpdateEnable = true;
            fieldAttr39.WhereMode = true;
            fieldAttr40.CheckNull = false;
            fieldAttr40.DataField = "Flowflag";
            fieldAttr40.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr40.DefaultValue = null;
            fieldAttr40.TrimLength = 0;
            fieldAttr40.UpdateEnable = true;
            fieldAttr40.WhereMode = true;
            fieldAttr41.CheckNull = false;
            fieldAttr41.DataField = "CreateBy";
            fieldAttr41.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr41.DefaultValue = null;
            fieldAttr41.TrimLength = 0;
            fieldAttr41.UpdateEnable = true;
            fieldAttr41.WhereMode = true;
            fieldAttr42.CheckNull = false;
            fieldAttr42.DataField = "CreateDate";
            fieldAttr42.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr42.DefaultValue = null;
            fieldAttr42.TrimLength = 0;
            fieldAttr42.UpdateEnable = true;
            fieldAttr42.WhereMode = true;
            fieldAttr43.CheckNull = false;
            fieldAttr43.DataField = "LastUpdateBy";
            fieldAttr43.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr43.DefaultValue = null;
            fieldAttr43.TrimLength = 0;
            fieldAttr43.UpdateEnable = true;
            fieldAttr43.WhereMode = true;
            fieldAttr44.CheckNull = false;
            fieldAttr44.DataField = "LastUpdateDate";
            fieldAttr44.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr44.DefaultValue = null;
            fieldAttr44.TrimLength = 0;
            fieldAttr44.UpdateEnable = true;
            fieldAttr44.WhereMode = true;
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr23);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr24);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr25);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr26);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr27);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr28);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr29);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr30);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr31);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr32);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr33);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr34);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr35);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr36);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr37);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr38);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr39);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr40);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr41);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr42);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr43);
            this.ucAssetMaster.FieldAttrs.Add(fieldAttr44);
            this.ucAssetMaster.LogInfo = null;
            this.ucAssetMaster.Name = "ucAssetMaster";
            this.ucAssetMaster.RowAffectsCheck = true;
            this.ucAssetMaster.SelectCmd = this.AssetMaster;
            this.ucAssetMaster.SelectCmdForUpdate = null;
            this.ucAssetMaster.SendSQLCmd = true;
            this.ucAssetMaster.ServerModify = true;
            this.ucAssetMaster.ServerModifyGetMax = false;
            this.ucAssetMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucAssetMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucAssetMaster.UseTranscationScope = false;
            this.ucAssetMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_AssetMaster
            // 
            this.View_AssetMaster.CacheConnection = false;
            this.View_AssetMaster.CommandText = "SELECT * FROM dbo.[AssetMaster]";
            this.View_AssetMaster.CommandTimeout = 30;
            this.View_AssetMaster.CommandType = System.Data.CommandType.Text;
            this.View_AssetMaster.DynamicTableName = false;
            this.View_AssetMaster.EEPAlias = null;
            this.View_AssetMaster.EncodingAfter = null;
            this.View_AssetMaster.EncodingBefore = "Windows-1252";
            this.View_AssetMaster.EncodingConvert = null;
            this.View_AssetMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AssetMasterNO";
            this.View_AssetMaster.KeyFields.Add(keyItem1);
            this.View_AssetMaster.MultiSetWhere = false;
            this.View_AssetMaster.Name = "View_AssetMaster";
            this.View_AssetMaster.NotificationAutoEnlist = false;
            this.View_AssetMaster.SecExcept = null;
            this.View_AssetMaster.SecFieldName = null;
            this.View_AssetMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_AssetMaster.SelectPaging = false;
            this.View_AssetMaster.SelectTop = 0;
            this.View_AssetMaster.SiteControl = false;
            this.View_AssetMaster.SiteFieldName = null;
            this.View_AssetMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "select InsGroup.InsGroupID,\r\n            InsGroupShortName  as InsGroupName\r\nfrom" +
    " InsGroup \r\nwhere IsAssetControl=1\r\norder by InsGroup.InsGroupID";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = null;
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.InfoConnection1;
            this.InsGroup.MultiSetWhere = false;
            this.InsGroup.Name = "InsGroup";
            this.InsGroup.NotificationAutoEnlist = false;
            this.InsGroup.SecExcept = null;
            this.InsGroup.SecFieldName = null;
            this.InsGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InsGroup.SelectPaging = false;
            this.InsGroup.SelectTop = 0;
            this.InsGroup.SiteControl = false;
            this.InsGroup.SiteFieldName = null;
            this.InsGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AssetGetType
            // 
            this.AssetGetType.CacheConnection = false;
            this.AssetGetType.CommandText = "SELECT \'購置\' As AssetGetType\r\nUNION\r\nSELECT  \'贈與\' As AssetGetType\r\nSELECT DISTINCT" +
    " AssetGetType\r\nFROM ASSETMASTER\r\nWHERE AssetGetType IS NOT Null or AssetGetType!" +
    "=\'\'\r\n";
            this.AssetGetType.CommandTimeout = 30;
            this.AssetGetType.CommandType = System.Data.CommandType.Text;
            this.AssetGetType.DynamicTableName = false;
            this.AssetGetType.EEPAlias = null;
            this.AssetGetType.EncodingAfter = null;
            this.AssetGetType.EncodingBefore = "Windows-1252";
            this.AssetGetType.EncodingConvert = null;
            this.AssetGetType.InfoConnection = this.InfoConnection1;
            this.AssetGetType.MultiSetWhere = false;
            this.AssetGetType.Name = "AssetGetType";
            this.AssetGetType.NotificationAutoEnlist = false;
            this.AssetGetType.SecExcept = null;
            this.AssetGetType.SecFieldName = null;
            this.AssetGetType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetGetType.SelectPaging = false;
            this.AssetGetType.SelectTop = 0;
            this.AssetGetType.SiteControl = false;
            this.AssetGetType.SiteFieldName = null;
            this.AssetGetType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = resources.GetString("Employee.CommandText");
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem2);
            this.Employee.MultiSetWhere = false;
            this.Employee.Name = "Employee";
            this.Employee.NotificationAutoEnlist = false;
            this.Employee.SecExcept = null;
            this.Employee.SecFieldName = null;
            this.Employee.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Employee.SelectPaging = false;
            this.Employee.SelectTop = 0;
            this.Employee.SiteControl = false;
            this.Employee.SiteFieldName = null;
            this.Employee.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AssetLocate
            // 
            this.AssetLocate.CacheConnection = false;
            this.AssetLocate.CommandText = resources.GetString("AssetLocate.CommandText");
            this.AssetLocate.CommandTimeout = 30;
            this.AssetLocate.CommandType = System.Data.CommandType.Text;
            this.AssetLocate.DynamicTableName = false;
            this.AssetLocate.EEPAlias = null;
            this.AssetLocate.EncodingAfter = null;
            this.AssetLocate.EncodingBefore = "Windows-1252";
            this.AssetLocate.EncodingConvert = null;
            this.AssetLocate.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AssetLocaID";
            this.AssetLocate.KeyFields.Add(keyItem3);
            this.AssetLocate.MultiSetWhere = false;
            this.AssetLocate.Name = "AssetLocate";
            this.AssetLocate.NotificationAutoEnlist = false;
            this.AssetLocate.SecExcept = null;
            this.AssetLocate.SecFieldName = null;
            this.AssetLocate.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetLocate.SelectPaging = false;
            this.AssetLocate.SelectTop = 0;
            this.AssetLocate.SiteControl = false;
            this.AssetLocate.SiteFieldName = null;
            this.AssetLocate.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ItemType
            // 
            this.ItemType.CacheConnection = false;
            this.ItemType.CommandText = "SELECT ITEMTYPEID,ITEMTYPENAME FROM  ITEMTYPE\r\nORDER BY ITEMTYPEID";
            this.ItemType.CommandTimeout = 30;
            this.ItemType.CommandType = System.Data.CommandType.Text;
            this.ItemType.DynamicTableName = false;
            this.ItemType.EEPAlias = null;
            this.ItemType.EncodingAfter = null;
            this.ItemType.EncodingBefore = "Windows-1252";
            this.ItemType.EncodingConvert = null;
            this.ItemType.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ITEMTYPEID";
            this.ItemType.KeyFields.Add(keyItem4);
            this.ItemType.MultiSetWhere = false;
            this.ItemType.Name = "ItemType";
            this.ItemType.NotificationAutoEnlist = false;
            this.ItemType.SecExcept = null;
            this.ItemType.SecFieldName = null;
            this.ItemType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ItemType.SelectPaging = false;
            this.ItemType.SelectTop = 0;
            this.ItemType.SiteControl = false;
            this.ItemType.SiteFieldName = null;
            this.ItemType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AssetTranType
            // 
            this.AssetTranType.CacheConnection = false;
            this.AssetTranType.CommandText = "SELECT * FROM ASSETTRANTYPE\r\nORDER BY  TRANTYPEID\r\n";
            this.AssetTranType.CommandTimeout = 30;
            this.AssetTranType.CommandType = System.Data.CommandType.Text;
            this.AssetTranType.DynamicTableName = false;
            this.AssetTranType.EEPAlias = null;
            this.AssetTranType.EncodingAfter = null;
            this.AssetTranType.EncodingBefore = "Windows-1252";
            this.AssetTranType.EncodingConvert = null;
            this.AssetTranType.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "TranTypeID";
            this.AssetTranType.KeyFields.Add(keyItem5);
            this.AssetTranType.MultiSetWhere = false;
            this.AssetTranType.Name = "AssetTranType";
            this.AssetTranType.NotificationAutoEnlist = false;
            this.AssetTranType.SecExcept = null;
            this.AssetTranType.SecFieldName = null;
            this.AssetTranType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetTranType.SelectPaging = false;
            this.AssetTranType.SelectTop = 0;
            this.AssetTranType.SiteControl = false;
            this.AssetTranType.SiteFieldName = null;
            this.AssetTranType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetGetType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLocate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetTranType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand AssetMaster;
        private Srvtools.UpdateComponent ucAssetMaster;
        private Srvtools.InfoCommand View_AssetMaster;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoCommand AssetGetType;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand AssetLocate;
        private Srvtools.InfoCommand ItemType;
        private Srvtools.InfoCommand AssetTranType;
    }
}
