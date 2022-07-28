namespace sERPCustMaintain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPCustMaintain = new Srvtools.InfoCommand(this.components);
            this.ucERPCustMaintain = new Srvtools.UpdateComponent(this.components);
            this.infoSalesMan = new Srvtools.InfoCommand(this.components);
            this.infoCustomerColumn = new Srvtools.InfoCommand(this.components);
            this.infoPostType = new Srvtools.InfoCommand(this.components);
            this.infoUserID = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustMaintain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomerColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPostType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoUserID)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "ReportCustomerToDoNotes";
            service1.NonLogin = false;
            service1.ServiceName = "ReportCustomerToDoNotes";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPCustMaintain
            // 
            this.ERPCustMaintain.CacheConnection = false;
            this.ERPCustMaintain.CommandText = resources.GetString("ERPCustMaintain.CommandText");
            this.ERPCustMaintain.CommandTimeout = 30;
            this.ERPCustMaintain.CommandType = System.Data.CommandType.Text;
            this.ERPCustMaintain.DynamicTableName = false;
            this.ERPCustMaintain.EEPAlias = null;
            this.ERPCustMaintain.EncodingAfter = null;
            this.ERPCustMaintain.EncodingBefore = "Windows-1252";
            this.ERPCustMaintain.EncodingConvert = null;
            this.ERPCustMaintain.InfoConnection = this.InfoConnection1;
            this.ERPCustMaintain.MultiSetWhere = false;
            this.ERPCustMaintain.Name = "ERPCustMaintain";
            this.ERPCustMaintain.NotificationAutoEnlist = false;
            this.ERPCustMaintain.SecExcept = null;
            this.ERPCustMaintain.SecFieldName = null;
            this.ERPCustMaintain.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPCustMaintain.SelectPaging = false;
            this.ERPCustMaintain.SelectTop = 0;
            this.ERPCustMaintain.SiteControl = false;
            this.ERPCustMaintain.SiteFieldName = null;
            this.ERPCustMaintain.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPCustMaintain
            // 
            this.ucERPCustMaintain.AutoTrans = true;
            this.ucERPCustMaintain.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CustAreaID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustAreaName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            this.ucERPCustMaintain.FieldAttrs.Add(fieldAttr1);
            this.ucERPCustMaintain.FieldAttrs.Add(fieldAttr2);
            this.ucERPCustMaintain.LogInfo = null;
            this.ucERPCustMaintain.Name = "ucERPCustMaintain";
            this.ucERPCustMaintain.RowAffectsCheck = true;
            this.ucERPCustMaintain.SelectCmd = this.ERPCustMaintain;
            this.ucERPCustMaintain.SelectCmdForUpdate = null;
            this.ucERPCustMaintain.SendSQLCmd = true;
            this.ucERPCustMaintain.ServerModify = true;
            this.ucERPCustMaintain.ServerModifyGetMax = false;
            this.ucERPCustMaintain.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPCustMaintain.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPCustMaintain.UseTranscationScope = false;
            this.ucERPCustMaintain.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoSalesMan
            // 
            this.infoSalesMan.CacheConnection = false;
            this.infoSalesMan.CommandText = "select distinct CreateBy\r\nfrom View_CustomerToDoNotes\r\norder by CreateBy\r\n";
            this.infoSalesMan.CommandTimeout = 30;
            this.infoSalesMan.CommandType = System.Data.CommandType.Text;
            this.infoSalesMan.DynamicTableName = false;
            this.infoSalesMan.EEPAlias = null;
            this.infoSalesMan.EncodingAfter = null;
            this.infoSalesMan.EncodingBefore = "Windows-1252";
            this.infoSalesMan.EncodingConvert = null;
            this.infoSalesMan.InfoConnection = this.InfoConnection1;
            this.infoSalesMan.MultiSetWhere = false;
            this.infoSalesMan.Name = "infoSalesMan";
            this.infoSalesMan.NotificationAutoEnlist = false;
            this.infoSalesMan.SecExcept = null;
            this.infoSalesMan.SecFieldName = null;
            this.infoSalesMan.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoSalesMan.SelectPaging = false;
            this.infoSalesMan.SelectTop = 0;
            this.infoSalesMan.SiteControl = false;
            this.infoSalesMan.SiteFieldName = null;
            this.infoSalesMan.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCustomerColumn
            // 
            this.infoCustomerColumn.CacheConnection = false;
            this.infoCustomerColumn.CommandText = "select FIELD_NAME,CAPTION\r\nfrom ERPCustMaintainFeild";
            this.infoCustomerColumn.CommandTimeout = 30;
            this.infoCustomerColumn.CommandType = System.Data.CommandType.Text;
            this.infoCustomerColumn.DynamicTableName = false;
            this.infoCustomerColumn.EEPAlias = null;
            this.infoCustomerColumn.EncodingAfter = null;
            this.infoCustomerColumn.EncodingBefore = "Windows-1252";
            this.infoCustomerColumn.EncodingConvert = null;
            this.infoCustomerColumn.InfoConnection = this.InfoConnection1;
            this.infoCustomerColumn.MultiSetWhere = false;
            this.infoCustomerColumn.Name = "infoCustomerColumn";
            this.infoCustomerColumn.NotificationAutoEnlist = false;
            this.infoCustomerColumn.SecExcept = null;
            this.infoCustomerColumn.SecFieldName = null;
            this.infoCustomerColumn.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustomerColumn.SelectPaging = false;
            this.infoCustomerColumn.SelectTop = 0;
            this.infoCustomerColumn.SiteControl = false;
            this.infoCustomerColumn.SiteFieldName = null;
            this.infoCustomerColumn.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoPostType
            // 
            this.infoPostType.CacheConnection = false;
            this.infoPostType.CommandText = "select ListID,ListContent from View_ERPReferenceTable\r\nwhere ListCategory=\'PostTy" +
    "pe\'";
            this.infoPostType.CommandTimeout = 30;
            this.infoPostType.CommandType = System.Data.CommandType.Text;
            this.infoPostType.DynamicTableName = false;
            this.infoPostType.EEPAlias = null;
            this.infoPostType.EncodingAfter = null;
            this.infoPostType.EncodingBefore = "Windows-1252";
            this.infoPostType.EncodingConvert = null;
            this.infoPostType.InfoConnection = this.InfoConnection1;
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
            // infoUserID
            // 
            this.infoUserID.CacheConnection = false;
            this.infoUserID.CommandText = "select distinct USERID,USERNAME\r\nfrom View_UsersGROUPS\r\nwhere GROUPNAME like \'%媒體" +
    "%\'\t\r\n";
            this.infoUserID.CommandTimeout = 30;
            this.infoUserID.CommandType = System.Data.CommandType.Text;
            this.infoUserID.DynamicTableName = false;
            this.infoUserID.EEPAlias = null;
            this.infoUserID.EncodingAfter = null;
            this.infoUserID.EncodingBefore = "Windows-1252";
            this.infoUserID.EncodingConvert = null;
            this.infoUserID.InfoConnection = this.InfoConnection1;
            this.infoUserID.MultiSetWhere = false;
            this.infoUserID.Name = "infoUserID";
            this.infoUserID.NotificationAutoEnlist = false;
            this.infoUserID.SecExcept = null;
            this.infoUserID.SecFieldName = null;
            this.infoUserID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoUserID.SelectPaging = false;
            this.infoUserID.SelectTop = 0;
            this.infoUserID.SiteControl = false;
            this.infoUserID.SiteFieldName = null;
            this.infoUserID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustMaintain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoSalesMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomerColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPostType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoUserID)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPCustMaintain;
        private Srvtools.UpdateComponent ucERPCustMaintain;
        private Srvtools.InfoCommand infoSalesMan;
        private Srvtools.InfoCommand infoCustomerColumn;
        private Srvtools.InfoCommand infoPostType;
        private Srvtools.InfoCommand infoUserID;
    }
}
