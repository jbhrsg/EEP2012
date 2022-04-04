namespace sEmpYearPayQuery
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
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.EmpYearBill_JB = new Srvtools.InfoCommand(this.components);
            this.ucEmpYearBill_JB = new Srvtools.UpdateComponent(this.components);
            this.View_EmpYearBill_JB = new Srvtools.InfoCommand(this.components);
            this.PayYear = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpYearBill_JB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_EmpYearBill_JB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayYear)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // EmpYearBill_JB
            // 
            this.EmpYearBill_JB.CacheConnection = false;
            this.EmpYearBill_JB.CommandText = resources.GetString("EmpYearBill_JB.CommandText");
            this.EmpYearBill_JB.CommandTimeout = 30;
            this.EmpYearBill_JB.CommandType = System.Data.CommandType.Text;
            this.EmpYearBill_JB.DynamicTableName = false;
            this.EmpYearBill_JB.EEPAlias = null;
            this.EmpYearBill_JB.EncodingAfter = null;
            this.EmpYearBill_JB.EncodingBefore = "Windows-1252";
            this.EmpYearBill_JB.InfoConnection = this.InfoConnection1;
            this.EmpYearBill_JB.MultiSetWhere = false;
            this.EmpYearBill_JB.Name = "EmpYearBill_JB";
            this.EmpYearBill_JB.NotificationAutoEnlist = false;
            this.EmpYearBill_JB.SecExcept = null;
            this.EmpYearBill_JB.SecFieldName = "USERID";
            this.EmpYearBill_JB.SecStyle = Srvtools.SecurityStyle.ssByUser;
            this.EmpYearBill_JB.SelectPaging = false;
            this.EmpYearBill_JB.SelectTop = 0;
            this.EmpYearBill_JB.SiteControl = false;
            this.EmpYearBill_JB.SiteFieldName = null;
            this.EmpYearBill_JB.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucEmpYearBill_JB
            // 
            this.ucEmpYearBill_JB.AutoTrans = true;
            this.ucEmpYearBill_JB.ExceptJoin = false;
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
            fieldAttr3.DataField = "Gender";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "IDNumber";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Address";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "IncomeSub";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "IDType";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "PersonGroup";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ErrorNote1";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ErrorNote2";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Profession";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "IsPEmp";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CustomerID";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "InsGroupID";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "OrderKey";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "StdYM";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "EndYM";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "HouseNO";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr1);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr2);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr3);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr4);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr5);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr6);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr7);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr8);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr9);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr10);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr11);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr12);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr13);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr14);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr15);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr16);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr17);
            this.ucEmpYearBill_JB.FieldAttrs.Add(fieldAttr18);
            this.ucEmpYearBill_JB.LogInfo = null;
            this.ucEmpYearBill_JB.Name = "ucEmpYearBill_JB";
            this.ucEmpYearBill_JB.RowAffectsCheck = true;
            this.ucEmpYearBill_JB.SelectCmd = this.EmpYearBill_JB;
            this.ucEmpYearBill_JB.SelectCmdForUpdate = null;
            this.ucEmpYearBill_JB.ServerModify = true;
            this.ucEmpYearBill_JB.ServerModifyGetMax = false;
            this.ucEmpYearBill_JB.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmpYearBill_JB.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmpYearBill_JB.UseTranscationScope = false;
            this.ucEmpYearBill_JB.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_EmpYearBill_JB
            // 
            this.View_EmpYearBill_JB.CacheConnection = false;
            this.View_EmpYearBill_JB.CommandText = "SELECT * FROM dbo.[EmpYearBill_JB]";
            this.View_EmpYearBill_JB.CommandTimeout = 30;
            this.View_EmpYearBill_JB.CommandType = System.Data.CommandType.Text;
            this.View_EmpYearBill_JB.DynamicTableName = false;
            this.View_EmpYearBill_JB.EEPAlias = null;
            this.View_EmpYearBill_JB.EncodingAfter = null;
            this.View_EmpYearBill_JB.EncodingBefore = "Windows-1252";
            this.View_EmpYearBill_JB.InfoConnection = this.InfoConnection1;
            this.View_EmpYearBill_JB.MultiSetWhere = false;
            this.View_EmpYearBill_JB.Name = "View_EmpYearBill_JB";
            this.View_EmpYearBill_JB.NotificationAutoEnlist = false;
            this.View_EmpYearBill_JB.SecExcept = null;
            this.View_EmpYearBill_JB.SecFieldName = null;
            this.View_EmpYearBill_JB.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_EmpYearBill_JB.SelectPaging = false;
            this.View_EmpYearBill_JB.SelectTop = 0;
            this.View_EmpYearBill_JB.SiteControl = false;
            this.View_EmpYearBill_JB.SiteFieldName = null;
            this.View_EmpYearBill_JB.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PayYear
            // 
            this.PayYear.CacheConnection = false;
            this.PayYear.CommandText = "SELECT DISTINCT YEARNO FROM EMPYEARINCOME_JB \r\n";
            this.PayYear.CommandTimeout = 30;
            this.PayYear.CommandType = System.Data.CommandType.Text;
            this.PayYear.DynamicTableName = false;
            this.PayYear.EEPAlias = null;
            this.PayYear.EncodingAfter = null;
            this.PayYear.EncodingBefore = "Windows-1252";
            this.PayYear.InfoConnection = this.InfoConnection1;
            this.PayYear.MultiSetWhere = false;
            this.PayYear.Name = "PayYear";
            this.PayYear.NotificationAutoEnlist = false;
            this.PayYear.SecExcept = null;
            this.PayYear.SecFieldName = null;
            this.PayYear.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PayYear.SelectPaging = false;
            this.PayYear.SelectTop = 0;
            this.PayYear.SiteControl = false;
            this.PayYear.SiteFieldName = null;
            this.PayYear.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpYearBill_JB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_EmpYearBill_JB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PayYear)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand EmpYearBill_JB;
        private Srvtools.UpdateComponent ucEmpYearBill_JB;
        private Srvtools.InfoCommand View_EmpYearBill_JB;
        private Srvtools.InfoCommand PayYear;
    }
}
