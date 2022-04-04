namespace sJBRecruitEmpSalary
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
            Srvtools.Service service7 = new Srvtools.Service();
            Srvtools.Service service8 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.FieldAttr fieldAttr1 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr2 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr3 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr4 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr5 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr6 = new Srvtools.FieldAttr();
            Srvtools.FieldAttr fieldAttr7 = new Srvtools.FieldAttr();
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.View_EmployeeSalary = new Srvtools.InfoCommand(this.components);
            this.ucView_EmployeeSalary = new Srvtools.UpdateComponent(this.components);
            this.YearMonth = new Srvtools.InfoCommand(this.components);
            this.YMCustomer = new Srvtools.InfoCommand(this.components);
            this.YMCustomerSaryID = new Srvtools.InfoCommand(this.components);
            this.SaryItem = new Srvtools.InfoCommand(this.components);
            this.DutyItem = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_EmployeeSalary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMCustomerSaryID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaryItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DutyItem)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GetCustomerID";
            service1.NonLogin = false;
            service1.ServiceName = "GetCustomerID";
            service2.DelegateName = "GetSaryID";
            service2.NonLogin = false;
            service2.ServiceName = "GetSaryID";
            service3.DelegateName = "GetCustomerID_Period";
            service3.NonLogin = false;
            service3.ServiceName = "GetCustomerID_Period";
            service4.DelegateName = "GetSaryID_Period";
            service4.NonLogin = false;
            service4.ServiceName = "GetSaryID_Period";
            service5.DelegateName = "GetSaryID_Select";
            service5.NonLogin = false;
            service5.ServiceName = "GetSaryID_Select";
            service6.DelegateName = "GetDutyID";
            service6.NonLogin = false;
            service6.ServiceName = "GetDutyID";
            service7.DelegateName = "GetDutyID_Period";
            service7.NonLogin = false;
            service7.ServiceName = "GetDutyID_Period";
            service8.DelegateName = "GetDutyID_Select";
            service8.NonLogin = false;
            service8.ServiceName = "GetDutyID_Select";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JBADMIN";
            // 
            // View_EmployeeSalary
            // 
            this.View_EmployeeSalary.CacheConnection = false;
            this.View_EmployeeSalary.CommandText = resources.GetString("View_EmployeeSalary.CommandText");
            this.View_EmployeeSalary.CommandTimeout = 30;
            this.View_EmployeeSalary.CommandType = System.Data.CommandType.Text;
            this.View_EmployeeSalary.DynamicTableName = false;
            this.View_EmployeeSalary.EEPAlias = null;
            this.View_EmployeeSalary.EncodingAfter = null;
            this.View_EmployeeSalary.EncodingBefore = "Windows-1252";
            this.View_EmployeeSalary.EncodingConvert = null;
            this.View_EmployeeSalary.InfoConnection = this.InfoConnection1;
            this.View_EmployeeSalary.MultiSetWhere = false;
            this.View_EmployeeSalary.Name = "View_EmployeeSalary";
            this.View_EmployeeSalary.NotificationAutoEnlist = false;
            this.View_EmployeeSalary.SecExcept = null;
            this.View_EmployeeSalary.SecFieldName = null;
            this.View_EmployeeSalary.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.View_EmployeeSalary.SelectPaging = false;
            this.View_EmployeeSalary.SelectTop = 0;
            this.View_EmployeeSalary.SiteControl = false;
            this.View_EmployeeSalary.SiteFieldName = null;
            this.View_EmployeeSalary.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // ucView_EmployeeSalary
            // 
            this.ucView_EmployeeSalary.AutoTrans = true;
            this.ucView_EmployeeSalary.ExceptJoin = false;
            fieldAttr1.CheckNull = false;
            fieldAttr1.DataField = "CustomerID";
            fieldAttr1.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr1.DefaultValue = null;
            fieldAttr1.TrimLength = 0;
            fieldAttr1.UpdateEnable = true;
            fieldAttr1.WhereMode = true;
            fieldAttr2.CheckNull = false;
            fieldAttr2.DataField = "CustomerName";
            fieldAttr2.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr2.DefaultValue = null;
            fieldAttr2.TrimLength = 0;
            fieldAttr2.UpdateEnable = true;
            fieldAttr2.WhereMode = true;
            fieldAttr3.CheckNull = false;
            fieldAttr3.DataField = "EmpID";
            fieldAttr3.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr3.DefaultValue = null;
            fieldAttr3.TrimLength = 0;
            fieldAttr3.UpdateEnable = true;
            fieldAttr3.WhereMode = true;
            fieldAttr4.CheckNull = false;
            fieldAttr4.DataField = "NameC";
            fieldAttr4.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr4.DefaultValue = null;
            fieldAttr4.TrimLength = 0;
            fieldAttr4.UpdateEnable = true;
            fieldAttr4.WhereMode = true;
            fieldAttr5.CheckNull = false;
            fieldAttr5.DataField = "YearMonth";
            fieldAttr5.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr5.DefaultValue = null;
            fieldAttr5.TrimLength = 0;
            fieldAttr5.UpdateEnable = true;
            fieldAttr5.WhereMode = true;
            fieldAttr6.CheckNull = false;
            fieldAttr6.DataField = "SaryName";
            fieldAttr6.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr6.DefaultValue = null;
            fieldAttr6.TrimLength = 0;
            fieldAttr6.UpdateEnable = true;
            fieldAttr6.WhereMode = true;
            fieldAttr7.CheckNull = false;
            fieldAttr7.DataField = "SaryAmount";
            fieldAttr7.DefaultMode = Srvtools.DefaultModeType.Insert;
            fieldAttr7.DefaultValue = null;
            fieldAttr7.TrimLength = 0;
            fieldAttr7.UpdateEnable = true;
            fieldAttr7.WhereMode = true;
            this.ucView_EmployeeSalary.FieldAttrs.Add(fieldAttr1);
            this.ucView_EmployeeSalary.FieldAttrs.Add(fieldAttr2);
            this.ucView_EmployeeSalary.FieldAttrs.Add(fieldAttr3);
            this.ucView_EmployeeSalary.FieldAttrs.Add(fieldAttr4);
            this.ucView_EmployeeSalary.FieldAttrs.Add(fieldAttr5);
            this.ucView_EmployeeSalary.FieldAttrs.Add(fieldAttr6);
            this.ucView_EmployeeSalary.FieldAttrs.Add(fieldAttr7);
            this.ucView_EmployeeSalary.LogInfo = null;
            this.ucView_EmployeeSalary.Name = "ucView_EmployeeSalary";
            this.ucView_EmployeeSalary.RowAffectsCheck = true;
            this.ucView_EmployeeSalary.SelectCmd = this.View_EmployeeSalary;
            this.ucView_EmployeeSalary.SelectCmdForUpdate = null;
            this.ucView_EmployeeSalary.SendSQLCmd = true;
            this.ucView_EmployeeSalary.ServerModify = true;
            this.ucView_EmployeeSalary.ServerModifyGetMax = false;
            this.ucView_EmployeeSalary.TranscationScopeTimeOut = System.TimeSpan.Parse("00:02:00");
            this.ucView_EmployeeSalary.TransIsolationLevel = System.Data.IsolationLevel.ReadCommitted;
            this.ucView_EmployeeSalary.UseTranscationScope = false;
            this.ucView_EmployeeSalary.WhereMode = Srvtools.WhereModeType.Keyfields;
            // 
            // YearMonth
            // 
            this.YearMonth.CacheConnection = false;
            this.YearMonth.CommandText = "SELECT  Distinct Substring(YearMonth,1,6) AS YearMonth\r\n  FROM  JBRecruit.dbo.Pay" +
    "Master\r\n  ORDER BY  YearMonth DESC";
            this.YearMonth.CommandTimeout = 30;
            this.YearMonth.CommandType = System.Data.CommandType.Text;
            this.YearMonth.DynamicTableName = false;
            this.YearMonth.EEPAlias = null;
            this.YearMonth.EncodingAfter = null;
            this.YearMonth.EncodingBefore = "Windows-1252";
            this.YearMonth.EncodingConvert = null;
            this.YearMonth.InfoConnection = this.InfoConnection1;
            this.YearMonth.MultiSetWhere = false;
            this.YearMonth.Name = "YearMonth";
            this.YearMonth.NotificationAutoEnlist = false;
            this.YearMonth.SecExcept = null;
            this.YearMonth.SecFieldName = null;
            this.YearMonth.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.YearMonth.SelectPaging = false;
            this.YearMonth.SelectTop = 0;
            this.YearMonth.SiteControl = false;
            this.YearMonth.SiteFieldName = null;
            this.YearMonth.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // YMCustomer
            // 
            this.YMCustomer.CacheConnection = false;
            this.YMCustomer.CommandText = resources.GetString("YMCustomer.CommandText");
            this.YMCustomer.CommandTimeout = 30;
            this.YMCustomer.CommandType = System.Data.CommandType.Text;
            this.YMCustomer.DynamicTableName = false;
            this.YMCustomer.EEPAlias = null;
            this.YMCustomer.EncodingAfter = null;
            this.YMCustomer.EncodingBefore = "Windows-1252";
            this.YMCustomer.EncodingConvert = null;
            this.YMCustomer.InfoConnection = this.InfoConnection1;
            this.YMCustomer.MultiSetWhere = false;
            this.YMCustomer.Name = "YMCustomer";
            this.YMCustomer.NotificationAutoEnlist = false;
            this.YMCustomer.SecExcept = null;
            this.YMCustomer.SecFieldName = null;
            this.YMCustomer.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.YMCustomer.SelectPaging = false;
            this.YMCustomer.SelectTop = 0;
            this.YMCustomer.SiteControl = false;
            this.YMCustomer.SiteFieldName = null;
            this.YMCustomer.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // YMCustomerSaryID
            // 
            this.YMCustomerSaryID.CacheConnection = false;
            this.YMCustomerSaryID.CommandText = resources.GetString("YMCustomerSaryID.CommandText");
            this.YMCustomerSaryID.CommandTimeout = 30;
            this.YMCustomerSaryID.CommandType = System.Data.CommandType.Text;
            this.YMCustomerSaryID.DynamicTableName = false;
            this.YMCustomerSaryID.EEPAlias = null;
            this.YMCustomerSaryID.EncodingAfter = null;
            this.YMCustomerSaryID.EncodingBefore = "Windows-1252";
            this.YMCustomerSaryID.EncodingConvert = null;
            this.YMCustomerSaryID.InfoConnection = this.InfoConnection1;
            this.YMCustomerSaryID.MultiSetWhere = false;
            this.YMCustomerSaryID.Name = "YMCustomerSaryID";
            this.YMCustomerSaryID.NotificationAutoEnlist = false;
            this.YMCustomerSaryID.SecExcept = null;
            this.YMCustomerSaryID.SecFieldName = null;
            this.YMCustomerSaryID.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.YMCustomerSaryID.SelectPaging = false;
            this.YMCustomerSaryID.SelectTop = 0;
            this.YMCustomerSaryID.SiteControl = false;
            this.YMCustomerSaryID.SiteFieldName = null;
            this.YMCustomerSaryID.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // SaryItem
            // 
            this.SaryItem.CacheConnection = false;
            this.SaryItem.CommandText = "SELECT A.SaryID,A.SaryName,B.SaryPayTypeName,A.SaryPayType\r\nFROM JBRecruit.dbo.SA" +
    "LARYBASE A,JBRecruit.dbo.SARYPAYTYPE B\r\nWHERE A.SARYPAYTYPE=B.SARYPAYTYPE AND 1=" +
    "2\r\n\r\n";
            this.SaryItem.CommandTimeout = 30;
            this.SaryItem.CommandType = System.Data.CommandType.Text;
            this.SaryItem.DynamicTableName = false;
            this.SaryItem.EEPAlias = null;
            this.SaryItem.EncodingAfter = null;
            this.SaryItem.EncodingBefore = "Windows-1252";
            this.SaryItem.EncodingConvert = null;
            this.SaryItem.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "SARYID";
            this.SaryItem.KeyFields.Add(keyItem1);
            this.SaryItem.MultiSetWhere = false;
            this.SaryItem.Name = "SaryItem";
            this.SaryItem.NotificationAutoEnlist = false;
            this.SaryItem.SecExcept = null;
            this.SaryItem.SecFieldName = null;
            this.SaryItem.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.SaryItem.SelectPaging = false;
            this.SaryItem.SelectTop = 0;
            this.SaryItem.SiteControl = false;
            this.SaryItem.SiteFieldName = null;
            this.SaryItem.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // DutyItem
            // 
            this.DutyItem.CacheConnection = false;
            this.DutyItem.CommandText = "SELECT A.SaryID,A.DutyName\r\nFROM JBRecruit.dbo.SALARYBASE A\r\nWHERE  1=2\r\n\r\n";
            this.DutyItem.CommandTimeout = 30;
            this.DutyItem.CommandType = System.Data.CommandType.Text;
            this.DutyItem.DynamicTableName = false;
            this.DutyItem.EEPAlias = null;
            this.DutyItem.EncodingAfter = null;
            this.DutyItem.EncodingBefore = "Windows-1252";
            this.DutyItem.EncodingConvert = null;
            this.DutyItem.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "SARYID";
            this.DutyItem.KeyFields.Add(keyItem2);
            this.DutyItem.MultiSetWhere = false;
            this.DutyItem.Name = "DutyItem";
            this.DutyItem.NotificationAutoEnlist = false;
            this.DutyItem.SecExcept = null;
            this.DutyItem.SecFieldName = null;
            this.DutyItem.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.DutyItem.SelectPaging = false;
            this.DutyItem.SelectTop = 0;
            this.DutyItem.SiteControl = false;
            this.DutyItem.SiteFieldName = null;
            this.DutyItem.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_EmployeeSalary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMCustomerSaryID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaryItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DutyItem)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand View_EmployeeSalary;
        private Srvtools.UpdateComponent ucView_EmployeeSalary;
        private Srvtools.InfoCommand YearMonth;
        private Srvtools.InfoCommand YMCustomer;
        private Srvtools.InfoCommand YMCustomerSaryID;
        private Srvtools.InfoCommand SaryItem;
        private Srvtools.InfoCommand DutyItem;
    }
}
