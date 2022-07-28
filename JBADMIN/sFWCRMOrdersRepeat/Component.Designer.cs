namespace sFWCRMOrdersRepeat
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
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
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMOrders = new Srvtools.InfoCommand(this.components);
            this.FWCRMOrdersDetails = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMOrdersDetails = new Srvtools.UpdateComponent(this.components);
            this.idFWCRMOrders_FWCRMOrdersDetails = new Srvtools.InfoDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrdersDetails)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMOrders
            // 
            this.FWCRMOrders.CacheConnection = false;
            this.FWCRMOrders.CommandText = "SELECT dbo.[FWCRMOrders].* FROM dbo.[FWCRMOrders]";
            this.FWCRMOrders.CommandTimeout = 30;
            this.FWCRMOrders.CommandType = System.Data.CommandType.Text;
            this.FWCRMOrders.DynamicTableName = false;
            this.FWCRMOrders.EEPAlias = null;
            this.FWCRMOrders.EncodingAfter = null;
            this.FWCRMOrders.EncodingBefore = "Windows-1252";
            this.FWCRMOrders.EncodingConvert = null;
            this.FWCRMOrders.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "OrderNo";
            this.FWCRMOrders.KeyFields.Add(keyItem1);
            this.FWCRMOrders.MultiSetWhere = false;
            this.FWCRMOrders.Name = "FWCRMOrders";
            this.FWCRMOrders.NotificationAutoEnlist = false;
            this.FWCRMOrders.SecExcept = null;
            this.FWCRMOrders.SecFieldName = null;
            this.FWCRMOrders.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMOrders.SelectPaging = false;
            this.FWCRMOrders.SelectTop = 0;
            this.FWCRMOrders.SiteControl = false;
            this.FWCRMOrders.SiteFieldName = null;
            this.FWCRMOrders.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // FWCRMOrdersDetails
            // 
            this.FWCRMOrdersDetails.CacheConnection = false;
            this.FWCRMOrdersDetails.CommandText = "SELECT dbo.[FWCRMOrdersDetails].* FROM dbo.[FWCRMOrdersDetails]";
            this.FWCRMOrdersDetails.CommandTimeout = 30;
            this.FWCRMOrdersDetails.CommandType = System.Data.CommandType.Text;
            this.FWCRMOrdersDetails.DynamicTableName = false;
            this.FWCRMOrdersDetails.EEPAlias = null;
            this.FWCRMOrdersDetails.EncodingAfter = null;
            this.FWCRMOrdersDetails.EncodingBefore = "Windows-1252";
            this.FWCRMOrdersDetails.EncodingConvert = null;
            this.FWCRMOrdersDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "OrderNo";
            keyItem3.KeyName = "Item";
            this.FWCRMOrdersDetails.KeyFields.Add(keyItem2);
            this.FWCRMOrdersDetails.KeyFields.Add(keyItem3);
            this.FWCRMOrdersDetails.MultiSetWhere = false;
            this.FWCRMOrdersDetails.Name = "FWCRMOrdersDetails";
            this.FWCRMOrdersDetails.NotificationAutoEnlist = false;
            this.FWCRMOrdersDetails.SecExcept = null;
            this.FWCRMOrdersDetails.SecFieldName = null;
            this.FWCRMOrdersDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMOrdersDetails.SelectPaging = false;
            this.FWCRMOrdersDetails.SelectTop = 0;
            this.FWCRMOrdersDetails.SiteControl = false;
            this.FWCRMOrdersDetails.SiteFieldName = null;
            this.FWCRMOrdersDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMOrdersDetails
            // 
            this.ucFWCRMOrdersDetails.AutoTrans = true;
            this.ucFWCRMOrdersDetails.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "OrderNo";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "Item";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "PlanIndate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "PersonQtyOriginal";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "PersonQtyFinal";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Gender";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "org_okno";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Notes";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateBy";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr6);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr7);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr8);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr9);
            this.ucFWCRMOrdersDetails.FieldAttrs.Add(fieldAttr10);
            this.ucFWCRMOrdersDetails.LogInfo = null;
            this.ucFWCRMOrdersDetails.Name = "ucFWCRMOrdersDetails";
            this.ucFWCRMOrdersDetails.RowAffectsCheck = true;
            this.ucFWCRMOrdersDetails.SelectCmd = this.FWCRMOrdersDetails;
            this.ucFWCRMOrdersDetails.SelectCmdForUpdate = null;
            this.ucFWCRMOrdersDetails.SendSQLCmd = true;
            this.ucFWCRMOrdersDetails.ServerModify = true;
            this.ucFWCRMOrdersDetails.ServerModifyGetMax = false;
            this.ucFWCRMOrdersDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMOrdersDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMOrdersDetails.UseTranscationScope = false;
            this.ucFWCRMOrdersDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idFWCRMOrders_FWCRMOrdersDetails
            // 
            this.idFWCRMOrders_FWCRMOrdersDetails.Detail = this.FWCRMOrdersDetails;
            columnItem1.FieldName = "OrderNo";
            this.idFWCRMOrders_FWCRMOrdersDetails.DetailColumns.Add(columnItem1);
            this.idFWCRMOrders_FWCRMOrdersDetails.DynamicTableName = false;
            this.idFWCRMOrders_FWCRMOrdersDetails.Master = this.FWCRMOrders;
            columnItem2.FieldName = "OrderNo";
            this.idFWCRMOrders_FWCRMOrdersDetails.MasterColumns.Add(columnItem2);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMOrdersDetails)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMOrders;
        private Srvtools.InfoCommand FWCRMOrdersDetails;
        private Srvtools.UpdateComponent ucFWCRMOrdersDetails;
        private Srvtools.InfoDataSource idFWCRMOrders_FWCRMOrdersDetails;
    }
}
