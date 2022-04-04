using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace _CON_Normal_ContactMemo
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_MEMO, logInfo_CON_CONTACT_MEMO, "CONTACT_MEMO_ID");  //LogInfo修正
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_MEMO);                                             //FieldAttr修正
        }

        public Component(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            IndentityLogInfoPatch.Execute(ucCON_CONTACT_MEMO, logInfo_CON_CONTACT_MEMO, "CONTACT_MEMO_ID");  //LogInfo修正
            IndentityLogInfoPatch.SysFieldAttr(ucCON_CONTACT_MEMO);                                             //FieldAttr修正
        }

        public const int G_HeadRowIndex = 0;

        //新增前
        private void ucCON_CONTACT_MEMO_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_CONTACT_MEMO_CompnnentValidate();    //驗證
        }

        //修改前
        private void ucCON_CONTACT_MEMO_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCON_CONTACT_MEMO_CompnnentValidate();    //驗證
        }

        //【檢查欄位】
        private string[] ucCON_CONTACT_MEMO_ValidateField = new string[] { "CONTACT_MEMO_ID", "CONTACT_ID", "MEMO_DATE", "MEMO_CONTENT", "MEMO_USER" };

        //【驗證】
        private void ucCON_CONTACT_MEMO_CompnnentValidate()
        {
            //抓驗證欄位
            Dictionary<string, object> InputContent = new Dictionary<string, object>();
            foreach (var aString in ucCON_CONTACT_MEMO_ValidateField)
            {
                InputContent[aString] = ucCON_CONTACT_MEMO.GetFieldCurrentValue(aString);
            }

            //資料驗證
            string ErrorMsg = ucCON_CONTACT_MEMO_ClientValidate(InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);

            //邏輯驗證
            ErrorMsg = ucCON_CONTACT_MEMO_ServerValidate(ucCON_CONTACT_MEMO.conn, ucCON_CONTACT_MEMO.trans, InputContent);
            if (ErrorMsg.Length > 0) throw new Exception(ErrorMsg);
        }

        //【資料驗證】
        private string ucCON_CONTACT_MEMO_ClientValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            //aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_MEMO_ID", DisplayName = "KEY" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "聯絡人ID" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_DATE", DisplayName = "註記日期" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_CONTENT", DisplayName = "註記內容" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_USER", DisplayName = "註記人員" });

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary.GetFirstErrorMsg();
        }

        //【邏輯驗證】        
        private string ucCON_CONTACT_MEMO_ServerValidate(IDbConnection connection, IDbTransaction transaction, Dictionary<string, object> InputContent)
        {
            //●判斷是否有重複資料在裡面了
            return "";
        }

        //前台呼叫【驗證】
        public object[] DataValidate(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

            //抓驗證欄位
            var Validate_Input = Parameter_Input.Where(m => ucCON_CONTACT_MEMO_ValidateField.Contains(m.Key)).ToDictionary(m => m.Key, m => m.Value);

            //資料驗證
            string ErrorMsg = ucCON_CONTACT_MEMO_ClientValidate(Validate_Input);
            if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //邏輯驗證
                ErrorMsg = ucCON_CONTACT_MEMO_ServerValidate(connection, transaction, Validate_Input);
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };
                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };
            }
            catch (Exception ex) { transaction.Rollback(); return new object[] { 0, new TheJsonResult { ErrorMsg = "error" }.ToJsonString() }; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        //-----------------------------------上傳資料-------------------------------------
        public object[] FileUpload(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

                var aResult = new { FileName = "", };

                //【輸入驗證】：前台的DataForm輸入驗證
                var aCheckDictionary = FileUploadFormValidate(Parameter_Input);
                string ErrorMsg = aCheckDictionary.GetFirstErrorMsg();
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

                //檔案路徑
                //string FilePathName = string.Format("../JQWebClient/{0}", Parameter_Input["FilePathName"]);
                string FilePathName = Parameter_Input["FilePathName"].ToString();

                //【取得ColumnName】：將前台輸入的資料轉換成ColumnName
                var aHeadList = FileUploadGetHeadList(Parameter_Input);

                //【抓資料】：ColumnName結合成新的DataTable                
                DataTable FileData = NPOIHelper.GetDataTable(FilePathName, aHeadList, G_HeadRowIndex);
                if (FileData == null) throw new Exception("【資料讀取】Error");

                //【欄位驗證】、【欄位轉換】：比對資料庫是否有這個數值，有就將數值轉換(Code->ID)
                if (!DataTableCellValidate(FileData)) throw new Exception("【欄位驗證】Error");
                if (FileData.HasErrors)
                {
                    if (!NPOIHelper.SetErrorMemo(FileData, FilePathName, aHeadList, G_HeadRowIndex)) throw new Exception("【檔案編輯】Error");
                    return new object[] { 0, new TheJsonResult { ErrorMsg = "欄位驗證有錯誤", Result = Parameter_Input["FilePathName"] }.ToJsonString() };
                }

                //【規則驗證】：整體驗證
                if (!DataTableDataValidate(FileData)) throw new Exception("【資料驗證】Error");
                if (FileData.HasErrors)
                {
                    if (!NPOIHelper.SetErrorMemo(FileData, FilePathName, aHeadList, G_HeadRowIndex)) throw new Exception("【檔案編輯】Error");
                    return new object[] { 0, new TheJsonResult { ErrorMsg = "資料驗證有錯誤", Result = Parameter_Input["FilePathName"] }.ToJsonString() };
                }

                //寫入
                if (!DataTableDataInsert(FileData)) throw new Exception("【資料寫入】Error");

                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };

            }
            catch (Exception ex)
            {
                return new object[] { 0, new TheJsonResult { ErrorMsg = "執行錯誤" }.ToJsonString() };
            }

        }

        //輸入驗證
        private TheDictionaryCheck FileUploadFormValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "FilePathName", DisplayName = "檔案路徑" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "聯絡人手機" });

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary;
        }

        //取得ColumnName
        private Dictionary<string, int> FileUploadGetHeadList(Dictionary<string, object> Parameter_Input)
        {
            Dictionary<string, int> aHeadList = new Dictionary<string, int>();

            foreach (var aProperty in typeof(ImportData).GetProperties())
            {
                if (Parameter_Input[aProperty.Name].ToString() != "")
                    aHeadList[aProperty.Name] = Convert.ToInt32(Parameter_Input[aProperty.Name]);
            }
            return aHeadList;
        }

        //--------------------------------------欄位驗證(順便欄位轉換)----------------------------
        private bool DataTableCellValidate(DataTable aTable)
        {
            bool codeFlag = false;

            try
            {
                var ValidData = GetValidData();

                TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "聯絡人手機", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_DATE", DisplayName = "註記日期", IsRequired = true, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_CONTENT", DisplayName = "註記內容", IsRequired = true, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "MEMO_USER", DisplayName = "註記人員", IsRequired = true, IsUserCheck = false });
                
                aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
                {
                    aCheckData.IsOK = false;
                    if (aCheckData.FieldName == "CONTACT_ID")
                    {
                        var aContactData = ValidData.ContactData.FirstOrDefault(m => m.Cellphone == aCheckData.TheValue.ToString());
                        if (aContactData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aContactData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應聯絡人手機資料", aCheckData.DisplayName);
                    }
                };

                foreach (DataRow aRow in aTable.Rows)
                {
                    Dictionary<string, object> InputContent = new Dictionary<string, object>();
                    foreach (DataColumn theColumn in aTable.Columns)
                    {
                        InputContent.Add(theColumn.Caption, aRow[theColumn.Caption]);
                        if (theColumn.ColumnName == "CONTACT_ID")
                            codeFlag = true;
                    }
                    aCheckDictionary.SetFieldValue(InputContent);
                    aCheckDictionary.DoCheck();

                    aCheckDictionary.CheckData.ForEach(delegate(TheDictionaryCheckData aCheckData)
                    {
                        if (aCheckData.TheValue != null)
                        {
                            if (!aCheckData.IsOK)
                                aRow.SetColumnError(aCheckData.FieldName, aCheckData.ErrorMsg);
                            else
                                aRow[aCheckData.FieldName] = aCheckData.TheResult;
                        }
                    });

                    //if (codeFlag)
                    //{
                    //    //聯絡人姓名
                    //    string sql = "CONTACT_NAME = '" + aRow["CONTACT_NAME"].ToString() + "'";
                    //    if (aTable.Select(sql).Count() > 1)
                    //    {
                    //        aRow.SetColumnError("CONTACT_NAME", "聯絡人姓名重複");
                    //        aRow.RowError = "驗證錯誤";
                    //    }
                    //}

                    aRow.RowError = aRow.HasErrors ? "驗證錯誤" : "";
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //取得欄位驗證個相關資料
        private DataTableCellValidateData GetValidData()
        {
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            DataTableCellValidateData aAns = new DataTableCellValidateData();

            string sql = "select CONTACT_ID, CONTACT_NAME, CONTACT_CELLPHONE from CON_CONTACT" + "\n\r";

            DataSet DataSet = this.ExecuteSql(sql, connection, transaction);

            aAns.ContactData = DataSet.Tables[0].AsEnumerable().Select(m => new ContactData { ID = m.Field<int>("CONTACT_ID"), Name = m.Field<string>("CONTACT_NAME"), Cellphone = m.Field<string>("CONTACT_CELLPHONE") }).ToList();
            return aAns;
        }

        //--------------------------------------欄位驗證時候的資料-------------
        public class DataTableCellValidateData
        {
            public List<ContactData> ContactData { get; set; }
            
            public DataTableCellValidateData()
            {
                ContactData = new List<ContactData>();
            }
        }

        //---------------------------------------資料驗證-----------------------------------------
        private bool DataTableDataValidate(DataTable aTable)
        {
            return true;
        }

        //---------------------------------------寫入資料庫---------------------------------------
        private bool DataTableDataInsert(DataTable aTable)
        {
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string sql = "";
            int columnCount = 0;

            try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select USERNAME from USERS where USERID= '" + userid + "'";
                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

                sql = "";
                foreach (DataRow aRow in aTable.Rows)
                {
                    columnCount = aRow.ItemArray.Length;
                    sql = sql + "insert into CON_CONTACT_MEMO (";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + column.ColumnName + ",";
                    }
                    sql = sql + " CREATE_MAN,CREATE_DATE,UPDATE_MAN,UPDATE_DATE)";
                    sql = sql + " select '";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + (aRow[column].ToString().Trim() == "" ? "null" : aRow[column].ToString().Trim()) + "','";
                    }
                    sql = sql + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                }
                sql = sql.Replace("'null'", "null");
                this.ExecuteSql(sql, connection, transaction);
                dsUSERS.Dispose();
                transaction.Commit();
                return true;
            }
            catch { transaction.Rollback(); return false; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        //聯絡人資料
        public class ContactData
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Cellphone { get; set; }
        }

        public class ImportData
        {
            public int CONTACT_ID { get; set; }
            public string MEMO_DATE { get; set; }
            public string MEMO_CONTENT { get; set; }
            public string MEMO_USER { get; set; }
        }
    }
}
