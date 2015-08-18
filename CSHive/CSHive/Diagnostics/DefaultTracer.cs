using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS.Diagnostics
{
    class DefaultTracer:ITracerLog
    {
        private  readonly object _syncLock = new object();

        /// <summary>
        /// 输出内容级别，设定后可输出设定的级别的消息
        /// </summary>
        public TraceLevel Level { get; set; }

        public DefaultTracer()
        {
            Level = TraceLevel.All;
        }

        public bool IsDebug => Level.HasFlag(TraceLevel.Debug);

        public bool IsInfo => Level.HasFlag(TraceLevel.Info);

        public bool IsWarn => Level.HasFlag(TraceLevel.Warn);

        public bool IsError => Level.HasFlag(TraceLevel.Error);

        public bool IsFatal => Level.HasFlag(TraceLevel.Fatal);

        public void Debug(object msg,params object[] parameters)
        {
            lock (_syncLock)
            {
                WriteLine(ConsoleColor.DarkGray, ConsoleColor.Black, msg, parameters);
            }
        }

        public void Info(object msg, params object[] parameters)
        {
            WriteLine(ConsoleColor.Cyan, ConsoleColor.White, msg, parameters);
        }

        public void Warn(object msg, params object[] parameters)
        {
            WriteLine(ConsoleColor.Magenta, ConsoleColor.Black, msg, parameters);
        }

        public void Error(object msg, params object[] parameters)
        {
            WriteLine(ConsoleColor.Yellow, ConsoleColor.Black, msg, parameters);
        }

        public void Exception(string msg, Exception ex)
        {
            WriteLine(ConsoleColor.Yellow, ConsoleColor.Black,$"Message:{msg}\nException:{ex.Message}\nStackTrace:{ex.StackTrace}");
        }

        public void Fatal(object msg, params object[] parameters)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, msg, parameters);
        }


        void WriteLine(ConsoleColor foreColor, ConsoleColor backColor, object str, params object[] parameters)
        {
            lock (_syncLock)
            {
                Console.ForegroundColor = foreColor;
                Console.BackgroundColor = backColor;
                Console.WriteLine("[{0:HH:mm:ss,ffff}] {1}", DateTime.Now, parameters.Length == 0 ? str : string.Format(str.ToString(), parameters));
                Console.ForegroundColor = ConsoleColor.White; //恢复成前景白色
                Console.BackgroundColor = ConsoleColor.Black;//恢复成黑色背景
            }
        }
    }
}
