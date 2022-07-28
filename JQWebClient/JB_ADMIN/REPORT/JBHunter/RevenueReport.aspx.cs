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
                string sType = Request.QueryString["sType"];//檔名
                string Type = Request.QueryString["Type"];
                string CustID = Request.QueryString["CustID"];
                string JobName = Request.QueryString["JobName"];
                string HunterID = Request.QueryString["HunterID"];
                string SalesTeamID = Request.QueryString["SalesTeamID"];
                string AssignHunterID = Request.QueryString["AssignHunterID"];
                string SADate = Request.QueryString["SADate"];
                string EADate = Request.QueryString["EADate"];
                string DraftS = Request.QueryString["DraftS"];
                string DraftE = Request.QueryString["DraftE"];

                // 1.執案預估營收 2.營收目標進度 
                string ReportPath = "JB_ADMIN/REPORT/JBHunter/Revenue1.rdlc";////報表路徑
                if (Type == "2")
                {
                    ReportPath = "JB_ADMIN/REPORT/JBHunter/Revenue2.rdlc";
                }
                

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                //------------------------------------------
                var parameters = new List<object>();
                parameters.Add(Type + "," + CustID + "," + JobName + "," + HunterID + "," + SalesTeamID + "," + SADate + "," + EADate + "," + DraftS + "," + DraftE + "," + AssignHunterID);

                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sHUTUser", "procReportRevenue", parameters);
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));


                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
               
                try
                {
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sType", sType) });

                }
                catch { }


                ReportViewer1.LocalReport.DisplayName = sType;
                this.ReportViewer1.LocalReport.Refresh();               
            }
        }
    }

    protected void SubreportProcessing(object sender, Microsoft.Reporting.WebForms.SubreportProcessingEventArgs e)
    {
        if (DataSource.InnerDataSet.Tables.Count > 1)
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", DataSource.InnerDataSet.Tables[1]));
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