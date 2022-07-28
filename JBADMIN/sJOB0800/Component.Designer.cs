namespace sJOB0800
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
            Srvtools.Service service9 = new Srvtools.Service();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Component));
            Srvtools.KeyItem keyItem1 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem2 = new Srvtools.KeyItem();
            Srvtools.KeyItem keyItem3 = new Srvtools.KeyItem();
            this.serviceManager1 = new Srvtools.ServiceManager(this.components);
            this.InfoConnection1 = new Srvtools.InfoConnection(this.components);
            this.PublishingRecord = new Srvtools.InfoCommand(this.components);
            this.cmdIndustry = new Srvtools.InfoCommand(this.components);
            this.infoCity = new Srvtools.InfoCommand(this.components);
            this.infoTown = new Srvtools.InfoCommand(this.components);
            this.PublishingCount = new Srvtools.InfoCommand(this.components);
            this.infoPublishingList = new Srvtools.InfoCommand(this.components);
            this.cmdPublishingJob = new Srvtools.InfoCommand(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PublishingRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdIndustry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoTown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PublishingCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPublishingList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdPublishingJob)).BeginInit();
            // 
            // serviceManager1
            // 
            service1.DelegateName = "GePublishingRecordInfo";
            service1.NonLogin = false;
            service1.ServiceName = "GePublishingRecordInfo";
            service2.DelegateName = "GePublishingRecordInfoData";
            service2.NonLogin = false;
            service2.ServiceName = "GePublishingRecordInfoData";
            service3.DelegateName = "GePublishingCount";
            service3.NonLogin = false;
            service3.ServiceName = "GePublishingCount";
            service4.DelegateName = "GePublishingCountData";
            service4.NonLogin = false;
            service4.ServiceName = "GePublishingCountData";
            service5.DelegateName = "GePublishingList";
            service5.NonLogin = false;
            service5.ServiceName = "GePublishingList";
            service6.DelegateName = "PublishingListAutoExcel";
            service6.NonLogin = false;
            service6.ServiceName = "PublishingListAutoExcel";
            service7.DelegateName = "PublishingAutoExcel";
            service7.NonLogin = false;
            service7.ServiceName = "PublishingAutoExcel";
            service8.DelegateName = "ProcessPublishingJobFB";
            service8.NonLogin = false;
            service8.ServiceName = "ProcessPublishingJobFB";
            service9.DelegateName = "PublishingJobFBExcel";
            service9.NonLogin = false;
            service9.ServiceName = "PublishingJobFBExcel";
            this.serviceManager1.ServiceCollection.Add(service1);
            this.serviceManager1.ServiceCollection.Add(service2);
            this.serviceManager1.ServiceCollection.Add(service3);
            this.serviceManager1.ServiceCollection.Add(service4);
            this.serviceManager1.ServiceCollection.Add(service5);
            this.serviceManager1.ServiceCollection.Add(service6);
            this.serviceManager1.ServiceCollection.Add(service7);
            this.serviceManager1.ServiceCollection.Add(service8);
            this.serviceManager1.ServiceCollection.Add(service9);
            // 
            // InfoConnection1
            // 
            this.InfoConnection1.EEPAlias = "JOB0800";
            // 
            // PublishingRecord
            // 
            this.PublishingRecord.CacheConnection = false;
            this.PublishingRecord.CommandText = resources.GetString("PublishingRecord.CommandText");
            this.PublishingRecord.CommandTimeout = 30;
            this.PublishingRecord.CommandType = System.Data.CommandType.Text;
            this.PublishingRecord.DynamicTableName = false;
            this.PublishingRecord.EEPAlias = "JOB0800";
            this.PublishingRecord.EncodingAfter = null;
            this.PublishingRecord.EncodingBefore = "Windows-1252";
            this.PublishingRecord.EncodingConvert = null;
            this.PublishingRecord.InfoConnection = this.InfoConnection1;
            this.PublishingRecord.MultiSetWhere = false;
            this.PublishingRecord.Name = "PublishingRecord";
            this.PublishingRecord.NotificationAutoEnlist = false;
            this.PublishingRecord.SecExcept = null;
            this.PublishingRecord.SecFieldName = null;
            this.PublishingRecord.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PublishingRecord.SelectPaging = false;
            this.PublishingRecord.SelectTop = 0;
            this.PublishingRecord.SiteControl = false;
            this.PublishingRecord.SiteFieldName = null;
            this.PublishingRecord.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // cmdIndustry
            // 
            this.cmdIndustry.CacheConnection = false;
            this.cmdIndustry.CommandText = "SELECT  *\r\nFROM  Industry\r\nwhere IsEnable=1\r\norder by [Order]";
            this.cmdIndustry.CommandTimeout = 30;
            this.cmdIndustry.CommandType = System.Data.CommandType.Text;
            this.cmdIndustry.DynamicTableName = false;
            this.cmdIndustry.EEPAlias = "JOB0800";
            this.cmdIndustry.EncodingAfter = null;
            this.cmdIndustry.EncodingBefore = "Windows-1252";
            this.cmdIndustry.EncodingConvert = null;
            this.cmdIndustry.InfoConnection = this.InfoConnection1;
            keyItem1.KeyName = "Id";
            this.cmdIndustry.KeyFields.Add(keyItem1);
            this.cmdIndustry.MultiSetWhere = false;
            this.cmdIndustry.Name = "cmdIndustry";
            this.cmdIndustry.NotificationAutoEnlist = false;
            this.cmdIndustry.SecExcept = null;
            this.cmdIndustry.SecFieldName = null;
            this.cmdIndustry.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.cmdIndustry.SelectPaging = false;
            this.cmdIndustry.SelectTop = 0;
            this.cmdIndustry.SiteControl = false;
            this.cmdIndustry.SiteFieldName = null;
            this.cmdIndustry.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoCity
            // 
            this.infoCity.CacheConnection = false;
            this.infoCity.CommandText = "SELECT  *\r\nFROM City\r\nwhere IsEnable=1\r\norder by [Order]";
            this.infoCity.CommandTimeout = 30;
            this.infoCity.CommandType = System.Data.CommandType.Text;
            this.infoCity.DynamicTableName = false;
            this.infoCity.EEPAlias = "JOB0800";
            this.infoCity.EncodingAfter = null;
            this.infoCity.EncodingBefore = "Windows-1252";
            this.infoCity.EncodingConvert = null;
            this.infoCity.InfoConnection = this.InfoConnection1;
            keyItem2.KeyName = "Id";
            this.infoCity.KeyFields.Add(keyItem2);
            this.infoCity.MultiSetWhere = false;
            this.infoCity.Name = "infoCity";
            this.infoCity.NotificationAutoEnlist = false;
            this.infoCity.SecExcept = null;
            this.infoCity.SecFieldName = null;
            this.infoCity.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoCity.SelectPaging = false;
            this.infoCity.SelectTop = 0;
            this.infoCity.SiteControl = false;
            this.infoCity.SiteFieldName = null;
            this.infoCity.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoTown
            // 
            this.infoTown.CacheConnection = false;
            this.infoTown.CommandText = "SELECT  *\r\nFROM Town\r\nwhere IsEnable=1\r\norder by [Order]";
            this.infoTown.CommandTimeout = 30;
            this.infoTown.CommandType = System.Data.CommandType.Text;
            this.infoTown.DynamicTableName = false;
            this.infoTown.EEPAlias = "JOB0800";
            this.infoTown.EncodingAfter = null;
            this.infoTown.EncodingBefore = "Windows-1252";
            this.infoTown.EncodingConvert = null;
            this.infoTown.InfoConnection = this.InfoConnection1;
            keyItem3.KeyName = "Id";
            this.infoTown.KeyFields.Add(keyItem3);
            this.infoTown.MultiSetWhere = false;
            this.infoTown.Name = "infoTown";
            this.infoTown.NotificationAutoEnlist = false;
            this.infoTown.SecExcept = null;
            this.infoTown.SecFieldName = null;
            this.infoTown.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoTown.SelectPaging = false;
            this.infoTown.SelectTop = 0;
            this.infoTown.SiteControl = false;
            this.infoTown.SiteFieldName = null;
            this.infoTown.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // PublishingCount
            // 
            this.PublishingCount.CacheConnection = false;
            this.PublishingCount.CommandText = "\r\nselect p.TeamId,t.Name as TeamName,p.IndustryId,JobCount,0 as iCount,CityId,0 a" +
    "s TownId\r\nfrom Publishing p\r\n\tinner join Team t on p.TeamId=t.Id\r\nwhere 1=0";
            this.PublishingCount.CommandTimeout = 30;
            this.PublishingCount.CommandType = System.Data.CommandType.Text;
            this.PublishingCount.DynamicTableName = false;
            this.PublishingCount.EEPAlias = "JOB0800";
            this.PublishingCount.EncodingAfter = null;
            this.PublishingCount.EncodingBefore = "Windows-1252";
            this.PublishingCount.EncodingConvert = null;
            this.PublishingCount.InfoConnection = this.InfoConnection1;
            this.PublishingCount.MultiSetWhere = false;
            this.PublishingCount.Name = "PublishingCount";
            this.PublishingCount.NotificationAutoEnlist = false;
            this.PublishingCount.SecExcept = null;
            this.PublishingCount.SecFieldName = null;
            this.PublishingCount.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.PublishingCount.SelectPaging = false;
            this.PublishingCount.SelectTop = 0;
            this.PublishingCount.SiteControl = false;
            this.PublishingCount.SiteFieldName = null;
            this.PublishingCount.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // infoPublishingList
            // 
            this.infoPublishingList.CacheConnection = false;
            this.infoPublishingList.CommandText = resources.GetString("infoPublishingList.CommandText");
            this.infoPublishingList.CommandTimeout = 30;
            this.infoPublishingList.CommandType = System.Data.CommandType.Text;
            this.infoPublishingList.DynamicTableName = false;
            this.infoPublishingList.EEPAlias = "JOB0800";
            this.infoPublishingList.EncodingAfter = null;
            this.infoPublishingList.EncodingBefore = "Windows-1252";
            this.infoPublishingList.EncodingConvert = null;
            this.infoPublishingList.InfoConnection = this.InfoConnection1;
            this.infoPublishingList.MultiSetWhere = false;
            this.infoPublishingList.Name = "infoPublishingList";
            this.infoPublishingList.NotificationAutoEnlist = false;
            this.infoPublishingList.SecExcept = null;
            this.infoPublishingList.SecFieldName = null;
            this.infoPublishingList.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.infoPublishingList.SelectPaging = false;
            this.infoPublishingList.SelectTop = 0;
            this.infoPublishingList.SiteControl = false;
            this.infoPublishingList.SiteFieldName = null;
            this.infoPublishingList.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // cmdPublishingJob
            // 
            this.cmdPublishingJob.CacheConnection = false;
            this.cmdPublishingJob.CommandText = "select * from PublishingJob";
            this.cmdPublishingJob.CommandTimeout = 30;
            this.cmdPublishingJob.CommandType = System.Data.CommandType.Text;
            this.cmdPublishingJob.DynamicTableName = false;
            this.cmdPublishingJob.EEPAlias = "JOB0800";
            this.cmdPublishingJob.EncodingAfter = null;
            this.cmdPublishingJob.EncodingBefore = "Windows-1252";
            this.cmdPublishingJob.EncodingConvert = null;
            this.cmdPublishingJob.InfoConnection = this.InfoConnection1;
            this.cmdPublishingJob.MultiSetWhere = false;
            this.cmdPublishingJob.Name = "cmdPublishingJob";
            this.cmdPublishingJob.NotificationAutoEnlist = false;
            this.cmdPublishingJob.SecExcept = null;
            this.cmdPublishingJob.SecFieldName = null;
            this.cmdPublishingJob.SecStyle = Srvtools.SecurityStyle.ssByNone;
            this.cmdPublishingJob.SelectPaging = false;
            this.cmdPublishingJob.SelectTop = 0;
            this.cmdPublishingJob.SiteControl = false;
            this.cmdPublishingJob.SiteFieldName = null;
            this.cmdPublishingJob.UpdatedRowSource = System.Data.UpdateRowSource.None;
            ((System.ComponentModel.ISupportInitialize)(this.InfoConnection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PublishingRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdIndustry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoTown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PublishingCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPublishingList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdPublishingJob)).EndInit();

        }

        #endregion

        private Srvtools.ServiceManager serviceManager1;
        private Srvtools.InfoConnection InfoConnection1;
        private Srvtools.InfoCommand PublishingRecord;
        private Srvtools.InfoCommand cmdIndustry;
        private Srvtools.InfoCommand infoCity;
        private Srvtools.InfoCommand infoTown;
        private Srvtools.InfoCommand PublishingCount;
        private Srvtools.InfoCommand infoPublishingList;
        private Srvtools.InfoCommand cmdPublishingJob;
    }
}
