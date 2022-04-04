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
        //SetLanguage(JQMultiLanguage1);
        //PageLoadFunction();

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

        //Default�ǻ�ID
        var default_EmployeeID = defaultMaster.Columns.FirstOrDefault(m => m.FieldName == "EMPLOYEE_ID");
        if (default_EmployeeID != null) default_EmployeeID.DefaultValue = ID;

        //��H������
        var column_EmployeeCode = dataFormMaster.Columns.FirstOrDefault(m => m.FieldName == "EMPLOYEE_CODE");
        if (column_EmployeeCode != null) column_EmployeeCode.ReadOnly = true;

        //��H�令JQClientTools.EidtModeType.Switch
        JQDialog1.EditMode = JQClientTools.EidtModeType.Switch;
    }
}