using JBTool;
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

namespace TheExcelFileImport
{
    /// <summary>上傳作業</summary>
    public class ExcelFileImport
    {
        /// <summary>檔案</summary>
        public MemoryStream FileStream { get; set; }

        /// <summary>標頭字典檔</summary>
        public Dictionary<string, object> HealderParameter { get; set; }

        /// <summary>執行者</summary>
        public string CreateMan { get; set; }

        /// <summary>連線用</summary>
        public DataModule theDataModule { get; set; }

        /// <summary>執行匯入並取得結果</summary>        
        public TheJsonResult GetTheFileImportResult()
        {
            //表頭字典檔轉換
            var Headler = this.GetHeadler(this.HealderParameter);            

            //取得資料Table
            var ExcelDataTable = this.GetFileDataTable(Headler);

            //欄位對應程序
            var aExecutor = GetFieldProcessExecutor();

            //欄位驗證
            if (!InputValidate(ExcelDataTable, aExecutor)) return new TheJsonResult { IsOK = false, ErrorMsg = "資料驗證錯誤",
                Result = NPOIHelper.SetErrorMemo(ExcelDataTable, new MemoryStream(this.FileStream.ToArray()), Headler) };

            //寫入資料庫
            if (!DataUpload(ExcelDataTable)) return new TheJsonResult { IsOK = false, ErrorMsg = "上傳資料錯誤", Result = null };

            return new TheJsonResult { IsOK = true, ErrorMsg = "執行成功", Result = null };
        }

        /// <summary>取得標題</summary>
        private Dictionary<string, int> GetHeadler(Dictionary<string, object> HeadlerParameter)
        {
            try
            {
                Dictionary<string, int> theResult = new Dictionary<string, int>();

                //所有的定義欄位都要有
                var AllField = Enum.GetValues(typeof(SystemImportField));
                foreach (SystemImportField aFiled in AllField)
                {
                    if (!HealderParameter.ContainsKey(aFiled.ToString())) 
                        throw new TheUserDefinedException(string.Format("找不到【{0}】標題列", aFiled.ToFieldName()));
                    theResult.Add(aFiled.ToString(), Convert.ToInt32(HealderParameter[aFiled.ToString()]));
                }
                return theResult;
            }
            catch (TheUserDefinedException ex) { throw ex; }
            catch (Exception) { throw new TheUserDefinedException("轉換標頭字典檔錯誤"); }
        }

        /// <summary>取得資料</summary>
        /// <param name="aHeadlerDictionary">標頭字典檔</param>        
        private DataTable GetFileDataTable(Dictionary<string, int> aHeadlerDictionary)
        {
            try
            {
                return NPOIHelper.GetDataTable(new MemoryStream(this.FileStream.ToArray()), aHeadlerDictionary);
            }
            catch (Exception ex) { throw new TheUserDefinedException("取得檔案資料錯誤"); }
        }

        /// <summary>取得欄位對應程序</summary>        
        public FieldProcessExecutor GetFieldProcessExecutor()
        {
            try { return new FieldProcessExecutor(this.theDataModule); }
            catch (TheUserDefinedException ex) { throw ex; }
            catch (Exception) { throw new TheUserDefinedException("取得欄位對應程序錯誤"); }
        }

        /// <summary>欄位驗證</summary>   
        private bool InputValidate(DataTable CheckTable, FieldProcessExecutor FieldValidate)
        {
            //驗證對錯
            bool tempBool = true;

            //資料檢查
            foreach (DataRow aRow in CheckTable.Rows)
            {
                //都沒寫的就不用做了
                if (!DataRowHasValue(aRow)) continue;
                try
                {
                    if (!RequiredFieldValidate(aRow, FieldValidate)) tempBool = false;
                }
                catch (Exception ex)
                {
                    aRow.SetColumnError(SystemImportField.EMPLOYEE_ID.ToString(), "欄位驗證時錯誤");
                    tempBool = false;
                }
            }

            //驗證通過進行資料驗證
            if (tempBool) return DataValidate(CheckTable);
            return tempBool;
        }

        /// <summary>Row是否有輸入</summary>        
        private bool DataRowHasValue(DataRow aRow)
        {
            return aRow.ItemArray.Any(m => m.ToString() != "");
        }

