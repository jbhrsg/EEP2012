namespace sJobType
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.jb_type = new Srvtools.InfoCommand(this.components);
            this.ucjb_type = new Srvtools.UpdateComponent(this.components);
            this.View_jb_type = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jb_type)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_jb_type)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckJobType";
            service1.NonLogin = false;
            service1.ServiceName = "CheckJobType";
            service2.DelegateName = "procCALLAPI";
            service2.NonLogin = false;
            service2.ServiceName = "procCALLAPI";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBNJB";
            // 
            // jb_type
            // 
            this.jb_type.CacheConnection = false;
            this.jb_type.CommandText = "SELECT * FROM JB_TYPE";
            this.jb_type.CommandTimeout = 30;
            this.jb_type.CommandType = System.Data.CommandType.Text;
            this.jb_type.DynamicTableName = false;
            this.jb_type.EEPAlias = "JBNJB";
            this.jb_type.EncodingAfter = null;
            this.jb_type.EncodingBefore = "Windows-1252";
            this.jb_type.EncodingConvert = null;
            this.jb_type.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "jb_type";
            this.jb_type.KeyFields.Add(keyItem1);
            this.jb_type.MultiSetWhere = false;
            this.jb_type.Name = "jb_type";
            this.jb_type.NotificationAutoEnlist = false;
            this.jb_type.SecExcept = null;
            this.jb_type.SecFieldName = null;
            this.jb_type.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.jb_type.SelectPaging = false;
            this.jb_type.SelectTop = 0;
            this.jb_type.SiteControl = false;
            this.jb_type.SiteFieldName = null;
            this.jb_type.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucjb_type
            // 
            this.ucjb_type.AutoTrans = true;
            this.ucjb_type.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "jb_type";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "jb_name";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "key_man";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "key_date";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "O_NO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            this.ucjb_type.FieldAttrs.Add(fieldAttr1);
            this.ucjb_type.FieldAttrs.Add(fieldAttr2);
            this.ucjb_type.FieldAttrs.Add(fieldAttr3);
            this.ucjb_type.FieldAttrs.Add(fieldAttr4);
            this.ucjb_type.FieldAttrs.Add(fieldAttr5);
            this.ucjb_type.LogInfo = null;
            this.ucjb_type.Name = "ucjb_type";
            this.ucjb_type.RowAffectsCheck = true;
            this.ucjb_type.SelectCmd = this.jb_type;
            this.ucjb_type.SelectCmdForUpdate = null;
            this.ucjb_type.SendSQLCmd = true;
            this.ucjb_type.ServerModify = true;
            this.ucjb_type.ServerModifyGetMax = false;
            this.ucjb_type.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucjb_type.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucjb_type.UseTranscationScope = false;
            this.ucjb_type.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_jb_type
            // 
            this.View_jb_type.CacheConnection = false;
            this.View_jb_type.CommandText = "SELECT * FROM dbo.[jb_type]";
            this.View_jb_type.CommandTimeout = 30;
            this.View_jb_type.CommandType = System.Data.CommandType.Text;
            this.View_jb_type.DynamicTableName = false;
            this.View_jb_type.EEPAlias = "JBNJB";
            this.View_jb_type.EncodingAfter = null;
            this.View_jb_type.EncodingBefore = "Windows-1252";
            this.View_jb_type.EncodingConvert = null;
            this.View_jb_type.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "jb_type";
            this.View_jb_type.KeyFields.Add(keyItem2);
            this.View_jb_type.MultiSetWhere = false;
            this.View_jb_type.Name = "View_jb_type";
            this.View_jb_type.NotificationAutoEnlist = false;
            this.View_jb_type.SecExcept = null;
            this.View_jb_type.SecFieldName = null;
            this.View_jb_type.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_jb_type.SelectPaging = false;
            this.View_jb_type.SelectTop = 0;
            this.View_jb_type.SiteControl = false;
            this.View_jb_type.SiteFieldName = null;
            this.View_jb_type.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jb_type)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_jb_type)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand jb_type;
        private Srvtools.UpdateComponent ucjb_type;
        private Srvtools.InfoCommand View_jb_type;
    }
}
