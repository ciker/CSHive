using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// 集合扩展
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// 带连接字符串的ToString
        /// </summary>
        /// <param name="source"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ToString(this List<string> source, string split=";")
        {
            return string.Join(split, source);
        }
    }
}