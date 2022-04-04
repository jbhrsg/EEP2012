namespace sERPSalesMan
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
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPSalesMan = new Srvtools.InfoCommand(this.components);
            this.ucERPSalesMan = new Srvtools.UpdateComponent(this.components);
            this.View_ERPSalesMan = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckSalesID";
            service1.NonLogin = false;
            service1.ServiceName = "CheckSalesID";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPSalesMan
            // 
            this.ERPSalesMan.CacheConnection = false;
            this.ERPSalesMan.CommandText = "SELECT dbo.[ERPSalesMan].* FROM dbo.[ERPSalesMan]\r\nORDER BY dbo.[ERPSalesMan].Sal" +
    "esName";
            this.ERPSalesMan.CommandTimeout = 30;
            this.ERPSalesMan.CommandType = System.Data.CommandType.Text;
            this.ERPSalesMan.DynamicTableName = false;
            this.ERPSalesMan.EEPAlias = null;
            this.ERPSalesMan.EncodingAfter = null;
            this.ERPSalesMan.EncodingBefore = "Windows-1252";
            this.ERPSalesMan.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SalesID";
            this.ERPSalesMan.KeyFields.Add(keyItem1);
            this.ERPSalesMan.MultiSetWhere = false;
            this.ERPSalesMan.Name = "ERPSalesMan";
            this.ERPSalesMan.NotificationAutoEnlist = false;
            this.ERPSalesMan.SecExcept = null;
            this.ERPSalesMan.SecFieldName = null;
            this.ERPSalesMan.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPSalesMan.SelectPaging = false;
            this.ERPSalesMan.SelectTop = 0;
            this.ERPSalesMan.SiteControl = false;
            this.ERPSalesMan.SiteFieldName = null;
            this.ERPSalesMan.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPSalesMan
            // 
            this.ucERPSalesMan.AutoTrans = true;
            this.ucERPSalesMan.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalseNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SalesID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "SalesName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SalesEmployeeID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "SalesTypeScope";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "IsMedia";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateBy";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = "_username";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "LastUpdateBy";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr9.DefaultValue = "_username";
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LastUpdateDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr1);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr2);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr3);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr4);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr5);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr6);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr7);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr8);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr9);
            this.ucERPSalesMan.FieldAttrs.Add(fieldAttr10);
            this.ucERPSalesMan.LogInfo = null;
            this.ucERPSalesMan.Name = "ucERPSalesMan";
            this.ucERPSalesMan.RowAffectsCheck = true;
            this.ucERPSalesMan.SelectCmd = this.ERPSalesMan;
            this.ucERPSalesMan.SelectCmdForUpdate = null;
            this.ucERPSalesMan.ServerModify = true;
            this.ucERPSalesMan.ServerModifyGetMax = false;
            this.ucERPSalesMan.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPSalesMan.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPSalesMan.UseTranscationScope = false;
            this.ucERPSalesMan.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucERPSalesMan.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucERPSalesMan_BeforeInsert);
            this.ucERPSalesMan.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucERPSalesMan_BeforeModify);
            // 
            // View_ERPSalesMan
            // 
            this.View_ERPSalesMan.CacheConnection = false;
            this.View_ERPSalesMan.CommandText = "SELECT * FROM dbo.[ERPSalesMan]";
            this.View_ERPSalesMan.CommandTimeout = 30;
            this.View_ERPSalesMan.CommandType = System.Data.CommandType.Text;
            this.View_ERPSalesMan.DynamicTableName = false;
            this.View_ERPSalesMan.EEPAlias = null;
            this.View_ERPSalesMan.EncodingAfter = null;
            this.View_ERPSalesMan.EncodingBefore = "Windows-1252";
            this.View_ERPSalesMan.InfoConnection = this.InfoConnection1;
            this.View_ERPSalesMan.MultiSetWhere = false;
            this.View_ERPSalesMan.Name = "View_ERPSalesMan";
            this.View_ERPSalesMan.NotificationAutoEnlist = false;
            this.View_ERPSalesMan.SecExcept = null;
            this.View_ERPSalesMan.SecFieldName = null;
            this.View_ERPSalesMan.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPSalesMan.SelectPaging = false;
            this.View_ERPSalesMan.SelectTop = 0;
            this.View_ERPSalesMan.SiteControl = false;
            this.View_ERPSalesMan.SiteFieldName = null;
            this.View_ERPSalesMan.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "EMPLOYEEID";
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPSalesMan;
        private Srvtools.UpdateComponent ucERPSalesMan;
        private Srvtools.InfoCommand View_ERPSalesMan;
        private Srvtools.InfoCommand Employee;
    }
}
