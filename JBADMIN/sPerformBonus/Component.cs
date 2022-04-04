using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using JBTool;
using Newtonsoft;
using Newtonsoft.Json;
using TheExcelFileImport;
using System.IO;
namespace sPerformBonus
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
        //���o�����N��
        public object[] GetUserOrgNOs(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;
        }
       
        //Excel�פJ
        //�s�W�e
        private void ucPerfBonusDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucPerfBonusDetails_CompnnentValidate();    //����
        }
        //�N�Z�ĩ��Ӫ��B�g���~��ɦ��o
        public object PutBonusAmtToSalary_Enrich(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();
            DataRow dr = (DataRow)objParam[0];
            string PerfBonusNO = dr["PerfBonusNO"].ToString();
            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                IDbTransaction trans = conn.BeginTransaction();
                string sql = "EXEC dbo.procPutBonusAmtToSalary_Enrich '" + PerfBonusNO + "','" + UserID + "'";
                this.ExecuteSql(sql, conn, trans);
                trans.Commit();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return ret;
        }
        //�i�ˬd���j
        private string[] ucPerfBonusDetails_ValidateField = new string[] { "EmpID", "EmpName", "BonusAmt"};
        //�i���ҡj
        private void ucPerfBonusDetails_CompnnentValidate()
        {
            //���������
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucPerfBonusDetails_ValidateField)
            {
                InputContent[aString] = ucPerfBonusDetails.GetFieldCurrentValue(aString);
            }
            //�������
            string ErrorMsg = ucPerfBonusDetails_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);
        }
        //�i������ҡj
        private string ucPerfBonusDetails_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EmpID", DisplayName = "���u�u��", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EmpName", DisplayName = "���u�m�W", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BonusAmt", DisplayName = "�������B", IsUserCheck = true });
            //aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "AdjustAmt", DisplayName = "�վ���B", IsUserCheck = true });
            //aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "AdjustNote", DisplayName = "�վ��]", IsUserCheck = true });
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
            var Validate_Input = Parameter_Input.Where(m => ucPerfBonusDetails_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //�������
            string ErrorMsg = ucPerfBonusDetails_ClientValidate(Validate_Input);
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
                var ContentObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(objParam[3].ToString());

                //�פJ�@�~���G
                var aExcelFileImportResult = new ExcelFileImport
                {
                    FileStream = aMemoryStream,
                    SheetIndex = SheetIndex,
                    HealderParameter = titleObject,
                    //
                    PerfBonusNO = ContentObject["PerfBonusNO"].ToString(),
                    //industry = ContentObject["NameC"].ToString(),
                    theDataModule = this,
                    UserID = SrvUtils.GetValue("_usercode", this)[1].ToString(),
                    //CreateMan = SrvUtils.GetValue("_usercode", this)[1].ToString()
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
        /// <summary>Combobox�θ��</summary>
        public class ComboboxField
        {
            public string text { get; set; }

            public string value { get; set; }

            public bool selected { get; set; }

            public ComboboxField()
            {
                selected = false;
            }
        }
        //�ˬd�վ���B,���ѵ�Flow�@�P�_
        public object[] GetAdjustAmt(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            //decimal AdjustAmt = 0;
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0];
            string PerfBonusNO = dr["PerfBonusNO"].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select sum(Isnull(AdjustAmt,0)) as AdjustAmt from PerfBonusDetails where PerfBonusNO ='" + PerfBonusNO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal AdjustAmt = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                //ret[1] = AdjustAmt;
                //ret[1] = AdjustAmt;
                if (AdjustAmt != 0)
                {
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }

        private void ucPerfBonusMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucPerfBonusMaster.SetFieldValue("CreateDate", DateTime.Now);
        }
        public object[] CheckIsDuplicateApply(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string Org_NOParent = parm[0];
                string PerfBonusYM = parm[1];
                string UserID = parm[2];
                string sql = "Select Top 1 ISNULL(dbo.funReturnIsDuplicateApply('" + Org_NOParent + "','" + PerfBonusYM + "'),0) AS STR FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                js = dsTemp.Tables[0].Rows[0]["STR"].ToString();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
     }
}
