namespace sJBRecruit
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
            Srvtools.Service service7 = new Srvtools.Service();
            Srvtools.Service service8 = new Srvtools.Service();
            Srvtools.Service service9 = new Srvtools.Service();
            Srvtools.Service service10 = new Srvtools.Service();
            Srvtools.Service service11 = new Srvtools.Service();
            Srvtools.Service service12 = new Srvtools.Service();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr24 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr25 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr26 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr27 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr28 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr29 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr30 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr31 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr32 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr33 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.ColumnItem columnItem3 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem4 = new Srvtools.ColumnItem();
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
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.UserList = new Srvtools.InfoCommand(this.components);
            this.UserDayCollarDetail = new Srvtools.InfoCommand(this.components);
            this.updateUserDayCollar = new Srvtools.UpdateComponent(this.components);
            this.infoUserID = new Srvtools.InfoCommand(this.components);
            this.infoCollarType = new Srvtools.InfoCommand(this.components);
            this.infoCustomerID = new Srvtools.InfoCommand(this.components);
            this.infoRecruitID = new Srvtools.InfoCommand(this.components);
            this.UserDayCollarMaster = new Srvtools.InfoCommand(this.components);
            this.idUserDayCollarMaster_Details = new Srvtools.InfoDataSource(this.components);
            this.updateUserDayCollarMaster = new Srvtools.UpdateComponent(this.components);
            this.infoServiceConsultants = new Srvtools.InfoCommand(this.components);
            this.infoREC_SalesTeam = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserDayCollarDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoUserID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCollarType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomerID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRecruitID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserDayCollarMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoServiceConsultants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoREC_SalesTeam)).BeginInit();
            // 
            // serviceManager1
            // 
            service7.DelegateName = "UserListEEP";
            service7.NonLogin = false;
            service7.ServiceName = "UserListEEP";
            service8.DelegateName = "UserListExcel";
            service8.NonLogin = false;
            service8.ServiceName = "UserListExcel";
            service9.DelegateName = "GetEmployer";
            service9.NonLogin = false;
            service9.ServiceName = "GetEmployer";
            service10.DelegateName = "InsertUserDayCollarUser";
            service10.NonLogin = false;
            service10.ServiceName = "InsertUserDayCollarUser";
            service11.DelegateName = "TxtUserDayCollarData";
            service11.NonLogin = false;
            service11.ServiceName = "TxtUserDayCollarData";
            service12.DelegateName = "TxtUserDayCollarDataSmall";
            service12.NonLogin = false;
            service12.ServiceName = "TxtUserDayCollarDataSmall";
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            this.serviceManager1.ServiceCollection.Add(service9);
            this.serviceManager1.ServiceCollection.Add(service10);
            this.serviceManager1.ServiceCollection.Add(service11);
            this.serviceManager1.ServiceCollection.Add(service12);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBRecruit";
            // 
            // UserList
            // 
            this.UserList.CacheConnection = false;
            this.UserList.CommandText = "SELECT top 100 dbo.[User].* FROM dbo.[User]";
            this.UserList.CommandTimeout = 30;
            this.UserList.CommandType = System.Data.CommandType.Text;
            this.UserList.DynamicTableName = false;
            this.UserList.EEPAlias = "JBRecruit";
            this.UserList.EncodingAfter = null;
            this.UserList.EncodingBefore = "Windows-1252";
            this.UserList.EncodingConvert = null;
            this.UserList.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "UserID";
            this.UserList.KeyFields.Add(keyItem6);
            this.UserList.MultiSetWhere = false;
            this.UserList.Name = "UserList";
            this.UserList.NotificationAutoEnlist = false;
            this.UserList.SecExcept = null;
            this.UserList.SecFieldName = null;
            this.UserList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UserList.SelectPaging = false;
            this.UserList.SelectTop = 0;
            this.UserList.SiteControl = false;
            this.UserList.SiteFieldName = null;
            this.UserList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // UserDayCollarDetail
            // 
            this.UserDayCollarDetail.CacheConnection = false;
            this.UserDayCollarDetail.CommandText = resources.GetString("UserDayCollarDetail.CommandText");
            this.UserDayCollarDetail.CommandTimeout = 30;
            this.UserDayCollarDetail.CommandType = System.Data.CommandType.Text;
            this.UserDayCollarDetail.DynamicTableName = false;
            this.UserDayCollarDetail.EEPAlias = "JBRecruit";
            this.UserDayCollarDetail.EncodingAfter = null;
            this.UserDayCollarDetail.EncodingBefore = "Windows-1252";
            this.UserDayCollarDetail.EncodingConvert = null;
            this.UserDayCollarDetail.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "Autokey";
            this.UserDayCollarDetail.KeyFields.Add(keyItem1);
            this.UserDayCollarDetail.MultiSetWhere = false;
            this.UserDayCollarDetail.Name = "UserDayCollarDetail";
            this.UserDayCollarDetail.NotificationAutoEnlist = false;
            this.UserDayCollarDetail.SecExcept = null;
            this.UserDayCollarDetail.SecFieldName = null;
            this.UserDayCollarDetail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UserDayCollarDetail.SelectPaging = false;
            this.UserDayCollarDetail.SelectTop = 0;
            this.UserDayCollarDetail.SiteControl = false;
            this.UserDayCollarDetail.SiteFieldName = null;
            this.UserDayCollarDetail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // updateUserDayCollar
            // 
            this.updateUserDayCollar.AutoTrans = true;
            this.updateUserDayCollar.ExceptJoin = false;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "AutoKey";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "RequisitionNO";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "Item";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "BorrowLendType";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "Acno";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "SubAcno";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "CostCenterID";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "Describe";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "Amt";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "CreateBy";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            fieldAttr33.CheckNull = false;
            fieldAttr33.DataField = "CreateDate";
            fieldAttr33.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr33.DefaultValue = null;
            fieldAttr33.TrimLength = 0;
            fieldAttr33.UpdateEnable = true;
            fieldAttr33.WhereMode = true;
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr23);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr24);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr25);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr26);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr27);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr28);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr29);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr30);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr31);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr32);
            this.updateUserDayCollar.FieldAttrs.Add(fieldAttr33);
            this.updateUserDayCollar.LogInfo = null;
            this.updateUserDayCollar.Name = "updateUserDayCollar";
            this.updateUserDayCollar.RowAffectsCheck = true;
            this.updateUserDayCollar.SelectCmd = this.UserDayCollarDetail;
            this.updateUserDayCollar.SelectCmdForUpdate = null;
            this.updateUserDayCollar.SendSQLCmd = true;
            this.updateUserDayCollar.ServerModify = true;
            this.updateUserDayCollar.ServerModifyGetMax = false;
            this.updateUserDayCollar.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.updateUserDayCollar.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.updateUserDayCollar.UseTranscationScope = false;
            this.updateUserDayCollar.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.updateUserDayCollar.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.updateUserDayCollar_BeforeInsert);
            this.updateUserDayCollar.AfterInsert += new Srvtools.UpdateComponentAfterInsertEventHandler(this.updateUserDayCollar_AfterInsert);
            this.updateUserDayCollar.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.updateUserDayCollar_BeforeModify);
            // 
            // infoUserID
            // 
            this.infoUserID.CacheConnection = false;
            this.infoUserID.CommandText = "SELECT top 100 dbo.[User].* FROM dbo.[User]";
            this.infoUserID.CommandTimeout = 30;
            this.infoUserID.CommandType = System.Data.CommandType.Text;
            this.infoUserID.DynamicTableName = false;
            this.infoUserID.EEPAlias = "JBRecruit";
            this.infoUserID.EncodingAfter = null;
            this.infoUserID.EncodingBefore = "Windows-1252";
            this.infoUserID.EncodingConvert = null;
            this.infoUserID.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "UserID";
            this.infoUserID.KeyFields.Add(keyItem2);
            this.infoUserID.MultiSetWhere = false;
            this.infoUserID.Name = "infoUserID";
            this.infoUserID.NotificationAutoEnlist = false;
            this.infoUserID.SecExcept = null;
            this.infoUserID.SecFieldName = null;
            this.infoUserID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoUserID.SelectPaging = false;
            this.infoUserID.SelectTop = 0;
            this.infoUserID.SiteControl = false;
            this.infoUserID.SiteFieldName = null;
            this.infoUserID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCollarType
            // 
            this.infoCollarType.CacheConnection = false;
            this.infoCollarType.CommandText = "SELECT 1 as ID,\'日領\' as Name\r\nunion all\r\nSelect 2 as ID,\'週領\' as Name";
            this.infoCollarType.CommandTimeout = 30;
            this.infoCollarType.CommandType = System.Data.CommandType.Text;
            this.infoCollarType.DynamicTableName = false;
            this.infoCollarType.EEPAlias = "JBRecruit";
            this.infoCollarType.EncodingAfter = null;
            this.infoCollarType.EncodingBefore = "Windows-1252";
            this.infoCollarType.EncodingConvert = null;
            this.infoCollarType.InfoConnection = this.InfoConnection1;
            this.infoCollarType.MultiSetWhere = false;
            this.infoCollarType.Name = "infoCollarType";
            this.infoCollarType.NotificationAutoEnlist = false;
            this.infoCollarType.SecExcept = null;
            this.infoCollarType.SecFieldName = null;
            this.infoCollarType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCollarType.SelectPaging = false;
            this.infoCollarType.SelectTop = 0;
            this.infoCollarType.SiteControl = false;
            this.infoCollarType.SiteFieldName = null;
            this.infoCollarType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCustomerID
            // 
            this.infoCustomerID.CacheConnection = false;
            this.infoCustomerID.CommandText = resources.GetString("infoCustomerID.CommandText");
            this.infoCustomerID.CommandTimeout = 30;
            this.infoCustomerID.CommandType = System.Data.CommandType.Text;
            this.infoCustomerID.DynamicTableName = false;
            this.infoCustomerID.EEPAlias = "JBRecruit";
            this.infoCustomerID.EncodingAfter = null;
            this.infoCustomerID.EncodingBefore = "Windows-1252";
            this.infoCustomerID.EncodingConvert = null;
            this.infoCustomerID.InfoConnection = this.InfoConnection1;
            this.infoCustomerID.MultiSetWhere = false;
            this.infoCustomerID.Name = "infoCustomerID";
            this.infoCustomerID.NotificationAutoEnlist = false;
            this.infoCustomerID.SecExcept = null;
            this.infoCustomerID.SecFieldName = null;
            this.infoCustomerID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCustomerID.SelectPaging = false;
            this.infoCustomerID.SelectTop = 0;
            this.infoCustomerID.SiteControl = false;
            this.infoCustomerID.SiteFieldName = null;
            this.infoCustomerID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoRecruitID
            // 
            this.infoRecruitID.CacheConnection = false;
            this.infoRecruitID.CommandText = "Select ListID,ListContent from ListTable\r\n where ListCategory = \'Handler\' and IsA" +
    "ctive=1 and ListID!=0 and ListID!=23\r\norder by ListContent";
            this.infoRecruitID.CommandTimeout = 30;
            this.infoRecruitID.CommandType = System.Data.CommandType.Text;
            this.infoRecruitID.DynamicTableName = false;
            this.infoRecruitID.EEPAlias = "JBRecruit";
            this.infoRecruitID.EncodingAfter = null;
            this.infoRecruitID.EncodingBefore = "Windows-1252";
            this.infoRecruitID.EncodingConvert = null;
            this.infoRecruitID.InfoConnection = this.InfoConnection1;
            this.infoRecruitID.MultiSetWhere = false;
            this.infoRecruitID.Name = "infoRecruitID";
            this.infoRecruitID.NotificationAutoEnlist = false;
            this.infoRecruitID.SecExcept = null;
            this.infoRecruitID.SecFieldName = null;
            this.infoRecruitID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoRecruitID.SelectPaging = false;
            this.infoRecruitID.SelectTop = 0;
            this.infoRecruitID.SiteControl = false;
            this.infoRecruitID.SiteFieldName = null;
            this.infoRecruitID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // UserDayCollarMaster
            // 
            this.UserDayCollarMaster.CacheConnection = false;
            this.UserDayCollarMaster.CommandText = "select *\r\nfrom vUserDayCollarMaster m\r\norder by m.CreateDate desc,m.Autokey desc";
            this.UserDayCollarMaster.CommandTimeout = 30;
            this.UserDayCollarMaster.CommandType = System.Data.CommandType.Text;
            this.UserDayCollarMaster.DynamicTableName = false;
            this.UserDayCollarMaster.EEPAlias = "JBRecruit";
            this.UserDayCollarMaster.EncodingAfter = null;
            this.UserDayCollarMaster.EncodingBefore = "Windows-1252";
            this.UserDayCollarMaster.EncodingConvert = null;
            this.UserDayCollarMaster.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "Autokey";
            this.UserDayCollarMaster.KeyFields.Add(keyItem3);
            this.UserDayCollarMaster.MultiSetWhere = false;
            this.UserDayCollarMaster.Name = "UserDayCollarMaster";
            this.UserDayCollarMaster.NotificationAutoEnlist = false;
            this.UserDayCollarMaster.SecExcept = null;
            this.UserDayCollarMaster.SecFieldName = null;
            this.UserDayCollarMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.UserDayCollarMaster.SelectPaging = false;
            this.UserDayCollarMaster.SelectTop = 0;
            this.UserDayCollarMaster.SiteControl = false;
            this.UserDayCollarMaster.SiteFieldName = null;
            this.UserDayCollarMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // idUserDayCollarMaster_Details
            // 
            this.idUserDayCollarMaster_Details.Detail = this.UserDayCollarDetail;
            columnItem3.FieldName = "MasterAutokey";
            this.idUserDayCollarMaster_Details.DetailColumns.Add(columnItem3);
            this.idUserDayCollarMaster_Details.DynamicTableName = false;
            this.idUserDayCollarMaster_Details.Master = this.UserDayCollarMaster;
            columnItem4.FieldName = "Autokey";
            this.idUserDayCollarMaster_Details.MasterColumns.Add(columnItem4);
            // 
            // updateUserDayCollarMaster
            // 
            this.updateUserDayCollarMaster.AutoTrans = true;
            this.updateUserDayCollarMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "RequisitionNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "Item";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "BorrowLendType";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Acno";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SubAcno";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CostCenterID";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "Describe";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Amt";
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
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr1);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr2);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr3);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr4);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr5);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr6);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr7);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr8);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr9);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr10);
            this.updateUserDayCollarMaster.FieldAttrs.Add(fieldAttr11);
            this.updateUserDayCollarMaster.LogInfo = null;
            this.updateUserDayCollarMaster.Name = "updateUserDayCollarMaster";
            this.updateUserDayCollarMaster.RowAffectsCheck = true;
            this.updateUserDayCollarMaster.SelectCmd = this.UserDayCollarMaster;
            this.updateUserDayCollarMaster.SelectCmdForUpdate = null;
            this.updateUserDayCollarMaster.SendSQLCmd = true;
            this.updateUserDayCollarMaster.ServerModify = true;
            this.updateUserDayCollarMaster.ServerModifyGetMax = false;
            this.updateUserDayCollarMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.updateUserDayCollarMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.updateUserDayCollarMaster.UseTranscationScope = false;
            this.updateUserDayCollarMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.updateUserDayCollarMaster.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.updateUserDayCollarMaster_BeforeInsert);
            this.updateUserDayCollarMaster.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.updateUserDayCollarMaster_BeforeModify);
            // 
            // infoServiceConsultants
            // 
            this.infoServiceConsultants.CacheConnection = false;
            this.infoServiceConsultants.CommandText = "select ID,ConsultantName+\' \'+ConsultantEName as ConsultantName,EmpID\r\n from REC_C" +
    "onsultants order by SalesTeamID,ConsultantEName";
            this.infoServiceConsultants.CommandTimeout = 30;
            this.infoServiceConsultants.CommandType = System.Data.CommandType.Text;
            this.infoServiceConsultants.DynamicTableName = false;
            this.infoServiceConsultants.EEPAlias = "JBHRIS_DISPATCH";
            this.infoServiceConsultants.EncodingAfter = null;
            this.infoServiceConsultants.EncodingBefore = "Windows-1252";
            this.infoServiceConsultants.EncodingConvert = null;
            this.infoServiceConsultants.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "ID";
            this.infoServiceConsultants.KeyFields.Add(keyItem4);
            this.infoServiceConsultants.MultiSetWhere = false;
            this.infoServiceConsultants.Name = "infoServiceConsultants";
            this.infoServiceConsultants.NotificationAutoEnlist = false;
            this.infoServiceConsultants.SecExcept = null;
            this.infoServiceConsultants.SecFieldName = null;
            this.infoServiceConsultants.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoServiceConsultants.SelectPaging = false;
            this.infoServiceConsultants.SelectTop = 0;
            this.infoServiceConsultants.SiteControl = false;
            this.infoServiceConsultants.SiteFieldName = null;
            this.infoServiceConsultants.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoREC_SalesTeam
            // 
            this.infoREC_SalesTeam.CacheConnection = false;
            this.infoREC_SalesTeam.CommandText = "select ID,SalesTeamName from REC_SalesTeam";
            this.infoREC_SalesTeam.CommandTimeout = 30;
            this.infoREC_SalesTeam.CommandType = System.Data.CommandType.Text;
            this.infoREC_SalesTeam.DynamicTableName = false;
            this.infoREC_SalesTeam.EEPAlias = "JBHRIS_DISPATCH";
            this.infoREC_SalesTeam.EncodingAfter = null;
            this.infoREC_SalesTeam.EncodingBefore = "Windows-1252";
            this.infoREC_SalesTeam.EncodingConvert = null;
            this.infoREC_SalesTeam.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "ID";
            this.infoREC_SalesTeam.KeyFields.Add(keyItem5);
            this.infoREC_SalesTeam.MultiSetWhere = false;
            this.infoREC_SalesTeam.Name = "infoREC_SalesTeam";
            this.infoREC_SalesTeam.NotificationAutoEnlist = false;
            this.infoREC_SalesTeam.SecExcept = null;
            this.infoREC_SalesTeam.SecFieldName = null;
            this.infoREC_SalesTeam.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoREC_SalesTeam.SelectPaging = false;
            this.infoREC_SalesTeam.SelectTop = 0;
            this.infoREC_SalesTeam.SiteControl = false;
            this.infoREC_SalesTeam.SiteFieldName = null;
            this.infoREC_SalesTeam.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserDayCollarDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoUserID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCollarType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCustomerID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRecruitID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserDayCollarMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoServiceConsultants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoREC_SalesTeam)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand UserList;
        private Srvtools.InfoCommand UserDayCollarDetail;
        private Srvtools.UpdateComponent updateUserDayCollar;
        private Srvtools.InfoCommand infoUserID;
        private Srvtools.InfoCommand infoCollarType;
        private Srvtools.InfoCommand infoCustomerID;
        private Srvtools.InfoCommand infoRecruitID;
        private Srvtools.InfoCommand UserDayCollarMaster;
        private Srvtools.InfoDataSource idUserDayCollarMaster_Details;
        private Srvtools.UpdateComponent updateUserDayCollarMaster;
        private Srvtools.InfoCommand infoServiceConsultants;
        private Srvtools.InfoCommand infoREC_SalesTeam;
    }
}
