namespace sFWCRMSetStatus
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMSetStatus = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMSetStatus = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMSetStatus)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMSetStatus
            // 
            this.FWCRMSetStatus.CacheConnection = false;
            this.FWCRMSetStatus.CommandText = "SELECT dbo.[FWCRMSetStatus].* FROM dbo.[FWCRMSetStatus]\r\norder by iAutoKey";
            this.FWCRMSetStatus.CommandTimeout = 30;
            this.FWCRMSetStatus.CommandType = System.Data.CommandType.Text;
            this.FWCRMSetStatus.DynamicTableName = false;
            this.FWCRMSetStatus.EEPAlias = null;
            this.FWCRMSetStatus.EncodingAfter = null;
            this.FWCRMSetStatus.EncodingBefore = "Windows-1252";
            this.FWCRMSetStatus.EncodingConvert = null;
            this.FWCRMSetStatus.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "iAutoKey";
            this.FWCRMSetStatus.KeyFields.Add(keyItem1);
            this.FWCRMSetStatus.MultiSetWhere = false;
            this.FWCRMSetStatus.Name = "FWCRMSetStatus";
            this.FWCRMSetStatus.NotificationAutoEnlist = false;
            this.FWCRMSetStatus.SecExcept = null;
            this.FWCRMSetStatus.SecFieldName = null;
            this.FWCRMSetStatus.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMSetStatus.SelectPaging = false;
            this.FWCRMSetStatus.SelectTop = 0;
            this.FWCRMSetStatus.SiteControl = false;
            this.FWCRMSetStatus.SiteFieldName = null;
            this.FWCRMSetStatus.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMSetStatus
            // 
            this.ucFWCRMSetStatus.AutoTrans = true;
            this.ucFWCRMSetStatus.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "iAutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "StatusName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateBy";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "iSort";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            this.ucFWCRMSetStatus.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMSetStatus.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMSetStatus.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMSetStatus.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMSetStatus.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMSetStatus.LogInfo = null;
            this.ucFWCRMSetStatus.Name = "ucFWCRMSetStatus";
            this.ucFWCRMSetStatus.RowAffectsCheck = true;
            this.ucFWCRMSetStatus.SelectCmd = this.FWCRMSetStatus;
            this.ucFWCRMSetStatus.SelectCmdForUpdate = null;
            this.ucFWCRMSetStatus.SendSQLCmd = true;
            this.ucFWCRMSetStatus.ServerModify = true;
            this.ucFWCRMSetStatus.ServerModifyGetMax = false;
            this.ucFWCRMSetStatus.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMSetStatus.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMSetStatus.UseTranscationScope = false;
            this.ucFWCRMSetStatus.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMSetStatus)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMSetStatus;
        private Srvtools.UpdateComponent ucFWCRMSetStatus;
    }
}
