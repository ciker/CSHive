namespace System
{
    /// <summary>
    /// 多用于HTML页面上的相关扩展
    /// </summary>
    public static class HtmlExtension
    {
        /// <summary>
        /// 如果值不为空时生成 key="value" 字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToProperty(this string value, string key)
        {
            return string.IsNullOrWhiteSpace(value) ? null : $"{key}=\"{value}\"";
        }

        /// <summary>
        /// 如果值不为空时生成 key="value" 字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SetProperty(this string key, string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : $"{key}=\"{value}\"";
        }
        /// <summary>
        /// 根据表达式结果设定属性值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="express"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SetProperty(this  bool express, string key, string value)
        {
            return express ? key.SetProperty(value) : null;
        }
    }
}