namespace sJBRecruitEmpSalary2
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
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.View_EmployeeSalary = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_EmployeeSalary)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "SelectEmpSalary2";
            service1.NonLogin = false;
            service1.ServiceName = "SelectEmpSalary2";
            service2.DelegateName = "EmpSalary2AutoExcel";
            service2.NonLogin = false;
            service2.ServiceName = "EmpSalary2AutoExcel";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
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
            this.View_EmployeeSalary.EEPAlias = "JBADMIN";
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
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View_EmployeeSalary)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand View_EmployeeSalary;
    }
}