        /// <summary>必要欄位檢測</summary>
        private bool RequiredFieldValidate(DataRow aRow, FieldProcessExecutor ValidateData)
        {
            //員工編號
            var EmployeeID = ValidateData.GetFieldValue(SystemImportField.EMPLOYEE_ID, aRow[SystemImportField.EMPLOYEE_ID.ToString()].ToString());
            if (EmployeeID == null)
            {
                //員工編號
                aRow.SetColumnError(SystemImportField.EMPLOYEE_ID.ToString(), string.Format("【{0}】找不到對應", SystemImportField.EMPLOYEE_ID.ToFieldName()));
                return false;
            }
            else aRow[SystemImportField.EMPLOYEE_ID.ToString()] = EmployeeID;

            //生效日期
            DateTime TempDateTime = new DateTime();
            if (!DateTime.TryParse(aRow[SystemImportField.EFFECT_DATE.ToString()].ToString(), out TempDateTime))
            {
                aRow.SetColumnError(SystemImportField.EFFECT_DATE.ToString(), string.Format("【{0}】格式錯誤", SystemImportField.EFFECT_DATE.ToFieldName()));
                return false;
            }
            else aRow[SystemImportField.EFFECT_DATE.ToString()] = TempDateTime.ToString("yyyy/MM/dd");

            //薪資項目
            var SalaryID = ValidateData.GetFieldValue(SystemImportField.SALARY_ID, aRow[SystemImportField.SALARY_ID.ToString()].ToString());
            if (SalaryID == null)
            {
                //薪資項目
                aRow.SetColumnError(SystemImportField.SALARY_ID.ToString(), string.Format("【{0}】找不到對應", SystemImportField.SALARY_ID.ToFieldName()));
                return false;
            }
            else aRow[SystemImportField.SALARY_ID.ToString()] = SalaryID;

            //核定金額
            int SalaryAMT = 0;
            if (!Int32.TryParse(aRow[SystemImportField.AMT.ToString()].ToString(), out SalaryAMT))
            {
                aRow.SetColumnError(SystemImportField.AMT.ToString(), string.Format("【{0}】格式錯誤", SystemImportField.AMT.ToFieldName()));
                return false;
            }
            else if (SalaryAMT < 0)
            {
                aRow.SetColumnError(SystemImportField.AMT.ToString(), string.Format("【{0}】不可小於零", SystemImportField.AMT.ToFieldName()));
                return false;
            }
            else aRow[SystemImportField.AMT.ToString()] = Salary.ENCODE(1, SalaryAMT);

            //進行一般欄位檢查
            return GeneralFieldValidate(aRow, ValidateData);
        }

        /// <summary>一般欄位檢測</summary>
        private bool GeneralFieldValidate(DataRow aRow, FieldProcessExecutor ValidateData)
        {
            //驗證欄位
            var ValidateRow = GetDataForValidate(aRow);

            //找到資料了
            if (ValidateRow != null)
            {
                aRow.SetColumnError(SystemImportField.EFFECT_DATE.ToString(), string.Format("【{0}】重複", SystemImportField.EFFECT_DATE.ToFieldName()));
                return false;
            }
            
            //所有欄位
            var OtherFieldList = Enum.GetValues(typeof(SystemImportField));
            foreach (SystemImportField aField in OtherFieldList)
            {
                //輸入數值
                var tempStr = aRow[aField.ToString()].ToString();
                if (tempStr.Length > 0)
                {
                    //如果需要對應
                    if (aField.IsBinding())
                    {
                        tempStr = ValidateData.GetFieldValue(aField, tempStr);
                        if (tempStr == null)
                        {
                            aRow.SetColumnError(aField.ToString(), string.Format("【{0}】找不到對應", aField.ToFieldName()));
                            return false;
                        }
                        else aRow[aField.ToString()] = tempStr;
                    }
                }                
                else aRow[aField.ToString()] = DBNull.Value;
            }

            return true;
        }

        /// <summary>取得驗證資料</summary>
        private DataRow GetDataForValidate(DataRow aRow)
        {
            string SQL = @" Select	Top 1 * From [dbo].[HRM_SALARY_SALBASE_BASETTS]		
                            Where	EMPLOYEE_ID = @EMPLOYEE_ID and EFFECT_DATE = @EFFECT_DATE and SALARY_ID = @SALARY_ID
                         ";
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@EMPLOYEE_ID", aRow[SystemImportField.EMPLOYEE_ID.ToString()]));
            Parameter.Add(new SqlParameter("@EFFECT_DATE", aRow[SystemImportField.EFFECT_DATE.ToString()]));
            Parameter.Add(new SqlParameter("@SALARY_ID", aRow[SystemImportField.SALARY_ID.ToString()]));
            DataSet DataSet = SQL_Tools.GetDataSet(this.theDataModule, SQL, Parameter);
            if (DataSet.Tables[0].Rows.Count > 0) return DataSet.Tables[0].Rows[0];
            return null;
        }

