using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Report_InvoiceDetails_MonthSummary
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
            string SalesID = parm[1];
            string InvoiceDateFrom = parm[2];
            string InvoiceDateTo = parm[3];
            string InsGroupID = parm[4];
            string ARDate = parm[5];
            string ReportType = parm[6];
            string InvoiceTypeID = parm[7];
            
            if (InsGroupID != "")
            {
                string[] arrInsGroupID = InsGroupID.Split('*');
                InsGroupID = string.Join(",", arrInsGroupID);
            }
            if (SalesID != "")
            {
                string[] arrSalesID = SalesID.Split('*');
                SalesID = string.Join(",", arrSalesID);
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
                if (ReportType == "1" )//���������
                {
                    sql = "select InvoiceYearMonth,[QInvoiceType],sum([SalesTotal]) as SalesTotalSum,sum(AcceptedAmount) as AcceptedAmountSum,sum(UncollectedAmount) as UncollectedAmountSum" + "\r\n";
                    sql = sql + "from(" + "\r\n";
                        sql = sql + "select it.InvoiceTypeName as [QInvoiceType]" + "\r\n";
                        sql = sql + ",LEFT(CONVERT(VARCHAR, id.InvoiceDate, 120), 7) as InvoiceYearMonth" + "\r\n";
                        sql = sql + ",id.[SalesTotal],AcceptedAmount,(id.[SalesTotal]-AcceptedAmount) as UncollectedAmount" + "\r\n";
                        sql = sql + "from [InvoiceDetails] id" + "\r\n";
                        sql = sql + "inner join (" + "\r\n";
		                    sql = sql + "select id.InvoiceNO" + "\r\n";//�C�X�C�i�o�������ڪ��B(�C�i�o�����쪺���B)
                            sql = sql + ",ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                            if (ARDate != "")
                            {
                                sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//���ڤ�I����ڪ��B
                            }
                            sql = sql + "),0) as AcceptedAmount" + "\r\n";
		                    sql = sql + "FROM [InvoiceDetails] id" + "\r\n";
		                    sql = sql + "where id.IsActive=1 " + "\r\n";
                            if (InsGroupID != "")
                            {
                                sql = sql + "and id.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                            }
                            if (SalesID != "")
                            {
                                sql = sql + "and id.SalesID in (" + SalesID + ")" + "\r\n";
                            }
                            if (InvoiceDateFrom != "")
                            {
                                sql = sql + "and id.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
                            }
                            if (InvoiceDateTo != "")
                            {
                                sql = sql + "and id.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
                            }
                            if (CustomerID != "")
                            {
                                sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
                            }
                            if (InvoiceTypeID != "")
                            {
                                sql = sql + "and id.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
                            }
                            //if (ARDate != "")
                            //{
                            //    sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//���ڤ�I��ڪ����B
                            //}
                            //sql = sql + "group by id.InvoiceNO" + "\r\n";
                        sql = sql + ") t1 on t1.InvoiceNO=id.InvoiceNO" + "\r\n";
                        sql = sql + "left join InvoiceType it on it.InvoiceTypeID=id.QInvoiceType" + "\r\n";
	                sql = sql + ") t2" + "\r\n";
                    sql = sql + "group by InvoiceYearMonth,QInvoiceType" + "\r\n";
                    sql = sql + "order by InvoiceYearMonth,QInvoiceType" + "\r\n";
                }
                else if (ReportType == "2")//�����������
                {
                    sql = "select InvoiceYearMonth,sum([SalesTotal]) as SalesTotalSum,sum(AcceptedAmount) as AcceptedAmountSum,sum(UncollectedAmount) as UncollectedAmountSum" + "\r\n";
                    sql = sql + "from(" + "\r\n";
                        sql = sql + "select id.[QInvoiceType]" + "\r\n";
                        sql = sql + ",LEFT(CONVERT(VARCHAR, id.InvoiceDate, 120), 7) as InvoiceYearMonth" + "\r\n";
                        sql = sql + ",id.[SalesTotal],AcceptedAmount,(id.[SalesTotal]-AcceptedAmount) as UncollectedAmount" + "\r\n";
                        sql = sql + "from [InvoiceDetails] id" + "\r\n";
                        sql = sql + "inner join (" + "\r\n";
                            sql = sql + "select id.InvoiceNO" + "\r\n";//�C�X�C�i�o�������ڪ��B
                            sql = sql + ",ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                            if (ARDate != "")
                            {
                                sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//���ڤ�I����ڪ��B
                            }
                            sql = sql + "),0) as AcceptedAmount" + "\r\n";
                            sql = sql + "FROM [InvoiceDetails] id" + "\r\n";
                            sql = sql + "where id.IsActive=1 " + "\r\n";
                            if (InsGroupID != "")
                            {
                                sql = sql + "and id.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                            }
                            if (SalesID != "")
                            {
                                sql = sql + "and id.SalesID in (" + SalesID + ")" + "\r\n";
                            }
                            if (InvoiceDateFrom != "")
                            {
                                sql = sql + "and id.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
                            }
                            if (InvoiceDateTo != "")
                            {
                                sql = sql + "and id.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
                            }
                            if (CustomerID != "")
                            {
                                sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
                            }
                            if (InvoiceTypeID != "")
                            {
                                sql = sql + "and id.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
                            }
                        sql = sql + ") t1 on t1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    sql = sql + ") t2" + "\r\n";
                    sql = sql + "group by InvoiceYearMonth" + "\r\n";
                    sql = sql + "order by InvoiceYearMonth" + "\r\n";
                }
                else if (ReportType == "3")//�����q�O�B�Ȥ�
                {
                    sql = "select InvoiceYearMonth,InsGroupID,CustomerID,ShortName,sum([SalesTotal]) as SalesTotalSum,sum(AcceptedAmount) as AcceptedAmountSum" + "\r\n";
                    sql = sql + ",sum(UncollectedAmount) as UncollectedAmountSum" + "\r\n";
                    sql = sql + "from(" + "\r\n";
                        sql = sql + "select id.[QInvoiceType]" + "\r\n";
                        sql = sql + ",LEFT(CONVERT(VARCHAR, id.InvoiceDate, 120), 7) as InvoiceYearMonth,id.InsGroupID,id.CustomerID,c.ShortName" + "\r\n";
                        sql = sql + ",id.[SalesTotal],AcceptedAmount,(id.[SalesTotal]-AcceptedAmount) as UncollectedAmount" + "\r\n";
                        sql = sql + "from [InvoiceDetails] id" + "\r\n";
    //--left join Customer c on c.CustomerID=(case when id.Employer is null or id.Employer='' then id.CustomerID else id.Employer end)
                        sql = sql + "left join Customer c on c.CustomerID=id.CustomerID" + "\r\n";
                        sql = sql + "inner join (" + "\r\n";
                            sql = sql + "select id.InvoiceNO" + "\r\n";//�C�X�C�i�o�������ڪ��B
                            sql = sql + ",ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)) from WarrantDetails d1 left join WarrantMaster wm on wm.WarrantNO=d1.WarrantNO where d1.InvoiceNO=id.InvoiceNO" + "\r\n";
                            if (ARDate != "")
                            {
                                sql = sql + "and wm.WarrantDate <= '" + ARDate + "'" + "\r\n";//���ڤ�I����ڪ��B
                            }
                            sql = sql + "),0) as AcceptedAmount" + "\r\n";
                            sql = sql + "FROM [InvoiceDetails] id" + "\r\n";
                            sql = sql + "where id.IsActive=1" + "\r\n";
                            if (InsGroupID != "")
                            {
                                sql = sql + "and id.InsGroupID in (" + InsGroupID + ") " + "\r\n";
                            }
                            if (SalesID != "")
                            {
                                sql = sql + "and id.SalesID in (" + SalesID + ")" + "\r\n";
                            }
                            if (InvoiceDateFrom != "")
                            {
                                sql = sql + "and id.InvoiceDate >='" + InvoiceDateFrom + "'" + "\r\n";
                            }
                            if (InvoiceDateTo != "")
                            {
                                sql = sql + "and id.InvoiceDate <='" + InvoiceDateTo + "'" + "\r\n";
                            }
                            if (CustomerID != "")
                            {
                                sql = sql + "and id.CustomerID = '" + CustomerID + "'" + "\r\n";
                            }
                            if (InvoiceTypeID != "")
                            {
                                sql = sql + "and id.QInvoiceType = '" + InvoiceTypeID + "'" + "\r\n";
                            }
                        sql = sql + ") t1 on t1.InvoiceNO=id.InvoiceNO" + "\r\n";
                    sql = sql + ") t2" + "\r\n";
                    sql = sql + "group by InvoiceYearMonth,InsGroupID,CustomerID,ShortName" + "\r\n";
                    sql = sql + "order by InvoiceYearMonth,InsGroupID,CustomerID" + "\r\n";
                }
                    
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
