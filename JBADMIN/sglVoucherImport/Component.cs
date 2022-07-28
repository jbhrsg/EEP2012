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

namespace sglVoucherImport
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
        private void ucglVoucherDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglVoucherDetails_CompnnentValidate();    //����
        }
        ////�ק�e
        //private void ucglVoucherDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        //{
        //    ucglVoucherDetails_CompnnentValidate();    //����
        //}

        //�i�ˬd���j
        private string[] ucglVoucherDetails_ValidateField = new string[] {"BorrowLendType", "Acno", "SubAcno", "CostCenterID", "Describe", "Amt" };

        //�i���ҡj
        private void ucglVoucherDetails_CompnnentValidate()
        {
            //���������
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucglVoucherDetails_ValidateField)
            {
                InputContent[aString] = ucglVoucherDetails.GetFieldCurrentValue(aString);
            }

            //�������
            string ErrorMsg = ucglVoucherDetails_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //�޿�����
            ErrorMsg = ucglVoucherDetails_ServerValidate(ucglVoucherDetails.conn, ucglVoucherDetails.trans, InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //�~��ƭȴ���(�[�K)
            ucglVoucherDetails.SetFieldValue("AMT", Salary.ENCODE(1, Convert.ToDecimal(InputContent["AMT_Decode"])));
        }

        //�i������ҡj
        private string ucglVoucherDetails_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "VoucherDate", DisplayName = "�ǲ����", SystemCheckType = TheDictionaryCheckType.DateTime, IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BorrowLendType", DisplayName = "�ɶU", SystemCheckType = TheDictionaryCheckType.Int, IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Acno", DisplayName = "���", SystemCheckType = TheDictionaryCheckType.Int, IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SubAcno", DisplayName = "��ة���",  IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Describe", DisplayName = "���e", IsUserCheck = true });

            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Amt", DisplayName = "���B", SystemCheckType = TheDictionaryCheckType.Int, IsUserCheck = true });

            var SalaryIDItemsTable = SQL_Tools.GetDataSet(this, cb_glVoucherDetails.CommandText, new ArrayList()).Tables[0];

            aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
            {
                if (aCheckData.FieldName == "Acno")
                {
                    if (!SalaryIDItemsTable.AsEnumerable().Any(m => m.Field<int>("Acno") == Convert.ToInt32(aCheckData.TheValue)))
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
        private string ucglVoucherDetails_ServerValidate(IDbConnection connection, IDbTransaction transaction, Dictionary<string, object> InputContent)
        {
            //���P�_�O�_���� ���q�O����ػP����
            string SQL = @"
select COUNT(*) as iCount from glAccountItem where CompanyID=@CompanyID
    and Acno=@Acno and SubAcno=@SubAcno 
";
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@CompanyID", InputContent["CompanyID"]));
            Parameter.Add(new SqlParameter("@Acno", InputContent["Acno"]));
            Parameter.Add(new SqlParameter("@SubAcno", InputContent["SubAcno"]));
            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

            try
            {
                DataSet DataSet = this.ExecuteSql(SQL, connection, transaction, Parameter);
                int Rows = Convert.ToInt32(DataSet.Tables[0].Rows[0][0]);
                if (Rows == 0) return "�i���q�O����ػP���ӡj���s�b";
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
            var Validate_Input = Parameter_Input.Where(m => ucglVoucherDetails_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //�������
            string ErrorMsg = ucglVoucherDetails_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //�޿�����
                ErrorMsg = ucglVoucherDetails_ServerValidate(connection, transaction, Validate_Input);
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        //public object[] GetOldSetting(object[] objParam)
        //{
//            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

//            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
//            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EMPLOYEE_ID", DisplayName = "���u�s��" });
//            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EFFECT_DATE", DisplayName = "�ͮĤ��", SystemCheckType = TheDictionaryCheckType.DateTime });
//            aCheckDictionary.SetFieldValue(Parameter_Input);
//            aCheckDictionary.DoCheck();

//            string ErrorMsg = aCheckDictionary.GetFirstErrorMsg();
//            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { Result = ErrorMsg }.ToJsonString() };

//            string SQL = @"
//Select	T.EFFECT_DATE,
//		C.SALARY_CODE,
//		C.SALARY_CNAME,
//		C.SALARY_ENAME,
//		[dbo].[Decode](T.AMT) as Num
//From	[dbo].[dtHRM_SALARY_SALBASE_BASETTS_DateAndMan](@EmployeeID,@TheDate) as T Left Join
//		[dbo].[HRM_SALARY_SALCODE]	as C on T.SALARY_ID = C.SALARY_ID
//            ";

//            ArrayList Parameter = new ArrayList();
//            Parameter.Add(new SqlParameter("@EmployeeID", Parameter_Input["EMPLOYEE_ID"]));
//            Parameter.Add(new SqlParameter("@TheDate", Parameter_Input["EFFECT_DATE"]));
//            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

//            DataSet DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
//            if (DataSet == null || DataSet.Tables.Count == 0 || DataSet.Tables[0].Rows.Count == 0) return new object[] { 0, new TheJsonResult { ErrorMsg = "�䤣��������" }.ToJsonString() };
//            return new object[] { 0, new TheJsonResult { IsOK = true, Result = DataSet.Tables[0] }.ToJsonString() };
        //}

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
                    CompanyID = Convert.ToInt32(voucherObject["CompanyID"]),
                    VoucherID = Convert.ToInt32(voucherObject["VoucherID"]),
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
       



    }
}
