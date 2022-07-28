using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sERPCustMaintain
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
        //客戶資料列表
        public object[] ReportCustomerToDoNotes(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesID = parm[0].ToString();
            string Date1 ="";
            if (parm[1].ToString() != "")
            {
                Date1 = (DateTime.Parse(parm[1].ToString())).ToString("yyyy/MM/dd");
            }
             string Date2 ="";
             if (parm[2].ToString() != "")
             {
                 Date2 = (DateTime.Parse(parm[2].ToString())).ToString("yyyy/MM/dd");
             }
         
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
                string SQL = "exec procReportCustomerToDoNotes '" + SalesID + "','" + Date1 + "','" + Date2 + "'";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }




    }
}
