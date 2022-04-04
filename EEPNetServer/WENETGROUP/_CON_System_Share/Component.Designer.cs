namespace _CON_System_Share
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
            Srvtools.Service service5 = new Srvtools.Service();
            Srvtools.Service service6 = new Srvtools.Service();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem4 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem5 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem6 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.COLDEF = new Srvtools.InfoCommand(this.components);
            this.CONTACT_VIEW = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.COLDEF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CONTACT_VIEW)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "checkRowCount";
            service1.NonLogin = false;
            service1.ServiceName = "checkRowCount";
            service2.DelegateName = "ExcelGetTitleName";
            service2.NonLogin = false;
            service2.ServiceName = "ExcelGetTitleName";
            service3.DelegateName = "getRoad";
            service3.NonLogin = false;
            service3.ServiceName = "getRoad";
            service4.DelegateName = "getCity";
            service4.NonLogin = false;
            service4.ServiceName = "getCity";
            service5.DelegateName = "getCountry";
            service5.NonLogin = false;
            service5.ServiceName = "getCountry";
            service6.DelegateName = "checkShareCodeRowCount";
            service6.NonLogin = false;
            service6.ServiceName = "checkShareCodeRowCount";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "WENETGROUP";
            // 
            // COLDEF
            // 
            this.COLDEF.CacheConnection = false;
            this.COLDEF.CommandText = "SELECT dbo.[COLDEF].* FROM dbo.[COLDEF]";
            this.COLDEF.CommandTimeout = 30;
            this.COLDEF.CommandType = System.Data.CommandType.Text;
            this.COLDEF.DynamicTableName = false;
            this.COLDEF.EEPAlias = null;
            this.COLDEF.EncodingAfter = null;
            this.COLDEF.EncodingBefore = "Windows-1252";
            this.COLDEF.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "TABLE_NAME";
            keyItem2.KeyName = "FIELD_NAME";
            this.COLDEF.KeyFields.Add(keyItem1);
            this.COLDEF.KeyFields.Add(keyItem2);
            this.COLDEF.MultiSetWhere = false;
            this.COLDEF.Name = "COLDEF";
            this.COLDEF.NotificationAutoEnlist = false;
            this.COLDEF.SecExcept = null;
            this.COLDEF.SecFieldName = null;
            this.COLDEF.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.COLDEF.SelectPaging = false;
            this.COLDEF.SelectTop = 0;
            this.COLDEF.SiteControl = false;
            this.COLDEF.SiteFieldName = null;
            this.COLDEF.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // CONTACT_VIEW
            // 
            this.CONTACT_VIEW.CacheConnection = false;
            this.CONTACT_VIEW.CommandText = "SELECT * FROM CONTACT_VIEW";
            this.CONTACT_VIEW.CommandTimeout = 30;
            this.CONTACT_VIEW.CommandType = System.Data.CommandType.Text;
            this.CONTACT_VIEW.DynamicTableName = false;
            this.CONTACT_VIEW.EEPAlias = null;
            this.CONTACT_VIEW.EncodingAfter = null;
            this.CONTACT_VIEW.EncodingBefore = "Windows-1252";
            this.CONTACT_VIEW.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "CENTER_ID";
            keyItem4.KeyName = "CONTACT_ID";
            keyItem5.KeyName = "CONTACT_ACTIVITY_ID";
            keyItem6.KeyName = "CONTACT_MEMO_ID";
            this.CONTACT_VIEW.KeyFields.Add(keyItem3);
            this.CONTACT_VIEW.KeyFields.Add(keyItem4);
            this.CONTACT_VIEW.KeyFields.Add(keyItem5);
            this.CONTACT_VIEW.KeyFields.Add(keyItem6);
            this.CONTACT_VIEW.MultiSetWhere = false;
            this.CONTACT_VIEW.Name = "CONTACT_VIEW";
            this.CONTACT_VIEW.NotificationAutoEnlist = false;
            this.CONTACT_VIEW.SecExcept = null;
            this.CONTACT_VIEW.SecFieldName = null;
            this.CONTACT_VIEW.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.CONTACT_VIEW.SelectPaging = false;
            this.CONTACT_VIEW.SelectTop = 0;
            this.CONTACT_VIEW.SiteControl = false;
            this.CONTACT_VIEW.SiteFieldName = null;
            this.CONTACT_VIEW.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.COLDEF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CONTACT_VIEW)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand COLDEF;
        private Srvtools.InfoCommand CONTACT_VIEW;
    }
}
