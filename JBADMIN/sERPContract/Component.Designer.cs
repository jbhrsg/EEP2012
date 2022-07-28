namespace sERPContract
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPContract = new Srvtools.InfoCommand(this.components);
            this.ucERPContract = new Srvtools.UpdateComponent(this.components);
            this.View_ERPContract = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContract)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPContract
            // 
            this.ERPContract.CacheConnection = false;
            this.ERPContract.CommandText = "SELECT dbo.[ERPContract].* FROM dbo.[ERPContract]";
            this.ERPContract.CommandTimeout = 30;
            this.ERPContract.CommandType = System.Data.CommandType.Text;
            this.ERPContract.DynamicTableName = false;
            this.ERPContract.EEPAlias = null;
            this.ERPContract.EncodingAfter = null;
            this.ERPContract.EncodingBefore = "Windows-1252";
            this.ERPContract.EncodingConvert = null;
            this.ERPContract.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ContractKey";
            this.ERPContract.KeyFields.Add(keyItem2);
            this.ERPContract.MultiSetWhere = false;
            this.ERPContract.Name = "ERPContract";
            this.ERPContract.NotificationAutoEnlist = false;
            this.ERPContract.SecExcept = null;
            this.ERPContract.SecFieldName = null;
            this.ERPContract.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPContract.SelectPaging = false;
            this.ERPContract.SelectTop = 0;
            this.ERPContract.SiteControl = false;
            this.ERPContract.SiteFieldName = null;
            this.ERPContract.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPContract
            // 
            this.ucERPContract.AutoTrans = true;
            this.ucERPContract.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ContractKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ParentKey";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ContractNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ContractName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ContractB";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ContractClass";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "BeginDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "EndDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Remarks";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Keeper";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Attachment1";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Attachment2";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "Attachment3";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "Attachment4";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "Attachment5";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            this.ucERPContract.FieldAttrs.Add(fieldAttr1);
            this.ucERPContract.FieldAttrs.Add(fieldAttr2);
            this.ucERPContract.FieldAttrs.Add(fieldAttr3);
            this.ucERPContract.FieldAttrs.Add(fieldAttr4);
            this.ucERPContract.FieldAttrs.Add(fieldAttr5);
            this.ucERPContract.FieldAttrs.Add(fieldAttr6);
            this.ucERPContract.FieldAttrs.Add(fieldAttr7);
            this.ucERPContract.FieldAttrs.Add(fieldAttr8);
            this.ucERPContract.FieldAttrs.Add(fieldAttr9);
            this.ucERPContract.FieldAttrs.Add(fieldAttr10);
            this.ucERPContract.FieldAttrs.Add(fieldAttr11);
            this.ucERPContract.FieldAttrs.Add(fieldAttr12);
            this.ucERPContract.FieldAttrs.Add(fieldAttr13);
            this.ucERPContract.FieldAttrs.Add(fieldAttr14);
            this.ucERPContract.FieldAttrs.Add(fieldAttr15);
            this.ucERPContract.LogInfo = null;
            this.ucERPContract.Name = "ucERPContract";
            this.ucERPContract.RowAffectsCheck = true;
            this.ucERPContract.SelectCmd = this.ERPContract;
            this.ucERPContract.SelectCmdForUpdate = null;
            this.ucERPContract.SendSQLCmd = true;
            this.ucERPContract.ServerModify = true;
            this.ucERPContract.ServerModifyGetMax = false;
            this.ucERPContract.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPContract.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPContract.UseTranscationScope = false;
            this.ucERPContract.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ERPContract
            // 
            this.View_ERPContract.CacheConnection = false;
            this.View_ERPContract.CommandText = "SELECT * FROM dbo.[ERPContract]";
            this.View_ERPContract.CommandTimeout = 30;
            this.View_ERPContract.CommandType = System.Data.CommandType.Text;
            this.View_ERPContract.DynamicTableName = false;
            this.View_ERPContract.EEPAlias = null;
            this.View_ERPContract.EncodingAfter = null;
            this.View_ERPContract.EncodingBefore = "Windows-1252";
            this.View_ERPContract.EncodingConvert = null;
            this.View_ERPContract.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ContractKey";
            this.View_ERPContract.KeyFields.Add(keyItem3);
            this.View_ERPContract.MultiSetWhere = false;
            this.View_ERPContract.Name = "View_ERPContract";
            this.View_ERPContract.NotificationAutoEnlist = false;
            this.View_ERPContract.SecExcept = null;
            this.View_ERPContract.SecFieldName = null;
            this.View_ERPContract.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPContract.SelectPaging = false;
            this.View_ERPContract.SelectTop = 0;
            this.View_ERPContract.SiteControl = false;
            this.View_ERPContract.SiteFieldName = null;
            this.View_ERPContract.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "AutoNO1";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = null;
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 1;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "";
            this.autoNumber1.UpdateComp = null;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContract)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPContract;
        private Srvtools.UpdateComponent ucERPContract;
        private Srvtools.InfoCommand View_ERPContract;
        private Srvtools.AutoNumber autoNumber1;
    }
}
