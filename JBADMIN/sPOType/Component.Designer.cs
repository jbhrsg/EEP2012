namespace sPO_POType
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.POType = new Srvtools.InfoCommand(this.components);
            this.ucPOType = new Srvtools.UpdateComponent(this.components);
            this.View_POType = new Srvtools.InfoCommand(this.components);
            this.GROUP = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.POType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_POType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUP)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // POType
            // 
            this.POType.CacheConnection = false;
            this.POType.CommandText = "SELECT dbo.[POType].* FROM dbo.[POType]";
            this.POType.CommandTimeout = 30;
            this.POType.CommandType = System.Data.CommandType.Text;
            this.POType.DynamicTableName = false;
            this.POType.EEPAlias = null;
            this.POType.EncodingAfter = null;
            this.POType.EncodingBefore = "Windows-1252";
            this.POType.EncodingConvert = null;
            this.POType.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            keyItem2.KeyName = "POTypeID";
            this.POType.KeyFields.Add(keyItem1);
            this.POType.KeyFields.Add(keyItem2);
            this.POType.MultiSetWhere = false;
            this.POType.Name = "POType";
            this.POType.NotificationAutoEnlist = false;
            this.POType.SecExcept = null;
            this.POType.SecFieldName = null;
            this.POType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.POType.SelectPaging = false;
            this.POType.SelectTop = 0;
            this.POType.SiteControl = false;
            this.POType.SiteFieldName = null;
            this.POType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPOType
            // 
            this.ucPOType.AutoTrans = true;
            this.ucPOType.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "POTypeID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "POTypeName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "GROUPID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = "_usercode";
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
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr7.DefaultValue = "_username";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucPOType.FieldAttrs.Add(fieldAttr1);
            this.ucPOType.FieldAttrs.Add(fieldAttr2);
            this.ucPOType.FieldAttrs.Add(fieldAttr3);
            this.ucPOType.FieldAttrs.Add(fieldAttr4);
            this.ucPOType.FieldAttrs.Add(fieldAttr5);
            this.ucPOType.FieldAttrs.Add(fieldAttr6);
            this.ucPOType.FieldAttrs.Add(fieldAttr7);
            this.ucPOType.FieldAttrs.Add(fieldAttr8);
            this.ucPOType.LogInfo = null;
            this.ucPOType.Name = "ucPOType";
            this.ucPOType.RowAffectsCheck = true;
            this.ucPOType.SelectCmd = this.POType;
            this.ucPOType.SelectCmdForUpdate = null;
            this.ucPOType.SendSQLCmd = true;
            this.ucPOType.ServerModify = true;
            this.ucPOType.ServerModifyGetMax = false;
            this.ucPOType.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPOType.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPOType.UseTranscationScope = false;
            this.ucPOType.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucPOType.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucPOType_BeforeInsert);
            this.ucPOType.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucPOType_BeforeModify);
            // 
            // View_POType
            // 
            this.View_POType.CacheConnection = false;
            this.View_POType.CommandText = "SELECT * FROM dbo.[POType]";
            this.View_POType.CommandTimeout = 30;
            this.View_POType.CommandType = System.Data.CommandType.Text;
            this.View_POType.DynamicTableName = false;
            this.View_POType.EEPAlias = null;
            this.View_POType.EncodingAfter = null;
            this.View_POType.EncodingBefore = "Windows-1252";
            this.View_POType.EncodingConvert = null;
            this.View_POType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.View_POType.KeyFields.Add(keyItem3);
            this.View_POType.MultiSetWhere = false;
            this.View_POType.Name = "View_POType";
            this.View_POType.NotificationAutoEnlist = false;
            this.View_POType.SecExcept = null;
            this.View_POType.SecFieldName = null;
            this.View_POType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_POType.SelectPaging = false;
            this.View_POType.SelectTop = 0;
            this.View_POType.SiteControl = false;
            this.View_POType.SiteFieldName = null;
            this.View_POType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // GROUP
            // 
            this.GROUP.CacheConnection = false;
            this.GROUP.CommandText = "SELECT GROUPID,GROUPNAME FROM EIPHRSYS.DBO.GROUPS WHERE ISROLE=\'Y\' ORDER BY GROUP" +
    "ID";
            this.GROUP.CommandTimeout = 30;
            this.GROUP.CommandType = System.Data.CommandType.Text;
            this.GROUP.DynamicTableName = false;
            this.GROUP.EEPAlias = null;
            this.GROUP.EncodingAfter = null;
            this.GROUP.EncodingBefore = "Windows-1252";
            this.GROUP.EncodingConvert = null;
            this.GROUP.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "GROUPID";
            this.GROUP.KeyFields.Add(keyItem4);
            this.GROUP.MultiSetWhere = false;
            this.GROUP.Name = "GROUP";
            this.GROUP.NotificationAutoEnlist = false;
            this.GROUP.SecExcept = null;
            this.GROUP.SecFieldName = null;
            this.GROUP.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.GROUP.SelectPaging = false;
            this.GROUP.SelectTop = 0;
            this.GROUP.SiteControl = false;
            this.GROUP.SiteFieldName = null;
            this.GROUP.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.POType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_POType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GROUP)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand POType;
        private Srvtools.UpdateComponent ucPOType;
        private Srvtools.InfoCommand View_POType;
        private Srvtools.InfoCommand GROUP;
    }
}
