namespace sERPPreOrderM
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPPreOrderM = new Srvtools.InfoCommand(this.components);
            this.ucERPPreOrderM = new Srvtools.UpdateComponent(this.components);
            this.ERPPreOrderD = new Srvtools.InfoCommand(this.components);
            this.ucERPPreOrderD = new Srvtools.UpdateComponent(this.components);
            this.idERPPreOrderM_ERPPreOrderD = new Srvtools.InfoDataSource(this.components);
            this.View_ERPPreOrderM = new Srvtools.InfoCommand(this.components);
            this.Customers = new Srvtools.InfoCommand(this.components);
            this.Org = new Srvtools.InfoCommand(this.components);
            this.OrderFlow = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPPreOrderM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPPreOrderD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPPreOrderM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Org)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderFlow)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPPreOrderM
            // 
            this.ERPPreOrderM.CacheConnection = false;
            this.ERPPreOrderM.CommandText = "SELECT dbo.[ERPPreOrderM].* ,\r\n\'測試\' AS NowStatus\r\nFROM dbo.[ERPPreOrderM]";
            this.ERPPreOrderM.CommandTimeout = 30;
            this.ERPPreOrderM.CommandType = System.Data.CommandType.Text;
            this.ERPPreOrderM.DynamicTableName = false;
            this.ERPPreOrderM.EEPAlias = null;
            this.ERPPreOrderM.EncodingAfter = null;
            this.ERPPreOrderM.EncodingBefore = "Windows-1252";
            this.ERPPreOrderM.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "PreOrderNO";
            this.ERPPreOrderM.KeyFields.Add(keyItem1);
            this.ERPPreOrderM.MultiSetWhere = false;
            this.ERPPreOrderM.Name = "ERPPreOrderM";
            this.ERPPreOrderM.NotificationAutoEnlist = false;
            this.ERPPreOrderM.SecExcept = null;
            this.ERPPreOrderM.SecFieldName = null;
            this.ERPPreOrderM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPPreOrderM.SelectPaging = false;
            this.ERPPreOrderM.SelectTop = 0;
            this.ERPPreOrderM.SiteControl = false;
            this.ERPPreOrderM.SiteFieldName = null;
            this.ERPPreOrderM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPPreOrderM
            // 
            this.ucERPPreOrderM.AutoTrans = true;
            this.ucERPPreOrderM.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "PreOrderNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "PreOrderDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "PreOrderDescr";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "OrderFlowNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "UnitPrice";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "OrderQty";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "OrderAmt";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "PreSalesDate";
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
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr1);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr2);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr3);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr4);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr5);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr6);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr7);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr8);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr9);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr10);
            this.ucERPPreOrderM.FieldAttrs.Add(fieldAttr11);
            this.ucERPPreOrderM.LogInfo = null;
            this.ucERPPreOrderM.Name = "ucERPPreOrderM";
            this.ucERPPreOrderM.RowAffectsCheck = true;
            this.ucERPPreOrderM.SelectCmd = this.ERPPreOrderM;
            this.ucERPPreOrderM.SelectCmdForUpdate = null;
            this.ucERPPreOrderM.ServerModify = true;
            this.ucERPPreOrderM.ServerModifyGetMax = false;
            this.ucERPPreOrderM.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPPreOrderM.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPPreOrderM.UseTranscationScope = false;
            this.ucERPPreOrderM.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ERPPreOrderD
            // 
            this.ERPPreOrderD.CacheConnection = false;
            this.ERPPreOrderD.CommandText = "SELECT dbo.[ERPPreOrderD].* ,\r\n(SELECT SEQNAME FROM ERPORDERFLOWD WHERE ORDERFLOW" +
    "NO=dbo.[ERPPreOrderD].ORDERFLOWNO AND SEQNO=dbo.[ERPPreOrderD].SEQNO) AS SEQNAME" +
    "\r\nFROM dbo.[ERPPreOrderD]";
            this.ERPPreOrderD.CommandTimeout = 30;
            this.ERPPreOrderD.CommandType = System.Data.CommandType.Text;
            this.ERPPreOrderD.DynamicTableName = false;
            this.ERPPreOrderD.EEPAlias = null;
            this.ERPPreOrderD.EncodingAfter = null;
            this.ERPPreOrderD.EncodingBefore = "Windows-1252";
            this.ERPPreOrderD.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "PreOrderNO";
            keyItem3.KeyName = "OrderFlowNO";
            keyItem4.KeyName = "SeqNO";
            this.ERPPreOrderD.KeyFields.Add(keyItem2);
            this.ERPPreOrderD.KeyFields.Add(keyItem3);
            this.ERPPreOrderD.KeyFields.Add(keyItem4);
            this.ERPPreOrderD.MultiSetWhere = false;
            this.ERPPreOrderD.Name = "ERPPreOrderD";
            this.ERPPreOrderD.NotificationAutoEnlist = false;
            this.ERPPreOrderD.SecExcept = null;
            this.ERPPreOrderD.SecFieldName = null;
            this.ERPPreOrderD.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPPreOrderD.SelectPaging = false;
            this.ERPPreOrderD.SelectTop = 0;
            this.ERPPreOrderD.SiteControl = false;
            this.ERPPreOrderD.SiteFieldName = null;
            this.ERPPreOrderD.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPPreOrderD
            // 
            this.ucERPPreOrderD.AutoTrans = true;
            this.ucERPPreOrderD.ExceptJoin = false;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "PreOrderNO";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "OrderFlowNO";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "SeqNO";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "SalesRate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "SalesAmt";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "SeqFinishDate";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucERPPreOrderD.FieldAttrs.Add(fieldAttr12);
            this.ucERPPreOrderD.FieldAttrs.Add(fieldAttr13);
            this.ucERPPreOrderD.FieldAttrs.Add(fieldAttr14);
            this.ucERPPreOrderD.FieldAttrs.Add(fieldAttr15);
            this.ucERPPreOrderD.FieldAttrs.Add(fieldAttr16);
            this.ucERPPreOrderD.FieldAttrs.Add(fieldAttr17);
            this.ucERPPreOrderD.LogInfo = null;
            this.ucERPPreOrderD.Name = "ucERPPreOrderD";
            this.ucERPPreOrderD.RowAffectsCheck = true;
            this.ucERPPreOrderD.SelectCmd = this.ERPPreOrderD;
            this.ucERPPreOrderD.SelectCmdForUpdate = null;
            this.ucERPPreOrderD.ServerModify = true;
            this.ucERPPreOrderD.ServerModifyGetMax = false;
            this.ucERPPreOrderD.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPPreOrderD.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPPreOrderD.UseTranscationScope = false;
            this.ucERPPreOrderD.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idERPPreOrderM_ERPPreOrderD
            // 
            this.idERPPreOrderM_ERPPreOrderD.Detail = this.ERPPreOrderD;
            columnItem1.FieldName = "PreOrderNO";
            this.idERPPreOrderM_ERPPreOrderD.DetailColumns.Add(columnItem1);
            this.idERPPreOrderM_ERPPreOrderD.DynamicTableName = false;
            this.idERPPreOrderM_ERPPreOrderD.Master = this.ERPPreOrderM;
            columnItem2.FieldName = "PreOrderNO";
            this.idERPPreOrderM_ERPPreOrderD.MasterColumns.Add(columnItem2);
            // 
            // View_ERPPreOrderM
            // 
            this.View_ERPPreOrderM.CacheConnection = false;
            this.View_ERPPreOrderM.CommandText = "SELECT * FROM dbo.[ERPPreOrderM]";
            this.View_ERPPreOrderM.CommandTimeout = 30;
            this.View_ERPPreOrderM.CommandType = System.Data.CommandType.Text;
            this.View_ERPPreOrderM.DynamicTableName = false;
            this.View_ERPPreOrderM.EEPAlias = null;
            this.View_ERPPreOrderM.EncodingAfter = null;
            this.View_ERPPreOrderM.EncodingBefore = "Windows-1252";
            this.View_ERPPreOrderM.InfoConnection = this.InfoConnection1;
            this.View_ERPPreOrderM.MultiSetWhere = false;
            this.View_ERPPreOrderM.Name = "View_ERPPreOrderM";
            this.View_ERPPreOrderM.NotificationAutoEnlist = false;
            this.View_ERPPreOrderM.SecExcept = null;
            this.View_ERPPreOrderM.SecFieldName = null;
            this.View_ERPPreOrderM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPPreOrderM.SelectPaging = false;
            this.View_ERPPreOrderM.SelectTop = 0;
            this.View_ERPPreOrderM.SiteControl = false;
            this.View_ERPPreOrderM.SiteFieldName = null;
            this.View_ERPPreOrderM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Customers
            // 
            this.Customers.CacheConnection = false;
            this.Customers.CommandText = "select \r\nERPCustomers.CustNO,ERPCustomers.CustShortName from ERPCustomers\r\nWHERE " +
    "SUBSTRING(ERPCustomers.CustNO,1,3)=\'323\'";
            this.Customers.CommandTimeout = 30;
            this.Customers.CommandType = System.Data.CommandType.Text;
            this.Customers.DynamicTableName = false;
            this.Customers.EEPAlias = null;
            this.Customers.EncodingAfter = null;
            this.Customers.EncodingBefore = "Windows-1252";
            this.Customers.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "CustNO";
            this.Customers.KeyFields.Add(keyItem5);
            this.Customers.MultiSetWhere = false;
            this.Customers.Name = "Customers";
            this.Customers.NotificationAutoEnlist = false;
            this.Customers.SecExcept = null;
            this.Customers.SecFieldName = null;
            this.Customers.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Customers.SelectPaging = false;
            this.Customers.SelectTop = 0;
            this.Customers.SiteControl = false;
            this.Customers.SiteFieldName = null;
            this.Customers.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Org
            // 
            this.Org.CacheConnection = false;
            this.Org.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nWHERE (Uppe" +
    "r_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR ORG_NO=\'99999\')\r\nORDER" +
    " BY ORG_NO";
            this.Org.CommandTimeout = 30;
            this.Org.CommandType = System.Data.CommandType.Text;
            this.Org.DynamicTableName = false;
            this.Org.EEPAlias = null;
            this.Org.EncodingAfter = null;
            this.Org.EncodingBefore = "Windows-1252";
            this.Org.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "ORG_NO";
            this.Org.KeyFields.Add(keyItem6);
            this.Org.MultiSetWhere = false;
            this.Org.Name = "Org";
            this.Org.NotificationAutoEnlist = false;
            this.Org.SecExcept = null;
            this.Org.SecFieldName = null;
            this.Org.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Org.SelectPaging = false;
            this.Org.SelectTop = 0;
            this.Org.SiteControl = false;
            this.Org.SiteFieldName = null;
            this.Org.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // OrderFlow
            // 
            this.OrderFlow.CacheConnection = false;
            this.OrderFlow.CommandText = "select ERPOrderFlowM.OrderFlowNO,ERPOrderFlowM.OrderFlowName from ERPOrderFlowM O" +
    "RDER BY ERPOrderFlowM.OrderFlowNO";
            this.OrderFlow.CommandTimeout = 30;
            this.OrderFlow.CommandType = System.Data.CommandType.Text;
            this.OrderFlow.DynamicTableName = false;
            this.OrderFlow.EEPAlias = null;
            this.OrderFlow.EncodingAfter = null;
            this.OrderFlow.EncodingBefore = "Windows-1252";
            this.OrderFlow.InfoConnection = this.InfoConnection1;
            this.OrderFlow.MultiSetWhere = false;
            this.OrderFlow.Name = "OrderFlow";
            this.OrderFlow.NotificationAutoEnlist = false;
            this.OrderFlow.SecExcept = null;
            this.OrderFlow.SecFieldName = null;
            this.OrderFlow.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.OrderFlow.SelectPaging = false;
            this.OrderFlow.SelectTop = 0;
            this.OrderFlow.SiteControl = false;
            this.OrderFlow.SiteFieldName = null;
            this.OrderFlow.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPPreOrderM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPPreOrderD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPPreOrderM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Org)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderFlow)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPPreOrderM;
        private Srvtools.UpdateComponent ucERPPreOrderM;
        private Srvtools.InfoCommand ERPPreOrderD;
        private Srvtools.UpdateComponent ucERPPreOrderD;
        private Srvtools.InfoDataSource idERPPreOrderM_ERPPreOrderD;
        private Srvtools.InfoCommand View_ERPPreOrderM;
        private Srvtools.InfoCommand Customers;
        private Srvtools.InfoCommand Org;
        private Srvtools.InfoCommand OrderFlow;
    }
}
