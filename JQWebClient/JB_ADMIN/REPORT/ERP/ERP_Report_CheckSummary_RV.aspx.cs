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
                string CustomerID = Request.QueryString["CustomerID"];
                string InsGroupID = Request.QueryString["InsGroupID"];
                string WarrantDateFrom = Request.QueryString["WarrantDateFrom"];
                string WarrantDateTo = Request.QueryString["WarrantDateTo"];
                string InsGroupName = Request.QueryString["InsGroupName"];
                string AccountID = Request.QueryString["AccountID"];
                string AccountName = Request.QueryString["AccountName"];
                string[] arrAccountName = AccountName.Split('*');
                AccountName = string.Join(",", arrAccountName);

                string ReportPath = "";
                if (AccountID == "")
                { ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_CheckSummary_Undue.rdlc"; }
                else if (AccountID != "")
                { ReportPath = "JB_ADMIN/REPORT/ERP/ERP_Report_CheckSummary_DueMonth.rdlc"; }

                var parameters = new List<object>();
                parameters.Add(CustomerID + "," + InsGroupID + "," + WarrantDateFrom + "," + WarrantDateTo + "," + AccountID);
                //回json string
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERP_Report_CheckSummary", "GetReportData", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                //把json string 轉成 DataTable
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                
                try
                {
                    string sUserName = EFClientTools.ClientUtility.ClientInfo.UserName.Trim();
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sUserName", sUserName) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sWarrantDateFrom", WarrantDateFrom) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sWarrantDateTo", WarrantDateTo) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sInsGroupName", InsGroupName) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sAccountName", AccountName) });
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