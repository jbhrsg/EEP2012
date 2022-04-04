namespace sLangLicence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_LangLicence = new Srvtools.InfoCommand(this.components);
            this.ucHUT_LangLicence = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_LangLicence = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_LangLicence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_LangLicence)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelLangLincence";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelLangLincence";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_LangLicence
            // 
            this.HUT_LangLicence.CacheConnection = false;
            this.HUT_LangLicence.CommandText = resources.GetString("HUT_LangLicence.CommandText");
            this.HUT_LangLicence.CommandTimeout = 30;
            this.HUT_LangLicence.CommandType = System.Data.CommandType.Text;
            this.HUT_LangLicence.DynamicTableName = false;
            this.HUT_LangLicence.EEPAlias = null;
            this.HUT_LangLicence.EncodingAfter = null;
            this.HUT_LangLicence.EncodingBefore = "Windows-1252";
            this.HUT_LangLicence.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "LangLicenceID";
            this.HUT_LangLicence.KeyFields.Add(keyItem1);
            this.HUT_LangLicence.MultiSetWhere = false;
            this.HUT_LangLicence.Name = "HUT_LangLicence";
            this.HUT_LangLicence.NotificationAutoEnlist = false;
            this.HUT_LangLicence.SecExcept = null;
            this.HUT_LangLicence.SecFieldName = null;
            this.HUT_LangLicence.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_LangLicence.SelectPaging = false;
            this.HUT_LangLicence.SelectTop = 0;
            this.HUT_LangLicence.SiteControl = false;
            this.HUT_LangLicence.SiteFieldName = null;
            this.HUT_LangLicence.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_LangLicence
            // 
            this.ucHUT_LangLicence.AutoTrans = true;
            this.ucHUT_LangLicence.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "LangLicenceID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "LangID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "LangLicenceName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateBy";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
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
            this.ucHUT_LangLicence.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_LangLicence.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_LangLicence.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_LangLicence.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_LangLicence.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_LangLicence.LogInfo = null;
            this.ucHUT_LangLicence.Name = "ucHUT_LangLicence";
            this.ucHUT_LangLicence.RowAffectsCheck = true;
            this.ucHUT_LangLicence.SelectCmd = this.HUT_LangLicence;
            this.ucHUT_LangLicence.SelectCmdForUpdate = null;
            this.ucHUT_LangLicence.ServerModify = true;
            this.ucHUT_LangLicence.ServerModifyGetMax = false;
            this.ucHUT_LangLicence.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_LangLicence.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_LangLicence.UseTranscationScope = false;
            this.ucHUT_LangLicence.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_LangLicence
            // 
            this.View_HUT_LangLicence.CacheConnection = false;
            this.View_HUT_LangLicence.CommandText = "SELECT * FROM dbo.[HUT_LangLicence]";
            this.View_HUT_LangLicence.CommandTimeout = 30;
            this.View_HUT_LangLicence.CommandType = System.Data.CommandType.Text;
            this.View_HUT_LangLicence.DynamicTableName = false;
            this.View_HUT_LangLicence.EEPAlias = null;
            this.View_HUT_LangLicence.EncodingAfter = null;
            this.View_HUT_LangLicence.EncodingBefore = "Windows-1252";
            this.View_HUT_LangLicence.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "LangLicenceID";
            this.View_HUT_LangLicence.KeyFields.Add(keyItem2);
            this.View_HUT_LangLicence.MultiSetWhere = false;
            this.View_HUT_LangLicence.Name = "View_HUT_LangLicence";
            this.View_HUT_LangLicence.NotificationAutoEnlist = false;
            this.View_HUT_LangLicence.SecExcept = null;
            this.View_HUT_LangLicence.SecFieldName = null;
            this.View_HUT_LangLicence.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_LangLicence.SelectPaging = false;
            this.View_HUT_LangLicence.SelectTop = 0;
            this.View_HUT_LangLicence.SiteControl = false;
            this.View_HUT_LangLicence.SiteFieldName = null;
            this.View_HUT_LangLicence.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "LangLicenceID";
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
            this.autoNumber1.TargetColumn = "LangLicenceID";
            this.autoNumber1.UpdateComp = this.ucHUT_LangLicence;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_LangLicence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_LangLicence)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_LangLicence;
        private Srvtools.UpdateComponent ucHUT_LangLicence;
        private Srvtools.InfoCommand View_HUT_LangLicence;
        private Srvtools.AutoNumber autoNumber1;
    }
}
