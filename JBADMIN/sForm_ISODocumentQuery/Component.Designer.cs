﻿namespace sForm_ISODocumentQuery
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr19 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr20 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr23 = new Srvtools.FieldAttr();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ISODocument = new Srvtools.InfoCommand(this.components);
            this.ucISODocument = new Srvtools.UpdateComponent(this.components);
            this.View_ISODocument = new Srvtools.InfoCommand(this.components);
            this.ISODocProperty = new Srvtools.InfoCommand(this.components);
            this.ISOFirstNO = new Srvtools.InfoCommand(this.components);
            this.USERS = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.ISOSecondNO = new Srvtools.InfoCommand(this.components);
            this.DocNO = new Srvtools.AutoNumber(this.components);
            this.FlowFlag = new Srvtools.InfoCommand(this.components);
            this.WhoCanDownload = new Srvtools.InfoCommand(this.components);
            this.WhoCanDownload_User = new Srvtools.InfoCommand(this.components);
            this.ISODocumentForm = new Srvtools.InfoCommand(this.components);
            this.ucISODocumentForm = new Srvtools.UpdateComponent(this.components);
            this.OrgCanDownload = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISODocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ISODocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISODocProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISOFirstNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISOSecondNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlowFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhoCanDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhoCanDownload_User)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISODocumentForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgCanDownload)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "IsProcess";
            service1.NonLogin = false;
            service1.ServiceName = "IsProcess";
            service2.DelegateName = "GetUserOrgNOs";
            service2.NonLogin = false;
            service2.ServiceName = "GetUserOrgNOs";
            service3.DelegateName = "ReturnUserNames";
            service3.NonLogin = false;
            service3.ServiceName = "ReturnUserNames";
            service4.DelegateName = "ReturnOrgNames";
            service4.NonLogin = false;
            service4.ServiceName = "ReturnOrgNames";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ISODocument
            // 
            this.ISODocument.CacheConnection = false;
            this.ISODocument.CommandText = "SELECT dbo.[ISODocument].*,\'\' as OrgCanDownloadC,\'\' as WhoCanDownloadC,\'\' as OrgC" +
    "anDownload1C,\'\' as WhoCanDownload1C FROM dbo.[ISODocument] order by DocNO desc";
            this.ISODocument.CommandTimeout = 30;
            this.ISODocument.CommandType = System.Data.CommandType.Text;
            this.ISODocument.DynamicTableName = false;
            this.ISODocument.EEPAlias = null;
            this.ISODocument.EncodingAfter = null;
            this.ISODocument.EncodingBefore = "Windows-1252";
            this.ISODocument.EncodingConvert = null;
            this.ISODocument.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "DocNO";
            this.ISODocument.KeyFields.Add(keyItem1);
            this.ISODocument.MultiSetWhere = false;
            this.ISODocument.Name = "ISODocument";
            this.ISODocument.NotificationAutoEnlist = false;
            this.ISODocument.SecExcept = null;
            this.ISODocument.SecFieldName = "";
            this.ISODocument.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ISODocument.SelectPaging = false;
            this.ISODocument.SelectTop = 0;
            this.ISODocument.SiteControl = false;
            this.ISODocument.SiteFieldName = null;
            this.ISODocument.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucISODocument
            // 
            this.ucISODocument.AutoTrans = true;
            this.ucISODocument.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "DocNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "DocPaperNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "FirstNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "SecondNO";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "DocPropertyNO";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "DocName";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Reason";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "PdfFile";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "WordFile";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "IsAllCanDownload";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "OrgCanDownload";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "WhoCanDownload";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = "_usercode";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = "_sysdate";
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr15.DefaultValue = "_usercode";
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LastUpdateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr16.DefaultValue = "_sysdate";
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "FlowFlag";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "IsModify";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.ucISODocument.FieldAttrs.Add(fieldAttr1);
            this.ucISODocument.FieldAttrs.Add(fieldAttr2);
            this.ucISODocument.FieldAttrs.Add(fieldAttr3);
            this.ucISODocument.FieldAttrs.Add(fieldAttr4);
            this.ucISODocument.FieldAttrs.Add(fieldAttr5);
            this.ucISODocument.FieldAttrs.Add(fieldAttr6);
            this.ucISODocument.FieldAttrs.Add(fieldAttr7);
            this.ucISODocument.FieldAttrs.Add(fieldAttr8);
            this.ucISODocument.FieldAttrs.Add(fieldAttr9);
            this.ucISODocument.FieldAttrs.Add(fieldAttr10);
            this.ucISODocument.FieldAttrs.Add(fieldAttr11);
            this.ucISODocument.FieldAttrs.Add(fieldAttr12);
            this.ucISODocument.FieldAttrs.Add(fieldAttr13);
            this.ucISODocument.FieldAttrs.Add(fieldAttr14);
            this.ucISODocument.FieldAttrs.Add(fieldAttr15);
            this.ucISODocument.FieldAttrs.Add(fieldAttr16);
            this.ucISODocument.FieldAttrs.Add(fieldAttr17);
            this.ucISODocument.FieldAttrs.Add(fieldAttr18);
            this.ucISODocument.LogInfo = null;
            this.ucISODocument.Name = "ucISODocument";
            this.ucISODocument.RowAffectsCheck = true;
            this.ucISODocument.SelectCmd = this.ISODocument;
            this.ucISODocument.SelectCmdForUpdate = null;
            this.ucISODocument.SendSQLCmd = true;
            this.ucISODocument.ServerModify = true;
            this.ucISODocument.ServerModifyGetMax = false;
            this.ucISODocument.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucISODocument.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucISODocument.UseTranscationScope = false;
            this.ucISODocument.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_ISODocument
            // 
            this.View_ISODocument.CacheConnection = false;
            this.View_ISODocument.CommandText = "SELECT * FROM dbo.[ISODocument]";
            this.View_ISODocument.CommandTimeout = 30;
            this.View_ISODocument.CommandType = System.Data.CommandType.Text;
            this.View_ISODocument.DynamicTableName = false;
            this.View_ISODocument.EEPAlias = null;
            this.View_ISODocument.EncodingAfter = null;
            this.View_ISODocument.EncodingBefore = "Windows-1252";
            this.View_ISODocument.EncodingConvert = null;
            this.View_ISODocument.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "DocNO";
            this.View_ISODocument.KeyFields.Add(keyItem2);
            this.View_ISODocument.MultiSetWhere = false;
            this.View_ISODocument.Name = "View_ISODocument";
            this.View_ISODocument.NotificationAutoEnlist = false;
            this.View_ISODocument.SecExcept = null;
            this.View_ISODocument.SecFieldName = null;
            this.View_ISODocument.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ISODocument.SelectPaging = false;
            this.View_ISODocument.SelectTop = 0;
            this.View_ISODocument.SiteControl = false;
            this.View_ISODocument.SiteFieldName = null;
            this.View_ISODocument.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ISODocProperty
            // 
            this.ISODocProperty.CacheConnection = false;
            this.ISODocProperty.CommandText = "select DocPropertyNO,DocPropertyName+\'-\'+DocPropertyNO as DocPropertyName from IS" +
    "ODocProperty";
            this.ISODocProperty.CommandTimeout = 30;
            this.ISODocProperty.CommandType = System.Data.CommandType.Text;
            this.ISODocProperty.DynamicTableName = false;
            this.ISODocProperty.EEPAlias = null;
            this.ISODocProperty.EncodingAfter = null;
            this.ISODocProperty.EncodingBefore = "Windows-1252";
            this.ISODocProperty.EncodingConvert = null;
            this.ISODocProperty.InfoConnection = this.InfoConnection1;
            this.ISODocProperty.MultiSetWhere = false;
            this.ISODocProperty.Name = "ISODocProperty";
            this.ISODocProperty.NotificationAutoEnlist = false;
            this.ISODocProperty.SecExcept = null;
            this.ISODocProperty.SecFieldName = null;
            this.ISODocProperty.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ISODocProperty.SelectPaging = false;
            this.ISODocProperty.SelectTop = 0;
            this.ISODocProperty.SiteControl = false;
            this.ISODocProperty.SiteFieldName = null;
            this.ISODocProperty.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ISOFirstNO
            // 
            this.ISOFirstNO.CacheConnection = false;
            this.ISOFirstNO.CommandText = "select FirstNO,FirstName+\'-\'+FirstNO as FirstName from ISOFirstNO";
            this.ISOFirstNO.CommandTimeout = 30;
            this.ISOFirstNO.CommandType = System.Data.CommandType.Text;
            this.ISOFirstNO.DynamicTableName = false;
            this.ISOFirstNO.EEPAlias = null;
            this.ISOFirstNO.EncodingAfter = null;
            this.ISOFirstNO.EncodingBefore = "Windows-1252";
            this.ISOFirstNO.EncodingConvert = null;
            this.ISOFirstNO.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "FirstNO";
            this.ISOFirstNO.KeyFields.Add(keyItem3);
            this.ISOFirstNO.MultiSetWhere = false;
            this.ISOFirstNO.Name = "ISOFirstNO";
            this.ISOFirstNO.NotificationAutoEnlist = false;
            this.ISOFirstNO.SecExcept = null;
            this.ISOFirstNO.SecFieldName = null;
            this.ISOFirstNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ISOFirstNO.SelectPaging = false;
            this.ISOFirstNO.SelectTop = 0;
            this.ISOFirstNO.SiteControl = false;
            this.ISOFirstNO.SiteFieldName = null;
            this.ISOFirstNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // USERS
            // 
            this.USERS.CacheConnection = false;
            this.USERS.CommandText = resources.GetString("USERS.CommandText");
            this.USERS.CommandTimeout = 30;
            this.USERS.CommandType = System.Data.CommandType.Text;
            this.USERS.DynamicTableName = false;
            this.USERS.EEPAlias = "EIPHRSYS";
            this.USERS.EncodingAfter = null;
            this.USERS.EncodingBefore = "Windows-1252";
            this.USERS.EncodingConvert = null;
            this.USERS.InfoConnection = this.infoConnection2;
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
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "EIPHRSYS";
            // 
            // ISOSecondNO
            // 
            this.ISOSecondNO.CacheConnection = false;
            this.ISOSecondNO.CommandText = "select SecondNO,FirstNO,SecondName+\'-\'+SecondNO as SecondName from ISOSecondNO";
            this.ISOSecondNO.CommandTimeout = 30;
            this.ISOSecondNO.CommandType = System.Data.CommandType.Text;
            this.ISOSecondNO.DynamicTableName = false;
            this.ISOSecondNO.EEPAlias = null;
            this.ISOSecondNO.EncodingAfter = null;
            this.ISOSecondNO.EncodingBefore = "Windows-1252";
            this.ISOSecondNO.EncodingConvert = null;
            this.ISOSecondNO.InfoConnection = this.InfoConnection1;
            this.ISOSecondNO.MultiSetWhere = false;
            this.ISOSecondNO.Name = "ISOSecondNO";
            this.ISOSecondNO.NotificationAutoEnlist = false;
            this.ISOSecondNO.SecExcept = null;
            this.ISOSecondNO.SecFieldName = null;
            this.ISOSecondNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ISOSecondNO.SelectPaging = false;
            this.ISOSecondNO.SelectTop = 0;
            this.ISOSecondNO.SiteControl = false;
            this.ISOSecondNO.SiteFieldName = null;
            this.ISOSecondNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DocNO
            // 
            this.DocNO.Active = true;
            this.DocNO.AutoNoID = "DocNO";
            this.DocNO.Description = null;
            this.DocNO.GetFixed = "DocNOGetFixed()";
            this.DocNO.isNumFill = false;
            this.DocNO.Name = "DocNO";
            this.DocNO.Number = null;
            this.DocNO.NumDig = 5;
            this.DocNO.OldVersion = false;
            this.DocNO.OverFlow = true;
            this.DocNO.StartValue = 1;
            this.DocNO.Step = 1;
            this.DocNO.TargetColumn = "DocNO";
            this.DocNO.UpdateComp = this.ucISODocument;
            // 
            // FlowFlag
            // 
            this.FlowFlag.CacheConnection = false;
            this.FlowFlag.CommandText = resources.GetString("FlowFlag.CommandText");
            this.FlowFlag.CommandTimeout = 30;
            this.FlowFlag.CommandType = System.Data.CommandType.Text;
            this.FlowFlag.DynamicTableName = false;
            this.FlowFlag.EEPAlias = null;
            this.FlowFlag.EncodingAfter = null;
            this.FlowFlag.EncodingBefore = "Windows-1252";
            this.FlowFlag.EncodingConvert = null;
            this.FlowFlag.InfoConnection = this.InfoConnection1;
            this.FlowFlag.MultiSetWhere = false;
            this.FlowFlag.Name = "FlowFlag";
            this.FlowFlag.NotificationAutoEnlist = false;
            this.FlowFlag.SecExcept = null;
            this.FlowFlag.SecFieldName = null;
            this.FlowFlag.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FlowFlag.SelectPaging = false;
            this.FlowFlag.SelectTop = 0;
            this.FlowFlag.SiteControl = false;
            this.FlowFlag.SiteFieldName = null;
            this.FlowFlag.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // WhoCanDownload
            // 
            this.WhoCanDownload.CacheConnection = false;
            this.WhoCanDownload.CommandText = resources.GetString("WhoCanDownload.CommandText");
            this.WhoCanDownload.CommandTimeout = 30;
            this.WhoCanDownload.CommandType = System.Data.CommandType.Text;
            this.WhoCanDownload.DynamicTableName = false;
            this.WhoCanDownload.EEPAlias = "EIPHRSYS";
            this.WhoCanDownload.EncodingAfter = null;
            this.WhoCanDownload.EncodingBefore = "Windows-1252";
            this.WhoCanDownload.EncodingConvert = null;
            this.WhoCanDownload.InfoConnection = this.infoConnection2;
            this.WhoCanDownload.MultiSetWhere = false;
            this.WhoCanDownload.Name = "WhoCanDownload";
            this.WhoCanDownload.NotificationAutoEnlist = false;
            this.WhoCanDownload.SecExcept = null;
            this.WhoCanDownload.SecFieldName = null;
            this.WhoCanDownload.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.WhoCanDownload.SelectPaging = false;
            this.WhoCanDownload.SelectTop = 0;
            this.WhoCanDownload.SiteControl = false;
            this.WhoCanDownload.SiteFieldName = null;
            this.WhoCanDownload.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // WhoCanDownload_User
            // 
            this.WhoCanDownload_User.CacheConnection = false;
            this.WhoCanDownload_User.CommandText = "SELECT USERID,USERNAME\r\n  FROM USERS\r\n  where [DESCRIPTION]=\'JB\'\r\n  order by USER" +
    "ID";
            this.WhoCanDownload_User.CommandTimeout = 30;
            this.WhoCanDownload_User.CommandType = System.Data.CommandType.Text;
            this.WhoCanDownload_User.DynamicTableName = false;
            this.WhoCanDownload_User.EEPAlias = "EIPHRSYS";
            this.WhoCanDownload_User.EncodingAfter = null;
            this.WhoCanDownload_User.EncodingBefore = "Windows-1252";
            this.WhoCanDownload_User.EncodingConvert = null;
            this.WhoCanDownload_User.InfoConnection = this.infoConnection2;
            this.WhoCanDownload_User.MultiSetWhere = false;
            this.WhoCanDownload_User.Name = "WhoCanDownload_User";
            this.WhoCanDownload_User.NotificationAutoEnlist = false;
            this.WhoCanDownload_User.SecExcept = null;
            this.WhoCanDownload_User.SecFieldName = null;
            this.WhoCanDownload_User.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.WhoCanDownload_User.SelectPaging = false;
            this.WhoCanDownload_User.SelectTop = 0;
            this.WhoCanDownload_User.SiteControl = false;
            this.WhoCanDownload_User.SiteFieldName = null;
            this.WhoCanDownload_User.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ISODocumentForm
            // 
            this.ISODocumentForm.CacheConnection = false;
            this.ISODocumentForm.CommandText = "SELECT * FROM dbo.[ISODocumentForm] order by FormNO desc";
            this.ISODocumentForm.CommandTimeout = 30;
            this.ISODocumentForm.CommandType = System.Data.CommandType.Text;
            this.ISODocumentForm.DynamicTableName = false;
            this.ISODocumentForm.EEPAlias = null;
            this.ISODocumentForm.EncodingAfter = null;
            this.ISODocumentForm.EncodingBefore = "Windows-1252";
            this.ISODocumentForm.EncodingConvert = null;
            this.ISODocumentForm.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "DocNO";
            keyItem5.KeyName = "FormNO";
            this.ISODocumentForm.KeyFields.Add(keyItem4);
            this.ISODocumentForm.KeyFields.Add(keyItem5);
            this.ISODocumentForm.MultiSetWhere = false;
            this.ISODocumentForm.Name = "ISODocumentForm";
            this.ISODocumentForm.NotificationAutoEnlist = false;
            this.ISODocumentForm.SecExcept = null;
            this.ISODocumentForm.SecFieldName = "";
            this.ISODocumentForm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ISODocumentForm.SelectPaging = false;
            this.ISODocumentForm.SelectTop = 0;
            this.ISODocumentForm.SiteControl = false;
            this.ISODocumentForm.SiteFieldName = null;
            this.ISODocumentForm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucISODocumentForm
            // 
            this.ucISODocumentForm.AutoTrans = true;
            this.ucISODocumentForm.ExceptJoin = false;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "DocNO";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "FormNO";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CreateBy";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = "_usercode";
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "CreateDate";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = "_sysdate";
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "Remark";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            this.ucISODocumentForm.FieldAttrs.Add(fieldAttr19);
            this.ucISODocumentForm.FieldAttrs.Add(fieldAttr20);
            this.ucISODocumentForm.FieldAttrs.Add(fieldAttr21);
            this.ucISODocumentForm.FieldAttrs.Add(fieldAttr22);
            this.ucISODocumentForm.FieldAttrs.Add(fieldAttr23);
            this.ucISODocumentForm.LogInfo = null;
            this.ucISODocumentForm.Name = "ucISODocumentForm";
            this.ucISODocumentForm.RowAffectsCheck = true;
            this.ucISODocumentForm.SelectCmd = this.ISODocumentForm;
            this.ucISODocumentForm.SelectCmdForUpdate = null;
            this.ucISODocumentForm.SendSQLCmd = true;
            this.ucISODocumentForm.ServerModify = true;
            this.ucISODocumentForm.ServerModifyGetMax = false;
            this.ucISODocumentForm.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucISODocumentForm.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucISODocumentForm.UseTranscationScope = false;
            this.ucISODocumentForm.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucISODocumentForm.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucISODocumentForm_BeforeInsert);
            // 
            // OrgCanDownload
            // 
            this.OrgCanDownload.CacheConnection = false;
            this.OrgCanDownload.CommandText = resources.GetString("OrgCanDownload.CommandText");
            this.OrgCanDownload.CommandTimeout = 30;
            this.OrgCanDownload.CommandType = System.Data.CommandType.Text;
            this.OrgCanDownload.DynamicTableName = false;
            this.OrgCanDownload.EEPAlias = "EIPHRSYS";
            this.OrgCanDownload.EncodingAfter = null;
            this.OrgCanDownload.EncodingBefore = "Windows-1252";
            this.OrgCanDownload.EncodingConvert = null;
            this.OrgCanDownload.InfoConnection = this.infoConnection2;
            this.OrgCanDownload.MultiSetWhere = false;
            this.OrgCanDownload.Name = "OrgCanDownload";
            this.OrgCanDownload.NotificationAutoEnlist = false;
            this.OrgCanDownload.SecExcept = null;
            this.OrgCanDownload.SecFieldName = null;
            this.OrgCanDownload.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.OrgCanDownload.SelectPaging = false;
            this.OrgCanDownload.SelectTop = 0;
            this.OrgCanDownload.SiteControl = false;
            this.OrgCanDownload.SiteFieldName = null;
            this.OrgCanDownload.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISODocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ISODocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISODocProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISOFirstNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USERS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISOSecondNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlowFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhoCanDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhoCanDownload_User)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ISODocumentForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgCanDownload)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ISODocument;
        private Srvtools.UpdateComponent ucISODocument;
        private Srvtools.InfoCommand View_ISODocument;
        private Srvtools.InfoCommand ISODocProperty;
        private Srvtools.InfoCommand ISOFirstNO;
        private Srvtools.InfoCommand USERS;
        private Srvtools.InfoCommand ISOSecondNO;
        private Srvtools.AutoNumber DocNO;
        private Srvtools.InfoCommand FlowFlag;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand WhoCanDownload;
        private Srvtools.InfoCommand WhoCanDownload_User;
        private Srvtools.InfoCommand ISODocumentForm;
        private Srvtools.UpdateComponent ucISODocumentForm;
        private Srvtools.InfoCommand OrgCanDownload;
    }
}
