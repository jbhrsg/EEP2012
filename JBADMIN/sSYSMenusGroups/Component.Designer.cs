namespace sSYSMenusGroups
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
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr26 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.MENUTABLE = new Srvtools.InfoCommand(this.components);
            this.ucMENUTABLE = new Srvtools.UpdateComponent(this.components);
            this.View_MENUTABLE = new Srvtools.InfoCommand(this.components);
            this.GROUPMENUS = new Srvtools.InfoCommand(this.components);
            this.GROUPS = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MENUTABLE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_MENUTABLE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPMENUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPS)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "EIPHRSYS";
            // 
            // MENUTABLE
            // 
            this.MENUTABLE.CacheConnection = false;
            this.MENUTABLE.CommandText = "SELECT *  FROM View_SYSMenuGroups ORDER BY MENUNAME";
            this.MENUTABLE.CommandTimeout = 30;
            this.MENUTABLE.CommandType = System.Data.CommandType.Text;
            this.MENUTABLE.DynamicTableName = false;
            this.MENUTABLE.EEPAlias = "EIPHRSYS";
            this.MENUTABLE.EncodingAfter = null;
            this.MENUTABLE.EncodingBefore = "Windows-1252";
            this.MENUTABLE.EncodingConvert = null;
            this.MENUTABLE.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "MENUID";
            this.MENUTABLE.KeyFields.Add(keyItem1);
            this.MENUTABLE.MultiSetWhere = false;
            this.MENUTABLE.Name = "MENUTABLE";
            this.MENUTABLE.NotificationAutoEnlist = false;
            this.MENUTABLE.SecExcept = null;
            this.MENUTABLE.SecFieldName = null;
            this.MENUTABLE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.MENUTABLE.SelectPaging = false;
            this.MENUTABLE.SelectTop = 0;
            this.MENUTABLE.SiteControl = false;
            this.MENUTABLE.SiteFieldName = null;
            this.MENUTABLE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucMENUTABLE
            // 
            this.ucMENUTABLE.AutoTrans = true;
            this.ucMENUTABLE.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "MENUID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CAPTION";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "PARENT";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "PACKAGE";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "MODULETYPE";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ITEMPARAM";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "FORM";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ISSHOWMODAL";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ITEMTYPE";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "SEQ_NO";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "PACKAGEDATE";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "IMAGE";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "OWNER";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "ISSERVER";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "VERSIONNO";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CHECKOUT";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CHECKOUTDATE";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "CAPTION0";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "CAPTION1";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CAPTION2";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CAPTION3";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "CAPTION4";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "CAPTION5";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "CAPTION6";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "CAPTION7";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "IMAGEURL";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr1);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr2);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr3);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr4);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr5);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr6);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr7);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr8);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr9);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr10);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr11);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr12);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr13);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr14);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr15);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr16);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr17);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr18);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr19);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr20);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr21);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr22);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr23);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr24);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr25);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr26);
            this.ucMENUTABLE.LogInfo = null;
            this.ucMENUTABLE.Name = "ucMENUTABLE";
            this.ucMENUTABLE.RowAffectsCheck = true;
            this.ucMENUTABLE.SelectCmd = this.MENUTABLE;
            this.ucMENUTABLE.SelectCmdForUpdate = null;
            this.ucMENUTABLE.SendSQLCmd = true;
            this.ucMENUTABLE.ServerModify = true;
            this.ucMENUTABLE.ServerModifyGetMax = false;
            this.ucMENUTABLE.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucMENUTABLE.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucMENUTABLE.UseTranscationScope = false;
            this.ucMENUTABLE.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_MENUTABLE
            // 
            this.View_MENUTABLE.CacheConnection = false;
            this.View_MENUTABLE.CommandText = "SELECT * FROM dbo.[MENUTABLE]";
            this.View_MENUTABLE.CommandTimeout = 30;
            this.View_MENUTABLE.CommandType = System.Data.CommandType.Text;
            this.View_MENUTABLE.DynamicTableName = false;
            this.View_MENUTABLE.EEPAlias = null;
            this.View_MENUTABLE.EncodingAfter = null;
            this.View_MENUTABLE.EncodingBefore = "Windows-1252";
            this.View_MENUTABLE.EncodingConvert = null;
            this.View_MENUTABLE.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "MENUID";
            this.View_MENUTABLE.KeyFields.Add(keyItem2);
            this.View_MENUTABLE.MultiSetWhere = false;
            this.View_MENUTABLE.Name = "View_MENUTABLE";
            this.View_MENUTABLE.NotificationAutoEnlist = false;
            this.View_MENUTABLE.SecExcept = null;
            this.View_MENUTABLE.SecFieldName = null;
            this.View_MENUTABLE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_MENUTABLE.SelectPaging = false;
            this.View_MENUTABLE.SelectTop = 0;
            this.View_MENUTABLE.SiteControl = false;
            this.View_MENUTABLE.SiteFieldName = null;
            this.View_MENUTABLE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // GROUPMENUS
            // 
            this.GROUPMENUS.CacheConnection = false;
            this.GROUPMENUS.CommandText = resources.GetString("GROUPMENUS.CommandText");
            this.GROUPMENUS.CommandTimeout = 30;
            this.GROUPMENUS.CommandType = System.Data.CommandType.Text;
            this.GROUPMENUS.DynamicTableName = false;
            this.GROUPMENUS.EEPAlias = "EIPHRSYS";
            this.GROUPMENUS.EncodingAfter = null;
            this.GROUPMENUS.EncodingBefore = "Windows-1252";
            this.GROUPMENUS.EncodingConvert = null;
            this.GROUPMENUS.InfoConnection = this.InfoConnection1;
            this.GROUPMENUS.MultiSetWhere = false;
            this.GROUPMENUS.Name = "GROUPMENUS";
            this.GROUPMENUS.NotificationAutoEnlist = false;
            this.GROUPMENUS.SecExcept = null;
            this.GROUPMENUS.SecFieldName = null;
            this.GROUPMENUS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.GROUPMENUS.SelectPaging = false;
            this.GROUPMENUS.SelectTop = 0;
            this.GROUPMENUS.SiteControl = false;
            this.GROUPMENUS.SiteFieldName = null;
            this.GROUPMENUS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // GROUPS
            // 
            this.GROUPS.CacheConnection = false;
            this.GROUPS.CommandText = resources.GetString("GROUPS.CommandText");
            this.GROUPS.CommandTimeout = 30;
            this.GROUPS.CommandType = System.Data.CommandType.Text;
            this.GROUPS.DynamicTableName = false;
            this.GROUPS.EEPAlias = "EIPHRSYS";
            this.GROUPS.EncodingAfter = null;
            this.GROUPS.EncodingBefore = "Windows-1252";
            this.GROUPS.EncodingConvert = null;
            this.GROUPS.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "MENUID";
            this.GROUPS.KeyFields.Add(keyItem3);
            this.GROUPS.MultiSetWhere = false;
            this.GROUPS.Name = "GROUPS";
            this.GROUPS.NotificationAutoEnlist = false;
            this.GROUPS.SecExcept = null;
            this.GROUPS.SecFieldName = null;
            this.GROUPS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.GROUPS.SelectPaging = false;
            this.GROUPS.SelectTop = 0;
            this.GROUPS.SiteControl = false;
            this.GROUPS.SiteFieldName = null;
            this.GROUPS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MENUTABLE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_MENUTABLE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPMENUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPS)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand MENUTABLE;
        private Srvtools.UpdateComponent ucMENUTABLE;
        private Srvtools.InfoCommand View_MENUTABLE;
        private Srvtools.InfoCommand GROUPMENUS;
        private Srvtools.InfoCommand GROUPS;
    }
}
