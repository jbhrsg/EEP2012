namespace sPerformBonus
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
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.PerfBonusMaster = new Srvtools.InfoCommand(this.components);
            this.ucPerfBonusMaster = new Srvtools.UpdateComponent(this.components);
            this.View_PerfBonusMaster = new Srvtools.InfoCommand(this.components);
            this.PerfBonusDetails = new Srvtools.InfoCommand(this.components);
            this.ucPerfBonusDetails = new Srvtools.UpdateComponent(this.components);
            this.ORG = new Srvtools.InfoCommand(this.components);
            this.EmpList = new Srvtools.InfoCommand(this.components);
            this.PerfBonusImport = new Srvtools.InfoCommand(this.components);
            this.PerfBonusYM = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PerfBonusMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ORG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusYM)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "DataValidate";
            service1.NonLogin = false;
            service1.ServiceName = "DataValidate";
            service2.DelegateName = "ExcelFileImport";
            service2.NonLogin = false;
            service2.ServiceName = "ExcelFileImport";
            service3.DelegateName = "PutBonusAmtToSalary_Enrich";
            service3.NonLogin = false;
            service3.ServiceName = "PutBonusAmtToSalary_Enrich";
            service4.DelegateName = "GetUserOrgNOs";
            service4.NonLogin = false;
            service4.ServiceName = "GetUserOrgNOs";
            service5.DelegateName = "CheckIsDuplicateApply";
            service5.NonLogin = false;
            service5.ServiceName = "CheckIsDuplicateApply";
            service6.DelegateName = "GetAdjustAmt";
            service6.NonLogin = false;
            service6.ServiceName = "GetAdjustAmt";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // PerfBonusMaster
            // 
            this.PerfBonusMaster.CacheConnection = false;
            this.PerfBonusMaster.CommandText = "SELECT dbo.[PerfBonusMaster].* FROM dbo.[PerfBonusMaster]";
            this.PerfBonusMaster.CommandTimeout = 30;
            this.PerfBonusMaster.CommandType = System.Data.CommandType.Text;
            this.PerfBonusMaster.DynamicTableName = false;
            this.PerfBonusMaster.EEPAlias = null;
            this.PerfBonusMaster.EncodingAfter = null;
            this.PerfBonusMaster.EncodingBefore = "Windows-1252";
            this.PerfBonusMaster.EncodingConvert = null;
            this.PerfBonusMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "PerfBonusNO";
            this.PerfBonusMaster.KeyFields.Add(keyItem1);
            this.PerfBonusMaster.MultiSetWhere = false;
            this.PerfBonusMaster.Name = "PerfBonusMaster";
            this.PerfBonusMaster.NotificationAutoEnlist = false;
            this.PerfBonusMaster.SecExcept = null;
            this.PerfBonusMaster.SecFieldName = null;
            this.PerfBonusMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PerfBonusMaster.SelectPaging = false;
            this.PerfBonusMaster.SelectTop = 0;
            this.PerfBonusMaster.SiteControl = false;
            this.PerfBonusMaster.SiteFieldName = null;
            this.PerfBonusMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPerfBonusMaster
            // 
            this.ucPerfBonusMaster.AutoTrans = true;
            this.ucPerfBonusMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "PerfBonusNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "PerfBonusYM";
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
            fieldAttr5.DataField = "Org_NOParent";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SalaryYM";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "RawExcel";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Flowflag";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateBy";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = "_username";
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr1);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr2);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr3);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr4);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr5);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr6);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr7);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr8);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr9);
            this.ucPerfBonusMaster.FieldAttrs.Add(fieldAttr10);
            this.ucPerfBonusMaster.LogInfo = null;
            this.ucPerfBonusMaster.Name = "ucPerfBonusMaster";
            this.ucPerfBonusMaster.RowAffectsCheck = true;
            this.ucPerfBonusMaster.SelectCmd = this.PerfBonusMaster;
            this.ucPerfBonusMaster.SelectCmdForUpdate = null;
            this.ucPerfBonusMaster.SendSQLCmd = true;
            this.ucPerfBonusMaster.ServerModify = true;
            this.ucPerfBonusMaster.ServerModifyGetMax = false;
            this.ucPerfBonusMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPerfBonusMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPerfBonusMaster.UseTranscationScope = false;
            this.ucPerfBonusMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucPerfBonusMaster.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucPerfBonusMaster_BeforeInsert);
            // 
            // View_PerfBonusMaster
            // 
            this.View_PerfBonusMaster.CacheConnection = false;
            this.View_PerfBonusMaster.CommandText = "SELECT * FROM dbo.[PerfBonusMaster]";
            this.View_PerfBonusMaster.CommandTimeout = 30;
            this.View_PerfBonusMaster.CommandType = System.Data.CommandType.Text;
            this.View_PerfBonusMaster.DynamicTableName = false;
            this.View_PerfBonusMaster.EEPAlias = null;
            this.View_PerfBonusMaster.EncodingAfter = null;
            this.View_PerfBonusMaster.EncodingBefore = "Windows-1252";
            this.View_PerfBonusMaster.EncodingConvert = null;
            this.View_PerfBonusMaster.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "PerfBonusNO";
            this.View_PerfBonusMaster.KeyFields.Add(keyItem2);
            this.View_PerfBonusMaster.MultiSetWhere = false;
            this.View_PerfBonusMaster.Name = "View_PerfBonusMaster";
            this.View_PerfBonusMaster.NotificationAutoEnlist = false;
            this.View_PerfBonusMaster.SecExcept = null;
            this.View_PerfBonusMaster.SecFieldName = null;
            this.View_PerfBonusMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_PerfBonusMaster.SelectPaging = false;
            this.View_PerfBonusMaster.SelectTop = 0;
            this.View_PerfBonusMaster.SiteControl = false;
            this.View_PerfBonusMaster.SiteFieldName = null;
            this.View_PerfBonusMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PerfBonusDetails
            // 
            this.PerfBonusDetails.CacheConnection = false;
            this.PerfBonusDetails.CommandText = "SELECT dbo.[PerfBonusDetails].*,\r\nCase IsActive when 0 then \'工號或姓名錯誤\' else \'\' end" +
    " AS ErrorMsg\r\nFROM dbo.[PerfBonusDetails]\r\nORDER BY EMPID";
            this.PerfBonusDetails.CommandTimeout = 30;
            this.PerfBonusDetails.CommandType = System.Data.CommandType.Text;
            this.PerfBonusDetails.DynamicTableName = false;
            this.PerfBonusDetails.EEPAlias = null;
            this.PerfBonusDetails.EncodingAfter = null;
            this.PerfBonusDetails.EncodingBefore = "Windows-1252";
            this.PerfBonusDetails.EncodingConvert = null;
            this.PerfBonusDetails.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.PerfBonusDetails.KeyFields.Add(keyItem3);
            this.PerfBonusDetails.MultiSetWhere = false;
            this.PerfBonusDetails.Name = "PerfBonusDetails";
            this.PerfBonusDetails.NotificationAutoEnlist = false;
            this.PerfBonusDetails.SecExcept = null;
            this.PerfBonusDetails.SecFieldName = null;
            this.PerfBonusDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PerfBonusDetails.SelectPaging = false;
            this.PerfBonusDetails.SelectTop = 0;
            this.PerfBonusDetails.SiteControl = false;
            this.PerfBonusDetails.SiteFieldName = null;
            this.PerfBonusDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPerfBonusDetails
            // 
            this.ucPerfBonusDetails.AutoTrans = true;
            this.ucPerfBonusDetails.ExceptJoin = false;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "PerfBonusNO";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "EmpID";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "EmpName";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "BonusAmt";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "AdjustAmt";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "AdjustNote";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CreateBy";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "CreateDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr11);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr12);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr13);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr14);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr15);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr16);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr17);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr18);
            this.ucPerfBonusDetails.LogInfo = null;
            this.ucPerfBonusDetails.Name = "ucPerfBonusDetails";
            this.ucPerfBonusDetails.RowAffectsCheck = true;
            this.ucPerfBonusDetails.SelectCmd = this.PerfBonusDetails;
            this.ucPerfBonusDetails.SelectCmdForUpdate = null;
            this.ucPerfBonusDetails.SendSQLCmd = true;
            this.ucPerfBonusDetails.ServerModify = true;
            this.ucPerfBonusDetails.ServerModifyGetMax = false;
            this.ucPerfBonusDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPerfBonusDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPerfBonusDetails.UseTranscationScope = false;
            this.ucPerfBonusDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ORG
            // 
            this.ORG.CacheConnection = false;
            this.ORG.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nWHERE (Uppe" +
    "r_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR ORG_NO=\'99999\')\r\nORDER" +
    " BY ORG_NO";
            this.ORG.CommandTimeout = 30;
            this.ORG.CommandType = System.Data.CommandType.Text;
            this.ORG.DynamicTableName = false;
            this.ORG.EEPAlias = null;
            this.ORG.EncodingAfter = null;
            this.ORG.EncodingBefore = "Windows-1252";
            this.ORG.EncodingConvert = null;
            this.ORG.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "USERID";
            this.ORG.KeyFields.Add(keyItem4);
            this.ORG.MultiSetWhere = false;
            this.ORG.Name = "ORG";
            this.ORG.NotificationAutoEnlist = false;
            this.ORG.SecExcept = null;
            this.ORG.SecFieldName = null;
            this.ORG.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ORG.SelectPaging = false;
            this.ORG.SelectTop = 0;
            this.ORG.SiteControl = false;
            this.ORG.SiteFieldName = null;
            this.ORG.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // EmpList
            // 
            this.EmpList.CacheConnection = false;
            this.EmpList.CommandText = "SELECT USERID,USERNAME,DESCRIPTION FROM EIPHRSYS.DBO.USERS\r\nWHERE USERID<>\'000\' O" +
    "RDER BY USERID";
            this.EmpList.CommandTimeout = 30;
            this.EmpList.CommandType = System.Data.CommandType.Text;
            this.EmpList.DynamicTableName = false;
            this.EmpList.EEPAlias = null;
            this.EmpList.EncodingAfter = null;
            this.EmpList.EncodingBefore = "Windows-1252";
            this.EmpList.EncodingConvert = null;
            this.EmpList.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "USERID";
            this.EmpList.KeyFields.Add(keyItem5);
            this.EmpList.MultiSetWhere = false;
            this.EmpList.Name = "EmpList";
            this.EmpList.NotificationAutoEnlist = false;
            this.EmpList.SecExcept = null;
            this.EmpList.SecFieldName = null;
            this.EmpList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EmpList.SelectPaging = false;
            this.EmpList.SelectTop = 0;
            this.EmpList.SiteControl = false;
            this.EmpList.SiteFieldName = null;
            this.EmpList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PerfBonusImport
            // 
            this.PerfBonusImport.CacheConnection = false;
            this.PerfBonusImport.CommandText = "Select   * From PerfBonusImport";
            this.PerfBonusImport.CommandTimeout = 30;
            this.PerfBonusImport.CommandType = System.Data.CommandType.Text;
            this.PerfBonusImport.DynamicTableName = false;
            this.PerfBonusImport.EEPAlias = null;
            this.PerfBonusImport.EncodingAfter = null;
            this.PerfBonusImport.EncodingBefore = "Windows-1252";
            this.PerfBonusImport.EncodingConvert = null;
            this.PerfBonusImport.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "PerfBonusNO";
            this.PerfBonusImport.KeyFields.Add(keyItem6);
            this.PerfBonusImport.MultiSetWhere = false;
            this.PerfBonusImport.Name = "PerfBonusImport";
            this.PerfBonusImport.NotificationAutoEnlist = false;
            this.PerfBonusImport.SecExcept = null;
            this.PerfBonusImport.SecFieldName = null;
            this.PerfBonusImport.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PerfBonusImport.SelectPaging = false;
            this.PerfBonusImport.SelectTop = 0;
            this.PerfBonusImport.SiteControl = false;
            this.PerfBonusImport.SiteFieldName = null;
            this.PerfBonusImport.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PerfBonusYM
            // 
            this.PerfBonusYM.CacheConnection = false;
            this.PerfBonusYM.CommandText = "SELECT  PerfBonusYM,SalaryYM    FROM PerfBonusYM\r\nORDER BY PerfBonusYM";
            this.PerfBonusYM.CommandTimeout = 30;
            this.PerfBonusYM.CommandType = System.Data.CommandType.Text;
            this.PerfBonusYM.DynamicTableName = false;
            this.PerfBonusYM.EEPAlias = null;
            this.PerfBonusYM.EncodingAfter = null;
            this.PerfBonusYM.EncodingBefore = "Windows-1252";
            this.PerfBonusYM.EncodingConvert = null;
            this.PerfBonusYM.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "USERID";
            this.PerfBonusYM.KeyFields.Add(keyItem7);
            this.PerfBonusYM.MultiSetWhere = false;
            this.PerfBonusYM.Name = "PerfBonusYM";
            this.PerfBonusYM.NotificationAutoEnlist = false;
            this.PerfBonusYM.SecExcept = null;
            this.PerfBonusYM.SecFieldName = null;
            this.PerfBonusYM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PerfBonusYM.SelectPaging = false;
            this.PerfBonusYM.SelectTop = 0;
            this.PerfBonusYM.SiteControl = false;
            this.PerfBonusYM.SiteFieldName = null;
            this.PerfBonusYM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PerfBonusMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ORG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusYM)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand PerfBonusMaster;
        private Srvtools.UpdateComponent ucPerfBonusMaster;
        private Srvtools.InfoCommand View_PerfBonusMaster;
        private Srvtools.InfoCommand PerfBonusDetails;
        private Srvtools.UpdateComponent ucPerfBonusDetails;
        private Srvtools.InfoCommand ORG;
        private Srvtools.InfoCommand EmpList;
        private Srvtools.InfoCommand PerfBonusImport;
        private Srvtools.InfoCommand PerfBonusYM;
    }
}
