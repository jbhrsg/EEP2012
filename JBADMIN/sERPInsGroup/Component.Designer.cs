namespace sERPInsGroup
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.InsGroup = new Srvtools.InfoCommand(this.components);
            this.ucInsGroup = new Srvtools.UpdateComponent(this.components);
            this.View_InsGroup = new Srvtools.InfoCommand(this.components);
            this.GroupRoles = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_InsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupRoles)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // InsGroup
            // 
            this.InsGroup.CacheConnection = false;
            this.InsGroup.CommandText = "SELECT dbo.[InsGroup].* FROM dbo.[InsGroup]";
            this.InsGroup.CommandTimeout = 30;
            this.InsGroup.CommandType = System.Data.CommandType.Text;
            this.InsGroup.DynamicTableName = false;
            this.InsGroup.EEPAlias = null;
            this.InsGroup.EncodingAfter = null;
            this.InsGroup.EncodingBefore = "Windows-1252";
            this.InsGroup.EncodingConvert = null;
            this.InsGroup.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "InsGroupID";
            this.InsGroup.KeyFields.Add(keyItem1);
            this.InsGroup.MultiSetWhere = false;
            this.InsGroup.Name = "InsGroup";
            this.InsGroup.NotificationAutoEnlist = false;
            this.InsGroup.SecExcept = null;
            this.InsGroup.SecFieldName = null;
            this.InsGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.InsGroup.SelectPaging = false;
            this.InsGroup.SelectTop = 0;
            this.InsGroup.SiteControl = false;
            this.InsGroup.SiteFieldName = null;
            this.InsGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucInsGroup
            // 
            this.ucInsGroup.AutoTrans = true;
            this.ucInsGroup.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "InsGroupID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "InsGroupName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "InsLaborRate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "InsBusinessTax";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "LaborInsNo";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LaborInsNoChk";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "HealthInsNo";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "HealthInsSubID";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "TaxNo";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "LastUpdateBy";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "InsAccount";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "InsNo";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "Person";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "Address";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "Tel";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "AssetControl";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "IsAssetControl";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "IsActive";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "ShortName";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "APIWebCode";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "APIPassword";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            this.ucInsGroup.FieldAttrs.Add(fieldAttr1);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr2);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr3);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr4);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr5);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr6);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr7);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr8);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr9);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr10);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr11);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr12);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr13);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr14);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr15);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr16);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr17);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr18);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr19);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr20);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr21);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr22);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr23);
            this.ucInsGroup.FieldAttrs.Add(fieldAttr24);
            this.ucInsGroup.LogInfo = null;
            this.ucInsGroup.Name = "ucInsGroup";
            this.ucInsGroup.RowAffectsCheck = true;
            this.ucInsGroup.SelectCmd = this.InsGroup;
            this.ucInsGroup.SelectCmdForUpdate = null;
            this.ucInsGroup.SendSQLCmd = true;
            this.ucInsGroup.ServerModify = true;
            this.ucInsGroup.ServerModifyGetMax = false;
            this.ucInsGroup.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucInsGroup.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucInsGroup.UseTranscationScope = false;
            this.ucInsGroup.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_InsGroup
            // 
            this.View_InsGroup.CacheConnection = false;
            this.View_InsGroup.CommandText = "SELECT * FROM dbo.[InsGroup]";
            this.View_InsGroup.CommandTimeout = 30;
            this.View_InsGroup.CommandType = System.Data.CommandType.Text;
            this.View_InsGroup.DynamicTableName = false;
            this.View_InsGroup.EEPAlias = null;
            this.View_InsGroup.EncodingAfter = null;
            this.View_InsGroup.EncodingBefore = "Windows-1252";
            this.View_InsGroup.EncodingConvert = null;
            this.View_InsGroup.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "InsGroupID";
            this.View_InsGroup.KeyFields.Add(keyItem2);
            this.View_InsGroup.MultiSetWhere = false;
            this.View_InsGroup.Name = "View_InsGroup";
            this.View_InsGroup.NotificationAutoEnlist = false;
            this.View_InsGroup.SecExcept = null;
            this.View_InsGroup.SecFieldName = null;
            this.View_InsGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_InsGroup.SelectPaging = false;
            this.View_InsGroup.SelectTop = 0;
            this.View_InsGroup.SiteControl = false;
            this.View_InsGroup.SiteFieldName = null;
            this.View_InsGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // GroupRoles
            // 
            this.GroupRoles.CacheConnection = false;
            this.GroupRoles.CommandText = "SELECT GROUPID,GROUPNAME FROM EIPHRSYS.DBO.GROUPS\r\nWHERE LEFT(GROUPID,5)=\'10100\' " +
    "\r\nORDER BY GROUPID";
            this.GroupRoles.CommandTimeout = 30;
            this.GroupRoles.CommandType = System.Data.CommandType.Text;
            this.GroupRoles.DynamicTableName = false;
            this.GroupRoles.EEPAlias = null;
            this.GroupRoles.EncodingAfter = null;
            this.GroupRoles.EncodingBefore = "Windows-1252";
            this.GroupRoles.EncodingConvert = null;
            this.GroupRoles.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "GROUPID";
            this.GroupRoles.KeyFields.Add(keyItem3);
            this.GroupRoles.MultiSetWhere = false;
            this.GroupRoles.Name = "GroupRoles";
            this.GroupRoles.NotificationAutoEnlist = false;
            this.GroupRoles.SecExcept = null;
            this.GroupRoles.SecFieldName = null;
            this.GroupRoles.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.GroupRoles.SelectPaging = false;
            this.GroupRoles.SelectTop = 0;
            this.GroupRoles.SiteControl = false;
            this.GroupRoles.SiteFieldName = null;
            this.GroupRoles.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_InsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupRoles)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand InsGroup;
        private Srvtools.UpdateComponent ucInsGroup;
        private Srvtools.InfoCommand View_InsGroup;
        private Srvtools.InfoCommand GroupRoles;
    }
}
