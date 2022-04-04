namespace sSalesTeam
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_SalesTeam = new Srvtools.InfoCommand(this.components);
            this.ucHUT_SalesTeam = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_SalesTeam = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_SalesTeam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_SalesTeam)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_SalesTeam
            // 
            this.HUT_SalesTeam.CacheConnection = false;
            this.HUT_SalesTeam.CommandText = "select * FROM [HUT_SalesTeam]";
            this.HUT_SalesTeam.CommandTimeout = 30;
            this.HUT_SalesTeam.CommandType = System.Data.CommandType.Text;
            this.HUT_SalesTeam.DynamicTableName = false;
            this.HUT_SalesTeam.EEPAlias = "Hunter";
            this.HUT_SalesTeam.EncodingAfter = null;
            this.HUT_SalesTeam.EncodingBefore = "Windows-1252";
            this.HUT_SalesTeam.EncodingConvert = null;
            this.HUT_SalesTeam.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ID";
            this.HUT_SalesTeam.KeyFields.Add(keyItem1);
            this.HUT_SalesTeam.MultiSetWhere = false;
            this.HUT_SalesTeam.Name = "HUT_SalesTeam";
            this.HUT_SalesTeam.NotificationAutoEnlist = false;
            this.HUT_SalesTeam.SecExcept = null;
            this.HUT_SalesTeam.SecFieldName = null;
            this.HUT_SalesTeam.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_SalesTeam.SelectPaging = false;
            this.HUT_SalesTeam.SelectTop = 0;
            this.HUT_SalesTeam.SiteControl = false;
            this.HUT_SalesTeam.SiteFieldName = null;
            this.HUT_SalesTeam.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_SalesTeam
            // 
            this.ucHUT_SalesTeam.AutoTrans = true;
            this.ucHUT_SalesTeam.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SalesTeamName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "LastUpdateBy";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "LastUpdateDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            this.ucHUT_SalesTeam.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_SalesTeam.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_SalesTeam.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_SalesTeam.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_SalesTeam.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_SalesTeam.LogInfo = null;
            this.ucHUT_SalesTeam.Name = "ucHUT_SalesTeam";
            this.ucHUT_SalesTeam.RowAffectsCheck = true;
            this.ucHUT_SalesTeam.SelectCmd = this.HUT_SalesTeam;
            this.ucHUT_SalesTeam.SelectCmdForUpdate = null;
            this.ucHUT_SalesTeam.SendSQLCmd = true;
            this.ucHUT_SalesTeam.ServerModify = true;
            this.ucHUT_SalesTeam.ServerModifyGetMax = true;
            this.ucHUT_SalesTeam.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_SalesTeam.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_SalesTeam.UseTranscationScope = false;
            this.ucHUT_SalesTeam.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_SalesTeam
            // 
            this.View_HUT_SalesTeam.CacheConnection = false;
            this.View_HUT_SalesTeam.CommandText = "SELECT * FROM [HUT_SalesTeam]";
            this.View_HUT_SalesTeam.CommandTimeout = 30;
            this.View_HUT_SalesTeam.CommandType = System.Data.CommandType.Text;
            this.View_HUT_SalesTeam.DynamicTableName = false;
            this.View_HUT_SalesTeam.EEPAlias = "Hunter";
            this.View_HUT_SalesTeam.EncodingAfter = null;
            this.View_HUT_SalesTeam.EncodingBefore = "Windows-1252";
            this.View_HUT_SalesTeam.EncodingConvert = null;
            this.View_HUT_SalesTeam.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ID";
            this.View_HUT_SalesTeam.KeyFields.Add(keyItem2);
            this.View_HUT_SalesTeam.MultiSetWhere = false;
            this.View_HUT_SalesTeam.Name = "View_HUT_SalesTeam";
            this.View_HUT_SalesTeam.NotificationAutoEnlist = false;
            this.View_HUT_SalesTeam.SecExcept = null;
            this.View_HUT_SalesTeam.SecFieldName = null;
            this.View_HUT_SalesTeam.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_SalesTeam.SelectPaging = false;
            this.View_HUT_SalesTeam.SelectTop = 0;
            this.View_HUT_SalesTeam.SiteControl = false;
            this.View_HUT_SalesTeam.SiteFieldName = null;
            this.View_HUT_SalesTeam.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_SalesTeam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_SalesTeam)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_SalesTeam;
        private Srvtools.UpdateComponent ucHUT_SalesTeam;
        private Srvtools.InfoCommand View_HUT_SalesTeam;
    }
}
