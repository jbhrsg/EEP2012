using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using JBTool;
using Newtonsoft.Json;
using Srvtools;
using TheExcelFileImport;


namespace sHumanImport
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

        //�H�~�s��
        public string HumanIDFixed()
        {
           
            DateTime VoucherDate = DateTime.Now;
            return VoucherDate.Year.ToString().Trim().Substring(2, 2) + VoucherDate.Month.ToString().PadLeft(2, '0') + VoucherDate.Day.ToString().PadLeft(2, '0');
        }

        //�s�W�e
        private void ucHumanImport_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHumanImport_CompnnentValidate();    //����

        }     

        //�i�ˬd���j
        private string[] ucHumanImport_ValidateField = new string[] { "NameC", "SexText", "PhoneNo", "BirthYear", "Address" };

        //�i���ҡj
        private void ucHumanImport_CompnnentValidate()
        {
            //���������
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucHumanImport_ValidateField)
            {
                InputContent[aString] = ucHumanImport.GetFieldCurrentValue(aString);
            }

            //�������
            string ErrorMsg = ucHumanImport_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

        }

        //�i������ҡj
        private string ucHumanImport_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "NameC", DisplayName = "�m�W", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SexText", DisplayName = "�ʧO", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "PhoneNo", DisplayName = "�q��", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BirthYear", DisplayName = "�X�ͦ~", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Address", DisplayName = "�~��ϰ�", IsUserCheck = true });

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
            var Validate_Input = Parameter_Input.Where(m => ucHumanImport_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //�������
            string ErrorMsg = ucHumanImport_ClientValidate(Validate_Input);
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
                    sLabel = voucherObject["sLabel"].ToString(),
                    industry = voucherObject["NameC"].ToString(),
                    theDataModule = this,
                    UserID = SrvUtils.GetValue("_username", this)[1].ToString(),
                    CreateMan = SrvUtils.GetValue("_usercode", this)[1].ToString()
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

        //================================================================================================================================//
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
        //�o��i��ܼ��Ҹ��
        public object[] GetrHumanClass(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var HumanID = (Parameter_Input["Human_ID"]).ToString();

                string SQL = @"
select Cast(s.AutoKey as nvarchar(10)) as AutoKey,s.ClassText
from [dbo].[HumanClassSet] s
	left join [dbo].[HumanClass] c on c.HumanClassID=s.AutoKey and c.HumanID=@HumanID
where  c.HumanID is null order by s.ClassText
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@HumanID", HumanID));

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("AutoKey").ToString(),
                    text = m.Field<string>("ClassText") ?? "",
                    selected = (m.Field<string>("AutoKey") == "1")
                }).ToList();

                //�w�]�Ĥ@��
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //�^��
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        
        //�H�~��Ʒj�M
        public object[] HumanSelect(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string BirthYear1 = (string)parm[0];
            string BirthYear2 = (string)parm[1];
            string fullText = (string)parm[2];
            string Type = (string)parm[3];
            
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec [dbo].procSearchHuman '" + userid + "','" + BirthYear1 + "','" + BirthYear2 + "','" + fullText + "'," + Type + "";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
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
        //�H�~��Ʒj�M �ץX
        public object[] HumanSelectExcel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string BirthYear1 = (string)parm[0];
            string BirthYear2 = (string)parm[1];
            string fullText = (string)parm[2];
            string Type = (string)parm[3];
            
            string js = string.Empty;           
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = "exec [dbo].procSearchHuman '" + userid + "','" + BirthYear1 + "','" + BirthYear2 + "','" + fullText + "'," + Type + "";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();


                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));

                theResult.Add("IsOK", true);
                theResult.Add("Msg", "���~�T��");
                theResult.Add("FileName", "�o�O�@���ɮ�.xls");

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };

        }
        //���oHuman=>HumanID�̤j�s��
        public string GetHumanID()
        {
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string HumanID = "";
           
            //�إ߸�Ʈw�s��           
            try
            {
                //���ItemSeq����~+���+�y����3�X10404001
                string sql = "select dbo.funReturnHumanID() as HumanID" + "\r\n";
                DataSet dsItemSeq = this.ExecuteSql(sql, connection, transaction);
                HumanID = dsItemSeq.Tables[0].Rows[0]["HumanID"].ToString();
            }
            catch { transaction.Rollback(); }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return HumanID;
        }

        private void ucHuman_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHuman.SetFieldValue("HumanID", GetHumanID());
            ucHuman.SetFieldValue("CreateDate", DateTime.Now);//�����
        }

        //�N�H�~�s�[�J����
        public object[] AddHumanLabel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sClassID = (string)parm[0];
            string HumanIDStr = (string)parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procInsertHumanAddLabel '" + sClassID + "','" + HumanIDStr + "','" + CreateBy + "'";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
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

        private void updateClassQuery_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            updateClassQuery.SetFieldValue("CreateDate", DateTime.Now);//�����

        }
        //�R���n�J�̪��Ҧ�����
        public object[] ClearQueryLabel(object[] objParam)
        {
            string UserID = objParam[0].ToString();
            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procDeleteHumanClassQuery '" + UserID+"'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();
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

        //�s�W���Ү��ˬd�O���ҧ_�s�b
        public object[] CheckHumanClassSet(object[] objParam)
        {
            string Class = objParam[0].ToString().Trim();

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string js = string.Empty;

            //�إ߸�Ʈw�s��           
            try
            {
                string SQL = "Select count(*) as iCount from HumanClassSet where ClassText = '" + Class + "'";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                string cnt = ds.Tables[0].Rows[0]["iCount"].ToString();
                //// Indented�Y�� �N����ഫ��Json�榡
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                js = cnt;
                transaction.Commit();
            }
            catch { transaction.Rollback(); }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return new object[] { 0, js };
        }

        //�N�H�~�R��
        public object[] DeleteHumanID(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sHumanID = (string)parm[0];
           
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procDeleteHumanID '" + sHumanID + "'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();             
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

        //��o�ثe�פJExecel���ƶq
        public object[] GetHumanImportInfo(object[] objParam)
        {

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string js = string.Empty;

            //�إ߸�Ʈw�s��           
            try
            {
                string SQL = "select SUM(iCount) as isum from HumanImportInfo";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                string cnt = ds.Tables[0].Rows[0]["isum"].ToString();
                //// Indented�Y�� �N����ഫ��Json�榡
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                js = cnt;
                transaction.Commit();
            }
            catch { transaction.Rollback(); }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return new object[] { 0, js };
        }





    }
}