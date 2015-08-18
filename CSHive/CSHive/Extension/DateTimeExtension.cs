namespace System
{
    /// <summary>
    /// 关于时间的扩展
    /// </summary>
    public static class DateTimeExtension
    {


        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static DateTime ToDateTime(this DateTime p, DateTime defaultValue, DateTime min, DateTime max)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为 0001/1/1 0:00:00
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static DateTime ToDateTime(this DateTime p, DateTime min, DateTime max)
        {
            return p.ToDateTime(DateTime.MinValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static DateTime? ToNullDateTime(this DateTime? p, DateTime? defaultValue, DateTime min, DateTime max)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为null
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static DateTime? ToNullDateTime(this DateTime? p, DateTime min, DateTime max)
        {
            return p.ToNullDateTime(null, min, max);
        }


        static readonly DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));

        /// <summary>
        ///  换为Unix时间戳格式(毫秒)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTime(this DateTime dateTime)
        {
            return (long)Math.Round((dateTime - StartTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        ///    换为Unix时间戳格式(秒)
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>double</returns>
        public static int ToSecondTime(this DateTime dateTime)
        {
            return (int)Math.Round((dateTime - StartTime).TotalSeconds, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 将毫秒时间转为当前时间
        /// </summary>
        /// <param name="millionseconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(long millionseconds)
        {
            return StartTime.AddMilliseconds(millionseconds);
        }
        /// <summary>
        /// 将秒时间转为当前时间
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(int seconds)
        {
            return StartTime.AddSeconds(seconds);
        }





    }

}