using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sRglVoucherList
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
        //傳票清單
        public object[] ReportglVoucherList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string VoucherID = parm[1].ToString();
            string VoucherNo = parm[2].ToString();
            string SDate = (DateTime.Parse(parm[3].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[4].ToString())).ToString("yyyy/MM/dd");
            int iType = int.Parse(parm[5].ToString().Trim());//呈現種類	1傳票清單 2日記帳 3分類帳  4總分類帳  5期間試算表

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
                if (iType == 1)
                {
                    string SQL = "exec procReportglVoucherList '" + CompanyID + "','" + VoucherID + "','" + VoucherNo + "','" + SDate + "','" + EDate + "'," + iType;
                }else
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
