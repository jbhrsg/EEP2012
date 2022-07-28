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


namespace sJCS1Import
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
        private void ucRoomerUpdate_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucRoomerUpdate_CompnnentValidate();    //����
        }
        //�i�ˬd���j
        private string[] ucRoomerUpdate_ValidateField = new string[] { "CustNo", "CostCenter", "CustClass"};

        //�i���ҡj
        private void ucRoomerUpdate_CompnnentValidate()
        {
            //���������
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucRoomerUpdate_ValidateField)
            {
                InputContent[aString] = ucRoomerUpdate.GetFieldCurrentValue(aString);
            }

            //�������
            string ErrorMsg = ucRoomerUpdate_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);
           
        }

        //�i������ҡj
        private string ucRoomerUpdate_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CustNo", DisplayName = "���u���X", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CostCenter", DisplayName = "��������", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CustClass", DisplayName = "�Z�O", IsUserCheck = true });  

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }

        #region =================================ServerMethod======================================

        //�e�x�I�s�i���ҡj
        public object[] DataValidate(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //���������
            var Validate_Input = Parameter_Input.Where(m => ucRoomerUpdate_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //�������
            string ErrorMsg = ucRoomerUpdate_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
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
                    CustomerID = voucherObject["CustomerID"].ToString(),
                    theDataModule = this,
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
