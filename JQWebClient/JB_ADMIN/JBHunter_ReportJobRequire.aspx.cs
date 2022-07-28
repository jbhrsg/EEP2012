using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class Template_JQueryQuery1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    //    ReportParameter para1 = new ReportParameter("para1", "2014/01/26");
    //LocalReport.SetParameters(para1);
        
    }

    public override void ProcessRequest(HttpContext context)
    {
        if (!JqHttpHandler.ProcessRequest(context))
        {
            base.ProcessRequest(context);
        }
    }
}