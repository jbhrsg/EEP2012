namespace _HRM_REC_Consultants_Management
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
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.REC_Consultants = new Srvtools.InfoCommand(this.components);
            this.ucREC_Consultants = new Srvtools.UpdateComponent(this.components);
            this.Rec_SalesTeam = new Srvtools.InfoCommand(this.components);
            this.infoUSERSUSERS = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REC_Consultants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rec_SalesTeam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoUSERSUSERS)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "InsertJBRecruitHandler";
            service1.NonLogin = false;
            service1.ServiceName = "InsertJBRecruitHandler";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBHRIS_DISPATCH";
            // 
            // REC_Consultants
            // 
            this.REC_Consultants.CacheConnection = false;
            this.REC_Consultants.CommandText = "SELECT dbo.[REC_Consultants].*,case when Gender=1 then \'男\' else \'女\' end as Gender" +
    "Text\r\nFROM dbo.[REC_Consultants]\r\norder by SalesTeamID,EmpID";
            this.REC_Consultants.CommandTimeout = 30;
            this.REC_Consultants.CommandType = System.Data.CommandType.Text;
            this.REC_Consultants.DynamicTableName = false;
            this.REC_Consultants.EEPAlias = "JBHRIS_DISPATCH";
            this.REC_Consultants.EncodingAfter = null;
            this.REC_Consultants.EncodingBefore = "Windows-1252";
            this.REC_Consultants.EncodingConvert = null;
            this.REC_Consultants.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ID";
            this.REC_Consultants.KeyFields.Add(keyItem1);
            this.REC_Consultants.MultiSetWhere = false;
            this.REC_Consultants.Name = "REC_Consultants";
            this.REC_Consultants.NotificationAutoEnlist = false;
            this.REC_Consultants.SecExcept = null;
            this.REC_Consultants.SecFieldName = null;
            this.REC_Consultants.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.REC_Consultants.SelectPaging = false;
            this.REC_Consultants.SelectTop = 0;
            this.REC_Consultants.SiteControl = false;
            this.REC_Consultants.SiteFieldName = null;
            this.REC_Consultants.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucREC_Consultants
            // 
            this.ucREC_Consultants.AutoTrans = true;
            this.ucREC_Consultants.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ConsultantName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ConsultantEName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "EmpID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ConsultantTel";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ConsultantMobile";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "SalesTeamID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LastUpdateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "LastUpdateDate";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "ConsultantEmail";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "IsActive";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr1);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr2);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr3);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr4);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr5);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr6);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr7);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr8);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr9);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr10);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr11);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr12);
            this.ucREC_Consultants.FieldAttrs.Add(fieldAttr13);
            this.ucREC_Consultants.LogInfo = null;
            this.ucREC_Consultants.Name = "ucREC_Consultants";
            this.ucREC_Consultants.RowAffectsCheck = true;
            this.ucREC_Consultants.SelectCmd = this.REC_Consultants;
            this.ucREC_Consultants.SelectCmdForUpdate = null;
            this.ucREC_Consultants.SendSQLCmd = true;
            this.ucREC_Consultants.ServerModify = true;
            this.ucREC_Consultants.ServerModifyGetMax = false;
            this.ucREC_Consultants.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucREC_Consultants.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucREC_Consultants.UseTranscationScope = false;
            this.ucREC_Consultants.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // Rec_SalesTeam
            // 
            this.Rec_SalesTeam.CacheConnection = false;
            this.Rec_SalesTeam.CommandText = "select * from Rec_SalesTeam\r\nOrder by ID";
            this.Rec_SalesTeam.CommandTimeout = 30;
            this.Rec_SalesTeam.CommandType = System.Data.CommandType.Text;
            this.Rec_SalesTeam.DynamicTableName = false;
            this.Rec_SalesTeam.EEPAlias = "JBHRIS_DISPATCH";
            this.Rec_SalesTeam.EncodingAfter = null;
            this.Rec_SalesTeam.EncodingBefore = "Windows-1252";
            this.Rec_SalesTeam.EncodingConvert = null;
            this.Rec_SalesTeam.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ID";
            this.Rec_SalesTeam.KeyFields.Add(keyItem2);
            this.Rec_SalesTeam.MultiSetWhere = false;
            this.Rec_SalesTeam.Name = "Rec_SalesTeam";
            this.Rec_SalesTeam.NotificationAutoEnlist = false;
            this.Rec_SalesTeam.SecExcept = null;
            this.Rec_SalesTeam.SecFieldName = null;
            this.Rec_SalesTeam.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Rec_SalesTeam.SelectPaging = false;
            this.Rec_SalesTeam.SelectTop = 0;
            this.Rec_SalesTeam.SiteControl = false;
            this.Rec_SalesTeam.SiteFieldName = null;
            this.Rec_SalesTeam.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoUSERSUSERS
            // 
            this.infoUSERSUSERS.CacheConnection = false;
            this.infoUSERSUSERS.CommandText = "select USERID,USERNAME,EMAIL from [USERS] where DESCRIPTION=\'JB\'";
            this.infoUSERSUSERS.CommandTimeout = 30;
            this.infoUSERSUSERS.CommandType = System.Data.CommandType.Text;
            this.infoUSERSUSERS.DynamicTableName = false;
            this.infoUSERSUSERS.EEPAlias = "EIPHRSYS";
            this.infoUSERSUSERS.EncodingAfter = null;
            this.infoUSERSUSERS.EncodingBefore = "Windows-1252";
            this.infoUSERSUSERS.EncodingConvert = null;
            this.infoUSERSUSERS.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "USERID";
            this.infoUSERSUSERS.KeyFields.Add(keyItem3);
            this.infoUSERSUSERS.MultiSetWhere = false;
            this.infoUSERSUSERS.Name = "infoUSERSUSERS";
            this.infoUSERSUSERS.NotificationAutoEnlist = false;
            this.infoUSERSUSERS.SecExcept = null;
            this.infoUSERSUSERS.SecFieldName = null;
            this.infoUSERSUSERS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoUSERSUSERS.SelectPaging = false;
            this.infoUSERSUSERS.SelectTop = 0;
            this.infoUSERSUSERS.SiteControl = false;
            this.infoUSERSUSERS.SiteFieldName = null;
            this.infoUSERSUSERS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REC_Consultants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rec_SalesTeam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoUSERSUSERS)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand REC_Consultants;
        private Srvtools.UpdateComponent ucREC_Consultants;
        private Srvtools.InfoCommand Rec_SalesTeam;
        private Srvtools.InfoCommand infoUSERSUSERS;

    }
}
