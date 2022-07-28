using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EFClientTools.EFServerReference;
using Microsoft.Reporting.WebForms;
using Newtonsoft;
using Newtonsoft.Json;

public partial class MyPage_Customers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ClientQueryString != "")
            {
                var client = EFClientTools.ClientUtility.Client;

                string sCustNO = Request.QueryString["sCustNO"];

                string ReportPath = "";
                ReportPath = "JB_ADMIN/REPORT/Media/Media_Report_Envelope_VisitRecord.rdlc";//報表路徑

                var parameters = new List<object>();
                parameters.Add(sCustNO);

                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sMedia_Report_Envelope", "GetERPCustomers_VisitRecord", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();//Collection<ReportDataSource>的所有元素清除

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());//把json字串轉成datatable

                //把callmethod取回的資料塞到ReportViewer1裡
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dt));//Collection<ReportDataSource>增加ReportDataSource
                this.ReportViewer1.LocalReport.Refresh();
            }
        }
    }

    protected void SubreportProcessing(object sender, Microsoft.Reporting.WebForms.SubreportProcessingEventArgs e)
    {
        if (DataSource.InnerDataSet.Tables.Count > 1)//DataSource是WebDataSource ID="DataSource"
            e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", DataSource.InnerDataSet.Tables[1]));
    }//e.DataSources是ReportViewer1.LocalReport.DataSources

    protected void ReportViewer_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
}