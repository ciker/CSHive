using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace System
{

    /// <summary>
    /// 关于Json的相关扩展
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>
        /// 使用4.0里的System.Web.Extension中的JavaScriptSerializer序列化
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJssJson(this object o)
        {
            var jser = new JavaScriptSerializer();
            return jser.Serialize(o);
        }
    }
}