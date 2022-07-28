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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
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
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalesApplyNO";
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
            fieldAttr3.DataField = "Contact";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ApplyDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ApplyEmpID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SalesID";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "TaxNO";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "SalesOutLine";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "SalesNotes";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "InsGroupID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "SalesTypeID";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "flowflag";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr1);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr2);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr3);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr4);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr5);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr6);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr7);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr8);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr9);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr10);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr11);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr12);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr13);
            this.ucERPSalesApplyMaster.FieldAttrs.Add(fieldAttr14);
            this.ucERPSalesApplyMaster.LogInfo = null;
            this.ucERPSalesApplyMaster.Name = "ucERPSalesApplyMaster";
            this.ucERPSalesApplyMaster.RowAffectsCheck = true;
            this.ucERPSalesApplyMaster.SelectCmd = this.ERPSalesApplyMaster;
            this.ucERPSalesApplyMaster.SelectCmdForUpdate = null;
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
            this.Customers.CommandText = "SELECT TOP 300\r\nERPCustomers.CustNO,\r\nERPCustomers.CustShortName,\r\nERPCustomers.S" +
    "alesID,\r\nERPCustomers.TaxNO,\r\nERPCustomers.ContactA \r\nFROM JBADMIN.dbo.[ERPCusto" +
    "mers]";
            this.Customers.CommandTimeout = 30;
            this.Customers.CommandType = System.Data.CommandType.Text;
            this.Customers.DynamicTableName = false;
            this.Customers.EEPAlias = "JBDBNJB";
            this.Customers.EncodingAfter = null;
            this.Customers.EncodingBefore = "Windows-1252";
            this.Customers.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CustNO";
            this.Customers.KeyFields.Add(keyItem1);
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
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "EmployeeID";
            this.Employee.KeyFields.Add(keyItem2);
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
            this.SalesItem.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "SalesItemID";
            this.SalesItem.KeyFields.Add(keyItem3);
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
