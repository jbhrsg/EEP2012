namespace sSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Schedule = new Srvtools.InfoCommand(this.components);
            this.ScheduleType = new Srvtools.InfoCommand(this.components);
            this.Organization = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Schedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetEmpOrg";
            service1.NonLogin = false;
            service1.ServiceName = "GetEmpOrg";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // Schedule
            // 
            this.Schedule.CacheConnection = false;
            this.Schedule.CommandText = "SELECT * FROM VIEW_SCHEDULE\r\nOrder by ScheItem,ScheStaTime\r\n";
            this.Schedule.CommandTimeout = 30;
            this.Schedule.CommandType = System.Data.CommandType.Text;
            this.Schedule.DynamicTableName = false;
            this.Schedule.EEPAlias = null;
            this.Schedule.EncodingAfter = null;
            this.Schedule.EncodingBefore = "Windows-1252";
            this.Schedule.EncodingConvert = null;
            this.Schedule.InfoConnection = this.InfoConnection1;
            this.Schedule.MultiSetWhere = false;
            this.Schedule.Name = "Schedule";
            this.Schedule.NotificationAutoEnlist = false;
            this.Schedule.SecExcept = null;
            this.Schedule.SecFieldName = null;
            this.Schedule.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Schedule.SelectPaging = false;
            this.Schedule.SelectTop = 0;
            this.Schedule.SiteControl = false;
            this.Schedule.SiteFieldName = null;
            this.Schedule.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ScheduleType
            // 
            this.ScheduleType.CacheConnection = false;
            this.ScheduleType.CommandText = resources.GetString("ScheduleType.CommandText");
            this.ScheduleType.CommandTimeout = 30;
            this.ScheduleType.CommandType = System.Data.CommandType.Text;
            this.ScheduleType.DynamicTableName = false;
            this.ScheduleType.EEPAlias = null;
            this.ScheduleType.EncodingAfter = null;
            this.ScheduleType.EncodingBefore = "Windows-1252";
            this.ScheduleType.EncodingConvert = null;
            this.ScheduleType.InfoConnection = this.InfoConnection1;
            this.ScheduleType.MultiSetWhere = false;
            this.ScheduleType.Name = "ScheduleType";
            this.ScheduleType.NotificationAutoEnlist = false;
            this.ScheduleType.SecExcept = null;
            this.ScheduleType.SecFieldName = null;
            this.ScheduleType.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ScheduleType.SelectPaging = false;
            this.ScheduleType.SelectTop = 0;
            this.ScheduleType.SiteControl = false;
            this.ScheduleType.SiteFieldName = null;
            this.ScheduleType.UpdatedRowSource = System.Data.UpdateRowSource.None;
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
            keyItem1.KeyName = "ORG_NO";
            this.Organization.KeyFields.Add(keyItem1);
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
            ((System.ComponentModel.ISupportInitialize)(this.Schedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Organization)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Schedule;
        private Srvtools.InfoCommand ScheduleType;
        private Srvtools.InfoCommand Organization;
    }
}
