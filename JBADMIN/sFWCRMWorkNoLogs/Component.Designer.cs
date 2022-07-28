namespace sFWCRMWorkNoLogs
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMWorkNoLogs = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMWorkNoLogs = new Srvtools.UpdateComponent(this.components);
            this.infoOrderNo = new Srvtools.InfoCommand(this.components);
            this.autoItem = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMWorkNoLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoOrderNo)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMWorkNoLogs
            // 
            this.FWCRMWorkNoLogs.CacheConnection = false;
            this.FWCRMWorkNoLogs.CommandText = "SELECT dbo.[FWCRMWorkNoLogs].*,WorkImg as DownloadWorkImg FROM dbo.[FWCRMWorkNoLo" +
    "gs]";
            this.FWCRMWorkNoLogs.CommandTimeout = 30;
            this.FWCRMWorkNoLogs.CommandType = System.Data.CommandType.Text;
            this.FWCRMWorkNoLogs.DynamicTableName = false;
            this.FWCRMWorkNoLogs.EEPAlias = null;
            this.FWCRMWorkNoLogs.EncodingAfter = null;
            this.FWCRMWorkNoLogs.EncodingBefore = "Windows-1252";
            this.FWCRMWorkNoLogs.EncodingConvert = null;
            this.FWCRMWorkNoLogs.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "Item";
            this.FWCRMWorkNoLogs.KeyFields.Add(keyItem1);
            this.FWCRMWorkNoLogs.MultiSetWhere = false;
            this.FWCRMWorkNoLogs.Name = "FWCRMWorkNoLogs";
            this.FWCRMWorkNoLogs.NotificationAutoEnlist = false;
            this.FWCRMWorkNoLogs.SecExcept = null;
            this.FWCRMWorkNoLogs.SecFieldName = null;
            this.FWCRMWorkNoLogs.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMWorkNoLogs.SelectPaging = false;
            this.FWCRMWorkNoLogs.SelectTop = 0;
            this.FWCRMWorkNoLogs.SiteControl = false;
            this.FWCRMWorkNoLogs.SiteFieldName = null;
            this.FWCRMWorkNoLogs.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMWorkNoLogs
            // 
            this.ucFWCRMWorkNoLogs.AutoTrans = true;
            this.ucFWCRMWorkNoLogs.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "Item";
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
            fieldAttr3.DataField = "WorkNo";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Memo";
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
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "UserID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucFWCRMWorkNoLogs.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMWorkNoLogs.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMWorkNoLogs.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMWorkNoLogs.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMWorkNoLogs.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMWorkNoLogs.FieldAttrs.Add(fieldAttr6);
            this.ucFWCRMWorkNoLogs.FieldAttrs.Add(fieldAttr7);
            this.ucFWCRMWorkNoLogs.FieldAttrs.Add(fieldAttr8);
            this.ucFWCRMWorkNoLogs.LogInfo = null;
            this.ucFWCRMWorkNoLogs.Name = "ucFWCRMWorkNoLogs";
            this.ucFWCRMWorkNoLogs.RowAffectsCheck = true;
            this.ucFWCRMWorkNoLogs.SelectCmd = this.FWCRMWorkNoLogs;
            this.ucFWCRMWorkNoLogs.SelectCmdForUpdate = null;
            this.ucFWCRMWorkNoLogs.SendSQLCmd = true;
            this.ucFWCRMWorkNoLogs.ServerModify = true;
            this.ucFWCRMWorkNoLogs.ServerModifyGetMax = false;
            this.ucFWCRMWorkNoLogs.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMWorkNoLogs.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMWorkNoLogs.UseTranscationScope = false;
            this.ucFWCRMWorkNoLogs.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoOrderNo
            // 
            this.infoOrderNo.CacheConnection = false;
            this.infoOrderNo.CommandText = "select distinct f.OrderNo,r.EmployerName,f.WorkNo\r\nfrom FWCRMOrders f\t\r\n\tinner jo" +
    "in dbo.View_FWCRMEmployer r on f.EmployerID=r.EmployerID";
            this.infoOrderNo.CommandTimeout = 30;
            this.infoOrderNo.CommandType = System.Data.CommandType.Text;
            this.infoOrderNo.DynamicTableName = false;
            this.infoOrderNo.EEPAlias = null;
            this.infoOrderNo.EncodingAfter = null;
            this.infoOrderNo.EncodingBefore = "Windows-1252";
            this.infoOrderNo.EncodingConvert = null;
            this.infoOrderNo.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "OrderNo";
            this.infoOrderNo.KeyFields.Add(keyItem2);
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
            // autoItem
            // 
            this.autoItem.Active = true;
            this.autoItem.AutoNoID = "FWCRMWorkNoLogsItem";
            this.autoItem.Description = null;
            this.autoItem.GetFixed = "";
            this.autoItem.isNumFill = false;
            this.autoItem.Name = "autoItem";
            this.autoItem.Number = null;
            this.autoItem.NumDig = 4;
            this.autoItem.OldVersion = false;
            this.autoItem.OverFlow = true;
            this.autoItem.StartValue = 1;
            this.autoItem.Step = 1;
            this.autoItem.TargetColumn = "Item";
            this.autoItem.UpdateComp = this.ucFWCRMWorkNoLogs;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMWorkNoLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoOrderNo)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMWorkNoLogs;
        private Srvtools.UpdateComponent ucFWCRMWorkNoLogs;
        private Srvtools.InfoCommand infoOrderNo;
        private Srvtools.AutoNumber autoItem;
    }
}
