namespace System
{
    /// <summary>
    /// Int16 short 扩展
    /// </summary>
    public static class ShortExtension
    {
        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static short ToShort(this short p, short defaultValue, short min, short max)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static short ToShort(this short p, short min, short max)
        {
            return p.ToShort(0, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ushort ToUShort(this ushort p, ushort defaultValue, ushort min, ushort max)
        {
            if (min <= p && p <= max) return p;
            return defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ushort ToUShort(this ushort p, ushort min, ushort max)
        {
            return p.ToUShort(0, min, max);
        }
    }
}