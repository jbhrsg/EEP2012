namespace sSalaryReport
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SalaryReport = new Srvtools.InfoCommand(this.components);
            this.ucSalaryReport = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalaryReport)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Salary";
            // 
            // SalaryReport
            // 
            this.SalaryReport.CacheConnection = false;
            this.SalaryReport.CommandText = "select * from View_SalaryMemberTable\r\norder by ValidateCount desc";
            this.SalaryReport.CommandTimeout = 30;
            this.SalaryReport.CommandType = System.Data.CommandType.Text;
            this.SalaryReport.DynamicTableName = false;
            this.SalaryReport.EEPAlias = "Salary";
            this.SalaryReport.EncodingAfter = null;
            this.SalaryReport.EncodingBefore = "Windows-1252";
            this.SalaryReport.EncodingConvert = null;
            this.SalaryReport.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CompanyID";
            this.SalaryReport.KeyFields.Add(keyItem1);
            this.SalaryReport.MultiSetWhere = false;
            this.SalaryReport.Name = "SalaryReport";
            this.SalaryReport.NotificationAutoEnlist = false;
            this.SalaryReport.SecExcept = null;
            this.SalaryReport.SecFieldName = null;
            this.SalaryReport.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalaryReport.SelectPaging = false;
            this.SalaryReport.SelectTop = 0;
            this.SalaryReport.SiteControl = false;
            this.SalaryReport.SiteFieldName = null;
            this.SalaryReport.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalaryReport
            // 
            this.ucSalaryReport.AutoTrans = true;
            this.ucSalaryReport.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CostCenterID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CostCenterName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateBy";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LastUpdateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucSalaryReport.FieldAttrs.Add(fieldAttr1);
            this.ucSalaryReport.FieldAttrs.Add(fieldAttr2);
            this.ucSalaryReport.FieldAttrs.Add(fieldAttr3);
            this.ucSalaryReport.FieldAttrs.Add(fieldAttr4);
            this.ucSalaryReport.FieldAttrs.Add(fieldAttr5);
            this.ucSalaryReport.FieldAttrs.Add(fieldAttr6);
            this.ucSalaryReport.FieldAttrs.Add(fieldAttr7);
            this.ucSalaryReport.LogInfo = null;
            this.ucSalaryReport.Name = "ucSalaryReport";
            this.ucSalaryReport.RowAffectsCheck = true;
            this.ucSalaryReport.SelectCmd = this.SalaryReport;
            this.ucSalaryReport.SelectCmdForUpdate = null;
            this.ucSalaryReport.SendSQLCmd = true;
            this.ucSalaryReport.ServerModify = true;
            this.ucSalaryReport.ServerModifyGetMax = false;
            this.ucSalaryReport.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalaryReport.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalaryReport.UseTranscationScope = false;
            this.ucSalaryReport.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalaryReport)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalaryReport;
        private Srvtools.UpdateComponent ucSalaryReport;
    }
}
