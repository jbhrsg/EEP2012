namespace sHunter
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_Hunter = new Srvtools.InfoCommand(this.components);
            this.ucHUT_Hunter = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_Hunter = new Srvtools.InfoCommand(this.components);
            this.HUT_SalesTeam = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Hunter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Hunter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_SalesTeam)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_Hunter
            // 
            this.HUT_Hunter.CacheConnection = false;
            this.HUT_Hunter.CommandText = "SELECT [HUT_Hunter].* FROM [HUT_Hunter]";
            this.HUT_Hunter.CommandTimeout = 30;
            this.HUT_Hunter.CommandType = System.Data.CommandType.Text;
            this.HUT_Hunter.DynamicTableName = false;
            this.HUT_Hunter.EEPAlias = null;
            this.HUT_Hunter.EncodingAfter = null;
            this.HUT_Hunter.EncodingBefore = "Windows-1252";
            this.HUT_Hunter.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ID";
            this.HUT_Hunter.KeyFields.Add(keyItem1);
            this.HUT_Hunter.MultiSetWhere = false;
            this.HUT_Hunter.Name = "HUT_Hunter";
            this.HUT_Hunter.NotificationAutoEnlist = false;
            this.HUT_Hunter.SecExcept = null;
            this.HUT_Hunter.SecFieldName = null;
            this.HUT_Hunter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_Hunter.SelectPaging = false;
            this.HUT_Hunter.SelectTop = 0;
            this.HUT_Hunter.SiteControl = false;
            this.HUT_Hunter.SiteFieldName = null;
            this.HUT_Hunter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_Hunter
            // 
            this.ucHUT_Hunter.AutoTrans = true;
            this.ucHUT_Hunter.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "HunterName";
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
            this.ucHUT_Hunter.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_Hunter.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_Hunter.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_Hunter.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_Hunter.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_Hunter.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_Hunter.LogInfo = null;
            this.ucHUT_Hunter.Name = "ucHUT_Hunter";
            this.ucHUT_Hunter.RowAffectsCheck = true;
            this.ucHUT_Hunter.SelectCmd = this.HUT_Hunter;
            this.ucHUT_Hunter.SelectCmdForUpdate = null;
            this.ucHUT_Hunter.ServerModify = true;
            this.ucHUT_Hunter.ServerModifyGetMax = true;
            this.ucHUT_Hunter.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_Hunter.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_Hunter.UseTranscationScope = false;
            this.ucHUT_Hunter.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_Hunter
            // 
            this.View_HUT_Hunter.CacheConnection = false;
            this.View_HUT_Hunter.CommandText = "SELECT * FROM [HUT_Hunter]";
            this.View_HUT_Hunter.CommandTimeout = 30;
            this.View_HUT_Hunter.CommandType = System.Data.CommandType.Text;
            this.View_HUT_Hunter.DynamicTableName = false;
            this.View_HUT_Hunter.EEPAlias = null;
            this.View_HUT_Hunter.EncodingAfter = null;
            this.View_HUT_Hunter.EncodingBefore = "Windows-1252";
            this.View_HUT_Hunter.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ID";
            this.View_HUT_Hunter.KeyFields.Add(keyItem2);
            this.View_HUT_Hunter.MultiSetWhere = false;
            this.View_HUT_Hunter.Name = "View_HUT_Hunter";
            this.View_HUT_Hunter.NotificationAutoEnlist = false;
            this.View_HUT_Hunter.SecExcept = null;
            this.View_HUT_Hunter.SecFieldName = null;
            this.View_HUT_Hunter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_Hunter.SelectPaging = false;
            this.View_HUT_Hunter.SelectTop = 0;
            this.View_HUT_Hunter.SiteControl = false;
            this.View_HUT_Hunter.SiteFieldName = null;
            this.View_HUT_Hunter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_SalesTeam
            // 
            this.HUT_SalesTeam.CacheConnection = false;
            this.HUT_SalesTeam.CommandText = "select HUT_SalesTeam.ID,HUT_SalesTeam.SalesTeamName from HUT_SalesTeam\r\nOrder by " +
    "HUT_SalesTeam.ID";
            this.HUT_SalesTeam.CommandTimeout = 30;
            this.HUT_SalesTeam.CommandType = System.Data.CommandType.Text;
            this.HUT_SalesTeam.DynamicTableName = false;
            this.HUT_SalesTeam.EEPAlias = null;
            this.HUT_SalesTeam.EncodingAfter = null;
            this.HUT_SalesTeam.EncodingBefore = "Windows-1252";
            this.HUT_SalesTeam.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ID";
            this.HUT_SalesTeam.KeyFields.Add(keyItem3);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_Hunter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_Hunter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_SalesTeam)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_Hunter;
        private Srvtools.UpdateComponent ucHUT_Hunter;
        private Srvtools.InfoCommand View_HUT_Hunter;
        private Srvtools.InfoCommand HUT_SalesTeam;
    }
}
