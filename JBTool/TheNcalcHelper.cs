using NCalc;
using System;
using System.Collections.Generic;

namespace JBTool
{
    public class TheNcalcHelper
    {
        /// <summary>【參數】
        /// </summary>
        public Dictionary<string, string> Parameter { get; set; }

        /// <summary>【方法】
        /// </summary>
        public Dictionary<string, TheNcalcCustomFunction> CustomFunction { get; set; }

        //初始化    
        public TheNcalcHelper()
        {
            this.Parameter = new Dictionary<string, string>();
            this.CustomFunction = new Dictionary<string, TheNcalcCustomFunction>();
        }

        /// <summary> 取得【指定公式】的值 </summary>
        /// <param name="Expression">【指定公式】的內容</param>
        public object GetExpressionValue(string Expression)
        {
            NCalc.Expression e = new NCalc.Expression(Expression, NCalc.EvaluateOptions.RoundAwayFromZero);

            e.EvaluateParameter += delegate(string ParameterName, NCalc.ParameterArgs args)
            {
                if (!this.Parameter.ContainsKey(ParameterName)) return;

                try
                {
                    args.Result = GetExpressionValue(this.Parameter[ParameterName]);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("參數:{0}錯誤({1})", ParameterName, ex.Message));
                }

            };

            e.EvaluateFunction += delegate(string FunctionName, NCalc.FunctionArgs args)
            {
                if (!this.CustomFunction.ContainsKey(FunctionName)) return;

                var thisCustomFunction = this.CustomFunction[FunctionName];
                var newParameter = new List<object>();
                try
                {
                    for (int IndexOfParameter = 0; IndexOfParameter < args.Parameters.Length; IndexOfParameter++)
                    {
                        //超過指定的參數數量之後就不會理會了
                        if (thisCustomFunction.PropertyNum <= 0 || IndexOfParameter < thisCustomFunction.PropertyNum)
                            newParameter.Add(GetExpressionValue(args.Parameters[IndexOfParameter].ParsedExpression.ToString()));
                    }
                    args.Result = thisCustomFunction.Function(newParameter);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("方法:{0}錯誤({1})", FunctionName, ex.Message));
                }

            };

            try
            {
                if (e.HasErrors()) throw new Exception(e.Error);
                else return e.Evaluate();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }

    public class TheNcalcCustomFunction
    {
        /// <summary>【參數上限】</summary>
        public int PropertyNum { get; set; }

        /// <summary>【自訂方法】</summary>
        public GetCalculateExpressionFunctionHandler Function { get; set; }

        public TheNcalcCustomFunction()
        {
            PropertyNum = 0;
        }
    }
        
    public class TheNcalcFunction
    {
        /// <summary>轉換DateTime</summary>
        public static string DateTimeTransfer(DateTime? aDateTime)
        {
            if (aDateTime.HasValue) return TheNcalcFunction.DateTimeTransfer(aDateTime.Value);
            else return "'null'";
        }

        /// <summary>轉換DateTime</summary>
        public static string DateTimeTransfer(DateTime aDateTime)
        {
            return aDateTime.ToString("#yyyy/MM/dd HH:mm:ss#");
        }

        /// <summary>轉換String</summary>
        public static string StringTransfer(string aString)
        {
            if (aString == null) return "''";
            return string.Format("'{0}'", aString);
        }

        /// <summary>轉換Boolean</summary>
        public static string BooleanTransfer(bool aBoolean)
        {
            return aBoolean ? "true" : "false";
        }
    }
}
