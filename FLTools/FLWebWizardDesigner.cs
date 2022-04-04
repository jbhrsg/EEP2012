using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using EnvDTE;
using Srvtools;
#if VS90
using WebDevPage = Microsoft.VisualWebDeveloper.Interop.WebDeveloperPage;
#endif

namespace FLTools
{
    public class FLWebWizardDesigner : DataSourceDesigner
    {
        private DesignerActionListCollection _actionLists;
#if VS90
        private WebDevPage.DesignerDocument _designerDocument;
#endif
        private FLWebWizard _wiz = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            _wiz = this.Component as FLWebWizard;
        }

        private void GetDesignerHost()
        {
#if VS90
            if (_designerDocument == null)
            {
                object windowObject = EditionDifference.ActiveWindowObject();
                if (windowObject != null)
                {
                    HTMLWindow _htmlWindow = windowObject as HTMLWindow;

                    _htmlWindow.CurrentTab = vsHTMLTabs.vsHTMLTabsDesign;
                    if (_htmlWindow.CurrentTabObject is WebDevPage.DesignerDocument)
                    {
                        _designerDocument = _htmlWindow.CurrentTabObject as WebDevPage.DesignerDocument;
                    }
                }
            }
#endif
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                _actionLists = base.ActionLists;

                if (_actionLists != null)
                    _actionLists.Add(new FLWebWizardDesignerActionList(this.Component, this));
                return _actionLists;
            }
        }

        private bool ContainsColumn(GridView gdv, string bindingField)
        {
            foreach (DataControlField column in gdv.Columns)
            {
                if (column is BoundField && string.Compare(((BoundField)column).DataField, bindingField, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void GenFields()
        {
            if (_wiz != null && !string.IsNullOrEmpty(_wiz.BindingObjectID))
            {
                object bindingObject = this._wiz.GetObjByID(this._wiz.BindingObjectID);
                if (bindingObject != null && bindingObject is GridView)
                {
                    //WebDevPage.IHTMLElement gridElement = _designerDocument.webControls.item(_wiz.BindingObjectID, 0) as WebDevPage.IHTMLElement;
                    //if (gridElement != null)
                    //{
                    //    this.SetColumnsValue(gridElement, new string[] { "test1", "test2" });
                    //}
                    GridView gdv = bindingObject as GridView;
                    Dictionary<string, bool> fields = new Dictionary<string, bool>();
                    switch (_wiz.SqlMode)
                    {
                        case ESqlMode.ToDoList:
                        case ESqlMode.ToDoHis:
                        case ESqlMode.Notify:
                            #region todolist,todohis,notify fields
                            fields.Add("LISTID", this.ContainsColumn(gdv, "LISTID"));
                            fields.Add("FLOW_ID", this.ContainsColumn(gdv, "FLOW_ID"));
                            fields.Add("FLOW_DESC", this.ContainsColumn(gdv, "FLOW_DESC"));
                            fields.Add("APPLICANT", this.ContainsColumn(gdv, "APPLICANT"));
                            fields.Add("S_USER_ID", this.ContainsColumn(gdv, "S_USER_ID"));
                            fields.Add("S_STEP_ID", this.ContainsColumn(gdv, "S_STEP_ID"));
                            fields.Add("S_STEP_DESC", this.ContainsColumn(gdv, "S_STEP_DESC"));
                            fields.Add("D_STEP_ID", this.ContainsColumn(gdv, "D_STEP_ID"));
                            fields.Add("D_STEP_DESC", this.ContainsColumn(gdv, "D_STEP_DESC"));
                            fields.Add("EXP_TIME", this.ContainsColumn(gdv, "EXP_TIME"));
                            fields.Add("URGENT_TIME", this.ContainsColumn(gdv, "URGENT_TIME"));
                            fields.Add("TIME_UNIT", this.ContainsColumn(gdv, "TIME_UNIT"));
                            fields.Add("USERNAME", this.ContainsColumn(gdv, "USERNAME"));
                            fields.Add("FORM_NAME", this.ContainsColumn(gdv, "FORM_NAME"));
                            fields.Add("NAVIGATOR_MODE", this.ContainsColumn(gdv, "NAVIGATOR_MODE"));
                            fields.Add("FLNAVIGATOR_MODE", this.ContainsColumn(gdv, "FLNAVIGATOR_MODE"));
                            fields.Add("PARAMETERS", this.ContainsColumn(gdv, "PARAMETERS"));
                            fields.Add("SENDTO_KIND", this.ContainsColumn(gdv, "SENDTO_KIND"));
                            fields.Add("SENDTO_ID", this.ContainsColumn(gdv, "SENDTO_ID"));
                            fields.Add("SENDTO_NAME", this.ContainsColumn(gdv, "SENDTO_NAME"));
                            fields.Add("FLOWIMPORTANT", this.ContainsColumn(gdv, "FLOWIMPORTANT"));
                            fields.Add("FLOWURGENT", this.ContainsColumn(gdv, "FLOWURGENT"));
                            fields.Add("STATUS", this.ContainsColumn(gdv, "STATUS"));
                            fields.Add("FORM_TABLE", this.ContainsColumn(gdv, "FORM_TABLE"));
                            fields.Add("FORM_KEYS", this.ContainsColumn(gdv, "FORM_KEYS"));
                            fields.Add("FORM_PRESENTATION", this.ContainsColumn(gdv, "FORM_PRESENTATION"));
                            fields.Add("FORM_PRESENT_CT", this.ContainsColumn(gdv, "FORM_PRESENT_CT"));
                            fields.Add("REMARK", this.ContainsColumn(gdv, "REMARK"));
                            fields.Add("PROVIDER_NAME", this.ContainsColumn(gdv, "PROVIDER_NAME"));
                            fields.Add("VERSION", this.ContainsColumn(gdv, "VERSION"));
                            fields.Add("EMAIL_ADD", this.ContainsColumn(gdv, "EMAIL_ADD"));
                            fields.Add("EMAIL_STATUS", this.ContainsColumn(gdv, "EMAIL_STATUS"));
                            fields.Add("VDSNAME", this.ContainsColumn(gdv, "VDSNAME"));
                            fields.Add("SENDBACKSTEP", this.ContainsColumn(gdv, "SENDBACKSTEP"));
                            fields.Add("LEVEL_NO", this.ContainsColumn(gdv, "LEVEL_NO"));
                            fields.Add("WEBFORM_NAME", this.ContainsColumn(gdv, "WEBFORM_NAME"));
                            fields.Add("UPDATE_WHOLE_TIME", this.ContainsColumn(gdv, "UPDATE_WHOLE_TIME"));
                            fields.Add("FLOWPATH", this.ContainsColumn(gdv, "FLOWPATH"));
                            fields.Add("PLUSAPPROVE", this.ContainsColumn(gdv, "PLUSAPPROVE"));
                            fields.Add("PLUSROLES", this.ContainsColumn(gdv, "PLUSROLES"));
                            fields.Add("MULTISTEPRETURN", this.ContainsColumn(gdv, "MULTISTEPRETURN"));
                            fields.Add("ATTACHMENTS", this.ContainsColumn(gdv, "ATTACHMENTS"));
                            #endregion
                            break;
                        case ESqlMode.Delay:
                            #region delay fields
                            fields.Add("LISTID", this.ContainsColumn(gdv, "LISTID"));
                            fields.Add("FLOW_ID", this.ContainsColumn(gdv, "FLOW_ID"));
                            fields.Add("FLOW_DESC", this.ContainsColumn(gdv, "FLOW_DESC"));
                            fields.Add("APPLICANT", this.ContainsColumn(gdv, "APPLICANT"));
                            fields.Add("S_USER_ID", this.ContainsColumn(gdv, "S_USER_ID"));
                            fields.Add("S_STEP_ID", this.ContainsColumn(gdv, "S_STEP_ID"));
                            fields.Add("S_STEP_DESC", this.ContainsColumn(gdv, "S_STEP_DESC"));
                            fields.Add("D_STEP_ID", this.ContainsColumn(gdv, "D_STEP_ID"));
                            fields.Add("D_STEP_DESC", this.ContainsColumn(gdv, "D_STEP_DESC"));
                            fields.Add("EXP_TIME", this.ContainsColumn(gdv, "EXP_TIME"));
                            fields.Add("URGENT_TIME", this.ContainsColumn(gdv, "URGENT_TIME"));
                            fields.Add("TIME_UNIT", this.ContainsColumn(gdv, "TIME_UNIT"));
                            fields.Add("USERNAME", this.ContainsColumn(gdv, "USERNAME"));
                            fields.Add("FORM_NAME", this.ContainsColumn(gdv, "FORM_NAME"));
                            fields.Add("NAVIGATOR_MODE", this.ContainsColumn(gdv, "NAVIGATOR_MODE"));
                            fields.Add("FLNAVIGATOR_MODE", this.ContainsColumn(gdv, "FLNAVIGATOR_MODE"));
                            fields.Add("PARAMETERS", this.ContainsColumn(gdv, "PARAMETERS"));
                            fields.Add("SENDTO_KIND", this.ContainsColumn(gdv, "SENDTO_KIND"));
                            fields.Add("SENDTO_ID", this.ContainsColumn(gdv, "SENDTO_ID"));
                            fields.Add("FLOWIMPORTANT", this.ContainsColumn(gdv, "FLOWIMPORTANT"));
                            fields.Add("FLOWURGENT", this.ContainsColumn(gdv, "FLOWURGENT"));
                            fields.Add("STATUS", this.ContainsColumn(gdv, "STATUS"));
                            fields.Add("FORM_TABLE", this.ContainsColumn(gdv, "FORM_TABLE"));
                            fields.Add("FORM_KEYS", this.ContainsColumn(gdv, "FORM_KEYS"));
                            fields.Add("FORM_PRESENTATION", this.ContainsColumn(gdv, "FORM_PRESENTATION"));
                            fields.Add("FORM_PRESENT_CT", this.ContainsColumn(gdv, "FORM_PRESENT_CT"));
                            fields.Add("REMARK", this.ContainsColumn(gdv, "REMARK"));
                            fields.Add("PROVIDER_NAME", this.ContainsColumn(gdv, "PROVIDER_NAME"));
                            fields.Add("VERSION", this.ContainsColumn(gdv, "VERSION"));
                            fields.Add("EMAIL_ADD", this.ContainsColumn(gdv, "EMAIL_ADD"));
                            fields.Add("EMAIL_STATUS", this.ContainsColumn(gdv, "EMAIL_STATUS"));
                            fields.Add("VDSNAME", this.ContainsColumn(gdv, "VDSNAME"));
                            fields.Add("SENDBACKSTEP", this.ContainsColumn(gdv, "SENDBACKSTEP"));
                            fields.Add("LEVEL_NO", this.ContainsColumn(gdv, "LEVEL_NO"));
                            fields.Add("WEBFORM_NAME", this.ContainsColumn(gdv, "WEBFORM_NAME"));
                            fields.Add("UPDATE_DATE", this.ContainsColumn(gdv, "UPDATE_DATE"));
                            fields.Add("UPDATE_TIME", this.ContainsColumn(gdv, "UPDATE_TIME"));
                            fields.Add("FLOWPATH", this.ContainsColumn(gdv, "FLOWPATH"));
                            fields.Add("PLUSAPPROVE", this.ContainsColumn(gdv, "PLUSAPPROVE"));
                            fields.Add("PLUSROLES", this.ContainsColumn(gdv, "PLUSROLES"));
                            fields.Add("SENDTO_DETAIL", this.ContainsColumn(gdv, "SENDTO_DETAIL"));
                            fields.Add("UPDATE_WHOLE_TIME", this.ContainsColumn(gdv, "UPDATE_WHOLE_TIME"));
                            fields.Add("OVERTIME", this.ContainsColumn(gdv, "OVERTIME"));
                            #endregion
                            break;
                        case ESqlMode.ToDoListStatist:
                            #region todoliststatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(gdv, "FLOW_DESC"));
                            fields.Add("TODOLIST_COUNT", this.ContainsColumn(gdv, "TODOLIST_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.ToDoHisStatist:
                            #region todohisstatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(gdv, "FLOW_DESC"));
                            fields.Add("TODOHIS_COUNT", this.ContainsColumn(gdv, "TODOHIS_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.NotifyStatist:
                            #region notifystatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(gdv, "FLOW_DESC"));
                            fields.Add("NOTIFY_COUNT", this.ContainsColumn(gdv, "NOTIFY_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.DelayStatist:
                            #region dealystatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(gdv, "FLOW_DESC"));
                            fields.Add("DELAY_COUNT", this.ContainsColumn(gdv, "DELAY_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.AllStatist:
                            #region allstatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(gdv, "FLOW_DESC"));
                            fields.Add("TODOLIST_COUNT", this.ContainsColumn(gdv, "TODOLIST_COUNT"));
                            fields.Add("TODOHIS_COUNT", this.ContainsColumn(gdv, "TODOHIS_COUNT"));
                            fields.Add("NOTIFY_COUNT", this.ContainsColumn(gdv, "NOTIFY_COUNT"));
                            fields.Add("DELAY_COUNT", this.ContainsColumn(gdv, "DELAY_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.FlowRunOver:
                            #region flowrunover
                            fields.Add("LISTID", this.ContainsColumn(gdv, "LISTID"));
                            fields.Add("FLOW_DESC", this.ContainsColumn(gdv, "FLOW_DESC"));
                            fields.Add("D_STEP_ID", this.ContainsColumn(gdv, "D_STEP_ID"));
                            fields.Add("FORM_NAME", this.ContainsColumn(gdv, "FORM_NAME"));
                            fields.Add("WEBFORM_NAME", this.ContainsColumn(gdv, "WEBFORM_NAME"));
                            fields.Add("FORM_PRESENTATION", this.ContainsColumn(gdv, "FORM_PRESENTATION"));
                            fields.Add("FORM_PRESENT_CT", this.ContainsColumn(gdv, "FORM_PRESENT_CT"));
                            fields.Add("REMARK", this.ContainsColumn(gdv, "REMARK"));
                            fields.Add("UPDATE_WHOLE_TIME", this.ContainsColumn(gdv, "UPDATE_WHOLE_TIME"));
                            fields.Add("ATTACHMENTS", this.ContainsColumn(gdv, "ATTACHMENTS"));
                            #endregion
                            break;
                    }
                    this.GetDesignerHost();
                    FLWebWizardEditorDialog dialog = new FLWebWizardEditorDialog();
#if VS90
                    dialog.DesignerDocument = _designerDocument;
#endif
                    dialog.Component = _wiz;
                    dialog.Fields = fields;
                    dialog.Show();
                }
            }
        }
    }

    public class FLWebWizardDesignerActionList : DesignerActionList
    {
        FLWebWizardDesigner _designer = null;

        public FLWebWizardDesignerActionList(IComponent component, FLWebWizardDesigner designer)
            : base(component)
        {
            _designer = designer;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();
            items.Add(new DesignerActionMethodItem(this, "GenFields", "Generate Fields", true));
            return items;
        }

        public void GenFields()
        {
            if (this._designer != null)
            {
                _designer.GenFields();
            }
        }
    }
}
