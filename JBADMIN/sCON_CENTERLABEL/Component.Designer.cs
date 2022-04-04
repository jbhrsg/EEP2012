namespace sCON_CENTERLABEL
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CON_CENTERLABEL = new Srvtools.InfoCommand(this.components);
            this.ucCON_CENTERLABEL = new Srvtools.UpdateComponent(this.components);
            this.View_CON_CENTERLABEL = new Srvtools.InfoCommand(this.components);
            this.GROUPTYPE = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTERLABEL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_CENTERLABEL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPTYPE)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // CON_CENTERLABEL
            // 
            this.CON_CENTERLABEL.CacheConnection = false;
            this.CON_CENTERLABEL.CommandText = "SELECT dbo.[CON_CENTERLABEL].* FROM dbo.[CON_CENTERLABEL]";
            this.CON_CENTERLABEL.CommandTimeout = 30;
            this.CON_CENTERLABEL.CommandType = System.Data.CommandType.Text;
            this.CON_CENTERLABEL.DynamicTableName = false;
            this.CON_CENTERLABEL.EEPAlias = null;
            this.CON_CENTERLABEL.EncodingAfter = null;
            this.CON_CENTERLABEL.EncodingBefore = "Windows-1252";
            this.CON_CENTERLABEL.EncodingConvert = null;
            this.CON_CENTERLABEL.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "LABEL_ID";
            keyItem2.KeyName = "CENTER_ID";
            this.CON_CENTERLABEL.KeyFields.Add(keyItem1);
            this.CON_CENTERLABEL.KeyFields.Add(keyItem2);
            this.CON_CENTERLABEL.MultiSetWhere = false;
            this.CON_CENTERLABEL.Name = "CON_CENTERLABEL";
            this.CON_CENTERLABEL.NotificationAutoEnlist = false;
            this.CON_CENTERLABEL.SecExcept = null;
            this.CON_CENTERLABEL.SecFieldName = null;
            this.CON_CENTERLABEL.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CON_CENTERLABEL.SelectPaging = false;
            this.CON_CENTERLABEL.SelectTop = 0;
            this.CON_CENTERLABEL.SiteControl = false;
            this.CON_CENTERLABEL.SiteFieldName = null;
            this.CON_CENTERLABEL.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCON_CENTERLABEL
            // 
            this.ucCON_CENTERLABEL.AutoTrans = true;
            this.ucCON_CENTERLABEL.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "LABEL_ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CENTER_ID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "LABELNAME";
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
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            this.ucCON_CENTERLABEL.FieldAttrs.Add(fieldAttr1);
            this.ucCON_CENTERLABEL.FieldAttrs.Add(fieldAttr2);
            this.ucCON_CENTERLABEL.FieldAttrs.Add(fieldAttr3);
            this.ucCON_CENTERLABEL.FieldAttrs.Add(fieldAttr4);
            this.ucCON_CENTERLABEL.FieldAttrs.Add(fieldAttr5);
            this.ucCON_CENTERLABEL.LogInfo = null;
            this.ucCON_CENTERLABEL.Name = "ucCON_CENTERLABEL";
            this.ucCON_CENTERLABEL.RowAffectsCheck = true;
            this.ucCON_CENTERLABEL.SelectCmd = this.CON_CENTERLABEL;
            this.ucCON_CENTERLABEL.SelectCmdForUpdate = null;
            this.ucCON_CENTERLABEL.SendSQLCmd = true;
            this.ucCON_CENTERLABEL.ServerModify = true;
            this.ucCON_CENTERLABEL.ServerModifyGetMax = false;
            this.ucCON_CENTERLABEL.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCON_CENTERLABEL.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCON_CENTERLABEL.UseTranscationScope = false;
            this.ucCON_CENTERLABEL.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucCON_CENTERLABEL.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucCON_CENTERLABEL_BeforeInsert);
            // 
            // View_CON_CENTERLABEL
            // 
            this.View_CON_CENTERLABEL.CacheConnection = false;
            this.View_CON_CENTERLABEL.CommandText = "SELECT * FROM dbo.[CON_CENTERLABEL]";
            this.View_CON_CENTERLABEL.CommandTimeout = 30;
            this.View_CON_CENTERLABEL.CommandType = System.Data.CommandType.Text;
            this.View_CON_CENTERLABEL.DynamicTableName = false;
            this.View_CON_CENTERLABEL.EEPAlias = null;
            this.View_CON_CENTERLABEL.EncodingAfter = null;
            this.View_CON_CENTERLABEL.EncodingBefore = "Windows-1252";
            this.View_CON_CENTERLABEL.EncodingConvert = null;
            this.View_CON_CENTERLABEL.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "LABEL_ID";
            this.View_CON_CENTERLABEL.KeyFields.Add(keyItem3);
            this.View_CON_CENTERLABEL.MultiSetWhere = false;
            this.View_CON_CENTERLABEL.Name = "View_CON_CENTERLABEL";
            this.View_CON_CENTERLABEL.NotificationAutoEnlist = false;
            this.View_CON_CENTERLABEL.SecExcept = null;
            this.View_CON_CENTERLABEL.SecFieldName = null;
            this.View_CON_CENTERLABEL.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CON_CENTERLABEL.SelectPaging = false;
            this.View_CON_CENTERLABEL.SelectTop = 0;
            this.View_CON_CENTERLABEL.SiteControl = false;
            this.View_CON_CENTERLABEL.SiteFieldName = null;
            this.View_CON_CENTERLABEL.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // GROUPTYPE
            // 
            this.GROUPTYPE.CacheConnection = false;
            this.GROUPTYPE.CommandText = "SELECT CENTER_ID,CENTER_CNAME\r\n FROM CON_CENTER";
            this.GROUPTYPE.CommandTimeout = 30;
            this.GROUPTYPE.CommandType = System.Data.CommandType.Text;
            this.GROUPTYPE.DynamicTableName = false;
            this.GROUPTYPE.EEPAlias = null;
            this.GROUPTYPE.EncodingAfter = null;
            this.GROUPTYPE.EncodingBefore = "Windows-1252";
            this.GROUPTYPE.EncodingConvert = null;
            this.GROUPTYPE.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "GROUPID";
            this.GROUPTYPE.KeyFields.Add(keyItem4);
            this.GROUPTYPE.MultiSetWhere = false;
            this.GROUPTYPE.Name = "GROUPTYPE";
            this.GROUPTYPE.NotificationAutoEnlist = false;
            this.GROUPTYPE.SecExcept = null;
            this.GROUPTYPE.SecFieldName = null;
            this.GROUPTYPE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.GROUPTYPE.SelectPaging = false;
            this.GROUPTYPE.SelectTop = 0;
            this.GROUPTYPE.SiteControl = false;
            this.GROUPTYPE.SiteFieldName = null;
            this.GROUPTYPE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CON_CENTERLABEL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CON_CENTERLABEL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPTYPE)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CON_CENTERLABEL;
        private Srvtools.UpdateComponent ucCON_CENTERLABEL;
        private Srvtools.InfoCommand View_CON_CENTERLABEL;
        private Srvtools.InfoCommand GROUPTYPE;
    }
}
