namespace sContinueEmploy
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ContinueEmployMaster = new Srvtools.InfoCommand(this.components);
            this.ucContinueEmployMaster = new Srvtools.UpdateComponent(this.components);
            this.ContinueEmployDetail = new Srvtools.InfoCommand(this.components);
            this.ucContinueEmployDetail = new Srvtools.UpdateComponent(this.components);
            this.idContinueEmployMaster_ContinueEmployDetail = new Srvtools.InfoDataSource(this.components);
            this.View_ContinueEmployMaster = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueEmployMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueEmployDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ContinueEmployMaster)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ContinueEmployMaster
            // 
            this.ContinueEmployMaster.CacheConnection = false;
            this.ContinueEmployMaster.CommandText = "SELECT dbo.[ContinueEmployMaster].* FROM dbo.[ContinueEmployMaster]";
            this.ContinueEmployMaster.CommandTimeout = 30;
            this.ContinueEmployMaster.CommandType = System.Data.CommandType.Text;
            this.ContinueEmployMaster.DynamicTableName = false;
            this.ContinueEmployMaster.EEPAlias = null;
            this.ContinueEmployMaster.EncodingAfter = null;
            this.ContinueEmployMaster.EncodingBefore = "Windows-1252";
            this.ContinueEmployMaster.EncodingConvert = null;
            this.ContinueEmployMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ContinueEmployNO";
            this.ContinueEmployMaster.KeyFields.Add(keyItem1);
            this.ContinueEmployMaster.MultiSetWhere = false;
            this.ContinueEmployMaster.Name = "ContinueEmployMaster";
            this.ContinueEmployMaster.NotificationAutoEnlist = false;
            this.ContinueEmployMaster.SecExcept = null;
            this.ContinueEmployMaster.SecFieldName = null;
            this.ContinueEmployMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ContinueEmployMaster.SelectPaging = false;
            this.ContinueEmployMaster.SelectTop = 0;
            this.ContinueEmployMaster.SiteControl = false;
            this.ContinueEmployMaster.SiteFieldName = null;
            this.ContinueEmployMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucContinueEmployMaster
            // 
            this.ucContinueEmployMaster.AutoTrans = true;
            this.ucContinueEmployMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = true;
            fieldAttr1.DataField = "ContinueEmployNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "Employer";
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
            this.ucContinueEmployMaster.FieldAttrs.Add(fieldAttr1);
            this.ucContinueEmployMaster.FieldAttrs.Add(fieldAttr2);
            this.ucContinueEmployMaster.FieldAttrs.Add(fieldAttr3);
            this.ucContinueEmployMaster.FieldAttrs.Add(fieldAttr4);
            this.ucContinueEmployMaster.LogInfo = null;
            this.ucContinueEmployMaster.Name = null;
            this.ucContinueEmployMaster.RowAffectsCheck = true;
            this.ucContinueEmployMaster.SelectCmd = this.ContinueEmployMaster;
            this.ucContinueEmployMaster.SelectCmdForUpdate = null;
            this.ucContinueEmployMaster.SendSQLCmd = true;
            this.ucContinueEmployMaster.ServerModify = true;
            this.ucContinueEmployMaster.ServerModifyGetMax = false;
            this.ucContinueEmployMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucContinueEmployMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucContinueEmployMaster.UseTranscationScope = false;
            this.ucContinueEmployMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ContinueEmployDetail
            // 
            this.ContinueEmployDetail.CacheConnection = false;
            this.ContinueEmployDetail.CommandText = "SELECT dbo.[ContinueEmployDetail].* FROM dbo.[ContinueEmployDetail]";
            this.ContinueEmployDetail.CommandTimeout = 30;
            this.ContinueEmployDetail.CommandType = System.Data.CommandType.Text;
            this.ContinueEmployDetail.DynamicTableName = false;
            this.ContinueEmployDetail.EEPAlias = null;
            this.ContinueEmployDetail.EncodingAfter = null;
            this.ContinueEmployDetail.EncodingBefore = "Windows-1252";
            this.ContinueEmployDetail.EncodingConvert = null;
            this.ContinueEmployDetail.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            keyItem3.KeyName = "ContinueEmployNO";
            this.ContinueEmployDetail.KeyFields.Add(keyItem2);
            this.ContinueEmployDetail.KeyFields.Add(keyItem3);
            this.ContinueEmployDetail.MultiSetWhere = false;
            this.ContinueEmployDetail.Name = "ContinueEmployDetail";
            this.ContinueEmployDetail.NotificationAutoEnlist = false;
            this.ContinueEmployDetail.SecExcept = null;
            this.ContinueEmployDetail.SecFieldName = null;
            this.ContinueEmployDetail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ContinueEmployDetail.SelectPaging = false;
            this.ContinueEmployDetail.SelectTop = 0;
            this.ContinueEmployDetail.SiteControl = false;
            this.ContinueEmployDetail.SiteFieldName = null;
            this.ContinueEmployDetail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucContinueEmployDetail
            // 
            this.ucContinueEmployDetail.AutoTrans = true;
            this.ucContinueEmployDetail.ExceptJoin = false;
            fieldAttr5.CheckNull = true;
            fieldAttr5.DataField = "AutoKey";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ContinueEmployNO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LaborName";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Gender";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Country";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "ImmigrationDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "DueDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CEConfirmNO";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            this.ucContinueEmployDetail.FieldAttrs.Add(fieldAttr5);
            this.ucContinueEmployDetail.FieldAttrs.Add(fieldAttr6);
            this.ucContinueEmployDetail.FieldAttrs.Add(fieldAttr7);
            this.ucContinueEmployDetail.FieldAttrs.Add(fieldAttr8);
            this.ucContinueEmployDetail.FieldAttrs.Add(fieldAttr9);
            this.ucContinueEmployDetail.FieldAttrs.Add(fieldAttr10);
            this.ucContinueEmployDetail.FieldAttrs.Add(fieldAttr11);
            this.ucContinueEmployDetail.FieldAttrs.Add(fieldAttr12);
            this.ucContinueEmployDetail.LogInfo = null;
            this.ucContinueEmployDetail.Name = null;
            this.ucContinueEmployDetail.RowAffectsCheck = true;
            this.ucContinueEmployDetail.SelectCmd = this.ContinueEmployDetail;
            this.ucContinueEmployDetail.SelectCmdForUpdate = null;
            this.ucContinueEmployDetail.SendSQLCmd = true;
            this.ucContinueEmployDetail.ServerModify = true;
            this.ucContinueEmployDetail.ServerModifyGetMax = false;
            this.ucContinueEmployDetail.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucContinueEmployDetail.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucContinueEmployDetail.UseTranscationScope = false;
            this.ucContinueEmployDetail.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idContinueEmployMaster_ContinueEmployDetail
            // 
            this.idContinueEmployMaster_ContinueEmployDetail.Detail = this.ContinueEmployDetail;
            columnItem1.FieldName = "ContinueEmployNO";
            this.idContinueEmployMaster_ContinueEmployDetail.DetailColumns.Add(columnItem1);
            this.idContinueEmployMaster_ContinueEmployDetail.DynamicTableName = false;
            this.idContinueEmployMaster_ContinueEmployDetail.Master = this.ContinueEmployMaster;
            columnItem2.FieldName = "ContinueEmployNO";
            this.idContinueEmployMaster_ContinueEmployDetail.MasterColumns.Add(columnItem2);
            // 
            // View_ContinueEmployMaster
            // 
            this.View_ContinueEmployMaster.CacheConnection = false;
            this.View_ContinueEmployMaster.CommandText = "SELECT * FROM dbo.[ContinueEmployMaster]";
            this.View_ContinueEmployMaster.CommandTimeout = 30;
            this.View_ContinueEmployMaster.CommandType = System.Data.CommandType.Text;
            this.View_ContinueEmployMaster.DynamicTableName = false;
            this.View_ContinueEmployMaster.EEPAlias = null;
            this.View_ContinueEmployMaster.EncodingAfter = null;
            this.View_ContinueEmployMaster.EncodingBefore = "Windows-1252";
            this.View_ContinueEmployMaster.EncodingConvert = null;
            this.View_ContinueEmployMaster.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ContinueEmployNO";
            this.View_ContinueEmployMaster.KeyFields.Add(keyItem4);
            this.View_ContinueEmployMaster.MultiSetWhere = false;
            this.View_ContinueEmployMaster.Name = "View_ContinueEmployMaster";
            this.View_ContinueEmployMaster.NotificationAutoEnlist = false;
            this.View_ContinueEmployMaster.SecExcept = null;
            this.View_ContinueEmployMaster.SecFieldName = null;
            this.View_ContinueEmployMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ContinueEmployMaster.SelectPaging = false;
            this.View_ContinueEmployMaster.SelectTop = 0;
            this.View_ContinueEmployMaster.SiteControl = false;
            this.View_ContinueEmployMaster.SiteFieldName = null;
            this.View_ContinueEmployMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueEmployMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueEmployDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ContinueEmployMaster)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ContinueEmployMaster;
        private Srvtools.UpdateComponent ucContinueEmployMaster;
        private Srvtools.InfoCommand ContinueEmployDetail;
        private Srvtools.UpdateComponent ucContinueEmployDetail;
        private Srvtools.InfoDataSource idContinueEmployMaster_ContinueEmployDetail;
        private Srvtools.InfoCommand View_ContinueEmployMaster;
    }
}
