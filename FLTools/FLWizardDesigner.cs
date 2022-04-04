using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace FLTools
{
    internal class FLWizardDesigner : ComponentDesigner
    {
        private IDesignerHost _designerHost = null;
        private FLWizard _wiz = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            _designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (_designerHost == null)
            {
                MessageBox.Show("The IDesignerHost service interface could not be obtained.");
                return;
            }
            _wiz = this.Component as FLWizard;
        }

        private bool ContainsColumn(DataGridView dgv, string bindingField)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (string.Compare(column.DataPropertyName, bindingField, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override void DoDefaultAction()
        {
            if (_wiz == null || _wiz.BindingObject == null) return;
            Dictionary<string, bool> fields = new Dictionary<string, bool>();
            switch (_wiz.SqlMode)
            { 
                        case ESqlMode.ToDoList:
                        case ESqlMode.ToDoHis:
                        case ESqlMode.Notify:
                            #region todolist,todohis,notify fields
                            fields.Add("LISTID", this.ContainsColumn(_wiz.BindingObject, "LISTID"));
                            fields.Add("FLOW_ID", this.ContainsColumn(_wiz.BindingObject, "FLOW_ID"));
                            fields.Add("FLOW_DESC", this.ContainsColumn(_wiz.BindingObject, "FLOW_DESC"));
                            fields.Add("APPLICANT", this.ContainsColumn(_wiz.BindingObject, "APPLICANT"));
                            fields.Add("S_USER_ID", this.ContainsColumn(_wiz.BindingObject, "S_USER_ID"));
                            fields.Add("S_STEP_ID", this.ContainsColumn(_wiz.BindingObject, "S_STEP_ID"));
                            fields.Add("S_STEP_DESC", this.ContainsColumn(_wiz.BindingObject, "S_STEP_DESC"));
                            fields.Add("D_STEP_ID", this.ContainsColumn(_wiz.BindingObject, "D_STEP_ID"));
                            fields.Add("D_STEP_DESC", this.ContainsColumn(_wiz.BindingObject, "D_STEP_DESC"));
                            fields.Add("EXP_TIME", this.ContainsColumn(_wiz.BindingObject, "EXP_TIME"));
                            fields.Add("URGENT_TIME", this.ContainsColumn(_wiz.BindingObject, "URGENT_TIME"));
                            fields.Add("TIME_UNIT", this.ContainsColumn(_wiz.BindingObject, "TIME_UNIT"));
                            fields.Add("USERNAME", this.ContainsColumn(_wiz.BindingObject, "USERNAME"));
                            fields.Add("FORM_NAME", this.ContainsColumn(_wiz.BindingObject, "FORM_NAME"));
                            fields.Add("NAVIGATOR_MODE", this.ContainsColumn(_wiz.BindingObject, "NAVIGATOR_MODE"));
                            fields.Add("FLNAVIGATOR_MODE", this.ContainsColumn(_wiz.BindingObject, "FLNAVIGATOR_MODE"));
                            fields.Add("PARAMETERS", this.ContainsColumn(_wiz.BindingObject, "PARAMETERS"));
                            fields.Add("SENDTO_KIND", this.ContainsColumn(_wiz.BindingObject, "SENDTO_KIND"));
                            fields.Add("SENDTO_ID", this.ContainsColumn(_wiz.BindingObject, "SENDTO_ID"));
                            fields.Add("SENDTO_NAME", this.ContainsColumn(_wiz.BindingObject, "SENDTO_NAME"));
                            fields.Add("FLOWIMPORTANT", this.ContainsColumn(_wiz.BindingObject, "FLOWIMPORTANT"));
                            fields.Add("FLOWURGENT", this.ContainsColumn(_wiz.BindingObject, "FLOWURGENT"));
                            fields.Add("STATUS", this.ContainsColumn(_wiz.BindingObject, "STATUS"));
                            fields.Add("FORM_TABLE", this.ContainsColumn(_wiz.BindingObject, "FORM_TABLE"));
                            fields.Add("FORM_KEYS", this.ContainsColumn(_wiz.BindingObject, "FORM_KEYS"));
                            fields.Add("FORM_PRESENTATION", this.ContainsColumn(_wiz.BindingObject, "FORM_PRESENTATION"));
                            fields.Add("FORM_PRESENT_CT", this.ContainsColumn(_wiz.BindingObject, "FORM_PRESENT_CT"));
                            fields.Add("REMARK", this.ContainsColumn(_wiz.BindingObject, "REMARK"));
                            fields.Add("PROVIDER_NAME", this.ContainsColumn(_wiz.BindingObject, "PROVIDER_NAME"));
                            fields.Add("VERSION", this.ContainsColumn(_wiz.BindingObject, "VERSION"));
                            fields.Add("EMAIL_ADD", this.ContainsColumn(_wiz.BindingObject, "EMAIL_ADD"));
                            fields.Add("EMAIL_STATUS", this.ContainsColumn(_wiz.BindingObject, "EMAIL_STATUS"));
                            fields.Add("VDSNAME", this.ContainsColumn(_wiz.BindingObject, "VDSNAME"));
                            fields.Add("SENDBACKSTEP", this.ContainsColumn(_wiz.BindingObject, "SENDBACKSTEP"));
                            fields.Add("LEVEL_NO", this.ContainsColumn(_wiz.BindingObject, "LEVEL_NO"));
                            fields.Add("WEBFORM_NAME", this.ContainsColumn(_wiz.BindingObject, "WEBFORM_NAME"));
                            fields.Add("UPDATE_WHOLE_TIME", this.ContainsColumn(_wiz.BindingObject, "UPDATE_WHOLE_TIME"));
                            fields.Add("FLOWPATH", this.ContainsColumn(_wiz.BindingObject, "FLOWPATH"));
                            fields.Add("PLUSAPPROVE", this.ContainsColumn(_wiz.BindingObject, "PLUSAPPROVE"));
                            fields.Add("PLUSROLES", this.ContainsColumn(_wiz.BindingObject, "PLUSROLES"));
                            fields.Add("MULTISTEPRETURN", this.ContainsColumn(_wiz.BindingObject, "MULTISTEPRETURN"));
                            fields.Add("ATTACHMENTS", this.ContainsColumn(_wiz.BindingObject, "ATTACHMENTS"));
                            #endregion
                            break;
                        case ESqlMode.Delay:
                            #region delay fields
                            fields.Add("LISTID", this.ContainsColumn(_wiz.BindingObject, "LISTID"));
                            fields.Add("FLOW_ID", this.ContainsColumn(_wiz.BindingObject, "FLOW_ID"));
                            fields.Add("FLOW_DESC", this.ContainsColumn(_wiz.BindingObject, "FLOW_DESC"));
                            fields.Add("APPLICANT", this.ContainsColumn(_wiz.BindingObject, "APPLICANT"));
                            fields.Add("S_USER_ID", this.ContainsColumn(_wiz.BindingObject, "S_USER_ID"));
                            fields.Add("S_STEP_ID", this.ContainsColumn(_wiz.BindingObject, "S_STEP_ID"));
                            fields.Add("S_STEP_DESC", this.ContainsColumn(_wiz.BindingObject, "S_STEP_DESC"));
                            fields.Add("D_STEP_ID", this.ContainsColumn(_wiz.BindingObject, "D_STEP_ID"));
                            fields.Add("D_STEP_DESC", this.ContainsColumn(_wiz.BindingObject, "D_STEP_DESC"));
                            fields.Add("EXP_TIME", this.ContainsColumn(_wiz.BindingObject, "EXP_TIME"));
                            fields.Add("URGENT_TIME", this.ContainsColumn(_wiz.BindingObject, "URGENT_TIME"));
                            fields.Add("TIME_UNIT", this.ContainsColumn(_wiz.BindingObject, "TIME_UNIT"));
                            fields.Add("USERNAME", this.ContainsColumn(_wiz.BindingObject, "USERNAME"));
                            fields.Add("FORM_NAME", this.ContainsColumn(_wiz.BindingObject, "FORM_NAME"));
                            fields.Add("NAVIGATOR_MODE", this.ContainsColumn(_wiz.BindingObject, "NAVIGATOR_MODE"));
                            fields.Add("FLNAVIGATOR_MODE", this.ContainsColumn(_wiz.BindingObject, "FLNAVIGATOR_MODE"));
                            fields.Add("PARAMETERS", this.ContainsColumn(_wiz.BindingObject, "PARAMETERS"));
                            fields.Add("SENDTO_KIND", this.ContainsColumn(_wiz.BindingObject, "SENDTO_KIND"));
                            fields.Add("SENDTO_ID", this.ContainsColumn(_wiz.BindingObject, "SENDTO_ID"));
                            fields.Add("FLOWIMPORTANT", this.ContainsColumn(_wiz.BindingObject, "FLOWIMPORTANT"));
                            fields.Add("FLOWURGENT", this.ContainsColumn(_wiz.BindingObject, "FLOWURGENT"));
                            fields.Add("STATUS", this.ContainsColumn(_wiz.BindingObject, "STATUS"));
                            fields.Add("FORM_TABLE", this.ContainsColumn(_wiz.BindingObject, "FORM_TABLE"));
                            fields.Add("FORM_KEYS", this.ContainsColumn(_wiz.BindingObject, "FORM_KEYS"));
                            fields.Add("FORM_PRESENTATION", this.ContainsColumn(_wiz.BindingObject, "FORM_PRESENTATION"));
                            fields.Add("FORM_PRESENT_CT", this.ContainsColumn(_wiz.BindingObject, "FORM_PRESENT_CT"));
                            fields.Add("REMARK", this.ContainsColumn(_wiz.BindingObject, "REMARK"));
                            fields.Add("PROVIDER_NAME", this.ContainsColumn(_wiz.BindingObject, "PROVIDER_NAME"));
                            fields.Add("VERSION", this.ContainsColumn(_wiz.BindingObject, "VERSION"));
                            fields.Add("EMAIL_ADD", this.ContainsColumn(_wiz.BindingObject, "EMAIL_ADD"));
                            fields.Add("EMAIL_STATUS", this.ContainsColumn(_wiz.BindingObject, "EMAIL_STATUS"));
                            fields.Add("VDSNAME", this.ContainsColumn(_wiz.BindingObject, "VDSNAME"));
                            fields.Add("SENDBACKSTEP", this.ContainsColumn(_wiz.BindingObject, "SENDBACKSTEP"));
                            fields.Add("LEVEL_NO", this.ContainsColumn(_wiz.BindingObject, "LEVEL_NO"));
                            fields.Add("WEBFORM_NAME", this.ContainsColumn(_wiz.BindingObject, "WEBFORM_NAME"));
                            fields.Add("UPDATE_DATE", this.ContainsColumn(_wiz.BindingObject, "UPDATE_DATE"));
                            fields.Add("UPDATE_TIME", this.ContainsColumn(_wiz.BindingObject, "UPDATE_TIME"));
                            fields.Add("FLOWPATH", this.ContainsColumn(_wiz.BindingObject, "FLOWPATH"));
                            fields.Add("PLUSAPPROVE", this.ContainsColumn(_wiz.BindingObject, "PLUSAPPROVE"));
                            fields.Add("PLUSROLES", this.ContainsColumn(_wiz.BindingObject, "PLUSROLES"));
                            fields.Add("SENDTO_DETAIL", this.ContainsColumn(_wiz.BindingObject, "SENDTO_DETAIL"));
                            fields.Add("UPDATE_WHOLE_TIME", this.ContainsColumn(_wiz.BindingObject, "UPDATE_WHOLE_TIME"));
                            fields.Add("OVERTIME", this.ContainsColumn(_wiz.BindingObject, "OVERTIME"));
                            #endregion
                            break;
                        case ESqlMode.ToDoListStatist:
                            #region todoliststatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(_wiz.BindingObject, "FLOW_DESC"));
                            fields.Add("TODOLIST_COUNT", this.ContainsColumn(_wiz.BindingObject, "TODOLIST_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.ToDoHisStatist:
                            #region todohisstatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(_wiz.BindingObject, "FLOW_DESC"));
                            fields.Add("TODOHIS_COUNT", this.ContainsColumn(_wiz.BindingObject, "TODOHIS_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.NotifyStatist:
                            #region notifystatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(_wiz.BindingObject, "FLOW_DESC"));
                            fields.Add("NOTIFY_COUNT", this.ContainsColumn(_wiz.BindingObject, "NOTIFY_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.DelayStatist:
                            #region dealystatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(_wiz.BindingObject, "FLOW_DESC"));
                            fields.Add("DELAY_COUNT", this.ContainsColumn(_wiz.BindingObject, "DELAY_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.AllStatist:
                            #region allstatist
                            fields.Add("FLOW_DESC", this.ContainsColumn(_wiz.BindingObject, "FLOW_DESC"));
                            fields.Add("TODOLIST_COUNT", this.ContainsColumn(_wiz.BindingObject, "TODOLIST_COUNT"));
                            fields.Add("TODOHIS_COUNT", this.ContainsColumn(_wiz.BindingObject, "TODOHIS_COUNT"));
                            fields.Add("NOTIFY_COUNT", this.ContainsColumn(_wiz.BindingObject, "NOTIFY_COUNT"));
                            fields.Add("DELAY_COUNT", this.ContainsColumn(_wiz.BindingObject, "DELAY_COUNT"));
                            #endregion
                            break;
                        case ESqlMode.FlowRunOver:
                            #region flowrunover
                            fields.Add("LISTID", this.ContainsColumn(_wiz.BindingObject, "LISTID"));
                            fields.Add("FLOW_DESC", this.ContainsColumn(_wiz.BindingObject, "FLOW_DESC"));
                            fields.Add("D_STEP_ID", this.ContainsColumn(_wiz.BindingObject, "D_STEP_ID"));
                            fields.Add("FORM_NAME", this.ContainsColumn(_wiz.BindingObject, "FORM_NAME"));
                            fields.Add("WEBFORM_NAME", this.ContainsColumn(_wiz.BindingObject, "WEBFORM_NAME"));
                            fields.Add("FORM_PRESENTATION", this.ContainsColumn(_wiz.BindingObject, "FORM_PRESENTATION"));
                            fields.Add("FORM_PRESENT_CT", this.ContainsColumn(_wiz.BindingObject, "FORM_PRESENT_CT"));
                            fields.Add("REMARK", this.ContainsColumn(_wiz.BindingObject, "REMARK"));
                            fields.Add("UPDATE_WHOLE_TIME", this.ContainsColumn(_wiz.BindingObject, "UPDATE_WHOLE_TIME"));
                            fields.Add("ATTACHMENTS", this.ContainsColumn(_wiz.BindingObject, "ATTACHMENTS"));
                            #endregion
                            break;
            }

            FLWizardEditorDialog dialog = new FLWizardEditorDialog();
            dialog.DesignerHost = _designerHost;
            dialog.Component = _wiz;
            dialog.Fields = fields;

            dialog.Show();
        }
    }
}
