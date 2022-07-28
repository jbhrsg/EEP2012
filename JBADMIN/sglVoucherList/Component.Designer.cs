﻿namespace sRglVoucherList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glVoucherList = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glVoucherList)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "ReportglVoucherList";
            service1.NonLogin = false;
            service1.ServiceName = "ReportglVoucherList";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBGL";
            // 
            // glVoucherList
            // 
            this.glVoucherList.CacheConnection = false;
            this.glVoucherList.CommandText = resources.GetString("glVoucherList.CommandText");
            this.glVoucherList.CommandTimeout = 30;
            this.glVoucherList.CommandType = System.Data.CommandType.Text;
            this.glVoucherList.DynamicTableName = false;
            this.glVoucherList.EEPAlias = null;
            this.glVoucherList.EncodingAfter = null;
            this.glVoucherList.EncodingBefore = "Windows-1252";
            this.glVoucherList.EncodingConvert = null;
            this.glVoucherList.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "VoucherNo";
            keyItem2.KeyName = "AutoKey";
            this.glVoucherList.KeyFields.Add(keyItem1);
            this.glVoucherList.KeyFields.Add(keyItem2);
            this.glVoucherList.MultiSetWhere = false;
            this.glVoucherList.Name = "glVoucherList";
            this.glVoucherList.NotificationAutoEnlist = false;
            this.glVoucherList.SecExcept = null;
            this.glVoucherList.SecFieldName = null;
            this.glVoucherList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glVoucherList.SelectPaging = false;
            this.glVoucherList.SelectTop = 0;
            this.glVoucherList.SiteControl = false;
            this.glVoucherList.SiteFieldName = null;
            this.glVoucherList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glVoucherList)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glVoucherList;
    }
}