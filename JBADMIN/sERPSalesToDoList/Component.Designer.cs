namespace sERPSalesToDoList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr26 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr27 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr28 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalesMaster = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesMaster = new Srvtools.UpdateComponent(this.components);
            this.ERPSalesDetails = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesDetails = new Srvtools.UpdateComponent(this.components);
            this.infoQuery = new Srvtools.InfoCommand(this.components);
            this.infoCustomerToDoNotes = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.updateCustomerToDoNotes = new Srvtools.UpdateComponent(this.components);
            this.infoNextCallTime = new Srvtools.InfoCommand(this.components);
            this.infoPostSource = new Srvtools.InfoCommand(this.components);
            this.infoPostType = new Srvtools.InfoCommand(this.components);
            this.infoCustomerToDoNotesList = new Srvtools.InfoCommand(this.components);
            this.infoTempToDoNotes = new Srvtools.InfoCommand(this.components);
            this.updateTempToDoNotes = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomerToDoNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoNextCallTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPostSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPostType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomerToDoNotesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoTempToDoNotes)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "AddCustomerToDoNotes";
            service1.NonLogin = false;
            service1.ServiceName = "AddCustomerToDoNotes";
            service2.DelegateName = "AddCustomerToDoNotesSalse";
            service2.NonLogin = false;
            service2.ServiceName = "AddCustomerToDoNotesSalse";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalesMaster
            // 
            this.ERPSalesMaster.CacheConnection = false;
            this.ERPSalesMaster.CommandText = resources.GetString("ERPSalesMaster.CommandText");
            this.ERPSalesMaster.CommandTimeout = 30;
            this.ERPSalesMaster.CommandType = System.Data.CommandType.Text;
            this.ERPSalesMaster.DynamicTableName = false;
            this.ERPSalesMaster.EEPAlias = null;
            this.ERPSalesMaster.EncodingAfter = null;
            this.ERPSalesMaster.EncodingBefore = "Windows-1252";
            this.ERPSalesMaster.EncodingConvert = null;
            this.ERPSalesMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesMasterNO";
            this.ERPSalesMaster.KeyFields.Add(keyItem1);
            this.ERPSalesMaster.MultiSetWhere = false;
            this.ERPSalesMaster.Name = "ERPSalesMaster";
            this.ERPSalesMaster.NotificationAutoEnlist = false;
            this.ERPSalesMaster.SecExcept = null;
            this.ERPSalesMaster.SecFieldName = null;
            this.ERPSalesMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesMaster.SelectPaging = false;
            this.ERPSalesMaster.SelectTop = 0;
            this.ERPSalesMaster.SiteControl = false;
            this.ERPSalesMaster.SiteFieldName = null;
            this.ERPSalesMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalesMaster
            // 
            this.ucERPSalesMaster.AutoTrans = true;
            this.ucERPSalesMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalesMasterNO";
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
            fieldAttr3.DataField = "KeepDays";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "KeepDaysAlert";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucERPSalesMaster.FieldAttrs.Add(fieldAttr1);
            this.ucERPSalesMaster.FieldAttrs.Add(fieldAttr2);
            this.ucERPSalesMaster.FieldAttrs.Add(fieldAttr3);
            this.ucERPSalesMaster.FieldAttrs.Add(fieldAttr4);
            this.ucERPSalesMaster.LogInfo = null;
            this.ucERPSalesMaster.Name = "ucERPSalesMaster";
            this.ucERPSalesMaster.RowAffectsCheck = true;
            this.ucERPSalesMaster.SelectCmd = this.ERPSalesMaster;
            this.ucERPSalesMaster.SelectCmdForUpdate = null;
            this.ucERPSalesMaster.SendSQLCmd = true;
            this.ucERPSalesMaster.ServerModify = true;
            this.ucERPSalesMaster.ServerModifyGetMax = false;
            this.ucERPSalesMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalesMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalesMaster.UseTranscationScope = false;
            this.ucERPSalesMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ERPSalesDetails
            // 
            this.ERPSalesDetails.CacheConnection = false;
            this.ERPSalesDetails.CommandText = resources.GetString("ERPSalesDetails.CommandText");
            this.ERPSalesDetails.CommandTimeout = 30;
            this.ERPSalesDetails.CommandType = System.Data.CommandType.Text;
            this.ERPSalesDetails.DynamicTableName = false;
            this.ERPSalesDetails.EEPAlias = null;
            this.ERPSalesDetails.EncodingAfter = null;
            this.ERPSalesDetails.EncodingBefore = "Windows-1252";
            this.ERPSalesDetails.EncodingConvert = null;
            this.ERPSalesDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SalesMasterNO";
            keyItem3.KeyName = "ItemSeq";
            this.ERPSalesDetails.KeyFields.Add(keyItem2);
            this.ERPSalesDetails.KeyFields.Add(keyItem3);
            this.ERPSalesDetails.MultiSetWhere = false;
            this.ERPSalesDetails.Name = "ERPSalesDetails";
            this.ERPSalesDetails.NotificationAutoEnlist = false;
            this.ERPSalesDetails.SecExcept = null;
            this.ERPSalesDetails.SecFieldName = null;
            this.ERPSalesDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesDetails.SelectPaging = false;
            this.ERPSalesDetails.SelectTop = 0;
            this.ERPSalesDetails.SiteControl = false;
            this.ERPSalesDetails.SiteFieldName = null;
            this.ERPSalesDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalesDetails
            // 
            this.ucERPSalesDetails.AutoTrans = true;
            this.ucERPSalesDetails.ExceptJoin = false;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "SalesMasterNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ItemSeq";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CustNO";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "SalesDescr";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "SalesDescrDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "SalesDescrAlert";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr5);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr6);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr7);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr8);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr9);
            this.ucERPSalesDetails.FieldAttrs.Add(fieldAttr10);
            this.ucERPSalesDetails.LogInfo = null;
            this.ucERPSalesDetails.Name = "ucERPSalesDetails";
            this.ucERPSalesDetails.RowAffectsCheck = true;
            this.ucERPSalesDetails.SelectCmd = this.ERPSalesDetails;
            this.ucERPSalesDetails.SelectCmdForUpdate = null;
            this.ucERPSalesDetails.SendSQLCmd = true;
            this.ucERPSalesDetails.ServerModify = true;
            this.ucERPSalesDetails.ServerModifyGetMax = false;
            this.ucERPSalesDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalesDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalesDetails.UseTranscationScope = false;
            this.ucERPSalesDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoQuery
            // 
            this.infoQuery.CacheConnection = false;
            this.infoQuery.CommandText = "select \'\' SalesID,\'\' as CustNO,\'\' as SDate,\'\' as EDate,0 as Sourse";
            this.infoQuery.CommandTimeout = 30;
            this.infoQuery.CommandType = System.Data.CommandType.Text;
            this.infoQuery.DynamicTableName = false;
            this.infoQuery.EEPAlias = null;
            this.infoQuery.EncodingAfter = null;
            this.infoQuery.EncodingBefore = "Windows-1252";
            this.infoQuery.EncodingConvert = null;
            this.infoQuery.InfoConnection = this.InfoConnection1;
            this.infoQuery.MultiSetWhere = false;
            this.infoQuery.Name = "infoQuery";
            this.infoQuery.NotificationAutoEnlist = false;
            this.infoQuery.SecExcept = null;
            this.infoQuery.SecFieldName = null;
            this.infoQuery.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoQuery.SelectPaging = false;
            this.infoQuery.SelectTop = 0;
            this.infoQuery.SiteControl = false;
            this.infoQuery.SiteFieldName = null;
            this.infoQuery.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCustomerToDoNotes
            // 
            this.infoCustomerToDoNotes.CacheConnection = false;
            this.infoCustomerToDoNotes.CommandText = resources.GetString("infoCustomerToDoNotes.CommandText");
            this.infoCustomerToDoNotes.CommandTimeout = 30;
            this.infoCustomerToDoNotes.CommandType = System.Data.CommandType.Text;
            this.infoCustomerToDoNotes.DynamicTableName = false;
            this.infoCustomerToDoNotes.EEPAlias = "";
            this.infoCustomerToDoNotes.EncodingAfter = null;
            this.infoCustomerToDoNotes.EncodingBefore = "Windows-1252";
            this.infoCustomerToDoNotes.EncodingConvert = null;
            this.infoCustomerToDoNotes.InfoConnection = this.infoConnection2;
            keyItem4.KeyName = "CustNO";
            this.infoCustomerToDoNotes.KeyFields.Add(keyItem4);
            this.infoCustomerToDoNotes.MultiSetWhere = false;
            this.infoCustomerToDoNotes.Name = "infoCustomerToDoNotes";
            this.infoCustomerToDoNotes.NotificationAutoEnlist = false;
            this.infoCustomerToDoNotes.SecExcept = null;
            this.infoCustomerToDoNotes.SecFieldName = null;
            this.infoCustomerToDoNotes.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustomerToDoNotes.SelectPaging = false;
            this.infoCustomerToDoNotes.SelectTop = 0;
            this.infoCustomerToDoNotes.SiteControl = false;
            this.infoCustomerToDoNotes.SiteFieldName = null;
            this.infoCustomerToDoNotes.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
            // 
            // updateCustomerToDoNotes
            // 
            this.updateCustomerToDoNotes.AutoTrans = true;
            this.updateCustomerToDoNotes.ExceptJoin = false;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "AutoKey";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CustNO";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "Notes";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "NextCallDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "NextCallTime";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "NotesCreateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "UpdateBy";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "UpateDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.updateCustomerToDoNotes.FieldAttrs.Add(fieldAttr11);
            this.updateCustomerToDoNotes.FieldAttrs.Add(fieldAttr12);
            this.updateCustomerToDoNotes.FieldAttrs.Add(fieldAttr13);
            this.updateCustomerToDoNotes.FieldAttrs.Add(fieldAttr14);
            this.updateCustomerToDoNotes.FieldAttrs.Add(fieldAttr15);
            this.updateCustomerToDoNotes.FieldAttrs.Add(fieldAttr16);
            this.updateCustomerToDoNotes.FieldAttrs.Add(fieldAttr17);
            this.updateCustomerToDoNotes.FieldAttrs.Add(fieldAttr18);
            this.updateCustomerToDoNotes.LogInfo = null;
            this.updateCustomerToDoNotes.Name = "updateCustomerToDoNotes";
            this.updateCustomerToDoNotes.RowAffectsCheck = true;
            this.updateCustomerToDoNotes.SelectCmd = this.infoCustomerToDoNotes;
            this.updateCustomerToDoNotes.SelectCmdForUpdate = null;
            this.updateCustomerToDoNotes.SendSQLCmd = true;
            this.updateCustomerToDoNotes.ServerModify = true;
            this.updateCustomerToDoNotes.ServerModifyGetMax = false;
            this.updateCustomerToDoNotes.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.updateCustomerToDoNotes.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.updateCustomerToDoNotes.UseTranscationScope = false;
            this.updateCustomerToDoNotes.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.updateCustomerToDoNotes.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.updateCustomerToDoNotes_BeforeModify);
            // 
            // infoNextCallTime
            // 
            this.infoNextCallTime.CacheConnection = false;
            this.infoNextCallTime.CommandText = "select  distinct Isnull(NextCallTime,\'\') as NextCallTime\r\nfrom ERPCustomerToDoNot" +
    "es\r\norder by NextCallTime";
            this.infoNextCallTime.CommandTimeout = 30;
            this.infoNextCallTime.CommandType = System.Data.CommandType.Text;
            this.infoNextCallTime.DynamicTableName = false;
            this.infoNextCallTime.EEPAlias = "";
            this.infoNextCallTime.EncodingAfter = null;
            this.infoNextCallTime.EncodingBefore = "Windows-1252";
            this.infoNextCallTime.EncodingConvert = null;
            this.infoNextCallTime.InfoConnection = this.infoConnection2;
            this.infoNextCallTime.MultiSetWhere = false;
            this.infoNextCallTime.Name = "infoNextCallTime";
            this.infoNextCallTime.NotificationAutoEnlist = false;
            this.infoNextCallTime.SecExcept = null;
            this.infoNextCallTime.SecFieldName = null;
            this.infoNextCallTime.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoNextCallTime.SelectPaging = false;
            this.infoNextCallTime.SelectTop = 0;
            this.infoNextCallTime.SiteControl = false;
            this.infoNextCallTime.SiteFieldName = null;
            this.infoNextCallTime.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoPostSource
            // 
            this.infoPostSource.CacheConnection = false;
            this.infoPostSource.CommandText = "select  ID,SourceName\r\nfrom ERPPostSource";
            this.infoPostSource.CommandTimeout = 30;
            this.infoPostSource.CommandType = System.Data.CommandType.Text;
            this.infoPostSource.DynamicTableName = false;
            this.infoPostSource.EEPAlias = "";
            this.infoPostSource.EncodingAfter = null;
            this.infoPostSource.EncodingBefore = "Windows-1252";
            this.infoPostSource.EncodingConvert = null;
            this.infoPostSource.InfoConnection = this.infoConnection2;
            this.infoPostSource.MultiSetWhere = false;
            this.infoPostSource.Name = "infoPostSource";
            this.infoPostSource.NotificationAutoEnlist = false;
            this.infoPostSource.SecExcept = null;
            this.infoPostSource.SecFieldName = null;
            this.infoPostSource.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoPostSource.SelectPaging = false;
            this.infoPostSource.SelectTop = 0;
            this.infoPostSource.SiteControl = false;
            this.infoPostSource.SiteFieldName = null;
            this.infoPostSource.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoPostType
            // 
            this.infoPostType.CacheConnection = false;
            this.infoPostType.CommandText = "select  ListID,ListContent\r\nfrom ERPReferenceTable\r\nwhere ListCategory=\'PostType\'" +
    "";
            this.infoPostType.CommandTimeout = 30;
            this.infoPostType.CommandType = System.Data.CommandType.Text;
            this.infoPostType.DynamicTableName = false;
            this.infoPostType.EEPAlias = "";
            this.infoPostType.EncodingAfter = null;
            this.infoPostType.EncodingBefore = "Windows-1252";
            this.infoPostType.EncodingConvert = null;
            this.infoPostType.InfoConnection = this.infoConnection2;
            this.infoPostType.MultiSetWhere = false;
            this.infoPostType.Name = "infoPostType";
            this.infoPostType.NotificationAutoEnlist = false;
            this.infoPostType.SecExcept = null;
            this.infoPostType.SecFieldName = null;
            this.infoPostType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoPostType.SelectPaging = false;
            this.infoPostType.SelectTop = 0;
            this.infoPostType.SiteControl = false;
            this.infoPostType.SiteFieldName = null;
            this.infoPostType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCustomerToDoNotesList
            // 
            this.infoCustomerToDoNotesList.CacheConnection = false;
            this.infoCustomerToDoNotesList.CommandText = resources.GetString("infoCustomerToDoNotesList.CommandText");
            this.infoCustomerToDoNotesList.CommandTimeout = 30;
            this.infoCustomerToDoNotesList.CommandType = System.Data.CommandType.Text;
            this.infoCustomerToDoNotesList.DynamicTableName = false;
            this.infoCustomerToDoNotesList.EEPAlias = "";
            this.infoCustomerToDoNotesList.EncodingAfter = null;
            this.infoCustomerToDoNotesList.EncodingBefore = "Windows-1252";
            this.infoCustomerToDoNotesList.EncodingConvert = null;
            this.infoCustomerToDoNotesList.InfoConnection = this.infoConnection2;
            keyItem5.KeyName = "AutoKey";
            this.infoCustomerToDoNotesList.KeyFields.Add(keyItem5);
            this.infoCustomerToDoNotesList.MultiSetWhere = false;
            this.infoCustomerToDoNotesList.Name = "infoCustomerToDoNotesList";
            this.infoCustomerToDoNotesList.NotificationAutoEnlist = false;
            this.infoCustomerToDoNotesList.SecExcept = null;
            this.infoCustomerToDoNotesList.SecFieldName = null;
            this.infoCustomerToDoNotesList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustomerToDoNotesList.SelectPaging = false;
            this.infoCustomerToDoNotesList.SelectTop = 0;
            this.infoCustomerToDoNotesList.SiteControl = false;
            this.infoCustomerToDoNotesList.SiteFieldName = null;
            this.infoCustomerToDoNotesList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoTempToDoNotes
            // 
            this.infoTempToDoNotes.CacheConnection = false;
            this.infoTempToDoNotes.CommandText = resources.GetString("infoTempToDoNotes.CommandText");
            this.infoTempToDoNotes.CommandTimeout = 30;
            this.infoTempToDoNotes.CommandType = System.Data.CommandType.Text;
            this.infoTempToDoNotes.DynamicTableName = false;
            this.infoTempToDoNotes.EEPAlias = "";
            this.infoTempToDoNotes.EncodingAfter = null;
            this.infoTempToDoNotes.EncodingBefore = "Windows-1252";
            this.infoTempToDoNotes.EncodingConvert = null;
            this.infoTempToDoNotes.InfoConnection = this.infoConnection2;
            keyItem6.KeyName = "CustNO";
            this.infoTempToDoNotes.KeyFields.Add(keyItem6);
            this.infoTempToDoNotes.MultiSetWhere = false;
            this.infoTempToDoNotes.Name = "infoTempToDoNotes";
            this.infoTempToDoNotes.NotificationAutoEnlist = false;
            this.infoTempToDoNotes.SecExcept = null;
            this.infoTempToDoNotes.SecFieldName = null;
            this.infoTempToDoNotes.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoTempToDoNotes.SelectPaging = false;
            this.infoTempToDoNotes.SelectTop = 0;
            this.infoTempToDoNotes.SiteControl = false;
            this.infoTempToDoNotes.SiteFieldName = null;
            this.infoTempToDoNotes.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // updateTempToDoNotes
            // 
            this.updateTempToDoNotes.AutoTrans = true;
            this.updateTempToDoNotes.ExceptJoin = false;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "AutoKey";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CustNO";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "Notes";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "NextCallDate";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "NextCallTime";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "NotesCreateDate";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "UpdateBy";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "UpateDate";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "CustShortName";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "CustName";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr19);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr20);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr21);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr22);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr23);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr24);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr25);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr26);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr27);
            this.updateTempToDoNotes.FieldAttrs.Add(fieldAttr28);
            this.updateTempToDoNotes.LogInfo = null;
            this.updateTempToDoNotes.Name = "updateTempToDoNotes";
            this.updateTempToDoNotes.RowAffectsCheck = true;
            this.updateTempToDoNotes.SelectCmd = this.infoTempToDoNotes;
            this.updateTempToDoNotes.SelectCmdForUpdate = null;
            this.updateTempToDoNotes.SendSQLCmd = true;
            this.updateTempToDoNotes.ServerModify = true;
            this.updateTempToDoNotes.ServerModifyGetMax = false;
            this.updateTempToDoNotes.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.updateTempToDoNotes.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.updateTempToDoNotes.UseTranscationScope = false;
            this.updateTempToDoNotes.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomerToDoNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoNextCallTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPostSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPostType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomerToDoNotesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoTempToDoNotes)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesMaster;
        private Srvtools.UpdateComponent ucERPSalesMaster;
        private Srvtools.InfoCommand ERPSalesDetails;
        private Srvtools.UpdateComponent ucERPSalesDetails;
        private Srvtools.InfoCommand infoQuery;
        private Srvtools.InfoCommand infoCustomerToDoNotes;
        private Srvtools.UpdateComponent updateCustomerToDoNotes;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand infoNextCallTime;
        private Srvtools.InfoCommand infoPostSource;
        private Srvtools.InfoCommand infoPostType;
        private Srvtools.InfoCommand infoCustomerToDoNotesList;
        private Srvtools.InfoCommand infoTempToDoNotes;
        private Srvtools.UpdateComponent updateTempToDoNotes;
    }
}
