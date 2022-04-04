namespace sJBRecruitOverDuty
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.JBRecruitOverDuty = new Srvtools.InfoCommand(this.components);
            this.ucView_JBRecruitOverDuty = new Srvtools.UpdateComponent(this.components);
            this.View_View_JBRecruitOverDuty = new Srvtools.InfoCommand(this.components);
            this.YearMonth = new Srvtools.InfoCommand(this.components);
            this.SalesDept = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JBRecruitOverDuty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_JBRecruitOverDuty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDept)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // JBRecruitOverDuty
            // 
            this.JBRecruitOverDuty.CacheConnection = false;
            this.JBRecruitOverDuty.CommandText = "SELECT dbo.[View_JBRecruitOverDuty].* FROM dbo.[View_JBRecruitOverDuty]";
            this.JBRecruitOverDuty.CommandTimeout = 30;
            this.JBRecruitOverDuty.CommandType = System.Data.CommandType.Text;
            this.JBRecruitOverDuty.DynamicTableName = false;
            this.JBRecruitOverDuty.EEPAlias = null;
            this.JBRecruitOverDuty.EncodingAfter = null;
            this.JBRecruitOverDuty.EncodingBefore = "Windows-1252";
            this.JBRecruitOverDuty.EncodingConvert = null;
            this.JBRecruitOverDuty.InfoConnection = this.InfoConnection1;
            this.JBRecruitOverDuty.MultiSetWhere = false;
            this.JBRecruitOverDuty.Name = "JBRecruitOverDuty";
            this.JBRecruitOverDuty.NotificationAutoEnlist = false;
            this.JBRecruitOverDuty.SecExcept = null;
            this.JBRecruitOverDuty.SecFieldName = null;
            this.JBRecruitOverDuty.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.JBRecruitOverDuty.SelectPaging = false;
            this.JBRecruitOverDuty.SelectTop = 0;
            this.JBRecruitOverDuty.SiteControl = false;
            this.JBRecruitOverDuty.SiteFieldName = null;
            this.JBRecruitOverDuty.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucView_JBRecruitOverDuty
            // 
            this.ucView_JBRecruitOverDuty.AutoTrans = true;
            this.ucView_JBRecruitOverDuty.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CUSTOMERID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustomerShortName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EMPID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Namec";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "YearMonth";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "DutyQty";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucView_JBRecruitOverDuty.FieldAttrs.Add(fieldAttr1);
            this.ucView_JBRecruitOverDuty.FieldAttrs.Add(fieldAttr2);
            this.ucView_JBRecruitOverDuty.FieldAttrs.Add(fieldAttr3);
            this.ucView_JBRecruitOverDuty.FieldAttrs.Add(fieldAttr4);
            this.ucView_JBRecruitOverDuty.FieldAttrs.Add(fieldAttr5);
            this.ucView_JBRecruitOverDuty.FieldAttrs.Add(fieldAttr6);
            this.ucView_JBRecruitOverDuty.LogInfo = null;
            this.ucView_JBRecruitOverDuty.Name = "ucView_JBRecruitOverDuty";
            this.ucView_JBRecruitOverDuty.RowAffectsCheck = true;
            this.ucView_JBRecruitOverDuty.SelectCmd = this.JBRecruitOverDuty;
            this.ucView_JBRecruitOverDuty.SelectCmdForUpdate = null;
            this.ucView_JBRecruitOverDuty.SendSQLCmd = true;
            this.ucView_JBRecruitOverDuty.ServerModify = true;
            this.ucView_JBRecruitOverDuty.ServerModifyGetMax = false;
            this.ucView_JBRecruitOverDuty.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucView_JBRecruitOverDuty.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucView_JBRecruitOverDuty.UseTranscationScope = false;
            this.ucView_JBRecruitOverDuty.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_View_JBRecruitOverDuty
            // 
            this.View_View_JBRecruitOverDuty.CacheConnection = false;
            this.View_View_JBRecruitOverDuty.CommandText = "SELECT * FROM dbo.[View_JBRecruitOverDuty]";
            this.View_View_JBRecruitOverDuty.CommandTimeout = 30;
            this.View_View_JBRecruitOverDuty.CommandType = System.Data.CommandType.Text;
            this.View_View_JBRecruitOverDuty.DynamicTableName = false;
            this.View_View_JBRecruitOverDuty.EEPAlias = null;
            this.View_View_JBRecruitOverDuty.EncodingAfter = null;
            this.View_View_JBRecruitOverDuty.EncodingBefore = "Windows-1252";
            this.View_View_JBRecruitOverDuty.EncodingConvert = null;
            this.View_View_JBRecruitOverDuty.InfoConnection = this.InfoConnection1;
            this.View_View_JBRecruitOverDuty.MultiSetWhere = false;
            this.View_View_JBRecruitOverDuty.Name = "View_View_JBRecruitOverDuty";
            this.View_View_JBRecruitOverDuty.NotificationAutoEnlist = false;
            this.View_View_JBRecruitOverDuty.SecExcept = null;
            this.View_View_JBRecruitOverDuty.SecFieldName = null;
            this.View_View_JBRecruitOverDuty.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_View_JBRecruitOverDuty.SelectPaging = false;
            this.View_View_JBRecruitOverDuty.SelectTop = 0;
            this.View_View_JBRecruitOverDuty.SiteControl = false;
            this.View_View_JBRecruitOverDuty.SiteFieldName = null;
            this.View_View_JBRecruitOverDuty.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // YearMonth
            // 
            this.YearMonth.CacheConnection = false;
            this.YearMonth.CommandText = "SELECT  Substring(YearMonth,1,6) AS YearMonth\r\nFrom JBRecruit.dbo.PayMaster \r\nGro" +
    "up By  Substring(YearMonth,1,6) Order By  Substring(YearMonth,1,6) Desc";
            this.YearMonth.CommandTimeout = 30;
            this.YearMonth.CommandType = System.Data.CommandType.Text;
            this.YearMonth.DynamicTableName = false;
            this.YearMonth.EEPAlias = null;
            this.YearMonth.EncodingAfter = null;
            this.YearMonth.EncodingBefore = "Windows-1252";
            this.YearMonth.EncodingConvert = null;
            this.YearMonth.InfoConnection = this.InfoConnection1;
            this.YearMonth.MultiSetWhere = false;
            this.YearMonth.Name = "YearMonth";
            this.YearMonth.NotificationAutoEnlist = false;
            this.YearMonth.SecExcept = null;
            this.YearMonth.SecFieldName = null;
            this.YearMonth.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.YearMonth.SelectPaging = false;
            this.YearMonth.SelectTop = 0;
            this.YearMonth.SiteControl = false;
            this.YearMonth.SiteFieldName = null;
            this.YearMonth.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SalesDept
            // 
            this.SalesDept.CacheConnection = false;
            this.SalesDept.CommandText = "select ListID as ID,ListContent as Name\r\nfrom  jbrecruit.dbo.ListTable\n \r\nwhere L" +
    "istID <> 0 and ListCategory = \'salesdepartment\'\r\norder by DisplaySort";
            this.SalesDept.CommandTimeout = 30;
            this.SalesDept.CommandType = System.Data.CommandType.Text;
            this.SalesDept.DynamicTableName = false;
            this.SalesDept.EEPAlias = null;
            this.SalesDept.EncodingAfter = null;
            this.SalesDept.EncodingBefore = "Windows-1252";
            this.SalesDept.EncodingConvert = null;
            this.SalesDept.InfoConnection = this.InfoConnection1;
            this.SalesDept.MultiSetWhere = false;
            this.SalesDept.Name = "SalesDept";
            this.SalesDept.NotificationAutoEnlist = false;
            this.SalesDept.SecExcept = null;
            this.SalesDept.SecFieldName = null;
            this.SalesDept.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SalesDept.SelectPaging = false;
            this.SalesDept.SelectTop = 0;
            this.SalesDept.SiteControl = false;
            this.SalesDept.SiteFieldName = null;
            this.SalesDept.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JBRecruitOverDuty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_JBRecruitOverDuty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesDept)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand JBRecruitOverDuty;
        private Srvtools.UpdateComponent ucView_JBRecruitOverDuty;
        private Srvtools.InfoCommand View_View_JBRecruitOverDuty;
        private Srvtools.InfoCommand YearMonth;
        private Srvtools.InfoCommand SalesDept;
    }
}
