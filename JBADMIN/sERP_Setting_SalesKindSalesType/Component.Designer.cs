namespace sERP_Setting_SalesKindSalesType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SalesKind = new Srvtools.InfoCommand(this.components);
            this.ucSalesKind = new Srvtools.UpdateComponent(this.components);
            this.SalesKindSalesType = new Srvtools.InfoCommand(this.components);
            this.ucSalesKindSalesType = new Srvtools.UpdateComponent(this.components);
            this.SalesType_dataForm1 = new Srvtools.InfoCommand(this.components);
            this.ucSalesType = new Srvtools.UpdateComponent(this.components);
            this.View_SalesKind = new Srvtools.InfoCommand(this.components);
            this.View_SalesKindSalesType = new Srvtools.InfoCommand(this.components);
            this.View_SalesType = new Srvtools.InfoCommand(this.components);
            this.SalesTypeTree = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesKindSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType_dataForm1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesKindSalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTypeTree)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "SaveKindSaleType";
            service1.NonLogin = false;
            service1.ServiceName = "SaveKindSaleType";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // SalesKind
            // 
            this.SalesKind.CacheConnection = false;
            this.SalesKind.CommandText = "SELECT dbo.[SalesKind].* ,\r\nJBERP.dbo.funReturnSalesKindSaleType(SalesKindID) As " +
    "KindSaleTypeIDS\r\nFROM dbo.[SalesKind] order by SalesKindID";
            this.SalesKind.CommandTimeout = 30;
            this.SalesKind.CommandType = System.Data.CommandType.Text;
            this.SalesKind.DynamicTableName = false;
            this.SalesKind.EEPAlias = "JBERP";
            this.SalesKind.EncodingAfter = null;
            this.SalesKind.EncodingBefore = "Windows-1252";
            this.SalesKind.EncodingConvert = null;
            this.SalesKind.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesKindID";
            this.SalesKind.KeyFields.Add(keyItem1);
            this.SalesKind.MultiSetWhere = false;
            this.SalesKind.Name = "SalesKind";
            this.SalesKind.NotificationAutoEnlist = false;
            this.SalesKind.SecExcept = null;
            this.SalesKind.SecFieldName = null;
            this.SalesKind.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesKind.SelectPaging = false;
            this.SalesKind.SelectTop = 0;
            this.SalesKind.SiteControl = false;
            this.SalesKind.SiteFieldName = null;
            this.SalesKind.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesKind
            // 
            this.ucSalesKind.AutoTrans = true;
            this.ucSalesKind.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SalesKindID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesKindName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateBy";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr4.DefaultValue = "_username";
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr5.DefaultValue = "";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LastUpdateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr6.DefaultValue = "_username";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = "";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucSalesKind.FieldAttrs.Add(fieldAttr1);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr2);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr3);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr4);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr5);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr6);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr7);
            this.ucSalesKind.LogInfo = null;
            this.ucSalesKind.Name = "ucSalesKind";
            this.ucSalesKind.RowAffectsCheck = true;
            this.ucSalesKind.SelectCmd = this.SalesKind;
            this.ucSalesKind.SelectCmdForUpdate = null;
            this.ucSalesKind.SendSQLCmd = true;
            this.ucSalesKind.ServerModify = true;
            this.ucSalesKind.ServerModifyGetMax = false;
            this.ucSalesKind.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesKind.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesKind.UseTranscationScope = false;
            this.ucSalesKind.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucSalesKind.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucSalesKind_BeforeInsert);
            this.ucSalesKind.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucSalesKind_BeforeModify);
            // 
            // SalesKindSalesType
            // 
            this.SalesKindSalesType.CacheConnection = false;
            this.SalesKindSalesType.CommandText = resources.GetString("SalesKindSalesType.CommandText");
            this.SalesKindSalesType.CommandTimeout = 30;
            this.SalesKindSalesType.CommandType = System.Data.CommandType.Text;
            this.SalesKindSalesType.DynamicTableName = false;
            this.SalesKindSalesType.EEPAlias = "JBERP";
            this.SalesKindSalesType.EncodingAfter = null;
            this.SalesKindSalesType.EncodingBefore = "Windows-1252";
            this.SalesKindSalesType.EncodingConvert = null;
            this.SalesKindSalesType.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SalesKindID";
            keyItem3.KeyName = "SalesTypeID";
            this.SalesKindSalesType.KeyFields.Add(keyItem2);
            this.SalesKindSalesType.KeyFields.Add(keyItem3);
            this.SalesKindSalesType.MultiSetWhere = false;
            this.SalesKindSalesType.Name = "SalesKindSalesType";
            this.SalesKindSalesType.NotificationAutoEnlist = false;
            this.SalesKindSalesType.SecExcept = null;
            this.SalesKindSalesType.SecFieldName = null;
            this.SalesKindSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesKindSalesType.SelectPaging = false;
            this.SalesKindSalesType.SelectTop = 0;
            this.SalesKindSalesType.SiteControl = false;
            this.SalesKindSalesType.SiteFieldName = null;
            this.SalesKindSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesKindSalesType
            // 
            this.ucSalesKindSalesType.AutoTrans = true;
            this.ucSalesKindSalesType.ExceptJoin = false;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AutoKey";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "SalesKindID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "SalesTypeID";
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
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = "_username";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpdateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = "_sysdate";
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            this.ucSalesKindSalesType.FieldAttrs.Add(fieldAttr8);
            this.ucSalesKindSalesType.FieldAttrs.Add(fieldAttr9);
            this.ucSalesKindSalesType.FieldAttrs.Add(fieldAttr10);
            this.ucSalesKindSalesType.FieldAttrs.Add(fieldAttr11);
            this.ucSalesKindSalesType.FieldAttrs.Add(fieldAttr12);
            this.ucSalesKindSalesType.FieldAttrs.Add(fieldAttr13);
            this.ucSalesKindSalesType.FieldAttrs.Add(fieldAttr14);
            this.ucSalesKindSalesType.LogInfo = null;
            this.ucSalesKindSalesType.Name = "ucSalesKindSalesType";
            this.ucSalesKindSalesType.RowAffectsCheck = true;
            this.ucSalesKindSalesType.SelectCmd = this.SalesKindSalesType;
            this.ucSalesKindSalesType.SelectCmdForUpdate = null;
            this.ucSalesKindSalesType.SendSQLCmd = true;
            this.ucSalesKindSalesType.ServerModify = true;
            this.ucSalesKindSalesType.ServerModifyGetMax = false;
            this.ucSalesKindSalesType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesKindSalesType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesKindSalesType.UseTranscationScope = false;
            this.ucSalesKindSalesType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // SalesType_dataForm1
            // 
            this.SalesType_dataForm1.CacheConnection = false;
            this.SalesType_dataForm1.CommandText = "SELECT SalesTypeID,SalesTypeName FROM dbo.[SalesType]\r\nwhere SalesTypeID not in (" +
    "select SalesTypeID from dbo.[SalesKindSalesType])";
            this.SalesType_dataForm1.CommandTimeout = 30;
            this.SalesType_dataForm1.CommandType = System.Data.CommandType.Text;
            this.SalesType_dataForm1.DynamicTableName = false;
            this.SalesType_dataForm1.EEPAlias = "JBERP";
            this.SalesType_dataForm1.EncodingAfter = null;
            this.SalesType_dataForm1.EncodingBefore = "Windows-1252";
            this.SalesType_dataForm1.EncodingConvert = null;
            this.SalesType_dataForm1.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AutoKey";
            this.SalesType_dataForm1.KeyFields.Add(keyItem4);
            this.SalesType_dataForm1.MultiSetWhere = false;
            this.SalesType_dataForm1.Name = "SalesType_dataForm1";
            this.SalesType_dataForm1.NotificationAutoEnlist = false;
            this.SalesType_dataForm1.SecExcept = null;
            this.SalesType_dataForm1.SecFieldName = null;
            this.SalesType_dataForm1.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesType_dataForm1.SelectPaging = false;
            this.SalesType_dataForm1.SelectTop = 0;
            this.SalesType_dataForm1.SiteControl = false;
            this.SalesType_dataForm1.SiteFieldName = null;
            this.SalesType_dataForm1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesType
            // 
            this.ucSalesType.AutoTrans = true;
            this.ucSalesType.ExceptJoin = false;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "AutoKey";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "SalesTypeID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "SalesTypeName";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "InsGroupID";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "Unit";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "SalesKindID";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CreateBy";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "CreateDate";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "LastUpdateBy";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "LastUpdateDate";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            this.ucSalesType.FieldAttrs.Add(fieldAttr15);
            this.ucSalesType.FieldAttrs.Add(fieldAttr16);
            this.ucSalesType.FieldAttrs.Add(fieldAttr17);
            this.ucSalesType.FieldAttrs.Add(fieldAttr18);
            this.ucSalesType.FieldAttrs.Add(fieldAttr19);
            this.ucSalesType.FieldAttrs.Add(fieldAttr20);
            this.ucSalesType.FieldAttrs.Add(fieldAttr21);
            this.ucSalesType.FieldAttrs.Add(fieldAttr22);
            this.ucSalesType.FieldAttrs.Add(fieldAttr23);
            this.ucSalesType.FieldAttrs.Add(fieldAttr24);
            this.ucSalesType.LogInfo = null;
            this.ucSalesType.Name = "ucSalesType";
            this.ucSalesType.RowAffectsCheck = true;
            this.ucSalesType.SelectCmd = null;
            this.ucSalesType.SelectCmdForUpdate = null;
            this.ucSalesType.SendSQLCmd = true;
            this.ucSalesType.ServerModify = true;
            this.ucSalesType.ServerModifyGetMax = false;
            this.ucSalesType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesType.UseTranscationScope = false;
            this.ucSalesType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_SalesKind
            // 
            this.View_SalesKind.CacheConnection = false;
            this.View_SalesKind.CommandText = "SELECT * FROM dbo.[SalesKind]";
            this.View_SalesKind.CommandTimeout = 30;
            this.View_SalesKind.CommandType = System.Data.CommandType.Text;
            this.View_SalesKind.DynamicTableName = false;
            this.View_SalesKind.EEPAlias = null;
            this.View_SalesKind.EncodingAfter = null;
            this.View_SalesKind.EncodingBefore = "Windows-1252";
            this.View_SalesKind.EncodingConvert = null;
            this.View_SalesKind.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "AutoKey";
            this.View_SalesKind.KeyFields.Add(keyItem5);
            this.View_SalesKind.MultiSetWhere = false;
            this.View_SalesKind.Name = "View_SalesKind";
            this.View_SalesKind.NotificationAutoEnlist = false;
            this.View_SalesKind.SecExcept = null;
            this.View_SalesKind.SecFieldName = null;
            this.View_SalesKind.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesKind.SelectPaging = false;
            this.View_SalesKind.SelectTop = 0;
            this.View_SalesKind.SiteControl = false;
            this.View_SalesKind.SiteFieldName = null;
            this.View_SalesKind.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_SalesKindSalesType
            // 
            this.View_SalesKindSalesType.CacheConnection = false;
            this.View_SalesKindSalesType.CommandText = "SELECT * FROM dbo.[SalesKindSalesType]";
            this.View_SalesKindSalesType.CommandTimeout = 30;
            this.View_SalesKindSalesType.CommandType = System.Data.CommandType.Text;
            this.View_SalesKindSalesType.DynamicTableName = false;
            this.View_SalesKindSalesType.EEPAlias = "JBERP";
            this.View_SalesKindSalesType.EncodingAfter = null;
            this.View_SalesKindSalesType.EncodingBefore = "Windows-1252";
            this.View_SalesKindSalesType.EncodingConvert = null;
            this.View_SalesKindSalesType.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "SalesKindID";
            keyItem7.KeyName = "SalesTypeID";
            this.View_SalesKindSalesType.KeyFields.Add(keyItem6);
            this.View_SalesKindSalesType.KeyFields.Add(keyItem7);
            this.View_SalesKindSalesType.MultiSetWhere = false;
            this.View_SalesKindSalesType.Name = "View_SalesKindSalesType";
            this.View_SalesKindSalesType.NotificationAutoEnlist = false;
            this.View_SalesKindSalesType.SecExcept = null;
            this.View_SalesKindSalesType.SecFieldName = null;
            this.View_SalesKindSalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesKindSalesType.SelectPaging = false;
            this.View_SalesKindSalesType.SelectTop = 0;
            this.View_SalesKindSalesType.SiteControl = false;
            this.View_SalesKindSalesType.SiteFieldName = null;
            this.View_SalesKindSalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_SalesType
            // 
            this.View_SalesType.CacheConnection = false;
            this.View_SalesType.CommandText = "SELECT * FROM dbo.[SalesType]";
            this.View_SalesType.CommandTimeout = 30;
            this.View_SalesType.CommandType = System.Data.CommandType.Text;
            this.View_SalesType.DynamicTableName = false;
            this.View_SalesType.EEPAlias = null;
            this.View_SalesType.EncodingAfter = null;
            this.View_SalesType.EncodingBefore = "Windows-1252";
            this.View_SalesType.EncodingConvert = null;
            this.View_SalesType.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "AutoKey";
            this.View_SalesType.KeyFields.Add(keyItem8);
            this.View_SalesType.MultiSetWhere = false;
            this.View_SalesType.Name = "View_SalesType";
            this.View_SalesType.NotificationAutoEnlist = false;
            this.View_SalesType.SecExcept = null;
            this.View_SalesType.SecFieldName = null;
            this.View_SalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesType.SelectPaging = false;
            this.View_SalesType.SelectTop = 0;
            this.View_SalesType.SiteControl = false;
            this.View_SalesType.SiteFieldName = null;
            this.View_SalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesTypeTree
            // 
            this.SalesTypeTree.CacheConnection = false;
            this.SalesTypeTree.CommandText = resources.GetString("SalesTypeTree.CommandText");
            this.SalesTypeTree.CommandTimeout = 30;
            this.SalesTypeTree.CommandType = System.Data.CommandType.Text;
            this.SalesTypeTree.DynamicTableName = false;
            this.SalesTypeTree.EEPAlias = "JBERP";
            this.SalesTypeTree.EncodingAfter = null;
            this.SalesTypeTree.EncodingBefore = "Windows-1252";
            this.SalesTypeTree.EncodingConvert = null;
            this.SalesTypeTree.InfoConnection = this.InfoConnection1;
            this.SalesTypeTree.MultiSetWhere = false;
            this.SalesTypeTree.Name = "SalesTypeTree";
            this.SalesTypeTree.NotificationAutoEnlist = false;
            this.SalesTypeTree.SecExcept = null;
            this.SalesTypeTree.SecFieldName = null;
            this.SalesTypeTree.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesTypeTree.SelectPaging = false;
            this.SalesTypeTree.SelectTop = 0;
            this.SalesTypeTree.SiteControl = false;
            this.SalesTypeTree.SiteFieldName = null;
            this.SalesTypeTree.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesKindSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType_dataForm1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesKindSalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesTypeTree)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesKind;
        private Srvtools.UpdateComponent ucSalesKind;
        private Srvtools.InfoCommand SalesKindSalesType;
        private Srvtools.UpdateComponent ucSalesKindSalesType;
        private Srvtools.InfoCommand SalesType_dataForm1;
        private Srvtools.UpdateComponent ucSalesType;
        private Srvtools.InfoCommand View_SalesKind;
        private Srvtools.InfoCommand View_SalesKindSalesType;
        private Srvtools.InfoCommand View_SalesType;
        private Srvtools.InfoCommand SalesTypeTree;
    }
}
