using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CS.Serialization
{
    /// <summary>
    /// 可实例化的字节序列化与反序列化
    /// </summary>
    public class ByteSerializor
    {
        /// <summary>
        /// 把对象序列化并返回相应的字节
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns>byte[]</returns>
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;
            using (var memory = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memory, obj);
                memory.Position = 0;
                var read = new byte[memory.Length];
                memory.Read(read, 0, read.Length);
                memory.Close();
                return read;
            }
        }

        /// <summary>
        /// 把字节反序列化成相应的对象
        /// </summary>
        /// <param name="bytes">字节流</param>
        public static T Deserialize<T>(byte[] bytes)
        {
            if (bytes == null)
                return default(T);
            using (var memory = new MemoryStream(bytes))
            {
                memory.Position = 0;
                var formatter = new BinaryFormatter();
                var newOjb = formatter.Deserialize(memory);
                memory.Close();
                return (T) newOjb;
            }
        }
    }
}