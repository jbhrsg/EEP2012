namespace _CON_Normal_ContactFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn1 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn2 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn3 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn4 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn5 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn6 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn7 = new Srvtools.SrcFieldNameColumn();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CON_CONTACT_FILE = new Srvtools.InfoCommand(this.components);
            this.ucCON_CONTACT_FILE = new Srvtools.UpdateComponent(this.components);
            this.CON_CONTACT_FILE_LOG = new Srvtools.InfoCommand(this.components);
            this.logInfo_CON_CONTACT_FILE = new Srvtools.LogInfo(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_FILE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_FILE_LOG)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "DataValidate";
            service1.NonLogin = false;
            service1.ServiceName = "DataValidate";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "WENETGROUP";
            // 
            // CON_CONTACT_FILE
            // 
            this.CON_CONTACT_FILE.CacheConnection = false;
            this.CON_CONTACT_FILE.CommandText = resources.GetString("CON_CONTACT_FILE.CommandText");
            this.CON_CONTACT_FILE.CommandTimeout = 30;
            this.CON_CONTACT_FILE.CommandType = System.Data.CommandType.Text;
            this.CON_CONTACT_FILE.DynamicTableName = false;
            this.CON_CONTACT_FILE.EEPAlias = null;
            this.CON_CONTACT_FILE.EncodingAfter = null;
            this.CON_CONTACT_FILE.EncodingBefore = "Windows-1252";
            this.CON_CONTACT_FILE.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CONTACT_FILE_ID";
            this.CON_CONTACT_FILE.KeyFields.Add(keyItem1);
            this.CON_CONTACT_FILE.MultiSetWhere = false;
            this.CON_CONTACT_FILE.Name = "CON_CONTACT_FILE";
            this.CON_CONTACT_FILE.NotificationAutoEnlist = false;
            this.CON_CONTACT_FILE.SecExcept = null;
            this.CON_CONTACT_FILE.SecFieldName = null;
            this.CON_CONTACT_FILE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CONTACT_FILE.SelectPaging = false;
            this.CON_CONTACT_FILE.SelectTop = 0;
            this.CON_CONTACT_FILE.SiteControl = false;
            this.CON_CONTACT_FILE.SiteFieldName = null;
            this.CON_CONTACT_FILE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_CONTACT_FILE
            // 
            this.ucCON_CONTACT_FILE.AutoTrans = true;
            this.ucCON_CONTACT_FILE.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CONTACT_FILE_ID";
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
            fieldAttr3.DataField = "CONTACT_FILE";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CREATE_MAN";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = "_username";
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CREATE_DATE";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = "_sysdate";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "UPDATE_MAN";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr6.DefaultValue = "_username";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "UPDATE_DATE";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr7.DefaultValue = "_sysdate";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucCON_CONTACT_FILE.FieldAttrs.Add(fieldAttr1);
            this.ucCON_CONTACT_FILE.FieldAttrs.Add(fieldAttr2);
            this.ucCON_CONTACT_FILE.FieldAttrs.Add(fieldAttr3);
            this.ucCON_CONTACT_FILE.FieldAttrs.Add(fieldAttr4);
            this.ucCON_CONTACT_FILE.FieldAttrs.Add(fieldAttr5);
            this.ucCON_CONTACT_FILE.FieldAttrs.Add(fieldAttr6);
            this.ucCON_CONTACT_FILE.FieldAttrs.Add(fieldAttr7);
            this.ucCON_CONTACT_FILE.LogInfo = null;
            this.ucCON_CONTACT_FILE.Name = "ucCON_CONTACT_FILE";
            this.ucCON_CONTACT_FILE.RowAffectsCheck = true;
            this.ucCON_CONTACT_FILE.SelectCmd = this.CON_CONTACT_FILE;
            this.ucCON_CONTACT_FILE.SelectCmdForUpdate = null;
            this.ucCON_CONTACT_FILE.ServerModify = true;
            this.ucCON_CONTACT_FILE.ServerModifyGetMax = false;
            this.ucCON_CONTACT_FILE.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_CONTACT_FILE.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_CONTACT_FILE.UseTranscationScope = false;
            this.ucCON_CONTACT_FILE.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCON_CONTACT_FILE.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCON_CONTACT_FILE_BeforeInsert);
            this.ucCON_CONTACT_FILE.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucCON_CONTACT_FILE_BeforeModify);
            // 
            // CON_CONTACT_FILE_LOG
            // 
            this.CON_CONTACT_FILE_LOG.CacheConnection = false;
            this.CON_CONTACT_FILE_LOG.CommandText = resources.GetString("CON_CONTACT_FILE_LOG.CommandText");
            this.CON_CONTACT_FILE_LOG.CommandTimeout = 30;
            this.CON_CONTACT_FILE_LOG.CommandType = System.Data.CommandType.Text;
            this.CON_CONTACT_FILE_LOG.DynamicTableName = false;
            this.CON_CONTACT_FILE_LOG.EEPAlias = null;
            this.CON_CONTACT_FILE_LOG.EncodingAfter = null;
            this.CON_CONTACT_FILE_LOG.EncodingBefore = "Windows-1252";
            this.CON_CONTACT_FILE_LOG.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "LOG_ID";
            this.CON_CONTACT_FILE_LOG.KeyFields.Add(keyItem2);
            this.CON_CONTACT_FILE_LOG.MultiSetWhere = false;
            this.CON_CONTACT_FILE_LOG.Name = "CON_CONTACT_FILE_LOG";
            this.CON_CONTACT_FILE_LOG.NotificationAutoEnlist = false;
            this.CON_CONTACT_FILE_LOG.SecExcept = null;
            this.CON_CONTACT_FILE_LOG.SecFieldName = null;
            this.CON_CONTACT_FILE_LOG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CONTACT_FILE_LOG.SelectPaging = false;
            this.CON_CONTACT_FILE_LOG.SelectTop = 0;
            this.CON_CONTACT_FILE_LOG.SiteControl = false;
            this.CON_CONTACT_FILE_LOG.SiteFieldName = null;
            this.CON_CONTACT_FILE_LOG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // logInfo_CON_CONTACT_FILE
            // 
            this.logInfo_CON_CONTACT_FILE.LogDateType = null;
            this.logInfo_CON_CONTACT_FILE.LogIDField = "LOG_ID";
            this.logInfo_CON_CONTACT_FILE.LogTableName = "CON_CONTACT_FILE_LOG";
            this.logInfo_CON_CONTACT_FILE.MarkField = "LOG_STATE";
            this.logInfo_CON_CONTACT_FILE.ModifierField = "LOG_USER";
            this.logInfo_CON_CONTACT_FILE.ModifyDateField = "LOG_DATE";
            this.logInfo_CON_CONTACT_FILE.Name = "logInfo_CON_CONTACT_FILE";
            this.logInfo_CON_CONTACT_FILE.NeedLog = true;
            this.logInfo_CON_CONTACT_FILE.OnlyDistinct = false;
            srcFieldNameColumn1.FieldName = "CONTACT_FILE_ID";
            srcFieldNameColumn2.FieldName = "CONTACT_ID";
            srcFieldNameColumn3.FieldName = "CONTACT_FILE";
            srcFieldNameColumn4.FieldName = "CREATE_MAN";
            srcFieldNameColumn5.FieldName = "CREATE_DATE";
            srcFieldNameColumn6.FieldName = "UPDATE_MAN";
            srcFieldNameColumn7.FieldName = "UPDATE_DATE";
            this.logInfo_CON_CONTACT_FILE.SrcFieldNames.Add(srcFieldNameColumn1);
            this.logInfo_CON_CONTACT_FILE.SrcFieldNames.Add(srcFieldNameColumn2);
            this.logInfo_CON_CONTACT_FILE.SrcFieldNames.Add(srcFieldNameColumn3);
            this.logInfo_CON_CONTACT_FILE.SrcFieldNames.Add(srcFieldNameColumn4);
            this.logInfo_CON_CONTACT_FILE.SrcFieldNames.Add(srcFieldNameColumn5);
            this.logInfo_CON_CONTACT_FILE.SrcFieldNames.Add(srcFieldNameColumn6);
            this.logInfo_CON_CONTACT_FILE.SrcFieldNames.Add(srcFieldNameColumn7);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_FILE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_FILE_LOG)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CON_CONTACT_FILE;
        private Srvtools.UpdateComponent ucCON_CONTACT_FILE;
        private Srvtools.InfoCommand CON_CONTACT_FILE_LOG;
        private Srvtools.LogInfo logInfo_CON_CONTACT_FILE;
    }
}
