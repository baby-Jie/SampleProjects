using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonSamples
{
    public class LitJsonUtil
    {
        #region Custom Serialize LitJson Tool  

        static Type[] _customTypes = new Type[] { typeof(int), typeof(long), typeof(ulong), typeof(double), typeof(decimal), typeof(bool), typeof(string) };

        private static void WriteValue(LitJson.JsonWriter jsonWriter, object val)
        {
            if (jsonWriter == null)
                return;

            if (val is int iVal)
            {
                jsonWriter.Write(iVal);
            }
            else if (val is long lVal)
            {
                jsonWriter.Write(lVal);
            }
            else if (val is ulong ulVal)
            {
                jsonWriter.Write(ulVal);
            }
            else if (val is double doVal)
            {
                jsonWriter.Write(doVal);
            }
            else if (val is decimal deVal)
            {
                jsonWriter.Write(deVal);
            }
            else if (val is bool bVal)
            {
                jsonWriter.Write(bVal);
            }
            else if (val is string sVal)
            {
                jsonWriter.Write(sVal);
            }
        }

        public static void CustomSerialization(Dictionary<string, object> dict, LitJson.JsonWriter jsonWriter)
        {
            jsonWriter.WriteObjectStart();
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
            jsonWriter.WriteObjectEnd();
        }

        #endregion Custom Serialize Tool	
    }
}
