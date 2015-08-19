using System;

namespace CS.Attribute
{
    /// <summary>
    /// 枚举文本描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EnumTextAttribute : TextAttribute
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nativeName"></param>
        public EnumTextAttribute(string nativeName)
        {
            NativeName = nativeName;
        }

        /// <summary>
        /// 枚举的名称（定义的字符串名）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 枚举的值（int类型）
        /// </summary>
        public int Value { get; set; }

    }
}