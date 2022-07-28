namespace sHRMAttendAbsentQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.HRM_ATTEND_ABSENT_MINUS = new Srvtools.InfoCommand(this.components);
            this.HRM_ATTEND_OVERTIME_DATA = new Srvtools.InfoCommand(this.components);
            this.HRM_BASE_BASE = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_ATTEND_ABSENT_MINUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_ATTEND_OVERTIME_DATA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_BASE_BASE)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBHR_EEP";
            // 
            // HRM_ATTEND_ABSENT_MINUS
            // 
            this.HRM_ATTEND_ABSENT_MINUS.CacheConnection = false;
            this.HRM_ATTEND_ABSENT_MINUS.CommandText = resources.GetString("HRM_ATTEND_ABSENT_MINUS.CommandText");
            this.HRM_ATTEND_ABSENT_MINUS.CommandTimeout = 30;
            this.HRM_ATTEND_ABSENT_MINUS.CommandType = System.Data.CommandType.Text;
            this.HRM_ATTEND_ABSENT_MINUS.DynamicTableName = false;
            this.HRM_ATTEND_ABSENT_MINUS.EEPAlias = "JBHR_EEP";
            this.HRM_ATTEND_ABSENT_MINUS.EncodingAfter = null;
            this.HRM_ATTEND_ABSENT_MINUS.EncodingBefore = "Windows-1252";
            this.HRM_ATTEND_ABSENT_MINUS.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "ABSENT_MINUS_ID";
            this.HRM_ATTEND_ABSENT_MINUS.KeyFields.Add(keyItem1);
            this.HRM_ATTEND_ABSENT_MINUS.MultiSetWhere = false;
            this.HRM_ATTEND_ABSENT_MINUS.Name = "HRM_ATTEND_ABSENT_MINUS";
            this.HRM_ATTEND_ABSENT_MINUS.NotificationAutoEnlist = false;
            this.HRM_ATTEND_ABSENT_MINUS.SecExcept = null;
            this.HRM_ATTEND_ABSENT_MINUS.SecFieldName = null;
            this.HRM_ATTEND_ABSENT_MINUS.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRM_ATTEND_ABSENT_MINUS.SelectPaging = false;
            this.HRM_ATTEND_ABSENT_MINUS.SelectTop = 0;
            this.HRM_ATTEND_ABSENT_MINUS.SiteControl = false;
            this.HRM_ATTEND_ABSENT_MINUS.SiteFieldName = null;
            this.HRM_ATTEND_ABSENT_MINUS.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HRM_ATTEND_OVERTIME_DATA
            // 
            this.HRM_ATTEND_OVERTIME_DATA.CacheConnection = false;
            this.HRM_ATTEND_OVERTIME_DATA.CommandText = resources.GetString("HRM_ATTEND_OVERTIME_DATA.CommandText");
            this.HRM_ATTEND_OVERTIME_DATA.CommandTimeout = 30;
            this.HRM_ATTEND_OVERTIME_DATA.CommandType = System.Data.CommandType.Text;
            this.HRM_ATTEND_OVERTIME_DATA.DynamicTableName = false;
            this.HRM_ATTEND_OVERTIME_DATA.EEPAlias = "JBHR_EEP";
            this.HRM_ATTEND_OVERTIME_DATA.EncodingAfter = null;
            this.HRM_ATTEND_OVERTIME_DATA.EncodingBefore = "Windows-1252";
            this.HRM_ATTEND_OVERTIME_DATA.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "OVERTIME_ID";
            this.HRM_ATTEND_OVERTIME_DATA.KeyFields.Add(keyItem2);
            this.HRM_ATTEND_OVERTIME_DATA.MultiSetWhere = false;
            this.HRM_ATTEND_OVERTIME_DATA.Name = "HRM_ATTEND_OVERTIME_DATA";
            this.HRM_ATTEND_OVERTIME_DATA.NotificationAutoEnlist = false;
            this.HRM_ATTEND_OVERTIME_DATA.SecExcept = null;
            this.HRM_ATTEND_OVERTIME_DATA.SecFieldName = null;
            this.HRM_ATTEND_OVERTIME_DATA.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRM_ATTEND_OVERTIME_DATA.SelectPaging = false;
            this.HRM_ATTEND_OVERTIME_DATA.SelectTop = 0;
            this.HRM_ATTEND_OVERTIME_DATA.SiteControl = false;
            this.HRM_ATTEND_OVERTIME_DATA.SiteFieldName = null;
            this.HRM_ATTEND_OVERTIME_DATA.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // HRM_BASE_BASE
            // 
            this.HRM_BASE_BASE.CacheConnection = false;
            this.HRM_BASE_BASE.CommandText = resources.GetString("HRM_BASE_BASE.CommandText");
            this.HRM_BASE_BASE.CommandTimeout = 30;
            this.HRM_BASE_BASE.CommandType = System.Data.CommandType.Text;
            this.HRM_BASE_BASE.DynamicTableName = false;
            this.HRM_BASE_BASE.EEPAlias = "JBHR_EEP";
            this.HRM_BASE_BASE.EncodingAfter = null;
            this.HRM_BASE_BASE.EncodingBefore = "Windows-1252";
            this.HRM_BASE_BASE.InfoConnection = this.InfoConnection1;
            this.HRM_BASE_BASE.MultiSetWhere = true;
            this.HRM_BASE_BASE.Name = "HRM_BASE_BASE";
            this.HRM_BASE_BASE.NotificationAutoEnlist = false;
            this.HRM_BASE_BASE.SecExcept = null;
            this.HRM_BASE_BASE.SecFieldName = null;
            this.HRM_BASE_BASE.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.HRM_BASE_BASE.SelectPaging = false;
            this.HRM_BASE_BASE.SelectTop = 0;
            this.HRM_BASE_BASE.SiteControl = false;
            this.HRM_BASE_BASE.SiteFieldName = null;
            this.HRM_BASE_BASE.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_ATTEND_ABSENT_MINUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_ATTEND_OVERTIME_DATA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRM_BASE_BASE)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand HRM_ATTEND_ABSENT_MINUS;
        private Srvtools.InfoCommand HRM_ATTEND_OVERTIME_DATA;
        private Srvtools.InfoCommand HRM_BASE_BASE;
    }
}