        /// <summary>資料驗證</summary>
        private bool DataValidate(DataTable CheckTable)
        {
            bool tempBool = true;

            var allRow = CheckTable.AsEnumerable().ToList();
            foreach (var aRow in allRow)
            {
                try
                {
                    if (allRow.Any(m => m != aRow &&
                                        m.Field<string>(SystemImportField.EMPLOYEE_ID.ToString()) == aRow.Field<string>(SystemImportField.EMPLOYEE_ID.ToString()) &&
                                        m.Field<string>(SystemImportField.EFFECT_DATE.ToString()) == aRow.Field<string>(SystemImportField.EFFECT_DATE.ToString()) &&
                                        m.Field<string>(SystemImportField.SALARY_ID.ToString()) == aRow.Field<string>(SystemImportField.SALARY_ID.ToString())))
                    {
                        aRow.SetColumnError(SystemImportField.EFFECT_DATE.ToString(), string.Format("【{0}】重複", SystemImportField.EFFECT_DATE.ToFieldName()));
                        tempBool = false;
                    }
                }
                catch (Exception)
                {
                    aRow.SetColumnError(SystemImportField.EMPLOYEE_ID.ToString(), "邏輯驗證時錯誤");
                }
            }
            return tempBool;
        }        

        /// <summary>資料寫入</summary>
        private bool DataUpload(DataTable aTable)
        {
            bool Ans = false;
            IDbConnection connection = (IDbConnection)this.theDataModule.AllocateConnection(this.theDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //新增寫入
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.KeepIdentity, (SqlTransaction)transaction))
                {
                    bulkCopy.BatchSize = 500;
                    bulkCopy.ColumnMappings.Add("EMPLOYEE_ID", SystemImportField.EMPLOYEE_ID.ToString());
                    bulkCopy.ColumnMappings.Add("EFFECT_DATE", SystemImportField.EFFECT_DATE.ToString());
                    bulkCopy.ColumnMappings.Add("SALARY_ID", SystemImportField.SALARY_ID.ToString());
                    bulkCopy.ColumnMappings.Add("AMT", SystemImportField.AMT.ToString());
                    bulkCopy.ColumnMappings.Add("MEMO", SystemImportField.MEMO.ToString());

                    bulkCopy.ColumnMappings.Add("CREATE_MAN", "CREATE_MAN");
                    bulkCopy.ColumnMappings.Add("CREATE_DATE", "CREATE_DATE");
                    bulkCopy.ColumnMappings.Add("UPDATE_MAN", "UPDATE_MAN");
                    bulkCopy.ColumnMappings.Add("UPDATE_DATE", "UPDATE_DATE");

                    bulkCopy.DestinationTableName = "dbo.HRM_SALARY_SALBASE_BASETTS";

                    //有資料才傳(也因只有判定有資料的Row)
                    var FilterDataTable = aTable.AsEnumerable().Where(aRow => DataRowHasValue(aRow)).ToArray();
                    aTable.Columns.Add(new DataColumn { ColumnName = "CREATE_MAN", DefaultValue = this.CreateMan });
                    aTable.Columns.Add(new DataColumn { ColumnName = "CREATE_DATE", DefaultValue = DateTime.Now });
                    aTable.Columns.Add(new DataColumn { ColumnName = "UPDATE_MAN", DefaultValue = this.CreateMan });
                    aTable.Columns.Add(new DataColumn { ColumnName = "UPDATE_DATE", DefaultValue = DateTime.Now });
                     
                    bulkCopy.WriteToServer(FilterDataTable);
                }

                transaction.Commit();
                Ans = true;
            }
            catch { transaction.Rollback(); }
            finally { this.theDataModule.ReleaseConnection(this.theDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return Ans;
        }

    }

    /// <summary>擴充功能</summary>
    static public class FieldProcessExtensions
    {
        /// <summary>轉換成描述</summary>        
        static public string ToFieldName(this SystemImportField aSystemImportField)
        {
            FieldInfo fi = aSystemImportField.GetType().GetField(aSystemImportField.ToString());
            EnumAttribute[] attributes = (EnumAttribute[])fi.GetCustomAttributes(typeof(EnumAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].FieldName;
            else return aSystemImportField.ToString();
        }

        /// <summary>是否需對應</summary>        
        static public bool IsBinding(this SystemImportField aSystemImportField)
        {
            FieldInfo fi = aSystemImportField.GetType().GetField(aSystemImportField.ToString());
            EnumAttribute[] attributes = (EnumAttribute[])fi.GetCustomAttributes(typeof(EnumAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].IsBinding;
            else return false;
        }
    }

    #region =====================================欄位對應處理程序==================================

    /// <summary>欄位處理程序執行</summary>
    public class FieldProcessExecutor
    {
        /// <summary>【執行SQL用到的】</summary>
        public DataModule theDataModule { get; set; }

        /// <summary>欄位處理程序</summary>
        private List<FieldProcess> AllFieldProcess { get; set; }

        /// <summary>所有資料字典檔</summary>
        private Dictionary<SystemImportField, IFieldBinding> AllDictionary { get; set; }

        public FieldProcessExecutor(DataModule theDataModule)
        {
            this.theDataModule = theDataModule;
            this.AllDictionary = new Dictionary<SystemImportField, IFieldBinding>();

            //新增各個欄位對應處理程序
            AllFieldProcess = new List<FieldProcess>();
            AllFieldProcess.Add(new EmployeeFieldProcess());
            AllFieldProcess.Add(new SalaryFieldProcess());

            SetFieldDictionary();
        }

        /// <summary>設定資料字典</summary>
        private void SetFieldDictionary()
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
            if (!AllDictionary.ContainsKey(aSystemField)) return null;
            if (!AllDictionary[aSystemField].IsExists(Input)) return null;
            return AllDictionary[aSystemField].GetValue(Input);
        }
    }

