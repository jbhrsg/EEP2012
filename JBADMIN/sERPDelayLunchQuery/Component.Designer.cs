namespace sERPDelayLunchQuery
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
            Srvtools.Service service7 = new Srvtools.Service();
            Srvtools.Service service8 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPDelayLunchApply = new Srvtools.InfoCommand(this.components);
            this.ucERPDelayLunchApply = new Srvtools.UpdateComponent(this.components);
            this.ERPDelayLunchList = new Srvtools.InfoCommand(this.components);
            this.ucERPDelayLunchList = new Srvtools.UpdateComponent(this.components);
            this.infoEmpOrder = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmpOrder)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetLunchApplyYM";
            service1.NonLogin = false;
            service1.ServiceName = "GetLunchApplyYM";
            service2.DelegateName = "checkLunchYMClose";
            service2.NonLogin = false;
            service2.ServiceName = "checkLunchYMClose";
            service3.DelegateName = "InsertDelayLunch";
            service3.NonLogin = false;
            service3.ServiceName = "InsertDelayLunch";
            service4.DelegateName = "UpdateDelayLunch";
            service4.NonLogin = false;
            service4.ServiceName = "UpdateDelayLunch";
            service5.DelegateName = "ReportDelayLunch";
            service5.NonLogin = false;
            service5.ServiceName = "ReportDelayLunch";
            service6.DelegateName = "DelayDateList";
            service6.NonLogin = false;
            service6.ServiceName = "DelayDateList";
            service7.DelegateName = "checkLunchEmpOrder";
            service7.NonLogin = false;
            service7.ServiceName = "checkLunchEmpOrder";
            service8.DelegateName = "JBePortalEmpOrderList";
            service8.NonLogin = false;
            service8.ServiceName = "JBePortalEmpOrderList";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPDelayLunchApply
            // 
            this.ERPDelayLunchApply.CacheConnection = false;
            this.ERPDelayLunchApply.CommandText = resources.GetString("ERPDelayLunchApply.CommandText");
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
            fieldAttr2.DataField = "UserID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "BeginDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "EndDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "YearMonth";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ApplyTotal";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ApplyEat";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ApplyAbsent";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "ApplyAmt";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CheckTotal";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CheckEat";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CheckAbsent";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CheckOther";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CheckAmt";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "CheckOtherMemo";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "IsClose";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CloseDate";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "flowflag";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "CreateBy";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CreateDate";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
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
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr18);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr19);
            this.ucERPDelayLunchApply.FieldAttrs.Add(fieldAttr20);
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
            // 
            // ERPDelayLunchList
            // 
            this.ERPDelayLunchList.CacheConnection = false;
            this.ERPDelayLunchList.CommandText = "select a.*,c.NAME_C\r\nfrom ERPDelayLunchApply a \r\n inner join [JBHR_EEP].dbo.HRM_B" +
    "ASE_BASE c on a.UserID=c.EMPLOYEE_CODE\r\n where flowflag=\'Z\' ";
            this.ERPDelayLunchList.CommandTimeout = 30;
            this.ERPDelayLunchList.CommandType = System.Data.CommandType.Text;
            this.ERPDelayLunchList.DynamicTableName = false;
            this.ERPDelayLunchList.EEPAlias = null;
            this.ERPDelayLunchList.EncodingAfter = null;
            this.ERPDelayLunchList.EncodingBefore = "Windows-1252";
            this.ERPDelayLunchList.EncodingConvert = null;
            this.ERPDelayLunchList.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DelayLunchID";
            this.ERPDelayLunchList.KeyFields.Add(keyItem2);
            this.ERPDelayLunchList.MultiSetWhere = false;
            this.ERPDelayLunchList.Name = "ERPDelayLunchList";
            this.ERPDelayLunchList.NotificationAutoEnlist = false;
            this.ERPDelayLunchList.SecExcept = null;
            this.ERPDelayLunchList.SecFieldName = null;
            this.ERPDelayLunchList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPDelayLunchList.SelectPaging = false;
            this.ERPDelayLunchList.SelectTop = 0;
            this.ERPDelayLunchList.SiteControl = false;
            this.ERPDelayLunchList.SiteFieldName = null;
            this.ERPDelayLunchList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPDelayLunchList
            // 
            this.ucERPDelayLunchList.AutoTrans = true;
            this.ucERPDelayLunchList.ExceptJoin = false;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CheckAmt";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "CheckOtherMemo";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "CreateBy";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            this.ucERPDelayLunchList.FieldAttrs.Add(fieldAttr21);
            this.ucERPDelayLunchList.FieldAttrs.Add(fieldAttr22);
            this.ucERPDelayLunchList.FieldAttrs.Add(fieldAttr23);
            this.ucERPDelayLunchList.LogInfo = null;
            this.ucERPDelayLunchList.Name = "ucERPDelayLunchList";
            this.ucERPDelayLunchList.RowAffectsCheck = true;
            this.ucERPDelayLunchList.SelectCmd = this.ERPDelayLunchList;
            this.ucERPDelayLunchList.SelectCmdForUpdate = null;
            this.ucERPDelayLunchList.SendSQLCmd = true;
            this.ucERPDelayLunchList.ServerModify = true;
            this.ucERPDelayLunchList.ServerModifyGetMax = false;
            this.ucERPDelayLunchList.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPDelayLunchList.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPDelayLunchList.UseTranscationScope = false;
            this.ucERPDelayLunchList.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoEmpOrder
            // 
            this.infoEmpOrder.CacheConnection = false;
            this.infoEmpOrder.CommandText = "select EmpNum,NAME_C,sum(Price) as Price,SalaryID\r\nfrom View_JBePortalEmpOrder\r\nw" +
    "here 1=1\r\ngroup by EmpNum,NAME_C,SalaryID";
            this.infoEmpOrder.CommandTimeout = 30;
            this.infoEmpOrder.CommandType = System.Data.CommandType.Text;
            this.infoEmpOrder.DynamicTableName = false;
            this.infoEmpOrder.EEPAlias = null;
            this.infoEmpOrder.EncodingAfter = null;
            this.infoEmpOrder.EncodingBefore = "Windows-1252";
            this.infoEmpOrder.EncodingConvert = null;
            this.infoEmpOrder.InfoConnection = this.InfoConnection1;
            this.infoEmpOrder.MultiSetWhere = false;
            this.infoEmpOrder.Name = "infoEmpOrder";
            this.infoEmpOrder.NotificationAutoEnlist = false;
            this.infoEmpOrder.SecExcept = null;
            this.infoEmpOrder.SecFieldName = null;
            this.infoEmpOrder.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoEmpOrder.SelectPaging = false;
            this.infoEmpOrder.SelectTop = 0;
            this.infoEmpOrder.SiteControl = false;
            this.infoEmpOrder.SiteFieldName = null;
            this.infoEmpOrder.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPDelayLunchList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmpOrder)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPDelayLunchApply;
        private Srvtools.UpdateComponent ucERPDelayLunchApply;
        private Srvtools.InfoCommand ERPDelayLunchList;
        private Srvtools.UpdateComponent ucERPDelayLunchList;
        private Srvtools.InfoCommand infoEmpOrder;
    }
}
