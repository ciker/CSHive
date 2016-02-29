using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CS.Attribute
{
    /// <summary>
    /// 针对枚举扩展的实体信息
    /// </summary>    
    public interface IEnumInfo
    {
        /// <summary>
        /// 是否忽略不显示出来
        /// </summary>
        bool Ignore { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 属性对应的值（必须为Int类型，暂不支持其它类型）
        /// <remarks>为了兼容字符串与数值这儿为object类型</remarks>
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// 本地化显示的名称
        /// </summary>
        string NativeName { get; set; }

        /// <summary>
        /// 初始化其它属性
        /// </summary>
        /// <param name="o"></param>
        void Init(object o);

    }

    /// <summary>
    /// 枚举信息
    /// </summary>
    [Serializable]
    public class EnumInfo : IEnumInfo
    {

        public bool Ignore { get; set; }
       
        public string Name { get; set; }

        public object Value { get; set; }
        
        public string NativeName { get; set; }
        /// <summary>
        /// 子类重写进行相关扩展的属性初始化
        /// </summary>
        /// <param name="o"></param>
        public virtual void Init(object o)
        {
            //默认里没有其它属性初始化，子类重写进行相关扩展的属性初始化
        }
    }


    /// <summary>
    /// 带排序性质的枚举信息
    /// </summary>
    public class EnumOrderInfo : EnumInfo
    {
        /// <summary>
        /// 排序属性
        /// </summary>
        public int Order { get; set; }


        public override void Init(object o)
        {
            var extAtt = o as EnumOrderExtAttribute;
            if (extAtt != null)
            {
                Order = extAtt.Order;
            }
        }
    }

    /// <summary>
    /// EnumInfo的相关扩展方法
    /// </summary>
    public static class EnumInfoExt
    {
        /// <summary>
        /// 通过值获取名称(取不到时为原值)
        /// </summary>
        /// <param name="ol"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetName(this List<EnumInfo> ol, object value)
        {
            var item = ol.FirstOrDefault(x => x.Value == value);
            //return item == null ? value.ToString(CultureInfo.InvariantCulture) : item.Name;
            return item == null ? value?.ToString() : item.Name;
        }

        /// <summary>
        ///  通过值获取本地化(中文)名称(取不到时为原值)
        /// </summary>
        /// <param name="ol"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetNativeNameName(this List<EnumInfo> ol, object value)
        {
            var item = ol.FirstOrDefault(x => x.Value?.ToString() == (value?.ToString()));
            //return item == null ? value.ToString(CultureInfo.InvariantCulture) : item.NativeName;
            return item == null ? value?.ToString() : item.NativeName;
        }

        ///// <summary>
        ///// 通过值获取颜色
        ///// </summary>
        ///// <param name="ol"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static int GetColor(this List<EnumInfo> ol, int value)
        //{
        //    var item = ol.FirstOrDefault(x => x.Value == value);
        //    return item == null ? 0 : item.BgColor;
        //}
    }
}