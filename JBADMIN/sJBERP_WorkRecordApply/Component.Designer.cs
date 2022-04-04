namespace sJBERP_WorkRecordApply
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
            Srvtools.Service service10 = new Srvtools.Service();
            Srvtools.Service service11 = new Srvtools.Service();
            Srvtools.Service service12 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem10 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem7 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem8 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem8 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.WRMaster = new Srvtools.InfoCommand(this.components);
            this.ucWRMaster = new Srvtools.UpdateComponent(this.components);
            this.WRDetail = new Srvtools.InfoCommand(this.components);
            this.ucWRDetail = new Srvtools.UpdateComponent(this.components);
            this.idWRMaster_WRDetail = new Srvtools.InfoDataSource(this.components);
            this.View_WRMaster = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            this.OrganizationParent = new Srvtools.InfoCommand(this.components);
            this.ORG_NO_Query = new Srvtools.InfoCommand(this.components);
            this.USERID_Query = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WRMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WRDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_WRMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrganizationParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ORG_NO_Query)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERID_Query)).BeginInit();
            // 
            // serviceManager1
            // 
            service10.DelegateName = "FlowStartUp";
            service10.NonLogin = false;
            service10.ServiceName = "FlowStartUp";
            service11.DelegateName = "GetUserOrg";
            service11.NonLogin = false;
            service11.ServiceName = "GetUserOrg";
            service12.DelegateName = "GetNextDayPlan";
            service12.NonLogin = false;
            service12.ServiceName = "GetNextDayPlan";
            this.serviceManager1.ServiceCollection.Add(service10);
            this.serviceManager1.ServiceCollection.Add(service11);
            this.serviceManager1.ServiceCollection.Add(service12);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // WRMaster
            // 
            this.WRMaster.CacheConnection = false;
            this.WRMaster.CommandText = resources.GetString("WRMaster.CommandText");
            this.WRMaster.CommandTimeout = 30;
            this.WRMaster.CommandType = System.Data.CommandType.Text;
            this.WRMaster.DynamicTableName = false;
            this.WRMaster.EEPAlias = null;
            this.WRMaster.EncodingAfter = null;
            this.WRMaster.EncodingBefore = "Windows-1252";
            this.WRMaster.EncodingConvert = null;
            this.WRMaster.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "WRNO";
            this.WRMaster.KeyFields.Add(keyItem6);
            this.WRMaster.MultiSetWhere = false;
            this.WRMaster.Name = "WRMaster";
            this.WRMaster.NotificationAutoEnlist = false;
            this.WRMaster.SecExcept = null;
            this.WRMaster.SecFieldName = null;
            this.WRMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.WRMaster.SelectPaging = false;
            this.WRMaster.SelectTop = 0;
            this.WRMaster.SiteControl = false;
            this.WRMaster.SiteFieldName = null;
            this.WRMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucWRMaster
            // 
            this.ucWRMaster.AutoTrans = true;
            this.ucWRMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = true;
            fieldAttr1.DataField = "WRNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "USERID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "WorkDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ORG_NO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ORG_NOParent";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "File1";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "File2";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "File3";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            this.ucWRMaster.FieldAttrs.Add(fieldAttr1);
            this.ucWRMaster.FieldAttrs.Add(fieldAttr2);
            this.ucWRMaster.FieldAttrs.Add(fieldAttr3);
            this.ucWRMaster.FieldAttrs.Add(fieldAttr4);
            this.ucWRMaster.FieldAttrs.Add(fieldAttr5);
            this.ucWRMaster.FieldAttrs.Add(fieldAttr19);
            this.ucWRMaster.FieldAttrs.Add(fieldAttr20);
            this.ucWRMaster.FieldAttrs.Add(fieldAttr21);
            this.ucWRMaster.LogInfo = null;
            this.ucWRMaster.Name = "ucWRMaster";
            this.ucWRMaster.RowAffectsCheck = true;
            this.ucWRMaster.SelectCmd = this.WRMaster;
            this.ucWRMaster.SelectCmdForUpdate = null;
            this.ucWRMaster.SendSQLCmd = true;
            this.ucWRMaster.ServerModify = true;
            this.ucWRMaster.ServerModifyGetMax = false;
            this.ucWRMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucWRMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucWRMaster.UseTranscationScope = false;
            this.ucWRMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // WRDetail
            // 
            this.WRDetail.CacheConnection = false;
            this.WRDetail.CommandText = "SELECT dbo.[WRDetail].* FROM dbo.[WRDetail]";
            this.WRDetail.CommandTimeout = 30;
            this.WRDetail.CommandType = System.Data.CommandType.Text;
            this.WRDetail.DynamicTableName = false;
            this.WRDetail.EEPAlias = null;
            this.WRDetail.EncodingAfter = null;
            this.WRDetail.EncodingBefore = "Windows-1252";
            this.WRDetail.EncodingConvert = null;
            this.WRDetail.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "WRNO";
            keyItem10.KeyName = "ItemNO";
            this.WRDetail.KeyFields.Add(keyItem7);
            this.WRDetail.KeyFields.Add(keyItem10);
            this.WRDetail.MultiSetWhere = false;
            this.WRDetail.Name = "WRDetail";
            this.WRDetail.NotificationAutoEnlist = false;
            this.WRDetail.SecExcept = null;
            this.WRDetail.SecFieldName = null;
            this.WRDetail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.WRDetail.SelectPaging = false;
            this.WRDetail.SelectTop = 0;
            this.WRDetail.SiteControl = false;
            this.WRDetail.SiteFieldName = null;
            this.WRDetail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucWRDetail
            // 
            this.ucWRDetail.AutoTrans = true;
            this.ucWRDetail.ExceptJoin = false;
            fieldAttr6.CheckNull = true;
            fieldAttr6.DataField = "WRNO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = true;
            fieldAttr7.DataField = "ItemNO";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "BeginTime";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "EndTime";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "RecordText";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucWRDetail.FieldAttrs.Add(fieldAttr6);
            this.ucWRDetail.FieldAttrs.Add(fieldAttr7);
            this.ucWRDetail.FieldAttrs.Add(fieldAttr8);
            this.ucWRDetail.FieldAttrs.Add(fieldAttr9);
            this.ucWRDetail.FieldAttrs.Add(fieldAttr10);
            this.ucWRDetail.LogInfo = null;
            this.ucWRDetail.Name = "ucWRDetail";
            this.ucWRDetail.RowAffectsCheck = true;
            this.ucWRDetail.SelectCmd = this.WRDetail;
            this.ucWRDetail.SelectCmdForUpdate = null;
            this.ucWRDetail.SendSQLCmd = true;
            this.ucWRDetail.ServerModify = true;
            this.ucWRDetail.ServerModifyGetMax = false;
            this.ucWRDetail.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucWRDetail.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucWRDetail.UseTranscationScope = false;
            this.ucWRDetail.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idWRMaster_WRDetail
            // 
            this.idWRMaster_WRDetail.Detail = this.WRDetail;
            columnItem7.FieldName = "WRNO";
            this.idWRMaster_WRDetail.DetailColumns.Add(columnItem7);
            this.idWRMaster_WRDetail.DynamicTableName = false;
            this.idWRMaster_WRDetail.Master = this.WRMaster;
            columnItem8.FieldName = "WRNO";
            this.idWRMaster_WRDetail.MasterColumns.Add(columnItem8);
            // 
            // View_WRMaster
            // 
            this.View_WRMaster.CacheConnection = false;
            this.View_WRMaster.CommandText = "SELECT * FROM dbo.[WRMaster]";
            this.View_WRMaster.CommandTimeout = 30;
            this.View_WRMaster.CommandType = System.Data.CommandType.Text;
            this.View_WRMaster.DynamicTableName = false;
            this.View_WRMaster.EEPAlias = null;
            this.View_WRMaster.EncodingAfter = null;
            this.View_WRMaster.EncodingBefore = "Windows-1252";
            this.View_WRMaster.EncodingConvert = null;
            this.View_WRMaster.InfoConnection = this.InfoConnection1;
            keyItem8.KeyName = "WRNO";
            this.View_WRMaster.KeyFields.Add(keyItem8);
            this.View_WRMaster.MultiSetWhere = false;
            this.View_WRMaster.Name = "View_WRMaster";
            this.View_WRMaster.NotificationAutoEnlist = false;
            this.View_WRMaster.SecExcept = null;
            this.View_WRMaster.SecFieldName = null;
            this.View_WRMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_WRMaster.SelectPaging = false;
            this.View_WRMaster.SelectTop = 0;
            this.View_WRMaster.SiteControl = false;
            this.View_WRMaster.SiteFieldName = null;
            this.View_WRMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "WRNOAutoNumber";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "autoNumber1GetFixed()";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 6;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "WRNO";
            this.autoNumber1.UpdateComp = this.ucWRMaster;
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
            keyItem1.KeyName = "EmployeeID";
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
            // Organization
            // 
            this.Organization.CacheConnection = false;
            this.Organization.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nORDER BY OR" +
    "G_NO\r\n\r\n--WHERE (Upper_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR O" +
    "RG_NO=\'99999\')";
            this.Organization.CommandTimeout = 30;
            this.Organization.CommandType = System.Data.CommandType.Text;
            this.Organization.DynamicTableName = false;
            this.Organization.EEPAlias = null;
            this.Organization.EncodingAfter = null;
            this.Organization.EncodingBefore = "Windows-1252";
            this.Organization.EncodingConvert = null;
            this.Organization.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem2);
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
            // OrganizationParent
            // 
            this.OrganizationParent.CacheConnection = false;
            this.OrganizationParent.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nORDER BY OR" +
    "G_NO";
            this.OrganizationParent.CommandTimeout = 30;
            this.OrganizationParent.CommandType = System.Data.CommandType.Text;
            this.OrganizationParent.DynamicTableName = false;
            this.OrganizationParent.EEPAlias = null;
            this.OrganizationParent.EncodingAfter = null;
            this.OrganizationParent.EncodingBefore = "Windows-1252";
            this.OrganizationParent.EncodingConvert = null;
            this.OrganizationParent.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ORG_NO";
            this.OrganizationParent.KeyFields.Add(keyItem3);
            this.OrganizationParent.MultiSetWhere = false;
            this.OrganizationParent.Name = "OrganizationParent";
            this.OrganizationParent.NotificationAutoEnlist = false;
            this.OrganizationParent.SecExcept = null;
            this.OrganizationParent.SecFieldName = null;
            this.OrganizationParent.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.OrganizationParent.SelectPaging = false;
            this.OrganizationParent.SelectTop = 0;
            this.OrganizationParent.SiteControl = false;
            this.OrganizationParent.SiteFieldName = null;
            this.OrganizationParent.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ORG_NO_Query
            // 
            this.ORG_NO_Query.CacheConnection = false;
            this.ORG_NO_Query.CommandText = resources.GetString("ORG_NO_Query.CommandText");
            this.ORG_NO_Query.CommandTimeout = 30;
            this.ORG_NO_Query.CommandType = System.Data.CommandType.Text;
            this.ORG_NO_Query.DynamicTableName = false;
            this.ORG_NO_Query.EEPAlias = "";
            this.ORG_NO_Query.EncodingAfter = null;
            this.ORG_NO_Query.EncodingBefore = "Windows-1252";
            this.ORG_NO_Query.EncodingConvert = null;
            this.ORG_NO_Query.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ORG_NO";
            this.ORG_NO_Query.KeyFields.Add(keyItem4);
            this.ORG_NO_Query.MultiSetWhere = true;
            this.ORG_NO_Query.Name = "ORG_NO_Query";
            this.ORG_NO_Query.NotificationAutoEnlist = false;
            this.ORG_NO_Query.SecExcept = null;
            this.ORG_NO_Query.SecFieldName = null;
            this.ORG_NO_Query.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ORG_NO_Query.SelectPaging = false;
            this.ORG_NO_Query.SelectTop = 0;
            this.ORG_NO_Query.SiteControl = false;
            this.ORG_NO_Query.SiteFieldName = null;
            this.ORG_NO_Query.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // USERID_Query
            // 
            this.USERID_Query.CacheConnection = false;
            this.USERID_Query.CommandText = resources.GetString("USERID_Query.CommandText");
            this.USERID_Query.CommandTimeout = 30;
            this.USERID_Query.CommandType = System.Data.CommandType.Text;
            this.USERID_Query.DynamicTableName = false;
            this.USERID_Query.EEPAlias = null;
            this.USERID_Query.EncodingAfter = null;
            this.USERID_Query.EncodingBefore = "Windows-1252";
            this.USERID_Query.EncodingConvert = null;
            this.USERID_Query.InfoConnection = this.InfoConnection1;
            this.USERID_Query.MultiSetWhere = true;
            this.USERID_Query.Name = "USERID_Query";
            this.USERID_Query.NotificationAutoEnlist = false;
            this.USERID_Query.SecExcept = null;
            this.USERID_Query.SecFieldName = null;
            this.USERID_Query.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.USERID_Query.SelectPaging = false;
            this.USERID_Query.SelectTop = 0;
            this.USERID_Query.SiteControl = false;
            this.USERID_Query.SiteFieldName = null;
            this.USERID_Query.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WRMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WRDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_WRMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrganizationParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ORG_NO_Query)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERID_Query)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand WRMaster;
        private Srvtools.UpdateComponent ucWRMaster;
        private Srvtools.InfoCommand WRDetail;
        private Srvtools.UpdateComponent ucWRDetail;
        private Srvtools.InfoDataSource idWRMaster_WRDetail;
        private Srvtools.InfoCommand View_WRMaster;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand Organization;
        private Srvtools.InfoCommand OrganizationParent;
        private Srvtools.InfoCommand ORG_NO_Query;
        private Srvtools.InfoCommand USERID_Query;
    }
}
