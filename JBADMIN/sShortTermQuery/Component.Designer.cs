namespace sShortTermQuery
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ShowTermQuery = new Srvtools.InfoCommand(this.components);
            this.ucView_ShowTermQuery = new Srvtools.UpdateComponent(this.components);
            this.View_View_ShowTermQuery = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.Company = new Srvtools.InfoCommand(this.components);
            this.PayType = new Srvtools.InfoCommand(this.components);
            this.Vendor = new Srvtools.InfoCommand(this.components);
            this.ShortTermType = new Srvtools.InfoCommand(this.components);
            this.Status = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowTermQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_ShowTermQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTermType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ShowTermQuery
            // 
            this.ShowTermQuery.CacheConnection = false;
            this.ShowTermQuery.CommandText = resources.GetString("ShowTermQuery.CommandText");
            this.ShowTermQuery.CommandTimeout = 30;
            this.ShowTermQuery.CommandType = System.Data.CommandType.Text;
            this.ShowTermQuery.DynamicTableName = false;
            this.ShowTermQuery.EEPAlias = null;
            this.ShowTermQuery.EncodingAfter = null;
            this.ShowTermQuery.EncodingBefore = "Windows-1252";
            this.ShowTermQuery.EncodingConvert = null;
            this.ShowTermQuery.InfoConnection = this.InfoConnection1;
            this.ShowTermQuery.MultiSetWhere = false;
            this.ShowTermQuery.Name = "ShowTermQuery";
            this.ShowTermQuery.NotificationAutoEnlist = false;
            this.ShowTermQuery.SecExcept = null;
            this.ShowTermQuery.SecFieldName = null;
            this.ShowTermQuery.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ShowTermQuery.SelectPaging = false;
            this.ShowTermQuery.SelectTop = 0;
            this.ShowTermQuery.SiteControl = false;
            this.ShowTermQuery.SiteFieldName = null;
            this.ShowTermQuery.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucView_ShowTermQuery
            // 
            this.ucView_ShowTermQuery.AutoTrans = true;
            this.ucView_ShowTermQuery.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ShortTermNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ShortTermDate";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EmployeeID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CompanyID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ShortTermTypeID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ShortTermGist";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ShortTermAmount";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "PayTypeID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "PayTo";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "RequestDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "PlanPayDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Flowflag";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr1);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr2);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr3);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr4);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr5);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr6);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr7);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr8);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr9);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr10);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr11);
            this.ucView_ShowTermQuery.FieldAttrs.Add(fieldAttr12);
            this.ucView_ShowTermQuery.LogInfo = null;
            this.ucView_ShowTermQuery.Name = "ucView_ShowTermQuery";
            this.ucView_ShowTermQuery.RowAffectsCheck = true;
            this.ucView_ShowTermQuery.SelectCmd = this.ShowTermQuery;
            this.ucView_ShowTermQuery.SelectCmdForUpdate = null;
            this.ucView_ShowTermQuery.SendSQLCmd = true;
            this.ucView_ShowTermQuery.ServerModify = true;
            this.ucView_ShowTermQuery.ServerModifyGetMax = false;
            this.ucView_ShowTermQuery.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucView_ShowTermQuery.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucView_ShowTermQuery.UseTranscationScope = false;
            this.ucView_ShowTermQuery.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_View_ShowTermQuery
            // 
            this.View_View_ShowTermQuery.CacheConnection = false;
            this.View_View_ShowTermQuery.CommandText = "SELECT * FROM [View_ShowTermQuery]";
            this.View_View_ShowTermQuery.CommandTimeout = 30;
            this.View_View_ShowTermQuery.CommandType = System.Data.CommandType.Text;
            this.View_View_ShowTermQuery.DynamicTableName = false;
            this.View_View_ShowTermQuery.EEPAlias = null;
            this.View_View_ShowTermQuery.EncodingAfter = null;
            this.View_View_ShowTermQuery.EncodingBefore = "Windows-1252";
            this.View_View_ShowTermQuery.EncodingConvert = null;
            this.View_View_ShowTermQuery.InfoConnection = this.InfoConnection1;
            this.View_View_ShowTermQuery.MultiSetWhere = false;
            this.View_View_ShowTermQuery.Name = "View_View_ShowTermQuery";
            this.View_View_ShowTermQuery.NotificationAutoEnlist = false;
            this.View_View_ShowTermQuery.SecExcept = null;
            this.View_View_ShowTermQuery.SecFieldName = null;
            this.View_View_ShowTermQuery.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_View_ShowTermQuery.SelectPaging = false;
            this.View_View_ShowTermQuery.SelectTop = 0;
            this.View_View_ShowTermQuery.SiteControl = false;
            this.View_View_ShowTermQuery.SiteFieldName = null;
            this.View_View_ShowTermQuery.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = "SELECT * FROM VIEW_EMPLOYEE";
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "EMPLOYEEID";
            this.Employee.KeyFields.Add(keyItem1);
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
            // Company
            // 
            this.Company.CacheConnection = false;
            this.Company.CommandText = "SELECT COMPANYID,COMPANYNAME FROM COMPANY";
            this.Company.CommandTimeout = 30;
            this.Company.CommandType = System.Data.CommandType.Text;
            this.Company.DynamicTableName = false;
            this.Company.EEPAlias = null;
            this.Company.EncodingAfter = null;
            this.Company.EncodingBefore = "Windows-1252";
            this.Company.EncodingConvert = null;
            this.Company.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "EMPLOYEEID";
            this.Company.KeyFields.Add(keyItem2);
            this.Company.MultiSetWhere = false;
            this.Company.Name = "Company";
            this.Company.NotificationAutoEnlist = false;
            this.Company.SecExcept = null;
            this.Company.SecFieldName = null;
            this.Company.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Company.SelectPaging = false;
            this.Company.SelectTop = 0;
            this.Company.SiteControl = false;
            this.Company.SiteFieldName = null;
            this.Company.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PayType
            // 
            this.PayType.CacheConnection = false;
            this.PayType.CommandText = "SELECT PAYTYPEID,PAYTYPENAME FROM PAYTYPE ORDER BY PAYTYPEID";
            this.PayType.CommandTimeout = 30;
            this.PayType.CommandType = System.Data.CommandType.Text;
            this.PayType.DynamicTableName = false;
            this.PayType.EEPAlias = null;
            this.PayType.EncodingAfter = null;
            this.PayType.EncodingBefore = "Windows-1252";
            this.PayType.EncodingConvert = null;
            this.PayType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "EMPLOYEEID";
            this.PayType.KeyFields.Add(keyItem3);
            this.PayType.MultiSetWhere = false;
            this.PayType.Name = "PayType";
            this.PayType.NotificationAutoEnlist = false;
            this.PayType.SecExcept = null;
            this.PayType.SecFieldName = null;
            this.PayType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PayType.SelectPaging = false;
            this.PayType.SelectTop = 0;
            this.PayType.SiteControl = false;
            this.PayType.SiteFieldName = null;
            this.PayType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Vendor
            // 
            this.Vendor.CacheConnection = false;
            this.Vendor.CommandText = "SELECT VENDID,VENDSHORTNAME AS VENDNAME\r\nFROM VENDORS ORDER BY VENDID";
            this.Vendor.CommandTimeout = 30;
            this.Vendor.CommandType = System.Data.CommandType.Text;
            this.Vendor.DynamicTableName = false;
            this.Vendor.EEPAlias = null;
            this.Vendor.EncodingAfter = null;
            this.Vendor.EncodingBefore = "Windows-1252";
            this.Vendor.EncodingConvert = null;
            this.Vendor.InfoConnection = this.InfoConnection1;
            this.Vendor.MultiSetWhere = false;
            this.Vendor.Name = "Vendor";
            this.Vendor.NotificationAutoEnlist = false;
            this.Vendor.SecExcept = null;
            this.Vendor.SecFieldName = null;
            this.Vendor.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Vendor.SelectPaging = false;
            this.Vendor.SelectTop = 0;
            this.Vendor.SiteControl = false;
            this.Vendor.SiteFieldName = null;
            this.Vendor.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ShortTermType
            // 
            this.ShortTermType.CacheConnection = false;
            this.ShortTermType.CommandText = "SELECT * FROM SHORTTERMTYPE";
            this.ShortTermType.CommandTimeout = 30;
            this.ShortTermType.CommandType = System.Data.CommandType.Text;
            this.ShortTermType.DynamicTableName = false;
            this.ShortTermType.EEPAlias = null;
            this.ShortTermType.EncodingAfter = null;
            this.ShortTermType.EncodingBefore = "Windows-1252";
            this.ShortTermType.EncodingConvert = null;
            this.ShortTermType.InfoConnection = this.InfoConnection1;
            this.ShortTermType.MultiSetWhere = false;
            this.ShortTermType.Name = "ShortTermType";
            this.ShortTermType.NotificationAutoEnlist = false;
            this.ShortTermType.SecExcept = null;
            this.ShortTermType.SecFieldName = null;
            this.ShortTermType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ShortTermType.SelectPaging = false;
            this.ShortTermType.SelectTop = 0;
            this.ShortTermType.SiteControl = false;
            this.ShortTermType.SiteFieldName = null;
            this.ShortTermType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Status
            // 
            this.Status.CacheConnection = false;
            this.Status.CommandText = "SELECT 1 AS ID,\r\n               \'流程中\' AS STATUS\r\nUNION \r\nSELECT 2 AS ID,\r\n       " +
    "        \'已結案\' AS STATUS";
            this.Status.CommandTimeout = 30;
            this.Status.CommandType = System.Data.CommandType.Text;
            this.Status.DynamicTableName = false;
            this.Status.EEPAlias = null;
            this.Status.EncodingAfter = null;
            this.Status.EncodingBefore = "Windows-1252";
            this.Status.EncodingConvert = null;
            this.Status.InfoConnection = this.InfoConnection1;
            this.Status.MultiSetWhere = false;
            this.Status.Name = "Status";
            this.Status.NotificationAutoEnlist = false;
            this.Status.SecExcept = null;
            this.Status.SecFieldName = null;
            this.Status.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Status.SelectPaging = false;
            this.Status.SelectTop = 0;
            this.Status.SiteControl = false;
            this.Status.SiteFieldName = null;
            this.Status.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowTermQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_ShowTermQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Company)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vendor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShortTermType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ShowTermQuery;
        private Srvtools.UpdateComponent ucView_ShowTermQuery;
        private Srvtools.InfoCommand View_View_ShowTermQuery;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand Company;
        private Srvtools.InfoCommand PayType;
        private Srvtools.InfoCommand Vendor;
        private Srvtools.InfoCommand ShortTermType;
        private Srvtools.InfoCommand Status;
    }
}
