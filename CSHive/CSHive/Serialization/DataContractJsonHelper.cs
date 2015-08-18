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

    [DataContract]
    public class DemoModel
    {
        [DataMember]
        public int Id { get; set; } = 6;

        [DataMember(Order = 2)]
        public string Name { get; set; }
    }
}