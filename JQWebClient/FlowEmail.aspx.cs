using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EFClientTools.EFServerReference;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

public partial class FlowEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var key = this.Request.QueryString["key"];
        var param = this.Request.QueryString["param"];
        if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(param))
        {
            try
            {
                var clientInfo = new ClientInfo() { SecurityKey = key, UseDataSet = true };
                EFServiceClient client = EFClientTools.ClientUtility.Client;
                clientInfo = client.LogOn(clientInfo);
                var locale = Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : "en-us";
                clientInfo.Locale = locale;
                clientInfo.LogonResult = LogonResult.Logoned;
                Session["ClientInfo"] = clientInfo;

                parameter.Value = DecryptParameters(param, key);
            }
            catch 
            {
                Response.Redirect("Timeout.aspx", true);
            }
        }
        else
        {
            Response.Redirect("Timeout.aspx", true);
        }
    }

    private string DecryptParameters(string param, string key)
    {
        var paramters = Convert.FromBase64String(param);
        var encryptCodes = Convert.FromBase64String(key);
        for (int i = 0; i < paramters.Length; i++)
        {
            var encryptCode = encryptCodes[i % encryptCodes.Length];
            paramters[i] -= encryptCode;
        }
        var hash = paramters.Take(16).ToArray();
        var info = paramters.Skip(16).ToArray();
        var md5 = new MD5CryptoServiceProvider();
        if (Convert.ToBase64String(md5.ComputeHash(info)) != Convert.ToBase64String(hash))
        {
            Response.Redirect("Timeout.aspx", true);
        }
        return Encoding.UTF8.GetString(info);
    }

    public override void ProcessRequest(HttpContext context)
    {
        if (!JqHttpHandler.ProcessRequest(context))
        {
            base.ProcessRequest(context);
        }
    }
}