    /// <summary>自訂屬性</summary>
    public class EnumAttribute : Attribute
    {
        /// <summary>欄位名稱</summary>
        public string FieldName { get; set; }

        /// <summary>是否需對應</summary>
        public bool IsBinding { get; set; }

        public EnumAttribute(string aFieldName)
        {
            FieldName = aFieldName;
            IsBinding = true;
        }

        public EnumAttribute(string aFieldName, bool aIsBinding)
        {
            FieldName = aFieldName;
            IsBinding = aIsBinding;
        }
    }

    /// <summary>系統對照欄</summary>
    public enum SystemImportField
    {
        /// <summary>員工編號</summary>
        [EnumAttribute("員工編號", false)]
        EMPLOYEE_ID = 1,
        /// <summary>生效日期</summary>
        [EnumAttribute("生效日期", false)]
        EFFECT_DATE = 2,
        /// <summary>薪資項目</summary>
        [EnumAttribute("薪資項目", false)]
        SALARY_ID = 3,
        /// <summary>金額</summary>
        [EnumAttribute("金額", false)]
        AMT = 4,        
        /// <summary>扶養人數</summary>
        [EnumAttribute("備註", false)]
        MEMO = 5
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
            catch (Exception) { throw new TheUserDefinedException(string.Format("取得{0}資料錯誤", this.ImportFieldKey.ToFieldName())); }
            
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

        private DictionaryField GetByValue(string InputValue)
        {
            var Ans = KeyValueList.FirstOrDefault(m => m.Code == InputValue);
            if (Ans == null) Ans = KeyValueList.FirstOrDefault(m => m.Name == InputValue);
            return Ans;
        }
    }

    /// <summary>員工資料</summary>
    public class EmployeeFieldProcess : FieldProcess
    {
        public EmployeeFieldProcess()
        {
            this.ImportFieldKey = SystemImportField.EMPLOYEE_ID;
        }

        //取得資料
        protected override IFieldBinding GetFieldDictionary()
        {
            string SQL = @" Select	EMPLOYEE_ID,EMPLOYEE_CODE,NAME_C From [dbo].[HRM_BASE_BASE]";
            var DataSet = SQL_Tools.GetDataSet(this.theDataModule, SQL, new ArrayList());
            return new DictionaryFieldBinding
            {
                KeyValueList = DataSet.Tables[0].AsEnumerable().Select(m => new DictionaryField
                {
                    ID = m.Field<string>("EMPLOYEE_ID"),
                    Code = m.Field<string>("EMPLOYEE_CODE") ?? "",
                    Name = m.Field<string>("NAME_C") ?? ""
                }).ToList()
            };
        }
    }

    /// <summary>薪資項目</summary>
    public class SalaryFieldProcess : FieldProcess
    {
        public SalaryFieldProcess()
        {
            this.ImportFieldKey = SystemImportField.SALARY_ID;
        }

        //取得資料
        protected override IFieldBinding GetFieldDictionary()
        {
            string SQL = @"
                   Select	SALARY_ID,
		           SALARY_CODE,
		           SALARY_CNAME
            From	[dbo].[HRM_SALARY_SALCODE] ";
            var DataSet = SQL_Tools.GetDataSet(this.theDataModule, SQL, new ArrayList());
            return new DictionaryFieldBinding
            {
                KeyValueList = DataSet.Tables[0].AsEnumerable().Select(m => new DictionaryField
                {
                    ID = m.Field<int>("SALARY_ID").ToString(),
                    Code = m.Field<string>("SALARY_CODE") ?? "",
                    Name = m.Field<string>("SALARY_CNAME") ?? ""
                }).ToList()
            };
        }
    }

    #endregion

    #endregion
}
