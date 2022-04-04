namespace sSYS_Variable
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SYS_Variable = new Srvtools.InfoCommand(this.components);
            this.ucSYS_Variable = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_Variable)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // SYS_Variable
            // 
            this.SYS_Variable.CacheConnection = false;
            this.SYS_Variable.CommandText = "SELECT dbo.[SYS_Variable].* FROM dbo.[SYS_Variable]";
            this.SYS_Variable.CommandTimeout = 30;
            this.SYS_Variable.CommandType = System.Data.CommandType.Text;
            this.SYS_Variable.DynamicTableName = false;
            this.SYS_Variable.EEPAlias = null;
            this.SYS_Variable.EncodingAfter = null;
            this.SYS_Variable.EncodingBefore = "Windows-1252";
            this.SYS_Variable.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "iAutokey";
            this.SYS_Variable.KeyFields.Add(keyItem1);
            this.SYS_Variable.MultiSetWhere = false;
            this.SYS_Variable.Name = "SYS_Variable";
            this.SYS_Variable.NotificationAutoEnlist = false;
            this.SYS_Variable.SecExcept = null;
            this.SYS_Variable.SecFieldName = null;
            this.SYS_Variable.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SYS_Variable.SelectPaging = false;
            this.SYS_Variable.SelectTop = 0;
            this.SYS_Variable.SiteControl = false;
            this.SYS_Variable.SiteFieldName = null;
            this.SYS_Variable.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSYS_Variable
            // 
            this.ucSYS_Variable.AutoTrans = true;
            this.ucSYS_Variable.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "iAutokey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "Title";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "Category";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CategoryValue";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucSYS_Variable.FieldAttrs.Add(fieldAttr1);
            this.ucSYS_Variable.FieldAttrs.Add(fieldAttr2);
            this.ucSYS_Variable.FieldAttrs.Add(fieldAttr3);
            this.ucSYS_Variable.FieldAttrs.Add(fieldAttr4);
            this.ucSYS_Variable.LogInfo = null;
            this.ucSYS_Variable.Name = "ucSYS_Variable";
            this.ucSYS_Variable.RowAffectsCheck = true;
            this.ucSYS_Variable.SelectCmd = this.SYS_Variable;
            this.ucSYS_Variable.SelectCmdForUpdate = null;
            this.ucSYS_Variable.ServerModify = true;
            this.ucSYS_Variable.ServerModifyGetMax = false;
            this.ucSYS_Variable.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSYS_Variable.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSYS_Variable.UseTranscationScope = false;
            this.ucSYS_Variable.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SYS_Variable)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SYS_Variable;
        private Srvtools.UpdateComponent ucSYS_Variable;
    }
}
