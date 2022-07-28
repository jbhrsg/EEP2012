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
                string SalesID = Request.QueryString["SalesID"];
                string LastUpdateDate1 = Request.QueryString["LastUpdateDate1"];
                string LastUpdateDate2 = Request.QueryString["LastUpdateDate2"];
                string CustAddr = Request.QueryString["CustAddr"];//交易別
                string LatelyDayD1 = Request.QueryString["LatelyDayD1"];//報別
                string LatelyDayD2 = Request.QueryString["LatelyDayD2"];
                string SalesID2 = Request.QueryString["SalesID2"];
                string PostType = Request.QueryString["PostType"];
                string sFIELD = Request.QueryString["sFIELD"];
                string ReportPath = "JB_ADMIN/REPORT/Media/ERPCustMaintain.rdlc";//報表路徑

                var parameters = new List<object>();
                parameters.Add(SalesID + "," + LastUpdateDate1 + "," + LastUpdateDate2 + "," + CustAddr + "," + LatelyDayD1 + "," + LatelyDayD2 + "," + SalesID2 + "," + PostType + "," + sFIELD);
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERPCustMaintain", "ReportCustMaintain", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
               
                try
                {
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sFIELD", sFIELD) });
                    //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("SalesTypeID", SalesTypeID) }); 
                    //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("SalesTypeText", SalesTypeText) });                  
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