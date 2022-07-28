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
                string AutoKey = Request.QueryString["AutoKey"];//履歷表=>AutoKey=0
                string FileName = Request.QueryString["FileName"];//檔名
                
                string ReportPath = "JB_ADMIN/REPORT/JBHunter/RecommendReport.rdlc";//報表路徑
                var parameters = new List<object>();
                parameters.Add(UserID + "," + JobID + "," + AutoKey + "," + 1);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sHUTUser", "procReportRecommendResume", parameters);
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

                var parameters2 = new List<object>();
                parameters2.Add(UserID + "," + JobID + "," + AutoKey + "," + 2);
                var obj2 = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sHUTUser", "procReportRecommendResume", parameters2);
                DataTable dt2 = new DataTable();
                dt2 = JsonConvert.DeserializeObject<DataTable>(obj2.ToString());

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
               
                try
                {
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("AutoKey", AutoKey) });

                }
                catch { }

                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("HUT_User", dt));
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("HUT_UserCareer", dt2));

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