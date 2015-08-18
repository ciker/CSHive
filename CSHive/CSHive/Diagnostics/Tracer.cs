using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace CS.Diagnostics
{
    /// <summary>
    /// Debug与Trace时的调试跟踪输出，控制台下使用
    /// <remarks>
    /// 默认情况下输出所有级别的消息
    /// DEBUG：深灰色
    /// INFO ： 信息 青色（蓝绿色）
    /// WARN : 警告 紫红色
    /// ERROR : 错误，异常 黄色
    /// FATAL ： 致命错误 红色
    /// 服务器接收消息输出为(白色背景)InDebug(),InInfo()
    /// </remarks>
    /// </summary>
    public static class Tracer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracerLog"></param>
        public static void Register(ITracerLog tracerLog)
        {
            _tracerLog = tracerLog;
        }

        private static ITracerLog _tracerLog;
        static ITracerLog TracerLog => _tracerLog ?? (_tracerLog = new DefaultTracer());

        

        #region 属性

       
        /// <summary>
        /// 
        /// </summary>
        public static bool IsDebug => TracerLog.IsDebug;

        /// <summary>
        /// 
        /// </summary>
        public static bool IsInfo => TracerLog.IsInfo;
        /// <summary>
        /// 
        /// </summary>
        public static bool IsWarn => TracerLog.IsWarn;
        /// <summary>
        /// 
        /// </summary>
        public static bool IsError => TracerLog.IsError;
        /// <summary>
        /// 
        /// </summary>
        public static bool IsFatal => TracerLog.IsFatal;

        #endregion


        #region 消息输出方法

        /// <summary>
        /// 按消息级别输出
        /// </summary>
        /// <param name="level"></param>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        public static void Out(TraceLevel level, string str, params object[] parameters)
        {
            switch (level)
            {
                case TraceLevel.Debug:
                    Debug(str, parameters);
                    break;
                case TraceLevel.Info:
                    Debug(str, parameters);
                    break;
                case TraceLevel.Warn:
                    Warn(str, parameters);
                    break;
                case TraceLevel.Error:
                    Error(str, parameters);
                    break;
                case TraceLevel.Fatal:
                    Fatal(str, parameters);
                    break;
            }
        }


        /// <summary>
        /// 输入数据调试消息(只在调试时)
        /// </summary>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void InDebug(object o)
        {
            if (!IsDebug || o == null) return;
            InDebug(o.ToString());
        }

        /// <summary>
        /// 输入数据调试消息(只在调试时)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void InDebug(string str, params object[] parameters)
        {
            if (!IsDebug) return;
           TracerLog.Debug(str,parameters);
        }

        /// <summary>
        /// 调试消息(只在调试时输出)
        /// </summary>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void Debug(object o)
        {
            if (!IsDebug || o == null) return;
            TracerLog.Debug(o);
        }

        /// <summary>
        /// 调试消息(只在调试时输出)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void Debug(string str, params object[] parameters)
        {
            if (!IsDebug) return;
            TracerLog.Debug(str,parameters);
        }

        /// <summary>
        /// 消息输出，仅用于输出一些注释性的说明
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        public static void Info(string str, params object[] parameters)
        {
            if (!IsInfo) return;
            TracerLog.Info(str,parameters);
        }

        /// <summary>
        /// 接收到的消息输出，仅用于输出一些注释性的说明
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        public static void InInfo(string str, params object[] parameters)
        {
            if (!IsInfo) return;
            TracerLog.Info(str, parameters);
        }

        /// <summary>
        /// 警告信息，某些数据有问题之类的
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        public static void Warn(string str, params object[] parameters)
        {
            if (!IsWarn) return;
            TracerLog.Warn(str, parameters);
        }

        /// <summary>
        /// 输出异常堆栈
        /// </summary>
        /// <param name="ex">异常堆栈信息</param>
        public static void Error(Exception ex)
        {
            //Error("<!--\r\n{0}\r\n-->",ex);
            TracerLog.Error("{0}", ex);
        }

        /// <summary>
        /// 一般性错误，如异常等
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        public static void Error(string str, params object[] parameters)
        {
            if (!IsError) return;
           TracerLog.Error(str, parameters);
        }

        /// <summary>
        /// 输出异常堆栈
        /// </summary>
        /// <param name="ex">异常堆栈信息</param>
        public static void Fatal(Exception ex)
        {
            TracerLog.Fatal("{0}", ex);
        }

        /// <summary>
        /// 致命错误，该类错误会导致服务器挂起
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        public static void Fatal(string str, params object[] parameters)
        {
            if (!IsFatal) return;
            TracerLog.Fatal( str, parameters);
        }



        #endregion


        #region 颜色输出方式

        private static readonly object syncLock = new object();
        static void WriteLine(ConsoleColor foreColor, ConsoleColor backColor, string str, params object[] parameters)
        {
            lock (syncLock)
            {
                Console.ForegroundColor = foreColor;
                Console.BackgroundColor = backColor;
                Console.WriteLine("[{0:HH:mm:ss,ffff}] {1}", DateTime.Now, parameters.Length == 0 ? str : string.Format(str, parameters));
                Console.ForegroundColor = ConsoleColor.White; //恢复成前景白色
                Console.BackgroundColor = ConsoleColor.Black;//恢复成黑色背景
            }
        }

        /// <summary>
        /// 调试时输出
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void WriteYellow(string str, params object[] parameters)
        {
            WriteLine(ConsoleColor.Yellow, ConsoleColor.Black, str, parameters);
        }

        /// <summary>
        /// 调试时输出
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void WriteGreen(string str, params object[] parameters)
        {
            WriteLine(ConsoleColor.Green, ConsoleColor.Black, str, parameters);
        }

        /// <summary>
        /// 调试时输出
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void WriteRed(string str, params object[] parameters)
        {
            WriteLine(ConsoleColor.Red, ConsoleColor.Black, str, parameters);
        }

        /// <summary>
        /// 调试时输出
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameters"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void WriteBlue(string str, params object[] parameters)
        {
            WriteLine(ConsoleColor.Blue, ConsoleColor.White, str, parameters);
        }

        #endregion


        #region 对像相关属性输出


        /// <summary>
        /// 输出DataSet里数据
        /// </summary>
        /// <param name="ds"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                DebugModel(dt, dt.TableName);
            }
        }

        /// <summary>
        /// 输出DataTable里数据
        /// </summary>
        /// <param name="dt"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel(DataTable dt)
        {
            DebugModel(dt, null);
        }

        /// <summary>
        /// 输出DataTable里数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="label"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel(DataTable dt, string label)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"----- PRINT {1}[{0}] -----", typeof(DataTable), label);
            if (dt == null || dt.Select().Length <= 0)
            {
                Console.WriteLine(@"     NULL DataTable (NO RESULT)    ");
                return;
            }
            var rows = dt.Select();
            foreach (DataColumn dc in dt.Columns)
            {
                Console.Write("{0}\t", dc.ColumnName);
            }
            Console.WriteLine();
            foreach (var r in rows)
            {
                foreach (DataColumn c in r.Table.Columns)
                {
                    Console.Write("{0}\t", r[c]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        /// <summary>
        ///  输出可枚举集合里的对像属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel<T>(List<T> items)
        {
            DebugModel(items, null);
        }

        /// <summary>
        ///  输出可枚举集合里的对像属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="lable"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel<T>(List<T> items, string lable)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"----- PRINT PROPERTIES FOR {1} [{0}] -----", typeof(List<T>), lable);
            if (items == null)
            {
                Console.WriteLine(@"     NULL REFERENCE (NO SHOW)      ");
                return;
            }
            if (!items.Any())
            {
                Console.WriteLine(@"     EMPTY COLLECTION (NO RESULT)    ");
                return;
            }
            var properties = typeof(T).GetProperties();
            foreach (var info in properties)
            {
                Console.Write("{0}\t", info.Name);
            }
            Console.WriteLine();
            foreach (var item in items)
            {
                var t = item.GetType();
                foreach (var info in properties)
                {
                    Console.Write("{0}\t", t.GetProperty(info.Name).GetValue(item, null));
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }


        /// <summary>
        /// 输出某一对像的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel<T>(T item)
        {
            DebugModel(item, null);
        }

        /// <summary>
        /// 输出某一对像的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="lable"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel<T>(T item, string lable)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"----- PRINT PROPERTIES FOR {1} [{0}] -----", typeof(T), lable);
            if (item == null)
            {
                Console.WriteLine(@"     NULL REFERENCE (NO SHOW)    ");
                return;
            }
            var properties = typeof(T).GetProperties();
            foreach (var info in properties)
            {
                Console.Write("{0}\t", info.Name);
            }
            Console.WriteLine();
            var t = item.GetType();
            foreach (var info in properties)
            {
                Console.Write("{0}\t", t.GetProperty(info.Name).GetValue(item, null));
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }


        #endregion

    }


   
}