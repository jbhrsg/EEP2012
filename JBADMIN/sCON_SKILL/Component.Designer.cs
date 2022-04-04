namespace sCON_SKILL
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
            this.CON_SHARECODE = new Srvtools.InfoCommand(this.components);
            this.ucCON_SHARECODE = new Srvtools.UpdateComponent(this.components);
            this.View_CON_SHARECODE = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_SHARECODE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_SHARECODE)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // CON_SHARECODE
            // 
            this.CON_SHARECODE.CacheConnection = false;
            this.CON_SHARECODE.CommandText = "SELECT dbo.[CON_SHARECODE].* \r\nFROM dbo.[CON_SHARECODE]\r\nWHERE FIELDNAME=\'CONTACT" +
    "_SKILL\'";
            this.CON_SHARECODE.CommandTimeout = 30;
            this.CON_SHARECODE.CommandType = System.Data.CommandType.Text;
            this.CON_SHARECODE.DynamicTableName = false;
            this.CON_SHARECODE.EEPAlias = null;
            this.CON_SHARECODE.EncodingAfter = null;
            this.CON_SHARECODE.EncodingBefore = "Windows-1252";
            this.CON_SHARECODE.EncodingConvert = null;
            this.CON_SHARECODE.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CODE_ID";
            this.CON_SHARECODE.KeyFields.Add(keyItem1);
            this.CON_SHARECODE.MultiSetWhere = false;
            this.CON_SHARECODE.Name = "CON_SHARECODE";
            this.CON_SHARECODE.NotificationAutoEnlist = false;
            this.CON_SHARECODE.SecExcept = null;
            this.CON_SHARECODE.SecFieldName = null;
            this.CON_SHARECODE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_SHARECODE.SelectPaging = false;
            this.CON_SHARECODE.SelectTop = 0;
            this.CON_SHARECODE.SiteControl = false;
            this.CON_SHARECODE.SiteFieldName = null;
            this.CON_SHARECODE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_SHARECODE
            // 
            this.ucCON_SHARECODE.AutoTrans = true;
            this.ucCON_SHARECODE.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CODE_ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "FIELDNAME";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "NAME";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SORT";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "DISPLAY";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CREATE_MAN";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CREATE_DATE";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "UPDATE_MAN";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "UPDATE_DATE";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr1);
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr2);
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr3);
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr4);
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr5);
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr6);
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr7);
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr8);
            this.ucCON_SHARECODE.FieldAttrs.Add(fieldAttr9);
            this.ucCON_SHARECODE.LogInfo = null;
            this.ucCON_SHARECODE.Name = "ucCON_SHARECODE";
            this.ucCON_SHARECODE.RowAffectsCheck = true;
            this.ucCON_SHARECODE.SelectCmd = this.CON_SHARECODE;
            this.ucCON_SHARECODE.SelectCmdForUpdate = null;
            this.ucCON_SHARECODE.SendSQLCmd = true;
            this.ucCON_SHARECODE.ServerModify = true;
            this.ucCON_SHARECODE.ServerModifyGetMax = false;
            this.ucCON_SHARECODE.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_SHARECODE.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_SHARECODE.UseTranscationScope = false;
            this.ucCON_SHARECODE.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_CON_SHARECODE
            // 
            this.View_CON_SHARECODE.CacheConnection = false;
            this.View_CON_SHARECODE.CommandText = "SELECT * FROM dbo.[CON_SHARECODE]";
            this.View_CON_SHARECODE.CommandTimeout = 30;
            this.View_CON_SHARECODE.CommandType = System.Data.CommandType.Text;
            this.View_CON_SHARECODE.DynamicTableName = false;
            this.View_CON_SHARECODE.EEPAlias = null;
            this.View_CON_SHARECODE.EncodingAfter = null;
            this.View_CON_SHARECODE.EncodingBefore = "Windows-1252";
            this.View_CON_SHARECODE.EncodingConvert = null;
            this.View_CON_SHARECODE.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CODE_ID";
            this.View_CON_SHARECODE.KeyFields.Add(keyItem2);
            this.View_CON_SHARECODE.MultiSetWhere = false;
            this.View_CON_SHARECODE.Name = "View_CON_SHARECODE";
            this.View_CON_SHARECODE.NotificationAutoEnlist = false;
            this.View_CON_SHARECODE.SecExcept = null;
            this.View_CON_SHARECODE.SecFieldName = null;
            this.View_CON_SHARECODE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CON_SHARECODE.SelectPaging = false;
            this.View_CON_SHARECODE.SelectTop = 0;
            this.View_CON_SHARECODE.SiteControl = false;
            this.View_CON_SHARECODE.SiteFieldName = null;
            this.View_CON_SHARECODE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_SHARECODE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_SHARECODE)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CON_SHARECODE;
        private Srvtools.UpdateComponent ucCON_SHARECODE;
        private Srvtools.InfoCommand View_CON_SHARECODE;
    }
}
