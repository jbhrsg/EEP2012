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
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net;
public partial class HtmlPages_JB_customers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ClientQueryString != "")
            {
                //設定Email相關資訊
                string InvoiceNO = Request.QueryString["InvoiceNO"];
                string UserID = Request.QueryString["UserID"];
                string To = Request.QueryString["ToMail"];
                string AccountClerk = Request.QueryString["ToName"];
                string DCou = Request.QueryString["DCou"];
                
                if (AccountClerk == ""){
                    AccountClerk = "客戶";
                }
                string MailServerName = "smtp.office365.com";        //MailServerName
                int iPort = 25;                                   //MailServer 阜號
                //string From = "service@jbjob.com.tw";               //寄件者EmailAddress
                bool IsUseDefaultCredentials = true;
                bool IsSSL = true;
                //string FromID = "sales@jbjob.com.tw";               //寄件者帳號
                //string FromPW = "ga^Y^c$UAJ";                       //寄件者密碼
                string FromID = "service@jbjob.com.tw";               //寄件者帳號
                string FromPW = "nmnvmqlxlfghvcqk";                   //寄件者密碼
                //產出PDF與設定Mail相關資訊
                string CurrentPath = Server.MapPath(@"~/JB_ADMIN");
                var client = EFClientTools.ClientUtility.Client;
                //報表RDLC位置,When 明細筆數<=3 then rInvoiceProofSheet.rdlc else rInvoiceProofSheet1.rdlc
                string ReportPath = "";
                if (Convert.ToInt32(DCou) <= 3){
                     ReportPath = "JB_ADMIN/rInvoiceProofSheet.rdlc";
                }
                else {
                     ReportPath = "JB_ADMIN/rInvoiceProofSheet1.rdlc";
                }
                var parameters = new List<object>();
                parameters.Add(InvoiceNO);
                var obj = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERPInvoiceEmail", "GetInvoiceByInvoiceNO", parameters);
                this.ReportViewer1.LocalReport.ReportPath = ReportPath;
                this.ReportViewer1.LocalReport.DataSources.Clear();
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(obj.ToString()); //把json string 轉成 DataTable
                string InvoiceYM = dt.Rows[0]["InvoiceYM"].ToString();
                //To = dt.Rows[0]["EmailAddress"].ToString();
                string TaxNO = dt.Rows[0]["TaxNO"].ToString();
                string CustomerName = dt.Rows[0]["CustomerName"].ToString();
                string SalesTypeName = dt.Rows[0]["SalesTypeName"].ToString();
                string InsGroupName = dt.Rows[0]["InsGroupName"].ToString();
                //string AccountClerk = dt.Rows[0]["AccountClerk"].ToString();
                string CYM = dt.Rows[0]["CYM"].ToString();
                string Subject = "致 "+CustomerName+"-"+SalesTypeName+"   "+CYM+"電子發票通知函"+"[發票號碼:"+InvoiceNO+"]";
                //string Body = GetMailBody(CustomerName,InvoiceNO);
                InvoiceYM = InvoiceYM.Substring(0, 4) + InvoiceYM.Substring(5, 2);
                string InvoicePdfPath = CurrentPath + "\\InvoiceSheet\\" + InvoiceYM;
                if (!Directory.Exists(InvoicePdfPath)){
                    DirectoryInfo di = Directory.CreateDirectory(InvoicePdfPath);
                }
                //PDF路徑檔案~\\JQWEBCLIENT\\JB_ADMIN\\InvoiceSheet\\201901\\84211021_WK2121212.pdf
                string fileName = TaxNO.Trim()+"_"+InvoiceNO.Trim() + ".pdf";
                string InvoicePdfPathFile = InvoicePdfPath + '\\' + fileName;
                string userName = EFClientTools.ClientUtility.ClientInfo.UserName.ToString();
                try
                {
                    string sUserName=EFClientTools.ClientUtility.ClientInfo.UserName.Trim();
                }
                catch { }
                this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("InvoiceDetails", dt));
                //產生PDF
                string status = ExportToFile("PDF", InvoicePdfPathFile.Trim());
                if (status == "ok"){
                    //寄出eMail
                    string Body = GetMailBody(InsGroupName, InvoiceNO, AccountClerk,CYM,SalesTypeName);
                    //寄出eMail
                    //bool SendOK = SendMail(MailServerName, iPort, FromID, IsUseDefaultCredentials, IsSSL, FromID, FromPW, To, Subject, Body, InvoicePdfPath, fileName, InvoicePdfPathFile, InsGroupName);
                    string MessStr = SendMail(MailServerName, iPort, FromID, IsUseDefaultCredentials, IsSSL, FromID, FromPW, To, Subject, Body, InvoicePdfPath, fileName, InvoicePdfPathFile, InsGroupName);
                    //if (MessStr == "OK")
                    //{
                         var para = new List<object>();
                         para.Add(InvoiceNO + "," + UserID + "," + To + "," + AccountClerk + "," + MessStr);
                        //呼叫存入Email紀錄
                         var obj1 = client.CallMethod(EFClientTools.ClientUtility.ClientInfo, "sERPInvoiceEmail", "CreateInvoiceLogs", para);
                    //}
                }
            }
        }
    }
   //寄送Email
    public string SendMail(string sMailServerName, int iPort, string sFrom, bool bIsUseDefaultCredentials, bool bSSL, string sFromID, string sFromPW, string sTo, string sSubject, string sBody, string tranPath, string sFlieName, string tranFile,string sFromName)
    {
       try
        {       
                //.NET Framework 支援TLS1.1,TLS1.2 通訊協定
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                string [] aTo = sTo.Split(',');
                string[] aToName = new string[(aTo.Length)];
                for (int i = 0; i < aTo.Length; i++){
                    if (aTo[i].Trim() != ""){
                        aToName[i] = aTo[i].Substring(0,aTo[i].IndexOf('@') - 0);
                    }
                }
                MailMessage message = new MailMessage();
                for (int i = 0; i < aToName.Length; i++){
                    message.To.Add(new MailAddress(aTo[i],aToName[i],Encoding.Default));
                }
                message.From = new MailAddress(sFrom, sFromName, Encoding.Default);
                message.Subject = sSubject;
                message.Body = sBody;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.Default;
                string strFilePath = tranPath;
                System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(tranFile);//添加附件 
                attachment1.Name = System.IO.Path.GetFileName(tranFile);
                attachment1.NameEncoding = System.Text.Encoding.Default;
                attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                attachment1.ContentDisposition.Inline = true;
                attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                string cid = attachment1.ContentId;
                message.Attachments.Add(attachment1);
                SmtpClient mailClient = new SmtpClient(sMailServerName);
                //mailClient.ServicePoint.MaxIdleTime = Convert.ToInt32(5000); //0.5 sec
                mailClient.Port = iPort;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
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
                return "OK";
            }
        catch (Exception ex)
        {
             return ex.Message + "(" + ex.InnerException.Message + ")";
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
    //產生PDF
    private string ExportToFile(string szFileType,string szFileName)
    {
        string sMSG = "ok";
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding = "utf_8";
        string extension;
        try
        {
            byte[] bytes = this.ReportViewer1.LocalReport.Render(szFileType.ToUpper(), null, out mimeType, out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(szFileName, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();

        }
        catch (Exception ex)
        {
            sMSG = ex.Message.ToString();
        }
        return sMSG;
   }
   //建出取得Email內文
   string GetMailBody(string FromCompany,string InvoiceNO,string AccountClerk,string CYM,string SalesTypeName)
    {
        string strToBody = "";
        DateTime dt = DateTime.Now;
        strToBody += "<script type='text/css'>a.button {-webkit-appearance: button;-moz-appearance: button;appearance: button;text-decoration: none;color: initial;}</script>" +
                       "<table border='0' width='100%' align='left'>" +
                       //"<tr><td align='left'><a  href='http://www.jbjob.com.tw/'><img src='http://www.jbhr.com.tw/jqwebclient/Files/JBERP_SalesPaper/logo.png'  alt='傑報人力資源服務集團' class='fusion-logo-1x fusion-standard-logo'></a>" +
                       "<tr><td align='left'><br/>親愛的<b>" + AccountClerk + "</b>您好;</td><tr>" +
                       "<tr><td align='left'>" + FromCompany.Trim() + " 已開立<label style='color:blue'>[" + CYM + '_' + SalesTypeName + "]</label>電子發票,發票號碼:" + InvoiceNO.Trim() + "</td></tr>" +
                       "<tr><td align='left'>請開啟附加檔案瀏覽您的電子發票</td></tr>" +
                       "<tr><td align='left'>若有發票內容相關問題,請洽業務人員(證明聯備註欄)</td></tr>" +
                       "<tr><td align='left'><br/></td></tr>" +
                       "<tr><td align='left'>若無法開啟附加檔案時,建議下載Acrobat Reader軟體開啟</td></tr>" +
                       "<tr><td align='left'>發送時間:" + dt.ToString() + "</td></tr>" +
                       "</table><br>";
        return strToBody;
    }
}