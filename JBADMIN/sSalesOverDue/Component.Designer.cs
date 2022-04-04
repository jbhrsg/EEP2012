namespace sSalesOverDue
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
            this.SalesDetails = new Srvtools.InfoCommand(this.components);
            this.ucSalesDetails = new Srvtools.UpdateComponent(this.components);
            this.View_SalesDetails = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesDetails)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBERP";
            // 
            // SalesDetails
            // 
            this.SalesDetails.CacheConnection = false;
            this.SalesDetails.CommandText = "Select * From View_SalesOverDue\r\norder by CustomerID,InvoiceDate,InvoiceNO\r\n";
            this.SalesDetails.CommandTimeout = 30;
            this.SalesDetails.CommandType = System.Data.CommandType.Text;
            this.SalesDetails.DynamicTableName = false;
            this.SalesDetails.EEPAlias = "JBERP";
            this.SalesDetails.EncodingAfter = null;
            this.SalesDetails.EncodingBefore = "Windows-1252";
            this.SalesDetails.EncodingConvert = null;
            this.SalesDetails.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "InvoiceNO";
            this.SalesDetails.KeyFields.Add(keyItem1);
            this.SalesDetails.MultiSetWhere = false;
            this.SalesDetails.Name = "SalesDetails";
            this.SalesDetails.NotificationAutoEnlist = false;
            this.SalesDetails.SecExcept = null;
            this.SalesDetails.SecFieldName = null;
            this.SalesDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesDetails.SelectPaging = false;
            this.SalesDetails.SelectTop = 0;
            this.SalesDetails.SiteControl = false;
            this.SalesDetails.SiteFieldName = null;
            this.SalesDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesDetails
            // 
            this.ucSalesDetails.AutoTrans = true;
            this.ucSalesDetails.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalesNO";
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
            fieldAttr3.DataField = "SalesTypeID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "FeeItemID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "SalesTypeName";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Quantity";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Unit";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "UnitPrice";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Amount";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "DType";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
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
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
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
            fieldAttr15.DataField = "NewsAreaID";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "Sections";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CustLines";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr1);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr2);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr3);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr4);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr5);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr6);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr7);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr8);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr9);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr10);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr11);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr12);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr13);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr14);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr15);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr16);
            this.ucSalesDetails.FieldAttrs.Add(fieldAttr17);
            this.ucSalesDetails.LogInfo = null;
            this.ucSalesDetails.Name = "ucSalesDetails";
            this.ucSalesDetails.RowAffectsCheck = true;
            this.ucSalesDetails.SelectCmd = this.SalesDetails;
            this.ucSalesDetails.SelectCmdForUpdate = null;
            this.ucSalesDetails.SendSQLCmd = true;
            this.ucSalesDetails.ServerModify = true;
            this.ucSalesDetails.ServerModifyGetMax = false;
            this.ucSalesDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesDetails.UseTranscationScope = false;
            this.ucSalesDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_SalesDetails
            // 
            this.View_SalesDetails.CacheConnection = false;
            this.View_SalesDetails.CommandText = "SELECT * FROM dbo.[SalesDetails]";
            this.View_SalesDetails.CommandTimeout = 30;
            this.View_SalesDetails.CommandType = System.Data.CommandType.Text;
            this.View_SalesDetails.DynamicTableName = false;
            this.View_SalesDetails.EEPAlias = null;
            this.View_SalesDetails.EncodingAfter = null;
            this.View_SalesDetails.EncodingBefore = "Windows-1252";
            this.View_SalesDetails.EncodingConvert = null;
            this.View_SalesDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SalesNO";
            keyItem3.KeyName = "ItemNO";
            this.View_SalesDetails.KeyFields.Add(keyItem2);
            this.View_SalesDetails.KeyFields.Add(keyItem3);
            this.View_SalesDetails.MultiSetWhere = false;
            this.View_SalesDetails.Name = "View_SalesDetails";
            this.View_SalesDetails.NotificationAutoEnlist = false;
            this.View_SalesDetails.SecExcept = null;
            this.View_SalesDetails.SecFieldName = null;
            this.View_SalesDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesDetails.SelectPaging = false;
            this.View_SalesDetails.SelectTop = 0;
            this.View_SalesDetails.SiteControl = false;
            this.View_SalesDetails.SiteFieldName = null;
            this.View_SalesDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesDetails)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesDetails;
        private Srvtools.UpdateComponent ucSalesDetails;
        private Srvtools.InfoCommand View_SalesDetails;
    }
}
