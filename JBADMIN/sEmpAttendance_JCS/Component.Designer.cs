namespace sEmpAttendance_JCS
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.ucEmployee = new Srvtools.UpdateComponent(this.components);
            this.View_Employee = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Employee)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JCS";
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = "SELECT dbo.[Employee].* FROM dbo.[Employee]";
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "EmpID";
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
            // ucEmployee
            // 
            this.ucEmployee.AutoTrans = true;
            this.ucEmployee.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "EmpID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "NameC";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CardNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "HireDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "QuitDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "IsActive";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "IsAtteAudit";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LastUpdateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "LastUpdateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            this.ucEmployee.FieldAttrs.Add(fieldAttr1);
            this.ucEmployee.FieldAttrs.Add(fieldAttr2);
            this.ucEmployee.FieldAttrs.Add(fieldAttr3);
            this.ucEmployee.FieldAttrs.Add(fieldAttr4);
            this.ucEmployee.FieldAttrs.Add(fieldAttr5);
            this.ucEmployee.FieldAttrs.Add(fieldAttr6);
            this.ucEmployee.FieldAttrs.Add(fieldAttr7);
            this.ucEmployee.FieldAttrs.Add(fieldAttr8);
            this.ucEmployee.FieldAttrs.Add(fieldAttr9);
            this.ucEmployee.FieldAttrs.Add(fieldAttr10);
            this.ucEmployee.FieldAttrs.Add(fieldAttr11);
            this.ucEmployee.LogInfo = null;
            this.ucEmployee.Name = "ucEmployee";
            this.ucEmployee.RowAffectsCheck = true;
            this.ucEmployee.SelectCmd = this.Employee;
            this.ucEmployee.SelectCmdForUpdate = null;
            this.ucEmployee.SendSQLCmd = true;
            this.ucEmployee.ServerModify = true;
            this.ucEmployee.ServerModifyGetMax = false;
            this.ucEmployee.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmployee.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmployee.UseTranscationScope = false;
            this.ucEmployee.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_Employee
            // 
            this.View_Employee.CacheConnection = false;
            this.View_Employee.CommandText = "SELECT * FROM dbo.[Employee]";
            this.View_Employee.CommandTimeout = 30;
            this.View_Employee.CommandType = System.Data.CommandType.Text;
            this.View_Employee.DynamicTableName = false;
            this.View_Employee.EEPAlias = null;
            this.View_Employee.EncodingAfter = null;
            this.View_Employee.EncodingBefore = "Windows-1252";
            this.View_Employee.EncodingConvert = null;
            this.View_Employee.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "EmpID";
            this.View_Employee.KeyFields.Add(keyItem2);
            this.View_Employee.MultiSetWhere = false;
            this.View_Employee.Name = "View_Employee";
            this.View_Employee.NotificationAutoEnlist = false;
            this.View_Employee.SecExcept = null;
            this.View_Employee.SecFieldName = null;
            this.View_Employee.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Employee.SelectPaging = false;
            this.View_Employee.SelectTop = 0;
            this.View_Employee.SiteControl = false;
            this.View_Employee.SiteFieldName = null;
            this.View_Employee.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Employee)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Employee;
        private Srvtools.UpdateComponent ucEmployee;
        private Srvtools.InfoCommand View_Employee;
    }
}
