namespace sERPBizCardApply
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPBizCardApply = new Srvtools.InfoCommand(this.components);
            this.ucERPBizCardApply = new Srvtools.UpdateComponent(this.components);
            this.View_ERPBizCardApply = new Srvtools.InfoCommand(this.components);
            this.autoNumber1 = new Srvtools.AutoNumber(this.components);
            this.USERS = new Srvtools.InfoCommand(this.components);
            this.ERPBizCardApplyList = new Srvtools.InfoCommand(this.components);
            this.WorkPlaceAddress = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPBizCardApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPBizCardApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPBizCardApplyList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkPlaceAddress)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetLastApply";
            service1.NonLogin = false;
            service1.ServiceName = "GetLastApply";
            service2.DelegateName = "Update_ERPBizCardApply";
            service2.NonLogin = false;
            service2.ServiceName = "Update_ERPBizCardApply";
            service3.DelegateName = "GetUserID";
            service3.NonLogin = false;
            service3.ServiceName = "GetUserID";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPBizCardApply
            // 
            this.ERPBizCardApply.CacheConnection = false;
            this.ERPBizCardApply.CommandText = "SELECT dbo.[ERPBizCardApply].*,FilePath as FilePath1,FilePathA as FilePathA1 FROM" +
    " dbo.[ERPBizCardApply]";
            this.ERPBizCardApply.CommandTimeout = 30;
            this.ERPBizCardApply.CommandType = System.Data.CommandType.Text;
            this.ERPBizCardApply.DynamicTableName = false;
            this.ERPBizCardApply.EEPAlias = null;
            this.ERPBizCardApply.EncodingAfter = null;
            this.ERPBizCardApply.EncodingBefore = "Windows-1252";
            this.ERPBizCardApply.EncodingConvert = null;
            this.ERPBizCardApply.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "BizCardNO";
            this.ERPBizCardApply.KeyFields.Add(keyItem1);
            this.ERPBizCardApply.MultiSetWhere = false;
            this.ERPBizCardApply.Name = "ERPBizCardApply";
            this.ERPBizCardApply.NotificationAutoEnlist = false;
            this.ERPBizCardApply.SecExcept = null;
            this.ERPBizCardApply.SecFieldName = null;
            this.ERPBizCardApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPBizCardApply.SelectPaging = false;
            this.ERPBizCardApply.SelectTop = 0;
            this.ERPBizCardApply.SiteControl = false;
            this.ERPBizCardApply.SiteFieldName = null;
            this.ERPBizCardApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPBizCardApply
            // 
            this.ucERPBizCardApply.AutoTrans = true;
            this.ucERPBizCardApply.ExceptJoin = false;
            fieldAttr1.CheckNull = true;
            fieldAttr1.DataField = "BizCardNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "Workplace";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "Title";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Cname0";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Cname";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Ename";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "ExtNum";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "PhoneNum";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Skype";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LineID";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "Email";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "Remark";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "FilePath";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "FilePathA";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "ApplyEmpID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "Quantity";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "IsUrgent";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "IsPrint";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "UserConfirm";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr1);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr2);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr3);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr4);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr5);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr6);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr7);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr8);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr9);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr10);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr11);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr12);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr13);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr14);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr15);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr16);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr17);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr18);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr19);
            this.ucERPBizCardApply.FieldAttrs.Add(fieldAttr20);
            this.ucERPBizCardApply.LogInfo = null;
            this.ucERPBizCardApply.Name = "ucERPBizCardApply";
            this.ucERPBizCardApply.RowAffectsCheck = true;
            this.ucERPBizCardApply.SelectCmd = this.ERPBizCardApply;
            this.ucERPBizCardApply.SelectCmdForUpdate = null;
            this.ucERPBizCardApply.SendSQLCmd = true;
            this.ucERPBizCardApply.ServerModify = true;
            this.ucERPBizCardApply.ServerModifyGetMax = false;
            this.ucERPBizCardApply.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPBizCardApply.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPBizCardApply.UseTranscationScope = false;
            this.ucERPBizCardApply.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ERPBizCardApply
            // 
            this.View_ERPBizCardApply.CacheConnection = false;
            this.View_ERPBizCardApply.CommandText = "SELECT * FROM dbo.[ERPBizCardApply]";
            this.View_ERPBizCardApply.CommandTimeout = 30;
            this.View_ERPBizCardApply.CommandType = System.Data.CommandType.Text;
            this.View_ERPBizCardApply.DynamicTableName = false;
            this.View_ERPBizCardApply.EEPAlias = null;
            this.View_ERPBizCardApply.EncodingAfter = null;
            this.View_ERPBizCardApply.EncodingBefore = "Windows-1252";
            this.View_ERPBizCardApply.EncodingConvert = null;
            this.View_ERPBizCardApply.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "BizCardNO";
            this.View_ERPBizCardApply.KeyFields.Add(keyItem2);
            this.View_ERPBizCardApply.MultiSetWhere = false;
            this.View_ERPBizCardApply.Name = "View_ERPBizCardApply";
            this.View_ERPBizCardApply.NotificationAutoEnlist = false;
            this.View_ERPBizCardApply.SecExcept = null;
            this.View_ERPBizCardApply.SecFieldName = null;
            this.View_ERPBizCardApply.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPBizCardApply.SelectPaging = false;
            this.View_ERPBizCardApply.SelectTop = 0;
            this.View_ERPBizCardApply.SiteControl = false;
            this.View_ERPBizCardApply.SiteFieldName = null;
            this.View_ERPBizCardApply.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoNumber1
            // 
            this.autoNumber1.Active = true;
            this.autoNumber1.AutoNoID = "AutoBizCardNO";
            this.autoNumber1.Description = null;
            this.autoNumber1.GetFixed = "GetBizCardNOPrefix()";
            this.autoNumber1.isNumFill = false;
            this.autoNumber1.Name = "autoNumber1";
            this.autoNumber1.Number = null;
            this.autoNumber1.NumDig = 3;
            this.autoNumber1.OldVersion = false;
            this.autoNumber1.OverFlow = true;
            this.autoNumber1.StartValue = 1;
            this.autoNumber1.Step = 1;
            this.autoNumber1.TargetColumn = "BizCardNO";
            this.autoNumber1.UpdateComp = this.ucERPBizCardApply;
            // 
            // USERS
            // 
            this.USERS.CacheConnection = false;
            this.USERS.CommandText = "select USERID,USERNAME from EIPHRSYS.DBO.USERS where DESCRIPTION=\'JB\'";
            this.USERS.CommandTimeout = 30;
            this.USERS.CommandType = System.Data.CommandType.Text;
            this.USERS.DynamicTableName = false;
            this.USERS.EEPAlias = null;
            this.USERS.EncodingAfter = null;
            this.USERS.EncodingBefore = "Windows-1252";
            this.USERS.EncodingConvert = null;
            this.USERS.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "USERID";
            this.USERS.KeyFields.Add(keyItem3);
            this.USERS.MultiSetWhere = false;
            this.USERS.Name = "USERS";
            this.USERS.NotificationAutoEnlist = false;
            this.USERS.SecExcept = null;
            this.USERS.SecFieldName = null;
            this.USERS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.USERS.SelectPaging = false;
            this.USERS.SelectTop = 0;
            this.USERS.SiteControl = false;
            this.USERS.SiteFieldName = null;
            this.USERS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ERPBizCardApplyList
            // 
            this.ERPBizCardApplyList.CacheConnection = false;
            this.ERPBizCardApplyList.CommandText = "SELECT dbo.[ERPBizCardApply].*,FilePath as FilePath1,FilePathA as FilePathA1 FROM" +
    " dbo.[ERPBizCardApply] ";
            this.ERPBizCardApplyList.CommandTimeout = 30;
            this.ERPBizCardApplyList.CommandType = System.Data.CommandType.Text;
            this.ERPBizCardApplyList.DynamicTableName = false;
            this.ERPBizCardApplyList.EEPAlias = null;
            this.ERPBizCardApplyList.EncodingAfter = null;
            this.ERPBizCardApplyList.EncodingBefore = "Windows-1252";
            this.ERPBizCardApplyList.EncodingConvert = null;
            this.ERPBizCardApplyList.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "BizCardNO";
            this.ERPBizCardApplyList.KeyFields.Add(keyItem4);
            this.ERPBizCardApplyList.MultiSetWhere = false;
            this.ERPBizCardApplyList.Name = "ERPBizCardApplyList";
            this.ERPBizCardApplyList.NotificationAutoEnlist = false;
            this.ERPBizCardApplyList.SecExcept = null;
            this.ERPBizCardApplyList.SecFieldName = null;
            this.ERPBizCardApplyList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPBizCardApplyList.SelectPaging = false;
            this.ERPBizCardApplyList.SelectTop = 0;
            this.ERPBizCardApplyList.SiteControl = false;
            this.ERPBizCardApplyList.SiteFieldName = null;
            this.ERPBizCardApplyList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // WorkPlaceAddress
            // 
            this.WorkPlaceAddress.CacheConnection = false;
            this.WorkPlaceAddress.CommandText = "select * from WorkPlaceAddress";
            this.WorkPlaceAddress.CommandTimeout = 30;
            this.WorkPlaceAddress.CommandType = System.Data.CommandType.Text;
            this.WorkPlaceAddress.DynamicTableName = false;
            this.WorkPlaceAddress.EEPAlias = null;
            this.WorkPlaceAddress.EncodingAfter = null;
            this.WorkPlaceAddress.EncodingBefore = "Windows-1252";
            this.WorkPlaceAddress.EncodingConvert = null;
            this.WorkPlaceAddress.InfoConnection = this.InfoConnection1;
            this.WorkPlaceAddress.MultiSetWhere = false;
            this.WorkPlaceAddress.Name = "WorkPlaceAddress";
            this.WorkPlaceAddress.NotificationAutoEnlist = false;
            this.WorkPlaceAddress.SecExcept = null;
            this.WorkPlaceAddress.SecFieldName = null;
            this.WorkPlaceAddress.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.WorkPlaceAddress.SelectPaging = false;
            this.WorkPlaceAddress.SelectTop = 0;
            this.WorkPlaceAddress.SiteControl = false;
            this.WorkPlaceAddress.SiteFieldName = null;
            this.WorkPlaceAddress.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPBizCardApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPBizCardApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPBizCardApplyList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorkPlaceAddress)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPBizCardApply;
        private Srvtools.UpdateComponent ucERPBizCardApply;
        private Srvtools.InfoCommand View_ERPBizCardApply;
        private Srvtools.AutoNumber autoNumber1;
        private Srvtools.InfoCommand USERS;
        private Srvtools.InfoCommand ERPBizCardApplyList;
        private Srvtools.InfoCommand WorkPlaceAddress;
    }
}
