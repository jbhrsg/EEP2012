using System;
using System.Text;
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
using System.IO;
using iTextSharp.text.pdf;
using System.Net.Mail;


public partial class HtmlPages_eva_customers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ClientQueryString != "")
            {
                string ReportPath = "";

                var client = EFClientTools.ClientUtility.Client;
                string companyCode = Request.QueryString["companyCode"];
                string companyName = Request.QueryString["companyName"];
                string employeeCode = Request.QueryString["employeeCode"];
                string employeeName = Request.QueryString["employeeName"];
                string deptCode = Request.QueryString["deptCode"];
                string salaryYYMM = Request.QueryString["salaryYYMM"];
                string salarySeq = Request.QueryString["salarySeq"];
                string transferDate = Request.QueryString["transferDate"];
                string attendDateBegin = Request.QueryString["attendDateBegin"];
                string attendDateEnd = Request.QueryString["attendDateEnd"];
                string effectDate = Request.QueryString["effectDate"];
                string dataType = Request.QueryString["dataType"];
                string reportType = Request.QueryString["reportType"];
                string memo = Request.QueryString["memo"];
                string exportToExcel = Request.QueryString["exportToExcel"];
                string isdispalycompany = Request.QueryString["isdispalycompany"];
                string isdispalydept = Request.QueryString["isdispalydept"];
                string isdispalyTitle = Request.QueryString["isdispalyTitle"];
                string isEmployeeData = Request.QueryString["isEmployeeData"];
                string Language = Request.QueryString["Language"];
                string deptCost = Request.QueryString["deptCost"];
                string identityType = Request.QueryString["identityType"];
                string workCode = Request.QueryString["workCode"];
                string deptType = Request.QueryString["deptType"];
                string BankTranserId = Request.QueryString["BankTranserId"];

                string sMailServerName = "";
                bool bIsUseDefaultCredentials = false;
                string sFromID = "";
                string sFromPW = "";
                int iPort = 25;
                bool bSSL = true;
                string mailtitle = Request.QueryString["mailtitle"];
                string body = Request.QueryString["body"];
                //string mailtitle = salaryYYMM.Substring(0, 4) + "年" + salaryYYMM.Substring(4, 2) + "月薪資單";
                //string body = "本月薪資如附件請參考，謝謝。請使用PDF軟體閱讀，如沒有該軟體請上公司首頁下載。薪資檔案密碼是您的身分證號後四碼";

                var pathParameters = new List<object>();
                switch (reportType)
                {
                    case "1":
                    case "12":
                    case "2":
                    case "21":
                    case "22":
                        pathParameters.Clear();
                        pathParameters.Add("HRM_Salary_Report_Salary");
                        if (isdispalyTitle == "Y")
                            pathParameters.Add("SalaryDeptDetail");
                        else
                            pathParameters.Add("SalaryDeptDetailNoTitle");
                        var pathObj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_System_Share", "getReportPath", pathParameters);
                        DataTable pathDt = JsonConvert.DeserializeObject<DataTable>(pathObj.ToString());

                        if (pathDt.Rows.Count > 0)
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + ".rdlc";
                        else
                            if (isdispalyTitle == "Y")
                                ReportPath = "JB_HRIS_Page/REPORT/SALARY/SalaryDeptDetail.rdlc";
                            else
                                ReportPath = "JB_HRIS_Page/REPORT/SALARY/SalaryDeptDetailNoTitle.rdlc";
                        break;
                    case "3":
                    case "4":
                        if (isdispalyTitle == "Y")
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/SalaryDeptSummary.rdlc";
                        else
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/SalaryDeptSummaryNoTitle.rdlc";
                        break;
                    case "5":
                    case "51":
                        //ReportPath = "JB_HRIS_Page/REPORT/SALARY/TransferBankDetail.rdlc";
                        pathParameters.Clear();
                        pathParameters.Add("HRM_Salary_Report_Salary");
                        if (isdispalyTitle == "Y")
                        {
                            if (isEmployeeData == "Y")
                            {

                                pathParameters.Add("TransferBankDetail");
                            }
                            else
                            {

                                pathParameters.Add("TransferBankDetailNoemployee");
                            }
                        }
                        else
                        {
                            if (isEmployeeData == "Y")
                            {

                                pathParameters.Add("TransferBankDetailNoTitle");
                            }
                            else
                            {

                                pathParameters.Add("TransferBankDetailNoTitleNoemployee");
                            }
                        }
                        pathObj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_System_Share", "getReportPath", pathParameters);
                        pathDt = JsonConvert.DeserializeObject<DataTable>(pathObj.ToString());

                        if (pathDt.Rows.Count > 0)
                        {
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + ".rdlc";
                        }
                        else
                        {
                            if (isdispalyTitle == "Y")
                            {
                                if (isEmployeeData == "Y")
                                {

                                    ReportPath = "JB_HRIS_Page/REPORT/SALARY/TransferBankDetail.rdlc";
                                }
                                else
                                {
                                    ReportPath = "JB_HRIS_Page/REPORT/SALARY/TransferBankDetailNoemployee.rdlc";
                                }
                            }
                            else
                            {
                                if (isEmployeeData == "Y")
                                {
                                    ReportPath = "JB_HRIS_Page/REPORT/SALARY/TransferBankDetailNoTitle.rdlc";
                                }
                                else
                                {
                                    ReportPath = "JB_HRIS_Page/REPORT/SALARY/TransferBankDetailNoTitleNoemployee.rdlc";
                                }

                            }
                        }
                        break;
                    case "6":
                        if (isdispalyTitle == "Y")
                        {
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/CashDetail.rdlc";
                        }
                        else
                        {
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/CashDetailNoTitle.rdlc";
                        }
                        break;
                    case "8":
                        ReportPath = "JB_HRIS_Page/REPORT/SALARY/SalaryPeopleCount.rdlc";
                        break;
                    case "9":
                    case "10":
                        pathParameters.Clear();
                        pathParameters.Add("HRM_Salary_Report_Salary");
                        pathParameters.Add("SalaryPayroll");
                        pathObj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_System_Share", "getReportPath", pathParameters);
                        pathDt = JsonConvert.DeserializeObject<DataTable>(pathObj.ToString());

                        if (pathDt.Rows.Count > 0)
                        {
                            if (Language == "VN")
                            {
                                ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + "_VN.rdlc";
                                string checkPath = Server.MapPath("../SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + "_VN.rdlc");
                                if (!File.Exists(checkPath)) //查無檔案
                                    ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + ".rdlc";
                            }
                            else if (Language == "CNUS")
                            {
                                ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + "_CNUS.rdlc";
                                string checkPath = Server.MapPath("../SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + "_CNUS.rdlc");
                                if (!File.Exists(checkPath)) //查無檔案
                                    ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + ".rdlc";
                            }
                            else if (Language == "IN")
                            {
                                ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + "_IN.rdlc";
                                string checkPath = Server.MapPath("../SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + "_IN.rdlc");
                                if (!File.Exists(checkPath)) //查無檔案
                                    ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + ".rdlc";
                            }
                            else
                                ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + ".rdlc";
                        }
                        else
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/SalaryPayroll.rdlc";
                        break;

                    case "11":
                        pathParameters.Clear();
                        pathParameters.Add("HRM_Salary_Report_Salary");
                        pathParameters.Add("SalaryDeptDetail");
                        pathObj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_System_Share", "getReportPath", pathParameters);
                        pathDt = JsonConvert.DeserializeObject<DataTable>(pathObj.ToString());

                        if (pathDt.Rows.Count > 0)
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/" + pathDt.Rows[0]["RDLC_TO"].ToString() + ".rdlc";
                        else
                            ReportPath = "JB_HRIS_Page/REPORT/SALARY/SalaryDeptDetail.rdlc";
                        break;
                }

                var parameters = new List<object>();
                parameters.Add(companyCode + "," + companyName + "," + employeeCode + "," + employeeName + "," + deptCode + "," + salaryYYMM + "," + salarySeq + "," + transferDate + "," + attendDateBegin + "," + attendDateEnd + "," + effectDate + "," + dataType + "," + reportType + "," + memo + "," + exportToExcel + "," + isdispalycompany + "," + isdispalydept + "," + isdispalyTitle + "," + Language + "," + deptCost + "," + identityType + "," + workCode + "," + deptType + "," + isEmployeeData + "," + BankTranserId);

                DataTable dt = new DataTable();
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_Salary_Report_Share", "getSalaryData", parameters);
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString());

                if (reportType == "9" || reportType == "10")
                {
                    DataTable dtYearHolidaySalary = new DataTable();
                    DataTable dtRestHolidaySalary = new DataTable();

                    if (dt.Rows.Count > 0)
                    {
                        parameters = new List<object>();
                        parameters.Add(companyCode + "," + companyName + "," + employeeCode + "," + employeeName + "," + deptCode + "," + salaryYYMM + "," + salarySeq + "," + transferDate + "," + attendDateBegin + "," + dt.Rows[0]["ATTEND_END_DATE"].ToString() + "," + effectDate + "," + dataType + "," + reportType + "," + memo + "," + exportToExcel + "," + isdispalycompany + "," + isdispalydept + "," + Language + "," + deptCost + "," + identityType + "," + workCode + "," + deptType + "," + isEmployeeData + "," + BankTranserId);

                        var objYearHolidaySalary = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_Salary_Report_Share", "getYearHolidaySalary", parameters);
                        dynamic JsonYearHolidaySalary = JsonConvert.DeserializeObject(objYearHolidaySalary.ToString());
                        if (JsonYearHolidaySalary.IsOK.ToString() == "True")
                            dtYearHolidaySalary = JsonConvert.DeserializeObject<DataTable>(JsonYearHolidaySalary.Result.ToString());

                        var objRestHolidaySalary = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_Salary_Report_Share", "getRestHolidaySalary", parameters);
                        dynamic JsonRestHolidaySalary = JsonConvert.DeserializeObject(objRestHolidaySalary.ToString());
                        if (JsonRestHolidaySalary.IsOK.ToString() == "True")
                            dtRestHolidaySalary = JsonConvert.DeserializeObject<DataTable>(JsonRestHolidaySalary.Result.ToString());
                    }

                    dt.Columns.Add("YEAR_HOLIDAY_SALARY_AMT");
                    dt.Columns.Add("REST_HOLIDAY_SALARY_AMT");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dtYearHolidaySalary.Rows.Count > 0)
                        {
                            object drYearHolidaySalary = dtYearHolidaySalary.Compute("SUM(AMT)", "EMPLOYEE_CODE='" + dt.Rows[i]["EMPLOYEE_CODE"].ToString() + "'");
                            //if (drYearHolidaySalary.Length > 0)
                            dt.Rows[i]["YEAR_HOLIDAY_SALARY_AMT"] = drYearHolidaySalary;
                        }

                        if (dtRestHolidaySalary.Rows.Count > 0)
                        {
                            object drRestHolidaySalary = dtRestHolidaySalary.Compute("SUM(AMT)", "EMPLOYEE_CODE='" + dt.Rows[i]["EMPLOYEE_CODE"].ToString() + "'");
                            //if (drRestHolidaySalary.Length > 0) 
                            dt.Rows[i]["REST_HOLIDAY_SALARY_AMT"] = drRestHolidaySalary;
                        }
                    }
                }

                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();
                //this.ReportViewer1.Report.PageSettings.Landscape = true;    

                if (reportType == "10" || reportType == "11")   //薪資單-Mail員工 && 薪資單-Mail主管
                {
                    var mailParameter = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_Salary_Report_Share", "getMailParamerterData", parameters);

                    DataTable dtMailParameter = new DataTable();
                    dtMailParameter = JsonConvert.DeserializeObject<DataTable>(mailParameter.ToString());
                    foreach (DataRow drMailParameter in dtMailParameter.Rows)
                    {
                        switch (drMailParameter["PARAMETER_CODE"].ToString())
                        {
                            case "JbMail.host":   //郵件主機的位置
                                sMailServerName = drMailParameter["VALUE"].ToString();
                                break;
                            case "JbMail.IsNeedCredentials":   //驗證
                                bIsUseDefaultCredentials = bool.Parse(drMailParameter["VALUE"].ToString());
                                break;
                            case "JbMail.sys_mail":   //帳號
                                sFromID = drMailParameter["VALUE"].ToString();
                                break;
                            case "JbMail.sys_pwd":   //密碼
                                sFromPW = drMailParameter["VALUE"].ToString();
                                break;
                            case "JbMail.port":   //連線埠
                                iPort = int.Parse(drMailParameter["VALUE"].ToString());
                                break;
                            case "JbMail.EnableSsl":   //開啟SSL
                                bSSL = bool.Parse(drMailParameter["VALUE"].ToString());
                                break;
                        }
                    }

                    //將Table拆細
                    DataTable dbSplit = new DataTable();
                    dbSplit = dt.Clone();
                    string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();

                    if (reportType == "10") //薪資單-Mail員工
                    {
                        foreach (DataRow Row in dt.Rows)
                        {
                            if (Row["COMPANY_MAIL"].ToString() == "" || Row["COMPANY_MAIL"] == DBNull.Value)
                                continue;
                            dbSplit.Clear();
                            dbSplit.ImportRow(Row);
                            try
                            {
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("userName", userName) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("companyName", companyName) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("transferDate", transferDate) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("attendDateBegin", attendDateBegin) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("attendDateEnd", attendDateEnd) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("effectDate", effectDate) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("salaryYYMM", salaryYYMM) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("salarySeq", salarySeq) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("memo", memo) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("isdispalycompany", isdispalycompany) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("Language", Language) });
                            }
                            catch { }

                            this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dbSplit));
                            this.ReportViewer1.LocalReport.Refresh();
                            if (reportType == "10")
                                Export_Mail(dbSplit.Rows[0]["NAME_C"].ToString(), dbSplit.Rows[0]["PASSWORD"].ToString(), dbSplit.Rows[0]["COMPANY_MAIL"].ToString(), salaryYYMM, sMailServerName, bIsUseDefaultCredentials, sFromID, sFromPW, iPort, bSSL, mailtitle, body, reportType);
                            else
                                Export_Mail(dbSplit.Rows[0]["MANAGER_NAME_C"].ToString(), dbSplit.Rows[0]["PASSWORD"].ToString(), dbSplit.Rows[0]["COMPANY_MAIL"].ToString(), salaryYYMM, sMailServerName, bIsUseDefaultCredentials, sFromID, sFromPW, iPort, bSSL, mailtitle, body, reportType);
                        }

                        string SaveDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");//取得年月日
                        string dir = Server.MapPath(@"~/Files/JBHRIS/Error");
                        string file = SaveDate + ".log";
                        string fullpath = dir;
                        string fullname = fullpath + "/" + file;
                        if (System.IO.File.Exists(fullname))
                        {
                            mailtitle = mailtitle + " send mail error message";
                            body = "send mail error message as attached file";
                            SendMail(sMailServerName, iPort, sFromID, bIsUseDefaultCredentials, bSSL, sFromID, sFromPW, sFromID, mailtitle, body, fullpath, file, fullname);
                        }
                        //if (System.IO.File.Exists(fullname))   ////檢查目錄是否存在
                    }
                    //薪資單-Mail主管
                    else
                    {
                        string managerEmployeeID = "";
                        try
                        {
                            foreach (DataRow Row in dt.Rows)
                            {
                                if (managerEmployeeID != Row["MANAGER_EMPLOYEE_ID"].ToString())
                                {
                                    if (managerEmployeeID != "")
                                    {
                                        //this.ReportViewer1.SetPageSettings(new System.Drawing.Printing.PageSettings() { Landscape = true});
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("userName", userName) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("companyName", companyName) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("transferDate", transferDate) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("attendDateBegin", attendDateBegin) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("attendDateEnd", attendDateEnd) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("effectDate", effectDate) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("salaryYYMM", salaryYYMM) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("salarySeq", salarySeq) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("memo", memo) });
                                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("isdispalycompany", isdispalycompany) });

                                        this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dbSplit));
                                        this.ReportViewer1.LocalReport.Refresh();
                                        Export_Mail(mailtitle, dbSplit.Rows[0][" PASSWORD"].ToString(), dbSplit.Rows[0]["COMPANY_MAIL"].ToString(), salaryYYMM, sMailServerName, bIsUseDefaultCredentials, sFromID, sFromPW, iPort, bSSL, mailtitle, body, reportType);
                                    }
                                    dbSplit.Clear();
                                }
                                dbSplit.ImportRow(Row);
                                managerEmployeeID = Row["MANAGER_EMPLOYEE_ID"].ToString();
                            }
                            if (dbSplit.Rows.Count > 0)
                            {
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("userName", userName) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("companyName", companyName) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("transferDate", transferDate) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("attendDateBegin", attendDateBegin) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("attendDateEnd", attendDateEnd) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("effectDate", effectDate) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("salaryYYMM", salaryYYMM) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("salarySeq", salarySeq) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("memo", memo) });
                                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("isdispalycompany", isdispalycompany) });

                                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dbSplit));
                                this.ReportViewer1.LocalReport.Refresh();
                                Export_Mail(mailtitle, dbSplit.Rows[0]["PASSWORD"].ToString(), dbSplit.Rows[0]["COMPANY_MAIL"].ToString(), salaryYYMM, sMailServerName, bIsUseDefaultCredentials, sFromID, sFromPW, iPort, bSSL, mailtitle, body, reportType);
                            }
                        }   //try
                        catch { }
                    }
                }
                else
                {
                    string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();

                    //ReportParameter rp;
                    //rp = new ReportParameter("ReportParameter1", WhereTextString);
                    try
                    {
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("userName", userName) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("companyName", companyName) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("transferDate", transferDate) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("attendDateBegin", attendDateBegin) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("attendDateEnd", attendDateEnd) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("effectDate", effectDate) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("salaryYYMM", salaryYYMM) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("salarySeq", salarySeq) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("memo", memo) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("isdispalycompany", isdispalycompany) });
                        if (reportType == "9")
                            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("Language", Language) });
                        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("isdispalydept", isdispalydept) });
                        if (reportType == "51")
                            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("showTotal", "N") });
                        else
                            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { new ReportParameter("showTotal", "Y") });
                        //this.ReportViewer1.LocalReport.SetParameters(rp);
                    }
                    catch { }

                    this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", dt));
                    //this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("HolidaySalary", dtHolidaySalary));
                    this.ReportViewer1.LocalReport.Refresh();
                    //add by lu 2014.2.10 
                    //直接輸出成PDF，沒有預覽功能，要使用此功能請將下行註釋拿掉。
                    //Export();
                }
            }
        }
    }

    protected void SubreportProcessing(object sender, Microsoft.Reporting.WebForms.SubreportProcessingEventArgs e)
    {
        //if (DataSource.InnerDataSet.Tables.Count > 1)
        //{
        //    e.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NewDataSet", DataSource.InnerDataSet.Tables[1]));


        //var client = EFClientTools.ClientUtility.Client;
        //string companyCode = Request.QueryString["companyCode"];
        //string companyName = Request.QueryString["companyName"];
        //string employeeCode = Request.QueryString["employeeCode"];
        //string employeeName = Request.QueryString["employeeName"];
        //string deptCode = Request.QueryString["deptCode"];
        //string salaryYYMM = Request.QueryString["salaryYYMM"];
        //string salarySeq = Request.QueryString["salarySeq"];
        //string transferDate = Request.QueryString["transferDate"];
        //string attendDateBegin = Request.QueryString["attendDateBegin"];
        //string attendDateEnd = Request.QueryString["attendDateEnd"];
        //string effectDate = Request.QueryString["effectDate"];
        //string dataType = Request.QueryString["dataType"];
        //string reportType = Request.QueryString["reportType"];
        //string memo = Request.QueryString["memo"];
        //string exportToExcel = Request.QueryString["exportToExcel"];
        //string isdispalycompany = Request.QueryString["isdispalycompany"];
        //string isdispalydept = Request.QueryString["isdispalydept"];
        //string Language = Request.QueryString["Language"];
        //string mailtitle = Request.QueryString["mailtitle"];
        //string body = Request.QueryString["body"];


        //var parameters = new List<object>();
        //parameters.Add(companyCode + "," + companyName + "," + employeeCode + "," + employeeName + "," + deptCode + "," + salaryYYMM + "," + salarySeq + "," + transferDate + "," + attendDateBegin + "," + attendDateEnd + "," + effectDate + "," + dataType + "," + reportType + "," + memo + "," + exportToExcel + "," + isdispalycompany + "," + isdispalydept + "," + Language);

        //DataTable dtHolidaySalary = new DataTable();
        //var objHolidaySalary = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "_HRM_Salary_Report_Share", "getYearHolidaySalary", parameters);
        //dynamic Json = JsonConvert.DeserializeObject(objHolidaySalary.ToString());
        //if (Json.IsOK.ToString() == "True")
        //    dtHolidaySalary = JsonConvert.DeserializeObject<DataTable>(Json.Result.ToString());


        //ReportDataSource dataSource = new ReportDataSource("HolidaySalary", dtHolidaySalary);
        //e.DataSources.Add(dataSource);
        //}
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

    private void Export_Mail(string employeeName, string pwd, string companyMail, string salaryYYMM, string sMailServerName, bool bIsUseDefaultCredentials, string sFromID, string sFromPW, int iPort, bool bSSL, string mailtitle, string body, string reportType)
    {
        //Export report file
        string mimeType, encoding, extension, deviceInfo;
        string[] streamids;
        //companyMail = "serlina@jbjob.com.tw";
        Microsoft.Reporting.WebForms.Warning[] warnings;
        string format = "PDF";
        //Desired format goes here (PDF, Excel, or Image)
        if (reportType == "11")
            deviceInfo = "<DeviceInfo><PageHeight>11.69in</PageHeight><PageWidth>27in</PageWidth><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight></DeviceInfo>";
        else
            deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" + "</DeviceInfo>";
        byte[] bytes = ReportViewer1.LocalReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
        //string mailtitle = "test";
        //string body = "test";
        //string mailtitle = salaryYYMM.Substring(0, 4) + "年" + salaryYYMM.Substring(4, 2) + "月薪資單";
        //string body = "本月薪資如附件請參考，謝謝。請使用PDF軟體閱讀，如沒有該軟體請上公司首頁下載。薪資檔案密碼是您的身分證號後四碼";

        using (MemoryStream input = new MemoryStream(bytes))
        {
            using (MemoryStream output = new MemoryStream())
            {
                string password = pwd;
                PdfReader reader = new PdfReader(input);
                PdfEncryptor.Encrypt(reader, output, true, password, password, PdfWriter.ALLOW_SCREENREADERS);

                bytes = output.ToArray();

                string path = Server.MapPath(@"~/Files/JBHRIS/Temp");
                string fileName = "Payroll_" + employeeName + ".pdf";
                string tranFile = path + "\\Payroll_" + employeeName + ".pdf";
                //FileStream fs = new FileStream(Path+"/Payroll_" + Spot + ".pdf",FileMode.Create);
                FileStream fs = new FileStream(tranFile, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                SendMail(sMailServerName, iPort, sFromID, bIsUseDefaultCredentials, bSSL, sFromID, sFromPW, companyMail, mailtitle, body, path, fileName, tranFile);

                File.Delete(tranFile);
            }
        }
    }

    /// <summary>
    /// 傳送信件
    /// </summary>
    /// <param name="sMailServerName">郵件伺服器IP或Name</param>
    /// <param name="iPort">郵件伺服器Port</param>
    /// <param name="sFrom">寄件者Mail</param>
    /// <param name="bIsUseDefaultCredentials">False = 要驗証</param>
    /// <param name="bSSL">郵件伺服器是否需要SSL認證 True = 需要</param>
    /// <param name="sFromID">寄件者帳號(若是需要驗証,則就需要輸入寄件者帳號)</param>
    /// <param name="sFromPW">寄件者密碼(若是需要驗証,則就需要輸入寄件者密碼)</param>
    /// <param name="sTo">收件者Mail</param>
    /// <param name="sSubject">主旨</param>
    /// <param name="sBody">內文</param>
    /// <param name="sFlieName">檔名</param>
    public bool SendMail(string sMailServerName, int iPort, string sFrom, bool bIsUseDefaultCredentials, bool bSSL, string sFromID, string sFromPW, string sTo, string sSubject, string sBody, string tranPath, string sFlieName, string tranFile)
    {
        //sMailServerName = "IP";
        //sFrom = "mail";
        //bIsUseDefaultCredentials = true;
        //sFromID= "id";
        //sFromPW = "pw";

        try
        {
            using (MailMessage message =
                new MailMessage(sFrom, sTo, sSubject, sBody))
            {
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;
                message.BodyEncoding = System.Text.Encoding.Default;
                message.SubjectEncoding = System.Text.Encoding.Default;

                //附件
                //string strFilePath = Directory.GetCurrentDirectory() + SalaryPath + sFlieName;
                string strFilePath = tranPath;

                System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(tranFile);//添加附件 
                attachment1.Name = System.IO.Path.GetFileName(tranFile);
                attachment1.NameEncoding = System.Text.Encoding.Default;
                attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                attachment1.ContentDisposition.Inline = true;
                attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                string cid = attachment1.ContentId;
                message.Attachments.Add(attachment1);

                //message.Body = "<table width='100%'><tr><td><img src ='cid:" + cid + "'/></td></tr>" + content.Trim();

                SmtpClient mailClient = new SmtpClient(sMailServerName);
                mailClient.ServicePoint.MaxIdleTime = Convert.ToInt32(5000); //0.5 sec
                mailClient.Port = iPort;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //mailClient.Timeout = 10000;
                mailClient.EnableSsl = bSSL;

                if (bIsUseDefaultCredentials)
                {
                    mailClient.UseDefaultCredentials = true;
                    mailClient.Credentials = new System.Net.NetworkCredential(sFromID, sFromPW);
                }
                else
                    mailClient.UseDefaultCredentials = false;

                mailClient.Send(message);
                mailClient.Dispose();

                return true;
            }
        }
        catch (Exception ex)
        {
            string SaveDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");//取得年月日

            string dir = Server.MapPath(@"~/Files/JBHRIS/Error");
            string file = SaveDate + ".log";
            string fullpath = dir;
            string fullname = fullpath + "/" + file;
            if (!System.IO.Directory.Exists(fullpath))//檢查目錄是否存在，不存在就建立新目錄
                System.IO.Directory.CreateDirectory(fullpath);
            using (System.IO.FileStream ds = new System.IO.FileStream(fullname, System.IO.FileMode.Append))//寫入error.log
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(ds, Encoding.Default))
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append(DateTime.Now.ToString());
                    builder.Append("\u0009");

                    builder.Append(System.IO.Path.GetFileName(tranFile));
                    builder.Append("\u0009");

                    if (ex.Message != null)
                    {
                        builder.Append(ex.Message);
                        builder.Append("\u0009");
                    }
                    builder.Append(ex.StackTrace);
                    builder.Append("\u0009");

                    sw.WriteLine(builder);
                    Console.WriteLine(builder);
                    sw.Flush();
                    sw.Close();
                }
                ds.Close();
            }
            return false;
        }
    }
}