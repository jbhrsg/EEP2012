namespace sTeacher
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
            Srvtools.Service service4 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
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
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Teacher = new Srvtools.InfoCommand(this.components);
            this.ucTeacher = new Srvtools.UpdateComponent(this.components);
            this.View_Teacher = new Srvtools.InfoCommand(this.components);
            this.TeacherGroup = new Srvtools.InfoCommand(this.components);
            this.TeacherTree = new Srvtools.InfoCommand(this.components);
            this.CourseTree = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Teacher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Teacher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TeacherGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TeacherTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseTree)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "SetTeacherCanCourseIDs";
            service1.NonLogin = false;
            service1.ServiceName = "SetTeacherCanCourseIDs";
            service2.DelegateName = "GetTeacherID";
            service2.NonLogin = false;
            service2.ServiceName = "GetTeacherID";
            service3.DelegateName = "SetTeacherGroupIDs";
            service3.NonLogin = false;
            service3.ServiceName = "SetTeacherGroupIDs";
            service4.DelegateName = "GetCourseIDsNames";
            service4.NonLogin = false;
            service4.ServiceName = "GetCourseIDsNames";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "eTraining";
            // 
            // Teacher
            // 
            this.Teacher.CacheConnection = false;
            this.Teacher.CommandText = resources.GetString("Teacher.CommandText");
            this.Teacher.CommandTimeout = 30;
            this.Teacher.CommandType = System.Data.CommandType.Text;
            this.Teacher.DynamicTableName = false;
            this.Teacher.EEPAlias = null;
            this.Teacher.EncodingAfter = null;
            this.Teacher.EncodingBefore = "Windows-1252";
            this.Teacher.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "TeacherID";
            keyItem2.KeyName = "TeacherGroupID";
            this.Teacher.KeyFields.Add(keyItem1);
            this.Teacher.KeyFields.Add(keyItem2);
            this.Teacher.MultiSetWhere = false;
            this.Teacher.Name = "Teacher";
            this.Teacher.NotificationAutoEnlist = false;
            this.Teacher.SecExcept = null;
            this.Teacher.SecFieldName = null;
            this.Teacher.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Teacher.SelectPaging = false;
            this.Teacher.SelectTop = 0;
            this.Teacher.SiteControl = false;
            this.Teacher.SiteFieldName = null;
            this.Teacher.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucTeacher
            // 
            this.ucTeacher.AutoTrans = true;
            this.ucTeacher.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "TeacherID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "TeacherName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "StudentID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "TeacherGroupID";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "TeacherCompany";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "TeacherTitle";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "WorkingHistory";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            fieldAttr8.CheckNull = false;
            fieldAttr8.DataField = "ProSkill";
            fieldAttr8.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr8.DefaultValue = null;
            fieldAttr8.TrimLength = 0;
            fieldAttr8.UpdateEnable = true;
            fieldAttr8.WhereMode = true;
            fieldAttr9.CheckNull = false;
            fieldAttr9.DataField = "Licence";
            fieldAttr9.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr9.DefaultValue = null;
            fieldAttr9.TrimLength = 0;
            fieldAttr9.UpdateEnable = true;
            fieldAttr9.WhereMode = true;
            fieldAttr10.CheckNull = false;
            fieldAttr10.DataField = "Desciption";
            fieldAttr10.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr10.DefaultValue = null;
            fieldAttr10.TrimLength = 0;
            fieldAttr10.UpdateEnable = true;
            fieldAttr10.WhereMode = true;
            fieldAttr11.CheckNull = false;
            fieldAttr11.DataField = "CreateBy";
            fieldAttr11.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr11.DefaultValue = "_usercode";
            fieldAttr11.TrimLength = 0;
            fieldAttr11.UpdateEnable = true;
            fieldAttr11.WhereMode = true;
            fieldAttr12.CheckNull = false;
            fieldAttr12.DataField = "CreateDate";
            fieldAttr12.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr12.DefaultValue = null;
            fieldAttr12.TrimLength = 0;
            fieldAttr12.UpdateEnable = true;
            fieldAttr12.WhereMode = true;
            fieldAttr13.CheckNull = false;
            fieldAttr13.DataField = "LastUpdateBy";
            fieldAttr13.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr13.DefaultValue = "_usercode";
            fieldAttr13.TrimLength = 0;
            fieldAttr13.UpdateEnable = true;
            fieldAttr13.WhereMode = true;
            fieldAttr14.CheckNull = false;
            fieldAttr14.DataField = "LastUpdateDate";
            fieldAttr14.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr14.DefaultValue = null;
            fieldAttr14.TrimLength = 0;
            fieldAttr14.UpdateEnable = true;
            fieldAttr14.WhereMode = true;
            this.ucTeacher.FieldAttrs.Add(fieldAttr1);
            this.ucTeacher.FieldAttrs.Add(fieldAttr2);
            this.ucTeacher.FieldAttrs.Add(fieldAttr3);
            this.ucTeacher.FieldAttrs.Add(fieldAttr4);
            this.ucTeacher.FieldAttrs.Add(fieldAttr5);
            this.ucTeacher.FieldAttrs.Add(fieldAttr6);
            this.ucTeacher.FieldAttrs.Add(fieldAttr7);
            this.ucTeacher.FieldAttrs.Add(fieldAttr8);
            this.ucTeacher.FieldAttrs.Add(fieldAttr9);
            this.ucTeacher.FieldAttrs.Add(fieldAttr10);
            this.ucTeacher.FieldAttrs.Add(fieldAttr11);
            this.ucTeacher.FieldAttrs.Add(fieldAttr12);
            this.ucTeacher.FieldAttrs.Add(fieldAttr13);
            this.ucTeacher.FieldAttrs.Add(fieldAttr14);
            this.ucTeacher.LogInfo = null;
            this.ucTeacher.Name = "ucTeacher";
            this.ucTeacher.RowAffectsCheck = true;
            this.ucTeacher.SelectCmd = this.Teacher;
            this.ucTeacher.SelectCmdForUpdate = null;
            this.ucTeacher.ServerModify = true;
            this.ucTeacher.ServerModifyGetMax = false;
            this.ucTeacher.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucTeacher.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucTeacher.UseTranscationScope = false;
            this.ucTeacher.WhereMode = Srvtools.WhereModeType.Keyfields;
            this.ucTeacher.BeforeInsert += new Srvtools.UpdateComponentBeforeInsertEventHandler(this.ucTeacher_BeforeInsert);
            // 
            // View_Teacher
            // 
            this.View_Teacher.CacheConnection = false;
            this.View_Teacher.CommandText = "SELECT * FROM dbo.[Teacher]";
            this.View_Teacher.CommandTimeout = 30;
            this.View_Teacher.CommandType = System.Data.CommandType.Text;
            this.View_Teacher.DynamicTableName = false;
            this.View_Teacher.EEPAlias = null;
            this.View_Teacher.EncodingAfter = null;
            this.View_Teacher.EncodingBefore = "Windows-1252";
            this.View_Teacher.InfoConnection = this.InfoConnection1;
            this.View_Teacher.MultiSetWhere = false;
            this.View_Teacher.Name = "View_Teacher";
            this.View_Teacher.NotificationAutoEnlist = false;
            this.View_Teacher.SecExcept = null;
            this.View_Teacher.SecFieldName = null;
            this.View_Teacher.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_Teacher.SelectPaging = false;
            this.View_Teacher.SelectTop = 0;
            this.View_Teacher.SiteControl = false;
            this.View_Teacher.SiteFieldName = null;
            this.View_Teacher.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // TeacherGroup
            // 
            this.TeacherGroup.CacheConnection = false;
            this.TeacherGroup.CommandText = "SELECT * FROM TEACHERGROUP";
            this.TeacherGroup.CommandTimeout = 30;
            this.TeacherGroup.CommandType = System.Data.CommandType.Text;
            this.TeacherGroup.DynamicTableName = false;
            this.TeacherGroup.EEPAlias = null;
            this.TeacherGroup.EncodingAfter = null;
            this.TeacherGroup.EncodingBefore = "Windows-1252";
            this.TeacherGroup.InfoConnection = this.InfoConnection1;
            this.TeacherGroup.MultiSetWhere = false;
            this.TeacherGroup.Name = "TeacherGroup";
            this.TeacherGroup.NotificationAutoEnlist = false;
            this.TeacherGroup.SecExcept = null;
            this.TeacherGroup.SecFieldName = null;
            this.TeacherGroup.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.TeacherGroup.SelectPaging = false;
            this.TeacherGroup.SelectTop = 0;
            this.TeacherGroup.SiteControl = false;
            this.TeacherGroup.SiteFieldName = null;
            this.TeacherGroup.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // TeacherTree
            // 
            this.TeacherTree.CacheConnection = false;
            this.TeacherTree.CommandText = resources.GetString("TeacherTree.CommandText");
            this.TeacherTree.CommandTimeout = 30;
            this.TeacherTree.CommandType = System.Data.CommandType.Text;
            this.TeacherTree.DynamicTableName = false;
            this.TeacherTree.EEPAlias = null;
            this.TeacherTree.EncodingAfter = null;
            this.TeacherTree.EncodingBefore = "Windows-1252";
            this.TeacherTree.InfoConnection = this.InfoConnection1;
            this.TeacherTree.MultiSetWhere = false;
            this.TeacherTree.Name = "TeacherTree";
            this.TeacherTree.NotificationAutoEnlist = false;
            this.TeacherTree.SecExcept = null;
            this.TeacherTree.SecFieldName = null;
            this.TeacherTree.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.TeacherTree.SelectPaging = false;
            this.TeacherTree.SelectTop = 0;
            this.TeacherTree.SiteControl = false;
            this.TeacherTree.SiteFieldName = null;
            this.TeacherTree.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CourseTree
            // 
            this.CourseTree.CacheConnection = false;
            this.CourseTree.CommandText = "SELECT CourseID,CourseParentID,CourseNAME\r\n FROM COURSE\r\n WHERE CourseParentID<>\'" +
    "08\'  AND  CourseParentID<>\'99\' \r\n Order by CourseID";
            this.CourseTree.CommandTimeout = 30;
            this.CourseTree.CommandType = System.Data.CommandType.Text;
            this.CourseTree.DynamicTableName = false;
            this.CourseTree.EEPAlias = null;
            this.CourseTree.EncodingAfter = null;
            this.CourseTree.EncodingBefore = "Windows-1252";
            this.CourseTree.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CourseID";
            this.CourseTree.KeyFields.Add(keyItem3);
            this.CourseTree.MultiSetWhere = false;
            this.CourseTree.Name = "CourseTree";
            this.CourseTree.NotificationAutoEnlist = false;
            this.CourseTree.SecExcept = null;
            this.CourseTree.SecFieldName = null;
            this.CourseTree.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CourseTree.SelectPaging = false;
            this.CourseTree.SelectTop = 0;
            this.CourseTree.SiteControl = false;
            this.CourseTree.SiteFieldName = null;
            this.CourseTree.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Teacher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_Teacher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TeacherGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TeacherTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CourseTree)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Teacher;
        private Srvtools.UpdateComponent ucTeacher;
        private Srvtools.InfoCommand View_Teacher;
        private Srvtools.InfoCommand TeacherGroup;
        private Srvtools.InfoCommand TeacherTree;
        private Srvtools.InfoCommand CourseTree;
    }
}
