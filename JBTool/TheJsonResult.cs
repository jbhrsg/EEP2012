using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace JBTool
{
    public class TheJsonResult
    {        
        public bool IsOK { get; set; }

        public string ErrorMsg { get; set; }

        public object Result { get; set; }

        public TheJsonResult()
        {
            IsOK = false;
            ErrorMsg = "";
            Result = null;
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static Dictionary<string, object> GetParameterObj(object[] objParam)
        {
            try
            {
                if (objParam.Length == 0 || objParam[0] == null) return null;

                string ParameterStr = objParam[0].ToString();
                var Parameter = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParameterStr);
                return Parameter;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static string GetParameterStr(object[] objParam)
        {
            try
            {
                if (objParam.Length == 0 || objParam[0] == null) return null;
                return objParam[0].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataTable GetParameterDataTable(object[] objParam)
        {
            try
            {
                if (objParam.Length == 0 || objParam[0] == null) return null;

                string ParameterStr = objParam[0].ToString();
                var Parameter = JsonConvert.DeserializeObject<DataTable>(ParameterStr);
                return Parameter;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static string GetDisplayName<TModel>(System.Linq.Expressions.Expression<Func<TModel, object>> expression)
        {
            Type type = typeof(TModel);
            MemberExpression memberExpression = (MemberExpression)expression.Body;
            string propertyName = ((memberExpression.Member is PropertyInfo) ? memberExpression.Member.Name : null);
            var aAttribute = (DisplayNameAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
            return aAttribute != null ? aAttribute.DisplayName : propertyName ?? "";
        }
    }

    public static class JBClassHelper
    {
        /// <summary> 回傳Description屬性描述
        /// </summary>
        public static string GetDescription<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }

    [Serializable]
    public class TheUserDefinedException : Exception
    {
        public TheUserDefinedException() { }
        public TheUserDefinedException(string message) : base(message) { }
        public TheUserDefinedException(string message, Exception inner) : base(message, inner) { }
        protected TheUserDefinedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public static class HandlerHelper
    {
        /// <summary>序列化</summary>
        public static string SerializeObject(object o)
        {
            if (!o.GetType().IsSerializable) { return null; }

            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, o);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        /// <summary>反序列化</summary>
        public static object DeserializeObject(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}
