using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CS.Extension
{
    public static class StringExtension
    {

        /// <summary>
        /// 将该字符串重复一定次数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Repeat(this string str, int num)
        {
            var list = new List<string>();
            for (int i = 0; i < num; i++)
            {
                list.Add(str);
            }
            return string.Join("", list.ToArray());
        }

        /// <summary>
        /// 首参固定，次参重复一定次数
        /// </summary>
        /// <param name="firstStr"></param>
        /// <param name="repeatBlackStr"></param>
        /// <param name="repeatStr"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Repeat(this string repeatBlackStr, string firstStr,  string repeatStr, int num)
        {
            return num < 1 ? null : string.Concat(repeatBlackStr.Repeat(num-1), firstStr, repeatStr.Repeat(1));
        }

        

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json, JsonSerializerSettings serializerSettings)
        {
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json, serializerSettings);
        }

    }
}