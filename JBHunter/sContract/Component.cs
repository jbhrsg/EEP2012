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

namespace sContract
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
        public string GetContFixed()
        {
            DateTime datetime = DateTime.Today;
            return "HR" + ((datetime.Year) - 1911).ToString().Trim() + "HT";
        }
        public string GetOrderFixed()
        {
            DateTime datetime = DateTime.Today;
            return "HTPO" + ((datetime.Year) - 1911).ToString().Trim();
        }
        public object[] CheckDelContract(object[] objParam)
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
             string ContractNO = objParam[0].ToString();
             string sql = "SELECT COUNT(*) AS CNT FROM HUT_JOB WHERE (CONTRACTNO) = '" + ContractNO+ "'";
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
       //檢查合約號碼是否重複
        public object[] CheckContractNODul(object[] objParam)
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
                string ContractNO = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM HUT_CONTRACT WHERE CONTRACTNO = '" + ContractNO + "'";
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
           
                ucHUT_Customer.SetFieldValue("CreateDate", DateTime.Now);
                ucHUT_Customer.SetFieldValue("LastUpdateDate", DateTime.Now);
           
        }


        private void ucHUT_Contract_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
              ucHUT_Contract.SetFieldValue("CreateDate", DateTime.Now);
              ucHUT_Contract.SetFieldValue("LastUpdateDate", DateTime.Now);
           

        }

        private void ucHUT_Customer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
              ucHUT_Customer.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        private void ucHUT_Contract_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
               ucHUT_Contract.SetFieldValue("LastUpdateDate", DateTime.Now);
        }

        
     
     
    }
}
