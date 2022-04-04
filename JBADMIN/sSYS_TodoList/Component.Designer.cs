namespace sSYS_TodoList
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.View_SYS_TODOLIST = new Srvtools.InfoCommand(this.components);
            this.ucView_SYS_TODOLIST = new Srvtools.UpdateComponent(this.components);
            this.View_View_SYS_TODOLIST = new Srvtools.InfoCommand(this.components);
            this.Applicant = new Srvtools.InfoCommand(this.components);
            this.Flow_Desc = new Srvtools.InfoCommand(this.components);
            this.Auditor = new Srvtools.InfoCommand(this.components);
            this.FlowStep = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SYS_TODOLIST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_SYS_TODOLIST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Applicant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Flow_Desc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Auditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlowStep)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // View_SYS_TODOLIST
            // 
            this.View_SYS_TODOLIST.CacheConnection = false;
            this.View_SYS_TODOLIST.CommandText = "SELECT A.* ,B.EMPLOYEENAME\r\nFROM dbo.[View_SYS_TODOLIST] AS A\r\nINNER JOIN  View_E" +
    "mployee AS B ON A.APPLICANT=B.EmployeeID\r\n\r\n\r\n";
            this.View_SYS_TODOLIST.CommandTimeout = 30;
            this.View_SYS_TODOLIST.CommandType = System.Data.CommandType.Text;
            this.View_SYS_TODOLIST.DynamicTableName = false;
            this.View_SYS_TODOLIST.EEPAlias = null;
            this.View_SYS_TODOLIST.EncodingAfter = null;
            this.View_SYS_TODOLIST.EncodingBefore = "Windows-1252";
            this.View_SYS_TODOLIST.InfoConnection = this.InfoConnection1;
            this.View_SYS_TODOLIST.MultiSetWhere = false;
            this.View_SYS_TODOLIST.Name = "View_SYS_TODOLIST";
            this.View_SYS_TODOLIST.NotificationAutoEnlist = false;
            this.View_SYS_TODOLIST.SecExcept = null;
            this.View_SYS_TODOLIST.SecFieldName = null;
            this.View_SYS_TODOLIST.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SYS_TODOLIST.SelectPaging = false;
            this.View_SYS_TODOLIST.SelectTop = 0;
            this.View_SYS_TODOLIST.SiteControl = false;
            this.View_SYS_TODOLIST.SiteFieldName = null;
            this.View_SYS_TODOLIST.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucView_SYS_TODOLIST
            // 
            this.ucView_SYS_TODOLIST.AutoTrans = true;
            this.ucView_SYS_TODOLIST.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "FLOW_DESC";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "APPLICANT";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "S_STEP_ID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "D_STEP_ID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "USERNAME";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "BILLNO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AUDITOR";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "APPLYDESCR";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "UPDATEDATE";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "HOURS";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr1);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr2);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr3);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr4);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr5);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr6);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr7);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr8);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr9);
            this.ucView_SYS_TODOLIST.FieldAttrs.Add(fieldAttr10);
            this.ucView_SYS_TODOLIST.LogInfo = null;
            this.ucView_SYS_TODOLIST.Name = "ucView_SYS_TODOLIST";
            this.ucView_SYS_TODOLIST.RowAffectsCheck = true;
            this.ucView_SYS_TODOLIST.SelectCmd = this.View_SYS_TODOLIST;
            this.ucView_SYS_TODOLIST.SelectCmdForUpdate = null;
            this.ucView_SYS_TODOLIST.ServerModify = true;
            this.ucView_SYS_TODOLIST.ServerModifyGetMax = false;
            this.ucView_SYS_TODOLIST.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucView_SYS_TODOLIST.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucView_SYS_TODOLIST.UseTranscationScope = false;
            this.ucView_SYS_TODOLIST.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_View_SYS_TODOLIST
            // 
            this.View_View_SYS_TODOLIST.CacheConnection = false;
            this.View_View_SYS_TODOLIST.CommandText = "SELECT * FROM dbo.[View_SYS_TODOLIST]";
            this.View_View_SYS_TODOLIST.CommandTimeout = 30;
            this.View_View_SYS_TODOLIST.CommandType = System.Data.CommandType.Text;
            this.View_View_SYS_TODOLIST.DynamicTableName = false;
            this.View_View_SYS_TODOLIST.EEPAlias = null;
            this.View_View_SYS_TODOLIST.EncodingAfter = null;
            this.View_View_SYS_TODOLIST.EncodingBefore = "Windows-1252";
            this.View_View_SYS_TODOLIST.InfoConnection = this.InfoConnection1;
            this.View_View_SYS_TODOLIST.MultiSetWhere = false;
            this.View_View_SYS_TODOLIST.Name = "View_View_SYS_TODOLIST";
            this.View_View_SYS_TODOLIST.NotificationAutoEnlist = false;
            this.View_View_SYS_TODOLIST.SecExcept = null;
            this.View_View_SYS_TODOLIST.SecFieldName = null;
            this.View_View_SYS_TODOLIST.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_View_SYS_TODOLIST.SelectPaging = false;
            this.View_View_SYS_TODOLIST.SelectTop = 0;
            this.View_View_SYS_TODOLIST.SiteControl = false;
            this.View_View_SYS_TODOLIST.SiteFieldName = null;
            this.View_View_SYS_TODOLIST.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Applicant
            // 
            this.Applicant.CacheConnection = false;
            this.Applicant.CommandText = "select View_ToDoList_Applicant.EmployeeID,View_ToDoList_Applicant.EmployeeName,Vi" +
    "ew_ToDoList_Applicant.FLOW_DESC from View_ToDoList_Applicant";
            this.Applicant.CommandTimeout = 30;
            this.Applicant.CommandType = System.Data.CommandType.Text;
            this.Applicant.DynamicTableName = false;
            this.Applicant.EEPAlias = null;
            this.Applicant.EncodingAfter = null;
            this.Applicant.EncodingBefore = "Windows-1252";
            this.Applicant.InfoConnection = this.InfoConnection1;
            this.Applicant.MultiSetWhere = false;
            this.Applicant.Name = "Applicant";
            this.Applicant.NotificationAutoEnlist = false;
            this.Applicant.SecExcept = null;
            this.Applicant.SecFieldName = null;
            this.Applicant.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Applicant.SelectPaging = false;
            this.Applicant.SelectTop = 0;
            this.Applicant.SiteControl = false;
            this.Applicant.SiteFieldName = null;
            this.Applicant.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Flow_Desc
            // 
            this.Flow_Desc.CacheConnection = false;
            this.Flow_Desc.CommandText = "select View_ToDoList_Flow_Desc.FLOW_DESC from View_ToDoList_Flow_Desc\r\norder by V" +
    "iew_ToDoList_Flow_Desc.FLOW_DESC";
            this.Flow_Desc.CommandTimeout = 30;
            this.Flow_Desc.CommandType = System.Data.CommandType.Text;
            this.Flow_Desc.DynamicTableName = false;
            this.Flow_Desc.EEPAlias = null;
            this.Flow_Desc.EncodingAfter = null;
            this.Flow_Desc.EncodingBefore = "Windows-1252";
            this.Flow_Desc.InfoConnection = this.InfoConnection1;
            this.Flow_Desc.MultiSetWhere = false;
            this.Flow_Desc.Name = "Flow_Desc";
            this.Flow_Desc.NotificationAutoEnlist = false;
            this.Flow_Desc.SecExcept = null;
            this.Flow_Desc.SecFieldName = null;
            this.Flow_Desc.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Flow_Desc.SelectPaging = false;
            this.Flow_Desc.SelectTop = 0;
            this.Flow_Desc.SiteControl = false;
            this.Flow_Desc.SiteFieldName = null;
            this.Flow_Desc.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Auditor
            // 
            this.Auditor.CacheConnection = false;
            this.Auditor.CommandText = "select View_ToDoList_Auditor.AUDITOR ,\r\n          View_ToDoList_Auditor.FLOW_DESC" +
    "\r\nfrom View_ToDoList_Auditor";
            this.Auditor.CommandTimeout = 30;
            this.Auditor.CommandType = System.Data.CommandType.Text;
            this.Auditor.DynamicTableName = false;
            this.Auditor.EEPAlias = null;
            this.Auditor.EncodingAfter = null;
            this.Auditor.EncodingBefore = "Windows-1252";
            this.Auditor.InfoConnection = this.InfoConnection1;
            this.Auditor.MultiSetWhere = false;
            this.Auditor.Name = "Auditor";
            this.Auditor.NotificationAutoEnlist = false;
            this.Auditor.SecExcept = null;
            this.Auditor.SecFieldName = null;
            this.Auditor.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Auditor.SelectPaging = false;
            this.Auditor.SelectTop = 0;
            this.Auditor.SiteControl = false;
            this.Auditor.SiteFieldName = null;
            this.Auditor.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // FlowStep
            // 
            this.FlowStep.CacheConnection = false;
            this.FlowStep.CommandText = "select View_SYS_TODOLIST_STEP.D_STEP_ID,\r\n          View_SYS_TODOLIST_STEP.FLOW_D" +
    "ESC\r\nfrom View_SYS_TODOLIST_STEP\r\n";
            this.FlowStep.CommandTimeout = 30;
            this.FlowStep.CommandType = System.Data.CommandType.Text;
            this.FlowStep.DynamicTableName = false;
            this.FlowStep.EEPAlias = null;
            this.FlowStep.EncodingAfter = null;
            this.FlowStep.EncodingBefore = "Windows-1252";
            this.FlowStep.InfoConnection = this.InfoConnection1;
            this.FlowStep.MultiSetWhere = false;
            this.FlowStep.Name = "FlowStep";
            this.FlowStep.NotificationAutoEnlist = false;
            this.FlowStep.SecExcept = null;
            this.FlowStep.SecFieldName = null;
            this.FlowStep.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FlowStep.SelectPaging = false;
            this.FlowStep.SelectTop = 0;
            this.FlowStep.SiteControl = false;
            this.FlowStep.SiteFieldName = null;
            this.FlowStep.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SYS_TODOLIST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_SYS_TODOLIST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Applicant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Flow_Desc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Auditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlowStep)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand View_SYS_TODOLIST;
        private Srvtools.UpdateComponent ucView_SYS_TODOLIST;
        private Srvtools.InfoCommand View_View_SYS_TODOLIST;
        private Srvtools.InfoCommand Applicant;
        private Srvtools.InfoCommand Flow_Desc;
        private Srvtools.InfoCommand Auditor;
        private Srvtools.InfoCommand FlowStep;
    }
}
