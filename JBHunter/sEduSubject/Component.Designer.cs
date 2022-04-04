namespace sEduSubject
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
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr8 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HUT_EduSubject = new Srvtools.InfoCommand(this.components);
            this.ucHUT_EduSubject = new Srvtools.UpdateComponent(this.components);
            this.View_HUT_EduSubject = new Srvtools.InfoCommand(this.components);
            this.HUT_EduSubjects = new Srvtools.InfoCommand(this.components);
            this.ucHUT_EduSubjects = new Srvtools.UpdateComponent(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_EduSubject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_EduSubject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_EduSubjects)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckDelMaster";
            service1.NonLogin = false;
            service1.ServiceName = "CheckDelMaster";
            service2.DelegateName = "CheckDelItem";
            service2.NonLogin = false;
            service2.ServiceName = "CheckDelItem";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "Hunter";
            // 
            // HUT_EduSubject
            // 
            this.HUT_EduSubject.CacheConnection = false;
            this.HUT_EduSubject.CommandText = "SELECT dbo.[HUT_EduSubject].*\r\n FROM dbo.[HUT_EduSubject]\r\nWHERE dbo.[HUT_EduSubj" +
    "ect].NodeLevel=1\r\nORDER BY dbo.[HUT_EduSubject].SubjectName";
            this.HUT_EduSubject.CommandTimeout = 30;
            this.HUT_EduSubject.CommandType = System.Data.CommandType.Text;
            this.HUT_EduSubject.DynamicTableName = false;
            this.HUT_EduSubject.EEPAlias = null;
            this.HUT_EduSubject.EncodingAfter = null;
            this.HUT_EduSubject.EncodingBefore = "Windows-1252";
            this.HUT_EduSubject.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ID";
            this.HUT_EduSubject.KeyFields.Add(keyItem1);
            this.HUT_EduSubject.MultiSetWhere = false;
            this.HUT_EduSubject.Name = "HUT_EduSubject";
            this.HUT_EduSubject.NotificationAutoEnlist = false;
            this.HUT_EduSubject.SecExcept = null;
            this.HUT_EduSubject.SecFieldName = null;
            this.HUT_EduSubject.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_EduSubject.SelectPaging = false;
            this.HUT_EduSubject.SelectTop = 0;
            this.HUT_EduSubject.SiteControl = false;
            this.HUT_EduSubject.SiteFieldName = null;
            this.HUT_EduSubject.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_EduSubject
            // 
            this.ucHUT_EduSubject.AutoTrans = true;
            this.ucHUT_EduSubject.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "ID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "SubjectName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "ParentID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "NodeLevel";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "CreateBy";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = "_username";
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "CreateDate";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "LastUpdateBy";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Update;
            fieldAttr7.DefaultValue = "_username";
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "LastUpdateDate";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.InsertAndUpdate;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            this.ucHUT_EduSubject.FieldAttrs.Add(fieldAttr1);
            this.ucHUT_EduSubject.FieldAttrs.Add(fieldAttr2);
            this.ucHUT_EduSubject.FieldAttrs.Add(fieldAttr3);
            this.ucHUT_EduSubject.FieldAttrs.Add(fieldAttr4);
            this.ucHUT_EduSubject.FieldAttrs.Add(fieldAttr5);
            this.ucHUT_EduSubject.FieldAttrs.Add(fieldAttr6);
            this.ucHUT_EduSubject.FieldAttrs.Add(fieldAttr7);
            this.ucHUT_EduSubject.FieldAttrs.Add(fieldAttr8);
            this.ucHUT_EduSubject.LogInfo = null;
            this.ucHUT_EduSubject.Name = "ucHUT_EduSubject";
            this.ucHUT_EduSubject.RowAffectsCheck = true;
            this.ucHUT_EduSubject.SelectCmd = this.HUT_EduSubject;
            this.ucHUT_EduSubject.SelectCmdForUpdate = null;
            this.ucHUT_EduSubject.ServerModify = true;
            this.ucHUT_EduSubject.ServerModifyGetMax = false;
            this.ucHUT_EduSubject.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_EduSubject.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_EduSubject.UseTranscationScope = false;
            this.ucHUT_EduSubject.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_HUT_EduSubject
            // 
            this.View_HUT_EduSubject.CacheConnection = false;
            this.View_HUT_EduSubject.CommandText = "SELECT * FROM dbo.[HUT_EduSubject]";
            this.View_HUT_EduSubject.CommandTimeout = 30;
            this.View_HUT_EduSubject.CommandType = System.Data.CommandType.Text;
            this.View_HUT_EduSubject.DynamicTableName = false;
            this.View_HUT_EduSubject.EEPAlias = null;
            this.View_HUT_EduSubject.EncodingAfter = null;
            this.View_HUT_EduSubject.EncodingBefore = "Windows-1252";
            this.View_HUT_EduSubject.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "ID";
            this.View_HUT_EduSubject.KeyFields.Add(keyItem2);
            this.View_HUT_EduSubject.MultiSetWhere = false;
            this.View_HUT_EduSubject.Name = "View_HUT_EduSubject";
            this.View_HUT_EduSubject.NotificationAutoEnlist = false;
            this.View_HUT_EduSubject.SecExcept = null;
            this.View_HUT_EduSubject.SecFieldName = null;
            this.View_HUT_EduSubject.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_HUT_EduSubject.SelectPaging = false;
            this.View_HUT_EduSubject.SelectTop = 0;
            this.View_HUT_EduSubject.SiteControl = false;
            this.View_HUT_EduSubject.SiteFieldName = null;
            this.View_HUT_EduSubject.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HUT_EduSubjects
            // 
            this.HUT_EduSubjects.CacheConnection = false;
            this.HUT_EduSubjects.CommandText = "select HUT_EduSubject.*\r\n from HUT_EduSubject\r\nwhere HUT_EduSubject.NodeLevel=2\r\n" +
    "order by HUT_EduSubject.SubjectName";
            this.HUT_EduSubjects.CommandTimeout = 30;
            this.HUT_EduSubjects.CommandType = System.Data.CommandType.Text;
            this.HUT_EduSubjects.DynamicTableName = false;
            this.HUT_EduSubjects.EEPAlias = null;
            this.HUT_EduSubjects.EncodingAfter = null;
            this.HUT_EduSubjects.EncodingBefore = "Windows-1252";
            this.HUT_EduSubjects.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "ID";
            this.HUT_EduSubjects.KeyFields.Add(keyItem3);
            this.HUT_EduSubjects.MultiSetWhere = false;
            this.HUT_EduSubjects.Name = "HUT_EduSubjects";
            this.HUT_EduSubjects.NotificationAutoEnlist = false;
            this.HUT_EduSubjects.SecExcept = null;
            this.HUT_EduSubjects.SecFieldName = null;
            this.HUT_EduSubjects.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HUT_EduSubjects.SelectPaging = false;
            this.HUT_EduSubjects.SelectTop = 0;
            this.HUT_EduSubjects.SiteControl = false;
            this.HUT_EduSubjects.SiteFieldName = null;
            this.HUT_EduSubjects.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucHUT_EduSubjects
            // 
            this.ucHUT_EduSubjects.AutoTrans = true;
            this.ucHUT_EduSubjects.ExceptJoin = false;
            this.ucHUT_EduSubjects.LogInfo = null;
            this.ucHUT_EduSubjects.Name = "ucHUT_EduSubjects";
            this.ucHUT_EduSubjects.RowAffectsCheck = true;
            this.ucHUT_EduSubjects.SelectCmd = this.HUT_EduSubjects;
            this.ucHUT_EduSubjects.SelectCmdForUpdate = null;
            this.ucHUT_EduSubjects.ServerModify = true;
            this.ucHUT_EduSubjects.ServerModifyGetMax = false;
            this.ucHUT_EduSubjects.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucHUT_EduSubjects.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucHUT_EduSubjects.UseTranscationScope = false;
            this.ucHUT_EduSubjects.WhereMode = Srvtools.WhereModeType.Keyfields;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_EduSubject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_HUT_EduSubject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HUT_EduSubjects)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HUT_EduSubject;
        private Srvtools.UpdateComponent ucHUT_EduSubject;
        private Srvtools.InfoCommand View_HUT_EduSubject;
        private Srvtools.InfoCommand HUT_EduSubjects;
        private Srvtools.UpdateComponent ucHUT_EduSubjects;
    }
}
