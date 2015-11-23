using System;
using System.IO;
using static System.String;

namespace CS.Utils
{
    /// <summary>
    /// 系统相关变量与信息
    /// </summary>
    public class App
    {
        /// <summary>
        /// 当前应用程序的基础路径
        /// </summary>
        public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Path 拼接
        /// </summary>
        /// <param name="paths">需要拼接的 Path 参数</param>
        /// <returns>拼接后的结果</returns>
        public static string CombinePath(params string[] paths)
        {
            var path = Join(Path.DirectorySeparatorChar + "", paths);
            path = path.Replace('/', Path.DirectorySeparatorChar);
            if (path.StartsWith("~" + Path.DirectorySeparatorChar) || path.StartsWith(Path.DirectorySeparatorChar + ""))
                path = BaseDirectory + path.TrimStart('~', Path.DirectorySeparatorChar);
            return path;
        }

        
    }
}