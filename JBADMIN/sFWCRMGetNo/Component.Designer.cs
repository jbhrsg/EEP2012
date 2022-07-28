namespace sFWCRMGetNo
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
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.FWCRMWorkNo = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMWorkNo = new Srvtools.UpdateComponent(this.components);
            this.FWCRMIndateNo = new Srvtools.InfoCommand(this.components);
            this.ucFWCRMIndateNo = new Srvtools.UpdateComponent(this.components);
            this.autoWorkNo = new Srvtools.AutoNumber(this.components);
            this.infoEmployerID = new Srvtools.InfoCommand(this.components);
            this.infoOrderNo = new Srvtools.InfoCommand(this.components);
            this.autoIndateNo = new Srvtools.AutoNumber(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMWorkNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMIndateNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmployerID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoOrderNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "ReportWorkNo";
            service1.NonLogin = false;
            service1.ServiceName = "ReportWorkNo";
            service2.DelegateName = "ReportIndateNo";
            service2.NonLogin = false;
            service2.ServiceName = "ReportIndateNo";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // FWCRMWorkNo
            // 
            this.FWCRMWorkNo.CacheConnection = false;
            this.FWCRMWorkNo.CommandText = "SELECT f.*,r.EmployerName\r\nFROM dbo.[FWCRMWorkNo] f\r\n\tinner join View_FWCRMEmploy" +
    "er r on f.EmployerID=r.EmployerID\r\norder by f.WorkNo desc,r.EmployerName";
            this.FWCRMWorkNo.CommandTimeout = 30;
            this.FWCRMWorkNo.CommandType = System.Data.CommandType.Text;
            this.FWCRMWorkNo.DynamicTableName = false;
            this.FWCRMWorkNo.EEPAlias = null;
            this.FWCRMWorkNo.EncodingAfter = null;
            this.FWCRMWorkNo.EncodingBefore = "Windows-1252";
            this.FWCRMWorkNo.EncodingConvert = null;
            this.FWCRMWorkNo.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "WorkNo";
            this.FWCRMWorkNo.KeyFields.Add(keyItem1);
            this.FWCRMWorkNo.MultiSetWhere = false;
            this.FWCRMWorkNo.Name = "FWCRMWorkNo";
            this.FWCRMWorkNo.NotificationAutoEnlist = false;
            this.FWCRMWorkNo.SecExcept = null;
            this.FWCRMWorkNo.SecFieldName = null;
            this.FWCRMWorkNo.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMWorkNo.SelectPaging = false;
            this.FWCRMWorkNo.SelectTop = 0;
            this.FWCRMWorkNo.SiteControl = false;
            this.FWCRMWorkNo.SiteFieldName = null;
            this.FWCRMWorkNo.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMWorkNo
            // 
            this.ucFWCRMWorkNo.AutoTrans = true;
            this.ucFWCRMWorkNo.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "WorkNo";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "EmployerID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "CreateBy";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "CreateDate";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            this.ucFWCRMWorkNo.FieldAttrs.Add(fieldAttr1);
            this.ucFWCRMWorkNo.FieldAttrs.Add(fieldAttr2);
            this.ucFWCRMWorkNo.FieldAttrs.Add(fieldAttr3);
            this.ucFWCRMWorkNo.FieldAttrs.Add(fieldAttr4);
            this.ucFWCRMWorkNo.LogInfo = null;
            this.ucFWCRMWorkNo.Name = "ucFWCRMWorkNo";
            this.ucFWCRMWorkNo.RowAffectsCheck = true;
            this.ucFWCRMWorkNo.SelectCmd = this.FWCRMWorkNo;
            this.ucFWCRMWorkNo.SelectCmdForUpdate = null;
            this.ucFWCRMWorkNo.SendSQLCmd = true;
            this.ucFWCRMWorkNo.ServerModify = true;
            this.ucFWCRMWorkNo.ServerModifyGetMax = false;
            this.ucFWCRMWorkNo.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMWorkNo.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMWorkNo.UseTranscationScope = false;
            this.ucFWCRMWorkNo.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucFWCRMWorkNo.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucFWCRMWorkNo_BeforeInsert);
            // 
            // FWCRMIndateNo
            // 
            this.FWCRMIndateNo.CacheConnection = false;
            this.FWCRMIndateNo.CommandText = "SELECT f.*,r.EmployerName FROM dbo.[FWCRMIndateNo] f\r\n\tinner join FWCRMOrders o o" +
    "n f.OrderNo=o.OrderNo\r\n\tinner join View_FWCRMEmployer r on o.EmployerID=r.Employ" +
    "erID\r\norder by IndateNo desc\t";
            this.FWCRMIndateNo.CommandTimeout = 30;
            this.FWCRMIndateNo.CommandType = System.Data.CommandType.Text;
            this.FWCRMIndateNo.DynamicTableName = false;
            this.FWCRMIndateNo.EEPAlias = null;
            this.FWCRMIndateNo.EncodingAfter = null;
            this.FWCRMIndateNo.EncodingBefore = "Windows-1252";
            this.FWCRMIndateNo.EncodingConvert = null;
            this.FWCRMIndateNo.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "IndateNo";
            this.FWCRMIndateNo.KeyFields.Add(keyItem2);
            this.FWCRMIndateNo.MultiSetWhere = false;
            this.FWCRMIndateNo.Name = "FWCRMIndateNo";
            this.FWCRMIndateNo.NotificationAutoEnlist = false;
            this.FWCRMIndateNo.SecExcept = null;
            this.FWCRMIndateNo.SecFieldName = null;
            this.FWCRMIndateNo.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.FWCRMIndateNo.SelectPaging = false;
            this.FWCRMIndateNo.SelectTop = 0;
            this.FWCRMIndateNo.SiteControl = false;
            this.FWCRMIndateNo.SiteFieldName = null;
            this.FWCRMIndateNo.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucFWCRMIndateNo
            // 
            this.ucFWCRMIndateNo.AutoTrans = true;
            this.ucFWCRMIndateNo.ExceptJoin = false;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "IndateNo";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "OrderNo";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "CreateBy";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "CreateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucFWCRMIndateNo.FieldAttrs.Add(fieldAttr5);
            this.ucFWCRMIndateNo.FieldAttrs.Add(fieldAttr6);
            this.ucFWCRMIndateNo.FieldAttrs.Add(fieldAttr7);
            this.ucFWCRMIndateNo.FieldAttrs.Add(fieldAttr8);
            this.ucFWCRMIndateNo.LogInfo = null;
            this.ucFWCRMIndateNo.Name = "ucFWCRMIndateNo";
            this.ucFWCRMIndateNo.RowAffectsCheck = true;
            this.ucFWCRMIndateNo.SelectCmd = this.FWCRMIndateNo;
            this.ucFWCRMIndateNo.SelectCmdForUpdate = null;
            this.ucFWCRMIndateNo.SendSQLCmd = true;
            this.ucFWCRMIndateNo.ServerModify = true;
            this.ucFWCRMIndateNo.ServerModifyGetMax = false;
            this.ucFWCRMIndateNo.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucFWCRMIndateNo.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucFWCRMIndateNo.UseTranscationScope = false;
            this.ucFWCRMIndateNo.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucFWCRMIndateNo.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucFWCRMIndateNo_BeforeInsert);
            // 
            // autoWorkNo
            // 
            this.autoWorkNo.Active = true;
            this.autoWorkNo.AutoNoID = "WorkNo";
            this.autoWorkNo.Description = null;
            this.autoWorkNo.GetFixed = "WorkNoFixed()";
            this.autoWorkNo.isNumFill = false;
            this.autoWorkNo.Name = "autoWorkNo";
            this.autoWorkNo.Number = null;
            this.autoWorkNo.NumDig = 4;
            this.autoWorkNo.OldVersion = false;
            this.autoWorkNo.OverFlow = true;
            this.autoWorkNo.StartValue = 1;
            this.autoWorkNo.Step = 1;
            this.autoWorkNo.TargetColumn = "WorkNo";
            this.autoWorkNo.UpdateComp = this.ucFWCRMWorkNo;
            // 
            // infoEmployerID
            // 
            this.infoEmployerID.CacheConnection = false;
            this.infoEmployerID.CommandText = "select EmployerID,EmployerName from Employer\r\norder by EmployerName ";
            this.infoEmployerID.CommandTimeout = 30;
            this.infoEmployerID.CommandType = System.Data.CommandType.Text;
            this.infoEmployerID.DynamicTableName = false;
            this.infoEmployerID.EEPAlias = "FWCRM";
            this.infoEmployerID.EncodingAfter = null;
            this.infoEmployerID.EncodingBefore = "Windows-1252";
            this.infoEmployerID.EncodingConvert = null;
            this.infoEmployerID.InfoConnection = this.infoConnection2;
            this.infoEmployerID.MultiSetWhere = false;
            this.infoEmployerID.Name = "infoEmployerID";
            this.infoEmployerID.NotificationAutoEnlist = false;
            this.infoEmployerID.SecExcept = null;
            this.infoEmployerID.SecFieldName = null;
            this.infoEmployerID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoEmployerID.SelectPaging = false;
            this.infoEmployerID.SelectTop = 0;
            this.infoEmployerID.SiteControl = false;
            this.infoEmployerID.SiteFieldName = null;
            this.infoEmployerID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoOrderNo
            // 
            this.infoOrderNo.CacheConnection = false;
            this.infoOrderNo.CommandText = "select OrderNo,r.EmployerName from FWCRMOrders f\r\n\tinner join dbo.View_FWCRMEmplo" +
    "yer r on f.EmployerID=r.EmployerID\r\norder by OrderNo desc";
            this.infoOrderNo.CommandTimeout = 30;
            this.infoOrderNo.CommandType = System.Data.CommandType.Text;
            this.infoOrderNo.DynamicTableName = false;
            this.infoOrderNo.EEPAlias = null;
            this.infoOrderNo.EncodingAfter = null;
            this.infoOrderNo.EncodingBefore = "Windows-1252";
            this.infoOrderNo.EncodingConvert = null;
            this.infoOrderNo.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "OrderNo";
            this.infoOrderNo.KeyFields.Add(keyItem3);
            this.infoOrderNo.MultiSetWhere = false;
            this.infoOrderNo.Name = "infoOrderNo";
            this.infoOrderNo.NotificationAutoEnlist = false;
            this.infoOrderNo.SecExcept = null;
            this.infoOrderNo.SecFieldName = null;
            this.infoOrderNo.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoOrderNo.SelectPaging = false;
            this.infoOrderNo.SelectTop = 0;
            this.infoOrderNo.SiteControl = false;
            this.infoOrderNo.SiteFieldName = null;
            this.infoOrderNo.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoIndateNo
            // 
            this.autoIndateNo.Active = true;
            this.autoIndateNo.AutoNoID = "IndateNo";
            this.autoIndateNo.Description = null;
            this.autoIndateNo.GetFixed = "IndateNoFixed()";
            this.autoIndateNo.isNumFill = false;
            this.autoIndateNo.Name = "autoIndateNo";
            this.autoIndateNo.Number = null;
            this.autoIndateNo.NumDig = 4;
            this.autoIndateNo.OldVersion = false;
            this.autoIndateNo.OverFlow = true;
            this.autoIndateNo.StartValue = 1;
            this.autoIndateNo.Step = 1;
            this.autoIndateNo.TargetColumn = "IndateNo";
            this.autoIndateNo.UpdateComp = this.ucFWCRMIndateNo;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "FWCRM";
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMWorkNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FWCRMIndateNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmployerID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoOrderNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand FWCRMWorkNo;
        private Srvtools.UpdateComponent ucFWCRMWorkNo;
        private Srvtools.InfoCommand FWCRMIndateNo;
        private Srvtools.UpdateComponent ucFWCRMIndateNo;
        private Srvtools.AutoNumber autoWorkNo;
        private Srvtools.InfoCommand infoEmployerID;
        private Srvtools.InfoCommand infoOrderNo;
        private Srvtools.AutoNumber autoIndateNo;
        private Srvtools.InfoConnection infoConnection2;
    }
}
