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

        //人才編號
        public string HumanIDFixed()
        {
           
            DateTime VoucherDate = DateTime.Now;
            return VoucherDate.Year.ToString().Trim().Substring(2, 2) + VoucherDate.Month.ToString().PadLeft(2, '0') + VoucherDate.Day.ToString().PadLeft(2, '0');
        }

        //新增前
        private void ucHumanImport_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucHumanImport_CompnnentValidate();    //驗證

        }     

        //【檢查欄位】
        private string[] ucHumanImport_ValidateField = new string[] { "NameC", "SexText", "PhoneNo", "BirthYear", "Address" };

        //【驗證】
        private void ucHumanImport_CompnnentValidate()
        {
            //抓驗證欄位
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucHumanImport_ValidateField)
            {
                InputContent[aString] = ucHumanImport.GetFieldCurrentValue(aString);
            }

            //資料驗證
            string ErrorMsg = ucHumanImport_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

        }

        //【資料驗證】
        private string ucHumanImport_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "NameC", DisplayName = "姓名", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SexText", DisplayName = "性別", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "PhoneNo", DisplayName = "電話", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BirthYear", DisplayName = "出生年", IsUserCheck = true });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "Address", DisplayName = "居住區域", IsUserCheck = true });

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }


        #region =================================ServerMethod======================================

        //前台呼叫【驗證】
        public object[] DataValidate(object[] objParam)
        {

            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //抓驗證欄位
            var Validate_Input = Parameter_Input.Where(m => ucHumanImport_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //資料驗證
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

        //Excel檔案匯入
        public object[] ExcelFileImport(object[] objParam)
        {
            //回傳
            var theResult = new Dictionary<string, object>();
            theResult.Add("IsOK", false);
            theResult.Add("Msg", "錯誤了唷");

            try
            {
                //檔案
                var aMemoryStream = (MemoryStream)HandlerHelper.DeserializeObject(objParam[0].ToString());

                //SheetIndex
                var SheetIndex = (int)(objParam[1]);

                //標題
                var titleObject = (Dictionary<string, object>)HandlerHelper.DeserializeObject(objParam[2].ToString());

                //參數
                var voucherObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(objParam[3].ToString());

                //匯入作業結果
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
                theResult["Msg"] = "執行錯誤";
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
        }

        //================================================================================================================================//
        /// <summary>Combobox用資料</summary>
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
        //得到可選擇標籤資料
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

                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        
        //人才資料搜尋
        public object[] HumanSelect(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string BirthYear1 = (string)parm[0];
            string BirthYear2 = (string)parm[1];
            string fullText = (string)parm[2];
            string Type = (string)parm[3];
            
            string js = string.Empty;
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec [dbo].procSearchHuman '" + userid + "','" + BirthYear1 + "','" + BirthYear2 + "','" + fullText + "'," + Type + "";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
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
        //人才資料搜尋 匯出
        public object[] HumanSelectExcel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string BirthYear1 = (string)parm[0];
            string BirthYear2 = (string)parm[1];
            string fullText = (string)parm[2];
            string Type = (string)parm[3];
            
            string js = string.Empty;           
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = "exec [dbo].procSearchHuman '" + userid + "','" + BirthYear1 + "','" + BirthYear2 + "','" + fullText + "'," + Type + "";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();


                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));

                theResult.Add("IsOK", true);
                theResult.Add("Msg", "錯誤訊息");
                theResult.Add("FileName", "這是一個檔案.xls");

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
        //取得Human=>HumanID最大編號
        public string GetHumanID()
        {
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string HumanID = "";
           
            //建立資料庫連結           
            try
            {
                //獲取ItemSeq民國年+月份+流水號3碼10404001
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
            ucHuman.SetFieldValue("CreateDate", DateTime.Now);//欄位賦值
        }

        //將人才群加入標籤
        public object[] AddHumanLabel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sClassID = (string)parm[0];
            string HumanIDStr = (string)parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procInsertHumanAddLabel '" + sClassID + "','" + HumanIDStr + "','" + CreateBy + "'";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
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
            updateClassQuery.SetFieldValue("CreateDate", DateTime.Now);//欄位賦值

        }
        //刪除登入者的所有標籤
        public object[] ClearQueryLabel(object[] objParam)
        {
            string UserID = objParam[0].ToString();
            string js = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
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

        //新增標籤時檢查是標籤否存在
        public object[] CheckHumanClassSet(object[] objParam)
        {
            string Class = objParam[0].ToString().Trim();

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string js = string.Empty;

            //建立資料庫連結           
            try
            {
                string SQL = "Select count(*) as iCount from HumanClassSet where ClassText = '" + Class + "'";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                string cnt = ds.Tables[0].Rows[0]["iCount"].ToString();
                //// Indented縮排 將資料轉換成Json格式
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                js = cnt;
                transaction.Commit();
            }
            catch { transaction.Rollback(); }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return new object[] { 0, js };
        }

        //將人才刪除
        public object[] DeleteHumanID(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sHumanID = (string)parm[0];
           
            string js = string.Empty;
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
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

        //獲得目前匯入Execel的數量
        public object[] GetHumanImportInfo(object[] objParam)
        {

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string js = string.Empty;

            //建立資料庫連結           
            try
            {
                string SQL = "select SUM(iCount) as isum from HumanImportInfo";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                string cnt = ds.Tables[0].Rows[0]["isum"].ToString();
                //// Indented縮排 將資料轉換成Json格式
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