using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sRVoucherList
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
            string CostCenterID = parm[3].ToString();
            string Acno1 = parm[4].ToString();
            string Acno2 = parm[5].ToString();
            string SubAcno1 = parm[6].ToString();
            string SubAcno2 = parm[7].ToString();
            string SDate = (DateTime.Parse(parm[8].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[9].ToString())).ToString("yyyy/MM/dd");
            int iType = int.Parse(parm[10].ToString().Trim());//呈現種類	 1傳票清單 2日記帳 3分類帳  4分類帳-不分頁  5分類帳-不分群組 6總分類帳-明細式 7總分類帳-統製式    

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
                string SQL = "";
                if (iType < 3)
                {
                    SQL = "exec procReportglVoucherList '" + CompanyID + "','" + VoucherID + "','" + VoucherNo + "','" + CostCenterID + "','" +
                        Acno1 + "','" + Acno2 + "','" + SubAcno1 + "','" + SubAcno2 + "','" + SDate + "','" + EDate + "'," + iType;
                }
                else if (iType >= 3)
                {
                    SQL = "exec procReportClassBills '" + CompanyID + "','" + VoucherID + "','" + VoucherNo + "','" + CostCenterID + "','" +
                        Acno1 + "','" + Acno2 + "','" + SubAcno1 + "','" + SubAcno2 + "','" + SDate + "','" + EDate + "'," + iType;
                }

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
        //損益表
        public object[] ReportProfitList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string VoucherID = parm[1].ToString();
            string CostCenterID = parm[2].ToString();
            string SDate = (DateTime.Parse(parm[3].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[4].ToString())).ToString("yyyy/MM/dd");
            string IsDiff = parm[5].ToString();
            string IsEng = parm[6].ToString();//英文版

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
                string SQL = "";
                if (IsEng == "0")
                {
                    SQL="exec procReportProfitList '" + CompanyID + "','" + VoucherID + "','" + CostCenterID + "','" +
                           SDate + "','" + EDate + "'," + IsDiff;
                }
                else
                {
                    SQL = "exec procReportProfitListEng '" + CompanyID + "','" + VoucherID + "','" + CostCenterID + "','" +
                           SDate + "','" + EDate + "'," + IsDiff;
                }

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

        //資產負債表
        public object[] ReportAssetDebt(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string VoucherID = parm[1].ToString();
            string EDate = (DateTime.Parse(parm[2].ToString())).ToString("yyyy/MM/dd");
            string iType = parm[3].ToString();//1報告式 2帳戶式
            string IsEng = parm[4].ToString();//英文版

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
                string SQL = "";
                if (IsEng == "0")
                {
                    SQL = "exec procReportAssetDebt1 '" + CompanyID + "','" + VoucherID + "','" + EDate+"'";
                }else
                {
                    SQL = "exec procReportAssetDebt1Eng '" + CompanyID + "','" + VoucherID + "','" + EDate + "'";
                }

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

        //各預估損益報表
        public object[] ReportEstimateProfit(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string JQDate1 = parm[0].ToString();
            string JQDate2 = parm[1].ToString();
            string CompanyID = parm[2].ToString();
            string VoucherID = parm[3].ToString();
            string CostCenterID = parm[4].ToString();
            string iType = parm[5].ToString();

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
                string SQL = "";
                //----1 各部門預估損益 ,2預估淨利  ,3 
                //if (iType == "1")////各部門預估損益
                //{
                SQL = "exec procReportEstimateProfit '" + JQDate1 + "','" + JQDate2 + "','" + CompanyID + "','" + VoucherID + "','" + CostCenterID + "'," + iType;
                //}                

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
