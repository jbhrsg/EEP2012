using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template_JQuerySingle1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Template_JQuerySingle1));
    }

    public override void ProcessRequest(HttpContext context)
    {
        if (context.Request.Form["Method"] == "GetHref") { GetHref(context); }
        else if (!JqHttpHandler.ProcessRequest(context)) { base.ProcessRequest(context); }
    }

    public void GetHref(HttpContext context)
    {
        var aResult = new TheJsonResult();
        try
        {
            var ServiceReference = new infolightServiceReference.SingleSignOnSoapClient();            
            string sKey = ServiceReference.LogOn(EFClientTools.ClientUtility.ClientInfo.UserID, EFClientTools.ClientUtility.ClientInfo.Password, "JBHRIS", "JBHRD");
            aResult.TheResult = string.Format("http://www.jbhr.com.tw/JQWebClient/SingleSignOn.aspx?publickey={0}&RedirectUrl={1}", HttpUtility.UrlEncode(sKey), "MainPage_Flow.aspx");
            aResult.IsOK = true;
        }
        catch (Exception ex)
        {
            aResult.IsOK = false;
            aResult.Msg = ex.Message ;
        }
        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(aResult));
    }

    public class TheJsonResult
    {
        public bool IsOK { get; set; }

        public string Msg { get; set; }
		
		public string TheResult { get; set; }

        public TheJsonResult()
        {
            this.IsOK = false;
            this.TheResult = "";
        }
    }
}