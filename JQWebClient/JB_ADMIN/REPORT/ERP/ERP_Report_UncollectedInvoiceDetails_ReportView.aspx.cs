using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EFClientTools.EFServerReference;
using Microsoft.Reporting.WebForms;
using Newtonsoft;
using Newtonsoft.Json;


public partial class HtmlPages_eva_customers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ClientQueryString != "")
            {                
                var client = EFClientTools.ClientUtility.Client;
                string InsGroupID = Request.QueryString["InsGroupID"];
                string SalesTypeID = Request.QueryString["SalesTypeID"];
                string SalesID = Request.QueryString["SalesID"];
                string SalesDateFrom = Request.QueryString["SalesDateFrom"];
                string SalesDateTo = Request.QueryString["SalesDateTo"];
                string CustomerID = Request.QueryString["CustomerID"];
                string ARDate = Request.QueryString["ARDate"];
                string ReportType = Request.QueryString["ReportType"];//報表格式	1日期 2客戶 3彙總
                string InsGroupName = Request.QueryString["InsGroupName"];
                string InvoiceDateFrom = Request.QueryString["InvoiceDateFrom"];
                string InvoiceDateTo = Request.QueryString["InvoiceDateTo"];
                string MediaSalesDateFrom = Request.QueryString["MediaSalesDateFrom"];
                string MediaSalesDateTo = Request.QueryString["MediaSalesDateTo"];

                string ReportPath = "";
                if (ReportType == "1")
                {
                    ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_UncollectedInvoiceDetails_Date.rdlc";//報表路徑
                }
                else if (ReportType == "2")
                {
                    ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_UncollectedInvoiceDetails_Customer.rdlc";
                }
                else if (ReportType == "3")
                {
                    ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_UncollectedInvoiceDetails_Summary.rdlc";
                }
                else if (ReportType == "4")
                {
                    ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_UncollectedInvoiceDetails_Media.rdlc";
                }

                var parameters = new List<object>();
                parameters.Add(InsGroupID + "," + SalesTypeID + "," + SalesID + "," + SalesDateFrom + "," + SalesDateTo + "," + CustomerID + "," + ARDate + "," + ReportType + "," + InvoiceDateFrom + "," + InvoiceDateTo + "," + MediaSalesDateFrom + "," + MediaSalesDateTo);
                //回json string
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERP_Report_UncollectedInvoiceDetails", "GetReportUncollectedInvoiceDetails", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                //把json string 轉成 DataTable
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0 && dt.Rows[i]["InvoiceNO"].ToString() == dt.Rows[i - 1]["InvoiceNO"].ToString())
                    {
                        dt.Rows[i]["SalesAmount"] = 0;
                        dt.Rows[i]["SalesTax"] = 0;
                        dt.Rows[i]["SalesTotal"] = 0;
                        dt.Rows[i]["AcceptedAmount"] = 0;
                        dt.Rows[i]["UncollectedAmount"] = 0;
                    }
                }


                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
                
                try
                {
                    string sUserName=EFClientTools.ClientUtility.ClientInfo.UserName.Trim();
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sUserName", sUserName) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("InsGroupName", InsGroupName) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sDateFrom", SalesDateFrom) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sDateTo", SalesDateTo) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sARDate", ARDate) });
                }
                catch { }

                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dt));
                this.ReportViewer1.LocalReport.Refresh();               
            }
        }
    }

    protected void SubreportProcessing(object sender, Microsoft.Reporting.WebForms.SubreportProcessingEventArgs e)
    {
        if (DataSource.InnerDataSet.Tables.Count > 1)
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", DataSource.InnerDataSet.Tables[1]));
    }

    protected void ReportViewer_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    private void Export()
    {
        //Export report file
        string mimeType, encoding, extension, deviceInfo;
        string[] streamids;
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string format = "PDF";
        //Desired format goes here (PDF, Excel, or Image)
        deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" + "</DeviceInfo>";
        byte[] bytes = ReportViewer1.LocalReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-disposition", "filename=output.pdf");

        Response.OutputStream.Write(bytes, 0, bytes.Length);
        Response.OutputStream.Flush();
        Response.OutputStream.Close();
        Response.Flush();
        Response.Close();
    }
}