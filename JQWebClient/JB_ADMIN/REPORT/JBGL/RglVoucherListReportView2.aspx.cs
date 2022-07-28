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
using System.Drawing.Printing;


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
                    ReportPath = "JB_ADMIN/REPORT/JBGL/glVoucherData.rdlc";//報表路徑
                }
                else if (iType == "1")//0轉帳傳票  1傳票清單 2日記帳 3分類帳  4總分類帳  5期間試算表
                {
                    ReportPath = "JB_ADMIN/REPORT/JBGL/glVoucherList.rdlc";//報表路徑
                }
                else if (iType == "2")
                {
                    ReportPath = "JB_ADMIN/REPORT/JBGL/DiaryBills.rdlc";//報表路徑
                }
                else if (iType == "3")
                {
                    ReportPath = "JB_ADMIN/REPORT/JBGL/ClassBills.rdlc";//報表路徑
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

                Export2();
                m_currentPageIndex = 0;
                PrintDocument pd = new PrintDocument();
                string printerName = "";
                printerName = printerName == "" ? pd.PrinterSettings.PrinterName : printerName;
                Print(printerName);
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
            ReportViewer report = sender as ReportViewer;
            var parameters = new List<ReportParameter>();
            //ReportParameter parameterQueryCondition = new ReportParameter("QueryCondition", Request.QueryString["WhereTextString"]);
            //ReportParameter parameterReportDate = new ReportParameter("ReportDate", DateTime.Today.ToString("yyyy/MM/dd"));
            //ReportParameter parameterReportDateTime = new ReportParameter("ReportDateTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            //ReportParameter parameterUserID = new ReportParameter("UserID", EFClientTools.ClientUtility.ClientInfo.UserID);
            //ReportParameter parameterUserName = new ReportParameter("UserName", EFClientTools.ClientUtility.ClientInfo.UserName);

            //var companyName = string.Empty;
            //var logoAddress = string.Empty;
            //if (!string.IsNullOrEmpty(EFClientTools.ClientUtility.ClientInfo.SDDeveloperID))
            //{
            //    var dataSet = DataSetHelper.GetSolutions(EFClientTools.ClientUtility.ClientInfo.SDDeveloperID); 
            //    var dataTable = dataSet.Tables[0];
            //    for (int i = 0; i < dataTable.Rows.Count; i++)
            //    {
            //        if (dataTable.Rows[i]["SolutionID"] != null && dataTable.Rows[i]["SolutionID"].ToString() == EFClientTools.ClientUtility.ClientInfo.Solution)
            //        {
            //            var images = dataTable.Rows[i]["Images"].ToString().Split(';');
            //            var reportLogo = images.FirstOrDefault(c => c.IndexOf("ReportLogo") >= 0);
            //            if (reportLogo != null && reportLogo.Split('=').Length == 2)
            //            {
            //                logoAddress = reportLogo.Split('=')[1];
            //            }

            //            var company = images.FirstOrDefault(c => c.IndexOf("CompanyName") >= 0);
            //            if (company != null && company.Split('=').Length == 2)
            //            {
            //                companyName = company.Split('=')[1];
            //            }
            //            break;
            //        }
            //    }
            //}
            //ReportParameter parameterCompanyName = new ReportParameter("CompanyName", companyName);

            //替換為本地地址
            //ReportParameter parameterLogo = new ReportParameter("Logo", logoAddress);
            //string ReportPath = Request.QueryString["ReportPath"];
            //if (ReportPath.IndexOf("rdlc/", StringComparison.OrdinalIgnoreCase) < 0)
            //{
            //    parameters.AddRange(new ReportParameter[] { parameterCompanyName, parameterQueryCondition, parameterReportDate
            //    ,parameterReportDateTime, parameterUserID, parameterUserName, parameterLogo});
            //}

            for (int i = 0; i < Request.QueryString.Keys.Count; i++)
            {
                var key = Request.QueryString.Keys[i];
                if (key != null && key.StartsWith("RP"))
                {
                    var value = Request.QueryString[key];
                    var parameter = new ReportParameter(key.Substring(2), value);
                    parameters.Add(parameter);
                }
            }

            report.LocalReport.SetParameters(parameters);
            report.LocalReport.Refresh();
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

    private int m_currentPageIndex;
    private IList<System.IO.Stream> m_streams;
    private void Export2()
    {
        Microsoft.Reporting.WebForms.Warning[] warnings;

        string deviceInfo =
          "<DeviceInfo>" +
          "  <OutputFormat>EMF</OutputFormat>" +
          "  <PageWidth>210mm</PageWidth>" +
          "  <PageHeight>297mm</PageHeight>" +
          "  <MarginTop>5mm</MarginTop>" +
          "  <MarginLeft>10mm</MarginLeft>" +
          "  <MarginRight>10mm</MarginRight>" +
          "  <MarginBottom>5mm</MarginBottom>" +
          "</DeviceInfo>";//这里是设置打印的格式 边距什么的
        m_streams = new List<System.IO.Stream>();
        try
        {
            ReportViewer1.LocalReport.Render("Image", deviceInfo, CreateStream, out warnings);//一般情况这里会出错的  使用catch得到错误原因  一般都是简单错误
            //byte[] bytes = ReportViewer1.LocalReport.Render("Image", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
        }
        catch (Exception ex)
        {
            Exception innerEx = ex.InnerException;//取内异常。因为内异常的信息才有用，才能排除问题。
            while (innerEx != null)
            {
                //MessageBox.Show(innerEx.Message);
                string errmessage = innerEx.Message;
                innerEx = innerEx.InnerException;
            }
        }
        foreach (System.IO.Stream stream in m_streams)
        {
            stream.Position = 0;
        }
    }

    private System.IO.Stream CreateStream(string name, string fileNameExtension, System.Text.Encoding encoding, string mimeType, bool willSeek)
    {
        //name 需要进一步处理
        System.IO.Stream stream = new System.IO.FileStream(name + DateTime.Now.Millisecond + "." + fileNameExtension, System.IO.FileMode.Create);//为文件名加上时间
        m_streams.Add(stream);
        return stream;
    }

    private void Print(string printerName)
    {
        //string printerName = this.TextBox1.Text.Trim();// "傳送至 OneNote 2007";
        if (m_streams == null || m_streams.Count == 0)
            return;
        System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
        // string aa = printDoc.PrinterSettings.PrinterName;
        if (printerName.Length > 0)
        {
            printDoc.PrinterSettings.PrinterName = printerName;
        }
        foreach (System.Drawing.Printing.PaperSize ps in printDoc.PrinterSettings.PaperSizes)
        {
            if (ps.PaperName == "A4")
            {
                printDoc.PrinterSettings.DefaultPageSettings.PaperSize = ps;
                printDoc.DefaultPageSettings.PaperSize = ps;
                // printDoc.PrinterSettings.IsDefaultPrinter;//知道是否是预设定的打印机
            }
        }
        if (!printDoc.PrinterSettings.IsValid)
        {
            string msg = String.Format("Can't find printer " + printerName);
            System.Diagnostics.Debug.WriteLine(msg);
            return;
        }
        printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPage);
        printDoc.Print();
    }


    private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
    {
        System.Drawing.Imaging.Metafile pageImage = new System.Drawing.Imaging.Metafile(m_streams[m_currentPageIndex]);
        ev.Graphics.DrawImage(pageImage, 0, 0, 827, 1169);//設置打印尺寸 单位是像素
        m_currentPageIndex++;
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
    }
}