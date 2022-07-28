namespace sERP_Setting_Item
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
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
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
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Item = new Srvtools.InfoCommand(this.components);
            this.ucItem = new Srvtools.UpdateComponent(this.components);
            this.ItemType = new Srvtools.InfoCommand(this.components);
            this.ucItemType = new Srvtools.UpdateComponent(this.components);
            this.View_Item = new Srvtools.InfoCommand(this.components);
            this.View_ItemType = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.ResponsibleGROUPID = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.autoNumber2 = new Srvtools.AutoNumber(this.components);
            this.ItemTypeForDG0 = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ItemType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResponsibleGROUPID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTypeForDG0)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDuplicate_ItemTypeName";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDuplicate_ItemTypeName";
            service2.DelegateName = "CheckDuplicate_ItemName";
            service2.NonLogin = false;
            service2.ServiceName = "CheckDuplicate_ItemName";
            service3.DelegateName = "CheckItems";
            service3.NonLogin = false;
            service3.ServiceName = "CheckItems";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // Item
            // 
            this.Item.CacheConnection = false;
            this.Item.CommandText = "SELECT dbo.[Item].* FROM dbo.[Item]";
            this.Item.CommandTimeout = 30;
            this.Item.CommandType = System.Data.CommandType.Text;
            this.Item.DynamicTableName = false;
            this.Item.EEPAlias = "JBERP";
            this.Item.EncodingAfter = null;
            this.Item.EncodingBefore = "Windows-1252";
            this.Item.EncodingConvert = null;
            this.Item.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ItemID";
            this.Item.KeyFields.Add(keyItem1);
            this.Item.MultiSetWhere = false;
            this.Item.Name = "Item";
            this.Item.NotificationAutoEnlist = false;
            this.Item.SecExcept = null;
            this.Item.SecFieldName = null;
            this.Item.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Item.SelectPaging = false;
            this.Item.SelectTop = 0;
            this.Item.SiteControl = false;
            this.Item.SiteFieldName = null;
            this.Item.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucItem
            // 
            this.ucItem.AutoTrans = true;
            this.ucItem.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ItemID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ItemName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ItemTypeID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateBy";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = "_username";
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = "_sysdate";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LastUpdateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr6.DefaultValue = "_username";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr7.DefaultValue = "_sysdate";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucItem.FieldAttrs.Add(fieldAttr1);
            this.ucItem.FieldAttrs.Add(fieldAttr2);
            this.ucItem.FieldAttrs.Add(fieldAttr3);
            this.ucItem.FieldAttrs.Add(fieldAttr4);
            this.ucItem.FieldAttrs.Add(fieldAttr5);
            this.ucItem.FieldAttrs.Add(fieldAttr6);
            this.ucItem.FieldAttrs.Add(fieldAttr7);
            this.ucItem.LogInfo = null;
            this.ucItem.Name = "ucItem";
            this.ucItem.RowAffectsCheck = true;
            this.ucItem.SelectCmd = this.Item;
            this.ucItem.SelectCmdForUpdate = null;
            this.ucItem.SendSQLCmd = true;
            this.ucItem.ServerModify = true;
            this.ucItem.ServerModifyGetMax = false;
            this.ucItem.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucItem.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucItem.UseTranscationScope = false;
            this.ucItem.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ItemType
            // 
            this.ItemType.CacheConnection = false;
            this.ItemType.CommandText = "SELECT it.*,g.GROUPNAME FROM dbo.[ItemType] it\r\nleft join EIPHRSYS.dbo.[GROUPS] g" +
    " on it.ResponsibleGROUPID=g.GROUPID";
            this.ItemType.CommandTimeout = 30;
            this.ItemType.CommandType = System.Data.CommandType.Text;
            this.ItemType.DynamicTableName = false;
            this.ItemType.EEPAlias = "JBERP";
            this.ItemType.EncodingAfter = null;
            this.ItemType.EncodingBefore = "Windows-1252";
            this.ItemType.EncodingConvert = null;
            this.ItemType.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ItemTypeID";
            this.ItemType.KeyFields.Add(keyItem2);
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
            // ucItemType
            // 
            this.ucItemType.AutoTrans = true;
            this.ucItemType.ExceptJoin = false;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ItemTypeID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ItemTypeName";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ResponsibleGROUPID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = "_username";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = "_sysdate";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr13.DefaultValue = "_username";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpdateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr14.DefaultValue = "_sysdate";
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            this.ucItemType.FieldAttrs.Add(fieldAttr8);
            this.ucItemType.FieldAttrs.Add(fieldAttr9);
            this.ucItemType.FieldAttrs.Add(fieldAttr10);
            this.ucItemType.FieldAttrs.Add(fieldAttr11);
            this.ucItemType.FieldAttrs.Add(fieldAttr12);
            this.ucItemType.FieldAttrs.Add(fieldAttr13);
            this.ucItemType.FieldAttrs.Add(fieldAttr14);
            this.ucItemType.LogInfo = null;
            this.ucItemType.Name = "ucItemType";
            this.ucItemType.RowAffectsCheck = true;
            this.ucItemType.SelectCmd = this.ItemType;
            this.ucItemType.SelectCmdForUpdate = null;
            this.ucItemType.SendSQLCmd = true;
            this.ucItemType.ServerModify = true;
            this.ucItemType.ServerModifyGetMax = false;
            this.ucItemType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucItemType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucItemType.UseTranscationScope = false;
            this.ucItemType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_Item
            // 
            this.View_Item.CacheConnection = false;
            this.View_Item.CommandText = "SELECT * FROM dbo.[Item]";
            this.View_Item.CommandTimeout = 30;
            this.View_Item.CommandType = System.Data.CommandType.Text;
            this.View_Item.DynamicTableName = false;
            this.View_Item.EEPAlias = null;
            this.View_Item.EncodingAfter = null;
            this.View_Item.EncodingBefore = "Windows-1252";
            this.View_Item.EncodingConvert = null;
            this.View_Item.InfoConnection = this.InfoConnection1;
            this.View_Item.MultiSetWhere = false;
            this.View_Item.Name = "View_Item";
            this.View_Item.NotificationAutoEnlist = false;
            this.View_Item.SecExcept = null;
            this.View_Item.SecFieldName = null;
            this.View_Item.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Item.SelectPaging = false;
            this.View_Item.SelectTop = 0;
            this.View_Item.SiteControl = false;
            this.View_Item.SiteFieldName = null;
            this.View_Item.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_ItemType
            // 
            this.View_ItemType.CacheConnection = false;
            this.View_ItemType.CommandText = "SELECT * FROM dbo.[ItemType]";
            this.View_ItemType.CommandTimeout = 30;
            this.View_ItemType.CommandType = System.Data.CommandType.Text;
            this.View_ItemType.DynamicTableName = false;
            this.View_ItemType.EEPAlias = null;
            this.View_ItemType.EncodingAfter = null;
            this.View_ItemType.EncodingBefore = "Windows-1252";
            this.View_ItemType.EncodingConvert = null;
            this.View_ItemType.InfoConnection = this.InfoConnection1;
            this.View_ItemType.MultiSetWhere = false;
            this.View_ItemType.Name = "View_ItemType";
            this.View_ItemType.NotificationAutoEnlist = false;
            this.View_ItemType.SecExcept = null;
            this.View_ItemType.SecFieldName = null;
            this.View_ItemType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ItemType.SelectPaging = false;
            this.View_ItemType.SelectTop = 0;
            this.View_ItemType.SiteControl = false;
            this.View_ItemType.SiteFieldName = null;
            this.View_ItemType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "EIPHRSYS";
            // 
            // ResponsibleGROUPID
            // 
            this.ResponsibleGROUPID.CacheConnection = false;
            this.ResponsibleGROUPID.CommandText = resources.GetString("ResponsibleGROUPID.CommandText");
            this.ResponsibleGROUPID.CommandTimeout = 30;
            this.ResponsibleGROUPID.CommandType = System.Data.CommandType.Text;
            this.ResponsibleGROUPID.DynamicTableName = false;
            this.ResponsibleGROUPID.EEPAlias = "EIPHRSYS";
            this.ResponsibleGROUPID.EncodingAfter = null;
            this.ResponsibleGROUPID.EncodingBefore = "Windows-1252";
            this.ResponsibleGROUPID.EncodingConvert = null;
            this.ResponsibleGROUPID.InfoConnection = this.infoConnection2;
            this.ResponsibleGROUPID.MultiSetWhere = false;
            this.ResponsibleGROUPID.Name = "ResponsibleGROUPID";
            this.ResponsibleGROUPID.NotificationAutoEnlist = false;
            this.ResponsibleGROUPID.SecExcept = null;
            this.ResponsibleGROUPID.SecFieldName = null;
            this.ResponsibleGROUPID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ResponsibleGROUPID.SelectPaging = false;
            this.ResponsibleGROUPID.SelectTop = 0;
            this.ResponsibleGROUPID.SiteControl = false;
            this.ResponsibleGROUPID.SiteFieldName = null;
            this.ResponsibleGROUPID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "ItemTypeID";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "IT";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 4;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "ItemTypeID";
            this.autoNumber1.UpdateComp = this.ucItemType;
            // 
            // autoNumber2
            // 
            this.autoNumber2.Active = true;
            this.autoNumber2.AutoNoID = "ItemID";
            this.autoNumber2.Description = null;
            this.autoNumber2.GetFixed = "I";
            this.autoNumber2.isNumFill = false;
            this.autoNumber2.Name = "autoNumber2";
            this.autoNumber2.Number = null;
            this.autoNumber2.NumDig = 5;
            this.autoNumber2.OldVersion = false;
            this.autoNumber2.OverFlow = true;
            this.autoNumber2.StartValue = 1;
            this.autoNumber2.Step = 1;
            this.autoNumber2.TargetColumn = "ItemID";
            this.autoNumber2.UpdateComp = this.ucItem;
            // 
            // ItemTypeForDG0
            // 
            this.ItemTypeForDG0.CacheConnection = false;
            this.ItemTypeForDG0.CommandText = "SELECT ItemTypeID,ItemTypeName FROM dbo.[ItemType]";
            this.ItemTypeForDG0.CommandTimeout = 30;
            this.ItemTypeForDG0.CommandType = System.Data.CommandType.Text;
            this.ItemTypeForDG0.DynamicTableName = false;
            this.ItemTypeForDG0.EEPAlias = "JBERP";
            this.ItemTypeForDG0.EncodingAfter = null;
            this.ItemTypeForDG0.EncodingBefore = "Windows-1252";
            this.ItemTypeForDG0.EncodingConvert = null;
            this.ItemTypeForDG0.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ItemTypeID";
            this.ItemTypeForDG0.KeyFields.Add(keyItem3);
            this.ItemTypeForDG0.MultiSetWhere = false;
            this.ItemTypeForDG0.Name = "ItemTypeForDG0";
            this.ItemTypeForDG0.NotificationAutoEnlist = false;
            this.ItemTypeForDG0.SecExcept = null;
            this.ItemTypeForDG0.SecFieldName = null;
            this.ItemTypeForDG0.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ItemTypeForDG0.SelectPaging = false;
            this.ItemTypeForDG0.SelectTop = 0;
            this.ItemTypeForDG0.SiteControl = false;
            this.ItemTypeForDG0.SiteFieldName = null;
            this.ItemTypeForDG0.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ItemType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResponsibleGROUPID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTypeForDG0)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Item;
        private Srvtools.UpdateComponent ucItem;
        private Srvtools.InfoCommand ItemType;
        private Srvtools.UpdateComponent ucItemType;
        private Srvtools.InfoCommand View_Item;
        private Srvtools.InfoCommand View_ItemType;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand ResponsibleGROUPID;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.AutoNumber autoNumber2;
        private Srvtools.InfoCommand ItemTypeForDG0;
    }
}
