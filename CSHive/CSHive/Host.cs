using System;

namespace CS
{
    /// <summary>
    /// 当前主机的相关情况，类似于Environment
    /// </summary>
    public class Host
    {

        /// <summary>
        /// 当前运行程序的目录
        /// </summary>
        public static string CurrentDirectory => Environment.CurrentDirectory;
        /// <summary>
        /// 当前CPU核心数（逻辑）
        /// </summary>
        public static int ProcessorCount => Environment.ProcessorCount;
        /// <summary>
        /// 系统换行符
        /// </summary>
        public static string NewLine => Environment.NewLine;
        /// <summary>
        /// 主机名称
        /// </summary>
        public static string UserDomainName => Environment.UserDomainName;


    }


    /// <summary>
    /// CLR运行时类型
    /// </summary>
    public enum ClrRuntimeType
    {

    }

}