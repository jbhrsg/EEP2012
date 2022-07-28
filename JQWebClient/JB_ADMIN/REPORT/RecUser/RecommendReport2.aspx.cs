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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.ClientQueryString != "")
        {
            string sDyItem="";
            foreach (ListItem oItem in ckDy.Items)
            {

                //判斷oItem是否有被選擇
                if (oItem.Selected == true)
                {

                    ////判斷oVaules是否已有值
                    if (sDyItem.Length > 0)
                    {
                        //有就加,區分
                        sDyItem += "*";
                    }

                    //將oItem的值加入oValues
                    //sDyItem += oItem.Value;

                    sDyItem += "Isnull(" + oItem.Value + "*'''') as " + oItem.Value;
                }
            }

            var client = EFClientTools.ClientUtility.Client;
            string UserID = Request.QueryString["UserID"];
            string JobID = Request.QueryString["JobID"];
            string AutoKey = Request.QueryString["AutoKey"];
            string FileName = Request.QueryString["FileName"];//檔名
            //string sDyItem = Request.QueryString["sDyItem"];//動態勾選條件

            string ReportPath = "JB_ADMIN/REPORT/RecUser/RecommendReport.rdlc";//報表路徑
            var parameters = new List<object>();
            parameters.Add(UserID + "," + JobID + "," + AutoKey + "," + 1 + "," + sDyItem);

            this.ReportViewer1.LocalReport.ReportPath = ReportPath;
            this.ReportViewer1.LocalReport.DataSources.Clear();

            var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_REC_User_Management2", "procReportRecommendResume", parameters);
            DataTable dt = new DataTable();
            dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

            var parameters2 = new List<object>();
            parameters2.Add(UserID + "," + JobID + "," + AutoKey + "," + 2 + ",");
            var obj2 = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_REC_User_Management2", "procReportRecommendResume", parameters2);
            DataTable dt2 = new DataTable();
            dt2 = JsonConvert.DeserializeObject<DataTable>(obj2.ToString());

            string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();

            try
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("AutoKey", AutoKey) });

            }
            catch { }

            this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("REC_User", dt));
            this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("REC_UserCareer", dt2));

            ReportViewer1.LocalReport.DisplayName = FileName;
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
    protected void ckDy_PreRender(object sender, EventArgs e)
    {
        
    }
    protected void ckDy_DataBound(object sender, EventArgs e)
    {
        //預設勾選
        for (int i = 0; i < 12; i++)
        {
            ckDy.Items[i].Selected = true;
        }
    }
}