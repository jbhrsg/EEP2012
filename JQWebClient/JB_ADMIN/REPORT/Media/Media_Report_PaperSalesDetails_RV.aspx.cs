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
                string CustNO = Request.QueryString["CustNO"];
                string SalesID = Request.QueryString["SalesID"];
                string SalesDateFrom = Request.QueryString["SalesDateFrom"];
                string SalesDateTo = Request.QueryString["SalesDateTo"];
                string NewsTypeID = Request.QueryString["NewsTypeID"];
                string NewsAreaID = Request.QueryString["NewsAreaID"];
                string NewsPublishID = Request.QueryString["NewsPublishID"];
                string ReportType = Request.QueryString["ReportType"];
                string GrantTypeID = Request.QueryString["GrantTypeID"];

                string ReportPath = "";
                if (ReportType == "1")
                {
                ReportPath = "JB_ADMIN/REPORT/Media/Media_Report_PaperSalesDetails_Date.rdlc";
                }
                else if (ReportType == "2")
                {
                    ReportPath = "JB_ADMIN/REPORT/Media/Media_Report_PaperSalesDetails_NewsType.rdlc";
                }
                else if (ReportType == "3")
                {
                    ReportPath = "JB_ADMIN/REPORT/Media/Media_Report_PaperSalesDetails_NewsPublish.rdlc";
                }
                else if (ReportType == "4")
                {
                    ReportPath = "JB_ADMIN/REPORT/Media/Media_Report_PaperSalesDetails_CustNO.rdlc";
                }
                else if (ReportType == "5")
                {
                    ReportPath = "JB_ADMIN/REPORT/Media/Media_Report_PaperSalesDetails_SalesID.rdlc";
                }

                var parameters = new List<object>();
                parameters.Add(CustNO + "," + SalesID + "," + SalesDateFrom + "," + SalesDateTo + "," + NewsTypeID + "," + NewsAreaID + "," + NewsPublishID + "," + ReportType + "," + GrantTypeID);
                //回json string

                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sMedia_Report_PaperSalesDetails", "GetReportData", parameters);

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
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("SalesDateFrom", SalesDateFrom) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("SalesDateTo", SalesDateTo) });
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