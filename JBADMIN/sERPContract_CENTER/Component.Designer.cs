namespace sERPContract_CENTER
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CON_CENTER = new Srvtools.InfoCommand(this.components);
            this.ucCON_CENTER = new Srvtools.UpdateComponent(this.components);
            this.View_CON_CENTER = new Srvtools.InfoCommand(this.components);
            this.CON_CENTER_AUTHORITY = new Srvtools.InfoCommand(this.components);
            this.ucCON_CENTER_AUTHORITY = new Srvtools.UpdateComponent(this.components);
            this.USERS = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_CENTER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTER_AUTHORITY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // CON_CENTER
            // 
            this.CON_CENTER.CacheConnection = false;
            this.CON_CENTER.CommandText = "SELECT dbo.[ERPContract_CENTER].* FROM dbo.[ERPContract_CENTER]\r\nORDER BY CENTER_" +
    "SEQ";
            this.CON_CENTER.CommandTimeout = 30;
            this.CON_CENTER.CommandType = System.Data.CommandType.Text;
            this.CON_CENTER.DynamicTableName = false;
            this.CON_CENTER.EEPAlias = null;
            this.CON_CENTER.EncodingAfter = null;
            this.CON_CENTER.EncodingBefore = "Windows-1252";
            this.CON_CENTER.EncodingConvert = null;
            this.CON_CENTER.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CENTER_ID";
            this.CON_CENTER.KeyFields.Add(keyItem1);
            this.CON_CENTER.MultiSetWhere = false;
            this.CON_CENTER.Name = "CON_CENTER";
            this.CON_CENTER.NotificationAutoEnlist = false;
            this.CON_CENTER.SecExcept = null;
            this.CON_CENTER.SecFieldName = null;
            this.CON_CENTER.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CENTER.SelectPaging = false;
            this.CON_CENTER.SelectTop = 0;
            this.CON_CENTER.SiteControl = false;
            this.CON_CENTER.SiteFieldName = null;
            this.CON_CENTER.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_CENTER
            // 
            this.ucCON_CENTER.AutoTrans = true;
            this.ucCON_CENTER.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CENTER_ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CENTER_CNAME";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CENTER_ENAME";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CENTER_SEQ";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CREATE_MAN";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = "_username";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CREATE_DATE";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "UPDATE_MAN";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = "_username";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "UPDATE_DATE";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucCON_CENTER.FieldAttrs.Add(fieldAttr1);
            this.ucCON_CENTER.FieldAttrs.Add(fieldAttr2);
            this.ucCON_CENTER.FieldAttrs.Add(fieldAttr3);
            this.ucCON_CENTER.FieldAttrs.Add(fieldAttr4);
            this.ucCON_CENTER.FieldAttrs.Add(fieldAttr5);
            this.ucCON_CENTER.FieldAttrs.Add(fieldAttr6);
            this.ucCON_CENTER.FieldAttrs.Add(fieldAttr7);
            this.ucCON_CENTER.FieldAttrs.Add(fieldAttr8);
            this.ucCON_CENTER.LogInfo = null;
            this.ucCON_CENTER.Name = "ucCON_CENTER";
            this.ucCON_CENTER.RowAffectsCheck = true;
            this.ucCON_CENTER.SelectCmd = this.CON_CENTER;
            this.ucCON_CENTER.SelectCmdForUpdate = null;
            this.ucCON_CENTER.SendSQLCmd = true;
            this.ucCON_CENTER.ServerModify = true;
            this.ucCON_CENTER.ServerModifyGetMax = false;
            this.ucCON_CENTER.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_CENTER.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_CENTER.UseTranscationScope = false;
            this.ucCON_CENTER.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCON_CENTER.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCON_CENTER_BeforeInsert);
            this.ucCON_CENTER.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCON_CENTER_BeforeModify);
            // 
            // View_CON_CENTER
            // 
            this.View_CON_CENTER.CacheConnection = false;
            this.View_CON_CENTER.CommandText = "SELECT * FROM dbo.[ERPContract_CENTER]";
            this.View_CON_CENTER.CommandTimeout = 30;
            this.View_CON_CENTER.CommandType = System.Data.CommandType.Text;
            this.View_CON_CENTER.DynamicTableName = false;
            this.View_CON_CENTER.EEPAlias = null;
            this.View_CON_CENTER.EncodingAfter = null;
            this.View_CON_CENTER.EncodingBefore = "Windows-1252";
            this.View_CON_CENTER.EncodingConvert = null;
            this.View_CON_CENTER.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CENTER_ID";
            this.View_CON_CENTER.KeyFields.Add(keyItem2);
            this.View_CON_CENTER.MultiSetWhere = false;
            this.View_CON_CENTER.Name = "View_CON_CENTER";
            this.View_CON_CENTER.NotificationAutoEnlist = false;
            this.View_CON_CENTER.SecExcept = null;
            this.View_CON_CENTER.SecFieldName = null;
            this.View_CON_CENTER.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CON_CENTER.SelectPaging = false;
            this.View_CON_CENTER.SelectTop = 0;
            this.View_CON_CENTER.SiteControl = false;
            this.View_CON_CENTER.SiteFieldName = null;
            this.View_CON_CENTER.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CON_CENTER_AUTHORITY
            // 
            this.CON_CENTER_AUTHORITY.CacheConnection = false;
            this.CON_CENTER_AUTHORITY.CommandText = "SELECT *  FROM ERPContract_CENTER_AUTHORITY";
            this.CON_CENTER_AUTHORITY.CommandTimeout = 30;
            this.CON_CENTER_AUTHORITY.CommandType = System.Data.CommandType.Text;
            this.CON_CENTER_AUTHORITY.DynamicTableName = false;
            this.CON_CENTER_AUTHORITY.EEPAlias = null;
            this.CON_CENTER_AUTHORITY.EncodingAfter = null;
            this.CON_CENTER_AUTHORITY.EncodingBefore = "Windows-1252";
            this.CON_CENTER_AUTHORITY.EncodingConvert = null;
            this.CON_CENTER_AUTHORITY.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CENTER_ID";
            keyItem4.KeyName = "USERID";
            this.CON_CENTER_AUTHORITY.KeyFields.Add(keyItem3);
            this.CON_CENTER_AUTHORITY.KeyFields.Add(keyItem4);
            this.CON_CENTER_AUTHORITY.MultiSetWhere = false;
            this.CON_CENTER_AUTHORITY.Name = "CON_CENTER_AUTHORITY";
            this.CON_CENTER_AUTHORITY.NotificationAutoEnlist = false;
            this.CON_CENTER_AUTHORITY.SecExcept = null;
            this.CON_CENTER_AUTHORITY.SecFieldName = null;
            this.CON_CENTER_AUTHORITY.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CENTER_AUTHORITY.SelectPaging = false;
            this.CON_CENTER_AUTHORITY.SelectTop = 0;
            this.CON_CENTER_AUTHORITY.SiteControl = false;
            this.CON_CENTER_AUTHORITY.SiteFieldName = null;
            this.CON_CENTER_AUTHORITY.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_CENTER_AUTHORITY
            // 
            this.ucCON_CENTER_AUTHORITY.AutoTrans = true;
            this.ucCON_CENTER_AUTHORITY.ExceptJoin = false;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CENTER_ID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "USERID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CREATE_MAN";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = "_username";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CREATE_DATE";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = "";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "UPDATE_MAN";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = "_username";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "UPDATE_DATE";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            this.ucCON_CENTER_AUTHORITY.FieldAttrs.Add(fieldAttr9);
            this.ucCON_CENTER_AUTHORITY.FieldAttrs.Add(fieldAttr10);
            this.ucCON_CENTER_AUTHORITY.FieldAttrs.Add(fieldAttr11);
            this.ucCON_CENTER_AUTHORITY.FieldAttrs.Add(fieldAttr12);
            this.ucCON_CENTER_AUTHORITY.FieldAttrs.Add(fieldAttr13);
            this.ucCON_CENTER_AUTHORITY.FieldAttrs.Add(fieldAttr14);
            this.ucCON_CENTER_AUTHORITY.LogInfo = null;
            this.ucCON_CENTER_AUTHORITY.Name = "ucCON_CENTER_AUTHORITY";
            this.ucCON_CENTER_AUTHORITY.RowAffectsCheck = true;
            this.ucCON_CENTER_AUTHORITY.SelectCmd = this.CON_CENTER_AUTHORITY;
            this.ucCON_CENTER_AUTHORITY.SelectCmdForUpdate = null;
            this.ucCON_CENTER_AUTHORITY.SendSQLCmd = true;
            this.ucCON_CENTER_AUTHORITY.ServerModify = true;
            this.ucCON_CENTER_AUTHORITY.ServerModifyGetMax = false;
            this.ucCON_CENTER_AUTHORITY.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_CENTER_AUTHORITY.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_CENTER_AUTHORITY.UseTranscationScope = false;
            this.ucCON_CENTER_AUTHORITY.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCON_CENTER_AUTHORITY.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCON_CENTER_AUTHORITY_BeforeInsert);
            this.ucCON_CENTER_AUTHORITY.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCON_CENTER_AUTHORITY_BeforeModify);
            // 
            // USERS
            // 
            this.USERS.CacheConnection = false;
            this.USERS.CommandText = "SELECT USERID,USERNAME\r\n FROM EIPHRSYS.DBO.USERS\r\nWHERE DESCRIPTION=\'JB\' \r\nORDER " +
    "BY  USERID";
            this.USERS.CommandTimeout = 30;
            this.USERS.CommandType = System.Data.CommandType.Text;
            this.USERS.DynamicTableName = false;
            this.USERS.EEPAlias = null;
            this.USERS.EncodingAfter = null;
            this.USERS.EncodingBefore = "Windows-1252";
            this.USERS.EncodingConvert = null;
            this.USERS.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "USERID";
            this.USERS.KeyFields.Add(keyItem5);
            this.USERS.MultiSetWhere = false;
            this.USERS.Name = "USERS";
            this.USERS.NotificationAutoEnlist = false;
            this.USERS.SecExcept = null;
            this.USERS.SecFieldName = null;
            this.USERS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.USERS.SelectPaging = false;
            this.USERS.SelectTop = 0;
            this.USERS.SiteControl = false;
            this.USERS.SiteFieldName = null;
            this.USERS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTER)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_CENTER)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTER_AUTHORITY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CON_CENTER;
        private Srvtools.UpdateComponent ucCON_CENTER;
        private Srvtools.InfoCommand View_CON_CENTER;
        private Srvtools.InfoCommand CON_CENTER_AUTHORITY;
        private Srvtools.UpdateComponent ucCON_CENTER_AUTHORITY;
        private Srvtools.InfoCommand USERS;
    }
}
