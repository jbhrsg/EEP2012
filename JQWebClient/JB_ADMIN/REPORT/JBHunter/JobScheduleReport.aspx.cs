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
                string Type = Request.QueryString["Type"];
                string CustID = Request.QueryString["CustID"];
                string JobName = Request.QueryString["JobName"];
                string SalesTeamID = Request.QueryString["SalesTeamID"];
                string JobStatus = Request.QueryString["JobStatus"];
                string HunterID = Request.QueryString["HunterID"];
                string HunterIDAssist = Request.QueryString["HunterIDAssist"];
                string SDate = Request.QueryString["SDate"];
                string EDate = Request.QueryString["EDate"];
                string iDay1 = Request.QueryString["iDay1"];
                string iDay2 = Request.QueryString["iDay2"];
                string sType = Request.QueryString["sType"];//檔名
                string SADate = Request.QueryString["SADate"];
                string EADate = Request.QueryString["EADate"];
                string AssignID = Request.QueryString["AssignID"];
                string AssignHunterID = Request.QueryString["AssignHunterID"];
                // 1.執案作業進度表 2.推薦複試報到總表 3.執案人次表 4.執案分析表
                string ReportPath = "JB_ADMIN/REPORT/JBHunter/JobSchedule.rdlc";////報表路徑
                if (Type == "2")
                {
                    ReportPath = "JB_ADMIN/REPORT/JBHunter/JobSchedule2.rdlc";
                }
                else if (Type == "3")
                {
                    ReportPath = "JB_ADMIN/REPORT/JBHunter/JobSchedule3.rdlc";
                }
                else if (Type == "4")
                {
                    ReportPath = "JB_ADMIN/REPORT/JBHunter/JobSchedule4.rdlc";
                }

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                //------------------------------------------
                var parameters = new List<object>();
                parameters.Add(Type + "," + CustID + "," + JobName + "," + SalesTeamID + "," + JobStatus + "," + HunterID + "," + HunterIDAssist + "," + SDate + "," + EDate + "," + iDay1 + "," + iDay2 + "," + SADate + "," + EADate + "," + AssignID + "," + AssignHunterID);

                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sHUTUser", "procReportJobSchedule", parameters);
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));

                //if (Type == "3")
                //{
                //var parameters2 = new List<object>();
                //parameters2.Add(4 + "," + CustID + "," + JobName + "," + SalesTeamID + "," + JobStatus + "," + HunterID + "," + HunterIDAssist + "," + SDate + "," + EDate + "," + iDay1 + "," + iDay2 + "," + SADate + "," + EADate);

                //var obj2 = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sHUTUser", "procReportJobSchedule", parameters2);
                //DataTable dt2 = new DataTable();
                //dt2 = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
                //this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("JobSchedule33", dt2));
                //}

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
               
                try
                {
                    //----JobStatus---1開,2關,0無
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("JobStatus", JobStatus) });

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