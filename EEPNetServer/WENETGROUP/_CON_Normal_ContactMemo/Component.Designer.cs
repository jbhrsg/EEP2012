namespace _CON_Normal_ContactMemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
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
            Srvtools.SrcFieldNameColumn srcFieldNameColumn1 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn2 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn3 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn4 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn5 = new Srvtools.SrcFieldNameColumn();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CON_CONTACT_MEMO = new Srvtools.InfoCommand(this.components);
            this.ucCON_CONTACT_MEMO = new Srvtools.UpdateComponent(this.components);
            this.CON_CONTACT_MEMO_LOG = new Srvtools.InfoCommand(this.components);
            this.logInfo_CON_CONTACT_MEMO = new Srvtools.LogInfo(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_MEMO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_MEMO_LOG)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "DataValidate";
            service1.NonLogin = false;
            service1.ServiceName = "DataValidate";
            service2.DelegateName = "FileUpload";
            service2.NonLogin = false;
            service2.ServiceName = "FileUpload";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "WENETGROUP";
            // 
            // CON_CONTACT_MEMO
            // 
            this.CON_CONTACT_MEMO.CacheConnection = false;
            this.CON_CONTACT_MEMO.CommandText = resources.GetString("CON_CONTACT_MEMO.CommandText");
            this.CON_CONTACT_MEMO.CommandTimeout = 30;
            this.CON_CONTACT_MEMO.CommandType = System.Data.CommandType.Text;
            this.CON_CONTACT_MEMO.DynamicTableName = false;
            this.CON_CONTACT_MEMO.EEPAlias = null;
            this.CON_CONTACT_MEMO.EncodingAfter = null;
            this.CON_CONTACT_MEMO.EncodingBefore = "Windows-1252";
            this.CON_CONTACT_MEMO.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CONTACT_MEMO_ID";
            this.CON_CONTACT_MEMO.KeyFields.Add(keyItem1);
            this.CON_CONTACT_MEMO.MultiSetWhere = false;
            this.CON_CONTACT_MEMO.Name = "CON_CONTACT_MEMO";
            this.CON_CONTACT_MEMO.NotificationAutoEnlist = false;
            this.CON_CONTACT_MEMO.SecExcept = null;
            this.CON_CONTACT_MEMO.SecFieldName = null;
            this.CON_CONTACT_MEMO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CONTACT_MEMO.SelectPaging = false;
            this.CON_CONTACT_MEMO.SelectTop = 0;
            this.CON_CONTACT_MEMO.SiteControl = false;
            this.CON_CONTACT_MEMO.SiteFieldName = null;
            this.CON_CONTACT_MEMO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_CONTACT_MEMO
            // 
            this.ucCON_CONTACT_MEMO.AutoTrans = true;
            this.ucCON_CONTACT_MEMO.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CONTACT_MEMO_ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CONTACT_ID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "MEMO_DATE";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "MEMO_CONTENT";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "MEMO_USER";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CREATE_MAN";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = "_username";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CREATE_DATE";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = "_sysdate";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "UPDATE_MAN";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr8.DefaultValue = "_username";
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "UPDATE_DATE";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr9.DefaultValue = "_sysdate";
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr1);
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr2);
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr3);
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr4);
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr5);
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr6);
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr7);
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr8);
            this.ucCON_CONTACT_MEMO.FieldAttrs.Add(fieldAttr9);
            this.ucCON_CONTACT_MEMO.LogInfo = null;
            this.ucCON_CONTACT_MEMO.Name = "ucCON_CONTACT_MEMO";
            this.ucCON_CONTACT_MEMO.RowAffectsCheck = true;
            this.ucCON_CONTACT_MEMO.SelectCmd = this.CON_CONTACT_MEMO;
            this.ucCON_CONTACT_MEMO.SelectCmdForUpdate = null;
            this.ucCON_CONTACT_MEMO.ServerModify = true;
            this.ucCON_CONTACT_MEMO.ServerModifyGetMax = false;
            this.ucCON_CONTACT_MEMO.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_CONTACT_MEMO.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_CONTACT_MEMO.UseTranscationScope = false;
            this.ucCON_CONTACT_MEMO.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCON_CONTACT_MEMO.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCON_CONTACT_MEMO_BeforeInsert);
            this.ucCON_CONTACT_MEMO.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCON_CONTACT_MEMO_BeforeModify);
            // 
            // CON_CONTACT_MEMO_LOG
            // 
            this.CON_CONTACT_MEMO_LOG.CacheConnection = false;
            this.CON_CONTACT_MEMO_LOG.CommandText = resources.GetString("CON_CONTACT_MEMO_LOG.CommandText");
            this.CON_CONTACT_MEMO_LOG.CommandTimeout = 30;
            this.CON_CONTACT_MEMO_LOG.CommandType = System.Data.CommandType.Text;
            this.CON_CONTACT_MEMO_LOG.DynamicTableName = false;
            this.CON_CONTACT_MEMO_LOG.EEPAlias = null;
            this.CON_CONTACT_MEMO_LOG.EncodingAfter = null;
            this.CON_CONTACT_MEMO_LOG.EncodingBefore = "Windows-1252";
            this.CON_CONTACT_MEMO_LOG.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "LOG_ID";
            this.CON_CONTACT_MEMO_LOG.KeyFields.Add(keyItem2);
            this.CON_CONTACT_MEMO_LOG.MultiSetWhere = false;
            this.CON_CONTACT_MEMO_LOG.Name = "CON_CONTACT_MEMO_LOG";
            this.CON_CONTACT_MEMO_LOG.NotificationAutoEnlist = false;
            this.CON_CONTACT_MEMO_LOG.SecExcept = null;
            this.CON_CONTACT_MEMO_LOG.SecFieldName = null;
            this.CON_CONTACT_MEMO_LOG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CONTACT_MEMO_LOG.SelectPaging = false;
            this.CON_CONTACT_MEMO_LOG.SelectTop = 0;
            this.CON_CONTACT_MEMO_LOG.SiteControl = false;
            this.CON_CONTACT_MEMO_LOG.SiteFieldName = null;
            this.CON_CONTACT_MEMO_LOG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // logInfo_CON_CONTACT_MEMO
            // 
            this.logInfo_CON_CONTACT_MEMO.LogDateType = null;
            this.logInfo_CON_CONTACT_MEMO.LogIDField = "LOG_ID";
            this.logInfo_CON_CONTACT_MEMO.LogTableName = "CON_CONTACT_MEMO_LOG";
            this.logInfo_CON_CONTACT_MEMO.MarkField = "LOG_STATE";
            this.logInfo_CON_CONTACT_MEMO.ModifierField = "LOG_USER";
            this.logInfo_CON_CONTACT_MEMO.ModifyDateField = "LOG_DATE";
            this.logInfo_CON_CONTACT_MEMO.Name = "logInfo_CON_CONTACT_MEMO";
            this.logInfo_CON_CONTACT_MEMO.NeedLog = true;
            this.logInfo_CON_CONTACT_MEMO.OnlyDistinct = false;
            srcFieldNameColumn1.FieldName = "CONTACT_MEMO_ID";
            srcFieldNameColumn2.FieldName = "CONTACT_ID";
            srcFieldNameColumn3.FieldName = "MEMO_DATE";
            srcFieldNameColumn4.FieldName = "MEMO_CONTENT";
            srcFieldNameColumn5.FieldName = "MEMO_USER";
            this.logInfo_CON_CONTACT_MEMO.SrcFieldNames.Add(srcFieldNameColumn1);
            this.logInfo_CON_CONTACT_MEMO.SrcFieldNames.Add(srcFieldNameColumn2);
            this.logInfo_CON_CONTACT_MEMO.SrcFieldNames.Add(srcFieldNameColumn3);
            this.logInfo_CON_CONTACT_MEMO.SrcFieldNames.Add(srcFieldNameColumn4);
            this.logInfo_CON_CONTACT_MEMO.SrcFieldNames.Add(srcFieldNameColumn5);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_MEMO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_MEMO_LOG)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CON_CONTACT_MEMO;
        private Srvtools.UpdateComponent ucCON_CONTACT_MEMO;
        private Srvtools.InfoCommand CON_CONTACT_MEMO_LOG;
        private Srvtools.LogInfo logInfo_CON_CONTACT_MEMO;
    }
}
