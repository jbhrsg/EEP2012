namespace _CON_Normal_Gift
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
            Srvtools.SrcFieldNameColumn srcFieldNameColumn11 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn12 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn13 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn14 = new Srvtools.SrcFieldNameColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CON_GIFT = new Srvtools.InfoCommand(this.components);
            this.ucCON_GIFT = new Srvtools.UpdateComponent(this.components);
            this.View_CON_GIFT = new Srvtools.InfoCommand(this.components);
            this.logInfo_CON_GIFT = new Srvtools.LogInfo(this.components);
            this.CON_GIFT_LOG = new Srvtools.InfoCommand(this.components);
            this.CON_GIFT_LEVEL = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_GIFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_GIFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_GIFT_LOG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_GIFT_LEVEL)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "checkGiftCode";
            service1.NonLogin = false;
            service1.ServiceName = "checkGiftCode";
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
            // CON_GIFT
            // 
            this.CON_GIFT.CacheConnection = false;
            this.CON_GIFT.CommandText = "SELECT A.*,\r\nGIFT_LEVEL.NAME as GIFT_LEVELE_NAME\r\nFROM CON_GIFT A\r\nleft join CON_" +
    "SHARECODE GIFT_LEVEL on GIFT_LEVEL.CODE_ID = A.GIFT_LEVEL_ID AND GIFT_LEVEL.FIEL" +
    "DNAME = \'GIFT_LEVEL\'\r\n";
            this.CON_GIFT.CommandTimeout = 30;
            this.CON_GIFT.CommandType = System.Data.CommandType.Text;
            this.CON_GIFT.DynamicTableName = false;
            this.CON_GIFT.EEPAlias = null;
            this.CON_GIFT.EncodingAfter = null;
            this.CON_GIFT.EncodingBefore = "Windows-1252";
            this.CON_GIFT.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "GIFT_ID";
            this.CON_GIFT.KeyFields.Add(keyItem1);
            this.CON_GIFT.MultiSetWhere = false;
            this.CON_GIFT.Name = "CON_GIFT";
            this.CON_GIFT.NotificationAutoEnlist = false;
            this.CON_GIFT.SecExcept = null;
            this.CON_GIFT.SecFieldName = null;
            this.CON_GIFT.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_GIFT.SelectPaging = false;
            this.CON_GIFT.SelectTop = 0;
            this.CON_GIFT.SiteControl = false;
            this.CON_GIFT.SiteFieldName = null;
            this.CON_GIFT.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_GIFT
            // 
            this.ucCON_GIFT.AutoTrans = true;
            this.ucCON_GIFT.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "GIFT_ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "GIFT_CODE";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "GIFT_NAME";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "GIFT_PHOTO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "GIFT_URL";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "GIFT_PRICE";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "GIFT_MEMO";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "GIFT_MEMO1";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "GIFT_YEAR";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CREATE_MAN";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = "_username";
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CREATE_DATE";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = "_sysdate";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "UPDATE_MAN";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr12.DefaultValue = "_username";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "UPDATE_DATE";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr13.DefaultValue = "_sysdate";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr1);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr2);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr3);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr4);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr5);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr6);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr7);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr8);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr9);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr10);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr11);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr12);
            this.ucCON_GIFT.FieldAttrs.Add(fieldAttr13);
            this.ucCON_GIFT.LogInfo = null;
            this.ucCON_GIFT.Name = "ucCON_GIFT";
            this.ucCON_GIFT.RowAffectsCheck = true;
            this.ucCON_GIFT.SelectCmd = this.CON_GIFT;
            this.ucCON_GIFT.SelectCmdForUpdate = null;
            this.ucCON_GIFT.ServerModify = true;
            this.ucCON_GIFT.ServerModifyGetMax = false;
            this.ucCON_GIFT.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_GIFT.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_GIFT.UseTranscationScope = false;
            this.ucCON_GIFT.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCON_GIFT.AfterInsert += new Srvtools.UpdateComponentAfterInsertEventHandler(this.ucCON_GIFT_AfterInsert);
            this.ucCON_GIFT.AfterDelete += new Srvtools.UpdateComponentAfterDeleteEventHandler(this.ucCON_GIFT_AfterDelete);
            this.ucCON_GIFT.AfterModify += new Srvtools.UpdateComponentAfterModifyEventHandler(this.ucCON_GIFT_AfterModify);
            // 
            // View_CON_GIFT
            // 
            this.View_CON_GIFT.CacheConnection = false;
            this.View_CON_GIFT.CommandText = "SELECT * FROM dbo.[CON_GIFT]";
            this.View_CON_GIFT.CommandTimeout = 30;
            this.View_CON_GIFT.CommandType = System.Data.CommandType.Text;
            this.View_CON_GIFT.DynamicTableName = false;
            this.View_CON_GIFT.EEPAlias = null;
            this.View_CON_GIFT.EncodingAfter = null;
            this.View_CON_GIFT.EncodingBefore = "Windows-1252";
            this.View_CON_GIFT.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "GIFT_ID";
            this.View_CON_GIFT.KeyFields.Add(keyItem2);
            this.View_CON_GIFT.MultiSetWhere = false;
            this.View_CON_GIFT.Name = "View_CON_GIFT";
            this.View_CON_GIFT.NotificationAutoEnlist = false;
            this.View_CON_GIFT.SecExcept = null;
            this.View_CON_GIFT.SecFieldName = null;
            this.View_CON_GIFT.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CON_GIFT.SelectPaging = false;
            this.View_CON_GIFT.SelectTop = 0;
            this.View_CON_GIFT.SiteControl = false;
            this.View_CON_GIFT.SiteFieldName = null;
            this.View_CON_GIFT.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // logInfo_CON_GIFT
            // 
            this.logInfo_CON_GIFT.LogDateType = null;
            this.logInfo_CON_GIFT.LogIDField = "Log_ID";
            this.logInfo_CON_GIFT.LogTableName = "CON_GIFT_LOG";
            this.logInfo_CON_GIFT.MarkField = "Log_State";
            this.logInfo_CON_GIFT.ModifierField = "Log_User";
            this.logInfo_CON_GIFT.ModifyDateField = "Log_Date";
            this.logInfo_CON_GIFT.Name = "logInfo_CON_GIFT";
            this.logInfo_CON_GIFT.NeedLog = true;
            this.logInfo_CON_GIFT.OnlyDistinct = false;
            srcFieldNameColumn1.FieldName = "GIFT_ID";
            srcFieldNameColumn2.FieldName = "GIFT_CODE";
            srcFieldNameColumn3.FieldName = "GIFT_LEVEL_ID";
            srcFieldNameColumn4.FieldName = "GIFT_NAME";
            srcFieldNameColumn5.FieldName = "GIFT_PHOTO";
            srcFieldNameColumn6.FieldName = "GIFT_URL";
            srcFieldNameColumn7.FieldName = "GIFT_PRICE";
            srcFieldNameColumn8.FieldName = "GIFT_MEMO";
            srcFieldNameColumn9.FieldName = "GIFT_MEMO1";
            srcFieldNameColumn10.FieldName = "GIFT_YEAR";
            srcFieldNameColumn11.FieldName = "CREATE_MAN";
            srcFieldNameColumn12.FieldName = "CREATE_DATE";
            srcFieldNameColumn13.FieldName = "UPDATE_MAN";
            srcFieldNameColumn14.FieldName = "UPDATE_DATE";
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn1);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn2);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn3);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn4);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn5);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn6);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn7);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn8);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn9);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn10);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn11);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn12);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn13);
            this.logInfo_CON_GIFT.SrcFieldNames.Add(srcFieldNameColumn14);
            // 
            // CON_GIFT_LOG
            // 
            this.CON_GIFT_LOG.CacheConnection = false;
            this.CON_GIFT_LOG.CommandText = resources.GetString("CON_GIFT_LOG.CommandText");
            this.CON_GIFT_LOG.CommandTimeout = 30;
            this.CON_GIFT_LOG.CommandType = System.Data.CommandType.Text;
            this.CON_GIFT_LOG.DynamicTableName = false;
            this.CON_GIFT_LOG.EEPAlias = null;
            this.CON_GIFT_LOG.EncodingAfter = null;
            this.CON_GIFT_LOG.EncodingBefore = "Windows-1252";
            this.CON_GIFT_LOG.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "LOG_ID";
            this.CON_GIFT_LOG.KeyFields.Add(keyItem3);
            this.CON_GIFT_LOG.MultiSetWhere = false;
            this.CON_GIFT_LOG.Name = "CON_GIFT_LOG";
            this.CON_GIFT_LOG.NotificationAutoEnlist = false;
            this.CON_GIFT_LOG.SecExcept = null;
            this.CON_GIFT_LOG.SecFieldName = null;
            this.CON_GIFT_LOG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_GIFT_LOG.SelectPaging = false;
            this.CON_GIFT_LOG.SelectTop = 0;
            this.CON_GIFT_LOG.SiteControl = false;
            this.CON_GIFT_LOG.SiteFieldName = null;
            this.CON_GIFT_LOG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CON_GIFT_LEVEL
            // 
            this.CON_GIFT_LEVEL.CacheConnection = false;
            this.CON_GIFT_LEVEL.CommandText = "SELECT * FROM CON_SHARECODE \r\nWHERE FIELDNAME = \'GIFT_LEVEL\' AND DISPLAY=\'Y\'\r\nORD" +
    "ER BY SORT";
            this.CON_GIFT_LEVEL.CommandTimeout = 30;
            this.CON_GIFT_LEVEL.CommandType = System.Data.CommandType.Text;
            this.CON_GIFT_LEVEL.DynamicTableName = false;
            this.CON_GIFT_LEVEL.EEPAlias = null;
            this.CON_GIFT_LEVEL.EncodingAfter = null;
            this.CON_GIFT_LEVEL.EncodingBefore = "Windows-1252";
            this.CON_GIFT_LEVEL.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "CODE_ID";
            this.CON_GIFT_LEVEL.KeyFields.Add(keyItem4);
            this.CON_GIFT_LEVEL.MultiSetWhere = false;
            this.CON_GIFT_LEVEL.Name = "CON_GIFT_LEVEL";
            this.CON_GIFT_LEVEL.NotificationAutoEnlist = false;
            this.CON_GIFT_LEVEL.SecExcept = null;
            this.CON_GIFT_LEVEL.SecFieldName = null;
            this.CON_GIFT_LEVEL.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_GIFT_LEVEL.SelectPaging = false;
            this.CON_GIFT_LEVEL.SelectTop = 0;
            this.CON_GIFT_LEVEL.SiteControl = false;
            this.CON_GIFT_LEVEL.SiteFieldName = null;
            this.CON_GIFT_LEVEL.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_GIFT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_GIFT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_GIFT_LOG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_GIFT_LEVEL)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CON_GIFT;
        private Srvtools.UpdateComponent ucCON_GIFT;
        private Srvtools.InfoCommand View_CON_GIFT;
        private Srvtools.LogInfo logInfo_CON_GIFT;
        private Srvtools.InfoCommand CON_GIFT_LOG;
        private Srvtools.InfoCommand CON_GIFT_LEVEL;
    }
}
