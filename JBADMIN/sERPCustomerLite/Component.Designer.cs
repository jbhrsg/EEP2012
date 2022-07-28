namespace sERPCustomerLite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
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
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPCustomers = new Srvtools.InfoCommand(this.components);
            this.ucERPCustomers = new Srvtools.UpdateComponent(this.components);
            this.View_ERPCustomers = new Srvtools.InfoCommand(this.components);
            this.Salse = new Srvtools.InfoCommand(this.components);
            this.PostSource = new Srvtools.InfoCommand(this.components);
            this.infoPostType = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Salse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPostType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckCustNO";
            service1.NonLogin = false;
            service1.ServiceName = "CheckCustNO";
            service2.DelegateName = "UpdateERPCustomerToDoNotes";
            service2.NonLogin = false;
            service2.ServiceName = "UpdateERPCustomerToDoNotes";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPCustomers
            // 
            this.ERPCustomers.CacheConnection = false;
            this.ERPCustomers.CommandText = resources.GetString("ERPCustomers.CommandText");
            this.ERPCustomers.CommandTimeout = 30;
            this.ERPCustomers.CommandType = System.Data.CommandType.Text;
            this.ERPCustomers.DynamicTableName = false;
            this.ERPCustomers.EEPAlias = "JBADMIN";
            this.ERPCustomers.EncodingAfter = null;
            this.ERPCustomers.EncodingBefore = "Windows-1252";
            this.ERPCustomers.EncodingConvert = null;
            this.ERPCustomers.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "CustNO";
            this.ERPCustomers.KeyFields.Add(keyItem1);
            this.ERPCustomers.MultiSetWhere = false;
            this.ERPCustomers.Name = "ERPCustomers";
            this.ERPCustomers.NotificationAutoEnlist = false;
            this.ERPCustomers.SecExcept = null;
            this.ERPCustomers.SecFieldName = null;
            this.ERPCustomers.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPCustomers.SelectPaging = false;
            this.ERPCustomers.SelectTop = 0;
            this.ERPCustomers.SiteControl = false;
            this.ERPCustomers.SiteFieldName = null;
            this.ERPCustomers.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPCustomers
            // 
            this.ucERPCustomers.AutoTrans = true;
            this.ucERPCustomers.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CustNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CustShortName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SalesID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CustTelNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CustAddr";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CustFaxNO";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ContactA";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ContactASubTel";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ContactAMail";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = "_username";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr13.DefaultValue = "_username";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpdateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "HrBankUrl";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "PostedDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "PostedMan";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr17.DefaultValue = "_username";
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr1);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr2);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr3);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr4);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr5);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr6);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr7);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr8);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr9);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr10);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr11);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr12);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr13);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr14);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr15);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr16);
            this.ucERPCustomers.FieldAttrs.Add(fieldAttr17);
            this.ucERPCustomers.LogInfo = null;
            this.ucERPCustomers.Name = "ucERPCustomers";
            this.ucERPCustomers.RowAffectsCheck = true;
            this.ucERPCustomers.SelectCmd = this.ERPCustomers;
            this.ucERPCustomers.SelectCmdForUpdate = null;
            this.ucERPCustomers.SendSQLCmd = true;
            this.ucERPCustomers.ServerModify = true;
            this.ucERPCustomers.ServerModifyGetMax = false;
            this.ucERPCustomers.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPCustomers.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPCustomers.UseTranscationScope = false;
            this.ucERPCustomers.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucERPCustomers.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucERPCustomers_BeforeInsert);
            this.ucERPCustomers.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucERPCustomers_BeforeModify);
            // 
            // View_ERPCustomers
            // 
            this.View_ERPCustomers.CacheConnection = false;
            this.View_ERPCustomers.CommandText = "SELECT * FROM dbo.[ERPCustomers]";
            this.View_ERPCustomers.CommandTimeout = 30;
            this.View_ERPCustomers.CommandType = System.Data.CommandType.Text;
            this.View_ERPCustomers.DynamicTableName = false;
            this.View_ERPCustomers.EEPAlias = null;
            this.View_ERPCustomers.EncodingAfter = null;
            this.View_ERPCustomers.EncodingBefore = "Windows-1252";
            this.View_ERPCustomers.EncodingConvert = null;
            this.View_ERPCustomers.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "CustNO";
            this.View_ERPCustomers.KeyFields.Add(keyItem2);
            this.View_ERPCustomers.MultiSetWhere = false;
            this.View_ERPCustomers.Name = "View_ERPCustomers";
            this.View_ERPCustomers.NotificationAutoEnlist = false;
            this.View_ERPCustomers.SecExcept = null;
            this.View_ERPCustomers.SecFieldName = null;
            this.View_ERPCustomers.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPCustomers.SelectPaging = false;
            this.View_ERPCustomers.SelectTop = 0;
            this.View_ERPCustomers.SiteControl = false;
            this.View_ERPCustomers.SiteFieldName = null;
            this.View_ERPCustomers.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Salse
            // 
            this.Salse.CacheConnection = false;
            this.Salse.CommandText = "SELECT  SALESID,RTRIM(SALESNAME)+\'-\'+RTRIM(SALESID)  AS SALESMAN\r\nFROM  ERPSALESM" +
    "AN   \r\nWHERE  ISMEDIA=1 ORDER BY SALESNAME";
            this.Salse.CommandTimeout = 30;
            this.Salse.CommandType = System.Data.CommandType.Text;
            this.Salse.DynamicTableName = false;
            this.Salse.EEPAlias = "JBADMIN";
            this.Salse.EncodingAfter = null;
            this.Salse.EncodingBefore = "Windows-1252";
            this.Salse.EncodingConvert = null;
            this.Salse.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "SalesNO";
            this.Salse.KeyFields.Add(keyItem3);
            this.Salse.MultiSetWhere = false;
            this.Salse.Name = "Salse";
            this.Salse.NotificationAutoEnlist = false;
            this.Salse.SecExcept = null;
            this.Salse.SecFieldName = null;
            this.Salse.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Salse.SelectPaging = false;
            this.Salse.SelectTop = 0;
            this.Salse.SiteControl = false;
            this.Salse.SiteFieldName = null;
            this.Salse.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PostSource
            // 
            this.PostSource.CacheConnection = false;
            this.PostSource.CommandText = "SELECT  *\r\nFROM  ERPPostSource";
            this.PostSource.CommandTimeout = 30;
            this.PostSource.CommandType = System.Data.CommandType.Text;
            this.PostSource.DynamicTableName = false;
            this.PostSource.EEPAlias = "JBADMIN";
            this.PostSource.EncodingAfter = null;
            this.PostSource.EncodingBefore = "Windows-1252";
            this.PostSource.EncodingConvert = null;
            this.PostSource.InfoConnection = this.InfoConnection1;
            this.PostSource.MultiSetWhere = false;
            this.PostSource.Name = "PostSource";
            this.PostSource.NotificationAutoEnlist = false;
            this.PostSource.SecExcept = null;
            this.PostSource.SecFieldName = null;
            this.PostSource.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PostSource.SelectPaging = false;
            this.PostSource.SelectTop = 0;
            this.PostSource.SiteControl = false;
            this.PostSource.SiteFieldName = null;
            this.PostSource.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoPostType
            // 
            this.infoPostType.CacheConnection = false;
            this.infoPostType.CommandText = "select ListID,ListContent\r\nfrom View_ERPReferenceTable\r\nwhere ListCategory=\'PostT" +
    "ype\'";
            this.infoPostType.CommandTimeout = 30;
            this.infoPostType.CommandType = System.Data.CommandType.Text;
            this.infoPostType.DynamicTableName = false;
            this.infoPostType.EEPAlias = "JBADMIN";
            this.infoPostType.EncodingAfter = null;
            this.infoPostType.EncodingBefore = "Windows-1252";
            this.infoPostType.EncodingConvert = null;
            this.infoPostType.InfoConnection = this.infoConnection2;
            this.infoPostType.MultiSetWhere = false;
            this.infoPostType.Name = "infoPostType";
            this.infoPostType.NotificationAutoEnlist = false;
            this.infoPostType.SecExcept = null;
            this.infoPostType.SecFieldName = null;
            this.infoPostType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoPostType.SelectPaging = false;
            this.infoPostType.SelectTop = 0;
            this.infoPostType.SiteControl = false;
            this.infoPostType.SiteFieldName = null;
            this.infoPostType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBADMIN";
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Salse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPostType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPCustomers;
        private Srvtools.UpdateComponent ucERPCustomers;
        private Srvtools.InfoCommand View_ERPCustomers;
        private Srvtools.InfoCommand Salse;
        private Srvtools.InfoCommand PostSource;
        private Srvtools.InfoCommand infoPostType;
        private Srvtools.InfoConnection infoConnection2;
    }
}
