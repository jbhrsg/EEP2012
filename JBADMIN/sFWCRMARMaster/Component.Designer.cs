namespace sFWCRMARMaster
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
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.Employer = new Srvtools.InfoCommand(this.components);
            this.infoEmployer = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmployer)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "procReportARMaster";
            service1.NonLogin = false;
            service1.ServiceName = "procReportARMaster";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "FWCRM";
            // 
            // Employer
            // 
            this.Employer.CacheConnection = false;
            this.Employer.CommandText = "select * from Employer";
            this.Employer.CommandTimeout = 30;
            this.Employer.CommandType = System.Data.CommandType.Text;
            this.Employer.DynamicTableName = false;
            this.Employer.EEPAlias = "FWCRM";
            this.Employer.EncodingAfter = null;
            this.Employer.EncodingBefore = "Windows-1252";
            this.Employer.EncodingConvert = null;
            this.Employer.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.Employer.KeyFields.Add(keyItem1);
            this.Employer.MultiSetWhere = false;
            this.Employer.Name = "Employer";
            this.Employer.NotificationAutoEnlist = false;
            this.Employer.SecExcept = null;
            this.Employer.SecFieldName = null;
            this.Employer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.Employer.SelectPaging = false;
            this.Employer.SelectTop = 0;
            this.Employer.SiteControl = false;
            this.Employer.SiteFieldName = null;
            this.Employer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoEmployer
            // 
            this.infoEmployer.CacheConnection = false;
            this.infoEmployer.CommandText = "select * from Employer";
            this.infoEmployer.CommandTimeout = 30;
            this.infoEmployer.CommandType = System.Data.CommandType.Text;
            this.infoEmployer.DynamicTableName = false;
            this.infoEmployer.EEPAlias = "FWCRM";
            this.infoEmployer.EncodingAfter = null;
            this.infoEmployer.EncodingBefore = "Windows-1252";
            this.infoEmployer.EncodingConvert = null;
            this.infoEmployer.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            this.infoEmployer.KeyFields.Add(keyItem2);
            this.infoEmployer.MultiSetWhere = false;
            this.infoEmployer.Name = "infoEmployer";
            this.infoEmployer.NotificationAutoEnlist = false;
            this.infoEmployer.SecExcept = null;
            this.infoEmployer.SecFieldName = null;
            this.infoEmployer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoEmployer.SelectPaging = false;
            this.infoEmployer.SelectTop = 0;
            this.infoEmployer.SiteControl = false;
            this.infoEmployer.SiteFieldName = null;
            this.infoEmployer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Employer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoEmployer)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand Employer;
        private Srvtools.InfoCommand infoEmployer;
    }
}
