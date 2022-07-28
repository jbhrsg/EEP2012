using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sFWCRMARMaster
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

        //EEP外勞輸入居留證號查應收使用
        public object[] procReportARMaster(object[] objParam)
        {
            string ResidenceID = objParam[0].ToString();           
            string js = string.Empty;
            string sLoginDB = "FWCRM";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportARMasterEEPMaster '" + ResidenceID + "'";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();


                //string SQL = "exec procReportARMasterEEPMaster '" + ResidenceID + "'";
                //DataSet ds = this.ExecuteSql(SQL, connection, transaction);

                //string SQL2 = "exec procReportARMasterEEP '" + ResidenceID + "'";
                //if (ds.Tables[0].Rows[0]["iCount"].ToString() == "0")
                //{
                //    DataSet ds2 = this.ExecuteSql(SQL2, connection, transaction);
                //}
                //else
                //{
                //    DataSet ds2 = this.ExecuteSql(SQL2, connection, transaction);

                //}




            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }



    }
}
