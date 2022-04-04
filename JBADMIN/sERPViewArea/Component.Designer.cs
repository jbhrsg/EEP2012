namespace sERPViewArea
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPViewArea = new Srvtools.InfoCommand(this.components);
            this.ucERPViewArea = new Srvtools.UpdateComponent(this.components);
            this.ERPDMType = new Srvtools.InfoCommand(this.components);
            this.ucERPDMType = new Srvtools.UpdateComponent(this.components);
            this.View_ERPViewArea = new Srvtools.InfoCommand(this.components);
            this.View_ERPDMType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPViewArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDMType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPViewArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPDMType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDMTypeID";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDMTypeID";
            service2.DelegateName = "CheckViewAreaID";
            service2.NonLogin = false;
            service2.ServiceName = "CheckViewAreaID";
            service3.DelegateName = "CheckDelViewArea";
            service3.NonLogin = false;
            service3.ServiceName = "CheckDelViewArea";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPViewArea
            // 
            this.ERPViewArea.CacheConnection = false;
            this.ERPViewArea.CommandText = "SELECT dbo.[ERPViewArea].* FROM dbo.[ERPViewArea]";
            this.ERPViewArea.CommandTimeout = 30;
            this.ERPViewArea.CommandType = System.Data.CommandType.Text;
            this.ERPViewArea.DynamicTableName = false;
            this.ERPViewArea.EEPAlias = null;
            this.ERPViewArea.EncodingAfter = null;
            this.ERPViewArea.EncodingBefore = "Windows-1252";
            this.ERPViewArea.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ViewAreaNO";
            this.ERPViewArea.KeyFields.Add(keyItem1);
            this.ERPViewArea.MultiSetWhere = false;
            this.ERPViewArea.Name = "ERPViewArea";
            this.ERPViewArea.NotificationAutoEnlist = false;
            this.ERPViewArea.SecExcept = null;
            this.ERPViewArea.SecFieldName = null;
            this.ERPViewArea.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPViewArea.SelectPaging = false;
            this.ERPViewArea.SelectTop = 0;
            this.ERPViewArea.SiteControl = false;
            this.ERPViewArea.SiteFieldName = null;
            this.ERPViewArea.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPViewArea
            // 
            this.ucERPViewArea.AutoTrans = true;
            this.ucERPViewArea.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ViewAreaID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ViewAreaName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "DMTypeID";
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
            fieldAttr5.DefaultValue = null;
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
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucERPViewArea.FieldAttrs.Add(fieldAttr1);
            this.ucERPViewArea.FieldAttrs.Add(fieldAttr2);
            this.ucERPViewArea.FieldAttrs.Add(fieldAttr3);
            this.ucERPViewArea.FieldAttrs.Add(fieldAttr4);
            this.ucERPViewArea.FieldAttrs.Add(fieldAttr5);
            this.ucERPViewArea.FieldAttrs.Add(fieldAttr6);
            this.ucERPViewArea.FieldAttrs.Add(fieldAttr7);
            this.ucERPViewArea.LogInfo = null;
            this.ucERPViewArea.Name = "ucERPViewArea";
            this.ucERPViewArea.RowAffectsCheck = true;
            this.ucERPViewArea.SelectCmd = this.ERPViewArea;
            this.ucERPViewArea.SelectCmdForUpdate = null;
            this.ucERPViewArea.ServerModify = true;
            this.ucERPViewArea.ServerModifyGetMax = false;
            this.ucERPViewArea.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPViewArea.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPViewArea.UseTranscationScope = false;
            this.ucERPViewArea.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucERPViewArea.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucERPViewArea_BeforeInsert);
            this.ucERPViewArea.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucERPViewArea_BeforeModify);
            // 
            // ERPDMType
            // 
            this.ERPDMType.CacheConnection = false;
            this.ERPDMType.CommandText = "SELECT dbo.[ERPDMType].* FROM dbo.[ERPDMType]";
            this.ERPDMType.CommandTimeout = 30;
            this.ERPDMType.CommandType = System.Data.CommandType.Text;
            this.ERPDMType.DynamicTableName = false;
            this.ERPDMType.EEPAlias = null;
            this.ERPDMType.EncodingAfter = null;
            this.ERPDMType.EncodingBefore = "Windows-1252";
            this.ERPDMType.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DMTypeNO";
            keyItem3.KeyName = "DMTypeID";
            this.ERPDMType.KeyFields.Add(keyItem2);
            this.ERPDMType.KeyFields.Add(keyItem3);
            this.ERPDMType.MultiSetWhere = false;
            this.ERPDMType.Name = "ERPDMType";
            this.ERPDMType.NotificationAutoEnlist = false;
            this.ERPDMType.SecExcept = null;
            this.ERPDMType.SecFieldName = null;
            this.ERPDMType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPDMType.SelectPaging = false;
            this.ERPDMType.SelectTop = 0;
            this.ERPDMType.SiteControl = false;
            this.ERPDMType.SiteFieldName = null;
            this.ERPDMType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPDMType
            // 
            this.ucERPDMType.AutoTrans = true;
            this.ucERPDMType.ExceptJoin = false;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "DMTypeNO";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "DMTypeID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "DMTypeName";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpDateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            this.ucERPDMType.FieldAttrs.Add(fieldAttr8);
            this.ucERPDMType.FieldAttrs.Add(fieldAttr9);
            this.ucERPDMType.FieldAttrs.Add(fieldAttr10);
            this.ucERPDMType.FieldAttrs.Add(fieldAttr11);
            this.ucERPDMType.FieldAttrs.Add(fieldAttr12);
            this.ucERPDMType.FieldAttrs.Add(fieldAttr13);
            this.ucERPDMType.FieldAttrs.Add(fieldAttr14);
            this.ucERPDMType.LogInfo = null;
            this.ucERPDMType.Name = "ucERPDMType";
            this.ucERPDMType.RowAffectsCheck = true;
            this.ucERPDMType.SelectCmd = this.ERPDMType;
            this.ucERPDMType.SelectCmdForUpdate = null;
            this.ucERPDMType.ServerModify = true;
            this.ucERPDMType.ServerModifyGetMax = false;
            this.ucERPDMType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPDMType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPDMType.UseTranscationScope = false;
            this.ucERPDMType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ERPViewArea
            // 
            this.View_ERPViewArea.CacheConnection = false;
            this.View_ERPViewArea.CommandText = "SELECT * FROM dbo.[ERPViewArea]";
            this.View_ERPViewArea.CommandTimeout = 30;
            this.View_ERPViewArea.CommandType = System.Data.CommandType.Text;
            this.View_ERPViewArea.DynamicTableName = false;
            this.View_ERPViewArea.EEPAlias = null;
            this.View_ERPViewArea.EncodingAfter = null;
            this.View_ERPViewArea.EncodingBefore = "Windows-1252";
            this.View_ERPViewArea.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ViewAreaID";
            this.View_ERPViewArea.KeyFields.Add(keyItem4);
            this.View_ERPViewArea.MultiSetWhere = false;
            this.View_ERPViewArea.Name = "View_ERPViewArea";
            this.View_ERPViewArea.NotificationAutoEnlist = false;
            this.View_ERPViewArea.SecExcept = null;
            this.View_ERPViewArea.SecFieldName = null;
            this.View_ERPViewArea.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPViewArea.SelectPaging = false;
            this.View_ERPViewArea.SelectTop = 0;
            this.View_ERPViewArea.SiteControl = false;
            this.View_ERPViewArea.SiteFieldName = null;
            this.View_ERPViewArea.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_ERPDMType
            // 
            this.View_ERPDMType.CacheConnection = false;
            this.View_ERPDMType.CommandText = "SELECT * FROM dbo.[ERPDMType]";
            this.View_ERPDMType.CommandTimeout = 30;
            this.View_ERPDMType.CommandType = System.Data.CommandType.Text;
            this.View_ERPDMType.DynamicTableName = false;
            this.View_ERPDMType.EEPAlias = null;
            this.View_ERPDMType.EncodingAfter = null;
            this.View_ERPDMType.EncodingBefore = "Windows-1252";
            this.View_ERPDMType.InfoConnection = this.InfoConnection1;
            this.View_ERPDMType.MultiSetWhere = false;
            this.View_ERPDMType.Name = "View_ERPDMType";
            this.View_ERPDMType.NotificationAutoEnlist = false;
            this.View_ERPDMType.SecExcept = null;
            this.View_ERPDMType.SecFieldName = null;
            this.View_ERPDMType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPDMType.SelectPaging = false;
            this.View_ERPDMType.SelectTop = 0;
            this.View_ERPDMType.SiteControl = false;
            this.View_ERPDMType.SiteFieldName = null;
            this.View_ERPDMType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPViewArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDMType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPViewArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPDMType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPViewArea;
        private Srvtools.UpdateComponent ucERPViewArea;
        private Srvtools.InfoCommand ERPDMType;
        private Srvtools.UpdateComponent ucERPDMType;
        private Srvtools.InfoCommand View_ERPViewArea;
        private Srvtools.InfoCommand View_ERPDMType;
    }
}
