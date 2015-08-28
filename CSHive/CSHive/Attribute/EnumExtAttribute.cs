using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CS.Attribute
{
    /// <summary>
    /// 枚举文本描述
    /// </summary>
    //[AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class EnumExtAttribute : TextAttribute
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nativeName"></param>
        public EnumExtAttribute(string nativeName):this(nativeName,0)
        {
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nativeName"></param>
        /// <param name="bgColor">32位颜色值的Int表现形式</param>
        public EnumExtAttribute(string nativeName, int bgColor):base(nativeName)
        {
            BgColor = bgColor;
        }

        /// <summary>
        /// 是否忽略当前项
        /// </summary>
        public bool Ignore { get; set; }
        /// <summary>
        /// 标识色
        /// </summary>
        public int BgColor { get; set; }
        /// <summary>
        /// 排序
        /// <remarks>
        /// 默认等于Value值
        /// </remarks>
        /// </summary>
        public int Order { get; set; }

    }

    /// <summary>
    /// 针对枚举的扩展
    /// <remarks>TODO：枚举不能继承short等非Int或者转换出错</remarks>
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        /// 返回枚举类型的自定义描述特性集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<EnumInfo> GetItems(this Type type)
        {
            var list = new List<EnumOrderInfo>();
            if (type.IsEnum)
            {
                var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                list = (from fi in fields select fi.GetValue(null) into value let name = Enum.GetName(type, value) where name != null select new EnumOrderInfo(name, (int)value)).ToList();
                var mbs = type.GetMembers();
                foreach (var info in mbs)
                {
                    var attr = System.Attribute.GetCustomAttribute(info, typeof(EnumExtAttribute)) as EnumExtAttribute;
                    if (attr == null) continue;
                    //Console.WriteLine("{0}",  info.Name);
                    var item = list.First(x => x.Name == info.Name);
                    if (item == null) continue;
                    item.NativeName = attr.NativeName;
                    item.BgColor = attr.BgColor;
                    item.Order = attr.Order;
                    item.Ignore = attr.Ignore;
                }
            }
            return list.Where(x => !x.Ignore).OrderBy(x => x.Order).Cast<EnumInfo>().ToList();
        }
    }

    /// <summary>
    /// 枚举信息
    /// </summary>
    public class EnumInfo
    {
        public EnumInfo()
        {
        }

        public EnumInfo(string name)
            : this(name, name, 0)
        {
        }

        public EnumInfo(string name, int value)
            : this(name, name, value)
        {
        }

        public EnumInfo(string nativeName, string name, int value)
        {
            NativeName = nativeName;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// 是否忽略不显示出来
        /// </summary>
        public bool Ignore { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public int BgColor { get; set; }
        /// <summary>
        /// 本地化显示的名称
        /// </summary>
        public string NativeName { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 属性对应的值（必须为Int类型，暂不支持其它类型）
        /// </summary>
        public int Value { get; set; }

    }

    /// <summary>
    /// 带排序性质的枚举信息
    /// </summary>
    public class EnumOrderInfo : EnumInfo
    {
        public EnumOrderInfo(string name, int value)
            : base(name, value)
        {
        }

        public int Order { get; set; }
    }

    public static class EnumInfoExt
    {
        /// <summary>
        /// 通过值获取名称(取不到时为原值)
        /// </summary>
        /// <param name="ol"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetName(this List<EnumInfo> ol, int value)
        {
            var item = ol.FirstOrDefault(x => x.Value == value);
            return item == null ? value.ToString(CultureInfo.InvariantCulture) : item.Name;
        }
        /// <summary>
        ///  通过值获取中文名称(取不到时为原值)
        /// </summary>
        /// <param name="ol"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetGbName(this List<EnumInfo> ol, int value)
        {
            var item = ol.FirstOrDefault(x => x.Value == value);
            return item == null ? value.ToString(CultureInfo.InvariantCulture) : item.NativeName;
        }

        /// <summary>
        /// 通过值获取颜色
        /// </summary>
        /// <param name="ol"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetColor(this List<EnumInfo> ol, int value)
        {
            var item = ol.FirstOrDefault(x => x.Value == value);
            return item == null ? 0 : item.BgColor;
        }

    }


}