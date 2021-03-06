namespace sERPSalesApplyQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalesApplyMaster = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesApplyMaster = new Srvtools.UpdateComponent(this.components);
            this.View_ERPSalesApplyMaster = new Srvtools.InfoCommand(this.components);
            this.Sales = new Srvtools.InfoCommand(this.components);
            this.Customers = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.SalesType = new Srvtools.InfoCommand(this.components);
            this.SalesItem = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesApplyMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPSalesApplyMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesItem)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalesApplyMaster
            // 
            this.ERPSalesApplyMaster.CacheConnection = false;
            this.ERPSalesApplyMaster.CommandText = resources.GetString("ERPSalesApplyMaster.CommandText");
            this.ERPSalesApplyMaster.CommandTimeout = 30;
            this.ERPSalesApplyMaster.CommandType = System.Data.CommandType.Text;
            this.ERPSalesApplyMaster.DynamicTableName = false;
            this.ERPSalesApplyMaster.EEPAlias = null;
            this.ERPSalesApplyMaster.EncodingAfter = null;
            this.ERPSalesApplyMaster.EncodingBefore = "Windows-1252";
            this.ERPSalesApplyMaster.EncodingConvert = null;
            this.ERPSalesApplyMaster.InfoConnection = this.InfoConnection1;
            this.ERPSalesApplyMaster.MultiSetWhere = false;
            this.ERPSalesApplyMaster.Name = "ERPSalesApplyMaster";
            this.ERPSalesApplyMaster.NotificationAutoEnlist = false;
            this.ERPSalesApplyMaster.SecExcept = null;
            this.ERPSalesApplyMaster.SecFieldName = null;
            this.ERPSalesApplyMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesApplyMaster.SelectPaging = false;
            this.ERPSalesApplyMaster.SelectTop = 0;
            this.ERPSalesApplyMaster.SiteControl = false;
            this.ERPSalesApplyMaster.SiteFieldName = null;
            this.ERPSalesApplyMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalesApplyMaster
            // 
            this.ucERPSalesApplyMaster.AutoTrans = true;
            this.ucERPSalesApplyMaster.ExceptJoin = false;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "SalesApplyNO";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CustNO";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "Contact";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "ApplyDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "ApplyEmpID";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "SalesID";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "TaxNO";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "SalesOutLine";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "SalesNotes";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "InsGroupID";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "SalesTypeID";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "flowflag";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "CreateBy";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "CreateDate";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr15);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr16);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr17);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr18);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr19);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr20);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr21);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr22);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr23);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr24);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr25);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr26);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr27);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr28);
            this.ucERPSalesApplyMaster.LogInfo = null;
            this.ucERPSalesApplyMaster.Name = "ucERPSalesApplyMaster";
            this.ucERPSalesApplyMaster.RowAffectsCheck = true;
            this.ucERPSalesApplyMaster.SelectCmd = this.ERPSalesApplyMaster;
            this.ucERPSalesApplyMaster.SelectCmdForUpdate = null;
            this.ucERPSalesApplyMaster.SendSQLCmd = true;
            this.ucERPSalesApplyMaster.ServerModify = true;
            this.ucERPSalesApplyMaster.ServerModifyGetMax = false;
            this.ucERPSalesApplyMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalesApplyMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalesApplyMaster.UseTranscationScope = false;
            this.ucERPSalesApplyMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ERPSalesApplyMaster
            // 
            this.View_ERPSalesApplyMaster.CacheConnection = false;
            this.View_ERPSalesApplyMaster.CommandText = "SELECT * FROM dbo.[ERPSalesApplyMaster]";
            this.View_ERPSalesApplyMaster.CommandTimeout = 30;
            this.View_ERPSalesApplyMaster.CommandType = System.Data.CommandType.Text;
            this.View_ERPSalesApplyMaster.DynamicTableName = false;
            this.View_ERPSalesApplyMaster.EEPAlias = null;
            this.View_ERPSalesApplyMaster.EncodingAfter = null;
            this.View_ERPSalesApplyMaster.EncodingBefore = "Windows-1252";
            this.View_ERPSalesApplyMaster.EncodingConvert = null;
            this.View_ERPSalesApplyMaster.InfoConnection = this.InfoConnection1;
            this.View_ERPSalesApplyMaster.MultiSetWhere = false;
            this.View_ERPSalesApplyMaster.Name = "View_ERPSalesApplyMaster";
            this.View_ERPSalesApplyMaster.NotificationAutoEnlist = false;
            this.View_ERPSalesApplyMaster.SecExcept = null;
            this.View_ERPSalesApplyMaster.SecFieldName = null;
            this.View_ERPSalesApplyMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPSalesApplyMaster.SelectPaging = false;
            this.View_ERPSalesApplyMaster.SelectTop = 0;
            this.View_ERPSalesApplyMaster.SiteControl = false;
            this.View_ERPSalesApplyMaster.SiteFieldName = null;
            this.View_ERPSalesApplyMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Sales
            // 
            this.Sales.CacheConnection = false;
            this.Sales.CommandText = "select ERPSalesMan.SalesID,ERPSalesMan.SalesName,ERPSalesMan.SalesEmployeeID from" +
    " ERPSalesMan";
            this.Sales.CommandTimeout = 30;
            this.Sales.CommandType = System.Data.CommandType.Text;
            this.Sales.DynamicTableName = false;
            this.Sales.EEPAlias = null;
            this.Sales.EncodingAfter = null;
            this.Sales.EncodingBefore = "Windows-1252";
            this.Sales.EncodingConvert = null;
            this.Sales.InfoConnection = this.InfoConnection1;
            this.Sales.MultiSetWhere = false;
            this.Sales.Name = "Sales";
            this.Sales.NotificationAutoEnlist = false;
            this.Sales.SecExcept = null;
            this.Sales.SecFieldName = null;
            this.Sales.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Sales.SelectPaging = false;
            this.Sales.SelectTop = 0;
            this.Sales.SiteControl = false;
            this.Sales.SiteFieldName = null;
            this.Sales.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Customers
            // 
            this.Customers.CacheConnection = false;
            this.Customers.CommandText = "SELECT TOP 300 CustNO,\r\nCustShortName,\r\nSalesID,\r\nTaxNO,\r\nContactA \r\nFROM View_ER" +
    "PCustomers";
            this.Customers.CommandTimeout = 30;
            this.Customers.CommandType = System.Data.CommandType.Text;
            this.Customers.DynamicTableName = false;
            this.Customers.EEPAlias = "";
            this.Customers.EncodingAfter = null;
            this.Customers.EncodingBefore = "Windows-1252";
            this.Customers.EncodingConvert = null;
            this.Customers.InfoConnection = this.InfoConnection1;
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
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "select ERPInsGroup.InsGroupID,ERPInsGroup.InsGroupName,ERPInsGroup.InsGroupShortN" +
    "ame from ERPInsGroup";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = null;
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.InfoConnection1;
            this.InsGroup.MultiSetWhere = false;
            this.InsGroup.Name = "InsGroup";
            this.InsGroup.NotificationAutoEnlist = false;
            this.InsGroup.SecExcept = null;
            this.InsGroup.SecFieldName = null;
            this.InsGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InsGroup.SelectPaging = false;
            this.InsGroup.SelectTop = 0;
            this.InsGroup.SiteControl = false;
            this.InsGroup.SiteFieldName = null;
            this.InsGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = "select View_Employee.EmployeeID,\r\n           View_Employee.EmployeeName\r\n        " +
    "   from View_Employee\r\n           where description=\'JB\' ";
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem3);
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
            // SalesType
            // 
            this.SalesType.CacheConnection = false;
            this.SalesType.CommandText = "select ERPSalesType.SalesTypeID,ERPSalesType.SalesTypeName from ERPSalesType";
            this.SalesType.CommandTimeout = 30;
            this.SalesType.CommandType = System.Data.CommandType.Text;
            this.SalesType.DynamicTableName = false;
            this.SalesType.EEPAlias = null;
            this.SalesType.EncodingAfter = null;
            this.SalesType.EncodingBefore = "Windows-1252";
            this.SalesType.EncodingConvert = null;
            this.SalesType.InfoConnection = this.InfoConnection1;
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
            // 
            // SalesItem
            // 
            this.SalesItem.CacheConnection = false;
            this.SalesItem.CommandText = "select ERPSalesItem.SalesItemID,ERPSalesItem.SalesItemName,ERPSalesItem.SalesItem" +
    "Type from ERPSalesItem";
            this.SalesItem.CommandTimeout = 30;
            this.SalesItem.CommandType = System.Data.CommandType.Text;
            this.SalesItem.DynamicTableName = false;
            this.SalesItem.EEPAlias = null;
            this.SalesItem.EncodingAfter = null;
            this.SalesItem.EncodingBefore = "Windows-1252";
            this.SalesItem.EncodingConvert = null;
            this.SalesItem.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesItemID";
            this.SalesItem.KeyFields.Add(keyItem1);
            this.SalesItem.MultiSetWhere = false;
            this.SalesItem.Name = "SalesItem";
            this.SalesItem.NotificationAutoEnlist = false;
            this.SalesItem.SecExcept = null;
            this.SalesItem.SecFieldName = null;
            this.SalesItem.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesItem.SelectPaging = false;
            this.SalesItem.SelectTop = 0;
            this.SalesItem.SiteControl = false;
            this.SalesItem.SiteFieldName = null;
            this.SalesItem.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesApplyMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPSalesApplyMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesItem)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesApplyMaster;
        private Srvtools.UpdateComponent ucERPSalesApplyMaster;
        private Srvtools.InfoCommand View_ERPSalesApplyMaster;
        private Srvtools.InfoCommand Sales;
        private Srvtools.InfoCommand Customers;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand SalesType;
        private Srvtools.InfoCommand SalesItem;
    }
}
