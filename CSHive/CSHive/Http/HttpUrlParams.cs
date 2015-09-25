using System.Linq;
using System.Web;

namespace CS.Http
{

    /// <summary>
    /// Http的Url参数处理
    /// </summary>
    public class HttpUrlParams:HttpParams
    {
        
        public HttpUrlParams()
        {
            var request = HttpContext.Current.Request;
            BaseUrl = request.Url.AbsolutePath;
            BackUrl = request.UrlReferrer?.ToString();
            var nv = request.QueryString;
            foreach (var key in nv.AllKeys)
            {
                Add(new HttpParam(key,nv[key]));
            }
        }
        /// <summary>
        /// ？号前的基础连接 URI
        /// </summary>
        public string BaseUrl { get; }
        /// <summary>
        /// 上一页URL
        /// </summary>
        public string BackUrl { get; }

        /// <summary>
        /// 有则改之，无则加之 ,仅限URL中的参数
        /// <remarks>链式</remarks>
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        public HttpUrlParams SetValue(string paramName,object value)
        {
            var y = Find(x => x.Name == paramName);

            //var y = this.FirstOrDefault(x => x.Name == paramName);

            if(y==null)
                Add(new HttpParam(paramName,value.ToString()));
            else
            {
                y.Value = value.ToString();
            }
            return this;
        }

        /// <summary>
        /// 获得可用的查询链接
        /// </summary>
        /// <returns></returns>
        public string ToUrl()
        {
            var qs = ToString();
            return $"{BaseUrl}?{qs}";
        }

    }




}