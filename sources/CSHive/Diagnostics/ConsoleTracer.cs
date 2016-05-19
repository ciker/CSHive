using System;
using System.Runtime.CompilerServices;

namespace CS.Diagnostics
{
    /// <summary>
    /// 控制台类型跟踪器
    /// </summary>
    public class ConsoleTracer:ITracer
    {
        

        /// <summary>
        /// 输出内容级别，设定后可输出设定的级别的消息
        /// </summary>
        public TraceLevel Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConsoleTracer()
        {
            Level = TraceLevel.All;
        }

        /// <summary>
        /// 是否为调试
        /// </summary>
        public bool IsDebugEnabled =>Level.HasFlag(TraceLevel.Debug);
        /// <summary>
        /// 是否为信息
        /// </summary>
        public bool IsInfoEnabled => Level.HasFlag(TraceLevel.Info);
        /// <summary>
        /// 是否为警告
        /// </summary>
        public bool IsWarnEnabled => Level.HasFlag(TraceLevel.Warn);
        /// <summary>
        /// 是否为错误
        /// </summary>
        public bool IsErrorEnabled => Level.HasFlag(TraceLevel.Error);
        /// <summary>
        /// 是否为致命错误
        /// </summary>
        public bool IsFatalEnabled => Level.HasFlag(TraceLevel.Fatal);

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message)
        {
            WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, message);
        }
        /// <summary>
        /// 调试
        /// </summary>
        public void Debug(object message, Exception exception)
        {
            WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }
        /// <summary>
        /// 调试
        /// </summary>
        public void DebugFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, string.Format(format, args));
        }
        /// <summary>
        /// 调试
        /// </summary>
        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, string.Format(provider, format, args));
        }
        /// <summary>
        /// 信息
        /// </summary>
        public void Info(object message)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, message);
        }
        /// <summary>
        /// 信息
        /// </summary>
        public void Info(object message, Exception exception)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }
        /// <summary>
        /// 信息
        /// </summary>
        public void InfoFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, string.Format(format, args));
        }
        /// <summary>
        /// 信息
        /// </summary>
        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, string.Format(provider, format, args));
        }
        /// <summary>
        /// 警告
        /// </summary>
        public void Warn(object message)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, message);
        }
        /// <summary>
        /// 警告
        /// </summary>
        public void Warn(object message, Exception exception)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }
        /// <summary>
        /// 警告
        /// </summary>
        public void WarnFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, string.Format(format, args));
        }
        /// <summary>
        /// 警告
        /// </summary>
        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, string.Format(provider, format, args));
        }
        /// <summary>
        /// 错误
        /// </summary>
        public void Error(object message)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, message);
        }
        /// <summary>
        /// 错误
        /// </summary>
        public void Error(object message, Exception exception)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }
        /// <summary>
        /// 错误
        /// </summary>
        public void ErrorFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format(format, args));
        }
        /// <summary>
        /// 错误
        /// </summary>
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format(provider, format, args));
        }


        /// <summary>
        /// 致命错误
        /// </summary>
        public void Fatal(object message)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, message);
        }
        /// <summary>
        /// 致命错误
        /// </summary>
        public void Fatal(object message, Exception exception)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }
        /// <summary>
        /// 致命错误
        /// </summary>
        public void FatalFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format(format, args));
        }
        /// <summary>
        /// 致命错误
        /// </summary>
        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format(provider,format,args));
        }

        //private readonly object _syncLock = new object();

        /// <summary>
        /// 整个方法锁定使用 MethodImplOptions.Synchronized
        /// </summary>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        /// <param name="str"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        static void WriteLine(ConsoleColor foreColor, ConsoleColor backColor, object str)
        {
            //lock (_syncLock)
            //{ 已由特性标注出
            //}
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.WriteLine("[{0:HH:mm:ss,ffff}] {1}", DateTime.Now, str);
            Console.ForegroundColor = ConsoleColor.White; //恢复成前景白色
            Console.BackgroundColor = ConsoleColor.Black;//恢复成黑色背景
        }

    }
}