using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CS.Serialization
{
    /// <summary>
    /// 通过DataContract序列化模型
    /// <code>
    /// var user={"name":"张三","gender":"男","birthday":"1980-8-8"}
    /// </code>
    /// </summary>
    public  class DataContractJsonHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string Serialize<T>(T t)
        {
            var ser = new DataContractJsonSerializer(typeof (T));
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T Deserialize<T>(string jsonString)
        {
            var ser = new DataContractJsonSerializer(typeof (T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                T obj = (T) ser.ReadObject(ms);
                return obj;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class DataContractJsonExt
    {
        /// <summary>
        /// 序列化成DataContractJson字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDataJson<T>(this T obj)
        {
            return DataContractJsonHelper.Serialize(obj);
        }

        /// <summary>
        /// 把DataContractJson反序列化成实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T FromDataJson<T>(this string str)
        {
            return DataContractJsonHelper.Deserialize<T>(str);
        }
    }


    [DataContract]
    public class DemoModel
    {
        [DataMember]
        public int Id { get; set; } = 6;

        [DataMember(Order = 2)]
        public string Name { get; set; }
    }
}