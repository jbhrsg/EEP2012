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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
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
            service1.DelegateName = "GetUserOrgNOs";
            service1.NonLogin = false;
            service1.ServiceName = "GetUserOrgNOs";
            service2.DelegateName = "PutIntoOutWorkDetails";
            service2.NonLogin = false;
            service2.ServiceName = "PutIntoOutWorkDetails";
            service3.DelegateName = "CheckOutWorkDateDul";
            service3.NonLogin = false;
            service3.ServiceName = "CheckOutWorkDateDul";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
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
            keyItem1.KeyName = "OutWorkNO";
            this.OutWorkMaster.KeyFields.Add(keyItem1);
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
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "OutWorkNO";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "ApplyEmpID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "OWDateS";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "OWDateE";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "ApplyGist";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "ApplyOrg_NO";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "Org_NOParent";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateBy";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "CreateDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr1);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr2);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr3);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr4);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr5);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr6);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr7);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr8);
            this.ucOutWorkMaster.FieldAttrs.Add(fieldAttr9);
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
            keyItem2.KeyName = "OutWorkNO";
            this.View_OutWorkMaster.KeyFields.Add(keyItem2);
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
            this.autoOutWorkNO.NumDig = 4;
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
            keyItem3.KeyName = "USERID";
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
            keyItem4.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem4);
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
