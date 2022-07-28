namespace JBERP_HRPostPublishes
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
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HRPostPublishes = new Srvtools.InfoCommand(this.components);
            this.ucHRPostPublishes = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRPostPublishes)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "MediaPost";
            // 
            // HRPostPublishes
            // 
            this.HRPostPublishes.CacheConnection = false;
            this.HRPostPublishes.CommandText = "SELECT dbo.[HRPostPublishes].* FROM dbo.[HRPostPublishes]";
            this.HRPostPublishes.CommandTimeout = 30;
            this.HRPostPublishes.CommandType = System.Data.CommandType.Text;
            this.HRPostPublishes.DynamicTableName = false;
            this.HRPostPublishes.EEPAlias = null;
            this.HRPostPublishes.EncodingAfter = null;
            this.HRPostPublishes.EncodingBefore = "Windows-1252";
            this.HRPostPublishes.EncodingConvert = null;
            this.HRPostPublishes.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "UID";
            this.HRPostPublishes.KeyFields.Add(keyItem1);
            this.HRPostPublishes.MultiSetWhere = false;
            this.HRPostPublishes.Name = "HRPostPublishes";
            this.HRPostPublishes.NotificationAutoEnlist = false;
            this.HRPostPublishes.SecExcept = null;
            this.HRPostPublishes.SecFieldName = null;
            this.HRPostPublishes.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRPostPublishes.SelectPaging = false;
            this.HRPostPublishes.SelectTop = 0;
            this.HRPostPublishes.SiteControl = false;
            this.HRPostPublishes.SiteFieldName = null;
            this.HRPostPublishes.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHRPostPublishes
            // 
            this.ucHRPostPublishes.AutoTrans = true;
            this.ucHRPostPublishes.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "SalesMasterNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateBy";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "TotalSalesQty";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "KeepDays";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "KeepDaysAlert";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "SalesTypeID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "DMTypeID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ViewAreaID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "SalesQty";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Commission";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "PublishCount";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "PresentCount";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "PresentWNewsCount";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CustAmt";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CustShortName";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "IsActive";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "AcceptDate";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "IsConvertNexMonth";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr1);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr2);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr3);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr4);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr5);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr6);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr7);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr8);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr9);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr10);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr11);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr12);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr13);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr14);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr15);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr16);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr17);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr18);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr19);
            this.ucHRPostPublishes.FieldAttrs.Add(fieldAttr20);
            this.ucHRPostPublishes.LogInfo = null;
            this.ucHRPostPublishes.Name = "ucERPSalesMaster";
            this.ucHRPostPublishes.RowAffectsCheck = true;
            this.ucHRPostPublishes.SelectCmd = this.HRPostPublishes;
            this.ucHRPostPublishes.SelectCmdForUpdate = null;
            this.ucHRPostPublishes.SendSQLCmd = true;
            this.ucHRPostPublishes.ServerModify = true;
            this.ucHRPostPublishes.ServerModifyGetMax = false;
            this.ucHRPostPublishes.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHRPostPublishes.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHRPostPublishes.UseTranscationScope = false;
            this.ucHRPostPublishes.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRPostPublishes)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HRPostPublishes;
        private Srvtools.UpdateComponent ucHRPostPublishes;
    }
}
