using System;

namespace CS.Diagnostics
{
    /// <summary>
    /// 控制台类型跟踪器
    /// </summary>
    public class ConsoleTracer:ITracer
    {
        private readonly object _syncLock = new object();

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


        public bool IsDebugEnabled =>Level.HasFlag(TraceLevel.Debug);
        public bool IsInfoEnabled => Level.HasFlag(TraceLevel.Info);
        public bool IsWarnEnabled => Level.HasFlag(TraceLevel.Warn);
        public bool IsErrorEnabled => Level.HasFlag(TraceLevel.Error);
        public bool IsFatalEnabled => Level.HasFlag(TraceLevel.Fatal);


        public void Debug(object message)
        {
            WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, message);
        }

        public void Debug(object message, Exception exception)
        {
            WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }

        public void DebugFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, string.Format(format, args));
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, string.Format(provider, format, args));
        }

        public void Info(object message)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, message);
        }

        public void Info(object message, Exception exception)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }

        public void InfoFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, string.Format(format, args));
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, string.Format(provider, format, args));
        }

        public void Warn(object message)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, message);
        }

        public void Warn(object message, Exception exception)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }

        public void WarnFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, string.Format(format, args));
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, string.Format(provider, format, args));
        }

        public void Error(object message)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, message);
        }

        public void Error(object message, Exception exception)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }

        public void ErrorFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format(format, args));
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format(provider, format, args));
        }



        public void Fatal(object message)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, message);
        }

        public void Fatal(object message, Exception exception)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, $"{message}\r\nException:{exception.Message}\r\nStrack:{exception.StackTrace}");
        }

        public void FatalFormat(string format, params object[] args)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format(format, args));
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format(provider,format,args));
        }


        void WriteLine(ConsoleColor foreColor, ConsoleColor backColor, object str)
        {
            lock (_syncLock)
            {
                Console.ForegroundColor = foreColor;
                Console.BackgroundColor = backColor;
                Console.WriteLine("[{0:HH:mm:ss,ffff}] {1}", DateTime.Now,  str);
                Console.ForegroundColor = ConsoleColor.White; //恢复成前景白色
                Console.BackgroundColor = ConsoleColor.Black;//恢复成黑色背景
            }
        }

    }
}