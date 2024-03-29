﻿namespace FWCRMServiceRecordMaster
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.ColumnItem columnItem1 = new Srvtools.ColumnItem();
            Srvtools.ColumnItem columnItem2 = new Srvtools.ColumnItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMServiceRecordMaster = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMServiceRecordMaster = new Srvtools.UpdateComponent(this.components);
            this.FWCRMServiceRecordDetails = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMServiceRecordDetails = new Srvtools.UpdateComponent(this.components);
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails = new Srvtools.InfoDataSource(this.components);
            this.View_FWCRMServiceRecordMaster = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMServiceRecordMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMServiceRecordDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_FWCRMServiceRecordMaster)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMServiceRecordMaster
            // 
            this.FWCRMServiceRecordMaster.CacheConnection = false;
            this.FWCRMServiceRecordMaster.CommandText = "SELECT dbo.[FWCRMServiceRecordMaster].* FROM dbo.[FWCRMServiceRecordMaster]";
            this.FWCRMServiceRecordMaster.CommandTimeout = 30;
            this.FWCRMServiceRecordMaster.CommandType = System.Data.CommandType.Text;
            this.FWCRMServiceRecordMaster.DynamicTableName = false;
            this.FWCRMServiceRecordMaster.EEPAlias = null;
            this.FWCRMServiceRecordMaster.EncodingAfter = null;
            this.FWCRMServiceRecordMaster.EncodingBefore = "Windows-1252";
            this.FWCRMServiceRecordMaster.EncodingConvert = null;
            this.FWCRMServiceRecordMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "Autokey";
            this.FWCRMServiceRecordMaster.KeyFields.Add(keyItem1);
            this.FWCRMServiceRecordMaster.MultiSetWhere = false;
            this.FWCRMServiceRecordMaster.Name = "FWCRMServiceRecordMaster";
            this.FWCRMServiceRecordMaster.NotificationAutoEnlist = false;
            this.FWCRMServiceRecordMaster.SecExcept = null;
            this.FWCRMServiceRecordMaster.SecFieldName = null;
            this.FWCRMServiceRecordMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMServiceRecordMaster.SelectPaging = false;
            this.FWCRMServiceRecordMaster.SelectTop = 0;
            this.FWCRMServiceRecordMaster.SiteControl = false;
            this.FWCRMServiceRecordMaster.SiteFieldName = null;
            this.FWCRMServiceRecordMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMServiceRecordMaster
            // 
            this.ucFWCRMServiceRecordMaster.AutoTrans = true;
            this.ucFWCRMServiceRecordMaster.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "Autokey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CompanyID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EmployerID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "NationalityID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "Remark";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateBy";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateDate";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "LastUpdateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr6);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr7);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr8);
            this.ucFWCRMServiceRecordMaster.FieldAttrs.Add(fieldAttr9);
            this.ucFWCRMServiceRecordMaster.LogInfo = null;
            this.ucFWCRMServiceRecordMaster.Name = null;
            this.ucFWCRMServiceRecordMaster.RowAffectsCheck = true;
            this.ucFWCRMServiceRecordMaster.SelectCmd = this.FWCRMServiceRecordMaster;
            this.ucFWCRMServiceRecordMaster.SelectCmdForUpdate = null;
            this.ucFWCRMServiceRecordMaster.SendSQLCmd = true;
            this.ucFWCRMServiceRecordMaster.ServerModify = true;
            this.ucFWCRMServiceRecordMaster.ServerModifyGetMax = false;
            this.ucFWCRMServiceRecordMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMServiceRecordMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMServiceRecordMaster.UseTranscationScope = false;
            this.ucFWCRMServiceRecordMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // FWCRMServiceRecordDetails
            // 
            this.FWCRMServiceRecordDetails.CacheConnection = false;
            this.FWCRMServiceRecordDetails.CommandText = "SELECT dbo.[FWCRMServiceRecordDetails].* FROM dbo.[FWCRMServiceRecordDetails]";
            this.FWCRMServiceRecordDetails.CommandTimeout = 30;
            this.FWCRMServiceRecordDetails.CommandType = System.Data.CommandType.Text;
            this.FWCRMServiceRecordDetails.DynamicTableName = false;
            this.FWCRMServiceRecordDetails.EEPAlias = null;
            this.FWCRMServiceRecordDetails.EncodingAfter = null;
            this.FWCRMServiceRecordDetails.EncodingBefore = "Windows-1252";
            this.FWCRMServiceRecordDetails.EncodingConvert = null;
            this.FWCRMServiceRecordDetails.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "Autokey";
            this.FWCRMServiceRecordDetails.KeyFields.Add(keyItem2);
            this.FWCRMServiceRecordDetails.MultiSetWhere = false;
            this.FWCRMServiceRecordDetails.Name = "FWCRMServiceRecordDetails";
            this.FWCRMServiceRecordDetails.NotificationAutoEnlist = false;
            this.FWCRMServiceRecordDetails.SecExcept = null;
            this.FWCRMServiceRecordDetails.SecFieldName = null;
            this.FWCRMServiceRecordDetails.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMServiceRecordDetails.SelectPaging = false;
            this.FWCRMServiceRecordDetails.SelectTop = 0;
            this.FWCRMServiceRecordDetails.SiteControl = false;
            this.FWCRMServiceRecordDetails.SiteFieldName = null;
            this.FWCRMServiceRecordDetails.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMServiceRecordDetails
            // 
            this.ucFWCRMServiceRecordDetails.AutoTrans = true;
            this.ucFWCRMServiceRecordDetails.ExceptJoin = false;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Autokey";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "MasterKey";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "EmployeeID";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "CreateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "CreateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateBy";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "LastUpdateDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr10);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr11);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr12);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr13);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr14);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr15);
            this.ucFWCRMServiceRecordDetails.FieldAttrs.Add(fieldAttr16);
            this.ucFWCRMServiceRecordDetails.LogInfo = null;
            this.ucFWCRMServiceRecordDetails.Name = null;
            this.ucFWCRMServiceRecordDetails.RowAffectsCheck = true;
            this.ucFWCRMServiceRecordDetails.SelectCmd = this.FWCRMServiceRecordDetails;
            this.ucFWCRMServiceRecordDetails.SelectCmdForUpdate = null;
            this.ucFWCRMServiceRecordDetails.SendSQLCmd = true;
            this.ucFWCRMServiceRecordDetails.ServerModify = true;
            this.ucFWCRMServiceRecordDetails.ServerModifyGetMax = false;
            this.ucFWCRMServiceRecordDetails.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMServiceRecordDetails.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMServiceRecordDetails.UseTranscationScope = false;
            this.ucFWCRMServiceRecordDetails.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails
            // 
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.Detail = this.FWCRMServiceRecordDetails;
            columnItem1.FieldName = "MasterKey";
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.DetailColumns.Add(columnItem1);
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.DynamicTableName = false;
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.Master = this.FWCRMServiceRecordMaster;
            columnItem2.FieldName = "Autokey";
            this.idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails.MasterColumns.Add(columnItem2);
            // 
            // View_FWCRMServiceRecordMaster
            // 
            this.View_FWCRMServiceRecordMaster.CacheConnection = false;
            this.View_FWCRMServiceRecordMaster.CommandText = "SELECT * FROM dbo.[FWCRMServiceRecordMaster]";
            this.View_FWCRMServiceRecordMaster.CommandTimeout = 30;
            this.View_FWCRMServiceRecordMaster.CommandType = System.Data.CommandType.Text;
            this.View_FWCRMServiceRecordMaster.DynamicTableName = false;
            this.View_FWCRMServiceRecordMaster.EEPAlias = null;
            this.View_FWCRMServiceRecordMaster.EncodingAfter = null;
            this.View_FWCRMServiceRecordMaster.EncodingBefore = "Windows-1252";
            this.View_FWCRMServiceRecordMaster.EncodingConvert = null;
            this.View_FWCRMServiceRecordMaster.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "Autokey";
            this.View_FWCRMServiceRecordMaster.KeyFields.Add(keyItem3);
            this.View_FWCRMServiceRecordMaster.MultiSetWhere = false;
            this.View_FWCRMServiceRecordMaster.Name = "View_FWCRMServiceRecordMaster";
            this.View_FWCRMServiceRecordMaster.NotificationAutoEnlist = false;
            this.View_FWCRMServiceRecordMaster.SecExcept = null;
            this.View_FWCRMServiceRecordMaster.SecFieldName = null;
            this.View_FWCRMServiceRecordMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_FWCRMServiceRecordMaster.SelectPaging = false;
            this.View_FWCRMServiceRecordMaster.SelectTop = 0;
            this.View_FWCRMServiceRecordMaster.SiteControl = false;
            this.View_FWCRMServiceRecordMaster.SiteFieldName = null;
            this.View_FWCRMServiceRecordMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMServiceRecordMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMServiceRecordDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_FWCRMServiceRecordMaster)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMServiceRecordMaster;
        private Srvtools.UpdateComponent ucFWCRMServiceRecordMaster;
        private Srvtools.InfoCommand FWCRMServiceRecordDetails;
        private Srvtools.UpdateComponent ucFWCRMServiceRecordDetails;
        private Srvtools.InfoDataSource idFWCRMServiceRecordMaster_FWCRMServiceRecordDetails;
        private Srvtools.InfoCommand View_FWCRMServiceRecordMaster;
    }
}
