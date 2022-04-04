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
        if (!string.IsNullOrEmpty(Request.QueryString["ID"])) SetSinglePage();
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

    private void SetSinglePage()
    {
        string ID = Request.QueryString["ID"];

        //Default傳遞ID
        var default_ContactID = defaultMaster.Columns.FirstOrDefault(m => m.FieldName == "CONTACT_ID");
        if (default_ContactID != null) default_ContactID.DefaultValue = ID;

        //選人的關掉
        var column_ContactID = dataFormMaster.Columns.FirstOrDefault(m => m.FieldName == "CONTACT_ID");
        if (column_ContactID != null) column_ContactID.ReadOnly = true;

        //單人改成JQClientTools.EidtModeType.Switch
        JQDialog1.EditMode = JQClientTools.EidtModeType.Switch;
    }
}