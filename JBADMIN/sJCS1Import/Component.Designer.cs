﻿namespace sJCS1Import
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
            Srvtools.FieldAttr fieldAttr21 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr22 = new Srvtools.FieldAttr();
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
            Srvtools.Service service1 = new Srvtools.Service();
            Srvtools.Service service2 = new Srvtools.Service();
            Srvtools.Service service3 = new Srvtools.Service();
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Roomer = new Srvtools.InfoCommand(this.components);
            this.ucRoomer = new Srvtools.UpdateComponent(this.components);
            this.ucRoomerUpdate = new Srvtools.UpdateComponent(this.components);
            this.TheServiceManager = new Srvtools.ServiceManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Roomer)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JCS1";
            // 
            // Roomer
            // 
            this.Roomer.CacheConnection = false;
            this.Roomer.CommandText = "SELECT *\r\nfrom RoomerImport";
            this.Roomer.CommandTimeout = 30;
            this.Roomer.CommandType = System.Data.CommandType.Text;
            this.Roomer.DynamicTableName = false;
            this.Roomer.EEPAlias = null;
            this.Roomer.EncodingAfter = null;
            this.Roomer.EncodingBefore = "Windows-1252";
            this.Roomer.EncodingConvert = null;
            this.Roomer.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "iAutokey";
            this.Roomer.KeyFields.Add(keyItem1);
            this.Roomer.MultiSetWhere = false;
            this.Roomer.Name = "Roomer";
            this.Roomer.NotificationAutoEnlist = false;
            this.Roomer.SecExcept = null;
            this.Roomer.SecFieldName = null;
            this.Roomer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Roomer.SelectPaging = false;
            this.Roomer.SelectTop = 0;
            this.Roomer.SiteControl = false;
            this.Roomer.SiteFieldName = null;
            this.Roomer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucRoomer
            // 
            this.ucRoomer.AutoTrans = true;
            this.ucRoomer.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
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
            fieldAttr3.DataField = "BalanceYear";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "VoucherType";
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
            fieldAttr8.DataField = "iMonth";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "BorrowAmt";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "LendAmt";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "BudgetAmt";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateBy";
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
            fieldAttr14.DataField = "LastUpdateBy";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "LastUpdateDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            this.ucRoomer.FieldAttrs.Add(fieldAttr1);
            this.ucRoomer.FieldAttrs.Add(fieldAttr2);
            this.ucRoomer.FieldAttrs.Add(fieldAttr3);
            this.ucRoomer.FieldAttrs.Add(fieldAttr4);
            this.ucRoomer.FieldAttrs.Add(fieldAttr5);
            this.ucRoomer.FieldAttrs.Add(fieldAttr6);
            this.ucRoomer.FieldAttrs.Add(fieldAttr7);
            this.ucRoomer.FieldAttrs.Add(fieldAttr8);
            this.ucRoomer.FieldAttrs.Add(fieldAttr9);
            this.ucRoomer.FieldAttrs.Add(fieldAttr10);
            this.ucRoomer.FieldAttrs.Add(fieldAttr11);
            this.ucRoomer.FieldAttrs.Add(fieldAttr12);
            this.ucRoomer.FieldAttrs.Add(fieldAttr13);
            this.ucRoomer.FieldAttrs.Add(fieldAttr14);
            this.ucRoomer.FieldAttrs.Add(fieldAttr15);
            this.ucRoomer.LogInfo = null;
            this.ucRoomer.Name = "ucRoomer";
            this.ucRoomer.RowAffectsCheck = true;
            this.ucRoomer.SelectCmd = this.Roomer;
            this.ucRoomer.SelectCmdForUpdate = null;
            this.ucRoomer.SendSQLCmd = true;
            this.ucRoomer.ServerModify = true;
            this.ucRoomer.ServerModifyGetMax = false;
            this.ucRoomer.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucRoomer.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucRoomer.UseTranscationScope = false;
            this.ucRoomer.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // ucRoomerUpdate
            // 
            this.ucRoomerUpdate.AutoTrans = true;
            this.ucRoomerUpdate.ExceptJoin = false;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "AutoKey";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CompanyID";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "VoucherID";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "VoucherNo";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "Item";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "BorrowLendType";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "Acno";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "SubAcno";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = null;
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "DescribeID";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = null;
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            fieldAttr25.CheckNull = false;
            fieldAttr25.DataField = "CostCenterID";
            fieldAttr25.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr25.DefaultValue = null;
            fieldAttr25.TrimLength = 0;
            fieldAttr25.UpdateEnable = true;
            fieldAttr25.WhereMode = true;
            fieldAttr26.CheckNull = false;
            fieldAttr26.DataField = "Describe";
            fieldAttr26.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr26.DefaultValue = null;
            fieldAttr26.TrimLength = 0;
            fieldAttr26.UpdateEnable = true;
            fieldAttr26.WhereMode = true;
            fieldAttr27.CheckNull = false;
            fieldAttr27.DataField = "Amt";
            fieldAttr27.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr27.DefaultValue = null;
            fieldAttr27.TrimLength = 0;
            fieldAttr27.UpdateEnable = true;
            fieldAttr27.WhereMode = true;
            fieldAttr28.CheckNull = false;
            fieldAttr28.DataField = "AmtShow";
            fieldAttr28.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr28.DefaultValue = null;
            fieldAttr28.TrimLength = 0;
            fieldAttr28.UpdateEnable = true;
            fieldAttr28.WhereMode = true;
            fieldAttr29.CheckNull = false;
            fieldAttr29.DataField = "CreateBy";
            fieldAttr29.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr29.DefaultValue = null;
            fieldAttr29.TrimLength = 0;
            fieldAttr29.UpdateEnable = true;
            fieldAttr29.WhereMode = true;
            fieldAttr30.CheckNull = false;
            fieldAttr30.DataField = "CreateDate";
            fieldAttr30.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr30.DefaultValue = null;
            fieldAttr30.TrimLength = 0;
            fieldAttr30.UpdateEnable = true;
            fieldAttr30.WhereMode = true;
            fieldAttr31.CheckNull = false;
            fieldAttr31.DataField = "LastUpdateBy";
            fieldAttr31.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr31.DefaultValue = null;
            fieldAttr31.TrimLength = 0;
            fieldAttr31.UpdateEnable = true;
            fieldAttr31.WhereMode = true;
            fieldAttr32.CheckNull = false;
            fieldAttr32.DataField = "LastUpdateDate";
            fieldAttr32.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr32.DefaultValue = null;
            fieldAttr32.TrimLength = 0;
            fieldAttr32.UpdateEnable = true;
            fieldAttr32.WhereMode = true;
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr16);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr17);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr18);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr19);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr20);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr21);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr22);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr23);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr24);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr25);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr26);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr27);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr28);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr29);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr30);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr31);
            this.ucRoomerUpdate.FieldAttrs.Add(fieldAttr32);
            this.ucRoomerUpdate.LogInfo = null;
            this.ucRoomerUpdate.Name = "ucRoomerUpdate";
            this.ucRoomerUpdate.RowAffectsCheck = true;
            this.ucRoomerUpdate.SelectCmd = this.Roomer;
            this.ucRoomerUpdate.SelectCmdForUpdate = null;
            this.ucRoomerUpdate.SendSQLCmd = true;
            this.ucRoomerUpdate.ServerModify = true;
            this.ucRoomerUpdate.ServerModifyGetMax = false;
            this.ucRoomerUpdate.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucRoomerUpdate.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucRoomerUpdate.UseTranscationScope = false;
            this.ucRoomerUpdate.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucRoomerUpdate.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucRoomerUpdate_BeforeInsert);
            // 
            // TheServiceManager
            // 
            service1.DelegateName = "DataValidate";
            service1.NonLogin = false;
            service1.ServiceName = "DataValidate";
            service2.DelegateName = "GetOldSetting";
            service2.NonLogin = false;
            service2.ServiceName = "GetOldSetting";
            service3.DelegateName = "ExcelFileImport";
            service3.NonLogin = false;
            service3.ServiceName = "ExcelFileImport";
            this.TheServiceManager.ServiceCollection.Add(service1);
            this.TheServiceManager.ServiceCollection.Add(service2);
            this.TheServiceManager.ServiceCollection.Add(service3);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Roomer)).EndInit();

        }

        #endregion

        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Roomer;
        private Srvtools.UpdateComponent ucRoomer;
        private Srvtools.UpdateComponent ucRoomerUpdate;
        private Srvtools.ServiceManager TheServiceManager;
    }
}
