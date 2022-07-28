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
           
        }
    }

    void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
    {
        //var client = EFClientTools.ClientUtility.Client;
        ////取得參數
        //string RecordNo = e.Parameters["pRecordNo"].Values[0];
        //string RecordType = e.Parameters["pRecordType"].Values[0];
        //string CompanyID = e.Parameters["pCompanyID"].Values[0];

        ////取得明細資料
        //var parameters = new List<object>();
        //parameters.Add(RecordNo + "," + RecordType + "," + 2);

        //var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sFWCRMServiceRecord", "ReportFWCRMServiceRecord", parameters);
        //DataTable dt = new DataTable();
        //dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
        //e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", dt));
        //繫結子報表
        //e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("sub_DataSet", sub_ds));
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.ClientQueryString != "")
        {
            var client = EFClientTools.ClientUtility.Client;
            string FileName = Request.QueryString["FileName"];
            string RecordNo = Request.QueryString["RecordNo"];
            string RecordType = Request.QueryString["RecordType"];//1雇主,2外勞
            string CompanyID = Request.QueryString["CompanyID"];//1傑報人力,2傑信管理,3傑信家服

            int iSort = int.Parse(RadioButtonList1.SelectedValue.Trim());//簽到名冊排序=> 1依入境日,2依外勞中文姓名,3依外勞英文姓名

            string ReportPath = "JB_ADMIN/REPORT/FWCRM/ServiceRecord1.rdlc";////報表路徑
            if (CompanyID == "3")//傑信家服(固定跑外勞表單)
            {
                ReportPath = "JB_ADMIN/REPORT/FWCRM/ServiceRecord2Care.rdlc";
            }
            else
            {
                if (RecordType == "2")
                {
                    //ReportPath = "JB_ADMIN/Form1.rdlc";
                    ReportPath = "JB_ADMIN/REPORT/FWCRM/ServiceRecord2.rdlc";
                }
            }

            this.ReportViewer1.LocalReport.ReportPath = ReportPath;
            this.ReportViewer1.LocalReport.DataSources.Clear();

            ////將SqlDataSource指定給報表前，必須加上一行指令，指定子報表事件處理常式。
            //ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

            //------------------------------------------
            var parameters = new List<object>();
            parameters.Add(RecordNo + "," + RecordType + "," + 1 + "," + iSort);

            var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sFWCRMServiceRecord", "ReportFWCRMServiceRecord", parameters);
            DataTable dt = new DataTable();
            dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());
            this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));

            //------------------------------------------
            var parameters2 = new List<object>();
            parameters2.Add(RecordNo + "," + RecordType + "," + 2 + "," + iSort);

            var obj2 = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sFWCRMServiceRecord", "ReportFWCRMServiceRecord", parameters2);
            DataTable dt2 = new DataTable();
            dt2 = JsonConvert.DeserializeObject<DataTable>(obj2.ToString());
            this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", dt2));

            string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();

            try
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("RecordType", RecordType) });
                //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("YearMonth", YearMonth) });

            }
            catch { }

            ReportViewer1.LocalReport.DisplayName = FileName;

            this.ReportViewer1.LocalReport.Refresh();
        }
    }
}