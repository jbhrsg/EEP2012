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
                string SDate = Request.QueryString["SDate"];
                string EDate = Request.QueryString["EDate"];
                string SalesEmployeeID = Request.QueryString["SalesEmployeeID"];
                string SalesTypeID = Request.QueryString["SalesTypeID"];
                string CustNO = Request.QueryString["CustNO"];
                string SalesTypeText = Request.QueryString["SalesTypeText"];
                string iClass = Request.QueryString["iClass"];//1排行榜	2數據分析	3銷貨明細 4月份別
                string sStats = Request.QueryString["sStats"];                
                string ReportPath = "";
                if (iClass == "1")
                {
                    ReportPath="JB_ADMIN/REPORT/Media/JBERP_R_SalesDetails3.rdlc";//報表路徑
                }
                else if (iClass == "2")
                {
                    ReportPath = "JB_ADMIN/REPORT/Media/JBERP_R_SalesDetails30.rdlc";//報表路徑
                }
                else if (iClass == "3")//顯示銷貨明細
                {
                    ReportPath = "JB_ADMIN/REPORT/Media/JBERP_R_SalesDetails31.rdlc";//報表路徑
                }
                else //各月份營業額
                {
                    ReportPath = "JB_ADMIN/REPORT/Media/JBERP_R_SalesDetails22.rdlc";//報表路徑
                }

                var parameters = new List<object>();
                parameters.Add(SDate + "*" + EDate + "*" + SalesEmployeeID + "*" + SalesTypeID + "*" + CustNO + "*" + iClass + "*" + sStats);
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sR_SalesDetails", "ReportSalesDetails3", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
               
                try
                {
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("SDate", SDate) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("EDate", EDate) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("SalesTypeID", SalesTypeID) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("SalesTypeText", SalesTypeText) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("iClass", iClass) });

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