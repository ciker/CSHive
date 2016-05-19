using System;
using System.Runtime.InteropServices;
using System.Security;

namespace CS.Diagnostics
{
    /// <summary>
    /// 进程相关信息辅助
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// 当前进程Id
        /// </summary>
        public static int ProcessId => ProcessPropertyAccess.GetCurrentProcessId();
        /// <summary>
        /// 当前线程Id
        /// </summary>
        public static int ThreadId => ProcessPropertyAccess.GetCurrentThreadId();

        class ProcessPropertyAccess
        {
            [SecurityCritical, SuppressUnmanagedCodeSecurity]
            private static class SafeNativeMethods
            {
                [DllImport("kernel32.dll")]
                public static extern int GetCurrentProcessId();

                [DllImport("kernel32.dll")]
                public static extern int GetCurrentThreadId();
            }

            private static readonly Func<int> ProcessIdAccessor;

            private static readonly Func<int> CurrentThreadIdAccessor;

            static ProcessPropertyAccess()
            {
                if (AppDomain.CurrentDomain.IsHomogenous && AppDomain.CurrentDomain.IsFullyTrusted)
                {
                    ProcessIdAccessor = new Func<int>(GetCurrentProcessIdSafe);
                    CurrentThreadIdAccessor = new Func<int>(GetCurrentThreadIdSafe);
                    return;
                }
                ProcessIdAccessor = null;
                CurrentThreadIdAccessor = null;
            }

            public static int GetCurrentProcessId()
            {
                if (ProcessIdAccessor == null)
                {
                    return 0;
                }
                return ProcessIdAccessor();
            }

            public static int GetCurrentThreadId()
            {
                if (CurrentThreadIdAccessor == null)
                {
                    return 0;
                }
                return CurrentThreadIdAccessor();
            }

            [SecuritySafeCritical]
            private static int GetCurrentProcessIdSafe()
            {
                int result;
                try
                {
                    result = SafeNativeMethods.GetCurrentProcessId();
                }
                catch (SecurityException)
                {
                    result = 0;
                }
                return result;
            }

            [SecuritySafeCritical]
            private static int GetCurrentThreadIdSafe()
            {
                int result;
                try
                {
                    result = SafeNativeMethods.GetCurrentThreadId();
                }
                catch (SecurityException)
                {
                    result = 0;
                }
                return result;
            }
        }
    }

    
}