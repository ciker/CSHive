using System.Web;

namespace CS.Http
{
    /// <summary>
    /// 获取Post中的参数
    /// </summary>
    public class HttpPostParams:HttpUrlParams
    {
        public HttpPostParams()
        {
            var request = HttpContext.Current.Request;
            var nv = request.Form;
            foreach (var key in nv.AllKeys)
            {
                Add(new HttpParam(key, nv[key]));
            }
        }
    }
}