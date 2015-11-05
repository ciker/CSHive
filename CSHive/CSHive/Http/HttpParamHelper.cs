using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace CS.Http
{
    /// <summary>
    ///     将HttpContent里的Request里的相关集合转换为Param对象集合
    ///     参数处理模块:Ver:0.11
    /// </summary>
    /// <description class="CS.HttpParamHelper">
    ///     2010-02-04      增加期望捕获的枚举，降低不必的内存损耗
    ///     2010-02-05      增加IEnumerable给HttpParamCollection
    ///     2015-09-09      修正初始化时对集合有效性判断的BUG
    /// </description>
    /// <history>
    ///     2010-3-3 18:14:33 , zhouyu ,  创建
    /// </history>
    public sealed class HttpParamHelper
    {
        /// <summary>
        ///     使用 HttpContext.Current 初始化 HttpParamHelper 对象
        ///   默认只获取QueryString
        /// </summary>
        public HttpParamHelper() : this(HttpContext.Current,HttpParamNeedType.QueryString)
        {
        }

        /// <summary>
        ///     增加型构造[避免实例化用不到的客户端变量键值集合]。
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="NullReferenceException">如果捕获设定以外的对象会抛出未引用的异常</exception>
        public HttpParamHelper(HttpParamNeedType type) : this(HttpContext.Current, type)
        {
        }

        /// <summary>
        ///     使用指定的 HttpContext 对象初始化 HttpParamHelper 对象.
        /// </summary>
        /// <param name="content">Http上下文</param>
        public HttpParamHelper(HttpContext content)
        {
            Context = content;
            var isQueryStringValid = Context.Request.QueryString.Count > 0;
            var isFormValid = Context.Request.Form.Count > 0;
            var isCookieValid = Context.Request.Cookies.Count > 0;
            var isParamsValid = Context.Request.Params.Count > 0;
            if (isQueryStringValid)
                QueryString = new HttpParamCollection(Context.Request.QueryString);
            if (isFormValid)
                Form = new HttpParamCollection(Context.Request.Form);
            if (isCookieValid)
                Cookies = new HttpCookieParamCollection(Context.Request.Cookies);

            if (isParamsValid)
                Params = new HttpParamCollection(Context.Request.Params);

          
        }

        /// <summary>
        ///     增加型构型
        /// </summary>
        /// <param name="content">Http上下文</param>
        /// <param name="type">需要捕获的HttpParam</param>
        /// <exception cref="NullReferenceException">如果捕获设定以外的对象会抛出未引用的异常</exception>
        public HttpParamHelper(HttpContext content, HttpParamNeedType type)
        {
            Context = content;

            var isQueryStringValid = Context.Request.QueryString.Count > 0;
            var isFormValid = Context.Request.Form.Count > 0;
            var isParamsValid = Context.Request.Params.Count > 0;
            var isCookieValid = Context.Request.Cookies.Count > 0;
            if (isQueryStringValid && type.HasFlag(HttpParamNeedType.QueryString))
                QueryString = new HttpParamCollection(Context.Request.QueryString);
            if (isFormValid && type.HasFlag(HttpParamNeedType.Form))
                Form = new HttpParamCollection(Context.Request.Form);
            if (isCookieValid && type.HasFlag(HttpParamNeedType.Cookie))
                Cookies = new HttpCookieParamCollection(Context.Request.Cookies);

            if (isParamsValid && type.HasFlag(HttpParamNeedType.Params))
                Params = new HttpParamCollection(Context.Request.Params);

           
        }


        /// <summary>
        ///     HttpContext 对象
        /// </summary>
        internal HttpContext Context { get; }
        /// <summary>
        /// 是否为POST方式
        /// </summary>
        public bool IsPostBack => Context.Request.HttpMethod == "POST";

        /// <summary>
        ///     带有Index与Name索引的HttpParamCollection
        /// </summary>
        public HttpParamCollection QueryString { get; }

        /// <summary>
        ///     带有Index与Name索引的HttpParamCollection
        /// </summary>
        /// <example>HttpParamHelper().Form["inputText"].ToString();//返回的是一个HttpParam对象</example>
        public HttpParamCollection Form { get; }

        /// <summary>
        ///     带有Index与Name索引的HttpCookieParamCollection
        /// </summary>
        public HttpCookieParamCollection Cookies { get; }

        /// <summary>
        ///     带有Index与Name索引的HttpParamCollection
        /// </summary>
        public HttpParamCollection Params { get; }

        /// <summary>
        /// </summary>
        /// <param name="index"></param>
        public ParamMethod this[int index] => Params[index];

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        public ParamMethod this[string name] => Params[name];


        #region HttpParamCollection

        /// <summary>
        ///     HttpContext里的 QueryString、Form、ServerVariable 参数键值的集合
        /// </summary>
        public sealed class HttpParamCollection : IEnumerable
        {
            /// <summary>
            ///     键值集合
            /// </summary>
            private readonly NameValueCollection _collection;

            /// <summary>
            ///     构造:给键值对赋值
            /// </summary>
            /// <param name="collection"></param>
            internal HttpParamCollection(NameValueCollection collection)
            {
                this._collection = collection;
            }

            /// <summary>
            ///     根据Index索引返回HttpParamMethod
            /// </summary>
            /// <param name="i">Index</param>
            public ParamMethod this[int i] => new ParamMethod(i, _collection[i]);

            /// <summary>
            ///     根据Name索引返回HttpParamMethod
            /// </summary>
            /// <param name="name">Name</param>
            public ParamMethod this[string name] => new ParamMethod(name, _collection[name]);

            #region IEnumerator

            /// <summary>
            ///     实现接口
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return _collection.Cast<object>().GetEnumerator();
            }

            #endregion

            /// <summary>
            ///     捕获集合中的对应枚举值的类型，并转换为枚举值[注:针对有Flag的枚举:用于权限]
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public Param<T> ToEnum<T>()
            {
                var name = Enum.GetNames(typeof (T));
                var value = string.Empty;
                for (var i = 0; i < name.Length; i++)
                {
                    var key = name[i];
                    var item = _collection[key];
                    if (item != null && item == "1")
                        value += key + ",";
                }
                return new Param<T>(typeof (T).ToString(), value.TrimEnd(',').ToEnum<T>());
            }

            /// <summary>
            ///     Todo:?
            /// </summary>
            /// <param name="defaultValue">默认的枚举的Int32值</param>
            /// <typeparam name="T">枚举的类型</typeparam>
            /// <returns>期望的枚举Int32值</returns>
            public int ToEnum<T>(int defaultValue)
            {
                var typeName = typeof (T).Name;
                var values = (int[]) Enum.GetValues(typeof (T));
                foreach (var i in values)
                {
                    var option = _collection[typeName + "_" + i] == "1";
                    //value.SetOption(i, );
                    if (option)
                        defaultValue |= i;
                    else
                        defaultValue &= ~i;
                }
                return defaultValue;
            }
        }

        #endregion


        #region IHttpParamCollection 无用

        ///// <summary>
        ///// HttpParamCollection 索引集合
        ///// </summary>
        //public interface IHttpParamCollection
        //{
        //    ///<summary>
        //    /// Index索引
        //    ///</summary>
        //    ///<param name="index"></param>
        //    HttpParamMethod this[int index]
        //    {
        //        get;
        //    }

        //    ///<summary>
        //    /// Name索引
        //    ///</summary>
        //    ///<param name="name"></param>
        //    HttpParamMethod this[string name]
        //    {
        //        get;
        //    }
        //}

        #endregion

        #region HttpCookieParam & HttpCookieParamCollection

        /// <summary>
        ///     Cookie专用的HttpParam
        /// </summary>
        public sealed class HttpCookieParam
        {
            private readonly int _index;
            private readonly string _key;
            private readonly HttpCookie _cookie;

            internal HttpCookieParam(int index, string key, HttpCookie cookie)
            {
                _index = index;
                _key = key;
                this._cookie = cookie;
            }

            /// <summary>
            ///     Cookie是否存在
            /// </summary>
            public bool HasValue => _cookie != null;

            /// <summary>
            ///     获取Index索引中的Cookie值的HttpParamMethod
            /// </summary>
            /// <param name="i">Index索引</param>
            public ParamMethod this[int i] => new ParamMethod(i, _cookie.Values[i]);

            /// <summary>
            ///     获取Name索引中的Cookie值的HttpParamMethod
            /// </summary>
            /// <param name="name">Name索引</param>
            public ParamMethod this[string name] => new ParamMethod(name, _cookie[name]);

            /// <summary>
            ///     根据Index或者Cookie的Key值返回HttpParamMethod
            /// </summary>
            public ParamMethod Value => _index > -1 ? new ParamMethod(_index, _cookie.Value) : new ParamMethod(_key, _cookie.Value);
        }

        /// <summary>
        ///     HttpContext里的 Cookie 参数键值的集合[Cookie专用]
        /// </summary>
        public sealed class HttpCookieParamCollection
        {
            private readonly HttpCookieCollection _collection;

            internal HttpCookieParamCollection(HttpCookieCollection collection)
            {
                this._collection = collection;
            }

            /// <summary>
            ///     HttpCookieParam的Index索引
            /// </summary>
            /// <param name="i">Index</param>
            public HttpCookieParam this[int i] => new HttpCookieParam(i, null, _collection[i]);

            /// <summary>
            ///     HttpCookieParam的Name索引
            /// </summary>
            /// <param name="name">Name</param>
            public HttpCookieParam this[string name] => new HttpCookieParam(-1, name, _collection[name]);
        }

        #endregion
    }

    /// <summary>
    ///     期望捕获客户端参数的类型[为了提高性能，不需要的键值变量不转换为HttpParma]
    /// </summary>
    [Flags]
    public enum HttpParamNeedType
    {
        /// <summary>
        ///     捕获URL
        /// </summary>
        QueryString = 1,

        /// <summary>
        ///     捕获表单
        /// </summary>
        Form = 2,

        /// <summary>
        ///     捕获Cookie
        /// </summary>
        Cookie = 4,

        /// <summary>
        ///     捕获Params
        /// </summary>
        Params = 8,

        /// <summary>
        ///     全部捕获
        /// </summary>
        All = QueryString | Form | Cookie | Params
    }
}