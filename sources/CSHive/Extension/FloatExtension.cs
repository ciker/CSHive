namespace System
{
    /// <summary>
    /// Dobule double的扩展
    /// </summary>
    public static class FloatExtension
    {
        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static float ToFloat(this float p, float defaultValue, float min, float max)
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
        /// <returns>不介于最小最大之间时返回默认值0</returns>
        public static float ToFloat(this float p, float min, float max)
        {
            return p.ToFloat(0, min, max);
        }

     
    }
}