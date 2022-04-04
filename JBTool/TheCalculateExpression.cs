using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace JBTool
{
    public delegate object GetCalculateExpressionFunctionHandler(object parameterList);

    /// <summary>計算類別</summary>
    /// <typeparam name="T">個別參數Class</typeparam>
    public class TheCalculateExpression<T>
    {
        /// <summary>【計算公式】</summary>
        public string Formula { get; set; }

        /// <summary>【公開參數】</summary>
        public List<TheCalculateExpressionParameter> AllPublicParameter { get; set; }

        public TheCalculateExpression()
        {
            Formula = "";
            AllPublicParameter = new List<TheCalculateExpressionParameter>();
        }

        /// <summary>計算</summary>
        /// <param name="EachParameterClass">個別參數資料</param>
        public object CalculateResult(T EachParameterClass)
        {
            //將參數與帶入後取值
            TheNcalcHelper aCala = new TheNcalcHelper { Parameter = GetParameter(EachParameterClass), CustomFunction = GetFunction(EachParameterClass) };
            return aCala.GetExpressionValue(Formula);
        }

        //取得所有方法
        private Dictionary<string, TheNcalcCustomFunction> GetFunction(T EachParameterClass)
        {
            Dictionary<string, TheNcalcCustomFunction> Ans = new Dictionary<string, TheNcalcCustomFunction>();

            var methods = EachParameterClass.GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            foreach (var aMethod in methods)
            {
                var customAttributes = aMethod.GetCustomAttributes(typeof(TheExpressionFunctionAttribute), false);
                if (customAttributes.Length > 0)
                {
                    int PropertyNum = ((TheExpressionFunctionAttribute)customAttributes[0]).PropertyNum;
                    var Function = (GetCalculateExpressionFunctionHandler)Delegate.CreateDelegate(typeof(GetCalculateExpressionFunctionHandler), EachParameterClass, aMethod);
                    Ans.Add(aMethod.Name, new TheNcalcCustomFunction { PropertyNum = PropertyNum, Function = Function });
                }
            }

            return Ans;
        }

        //取得所有參數
        private Dictionary<string, string> GetParameter(T EachParameterClass)
        {
            var Ans = new Dictionary<string, string>();

            string Formula = "";
            //將系統值 經由 系統參數做轉換
            foreach (var aPublicParameter in AllPublicParameter)
            {
                Formula = "";
                if (string.IsNullOrEmpty(aPublicParameter.SystemCode)) Formula = aPublicParameter.Formula;
                else Formula = GetSystemParameterValue(aPublicParameter.SystemCode, EachParameterClass);

                if (!string.IsNullOrEmpty(Formula)) Ans.Add(aPublicParameter.Code, Formula);
            }
            return Ans;
        }

        //如果是系統值的取值
        private string GetSystemParameterValue(string PropertyName, T EachParameterClass)
        {
            if (EachParameterClass == null) return "";
            var aProperty = EachParameterClass.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(m => m.Name == PropertyName);
            return aProperty == null ? "" : aProperty.GetValue(EachParameterClass, null) == null ? "" : aProperty.GetValue(EachParameterClass, null).ToString();
        }

    }

    /// <summary>公開參數</summary>
    public class TheCalculateExpressionParameter
    {
        /// <summary>【參數名稱】</summary>
        public string Code { get; set; }

        /// <summary>【參數數值】</summary>
        public string Formula { get; set; }

        /// <summary>【系統參數名稱】(對應到的是哪一個)</summary>
        public string SystemCode { get; set; }
    }

    /// <summary>自訂方法(屬性)</summary>
    public class TheExpressionFunctionAttribute : Attribute
    {
        /// <summary>【參數上限】</summary>
        public int PropertyNum { get; set; }

        public TheExpressionFunctionAttribute()
        {
            this.PropertyNum = 0;
        }
    }
}
