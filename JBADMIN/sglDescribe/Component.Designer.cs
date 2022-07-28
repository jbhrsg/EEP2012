namespace sglDescribe
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
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.glDescribe = new Srvtools.InfoCommand(this.components);
            this.ucglDescribe = new Srvtools.UpdateComponent(this.components);
            this.glAccountItem = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glDescribe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glAccountItem)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetrOftenUsed";
            service1.NonLogin = false;
            service1.ServiceName = "GetrOftenUsed";
            this.serviceManager1.ServiceCollection.Add(service1);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // glDescribe
            // 
            this.glDescribe.CacheConnection = false;
            this.glDescribe.CommandText = "SELECT dbo.[glDescribe].* FROM dbo.[glDescribe]\r\norder by DescribeID";
            this.glDescribe.CommandTimeout = 30;
            this.glDescribe.CommandType = System.Data.CommandType.Text;
            this.glDescribe.DynamicTableName = false;
            this.glDescribe.EEPAlias = "";
            this.glDescribe.EncodingAfter = null;
            this.glDescribe.EncodingBefore = "Windows-1252";
            this.glDescribe.EncodingConvert = null;
            this.glDescribe.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "AutoKey";
            this.glDescribe.KeyFields.Add(keyItem1);
            this.glDescribe.MultiSetWhere = false;
            this.glDescribe.Name = "glDescribe";
            this.glDescribe.NotificationAutoEnlist = false;
            this.glDescribe.SecExcept = null;
            this.glDescribe.SecFieldName = null;
            this.glDescribe.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glDescribe.SelectPaging = false;
            this.glDescribe.SelectTop = 0;
            this.glDescribe.SiteControl = false;
            this.glDescribe.SiteFieldName = null;
            this.glDescribe.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucglDescribe
            // 
            this.ucglDescribe.AutoTrans = true;
            this.ucglDescribe.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "AutoKey";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CompanyID";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "VoucherType";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "Acno";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "DescribeID";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "Describe";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            this.ucglDescribe.FieldAttrs.Add(fieldAttr1);
            this.ucglDescribe.FieldAttrs.Add(fieldAttr2);
            this.ucglDescribe.FieldAttrs.Add(fieldAttr3);
            this.ucglDescribe.FieldAttrs.Add(fieldAttr4);
            this.ucglDescribe.FieldAttrs.Add(fieldAttr5);
            this.ucglDescribe.FieldAttrs.Add(fieldAttr6);
            this.ucglDescribe.LogInfo = null;
            this.ucglDescribe.Name = "ucglDescribe";
            this.ucglDescribe.RowAffectsCheck = true;
            this.ucglDescribe.SelectCmd = this.glDescribe;
            this.ucglDescribe.SelectCmdForUpdate = null;
            this.ucglDescribe.SendSQLCmd = true;
            this.ucglDescribe.ServerModify = true;
            this.ucglDescribe.ServerModifyGetMax = false;
            this.ucglDescribe.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucglDescribe.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucglDescribe.UseTranscationScope = false;
            this.ucglDescribe.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // glAccountItem
            // 
            this.glAccountItem.CacheConnection = false;
            this.glAccountItem.CommandText = "select distinct i.Acno,(select top 1 AcnoName from glAccountItem where Acno=i.Acn" +
    "o order by SubAcno) as AcnoName\r\nfrom glAccountItem i\r\nwhere i.Acno!=\'\'\r\norder b" +
    "y i.Acno";
            this.glAccountItem.CommandTimeout = 30;
            this.glAccountItem.CommandType = System.Data.CommandType.Text;
            this.glAccountItem.DynamicTableName = false;
            this.glAccountItem.EEPAlias = "";
            this.glAccountItem.EncodingAfter = null;
            this.glAccountItem.EncodingBefore = "Windows-1252";
            this.glAccountItem.EncodingConvert = null;
            this.glAccountItem.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "AutoKey";
            keyItem3.KeyName = "CompanyID1";
            this.glAccountItem.KeyFields.Add(keyItem2);
            this.glAccountItem.KeyFields.Add(keyItem3);
            this.glAccountItem.MultiSetWhere = false;
            this.glAccountItem.Name = "glAccountItem";
            this.glAccountItem.NotificationAutoEnlist = false;
            this.glAccountItem.SecExcept = null;
            this.glAccountItem.SecFieldName = null;
            this.glAccountItem.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.glAccountItem.SelectPaging = false;
            this.glAccountItem.SelectTop = 0;
            this.glAccountItem.SiteControl = false;
            this.glAccountItem.SiteFieldName = null;
            this.glAccountItem.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glDescribe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glAccountItem)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand glDescribe;
        private Srvtools.UpdateComponent ucglDescribe;
        private Srvtools.InfoCommand glAccountItem;
    }
}
