namespace sSalesKind_ImportXlsx
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SalesKind = new Srvtools.InfoCommand(this.components);
            this.ucSalesKind = new Srvtools.UpdateComponent(this.components);
            this.View_SalesKind = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesKind)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // SalesKind
            // 
            this.SalesKind.CacheConnection = false;
            this.SalesKind.CommandText = "SELECT dbo.[SalesKind].* FROM dbo.[SalesKind]";
            this.SalesKind.CommandTimeout = 30;
            this.SalesKind.CommandType = System.Data.CommandType.Text;
            this.SalesKind.DynamicTableName = false;
            this.SalesKind.EEPAlias = "JBERP";
            this.SalesKind.EncodingAfter = null;
            this.SalesKind.EncodingBefore = "Windows-1252";
            this.SalesKind.EncodingConvert = null;
            this.SalesKind.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.SalesKind.KeyFields.Add(keyItem1);
            this.SalesKind.MultiSetWhere = false;
            this.SalesKind.Name = "SalesKind";
            this.SalesKind.NotificationAutoEnlist = false;
            this.SalesKind.SecExcept = null;
            this.SalesKind.SecFieldName = null;
            this.SalesKind.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesKind.SelectPaging = false;
            this.SalesKind.SelectTop = 0;
            this.SalesKind.SiteControl = false;
            this.SalesKind.SiteFieldName = null;
            this.SalesKind.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesKind
            // 
            this.ucSalesKind.AutoTrans = true;
            this.ucSalesKind.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SalesKindID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesKindName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateBy";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LastUpdateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucSalesKind.FieldAttrs.Add(fieldAttr1);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr2);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr3);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr4);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr5);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr6);
            this.ucSalesKind.FieldAttrs.Add(fieldAttr7);
            this.ucSalesKind.LogInfo = null;
            this.ucSalesKind.Name = "ucSalesKind";
            this.ucSalesKind.RowAffectsCheck = true;
            this.ucSalesKind.SelectCmd = this.SalesKind;
            this.ucSalesKind.SelectCmdForUpdate = null;
            this.ucSalesKind.SendSQLCmd = true;
            this.ucSalesKind.ServerModify = true;
            this.ucSalesKind.ServerModifyGetMax = false;
            this.ucSalesKind.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesKind.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesKind.UseTranscationScope = false;
            this.ucSalesKind.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_SalesKind
            // 
            this.View_SalesKind.CacheConnection = false;
            this.View_SalesKind.CommandText = "SELECT * FROM dbo.[SalesKind]";
            this.View_SalesKind.CommandTimeout = 30;
            this.View_SalesKind.CommandType = System.Data.CommandType.Text;
            this.View_SalesKind.DynamicTableName = false;
            this.View_SalesKind.EEPAlias = "JBERP";
            this.View_SalesKind.EncodingAfter = null;
            this.View_SalesKind.EncodingBefore = "Windows-1252";
            this.View_SalesKind.EncodingConvert = null;
            this.View_SalesKind.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.View_SalesKind.KeyFields.Add(keyItem2);
            this.View_SalesKind.MultiSetWhere = false;
            this.View_SalesKind.Name = "View_SalesKind";
            this.View_SalesKind.NotificationAutoEnlist = false;
            this.View_SalesKind.SecExcept = null;
            this.View_SalesKind.SecFieldName = null;
            this.View_SalesKind.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesKind.SelectPaging = false;
            this.View_SalesKind.SelectTop = 0;
            this.View_SalesKind.SiteControl = false;
            this.View_SalesKind.SiteFieldName = null;
            this.View_SalesKind.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesKind)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesKind;
        private Srvtools.UpdateComponent ucSalesKind;
        private Srvtools.InfoCommand View_SalesKind;
    }
}
