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
                string InvoiceDateFrom = Request.QueryString["InvoiceDateFrom"];
                string InvoiceDateTo = Request.QueryString["InvoiceDateTo"];
                string CustomerID = Request.QueryString["CustomerID"];
                string InvoiceTypeID = Request.QueryString["InvoiceTypeID"];
                string ReportType = Request.QueryString["ReportType"];//報表格式	1明細 2彙總 3類別
                string InsGroupName = Request.QueryString["InsGroupName"];

                string ReportPath = "";
                if (ReportType == "1")
                {
                ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_InvoiceDetails.rdlc";//報表路徑
                }
                else if (ReportType == "2")
                {
                    ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_InvoiceDetails_Summary.rdlc";//報表路徑
                }
                else if (ReportType == "3")
                {
                    ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_InvoiceDetails_Type.rdlc";//報表路徑
                }

                var parameters = new List<object>();
                parameters.Add(InsGroupID + "," + SalesTypeID + "," + SalesID + "," + InvoiceDateFrom + "," + InvoiceDateTo + "," + CustomerID + "," + InvoiceTypeID + "," + ReportType);
                //回json string
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERP_Report_InvoiceDetails", "GetReportInvoiceDetails", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                //把json string 轉成 DataTable
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
                
                try
                {
                    string sUserName=EFClientTools.ClientUtility.ClientInfo.UserName.Trim();
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sUserName", sUserName) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("InsGroupName", InsGroupName) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sDateFrom", InvoiceDateFrom) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sDateTo", InvoiceDateTo) });
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