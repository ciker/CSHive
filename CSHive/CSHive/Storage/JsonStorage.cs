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
using System.Runtime.Serialization.Json;

namespace CS.Storage
{
    /// <summary>
    ///   
    /// </summary>
    /// 
    /// <description class = "CS.Data.Storage.JsonStorage">
    ///   
    /// </description>
    /// 
    /// <history>
    ///   2010-7-15 15:14:01 , zhouyu ,  创建	     
    ///  </history>
    public class JsonStorage
    {
         public static Stream SaveToStream<T>(T value, Stream stream = null)
        {
            if (stream == null) stream = new MemoryStream();
            (new DataContractJsonSerializer(typeof(T))).WriteObject(stream, value);
            return stream;
        }

        public static string SaveToText<T>(T value)
        {
            var stream = SaveToStream(value);
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            var result = sr.ReadToEnd();
            sr.Close();

            return result;
        }


        public static T LoadFromStream<T>(Stream stream)
        {
            stream.Position = 0;
            return (T)(new DataContractJsonSerializer(typeof(T))).ReadObject(stream);
        }

        public static T LoadFromText<T>(string value)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms);
            writer.Write(value);

            var result = LoadFromStream<T>(ms);
            writer.Close();

            return result;
        }
    }
}

