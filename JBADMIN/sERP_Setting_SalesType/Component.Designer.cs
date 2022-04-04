namespace sERP_Setting_SalesType
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            this.ucSalesType = new Srvtools.UpdateComponent(this.components);
            this.View_SalesType = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "SELECT dbo.[SalesType].* FROM dbo.[SalesType]\r\nOrder By SalesTypeID";
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = "JBERP";
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.SalesType.KeyFields.Add(keyItem1);
            this.SalesType.MultiSetWhere = false;
            this.SalesType.Name = "SalesType";
            this.SalesType.NotificationAutoEnlist = false;
            this.SalesType.SecExcept = null;
            this.SalesType.SecFieldName = null;
            this.SalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesType.SelectPaging = false;
            this.SalesType.SelectTop = 0;
            this.SalesType.SiteControl = false;
            this.SalesType.SiteFieldName = null;
            this.SalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesType
            // 
            this.ucSalesType.AutoTrans = true;
            this.ucSalesType.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SalesTypeID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesTypeName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "InsGroupID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Unit";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = "_username";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = "";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr8.DefaultValue = "_username";
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "LastUpdateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr9.DefaultValue = "";
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucSalesType.FieldAttrs.Add(fieldAttr1);
            this.ucSalesType.FieldAttrs.Add(fieldAttr2);
            this.ucSalesType.FieldAttrs.Add(fieldAttr3);
            this.ucSalesType.FieldAttrs.Add(fieldAttr4);
            this.ucSalesType.FieldAttrs.Add(fieldAttr5);
            this.ucSalesType.FieldAttrs.Add(fieldAttr6);
            this.ucSalesType.FieldAttrs.Add(fieldAttr7);
            this.ucSalesType.FieldAttrs.Add(fieldAttr8);
            this.ucSalesType.FieldAttrs.Add(fieldAttr9);
            this.ucSalesType.LogInfo = null;
            this.ucSalesType.Name = "ucSalesType";
            this.ucSalesType.RowAffectsCheck = true;
            this.ucSalesType.SelectCmd = this.SalesType;
            this.ucSalesType.SelectCmdForUpdate = null;
            this.ucSalesType.SendSQLCmd = true;
            this.ucSalesType.ServerModify = true;
            this.ucSalesType.ServerModifyGetMax = false;
            this.ucSalesType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesType.UseTranscationScope = false;
            this.ucSalesType.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucSalesType.BeforeApply += new Srvtools.UpdateComponentBeforeApplyEventHandler(this.ucSalesType_BeforeApply);
            this.ucSalesType.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucSalesType_BeforeModify);
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
            keyItem2.KeyName = "AutoKey";
            this.View_SalesType.KeyFields.Add(keyItem2);
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
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "Select InsGroupID,InsGroupName,ShortName from InsGroup where IsActive=1";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = "JBADMIN";
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesType;
        private Srvtools.UpdateComponent ucSalesType;
        private Srvtools.InfoCommand View_SalesType;
        private Srvtools.InfoCommand InsGroup;
    }
}
