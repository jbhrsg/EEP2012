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
                string UserID = Request.QueryString["UserID"];
                string JobID = Request.QueryString["JobID"];
                string AutoKey = Request.QueryString["AutoKey"];
                string FileName = Request.QueryString["FileName"];//檔名

                string ReportPath = "JB_ADMIN/REPORT/JBHunter/PleasePayReport.rdlc";//報表路徑
                var parameters = new List<object>();
                parameters.Add(UserID + "," + JobID + "," + AutoKey );

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sHUTUser", "procReportPleasePay", parameters);
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                              
                try
                {

                }
                catch { }

                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("PleasePay", dt));

                ReportViewer1.LocalReport.DisplayName = FileName;
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