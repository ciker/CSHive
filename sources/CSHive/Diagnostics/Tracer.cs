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
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tracerLog"></param>
        //public static void Register(ITracerLog tracerLog)
        //{
        //    _tracerLog = tracerLog;
        //}

        //private static ITracerLog _tracerLog;
        //static ITracerLog TracerLog => _tracerLog ?? (_tracerLog = new DefaultTracer());


        /// <summary>
        /// 注册一个实现了<see cref="ITracer"/>的日志实例
        /// </summary>
        /// <param name="tracerLog"></param>
        public static void Register(ITracer tracerLog)
        {
            _tracer = tracerLog;
        }

        private static ITracer _tracer;

        static ITracer TraceLog => _tracer ?? (_tracer = new ConsoleTracer());
        



        #region 属性

       
        /// <summary>
        /// 
        /// </summary>
        public static bool IsDebug => TraceLog.IsDebugEnabled;

        /// <summary>
        /// 
        /// </summary>
        public static bool IsInfo => TraceLog.IsInfoEnabled;
        /// <summary>
        /// 
        /// </summary>
        public static bool IsWarn => TraceLog.IsWarnEnabled;
        /// <summary>
        /// 
        /// </summary>
        public static bool IsError => TraceLog.IsErrorEnabled;
        /// <summary>
        /// 
        /// </summary>
        public static bool IsFatal => TraceLog.IsFatalEnabled;

        #endregion


        #region 消息输出方法
        
        /// <summary>
        /// 输入数据调试消息(只在调试时)
        /// </summary>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void Debug(object msg)
        {
            TraceLog.Debug(msg);
        }

        /// <summary>
        /// 输入数据调试消息(只在调试时)
        /// </summary>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void Debug(object msg,Exception exp )
        {
            TraceLog.Debug(msg,exp);
        }

        /// <summary>
        /// 调试消息(只在调试时输出)
        /// </summary>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugFormat(string msg, params object[] pms)
        {
            TraceLog.DebugFormat(msg, pms);
        }


        /// <summary>
        /// 消息输出，仅用于输出一些注释性的说明
        /// </summary>
        public static void Info(object msg)
        {
            TraceLog.Info(msg);
        }
        /// <summary>
        /// 接收到的消息输出，仅用于输出一些注释性的说明
        /// </summary>
        public static void Info(object msg, Exception exp)
        {
            TraceLog.Info(msg,exp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pms"></param>
        public static void InfoFormat(string msg, params object[] pms)
        {
            TraceLog.InfoFormat(msg, pms);
        }


        /// <summary>
        /// 
        /// </summary>
        public static void Error(object msg)
        {
            TraceLog.Error(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Error(object msg, Exception exp)
        {
            TraceLog.Error(msg, exp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pms"></param>
        public static void ErrorFormat(string msg, params object[] pms)
        {
            TraceLog.ErrorFormat(msg, pms);
        }


        /// <summary>
        /// 
        /// </summary>
        public static void Warn(object msg)
        {
            TraceLog.Warn(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Warn(object msg, Exception exp)
        {
            TraceLog.Warn(msg, exp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pms"></param>
        public static void WarnFormat(string msg, params object[] pms)
        {
            TraceLog.WarnFormat(msg, pms);
        }


        /// <summary>
        /// 
        /// </summary>
        public static void Fatal(object msg)
        {
            TraceLog.Fatal(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Fatal(object msg, Exception exp)
        {
            TraceLog.Fatal(msg, exp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pms"></param>
        public static void FatalFormat(string msg, params object[] pms)
        {
            TraceLog.FatalFormat(msg, pms);
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

        /// <summary>
        /// 输出静态类的所有属性值
        /// </summary>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel(Type staticClassType)
        {
            DebugModel(staticClassType, null);
        }

        /// <summary>
        /// 输出静态类的所有属性值
        /// </summary>
        /// <param name="staticClassType"></param>
        /// <param name="lable"></param>
        [Conditional("DEBUG"), Conditional("TRACE")]
        public static void DebugModel(Type staticClassType,string lable)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"----- PRINT PROPERTIES FOR {1} [{0}] -----", staticClassType, lable);
            var type = staticClassType;
            var prs = type.GetProperties();
            foreach (var pr in prs)
            {
                Console.WriteLine($"{pr.Name}:\t{pr.GetValue(null, null)}");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
           
        }

        #endregion

    }


   
}