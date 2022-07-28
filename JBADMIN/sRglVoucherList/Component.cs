using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace TAG_NAMESPACE
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
        public object[] ReportglVoucherList2(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string VoucherID = parm[1].ToString();
            string VoucherNo = parm[2].ToString();
            string CostCenterID = parm[3].ToString();
            string Acno1 = parm[4].ToString();
            string Acno2 = parm[5].ToString();
            string SubAcno1 = parm[6].ToString();
            string SubAcno2 = parm[7].ToString();
            string SDate = (DateTime.Parse(parm[8].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[9].ToString())).ToString("yyyy/MM/dd");
            string iType = parm[10].ToString();//呈現種類	1傳票清單 2日記帳

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
                string SQL = "exec procReportglVoucherList '" + CompanyID + "','" + VoucherID + "','" + VoucherNo + "','" + CostCenterID + "','" +
                    Acno1 + "','" + Acno2 + "','" + SubAcno1 + "','" + SubAcno2 + "','" + SDate + "','" + EDate + "'," + iType;
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
