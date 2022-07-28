namespace sMedia_Report_PostList
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.PostList = new Srvtools.InfoCommand(this.components);
            this.ucPostList = new Srvtools.UpdateComponent(this.components);
            this.View_PostList = new Srvtools.InfoCommand(this.components);
            this.infoConnection2 = new Srvtools.InfoConnection(this.components);
            this.ERPCustomers2 = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PostList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomers2)).BeginInit();
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // PostList
            // 
            this.PostList.CacheConnection = false;
            this.PostList.CommandText = "SELECT dbo.[PostList].* FROM dbo.[PostList]";
            this.PostList.CommandTimeout = 30;
            this.PostList.CommandType = System.Data.CommandType.Text;
            this.PostList.DynamicTableName = false;
            this.PostList.EEPAlias = null;
            this.PostList.EncodingAfter = null;
            this.PostList.EncodingBefore = "Windows-1252";
            this.PostList.EncodingConvert = null;
            this.PostList.InfoConnection = this.InfoConnection1;
            this.PostList.MultiSetWhere = false;
            this.PostList.Name = "PostList";
            this.PostList.NotificationAutoEnlist = false;
            this.PostList.SecExcept = null;
            this.PostList.SecFieldName = null;
            this.PostList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PostList.SelectPaging = false;
            this.PostList.SelectTop = 0;
            this.PostList.SiteControl = false;
            this.PostList.SiteFieldName = null;
            this.PostList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucPostList
            // 
            this.ucPostList.AutoTrans = true;
            this.ucPostList.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CustShortName";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustAddr";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            this.ucPostList.FieldAttrs.Add(fieldAttr1);
            this.ucPostList.FieldAttrs.Add(fieldAttr2);
            this.ucPostList.LogInfo = null;
            this.ucPostList.Name = "ucPostList";
            this.ucPostList.RowAffectsCheck = true;
            this.ucPostList.SelectCmd = this.PostList;
            this.ucPostList.SelectCmdForUpdate = null;
            this.ucPostList.SendSQLCmd = true;
            this.ucPostList.ServerModify = true;
            this.ucPostList.ServerModifyGetMax = false;
            this.ucPostList.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucPostList.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucPostList.UseTranscationScope = false;
            this.ucPostList.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // View_PostList
            // 
            this.View_PostList.CacheConnection = false;
            this.View_PostList.CommandText = "SELECT * FROM dbo.[PostList]";
            this.View_PostList.CommandTimeout = 30;
            this.View_PostList.CommandType = System.Data.CommandType.Text;
            this.View_PostList.DynamicTableName = false;
            this.View_PostList.EEPAlias = null;
            this.View_PostList.EncodingAfter = null;
            this.View_PostList.EncodingBefore = "Windows-1252";
            this.View_PostList.EncodingConvert = null;
            this.View_PostList.InfoConnection = this.InfoConnection1;
            this.View_PostList.MultiSetWhere = false;
            this.View_PostList.Name = "View_PostList";
            this.View_PostList.NotificationAutoEnlist = false;
            this.View_PostList.SecExcept = null;
            this.View_PostList.SecFieldName = null;
            this.View_PostList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_PostList.SelectPaging = false;
            this.View_PostList.SelectTop = 0;
            this.View_PostList.SiteControl = false;
            this.View_PostList.SiteFieldName = null;
            this.View_PostList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoConnection2
            // 
            this.infoConnection2.EEPAlias = "JBDBNJB";
            // 
            // ERPCustomers2
            // 
            this.ERPCustomers2.CacheConnection = false;
            this.ERPCustomers2.CommandText = "select  distinct  top 100 CustNO,CustShortName,CustAddr from ERPCustomers";
            this.ERPCustomers2.CommandTimeout = 30;
            this.ERPCustomers2.CommandType = System.Data.CommandType.Text;
            this.ERPCustomers2.DynamicTableName = false;
            this.ERPCustomers2.EEPAlias = "";
            this.ERPCustomers2.EncodingAfter = null;
            this.ERPCustomers2.EncodingBefore = "Windows-1252";
            this.ERPCustomers2.EncodingConvert = null;
            this.ERPCustomers2.InfoConnection = this.infoConnection2;
            keyItem1.KeyName = "CustNO";
            this.ERPCustomers2.KeyFields.Add(keyItem1);
            this.ERPCustomers2.MultiSetWhere = false;
            this.ERPCustomers2.Name = "ERPCustomers2";
            this.ERPCustomers2.NotificationAutoEnlist = false;
            this.ERPCustomers2.SecExcept = null;
            this.ERPCustomers2.SecFieldName = null;
            this.ERPCustomers2.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.ERPCustomers2.SelectPaging = false;
            this.ERPCustomers2.SelectTop = 0;
            this.ERPCustomers2.SiteControl = false;
            this.ERPCustomers2.SiteFieldName = null;
            this.ERPCustomers2.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_PostList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoConnection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERPCustomers2)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand PostList;
        private Srvtools.UpdateComponent ucPostList;
        private Srvtools.InfoCommand View_PostList;
        private Srvtools.InfoConnection infoConnection2;
        private Srvtools.InfoCommand ERPCustomers2;
    }
}
