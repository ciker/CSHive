namespace System
{
    /// <summary>
    /// Int32扩展
    /// </summary>
    public static class IntExtension
    {

        /// <summary>
        /// 当两个给出的int值相同时返回trueStr，否则返回null
        /// </summary>
        /// <param name="int1"></param>
        /// <param name="int2"></param>
        /// <param name="trueStr"></param>
        /// <returns></returns>
        public static string IsEqual(this int int1, int int2, string trueStr)
        {
            return int1 == int2 ? trueStr : null;
        }

        /// <summary>
        /// 返回Init对应的字节数组
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this int param)
        {
            return BitConverter.GetBytes(param);
        }

        #region ToInt() Int32 类型处理



        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="param">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static int ToInt(this int param, int defalutValue, int min, int max)
        {
            if (min <= param && param <= max) return param;
            return defalutValue;
        }


        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="param">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static int ToInt(this int param, int min, int max)
        {
            return param.ToInt(0, min, max);
        }

        #endregion


        #region ToEnum(int) ToEnum(int,T)  用int来返回可用的枚举类型

        /// <summary>
        /// 将 数值 转换为 枚举
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="param">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static T Enum<T>(int param)
        {
            return (param + "").ToEnum<T>();
        }

        /// <summary>
        /// 将 数值 转换为 枚举
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="param">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static T ToEnum<T>(int param, T defaultValue)
        {
            return (param + "").ToEnum<T>(defaultValue);
        }

        #endregion

    }
}