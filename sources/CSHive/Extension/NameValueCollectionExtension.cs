using System.Collections.Specialized;
using CS.Http;

namespace System
{
    /// <summary>
    /// 针对键值集合的进行扩展
    /// </summary>
    public static class NameValueCollectionExtension
    {
        /// <summary>
        /// 转为HttpParam参数
        /// </summary>
        /// <param name="kvs"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static HttpParam ToParam(this NameValueCollection kvs, string key)
        {
            return new HttpParam(key,kvs[key]);
        }
    }
}