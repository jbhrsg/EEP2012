namespace sglCompany
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glCompany = new Srvtools.InfoCommand(this.components);
            this.ucglCompany = new Srvtools.UpdateComponent(this.components);
            this.glCostCenter = new Srvtools.InfoCommand(this.components);
            this.ucglCostCenter = new Srvtools.UpdateComponent(this.components);
            this.glCostCenterUser = new Srvtools.InfoCommand(this.components);
            this.ucglCostCenterUser = new Srvtools.UpdateComponent(this.components);
            this.idglCostCenter_glCostCenterUser = new Srvtools.InfoDataSource(this.components);
            this.infoUSERS = new Srvtools.InfoCommand(this.components);
            this.infoERPIndustryType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCostCenterUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoUSERS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPIndustryType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetCostCenter";
            service1.NonLogin = false;
            service1.ServiceName = "GetCostCenter";
            service2.DelegateName = "checkLockYM";
            service2.NonLogin = false;
            service2.ServiceName = "checkLockYM";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // glCompany
            // 
            this.glCompany.CacheConnection = false;
            this.glCompany.CommandText = "SELECT dbo.[glCompany].* FROM dbo.[glCompany]";
            this.glCompany.CommandTimeout = 30;
            this.glCompany.CommandType = System.Data.CommandType.Text;
            this.glCompany.DynamicTableName = false;
            this.glCompany.EEPAlias = "";
            this.glCompany.EncodingAfter = null;
            this.glCompany.EncodingBefore = "Windows-1252";
            this.glCompany.EncodingConvert = null;
            this.glCompany.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CompanyID";
            this.glCompany.KeyFields.Add(keyItem1);
            this.glCompany.MultiSetWhere = false;
            this.glCompany.Name = "glCompany";
            this.glCompany.NotificationAutoEnlist = false;
            this.glCompany.SecExcept = null;
            this.glCompany.SecFieldName = null;
            this.glCompany.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glCompany.SelectPaging = false;
            this.glCompany.SelectTop = 0;
            this.glCompany.SiteControl = false;
            this.glCompany.SiteFieldName = null;
            this.glCompany.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglCompany
            // 
            this.ucglCompany.AutoTrans = true;
            this.ucglCompany.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CompanyID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CompanyName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "Synchronize";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "LastUpdateBy";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "LastUpdateDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            this.ucglCompany.FieldAttrs.Add(fieldAttr1);
            this.ucglCompany.FieldAttrs.Add(fieldAttr2);
            this.ucglCompany.FieldAttrs.Add(fieldAttr3);
            this.ucglCompany.FieldAttrs.Add(fieldAttr4);
            this.ucglCompany.FieldAttrs.Add(fieldAttr5);
            this.ucglCompany.LogInfo = null;
            this.ucglCompany.Name = "ucglCompany";
            this.ucglCompany.RowAffectsCheck = true;
            this.ucglCompany.SelectCmd = this.glCompany;
            this.ucglCompany.SelectCmdForUpdate = null;
            this.ucglCompany.SendSQLCmd = true;
            this.ucglCompany.ServerModify = true;
            this.ucglCompany.ServerModifyGetMax = false;
            this.ucglCompany.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglCompany.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglCompany.UseTranscationScope = false;
            this.ucglCompany.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // glCostCenter
            // 
            this.glCostCenter.CacheConnection = false;
            this.glCostCenter.CommandText = "SELECT dbo.[glCostCenter].* FROM dbo.[glCostCenter]";
            this.glCostCenter.CommandTimeout = 30;
            this.glCostCenter.CommandType = System.Data.CommandType.Text;
            this.glCostCenter.DynamicTableName = false;
            this.glCostCenter.EEPAlias = null;
            this.glCostCenter.EncodingAfter = null;
            this.glCostCenter.EncodingBefore = "Windows-1252";
            this.glCostCenter.EncodingConvert = null;
            this.glCostCenter.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.glCostCenter.KeyFields.Add(keyItem2);
            this.glCostCenter.MultiSetWhere = false;
            this.glCostCenter.Name = "glCostCenter";
            this.glCostCenter.NotificationAutoEnlist = false;
            this.glCostCenter.SecExcept = null;
            this.glCostCenter.SecFieldName = null;
            this.glCostCenter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glCostCenter.SelectPaging = false;
            this.glCostCenter.SelectTop = 0;
            this.glCostCenter.SiteControl = false;
            this.glCostCenter.SiteFieldName = null;
            this.glCostCenter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglCostCenter
            // 
            this.ucglCostCenter.AutoTrans = true;
            this.ucglCostCenter.ExceptJoin = false;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AutoKey";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CostCenterID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CostCenterName";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "AuthorUsers";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "SortID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            this.ucglCostCenter.FieldAttrs.Add(fieldAttr6);
            this.ucglCostCenter.FieldAttrs.Add(fieldAttr7);
            this.ucglCostCenter.FieldAttrs.Add(fieldAttr8);
            this.ucglCostCenter.FieldAttrs.Add(fieldAttr9);
            this.ucglCostCenter.FieldAttrs.Add(fieldAttr10);
            this.ucglCostCenter.LogInfo = null;
            this.ucglCostCenter.Name = "ucglCostCenter";
            this.ucglCostCenter.RowAffectsCheck = true;
            this.ucglCostCenter.SelectCmd = this.glCostCenter;
            this.ucglCostCenter.SelectCmdForUpdate = null;
            this.ucglCostCenter.SendSQLCmd = true;
            this.ucglCostCenter.ServerModify = true;
            this.ucglCostCenter.ServerModifyGetMax = false;
            this.ucglCostCenter.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglCostCenter.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglCostCenter.UseTranscationScope = false;
            this.ucglCostCenter.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // glCostCenterUser
            // 
            this.glCostCenterUser.CacheConnection = false;
            this.glCostCenterUser.CommandText = "select *,u.USERNAME from glCostCenterUser g\r\n\tinner join [EIPHRSYS].dbo.USERS u o" +
    "n g.userid=u.USERID\r\nwhere u.DESCRIPTION=\'JB\'";
            this.glCostCenterUser.CommandTimeout = 30;
            this.glCostCenterUser.CommandType = System.Data.CommandType.Text;
            this.glCostCenterUser.DynamicTableName = false;
            this.glCostCenterUser.EEPAlias = null;
            this.glCostCenterUser.EncodingAfter = null;
            this.glCostCenterUser.EncodingBefore = "Windows-1252";
            this.glCostCenterUser.EncodingConvert = null;
            this.glCostCenterUser.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.glCostCenterUser.KeyFields.Add(keyItem3);
            this.glCostCenterUser.MultiSetWhere = false;
            this.glCostCenterUser.Name = "glCostCenterUser";
            this.glCostCenterUser.NotificationAutoEnlist = false;
            this.glCostCenterUser.SecExcept = null;
            this.glCostCenterUser.SecFieldName = null;
            this.glCostCenterUser.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glCostCenterUser.SelectPaging = false;
            this.glCostCenterUser.SelectTop = 0;
            this.glCostCenterUser.SiteControl = false;
            this.glCostCenterUser.SiteFieldName = null;
            this.glCostCenterUser.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglCostCenterUser
            // 
            this.ucglCostCenterUser.AutoTrans = true;
            this.ucglCostCenterUser.ExceptJoin = false;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "AutoKey";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CostCenterID";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "UserID";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpdateBy";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            this.ucglCostCenterUser.FieldAttrs.Add(fieldAttr11);
            this.ucglCostCenterUser.FieldAttrs.Add(fieldAttr12);
            this.ucglCostCenterUser.FieldAttrs.Add(fieldAttr13);
            this.ucglCostCenterUser.FieldAttrs.Add(fieldAttr14);
            this.ucglCostCenterUser.FieldAttrs.Add(fieldAttr15);
            this.ucglCostCenterUser.LogInfo = null;
            this.ucglCostCenterUser.Name = "ucglCostCenterUser";
            this.ucglCostCenterUser.RowAffectsCheck = true;
            this.ucglCostCenterUser.SelectCmd = this.glCostCenterUser;
            this.ucglCostCenterUser.SelectCmdForUpdate = null;
            this.ucglCostCenterUser.SendSQLCmd = true;
            this.ucglCostCenterUser.ServerModify = true;
            this.ucglCostCenterUser.ServerModifyGetMax = false;
            this.ucglCostCenterUser.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglCostCenterUser.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglCostCenterUser.UseTranscationScope = false;
            this.ucglCostCenterUser.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucglCostCenterUser.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucglCostCenterUser_BeforeInsert);
            this.ucglCostCenterUser.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucglCostCenterUser_BeforeModify);
            // 
            // idglCostCenter_glCostCenterUser
            // 
            this.idglCostCenter_glCostCenterUser.Detail = this.glCostCenterUser;
            columnItem1.FieldName = "CostCenterID";
            this.idglCostCenter_glCostCenterUser.DetailColumns.Add(columnItem1);
            this.idglCostCenter_glCostCenterUser.DynamicTableName = false;
            this.idglCostCenter_glCostCenterUser.Master = this.glCostCenter;
            columnItem2.FieldName = "CostCenterID";
            this.idglCostCenter_glCostCenterUser.MasterColumns.Add(columnItem2);
            // 
            // infoUSERS
            // 
            this.infoUSERS.CacheConnection = false;
            this.infoUSERS.CommandText = "select USERID,USERNAME from [EIPHRSYS].dbo.USERS where DESCRIPTION=\'JB\'";
            this.infoUSERS.CommandTimeout = 30;
            this.infoUSERS.CommandType = System.Data.CommandType.Text;
            this.infoUSERS.DynamicTableName = false;
            this.infoUSERS.EEPAlias = null;
            this.infoUSERS.EncodingAfter = null;
            this.infoUSERS.EncodingBefore = "Windows-1252";
            this.infoUSERS.EncodingConvert = null;
            this.infoUSERS.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "USERID";
            this.infoUSERS.KeyFields.Add(keyItem4);
            this.infoUSERS.MultiSetWhere = false;
            this.infoUSERS.Name = "infoUSERS";
            this.infoUSERS.NotificationAutoEnlist = false;
            this.infoUSERS.SecExcept = null;
            this.infoUSERS.SecFieldName = null;
            this.infoUSERS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoUSERS.SelectPaging = false;
            this.infoUSERS.SelectTop = 0;
            this.infoUSERS.SiteControl = false;
            this.infoUSERS.SiteFieldName = null;
            this.infoUSERS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoERPIndustryType
            // 
            this.infoERPIndustryType.CacheConnection = false;
            this.infoERPIndustryType.CommandText = "SELECT jb_type,substring(jb_name,PATINDEX(\'%.%\',jb_name)+1,len(jb_name)) as jb_na" +
    "me\r\nFROM ERPIndustryType\r\norder by  substring(jb_name,PATINDEX(\'%.%\',jb_name)+1," +
    "len(jb_name)) ";
            this.infoERPIndustryType.CommandTimeout = 30;
            this.infoERPIndustryType.CommandType = System.Data.CommandType.Text;
            this.infoERPIndustryType.DynamicTableName = false;
            this.infoERPIndustryType.EEPAlias = "";
            this.infoERPIndustryType.EncodingAfter = null;
            this.infoERPIndustryType.EncodingBefore = "Windows-1252";
            this.infoERPIndustryType.EncodingConvert = null;
            this.infoERPIndustryType.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "jb_type";
            this.infoERPIndustryType.KeyFields.Add(keyItem5);
            this.infoERPIndustryType.MultiSetWhere = false;
            this.infoERPIndustryType.Name = "infoERPIndustryType";
            this.infoERPIndustryType.NotificationAutoEnlist = false;
            this.infoERPIndustryType.SecExcept = null;
            this.infoERPIndustryType.SecFieldName = null;
            this.infoERPIndustryType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoERPIndustryType.SelectPaging = false;
            this.infoERPIndustryType.SelectTop = 0;
            this.infoERPIndustryType.SiteControl = false;
            this.infoERPIndustryType.SiteFieldName = null;
            this.infoERPIndustryType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glCostCenterUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoUSERS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoERPIndustryType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glCompany;
        private Srvtools.UpdateComponent ucglCompany;
        private Srvtools.InfoCommand glCostCenter;
        private Srvtools.UpdateComponent ucglCostCenter;
        private Srvtools.InfoCommand glCostCenterUser;
        private Srvtools.UpdateComponent ucglCostCenterUser;
        private Srvtools.InfoDataSource idglCostCenter_glCostCenterUser;
        private Srvtools.InfoCommand infoUSERS;
        private Srvtools.InfoCommand infoERPIndustryType;
    }
}
