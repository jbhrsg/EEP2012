namespace sERPContractGroup
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
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPContractGroup = new Srvtools.InfoCommand(this.components);
            this.ucERPContractGroup = new Srvtools.UpdateComponent(this.components);
            this.ERPContractGroupUser = new Srvtools.InfoCommand(this.components);
            this.ucERPContractGroupUser = new Srvtools.UpdateComponent(this.components);
            this.View_ERPContractGroup = new Srvtools.InfoCommand(this.components);
            this.USERS = new Srvtools.InfoCommand(this.components);
            this.SYS_ORG = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContractGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContractGroupUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContractGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ORG)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPContractGroup
            // 
            this.ERPContractGroup.CacheConnection = false;
            this.ERPContractGroup.CommandText = "SELECT dbo.[ERPContractGroup].* FROM dbo.[ERPContractGroup]";
            this.ERPContractGroup.CommandTimeout = 30;
            this.ERPContractGroup.CommandType = System.Data.CommandType.Text;
            this.ERPContractGroup.DynamicTableName = false;
            this.ERPContractGroup.EEPAlias = null;
            this.ERPContractGroup.EncodingAfter = null;
            this.ERPContractGroup.EncodingBefore = "Windows-1252";
            this.ERPContractGroup.EncodingConvert = null;
            this.ERPContractGroup.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CENTER_ID";
            this.ERPContractGroup.KeyFields.Add(keyItem1);
            this.ERPContractGroup.MultiSetWhere = false;
            this.ERPContractGroup.Name = "ERPContractGroup";
            this.ERPContractGroup.NotificationAutoEnlist = false;
            this.ERPContractGroup.SecExcept = null;
            this.ERPContractGroup.SecFieldName = null;
            this.ERPContractGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPContractGroup.SelectPaging = false;
            this.ERPContractGroup.SelectTop = 0;
            this.ERPContractGroup.SiteControl = false;
            this.ERPContractGroup.SiteFieldName = null;
            this.ERPContractGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPContractGroup
            // 
            this.ucERPContractGroup.AutoTrans = true;
            this.ucERPContractGroup.ExceptJoin = false;
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
            fieldAttr5.DefaultValue = null;
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
            fieldAttr7.DefaultValue = null;
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
            this.ucERPContractGroup.FieldAttrs.Add(fieldAttr1);
            this.ucERPContractGroup.FieldAttrs.Add(fieldAttr2);
            this.ucERPContractGroup.FieldAttrs.Add(fieldAttr3);
            this.ucERPContractGroup.FieldAttrs.Add(fieldAttr4);
            this.ucERPContractGroup.FieldAttrs.Add(fieldAttr5);
            this.ucERPContractGroup.FieldAttrs.Add(fieldAttr6);
            this.ucERPContractGroup.FieldAttrs.Add(fieldAttr7);
            this.ucERPContractGroup.FieldAttrs.Add(fieldAttr8);
            this.ucERPContractGroup.LogInfo = null;
            this.ucERPContractGroup.Name = "ucERPContractGroup";
            this.ucERPContractGroup.RowAffectsCheck = true;
            this.ucERPContractGroup.SelectCmd = this.ERPContractGroup;
            this.ucERPContractGroup.SelectCmdForUpdate = null;
            this.ucERPContractGroup.SendSQLCmd = true;
            this.ucERPContractGroup.ServerModify = true;
            this.ucERPContractGroup.ServerModifyGetMax = false;
            this.ucERPContractGroup.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPContractGroup.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPContractGroup.UseTranscationScope = false;
            this.ucERPContractGroup.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ERPContractGroupUser
            // 
            this.ERPContractGroupUser.CacheConnection = false;
            this.ERPContractGroupUser.CommandText = "SELECT dbo.[ERPContractGroupUser].* FROM dbo.[ERPContractGroupUser]";
            this.ERPContractGroupUser.CommandTimeout = 30;
            this.ERPContractGroupUser.CommandType = System.Data.CommandType.Text;
            this.ERPContractGroupUser.DynamicTableName = false;
            this.ERPContractGroupUser.EEPAlias = null;
            this.ERPContractGroupUser.EncodingAfter = null;
            this.ERPContractGroupUser.EncodingBefore = "Windows-1252";
            this.ERPContractGroupUser.EncodingConvert = null;
            this.ERPContractGroupUser.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CENTER_ID";
            keyItem3.KeyName = "USERID";
            this.ERPContractGroupUser.KeyFields.Add(keyItem2);
            this.ERPContractGroupUser.KeyFields.Add(keyItem3);
            this.ERPContractGroupUser.MultiSetWhere = false;
            this.ERPContractGroupUser.Name = "ERPContractGroupUser";
            this.ERPContractGroupUser.NotificationAutoEnlist = false;
            this.ERPContractGroupUser.SecExcept = null;
            this.ERPContractGroupUser.SecFieldName = null;
            this.ERPContractGroupUser.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPContractGroupUser.SelectPaging = false;
            this.ERPContractGroupUser.SelectTop = 0;
            this.ERPContractGroupUser.SiteControl = false;
            this.ERPContractGroupUser.SiteFieldName = null;
            this.ERPContractGroupUser.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPContractGroupUser
            // 
            this.ucERPContractGroupUser.AutoTrans = true;
            this.ucERPContractGroupUser.ExceptJoin = false;
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
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CREATE_DATE";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "UPDATE_MAN";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
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
            this.ucERPContractGroupUser.FieldAttrs.Add(fieldAttr9);
            this.ucERPContractGroupUser.FieldAttrs.Add(fieldAttr10);
            this.ucERPContractGroupUser.FieldAttrs.Add(fieldAttr11);
            this.ucERPContractGroupUser.FieldAttrs.Add(fieldAttr12);
            this.ucERPContractGroupUser.FieldAttrs.Add(fieldAttr13);
            this.ucERPContractGroupUser.FieldAttrs.Add(fieldAttr14);
            this.ucERPContractGroupUser.LogInfo = null;
            this.ucERPContractGroupUser.Name = "ucERPContractGroupUser";
            this.ucERPContractGroupUser.RowAffectsCheck = true;
            this.ucERPContractGroupUser.SelectCmd = this.ERPContractGroupUser;
            this.ucERPContractGroupUser.SelectCmdForUpdate = null;
            this.ucERPContractGroupUser.SendSQLCmd = true;
            this.ucERPContractGroupUser.ServerModify = true;
            this.ucERPContractGroupUser.ServerModifyGetMax = false;
            this.ucERPContractGroupUser.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPContractGroupUser.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPContractGroupUser.UseTranscationScope = false;
            this.ucERPContractGroupUser.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ERPContractGroup
            // 
            this.View_ERPContractGroup.CacheConnection = false;
            this.View_ERPContractGroup.CommandText = "SELECT * FROM dbo.[ERPContractGroup]";
            this.View_ERPContractGroup.CommandTimeout = 30;
            this.View_ERPContractGroup.CommandType = System.Data.CommandType.Text;
            this.View_ERPContractGroup.DynamicTableName = false;
            this.View_ERPContractGroup.EEPAlias = null;
            this.View_ERPContractGroup.EncodingAfter = null;
            this.View_ERPContractGroup.EncodingBefore = "Windows-1252";
            this.View_ERPContractGroup.EncodingConvert = null;
            this.View_ERPContractGroup.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "CENTER_ID";
            this.View_ERPContractGroup.KeyFields.Add(keyItem4);
            this.View_ERPContractGroup.MultiSetWhere = false;
            this.View_ERPContractGroup.Name = "View_ERPContractGroup";
            this.View_ERPContractGroup.NotificationAutoEnlist = false;
            this.View_ERPContractGroup.SecExcept = null;
            this.View_ERPContractGroup.SecFieldName = null;
            this.View_ERPContractGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPContractGroup.SelectPaging = false;
            this.View_ERPContractGroup.SelectTop = 0;
            this.View_ERPContractGroup.SiteControl = false;
            this.View_ERPContractGroup.SiteFieldName = null;
            this.View_ERPContractGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            // 
            // SYS_ORG
            // 
            this.SYS_ORG.CacheConnection = false;
            this.SYS_ORG.CommandText = "select distinct o.ORG_NO,o.ORG_NO+\'-\'+o.ORG_DESC as ORG_DESC\r\nFROM [EIPHRSYS].[db" +
    "o].[SYS_ORG] o";
            this.SYS_ORG.CommandTimeout = 30;
            this.SYS_ORG.CommandType = System.Data.CommandType.Text;
            this.SYS_ORG.DynamicTableName = false;
            this.SYS_ORG.EEPAlias = "EIPHRSYS";
            this.SYS_ORG.EncodingAfter = null;
            this.SYS_ORG.EncodingBefore = "Windows-1252";
            this.SYS_ORG.EncodingConvert = null;
            this.SYS_ORG.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "ORG_NO";
            this.SYS_ORG.KeyFields.Add(keyItem6);
            this.SYS_ORG.MultiSetWhere = false;
            this.SYS_ORG.Name = "SYS_ORG";
            this.SYS_ORG.NotificationAutoEnlist = false;
            this.SYS_ORG.SecExcept = null;
            this.SYS_ORG.SecFieldName = null;
            this.SYS_ORG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SYS_ORG.SelectPaging = false;
            this.SYS_ORG.SelectTop = 0;
            this.SYS_ORG.SiteControl = false;
            this.SYS_ORG.SiteFieldName = null;
            this.SYS_ORG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContractGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContractGroupUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContractGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ORG)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPContractGroup;
        private Srvtools.UpdateComponent ucERPContractGroup;
        private Srvtools.InfoCommand ERPContractGroupUser;
        private Srvtools.UpdateComponent ucERPContractGroupUser;
        private Srvtools.InfoCommand View_ERPContractGroup;
        private Srvtools.InfoCommand USERS;
        private Srvtools.InfoCommand SYS_ORG;
    }
}
