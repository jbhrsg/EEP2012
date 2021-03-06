namespace sZLangType
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_ZLangType = new Srvtools.InfoCommand(this.components);
            this.ucHUT_ZLangType = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_ZLangType = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_ZLangType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_ZLangType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelLangType";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelLangType";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_ZLangType
            // 
            this.HUT_ZLangType.CacheConnection = false;
            this.HUT_ZLangType.CommandText = "SELECT dbo.[HUT_ZLangType].* FROM dbo.[HUT_ZLangType]";
            this.HUT_ZLangType.CommandTimeout = 30;
            this.HUT_ZLangType.CommandType = System.Data.CommandType.Text;
            this.HUT_ZLangType.DynamicTableName = false;
            this.HUT_ZLangType.EEPAlias = null;
            this.HUT_ZLangType.EncodingAfter = null;
            this.HUT_ZLangType.EncodingBefore = "Windows-1252";
            this.HUT_ZLangType.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "LangID";
            this.HUT_ZLangType.KeyFields.Add(keyItem1);
            this.HUT_ZLangType.MultiSetWhere = false;
            this.HUT_ZLangType.Name = "HUT_ZLangType";
            this.HUT_ZLangType.NotificationAutoEnlist = false;
            this.HUT_ZLangType.SecExcept = null;
            this.HUT_ZLangType.SecFieldName = null;
            this.HUT_ZLangType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_ZLangType.SelectPaging = false;
            this.HUT_ZLangType.SelectTop = 0;
            this.HUT_ZLangType.SiteControl = false;
            this.HUT_ZLangType.SiteFieldName = null;
            this.HUT_ZLangType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_ZLangType
            // 
            this.ucHUT_ZLangType.AutoTrans = true;
            this.ucHUT_ZLangType.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "LangID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "LangName";
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
            this.ucHUT_ZLangType.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_ZLangType.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_ZLangType.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_ZLangType.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_ZLangType.LogInfo = null;
            this.ucHUT_ZLangType.Name = "ucHUT_ZLangType";
            this.ucHUT_ZLangType.RowAffectsCheck = true;
            this.ucHUT_ZLangType.SelectCmd = this.HUT_ZLangType;
            this.ucHUT_ZLangType.SelectCmdForUpdate = null;
            this.ucHUT_ZLangType.ServerModify = true;
            this.ucHUT_ZLangType.ServerModifyGetMax = false;
            this.ucHUT_ZLangType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_ZLangType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_ZLangType.UseTranscationScope = false;
            this.ucHUT_ZLangType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_ZLangType
            // 
            this.View_HUT_ZLangType.CacheConnection = false;
            this.View_HUT_ZLangType.CommandText = "SELECT * FROM dbo.[HUT_ZLangType]";
            this.View_HUT_ZLangType.CommandTimeout = 30;
            this.View_HUT_ZLangType.CommandType = System.Data.CommandType.Text;
            this.View_HUT_ZLangType.DynamicTableName = false;
            this.View_HUT_ZLangType.EEPAlias = null;
            this.View_HUT_ZLangType.EncodingAfter = null;
            this.View_HUT_ZLangType.EncodingBefore = "Windows-1252";
            this.View_HUT_ZLangType.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "LangID";
            this.View_HUT_ZLangType.KeyFields.Add(keyItem2);
            this.View_HUT_ZLangType.MultiSetWhere = false;
            this.View_HUT_ZLangType.Name = "View_HUT_ZLangType";
            this.View_HUT_ZLangType.NotificationAutoEnlist = false;
            this.View_HUT_ZLangType.SecExcept = null;
            this.View_HUT_ZLangType.SecFieldName = null;
            this.View_HUT_ZLangType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_ZLangType.SelectPaging = false;
            this.View_HUT_ZLangType.SelectTop = 0;
            this.View_HUT_ZLangType.SiteControl = false;
            this.View_HUT_ZLangType.SiteFieldName = null;
            this.View_HUT_ZLangType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "LangID";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 3;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "LangID";
            this.autoNumber1.UpdateComp = this.ucHUT_ZLangType;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_ZLangType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_ZLangType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_ZLangType;
        private Srvtools.UpdateComponent ucHUT_ZLangType;
        private Srvtools.InfoCommand View_HUT_ZLangType;
        private Srvtools.AutoNumber autoNumber1;
    }
}
