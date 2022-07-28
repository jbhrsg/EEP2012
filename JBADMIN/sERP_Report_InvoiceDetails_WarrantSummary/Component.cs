using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_InvoiceDetails_WarrantSummary
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

        public object[] GetReportInvoiceDetails(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string CustomerID = parm[0];
            string InsGroupID = parm[1];
            string SalesDate = parm[2];
            string ARDate = parm[3];
            string ReportType = parm[4];

            if (InsGroupID != "")
            {
                string[] arrInsGroupID = InsGroupID.Split('*');
                InsGroupID = string.Join(",", arrInsGroupID);
            }

            string sql = "";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                    sql = "SELECT id.[CustomerID],c.ShortName,id.[InsGroupID],id.[InvoiceNO],id.[SalesDate]"+"\r\n";
                    sql = sql + ",id.[SalesTotal],ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    if (ARDate != "")
                    {
                        sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//ARDate�����ڸ�ƪ����ڤ�I��ɦ���ڪ����B
                    }
                    sql = sql + "),0) as AcceptedAmount" + "\r\n";
                    sql = sql + ",SalesTotal - ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                    sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    if (ARDate != "")
                    {
                        sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//ARDate�����ڸ�ƪ����ڤ�I��ɥ�����ڪ����B
                    }
                    sql = sql + "),0) as UncollectedAmount" + "\r\n";
                    sql = sql+"FROM [InvoiceDetails] id"+"\r\n";
                    sql = sql+"left join Customer c on c.CustomerID=id.CustomerID"+"\r\n";
                    sql = sql + "where id.IsActive=1 " + "\r\n";

                    if (CustomerID != "")
                    {
                        sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
                    }
                    if (InsGroupID != "")
                    {
                        sql = sql + "and id.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                    }
                    if (SalesDate != "")
                    {
                        sql = sql + "and id.SalesDate <='" + SalesDate + "'" + "\r\n";
                    }
                    //if (ARDate != "")
                    //{
                    //    sql = sql + "and id.ARDate <= '" + ARDate + "'" + "\r\n";
                    //}
                    if (ReportType == "2")//�������o������
                    {
                        sql = sql + "and id.SalesTotal !=ISNULL(" + "\r\n";
                        sql = sql + "(select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) " + "\r\n";
                        sql = sql + "from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                        if (ARDate != "")
                        {
                        sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//ARDate�����ڸ�ƪ����ڤ�I��ɦ���ڪ����B
                        }
                        sql = sql + "),0)" + "\r\n";
                    }
                    sql = sql + "order by  id.[CustomerID],id.[SalesDate]" + "\r\n";

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
    }
}
