﻿namespace sHRDSubscriber
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.SubscriberMail = new Srvtools.InfoCommand(this.components);
            this.ucSubscriberMail = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriberMail)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JB0800";
            // 
            // SubscriberMail
            // 
            this.SubscriberMail.CacheConnection = false;
            this.SubscriberMail.CommandText = "SELECT  *\r\n FROM JB0800.dbo.SubscriberMail\r\nwhere IsSubscriber=1 and MailAddress " +
    "not in (SELECT eMail From JB0800.DBO.BlockList)\r\n";
            this.SubscriberMail.CommandTimeout = 30;
            this.SubscriberMail.CommandType = System.Data.CommandType.Text;
            this.SubscriberMail.DynamicTableName = false;
            this.SubscriberMail.EEPAlias = "JB0800";
            this.SubscriberMail.EncodingAfter = null;
            this.SubscriberMail.EncodingBefore = "Windows-1252";
            this.SubscriberMail.EncodingConvert = null;
            this.SubscriberMail.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.SubscriberMail.KeyFields.Add(keyItem1);
            this.SubscriberMail.MultiSetWhere = false;
            this.SubscriberMail.Name = "SubscriberMail";
            this.SubscriberMail.NotificationAutoEnlist = false;
            this.SubscriberMail.SecExcept = null;
            this.SubscriberMail.SecFieldName = null;
            this.SubscriberMail.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SubscriberMail.SelectPaging = false;
            this.SubscriberMail.SelectTop = 0;
            this.SubscriberMail.SiteControl = false;
            this.SubscriberMail.SiteFieldName = null;
            this.SubscriberMail.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucSubscriberMail
            // 
            this.ucSubscriberMail.AutoTrans = true;
            this.ucSubscriberMail.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "MailAddress";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "DataFromDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "IsSubscriber";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "DataFrom";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = "_username";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CancelDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucSubscriberMail.FieldAttrs.Add(fieldAttr1);
            this.ucSubscriberMail.FieldAttrs.Add(fieldAttr2);
            this.ucSubscriberMail.FieldAttrs.Add(fieldAttr3);
            this.ucSubscriberMail.FieldAttrs.Add(fieldAttr4);
            this.ucSubscriberMail.FieldAttrs.Add(fieldAttr5);
            this.ucSubscriberMail.FieldAttrs.Add(fieldAttr6);
            this.ucSubscriberMail.LogInfo = null;
            this.ucSubscriberMail.Name = "ucBank";
            this.ucSubscriberMail.RowAffectsCheck = true;
            this.ucSubscriberMail.SelectCmd = this.SubscriberMail;
            this.ucSubscriberMail.SelectCmdForUpdate = null;
            this.ucSubscriberMail.SendSQLCmd = true;
            this.ucSubscriberMail.ServerModify = true;
            this.ucSubscriberMail.ServerModifyGetMax = false;
            this.ucSubscriberMail.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucSubscriberMail.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucSubscriberMail.UseTranscationScope = false;
            this.ucSubscriberMail.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucSubscriberMail.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucSubscriberMail_BeforeInsert);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriberMail)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand SubscriberMail;
        private Srvtools.UpdateComponent ucSubscriberMail;
    }
}
