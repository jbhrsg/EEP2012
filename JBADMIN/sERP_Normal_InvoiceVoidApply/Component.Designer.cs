namespace sERP_Normal_InvoiceVoidApply
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
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem9 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.InvoiceVoidApply = new Srvtools.InfoCommand(this.components);
            this.ucInvoiceVoidApply = new Srvtools.UpdateComponent(this.components);
            this.View_InvoiceVoidApply = new Srvtools.InfoCommand(this.components);
            this.SalesDetails = new Srvtools.InfoCommand(this.components);
            this.InvoiceDetails = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            this.QInvoiceType = new Srvtools.InfoCommand(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceVoidApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_InvoiceVoidApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QInvoiceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetInvoiceVoidNO";
            service1.NonLogin = false;
            service1.ServiceName = "GetInvoiceVoidNO";
            service2.DelegateName = "GetUserOrgNOs";
            service2.NonLogin = false;
            service2.ServiceName = "GetUserOrgNOs";
            service3.DelegateName = "GetEmpFlowAgentList";
            service3.NonLogin = false;
            service3.ServiceName = "GetEmpFlowAgentList";
            service4.DelegateName = "VoidInvoiceDetailsNSalesMaster";
            service4.NonLogin = false;
            service4.ServiceName = "VoidInvoiceDetailsNSalesMaster";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // InvoiceVoidApply
            // 
            this.InvoiceVoidApply.CacheConnection = false;
            this.InvoiceVoidApply.CommandText = "SELECT dbo.[InvoiceVoidApply].* FROM dbo.[InvoiceVoidApply]";
            this.InvoiceVoidApply.CommandTimeout = 30;
            this.InvoiceVoidApply.CommandType = System.Data.CommandType.Text;
            this.InvoiceVoidApply.DynamicTableName = false;
            this.InvoiceVoidApply.EEPAlias = "JBERP";
            this.InvoiceVoidApply.EncodingAfter = null;
            this.InvoiceVoidApply.EncodingBefore = "Windows-1252";
            this.InvoiceVoidApply.EncodingConvert = null;
            this.InvoiceVoidApply.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "InvoiceVoidNO";
            this.InvoiceVoidApply.KeyFields.Add(keyItem1);
            this.InvoiceVoidApply.MultiSetWhere = false;
            this.InvoiceVoidApply.Name = "InvoiceVoidApply";
            this.InvoiceVoidApply.NotificationAutoEnlist = false;
            this.InvoiceVoidApply.SecExcept = null;
            this.InvoiceVoidApply.SecFieldName = null;
            this.InvoiceVoidApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InvoiceVoidApply.SelectPaging = false;
            this.InvoiceVoidApply.SelectTop = 0;
            this.InvoiceVoidApply.SiteControl = false;
            this.InvoiceVoidApply.SiteFieldName = null;
            this.InvoiceVoidApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucInvoiceVoidApply
            // 
            this.ucInvoiceVoidApply.AutoTrans = true;
            this.ucInvoiceVoidApply.ExceptJoin = false;
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
            fieldAttr6.DataField = "VoidNotes";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CustomerID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ShortName";
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
            fieldAttr10.DataField = "SalesAmount";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "SalesTax";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "SalesTotal";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "SalesNO";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "QInvoiceType";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = "_username";
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CreateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = "_sysdate";
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "IsVoidSalesMaster";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = "";
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr1);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr2);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr3);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr4);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr5);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr6);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr7);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr8);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr9);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr10);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr11);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr12);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr13);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr14);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr15);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr16);
            this.ucInvoiceVoidApply.FieldAttrs.Add(fieldAttr17);
            this.ucInvoiceVoidApply.LogInfo = null;
            this.ucInvoiceVoidApply.Name = "ucInvoiceVoidApply";
            this.ucInvoiceVoidApply.RowAffectsCheck = true;
            this.ucInvoiceVoidApply.SelectCmd = this.InvoiceVoidApply;
            this.ucInvoiceVoidApply.SelectCmdForUpdate = null;
            this.ucInvoiceVoidApply.SendSQLCmd = true;
            this.ucInvoiceVoidApply.ServerModify = true;
            this.ucInvoiceVoidApply.ServerModifyGetMax = false;
            this.ucInvoiceVoidApply.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucInvoiceVoidApply.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucInvoiceVoidApply.UseTranscationScope = false;
            this.ucInvoiceVoidApply.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_InvoiceVoidApply
            // 
            this.View_InvoiceVoidApply.CacheConnection = false;
            this.View_InvoiceVoidApply.CommandText = "SELECT * FROM dbo.[InvoiceVoidApply]";
            this.View_InvoiceVoidApply.CommandTimeout = 30;
            this.View_InvoiceVoidApply.CommandType = System.Data.CommandType.Text;
            this.View_InvoiceVoidApply.DynamicTableName = false;
            this.View_InvoiceVoidApply.EEPAlias = "JBERP";
            this.View_InvoiceVoidApply.EncodingAfter = null;
            this.View_InvoiceVoidApply.EncodingBefore = "Windows-1252";
            this.View_InvoiceVoidApply.EncodingConvert = null;
            this.View_InvoiceVoidApply.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "InvoiceVoidNO";
            this.View_InvoiceVoidApply.KeyFields.Add(keyItem2);
            this.View_InvoiceVoidApply.MultiSetWhere = false;
            this.View_InvoiceVoidApply.Name = "View_InvoiceVoidApply";
            this.View_InvoiceVoidApply.NotificationAutoEnlist = false;
            this.View_InvoiceVoidApply.SecExcept = null;
            this.View_InvoiceVoidApply.SecFieldName = null;
            this.View_InvoiceVoidApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_InvoiceVoidApply.SelectPaging = false;
            this.View_InvoiceVoidApply.SelectTop = 0;
            this.View_InvoiceVoidApply.SiteControl = false;
            this.View_InvoiceVoidApply.SiteFieldName = null;
            this.View_InvoiceVoidApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesDetails
            // 
            this.SalesDetails.CacheConnection = false;
            this.SalesDetails.CommandText = "SELECT * FROM dbo.SalesDetails";
            this.SalesDetails.CommandTimeout = 30;
            this.SalesDetails.CommandType = System.Data.CommandType.Text;
            this.SalesDetails.DynamicTableName = false;
            this.SalesDetails.EEPAlias = "JBERP";
            this.SalesDetails.EncodingAfter = null;
            this.SalesDetails.EncodingBefore = "Windows-1252";
            this.SalesDetails.EncodingConvert = null;
            this.SalesDetails.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "SalesNO";
            keyItem4.KeyName = "ItemNO";
            this.SalesDetails.KeyFields.Add(keyItem3);
            this.SalesDetails.KeyFields.Add(keyItem4);
            this.SalesDetails.MultiSetWhere = false;
            this.SalesDetails.Name = "SalesDetails";
            this.SalesDetails.NotificationAutoEnlist = false;
            this.SalesDetails.SecExcept = null;
            this.SalesDetails.SecFieldName = null;
            this.SalesDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesDetails.SelectPaging = false;
            this.SalesDetails.SelectTop = 0;
            this.SalesDetails.SiteControl = false;
            this.SalesDetails.SiteFieldName = null;
            this.SalesDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InvoiceDetails
            // 
            this.InvoiceDetails.CacheConnection = false;
            this.InvoiceDetails.CommandText = resources.GetString("InvoiceDetails.CommandText");
            this.InvoiceDetails.CommandTimeout = 30;
            this.InvoiceDetails.CommandType = System.Data.CommandType.Text;
            this.InvoiceDetails.DynamicTableName = false;
            this.InvoiceDetails.EEPAlias = "JBERP";
            this.InvoiceDetails.EncodingAfter = null;
            this.InvoiceDetails.EncodingBefore = "Windows-1252";
            this.InvoiceDetails.EncodingConvert = null;
            this.InvoiceDetails.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "InvoiceNO";
            this.InvoiceDetails.KeyFields.Add(keyItem5);
            this.InvoiceDetails.MultiSetWhere = false;
            this.InvoiceDetails.Name = "InvoiceDetails";
            this.InvoiceDetails.NotificationAutoEnlist = false;
            this.InvoiceDetails.SecExcept = null;
            this.InvoiceDetails.SecFieldName = null;
            this.InvoiceDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InvoiceDetails.SelectPaging = false;
            this.InvoiceDetails.SelectTop = 0;
            this.InvoiceDetails.SiteControl = false;
            this.InvoiceDetails.SiteFieldName = null;
            this.InvoiceDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = resources.GetString("Employee.CommandText");
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = "JBADMIN";
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.infoConnection2;
            keyItem6.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem6);
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
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
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
            this.Organization.EEPAlias = "JBADMIN";
            this.Organization.EncodingAfter = null;
            this.Organization.EncodingBefore = "Windows-1252";
            this.Organization.EncodingConvert = null;
            this.Organization.InfoConnection = this.infoConnection2;
            keyItem7.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem7);
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
            // 
            // QInvoiceType
            // 
            this.QInvoiceType.CacheConnection = false;
            this.QInvoiceType.CommandText = "SELECT  INVOICETYPEID,INVOICETYPENAME  \r\nFROM InvoiceType WHERE  INVOICETYPEID>=9" +
    "7\r\nORDER BY INVOICETYPEID DESC";
            this.QInvoiceType.CommandTimeout = 30;
            this.QInvoiceType.CommandType = System.Data.CommandType.Text;
            this.QInvoiceType.DynamicTableName = false;
            this.QInvoiceType.EEPAlias = "JBERP";
            this.QInvoiceType.EncodingAfter = null;
            this.QInvoiceType.EncodingBefore = "Windows-1252";
            this.QInvoiceType.EncodingConvert = null;
            this.QInvoiceType.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "CustomerID";
            this.QInvoiceType.KeyFields.Add(keyItem8);
            this.QInvoiceType.MultiSetWhere = false;
            this.QInvoiceType.Name = "QInvoiceType";
            this.QInvoiceType.NotificationAutoEnlist = false;
            this.QInvoiceType.SecExcept = null;
            this.QInvoiceType.SecFieldName = null;
            this.QInvoiceType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.QInvoiceType.SelectPaging = false;
            this.QInvoiceType.SelectTop = 0;
            this.QInvoiceType.SiteControl = false;
            this.QInvoiceType.SiteFieldName = null;
            this.QInvoiceType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "SELECT  SalesTypeID,SalesTypeName,InsGroupID FROM SalesType \r\nOrder By SalesTypeI" +
    "D";
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = "JBERP";
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection1;
            keyItem9.KeyName = "CustomerID";
            this.SalesType.KeyFields.Add(keyItem9);
            this.SalesType.MultiSetWhere = false;
            this.SalesType.Name = "SalesType";
            this.SalesType.NotificationAutoEnlist = false;
            this.SalesType.SecExcept = null;
            this.SalesType.SecFieldName = null;
            this.SalesType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesType.SelectPaging = false;
            this.SalesType.SelectTop = 0;
            this.SalesType.SiteControl = false;
            this.SalesType.SiteFieldName = null;
            this.SalesType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceVoidApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_InvoiceVoidApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QInvoiceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand InvoiceVoidApply;
        private Srvtools.UpdateComponent ucInvoiceVoidApply;
        private Srvtools.InfoCommand View_InvoiceVoidApply;
        private Srvtools.InfoCommand SalesDetails;
        private Srvtools.InfoCommand InvoiceDetails;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand Organization;
        private Srvtools.InfoCommand QInvoiceType;
        private Srvtools.InfoCommand SalesType;
    }
}
