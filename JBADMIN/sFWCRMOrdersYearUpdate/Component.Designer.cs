namespace sFWCRMOrdersYearUpdate
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMOrdersYearUpdate = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMOrdersYearUpdate = new Srvtools.UpdateComponent(this.components);
            this.View_FWCRMOrdersYearUpdate = new Srvtools.InfoCommand(this.components);
            this.infoOrderNo = new Srvtools.InfoCommand(this.components);
            this.autoAutoKey = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrdersYearUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_FWCRMOrdersYearUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoOrderNo)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "procUpdateOrderYear";
            service1.NonLogin = false;
            service1.ServiceName = "procUpdateOrderYear";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMOrdersYearUpdate
            // 
            this.FWCRMOrdersYearUpdate.CacheConnection = false;
            this.FWCRMOrdersYearUpdate.CommandText = "SELECT dbo.[FWCRMOrdersYearUpdate].* FROM dbo.[FWCRMOrdersYearUpdate]";
            this.FWCRMOrdersYearUpdate.CommandTimeout = 30;
            this.FWCRMOrdersYearUpdate.CommandType = System.Data.CommandType.Text;
            this.FWCRMOrdersYearUpdate.DynamicTableName = false;
            this.FWCRMOrdersYearUpdate.EEPAlias = null;
            this.FWCRMOrdersYearUpdate.EncodingAfter = null;
            this.FWCRMOrdersYearUpdate.EncodingBefore = "Windows-1252";
            this.FWCRMOrdersYearUpdate.EncodingConvert = null;
            this.FWCRMOrdersYearUpdate.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "iAutokey";
            this.FWCRMOrdersYearUpdate.KeyFields.Add(keyItem1);
            this.FWCRMOrdersYearUpdate.MultiSetWhere = false;
            this.FWCRMOrdersYearUpdate.Name = "FWCRMOrdersYearUpdate";
            this.FWCRMOrdersYearUpdate.NotificationAutoEnlist = false;
            this.FWCRMOrdersYearUpdate.SecExcept = null;
            this.FWCRMOrdersYearUpdate.SecFieldName = null;
            this.FWCRMOrdersYearUpdate.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMOrdersYearUpdate.SelectPaging = false;
            this.FWCRMOrdersYearUpdate.SelectTop = 0;
            this.FWCRMOrdersYearUpdate.SiteControl = false;
            this.FWCRMOrdersYearUpdate.SiteFieldName = null;
            this.FWCRMOrdersYearUpdate.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMOrdersYearUpdate
            // 
            this.ucFWCRMOrdersYearUpdate.AutoTrans = true;
            this.ucFWCRMOrdersYearUpdate.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "iAutokey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "OrderNo";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "OrderYear";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "OrderYearNew";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "flowflag";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucFWCRMOrdersYearUpdate.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMOrdersYearUpdate.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMOrdersYearUpdate.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMOrdersYearUpdate.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMOrdersYearUpdate.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMOrdersYearUpdate.FieldAttrs.Add(fieldAttr6);
            this.ucFWCRMOrdersYearUpdate.FieldAttrs.Add(fieldAttr7);
            this.ucFWCRMOrdersYearUpdate.LogInfo = null;
            this.ucFWCRMOrdersYearUpdate.Name = "ucFWCRMOrdersYearUpdate";
            this.ucFWCRMOrdersYearUpdate.RowAffectsCheck = true;
            this.ucFWCRMOrdersYearUpdate.SelectCmd = this.FWCRMOrdersYearUpdate;
            this.ucFWCRMOrdersYearUpdate.SelectCmdForUpdate = null;
            this.ucFWCRMOrdersYearUpdate.SendSQLCmd = true;
            this.ucFWCRMOrdersYearUpdate.ServerModify = true;
            this.ucFWCRMOrdersYearUpdate.ServerModifyGetMax = false;
            this.ucFWCRMOrdersYearUpdate.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMOrdersYearUpdate.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMOrdersYearUpdate.UseTranscationScope = false;
            this.ucFWCRMOrdersYearUpdate.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_FWCRMOrdersYearUpdate
            // 
            this.View_FWCRMOrdersYearUpdate.CacheConnection = false;
            this.View_FWCRMOrdersYearUpdate.CommandText = "SELECT * FROM dbo.[FWCRMOrdersYearUpdate]";
            this.View_FWCRMOrdersYearUpdate.CommandTimeout = 30;
            this.View_FWCRMOrdersYearUpdate.CommandType = System.Data.CommandType.Text;
            this.View_FWCRMOrdersYearUpdate.DynamicTableName = false;
            this.View_FWCRMOrdersYearUpdate.EEPAlias = null;
            this.View_FWCRMOrdersYearUpdate.EncodingAfter = null;
            this.View_FWCRMOrdersYearUpdate.EncodingBefore = "Windows-1252";
            this.View_FWCRMOrdersYearUpdate.EncodingConvert = null;
            this.View_FWCRMOrdersYearUpdate.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "iAutokey";
            this.View_FWCRMOrdersYearUpdate.KeyFields.Add(keyItem2);
            this.View_FWCRMOrdersYearUpdate.MultiSetWhere = false;
            this.View_FWCRMOrdersYearUpdate.Name = "View_FWCRMOrdersYearUpdate";
            this.View_FWCRMOrdersYearUpdate.NotificationAutoEnlist = false;
            this.View_FWCRMOrdersYearUpdate.SecExcept = null;
            this.View_FWCRMOrdersYearUpdate.SecFieldName = null;
            this.View_FWCRMOrdersYearUpdate.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_FWCRMOrdersYearUpdate.SelectPaging = false;
            this.View_FWCRMOrdersYearUpdate.SelectTop = 0;
            this.View_FWCRMOrdersYearUpdate.SiteControl = false;
            this.View_FWCRMOrdersYearUpdate.SiteFieldName = null;
            this.View_FWCRMOrdersYearUpdate.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoOrderNo
            // 
            this.infoOrderNo.CacheConnection = false;
            this.infoOrderNo.CommandText = resources.GetString("infoOrderNo.CommandText");
            this.infoOrderNo.CommandTimeout = 30;
            this.infoOrderNo.CommandType = System.Data.CommandType.Text;
            this.infoOrderNo.DynamicTableName = false;
            this.infoOrderNo.EEPAlias = null;
            this.infoOrderNo.EncodingAfter = null;
            this.infoOrderNo.EncodingBefore = "Windows-1252";
            this.infoOrderNo.EncodingConvert = null;
            this.infoOrderNo.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "OrderNo";
            this.infoOrderNo.KeyFields.Add(keyItem3);
            this.infoOrderNo.MultiSetWhere = false;
            this.infoOrderNo.Name = "infoOrderNo";
            this.infoOrderNo.NotificationAutoEnlist = false;
            this.infoOrderNo.SecExcept = null;
            this.infoOrderNo.SecFieldName = null;
            this.infoOrderNo.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoOrderNo.SelectPaging = false;
            this.infoOrderNo.SelectTop = 0;
            this.infoOrderNo.SiteControl = false;
            this.infoOrderNo.SiteFieldName = null;
            this.infoOrderNo.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoAutoKey
            // 
            this.autoAutoKey.Active = true;
            this.autoAutoKey.AutoNoID = "OrdersYeariAutokey";
            this.autoAutoKey.Description = null;
            this.autoAutoKey.GetFixed = "";
            this.autoAutoKey.isNumFill = false;
            this.autoAutoKey.Name = "autoAutoKey";
            this.autoAutoKey.Number = null;
            this.autoAutoKey.NumDig = 8;
            this.autoAutoKey.OldVersion = false;
            this.autoAutoKey.OverFlow = true;
            this.autoAutoKey.StartValue = 1;
            this.autoAutoKey.Step = 1;
            this.autoAutoKey.TargetColumn = "iAutokey";
            this.autoAutoKey.UpdateComp = this.ucFWCRMOrdersYearUpdate;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrdersYearUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_FWCRMOrdersYearUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoOrderNo)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMOrdersYearUpdate;
        private Srvtools.UpdateComponent ucFWCRMOrdersYearUpdate;
        private Srvtools.InfoCommand View_FWCRMOrdersYearUpdate;
        private Srvtools.InfoCommand infoOrderNo;
        private Srvtools.AutoNumber autoAutoKey;
    }
}
