using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using System.Reflection;
using Newtonsoft;
using Newtonsoft.Json;
using JBTool;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using NPOI.SS.UserModel;

namespace _CON_Normal_ContactActivity
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

        public const int G_HeadRowIndex = 0;

        public object[] checkActivityName(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string contactID = parm[0];
            string activityName = parm[1];

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
                string sql = "select count(*) as cnt from CON_CONTACT_ACTIVITY where CONTACT_ID = " + contactID + " and ltrim(ACTIVITY_NAME) = '" + activityName + "'";

                DataSet dsCON_CONTACT_ACTIVITY = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsCON_CONTACT_ACTIVITY.Tables[0].Rows[0]["cnt"].ToString();

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] checkActivityFileName(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string ContactActivityID = parm[0];
            string ActivityFileName = parm[1];

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
                string sql = "select count(*) as cnt from CON_ACTIVITY_FILE where CONTACT_ACTIVITY_ID = " + ContactActivityID + " and ltrim(ACTIVIT_FILE) = '" + ActivityFileName + "'";

                DataSet dsCON_ACTIVITY_FILE = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsCON_ACTIVITY_FILE.Tables[0].Rows[0]["cnt"].ToString();

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }


        private void ucCON_CONTACT_ACTIVITY_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucCON_CONTACT_ACTIVITY.conn, ucCON_CONTACT_ACTIVITY.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            var command = ucCON_CONTACT_ACTIVITY.conn.CreateCommand();
            command.CommandText = "SELECT SCOPE_IDENTITY()";
            command.Transaction = ucCON_CONTACT_ACTIVITY.trans;
            int newID = Convert.ToInt32(command.ExecuteScalar());

            var dataset = (DataSet)ucCON_CONTACT_ACTIVITY.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT_ACTIVITY);
            var table = (string)ucCON_CONTACT_ACTIVITY.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT_ACTIVITY);
            DataTable dt = ucCON_CONTACT_ACTIVITY.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                if (dataset.Tables[table].Rows[i].RowState == DataRowState.Added)
                {
                    dataset.Tables[table].Rows[i]["CONTACT_ACTIVITY_ID"] = newID;
                    dataset.Tables[table].Rows[i]["CREATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["UPDATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["CREATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataset.Tables[table].Rows[i]["UPDATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                logInfo_CON_CONTACT_ACTIVITY.Log(dataset.Tables[table].Rows[i], dt, ucCON_CONTACT_ACTIVITY.conn, ucCON_CONTACT_ACTIVITY.trans, ucCON_CONTACT_ACTIVITY.SelectCmd.KeyFields);
            }
        }

        private void ucCON_CONTACT_ACTIVITY_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucCON_CONTACT_ACTIVITY.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT_ACTIVITY);
            var table = (string)ucCON_CONTACT_ACTIVITY.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT_ACTIVITY);
            DataTable dt = ucCON_CONTACT_ACTIVITY.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_CONTACT_ACTIVITY.Log(dataset.Tables[table].Rows[i], dt, ucCON_CONTACT_ACTIVITY.conn, ucCON_CONTACT_ACTIVITY.trans, ucCON_CONTACT_ACTIVITY.SelectCmd.KeyFields);
            }
        }

        private void ucCON_CONTACT_ACTIVITY_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucCON_CONTACT_ACTIVITY.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT_ACTIVITY);
            var table = (string)ucCON_CONTACT_ACTIVITY.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_CONTACT_ACTIVITY);
            DataTable dt = ucCON_CONTACT_ACTIVITY.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_CONTACT_ACTIVITY.Log(dataset.Tables[table].Rows[i], dt, ucCON_CONTACT_ACTIVITY.conn, ucCON_CONTACT_ACTIVITY.trans, ucCON_CONTACT_ACTIVITY.SelectCmd.KeyFields);
            }
        }

        private void ucCON_ACTIVITY_FILE_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucCON_ACTIVITY_FILE.conn, ucCON_ACTIVITY_FILE.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            var command = ucCON_ACTIVITY_FILE.conn.CreateCommand();
            command.CommandText = "SELECT SCOPE_IDENTITY()";
            command.Transaction = ucCON_ACTIVITY_FILE.trans;
            int newID = Convert.ToInt32(command.ExecuteScalar());

            var dataset = (DataSet)ucCON_ACTIVITY_FILE.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_FILE);
            var table = (string)ucCON_ACTIVITY_FILE.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_FILE);
            DataTable dt = ucCON_ACTIVITY_FILE.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                if (dataset.Tables[table].Rows[i].RowState == DataRowState.Added)
                {
                    dataset.Tables[table].Rows[i]["ACTIVITY_FILE_ID"] = newID;
                    dataset.Tables[table].Rows[i]["CREATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["UPDATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["CREATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataset.Tables[table].Rows[i]["UPDATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                logInfo_CON_ACTIVITY_FILE.Log(dataset.Tables[table].Rows[i], dt, ucCON_ACTIVITY_FILE.conn, ucCON_ACTIVITY_FILE.trans, ucCON_ACTIVITY_FILE.SelectCmd.KeyFields);
            }
        }

        private void ucCON_ACTIVITY_FILE_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucCON_ACTIVITY_FILE.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_FILE);
            var table = (string)ucCON_ACTIVITY_FILE.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_FILE);
            DataTable dt = ucCON_ACTIVITY_FILE.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_ACTIVITY_FILE.Log(dataset.Tables[table].Rows[i], dt, ucCON_ACTIVITY_FILE.conn, ucCON_ACTIVITY_FILE.trans, ucCON_ACTIVITY_FILE.SelectCmd.KeyFields);
            }
        }

        private void ucCON_ACTIVITY_FILE_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucCON_ACTIVITY_FILE.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_FILE);
            var table = (string)ucCON_ACTIVITY_FILE.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_FILE);
            DataTable dt = ucCON_ACTIVITY_FILE.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_ACTIVITY_FILE.Log(dataset.Tables[table].Rows[i], dt, ucCON_ACTIVITY_FILE.conn, ucCON_ACTIVITY_FILE.trans, ucCON_ACTIVITY_FILE.SelectCmd.KeyFields);
            }
        }

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
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_NAME", DisplayName = "活動名稱" });
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
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_NAME", DisplayName = "活動名稱", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "CONTACT_ID", DisplayName = "聯絡人手機", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_YEAR", DisplayName = "活動年度", IsRequired = false, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Year });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_DATE", DisplayName = "活動起始日期", IsRequired = false, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_DATE", DisplayName = "活動截止日期", IsRequired = false, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_TIME", DisplayName = "活動起始時間", IsRequired = false, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Time24Hours });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_TIME", DisplayName = "活動截止時間", IsRequired = false, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Time24Hours});
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_TYPE_ID", DisplayName = "活動類型", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_IDENTITY_ID", DisplayName = "活動身分別", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_CHILD_TYPE_ID", DisplayName = "活動子類別", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_LEVEL_ID", DisplayName = "活動級別", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_STATUS_ID", DisplayName = "活動狀況", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "ACTIVITY_EVALUATE_ID", DisplayName = "活動評估", IsRequired = false, IsUserCheck = true });

                aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
                {
                    aCheckData.IsOK = false;
                    if (aCheckData.FieldName == "ACTIVITY_NAME")
                    {
                        var aContactActivityData = ValidData.ContactActivityData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aContactActivityData == null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("【{0}】資料已存在", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "CONTACT_ID")
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
                    else if (aCheckData.FieldName == "ACTIVITY_TYPE_ID")
                    {
                        var aActivityTypeData = ValidData.ActivityTypeData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aActivityTypeData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aActivityTypeData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應活動類型資料", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "ACTIVITY_IDENTITY_ID")
                    {
                        var aActivityIdentityData = ValidData.ActivityIdentityData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aActivityIdentityData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aActivityIdentityData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應活動身分別資料", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "ACTIVITY_CHILD_TYPE_ID")
                    {
                        var aActivityChildTypeData = ValidData.ActivityChildTypeData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aActivityChildTypeData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aActivityChildTypeData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應活動子類別資料", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "ACTIVITY_LEVEL_ID")
                    {
                        var aActivityLevelData = ValidData.ActivityLevelData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aActivityLevelData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aActivityLevelData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應活動級別資料", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "ACTIVITY_STATUS_ID")
                    {
                        var aActivityStatusData = ValidData.ActivityStatusData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aActivityStatusData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aActivityStatusData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應活動狀況資料", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "ACTIVITY_EVALUATE_ID")
                    {
                        var aActivityEvaluateData = ValidData.ActivityEvaluateData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                        if (aActivityEvaluateData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aActivityEvaluateData.ID;
                        }
                        else
                            aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應活動評估資料", aCheckData.DisplayName);
                    }
                };

                foreach (DataRow aRow in aTable.Rows)
                {
                    Dictionary<string, object> InputContent = new Dictionary<string, object>();
                    foreach (DataColumn theColumn in aTable.Columns)
                    {
                        InputContent.Add(theColumn.Caption, aRow[theColumn.Caption]);
                        if (theColumn.ColumnName == "ACTIVITY_NAME")
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

                    if (codeFlag)
                    {
                        //活動名稱
                        string sql = "ACTIVITY_NAME = '" + aRow["ACTIVITY_NAME"].ToString() + "'";
                        if (aTable.Select(sql).Count() > 1)
                        {
                            aRow.SetColumnError("ACTIVITY_NAME", "活動名稱重複");
                            aRow.RowError = "驗證錯誤";
                        }
                    }

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
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'ACTIVITY_TYPE' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'ACTIVITY_IDENTITY' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'ACTIVITY_CHILD_TYPE' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'ACTIVITY_LEVEL' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'ACTIVITY_STATUS' ORDER BY SORT" + "\n\r";
            sql = sql + "select CODE_ID,NAME FROM CON_SHARECODE WHERE FIELDNAME = 'ACTIVITY_EVALUATE' ORDER BY SORT" + "\n\r";
            sql = sql + "select CONTACT_ACTIVITY_ID, ACTIVITY_NAME from CON_CONTACT_ACTIVITY" + "\n\r";

            DataSet DataSet = this.ExecuteSql(sql, connection, transaction);

            aAns.ContactData = DataSet.Tables[0].AsEnumerable().Select(m => new ContactData { ID = m.Field<int>("CONTACT_ID"), Name = m.Field<string>("CONTACT_NAME"), Cellphone = m.Field<string>("CONTACT_CELLPHONE") }).ToList();
            aAns.ActivityTypeData = DataSet.Tables[1].AsEnumerable().Select(m => new ActivityTypeData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ActivityIdentityData = DataSet.Tables[2].AsEnumerable().Select(m => new ActivityIdentityData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ActivityChildTypeData = DataSet.Tables[3].AsEnumerable().Select(m => new ActivityChildTypeData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ActivityLevelData = DataSet.Tables[4].AsEnumerable().Select(m => new ActivityLevelData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ActivityStatusData = DataSet.Tables[5].AsEnumerable().Select(m => new ActivityStatusData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ActivityEvaluateData = DataSet.Tables[6].AsEnumerable().Select(m => new ActivityEvaluateData { ID = m.Field<int>("CODE_ID"), Name = m.Field<string>("NAME") }).ToList();
            aAns.ContactActivityData = DataSet.Tables[7].AsEnumerable().Select(m => new ContactActivityData { ID = m.Field<int>("CONTACT_ACTIVITY_ID"), Name = m.Field<string>("ACTIVITY_NAME") }).ToList();
            return aAns;
        }

        //--------------------------------------欄位驗證時候的資料-------------
        public class DataTableCellValidateData
        {
            public List<ContactData> ContactData { get; set; }
            public List<ContactActivityData> ContactActivityData { get; set; }
            public List<ActivityTypeData> ActivityTypeData { get; set; }
            public List<ActivityIdentityData> ActivityIdentityData { get; set; }
            public List<ActivityChildTypeData> ActivityChildTypeData { get; set; }
            public List<ActivityLevelData> ActivityLevelData { get; set; }
            public List<ActivityStatusData> ActivityStatusData { get; set; }
            public List<ActivityEvaluateData> ActivityEvaluateData { get; set; }

            public DataTableCellValidateData()
            {
                ContactData = new List<ContactData>();
                ContactActivityData = new List<ContactActivityData>();
                ActivityTypeData = new List<ActivityTypeData>();
                ActivityIdentityData = new List<ActivityIdentityData>();
                ActivityChildTypeData = new List<ActivityChildTypeData>();
                ActivityLevelData = new List<ActivityLevelData>();
                ActivityStatusData = new List<ActivityStatusData>();
                ActivityEvaluateData = new List<ActivityEvaluateData>();
            }
        }

        //---------------------------------------資料驗證-----------------------------------------
        private bool DataTableDataValidate(DataTable aTable)
        {
            string beginDate = "";
            string endDate = "";
            string beginTime = "";
            string endTime = "";

            int cnt = 0;

            bool flag = false;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            //1. 判斷請假起始日期不可大於截止日期
            //2. 判斷請假起始時間不可大於截止時間
            try
            {
                foreach (DataRow aRow in aTable.Rows)
                {
                    flag = false;
                    beginDate = aRow["BEGIN_DATE"].ToString();
                    endDate = aRow["END_DATE"].ToString();
                    beginTime = aRow["BEGIN_TIME"].ToString();
                    endTime = aRow["END_TIME"].ToString();

                    //1. 判斷活動起始日期不可大於截止日期
                    if (DateTime.Parse(beginDate) > DateTime.Parse(endDate))
                    {
                        aRow.SetColumnError("BEGIN_DATE", "活動起始日期不可大於截止日期");
                        flag = true;
                    }

                    //2. 判斷活動起始時間不可大於截止時間
                    if (int.Parse(beginTime) >= int.Parse(endTime))
                    {
                        aRow.SetColumnError("BEGIN_TIME", "活動起始時間不可大於截止時間");
                        flag = true;
                    }

                    if (flag)
                        aRow.RowError = "驗證錯誤";
                }
                return true;
            }
            catch { transaction.Rollback(); return false; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
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
                    sql = sql + "insert into CON_CONTACT_ACTIVITY (";
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

        //活動記錄
        public class ContactActivityData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //活動類型
        public class ActivityTypeData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //活動身分別
        public class ActivityIdentityData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //活動子類別
        public class ActivityChildTypeData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //活動級別
        public class ActivityLevelData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //活動狀況
        public class ActivityStatusData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        //活動評估
        public class ActivityEvaluateData
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class ImportData
        {
            public int CONTACT_ID { get; set; }
            public string ACTIVITY_NAME { get; set; }
            public string ACTIVITY_YEAR { get; set; }
            public string BEGIN_DATE { get; set; }
            public string END_DATE { get; set; }
            public string BEGIN_TIME { get; set; }
            public string END_TIME { get; set; }
            public string ACTIVITY_TYPE_ID { get; set; }
            public string ACTIVITY_IDENTITY_ID { get; set; }
            public string ACTIVITY_CHILD_TYPE_ID { get; set; }
            public int ACTIVITY_LEVEL_ID { get; set; }
            public int ACTIVITY_STATUS_ID { get; set; }
            public int ACTIVITY_EVALUATE_ID { get; set; }
            public int ACTIVITY_ADDR { get; set; }
            public int ACTIVITY_PERSON { get; set; }
            public int ACTIVITY_WORKS { get; set; }
            public int ACTIVITY_DESCRIPTION { get; set; }
        }

        private void ucCON_ACTIVITY_GIFT_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucCON_ACTIVITY_GIFT.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_GIFT);
            var table = (string)ucCON_ACTIVITY_GIFT.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_GIFT);
            DataTable dt = ucCON_ACTIVITY_GIFT.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_ACTIVITY_GIFT.Log(dataset.Tables[table].Rows[i], dt, ucCON_ACTIVITY_GIFT.conn, ucCON_ACTIVITY_GIFT.trans, ucCON_ACTIVITY_GIFT.SelectCmd.KeyFields);
            }
        }

        private void ucCON_ACTIVITY_GIFT_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucCON_ACTIVITY_GIFT.conn, ucCON_ACTIVITY_GIFT.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            var command = ucCON_ACTIVITY_GIFT.conn.CreateCommand();
            command.CommandText = "SELECT SCOPE_IDENTITY()";
            command.Transaction = ucCON_ACTIVITY_GIFT.trans;
            int newID = Convert.ToInt32(command.ExecuteScalar());

            var dataset = (DataSet)ucCON_ACTIVITY_GIFT.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_GIFT);
            var table = (string)ucCON_ACTIVITY_GIFT.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_GIFT);
            DataTable dt = ucCON_ACTIVITY_GIFT.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                if (dataset.Tables[table].Rows[i].RowState == DataRowState.Added)
                {
                    dataset.Tables[table].Rows[i]["ACTIVITY_GIFT_ID"] = newID;
                    dataset.Tables[table].Rows[i]["CREATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["UPDATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["CREATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataset.Tables[table].Rows[i]["UPDATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                logInfo_CON_ACTIVITY_GIFT.Log(dataset.Tables[table].Rows[i], dt, ucCON_ACTIVITY_GIFT.conn, ucCON_ACTIVITY_GIFT.trans, ucCON_ACTIVITY_GIFT.SelectCmd.KeyFields);
            }
        }

        private void ucCON_ACTIVITY_GIFT_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucCON_ACTIVITY_GIFT.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_GIFT);
            var table = (string)ucCON_ACTIVITY_GIFT.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucCON_ACTIVITY_GIFT);
            DataTable dt = ucCON_ACTIVITY_GIFT.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_CON_ACTIVITY_GIFT.Log(dataset.Tables[table].Rows[i], dt, ucCON_ACTIVITY_GIFT.conn, ucCON_ACTIVITY_GIFT.trans, ucCON_ACTIVITY_GIFT.SelectCmd.KeyFields);
            }
        }
    }
}
