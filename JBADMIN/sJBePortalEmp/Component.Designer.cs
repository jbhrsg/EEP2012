namespace sJBePortalEmp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.EmpBase = new Srvtools.InfoCommand(this.components);
            this.ucEmpBase = new Srvtools.UpdateComponent(this.components);
            this.infoDept = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDept)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "checkEmpNumCount";
            service1.NonLogin = false;
            service1.ServiceName = "checkEmpNumCount";
            service2.DelegateName = "checkAccountCount";
            service2.NonLogin = false;
            service2.ServiceName = "checkAccountCount";
            service3.DelegateName = "EmpBaseModify";
            service3.NonLogin = false;
            service3.ServiceName = "EmpBaseModify";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBePortal";
            // 
            // EmpBase
            // 
            this.EmpBase.CacheConnection = false;
            this.EmpBase.CommandText = resources.GetString("EmpBase.CommandText");
            this.EmpBase.CommandTimeout = 30;
            this.EmpBase.CommandType = System.Data.CommandType.Text;
            this.EmpBase.DynamicTableName = false;
            this.EmpBase.EEPAlias = "JBePortal";
            this.EmpBase.EncodingAfter = null;
            this.EmpBase.EncodingBefore = "Windows-1252";
            this.EmpBase.EncodingConvert = null;
            this.EmpBase.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "EmpNum";
            this.EmpBase.KeyFields.Add(keyItem1);
            this.EmpBase.MultiSetWhere = false;
            this.EmpBase.Name = "EmpBase";
            this.EmpBase.NotificationAutoEnlist = false;
            this.EmpBase.SecExcept = null;
            this.EmpBase.SecFieldName = null;
            this.EmpBase.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.EmpBase.SelectPaging = false;
            this.EmpBase.SelectTop = 0;
            this.EmpBase.SiteControl = false;
            this.EmpBase.SiteFieldName = null;
            this.EmpBase.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucEmpBase
            // 
            this.ucEmpBase.AutoTrans = true;
            this.ucEmpBase.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "BankID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "BankNO";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "BankBranchNO";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "BankName";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "IsRemit";
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
            this.ucEmpBase.FieldAttrs.Add(fieldAttr1);
            this.ucEmpBase.FieldAttrs.Add(fieldAttr2);
            this.ucEmpBase.FieldAttrs.Add(fieldAttr3);
            this.ucEmpBase.FieldAttrs.Add(fieldAttr4);
            this.ucEmpBase.FieldAttrs.Add(fieldAttr5);
            this.ucEmpBase.FieldAttrs.Add(fieldAttr6);
            this.ucEmpBase.FieldAttrs.Add(fieldAttr7);
            this.ucEmpBase.LogInfo = null;
            this.ucEmpBase.Name = "ucEmpBase";
            this.ucEmpBase.RowAffectsCheck = true;
            this.ucEmpBase.SelectCmd = this.EmpBase;
            this.ucEmpBase.SelectCmdForUpdate = null;
            this.ucEmpBase.SendSQLCmd = true;
            this.ucEmpBase.ServerModify = true;
            this.ucEmpBase.ServerModifyGetMax = false;
            this.ucEmpBase.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucEmpBase.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucEmpBase.UseTranscationScope = false;
            this.ucEmpBase.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucEmpBase.AfterInsert += new Srvtools.UpdateComponentAfterInsertEventHandler(this.ucEmpBase_AfterInsert);
            this.ucEmpBase.AfterModify += new Srvtools.UpdateComponentAfterModifyEventHandler(this.ucEmpBase_AfterModify);
            // 
            // infoDept
            // 
            this.infoDept.CacheConnection = false;
            this.infoDept.CommandText = "select * from Dept\r\norder by  DeptName";
            this.infoDept.CommandTimeout = 30;
            this.infoDept.CommandType = System.Data.CommandType.Text;
            this.infoDept.DynamicTableName = false;
            this.infoDept.EEPAlias = "JBePortal";
            this.infoDept.EncodingAfter = null;
            this.infoDept.EncodingBefore = "Windows-1252";
            this.infoDept.EncodingConvert = null;
            this.infoDept.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SeqID";
            keyItem3.KeyName = "DeptCode";
            this.infoDept.KeyFields.Add(keyItem2);
            this.infoDept.KeyFields.Add(keyItem3);
            this.infoDept.MultiSetWhere = false;
            this.infoDept.Name = "infoDept";
            this.infoDept.NotificationAutoEnlist = false;
            this.infoDept.SecExcept = null;
            this.infoDept.SecFieldName = null;
            this.infoDept.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoDept.SelectPaging = false;
            this.infoDept.SelectTop = 0;
            this.infoDept.SiteControl = false;
            this.infoDept.SiteFieldName = null;
            this.infoDept.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmpBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoDept)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand EmpBase;
        private Srvtools.UpdateComponent ucEmpBase;
        private Srvtools.InfoCommand infoDept;
    }
}
