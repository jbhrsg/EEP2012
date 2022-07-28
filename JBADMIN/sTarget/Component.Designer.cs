namespace sTarget
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SalesMonthTarget = new Srvtools.InfoCommand(this.components);
            this.ucSalesMonthTarget = new Srvtools.UpdateComponent(this.components);
            this.View_SalesMonthTarget = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.SalesDayTarget = new Srvtools.InfoCommand(this.components);
            this.ucSalesDayTarget = new Srvtools.UpdateComponent(this.components);
            this.autoNumber2 = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMonthTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesMonthTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDayTarget)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // SalesMonthTarget
            // 
            this.SalesMonthTarget.CacheConnection = false;
            this.SalesMonthTarget.CommandText = "SELECT dbo.[SalesMonthTarget].* ,\'\' as Btn FROM dbo.[SalesMonthTarget]";
            this.SalesMonthTarget.CommandTimeout = 30;
            this.SalesMonthTarget.CommandType = System.Data.CommandType.Text;
            this.SalesMonthTarget.DynamicTableName = false;
            this.SalesMonthTarget.EEPAlias = null;
            this.SalesMonthTarget.EncodingAfter = null;
            this.SalesMonthTarget.EncodingBefore = "Windows-1252";
            this.SalesMonthTarget.EncodingConvert = null;
            this.SalesMonthTarget.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.SalesMonthTarget.KeyFields.Add(keyItem1);
            this.SalesMonthTarget.MultiSetWhere = false;
            this.SalesMonthTarget.Name = "SalesMonthTarget";
            this.SalesMonthTarget.NotificationAutoEnlist = false;
            this.SalesMonthTarget.SecExcept = null;
            this.SalesMonthTarget.SecFieldName = null;
            this.SalesMonthTarget.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesMonthTarget.SelectPaging = false;
            this.SalesMonthTarget.SelectTop = 0;
            this.SalesMonthTarget.SiteControl = false;
            this.SalesMonthTarget.SiteFieldName = null;
            this.SalesMonthTarget.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesMonthTarget
            // 
            this.ucSalesMonthTarget.AutoTrans = true;
            this.ucSalesMonthTarget.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "Year";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "Month";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Target";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Sales";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            this.ucSalesMonthTarget.FieldAttrs.Add(fieldAttr1);
            this.ucSalesMonthTarget.FieldAttrs.Add(fieldAttr2);
            this.ucSalesMonthTarget.FieldAttrs.Add(fieldAttr3);
            this.ucSalesMonthTarget.FieldAttrs.Add(fieldAttr4);
            this.ucSalesMonthTarget.FieldAttrs.Add(fieldAttr5);
            this.ucSalesMonthTarget.LogInfo = null;
            this.ucSalesMonthTarget.Name = "ucSalesMonthTarget";
            this.ucSalesMonthTarget.RowAffectsCheck = true;
            this.ucSalesMonthTarget.SelectCmd = this.SalesMonthTarget;
            this.ucSalesMonthTarget.SelectCmdForUpdate = null;
            this.ucSalesMonthTarget.SendSQLCmd = true;
            this.ucSalesMonthTarget.ServerModify = true;
            this.ucSalesMonthTarget.ServerModifyGetMax = false;
            this.ucSalesMonthTarget.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesMonthTarget.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesMonthTarget.UseTranscationScope = false;
            this.ucSalesMonthTarget.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_SalesMonthTarget
            // 
            this.View_SalesMonthTarget.CacheConnection = false;
            this.View_SalesMonthTarget.CommandText = "SELECT * FROM dbo.[SalesMonthTarget]";
            this.View_SalesMonthTarget.CommandTimeout = 30;
            this.View_SalesMonthTarget.CommandType = System.Data.CommandType.Text;
            this.View_SalesMonthTarget.DynamicTableName = false;
            this.View_SalesMonthTarget.EEPAlias = null;
            this.View_SalesMonthTarget.EncodingAfter = null;
            this.View_SalesMonthTarget.EncodingBefore = "Windows-1252";
            this.View_SalesMonthTarget.EncodingConvert = null;
            this.View_SalesMonthTarget.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.View_SalesMonthTarget.KeyFields.Add(keyItem2);
            this.View_SalesMonthTarget.MultiSetWhere = false;
            this.View_SalesMonthTarget.Name = "View_SalesMonthTarget";
            this.View_SalesMonthTarget.NotificationAutoEnlist = false;
            this.View_SalesMonthTarget.SecExcept = null;
            this.View_SalesMonthTarget.SecFieldName = null;
            this.View_SalesMonthTarget.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_SalesMonthTarget.SelectPaging = false;
            this.View_SalesMonthTarget.SelectTop = 0;
            this.View_SalesMonthTarget.SiteControl = false;
            this.View_SalesMonthTarget.SiteFieldName = null;
            this.View_SalesMonthTarget.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "AutoKey";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 9;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "AutoKey";
            this.autoNumber1.UpdateComp = this.ucSalesMonthTarget;
            // 
            // SalesDayTarget
            // 
            this.SalesDayTarget.CacheConnection = false;
            this.SalesDayTarget.CommandText = "SELECT dbo.[SalesDayTarget].*  FROM dbo.[SalesDayTarget]";
            this.SalesDayTarget.CommandTimeout = 30;
            this.SalesDayTarget.CommandType = System.Data.CommandType.Text;
            this.SalesDayTarget.DynamicTableName = false;
            this.SalesDayTarget.EEPAlias = null;
            this.SalesDayTarget.EncodingAfter = null;
            this.SalesDayTarget.EncodingBefore = "Windows-1252";
            this.SalesDayTarget.EncodingConvert = null;
            this.SalesDayTarget.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.SalesDayTarget.KeyFields.Add(keyItem3);
            this.SalesDayTarget.MultiSetWhere = false;
            this.SalesDayTarget.Name = "SalesDayTarget";
            this.SalesDayTarget.NotificationAutoEnlist = false;
            this.SalesDayTarget.SecExcept = null;
            this.SalesDayTarget.SecFieldName = null;
            this.SalesDayTarget.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesDayTarget.SelectPaging = false;
            this.SalesDayTarget.SelectTop = 0;
            this.SalesDayTarget.SiteControl = false;
            this.SalesDayTarget.SiteFieldName = null;
            this.SalesDayTarget.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSalesDayTarget
            // 
            this.ucSalesDayTarget.AutoTrans = true;
            this.ucSalesDayTarget.ExceptJoin = false;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "AutoKey";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Date";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Target";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Sales";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucSalesDayTarget.FieldAttrs.Add(fieldAttr6);
            this.ucSalesDayTarget.FieldAttrs.Add(fieldAttr7);
            this.ucSalesDayTarget.FieldAttrs.Add(fieldAttr8);
            this.ucSalesDayTarget.FieldAttrs.Add(fieldAttr9);
            this.ucSalesDayTarget.LogInfo = null;
            this.ucSalesDayTarget.Name = "ucSalesDayTarget";
            this.ucSalesDayTarget.RowAffectsCheck = true;
            this.ucSalesDayTarget.SelectCmd = this.SalesDayTarget;
            this.ucSalesDayTarget.SelectCmdForUpdate = null;
            this.ucSalesDayTarget.SendSQLCmd = true;
            this.ucSalesDayTarget.ServerModify = true;
            this.ucSalesDayTarget.ServerModifyGetMax = false;
            this.ucSalesDayTarget.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSalesDayTarget.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSalesDayTarget.UseTranscationScope = false;
            this.ucSalesDayTarget.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // autoNumber2
            // 
            this.autoNumber2.Active = true;
            this.autoNumber2.AutoNoID = "AutoKey";
            this.autoNumber2.Description = null;
            this.autoNumber2.GetFixed = "";
            this.autoNumber2.isNumFill = false;
            this.autoNumber2.Name = "autoNumber2";
            this.autoNumber2.Number = null;
            this.autoNumber2.NumDig = 10;
            this.autoNumber2.OldVersion = false;
            this.autoNumber2.OverFlow = true;
            this.autoNumber2.StartValue = 1;
            this.autoNumber2.Step = 1;
            this.autoNumber2.TargetColumn = "AutoKey";
            this.autoNumber2.UpdateComp = this.ucSalesDayTarget;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMonthTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_SalesMonthTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDayTarget)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SalesMonthTarget;
        private Srvtools.UpdateComponent ucSalesMonthTarget;
        private Srvtools.InfoCommand View_SalesMonthTarget;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand SalesDayTarget;
        private Srvtools.UpdateComponent ucSalesDayTarget;
        private Srvtools.AutoNumber autoNumber2;
    }
}
