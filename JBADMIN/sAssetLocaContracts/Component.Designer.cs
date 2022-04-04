namespace sAssetLocaContracts
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.AssetLocaContracts = new Srvtools.InfoCommand(this.components);
            this.ucAssetLocaContracts = new Srvtools.UpdateComponent(this.components);
            this.View_AssetLocaContracts = new Srvtools.InfoCommand(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLocaContracts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetLocaContracts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // AssetLocaContracts
            // 
            this.AssetLocaContracts.CacheConnection = false;
            this.AssetLocaContracts.CommandText = "SELECT dbo.[AssetLocaContracts].* FROM dbo.[AssetLocaContracts]";
            this.AssetLocaContracts.CommandTimeout = 30;
            this.AssetLocaContracts.CommandType = System.Data.CommandType.Text;
            this.AssetLocaContracts.DynamicTableName = false;
            this.AssetLocaContracts.EEPAlias = null;
            this.AssetLocaContracts.EncodingAfter = null;
            this.AssetLocaContracts.EncodingBefore = "Windows-1252";
            this.AssetLocaContracts.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "LocaContNO";
            this.AssetLocaContracts.KeyFields.Add(keyItem1);
            this.AssetLocaContracts.MultiSetWhere = false;
            this.AssetLocaContracts.Name = "AssetLocaContracts";
            this.AssetLocaContracts.NotificationAutoEnlist = false;
            this.AssetLocaContracts.SecExcept = null;
            this.AssetLocaContracts.SecFieldName = null;
            this.AssetLocaContracts.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.AssetLocaContracts.SelectPaging = false;
            this.AssetLocaContracts.SelectTop = 0;
            this.AssetLocaContracts.SiteControl = false;
            this.AssetLocaContracts.SiteFieldName = null;
            this.AssetLocaContracts.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucAssetLocaContracts
            // 
            this.ucAssetLocaContracts.AutoTrans = true;
            this.ucAssetLocaContracts.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "LocaContNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "LocaContID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "LocaContName";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "LocaContStdDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "LocaContEndDate";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "LocaContOwner";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LocaContOwnerTel";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LocaContAmt";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "LocaContNotes";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "CreateBy";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr10.DefaultValue = "_username";
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
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "LastUpdateBy";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr12.DefaultValue = "_username";
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateDate";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr1);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr2);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr3);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr4);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr5);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr6);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr7);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr8);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr9);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr10);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr11);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr12);
            this.ucAssetLocaContracts.FieldAttrs.Add(fieldAttr13);
            this.ucAssetLocaContracts.LogInfo = null;
            this.ucAssetLocaContracts.Name = "ucAssetLocaContracts";
            this.ucAssetLocaContracts.RowAffectsCheck = true;
            this.ucAssetLocaContracts.SelectCmd = this.AssetLocaContracts;
            this.ucAssetLocaContracts.SelectCmdForUpdate = null;
            this.ucAssetLocaContracts.ServerModify = true;
            this.ucAssetLocaContracts.ServerModifyGetMax = false;
            this.ucAssetLocaContracts.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucAssetLocaContracts.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucAssetLocaContracts.UseTranscationScope = false;
            this.ucAssetLocaContracts.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucAssetLocaContracts.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucAssetLocaContracts_BeforeInsert);
            this.ucAssetLocaContracts.BeforeModify += new Srvtools.UpdateComponentBeforeModifyEventHandler(this.ucAssetLocaContracts_BeforeModify);
            // 
            // View_AssetLocaContracts
            // 
            this.View_AssetLocaContracts.CacheConnection = false;
            this.View_AssetLocaContracts.CommandText = "SELECT * FROM dbo.[AssetLocaContracts]";
            this.View_AssetLocaContracts.CommandTimeout = 30;
            this.View_AssetLocaContracts.CommandType = System.Data.CommandType.Text;
            this.View_AssetLocaContracts.DynamicTableName = false;
            this.View_AssetLocaContracts.EEPAlias = null;
            this.View_AssetLocaContracts.EncodingAfter = null;
            this.View_AssetLocaContracts.EncodingBefore = "Windows-1252";
            this.View_AssetLocaContracts.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "LocaContNO";
            this.View_AssetLocaContracts.KeyFields.Add(keyItem2);
            this.View_AssetLocaContracts.MultiSetWhere = false;
            this.View_AssetLocaContracts.Name = "View_AssetLocaContracts";
            this.View_AssetLocaContracts.NotificationAutoEnlist = false;
            this.View_AssetLocaContracts.SecExcept = null;
            this.View_AssetLocaContracts.SecFieldName = null;
            this.View_AssetLocaContracts.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_AssetLocaContracts.SelectPaging = false;
            this.View_AssetLocaContracts.SelectTop = 0;
            this.View_AssetLocaContracts.SiteControl = false;
            this.View_AssetLocaContracts.SiteFieldName = null;
            this.View_AssetLocaContracts.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = resources.GetString("Employee.CommandText");
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "EMPLOYEEID";
            this.Employee.KeyFields.Add(keyItem3);
            this.Employee.MultiSetWhere = false;
            this.Employee.Name = "Employee";
            this.Employee.NotificationAutoEnlist = false;
            this.Employee.SecExcept = null;
            this.Employee.SecFieldName = null;
            this.Employee.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Employee.SelectPaging = false;
            this.Employee.SelectTop = 0;
            this.Employee.SiteControl = false;
            this.Employee.SiteFieldName = null;
            this.Employee.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssetLocaContracts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_AssetLocaContracts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand AssetLocaContracts;
        private Srvtools.UpdateComponent ucAssetLocaContracts;
        private Srvtools.InfoCommand View_AssetLocaContracts;
        private Srvtools.InfoCommand Employee;
    }
}
