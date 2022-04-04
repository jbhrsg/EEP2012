namespace sFunction
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
            this.HUT_Function = new Srvtools.InfoCommand(this.components);
            this.ucHUT_Function = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_Function = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Function)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Function)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_Function
            // 
            this.HUT_Function.CacheConnection = false;
            this.HUT_Function.CommandText = "SELECT [HUT_Function].* FROM [HUT_Function]";
            this.HUT_Function.CommandTimeout = 30;
            this.HUT_Function.CommandType = System.Data.CommandType.Text;
            this.HUT_Function.DynamicTableName = false;
            this.HUT_Function.EEPAlias = null;
            this.HUT_Function.EncodingAfter = null;
            this.HUT_Function.EncodingBefore = "Windows-1252";
            this.HUT_Function.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "FunctionID";
            this.HUT_Function.KeyFields.Add(keyItem1);
            this.HUT_Function.MultiSetWhere = false;
            this.HUT_Function.Name = "HUT_Function";
            this.HUT_Function.NotificationAutoEnlist = false;
            this.HUT_Function.SecExcept = null;
            this.HUT_Function.SecFieldName = null;
            this.HUT_Function.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_Function.SelectPaging = false;
            this.HUT_Function.SelectTop = 0;
            this.HUT_Function.SiteControl = false;
            this.HUT_Function.SiteFieldName = null;
            this.HUT_Function.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_Function
            // 
            this.ucHUT_Function.AutoTrans = true;
            this.ucHUT_Function.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "FunctionID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "FunctionName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            this.ucHUT_Function.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_Function.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_Function.LogInfo = null;
            this.ucHUT_Function.Name = "ucHUT_Function";
            this.ucHUT_Function.RowAffectsCheck = true;
            this.ucHUT_Function.SelectCmd = this.HUT_Function;
            this.ucHUT_Function.ServerModify = true;
            this.ucHUT_Function.ServerModifyGetMax = true;
            this.ucHUT_Function.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_Function.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_Function.UseTranscationScope = false;
            this.ucHUT_Function.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_Function
            // 
            this.View_HUT_Function.CacheConnection = false;
            this.View_HUT_Function.CommandText = "SELECT * FROM [HUT_Function]";
            this.View_HUT_Function.CommandTimeout = 30;
            this.View_HUT_Function.CommandType = System.Data.CommandType.Text;
            this.View_HUT_Function.DynamicTableName = false;
            this.View_HUT_Function.EEPAlias = null;
            this.View_HUT_Function.EncodingAfter = null;
            this.View_HUT_Function.EncodingBefore = "Windows-1252";
            this.View_HUT_Function.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "FunctionID";
            this.View_HUT_Function.KeyFields.Add(keyItem2);
            this.View_HUT_Function.MultiSetWhere = false;
            this.View_HUT_Function.Name = "View_HUT_Function";
            this.View_HUT_Function.NotificationAutoEnlist = false;
            this.View_HUT_Function.SecExcept = null;
            this.View_HUT_Function.SecFieldName = null;
            this.View_HUT_Function.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_Function.SelectPaging = false;
            this.View_HUT_Function.SelectTop = 0;
            this.View_HUT_Function.SiteControl = false;
            this.View_HUT_Function.SiteFieldName = null;
            this.View_HUT_Function.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Function)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Function)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_Function;
        private Srvtools.UpdateComponent ucHUT_Function;
        private Srvtools.InfoCommand View_HUT_Function;
    }
}
