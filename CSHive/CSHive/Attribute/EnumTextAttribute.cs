using System;

namespace CS.Attribute
{
    /// <summary>
    /// 枚举文本描述
    /// </summary>
    
    public class EnumTextAttribute : TextAttribute
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nativeName"></param>
        public EnumTextAttribute(string nativeName):this(nativeName,0)
        {
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nativeName"></param>
        /// <param name="order"></param>
        public EnumTextAttribute(string nativeName, int order)
        {
            NativeName = nativeName;
            Order = order;
        }

        /// <summary>
        /// 枚举的名称（定义的字符串名）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 枚举的值（int类型）
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 排序
        /// <remarks>
        /// 默认等于Value值
        /// </remarks>
        /// </summary>
        public int Order { get; set; }

    }
}