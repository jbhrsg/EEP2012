namespace sIndCategory
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr9 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_IndCategory = new Srvtools.InfoCommand(this.components);
            this.ucHUT_IndCategory = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_IndCategory = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.HUT_IndCategorys = new Srvtools.InfoCommand(this.components);
            this.ucHUT_IndCategorys = new Srvtools.UpdateComponent(this.components);
            this.autoNumber2 = new Srvtools.AutoNumber(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_IndCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_IndCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_IndCategorys)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelItem";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelItem";
            service2.DelegateName = "CheckMasterDelete";
            service2.NonLogin = false;
            service2.ServiceName = "CheckMasterDelete";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_IndCategory
            // 
            this.HUT_IndCategory.CacheConnection = false;
            this.HUT_IndCategory.CommandText = "SELECT dbo.[HUT_IndCategory].* FROM dbo.[HUT_IndCategory]\r\nWHERE dbo.[HUT_IndCate" +
    "gory].NodeLevel=1\r\nORDER BY   dbo.[HUT_IndCategory].ID";
            this.HUT_IndCategory.CommandTimeout = 30;
            this.HUT_IndCategory.CommandType = System.Data.CommandType.Text;
            this.HUT_IndCategory.DynamicTableName = false;
            this.HUT_IndCategory.EEPAlias = null;
            this.HUT_IndCategory.EncodingAfter = null;
            this.HUT_IndCategory.EncodingBefore = "Windows-1252";
            this.HUT_IndCategory.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ID";
            this.HUT_IndCategory.KeyFields.Add(keyItem1);
            this.HUT_IndCategory.MultiSetWhere = false;
            this.HUT_IndCategory.Name = "HUT_IndCategory";
            this.HUT_IndCategory.NotificationAutoEnlist = false;
            this.HUT_IndCategory.SecExcept = null;
            this.HUT_IndCategory.SecFieldName = null;
            this.HUT_IndCategory.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_IndCategory.SelectPaging = false;
            this.HUT_IndCategory.SelectTop = 0;
            this.HUT_IndCategory.SiteControl = false;
            this.HUT_IndCategory.SiteFieldName = null;
            this.HUT_IndCategory.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_IndCategory
            // 
            this.ucHUT_IndCategory.AutoTrans = true;
            this.ucHUT_IndCategory.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "IndCategory";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ParentID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "NodeLevel";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateBy";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr7);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr8);
            this.ucHUT_IndCategory.LogInfo = null;
            this.ucHUT_IndCategory.Name = "ucHUT_IndCategory";
            this.ucHUT_IndCategory.RowAffectsCheck = true;
            this.ucHUT_IndCategory.SelectCmd = this.HUT_IndCategory;
            this.ucHUT_IndCategory.SelectCmdForUpdate = null;
            this.ucHUT_IndCategory.ServerModify = true;
            this.ucHUT_IndCategory.ServerModifyGetMax = true;
            this.ucHUT_IndCategory.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_IndCategory.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_IndCategory.UseTranscationScope = false;
            this.ucHUT_IndCategory.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_IndCategory
            // 
            this.View_HUT_IndCategory.CacheConnection = false;
            this.View_HUT_IndCategory.CommandText = "SELECT * FROM dbo.[HUT_IndCategory]\r\nWHERE dbo.[HUT_IndCategory].NodeLevel=1\r\nORD" +
    "ER BY  INDCATEGORY";
            this.View_HUT_IndCategory.CommandTimeout = 30;
            this.View_HUT_IndCategory.CommandType = System.Data.CommandType.Text;
            this.View_HUT_IndCategory.DynamicTableName = false;
            this.View_HUT_IndCategory.EEPAlias = null;
            this.View_HUT_IndCategory.EncodingAfter = null;
            this.View_HUT_IndCategory.EncodingBefore = "Windows-1252";
            this.View_HUT_IndCategory.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ID";
            this.View_HUT_IndCategory.KeyFields.Add(keyItem2);
            this.View_HUT_IndCategory.MultiSetWhere = false;
            this.View_HUT_IndCategory.Name = "View_HUT_IndCategory";
            this.View_HUT_IndCategory.NotificationAutoEnlist = false;
            this.View_HUT_IndCategory.SecExcept = null;
            this.View_HUT_IndCategory.SecFieldName = null;
            this.View_HUT_IndCategory.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_IndCategory.SelectPaging = false;
            this.View_HUT_IndCategory.SelectTop = 0;
            this.View_HUT_IndCategory.SiteControl = false;
            this.View_HUT_IndCategory.SiteFieldName = null;
            this.View_HUT_IndCategory.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "ID";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 3;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "ID";
            this.autoNumber1.UpdateComp = this.ucHUT_IndCategory;
            // 
            // HUT_IndCategorys
            // 
            this.HUT_IndCategorys.CacheConnection = false;
            this.HUT_IndCategorys.CommandText = "select HUT_IndCategory.* \r\nfrom HUT_IndCategory \r\nwhere NodeLevel=2\r\norder by ID " +
    " ";
            this.HUT_IndCategorys.CommandTimeout = 30;
            this.HUT_IndCategorys.CommandType = System.Data.CommandType.Text;
            this.HUT_IndCategorys.DynamicTableName = false;
            this.HUT_IndCategorys.EEPAlias = null;
            this.HUT_IndCategorys.EncodingAfter = null;
            this.HUT_IndCategorys.EncodingBefore = "Windows-1252";
            this.HUT_IndCategorys.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ID";
            this.HUT_IndCategorys.KeyFields.Add(keyItem3);
            this.HUT_IndCategorys.MultiSetWhere = false;
            this.HUT_IndCategorys.Name = "HUT_IndCategorys";
            this.HUT_IndCategorys.NotificationAutoEnlist = false;
            this.HUT_IndCategorys.SecExcept = null;
            this.HUT_IndCategorys.SecFieldName = null;
            this.HUT_IndCategorys.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_IndCategorys.SelectPaging = false;
            this.HUT_IndCategorys.SelectTop = 0;
            this.HUT_IndCategorys.SiteControl = false;
            this.HUT_IndCategorys.SiteFieldName = null;
            this.HUT_IndCategorys.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_IndCategorys
            // 
            this.ucHUT_IndCategorys.AutoTrans = true;
            this.ucHUT_IndCategorys.ExceptJoin = false;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ID";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "IndCategory";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "ParentID";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "NodeLevel";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LastUpdateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            this.ucHUT_IndCategorys.FieldAttrs.Add(fieldAttr9);
            this.ucHUT_IndCategorys.FieldAttrs.Add(fieldAttr10);
            this.ucHUT_IndCategorys.FieldAttrs.Add(fieldAttr11);
            this.ucHUT_IndCategorys.FieldAttrs.Add(fieldAttr12);
            this.ucHUT_IndCategorys.FieldAttrs.Add(fieldAttr13);
            this.ucHUT_IndCategorys.FieldAttrs.Add(fieldAttr14);
            this.ucHUT_IndCategorys.FieldAttrs.Add(fieldAttr15);
            this.ucHUT_IndCategorys.FieldAttrs.Add(fieldAttr16);
            this.ucHUT_IndCategorys.LogInfo = null;
            this.ucHUT_IndCategorys.Name = "ucHUT_IndCategorys";
            this.ucHUT_IndCategorys.RowAffectsCheck = true;
            this.ucHUT_IndCategorys.SelectCmd = this.HUT_IndCategorys;
            this.ucHUT_IndCategorys.SelectCmdForUpdate = this.HUT_IndCategorys;
            this.ucHUT_IndCategorys.ServerModify = true;
            this.ucHUT_IndCategorys.ServerModifyGetMax = false;
            this.ucHUT_IndCategorys.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_IndCategorys.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_IndCategorys.UseTranscationScope = false;
            this.ucHUT_IndCategorys.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // autoNumber2
            // 
            this.autoNumber2.Active = true;
            this.autoNumber2.AutoNoID = "ID";
            this.autoNumber2.Description = null;
            this.autoNumber2.GetFixed = "";
            this.autoNumber2.isNumFill = false;
            this.autoNumber2.Name = "autoNumber2";
            this.autoNumber2.Number = null;
            this.autoNumber2.NumDig = 3;
            this.autoNumber2.OldVersion = false;
            this.autoNumber2.OverFlow = true;
            this.autoNumber2.StartValue = 1;
            this.autoNumber2.Step = 1;
            this.autoNumber2.TargetColumn = "ID";
            this.autoNumber2.UpdateComp = this.ucHUT_IndCategorys;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_IndCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_IndCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_IndCategorys)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_IndCategory;
        private Srvtools.UpdateComponent ucHUT_IndCategory;
        private Srvtools.InfoCommand View_HUT_IndCategory;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand HUT_IndCategorys;
        private Srvtools.UpdateComponent ucHUT_IndCategorys;
        private Srvtools.AutoNumber autoNumber2;
    }
}
