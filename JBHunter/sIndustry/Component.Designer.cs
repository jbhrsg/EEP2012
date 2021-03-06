namespace sIndustry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
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
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_IndCategory = new Srvtools.InfoCommand(this.components);
            this.ucHUT_IndCategory = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_IndCategory)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_IndCategory
            // 
            this.HUT_IndCategory.CacheConnection = false;
            this.HUT_IndCategory.CommandText = resources.GetString("HUT_IndCategory.CommandText");
            this.HUT_IndCategory.CommandTimeout = 30;
            this.HUT_IndCategory.CommandType = System.Data.CommandType.Text;
            this.HUT_IndCategory.DynamicTableName = false;
            this.HUT_IndCategory.EEPAlias = null;
            this.HUT_IndCategory.EncodingAfter = null;
            this.HUT_IndCategory.EncodingBefore = "Windows-1252";
            this.HUT_IndCategory.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ID";
            keyItem2.KeyName = "ID";
            this.HUT_IndCategory.KeyFields.Add(keyItem1);
            this.HUT_IndCategory.KeyFields.Add(keyItem2);
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
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr7);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr8);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr9);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr10);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr11);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr12);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr13);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr14);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr15);
            this.ucHUT_IndCategory.FieldAttrs.Add(fieldAttr16);
            this.ucHUT_IndCategory.LogInfo = null;
            this.ucHUT_IndCategory.Name = null;
            this.ucHUT_IndCategory.RowAffectsCheck = true;
            this.ucHUT_IndCategory.SelectCmd = this.HUT_IndCategory;
            this.ucHUT_IndCategory.SelectCmdForUpdate = null;
            this.ucHUT_IndCategory.ServerModify = true;
            this.ucHUT_IndCategory.ServerModifyGetMax = false;
            this.ucHUT_IndCategory.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_IndCategory.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_IndCategory.UseTranscationScope = false;
            this.ucHUT_IndCategory.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_IndCategory)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_IndCategory;
        private Srvtools.UpdateComponent ucHUT_IndCategory;
    }
}
