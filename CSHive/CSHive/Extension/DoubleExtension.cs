namespace System
{
    /// <summary>
    /// Dobule double的扩展
    /// </summary>
    public static class DoubleExtension
    {
        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static double ToDouble(this double p, double defaultValue, double min, double max)
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
        public static double ToDouble(this double p, double min, double max)
        {
            return p.ToDouble(0, min, max);
        }

     
    }
}