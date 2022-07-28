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
        //�ǲ��M��
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
            int iType = int.Parse(parm[10].ToString().Trim());//�e�{����	 1�ǲ��M�� 2��O�b 3�����b  4�����b-������  5�����b-�����s�� 6�`�����b-���Ӧ� 7�`�����b-�λs��    

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
                //// Indented�Y�� �N����ഫ��Json�榡
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
        //�l�q��
        public object[] ReportProfitList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string VoucherID = parm[1].ToString();
            string CostCenterID = parm[2].ToString();
            string SDate = (DateTime.Parse(parm[3].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[4].ToString())).ToString("yyyy/MM/dd");
            string IsDiff = parm[5].ToString();
            string IsEng = parm[6].ToString();//�^�媩

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
                //// Indented�Y�� �N����ഫ��Json�榡
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

        //�겣�t�Ū�
        public object[] ReportAssetDebt(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string VoucherID = parm[1].ToString();
            string EDate = (DateTime.Parse(parm[2].ToString())).ToString("yyyy/MM/dd");
            string iType = parm[3].ToString();//1���i�� 2�b�ᦡ
            string IsEng = parm[4].ToString();//�^�媩

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
                //// Indented�Y�� �N����ഫ��Json�榡
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

        //�U�w���l�q����
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
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "";
                //----1 �U�����w���l�q ,2�w���b�Q  ,3 
                //if (iType == "1")////�U�����w���l�q
                //{
                SQL = "exec procReportEstimateProfit '" + JQDate1 + "','" + JQDate2 + "','" + CompanyID + "','" + VoucherID + "','" + CostCenterID + "'," + iType;
                //}                

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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
