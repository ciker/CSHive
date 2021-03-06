﻿using System;

namespace CS.Diagnostics
{
    /// <summary>
    /// 诊断日志接口
    /// <remarks>
    /// 有一个默认认的使用Console的实现，在项目中可以使用自已的实现来代替以便将消息输出到不同持久化实体中
    /// </remarks>
    /// </summary>
    public interface ITracer
    {
        /// <summary>
        /// 消息输出可用 <see cref="TraceLevel.Debug" />
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="TraceLevel.Info" />
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="TraceLevel.Warn" />
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="TraceLevel.Error" />
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 消息输出可用 <see cref="TraceLevel.Fatal" />
        /// </summary>
        bool IsFatalEnabled { get; }


        /// <summary>
        /// Debug消息输出
        /// </summary>
        /// <param name="message"></param>
        void Debug(object message);

        /// <summary>
        /// 记录Debug消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// 记录Debug ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void DebugFormat(string format, params object[] args);

        //void Debug(string format, object arg0);

        //void Debug(string format, object arg0, object arg1);

        //void DebugF(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Debug ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void DebugFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Info消息输出
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);

        /// <summary>
        /// 记录Info消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// 记录Info ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void InfoFormat(string format, params object[] args);

        //void Info(string format, object arg0);

        //void Info(string format, object arg0, object arg1);

        //void Info(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Info ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void InfoFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Warn消息输出
        /// </summary>
        /// <param name="message"></param>
        void Warn(object message);

        /// <summary>
        /// 记录Warn消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// 记录Warn ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void WarnFormat(string format, params object[] args);

        //void Warn(string format, object arg0);

        //void Warn(string format, object arg0, object arg1);

        //void Warn(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Warn ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void WarnFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Error消息输出
        /// </summary>
        /// <param name="message"></param>
        void Error(object message);

        /// <summary>
        /// 记录Warn消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// 记录Error ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void ErrorFormat(string format, params object[] args);

        //void Warn(string format, object arg0);

        //void Warn(string format, object arg0, object arg1);

        //void Warn(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Warn ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void ErrorFormat(IFormatProvider provider, string format, params object[] args);


        /// <summary>
        /// Fatal消息输出
        /// </summary>
        /// <param name="message"></param>
        void Fatal(object message);

        /// <summary>
        /// 记录Fatal消息，并且包含<see cref="System.Exception" /> 的堆栈消息
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">包括跟踪堆栈的异常</param>
        void Fatal(object message, Exception exception);

        /// <summary>
        /// 记录Fatal ，使用格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void FatalFormat(string format, params object[] args);

        //void Fatal(string format, object arg0);

        //void Fatal(string format, object arg0, object arg1);

        //void Fatal(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// 记录Fatal ，使用格式化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void FatalFormat(IFormatProvider provider, string format, params object[] args);
    }

    /// <summary>
    /// 调试级别，对应不同的方法
    /// </summary>
    [Flags]
    public enum TraceLevel
    {
        /// <summary>
        /// 关闭输出
        /// </summary>
        Off = 0,
        /// <summary>
        /// 输出全开
        /// </summary>
        All = Debug | Info | Warn | Error | Fatal,
        /// <summary>
        /// 调试 对应条件编译Debug
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 消息
        /// </summary>
        Info = 2,
        /// <summary>
        /// 警告
        /// </summary>
        Warn = 4,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 8,
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 16,
    }
}