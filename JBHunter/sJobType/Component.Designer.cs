namespace sJobType
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_JobType = new Srvtools.InfoCommand(this.components);
            this.ucHUT_JobType = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_JobType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_JobType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_JobType)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_JobType
            // 
            this.HUT_JobType.CacheConnection = false;
            this.HUT_JobType.CommandText = "SELECT [HUT_JobType].* FROM [HUT_JobType]";
            this.HUT_JobType.CommandTimeout = 30;
            this.HUT_JobType.CommandType = System.Data.CommandType.Text;
            this.HUT_JobType.DynamicTableName = false;
            this.HUT_JobType.EEPAlias = null;
            this.HUT_JobType.EncodingAfter = null;
            this.HUT_JobType.EncodingBefore = "Windows-1252";
            this.HUT_JobType.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ID";
            this.HUT_JobType.KeyFields.Add(keyItem1);
            this.HUT_JobType.MultiSetWhere = false;
            this.HUT_JobType.Name = "HUT_JobType";
            this.HUT_JobType.NotificationAutoEnlist = false;
            this.HUT_JobType.SecExcept = null;
            this.HUT_JobType.SecFieldName = null;
            this.HUT_JobType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_JobType.SelectPaging = false;
            this.HUT_JobType.SelectTop = 0;
            this.HUT_JobType.SiteControl = false;
            this.HUT_JobType.SiteFieldName = null;
            this.HUT_JobType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_JobType
            // 
            this.ucHUT_JobType.AutoTrans = true;
            this.ucHUT_JobType.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "JobTypeName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            this.ucHUT_JobType.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_JobType.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_JobType.LogInfo = null;
            this.ucHUT_JobType.Name = "ucHUT_JobType";
            this.ucHUT_JobType.RowAffectsCheck = true;
            this.ucHUT_JobType.SelectCmd = this.HUT_JobType;
            this.ucHUT_JobType.ServerModify = true;
            this.ucHUT_JobType.ServerModifyGetMax = true;
            this.ucHUT_JobType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_JobType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_JobType.UseTranscationScope = false;
            this.ucHUT_JobType.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_JobType
            // 
            this.View_HUT_JobType.CacheConnection = false;
            this.View_HUT_JobType.CommandText = "SELECT * FROM [HUT_JobType]";
            this.View_HUT_JobType.CommandTimeout = 30;
            this.View_HUT_JobType.CommandType = System.Data.CommandType.Text;
            this.View_HUT_JobType.DynamicTableName = false;
            this.View_HUT_JobType.EEPAlias = null;
            this.View_HUT_JobType.EncodingAfter = null;
            this.View_HUT_JobType.EncodingBefore = "Windows-1252";
            this.View_HUT_JobType.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ID";
            this.View_HUT_JobType.KeyFields.Add(keyItem2);
            this.View_HUT_JobType.MultiSetWhere = false;
            this.View_HUT_JobType.Name = "View_HUT_JobType";
            this.View_HUT_JobType.NotificationAutoEnlist = false;
            this.View_HUT_JobType.SecExcept = null;
            this.View_HUT_JobType.SecFieldName = null;
            this.View_HUT_JobType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_JobType.SelectPaging = false;
            this.View_HUT_JobType.SelectTop = 0;
            this.View_HUT_JobType.SiteControl = false;
            this.View_HUT_JobType.SiteFieldName = null;
            this.View_HUT_JobType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_JobType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_JobType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_JobType;
        private Srvtools.UpdateComponent ucHUT_JobType;
        private Srvtools.InfoCommand View_HUT_JobType;
    }
}
