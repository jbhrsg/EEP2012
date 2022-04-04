namespace sInvoiceVoidApply
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
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.Service service7 = new Srvtools.Service();
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
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPInvoiceVoidApplyMaster = new Srvtools.InfoCommand(this.components);
            this.ucERPInvoiceVoidApplyMaster = new Srvtools.UpdateComponent(this.components);
            this.ERPInvoiceVoidApplyDetails = new Srvtools.InfoCommand(this.components);
            this.ucERPInvoiceVoidApplyDetails = new Srvtools.UpdateComponent(this.components);
            this.View_ERPInvoiceVoidApplyMaster = new Srvtools.InfoCommand(this.components);
            this.InvoiceLists = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPInvoiceVoidApplyMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPInvoiceVoidApplyDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPInvoiceVoidApplyMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceLists)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetEmpFlowAgentList";
            service1.NonLogin = false;
            service1.ServiceName = "GetEmpFlowAgentList";
            service2.DelegateName = "GetUserOrgNOs";
            service2.NonLogin = false;
            service2.ServiceName = "GetUserOrgNOs";
            service3.DelegateName = "GetInvoiceVoidApplyDetails";
            service3.NonLogin = false;
            service3.ServiceName = "GetInvoiceVoidApplyDetails";
            service4.DelegateName = "GetInvoiceVoidNO";
            service4.NonLogin = false;
            service4.ServiceName = "GetInvoiceVoidNO";
            service5.DelegateName = "VoidToNjbLTODLVER";
            service5.NonLogin = false;
            service5.ServiceName = "VoidToNjbLTODLVER";
            service6.DelegateName = "IsInvoiceNOExist";
            service6.NonLogin = false;
            service6.ServiceName = "IsInvoiceNOExist";
            service7.DelegateName = "GetSignCount";
            service7.NonLogin = false;
            service7.ServiceName = "GetSignCount";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPInvoiceVoidApplyMaster
            // 
            this.ERPInvoiceVoidApplyMaster.CacheConnection = false;
            this.ERPInvoiceVoidApplyMaster.CommandText = "SELECT TOP 50\r\ndbo.[ERPInvoiceVoidApplyMaster].* FROM dbo.[ERPInvoiceVoidApplyMas" +
    "ter]";
            this.ERPInvoiceVoidApplyMaster.CommandTimeout = 30;
            this.ERPInvoiceVoidApplyMaster.CommandType = System.Data.CommandType.Text;
            this.ERPInvoiceVoidApplyMaster.DynamicTableName = false;
            this.ERPInvoiceVoidApplyMaster.EEPAlias = null;
            this.ERPInvoiceVoidApplyMaster.EncodingAfter = null;
            this.ERPInvoiceVoidApplyMaster.EncodingBefore = "Windows-1252";
            this.ERPInvoiceVoidApplyMaster.EncodingConvert = null;
            this.ERPInvoiceVoidApplyMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "InvoiceVoidNO";
            this.ERPInvoiceVoidApplyMaster.KeyFields.Add(keyItem1);
            this.ERPInvoiceVoidApplyMaster.MultiSetWhere = false;
            this.ERPInvoiceVoidApplyMaster.Name = "ERPInvoiceVoidApplyMaster";
            this.ERPInvoiceVoidApplyMaster.NotificationAutoEnlist = false;
            this.ERPInvoiceVoidApplyMaster.SecExcept = null;
            this.ERPInvoiceVoidApplyMaster.SecFieldName = null;
            this.ERPInvoiceVoidApplyMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPInvoiceVoidApplyMaster.SelectPaging = false;
            this.ERPInvoiceVoidApplyMaster.SelectTop = 0;
            this.ERPInvoiceVoidApplyMaster.SiteControl = false;
            this.ERPInvoiceVoidApplyMaster.SiteFieldName = null;
            this.ERPInvoiceVoidApplyMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPInvoiceVoidApplyMaster
            // 
            this.ucERPInvoiceVoidApplyMaster.AutoTrans = true;
            this.ucERPInvoiceVoidApplyMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "InvoiceVoidNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ApplyDate";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ApplyEmpID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ApplyOrg_NO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Org_NOParent";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CustNO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CustShortName";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "InvoiceYM";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "InvoiceNO";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "InvoiceAmt";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "InvoiceTax";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "VoidNotes";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "FlowFlag";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateBy";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = "_username";
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr1);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr2);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr3);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr4);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr5);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr6);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr7);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr8);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr9);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr10);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr11);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr12);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr13);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr14);
            this.ucERPInvoiceVoidApplyMaster.FieldAttrs.Add(fieldAttr15);
            this.ucERPInvoiceVoidApplyMaster.LogInfo = null;
            this.ucERPInvoiceVoidApplyMaster.Name = "ucERPInvoiceVoidApplyMaster";
            this.ucERPInvoiceVoidApplyMaster.RowAffectsCheck = true;
            this.ucERPInvoiceVoidApplyMaster.SelectCmd = this.ERPInvoiceVoidApplyMaster;
            this.ucERPInvoiceVoidApplyMaster.SelectCmdForUpdate = null;
            this.ucERPInvoiceVoidApplyMaster.SendSQLCmd = true;
            this.ucERPInvoiceVoidApplyMaster.ServerModify = true;
            this.ucERPInvoiceVoidApplyMaster.ServerModifyGetMax = false;
            this.ucERPInvoiceVoidApplyMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPInvoiceVoidApplyMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPInvoiceVoidApplyMaster.UseTranscationScope = false;
            this.ucERPInvoiceVoidApplyMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucERPInvoiceVoidApplyMaster.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucERPInvoiceVoidApplyMaster_BeforeInsert);
            // 
            // ERPInvoiceVoidApplyDetails
            // 
            this.ERPInvoiceVoidApplyDetails.CacheConnection = false;
            this.ERPInvoiceVoidApplyDetails.CommandText = "SELECT  dbo.[ERPInvoiceVoidApplyDetails].* \r\nFROM dbo.[ERPInvoiceVoidApplyDetails" +
    "]";
            this.ERPInvoiceVoidApplyDetails.CommandTimeout = 30;
            this.ERPInvoiceVoidApplyDetails.CommandType = System.Data.CommandType.Text;
            this.ERPInvoiceVoidApplyDetails.DynamicTableName = false;
            this.ERPInvoiceVoidApplyDetails.EEPAlias = null;
            this.ERPInvoiceVoidApplyDetails.EncodingAfter = null;
            this.ERPInvoiceVoidApplyDetails.EncodingBefore = "Windows-1252";
            this.ERPInvoiceVoidApplyDetails.EncodingConvert = null;
            this.ERPInvoiceVoidApplyDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.ERPInvoiceVoidApplyDetails.KeyFields.Add(keyItem2);
            this.ERPInvoiceVoidApplyDetails.MultiSetWhere = false;
            this.ERPInvoiceVoidApplyDetails.Name = "ERPInvoiceVoidApplyDetails";
            this.ERPInvoiceVoidApplyDetails.NotificationAutoEnlist = false;
            this.ERPInvoiceVoidApplyDetails.SecExcept = null;
            this.ERPInvoiceVoidApplyDetails.SecFieldName = null;
            this.ERPInvoiceVoidApplyDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPInvoiceVoidApplyDetails.SelectPaging = false;
            this.ERPInvoiceVoidApplyDetails.SelectTop = 0;
            this.ERPInvoiceVoidApplyDetails.SiteControl = false;
            this.ERPInvoiceVoidApplyDetails.SiteFieldName = null;
            this.ERPInvoiceVoidApplyDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPInvoiceVoidApplyDetails
            // 
            this.ucERPInvoiceVoidApplyDetails.AutoTrans = true;
            this.ucERPInvoiceVoidApplyDetails.ExceptJoin = false;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "AutoKey";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "InvoiceVoidNO";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "InvoiceNO";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "ItemNO";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "InvoiceAmt";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "InvoiceTax";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "CreateBy";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "CreateDate";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            this.ucERPInvoiceVoidApplyDetails.FieldAttrs.Add(fieldAttr16);
            this.ucERPInvoiceVoidApplyDetails.FieldAttrs.Add(fieldAttr17);
            this.ucERPInvoiceVoidApplyDetails.FieldAttrs.Add(fieldAttr18);
            this.ucERPInvoiceVoidApplyDetails.FieldAttrs.Add(fieldAttr19);
            this.ucERPInvoiceVoidApplyDetails.FieldAttrs.Add(fieldAttr20);
            this.ucERPInvoiceVoidApplyDetails.FieldAttrs.Add(fieldAttr21);
            this.ucERPInvoiceVoidApplyDetails.FieldAttrs.Add(fieldAttr22);
            this.ucERPInvoiceVoidApplyDetails.FieldAttrs.Add(fieldAttr23);
            this.ucERPInvoiceVoidApplyDetails.LogInfo = null;
            this.ucERPInvoiceVoidApplyDetails.Name = "ucERPInvoiceVoidApplyDetails";
            this.ucERPInvoiceVoidApplyDetails.RowAffectsCheck = true;
            this.ucERPInvoiceVoidApplyDetails.SelectCmd = this.ERPInvoiceVoidApplyDetails;
            this.ucERPInvoiceVoidApplyDetails.SelectCmdForUpdate = null;
            this.ucERPInvoiceVoidApplyDetails.SendSQLCmd = true;
            this.ucERPInvoiceVoidApplyDetails.ServerModify = true;
            this.ucERPInvoiceVoidApplyDetails.ServerModifyGetMax = false;
            this.ucERPInvoiceVoidApplyDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPInvoiceVoidApplyDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPInvoiceVoidApplyDetails.UseTranscationScope = false;
            this.ucERPInvoiceVoidApplyDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ERPInvoiceVoidApplyMaster
            // 
            this.View_ERPInvoiceVoidApplyMaster.CacheConnection = false;
            this.View_ERPInvoiceVoidApplyMaster.CommandText = "SELECT * FROM dbo.[ERPInvoiceVoidApplyMaster]";
            this.View_ERPInvoiceVoidApplyMaster.CommandTimeout = 30;
            this.View_ERPInvoiceVoidApplyMaster.CommandType = System.Data.CommandType.Text;
            this.View_ERPInvoiceVoidApplyMaster.DynamicTableName = false;
            this.View_ERPInvoiceVoidApplyMaster.EEPAlias = null;
            this.View_ERPInvoiceVoidApplyMaster.EncodingAfter = null;
            this.View_ERPInvoiceVoidApplyMaster.EncodingBefore = "Windows-1252";
            this.View_ERPInvoiceVoidApplyMaster.EncodingConvert = null;
            this.View_ERPInvoiceVoidApplyMaster.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "InvoiceVoidNO";
            this.View_ERPInvoiceVoidApplyMaster.KeyFields.Add(keyItem3);
            this.View_ERPInvoiceVoidApplyMaster.MultiSetWhere = false;
            this.View_ERPInvoiceVoidApplyMaster.Name = "View_ERPInvoiceVoidApplyMaster";
            this.View_ERPInvoiceVoidApplyMaster.NotificationAutoEnlist = false;
            this.View_ERPInvoiceVoidApplyMaster.SecExcept = null;
            this.View_ERPInvoiceVoidApplyMaster.SecFieldName = null;
            this.View_ERPInvoiceVoidApplyMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPInvoiceVoidApplyMaster.SelectPaging = false;
            this.View_ERPInvoiceVoidApplyMaster.SelectTop = 0;
            this.View_ERPInvoiceVoidApplyMaster.SiteControl = false;
            this.View_ERPInvoiceVoidApplyMaster.SiteFieldName = null;
            this.View_ERPInvoiceVoidApplyMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InvoiceLists
            // 
            this.InvoiceLists.CacheConnection = false;
            this.InvoiceLists.CommandText = resources.GetString("InvoiceLists.CommandText");
            this.InvoiceLists.CommandTimeout = 30;
            this.InvoiceLists.CommandType = System.Data.CommandType.Text;
            this.InvoiceLists.DynamicTableName = false;
            this.InvoiceLists.EEPAlias = null;
            this.InvoiceLists.EncodingAfter = null;
            this.InvoiceLists.EncodingBefore = "Windows-1252";
            this.InvoiceLists.EncodingConvert = null;
            this.InvoiceLists.InfoConnection = this.InfoConnection1;
            this.InvoiceLists.MultiSetWhere = false;
            this.InvoiceLists.Name = "InvoiceLists";
            this.InvoiceLists.NotificationAutoEnlist = false;
            this.InvoiceLists.SecExcept = null;
            this.InvoiceLists.SecFieldName = null;
            this.InvoiceLists.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InvoiceLists.SelectPaging = false;
            this.InvoiceLists.SelectTop = 0;
            this.InvoiceLists.SiteControl = false;
            this.InvoiceLists.SiteFieldName = null;
            this.InvoiceLists.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = resources.GetString("Employee.CommandText");
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem4);
            this.Employee.MultiSetWhere = false;
            this.Employee.Name = "Employee";
            this.Employee.NotificationAutoEnlist = false;
            this.Employee.SecExcept = null;
            this.Employee.SecFieldName = null;
            this.Employee.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Employee.SelectPaging = false;
            this.Employee.SelectTop = 0;
            this.Employee.SiteControl = false;
            this.Employee.SiteFieldName = null;
            this.Employee.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Organization
            // 
            this.Organization.CacheConnection = false;
            this.Organization.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nWHERE (Uppe" +
    "r_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR ORG_NO=\'99999\')\r\nORDER" +
    " BY ORG_NO";
            this.Organization.CommandTimeout = 30;
            this.Organization.CommandType = System.Data.CommandType.Text;
            this.Organization.DynamicTableName = false;
            this.Organization.EEPAlias = null;
            this.Organization.EncodingAfter = null;
            this.Organization.EncodingBefore = "Windows-1252";
            this.Organization.EncodingConvert = null;
            this.Organization.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem5);
            this.Organization.MultiSetWhere = false;
            this.Organization.Name = "Organization";
            this.Organization.NotificationAutoEnlist = false;
            this.Organization.SecExcept = null;
            this.Organization.SecFieldName = null;
            this.Organization.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Organization.SelectPaging = false;
            this.Organization.SelectTop = 0;
            this.Organization.SiteControl = false;
            this.Organization.SiteFieldName = null;
            this.Organization.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPInvoiceVoidApplyMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPInvoiceVoidApplyDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPInvoiceVoidApplyMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceLists)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPInvoiceVoidApplyMaster;
        private Srvtools.UpdateComponent ucERPInvoiceVoidApplyMaster;
        private Srvtools.InfoCommand ERPInvoiceVoidApplyDetails;
        private Srvtools.UpdateComponent ucERPInvoiceVoidApplyDetails;
        private Srvtools.InfoCommand View_ERPInvoiceVoidApplyMaster;
        private Srvtools.InfoCommand InvoiceLists;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand Organization;
    }
}
