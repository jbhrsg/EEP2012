namespace sIssueBelong
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.View_IssueBelong = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_IssueBelong)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "CheckMasterDelete";
            service1.NonLogin = false;
            service1.ServiceName = "CheckMasterDelete";
            service2.DelegateName = "CheckDetailDelete";
            service2.NonLogin = false;
            service2.ServiceName = "CheckDetailDelete";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // View_IssueBelong
            // 
            this.View_IssueBelong.CacheConnection = false;
            this.View_IssueBelong.CommandText = "SELECT * FROM dbo.[IssueBelong]";
            this.View_IssueBelong.CommandTimeout = 30;
            this.View_IssueBelong.CommandType = System.Data.CommandType.Text;
            this.View_IssueBelong.DynamicTableName = false;
            this.View_IssueBelong.EEPAlias = null;
            this.View_IssueBelong.EncodingAfter = null;
            this.View_IssueBelong.EncodingBefore = "Windows-1252";
            this.View_IssueBelong.InfoConnection = this.InfoConnection1;
            this.View_IssueBelong.MultiSetWhere = false;
            this.View_IssueBelong.Name = "View_IssueBelong";
            this.View_IssueBelong.NotificationAutoEnlist = false;
            this.View_IssueBelong.SecExcept = null;
            this.View_IssueBelong.SecFieldName = null;
            this.View_IssueBelong.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_IssueBelong.SelectPaging = false;
            this.View_IssueBelong.SelectTop = 0;
            this.View_IssueBelong.SiteControl = false;
            this.View_IssueBelong.SiteFieldName = null;
            this.View_IssueBelong.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_IssueBelong)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand View_IssueBelong;
    }
}
