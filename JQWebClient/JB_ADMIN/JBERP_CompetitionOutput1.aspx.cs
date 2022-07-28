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
        DateTime d1 = DateTime.Today;
        DateTime endDay = new DateTime(2020, 9, 30);
        TimeSpan diff1 = (endDay.Date - d1.Date);
        if (diff1.Days >= 0)
        {
            Label3.Text = "    距第三季末" + diff1.Days.ToString() + "天";
        }
        else if(diff1.Days < 0) {
            Label3.Text = "    第三季競賽開始";
        }
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Template_JQuerySingle1));
    }

    public override void ProcessRequest(HttpContext context)
    {
        if (!JqHttpHandler.ProcessRequest(context))
        {
            base.ProcessRequest(context);
        }
    }
}