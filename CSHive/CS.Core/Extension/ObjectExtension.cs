using Newtonsoft.Json;

namespace CS.Extension
{


    /// <summary>
    /// 序列化成JSON字符串
    /// </summary>
    public static class ObjectExtension
    {


        public static string ToJson(this object o)
        {
            //var jst = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }; //可以忽略所有null值的属性
            return JsonConvert.SerializeObject(o);
        }

        public static string ToJson(this object o, JsonSerializerSettings serializerSettings)
        {
            return JsonConvert.SerializeObject(o, serializerSettings);
        }
        public static string ToJson(this object o, JsonConverter jsonConverter)
        {
            //var jst = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }; //可以忽略所有null值的属性
            return JsonConvert.SerializeObject(o,  jsonConverter);
        }
    }
}