namespace sCashTakeBackQuery
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.CashTakeBackDetails = new Srvtools.InfoCommand(this.components);
            this.ucCashTakeBackDetails = new Srvtools.UpdateComponent(this.components);
            this.View_CashTakeBackDetails = new Srvtools.InfoCommand(this.components);
            this.Applyer = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            this.CashAgainBillType = new Srvtools.InfoCommand(this.components);
            this.Status = new Srvtools.InfoCommand(this.components);
            this.CashTakeBackType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CashTakeBackDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Applyer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashAgainBillType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackType)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // CashTakeBackDetails
            // 
            this.CashTakeBackDetails.CacheConnection = false;
            this.CashTakeBackDetails.CommandText = resources.GetString("CashTakeBackDetails.CommandText");
            this.CashTakeBackDetails.CommandTimeout = 30;
            this.CashTakeBackDetails.CommandType = System.Data.CommandType.Text;
            this.CashTakeBackDetails.DynamicTableName = false;
            this.CashTakeBackDetails.EEPAlias = null;
            this.CashTakeBackDetails.EncodingAfter = null;
            this.CashTakeBackDetails.EncodingBefore = "Windows-1252";
            this.CashTakeBackDetails.EncodingConvert = null;
            this.CashTakeBackDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CashTakeBackNO";
            keyItem2.KeyName = "ItemNO";
            this.CashTakeBackDetails.KeyFields.Add(keyItem1);
            this.CashTakeBackDetails.KeyFields.Add(keyItem2);
            this.CashTakeBackDetails.MultiSetWhere = false;
            this.CashTakeBackDetails.Name = "CashTakeBackDetails";
            this.CashTakeBackDetails.NotificationAutoEnlist = false;
            this.CashTakeBackDetails.SecExcept = null;
            this.CashTakeBackDetails.SecFieldName = null;
            this.CashTakeBackDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CashTakeBackDetails.SelectPaging = false;
            this.CashTakeBackDetails.SelectTop = 0;
            this.CashTakeBackDetails.SiteControl = false;
            this.CashTakeBackDetails.SiteFieldName = null;
            this.CashTakeBackDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucCashTakeBackDetails
            // 
            this.ucCashTakeBackDetails.AutoTrans = true;
            this.ucCashTakeBackDetails.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CashTakeBackNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ItemNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ShortTermNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ShortTermGist";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Currency";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Amount";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CashType";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr1);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr2);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr3);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr4);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr5);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr6);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr7);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr8);
            this.ucCashTakeBackDetails.FieldAttrs.Add(fieldAttr9);
            this.ucCashTakeBackDetails.LogInfo = null;
            this.ucCashTakeBackDetails.Name = "ucCashTakeBackDetails";
            this.ucCashTakeBackDetails.RowAffectsCheck = true;
            this.ucCashTakeBackDetails.SelectCmd = this.CashTakeBackDetails;
            this.ucCashTakeBackDetails.SelectCmdForUpdate = null;
            this.ucCashTakeBackDetails.SendSQLCmd = true;
            this.ucCashTakeBackDetails.ServerModify = true;
            this.ucCashTakeBackDetails.ServerModifyGetMax = false;
            this.ucCashTakeBackDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucCashTakeBackDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucCashTakeBackDetails.UseTranscationScope = false;
            this.ucCashTakeBackDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_CashTakeBackDetails
            // 
            this.View_CashTakeBackDetails.CacheConnection = false;
            this.View_CashTakeBackDetails.CommandText = "SELECT * FROM dbo.[CashTakeBackDetails]";
            this.View_CashTakeBackDetails.CommandTimeout = 30;
            this.View_CashTakeBackDetails.CommandType = System.Data.CommandType.Text;
            this.View_CashTakeBackDetails.DynamicTableName = false;
            this.View_CashTakeBackDetails.EEPAlias = null;
            this.View_CashTakeBackDetails.EncodingAfter = null;
            this.View_CashTakeBackDetails.EncodingBefore = "Windows-1252";
            this.View_CashTakeBackDetails.EncodingConvert = null;
            this.View_CashTakeBackDetails.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CashTakeBackNO";
            keyItem4.KeyName = "ItemNO";
            this.View_CashTakeBackDetails.KeyFields.Add(keyItem3);
            this.View_CashTakeBackDetails.KeyFields.Add(keyItem4);
            this.View_CashTakeBackDetails.MultiSetWhere = false;
            this.View_CashTakeBackDetails.Name = "View_CashTakeBackDetails";
            this.View_CashTakeBackDetails.NotificationAutoEnlist = false;
            this.View_CashTakeBackDetails.SecExcept = null;
            this.View_CashTakeBackDetails.SecFieldName = null;
            this.View_CashTakeBackDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_CashTakeBackDetails.SelectPaging = false;
            this.View_CashTakeBackDetails.SelectTop = 0;
            this.View_CashTakeBackDetails.SiteControl = false;
            this.View_CashTakeBackDetails.SiteFieldName = null;
            this.View_CashTakeBackDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Applyer
            // 
            this.Applyer.CacheConnection = false;
            this.Applyer.CommandText = "SELECT * FROM View_Employee\r\nWHERE EMPLOYEEID IN \r\n(SELECT  DISTINCT  ApplyEmpID\r" +
    "\n FROM  CashTakeBackMaster)\r\nORDER BY EMPLOYEEID\r\n";
            this.Applyer.CommandTimeout = 30;
            this.Applyer.CommandType = System.Data.CommandType.Text;
            this.Applyer.DynamicTableName = false;
            this.Applyer.EEPAlias = null;
            this.Applyer.EncodingAfter = null;
            this.Applyer.EncodingBefore = "Windows-1252";
            this.Applyer.EncodingConvert = null;
            this.Applyer.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "EMPLOYEEID";
            this.Applyer.KeyFields.Add(keyItem5);
            this.Applyer.MultiSetWhere = false;
            this.Applyer.Name = "Applyer";
            this.Applyer.NotificationAutoEnlist = false;
            this.Applyer.SecExcept = null;
            this.Applyer.SecFieldName = null;
            this.Applyer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Applyer.SelectPaging = false;
            this.Applyer.SelectTop = 0;
            this.Applyer.SiteControl = false;
            this.Applyer.SiteFieldName = null;
            this.Applyer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Organization
            // 
            this.Organization.CacheConnection = false;
            this.Organization.CommandText = "SELECT ORG_NO,ORG_DESC,ORG_KIND,UPPER_ORG FROM EIPHRSYS.dbo.sys_org \r\nWHERE (Uppe" +
    "r_Org=\'10000\' OR Upper_Org=\'13000\'  OR  ORG_NO=\'10000\' OR ORG_NO=\'99999\')\r\nORDER" +
    " BY ORG_NO";
            this.Organization.CommandTimeout = 30;
            this.Organization.CommandType = System.Data.CommandType.Text;
            this.Organization.DynamicTableName = false;
            this.Organization.EEPAlias = null;
            this.Organization.EncodingAfter = null;
            this.Organization.EncodingBefore = "Windows-1252";
            this.Organization.EncodingConvert = null;
            this.Organization.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem6);
            this.Organization.MultiSetWhere = false;
            this.Organization.Name = "Organization";
            this.Organization.NotificationAutoEnlist = false;
            this.Organization.SecExcept = null;
            this.Organization.SecFieldName = null;
            this.Organization.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Organization.SelectPaging = false;
            this.Organization.SelectTop = 0;
            this.Organization.SiteControl = false;
            this.Organization.SiteFieldName = null;
            this.Organization.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CashAgainBillType
            // 
            this.CashAgainBillType.CacheConnection = false;
            this.CashAgainBillType.CommandText = "select CashAgainBillType.AgainBillType,CashAgainBillType.AgainBillName\r\n from Cas" +
    "hAgainBillType\r\nwhere IsCashTakeBackItem=1";
            this.CashAgainBillType.CommandTimeout = 30;
            this.CashAgainBillType.CommandType = System.Data.CommandType.Text;
            this.CashAgainBillType.DynamicTableName = false;
            this.CashAgainBillType.EEPAlias = null;
            this.CashAgainBillType.EncodingAfter = null;
            this.CashAgainBillType.EncodingBefore = "Windows-1252";
            this.CashAgainBillType.EncodingConvert = null;
            this.CashAgainBillType.InfoConnection = this.InfoConnection1;
            this.CashAgainBillType.MultiSetWhere = false;
            this.CashAgainBillType.Name = "CashAgainBillType";
            this.CashAgainBillType.NotificationAutoEnlist = false;
            this.CashAgainBillType.SecExcept = null;
            this.CashAgainBillType.SecFieldName = null;
            this.CashAgainBillType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CashAgainBillType.SelectPaging = false;
            this.CashAgainBillType.SelectTop = 0;
            this.CashAgainBillType.SiteControl = false;
            this.CashAgainBillType.SiteFieldName = null;
            this.CashAgainBillType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Status
            // 
            this.Status.CacheConnection = false;
            this.Status.CommandText = "SELECT 1 AS ID,\r\n               \'流程中\' AS STATUS\r\nUNION\r\nSELECT 2 AS ID,\r\n        " +
    "       \'已結案\' AS STATUS";
            this.Status.CommandTimeout = 30;
            this.Status.CommandType = System.Data.CommandType.Text;
            this.Status.DynamicTableName = false;
            this.Status.EEPAlias = null;
            this.Status.EncodingAfter = null;
            this.Status.EncodingBefore = "Windows-1252";
            this.Status.EncodingConvert = null;
            this.Status.InfoConnection = this.InfoConnection1;
            this.Status.MultiSetWhere = false;
            this.Status.Name = "Status";
            this.Status.NotificationAutoEnlist = false;
            this.Status.SecExcept = null;
            this.Status.SecFieldName = null;
            this.Status.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Status.SelectPaging = false;
            this.Status.SelectTop = 0;
            this.Status.SiteControl = false;
            this.Status.SiteFieldName = null;
            this.Status.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CashTakeBackType
            // 
            this.CashTakeBackType.CacheConnection = false;
            this.CashTakeBackType.CommandText = " SELECT * FROM  CashTakeBackType ORDER BY CashTakeBackType";
            this.CashTakeBackType.CommandTimeout = 30;
            this.CashTakeBackType.CommandType = System.Data.CommandType.Text;
            this.CashTakeBackType.DynamicTableName = false;
            this.CashTakeBackType.EEPAlias = null;
            this.CashTakeBackType.EncodingAfter = null;
            this.CashTakeBackType.EncodingBefore = "Windows-1252";
            this.CashTakeBackType.EncodingConvert = null;
            this.CashTakeBackType.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "SHORTTERMNO";
            this.CashTakeBackType.KeyFields.Add(keyItem7);
            this.CashTakeBackType.MultiSetWhere = false;
            this.CashTakeBackType.Name = "CashTakeBackType";
            this.CashTakeBackType.NotificationAutoEnlist = false;
            this.CashTakeBackType.SecExcept = null;
            this.CashTakeBackType.SecFieldName = null;
            this.CashTakeBackType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CashTakeBackType.SelectPaging = false;
            this.CashTakeBackType.SelectTop = 0;
            this.CashTakeBackType.SiteControl = false;
            this.CashTakeBackType.SiteFieldName = null;
            this.CashTakeBackType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_CashTakeBackDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Applyer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashAgainBillType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashTakeBackType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand CashTakeBackDetails;
        private Srvtools.UpdateComponent ucCashTakeBackDetails;
        private Srvtools.InfoCommand View_CashTakeBackDetails;
        private Srvtools.InfoCommand Applyer;
        private Srvtools.InfoCommand Organization;
        private Srvtools.InfoCommand CashAgainBillType;
        private Srvtools.InfoCommand Status;
        private Srvtools.InfoCommand CashTakeBackType;
    }
}
