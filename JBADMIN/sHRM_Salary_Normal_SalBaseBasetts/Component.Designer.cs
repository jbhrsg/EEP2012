namespace _HRM_Salary_Normal_SalBaseBasetts
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn1 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn2 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn3 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn4 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn5 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn6 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn7 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn8 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn9 = new Srvtools.SrcFieldNameColumn();
            Srvtools.SrcFieldNameColumn srcFieldNameColumn10 = new Srvtools.SrcFieldNameColumn();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.TheServiceManager = new Srvtools.ServiceManager(this.components);
            this.TheInfoConnection = new Srvtools.InfoConnection(this.components);
            this.cb_HRM_BASE_BASE = new Srvtools.InfoCommand(this.components);
            this.cb_HRM_SALARY_SALCODE_SalaryApproval = new Srvtools.InfoCommand(this.components);
            this.HRM_SALARY_SALBASE_BASETTS = new Srvtools.InfoCommand(this.components);
            this.ucHRM_SALARY_SALBASE_BASETTS = new Srvtools.UpdateComponent(this.components);
            this.log_HRM_SALARY_SALBASE_BASETTS = new Srvtools.LogInfo(this.components);
            this.HRM_SALARY_SALBASE_BASETTS_LOG = new Srvtools.InfoCommand(this.components);
            this.cb_HRM_DEPT = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TheInfoConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_HRM_BASE_BASE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_HRM_SALARY_SALCODE_SalaryApproval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_SALARY_SALBASE_BASETTS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_SALARY_SALBASE_BASETTS_LOG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_HRM_DEPT)).BeginInit();
            // 
            // TheServiceManager
            // 
            service1.DelegateName = "DataValidate";
            service1.NonLogin = false;
            service1.ServiceName = "DataValidate";
            service2.DelegateName = "GetOldSetting";
            service2.NonLogin = false;
            service2.ServiceName = "GetOldSetting";
            service3.DelegateName = "ExcelFileImport";
            service3.NonLogin = false;
            service3.ServiceName = "ExcelFileImport";
            this.TheServiceManager.ServiceCollection.Add(service1);
            this.TheServiceManager.ServiceCollection.Add(service2);
            this.TheServiceManager.ServiceCollection.Add(service3);
            // 
            // TheInfoConnection
            // 
            this.TheInfoConnection.EEPAlias = "JBHRIS";
            // 
            // cb_HRM_BASE_BASE
            // 
            this.cb_HRM_BASE_BASE.CacheConnection = false;
            this.cb_HRM_BASE_BASE.CommandText = "Select\t[EMPLOYEE_ID],\r\n\t\t[EMPLOYEE_CODE],\r\n\t\t[NAME_C],\r\n\t\t[NAME_E]\r\nFrom\t[dbo].[H" +
    "RM_BASE_BASE]";
            this.cb_HRM_BASE_BASE.CommandTimeout = 30;
            this.cb_HRM_BASE_BASE.CommandType = System.Data.CommandType.Text;
            this.cb_HRM_BASE_BASE.DynamicTableName = false;
            this.cb_HRM_BASE_BASE.EEPAlias = null;
            this.cb_HRM_BASE_BASE.EncodingAfter = null;
            this.cb_HRM_BASE_BASE.EncodingBefore = "Windows-1252";            
            this.cb_HRM_BASE_BASE.InfoConnection = this.TheInfoConnection;
            keyItem1.KeyName = "EMPLOYEE_ID";
            this.cb_HRM_BASE_BASE.KeyFields.Add(keyItem1);
            this.cb_HRM_BASE_BASE.MultiSetWhere = false;
            this.cb_HRM_BASE_BASE.Name = "cb_HRM_BASE_BASE";
            this.cb_HRM_BASE_BASE.NotificationAutoEnlist = false;
            this.cb_HRM_BASE_BASE.SecExcept = null;
            this.cb_HRM_BASE_BASE.SecFieldName = null;
            this.cb_HRM_BASE_BASE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.cb_HRM_BASE_BASE.SelectPaging = false;
            this.cb_HRM_BASE_BASE.SelectTop = 0;
            this.cb_HRM_BASE_BASE.SiteControl = false;
            this.cb_HRM_BASE_BASE.SiteFieldName = null;
            this.cb_HRM_BASE_BASE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // cb_HRM_SALARY_SALCODE_SalaryApproval
            // 
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.CacheConnection = false;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.CommandText = resources.GetString("cb_HRM_SALARY_SALCODE_SalaryApproval.CommandText");
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.CommandTimeout = 30;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.CommandType = System.Data.CommandType.Text;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.DynamicTableName = false;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.EEPAlias = null;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.EncodingAfter = null;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.EncodingBefore = "Windows-1252";            
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.InfoConnection = this.TheInfoConnection;
            keyItem2.KeyName = "SALARY_ID";
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.KeyFields.Add(keyItem2);
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.MultiSetWhere = false;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.Name = "cb_HRM_SALARY_SALCODE_SalaryApproval";
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.NotificationAutoEnlist = false;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.SecExcept = null;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.SecFieldName = null;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.SelectPaging = false;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.SelectTop = 0;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.SiteControl = false;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.SiteFieldName = null;
            this.cb_HRM_SALARY_SALCODE_SalaryApproval.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HRM_SALARY_SALBASE_BASETTS
            // 
            this.HRM_SALARY_SALBASE_BASETTS.CacheConnection = false;
            this.HRM_SALARY_SALBASE_BASETTS.CommandText = resources.GetString("HRM_SALARY_SALBASE_BASETTS.CommandText");
            this.HRM_SALARY_SALBASE_BASETTS.CommandTimeout = 30;
            this.HRM_SALARY_SALBASE_BASETTS.CommandType = System.Data.CommandType.Text;
            this.HRM_SALARY_SALBASE_BASETTS.DynamicTableName = false;
            this.HRM_SALARY_SALBASE_BASETTS.EEPAlias = null;
            this.HRM_SALARY_SALBASE_BASETTS.EncodingAfter = null;
            this.HRM_SALARY_SALBASE_BASETTS.EncodingBefore = "Windows-1252";            
            this.HRM_SALARY_SALBASE_BASETTS.InfoConnection = this.TheInfoConnection;
            keyItem3.KeyName = "SALBASE_BASETTS_ID";
            this.HRM_SALARY_SALBASE_BASETTS.KeyFields.Add(keyItem3);
            this.HRM_SALARY_SALBASE_BASETTS.MultiSetWhere = false;
            this.HRM_SALARY_SALBASE_BASETTS.Name = "HRM_SALARY_SALBASE_BASETTS";
            this.HRM_SALARY_SALBASE_BASETTS.NotificationAutoEnlist = false;
            this.HRM_SALARY_SALBASE_BASETTS.SecExcept = null;
            this.HRM_SALARY_SALBASE_BASETTS.SecFieldName = null;
            this.HRM_SALARY_SALBASE_BASETTS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRM_SALARY_SALBASE_BASETTS.SelectPaging = false;
            this.HRM_SALARY_SALBASE_BASETTS.SelectTop = 0;
            this.HRM_SALARY_SALBASE_BASETTS.SiteControl = false;
            this.HRM_SALARY_SALBASE_BASETTS.SiteFieldName = null;
            this.HRM_SALARY_SALBASE_BASETTS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHRM_SALARY_SALBASE_BASETTS
            // 
            this.ucHRM_SALARY_SALBASE_BASETTS.AutoTrans = true;
            this.ucHRM_SALARY_SALBASE_BASETTS.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CREATE_DATE";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = "_sysdate";
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CREATE_MAN";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = "_username";
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "UPDATE_DATE";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr3.DefaultValue = "_sysdate";
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "UPDATE_MAN";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr4.DefaultValue = "_username";
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucHRM_SALARY_SALBASE_BASETTS.FieldAttrs.Add(fieldAttr1);
            this.ucHRM_SALARY_SALBASE_BASETTS.FieldAttrs.Add(fieldAttr2);
            this.ucHRM_SALARY_SALBASE_BASETTS.FieldAttrs.Add(fieldAttr3);
            this.ucHRM_SALARY_SALBASE_BASETTS.FieldAttrs.Add(fieldAttr4);
            this.ucHRM_SALARY_SALBASE_BASETTS.LogInfo = null;
            this.ucHRM_SALARY_SALBASE_BASETTS.Name = "ucHRM_SALARY_SALBASE_BASETTS";
            this.ucHRM_SALARY_SALBASE_BASETTS.RowAffectsCheck = true;
            this.ucHRM_SALARY_SALBASE_BASETTS.SelectCmd = this.HRM_SALARY_SALBASE_BASETTS;
            this.ucHRM_SALARY_SALBASE_BASETTS.SelectCmdForUpdate = null;            
            this.ucHRM_SALARY_SALBASE_BASETTS.ServerModify = true;
            this.ucHRM_SALARY_SALBASE_BASETTS.ServerModifyGetMax = false;
            this.ucHRM_SALARY_SALBASE_BASETTS.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHRM_SALARY_SALBASE_BASETTS.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHRM_SALARY_SALBASE_BASETTS.UseTranscationScope = false;
            this.ucHRM_SALARY_SALBASE_BASETTS.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHRM_SALARY_SALBASE_BASETTS.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHRM_SALARY_SALBASE_BASETTS_BeforeInsert);
            this.ucHRM_SALARY_SALBASE_BASETTS.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucHRM_SALARY_SALBASE_BASETTS_BeforeModify);
            // 
            // log_HRM_SALARY_SALBASE_BASETTS
            // 
            this.log_HRM_SALARY_SALBASE_BASETTS.LogDateType = null;
            this.log_HRM_SALARY_SALBASE_BASETTS.LogIDField = "Log_ID";
            this.log_HRM_SALARY_SALBASE_BASETTS.LogTableName = "HRM_SALARY_SALBASE_BASETTS_LOG";
            this.log_HRM_SALARY_SALBASE_BASETTS.MarkField = "Log_State";
            this.log_HRM_SALARY_SALBASE_BASETTS.ModifierField = "Log_User";
            this.log_HRM_SALARY_SALBASE_BASETTS.ModifyDateField = "Log_Date";
            this.log_HRM_SALARY_SALBASE_BASETTS.Name = "log_HRM_SALARY_SALBASE_BASETTS";
            this.log_HRM_SALARY_SALBASE_BASETTS.NeedLog = true;
            this.log_HRM_SALARY_SALBASE_BASETTS.OnlyDistinct = false;
            srcFieldNameColumn1.FieldName = "SALBASE_BASETTS_ID";
            srcFieldNameColumn2.FieldName = "EMPLOYEE_ID";
            srcFieldNameColumn3.FieldName = "EFFECT_DATE";
            srcFieldNameColumn4.FieldName = "SALARY_ID";
            srcFieldNameColumn5.FieldName = "AMT";
            srcFieldNameColumn6.FieldName = "MEMO";
            srcFieldNameColumn7.FieldName = "CREATE_DATE";
            srcFieldNameColumn8.FieldName = "CREATE_MAN";
            srcFieldNameColumn9.FieldName = "UPDATE_DATE";
            srcFieldNameColumn10.FieldName = "UPDATE_MAN";
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn1);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn2);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn3);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn4);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn5);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn6);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn7);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn8);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn9);
            this.log_HRM_SALARY_SALBASE_BASETTS.SrcFieldNames.Add(srcFieldNameColumn10);
            // 
            // HRM_SALARY_SALBASE_BASETTS_LOG
            // 
            this.HRM_SALARY_SALBASE_BASETTS_LOG.CacheConnection = false;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.CommandText = resources.GetString("HRM_SALARY_SALBASE_BASETTS_LOG.CommandText");
            this.HRM_SALARY_SALBASE_BASETTS_LOG.CommandTimeout = 30;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.CommandType = System.Data.CommandType.Text;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.DynamicTableName = false;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.EEPAlias = null;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.EncodingAfter = null;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.EncodingBefore = "Windows-1252";            
            this.HRM_SALARY_SALBASE_BASETTS_LOG.InfoConnection = this.TheInfoConnection;
            keyItem4.KeyName = "LOG_ID";
            this.HRM_SALARY_SALBASE_BASETTS_LOG.KeyFields.Add(keyItem4);
            this.HRM_SALARY_SALBASE_BASETTS_LOG.MultiSetWhere = false;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.Name = "HRM_SALARY_SALBASE_BASETTS_LOG";
            this.HRM_SALARY_SALBASE_BASETTS_LOG.NotificationAutoEnlist = false;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.SecExcept = null;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.SecFieldName = null;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.SelectPaging = false;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.SelectTop = 0;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.SiteControl = false;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.SiteFieldName = null;
            this.HRM_SALARY_SALBASE_BASETTS_LOG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // cb_HRM_DEPT
            // 
            this.cb_HRM_DEPT.CacheConnection = false;
            this.cb_HRM_DEPT.CommandText = "Select\t[DEPT_ID],\r\n\t\t[DEPT_CODE],\r\n\t\t[DEPT_CNAME],\r\n\t\t[DEPT_ENAME],\r\n\t\tCase When " +
    "[UPPER_DEPT_ID] is Null Then 0 Else [UPPER_DEPT_ID] \r\n\t\tEnd [parentField]\r\nFrom\t" +
    "[dbo].[HRM_DEPT]\r\n\r\n\r\n\r\n";
            this.cb_HRM_DEPT.CommandTimeout = 30;
            this.cb_HRM_DEPT.CommandType = System.Data.CommandType.Text;
            this.cb_HRM_DEPT.DynamicTableName = false;
            this.cb_HRM_DEPT.EEPAlias = null;
            this.cb_HRM_DEPT.EncodingAfter = null;
            this.cb_HRM_DEPT.EncodingBefore = "Windows-1252";            
            this.cb_HRM_DEPT.InfoConnection = this.TheInfoConnection;
            keyItem5.KeyName = "DEPT_ID";
            this.cb_HRM_DEPT.KeyFields.Add(keyItem5);
            this.cb_HRM_DEPT.MultiSetWhere = false;
            this.cb_HRM_DEPT.Name = "cb_HRM_DEPT";
            this.cb_HRM_DEPT.NotificationAutoEnlist = false;
            this.cb_HRM_DEPT.SecExcept = null;
            this.cb_HRM_DEPT.SecFieldName = null;
            this.cb_HRM_DEPT.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.cb_HRM_DEPT.SelectPaging = false;
            this.cb_HRM_DEPT.SelectTop = 0;
            this.cb_HRM_DEPT.SiteControl = false;
            this.cb_HRM_DEPT.SiteFieldName = null;
            this.cb_HRM_DEPT.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.TheInfoConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_HRM_BASE_BASE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_HRM_SALARY_SALCODE_SalaryApproval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_SALARY_SALBASE_BASETTS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_SALARY_SALBASE_BASETTS_LOG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_HRM_DEPT)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager TheServiceManager;
        private Srvtools.InfoConnection TheInfoConnection;
        private Srvtools.InfoCommand cb_HRM_BASE_BASE;
        private Srvtools.InfoCommand cb_HRM_SALARY_SALCODE_SalaryApproval;
        private Srvtools.InfoCommand HRM_SALARY_SALBASE_BASETTS;
        private Srvtools.UpdateComponent ucHRM_SALARY_SALBASE_BASETTS;
        private Srvtools.LogInfo log_HRM_SALARY_SALBASE_BASETTS;
        private Srvtools.InfoCommand HRM_SALARY_SALBASE_BASETTS_LOG;
        private Srvtools.InfoCommand cb_HRM_DEPT;
    }
}
