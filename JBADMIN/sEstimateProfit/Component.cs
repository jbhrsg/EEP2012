using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using TheExcelFileImport;
using Newtonsoft.Json;

namespace sEstimateProfit
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
        }

        public Component(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        //�s�W�e
        private void ucEstimateProfit_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            //ucEstimateProfit_CompnnentValidate();    //����
            ucEstimateProfit.SetFieldValue("CreateDate", DateTime.Now);//�����
            ucEstimateProfit.SetFieldValue("LastUpdateDate", DateTime.Now);//�����

        }
       
        ////�ק�e
        //private void ucglVoucherDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        //{
        //    ucglVoucherDetails_CompnnentValidate();    //����
        //}

        //�i�ˬd���j
        private string[] ucEstimateProfit_ValidateField = new string[] { "AcnoAll", "CostCenterID", "Describe", "Amt" };

        //�i���ҡj
        private void ucEstimateProfit_CompnnentValidate()
        {
            //���������
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucEstimateProfit_ValidateField)
            {
                InputContent[aString] = ucEstimateProfit.GetFieldCurrentValue(aString);
            }

            //�������
            string ErrorMsg = ucglVoucherDetails_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //�޿�����
            ErrorMsg = ucEstimateProfit_ServerValidate(ucEstimateProfit.conn, ucEstimateProfit.trans, InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //�~��ƭȴ���(�[�K)
            ucEstimateProfit.SetFieldValue("Amt", Salary.ENCODE(1, Convert.ToDecimal(InputContent["AMT_Decode"])));
        }

        //�i������ҡj
        private string ucglVoucherDetails_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "AcnoAll", DisplayName = "��إN��", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CostCenterID", DisplayName = "��������", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Describe", DisplayName = "���e", IsUserCheck = true });

            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Amt", DisplayName = "���B", SystemCheckType = TheDictionaryCheckType.Int, IsUserCheck = true });

            var SalaryIDItemsTable = SQL_Tools.GetDataSet(this, cb_glVoucherDetails.CommandText, new ArrayList()).Tables[0];

            aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
            {
                if (aCheckData.FieldName == "AcnoAll")
                {
                    if (!SalaryIDItemsTable.AsEnumerable().Any(m => m.Field<int>("AcnoAll") == Convert.ToInt32(aCheckData.TheValue)))
                    {
                        aCheckData.ErrorMsg = string.Format("�i{0}�j�]�w���~", aCheckData.DisplayName);
                    }
                    else aCheckData.IsOK = true;
                }
                if (aCheckData.FieldName == "Amt")
                {
                    if (Convert.ToDecimal(aCheckData.TheValue) < 0)
                    {
                        aCheckData.ErrorMsg = string.Format("�i{0}�j�]�w���~", aCheckData.DisplayName);
                    }
                    else aCheckData.IsOK = true;
                }
            };

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }

        //�i�޿����ҡj        
        private string ucEstimateProfit_ServerValidate(IDbConnection connection, IDbTransaction transaction, Dictionary<string, object> InputContent)
        {
            //���P�_�O�_���� ���q�O����ػP����
            string SQL = @"
select COUNT(*) as iCount from glAccountItem where CompanyID=1
    and Acno+SubAcno=@AcnoAll 
";
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@AcnoAll", InputContent["AcnoAll"]));
            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

            try
            {
                DataSet DataSet = this.ExecuteSql(SQL, connection, transaction, Parameter);
                int Rows = Convert.ToInt32(DataSet.Tables[0].Rows[0][0]);
                if (Rows == 0) return "�i��إN���j���s�b";
                return "";
            }
            catch (Exception ex) { return ex.Message; }
        }

        #region =================================ServerMethod======================================

        //�e�x�I�s�i���ҡj
        public object[] DataValidate(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //���������
            var Validate_Input = Parameter_Input.Where(m => ucEstimateProfit_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //�������
            string ErrorMsg = ucglVoucherDetails_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //�޿�����
                ErrorMsg = ucEstimateProfit_ServerValidate(connection, transaction, Validate_Input);
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        #endregion

        //Excel�ɮ׶פJ
        public object[] ExcelFileImport(object[] objParam)
        {
            //�^��
            var theResult = new Dictionary<string, object>();
            theResult.Add("IsOK", false);
            theResult.Add("Msg", "���~�F��");

            try
            {
                //�ɮ�
                var aMemoryStream = (MemoryStream)HandlerHelper.DeserializeObject(objParam[0].ToString());

                //SheetIndex
                var SheetIndex = (int)(objParam[1]);

                //���D
                var titleObject = (Dictionary<string, object>)HandlerHelper.DeserializeObject(objParam[2].ToString());

                //�Ѽ�
                var voucherObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(objParam[3].ToString());                
               
                //�פJ�@�~���G
                var aExcelFileImportResult = new ExcelFileImport
                {
                    FileStream = aMemoryStream,
                    SheetIndex = SheetIndex,
                    HealderParameter = titleObject,
                    sYM = (voucherObject["sYM"]).ToString(),
                    theDataModule = this,
                    UserID = SrvUtils.GetValue("_usercode", this)[1].ToString(),
                    CreateMan = SrvUtils.GetValue("_username", this)[1].ToString()
                }.GetTheFileImportResult();

                theResult["IsOK"] = aExcelFileImportResult.IsOK;
                theResult["Msg"] = aExcelFileImportResult.ErrorMsg;
                if (!aExcelFileImportResult.IsOK && aExcelFileImportResult.Result != null) theResult["File"] = (MemoryStream)aExcelFileImportResult.Result;
                
            }
            catch (TheUserDefinedException ex)
            {
                theResult["IsOK"] = false;
                theResult["Msg"] = ex.Message;
            }
            catch (Exception)
            {
                theResult["IsOK"] = false;
                theResult["Msg"] = "������~";
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
        }

        private void ucEstimateProfit_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEstimateProfit.SetFieldValue("LastUpdateDate", DateTime.Now);//�����
        }

        
       



    }
}
