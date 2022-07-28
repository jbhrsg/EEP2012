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
        //�ǲ��M��
        public object[] ReportglVoucherList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string VoucherID = parm[1].ToString();
            string VoucherNo = parm[2].ToString();
            string SDate = (DateTime.Parse(parm[3].ToString())).ToString("yyyy/MM/dd");
            string EDate = (DateTime.Parse(parm[4].ToString())).ToString("yyyy/MM/dd");
            int iType = int.Parse(parm[5].ToString().Trim());//�e�{����	1�ǲ��M�� 2��O�b 3�����b  4�`�����b  5�����պ��

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
                if (iType == 1)
                {
                    string SQL = "exec procReportglVoucherList '" + CompanyID + "','" + VoucherID + "','" + VoucherNo + "','" + SDate + "','" + EDate + "'," + iType;
                }else
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
