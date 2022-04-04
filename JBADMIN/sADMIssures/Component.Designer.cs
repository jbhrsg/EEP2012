namespace sADMIssures
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.AdmIssures = new Srvtools.InfoCommand(this.components);
            this.ucAdmIssures = new Srvtools.UpdateComponent(this.components);
            this.View_AdmIssures = new Srvtools.InfoCommand(this.components);
            this.IssureType = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.autoIssureID = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdmIssures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AdmIssures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssureType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // AdmIssures
            // 
            this.AdmIssures.CacheConnection = false;
            this.AdmIssures.CommandText = resources.GetString("AdmIssures.CommandText");
            this.AdmIssures.CommandTimeout = 30;
            this.AdmIssures.CommandType = System.Data.CommandType.Text;
            this.AdmIssures.DynamicTableName = false;
            this.AdmIssures.EEPAlias = null;
            this.AdmIssures.EncodingAfter = null;
            this.AdmIssures.EncodingBefore = "Windows-1252";
            this.AdmIssures.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "IssureID";
            this.AdmIssures.KeyFields.Add(keyItem1);
            this.AdmIssures.MultiSetWhere = false;
            this.AdmIssures.Name = "AdmIssures";
            this.AdmIssures.NotificationAutoEnlist = false;
            this.AdmIssures.SecExcept = null;
            this.AdmIssures.SecFieldName = null;
            this.AdmIssures.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AdmIssures.SelectPaging = false;
            this.AdmIssures.SelectTop = 0;
            this.AdmIssures.SiteControl = false;
            this.AdmIssures.SiteFieldName = null;
            this.AdmIssures.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucAdmIssures
            // 
            this.ucAdmIssures.AutoTrans = true;
            this.ucAdmIssures.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "IssureID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "IssureType";
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
            fieldAttr4.DataField = "ApplyDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "IssureDescription";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "RequireDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AssumeEmpID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "EstimateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "FinishDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Flowflag";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr11.DefaultValue = "_username";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr1);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr2);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr3);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr4);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr5);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr6);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr7);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr8);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr9);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr10);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr11);
            this.ucAdmIssures.FieldAttrs.Add(fieldAttr12);
            this.ucAdmIssures.LogInfo = null;
            this.ucAdmIssures.Name = "ucAdmIssures";
            this.ucAdmIssures.RowAffectsCheck = true;
            this.ucAdmIssures.SelectCmd = this.AdmIssures;
            this.ucAdmIssures.SelectCmdForUpdate = null;
            this.ucAdmIssures.ServerModify = true;
            this.ucAdmIssures.ServerModifyGetMax = false;
            this.ucAdmIssures.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucAdmIssures.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucAdmIssures.UseTranscationScope = false;
            this.ucAdmIssures.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucAdmIssures.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucAdmIssures_BeforeInsert);
            this.ucAdmIssures.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucAdmIssures_BeforeModify);
            // 
            // View_AdmIssures
            // 
            this.View_AdmIssures.CacheConnection = false;
            this.View_AdmIssures.CommandText = "SELECT * FROM dbo.[AdmIssures]";
            this.View_AdmIssures.CommandTimeout = 30;
            this.View_AdmIssures.CommandType = System.Data.CommandType.Text;
            this.View_AdmIssures.DynamicTableName = false;
            this.View_AdmIssures.EEPAlias = null;
            this.View_AdmIssures.EncodingAfter = null;
            this.View_AdmIssures.EncodingBefore = "Windows-1252";
            this.View_AdmIssures.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "IssureID";
            this.View_AdmIssures.KeyFields.Add(keyItem2);
            this.View_AdmIssures.MultiSetWhere = false;
            this.View_AdmIssures.Name = "View_AdmIssures";
            this.View_AdmIssures.NotificationAutoEnlist = false;
            this.View_AdmIssures.SecExcept = null;
            this.View_AdmIssures.SecFieldName = null;
            this.View_AdmIssures.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_AdmIssures.SelectPaging = false;
            this.View_AdmIssures.SelectTop = 0;
            this.View_AdmIssures.SiteControl = false;
            this.View_AdmIssures.SiteFieldName = null;
            this.View_AdmIssures.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // IssureType
            // 
            this.IssureType.CacheConnection = false;
            this.IssureType.CommandText = "select IssureType.IssureTypeID,\r\n           IssureType.IssureTypeName\r\nfrom Issur" +
    "eType\r\norder by IssureType.IssureTypeName";
            this.IssureType.CommandTimeout = 30;
            this.IssureType.CommandType = System.Data.CommandType.Text;
            this.IssureType.DynamicTableName = false;
            this.IssureType.EEPAlias = null;
            this.IssureType.EncodingAfter = null;
            this.IssureType.EncodingBefore = "Windows-1252";
            this.IssureType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "IssureTypeID";
            this.IssureType.KeyFields.Add(keyItem3);
            this.IssureType.MultiSetWhere = false;
            this.IssureType.Name = "IssureType";
            this.IssureType.NotificationAutoEnlist = false;
            this.IssureType.SecExcept = null;
            this.IssureType.SecFieldName = null;
            this.IssureType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.IssureType.SelectPaging = false;
            this.IssureType.SelectTop = 0;
            this.IssureType.SiteControl = false;
            this.IssureType.SiteFieldName = null;
            this.IssureType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = "select Employee.EmployeeID,Employee.EmployeeName \r\nfrom Employee\r\norder by  Emplo" +
    "yee.EmployeeID";
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.InfoConnection = this.InfoConnection1;
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
            // autoIssureID
            // 
            this.autoIssureID.Active = true;
            this.autoIssureID.AutoNoID = "IssureID";
            this.autoIssureID.Description = null;
            this.autoIssureID.GetFixed = "";
            this.autoIssureID.isNumFill = false;
            this.autoIssureID.Name = "autoIssureID";
            this.autoIssureID.Number = null;
            this.autoIssureID.NumDig = 3;
            this.autoIssureID.OldVersion = false;
            this.autoIssureID.OverFlow = true;
            this.autoIssureID.StartValue = 1;
            this.autoIssureID.Step = 1;
            this.autoIssureID.TargetColumn = "IssureID";
            this.autoIssureID.UpdateComp = this.ucAdmIssures;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdmIssures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AdmIssures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IssureType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand AdmIssures;
        private Srvtools.UpdateComponent ucAdmIssures;
        private Srvtools.InfoCommand View_AdmIssures;
        private Srvtools.InfoCommand IssureType;
        private Srvtools.InfoCommand Employee;
        private Srvtools.AutoNumber autoIssureID;
    }
}
