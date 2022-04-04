namespace sNHI2Declare
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
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.NHI2DeclareBill = new Srvtools.InfoCommand(this.components);
            this.ucView_2NHIDeclareBill = new Srvtools.UpdateComponent(this.components);
            this.NHI2DeclareAmount = new Srvtools.InfoCommand(this.components);
            this.ucView_2NHIDeclareAmount = new Srvtools.UpdateComponent(this.components);
            this.View_View_2NHIDeclareBill = new Srvtools.InfoCommand(this.components);
            this.View_View_2NHIDeclareAmount = new Srvtools.InfoCommand(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.YearMonth = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NHI2DeclareBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NHI2DeclareAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_2NHIDeclareBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_2NHIDeclareAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearMonth)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // NHI2DeclareBill
            // 
            this.NHI2DeclareBill.CacheConnection = false;
            this.NHI2DeclareBill.CommandText = "SELECT dbo.[View_2NHIDeclareBill].* FROM dbo.[View_2NHIDeclareBill]";
            this.NHI2DeclareBill.CommandTimeout = 30;
            this.NHI2DeclareBill.CommandType = System.Data.CommandType.Text;
            this.NHI2DeclareBill.DynamicTableName = false;
            this.NHI2DeclareBill.EEPAlias = null;
            this.NHI2DeclareBill.EncodingAfter = null;
            this.NHI2DeclareBill.EncodingBefore = "Windows-1252";
            this.NHI2DeclareBill.EncodingConvert = null;
            this.NHI2DeclareBill.InfoConnection = this.InfoConnection1;
            this.NHI2DeclareBill.MultiSetWhere = false;
            this.NHI2DeclareBill.Name = "NHI2DeclareBill";
            this.NHI2DeclareBill.NotificationAutoEnlist = false;
            this.NHI2DeclareBill.SecExcept = null;
            this.NHI2DeclareBill.SecFieldName = null;
            this.NHI2DeclareBill.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.NHI2DeclareBill.SelectPaging = false;
            this.NHI2DeclareBill.SelectTop = 0;
            this.NHI2DeclareBill.SiteControl = false;
            this.NHI2DeclareBill.SiteFieldName = null;
            this.NHI2DeclareBill.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucView_2NHIDeclareBill
            // 
            this.ucView_2NHIDeclareBill.AutoTrans = true;
            this.ucView_2NHIDeclareBill.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "YearMonth";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SaryID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "InsGroupID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "TaxNo";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "IDNumber";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "NameC";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Addr";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucView_2NHIDeclareBill.FieldAttrs.Add(fieldAttr1);
            this.ucView_2NHIDeclareBill.FieldAttrs.Add(fieldAttr2);
            this.ucView_2NHIDeclareBill.FieldAttrs.Add(fieldAttr3);
            this.ucView_2NHIDeclareBill.FieldAttrs.Add(fieldAttr4);
            this.ucView_2NHIDeclareBill.FieldAttrs.Add(fieldAttr5);
            this.ucView_2NHIDeclareBill.FieldAttrs.Add(fieldAttr6);
            this.ucView_2NHIDeclareBill.FieldAttrs.Add(fieldAttr7);
            this.ucView_2NHIDeclareBill.LogInfo = null;
            this.ucView_2NHIDeclareBill.Name = "ucView_2NHIDeclareBill";
            this.ucView_2NHIDeclareBill.RowAffectsCheck = true;
            this.ucView_2NHIDeclareBill.SelectCmd = this.NHI2DeclareBill;
            this.ucView_2NHIDeclareBill.SelectCmdForUpdate = null;
            this.ucView_2NHIDeclareBill.SendSQLCmd = true;
            this.ucView_2NHIDeclareBill.ServerModify = true;
            this.ucView_2NHIDeclareBill.ServerModifyGetMax = false;
            this.ucView_2NHIDeclareBill.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucView_2NHIDeclareBill.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucView_2NHIDeclareBill.UseTranscationScope = false;
            this.ucView_2NHIDeclareBill.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // NHI2DeclareAmount
            // 
            this.NHI2DeclareAmount.CacheConnection = false;
            this.NHI2DeclareAmount.CommandText = "SELECT dbo.[View_2NHIDeclareAmount].* FROM dbo.[View_2NHIDeclareAmount]";
            this.NHI2DeclareAmount.CommandTimeout = 30;
            this.NHI2DeclareAmount.CommandType = System.Data.CommandType.Text;
            this.NHI2DeclareAmount.DynamicTableName = false;
            this.NHI2DeclareAmount.EEPAlias = null;
            this.NHI2DeclareAmount.EncodingAfter = null;
            this.NHI2DeclareAmount.EncodingBefore = "Windows-1252";
            this.NHI2DeclareAmount.EncodingConvert = null;
            this.NHI2DeclareAmount.InfoConnection = this.InfoConnection1;
            this.NHI2DeclareAmount.MultiSetWhere = false;
            this.NHI2DeclareAmount.Name = "NHI2DeclareAmount";
            this.NHI2DeclareAmount.NotificationAutoEnlist = false;
            this.NHI2DeclareAmount.SecExcept = null;
            this.NHI2DeclareAmount.SecFieldName = null;
            this.NHI2DeclareAmount.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.NHI2DeclareAmount.SelectPaging = false;
            this.NHI2DeclareAmount.SelectTop = 0;
            this.NHI2DeclareAmount.SiteControl = false;
            this.NHI2DeclareAmount.SiteFieldName = null;
            this.NHI2DeclareAmount.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucView_2NHIDeclareAmount
            // 
            this.ucView_2NHIDeclareAmount.AutoTrans = true;
            this.ucView_2NHIDeclareAmount.ExceptJoin = false;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "YearMonth";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "InsGroupID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "HealthInsNo";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "TaxNo";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "IDNumber";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "NameC";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "所得給付日期";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "InComeType";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "Note1";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "Note2";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "Note3";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "Note4";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "Note5";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "Note6";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "Note7";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "Note8";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "Income";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "NHI2Amount";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr8);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr9);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr10);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr11);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr12);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr13);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr14);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr15);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr16);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr17);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr18);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr19);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr20);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr21);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr22);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr23);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr24);
            this.ucView_2NHIDeclareAmount.FieldAttrs.Add(fieldAttr25);
            this.ucView_2NHIDeclareAmount.LogInfo = null;
            this.ucView_2NHIDeclareAmount.Name = "ucView_2NHIDeclareAmount";
            this.ucView_2NHIDeclareAmount.RowAffectsCheck = true;
            this.ucView_2NHIDeclareAmount.SelectCmd = this.NHI2DeclareAmount;
            this.ucView_2NHIDeclareAmount.SelectCmdForUpdate = null;
            this.ucView_2NHIDeclareAmount.SendSQLCmd = true;
            this.ucView_2NHIDeclareAmount.ServerModify = true;
            this.ucView_2NHIDeclareAmount.ServerModifyGetMax = false;
            this.ucView_2NHIDeclareAmount.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucView_2NHIDeclareAmount.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucView_2NHIDeclareAmount.UseTranscationScope = false;
            this.ucView_2NHIDeclareAmount.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_View_2NHIDeclareBill
            // 
            this.View_View_2NHIDeclareBill.CacheConnection = false;
            this.View_View_2NHIDeclareBill.CommandText = "SELECT * FROM dbo.[View_2NHIDeclareBill]";
            this.View_View_2NHIDeclareBill.CommandTimeout = 30;
            this.View_View_2NHIDeclareBill.CommandType = System.Data.CommandType.Text;
            this.View_View_2NHIDeclareBill.DynamicTableName = false;
            this.View_View_2NHIDeclareBill.EEPAlias = null;
            this.View_View_2NHIDeclareBill.EncodingAfter = null;
            this.View_View_2NHIDeclareBill.EncodingBefore = "Windows-1252";
            this.View_View_2NHIDeclareBill.EncodingConvert = null;
            this.View_View_2NHIDeclareBill.InfoConnection = this.InfoConnection1;
            this.View_View_2NHIDeclareBill.MultiSetWhere = false;
            this.View_View_2NHIDeclareBill.Name = "View_View_2NHIDeclareBill";
            this.View_View_2NHIDeclareBill.NotificationAutoEnlist = false;
            this.View_View_2NHIDeclareBill.SecExcept = null;
            this.View_View_2NHIDeclareBill.SecFieldName = null;
            this.View_View_2NHIDeclareBill.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_View_2NHIDeclareBill.SelectPaging = false;
            this.View_View_2NHIDeclareBill.SelectTop = 0;
            this.View_View_2NHIDeclareBill.SiteControl = false;
            this.View_View_2NHIDeclareBill.SiteFieldName = null;
            this.View_View_2NHIDeclareBill.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_View_2NHIDeclareAmount
            // 
            this.View_View_2NHIDeclareAmount.CacheConnection = false;
            this.View_View_2NHIDeclareAmount.CommandText = "SELECT * FROM dbo.[View_2NHIDeclareAmount]";
            this.View_View_2NHIDeclareAmount.CommandTimeout = 30;
            this.View_View_2NHIDeclareAmount.CommandType = System.Data.CommandType.Text;
            this.View_View_2NHIDeclareAmount.DynamicTableName = false;
            this.View_View_2NHIDeclareAmount.EEPAlias = null;
            this.View_View_2NHIDeclareAmount.EncodingAfter = null;
            this.View_View_2NHIDeclareAmount.EncodingBefore = "Windows-1252";
            this.View_View_2NHIDeclareAmount.EncodingConvert = null;
            this.View_View_2NHIDeclareAmount.InfoConnection = this.InfoConnection1;
            this.View_View_2NHIDeclareAmount.MultiSetWhere = false;
            this.View_View_2NHIDeclareAmount.Name = "View_View_2NHIDeclareAmount";
            this.View_View_2NHIDeclareAmount.NotificationAutoEnlist = false;
            this.View_View_2NHIDeclareAmount.SecExcept = null;
            this.View_View_2NHIDeclareAmount.SecFieldName = null;
            this.View_View_2NHIDeclareAmount.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_View_2NHIDeclareAmount.SelectPaging = false;
            this.View_View_2NHIDeclareAmount.SelectTop = 0;
            this.View_View_2NHIDeclareAmount.SiteControl = false;
            this.View_View_2NHIDeclareAmount.SiteFieldName = null;
            this.View_View_2NHIDeclareAmount.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "SELECT INSGROUPID,INSGROUPSHORTNAME  FROM INSGROUP ";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = null;
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "INSGROUPID";
            this.InsGroup.KeyFields.Add(keyItem1);
            this.InsGroup.MultiSetWhere = false;
            this.InsGroup.Name = "InsGroup";
            this.InsGroup.NotificationAutoEnlist = false;
            this.InsGroup.SecExcept = null;
            this.InsGroup.SecFieldName = null;
            this.InsGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InsGroup.SelectPaging = false;
            this.InsGroup.SelectTop = 0;
            this.InsGroup.SiteControl = false;
            this.InsGroup.SiteFieldName = null;
            this.InsGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // YearMonth
            // 
            this.YearMonth.CacheConnection = false;
            this.YearMonth.CommandText = resources.GetString("YearMonth.CommandText");
            this.YearMonth.CommandTimeout = 30;
            this.YearMonth.CommandType = System.Data.CommandType.Text;
            this.YearMonth.DynamicTableName = false;
            this.YearMonth.EEPAlias = null;
            this.YearMonth.EncodingAfter = null;
            this.YearMonth.EncodingBefore = "Windows-1252";
            this.YearMonth.EncodingConvert = null;
            this.YearMonth.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "INSGROUPID";
            this.YearMonth.KeyFields.Add(keyItem2);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NHI2DeclareBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NHI2DeclareAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_2NHIDeclareBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_View_2NHIDeclareAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearMonth)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand NHI2DeclareBill;
        private Srvtools.UpdateComponent ucView_2NHIDeclareBill;
        private Srvtools.InfoCommand NHI2DeclareAmount;
        private Srvtools.UpdateComponent ucView_2NHIDeclareAmount;
        private Srvtools.InfoCommand View_View_2NHIDeclareBill;
        private Srvtools.InfoCommand View_View_2NHIDeclareAmount;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.InfoCommand YearMonth;
    }
}
