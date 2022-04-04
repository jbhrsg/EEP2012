namespace sERPDelayLunchSet
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ucERPDelayLunchEmpAuto = new Srvtools.UpdateComponent(this.components);
            this.ucERPDelayLunchEmp = new Srvtools.UpdateComponent(this.components);
            this.ERPDelayLunchEmp = new Srvtools.InfoCommand(this.components);
            this.infoEmpID = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.ERPDelayLunchEmpAuto = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchEmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmpID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchEmpAuto)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "checkDateData";
            service1.NonLogin = false;
            service1.ServiceName = "checkDateData";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ucERPDelayLunchEmpAuto
            // 
            this.ucERPDelayLunchEmpAuto.AutoTrans = true;
            this.ucERPDelayLunchEmpAuto.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "EmpID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CreateBy";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            this.ucERPDelayLunchEmpAuto.FieldAttrs.Add(fieldAttr1);
            this.ucERPDelayLunchEmpAuto.FieldAttrs.Add(fieldAttr2);
            this.ucERPDelayLunchEmpAuto.FieldAttrs.Add(fieldAttr3);
            this.ucERPDelayLunchEmpAuto.LogInfo = null;
            this.ucERPDelayLunchEmpAuto.Name = "uc";
            this.ucERPDelayLunchEmpAuto.RowAffectsCheck = true;
            this.ucERPDelayLunchEmpAuto.SelectCmd = this.ERPDelayLunchEmpAuto;
            this.ucERPDelayLunchEmpAuto.SelectCmdForUpdate = this.ERPDelayLunchEmpAuto;
            this.ucERPDelayLunchEmpAuto.SendSQLCmd = true;
            this.ucERPDelayLunchEmpAuto.ServerModify = true;
            this.ucERPDelayLunchEmpAuto.ServerModifyGetMax = false;
            this.ucERPDelayLunchEmpAuto.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPDelayLunchEmpAuto.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPDelayLunchEmpAuto.UseTranscationScope = false;
            this.ucERPDelayLunchEmpAuto.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ucERPDelayLunchEmp
            // 
            this.ucERPDelayLunchEmp.AutoTrans = true;
            this.ucERPDelayLunchEmp.ExceptJoin = false;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "EmpID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
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
            this.ucERPDelayLunchEmp.FieldAttrs.Add(fieldAttr4);
            this.ucERPDelayLunchEmp.FieldAttrs.Add(fieldAttr5);
            this.ucERPDelayLunchEmp.FieldAttrs.Add(fieldAttr6);
            this.ucERPDelayLunchEmp.LogInfo = null;
            this.ucERPDelayLunchEmp.Name = "ucERPDelayLunchEmp";
            this.ucERPDelayLunchEmp.RowAffectsCheck = true;
            this.ucERPDelayLunchEmp.SelectCmd = this.ERPDelayLunchEmp;
            this.ucERPDelayLunchEmp.SelectCmdForUpdate = null;
            this.ucERPDelayLunchEmp.SendSQLCmd = true;
            this.ucERPDelayLunchEmp.ServerModify = true;
            this.ucERPDelayLunchEmp.ServerModifyGetMax = false;
            this.ucERPDelayLunchEmp.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPDelayLunchEmp.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPDelayLunchEmp.UseTranscationScope = false;
            this.ucERPDelayLunchEmp.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ERPDelayLunchEmp
            // 
            this.ERPDelayLunchEmp.CacheConnection = false;
            this.ERPDelayLunchEmp.CommandText = "SELECT dbo.[ERPDelayLunchEmp].* FROM dbo.[ERPDelayLunchEmp]";
            this.ERPDelayLunchEmp.CommandTimeout = 30;
            this.ERPDelayLunchEmp.CommandType = System.Data.CommandType.Text;
            this.ERPDelayLunchEmp.DynamicTableName = false;
            this.ERPDelayLunchEmp.EEPAlias = null;
            this.ERPDelayLunchEmp.EncodingAfter = null;
            this.ERPDelayLunchEmp.EncodingBefore = "Windows-1252";
            this.ERPDelayLunchEmp.EncodingConvert = null;
            this.ERPDelayLunchEmp.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "EmpID";
            this.ERPDelayLunchEmp.KeyFields.Add(keyItem2);
            this.ERPDelayLunchEmp.MultiSetWhere = false;
            this.ERPDelayLunchEmp.Name = "ERPDelayLunchEmp";
            this.ERPDelayLunchEmp.NotificationAutoEnlist = false;
            this.ERPDelayLunchEmp.SecExcept = null;
            this.ERPDelayLunchEmp.SecFieldName = null;
            this.ERPDelayLunchEmp.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPDelayLunchEmp.SelectPaging = false;
            this.ERPDelayLunchEmp.SelectTop = 0;
            this.ERPDelayLunchEmp.SiteControl = false;
            this.ERPDelayLunchEmp.SiteFieldName = null;
            this.ERPDelayLunchEmp.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoEmpID
            // 
            this.infoEmpID.CacheConnection = false;
            this.infoEmpID.CommandText = "SELECT  EMPLOYEE_ID,EMPLOYEE_CODE,NAME_C  \r\nFrom [dtHRM_BaseAndBasetts_Employed](" +
    "GetDate())";
            this.infoEmpID.CommandTimeout = 30;
            this.infoEmpID.CommandType = System.Data.CommandType.Text;
            this.infoEmpID.DynamicTableName = false;
            this.infoEmpID.EEPAlias = "JBHR_EEP";
            this.infoEmpID.EncodingAfter = null;
            this.infoEmpID.EncodingBefore = "Windows-1252";
            this.infoEmpID.EncodingConvert = null;
            this.infoEmpID.InfoConnection = this.infoConnection2;
            keyItem3.KeyName = "EMPLOYEE_ID";
            this.infoEmpID.KeyFields.Add(keyItem3);
            this.infoEmpID.MultiSetWhere = false;
            this.infoEmpID.Name = "infoEmpID";
            this.infoEmpID.NotificationAutoEnlist = false;
            this.infoEmpID.SecExcept = null;
            this.infoEmpID.SecFieldName = null;
            this.infoEmpID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoEmpID.SelectPaging = false;
            this.infoEmpID.SelectTop = 0;
            this.infoEmpID.SiteControl = false;
            this.infoEmpID.SiteFieldName = null;
            this.infoEmpID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBHR_EEP";
            // 
            // ERPDelayLunchEmpAuto
            // 
            this.ERPDelayLunchEmpAuto.CacheConnection = false;
            this.ERPDelayLunchEmpAuto.CommandText = "SELECT dbo.[ERPDelayLunchEmpAuto].* FROM dbo.[ERPDelayLunchEmpAuto]\r\norder by Emp" +
    "ID\r\n";
            this.ERPDelayLunchEmpAuto.CommandTimeout = 30;
            this.ERPDelayLunchEmpAuto.CommandType = System.Data.CommandType.Text;
            this.ERPDelayLunchEmpAuto.DynamicTableName = false;
            this.ERPDelayLunchEmpAuto.EEPAlias = null;
            this.ERPDelayLunchEmpAuto.EncodingAfter = null;
            this.ERPDelayLunchEmpAuto.EncodingBefore = "Windows-1252";
            this.ERPDelayLunchEmpAuto.EncodingConvert = null;
            this.ERPDelayLunchEmpAuto.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "EmpID";
            this.ERPDelayLunchEmpAuto.KeyFields.Add(keyItem1);
            this.ERPDelayLunchEmpAuto.MultiSetWhere = false;
            this.ERPDelayLunchEmpAuto.Name = "ERPDelayLunchEmpAuto";
            this.ERPDelayLunchEmpAuto.NotificationAutoEnlist = false;
            this.ERPDelayLunchEmpAuto.SecExcept = null;
            this.ERPDelayLunchEmpAuto.SecFieldName = null;
            this.ERPDelayLunchEmpAuto.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPDelayLunchEmpAuto.SelectPaging = false;
            this.ERPDelayLunchEmpAuto.SelectTop = 0;
            this.ERPDelayLunchEmpAuto.SiteControl = false;
            this.ERPDelayLunchEmpAuto.SiteFieldName = null;
            this.ERPDelayLunchEmpAuto.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchEmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmpID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchEmpAuto)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.UpdateComponent ucERPDelayLunchEmpAuto;
        private Srvtools.UpdateComponent ucERPDelayLunchEmp;
        private Srvtools.InfoCommand ERPDelayLunchEmp;
        private Srvtools.InfoCommand infoEmpID;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand ERPDelayLunchEmpAuto;
    }
}
