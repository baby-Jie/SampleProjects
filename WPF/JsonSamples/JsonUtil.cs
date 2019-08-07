using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace JsonSamples
{
    /// <summary>
    /// 测试案例 见JsonSamples
    /// </summary>
    public class JsonUtil
    {
        #region Custom Serialize Newtonsoftjson Tool  

        static Type[] _customTypes = new Type[] { typeof(int), typeof(uint), typeof(short), typeof(long), typeof(ulong), typeof(double), typeof(decimal), typeof(bool), typeof(string), typeof(float), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan)};

        private static void WriteValue(Newtonsoft.Json.JsonWriter jsonWriter, object val)
        {
            if (jsonWriter == null)
                return;

            if (val is int iVal)
            {
                jsonWriter.WriteValue(iVal);
            }
            else if (val is uint uVal)
            {
                jsonWriter.WriteValue(uVal);
            }
            else if (val is short shVal)
            {
                jsonWriter.WriteValue(shVal);
            }
            else if (val is long lVal)
            {
                jsonWriter.WriteValue(lVal);
            }
            else if (val is ulong ulVal)
            {
                jsonWriter.WriteValue(ulVal);
            }
            else if (val is double doVal)
            {
                jsonWriter.WriteValue(doVal);
            }
            else if (val is decimal deVal)
            {
                jsonWriter.WriteValue(deVal);
            }
            else if (val is bool bVal)
            {
                jsonWriter.WriteValue(bVal);
            }
            else if (val is string sVal)
            {
                jsonWriter.WriteValue(sVal);
            }
            else if (val is float fVal)
            {
                jsonWriter.WriteValue(fVal);
            }
            else if (val is DateTime dtVal)
            {
                jsonWriter.WriteValue(dtVal);
            }
            else if (val is DateTimeOffset dtoVal)
            {
                jsonWriter.WriteValue(dtoVal);
            }
            else if (val is TimeSpan tmsVal)
            {
                jsonWriter.WriteValue(tmsVal);
            }
        }

        public static void CustomSerialization(Dictionary<string, object> dict, Newtonsoft.Json.JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartObject();
            foreach (var dictKey in dict.Keys)
            {
                var val = dict[dictKey];

                if (_customTypes.Contains(val.GetType()))
                {
                    // 可以写入的类型
                    jsonWriter.WritePropertyName(dictKey);
                    WriteValue(jsonWriter, val);
                }
                else if (val is Dictionary<string, object> subDic)
                {
                    jsonWriter.WritePropertyName(dictKey);
                    CustomSerialization(subDic, jsonWriter);
                }
            }
            jsonWriter.WriteEndObject();
        }

        #endregion Custom Serialize Tool	

        #region JsonObject 

        public static string GetString(JToken data, string key)
        {
            string result = null;
            if (data == null || string.IsNullOrWhiteSpace(key))
            {
                return result;
            }

            if (data[key] != null)
            {
                result = data[key].Value<string>();
            }
            return result;
        }

        public static int GetInt(JToken data, string key, int defaultValue)
        {
            int result = defaultValue;
            if (data == null || string.IsNullOrWhiteSpace(key))
            {
                return result;
            }

            if (data[key] != null)
            {
                result = data[key].Value<int>();
            }
            return result;
        }

        public static bool GetBool(JToken data, string key, bool defaultValue)
        {
            bool result = defaultValue;
            if (data == null || string.IsNullOrWhiteSpace(key))
            {
                return result;
            }

            if (data[key] != null)
            {
                result = data[key].Value<bool>();
            }
            return result;
        }

        public static List<JToken> GetList(JToken data, string key)
        {
            List<JToken> result = null;
            if (data == null || string.IsNullOrWhiteSpace(key))
            {
                return result;
            }

            if (data[key] != null)
            {
                result = data[key].Children().ToList<JToken>(); ;
            }
            return result;
        }

        #region Get JsonData

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">JObject or JToken or IDictionary or DataRow</typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJDataStringValue<T>(T data, string key)
        {
            string result = string.Empty;
            if (data is JObject)
            {
                result = (data as JObject)[key] != null ? (data as JObject)[key].Value<string>() : string.Empty;
            }
            else if (data is JToken)
            {
                result = (data as JToken)[key] != null ? (data as JToken)[key].Value<string>() : string.Empty;
            }
            else if (data is IDictionary<string, string>)
            {
                result = (data as IDictionary<string, string>)[key] != null ? (data as IDictionary<string, string>)[key] : string.Empty;
            }
            else if (data is DataRow)
            {
                if ((data as DataRow).Table.Columns.Contains(key))
                {
                    result = (data as DataRow)[key].ToString();
                }
            }
            if (string.IsNullOrEmpty(result))
            {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">JObject or JToken or IDictionary or DataRow</typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long GetJDataLongValue<T>(T data, string key)
        {
            long result = 0L;
            if (data is JObject)
            {
                if ((data as JObject)[key] != null
                    && !string.IsNullOrEmpty((data as JObject)[key].ToString()))
                {
                    result = (data as JObject)[key].Value<long>();
                }
            }
            else if (data is JToken)
            {
                if ((data as JToken)[key] != null
                    && string.IsNullOrEmpty((data as JToken)[key].ToString()))
                {
                    result = (data as JToken)[key].Value<long>();
                }
            }
            else if (data is IDictionary<string, string>)
            {
                string str = (data as IDictionary<string, string>)[key] != null ? (data as IDictionary<string, string>)[key] : string.Empty;
                if (!long.TryParse(str, out result))
                {
                    result = 0L;
                }
            }
            else if (data is IDictionary<string, int>)
            {
                if ((data as IDictionary<string, int>).ContainsKey(key))
                {
                    result = (data as IDictionary<string, int>)[key];
                }
            }
            else if (data is IDictionary<string, long>)
            {
                if ((data as IDictionary<string, long>).ContainsKey(key))
                {
                    result = (data as IDictionary<string, long>)[key];
                }
            }
            else if (data is DataRow)
            {
                if ((data as DataRow).Table.Columns.Contains(key))
                {
                    object obj = (data as DataRow)[key];
                    if (obj is string)
                    {
                        if (!long.TryParse(obj.ToString(), out result))
                        {
                            result = 0L;
                        }
                    }
                    else if (obj is int || obj is long)
                    {
                        result = long.Parse(obj.ToString());
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">JObject or JToken or IDictionary or DataRow</typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetJDataInt32Value<T>(T data, string key)
        {
            int result = 0;
            if (data is JObject)
            {
                if ((data as JObject)[key] != null
                    && !string.IsNullOrEmpty((data as JObject)[key].ToString()))
                {
                    result = (data as JObject)[key].Value<int>();
                }
            }
            else if (data is JToken)
            {
                if ((data as JToken)[key] != null
                    && string.IsNullOrEmpty((data as JToken)[key].ToString()))
                {
                    result = (data as JToken)[key].Value<int>();
                }
            }
            else if (data is IDictionary<string, string>)
            {
                string str = (data as IDictionary<string, string>)[key] != null ? (data as IDictionary<string, string>)[key] : string.Empty;
                if (!int.TryParse(str, out result))
                {
                    result = 0;
                }
            }
            else if (data is IDictionary<string, int>)
            {
                if ((data as IDictionary<string, int>).ContainsKey(key))
                {
                    result = (data as IDictionary<string, int>)[key];
                }
            }
            else if (data is DataRow)
            {
                if ((data as DataRow).Table.Columns.Contains(key))
                {
                    object obj = (data as DataRow)[key];
                    if (obj is string)
                    {
                        if (!int.TryParse(obj.ToString(), out result))
                        {
                            result = 0;
                        }
                    }
                    else if (obj is int)
                    {
                        result = int.Parse(obj.ToString());
                    }
                }
            }

            return result;
        }

        public static long GetJDataLongIdValue<T>(T data, string key)
        {
            long result = -1L;
            if (data is JObject)
            {
                if ((data as JObject)[key] != null
                    && !string.IsNullOrEmpty((data as JObject)[key].ToString()))
                {
                    result = (data as JObject)[key].Value<long>();
                }
            }
            else if (data is JToken)
            {
                if ((data as JToken)[key] != null
                    && string.IsNullOrEmpty((data as JToken)[key].ToString()))
                {
                    result = (data as JToken)[key].Value<long>();
                }
            }
            else if (data is IDictionary<string, string>)
            {
                string str = (data as IDictionary<string, string>)[key] != null ? (data as IDictionary<string, string>)[key] : string.Empty;
                if (!long.TryParse(str, out result))
                {
                    result = -1L;
                }
            }
            else if (data is IDictionary<string, int>)
            {
                if ((data as IDictionary<string, int>).ContainsKey(key))
                {
                    result = (data as IDictionary<string, int>)[key];
                }
            }
            else if (data is IDictionary<string, long>)
            {
                if ((data as IDictionary<string, long>).ContainsKey(key))
                {
                    result = (data as IDictionary<string, long>)[key];
                }
            }
            else if (data is DataRow)
            {
                if ((data as DataRow).Table.Columns.Contains(key))
                {
                    object obj = (data as DataRow)[key];
                    if (obj is string)
                    {
                        if (!long.TryParse(obj.ToString(), out result))
                        {
                            result = -1L;
                        }
                    }
                    else if (obj is int || obj is long)
                    {
                        result = long.Parse(obj.ToString());
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">JObject or JToken or IDictionary or DataRow</typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetJDataInt32IdValue<T>(T data, string key)
        {
            int result = -1;
            if (data is JObject)
            {
                if ((data as JObject)[key] != null
                    && !string.IsNullOrEmpty((data as JObject)[key].ToString()))
                {
                    result = (data as JObject)[key].Value<int>();
                }
            }
            else if (data is JToken)
            {
                if ((data as JToken)[key] != null
                    && string.IsNullOrEmpty((data as JToken)[key].ToString()))
                {
                    result = (data as JToken)[key].Value<int>();
                }
            }
            else if (data is IDictionary<string, string>)
            {
                string str = (data as IDictionary<string, string>)[key] != null ? (data as IDictionary<string, string>)[key] : string.Empty;
                if (!int.TryParse(str, out result))
                {
                    result = -1;
                }
            }
            else if (data is IDictionary<string, int>)
            {
                if ((data as IDictionary<string, int>).ContainsKey(key))
                {
                    result = (data as IDictionary<string, int>)[key];
                }
            }
            else if (data is DataRow)
            {
                if ((data as DataRow).Table.Columns.Contains(key))
                {
                    object obj = (data as DataRow)[key];
                    if (obj is string)
                    {
                        if (!int.TryParse(obj.ToString(), out result))
                        {
                            result = -1;
                        }
                    }
                    else if (obj is int)
                    {
                        result = int.Parse(obj.ToString());
                    }
                }
            }
            return result;
        }

        /// <summary>
        ///  如果为空 返回 DateTime(1900, 1, 1);
        /// </summary>
        /// <typeparam name="T">JObject or JToken or IDictionary or DataRow</typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetJDataDateTimeValue<T>(T data, string key)
        {
            DateTime result = new DateTime(1900, 1, 1);
            if (data is JObject
                || data is JToken
                || data is IDictionary<string, string>)
            {
                result = StrToDateTime(GetJDataStringValue(data, key));
            }
            else if (data is IDictionary<string, DateTime>)
            {
                if ((data as IDictionary<string, DateTime>)[key] != null)
                {
                    result = (data as IDictionary<string, DateTime>)[key];
                }
            }
            else if (data is DataRow)
            {
                if ((data as DataRow).Table.Columns.Contains(key))
                {
                    object obj = (data as DataRow)[key];
                    if (obj is string)
                    {
                        result = StrToDateTime(GetJDataStringValue(data, key));
                    }
                    else if (obj is DateTime)
                    {
                        result = (DateTime)obj;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// string型转换为DateTime型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的DateTime类型结果</returns>
        public static DateTime StrToDateTime(object strValue)
        {
            DateTime result;

            if (DateTime.TryParse(strValue.ToString(), out result))
            {
                return result;
            }
            else
            {
                return new DateTime(1900, 1, 1);
            }
        }

        #endregion

        #endregion JsonObject 	
    }
}
