using System;
using System.Collections.Generic;
using System.Linq;

namespace CS.Http
{

    #region HttpParams 是 HttpParam 的集合

    /// <summary>
    /// 自定义的Http请求时便于处理的参数集合。
    /// <par>可显示将键值对的字符串转为HttpParams</par>
    /// <par>可隐式将HttpParams转换为字符串形式的键值对</par>
    /// </summary>
    public class HttpParams : List<HttpParam>
    {

        public override string ToString()
        {
            string[] arrQ = Array.ConvertAll<HttpParam, string>(ToArray(), s => s);
            return string.Join("&", arrQ);
        }

        /// <summary>
        /// 隐式转换，将HttpParams隐式转换成形如：Name=Value&Name2=Value2 ... 形式的字符串
        /// </summary>
        /// <param name="httpParams">Http请求的参数集合<see cref="HttpParams"/></param>
        /// <returns></returns>
        public static implicit operator string(HttpParams httpParams)
        {
            return httpParams.ToString();
        }

        /// <summary>
        /// 强制将查询字符串转换为<see cref="HttpParams"/>。
        /// </summary>
        /// <param name="nvs">含有查询字符串的部分或全部，形如：Name=Value&Name2=Value2 ... </param>
        /// <returns>可能为null</returns>
        /// <exception cref="NullReferenceException">非法的名时值有也无效，此时无法转换合法的HttpParams。只会抛出Null引用的异常。</exception>
        public static explicit operator HttpParams(string nvs)
        {
            var index = nvs.IndexOf("?") + 1;
            var nvString = nvs.Substring(index);
            if (string.IsNullOrEmpty(nvString)) return null;

            var arrNv = nvString.Split('&');
            if (arrNv.Length < 1) return null;
            var result = new HttpParams();
            result.AddRange(arrNv.Select(s => (HttpParam)s));
            return result;
        }

        /// <summary>
        /// name素引器实现
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                var y = Find(x => x.Name == name);
                return y != null ? y.Value : null;
            }
        }

    }

    #endregion


    #region HttpParam Http的参数(URL参数，POST等参数键值对)键值对

    /// <summary>
    /// Http的参数名及值
    /// 键值对只读，只能在构造时赋值。
    /// </summary>
    public class HttpParam
    {
        private readonly string _name;
        private readonly string _value;

        public HttpParam(string name, string value)
        {
            _name = name;
            _value = value;
        }

        /// <summary>
        /// 参数名
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ? string.Format("{0}={1}", Name, Value) : string.Empty;
        }

        /// <summary>
        /// 隐式转换，将UrlParamter隐式转换成形如：Name=Value形式的字符串
        /// </summary>
        /// <param name="httpParam"></param>
        /// <returns></returns>
        public static implicit operator string(HttpParam httpParam)
        {
            return httpParam.ToString();
        }

        /// <summary>
        /// 强制将形如Name=Value的字符串转换为HttpParam的形式。
        /// </summary>
        /// <param name="keyValue">形如Name=Value的字符串</param>
        /// <returns>可能为null</returns>
        /// <exception cref="NullReferenceException">非法的名时值有也无效，此时无法转换合法的UrlParamter参数。只会抛出Null引用的异常。</exception>
        public static explicit operator HttpParam(string keyValue)
        {
            var arr = keyValue.Split('=');
            return (arr.Length != 2 || string.IsNullOrEmpty(arr[0])) ? null : new HttpParam(arr[0], arr[1]);
        }

    }

    #endregion


    #region HttpParam  比较功能，用于Http参数(URL,POST等里的参数)集合按名称排序时使用

    /// <summary>
    /// Comparer class used to perform the sorting of the query parameters
    /// <remarks>
    /// 同名参数比较其值(按值排)，否则按名字比较(排序)。
    /// </remarks>
    /// </summary>
    public class HttpParamComparer : IComparer<HttpParam>
    {
        #region IComparer<UrlParameter> Members

        public int Compare(HttpParam x, HttpParam y)
        {
            return x.Name == y.Name ? string.Compare(x.Value, y.Value) : string.Compare(x.Name, y.Name);
        }

        #endregion
    }

    #endregion


    #region HttpMethod enum

    /// <summary>
    /// 请求方法（所有方法全为大写）
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// 请求获取Request-URI所标识的资源
        /// </summary>
        GET,
        /// <summary>
        ///  在Request-URI所标识的资源后附加新的数据
        /// </summary>
        POST,
        /// <summary>
        /// 请求获取由Request-URI所标识的资源的响应消息报头
        /// </summary>
        HEAD,
        /// <summary>
        ///  请求服务器存储一个资源，并用Request-URI作为其标识
        /// </summary>
        PUT,
        /// <summary>
        /// 请求服务器删除Request-URI所标识的资源
        /// </summary>
        DELETE,
        /// <summary>
        /// 请求服务器回送收到的请求信息，主要用于测试或诊断
        /// </summary>
        TRACE,
        /// <summary>
        /// 保留将来使用
        /// </summary>
        CONNECT,
        /// <summary>
        /// 请求查询服务器的性能，或者查询与资源相关的选项和需求
        /// </summary>
        OPTIONS,
    }

    #endregion

 
}