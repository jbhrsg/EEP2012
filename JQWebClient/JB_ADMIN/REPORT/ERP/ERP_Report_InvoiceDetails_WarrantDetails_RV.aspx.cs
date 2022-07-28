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


public partial class HtmlPages_eva_customers1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ClientQueryString != "")
            {                
                var client = EFClientTools.ClientUtility.Client;
                string InsGroupID = Request.QueryString["InsGroupID"];
                string CustomerID = Request.QueryString["CustomerID"];
                string SalesDateFrom = Request.QueryString["SalesDateFrom"];
                string SalesDateTo = Request.QueryString["SalesDateTo"];
                string ARDateFrom = Request.QueryString["ARDateFrom"];
                string ARDateTo = Request.QueryString["ARDateTo"];
                string SalesTypeID = Request.QueryString["SalesTypeID"];
                string SalesID = Request.QueryString["SalesID"];
                string ReportType = Request.QueryString["ReportType"];
                //string PayWayID = Request.QueryString["PayWayID"];

                string ReportPath = "";
                
                ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_InvoiceDetails_WarrantDetails.rdlc";//報表路徑
                
                var parameters = new List<object>();
                parameters.Add(CustomerID + "," + SalesDateFrom + "," + SalesDateTo + "," + ARDateFrom + "," + ARDateTo + "," + SalesTypeID + "," + SalesID + "," + ReportType + "," + InsGroupID);//+ "," + PayWayID
                //回json string
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERP_Report_InvoiceDetails_WarrantDetails", "GetReportInvoiceDetails", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                //把json string 轉成 DataTable
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

                //string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
                
                try
                {
                    string sUserName = EFClientTools.ClientUtility.ClientInfo.UserName.Trim();
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sUserName", sUserName) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sSalesDateFrom", SalesDateFrom) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sSalesDateTo", SalesDateTo) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sARDateFrom", ARDateFrom) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sARDateTo", ARDateTo) });
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