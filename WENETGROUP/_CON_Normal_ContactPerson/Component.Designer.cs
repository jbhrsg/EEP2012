namespace _CON_Normal_ContactPerson
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn1 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn2 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn3 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn4 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn5 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn6 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn7 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn8 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn9 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn10 = new Srvtools.SrcFieldNameColumn();
            this.TheServiceManager = new Srvtools.ServiceManager(this.components);
            this.TheInfoConnection = new Srvtools.InfoConnection(this.components);
            this.CON_CONTACT_PERSON = new Srvtools.InfoCommand(this.components);
            this.ucCON_CONTACT_PERSON = new Srvtools.UpdateComponent(this.components);
            this.CON_CONTACT_PERSON_LOG = new Srvtools.InfoCommand(this.components);
            this.logCON_CONTACT_PERSON = new Srvtools.LogInfo(this.components);
            this.cb_CON_CONTACT = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TheInfoConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_PERSON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_PERSON_LOG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_CON_CONTACT)).BeginInit();
            // 
            // TheInfoConnection
            // 
            this.TheInfoConnection.EEPAlias = "WENETGROUP";
            // 
            // CON_CONTACT_PERSON
            // 
            this.CON_CONTACT_PERSON.CacheConnection = false;
            this.CON_CONTACT_PERSON.CommandText = "Select\tCPR.*,\r\n\t\tCON.CONTACT_NAME,\r\n\t\tCON.CENTER_ID\r\nFrom\t[dbo].CON_CONTACT_PERSO" +
    "N\t\t\t\tas CPR\r\n\t\tLeft Join [dbo].[CON_CONTACT]\t\t\tas CON on CPR.CONTACT_ID = CON.CO" +
    "NTACT_ID\r\nOrder By CPR.BEGIN_DATE Desc";
            this.CON_CONTACT_PERSON.CommandTimeout = 30;
            this.CON_CONTACT_PERSON.CommandType = System.Data.CommandType.Text;
            this.CON_CONTACT_PERSON.DynamicTableName = false;
            this.CON_CONTACT_PERSON.EEPAlias = null;
            this.CON_CONTACT_PERSON.EncodingAfter = null;
            this.CON_CONTACT_PERSON.EncodingBefore = "Windows-1252";
            this.CON_CONTACT_PERSON.InfoConnection = this.TheInfoConnection;
            keyItem1.KeyName = "CONTACT_PERSON_ID";
            this.CON_CONTACT_PERSON.KeyFields.Add(keyItem1);
            this.CON_CONTACT_PERSON.MultiSetWhere = false;
            this.CON_CONTACT_PERSON.Name = "CON_CONTACT_PERSON";
            this.CON_CONTACT_PERSON.NotificationAutoEnlist = false;
            this.CON_CONTACT_PERSON.SecExcept = null;
            this.CON_CONTACT_PERSON.SecFieldName = "";
            this.CON_CONTACT_PERSON.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CONTACT_PERSON.SelectPaging = false;
            this.CON_CONTACT_PERSON.SelectTop = 0;
            this.CON_CONTACT_PERSON.SiteControl = true;
            this.CON_CONTACT_PERSON.SiteFieldName = "CENTER_ID";
            this.CON_CONTACT_PERSON.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_CONTACT_PERSON
            // 
            this.ucCON_CONTACT_PERSON.AutoTrans = true;
            this.ucCON_CONTACT_PERSON.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CREATE_MAN";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = "_username";
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CREATE_DATE";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = "_sysdate";
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "UPDATE_MAN";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr3.DefaultValue = "_username";
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "UPDATE_DATE";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr4.DefaultValue = "_sysdate";
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucCON_CONTACT_PERSON.FieldAttrs.Add(fieldAttr1);
            this.ucCON_CONTACT_PERSON.FieldAttrs.Add(fieldAttr2);
            this.ucCON_CONTACT_PERSON.FieldAttrs.Add(fieldAttr3);
            this.ucCON_CONTACT_PERSON.FieldAttrs.Add(fieldAttr4);
            this.ucCON_CONTACT_PERSON.LogInfo = null;
            this.ucCON_CONTACT_PERSON.Name = "ucCON_CONTACT_PERSON";
            this.ucCON_CONTACT_PERSON.RowAffectsCheck = true;
            this.ucCON_CONTACT_PERSON.SelectCmd = this.CON_CONTACT_PERSON;
            this.ucCON_CONTACT_PERSON.SelectCmdForUpdate = null;
            this.ucCON_CONTACT_PERSON.ServerModify = true;
            this.ucCON_CONTACT_PERSON.ServerModifyGetMax = false;
            this.ucCON_CONTACT_PERSON.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_CONTACT_PERSON.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_CONTACT_PERSON.UseTranscationScope = false;
            this.ucCON_CONTACT_PERSON.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // CON_CONTACT_PERSON_LOG
            // 
            this.CON_CONTACT_PERSON_LOG.CacheConnection = false;
            this.CON_CONTACT_PERSON_LOG.CommandText = resources.GetString("CON_CONTACT_PERSON_LOG.CommandText");
            this.CON_CONTACT_PERSON_LOG.CommandTimeout = 30;
            this.CON_CONTACT_PERSON_LOG.CommandType = System.Data.CommandType.Text;
            this.CON_CONTACT_PERSON_LOG.DynamicTableName = false;
            this.CON_CONTACT_PERSON_LOG.EEPAlias = null;
            this.CON_CONTACT_PERSON_LOG.EncodingAfter = null;
            this.CON_CONTACT_PERSON_LOG.EncodingBefore = "Windows-1252";
            this.CON_CONTACT_PERSON_LOG.InfoConnection = this.TheInfoConnection;
            keyItem2.KeyName = "LOG_ID";
            this.CON_CONTACT_PERSON_LOG.KeyFields.Add(keyItem2);
            this.CON_CONTACT_PERSON_LOG.MultiSetWhere = false;
            this.CON_CONTACT_PERSON_LOG.Name = "CON_CONTACT_PERSON_LOG";
            this.CON_CONTACT_PERSON_LOG.NotificationAutoEnlist = false;
            this.CON_CONTACT_PERSON_LOG.SecExcept = null;
            this.CON_CONTACT_PERSON_LOG.SecFieldName = null;
            this.CON_CONTACT_PERSON_LOG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CONTACT_PERSON_LOG.SelectPaging = false;
            this.CON_CONTACT_PERSON_LOG.SelectTop = 0;
            this.CON_CONTACT_PERSON_LOG.SiteControl = false;
            this.CON_CONTACT_PERSON_LOG.SiteFieldName = null;
            this.CON_CONTACT_PERSON_LOG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // logCON_CONTACT_PERSON
            // 
            this.logCON_CONTACT_PERSON.LogDateType = null;
            this.logCON_CONTACT_PERSON.LogIDField = "LOG_ID";
            this.logCON_CONTACT_PERSON.LogTableName = "CON_CONTACT_PERSON_LOG";
            this.logCON_CONTACT_PERSON.MarkField = "LOG_STATE";
            this.logCON_CONTACT_PERSON.ModifierField = "LOG_USER";
            this.logCON_CONTACT_PERSON.ModifyDateField = "LOG_DATE";
            this.logCON_CONTACT_PERSON.Name = "logInfo_CON_CONTACT_MEMO";
            this.logCON_CONTACT_PERSON.NeedLog = true;
            this.logCON_CONTACT_PERSON.OnlyDistinct = false;
            srcFieldNameColumn1.FieldName = "CONTACT_PERSON_ID";
            srcFieldNameColumn2.FieldName = "CONTACT_ID";
            srcFieldNameColumn3.FieldName = "NAME";
            srcFieldNameColumn4.FieldName = "MEMO";
            srcFieldNameColumn5.FieldName = "BEGIN_DATE";
            srcFieldNameColumn6.FieldName = "END_DATE";
            srcFieldNameColumn7.FieldName = "CREATE_MAN";
            srcFieldNameColumn8.FieldName = "CREATE_DATE";
            srcFieldNameColumn9.FieldName = "UPDATE_MAN";
            srcFieldNameColumn10.FieldName = "UPDATE_DATE";
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn1);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn2);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn3);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn4);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn5);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn6);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn7);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn8);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn9);
            this.logCON_CONTACT_PERSON.SrcFieldNames.Add(srcFieldNameColumn10);
            // 
            // cb_CON_CONTACT
            // 
            this.cb_CON_CONTACT.CacheConnection = false;
            this.cb_CON_CONTACT.CommandText = resources.GetString("cb_CON_CONTACT.CommandText");
            this.cb_CON_CONTACT.CommandTimeout = 30;
            this.cb_CON_CONTACT.CommandType = System.Data.CommandType.Text;
            this.cb_CON_CONTACT.DynamicTableName = false;
            this.cb_CON_CONTACT.EEPAlias = null;
            this.cb_CON_CONTACT.EncodingAfter = null;
            this.cb_CON_CONTACT.EncodingBefore = "Windows-1252";
            this.cb_CON_CONTACT.InfoConnection = this.TheInfoConnection;
            this.cb_CON_CONTACT.MultiSetWhere = false;
            this.cb_CON_CONTACT.Name = "cb_CON_CONTACT";
            this.cb_CON_CONTACT.NotificationAutoEnlist = false;
            this.cb_CON_CONTACT.SecExcept = null;
            this.cb_CON_CONTACT.SecFieldName = null;
            this.cb_CON_CONTACT.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.cb_CON_CONTACT.SelectPaging = false;
            this.cb_CON_CONTACT.SelectTop = 0;
            this.cb_CON_CONTACT.SiteControl = false;
            this.cb_CON_CONTACT.SiteFieldName = null;
            this.cb_CON_CONTACT.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.TheInfoConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_PERSON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CONTACT_PERSON_LOG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_CON_CONTACT)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager TheServiceManager;
        private Srvtools.InfoConnection TheInfoConnection;
        private Srvtools.InfoCommand CON_CONTACT_PERSON;
        private Srvtools.UpdateComponent ucCON_CONTACT_PERSON;
        private Srvtools.InfoCommand CON_CONTACT_PERSON_LOG;
        private Srvtools.LogInfo logCON_CONTACT_PERSON;
        private Srvtools.InfoCommand cb_CON_CONTACT;
    }
}
