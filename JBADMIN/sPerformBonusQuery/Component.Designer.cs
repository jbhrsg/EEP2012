namespace sPerformBonusQuery
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
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.PerfBonusDetails = new Srvtools.InfoCommand(this.components);
            this.ucPerfBonusDetails = new Srvtools.UpdateComponent(this.components);
            this.View_PerfBonusDetails = new Srvtools.InfoCommand(this.components);
            this.PerfBonusYM = new Srvtools.InfoCommand(this.components);
            this.OrgParent = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PerfBonusDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusYM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgParent)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // PerfBonusDetails
            // 
            this.PerfBonusDetails.CacheConnection = false;
            this.PerfBonusDetails.CommandText = "SELECT *  FROM dbo.View_PerfBonus\r\nORDER BY Org_NOParent,ApplyEmpID";
            this.PerfBonusDetails.CommandTimeout = 30;
            this.PerfBonusDetails.CommandType = System.Data.CommandType.Text;
            this.PerfBonusDetails.DynamicTableName = false;
            this.PerfBonusDetails.EEPAlias = null;
            this.PerfBonusDetails.EncodingAfter = null;
            this.PerfBonusDetails.EncodingBefore = "Windows-1252";
            this.PerfBonusDetails.EncodingConvert = null;
            this.PerfBonusDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.PerfBonusDetails.KeyFields.Add(keyItem1);
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
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "PerfBonusNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EmpID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "EmpName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "BonusAmt";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AdjustAmt";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "AdjustNote";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "IsActive";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateBy";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
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
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr1);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr2);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr3);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr4);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr5);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr6);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr7);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr8);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr9);
            this.ucPerfBonusDetails.FieldAttrs.Add(fieldAttr10);
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
            // View_PerfBonusDetails
            // 
            this.View_PerfBonusDetails.CacheConnection = false;
            this.View_PerfBonusDetails.CommandText = "SELECT * FROM dbo.[PerfBonusDetails]";
            this.View_PerfBonusDetails.CommandTimeout = 30;
            this.View_PerfBonusDetails.CommandType = System.Data.CommandType.Text;
            this.View_PerfBonusDetails.DynamicTableName = false;
            this.View_PerfBonusDetails.EEPAlias = null;
            this.View_PerfBonusDetails.EncodingAfter = null;
            this.View_PerfBonusDetails.EncodingBefore = "Windows-1252";
            this.View_PerfBonusDetails.EncodingConvert = null;
            this.View_PerfBonusDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.View_PerfBonusDetails.KeyFields.Add(keyItem2);
            this.View_PerfBonusDetails.MultiSetWhere = false;
            this.View_PerfBonusDetails.Name = "View_PerfBonusDetails";
            this.View_PerfBonusDetails.NotificationAutoEnlist = false;
            this.View_PerfBonusDetails.SecExcept = null;
            this.View_PerfBonusDetails.SecFieldName = null;
            this.View_PerfBonusDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_PerfBonusDetails.SelectPaging = false;
            this.View_PerfBonusDetails.SelectTop = 0;
            this.View_PerfBonusDetails.SiteControl = false;
            this.View_PerfBonusDetails.SiteFieldName = null;
            this.View_PerfBonusDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PerfBonusYM
            // 
            this.PerfBonusYM.CacheConnection = false;
            this.PerfBonusYM.CommandText = "SELECT PerfBonusYM   FROM PerfBonusMaster\r\nWhere flowflag=\'Z\' GROUP BY PerfBonusY" +
    "M\r\nORDER BY PerfBonusYM";
            this.PerfBonusYM.CommandTimeout = 30;
            this.PerfBonusYM.CommandType = System.Data.CommandType.Text;
            this.PerfBonusYM.DynamicTableName = false;
            this.PerfBonusYM.EEPAlias = null;
            this.PerfBonusYM.EncodingAfter = null;
            this.PerfBonusYM.EncodingBefore = "Windows-1252";
            this.PerfBonusYM.EncodingConvert = null;
            this.PerfBonusYM.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.PerfBonusYM.KeyFields.Add(keyItem3);
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
            // 
            // OrgParent
            // 
            this.OrgParent.CacheConnection = false;
            this.OrgParent.CommandText = "SELECT Org_NOParent,OrgParentName \r\nFROM PerfBonusMaster Where flowflag=\'Z\'\r\nGROU" +
    "P BY Org_NOParent,OrgParentName\r\nORDER BY Org_NOParent";
            this.OrgParent.CommandTimeout = 30;
            this.OrgParent.CommandType = System.Data.CommandType.Text;
            this.OrgParent.DynamicTableName = false;
            this.OrgParent.EEPAlias = null;
            this.OrgParent.EncodingAfter = null;
            this.OrgParent.EncodingBefore = "Windows-1252";
            this.OrgParent.EncodingConvert = null;
            this.OrgParent.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AutoKey";
            this.OrgParent.KeyFields.Add(keyItem4);
            this.OrgParent.MultiSetWhere = false;
            this.OrgParent.Name = "OrgParent";
            this.OrgParent.NotificationAutoEnlist = false;
            this.OrgParent.SecExcept = null;
            this.OrgParent.SecFieldName = null;
            this.OrgParent.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.OrgParent.SelectPaging = false;
            this.OrgParent.SelectTop = 0;
            this.OrgParent.SiteControl = false;
            this.OrgParent.SiteFieldName = null;
            this.OrgParent.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PerfBonusDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerfBonusYM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgParent)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand PerfBonusDetails;
        private Srvtools.UpdateComponent ucPerfBonusDetails;
        private Srvtools.InfoCommand View_PerfBonusDetails;
        private Srvtools.InfoCommand PerfBonusYM;
        private Srvtools.InfoCommand OrgParent;
    }
}
