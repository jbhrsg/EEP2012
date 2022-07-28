namespace sglAccountItem
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glAccountClass = new Srvtools.InfoCommand(this.components);
            this.ucglAccountClass = new Srvtools.UpdateComponent(this.components);
            this.glAccountItem = new Srvtools.InfoCommand(this.components);
            this.ucglAccountItem = new Srvtools.UpdateComponent(this.components);
            this.glAccountBalanceSheet = new Srvtools.InfoCommand(this.components);
            this.ucglAccountBalanceSheet = new Srvtools.UpdateComponent(this.components);
            this.glCompanyAll = new Srvtools.InfoCommand(this.components);
            this.infoExternalAcno = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glAccountClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glAccountItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glAccountBalanceSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCompanyAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoExternalAcno)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckMasterDelete";
            service1.NonLogin = false;
            service1.ServiceName = "CheckMasterDelete";
            service2.DelegateName = "ReturnAcnoSubAcnoCount";
            service2.NonLogin = false;
            service2.ServiceName = "ReturnAcnoSubAcnoCount";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // glAccountClass
            // 
            this.glAccountClass.CacheConnection = false;
            this.glAccountClass.CommandText = "SELECT c.*,s.BalanceSheetName\r\nFROM [glAccountClass] c\r\n\tinner join glAccountBala" +
    "nceSheet s on c.BalanceSheetID=s.BalanceSheetID\r\norder by c.BalanceSheetID,c.Cla" +
    "ssID";
            this.glAccountClass.CommandTimeout = 30;
            this.glAccountClass.CommandType = System.Data.CommandType.Text;
            this.glAccountClass.DynamicTableName = false;
            this.glAccountClass.EEPAlias = "";
            this.glAccountClass.EncodingAfter = null;
            this.glAccountClass.EncodingBefore = "Windows-1252";
            this.glAccountClass.EncodingConvert = null;
            this.glAccountClass.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.glAccountClass.KeyFields.Add(keyItem1);
            this.glAccountClass.MultiSetWhere = false;
            this.glAccountClass.Name = "glAccountClass";
            this.glAccountClass.NotificationAutoEnlist = false;
            this.glAccountClass.SecExcept = null;
            this.glAccountClass.SecFieldName = null;
            this.glAccountClass.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glAccountClass.SelectPaging = false;
            this.glAccountClass.SelectTop = 0;
            this.glAccountClass.SiteControl = false;
            this.glAccountClass.SiteFieldName = null;
            this.glAccountClass.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglAccountClass
            // 
            this.ucglAccountClass.AutoTrans = true;
            this.ucglAccountClass.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "BalanceSheetID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ClassID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CompanyID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ClassName";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AcnoSNo";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AcnoENo";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucglAccountClass.FieldAttrs.Add(fieldAttr1);
            this.ucglAccountClass.FieldAttrs.Add(fieldAttr2);
            this.ucglAccountClass.FieldAttrs.Add(fieldAttr3);
            this.ucglAccountClass.FieldAttrs.Add(fieldAttr4);
            this.ucglAccountClass.FieldAttrs.Add(fieldAttr5);
            this.ucglAccountClass.FieldAttrs.Add(fieldAttr6);
            this.ucglAccountClass.FieldAttrs.Add(fieldAttr7);
            this.ucglAccountClass.LogInfo = null;
            this.ucglAccountClass.Name = "ucglAccountClass";
            this.ucglAccountClass.RowAffectsCheck = true;
            this.ucglAccountClass.SelectCmd = this.glAccountClass;
            this.ucglAccountClass.SelectCmdForUpdate = null;
            this.ucglAccountClass.SendSQLCmd = true;
            this.ucglAccountClass.ServerModify = true;
            this.ucglAccountClass.ServerModifyGetMax = false;
            this.ucglAccountClass.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglAccountClass.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglAccountClass.UseTranscationScope = false;
            this.ucglAccountClass.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // glAccountItem
            // 
            this.glAccountItem.CacheConnection = false;
            this.glAccountItem.CommandText = "select a.*,c.CompanyName\r\nfrom glAccountItem a\r\n\tinner join glCompany c on a.Comp" +
    "anyID=c.CompanyID\r\norder by a.acno,a.SubAcno";
            this.glAccountItem.CommandTimeout = 30;
            this.glAccountItem.CommandType = System.Data.CommandType.Text;
            this.glAccountItem.DynamicTableName = false;
            this.glAccountItem.EEPAlias = "";
            this.glAccountItem.EncodingAfter = null;
            this.glAccountItem.EncodingBefore = "Windows-1252";
            this.glAccountItem.EncodingConvert = null;
            this.glAccountItem.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            keyItem3.KeyName = "CompanyID1";
            this.glAccountItem.KeyFields.Add(keyItem2);
            this.glAccountItem.KeyFields.Add(keyItem3);
            this.glAccountItem.MultiSetWhere = false;
            this.glAccountItem.Name = "glAccountItem";
            this.glAccountItem.NotificationAutoEnlist = false;
            this.glAccountItem.SecExcept = null;
            this.glAccountItem.SecFieldName = null;
            this.glAccountItem.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glAccountItem.SelectPaging = false;
            this.glAccountItem.SelectTop = 0;
            this.glAccountItem.SiteControl = false;
            this.glAccountItem.SiteFieldName = null;
            this.glAccountItem.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglAccountItem
            // 
            this.ucglAccountItem.AutoTrans = true;
            this.ucglAccountItem.ExceptJoin = false;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "AutoKey";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ClassID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CompanyID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Acno";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "SubAcno";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "AcnoName";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucglAccountItem.FieldAttrs.Add(fieldAttr8);
            this.ucglAccountItem.FieldAttrs.Add(fieldAttr9);
            this.ucglAccountItem.FieldAttrs.Add(fieldAttr10);
            this.ucglAccountItem.FieldAttrs.Add(fieldAttr11);
            this.ucglAccountItem.FieldAttrs.Add(fieldAttr12);
            this.ucglAccountItem.FieldAttrs.Add(fieldAttr13);
            this.ucglAccountItem.LogInfo = null;
            this.ucglAccountItem.Name = "ucglAccountItem";
            this.ucglAccountItem.RowAffectsCheck = true;
            this.ucglAccountItem.SelectCmd = this.glAccountItem;
            this.ucglAccountItem.SelectCmdForUpdate = null;
            this.ucglAccountItem.SendSQLCmd = true;
            this.ucglAccountItem.ServerModify = true;
            this.ucglAccountItem.ServerModifyGetMax = false;
            this.ucglAccountItem.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglAccountItem.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglAccountItem.UseTranscationScope = false;
            this.ucglAccountItem.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // glAccountBalanceSheet
            // 
            this.glAccountBalanceSheet.CacheConnection = false;
            this.glAccountBalanceSheet.CommandText = "SELECT dbo.[glAccountBalanceSheet].* FROM dbo.[glAccountBalanceSheet]";
            this.glAccountBalanceSheet.CommandTimeout = 30;
            this.glAccountBalanceSheet.CommandType = System.Data.CommandType.Text;
            this.glAccountBalanceSheet.DynamicTableName = false;
            this.glAccountBalanceSheet.EEPAlias = "";
            this.glAccountBalanceSheet.EncodingAfter = null;
            this.glAccountBalanceSheet.EncodingBefore = "Windows-1252";
            this.glAccountBalanceSheet.EncodingConvert = null;
            this.glAccountBalanceSheet.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AutoKey";
            this.glAccountBalanceSheet.KeyFields.Add(keyItem4);
            this.glAccountBalanceSheet.MultiSetWhere = false;
            this.glAccountBalanceSheet.Name = "glAccountBalanceSheet";
            this.glAccountBalanceSheet.NotificationAutoEnlist = false;
            this.glAccountBalanceSheet.SecExcept = null;
            this.glAccountBalanceSheet.SecFieldName = null;
            this.glAccountBalanceSheet.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glAccountBalanceSheet.SelectPaging = false;
            this.glAccountBalanceSheet.SelectTop = 0;
            this.glAccountBalanceSheet.SiteControl = false;
            this.glAccountBalanceSheet.SiteFieldName = null;
            this.glAccountBalanceSheet.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglAccountBalanceSheet
            // 
            this.ucglAccountBalanceSheet.AutoTrans = true;
            this.ucglAccountBalanceSheet.ExceptJoin = false;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "AutoKey";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "BalanceSheetID";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "BalanceSheetName";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            this.ucglAccountBalanceSheet.FieldAttrs.Add(fieldAttr14);
            this.ucglAccountBalanceSheet.FieldAttrs.Add(fieldAttr15);
            this.ucglAccountBalanceSheet.FieldAttrs.Add(fieldAttr16);
            this.ucglAccountBalanceSheet.LogInfo = null;
            this.ucglAccountBalanceSheet.Name = "ucglAccountBalanceSheet";
            this.ucglAccountBalanceSheet.RowAffectsCheck = true;
            this.ucglAccountBalanceSheet.SelectCmd = this.glAccountBalanceSheet;
            this.ucglAccountBalanceSheet.SelectCmdForUpdate = null;
            this.ucglAccountBalanceSheet.SendSQLCmd = true;
            this.ucglAccountBalanceSheet.ServerModify = true;
            this.ucglAccountBalanceSheet.ServerModifyGetMax = false;
            this.ucglAccountBalanceSheet.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglAccountBalanceSheet.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglAccountBalanceSheet.UseTranscationScope = false;
            this.ucglAccountBalanceSheet.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // glCompanyAll
            // 
            this.glCompanyAll.CacheConnection = false;
            this.glCompanyAll.CommandText = "Select 0 as CompanyID,\'--請選擇--\' as CompanyName\r\nunion all\r\nSELECT CompanyID,Compa" +
    "nyName\r\nFROM glCompany\r\norder by CompanyID";
            this.glCompanyAll.CommandTimeout = 30;
            this.glCompanyAll.CommandType = System.Data.CommandType.Text;
            this.glCompanyAll.DynamicTableName = false;
            this.glCompanyAll.EEPAlias = "";
            this.glCompanyAll.EncodingAfter = null;
            this.glCompanyAll.EncodingBefore = "Windows-1252";
            this.glCompanyAll.EncodingConvert = null;
            this.glCompanyAll.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "CompanyID";
            this.glCompanyAll.KeyFields.Add(keyItem5);
            this.glCompanyAll.MultiSetWhere = false;
            this.glCompanyAll.Name = "glCompanyAll";
            this.glCompanyAll.NotificationAutoEnlist = false;
            this.glCompanyAll.SecExcept = null;
            this.glCompanyAll.SecFieldName = null;
            this.glCompanyAll.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glCompanyAll.SelectPaging = false;
            this.glCompanyAll.SelectTop = 0;
            this.glCompanyAll.SiteControl = false;
            this.glCompanyAll.SiteFieldName = null;
            this.glCompanyAll.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoExternalAcno
            // 
            this.infoExternalAcno.CacheConnection = false;
            this.infoExternalAcno.CommandText = "select distinct a.Acno,a.AcnoName\r\nfrom glAccountItem a\r\n\tinner join glCompany c " +
    "on a.CompanyID=c.CompanyID\r\n\twhere a.CompanyID=2\r\norder by a.acno";
            this.infoExternalAcno.CommandTimeout = 30;
            this.infoExternalAcno.CommandType = System.Data.CommandType.Text;
            this.infoExternalAcno.DynamicTableName = false;
            this.infoExternalAcno.EEPAlias = "";
            this.infoExternalAcno.EncodingAfter = null;
            this.infoExternalAcno.EncodingBefore = "Windows-1252";
            this.infoExternalAcno.EncodingConvert = null;
            this.infoExternalAcno.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "AutoKey";
            keyItem7.KeyName = "CompanyID1";
            this.infoExternalAcno.KeyFields.Add(keyItem6);
            this.infoExternalAcno.KeyFields.Add(keyItem7);
            this.infoExternalAcno.MultiSetWhere = false;
            this.infoExternalAcno.Name = "infoExternalAcno";
            this.infoExternalAcno.NotificationAutoEnlist = false;
            this.infoExternalAcno.SecExcept = null;
            this.infoExternalAcno.SecFieldName = null;
            this.infoExternalAcno.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoExternalAcno.SelectPaging = false;
            this.infoExternalAcno.SelectTop = 0;
            this.infoExternalAcno.SiteControl = false;
            this.infoExternalAcno.SiteFieldName = null;
            this.infoExternalAcno.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glAccountClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glAccountItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glAccountBalanceSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCompanyAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoExternalAcno)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glAccountClass;
        private Srvtools.UpdateComponent ucglAccountClass;
        private Srvtools.InfoCommand glAccountItem;
        private Srvtools.UpdateComponent ucglAccountItem;
        private Srvtools.InfoCommand glAccountBalanceSheet;
        private Srvtools.UpdateComponent ucglAccountBalanceSheet;
        private Srvtools.InfoCommand glCompanyAll;
        private Srvtools.InfoCommand infoExternalAcno;
    }
}
