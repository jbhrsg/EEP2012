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
            //if (this.ClientQueryString != "")
            //{
                var client = EFClientTools.ClientUtility.Client;


                //string sCustNO = Request.QueryString["sCustNO"];
                //string MailType = Request.QueryString["MailType"];

                //string sCustomerName = HttpUtility.UrlDecode(Request.QueryString["sCustomerName"].ToString(), System.Text.Encoding.UTF8);
                //string sTelNO = HttpUtility.UrlDecode(Request.QueryString["sTelNO"].ToString(), System.Text.Encoding.UTF8);
                //string sAddr_Desc = HttpUtility.UrlDecode(Request.QueryString["sAddr_Desc"].ToString(), System.Text.Encoding.UTF8);
                //string sZIPCode = HttpUtility.UrlDecode(Request.QueryString["sZIPCode"].ToString(), System.Text.Encoding.UTF8);
                //string sAccountClerk = HttpUtility.UrlDecode(Request.QueryString["sAccountClerk"].ToString(), System.Text.Encoding.UTF8);
                //string MailType = HttpUtility.UrlDecode(Request.QueryString["MailType"].ToString(), System.Text.Encoding.UTF8);

                //string[] aCustomerName = sCustomerName.Split(',');
                //string[] aTelNO = sTelNO.Split(',');
                //string[] aAddr_Desc = sAddr_Desc.Split(',');
                //string[] aZIPCode = sZIPCode.Split(',');
                //string[] aAccountClerk = sAccountClerk.Split(',');

                string ReportPath = "";
                ReportPath = "JB_ADMIN/REPORT/Media/Media_Report_BulkRegisteredMail.rdlc";//報表路徑

                var parameters = new List<object>();
                //parameters.Add(sCustNO);

                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sMedia_Report_BulkRegisteredMail", "GetAllData", parameters);

                //ReportViewer1的模板設定
                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();//Collection<ReportDataSource>的所有元素清除

                //DataTable dt = new DataTable();
                //dt.Columns.Add("CustomerName", typeof(string));
                //dt.Columns.Add("TelNO", typeof(string));
                //dt.Columns.Add("Addr_Desc", typeof(string));
                //dt.Columns.Add("ZIPCode", typeof(string));
                //dt.Columns.Add("AccountClerk", typeof(string));

                //for (int i = 0; i < aCustomerName.Length; i++) {
                //    dt.Rows.Add(new object[] { aCustomerName[i], aTelNO[i], aAddr_Desc[i], aZIPCode[i], aAccountClerk[i] });
                //}

                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());//把json字串轉成datatable

                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();

                try
                {
                    //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("MailType", MailType) });
                }
                catch { }

                //把callmethod取回的資料塞到ReportViewer1裡
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dt));//Collection<ReportDataSource>增加ReportDataSource
                this.ReportViewer1.LocalReport.Refresh();
            //}
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