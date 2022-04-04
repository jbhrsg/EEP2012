namespace sOutWorkMaster
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
            Srvtools.Service service4 = new Srvtools.Service();
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr10 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr11 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr12 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr13 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr14 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr15 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr16 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr17 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr18 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.OutWorkMaster = new Srvtools.InfoCommand(this.components);
            this.ucOutWorkMaster = new Srvtools.UpdateComponent(this.components);
            this.View_OutWorkMaster = new Srvtools.InfoCommand(this.components);
            this.autoOutWorkNO = new Srvtools.AutoNumber(this.components);
            this.Employee = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutWorkMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_OutWorkMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            // 
            // serviceManager1
            // 
            service4.DelegateName = "GetUserOrgNOs";
            service4.NonLogin = false;
            service4.ServiceName = "GetUserOrgNOs";
            service5.DelegateName = "PutIntoOutWorkDetails";
            service5.NonLogin = false;
            service5.ServiceName = "PutIntoOutWorkDetails";
            service6.DelegateName = "CheckOutWorkDateDul";
            service6.NonLogin = false;
            service6.ServiceName = "CheckOutWorkDateDul";
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // OutWorkMaster
            // 
            this.OutWorkMaster.CacheConnection = false;
            this.OutWorkMaster.CommandText = "SELECT dbo.[OutWorkMaster].* FROM dbo.[OutWorkMaster]";
            this.OutWorkMaster.CommandTimeout = 30;
            this.OutWorkMaster.CommandType = System.Data.CommandType.Text;
            this.OutWorkMaster.DynamicTableName = false;
            this.OutWorkMaster.EEPAlias = null;
            this.OutWorkMaster.EncodingAfter = null;
            this.OutWorkMaster.EncodingBefore = "Windows-1252";
            this.OutWorkMaster.EncodingConvert = null;
            this.OutWorkMaster.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "OutWorkNO";
            this.OutWorkMaster.KeyFields.Add(keyItem5);
            this.OutWorkMaster.MultiSetWhere = false;
            this.OutWorkMaster.Name = "OutWorkMaster";
            this.OutWorkMaster.NotificationAutoEnlist = false;
            this.OutWorkMaster.SecExcept = null;
            this.OutWorkMaster.SecFieldName = null;
            this.OutWorkMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.OutWorkMaster.SelectPaging = false;
            this.OutWorkMaster.SelectTop = 0;
            this.OutWorkMaster.SiteControl = false;
            this.OutWorkMaster.SiteFieldName = null;
            this.OutWorkMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucOutWorkMaster
            // 
            this.ucOutWorkMaster.AutoTrans = true;
            this.ucOutWorkMaster.ExceptJoin = false;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "OutWorkNO";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "ApplyEmpID";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "OWDateS";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "OWDateE";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "ApplyGist";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "ApplyOrg_NO";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "Org_NOParent";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "CreateBy";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "CreateDate";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr10);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr11);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr12);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr13);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr14);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr15);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr16);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr17);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr18);
            this.ucOutWorkMaster.LogInfo = null;
            this.ucOutWorkMaster.Name = "ucOutWorkMaster";
            this.ucOutWorkMaster.RowAffectsCheck = true;
            this.ucOutWorkMaster.SelectCmd = this.OutWorkMaster;
            this.ucOutWorkMaster.SelectCmdForUpdate = null;
            this.ucOutWorkMaster.SendSQLCmd = true;
            this.ucOutWorkMaster.ServerModify = true;
            this.ucOutWorkMaster.ServerModifyGetMax = false;
            this.ucOutWorkMaster.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucOutWorkMaster.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucOutWorkMaster.UseTranscationScope = false;
            this.ucOutWorkMaster.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_OutWorkMaster
            // 
            this.View_OutWorkMaster.CacheConnection = false;
            this.View_OutWorkMaster.CommandText = "SELECT * FROM dbo.[OutWorkMaster]";
            this.View_OutWorkMaster.CommandTimeout = 30;
            this.View_OutWorkMaster.CommandType = System.Data.CommandType.Text;
            this.View_OutWorkMaster.DynamicTableName = false;
            this.View_OutWorkMaster.EEPAlias = null;
            this.View_OutWorkMaster.EncodingAfter = null;
            this.View_OutWorkMaster.EncodingBefore = "Windows-1252";
            this.View_OutWorkMaster.EncodingConvert = null;
            this.View_OutWorkMaster.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "OutWorkNO";
            this.View_OutWorkMaster.KeyFields.Add(keyItem1);
            this.View_OutWorkMaster.MultiSetWhere = false;
            this.View_OutWorkMaster.Name = "View_OutWorkMaster";
            this.View_OutWorkMaster.NotificationAutoEnlist = false;
            this.View_OutWorkMaster.SecExcept = null;
            this.View_OutWorkMaster.SecFieldName = null;
            this.View_OutWorkMaster.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_OutWorkMaster.SelectPaging = false;
            this.View_OutWorkMaster.SelectTop = 0;
            this.View_OutWorkMaster.SiteControl = false;
            this.View_OutWorkMaster.SiteFieldName = null;
            this.View_OutWorkMaster.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoOutWorkNO
            // 
            this.autoOutWorkNO.Active = true;
            this.autoOutWorkNO.AutoNoID = "OutWorkNO";
            this.autoOutWorkNO.Description = null;
            this.autoOutWorkNO.GetFixed = "OW";
            this.autoOutWorkNO.isNumFill = false;
            this.autoOutWorkNO.Name = "autoOutWorkNO";
            this.autoOutWorkNO.Number = null;
            this.autoOutWorkNO.NumDig = 6;
            this.autoOutWorkNO.OldVersion = false;
            this.autoOutWorkNO.OverFlow = true;
            this.autoOutWorkNO.StartValue = 1;
            this.autoOutWorkNO.Step = 1;
            this.autoOutWorkNO.TargetColumn = "OutWorkNO";
            this.autoOutWorkNO.UpdateComp = this.ucOutWorkMaster;
            // 
            // Employee
            // 
            this.Employee.CacheConnection = false;
            this.Employee.CommandText = "SELECT  USERID,USERNAME\r\n  FROM EIPHRSYS.DBO.USERS WHERE DESCRIPTION=\'JB\'";
            this.Employee.CommandTimeout = 30;
            this.Employee.CommandType = System.Data.CommandType.Text;
            this.Employee.DynamicTableName = false;
            this.Employee.EEPAlias = null;
            this.Employee.EncodingAfter = null;
            this.Employee.EncodingBefore = "Windows-1252";
            this.Employee.EncodingConvert = null;
            this.Employee.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "USERID";
            this.Employee.KeyFields.Add(keyItem2);
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
            // 
            // Organization
            // 
            this.Organization.CacheConnection = false;
            this.Organization.CommandText = resources.GetString("Organization.CommandText");
            this.Organization.CommandTimeout = 30;
            this.Organization.CommandType = System.Data.CommandType.Text;
            this.Organization.DynamicTableName = false;
            this.Organization.EEPAlias = null;
            this.Organization.EncodingAfter = null;
            this.Organization.EncodingBefore = "Windows-1252";
            this.Organization.EncodingConvert = null;
            this.Organization.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem3);
            this.Organization.MultiSetWhere = false;
            this.Organization.Name = "Organization";
            this.Organization.NotificationAutoEnlist = false;
            this.Organization.SecExcept = null;
            this.Organization.SecFieldName = null;
            this.Organization.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Organization.SelectPaging = false;
            this.Organization.SelectTop = 0;
            this.Organization.SiteControl = false;
            this.Organization.SiteFieldName = null;
            this.Organization.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutWorkMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_OutWorkMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand OutWorkMaster;
        private Srvtools.UpdateComponent ucOutWorkMaster;
        private Srvtools.InfoCommand View_OutWorkMaster;
        private Srvtools.AutoNumber autoOutWorkNO;
        private Srvtools.InfoCommand Employee;
        private Srvtools.InfoCommand Organization;
    }
}
