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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr27 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr28 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr29 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr30 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr31 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr32 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr33 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr34 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr35 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr36 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr37 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr38 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr39 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr40 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr41 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr42 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr43 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr44 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr45 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr46 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr47 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr48 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr49 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr50 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr51 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr52 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.MENUTABLE = new Srvtools.InfoCommand(this.components);
            this.ucMENUTABLE = new Srvtools.UpdateComponent(this.components);
            this.View_MENUTABLE = new Srvtools.InfoCommand(this.components);
            this.GROUPMENUS = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MENUTABLE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_MENUTABLE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPMENUS)).BeginInit();
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
            keyItem3.KeyName = "MENUID";
            this.MENUTABLE.KeyFields.Add(keyItem3);
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
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "MENUID";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "CAPTION";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "PARENT";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "PACKAGE";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "MODULETYPE";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "ITEMPARAM";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "FORM";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            fieldAttr34.CheckNull = false;
            fieldAttr34.DataField = "ISSHOWMODAL";
            fieldAttr34.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr34.DefaultValue = null;
            fieldAttr34.TrimLength = 0;
            fieldAttr34.UpdateEnable = true;
            fieldAttr34.WhereMode = true;
            fieldAttr35.CheckNull = false;
            fieldAttr35.DataField = "ITEMTYPE";
            fieldAttr35.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr35.DefaultValue = null;
            fieldAttr35.TrimLength = 0;
            fieldAttr35.UpdateEnable = true;
            fieldAttr35.WhereMode = true;
            fieldAttr36.CheckNull = false;
            fieldAttr36.DataField = "SEQ_NO";
            fieldAttr36.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr36.DefaultValue = null;
            fieldAttr36.TrimLength = 0;
            fieldAttr36.UpdateEnable = true;
            fieldAttr36.WhereMode = true;
            fieldAttr37.CheckNull = false;
            fieldAttr37.DataField = "PACKAGEDATE";
            fieldAttr37.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr37.DefaultValue = null;
            fieldAttr37.TrimLength = 0;
            fieldAttr37.UpdateEnable = true;
            fieldAttr37.WhereMode = true;
            fieldAttr38.CheckNull = false;
            fieldAttr38.DataField = "IMAGE";
            fieldAttr38.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr38.DefaultValue = null;
            fieldAttr38.TrimLength = 0;
            fieldAttr38.UpdateEnable = true;
            fieldAttr38.WhereMode = true;
            fieldAttr39.CheckNull = false;
            fieldAttr39.DataField = "OWNER";
            fieldAttr39.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr39.DefaultValue = null;
            fieldAttr39.TrimLength = 0;
            fieldAttr39.UpdateEnable = true;
            fieldAttr39.WhereMode = true;
            fieldAttr40.CheckNull = false;
            fieldAttr40.DataField = "ISSERVER";
            fieldAttr40.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr40.DefaultValue = null;
            fieldAttr40.TrimLength = 0;
            fieldAttr40.UpdateEnable = true;
            fieldAttr40.WhereMode = true;
            fieldAttr41.CheckNull = false;
            fieldAttr41.DataField = "VERSIONNO";
            fieldAttr41.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr41.DefaultValue = null;
            fieldAttr41.TrimLength = 0;
            fieldAttr41.UpdateEnable = true;
            fieldAttr41.WhereMode = true;
            fieldAttr42.CheckNull = false;
            fieldAttr42.DataField = "CHECKOUT";
            fieldAttr42.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr42.DefaultValue = null;
            fieldAttr42.TrimLength = 0;
            fieldAttr42.UpdateEnable = true;
            fieldAttr42.WhereMode = true;
            fieldAttr43.CheckNull = false;
            fieldAttr43.DataField = "CHECKOUTDATE";
            fieldAttr43.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr43.DefaultValue = null;
            fieldAttr43.TrimLength = 0;
            fieldAttr43.UpdateEnable = true;
            fieldAttr43.WhereMode = true;
            fieldAttr44.CheckNull = false;
            fieldAttr44.DataField = "CAPTION0";
            fieldAttr44.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr44.DefaultValue = null;
            fieldAttr44.TrimLength = 0;
            fieldAttr44.UpdateEnable = true;
            fieldAttr44.WhereMode = true;
            fieldAttr45.CheckNull = false;
            fieldAttr45.DataField = "CAPTION1";
            fieldAttr45.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr45.DefaultValue = null;
            fieldAttr45.TrimLength = 0;
            fieldAttr45.UpdateEnable = true;
            fieldAttr45.WhereMode = true;
            fieldAttr46.CheckNull = false;
            fieldAttr46.DataField = "CAPTION2";
            fieldAttr46.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr46.DefaultValue = null;
            fieldAttr46.TrimLength = 0;
            fieldAttr46.UpdateEnable = true;
            fieldAttr46.WhereMode = true;
            fieldAttr47.CheckNull = false;
            fieldAttr47.DataField = "CAPTION3";
            fieldAttr47.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr47.DefaultValue = null;
            fieldAttr47.TrimLength = 0;
            fieldAttr47.UpdateEnable = true;
            fieldAttr47.WhereMode = true;
            fieldAttr48.CheckNull = false;
            fieldAttr48.DataField = "CAPTION4";
            fieldAttr48.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr48.DefaultValue = null;
            fieldAttr48.TrimLength = 0;
            fieldAttr48.UpdateEnable = true;
            fieldAttr48.WhereMode = true;
            fieldAttr49.CheckNull = false;
            fieldAttr49.DataField = "CAPTION5";
            fieldAttr49.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr49.DefaultValue = null;
            fieldAttr49.TrimLength = 0;
            fieldAttr49.UpdateEnable = true;
            fieldAttr49.WhereMode = true;
            fieldAttr50.CheckNull = false;
            fieldAttr50.DataField = "CAPTION6";
            fieldAttr50.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr50.DefaultValue = null;
            fieldAttr50.TrimLength = 0;
            fieldAttr50.UpdateEnable = true;
            fieldAttr50.WhereMode = true;
            fieldAttr51.CheckNull = false;
            fieldAttr51.DataField = "CAPTION7";
            fieldAttr51.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr51.DefaultValue = null;
            fieldAttr51.TrimLength = 0;
            fieldAttr51.UpdateEnable = true;
            fieldAttr51.WhereMode = true;
            fieldAttr52.CheckNull = false;
            fieldAttr52.DataField = "IMAGEURL";
            fieldAttr52.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr52.DefaultValue = null;
            fieldAttr52.TrimLength = 0;
            fieldAttr52.UpdateEnable = true;
            fieldAttr52.WhereMode = true;
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr27);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr28);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr29);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr30);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr31);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr32);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr33);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr34);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr35);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr36);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr37);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr38);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr39);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr40);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr41);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr42);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr43);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr44);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr45);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr46);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr47);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr48);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr49);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr50);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr51);
            this.ucMENUTABLE.FieldAttrs.Add(fieldAttr52);
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
            keyItem1.KeyName = "MENUID";
            this.View_MENUTABLE.KeyFields.Add(keyItem1);
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MENUTABLE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_MENUTABLE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUPMENUS)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand MENUTABLE;
        private Srvtools.UpdateComponent ucMENUTABLE;
        private Srvtools.InfoCommand View_MENUTABLE;
        private Srvtools.InfoCommand GROUPMENUS;
    }
}
