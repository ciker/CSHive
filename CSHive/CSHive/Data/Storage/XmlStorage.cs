#region copyright info

//------------------------------------------------------------------------------
// <copyright company="ChaosStudio">
//     Copyright (c) 2002-2010 巧思工作室.  All rights reserved.
//     Contact:		MSN:zhouyu@cszi.com , QQ:478779122
//		Link:				http://www.69sea.com http://www.cszi.com
// </copyright>
//------------------------------------------------------------------------------

#endregion

using System.IO;
using System.Xml.Serialization;

namespace CS.Data.Storage
{
    /// <summary>
    ///   对象的XML持久化
    /// </summary>
    /// 
    /// <description class = "CS.Data.Storage.XmlStorage">
    ///   
    /// </description>
    /// 
    /// <history>
    ///   2010-7-15 15:15:37 , zhouyu ,  创建	     
    ///  </history>
    public class XmlStorage
    {
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>存在性</returns>
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 对象序列化后以XML格式保存
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">保存路径</param>
        /// <param name="value">对象的引用</param>
        public static void Save<T>(string path, T value)
        {
            using (var writer = new StreamWriter(path))
            {
                (new XmlSerializer(typeof (T))).Serialize(writer, value);
            }
        }

        /// <summary>
        /// 将保存后的XML文件反序列化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>对象实例</returns>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(string.Format("{0} not exist.",path));
            using (var reader = new StreamReader(path))
            {
                return (T) (new XmlSerializer(typeof (T))).Deserialize(reader);
            }
        }
      
    }
}