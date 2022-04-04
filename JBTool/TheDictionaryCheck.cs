using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace JBTool
{
    public delegate void CheckaDataHandler(TheDictionaryCheckData aCheckData);

    public class TheDictionaryCheck
    {
        /// <summary>【待檢查資料】
        /// </summary>
        public List<TheDictionaryCheckData> CheckData { get; set; }

        /// <summary>【使用者自定義方法】
        /// </summary>
        public event CheckaDataHandler CheckUserDefined;

        //初始化
        public TheDictionaryCheck()
        {
            CheckData = new List<TheDictionaryCheckData>();
        }

        /// <summary>參數欄位數值輸入
        /// </summary>
        /// <param name="InputContent">參數字典檔</param>
        public void SetFieldValue(Dictionary<string, object> InputContent)
        {            
            if (InputContent != null)
            {
                foreach (var aCheckData in CheckData)
                {
                    aCheckData.ReSetContent();
                    aCheckData.TheValue = InputContent.ContainsKey(aCheckData.FieldName) ? InputContent[aCheckData.FieldName] : null;                    
                }
            }
        }

        /// <summary>執行驗證
        /// </summary>
        public void DoCheck()
        {
            foreach (var aCheckData in CheckData)
            {
                string TheValue = aCheckData.TheValue == null ? "" : aCheckData.TheValue.ToString().Trim();

                if (TheValue.Length > 0) aCheckData.HasValue(true);

                //輸入檢查
                if (aCheckData.IsRequired && !aCheckData.HasValue()) aCheckData.ErrorMsg = string.Format("【{0}】沒有輸入", aCheckData.DisplayName);
                else Check_Not(aCheckData);

                //檢查
                if (aCheckData.HasValue())
                {
                    bool IsUserCheck = false;

                    //檢查
                    if (aCheckData.IsSysCheck)
                    {
                        Check_aCheckData(aCheckData);                                           //系統檢查                        
                        if (aCheckData.IsUserCheck && aCheckData.IsOK) { IsUserCheck = true; }  //使用者自訂檢查
                    }
                    else if (aCheckData.IsUserCheck) { IsUserCheck = true; }

                    //使用者自訂檢查
                    if (IsUserCheck)
                    {
                        aCheckData.IsOK = false;
                        if (CheckUserDefined == null) aCheckData.ErrorMsg = "CheckUserDefined Not Defined";
                        else
                        {
                            try { CheckUserDefined(aCheckData); }
                            catch (Exception ex) { aCheckData.ErrorMsg = ex.Message; }
                        }
                    }
                }
            }
        }        

        /// <summary>錯誤訊息(第一個)
        /// </summary>
        public string GetFirstErrorMsg()
        {
            var aCheckData = CheckData.FirstOrDefault(m => !m.IsOK);
            if (aCheckData == null) return "";
            else return string.IsNullOrEmpty(aCheckData.ErrorMsg) ? "驗證錯誤" : aCheckData.ErrorMsg;
        }          

        /// <summary>將結果轉換成簡單的Object
        /// </summary>
        /// <typeparam name="T">簡單的Object</typeparam>
        /// <returns>簡單的Object</returns>
        public T ResultToObject<T>() where T : class, new()
        {
            T someObject = new T();
            Type someObjectType = someObject.GetType();

            foreach (var aProperty in someObjectType.GetProperties())
            {
                var aCheckData = CheckData.FirstOrDefault(m => m.FieldName == aProperty.Name);
                if (aCheckData != null)
                {
                    try { aProperty.SetValue(someObject, aCheckData.TheResult, null); }
                    catch (Exception ex) { }
                }
            }
            return someObject;
        }        

        //各項驗證分類
        private void Check_aCheckData(TheDictionaryCheckData aCheckData)
        {
            aCheckData.IsOK = false;
            switch (aCheckData.SystemCheckType)
            {
                case TheDictionaryCheckType.Not: Check_Not(aCheckData); break;
                case TheDictionaryCheckType.DateTime: Check_DateTime(aCheckData); break;
                case TheDictionaryCheckType.DateStr: Check_DateStr(aCheckData); break;
                case TheDictionaryCheckType.Int: Check_Int(aCheckData); break;
                case TheDictionaryCheckType.Email: Check_Email(aCheckData); break;
                case TheDictionaryCheckType.Decimal: Check_Decimal(aCheckData); break;
                case TheDictionaryCheckType.Month: Check_Month(aCheckData); break;
                case TheDictionaryCheckType.Bool: Check_Bool(aCheckData); break;
                case TheDictionaryCheckType.Idno: Check_Idno(aCheckData); break;
                case TheDictionaryCheckType.YearMonth: Check_YearMonth(aCheckData); break;
                case TheDictionaryCheckType.Time48Hours: Check_Time48Hours(aCheckData); break;
                case TheDictionaryCheckType.Time48Hours_60: Check_Time48Hours_60(aCheckData); break;
                case TheDictionaryCheckType.Year: Check_Year(aCheckData); break;
                case TheDictionaryCheckType.Time24Hours: Check_Time24Hours(aCheckData); break;
            }
        }

        //不用驗證
        private void Check_Not(TheDictionaryCheckData aCheckData)
        {
            aCheckData.ErrorMsg = "";
            aCheckData.IsOK = true;
            aCheckData.TheResult = aCheckData.TheDefault ?? aCheckData.TheValue;
        }

        //時間驗證
        private void Check_DateTime(TheDictionaryCheckData aCheckData)
        {
            DateTime aDateTime = new DateTime();
            if (!DateTime.TryParse(aCheckData.TheValue.ToString(), out aDateTime)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = aDateTime;
            }
        }

        //日期字串驗證
        private void Check_DateStr(TheDictionaryCheckData aCheckData)
        {
            DateTime aDateTime = new DateTime();
            if (!DateTime.TryParse(aCheckData.TheValue.ToString(), out aDateTime)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = aDateTime.ToString("yyyy/MM/dd");
            }
        }

        //整數驗證
        private void Check_Int(TheDictionaryCheckData aCheckData)
        {
            int aInt = 0;
            if (!Int32.TryParse(aCheckData.TheValue.ToString(), out aInt)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = aInt;
            }
        }

        //信箱驗證
        private void Check_Email(TheDictionaryCheckData aCheckData)
        {
            string theValue = aCheckData.TheValue.ToString();
            string Pattern = @"^([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$";
            if (!Regex.IsMatch(theValue, Pattern)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = theValue;
            }
        }

        //小數驗證
        private void Check_Decimal(TheDictionaryCheckData aCheckData)
        {
            Decimal aDecimal = 0;
            if (!Decimal.TryParse(aCheckData.TheValue.ToString(), out aDecimal)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = aDecimal;
            }
        }

        //整數驗證
        private void Check_Month(TheDictionaryCheckData aCheckData)
        {
            int aInt = 0;
            if (!Int32.TryParse(aCheckData.TheValue.ToString(), out aInt)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else if (aInt < 1 || aInt > 12) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = aInt;
            }
        }

        //Boolean驗證
        private void Check_Bool(TheDictionaryCheckData aCheckData)
        {
            bool aBool = false;
            if (!bool.TryParse(aCheckData.TheValue.ToString(), out aBool)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = aBool;
            }
        }

        //身分證驗證
        private void Check_Idno(TheDictionaryCheckData aCheckData)
        {
            string theValue = aCheckData.TheValue.ToString();
            if (!check_Idno(theValue)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = theValue;
            }
        }

        //檢查身分證字號
        private bool check_Idno(string Idno) {
            if( Idno == "")
                return false;
            Idno = Idno.Trim().ToUpper(); //將英文字母全部轉成大寫，消除前後空白
            //檢查第一個字母是否為英文字，第二個字元1 or 2 其餘為數字共十碼
            string Pattern = "[a-zA-Z][0-9]{9}";
            if (!Regex.IsMatch(Idno, Pattern)) return false;
            string wd_str ="BAKJHGFEDCNMLVUTSRQPZWYX0000OI";   //關鍵在這行字串，你能領悟IQ+++
            int d1 = wd_str.IndexOf(Idno[0])%10;
            int sum = 0;
            for(int ii=1; ii<9; ii++ )
                sum+= int.Parse(Idno[ii].ToString())*(9-ii);
            sum += d1 + int.Parse(Idno[9].ToString());
            if(sum%10 != 0)return false;
            return true;
        }

        //年度月份檢查
        private void Check_YearMonth(TheDictionaryCheckData aCheckData)
        {
            string theValue = aCheckData.TheValue.ToString();
            string Pattern = @"^(\d{4})(0[1-9]|1[0-2])$";

            if (!Regex.IsMatch(theValue, Pattern)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                var MatchGroups = Regex.Match(theValue, Pattern).Groups;
                DateTime aDateTime = new DateTime();
                if (!DateTime.TryParse(string.Format("{0}/{1}/01", MatchGroups[1], MatchGroups[2]), out aDateTime)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
                else
                {
                    aCheckData.ErrorMsg = "";
                    aCheckData.IsOK = true;
                    aCheckData.TheResult = theValue;
                }
            }
        }

        //48小時檢查
        private void Check_Time48Hours(TheDictionaryCheckData aCheckData)
        {
            string theValue = aCheckData.TheValue.ToString();
            string Pattern = @"^(?:([0-3]\d|4[0-7])(30|00)|4800)$";
            if (!Regex.IsMatch(theValue, Pattern)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = theValue;
            }
        }

        //48小時檢查
        private void Check_Time48Hours_60(TheDictionaryCheckData aCheckData)
        {
            string theValue = aCheckData.TheValue.ToString();
            string Pattern = @"^(?:([0-3]\d|4[0-7])([0-5][0-9])|4800)$";
            if (!Regex.IsMatch(theValue, Pattern)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = theValue;
            }
        }

        //年度檢查
        private void Check_Year(TheDictionaryCheckData aCheckData)
        {
            string theValue = aCheckData.TheValue.ToString();
            string Pattern = @"^\d{4}$";

            if (!Regex.IsMatch(theValue, Pattern)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                var MatchGroups = Regex.Match(theValue, Pattern).Groups;
                DateTime aDateTime = new DateTime();
                if (!DateTime.TryParse(string.Format("{0}/01/01", MatchGroups[0]), out aDateTime)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
                else
                {
                    aCheckData.ErrorMsg = "";
                    aCheckData.IsOK = true;
                    aCheckData.TheResult = aDateTime.Year;
                }
            }
        }

        //24小時檢查
        private void Check_Time24Hours(TheDictionaryCheckData aCheckData)
        {
            string theValue = aCheckData.TheValue.ToString();
            string Pattern = @"^(?:2[0-3]|[0-1][0-9])[0-5][0-9]$";
            if (!Regex.IsMatch(theValue, Pattern)) aCheckData.ErrorMsg = string.Format("【{0}】格式錯誤", aCheckData.DisplayName);
            else
            {
                aCheckData.ErrorMsg = "";
                aCheckData.IsOK = true;
                aCheckData.TheResult = theValue;
            }
        }
    }

    public enum TheDictionaryCheckType
    {
        /// <summary>【不驗證】</summary>
        Not,
        /// <summary>【時間】轉DateTime</summary>
        DateTime,
        /// <summary>【日期】轉String</summary>
        DateStr,
        /// <summary>【整數】轉Int</summary>
        Int,
        /// <summary>【信箱】轉String</summary>
        Email,
        /// <summary>【小數】轉Decimal</summary>
        Decimal,
        /// <summary>【月份】轉Int</summary>
        Month,
        /// <summary>【Boolean】轉Bool</summary>
        Bool,
        /// <summary>【身分證字號】轉String</summary>
        Idno,
        /// <summary>【年度月份】轉String</summary>
        YearMonth,
        /// <summary>【48小時】轉String</summary>
        Time48Hours,
        /// <summary>【48小時】轉String</summary>
        Time48Hours_60,
        /// <summary>【年度】轉Int</summary>
        Year,
        /// <summary>【24小時】轉String</summary>
        Time24Hours
    }

    public class TheDictionaryCheckData
    {
        /// <summary> 【欄位名稱】
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>【顯示名稱】
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>【數值】</summary>
        public object TheValue { get; set; }

        /// <summary>【是否執行系統方法】
        /// </summary>                                              
        public bool IsSysCheck { get; set; }

        /// <summary>【是否必須輸入】
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>【系統內建檢查方法】
        /// </summary>
        public TheDictionaryCheckType SystemCheckType { get; set; }

        /// <summary>【是否執行自定義方法】
        /// </summary>                                              
        public bool IsUserCheck { get; set; }

        /// <summary>【驗證是否通過】</summary>                                              
        public bool IsOK { get; set; }

        /// <summary>【成功轉換的結果】型別的轉換</summary>
        public object TheResult { get; set; }

        /// <summary>【預設值】</summary>
        public object TheDefault { get; set; }

        /// <summary>【錯誤訊息】
        /// </summary>                                              
        public string ErrorMsg { get; set; }

        /// <summary>是否有數值</summary>
        private bool IsHasValue { get; set; }

        //初始化
        public TheDictionaryCheckData()
        {
            FieldName = "";
            DisplayName = "";
            TheValue = null;
            IsRequired = true;
            IsSysCheck = false;
            SystemCheckType = TheDictionaryCheckType.Not;
            IsUserCheck = false;
            IsOK = false;
            TheResult = null;
            TheDefault = null;
            ErrorMsg = "驗證錯誤";
            IsHasValue = false;
        }

        //重重置一些計算數值
        public void ReSetContent()
        {
            TheValue = null;
            IsOK = false;
            ErrorMsg = "驗證錯誤";
            IsHasValue = false;
        }

        /// <summary>是否有值<para>執行驗證後才做判定</para></summary>        
        public bool HasValue()
        {
            return this.IsHasValue;
        }

        /// <summary>是否有值(設定)</summary>
        public void HasValue(bool HasValue)
        {
            this.IsHasValue = HasValue;
        }
    }
}
