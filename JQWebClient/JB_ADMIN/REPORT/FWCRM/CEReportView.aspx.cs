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

public partial class JB_ADMIN_REPORT_FWCRM_CEReportView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ClientQueryString != "")
            {
                var client = EFClientTools.ClientUtility.Client;
                string ContinueEmployNO = Request.QueryString["ContinueEmployNO"];
                string IsRecontract = Request.QueryString["IsRecontract"];
                //string OrderType = Request.QueryString["OrderType"];//OrderType=>1入境,2承接,3轉單
                string ReportPath = "";
                if (IsRecontract == "1")
                {
                    ReportPath = "JB_ADMIN/REPORT/FWCRM/rERPContinueEmploy.rdlc";//續聘報表路徑
                }
                else ReportPath = "JB_ADMIN/REPORT/FWCRM/rERPContinueEmploy1.rdlc";//不續聘報表路徑


                var parameters = new List<object>();
                parameters.Add(ContinueEmployNO);
                parameters.Add(IsRecontract);
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERPContinueEmploy", "ReportOrders", parameters);

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());//把json字串轉成datatable

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();

                //try
                //{
                //    this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("OrderType", OrderType) });
                //}
                //catch { }

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
}