namespace sPettyCashYM
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.PettyCashYM = new Srvtools.InfoCommand(this.components);
            this.ucPettyCashYM = new Srvtools.UpdateComponent(this.components);
            this.View_PettyCashYM = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PettyCashYM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PettyCashYM)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckYearMonthNODul";
            service1.NonLogin = false;
            service1.ServiceName = "CheckYearMonthNODul";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // PettyCashYM
            // 
            this.PettyCashYM.CacheConnection = false;
            this.PettyCashYM.CommandText = "SELECT dbo.[PettyCashYM].* FROM dbo.[PettyCashYM]\r\nORDER BY YearMonth Desc";
            this.PettyCashYM.CommandTimeout = 30;
            this.PettyCashYM.CommandType = System.Data.CommandType.Text;
            this.PettyCashYM.DynamicTableName = false;
            this.PettyCashYM.EEPAlias = null;
            this.PettyCashYM.EncodingAfter = null;
            this.PettyCashYM.EncodingBefore = "Windows-1252";
            this.PettyCashYM.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "YearMonth";
            this.PettyCashYM.KeyFields.Add(keyItem1);
            this.PettyCashYM.MultiSetWhere = false;
            this.PettyCashYM.Name = "PettyCashYM";
            this.PettyCashYM.NotificationAutoEnlist = false;
            this.PettyCashYM.SecExcept = null;
            this.PettyCashYM.SecFieldName = null;
            this.PettyCashYM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PettyCashYM.SelectPaging = false;
            this.PettyCashYM.SelectTop = 0;
            this.PettyCashYM.SiteControl = false;
            this.PettyCashYM.SiteFieldName = null;
            this.PettyCashYM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPettyCashYM
            // 
            this.ucPettyCashYM.AutoTrans = true;
            this.ucPettyCashYM.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "YearMonth";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "StdDate";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EndDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CardOilRate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "MotoOilRate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr6.DefaultValue = "_username";
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr8.DefaultValue = "_username";
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "LastUpdateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr1);
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr2);
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr3);
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr4);
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr5);
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr6);
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr7);
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr8);
            this.ucPettyCashYM.FieldAttrs.Add(fieldAttr9);
            this.ucPettyCashYM.LogInfo = null;
            this.ucPettyCashYM.Name = "ucPettyCashYM";
            this.ucPettyCashYM.RowAffectsCheck = true;
            this.ucPettyCashYM.SelectCmd = this.PettyCashYM;
            this.ucPettyCashYM.SelectCmdForUpdate = null;
            this.ucPettyCashYM.ServerModify = true;
            this.ucPettyCashYM.ServerModifyGetMax = false;
            this.ucPettyCashYM.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPettyCashYM.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPettyCashYM.UseTranscationScope = false;
            this.ucPettyCashYM.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucPettyCashYM.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucPettyCashYM_BeforeInsert);
            this.ucPettyCashYM.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucPettyCashYM_BeforeModify);
            // 
            // View_PettyCashYM
            // 
            this.View_PettyCashYM.CacheConnection = false;
            this.View_PettyCashYM.CommandText = "SELECT * FROM dbo.[PettyCashYM]";
            this.View_PettyCashYM.CommandTimeout = 30;
            this.View_PettyCashYM.CommandType = System.Data.CommandType.Text;
            this.View_PettyCashYM.DynamicTableName = false;
            this.View_PettyCashYM.EEPAlias = null;
            this.View_PettyCashYM.EncodingAfter = null;
            this.View_PettyCashYM.EncodingBefore = "Windows-1252";
            this.View_PettyCashYM.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "YearMonth";
            this.View_PettyCashYM.KeyFields.Add(keyItem2);
            this.View_PettyCashYM.MultiSetWhere = false;
            this.View_PettyCashYM.Name = "View_PettyCashYM";
            this.View_PettyCashYM.NotificationAutoEnlist = false;
            this.View_PettyCashYM.SecExcept = null;
            this.View_PettyCashYM.SecFieldName = null;
            this.View_PettyCashYM.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_PettyCashYM.SelectPaging = false;
            this.View_PettyCashYM.SelectTop = 0;
            this.View_PettyCashYM.SiteControl = false;
            this.View_PettyCashYM.SiteFieldName = null;
            this.View_PettyCashYM.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PettyCashYM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PettyCashYM)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand PettyCashYM;
        private Srvtools.UpdateComponent ucPettyCashYM;
        private Srvtools.InfoCommand View_PettyCashYM;
    }
}
