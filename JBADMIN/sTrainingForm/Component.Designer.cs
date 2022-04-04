namespace sTrainingForm
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem7 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.TrainingForm = new Srvtools.InfoCommand(this.components);
            this.ucTrainingForm = new Srvtools.UpdateComponent(this.components);
            this.View_TrainingForm = new Srvtools.InfoCommand(this.components);
            this.autoTrainingFormID = new Srvtools.AutoNumber(this.components);
            this.infoTrainType = new Srvtools.InfoCommand(this.components);
            this.infoTrainKind = new Srvtools.InfoCommand(this.components);
            this.infoRequisition = new Srvtools.InfoCommand(this.components);
            this.infoProofType = new Srvtools.InfoCommand(this.components);
            this.infoPayType = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_TrainingForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoTrainType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoTrainKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRequisition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoProofType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPayType)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "InsertRequisition";
            service1.NonLogin = false;
            service1.ServiceName = "InsertRequisition";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // TrainingForm
            // 
            this.TrainingForm.CacheConnection = false;
            this.TrainingForm.CommandText = "SELECT dbo.[TrainingForm].* FROM dbo.[TrainingForm]";
            this.TrainingForm.CommandTimeout = 30;
            this.TrainingForm.CommandType = System.Data.CommandType.Text;
            this.TrainingForm.DynamicTableName = false;
            this.TrainingForm.EEPAlias = null;
            this.TrainingForm.EncodingAfter = null;
            this.TrainingForm.EncodingBefore = "Windows-1252";
            this.TrainingForm.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "TrainingFormID";
            this.TrainingForm.KeyFields.Add(keyItem1);
            this.TrainingForm.MultiSetWhere = false;
            this.TrainingForm.Name = "TrainingForm";
            this.TrainingForm.NotificationAutoEnlist = false;
            this.TrainingForm.SecExcept = null;
            this.TrainingForm.SecFieldName = null;
            this.TrainingForm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.TrainingForm.SelectPaging = false;
            this.TrainingForm.SelectTop = 0;
            this.TrainingForm.SiteControl = false;
            this.TrainingForm.SiteFieldName = null;
            this.TrainingForm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucTrainingForm
            // 
            this.ucTrainingForm.AutoTrans = true;
            this.ucTrainingForm.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "TrainingFormID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CourseName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ApplyDate";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "TrainTypeID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "TrainKindID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "TrainEmpList";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "TrainOrg";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "TrainLocation";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "TrainStdDate";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "TrainEndDate";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "TrainHours";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = null;
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "TrainFee";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "TrainHeadCount";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = null;
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "OutLine";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            fieldAttr15.CheckNull = false;
            fieldAttr15.DataField = "PayTypeID";
            fieldAttr15.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr15.DefaultValue = null;
            fieldAttr15.TrimLength = 0;
            fieldAttr15.UpdateEnable = true;
            fieldAttr15.WhereMode = true;
            fieldAttr16.CheckNull = false;
            fieldAttr16.DataField = "ProofTypeID";
            fieldAttr16.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr16.DefaultValue = null;
            fieldAttr16.TrimLength = 0;
            fieldAttr16.UpdateEnable = true;
            fieldAttr16.WhereMode = true;
            fieldAttr17.CheckNull = false;
            fieldAttr17.DataField = "RequisitionNO";
            fieldAttr17.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr17.DefaultValue = null;
            fieldAttr17.TrimLength = 0;
            fieldAttr17.UpdateEnable = true;
            fieldAttr17.WhereMode = true;
            fieldAttr18.CheckNull = false;
            fieldAttr18.DataField = "ApplyNotes";
            fieldAttr18.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr18.DefaultValue = null;
            fieldAttr18.TrimLength = 0;
            fieldAttr18.UpdateEnable = true;
            fieldAttr18.WhereMode = true;
            fieldAttr19.CheckNull = false;
            fieldAttr19.DataField = "IsApplyHelp";
            fieldAttr19.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr19.DefaultValue = null;
            fieldAttr19.TrimLength = 0;
            fieldAttr19.UpdateEnable = true;
            fieldAttr19.WhereMode = true;
            fieldAttr20.CheckNull = false;
            fieldAttr20.DataField = "CreateBy";
            fieldAttr20.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr20.DefaultValue = null;
            fieldAttr20.TrimLength = 0;
            fieldAttr20.UpdateEnable = true;
            fieldAttr20.WhereMode = true;
            fieldAttr21.CheckNull = false;
            fieldAttr21.DataField = "CreateDate";
            fieldAttr21.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr21.DefaultValue = null;
            fieldAttr21.TrimLength = 0;
            fieldAttr21.UpdateEnable = true;
            fieldAttr21.WhereMode = true;
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr1);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr2);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr3);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr4);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr5);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr6);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr7);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr8);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr9);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr10);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr11);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr12);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr13);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr14);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr15);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr16);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr17);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr18);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr19);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr20);
            this.ucTrainingForm.FieldAttrs.Add(fieldAttr21);
            this.ucTrainingForm.LogInfo = null;
            this.ucTrainingForm.Name = "ucTrainingForm";
            this.ucTrainingForm.RowAffectsCheck = true;
            this.ucTrainingForm.SelectCmd = this.TrainingForm;
            this.ucTrainingForm.SelectCmdForUpdate = null;
            this.ucTrainingForm.ServerModify = true;
            this.ucTrainingForm.ServerModifyGetMax = false;
            this.ucTrainingForm.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucTrainingForm.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucTrainingForm.UseTranscationScope = false;
            this.ucTrainingForm.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_TrainingForm
            // 
            this.View_TrainingForm.CacheConnection = false;
            this.View_TrainingForm.CommandText = "SELECT * FROM dbo.[TrainingForm]";
            this.View_TrainingForm.CommandTimeout = 30;
            this.View_TrainingForm.CommandType = System.Data.CommandType.Text;
            this.View_TrainingForm.DynamicTableName = false;
            this.View_TrainingForm.EEPAlias = null;
            this.View_TrainingForm.EncodingAfter = null;
            this.View_TrainingForm.EncodingBefore = "Windows-1252";
            this.View_TrainingForm.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "TrainingFormID";
            this.View_TrainingForm.KeyFields.Add(keyItem2);
            this.View_TrainingForm.MultiSetWhere = false;
            this.View_TrainingForm.Name = "View_TrainingForm";
            this.View_TrainingForm.NotificationAutoEnlist = false;
            this.View_TrainingForm.SecExcept = null;
            this.View_TrainingForm.SecFieldName = null;
            this.View_TrainingForm.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_TrainingForm.SelectPaging = false;
            this.View_TrainingForm.SelectTop = 0;
            this.View_TrainingForm.SiteControl = false;
            this.View_TrainingForm.SiteFieldName = null;
            this.View_TrainingForm.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // autoTrainingFormID
            // 
            this.autoTrainingFormID.Active = true;
            this.autoTrainingFormID.AutoNoID = "TrainingFormID";
            this.autoTrainingFormID.Description = null;
            this.autoTrainingFormID.GetFixed = "GetTrainingIDFixed()";
            this.autoTrainingFormID.isNumFill = false;
            this.autoTrainingFormID.Name = "autoTrainingFormID";
            this.autoTrainingFormID.Number = null;
            this.autoTrainingFormID.NumDig = 3;
            this.autoTrainingFormID.OldVersion = false;
            this.autoTrainingFormID.OverFlow = true;
            this.autoTrainingFormID.StartValue = 1;
            this.autoTrainingFormID.Step = 1;
            this.autoTrainingFormID.TargetColumn = "TrainingFormID";
            this.autoTrainingFormID.UpdateComp = this.ucTrainingForm;
            // 
            // infoTrainType
            // 
            this.infoTrainType.CacheConnection = false;
            this.infoTrainType.CommandText = "SELECT dbo.[TrainType].* FROM dbo.[TrainType]";
            this.infoTrainType.CommandTimeout = 30;
            this.infoTrainType.CommandType = System.Data.CommandType.Text;
            this.infoTrainType.DynamicTableName = false;
            this.infoTrainType.EEPAlias = null;
            this.infoTrainType.EncodingAfter = null;
            this.infoTrainType.EncodingBefore = "Windows-1252";
            this.infoTrainType.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "TrainTypeID";
            this.infoTrainType.KeyFields.Add(keyItem3);
            this.infoTrainType.MultiSetWhere = false;
            this.infoTrainType.Name = "infoTrainType";
            this.infoTrainType.NotificationAutoEnlist = false;
            this.infoTrainType.SecExcept = null;
            this.infoTrainType.SecFieldName = null;
            this.infoTrainType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoTrainType.SelectPaging = false;
            this.infoTrainType.SelectTop = 0;
            this.infoTrainType.SiteControl = false;
            this.infoTrainType.SiteFieldName = null;
            this.infoTrainType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoTrainKind
            // 
            this.infoTrainKind.CacheConnection = false;
            this.infoTrainKind.CommandText = "SELECT dbo.[TrainKind].* FROM dbo.[TrainKind]";
            this.infoTrainKind.CommandTimeout = 30;
            this.infoTrainKind.CommandType = System.Data.CommandType.Text;
            this.infoTrainKind.DynamicTableName = false;
            this.infoTrainKind.EEPAlias = null;
            this.infoTrainKind.EncodingAfter = null;
            this.infoTrainKind.EncodingBefore = "Windows-1252";
            this.infoTrainKind.InfoConnection = this.InfoConnection1;
            keyItem4.KeyName = "TrainKindID";
            this.infoTrainKind.KeyFields.Add(keyItem4);
            this.infoTrainKind.MultiSetWhere = false;
            this.infoTrainKind.Name = "infoTrainKind";
            this.infoTrainKind.NotificationAutoEnlist = false;
            this.infoTrainKind.SecExcept = null;
            this.infoTrainKind.SecFieldName = null;
            this.infoTrainKind.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoTrainKind.SelectPaging = false;
            this.infoTrainKind.SelectTop = 0;
            this.infoTrainKind.SiteControl = false;
            this.infoTrainKind.SiteFieldName = null;
            this.infoTrainKind.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoRequisition
            // 
            this.infoRequisition.CacheConnection = false;
            this.infoRequisition.CommandText = "select dbo.Requisition.RequisitionNO,dbo.Requisition.RequisitionDescr+\'/\'+dbo.Req" +
    "uisition.RequisitionNO as RequisitionDescr,dbo.Requisition.CreateBy from dbo.Req" +
    "uisition";
            this.infoRequisition.CommandTimeout = 30;
            this.infoRequisition.CommandType = System.Data.CommandType.Text;
            this.infoRequisition.DynamicTableName = false;
            this.infoRequisition.EEPAlias = null;
            this.infoRequisition.EncodingAfter = null;
            this.infoRequisition.EncodingBefore = "Windows-1252";
            this.infoRequisition.InfoConnection = this.InfoConnection1;
            keyItem5.KeyName = "RequisitionNO";
            this.infoRequisition.KeyFields.Add(keyItem5);
            this.infoRequisition.MultiSetWhere = false;
            this.infoRequisition.Name = "infoRequisition";
            this.infoRequisition.NotificationAutoEnlist = false;
            this.infoRequisition.SecExcept = null;
            this.infoRequisition.SecFieldName = null;
            this.infoRequisition.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoRequisition.SelectPaging = false;
            this.infoRequisition.SelectTop = 0;
            this.infoRequisition.SiteControl = false;
            this.infoRequisition.SiteFieldName = null;
            this.infoRequisition.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoProofType
            // 
            this.infoProofType.CacheConnection = false;
            this.infoProofType.CommandText = "select ProofType.ProofTypeID,ProofType.ProofTypeName from ProofType\r\norder by Pro" +
    "ofType.ProofTypeID";
            this.infoProofType.CommandTimeout = 30;
            this.infoProofType.CommandType = System.Data.CommandType.Text;
            this.infoProofType.DynamicTableName = false;
            this.infoProofType.EEPAlias = null;
            this.infoProofType.EncodingAfter = null;
            this.infoProofType.EncodingBefore = "Windows-1252";
            this.infoProofType.InfoConnection = this.InfoConnection1;
            keyItem6.KeyName = "ProofTypeID";
            this.infoProofType.KeyFields.Add(keyItem6);
            this.infoProofType.MultiSetWhere = false;
            this.infoProofType.Name = "infoProofType";
            this.infoProofType.NotificationAutoEnlist = false;
            this.infoProofType.SecExcept = null;
            this.infoProofType.SecFieldName = null;
            this.infoProofType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoProofType.SelectPaging = false;
            this.infoProofType.SelectTop = 0;
            this.infoProofType.SiteControl = false;
            this.infoProofType.SiteFieldName = null;
            this.infoProofType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoPayType
            // 
            this.infoPayType.CacheConnection = false;
            this.infoPayType.CommandText = "select PayType.PayTypeID,PayType.PayTypeName from PayType\r\norder by PayType.PayTy" +
    "peID";
            this.infoPayType.CommandTimeout = 30;
            this.infoPayType.CommandType = System.Data.CommandType.Text;
            this.infoPayType.DynamicTableName = false;
            this.infoPayType.EEPAlias = null;
            this.infoPayType.EncodingAfter = null;
            this.infoPayType.EncodingBefore = "Windows-1252";
            this.infoPayType.InfoConnection = this.InfoConnection1;
            keyItem7.KeyName = "PayTypeID";
            this.infoPayType.KeyFields.Add(keyItem7);
            this.infoPayType.MultiSetWhere = false;
            this.infoPayType.Name = "infoPayType";
            this.infoPayType.NotificationAutoEnlist = false;
            this.infoPayType.SecExcept = null;
            this.infoPayType.SecFieldName = null;
            this.infoPayType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoPayType.SelectPaging = false;
            this.infoPayType.SelectTop = 0;
            this.infoPayType.SiteControl = false;
            this.infoPayType.SiteFieldName = null;
            this.infoPayType.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_TrainingForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoTrainType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoTrainKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoRequisition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoProofType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPayType)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand TrainingForm;
        private Srvtools.UpdateComponent ucTrainingForm;
        private Srvtools.InfoCommand View_TrainingForm;
        private Srvtools.AutoNumber autoTrainingFormID;
        private Srvtools.InfoCommand infoTrainType;
        private Srvtools.InfoCommand infoTrainKind;
        private Srvtools.InfoCommand infoRequisition;
        private Srvtools.InfoCommand infoProofType;
        private Srvtools.InfoCommand infoPayType;
    }
}
