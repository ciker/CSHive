using System;
using System.IO;
using static System.String;

namespace CS.Utils
{
    /// <summary>
    /// 系统相关变量与信息
    /// </summary>
    public class AppHelper
    {
        /// <summary>
        /// 当前应用程序的基础路径
        /// </summary>
        public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;

        ///// <summary>
        ///// 返回本地格式的 文件的绝对路径
        ///// </summary>
        ///// <param name="paths"></param>
        ///// <returns></returns>
        //public static string GetFilePath(params string[] paths)
        //{
        //    return $"{CombinePath(paths)}";
        //}





    }
}