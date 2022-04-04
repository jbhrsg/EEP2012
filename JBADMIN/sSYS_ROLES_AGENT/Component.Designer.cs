namespace sSYS_ROLES_AGENT
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
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SYS_ROLES_AGENT = new Srvtools.InfoCommand(this.components);
            this.ucSYS_ROLES_AGENT = new Srvtools.UpdateComponent(this.components);
            this.View_SYS_ROLES_AGENT = new Srvtools.InfoCommand(this.components);
            this.USERSGROUP = new Srvtools.InfoCommand(this.components);
            this.EIPHRSYS = new Srvtools.InfoConnection(this.components);
            this.FLOWTYPE = new Srvtools.InfoCommand(this.components);
            this.USERS = new Srvtools.InfoCommand(this.components);
            this.GROUPS = new Srvtools.InfoCommand(this.components);
            this.UsersRoles = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ROLES_AGENT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SYS_ROLES_AGENT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERSGROUP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EIPHRSYS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FLOWTYPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersRoles)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckRoleAgentFlow";
            service1.NonLogin = false;
            service1.ServiceName = "CheckRoleAgentFlow";
            service2.DelegateName = "GetEmpRoleStr";
            service2.NonLogin = false;
            service2.ServiceName = "GetEmpRoleStr";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // SYS_ROLES_AGENT
            // 
            this.SYS_ROLES_AGENT.CacheConnection = false;
            this.SYS_ROLES_AGENT.CommandText = "SELECT dbo.[SYS_ROLES_AGENT].* FROM dbo.[SYS_ROLES_AGENT]";
            this.SYS_ROLES_AGENT.CommandTimeout = 30;
            this.SYS_ROLES_AGENT.CommandType = System.Data.CommandType.Text;
            this.SYS_ROLES_AGENT.DynamicTableName = false;
            this.SYS_ROLES_AGENT.EEPAlias = "JBADMIN";
            this.SYS_ROLES_AGENT.EncodingAfter = null;
            this.SYS_ROLES_AGENT.EncodingBefore = "Windows-1252";
            this.SYS_ROLES_AGENT.EncodingConvert = null;
            this.SYS_ROLES_AGENT.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ROLE_ID";
            keyItem2.KeyName = "AGENT";
            keyItem3.KeyName = "FLOW_DESC";
            this.SYS_ROLES_AGENT.KeyFields.Add(keyItem1);
            this.SYS_ROLES_AGENT.KeyFields.Add(keyItem2);
            this.SYS_ROLES_AGENT.KeyFields.Add(keyItem3);
            this.SYS_ROLES_AGENT.MultiSetWhere = false;
            this.SYS_ROLES_AGENT.Name = "SYS_ROLES_AGENT";
            this.SYS_ROLES_AGENT.NotificationAutoEnlist = false;
            this.SYS_ROLES_AGENT.SecExcept = null;
            this.SYS_ROLES_AGENT.SecFieldName = null;
            this.SYS_ROLES_AGENT.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SYS_ROLES_AGENT.SelectPaging = false;
            this.SYS_ROLES_AGENT.SelectTop = 0;
            this.SYS_ROLES_AGENT.SiteControl = false;
            this.SYS_ROLES_AGENT.SiteFieldName = null;
            this.SYS_ROLES_AGENT.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSYS_ROLES_AGENT
            // 
            this.ucSYS_ROLES_AGENT.AutoTrans = true;
            this.ucSYS_ROLES_AGENT.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ROLE_ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "AGENT";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "FLOW_DESC";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "START_DATE";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "START_TIME";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "END_DATE";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "END_TIME";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "PAR_AGENT";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "REMARK";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CREATEBY";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = "_username";
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CREATEDATE";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "LASTUPDATEBY";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = "_username";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LASTUPDATEDATE";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr1);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr2);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr3);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr4);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr5);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr6);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr7);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr8);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr9);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr10);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr11);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr12);
            this.ucSYS_ROLES_AGENT.FieldAttrs.Add(fieldAttr13);
            this.ucSYS_ROLES_AGENT.LogInfo = null;
            this.ucSYS_ROLES_AGENT.Name = "ucSYS_ROLES_AGENT";
            this.ucSYS_ROLES_AGENT.RowAffectsCheck = false;
            this.ucSYS_ROLES_AGENT.SelectCmd = this.SYS_ROLES_AGENT;
            this.ucSYS_ROLES_AGENT.SelectCmdForUpdate = null;
            this.ucSYS_ROLES_AGENT.SendSQLCmd = true;
            this.ucSYS_ROLES_AGENT.ServerModify = true;
            this.ucSYS_ROLES_AGENT.ServerModifyGetMax = false;
            this.ucSYS_ROLES_AGENT.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSYS_ROLES_AGENT.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSYS_ROLES_AGENT.UseTranscationScope = false;
            this.ucSYS_ROLES_AGENT.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucSYS_ROLES_AGENT.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucSYS_ROLES_AGENT_BeforeInsert);
            this.ucSYS_ROLES_AGENT.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucSYS_ROLES_AGENT_BeforeModify);
            // 
            // View_SYS_ROLES_AGENT
            // 
            this.View_SYS_ROLES_AGENT.CacheConnection = false;
            this.View_SYS_ROLES_AGENT.CommandText = "SELECT * FROM dbo.[SYS_ROLES_AGENT]";
            this.View_SYS_ROLES_AGENT.CommandTimeout = 30;
            this.View_SYS_ROLES_AGENT.CommandType = System.Data.CommandType.Text;
            this.View_SYS_ROLES_AGENT.DynamicTableName = false;
            this.View_SYS_ROLES_AGENT.EEPAlias = null;
            this.View_SYS_ROLES_AGENT.EncodingAfter = null;
            this.View_SYS_ROLES_AGENT.EncodingBefore = "Windows-1252";
            this.View_SYS_ROLES_AGENT.EncodingConvert = null;
            this.View_SYS_ROLES_AGENT.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ROLE_ID";
            keyItem5.KeyName = "AGENT";
            this.View_SYS_ROLES_AGENT.KeyFields.Add(keyItem4);
            this.View_SYS_ROLES_AGENT.KeyFields.Add(keyItem5);
            this.View_SYS_ROLES_AGENT.MultiSetWhere = false;
            this.View_SYS_ROLES_AGENT.Name = "View_SYS_ROLES_AGENT";
            this.View_SYS_ROLES_AGENT.NotificationAutoEnlist = false;
            this.View_SYS_ROLES_AGENT.SecExcept = null;
            this.View_SYS_ROLES_AGENT.SecFieldName = null;
            this.View_SYS_ROLES_AGENT.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SYS_ROLES_AGENT.SelectPaging = false;
            this.View_SYS_ROLES_AGENT.SelectTop = 0;
            this.View_SYS_ROLES_AGENT.SiteControl = false;
            this.View_SYS_ROLES_AGENT.SiteFieldName = null;
            this.View_SYS_ROLES_AGENT.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // USERSGROUP
            // 
            this.USERSGROUP.CacheConnection = false;
            this.USERSGROUP.CommandText = "SELECT U.USERID,U.GROUPID,G.GROUPNAME\r\nFROM \r\nUSERGROUPS U\r\nINNER JOIN GROUPS G O" +
    "N U.GROUPID=G.GROUPID\r\nWHERE G.ISROLE=\'Y\'";
            this.USERSGROUP.CommandTimeout = 30;
            this.USERSGROUP.CommandType = System.Data.CommandType.Text;
            this.USERSGROUP.DynamicTableName = false;
            this.USERSGROUP.EEPAlias = "EIPHRSYS";
            this.USERSGROUP.EncodingAfter = null;
            this.USERSGROUP.EncodingBefore = "Windows-1252";
            this.USERSGROUP.EncodingConvert = null;
            this.USERSGROUP.InfoConnection = this.EIPHRSYS;
            this.USERSGROUP.MultiSetWhere = false;
            this.USERSGROUP.Name = "USERSGROUP";
            this.USERSGROUP.NotificationAutoEnlist = false;
            this.USERSGROUP.SecExcept = null;
            this.USERSGROUP.SecFieldName = null;
            this.USERSGROUP.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.USERSGROUP.SelectPaging = false;
            this.USERSGROUP.SelectTop = 0;
            this.USERSGROUP.SiteControl = false;
            this.USERSGROUP.SiteFieldName = null;
            this.USERSGROUP.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // EIPHRSYS
            // 
            this.EIPHRSYS.EEPAlias = "EIPHRSYS";
            // 
            // FLOWTYPE
            // 
            this.FLOWTYPE.CacheConnection = false;
            this.FLOWTYPE.CommandText = "SELECT DISTINCT FLOW_DESC \r\nFROM  SYS_TODOLIST\r\nWHERE FLOW_DESC<>\'工作需求單申請\'\r\nUNION" +
    " \r\nSELECT \'*\' AS FLOW_DESC\r\n\r\nORDER BY FLOW_DESC ";
            this.FLOWTYPE.CommandTimeout = 30;
            this.FLOWTYPE.CommandType = System.Data.CommandType.Text;
            this.FLOWTYPE.DynamicTableName = false;
            this.FLOWTYPE.EEPAlias = "EIPHRSYS";
            this.FLOWTYPE.EncodingAfter = null;
            this.FLOWTYPE.EncodingBefore = "Windows-1252";
            this.FLOWTYPE.EncodingConvert = null;
            this.FLOWTYPE.InfoConnection = this.EIPHRSYS;
            this.FLOWTYPE.MultiSetWhere = false;
            this.FLOWTYPE.Name = "FLOWTYPE";
            this.FLOWTYPE.NotificationAutoEnlist = false;
            this.FLOWTYPE.SecExcept = null;
            this.FLOWTYPE.SecFieldName = null;
            this.FLOWTYPE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FLOWTYPE.SelectPaging = false;
            this.FLOWTYPE.SelectTop = 0;
            this.FLOWTYPE.SiteControl = false;
            this.FLOWTYPE.SiteFieldName = null;
            this.FLOWTYPE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // USERS
            // 
            this.USERS.CacheConnection = false;
            this.USERS.CommandText = "SELECT USERID,RTRIM(USERID)+\' \'+USERNAME  AS USERNAME\r\nFROM USERS\r\nWHERE DESCRIPT" +
    "ION=\'JB\'  AND USERID<>\'HR01\'\r\nORDER BY USERID";
            this.USERS.CommandTimeout = 30;
            this.USERS.CommandType = System.Data.CommandType.Text;
            this.USERS.DynamicTableName = false;
            this.USERS.EEPAlias = "EIPHRSYS";
            this.USERS.EncodingAfter = null;
            this.USERS.EncodingBefore = "Windows-1252";
            this.USERS.EncodingConvert = null;
            this.USERS.InfoConnection = this.EIPHRSYS;
            keyItem6.KeyName = "USERID";
            this.USERS.KeyFields.Add(keyItem6);
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
            // GROUPS
            // 
            this.GROUPS.CacheConnection = false;
            this.GROUPS.CommandText = "SELECT GROUPID,\r\n               GROUPNAME+ \' / \' + GROUPID AS GROUPNAME\r\nFROM GRO" +
    "UPS\r\nWHERE ISROLE=\'Y\'\r\nORDER BY GROUPID";
            this.GROUPS.CommandTimeout = 30;
            this.GROUPS.CommandType = System.Data.CommandType.Text;
            this.GROUPS.DynamicTableName = false;
            this.GROUPS.EEPAlias = "EIPHRSYS";
            this.GROUPS.EncodingAfter = null;
            this.GROUPS.EncodingBefore = "Windows-1252";
            this.GROUPS.EncodingConvert = null;
            this.GROUPS.InfoConnection = this.EIPHRSYS;
            keyItem7.KeyName = "GROUPID";
            this.GROUPS.KeyFields.Add(keyItem7);
            this.GROUPS.MultiSetWhere = false;
            this.GROUPS.Name = "GROUPS";
            this.GROUPS.NotificationAutoEnlist = false;
            this.GROUPS.SecExcept = null;
            this.GROUPS.SecFieldName = null;
            this.GROUPS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.GROUPS.SelectPaging = false;
            this.GROUPS.SelectTop = 0;
            this.GROUPS.SiteControl = false;
            this.GROUPS.SiteFieldName = null;
            this.GROUPS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // UsersRoles
            // 
            this.UsersRoles.CacheConnection = false;
            this.UsersRoles.CommandText = "SELECT USERID,\r\n               dbo.funReturnEmpRoles(USERID) AS ROLESTR\r\nFROM EIP" +
    "HRSYS.DBO.USERS\r\nWHERE DESCRIPTION=\'JB\'";
            this.UsersRoles.CommandTimeout = 30;
            this.UsersRoles.CommandType = System.Data.CommandType.Text;
            this.UsersRoles.DynamicTableName = false;
            this.UsersRoles.EEPAlias = "JBADMIN";
            this.UsersRoles.EncodingAfter = null;
            this.UsersRoles.EncodingBefore = "Windows-1252";
            this.UsersRoles.EncodingConvert = null;
            this.UsersRoles.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "USERID";
            this.UsersRoles.KeyFields.Add(keyItem8);
            this.UsersRoles.MultiSetWhere = false;
            this.UsersRoles.Name = "UsersRoles";
            this.UsersRoles.NotificationAutoEnlist = false;
            this.UsersRoles.SecExcept = null;
            this.UsersRoles.SecFieldName = null;
            this.UsersRoles.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UsersRoles.SelectPaging = false;
            this.UsersRoles.SelectTop = 0;
            this.UsersRoles.SiteControl = false;
            this.UsersRoles.SiteFieldName = null;
            this.UsersRoles.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_ROLES_AGENT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SYS_ROLES_AGENT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERSGROUP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EIPHRSYS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FLOWTYPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersRoles)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SYS_ROLES_AGENT;
        private Srvtools.UpdateComponent ucSYS_ROLES_AGENT;
        private Srvtools.InfoCommand View_SYS_ROLES_AGENT;
        private Srvtools.InfoCommand USERSGROUP;
        private Srvtools.InfoCommand FLOWTYPE;
        private Srvtools.InfoConnection EIPHRSYS;
        private Srvtools.InfoCommand USERS;
        private Srvtools.InfoCommand GROUPS;
        private Srvtools.InfoCommand UsersRoles;
    }
}
