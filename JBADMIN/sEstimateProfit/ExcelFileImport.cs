﻿using JBTool;
using Srvtools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TheExcelFileImport
{
    /// <summary>上傳作業</summary>
    public class ExcelFileImport
    {
        /// <summary>檔案</summary>
        public MemoryStream FileStream { get; set; }

        /// <summary>工作表Index</summary>
        public int SheetIndex { get; set; }

        /// <summary>標頭字典檔</summary>
        public Dictionary<string, object> HealderParameter { get; set; }

        public int CompanyID { get; set; }

        public string sYM { get; set; }

        /// <summary>執行者</summary>
        public string CreateMan { get; set; }

        /// <summary>執行者</summary>
        public string UserID { get; set; }

        /// <summary>是否為超級使用者</summary>
        public bool IsSuperUser { get; set; }

        /// <summary>使用者群組</summary>
        public List<string> UserGroup { get; set; }

        /// <summary>連線用</summary>
        public DataModule theDataModule { get; set; }

        /// <summary>原匯入欄位</summary>
        protected List<SystemImportField> ImportFields { get; set; }

        /// <summary>將上傳欄位</summary>
        protected List<SystemImportField> UploadFields { get; set; }

        public ExcelFileImport()
        {
            this.ImportFields = new List<SystemImportField>();
            this.UploadFields = new List<SystemImportField>();
        }

        /// <summary>取得錯誤欄位名稱</summary>        
        protected string GetErrorColumnName(SystemImportField aField)
        {
            //如果本來就有，回傳原本的欄位，沒有的話要用統一欄位輸出
            return (this.ImportFields.Contains(aField) ? aField : SystemImportField.AcnoAll).ToString();
        }

        /// <summary>執行匯入並取得結果</summary>        
        public TheJsonResult GetTheFileImportResult()
        {
            //表頭字典檔轉換
            var Headler = this.GetHeadler(this.HealderParameter);

            //取得資料Table
            var ExcelDataTable = this.GetFileDataTable(Headler);

            //設定原本輸入的欄位然後加入補齊的欄位
            SetImportHeadler(ExcelDataTable);

            //欄位對應程序
            var aFieldProcess = GetFieldProcessExecutor();

            //欄位驗證
            if (!InputValidate(ExcelDataTable, aFieldProcess))
            {
                //var ErrorList = ExcelDataTable.AsEnumerable().Where(m => m.HasErrors).ToList();
                return new TheJsonResult { IsOK = false, ErrorMsg = "資料驗證錯誤", Result = NPOIHelper.SetErrorMemoToSheet(ExcelDataTable, new MemoryStream(this.FileStream.ToArray()), SheetIndex, Headler) };
            }

            //寫入EstimateProfitImport
            InputValidate2(ExcelDataTable, aFieldProcess);
           
            //寫入損益資料
            if (!DataUpload()) return new TheJsonResult { IsOK = false, ErrorMsg = "上傳資料錯誤", Result = null };

            return new TheJsonResult { IsOK = true, ErrorMsg = "執行成功", Result = null };
            
        }

        /// <summary>取得標題</summary>
        protected Dictionary<string, int> GetHeadler(Dictionary<string, object> HeadlerParameter)
        {
            try
            {
                Dictionary<string, int> theResult = new Dictionary<string, int>();
                int tempIndex = 0;

                //所有的定義欄位
                var AllFields = Enum.GetNames(typeof(SystemImportField)).ToList();
                foreach (var aHead in HeadlerParameter)
                {
                    if (!AllFields.Contains(aHead.Key)) throw new TheUserDefinedException(string.Format("標題列【{0}】找不到對應", aHead.Key));
                    else
                    {
                        if (aHead.Value.ToString().Length == 0) continue;
                        if (Int32.TryParse(aHead.Value.ToString(), out tempIndex)) theResult.Add(aHead.Key, tempIndex);
                        else throw new TheUserDefinedException(string.Format("標題列【{0}】索引值格式錯誤", aHead.Key));
                    }
                }

                //檢查必須要有的欄位                
                foreach (SystemImportField aImportField in Enum.GetValues(typeof(SystemImportField)))
                {
                    if (aImportField.IsRequired() && !theResult.ContainsKey(aImportField.ToString()))
                        throw new TheUserDefinedException(string.Format("標題列【{0}】沒有輸入", aImportField.ToDisplayName()));
                }
                return theResult;
            }
            catch (TheUserDefinedException ex) { throw ex; }
            catch (Exception) { throw new TheUserDefinedException("轉換標頭字典檔錯誤"); }
        }

        /// <summary>取得資料</summary>
        protected DataTable GetFileDataTable(Dictionary<string, int> aHeadlerDictionary)
        {
            try
            {
                return NPOIHelper.GetDataTableFromSheet(new MemoryStream(this.FileStream.ToArray()), SheetIndex, aHeadlerDictionary);
            }
            catch (Exception ex) { throw new TheUserDefinedException("取得檔案資料錯誤"); }
        }

        /// <summary>設定原本輸入的欄位然後加入補齊的欄位</summary>
        protected void SetImportHeadler(DataTable ExcelDataTable)
        {
            try
            {
                //設定匯入的Title 上傳的Title
                this.ImportFields = new List<SystemImportField>();
                this.UploadFields = new List<SystemImportField>();
                foreach (DataColumn aColumn in ExcelDataTable.Columns)
                {
                    this.ImportFields.Add((SystemImportField)Enum.Parse(typeof(SystemImportField), aColumn.ColumnName));
                    this.UploadFields.Add((SystemImportField)Enum.Parse(typeof(SystemImportField), aColumn.ColumnName));
                }

                //檢查補齊的欄位                
                foreach (SystemImportField aImportField in Enum.GetValues(typeof(SystemImportField)))
                {
                    if (aImportField.IsTitleFixed() && !this.ImportFields.Contains(aImportField))
                    {
                        ExcelDataTable.Columns.Add(new DataColumn { ColumnName = aImportField.ToString(), DefaultValue = "" });
                        this.UploadFields.Add(aImportField);
                    }
                }
            }
            catch (Exception) { throw new TheUserDefinedException("補齊欄位錯誤"); }
        }

        /// <summary>取得欄位對應程序</summary>        
        protected FieldProcessExecutor GetFieldProcessExecutor()
        {
            try
            {
                //新的對應程序
                var aProcess = new FieldProcessExecutor(this.theDataModule);

                //新增各個欄位對應處理程序
                aProcess.AllFieldProcess.Add(new AcnoAllFieldProcess());
                aProcess.AllFieldProcess.Add(new CostCenterIDFieldProcess());

                
                //設定字典
                aProcess.SetFieldDictionary();
                return aProcess;
            }
            catch (TheUserDefinedException ex) { throw ex; }
            catch (Exception) { throw new TheUserDefinedException("取得欄位對應程序錯誤"); }
        }

        #region -----欄位驗證-----

        /// <summary>欄位驗證</summary>   
        protected bool InputValidate(DataTable CheckTable, FieldProcessExecutor FieldValidate)
        {
            bool tempBool = true;
            //資料檢查
            foreach (DataRow aRow in CheckTable.Rows)
            {
                //如果都沒有輸入就不用檢測了
                if (!DataRowHasValue(aRow)) continue;
                try
                {
                    //先做一般驗證，再做邏輯相關的驗證                  
                    if (GeneralRowValidate(aRow, FieldValidate)) CustomRowValidator(aRow);
                    //CustomRowValidator(aRow);
                }
                catch (Exception ex) { aRow.SetColumnError(SystemImportField.AcnoAll.ToString(), "欄位驗證時錯誤"); }
            }

            //如果有錯就回傳
            if (CheckTable.HasErrors) return false;          

            return tempBool;
        }

        ///// <summary>欄位驗證</summary>   
        protected void InputValidate2(DataTable CheckTable, FieldProcessExecutor FieldValidate)
        {
            try
            {
                //寫入EstimateProfitImport
                IDbConnection connection = (IDbConnection)this.theDataModule.AllocateConnection(this.theDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                IDbTransaction transaction = connection.BeginTransaction();

                //寫入新的判斷
                InsertEstimateProfitImport(connection, transaction, CheckTable);
            }
            catch (Exception) { throw new TheUserDefinedException("型態錯誤"); }

        }
        /// <summary>寫入EstimateProfitImport</summary>
        protected void InsertEstimateProfitImport(IDbConnection connection, IDbTransaction transaction, DataTable aTable)
        {
            try
            {
                //新增寫入
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.KeepIdentity, (SqlTransaction)transaction))
                {
                    bulkCopy.BatchSize = 500;

                    foreach (var aField in this.UploadFields)
                    {
                        bulkCopy.ColumnMappings.Add(aField.ToString(), aField.ToString());
                    }
                    bulkCopy.ColumnMappings.Add("UserID", "UserID");
                    bulkCopy.ColumnMappings.Add("sYM", "sYM");
                    bulkCopy.ColumnMappings.Add("CreateBy", "CreateBy");
                    bulkCopy.ColumnMappings.Add("CreateDate", "CreateDate");

                    //有資料才傳(也因只有判定有資料的Row)                    
                    var FilterDataTable = aTable.AsEnumerable().Where(aRow => DataRowHasValue(aRow)).ToArray();
                    aTable.Columns.Add(new DataColumn { ColumnName = "UserID", DefaultValue = this.UserID });
                    aTable.Columns.Add(new DataColumn { ColumnName = "sYM", DefaultValue = this.sYM });
                    aTable.Columns.Add(new DataColumn { ColumnName = "CreateBy", DefaultValue = this.CreateMan });
                    aTable.Columns.Add(new DataColumn { ColumnName = "CreateDate", DefaultValue = DateTime.Now });
                    bulkCopy.DestinationTableName = "dbo.[EstimateProfitImport]";
                    bulkCopy.WriteToServer(FilterDataTable);
                }

                transaction.Commit();
            }
            catch (Exception) { transaction.Rollback(); throw new TheUserDefinedException("型態錯誤"); }
            finally { this.theDataModule.ReleaseConnection(this.theDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }

        }

        /// <summary>刪除舊資料</summary>
        protected void DeleteEstimateProfitImport(IDbConnection connection, IDbTransaction transaction)
        {
            SqlCommand commandDelete = new SqlCommand("", (SqlConnection)connection, (SqlTransaction)transaction);
            commandDelete.CommandText = @"
Delete	T
From	[dbo].EstimateProfitImport as T
Where	UserID = @UserID";

            commandDelete.Parameters.Add(new SqlParameter { ParameterName = "@UserID", Value = this.UserID });
            commandDelete.ExecuteNonQuery();
        }

        /// <summary>Row是否有輸入</summary>        
        protected bool DataRowHasValue(DataRow aRow)
        {
            return aRow.ItemArray.Any(m => m.ToString() != "");
        }

        /// <summary>一般資料列檢測</summary>        
        protected bool GeneralRowValidate(DataRow aRow, FieldProcessExecutor ValidateData)
        {
            //所有資料欄位
            foreach (var aField in this.UploadFields)
            {
                //輸入數值
                var theValue = aRow[aField.ToString()].ToString().Trim();
                
                //必填(輸入)欄位
                if (theValue.Length == 0)
                {
                    if (aField.IsRequired()) aRow.SetColumnError(this.GetErrorColumnName(aField), string.Format("【{0}】沒有輸入", aField.ToDisplayName()));
                    else aRow[aField.ToString()] = null;
                    continue;
                }

                //針對欄位檢查
                switch (aField)
                {                    
                    default:
                        //是否需要對應
                        if (aField.IsBinding())
                        {
                            theValue = ValidateData.GetFieldValue(aField, theValue);
                            if (theValue != null) aRow[aField.ToString()] = theValue;
                            else aRow.SetColumnError(this.GetErrorColumnName(aField), string.Format("【{0}】找不到對應", aField.ToDisplayName()));
                        }
                        else aRow[aField.ToString()] = theValue;
                        break;
                }


            }

            //有錯要回傳
            return !aRow.HasErrors;
        }

        /// <summary>自訂資料列檢測</summary>        
        protected bool CustomRowValidator(DataRow aRow)
        {
          
            //是否需要成本中心
            var ValidateRow = GetCostCenterIDValidate(aRow);
            //無資料
            if (ValidateRow == null)
            {
                aRow.SetColumnError(SystemImportField.AcnoAll.ToString(), "此科目代號不存在");
                return false;
            }
            else if (ValidateRow != null) //找到資料了
            {
                if (ValidateRow["bCostCenterID"].ToString() == "True" && aRow[SystemImportField.CostCenterID.ToString()].ToString()=="")
                {
                    aRow.SetColumnError(SystemImportField.CostCenterID.ToString(), "此項需要成本中心");
                    return false;
                }
            }            

            //有錯要回傳
            return !aRow.HasErrors;
        }

        /// <summary>是否要成本中心=>由Acno+SubAcno推</summary>
        protected DataRow GetCostCenterIDValidate(DataRow aRow)
        {
            string SQL = @"
select bCostCenterID from glAccountItem 
where CompanyID=@CompanyID and Acno+SubAcno=Isnull(@AcnoAll,'') 
";
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@CompanyID", 1));
            Parameter.Add(new SqlParameter("@AcnoAll", aRow[SystemImportField.AcnoAll.ToString()]));

            DataSet DataSet = SQL_Tools.GetDataSet(this.theDataModule, SQL, Parameter);
            if (DataSet.Tables[0].Rows.Count > 0) return DataSet.Tables[0].Rows[0];
            return null;
        }

       

       
        #endregion

        /// <summary>損益資料寫入</summary>
        protected bool DataUpload()
        {

            bool Ans = false;
            IDbConnection connection = (IDbConnection)this.theDataModule.AllocateConnection(this.theDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();


            string SQL = @"
exec procInsertEstimateProfitImportData @sYM,@UserID,@CreateBy";
          
            try
            {
                var Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@sYM", this.sYM));
                Parameter.Add(new SqlParameter("@UserID", this.UserID));
                Parameter.Add(new SqlParameter("@CreateBy", this.CreateMan));

                DataSet DataSet = SQL_Tools.GetDataSet(this.theDataModule, SQL, Parameter);

                Ans = true;

            }
            catch { transaction.Rollback(); }
            finally { this.theDataModule.ReleaseConnection(this.theDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }

            return Ans;
          
        }

    }

    #region =====================================欄位資料==========================================

    /// <summary>系統對照欄</summary>
    public enum SystemImportField
    {
        /// <summary>科目</summary>
        [EnumAttribute(FieldName = "科目代號", IsRequired = true, IsBinding = true)]
        AcnoAll,
        /// <summary>成本中心</summary>
        [EnumAttribute(FieldName = "成本中心", IsBinding = true)]
        CostCenterID,
        /// <summary>內容</summary>
        [EnumAttribute(FieldName = "內容")]
        Describe,
        /// <summary>金額</summary>
        [EnumAttribute(FieldName = "金額", IsRequired = true)]
        Amt    
    }

    /// <summary>自訂屬性</summary>
    public class EnumAttribute : Attribute
    {
        /// <summary>欄位名稱</summary>
        public string FieldName { get; set; }

        /// <summary>是否為必要欄位</summary>
        public bool IsRequired { get; set; }

        /// <summary>是否需補齊欄位</summary>
        public bool IsTitleFixed { get; set; }

        /// <summary>是否需對應</summary>
        public bool IsBinding { get; set; }

        /// <summary>是否確認前筆資料</summary>
        public bool IsPrevCheck { get; set; }

        public EnumAttribute()
        {
            this.FieldName = "";
            this.IsBinding = false;
            this.IsTitleFixed = false;
            this.IsRequired = false;
            this.IsPrevCheck = false;
        }
    }

    /// <summary>擴充功能</summary>
    static public class FieldProcessExtensions
    {
        /// <summary>轉換成描述</summary>        
        static public string ToDisplayName(this SystemImportField aSystemImportField)
        {
            var FieldValue = GetAttributeValue(aSystemImportField, "FieldName");
            return FieldValue == null ? aSystemImportField.ToString() : (string)FieldValue;
        }

        /// <summary>是否需對應</summary>        
        static public bool IsBinding(this SystemImportField aSystemImportField)
        {
            var FieldValue = GetAttributeValue(aSystemImportField, "IsBinding");
            return FieldValue == null ? false : (bool)FieldValue;
        }

        /// <summary>是否需補齊欄位</summary>        
        static public bool IsTitleFixed(this SystemImportField aSystemImportField)
        {
            var FieldValue = GetAttributeValue(aSystemImportField, "IsTitleFixed");
            return FieldValue == null ? false : (bool)FieldValue;
        }

        /// <summary>是否為必要欄位</summary>        
        static public bool IsRequired(this SystemImportField aSystemImportField)
        {
            var FieldValue = GetAttributeValue(aSystemImportField, "IsRequired");
            return FieldValue == null ? false : (bool)FieldValue;
        }

        /// <summary>是否檢查前一筆要有資料</summary>        
        static public bool IsPrevCheck(this SystemImportField aSystemImportField)
        {
            var FieldValue = GetAttributeValue(aSystemImportField, "IsPrevCheck");
            return FieldValue == null ? false : (bool)FieldValue;
        }

        /// <summary>取得屬性值</summary>        
        static private object GetAttributeValue(SystemImportField aSystemImportField, string propName)
        {
            FieldInfo fi = aSystemImportField.GetType().GetField(aSystemImportField.ToString());
            var attribute = (EnumAttribute)fi.GetCustomAttributes(typeof(EnumAttribute), false).FirstOrDefault();
            if (attribute == null) return null;
            return attribute.GetType().GetProperty(propName).GetValue(attribute, null);
        }
    }

    #endregion

    #region =====================================資料對應處理程序==================================

    /// <summary>欄位處理程序執行</summary>
    public class FieldProcessExecutor
    {
        /// <summary>【執行SQL用到的】</summary>
        public DataModule theDataModule { get; set; }

        /// <summary>欄位處理程序</summary>
        public List<FieldProcess> AllFieldProcess { get; set; }

        /// <summary>所有資料字典檔</summary>
        protected Dictionary<SystemImportField, IFieldBinding> AllDictionary { get; set; }

        public FieldProcessExecutor(DataModule theDataModule)
        {
            this.theDataModule = theDataModule;
            this.AllDictionary = new Dictionary<SystemImportField, IFieldBinding>();
            this.AllFieldProcess = new List<FieldProcess>();
        }

        /// <summary>設定資料字典</summary>
        public void SetFieldDictionary()
        {
            //各自去寫入字典
            foreach (var aProcess in AllFieldProcess)
            {
                aProcess.CreateProcess(this);
                aProcess.SetFieldDictionary(AllDictionary);
            }
        }

        /// <summary>取值</summary>
        public string GetFieldValue(SystemImportField aSystemField, string Input)
        {
            if (!AllDictionary.ContainsKey(aSystemField)) throw new TheUserDefinedException("欄位資料對應錯誤");
            if (!AllDictionary[aSystemField].IsExists(Input)) return null;
            return AllDictionary[aSystemField].GetValue(Input);
        }
    }

    /// <summary>欄位處理</summary>
    public abstract class FieldProcess
    {
        /// <summary>字典檔Key</summary>
        protected SystemImportField ImportFieldKey { get; set; }

        /// <summary>【執行SQL用到的】</summary>
        protected DataModule theDataModule { get; set; }

        /// <summary>產生處理</summary>
        public void CreateProcess(FieldProcessExecutor aProcessMaker)
        {
            this.theDataModule = aProcessMaker.theDataModule;
        }

        /// <summary>設定字典檔</summary>
        public void SetFieldDictionary(Dictionary<SystemImportField, IFieldBinding> aFieldDictionary)
        {
            try
            {
                aFieldDictionary[ImportFieldKey] = GetFieldDictionary();
            }
            catch (TheUserDefinedException ex) { throw ex; }
            catch (Exception) { throw new TheUserDefinedException(string.Format("取得{0}資料錯誤", this.ImportFieldKey.ToDisplayName())); }
        }

        /// <summary>取得設定字典</summary>        
        protected abstract IFieldBinding GetFieldDictionary();
    }

    /// <summary>欄位對應</summary>
    public interface IFieldBinding
    {
        /// <summary>取值</summary>
        string GetValue(string InputValue);

        /// <summary>判定是否存在</summary>        
        bool IsExists(string InputValue);
    }

    #region -----各處理程序與欄位對應-----

    /// <summary>字典欄位</summary>
    public class DictionaryField
    {
        /// <summary>Key</summary>
        public string ID { get; set; }

        /// <summary>代碼</summary>
        public string Code { get; set; }

        /// <summary>名稱</summary>
        public string Name { get; set; }
    }

    /// <summary>字典資料對應</summary>
    public class DictionaryFieldBinding : IFieldBinding
    {
        /// <summary>字典資料對應</summary>
        public List<DictionaryField> KeyValueList { get; set; }

        /// <summary>取值</summary>        
        public string GetValue(string InputValue)
        {
            var Ans = GetByValue(InputValue);
            return Ans == null ? "" : Ans.ID;
        }

        /// <summary>判定存在</summary>        
        public bool IsExists(string InputValue)
        {
            return (GetByValue(InputValue) != null);
        }

        protected DictionaryField GetByValue(string InputValue)
        {
            var Ans = KeyValueList.FirstOrDefault(m => m.Code == InputValue);
            if (Ans == null) Ans = KeyValueList.FirstOrDefault(m => m.Name == InputValue);
            return Ans;
        }
    }


    /// <summary>科目代號</summary>
    public class AcnoAllFieldProcess : FieldProcess
    {

        public AcnoAllFieldProcess()
        {
            this.ImportFieldKey = SystemImportField.AcnoAll;
        }

        protected override IFieldBinding GetFieldDictionary()
        {
            string SQL = @"
Select Acno+SubAcno as ID,Acno+SubAcno as Name
From	glAccountItem
where CompanyID=1
group by Acno+SubAcno
order by Acno+SubAcno
";
        
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@CompanyID", 1));

            //var DataSet = SQL_Tools.GetDataSet(this.theDataModule, SQL, new ArrayList());
            DataSet DataSet = SQL_Tools.GetDataSet(this.theDataModule, SQL, Parameter);

            return new DictionaryFieldBinding
            {
                KeyValueList = DataSet.Tables[0].AsEnumerable().Select(m => new DictionaryField
                {
                    ID = m.Field<string>("ID"),
                    Code = m.Field<string>("ID") ?? "",
                    Name = m.Field<string>("Name") ?? ""
                }).ToList()
            };
        }
    }

    /// <summary>成本中心</summary>
    public class CostCenterIDFieldProcess : FieldProcess
    {

        public CostCenterIDFieldProcess()
        {
            this.ImportFieldKey = SystemImportField.CostCenterID;
        }

        protected override IFieldBinding GetFieldDictionary()
        {
            string SQL = @"
Select	'' as ID,'' as Name
union all
Select	CostCenterID,CostCenterName
From	glCostCenter
";        

            var DataSet = SQL_Tools.GetDataSet(this.theDataModule, SQL, new ArrayList());

            return new DictionaryFieldBinding
            {
                KeyValueList = DataSet.Tables[0].AsEnumerable().Select(m => new DictionaryField
                {
                    ID = m.Field<string>("ID"),
                    Code = m.Field<string>("ID") ?? "",
                    Name = m.Field<string>("Name") ?? ""
                }).ToList()
            };
        }
    }

   


    #endregion

    #endregion
}

