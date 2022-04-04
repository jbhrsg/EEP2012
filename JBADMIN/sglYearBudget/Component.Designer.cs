namespace sglYearBudget
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
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.Service service7 = new Srvtools.Service();
            Srvtools.Service service8 = new Srvtools.Service();
            Srvtools.Service service9 = new Srvtools.Service();
            Srvtools.Service service10 = new Srvtools.Service();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr26 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr27 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr28 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glYearBudget = new Srvtools.InfoCommand(this.components);
            this.ucglYearBudget = new Srvtools.UpdateComponent(this.components);
            this.View_glYearBudget = new Srvtools.InfoCommand(this.components);
            this.VoucherYear = new Srvtools.InfoCommand(this.components);
            this.glBudgetType = new Srvtools.InfoCommand(this.components);
            this.AccItemM = new Srvtools.InfoCommand(this.components);
            this.VoucherMonth = new Srvtools.InfoCommand(this.components);
            this.CostCenter = new Srvtools.InfoCommand(this.components);
            this.AccItemS = new Srvtools.InfoCommand(this.components);
            this.CostCenterList = new Srvtools.InfoCommand(this.components);
            this.BookedAmtDetails = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glYearBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_glYearBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoucherYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glBudgetType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccItemM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoucherMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccItemS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookedAmtDetails)).BeginInit();
            // 
            // serviceManager1
            // 
            service6.DelegateName = "GetAccitemM";
            service6.NonLogin = false;
            service6.ServiceName = "GetAccitemM";
            service7.DelegateName = "GetGridDataDynamic";
            service7.NonLogin = false;
            service7.ServiceName = "GetGridDataDynamic";
            service8.DelegateName = "GetGridDataDynamicExcel";
            service8.NonLogin = false;
            service8.ServiceName = "GetGridDataDynamicExcel";
            service9.DelegateName = "GetAuditorList";
            service9.NonLogin = false;
            service9.ServiceName = "GetAuditorList";
            service10.DelegateName = "GetCostCenter";
            service10.NonLogin = false;
            service10.ServiceName = "GetCostCenter";
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            this.serviceManager1.ServiceCollection.Add(service9);
            this.serviceManager1.ServiceCollection.Add(service10);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // glYearBudget
            // 
            this.glYearBudget.CacheConnection = false;
            this.glYearBudget.CommandText = "SELECT     YB.*,\r\n                 GetDate()  AS EndDate\r\nFROM  dbo.glYearBudgetQ" +
    "uery  AS YB\r\nORDER BY  YB.BudgetType,YB.RecordType,YB.Acno_S+SubAcno_S\r\n\r\n";
            this.glYearBudget.CommandTimeout = 30;
            this.glYearBudget.CommandType = System.Data.CommandType.Text;
            this.glYearBudget.DynamicTableName = false;
            this.glYearBudget.EEPAlias = null;
            this.glYearBudget.EncodingAfter = null;
            this.glYearBudget.EncodingBefore = "Windows-1252";
            this.glYearBudget.EncodingConvert = null;
            this.glYearBudget.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.glYearBudget.KeyFields.Add(keyItem3);
            this.glYearBudget.MultiSetWhere = false;
            this.glYearBudget.Name = "glYearBudget";
            this.glYearBudget.NotificationAutoEnlist = false;
            this.glYearBudget.SecExcept = null;
            this.glYearBudget.SecFieldName = null;
            this.glYearBudget.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glYearBudget.SelectPaging = false;
            this.glYearBudget.SelectTop = 0;
            this.glYearBudget.SiteControl = false;
            this.glYearBudget.SiteFieldName = null;
            this.glYearBudget.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglYearBudget
            // 
            this.ucglYearBudget.AutoTrans = true;
            this.ucglYearBudget.ExceptJoin = false;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "AutoKey";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "BudgetType";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CostCenterID";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "AcnoName";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "Acno_S";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "SubAcno_S";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "Acno_E";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "SubAcno_E";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "BudgetAmt";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "VoucherYear";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "CreateBy";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = "_username";
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "CreateDate";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "LastUpdateBy";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = "_username";
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "LastUpdateDate";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr15);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr16);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr17);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr18);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr19);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr20);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr21);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr22);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr23);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr24);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr25);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr26);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr27);
            this.ucglYearBudget.FieldAttrs.Add(fieldAttr28);
            this.ucglYearBudget.LogInfo = null;
            this.ucglYearBudget.Name = "ucglYearBudget";
            this.ucglYearBudget.RowAffectsCheck = true;
            this.ucglYearBudget.SelectCmd = this.glYearBudget;
            this.ucglYearBudget.SelectCmdForUpdate = null;
            this.ucglYearBudget.SendSQLCmd = true;
            this.ucglYearBudget.ServerModify = true;
            this.ucglYearBudget.ServerModifyGetMax = false;
            this.ucglYearBudget.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglYearBudget.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglYearBudget.UseTranscationScope = false;
            this.ucglYearBudget.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucglYearBudget.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucglYearBudget_BeforeInsert);
            this.ucglYearBudget.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucglYearBudget_BeforeModify);
            // 
            // View_glYearBudget
            // 
            this.View_glYearBudget.CacheConnection = false;
            this.View_glYearBudget.CommandText = "SELECT * FROM dbo.[glYearBudget]";
            this.View_glYearBudget.CommandTimeout = 30;
            this.View_glYearBudget.CommandType = System.Data.CommandType.Text;
            this.View_glYearBudget.DynamicTableName = false;
            this.View_glYearBudget.EEPAlias = null;
            this.View_glYearBudget.EncodingAfter = null;
            this.View_glYearBudget.EncodingBefore = "Windows-1252";
            this.View_glYearBudget.EncodingConvert = null;
            this.View_glYearBudget.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.View_glYearBudget.KeyFields.Add(keyItem1);
            this.View_glYearBudget.MultiSetWhere = false;
            this.View_glYearBudget.Name = "View_glYearBudget";
            this.View_glYearBudget.NotificationAutoEnlist = false;
            this.View_glYearBudget.SecExcept = null;
            this.View_glYearBudget.SecFieldName = null;
            this.View_glYearBudget.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_glYearBudget.SelectPaging = false;
            this.View_glYearBudget.SelectTop = 0;
            this.View_glYearBudget.SiteControl = false;
            this.View_glYearBudget.SiteFieldName = null;
            this.View_glYearBudget.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VoucherYear
            // 
            this.VoucherYear.CacheConnection = false;
            this.VoucherYear.CommandText = "SELECT DISTINCT  VoucherYear  FROM glYearBudget\r\nORDER BY  VoucherYear DESC";
            this.VoucherYear.CommandTimeout = 30;
            this.VoucherYear.CommandType = System.Data.CommandType.Text;
            this.VoucherYear.DynamicTableName = false;
            this.VoucherYear.EEPAlias = null;
            this.VoucherYear.EncodingAfter = null;
            this.VoucherYear.EncodingBefore = "Windows-1252";
            this.VoucherYear.EncodingConvert = null;
            this.VoucherYear.InfoConnection = this.InfoConnection1;
            this.VoucherYear.MultiSetWhere = false;
            this.VoucherYear.Name = "VoucherYear";
            this.VoucherYear.NotificationAutoEnlist = false;
            this.VoucherYear.SecExcept = null;
            this.VoucherYear.SecFieldName = null;
            this.VoucherYear.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VoucherYear.SelectPaging = false;
            this.VoucherYear.SelectTop = 0;
            this.VoucherYear.SiteControl = false;
            this.VoucherYear.SiteFieldName = null;
            this.VoucherYear.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // glBudgetType
            // 
            this.glBudgetType.CacheConnection = false;
            this.glBudgetType.CommandText = "SELECT  BudgetType,BudgetTypeName  FROM glBudgetType\r\nOrder by  BudgetType";
            this.glBudgetType.CommandTimeout = 30;
            this.glBudgetType.CommandType = System.Data.CommandType.Text;
            this.glBudgetType.DynamicTableName = false;
            this.glBudgetType.EEPAlias = null;
            this.glBudgetType.EncodingAfter = null;
            this.glBudgetType.EncodingBefore = "Windows-1252";
            this.glBudgetType.EncodingConvert = null;
            this.glBudgetType.InfoConnection = this.InfoConnection1;
            this.glBudgetType.MultiSetWhere = false;
            this.glBudgetType.Name = "glBudgetType";
            this.glBudgetType.NotificationAutoEnlist = false;
            this.glBudgetType.SecExcept = null;
            this.glBudgetType.SecFieldName = null;
            this.glBudgetType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glBudgetType.SelectPaging = false;
            this.glBudgetType.SelectTop = 0;
            this.glBudgetType.SiteControl = false;
            this.glBudgetType.SiteFieldName = null;
            this.glBudgetType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccItemM
            // 
            this.AccItemM.CacheConnection = false;
            this.AccItemM.CommandText = resources.GetString("AccItemM.CommandText");
            this.AccItemM.CommandTimeout = 30;
            this.AccItemM.CommandType = System.Data.CommandType.Text;
            this.AccItemM.DynamicTableName = false;
            this.AccItemM.EEPAlias = null;
            this.AccItemM.EncodingAfter = null;
            this.AccItemM.EncodingBefore = "Windows-1252";
            this.AccItemM.EncodingConvert = null;
            this.AccItemM.InfoConnection = this.InfoConnection1;
            this.AccItemM.MultiSetWhere = false;
            this.AccItemM.Name = "AccItemM";
            this.AccItemM.NotificationAutoEnlist = false;
            this.AccItemM.SecExcept = null;
            this.AccItemM.SecFieldName = null;
            this.AccItemM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AccItemM.SelectPaging = false;
            this.AccItemM.SelectTop = 0;
            this.AccItemM.SiteControl = false;
            this.AccItemM.SiteFieldName = null;
            this.AccItemM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VoucherMonth
            // 
            this.VoucherMonth.CacheConnection = false;
            this.VoucherMonth.CommandText = resources.GetString("VoucherMonth.CommandText");
            this.VoucherMonth.CommandTimeout = 30;
            this.VoucherMonth.CommandType = System.Data.CommandType.Text;
            this.VoucherMonth.DynamicTableName = false;
            this.VoucherMonth.EEPAlias = null;
            this.VoucherMonth.EncodingAfter = null;
            this.VoucherMonth.EncodingBefore = "Windows-1252";
            this.VoucherMonth.EncodingConvert = null;
            this.VoucherMonth.InfoConnection = this.InfoConnection1;
            this.VoucherMonth.MultiSetWhere = false;
            this.VoucherMonth.Name = "VoucherMonth";
            this.VoucherMonth.NotificationAutoEnlist = false;
            this.VoucherMonth.SecExcept = null;
            this.VoucherMonth.SecFieldName = null;
            this.VoucherMonth.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VoucherMonth.SelectPaging = false;
            this.VoucherMonth.SelectTop = 0;
            this.VoucherMonth.SiteControl = false;
            this.VoucherMonth.SiteFieldName = null;
            this.VoucherMonth.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CostCenter
            // 
            this.CostCenter.CacheConnection = false;
            this.CostCenter.CommandText = resources.GetString("CostCenter.CommandText");
            this.CostCenter.CommandTimeout = 30;
            this.CostCenter.CommandType = System.Data.CommandType.Text;
            this.CostCenter.DynamicTableName = false;
            this.CostCenter.EEPAlias = null;
            this.CostCenter.EncodingAfter = null;
            this.CostCenter.EncodingBefore = "Windows-1252";
            this.CostCenter.EncodingConvert = null;
            this.CostCenter.InfoConnection = this.InfoConnection1;
            this.CostCenter.MultiSetWhere = false;
            this.CostCenter.Name = "CostCenter";
            this.CostCenter.NotificationAutoEnlist = false;
            this.CostCenter.SecExcept = null;
            this.CostCenter.SecFieldName = null;
            this.CostCenter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CostCenter.SelectPaging = false;
            this.CostCenter.SelectTop = 0;
            this.CostCenter.SiteControl = false;
            this.CostCenter.SiteFieldName = null;
            this.CostCenter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // AccItemS
            // 
            this.AccItemS.CacheConnection = false;
            this.AccItemS.CommandText = resources.GetString("AccItemS.CommandText");
            this.AccItemS.CommandTimeout = 30;
            this.AccItemS.CommandType = System.Data.CommandType.Text;
            this.AccItemS.DynamicTableName = false;
            this.AccItemS.EEPAlias = null;
            this.AccItemS.EncodingAfter = null;
            this.AccItemS.EncodingBefore = "Windows-1252";
            this.AccItemS.EncodingConvert = null;
            this.AccItemS.InfoConnection = this.InfoConnection1;
            this.AccItemS.MultiSetWhere = false;
            this.AccItemS.Name = "AccItemS";
            this.AccItemS.NotificationAutoEnlist = false;
            this.AccItemS.SecExcept = null;
            this.AccItemS.SecFieldName = null;
            this.AccItemS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AccItemS.SelectPaging = false;
            this.AccItemS.SelectTop = 0;
            this.AccItemS.SiteControl = false;
            this.AccItemS.SiteFieldName = null;
            this.AccItemS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CostCenterList
            // 
            this.CostCenterList.CacheConnection = false;
            this.CostCenterList.CommandText = " SELECT  COSTCENTERID,COSTCENTERNAME   FROM glCOSTCENTER  ORDER BY  SORTID";
            this.CostCenterList.CommandTimeout = 30;
            this.CostCenterList.CommandType = System.Data.CommandType.Text;
            this.CostCenterList.DynamicTableName = false;
            this.CostCenterList.EEPAlias = null;
            this.CostCenterList.EncodingAfter = null;
            this.CostCenterList.EncodingBefore = "Windows-1252";
            this.CostCenterList.EncodingConvert = null;
            this.CostCenterList.InfoConnection = this.InfoConnection1;
            this.CostCenterList.MultiSetWhere = false;
            this.CostCenterList.Name = "CostCenterList";
            this.CostCenterList.NotificationAutoEnlist = false;
            this.CostCenterList.SecExcept = null;
            this.CostCenterList.SecFieldName = null;
            this.CostCenterList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CostCenterList.SelectPaging = false;
            this.CostCenterList.SelectTop = 0;
            this.CostCenterList.SiteControl = false;
            this.CostCenterList.SiteFieldName = null;
            this.CostCenterList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // BookedAmtDetails
            // 
            this.BookedAmtDetails.CacheConnection = false;
            this.BookedAmtDetails.CommandText = " SELECT * FROM dbo.View_glYearBudgetVoucherDetails\r\n ORDER BY VoucherDate\r\n";
            this.BookedAmtDetails.CommandTimeout = 30;
            this.BookedAmtDetails.CommandType = System.Data.CommandType.Text;
            this.BookedAmtDetails.DynamicTableName = false;
            this.BookedAmtDetails.EEPAlias = null;
            this.BookedAmtDetails.EncodingAfter = null;
            this.BookedAmtDetails.EncodingBefore = "Windows-1252";
            this.BookedAmtDetails.EncodingConvert = null;
            this.BookedAmtDetails.InfoConnection = this.InfoConnection1;
            this.BookedAmtDetails.MultiSetWhere = false;
            this.BookedAmtDetails.Name = "BookedAmtDetails";
            this.BookedAmtDetails.NotificationAutoEnlist = false;
            this.BookedAmtDetails.SecExcept = null;
            this.BookedAmtDetails.SecFieldName = null;
            this.BookedAmtDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.BookedAmtDetails.SelectPaging = false;
            this.BookedAmtDetails.SelectTop = 0;
            this.BookedAmtDetails.SiteControl = false;
            this.BookedAmtDetails.SiteFieldName = null;
            this.BookedAmtDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glYearBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_glYearBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoucherYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glBudgetType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccItemM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoucherMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccItemS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostCenterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookedAmtDetails)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glYearBudget;
        private Srvtools.UpdateComponent ucglYearBudget;
        private Srvtools.InfoCommand View_glYearBudget;
        private Srvtools.InfoCommand VoucherYear;
        private Srvtools.InfoCommand glBudgetType;
        private Srvtools.InfoCommand AccItemM;
        private Srvtools.InfoCommand VoucherMonth;
        private Srvtools.InfoCommand CostCenter;
        private Srvtools.InfoCommand AccItemS;
        private Srvtools.InfoCommand CostCenterList;
        private Srvtools.InfoCommand BookedAmtDetails;
    }
}
