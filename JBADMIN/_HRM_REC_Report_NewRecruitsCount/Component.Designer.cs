namespace _HRM_REC_Report_NewRecruitsCount
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.NewREC_User = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewREC_User)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "procReportNewRecruitsCount";
            service1.NonLogin = false;
            service1.ServiceName = "procReportNewRecruitsCount";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBHRIS_DISPATCH";
            // 
            // NewREC_User
            // 
            this.NewREC_User.CacheConnection = false;
            this.NewREC_User.CommandText = resources.GetString("NewREC_User.CommandText");
            this.NewREC_User.CommandTimeout = 30;
            this.NewREC_User.CommandType = System.Data.CommandType.Text;
            this.NewREC_User.DynamicTableName = false;
            this.NewREC_User.EEPAlias = "JBHRIS_DISPATCH";
            this.NewREC_User.EncodingAfter = null;
            this.NewREC_User.EncodingBefore = "Windows-1252";
            this.NewREC_User.EncodingConvert = null;
            this.NewREC_User.InfoConnection = this.InfoConnection1;
            this.NewREC_User.MultiSetWhere = false;
            this.NewREC_User.Name = "NewREC_User";
            this.NewREC_User.NotificationAutoEnlist = false;
            this.NewREC_User.SecExcept = null;
            this.NewREC_User.SecFieldName = null;
            this.NewREC_User.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.NewREC_User.SelectPaging = false;
            this.NewREC_User.SelectTop = 0;
            this.NewREC_User.SiteControl = false;
            this.NewREC_User.SiteFieldName = null;
            this.NewREC_User.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewREC_User)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand NewREC_User;
    }
}
