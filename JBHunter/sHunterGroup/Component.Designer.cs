namespace sHunterGroup
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_HuntGroup = new Srvtools.InfoCommand(this.components);
            this.ucHUT_HuntGroup = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_HuntGroup = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_HuntGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_HuntGroup)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_HuntGroup
            // 
            this.HUT_HuntGroup.CacheConnection = false;
            this.HUT_HuntGroup.CommandText = "SELECT [HUT_HuntGroup].* FROM [HUT_HuntGroup]";
            this.HUT_HuntGroup.CommandTimeout = 30;
            this.HUT_HuntGroup.CommandType = System.Data.CommandType.Text;
            this.HUT_HuntGroup.DynamicTableName = false;
            this.HUT_HuntGroup.EEPAlias = null;
            this.HUT_HuntGroup.EncodingAfter = null;
            this.HUT_HuntGroup.EncodingBefore = "Windows-1252";
            this.HUT_HuntGroup.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ID";
            this.HUT_HuntGroup.KeyFields.Add(keyItem1);
            this.HUT_HuntGroup.MultiSetWhere = false;
            this.HUT_HuntGroup.Name = "HUT_HuntGroup";
            this.HUT_HuntGroup.NotificationAutoEnlist = false;
            this.HUT_HuntGroup.SecExcept = null;
            this.HUT_HuntGroup.SecFieldName = null;
            this.HUT_HuntGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_HuntGroup.SelectPaging = false;
            this.HUT_HuntGroup.SelectTop = 0;
            this.HUT_HuntGroup.SiteControl = false;
            this.HUT_HuntGroup.SiteFieldName = null;
            this.HUT_HuntGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_HuntGroup
            // 
            this.ucHUT_HuntGroup.AutoTrans = true;
            this.ucHUT_HuntGroup.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "HuntGroupName";
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
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "LastUpdateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LastUpdateDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucHUT_HuntGroup.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_HuntGroup.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_HuntGroup.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_HuntGroup.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_HuntGroup.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_HuntGroup.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_HuntGroup.LogInfo = null;
            this.ucHUT_HuntGroup.Name = "ucHUT_HuntGroup";
            this.ucHUT_HuntGroup.RowAffectsCheck = true;
            this.ucHUT_HuntGroup.SelectCmd = this.HUT_HuntGroup;
            this.ucHUT_HuntGroup.ServerModify = true;
            this.ucHUT_HuntGroup.ServerModifyGetMax = true;
            this.ucHUT_HuntGroup.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_HuntGroup.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_HuntGroup.UseTranscationScope = false;
            this.ucHUT_HuntGroup.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_HuntGroup
            // 
            this.View_HUT_HuntGroup.CacheConnection = false;
            this.View_HUT_HuntGroup.CommandText = "SELECT * FROM [HUT_HuntGroup]";
            this.View_HUT_HuntGroup.CommandTimeout = 30;
            this.View_HUT_HuntGroup.CommandType = System.Data.CommandType.Text;
            this.View_HUT_HuntGroup.DynamicTableName = false;
            this.View_HUT_HuntGroup.EEPAlias = null;
            this.View_HUT_HuntGroup.EncodingAfter = null;
            this.View_HUT_HuntGroup.EncodingBefore = "Windows-1252";
            this.View_HUT_HuntGroup.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ID";
            this.View_HUT_HuntGroup.KeyFields.Add(keyItem2);
            this.View_HUT_HuntGroup.MultiSetWhere = false;
            this.View_HUT_HuntGroup.Name = "View_HUT_HuntGroup";
            this.View_HUT_HuntGroup.NotificationAutoEnlist = false;
            this.View_HUT_HuntGroup.SecExcept = null;
            this.View_HUT_HuntGroup.SecFieldName = null;
            this.View_HUT_HuntGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_HuntGroup.SelectPaging = false;
            this.View_HUT_HuntGroup.SelectTop = 0;
            this.View_HUT_HuntGroup.SiteControl = false;
            this.View_HUT_HuntGroup.SiteFieldName = null;
            this.View_HUT_HuntGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_HuntGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_HuntGroup)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_HuntGroup;
        private Srvtools.UpdateComponent ucHUT_HuntGroup;
        private Srvtools.InfoCommand View_HUT_HuntGroup;
    }
}
