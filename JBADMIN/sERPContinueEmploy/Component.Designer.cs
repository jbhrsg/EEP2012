namespace sERPContinueEmploy
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
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
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
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPContinueEmployMaster = new Srvtools.InfoCommand(this.components);
            this.ucERPContinueEmployMaster = new Srvtools.UpdateComponent(this.components);
            this.ERPContinueEmployDetail = new Srvtools.InfoCommand(this.components);
            this.ucERPContinueEmployDetail = new Srvtools.UpdateComponent(this.components);
            this.idERPContinueEmployMaster_ERPContinueEmployDetail = new Srvtools.InfoDataSource(this.components);
            this.View_ERPContinueEmployMaster = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.cus = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.lab = new Srvtools.InfoCommand(this.components);
            this.View_ERPContinueEmploy = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContinueEmployMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContinueEmployDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContinueEmployMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContinueEmploy)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "ReportOrders";
            service1.NonLogin = false;
            service1.ServiceName = "ReportOrders";
            service2.DelegateName = "XFlowFlag";
            service2.NonLogin = false;
            service2.ServiceName = "XFlowFlag";
            service3.DelegateName = "DeleteDetail";
            service3.NonLogin = false;
            service3.ServiceName = "DeleteDetail";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPContinueEmployMaster
            // 
            this.ERPContinueEmployMaster.CacheConnection = false;
            this.ERPContinueEmployMaster.CommandText = "SELECT dbo.[ERPContinueEmployMaster].* FROM dbo.[ERPContinueEmployMaster]";
            this.ERPContinueEmployMaster.CommandTimeout = 30;
            this.ERPContinueEmployMaster.CommandType = System.Data.CommandType.Text;
            this.ERPContinueEmployMaster.DynamicTableName = false;
            this.ERPContinueEmployMaster.EEPAlias = null;
            this.ERPContinueEmployMaster.EncodingAfter = null;
            this.ERPContinueEmployMaster.EncodingBefore = "Windows-1252";
            this.ERPContinueEmployMaster.EncodingConvert = null;
            this.ERPContinueEmployMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ContinueEmployNO";
            this.ERPContinueEmployMaster.KeyFields.Add(keyItem1);
            this.ERPContinueEmployMaster.MultiSetWhere = false;
            this.ERPContinueEmployMaster.Name = "ERPContinueEmployMaster";
            this.ERPContinueEmployMaster.NotificationAutoEnlist = false;
            this.ERPContinueEmployMaster.SecExcept = null;
            this.ERPContinueEmployMaster.SecFieldName = null;
            this.ERPContinueEmployMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPContinueEmployMaster.SelectPaging = false;
            this.ERPContinueEmployMaster.SelectTop = 0;
            this.ERPContinueEmployMaster.SiteControl = false;
            this.ERPContinueEmployMaster.SiteFieldName = null;
            this.ERPContinueEmployMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPContinueEmployMaster
            // 
            this.ucERPContinueEmployMaster.AutoTrans = true;
            this.ucERPContinueEmployMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ContinueEmployNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CreateBy";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            this.ucERPContinueEmployMaster.FieldAttrs.Add(fieldAttr1);
            this.ucERPContinueEmployMaster.FieldAttrs.Add(fieldAttr2);
            this.ucERPContinueEmployMaster.FieldAttrs.Add(fieldAttr3);
            this.ucERPContinueEmployMaster.LogInfo = null;
            this.ucERPContinueEmployMaster.Name = "ucERPContinueEmployMaster";
            this.ucERPContinueEmployMaster.RowAffectsCheck = true;
            this.ucERPContinueEmployMaster.SelectCmd = this.ERPContinueEmployMaster;
            this.ucERPContinueEmployMaster.SelectCmdForUpdate = null;
            this.ucERPContinueEmployMaster.SendSQLCmd = true;
            this.ucERPContinueEmployMaster.ServerModify = true;
            this.ucERPContinueEmployMaster.ServerModifyGetMax = false;
            this.ucERPContinueEmployMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPContinueEmployMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPContinueEmployMaster.UseTranscationScope = false;
            this.ucERPContinueEmployMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ERPContinueEmployDetail
            // 
            this.ERPContinueEmployDetail.CacheConnection = false;
            this.ERPContinueEmployDetail.CommandText = "SELECT dbo.[ERPContinueEmployDetail].* FROM dbo.[ERPContinueEmployDetail] order b" +
    "y Employer desc,isRecontract desc,Transfer desc,ReturnHome desc";
            this.ERPContinueEmployDetail.CommandTimeout = 30;
            this.ERPContinueEmployDetail.CommandType = System.Data.CommandType.Text;
            this.ERPContinueEmployDetail.DynamicTableName = false;
            this.ERPContinueEmployDetail.EEPAlias = null;
            this.ERPContinueEmployDetail.EncodingAfter = null;
            this.ERPContinueEmployDetail.EncodingBefore = "Windows-1252";
            this.ERPContinueEmployDetail.EncodingConvert = null;
            this.ERPContinueEmployDetail.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            keyItem3.KeyName = "ContinueEmployNO";
            this.ERPContinueEmployDetail.KeyFields.Add(keyItem2);
            this.ERPContinueEmployDetail.KeyFields.Add(keyItem3);
            this.ERPContinueEmployDetail.MultiSetWhere = false;
            this.ERPContinueEmployDetail.Name = "ERPContinueEmployDetail";
            this.ERPContinueEmployDetail.NotificationAutoEnlist = false;
            this.ERPContinueEmployDetail.SecExcept = null;
            this.ERPContinueEmployDetail.SecFieldName = null;
            this.ERPContinueEmployDetail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPContinueEmployDetail.SelectPaging = false;
            this.ERPContinueEmployDetail.SelectTop = 0;
            this.ERPContinueEmployDetail.SiteControl = false;
            this.ERPContinueEmployDetail.SiteFieldName = null;
            this.ERPContinueEmployDetail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPContinueEmployDetail
            // 
            this.ucERPContinueEmployDetail.AutoTrans = true;
            this.ucERPContinueEmployDetail.ExceptJoin = false;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "AutoKey";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ContinueEmployNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LaborName";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Employer";
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
            fieldAttr12.DataField = "IsRecontract";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CEConfirmNO";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LetterClass";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LetterNO";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr4);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr5);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr6);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr7);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr8);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr9);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr10);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr11);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr12);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr13);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr14);
            this.ucERPContinueEmployDetail.FieldAttrs.Add(fieldAttr15);
            this.ucERPContinueEmployDetail.LogInfo = null;
            this.ucERPContinueEmployDetail.Name = "ucERPContinueEmployDetail";
            this.ucERPContinueEmployDetail.RowAffectsCheck = true;
            this.ucERPContinueEmployDetail.SelectCmd = this.ERPContinueEmployDetail;
            this.ucERPContinueEmployDetail.SelectCmdForUpdate = null;
            this.ucERPContinueEmployDetail.SendSQLCmd = true;
            this.ucERPContinueEmployDetail.ServerModify = true;
            this.ucERPContinueEmployDetail.ServerModifyGetMax = false;
            this.ucERPContinueEmployDetail.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPContinueEmployDetail.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPContinueEmployDetail.UseTranscationScope = false;
            this.ucERPContinueEmployDetail.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idERPContinueEmployMaster_ERPContinueEmployDetail
            // 
            this.idERPContinueEmployMaster_ERPContinueEmployDetail.Detail = this.ERPContinueEmployDetail;
            columnItem1.FieldName = "ContinueEmployNO";
            this.idERPContinueEmployMaster_ERPContinueEmployDetail.DetailColumns.Add(columnItem1);
            this.idERPContinueEmployMaster_ERPContinueEmployDetail.DynamicTableName = false;
            this.idERPContinueEmployMaster_ERPContinueEmployDetail.Master = this.ERPContinueEmployMaster;
            columnItem2.FieldName = "ContinueEmployNO";
            this.idERPContinueEmployMaster_ERPContinueEmployDetail.MasterColumns.Add(columnItem2);
            // 
            // View_ERPContinueEmployMaster
            // 
            this.View_ERPContinueEmployMaster.CacheConnection = false;
            this.View_ERPContinueEmployMaster.CommandText = "SELECT * FROM dbo.[ERPContinueEmployMaster]";
            this.View_ERPContinueEmployMaster.CommandTimeout = 30;
            this.View_ERPContinueEmployMaster.CommandType = System.Data.CommandType.Text;
            this.View_ERPContinueEmployMaster.DynamicTableName = false;
            this.View_ERPContinueEmployMaster.EEPAlias = null;
            this.View_ERPContinueEmployMaster.EncodingAfter = null;
            this.View_ERPContinueEmployMaster.EncodingBefore = "Windows-1252";
            this.View_ERPContinueEmployMaster.EncodingConvert = null;
            this.View_ERPContinueEmployMaster.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ContinueEmployNO";
            this.View_ERPContinueEmployMaster.KeyFields.Add(keyItem4);
            this.View_ERPContinueEmployMaster.MultiSetWhere = false;
            this.View_ERPContinueEmployMaster.Name = "View_ERPContinueEmployMaster";
            this.View_ERPContinueEmployMaster.NotificationAutoEnlist = false;
            this.View_ERPContinueEmployMaster.SecExcept = null;
            this.View_ERPContinueEmployMaster.SecFieldName = null;
            this.View_ERPContinueEmployMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPContinueEmployMaster.SelectPaging = false;
            this.View_ERPContinueEmployMaster.SelectTop = 0;
            this.View_ERPContinueEmployMaster.SiteControl = false;
            this.View_ERPContinueEmployMaster.SiteFieldName = null;
            this.View_ERPContinueEmployMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "AutoNO1";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "GetContinueEmployNOPrefix()";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 4;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "ContinueEmployNO";
            this.autoNumber1.UpdateComp = this.ucERPContinueEmployMaster;
            // 
            // cus
            // 
            this.cus.CacheConnection = false;
            this.cus.CommandText = "SELECT   [cus_no],[cus_name],[title]  FROM  [cus]";
            this.cus.CommandTimeout = 30;
            this.cus.CommandType = System.Data.CommandType.Text;
            this.cus.DynamicTableName = false;
            this.cus.EEPAlias = "lab";
            this.cus.EncodingAfter = null;
            this.cus.EncodingBefore = "Windows-1252";
            this.cus.EncodingConvert = null;
            this.cus.InfoConnection = this.infoConnection2;
            keyItem5.KeyName = "cus_no";
            this.cus.KeyFields.Add(keyItem5);
            this.cus.MultiSetWhere = false;
            this.cus.Name = "cus";
            this.cus.NotificationAutoEnlist = false;
            this.cus.SecExcept = null;
            this.cus.SecFieldName = null;
            this.cus.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.cus.SelectPaging = false;
            this.cus.SelectTop = 0;
            this.cus.SiteControl = false;
            this.cus.SiteFieldName = null;
            this.cus.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "lab";
            // 
            // lab
            // 
            this.lab.CacheConnection = false;
            this.lab.CommandText = "SELECT [lab_no],[lab_cname],[lab_name],[cus_no],sex,n.nat_name,lab_idate,lab_edat" +
    "e FROM [lab] l left join [nat] n on n.nat_no=l.nat_no";
            this.lab.CommandTimeout = 30;
            this.lab.CommandType = System.Data.CommandType.Text;
            this.lab.DynamicTableName = false;
            this.lab.EEPAlias = "lab";
            this.lab.EncodingAfter = null;
            this.lab.EncodingBefore = "Windows-1252";
            this.lab.EncodingConvert = null;
            this.lab.InfoConnection = this.infoConnection2;
            keyItem6.KeyName = "lab_no";
            this.lab.KeyFields.Add(keyItem6);
            this.lab.MultiSetWhere = false;
            this.lab.Name = "lab";
            this.lab.NotificationAutoEnlist = false;
            this.lab.SecExcept = null;
            this.lab.SecFieldName = null;
            this.lab.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.lab.SelectPaging = false;
            this.lab.SelectTop = 0;
            this.lab.SiteControl = false;
            this.lab.SiteFieldName = null;
            this.lab.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // View_ERPContinueEmploy
            // 
            this.View_ERPContinueEmploy.CacheConnection = false;
            this.View_ERPContinueEmploy.CommandText = resources.GetString("View_ERPContinueEmploy.CommandText");
            this.View_ERPContinueEmploy.CommandTimeout = 30;
            this.View_ERPContinueEmploy.CommandType = System.Data.CommandType.Text;
            this.View_ERPContinueEmploy.DynamicTableName = false;
            this.View_ERPContinueEmploy.EEPAlias = null;
            this.View_ERPContinueEmploy.EncodingAfter = null;
            this.View_ERPContinueEmploy.EncodingBefore = "Windows-1252";
            this.View_ERPContinueEmploy.EncodingConvert = null;
            this.View_ERPContinueEmploy.InfoConnection = this.InfoConnection1;
            this.View_ERPContinueEmploy.MultiSetWhere = false;
            this.View_ERPContinueEmploy.Name = "View_ERPContinueEmploy";
            this.View_ERPContinueEmploy.NotificationAutoEnlist = false;
            this.View_ERPContinueEmploy.SecExcept = null;
            this.View_ERPContinueEmploy.SecFieldName = null;
            this.View_ERPContinueEmploy.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPContinueEmploy.SelectPaging = false;
            this.View_ERPContinueEmploy.SelectTop = 0;
            this.View_ERPContinueEmploy.SiteControl = false;
            this.View_ERPContinueEmploy.SiteFieldName = null;
            this.View_ERPContinueEmploy.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContinueEmployMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContinueEmployDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContinueEmployMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContinueEmploy)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPContinueEmployMaster;
        private Srvtools.UpdateComponent ucERPContinueEmployMaster;
        private Srvtools.InfoCommand ERPContinueEmployDetail;
        private Srvtools.UpdateComponent ucERPContinueEmployDetail;
        private Srvtools.InfoDataSource idERPContinueEmployMaster_ERPContinueEmployDetail;
        private Srvtools.InfoCommand View_ERPContinueEmployMaster;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand cus;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand lab;
        private Srvtools.InfoCommand View_ERPContinueEmploy;
    }
}
