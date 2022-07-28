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
                string CompanyID = Request.QueryString["CompanyID"];
                string VoucherID = Request.QueryString["VoucherID"];
                string VoucherNo = Request.QueryString["VoucherNo"];
                string CostCenterID = Request.QueryString["CostCenterID"];
                string JQDate1 = DateTime.Parse(Request.QueryString["SDate"]).ToShortDateString();
                string JQDate2 = DateTime.Parse(Request.QueryString["EDate"]).ToShortDateString();
                string Acno1 = Request.QueryString["Acno1"];
                string Acno2 = Request.QueryString["Acno2"];
                string SubAcno1 = Request.QueryString["SubAcno1"];
                string SubAcno2 = Request.QueryString["SubAcno2"];
                string iType = Request.QueryString["iType"];
                string TypeText = Request.QueryString["TypeText"];
                string CompanyText = Request.QueryString["CompanyText"];
                string sDate = JQDate1 +"~"+ JQDate2;

                string ReportPath = "";
                if (iType == "0")//0轉帳傳票  1傳票清單 2日記帳 3分類帳  4總分類帳  5期間試算表
                {
                    sDate = JQDate1;
                    ReportPath = "JBGL/REPORT/glVoucherData.rdlc";//報表路徑
                }
                else if (iType == "1")//0轉帳傳票  1傳票清單 2日記帳 3分類帳  4總分類帳  5期間試算表
                {
                    ReportPath = "JBGL/REPORT/glVoucherList.rdlc";//報表路徑
                }
                else if (iType == "2")
                {
                    ReportPath = "JBGL/REPORT/DiaryBills.rdlc";//報表路徑
                }
                else if (iType == "3")
                {
                    ReportPath = "JBGL/REPORT/ClassBills.rdlc";//報表路徑
                }
                

                var parameters = new List<object>();
                parameters.Add(CompanyID + "," + VoucherID + "," + VoucherNo + "," + CostCenterID + "," + Acno1 + "," + Acno2 + "," + SubAcno1 + "," + SubAcno2 + "," + JQDate1 + "," + JQDate2 + "," + iType);
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sRVoucherList", "ReportglVoucherList", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
               
                try
                {
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("CompanyID", CompanyID) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("TypeText", TypeText) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("CompanyText", CompanyText) });
                    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("sDate", sDate) });
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