namespace sERPDispatchArea
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPDispatchAreaID = new Srvtools.InfoCommand(this.components);
            this.ucERPDispatchAreaID = new Srvtools.UpdateComponent(this.components);
            this.View_ERPDispatchAreaID = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDispatchAreaID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPDispatchAreaID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPDispatchAreaID
            // 
            this.ERPDispatchAreaID.CacheConnection = false;
            this.ERPDispatchAreaID.CommandText = "SELECT dbo.[ERPDispatchAreaID].* FROM dbo.[ERPDispatchAreaID]";
            this.ERPDispatchAreaID.CommandTimeout = 30;
            this.ERPDispatchAreaID.CommandType = System.Data.CommandType.Text;
            this.ERPDispatchAreaID.DynamicTableName = false;
            this.ERPDispatchAreaID.EEPAlias = null;
            this.ERPDispatchAreaID.EncodingAfter = null;
            this.ERPDispatchAreaID.EncodingBefore = "Windows-1252";
            this.ERPDispatchAreaID.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "DispatchAreaNO";
            this.ERPDispatchAreaID.KeyFields.Add(keyItem1);
            this.ERPDispatchAreaID.MultiSetWhere = false;
            this.ERPDispatchAreaID.Name = "ERPDispatchAreaID";
            this.ERPDispatchAreaID.NotificationAutoEnlist = false;
            this.ERPDispatchAreaID.SecExcept = null;
            this.ERPDispatchAreaID.SecFieldName = null;
            this.ERPDispatchAreaID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPDispatchAreaID.SelectPaging = false;
            this.ERPDispatchAreaID.SelectTop = 0;
            this.ERPDispatchAreaID.SiteControl = false;
            this.ERPDispatchAreaID.SiteFieldName = null;
            this.ERPDispatchAreaID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPDispatchAreaID
            // 
            this.ucERPDispatchAreaID.AutoTrans = true;
            this.ucERPDispatchAreaID.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "DispatchAreaNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "DispatchAreaID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "DispatchAreaName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "DispatchAreaManager";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr5.DefaultValue = "_username";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateBy";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr7.DefaultValue = "_username";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucERPDispatchAreaID.FieldAttrs.Add(fieldAttr1);
            this.ucERPDispatchAreaID.FieldAttrs.Add(fieldAttr2);
            this.ucERPDispatchAreaID.FieldAttrs.Add(fieldAttr3);
            this.ucERPDispatchAreaID.FieldAttrs.Add(fieldAttr4);
            this.ucERPDispatchAreaID.FieldAttrs.Add(fieldAttr5);
            this.ucERPDispatchAreaID.FieldAttrs.Add(fieldAttr6);
            this.ucERPDispatchAreaID.FieldAttrs.Add(fieldAttr7);
            this.ucERPDispatchAreaID.FieldAttrs.Add(fieldAttr8);
            this.ucERPDispatchAreaID.LogInfo = null;
            this.ucERPDispatchAreaID.Name = "ucERPDispatchAreaID";
            this.ucERPDispatchAreaID.RowAffectsCheck = true;
            this.ucERPDispatchAreaID.SelectCmd = this.ERPDispatchAreaID;
            this.ucERPDispatchAreaID.SelectCmdForUpdate = null;
            this.ucERPDispatchAreaID.ServerModify = true;
            this.ucERPDispatchAreaID.ServerModifyGetMax = false;
            this.ucERPDispatchAreaID.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPDispatchAreaID.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPDispatchAreaID.UseTranscationScope = false;
            this.ucERPDispatchAreaID.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucERPDispatchAreaID.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucERPDispatchAreaID_BeforeInsert);
            this.ucERPDispatchAreaID.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucERPDispatchAreaID_BeforeModify);
            // 
            // View_ERPDispatchAreaID
            // 
            this.View_ERPDispatchAreaID.CacheConnection = false;
            this.View_ERPDispatchAreaID.CommandText = "SELECT * FROM dbo.[ERPDispatchAreaID]";
            this.View_ERPDispatchAreaID.CommandTimeout = 30;
            this.View_ERPDispatchAreaID.CommandType = System.Data.CommandType.Text;
            this.View_ERPDispatchAreaID.DynamicTableName = false;
            this.View_ERPDispatchAreaID.EEPAlias = null;
            this.View_ERPDispatchAreaID.EncodingAfter = null;
            this.View_ERPDispatchAreaID.EncodingBefore = "Windows-1252";
            this.View_ERPDispatchAreaID.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DispatchAreaNO";
            this.View_ERPDispatchAreaID.KeyFields.Add(keyItem2);
            this.View_ERPDispatchAreaID.MultiSetWhere = false;
            this.View_ERPDispatchAreaID.Name = "View_ERPDispatchAreaID";
            this.View_ERPDispatchAreaID.NotificationAutoEnlist = false;
            this.View_ERPDispatchAreaID.SecExcept = null;
            this.View_ERPDispatchAreaID.SecFieldName = null;
            this.View_ERPDispatchAreaID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPDispatchAreaID.SelectPaging = false;
            this.View_ERPDispatchAreaID.SelectTop = 0;
            this.View_ERPDispatchAreaID.SiteControl = false;
            this.View_ERPDispatchAreaID.SiteFieldName = null;
            this.View_ERPDispatchAreaID.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDispatchAreaID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPDispatchAreaID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPDispatchAreaID;
        private Srvtools.UpdateComponent ucERPDispatchAreaID;
        private Srvtools.InfoCommand View_ERPDispatchAreaID;
        private Srvtools.InfoCommand Employee;
    }
}
