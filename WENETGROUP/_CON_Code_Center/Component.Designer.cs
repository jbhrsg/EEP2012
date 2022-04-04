namespace _CON_Code_Center
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
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.Service service4 = new Srvtools.Service();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn1 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn2 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn3 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn4 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn5 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn6 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn7 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn8 = new Srvtools.SrcFieldNameColumn();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CON_CENTER = new Srvtools.InfoCommand(this.components);
            this.ucCON_CENTER = new Srvtools.UpdateComponent(this.components);
            this.View_CON_CENTER = new Srvtools.InfoCommand(this.components);
            this.CON_CENTER_LOG = new Srvtools.InfoCommand(this.components);
            this.logInfo_CON_CENTER = new Srvtools.LogInfo(this.components);
            this.USERS = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_CENTER)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTER_LOG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "checkCenterCname";
            service1.NonLogin = false;
            service1.ServiceName = "checkCenterCname";
            service2.DelegateName = "FileUpload";
            service2.NonLogin = false;
            service2.ServiceName = "FileUpload";
            service3.DelegateName = "updateCetnerAuthorityData";
            service3.NonLogin = false;
            service3.ServiceName = "updateCetnerAuthorityData";
            service4.DelegateName = "getAuthorityDialogData";
            service4.NonLogin = false;
            service4.ServiceName = "getAuthorityDialogData";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "WENETGROUP";
            // 
            // CON_CENTER
            // 
            this.CON_CENTER.CacheConnection = false;
            this.CON_CENTER.CommandText = "SELECT dbo.[CON_CENTER].*,\'異動資料紀錄\' AS TRANSLOG  FROM dbo.[CON_CENTER]";
            this.CON_CENTER.CommandTimeout = 30;
            this.CON_CENTER.CommandType = System.Data.CommandType.Text;
            this.CON_CENTER.DynamicTableName = false;
            this.CON_CENTER.EEPAlias = null;
            this.CON_CENTER.EncodingAfter = null;
            this.CON_CENTER.EncodingBefore = "Windows-1252";
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
            fieldAttr6.DefaultValue = "_sysdate";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "UPDATE_MAN";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr7.DefaultValue = "_username";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "UPDATE_DATE";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr8.DefaultValue = "_sysdate";
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
            this.ucCON_CENTER.ServerModify = true;
            this.ucCON_CENTER.ServerModifyGetMax = false;
            this.ucCON_CENTER.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_CENTER.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_CENTER.UseTranscationScope = false;
            this.ucCON_CENTER.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCON_CENTER.AfterInsert += new Srvtools.UpdateComponentAfterInsertEventHandler(this.ucCON_CENTER_AfterInsert);
            this.ucCON_CENTER.AfterDelete += new Srvtools.UpdateComponentAfterDeleteEventHandler(this.ucCON_CENTER_AfterDelete);
            this.ucCON_CENTER.AfterModify += new Srvtools.UpdateComponentAfterModifyEventHandler(this.ucCON_CENTER_AfterModify);
            // 
            // View_CON_CENTER
            // 
            this.View_CON_CENTER.CacheConnection = false;
            this.View_CON_CENTER.CommandText = "SELECT * FROM dbo.[CON_CENTER]";
            this.View_CON_CENTER.CommandTimeout = 30;
            this.View_CON_CENTER.CommandType = System.Data.CommandType.Text;
            this.View_CON_CENTER.DynamicTableName = false;
            this.View_CON_CENTER.EEPAlias = null;
            this.View_CON_CENTER.EncodingAfter = null;
            this.View_CON_CENTER.EncodingBefore = "Windows-1252";
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
            // CON_CENTER_LOG
            // 
            this.CON_CENTER_LOG.CacheConnection = false;
            this.CON_CENTER_LOG.CommandText = resources.GetString("CON_CENTER_LOG.CommandText");
            this.CON_CENTER_LOG.CommandTimeout = 30;
            this.CON_CENTER_LOG.CommandType = System.Data.CommandType.Text;
            this.CON_CENTER_LOG.DynamicTableName = false;
            this.CON_CENTER_LOG.EEPAlias = null;
            this.CON_CENTER_LOG.EncodingAfter = null;
            this.CON_CENTER_LOG.EncodingBefore = "Windows-1252";
            this.CON_CENTER_LOG.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "LOG_ID";
            this.CON_CENTER_LOG.KeyFields.Add(keyItem3);
            this.CON_CENTER_LOG.MultiSetWhere = false;
            this.CON_CENTER_LOG.Name = "CON_CENTER_LOG";
            this.CON_CENTER_LOG.NotificationAutoEnlist = false;
            this.CON_CENTER_LOG.SecExcept = null;
            this.CON_CENTER_LOG.SecFieldName = null;
            this.CON_CENTER_LOG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CENTER_LOG.SelectPaging = false;
            this.CON_CENTER_LOG.SelectTop = 0;
            this.CON_CENTER_LOG.SiteControl = false;
            this.CON_CENTER_LOG.SiteFieldName = null;
            this.CON_CENTER_LOG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // logInfo_CON_CENTER
            // 
            this.logInfo_CON_CENTER.LogDateType = null;
            this.logInfo_CON_CENTER.LogIDField = "LOG_ID";
            this.logInfo_CON_CENTER.LogTableName = "CON_CENTER_LOG";
            this.logInfo_CON_CENTER.MarkField = "LOG_STATE";
            this.logInfo_CON_CENTER.ModifierField = "LOG_USER";
            this.logInfo_CON_CENTER.ModifyDateField = "LOG_DATE";
            this.logInfo_CON_CENTER.Name = "logInfo_CON_CENTER";
            this.logInfo_CON_CENTER.NeedLog = true;
            this.logInfo_CON_CENTER.OnlyDistinct = false;
            srcFieldNameColumn1.FieldName = "CENTER_ID";
            srcFieldNameColumn2.FieldName = "CENTER_CNAME";
            srcFieldNameColumn3.FieldName = "CENTER_ENAME";
            srcFieldNameColumn4.FieldName = "CENTER_SEQ";
            srcFieldNameColumn5.FieldName = "CREATE_MAN";
            srcFieldNameColumn6.FieldName = "CREATE_DATE";
            srcFieldNameColumn7.FieldName = "UPDATE_MAN";
            srcFieldNameColumn8.FieldName = "UPDATE_DATE";
            this.logInfo_CON_CENTER.SrcFieldNames.Add(srcFieldNameColumn1);
            this.logInfo_CON_CENTER.SrcFieldNames.Add(srcFieldNameColumn2);
            this.logInfo_CON_CENTER.SrcFieldNames.Add(srcFieldNameColumn3);
            this.logInfo_CON_CENTER.SrcFieldNames.Add(srcFieldNameColumn4);
            this.logInfo_CON_CENTER.SrcFieldNames.Add(srcFieldNameColumn5);
            this.logInfo_CON_CENTER.SrcFieldNames.Add(srcFieldNameColumn6);
            this.logInfo_CON_CENTER.SrcFieldNames.Add(srcFieldNameColumn7);
            this.logInfo_CON_CENTER.SrcFieldNames.Add(srcFieldNameColumn8);
            // 
            // USERS
            // 
            this.USERS.CacheConnection = false;
            this.USERS.CommandText = "select USERID,USERNAME from USERS";
            this.USERS.CommandTimeout = 30;
            this.USERS.CommandType = System.Data.CommandType.Text;
            this.USERS.DynamicTableName = false;
            this.USERS.EEPAlias = null;
            this.USERS.EncodingAfter = null;
            this.USERS.EncodingBefore = "Windows-1252";
            this.USERS.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "USERID";
            this.USERS.KeyFields.Add(keyItem4);
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
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTER_LOG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CON_CENTER;
        private Srvtools.UpdateComponent ucCON_CENTER;
        private Srvtools.InfoCommand View_CON_CENTER;
        private Srvtools.InfoCommand CON_CENTER_LOG;
        private Srvtools.LogInfo logInfo_CON_CENTER;
        private Srvtools.InfoCommand USERS;
    }
}
