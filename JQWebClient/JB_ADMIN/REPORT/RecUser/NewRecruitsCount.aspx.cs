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
                string sDate = Request.QueryString["sDate"];
                string eDate = Request.QueryString["eDate"];
                string SalesTeam = Request.QueryString["SalesTeam"];
                string ServiceConsultants = Request.QueryString["ServiceConsultants"];
                string ContactPeople = Request.QueryString["ContactPeople"];

                string sFileName = Request.QueryString["sFileName"];
                
                string ReportPath = "JB_ADMIN/REPORT/RecUser/NewRecruitsCount.rdlc";//報表路徑
                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                var parameters = new List<object>();
                parameters.Add(sDate + "," + eDate + "," + SalesTeam + "," + ServiceConsultants + "," + ContactPeople + "," + 1);
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_REC_Report_NewRecruitsCount", "procReportNewRecruitsCount", parameters);
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());


                var parameters2 = new List<object>();
                parameters2.Add(sDate + "," + eDate + "," + SalesTeam + "," + ServiceConsultants + "," + ContactPeople + "," + 2);
                var obj2 = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_REC_Report_NewRecruitsCount", "procReportNewRecruitsCount", parameters2);
                DataTable dt2 = new DataTable();
                dt2 = JsonConvert.DeserializeObject<DataTable>(obj2.ToString());


                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
               
                try
                {
                    //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("YM", YM) });
                    //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("CustID", CustID) }); 
                }
                catch { }

                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dt));
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet2", dt2));

                ReportViewer1.LocalReport.DisplayName = sFileName;
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