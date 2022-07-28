using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EFClientTools.EFServerReference;
using Microsoft.Reporting.WebForms;
using Newtonsoft;
using Newtonsoft.Json;

public partial class MyPage_Customers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ClientQueryString != "")
            {
                var client = EFClientTools.ClientUtility.Client;
                string SalesTypeID = Request.QueryString["SalesTypeID"];
                string InvoiceDateFrom = Request.QueryString["InvoiceDateFrom"];
                string InvoiceDateTo = Request.QueryString["InvoiceDateTo"];
                string CustomerID = Request.QueryString["CustomerID"];
                string InvoiceNO = Request.QueryString["InvoiceNO"];
                string SalesID = Request.QueryString["SalesID"];

                string ReportPath = "";
                //if (OrderType == "1")
                //{
                ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_PrintReceipt.rdlc";//報表路徑
                //}
                //else ReportPath = "JB_ADMIN/REPORT/FWCRM/FWCRMOrders2.rdlc";//報表路徑

                var parameters = new List<object>();
                parameters.Add(SalesTypeID + "," + InvoiceDateFrom + "," + InvoiceDateTo + "," + CustomerID + "," + InvoiceNO + "," + SalesID);
                //回json string
                //回傳的obj是陣列，[0,json string]
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERP_Report_PrintReceipt", "GetReceiptFromInvoiceDetails", parameters);

                //ReportViewer1的模板設定
                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();//Collection<ReportDataSource>的所有元素清除

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());//把json字串轉成datatable

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();

                //try
                //{
                //    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("OrderType", OrderType) });
                //}
                //catch { }

                //把callmethod取回的資料塞到ReportViewer1裡
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dt));//Collection<ReportDataSource>增加ReportDataSource
                this.ReportViewer1.LocalReport.Refresh();
            }
        }
    }

    protected void SubreportProcessing(object sender, Microsoft.Reporting.WebForms.SubreportProcessingEventArgs e)
    {
        if (DataSource.InnerDataSet.Tables.Count > 1)//DataSource是WebDataSource ID="DataSource"
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", DataSource.InnerDataSet.Tables[1]));
    }//e.DataSources是ReportViewer1.LocalReport.DataSources

    protected void ReportViewer_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
}