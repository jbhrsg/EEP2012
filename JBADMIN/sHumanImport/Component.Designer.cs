namespace sHumanImport
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
            Srvtools.Service service9 = new Srvtools.Service();
            Srvtools.Service service10 = new Srvtools.Service();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.HumanImport = new Srvtools.InfoCommand(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ucHumanImport = new Srvtools.UpdateComponent(this.components);
            this.ucHuman = new Srvtools.UpdateComponent(this.components);
            this.infoHuman = new Srvtools.InfoCommand(this.components);
            this.infoHumanClass = new Srvtools.InfoCommand(this.components);
            this.Human_HumanClass = new Srvtools.InfoDataSource(this.components);
            this.infoHumanClassSet = new Srvtools.InfoCommand(this.components);
            this.ucHumanClass = new Srvtools.UpdateComponent(this.components);
            this.infoHumanClassQuery = new Srvtools.InfoCommand(this.components);
            this.updateClassQuery = new Srvtools.UpdateComponent(this.components);
            this.updateHumanClassSet = new Srvtools.UpdateComponent(this.components);
            this.infoHumanClassSetNew = new Srvtools.InfoCommand(this.components);
            this.infoImportInfo = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.HumanImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHuman)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHumanClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHumanClassSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHumanClassQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHumanClassSetNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoImportInfo)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "DataValidate";
            service1.NonLogin = false;
            service1.ServiceName = "DataValidate";
            service2.DelegateName = "ExcelFileImport";
            service2.NonLogin = false;
            service2.ServiceName = "ExcelFileImport";
            service3.DelegateName = "GetrHumanClass";
            service3.NonLogin = false;
            service3.ServiceName = "GetrHumanClass";
            service4.DelegateName = "HumanSelect";
            service4.NonLogin = false;
            service4.ServiceName = "HumanSelect";
            service5.DelegateName = "HumanSelectExcel";
            service5.NonLogin = false;
            service5.ServiceName = "HumanSelectExcel";
            service6.DelegateName = "AddHumanLabel";
            service6.NonLogin = false;
            service6.ServiceName = "AddHumanLabel";
            service7.DelegateName = "ClearQueryLabel";
            service7.NonLogin = false;
            service7.ServiceName = "ClearQueryLabel";
            service8.DelegateName = "CheckHumanClassSet";
            service8.NonLogin = false;
            service8.ServiceName = "CheckHumanClassSet";
            service9.DelegateName = "DeleteHumanID";
            service9.NonLogin = false;
            service9.ServiceName = "DeleteHumanID";
            service10.DelegateName = "GetHumanImportInfo";
            service10.NonLogin = false;
            service10.ServiceName = "GetHumanImportInfo";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            this.serviceManager1.ServiceCollection.Add(service9);
            this.serviceManager1.ServiceCollection.Add(service10);
            // 
            // HumanImport
            // 
            this.HumanImport.CacheConnection = false;
            this.HumanImport.CommandText = "select * from HumanImport";
            this.HumanImport.CommandTimeout = 30;
            this.HumanImport.CommandType = System.Data.CommandType.Text;
            this.HumanImport.DynamicTableName = false;
            this.HumanImport.EEPAlias = "";
            this.HumanImport.EncodingAfter = null;
            this.HumanImport.EncodingBefore = "Windows-1252";
            this.HumanImport.EncodingConvert = null;
            this.HumanImport.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.HumanImport.KeyFields.Add(keyItem1);
            this.HumanImport.MultiSetWhere = false;
            this.HumanImport.Name = "HumanImport";
            this.HumanImport.NotificationAutoEnlist = false;
            this.HumanImport.SecExcept = null;
            this.HumanImport.SecFieldName = null;
            this.HumanImport.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HumanImport.SelectPaging = false;
            this.HumanImport.SelectTop = 0;
            this.HumanImport.SiteControl = false;
            this.HumanImport.SiteFieldName = null;
            this.HumanImport.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ucHumanImport
            // 
            this.ucHumanImport.AutoTrans = true;
            this.ucHumanImport.ExceptJoin = false;
            this.ucHumanImport.LogInfo = null;
            this.ucHumanImport.Name = "ucHumanImport";
            this.ucHumanImport.RowAffectsCheck = true;
            this.ucHumanImport.SelectCmd = this.HumanImport;
            this.ucHumanImport.SelectCmdForUpdate = null;
            this.ucHumanImport.SendSQLCmd = true;
            this.ucHumanImport.ServerModify = true;
            this.ucHumanImport.ServerModifyGetMax = false;
            this.ucHumanImport.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHumanImport.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHumanImport.UseTranscationScope = false;
            this.ucHumanImport.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHumanImport.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHumanImport_BeforeInsert);
            // 
            // ucHuman
            // 
            this.ucHuman.AutoTrans = true;
            this.ucHuman.ExceptJoin = false;
            this.ucHuman.LogInfo = null;
            this.ucHuman.Name = "ucHuman";
            this.ucHuman.RowAffectsCheck = true;
            this.ucHuman.SelectCmd = this.infoHuman;
            this.ucHuman.SelectCmdForUpdate = null;
            this.ucHuman.SendSQLCmd = true;
            this.ucHuman.ServerModify = true;
            this.ucHuman.ServerModifyGetMax = false;
            this.ucHuman.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHuman.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHuman.UseTranscationScope = false;
            this.ucHuman.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucHuman.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucHuman_BeforeInsert);
            // 
            // infoHuman
            // 
            this.infoHuman.CacheConnection = false;
            this.infoHuman.CommandText = "select * from Human";
            this.infoHuman.CommandTimeout = 30;
            this.infoHuman.CommandType = System.Data.CommandType.Text;
            this.infoHuman.DynamicTableName = false;
            this.infoHuman.EEPAlias = "";
            this.infoHuman.EncodingAfter = null;
            this.infoHuman.EncodingBefore = "Windows-1252";
            this.infoHuman.EncodingConvert = null;
            this.infoHuman.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.infoHuman.KeyFields.Add(keyItem2);
            this.infoHuman.MultiSetWhere = false;
            this.infoHuman.Name = "infoHuman";
            this.infoHuman.NotificationAutoEnlist = false;
            this.infoHuman.SecExcept = null;
            this.infoHuman.SecFieldName = null;
            this.infoHuman.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoHuman.SelectPaging = false;
            this.infoHuman.SelectTop = 0;
            this.infoHuman.SiteControl = false;
            this.infoHuman.SiteFieldName = null;
            this.infoHuman.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoHumanClass
            // 
            this.infoHumanClass.CacheConnection = false;
            this.infoHumanClass.CommandText = resources.GetString("infoHumanClass.CommandText");
            this.infoHumanClass.CommandTimeout = 30;
            this.infoHumanClass.CommandType = System.Data.CommandType.Text;
            this.infoHumanClass.DynamicTableName = false;
            this.infoHumanClass.EEPAlias = "";
            this.infoHumanClass.EncodingAfter = null;
            this.infoHumanClass.EncodingBefore = "Windows-1252";
            this.infoHumanClass.EncodingConvert = null;
            this.infoHumanClass.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "AutoKey";
            this.infoHumanClass.KeyFields.Add(keyItem3);
            this.infoHumanClass.MultiSetWhere = false;
            this.infoHumanClass.Name = "infoHumanClass";
            this.infoHumanClass.NotificationAutoEnlist = false;
            this.infoHumanClass.SecExcept = null;
            this.infoHumanClass.SecFieldName = null;
            this.infoHumanClass.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoHumanClass.SelectPaging = false;
            this.infoHumanClass.SelectTop = 0;
            this.infoHumanClass.SiteControl = false;
            this.infoHumanClass.SiteFieldName = null;
            this.infoHumanClass.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Human_HumanClass
            // 
            this.Human_HumanClass.Detail = this.infoHumanClass;
            columnItem1.FieldName = "HumanID";
            this.Human_HumanClass.DetailColumns.Add(columnItem1);
            this.Human_HumanClass.DynamicTableName = false;
            this.Human_HumanClass.Master = this.infoHuman;
            columnItem2.FieldName = "HumanID";
            this.Human_HumanClass.MasterColumns.Add(columnItem2);
            // 
            // infoHumanClassSet
            // 
            this.infoHumanClassSet.CacheConnection = false;
            this.infoHumanClassSet.CommandText = "select * from HumanClassSet\r\norder by ClassText";
            this.infoHumanClassSet.CommandTimeout = 30;
            this.infoHumanClassSet.CommandType = System.Data.CommandType.Text;
            this.infoHumanClassSet.DynamicTableName = false;
            this.infoHumanClassSet.EEPAlias = "";
            this.infoHumanClassSet.EncodingAfter = null;
            this.infoHumanClassSet.EncodingBefore = "Windows-1252";
            this.infoHumanClassSet.EncodingConvert = null;
            this.infoHumanClassSet.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "AutoKey";
            this.infoHumanClassSet.KeyFields.Add(keyItem4);
            this.infoHumanClassSet.MultiSetWhere = false;
            this.infoHumanClassSet.Name = "infoHumanClassSet";
            this.infoHumanClassSet.NotificationAutoEnlist = false;
            this.infoHumanClassSet.SecExcept = null;
            this.infoHumanClassSet.SecFieldName = null;
            this.infoHumanClassSet.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoHumanClassSet.SelectPaging = false;
            this.infoHumanClassSet.SelectTop = 0;
            this.infoHumanClassSet.SiteControl = false;
            this.infoHumanClassSet.SiteFieldName = null;
            this.infoHumanClassSet.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHumanClass
            // 
            this.ucHumanClass.AutoTrans = true;
            this.ucHumanClass.ExceptJoin = false;
            this.ucHumanClass.LogInfo = null;
            this.ucHumanClass.Name = "ucHumanClass";
            this.ucHumanClass.RowAffectsCheck = true;
            this.ucHumanClass.SelectCmd = this.infoHumanClass;
            this.ucHumanClass.SelectCmdForUpdate = null;
            this.ucHumanClass.SendSQLCmd = true;
            this.ucHumanClass.ServerModify = true;
            this.ucHumanClass.ServerModifyGetMax = false;
            this.ucHumanClass.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHumanClass.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHumanClass.UseTranscationScope = false;
            this.ucHumanClass.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoHumanClassQuery
            // 
            this.infoHumanClassQuery.CacheConnection = false;
            this.infoHumanClassQuery.CommandText = "select * from HumanClassQuery\r\norder by CreateDate desc";
            this.infoHumanClassQuery.CommandTimeout = 30;
            this.infoHumanClassQuery.CommandType = System.Data.CommandType.Text;
            this.infoHumanClassQuery.DynamicTableName = false;
            this.infoHumanClassQuery.EEPAlias = "";
            this.infoHumanClassQuery.EncodingAfter = null;
            this.infoHumanClassQuery.EncodingBefore = "Windows-1252";
            this.infoHumanClassQuery.EncodingConvert = null;
            this.infoHumanClassQuery.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "AutoKey";
            this.infoHumanClassQuery.KeyFields.Add(keyItem5);
            this.infoHumanClassQuery.MultiSetWhere = false;
            this.infoHumanClassQuery.Name = "infoHumanClassQuery";
            this.infoHumanClassQuery.NotificationAutoEnlist = false;
            this.infoHumanClassQuery.SecExcept = null;
            this.infoHumanClassQuery.SecFieldName = null;
            this.infoHumanClassQuery.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoHumanClassQuery.SelectPaging = false;
            this.infoHumanClassQuery.SelectTop = 0;
            this.infoHumanClassQuery.SiteControl = false;
            this.infoHumanClassQuery.SiteFieldName = null;
            this.infoHumanClassQuery.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // updateClassQuery
            // 
            this.updateClassQuery.AutoTrans = true;
            this.updateClassQuery.ExceptJoin = false;
            this.updateClassQuery.LogInfo = null;
            this.updateClassQuery.Name = "updateClassQuery";
            this.updateClassQuery.RowAffectsCheck = true;
            this.updateClassQuery.SelectCmd = this.infoHumanClassQuery;
            this.updateClassQuery.SelectCmdForUpdate = null;
            this.updateClassQuery.SendSQLCmd = true;
            this.updateClassQuery.ServerModify = true;
            this.updateClassQuery.ServerModifyGetMax = false;
            this.updateClassQuery.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.updateClassQuery.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.updateClassQuery.UseTranscationScope = false;
            this.updateClassQuery.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.updateClassQuery.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.updateClassQuery_BeforeInsert);
            // 
            // updateHumanClassSet
            // 
            this.updateHumanClassSet.AutoTrans = true;
            this.updateHumanClassSet.ExceptJoin = false;
            this.updateHumanClassSet.LogInfo = null;
            this.updateHumanClassSet.Name = "updateHumanClassSet";
            this.updateHumanClassSet.RowAffectsCheck = true;
            this.updateHumanClassSet.SelectCmd = this.infoHumanClassSetNew;
            this.updateHumanClassSet.SelectCmdForUpdate = null;
            this.updateHumanClassSet.SendSQLCmd = true;
            this.updateHumanClassSet.ServerModify = true;
            this.updateHumanClassSet.ServerModifyGetMax = false;
            this.updateHumanClassSet.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.updateHumanClassSet.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.updateHumanClassSet.UseTranscationScope = false;
            this.updateHumanClassSet.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // infoHumanClassSetNew
            // 
            this.infoHumanClassSetNew.CacheConnection = false;
            this.infoHumanClassSetNew.CommandText = "select * from HumanClassSet\r\norder by AutoKey desc";
            this.infoHumanClassSetNew.CommandTimeout = 30;
            this.infoHumanClassSetNew.CommandType = System.Data.CommandType.Text;
            this.infoHumanClassSetNew.DynamicTableName = false;
            this.infoHumanClassSetNew.EEPAlias = "";
            this.infoHumanClassSetNew.EncodingAfter = null;
            this.infoHumanClassSetNew.EncodingBefore = "Windows-1252";
            this.infoHumanClassSetNew.EncodingConvert = null;
            this.infoHumanClassSetNew.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "AutoKey";
            this.infoHumanClassSetNew.KeyFields.Add(keyItem6);
            this.infoHumanClassSetNew.MultiSetWhere = false;
            this.infoHumanClassSetNew.Name = "infoHumanClassSetNew";
            this.infoHumanClassSetNew.NotificationAutoEnlist = false;
            this.infoHumanClassSetNew.SecExcept = null;
            this.infoHumanClassSetNew.SecFieldName = null;
            this.infoHumanClassSetNew.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoHumanClassSetNew.SelectPaging = false;
            this.infoHumanClassSetNew.SelectTop = 0;
            this.infoHumanClassSetNew.SiteControl = false;
            this.infoHumanClassSetNew.SiteFieldName = null;
            this.infoHumanClassSetNew.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoImportInfo
            // 
            this.infoImportInfo.CacheConnection = false;
            this.infoImportInfo.CommandText = "select * from HumanImportInfo\r\norder by cDate desc";
            this.infoImportInfo.CommandTimeout = 30;
            this.infoImportInfo.CommandType = System.Data.CommandType.Text;
            this.infoImportInfo.DynamicTableName = false;
            this.infoImportInfo.EEPAlias = "";
            this.infoImportInfo.EncodingAfter = null;
            this.infoImportInfo.EncodingBefore = "Windows-1252";
            this.infoImportInfo.EncodingConvert = null;
            this.infoImportInfo.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "AutoKey";
            this.infoImportInfo.KeyFields.Add(keyItem7);
            this.infoImportInfo.MultiSetWhere = false;
            this.infoImportInfo.Name = "infoImportInfo";
            this.infoImportInfo.NotificationAutoEnlist = false;
            this.infoImportInfo.SecExcept = null;
            this.infoImportInfo.SecFieldName = null;
            this.infoImportInfo.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoImportInfo.SelectPaging = false;
            this.infoImportInfo.SelectTop = 0;
            this.infoImportInfo.SiteControl = false;
            this.infoImportInfo.SiteFieldName = null;
            this.infoImportInfo.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.HumanImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHuman)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHumanClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHumanClassSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHumanClassQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoHumanClassSetNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoImportInfo)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoCommand HumanImport;
        private Srvtools.UpdateComponent ucHumanImport;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.UpdateComponent ucHuman;
        private Srvtools.InfoCommand infoHuman;
        private Srvtools.InfoCommand infoHumanClass;
        private Srvtools.InfoDataSource Human_HumanClass;
        private Srvtools.InfoCommand infoHumanClassSet;
        private Srvtools.UpdateComponent ucHumanClass;
        private Srvtools.InfoCommand infoHumanClassQuery;
        private Srvtools.UpdateComponent updateClassQuery;
        private Srvtools.UpdateComponent updateHumanClassSet;
        private Srvtools.InfoCommand infoHumanClassSetNew;
        private Srvtools.InfoCommand infoImportInfo;
    }
}
