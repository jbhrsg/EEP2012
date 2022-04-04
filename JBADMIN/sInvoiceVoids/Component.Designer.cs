namespace sInvoiceVoids
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
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPInvoiceVoidApply = new Srvtools.InfoCommand(this.components);
            this.ucERPInvoiceVoidApply = new Srvtools.UpdateComponent(this.components);
            this.View_ERPInvoiceVoidApply = new Srvtools.InfoCommand(this.components);
            this.InvoiceLists = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPInvoiceVoidApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPInvoiceVoidApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceLists)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPInvoiceVoidApply
            // 
            this.ERPInvoiceVoidApply.CacheConnection = false;
            this.ERPInvoiceVoidApply.CommandText = "SELECT dbo.[ERPInvoiceVoidApply].* FROM dbo.[ERPInvoiceVoidApply]";
            this.ERPInvoiceVoidApply.CommandTimeout = 30;
            this.ERPInvoiceVoidApply.CommandType = System.Data.CommandType.Text;
            this.ERPInvoiceVoidApply.DynamicTableName = false;
            this.ERPInvoiceVoidApply.EEPAlias = null;
            this.ERPInvoiceVoidApply.EncodingAfter = null;
            this.ERPInvoiceVoidApply.EncodingBefore = "Windows-1252";
            this.ERPInvoiceVoidApply.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "InvoiceVoidNO";
            this.ERPInvoiceVoidApply.KeyFields.Add(keyItem1);
            this.ERPInvoiceVoidApply.MultiSetWhere = false;
            this.ERPInvoiceVoidApply.Name = "ERPInvoiceVoidApply";
            this.ERPInvoiceVoidApply.NotificationAutoEnlist = false;
            this.ERPInvoiceVoidApply.SecExcept = null;
            this.ERPInvoiceVoidApply.SecFieldName = null;
            this.ERPInvoiceVoidApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPInvoiceVoidApply.SelectPaging = false;
            this.ERPInvoiceVoidApply.SelectTop = 0;
            this.ERPInvoiceVoidApply.SiteControl = false;
            this.ERPInvoiceVoidApply.SiteFieldName = null;
            this.ERPInvoiceVoidApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPInvoiceVoidApply
            // 
            this.ucERPInvoiceVoidApply.AutoTrans = true;
            this.ucERPInvoiceVoidApply.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "InvoiceVoidNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ApplyDate";
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
            fieldAttr4.DataField = "ApplyOrg_NO";
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
            fieldAttr6.DataField = "CustNO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "InvoiceNO";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "InvoiceAmt";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "VoidNotes";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = "_username";
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr1);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr2);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr3);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr4);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr5);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr6);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr7);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr8);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr9);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr10);
            this.ucERPInvoiceVoidApply.FieldAttrs.Add(fieldAttr11);
            this.ucERPInvoiceVoidApply.LogInfo = null;
            this.ucERPInvoiceVoidApply.Name = "ucERPInvoiceVoidApply";
            this.ucERPInvoiceVoidApply.RowAffectsCheck = true;
            this.ucERPInvoiceVoidApply.SelectCmd = this.ERPInvoiceVoidApply;
            this.ucERPInvoiceVoidApply.SelectCmdForUpdate = null;
            this.ucERPInvoiceVoidApply.ServerModify = true;
            this.ucERPInvoiceVoidApply.ServerModifyGetMax = false;
            this.ucERPInvoiceVoidApply.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPInvoiceVoidApply.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPInvoiceVoidApply.UseTranscationScope = false;
            this.ucERPInvoiceVoidApply.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucERPInvoiceVoidApply.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucERPInvoiceVoidApply_BeforeInsert);
            // 
            // View_ERPInvoiceVoidApply
            // 
            this.View_ERPInvoiceVoidApply.CacheConnection = false;
            this.View_ERPInvoiceVoidApply.CommandText = "SELECT * FROM dbo.[ERPInvoiceVoidApply]";
            this.View_ERPInvoiceVoidApply.CommandTimeout = 30;
            this.View_ERPInvoiceVoidApply.CommandType = System.Data.CommandType.Text;
            this.View_ERPInvoiceVoidApply.DynamicTableName = false;
            this.View_ERPInvoiceVoidApply.EEPAlias = null;
            this.View_ERPInvoiceVoidApply.EncodingAfter = null;
            this.View_ERPInvoiceVoidApply.EncodingBefore = "Windows-1252";
            this.View_ERPInvoiceVoidApply.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "InvoiceVoidNO";
            this.View_ERPInvoiceVoidApply.KeyFields.Add(keyItem2);
            this.View_ERPInvoiceVoidApply.MultiSetWhere = false;
            this.View_ERPInvoiceVoidApply.Name = "View_ERPInvoiceVoidApply";
            this.View_ERPInvoiceVoidApply.NotificationAutoEnlist = false;
            this.View_ERPInvoiceVoidApply.SecExcept = null;
            this.View_ERPInvoiceVoidApply.SecFieldName = null;
            this.View_ERPInvoiceVoidApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPInvoiceVoidApply.SelectPaging = false;
            this.View_ERPInvoiceVoidApply.SelectTop = 0;
            this.View_ERPInvoiceVoidApply.SiteControl = false;
            this.View_ERPInvoiceVoidApply.SiteFieldName = null;
            this.View_ERPInvoiceVoidApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InvoiceLists
            // 
            this.InvoiceLists.CacheConnection = false;
            this.InvoiceLists.CommandText = resources.GetString("InvoiceLists.CommandText");
            this.InvoiceLists.CommandTimeout = 30;
            this.InvoiceLists.CommandType = System.Data.CommandType.Text;
            this.InvoiceLists.DynamicTableName = false;
            this.InvoiceLists.EEPAlias = null;
            this.InvoiceLists.EncodingAfter = null;
            this.InvoiceLists.EncodingBefore = "Windows-1252";
            this.InvoiceLists.InfoConnection = this.InfoConnection1;
            this.InvoiceLists.MultiSetWhere = false;
            this.InvoiceLists.Name = "InvoiceLists";
            this.InvoiceLists.NotificationAutoEnlist = false;
            this.InvoiceLists.SecExcept = null;
            this.InvoiceLists.SecFieldName = null;
            this.InvoiceLists.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InvoiceLists.SelectPaging = false;
            this.InvoiceLists.SelectTop = 0;
            this.InvoiceLists.SiteControl = false;
            this.InvoiceLists.SiteFieldName = null;
            this.InvoiceLists.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPInvoiceVoidApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPInvoiceVoidApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceLists)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPInvoiceVoidApply;
        private Srvtools.UpdateComponent ucERPInvoiceVoidApply;
        private Srvtools.InfoCommand View_ERPInvoiceVoidApply;
        private Srvtools.InfoCommand InvoiceLists;
    }
}
