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
                string InvoiceYM = Request.QueryString["InvoiceYM"];

                string ReportPath = "JB_ADMIN/REPORT/JCS1/InvoiceDetails.rdlc";//報表路徑

                //明細
                var parameters = new List<object>();
                parameters.Add(InsGroupID + "," + SalesTypeID + "," + InvoiceYM + ",1");
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERPInvoiceDetails", "GetReportInvoiceDetailsData", parameters);

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

               //總表
                var parameters2 = new List<object>();
                parameters2.Add(InsGroupID + "," + SalesTypeID + "," + InvoiceYM + ",2");
                var obj2 = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERPInvoiceDetails", "GetReportInvoiceDetailsData", parameters2);

                DataTable dt2 = new DataTable();
                dt2 = JsonConvert.DeserializeObject<DataTable>(obj2.ToString());

                //路徑
                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                try
                {
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("InvoiceYM", InvoiceYM) });

                }
                catch { }

                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("InvoiceDetails", dt));
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("InvoiceDetails2", dt2));

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