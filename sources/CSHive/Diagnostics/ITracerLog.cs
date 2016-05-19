using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CS.Diagnostics
{
    /// <summary>
    /// 诊断日志接口
    /// <remarks>
    /// 实现该接口后进行注册到Tracer中
    /// </remarks>
    /// </summary>
    [Obsolete("过期，请使用ITracer来实现新的实现")]
    public interface ITracerLog
    {
        /// <summary>
        /// 是否输出
        /// </summary>
        bool IsDebug { get; }
        /// <summary>
        /// 是否输出
        /// </summary>
       bool IsInfo { get;  }
        /// <summary>
        /// 是否输出
        /// </summary>
         bool IsWarn { get; }
        /// <summary>
        /// 是否输出
        /// </summary>
         bool IsError { get;  }
        /// <summary>
        /// 是否输出
        /// </summary>
         bool IsFatal { get; }

        /// <summary>
        /// DEBUG  输出
        /// </summary>
        /// <param name="msg"></param>
         void Debug(object msg, params object[] parameters);
        /// <summary>
        /// INFO 输出
        /// </summary>
        /// <param name="msg"></param>
         void Info(object msg, params object[] parameters);

        /// <summary>
        /// Warn 输出
        /// </summary>
        /// <param name="msg"></param>
         void Warn(object msg, params object[] parameters);

        /// <summary>
        /// 错误输出
        /// </summary>
        /// <param name="msg"></param>
         void Error(object msg, params object[] parameters);

        /// <summary>
        /// 异常提示
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Exception(string msg, Exception ex);

        /// <summary>
        /// 致命输出
        /// </summary>
        /// <param name="msg"></param>
         void Fatal(object msg, params object[] parameters);

    }


   
}
