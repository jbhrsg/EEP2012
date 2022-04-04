using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;


namespace sCustomersJobs
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
        }

        public Component(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        //取得訂單編號固定碼
        public string GetOrderFixed()
        {
            DateTime datetime = DateTime.Today;
            return "HTPO" + ((datetime.Year) - 1911).ToString().Trim();
        }

        private void ucHUT_Job_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            int JobID = Convert.ToInt32(ucHUT_Job.GetFieldCurrentValue("JobID"));
            string CreateBy = Convert.ToString(ucHUT_Job.GetFieldCurrentValue("LastUpdateBy"));
            IPDJobEng("Insert",JobID);
            IPDJobNotify("Insert", JobID, CreateBy);
        }
        private void ucHUT_Job_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
             int JobID = Convert.ToInt32(ucHUT_Job.GetFieldCurrentValue("JobID"));
             string CreateBy = Convert.ToString(ucHUT_Job.GetFieldCurrentValue("LastUpdateBy"));
             IPDJobEng("Update",JobID);
             IPDJobNotify("Update", JobID,CreateBy);
        }
        private void ucHUT_Job_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
              int JobID = Convert.ToInt32(ucHUT_Job.GetFieldOldValue("JobID"));
              string CreateBy = Convert.ToString(ucHUT_Job.GetFieldOldValue("LastUpdateBy"));
              IPDJobEng("Delete",JobID);
              IPDJobNotify("Delete", JobID, CreateBy);
         }
        //將職缺語文狀態更新 Table JobLang
        private void IPDJobEng(string UpdateType,int JobID)
        {
            string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();
            if (!JobID.Equals(null))
            {
                string sql = "EXEC dbo.procUpdateJobLang  " + JobID.ToString() + ",'"+ UpdateType+ "' ," + UserID;
                this.ExecuteCommand(sql, ucHUT_Job.conn, ucHUT_Job.trans);
            }
        }
        //將職缺履歷通知人更新 Table HUT_JobNotify
        private void IPDJobNotify(string UpdateType, int JobID,string CreateBy) 
        {
            string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();
            if (!JobID.Equals(null))
            {
                string sql = "EXEC procUpdateJobNotify  " + JobID.ToString() + ",'" + UpdateType + "' ," + CreateBy;
                this.ExecuteCommand(sql, ucHUT_Job.conn, ucHUT_Job.trans);
            }
        }

        public object[] CheckDelCustomer(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string CustID = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM HUT_CONTRACT WHERE (CUSTID) = '" + CustID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        private void ucHUT_Customer_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_Customer.SetFieldValue("CreateDate",DateTime.Now);
            ucHUT_Customer.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucHUT_Customer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
             ucHUT_Customer.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucHUT_Job_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHUT_Job.SetFieldValue("CreateDate", DateTime.Now);
            ucHUT_Job.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucHUT_Job_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucHUT_Job.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucHUT_Job_AfterApplied(object sender, EventArgs e)
        {

        }
     
     }
}
