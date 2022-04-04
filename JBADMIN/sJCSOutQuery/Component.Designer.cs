namespace sJCSOutQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.RoomerInOutQuery = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoomerInOutQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetRoomerInOutData";
            service1.NonLogin = false;
            service1.ServiceName = "GetRoomerInOutData";
            service2.DelegateName = "RoomerInOutAutoExcel";
            service2.NonLogin = false;
            service2.ServiceName = "RoomerInOutAutoExcel";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // RoomerInOutQuery
            // 
            this.RoomerInOutQuery.CacheConnection = false;
            this.RoomerInOutQuery.CommandText = resources.GetString("RoomerInOutQuery.CommandText");
            this.RoomerInOutQuery.CommandTimeout = 30;
            this.RoomerInOutQuery.CommandType = System.Data.CommandType.Text;
            this.RoomerInOutQuery.DynamicTableName = false;
            this.RoomerInOutQuery.EEPAlias = "JCS";
            this.RoomerInOutQuery.EncodingAfter = null;
            this.RoomerInOutQuery.EncodingBefore = "Windows-1252";
            this.RoomerInOutQuery.EncodingConvert = null;
            this.RoomerInOutQuery.InfoConnection = this.infoConnection2;
            keyItem1.KeyName = "RoomerID";
            this.RoomerInOutQuery.KeyFields.Add(keyItem1);
            this.RoomerInOutQuery.MultiSetWhere = false;
            this.RoomerInOutQuery.Name = "RoomerInOutQuery";
            this.RoomerInOutQuery.NotificationAutoEnlist = false;
            this.RoomerInOutQuery.SecExcept = null;
            this.RoomerInOutQuery.SecFieldName = null;
            this.RoomerInOutQuery.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.RoomerInOutQuery.SelectPaging = false;
            this.RoomerInOutQuery.SelectTop = 0;
            this.RoomerInOutQuery.SiteControl = false;
            this.RoomerInOutQuery.SiteFieldName = null;
            this.RoomerInOutQuery.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JCS";
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoomerInOutQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand RoomerInOutQuery;
        private Srvtools.InfoConnection infoConnection2;
    }
}
