namespace sERPContractAlter
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.ERPContractAlter = new Srvtools.InfoCommand(this.components);
            this.ucERPContractAlter = new Srvtools.UpdateComponent(this.components);
            this.View_ERPContractAlter = new Srvtools.InfoCommand(this.components);
            this.ContractAlterNO = new Srvtools.AutoNumber(this.components);
            this.ContractNO = new Srvtools.InfoCommand(this.components);
            this.VenderCustomer = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContractAlter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContractAlter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VenderCustomer)).BeginInit();
            // 
            // serviceManager1
            // 
            service4.DelegateName = "ReturnGetFixed";
            service4.NonLogin = false;
            service4.ServiceName = "ReturnGetFixed";
            service5.DelegateName = "GetContractAlterNO";
            service5.NonLogin = false;
            service5.ServiceName = "GetContractAlterNO";
            service6.DelegateName = "UpdateERPContract";
            service6.NonLogin = false;
            service6.ServiceName = "UpdateERPContract";
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // ERPContractAlter
            // 
            this.ERPContractAlter.CacheConnection = false;
            this.ERPContractAlter.CommandText = "SELECT dbo.[ERPContractAlter].*,ERPContract.* FROM dbo.[ERPContractAlter]\r\nleft j" +
    "oin ERPContract on ERPContract.ContractNO=[ERPContractAlter].ContractNO\r\n\r\n";
            this.ERPContractAlter.CommandTimeout = 30;
            this.ERPContractAlter.CommandType = System.Data.CommandType.Text;
            this.ERPContractAlter.DynamicTableName = false;
            this.ERPContractAlter.EEPAlias = null;
            this.ERPContractAlter.EncodingAfter = null;
            this.ERPContractAlter.EncodingBefore = "Windows-1252";
            this.ERPContractAlter.EncodingConvert = null;
            this.ERPContractAlter.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ContractAlterNO";
            this.ERPContractAlter.KeyFields.Add(keyItem3);
            this.ERPContractAlter.MultiSetWhere = false;
            this.ERPContractAlter.Name = "ERPContractAlter";
            this.ERPContractAlter.NotificationAutoEnlist = false;
            this.ERPContractAlter.SecExcept = null;
            this.ERPContractAlter.SecFieldName = null;
            this.ERPContractAlter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPContractAlter.SelectPaging = false;
            this.ERPContractAlter.SelectTop = 0;
            this.ERPContractAlter.SiteControl = false;
            this.ERPContractAlter.SiteFieldName = null;
            this.ERPContractAlter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucERPContractAlter
            // 
            this.ucERPContractAlter.AutoTrans = true;
            this.ucERPContractAlter.ExceptJoin = false;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "ContractAlterNO";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "ContractNO";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "BeginDate";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "EndDate";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "PhysicalContractNO";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "RemindDays";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "GuarantyEndDate";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "IsForeignDept";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "AssignChecker";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            fieldAttr22.CheckNull = false;
            fieldAttr22.DataField = "Keeper";
            fieldAttr22.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr22.DefaultValue = null;
            fieldAttr22.TrimLength = 0;
            fieldAttr22.UpdateEnable = true;
            fieldAttr22.WhereMode = true;
            fieldAttr23.CheckNull = false;
            fieldAttr23.DataField = "CreateBy";
            fieldAttr23.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr23.DefaultValue = "_usercode";
            fieldAttr23.TrimLength = 0;
            fieldAttr23.UpdateEnable = true;
            fieldAttr23.WhereMode = true;
            fieldAttr24.CheckNull = false;
            fieldAttr24.DataField = "CreateDate";
            fieldAttr24.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr24.DefaultValue = "_today";
            fieldAttr24.TrimLength = 0;
            fieldAttr24.UpdateEnable = true;
            fieldAttr24.WhereMode = true;
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr13);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr14);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr15);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr16);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr17);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr18);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr19);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr20);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr21);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr22);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr23);
            this.ucERPContractAlter.FieldAttrs.Add(fieldAttr24);
            this.ucERPContractAlter.LogInfo = null;
            this.ucERPContractAlter.Name = "ucERPContractAlter";
            this.ucERPContractAlter.RowAffectsCheck = true;
            this.ucERPContractAlter.SelectCmd = this.ERPContractAlter;
            this.ucERPContractAlter.SelectCmdForUpdate = null;
            this.ucERPContractAlter.SendSQLCmd = true;
            this.ucERPContractAlter.ServerModify = true;
            this.ucERPContractAlter.ServerModifyGetMax = false;
            this.ucERPContractAlter.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucERPContractAlter.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucERPContractAlter.UseTranscationScope = false;
            this.ucERPContractAlter.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucERPContractAlter.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucERPContractAlter_BeforeInsert);
            // 
            // View_ERPContractAlter
            // 
            this.View_ERPContractAlter.CacheConnection = false;
            this.View_ERPContractAlter.CommandText = "SELECT * FROM dbo.[ERPContractAlter]";
            this.View_ERPContractAlter.CommandTimeout = 30;
            this.View_ERPContractAlter.CommandType = System.Data.CommandType.Text;
            this.View_ERPContractAlter.DynamicTableName = false;
            this.View_ERPContractAlter.EEPAlias = null;
            this.View_ERPContractAlter.EncodingAfter = null;
            this.View_ERPContractAlter.EncodingBefore = "Windows-1252";
            this.View_ERPContractAlter.EncodingConvert = null;
            this.View_ERPContractAlter.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ContractAlterNO";
            this.View_ERPContractAlter.KeyFields.Add(keyItem1);
            this.View_ERPContractAlter.MultiSetWhere = false;
            this.View_ERPContractAlter.Name = "View_ERPContractAlter";
            this.View_ERPContractAlter.NotificationAutoEnlist = false;
            this.View_ERPContractAlter.SecExcept = null;
            this.View_ERPContractAlter.SecFieldName = null;
            this.View_ERPContractAlter.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_ERPContractAlter.SelectPaging = false;
            this.View_ERPContractAlter.SelectTop = 0;
            this.View_ERPContractAlter.SiteControl = false;
            this.View_ERPContractAlter.SiteFieldName = null;
            this.View_ERPContractAlter.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ContractAlterNO
            // 
            this.ContractAlterNO.Active = true;
            this.ContractAlterNO.AutoNoID = "ContractAlterNO";
            this.ContractAlterNO.Description = null;
            this.ContractAlterNO.GetFixed = "ReturnGetFixed()";
            this.ContractAlterNO.isNumFill = false;
            this.ContractAlterNO.Name = "ContractAlterNO";
            this.ContractAlterNO.Number = null;
            this.ContractAlterNO.NumDig = 5;
            this.ContractAlterNO.OldVersion = false;
            this.ContractAlterNO.OverFlow = true;
            this.ContractAlterNO.StartValue = 1;
            this.ContractAlterNO.Step = 1;
            this.ContractAlterNO.TargetColumn = "";
            this.ContractAlterNO.UpdateComp = null;
            // 
            // ContractNO
            // 
            this.ContractNO.CacheConnection = false;
            this.ContractNO.CommandText = resources.GetString("ContractNO.CommandText");
            this.ContractNO.CommandTimeout = 30;
            this.ContractNO.CommandType = System.Data.CommandType.Text;
            this.ContractNO.DynamicTableName = false;
            this.ContractNO.EEPAlias = null;
            this.ContractNO.EncodingAfter = null;
            this.ContractNO.EncodingBefore = "Windows-1252";
            this.ContractNO.EncodingConvert = null;
            this.ContractNO.InfoConnection = this.InfoConnection1;
            this.ContractNO.MultiSetWhere = false;
            this.ContractNO.Name = "ContractNO";
            this.ContractNO.NotificationAutoEnlist = false;
            this.ContractNO.SecExcept = null;
            this.ContractNO.SecFieldName = null;
            this.ContractNO.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ContractNO.SelectPaging = false;
            this.ContractNO.SelectTop = 0;
            this.ContractNO.SiteControl = false;
            this.ContractNO.SiteFieldName = null;
            this.ContractNO.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // VenderCustomer
            // 
            this.VenderCustomer.CacheConnection = false;
            this.VenderCustomer.CommandText = resources.GetString("VenderCustomer.CommandText");
            this.VenderCustomer.CommandTimeout = 30;
            this.VenderCustomer.CommandType = System.Data.CommandType.Text;
            this.VenderCustomer.DynamicTableName = false;
            this.VenderCustomer.EEPAlias = null;
            this.VenderCustomer.EncodingAfter = null;
            this.VenderCustomer.EncodingBefore = "Windows-1252";
            this.VenderCustomer.EncodingConvert = null;
            this.VenderCustomer.InfoConnection = this.InfoConnection1;
            this.VenderCustomer.MultiSetWhere = false;
            this.VenderCustomer.Name = "VenderCustomer";
            this.VenderCustomer.NotificationAutoEnlist = false;
            this.VenderCustomer.SecExcept = null;
            this.VenderCustomer.SecFieldName = null;
            this.VenderCustomer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.VenderCustomer.SelectPaging = false;
            this.VenderCustomer.SelectTop = 0;
            this.VenderCustomer.SiteControl = false;
            this.VenderCustomer.SiteFieldName = null;
            this.VenderCustomer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPContractAlter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_ERPContractAlter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VenderCustomer)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand ERPContractAlter;
        private Srvtools.UpdateComponent ucERPContractAlter;
        private Srvtools.InfoCommand View_ERPContractAlter;
        private Srvtools.AutoNumber ContractAlterNO;
        private Srvtools.InfoCommand ContractNO;
        private Srvtools.InfoCommand VenderCustomer;
    }
}
