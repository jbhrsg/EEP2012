namespace sERPDelayLunchApply
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
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPDelayLunchApply = new Srvtools.InfoCommand(this.components);
            this.ucERPDelayLunchApply = new Srvtools.UpdateComponent(this.components);
            this.infoEatRecord = new Srvtools.InfoCommand(this.components);
            this.infoAbsentRecord = new Srvtools.InfoCommand(this.components);
            this.DelayLunchID = new Srvtools.AutoNumber(this.components);
            this.infoDateRecord = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEatRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAbsentRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDateRecord)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "getLunchData";
            service1.NonLogin = false;
            service1.ServiceName = "getLunchData";
            service2.DelegateName = "checkLunchApplyClose";
            service2.NonLogin = false;
            service2.ServiceName = "checkLunchApplyClose";
            service3.DelegateName = "checkLunchData";
            service3.NonLogin = false;
            service3.ServiceName = "checkLunchData";
            service4.DelegateName = "checkLunchEmp";
            service4.NonLogin = false;
            service4.ServiceName = "checkLunchEmp";
            service5.DelegateName = "checkNotCheckDate";
            service5.NonLogin = false;
            service5.ServiceName = "checkNotCheckDate";
            service6.DelegateName = "checkDateduplicate";
            service6.NonLogin = false;
            service6.ServiceName = "checkDateduplicate";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPDelayLunchApply
            // 
            this.ERPDelayLunchApply.CacheConnection = false;
            this.ERPDelayLunchApply.CommandText = "SELECT dbo.[ERPDelayLunchApply].* FROM dbo.[ERPDelayLunchApply]";
            this.ERPDelayLunchApply.CommandTimeout = 30;
            this.ERPDelayLunchApply.CommandType = System.Data.CommandType.Text;
            this.ERPDelayLunchApply.DynamicTableName = false;
            this.ERPDelayLunchApply.EEPAlias = null;
            this.ERPDelayLunchApply.EncodingAfter = null;
            this.ERPDelayLunchApply.EncodingBefore = "Windows-1252";
            this.ERPDelayLunchApply.EncodingConvert = null;
            this.ERPDelayLunchApply.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "DelayLunchID";
            this.ERPDelayLunchApply.KeyFields.Add(keyItem1);
            this.ERPDelayLunchApply.MultiSetWhere = false;
            this.ERPDelayLunchApply.Name = "ERPDelayLunchApply";
            this.ERPDelayLunchApply.NotificationAutoEnlist = false;
            this.ERPDelayLunchApply.SecExcept = null;
            this.ERPDelayLunchApply.SecFieldName = null;
            this.ERPDelayLunchApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPDelayLunchApply.SelectPaging = false;
            this.ERPDelayLunchApply.SelectTop = 0;
            this.ERPDelayLunchApply.SiteControl = false;
            this.ERPDelayLunchApply.SiteFieldName = null;
            this.ERPDelayLunchApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPDelayLunchApply
            // 
            this.ucERPDelayLunchApply.AutoTrans = true;
            this.ucERPDelayLunchApply.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "DelayLunchID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "BeginDate";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EndDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "ApplyTotal";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ApplyEat";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ApplyAbsent";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CheckTotal";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CheckEat";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CheckAbsent";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CheckOther";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CheckOtherMemo";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "IsClose";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CloseDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "flowflag";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CreateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "CreateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "NotCheckDate";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr1);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr2);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr3);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr4);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr5);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr6);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr7);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr8);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr9);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr10);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr11);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr12);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr13);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr14);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr15);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr16);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr17);
            this.ucERPDelayLunchApply.LogInfo = null;
            this.ucERPDelayLunchApply.Name = "ucERPDelayLunchApply";
            this.ucERPDelayLunchApply.RowAffectsCheck = true;
            this.ucERPDelayLunchApply.SelectCmd = this.ERPDelayLunchApply;
            this.ucERPDelayLunchApply.SelectCmdForUpdate = null;
            this.ucERPDelayLunchApply.SendSQLCmd = true;
            this.ucERPDelayLunchApply.ServerModify = true;
            this.ucERPDelayLunchApply.ServerModifyGetMax = false;
            this.ucERPDelayLunchApply.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPDelayLunchApply.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPDelayLunchApply.UseTranscationScope = false;
            this.ucERPDelayLunchApply.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucERPDelayLunchApply.AfterInsert += new Srvtools.UpdateComponentAfterInsertEventHandler(this.ucERPDelayLunchApply_AfterInsert);
            // 
            // infoEatRecord
            // 
            this.infoEatRecord.CacheConnection = false;
            this.infoEatRecord.CommandText = " select distinct Convert(nvarchar(10),o.Adate,111) as Adate from [192.168.1.41].JBePortal.dbo.EmpOrderFood o  \r\n where o.UnitPrice!=0";
            this.infoEatRecord.CommandTimeout = 30;
            this.infoEatRecord.CommandType = System.Data.CommandType.Text;
            this.infoEatRecord.DynamicTableName = false;
            this.infoEatRecord.EEPAlias = null;
            this.infoEatRecord.EncodingAfter = null;
            this.infoEatRecord.EncodingBefore = "Windows-1252";
            this.infoEatRecord.EncodingConvert = null;
            this.infoEatRecord.InfoConnection = this.InfoConnection1;
            this.infoEatRecord.MultiSetWhere = false;
            this.infoEatRecord.Name = "infoEatRecord";
            this.infoEatRecord.NotificationAutoEnlist = false;
            this.infoEatRecord.SecExcept = null;
            this.infoEatRecord.SecFieldName = null;
            this.infoEatRecord.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoEatRecord.SelectPaging = false;
            this.infoEatRecord.SelectTop = 0;
            this.infoEatRecord.SiteControl = false;
            this.infoEatRecord.SiteFieldName = null;
            this.infoEatRecord.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoAbsentRecord
            // 
            this.infoAbsentRecord.CacheConnection = false;
            this.infoAbsentRecord.CommandText = resources.GetString("infoAbsentRecord.CommandText");
            this.infoAbsentRecord.CommandTimeout = 30;
            this.infoAbsentRecord.CommandType = System.Data.CommandType.Text;
            this.infoAbsentRecord.DynamicTableName = false;
            this.infoAbsentRecord.EEPAlias = null;
            this.infoAbsentRecord.EncodingAfter = null;
            this.infoAbsentRecord.EncodingBefore = "Windows-1252";
            this.infoAbsentRecord.EncodingConvert = null;
            this.infoAbsentRecord.InfoConnection = this.InfoConnection1;
            this.infoAbsentRecord.MultiSetWhere = false;
            this.infoAbsentRecord.Name = "infoAbsentRecord";
            this.infoAbsentRecord.NotificationAutoEnlist = false;
            this.infoAbsentRecord.SecExcept = null;
            this.infoAbsentRecord.SecFieldName = null;
            this.infoAbsentRecord.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoAbsentRecord.SelectPaging = false;
            this.infoAbsentRecord.SelectTop = 0;
            this.infoAbsentRecord.SiteControl = false;
            this.infoAbsentRecord.SiteFieldName = null;
            this.infoAbsentRecord.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DelayLunchID
            // 
            this.DelayLunchID.Active = true;
            this.DelayLunchID.AutoNoID = "DelayLunchID";
            this.DelayLunchID.Description = "";
            this.DelayLunchID.GetFixed = "";
            this.DelayLunchID.isNumFill = false;
            this.DelayLunchID.Name = "DelayLunchID";
            this.DelayLunchID.Number = null;
            this.DelayLunchID.NumDig = 10;
            this.DelayLunchID.OldVersion = false;
            this.DelayLunchID.OverFlow = true;
            this.DelayLunchID.StartValue = 1;
            this.DelayLunchID.Step = 1;
            this.DelayLunchID.TargetColumn = "DelayLunchID";
            this.DelayLunchID.UpdateComp = this.ucERPDelayLunchApply;
            // 
            // infoDateRecord
            // 
            this.infoDateRecord.CacheConnection = false;
            this.infoDateRecord.CommandText = "select NotCheckDate from ERPDelayLunchApplyDate";
            this.infoDateRecord.CommandTimeout = 30;
            this.infoDateRecord.CommandType = System.Data.CommandType.Text;
            this.infoDateRecord.DynamicTableName = false;
            this.infoDateRecord.EEPAlias = null;
            this.infoDateRecord.EncodingAfter = null;
            this.infoDateRecord.EncodingBefore = "Windows-1252";
            this.infoDateRecord.EncodingConvert = null;
            this.infoDateRecord.InfoConnection = this.InfoConnection1;
            this.infoDateRecord.MultiSetWhere = false;
            this.infoDateRecord.Name = "infoDateRecord";
            this.infoDateRecord.NotificationAutoEnlist = false;
            this.infoDateRecord.SecExcept = null;
            this.infoDateRecord.SecFieldName = null;
            this.infoDateRecord.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoDateRecord.SelectPaging = false;
            this.infoDateRecord.SelectTop = 0;
            this.infoDateRecord.SiteControl = false;
            this.infoDateRecord.SiteFieldName = null;
            this.infoDateRecord.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEatRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoAbsentRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDateRecord)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPDelayLunchApply;
        private Srvtools.UpdateComponent ucERPDelayLunchApply;
        private Srvtools.InfoCommand infoEatRecord;
        private Srvtools.InfoCommand infoAbsentRecord;
        private Srvtools.AutoNumber DelayLunchID;
        private Srvtools.InfoCommand infoDateRecord;
    }
}
