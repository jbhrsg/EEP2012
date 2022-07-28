namespace sForm_ISODocumentModify
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
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ISODocument = new Srvtools.InfoCommand(this.components);
            this.ucISODocument = new Srvtools.UpdateComponent(this.components);
            this.View_ISODocument = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISODocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ISODocument)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ISODocument
            // 
            this.ISODocument.CacheConnection = false;
            this.ISODocument.CommandText = "SELECT dbo.[ISODocument].* FROM dbo.[ISODocument]";
            this.ISODocument.CommandTimeout = 30;
            this.ISODocument.CommandType = System.Data.CommandType.Text;
            this.ISODocument.DynamicTableName = false;
            this.ISODocument.EEPAlias = null;
            this.ISODocument.EncodingAfter = null;
            this.ISODocument.EncodingBefore = "Windows-1252";
            this.ISODocument.EncodingConvert = null;
            this.ISODocument.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "DocNO";
            this.ISODocument.KeyFields.Add(keyItem1);
            this.ISODocument.MultiSetWhere = false;
            this.ISODocument.Name = "ISODocument";
            this.ISODocument.NotificationAutoEnlist = false;
            this.ISODocument.SecExcept = null;
            this.ISODocument.SecFieldName = null;
            this.ISODocument.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ISODocument.SelectPaging = false;
            this.ISODocument.SelectTop = 0;
            this.ISODocument.SiteControl = false;
            this.ISODocument.SiteFieldName = null;
            this.ISODocument.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucISODocument
            // 
            this.ucISODocument.AutoTrans = true;
            this.ucISODocument.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "DocNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "DocPaperNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "FirstNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SecondNO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "DocPropertyNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "DocName";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "PdfFile";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "WordFile";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "WhoCanDownload";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "LastUpdateBy";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "FlowFlag";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            this.ucISODocument.FieldAttrs.Add(fieldAttr1);
            this.ucISODocument.FieldAttrs.Add(fieldAttr2);
            this.ucISODocument.FieldAttrs.Add(fieldAttr3);
            this.ucISODocument.FieldAttrs.Add(fieldAttr4);
            this.ucISODocument.FieldAttrs.Add(fieldAttr5);
            this.ucISODocument.FieldAttrs.Add(fieldAttr6);
            this.ucISODocument.FieldAttrs.Add(fieldAttr7);
            this.ucISODocument.FieldAttrs.Add(fieldAttr8);
            this.ucISODocument.FieldAttrs.Add(fieldAttr9);
            this.ucISODocument.FieldAttrs.Add(fieldAttr10);
            this.ucISODocument.FieldAttrs.Add(fieldAttr11);
            this.ucISODocument.FieldAttrs.Add(fieldAttr12);
            this.ucISODocument.FieldAttrs.Add(fieldAttr13);
            this.ucISODocument.FieldAttrs.Add(fieldAttr14);
            this.ucISODocument.LogInfo = null;
            this.ucISODocument.Name = null;
            this.ucISODocument.RowAffectsCheck = true;
            this.ucISODocument.SelectCmd = this.ISODocument;
            this.ucISODocument.SelectCmdForUpdate = null;
            this.ucISODocument.SendSQLCmd = true;
            this.ucISODocument.ServerModify = true;
            this.ucISODocument.ServerModifyGetMax = false;
            this.ucISODocument.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucISODocument.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucISODocument.UseTranscationScope = false;
            this.ucISODocument.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ISODocument
            // 
            this.View_ISODocument.CacheConnection = false;
            this.View_ISODocument.CommandText = "SELECT * FROM dbo.[ISODocument]";
            this.View_ISODocument.CommandTimeout = 30;
            this.View_ISODocument.CommandType = System.Data.CommandType.Text;
            this.View_ISODocument.DynamicTableName = false;
            this.View_ISODocument.EEPAlias = null;
            this.View_ISODocument.EncodingAfter = null;
            this.View_ISODocument.EncodingBefore = "Windows-1252";
            this.View_ISODocument.EncodingConvert = null;
            this.View_ISODocument.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DocNO";
            this.View_ISODocument.KeyFields.Add(keyItem2);
            this.View_ISODocument.MultiSetWhere = false;
            this.View_ISODocument.Name = "View_ISODocument";
            this.View_ISODocument.NotificationAutoEnlist = false;
            this.View_ISODocument.SecExcept = null;
            this.View_ISODocument.SecFieldName = null;
            this.View_ISODocument.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ISODocument.SelectPaging = false;
            this.View_ISODocument.SelectTop = 0;
            this.View_ISODocument.SiteControl = false;
            this.View_ISODocument.SiteFieldName = null;
            this.View_ISODocument.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISODocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ISODocument)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ISODocument;
        private Srvtools.UpdateComponent ucISODocument;
        private Srvtools.InfoCommand View_ISODocument;
    }
}
