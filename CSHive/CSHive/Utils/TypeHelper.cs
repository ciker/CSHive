using System;

namespace CS.Utils
{

    /// <summary>
    /// 类型辅助
    /// </summary>
    public class TypeHelper
    {

        /// <summary>
        /// 转换值为目标类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static  object ChangeType(object value, Type type)
        {
            //if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, (string) value);
                return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, innerValue);
            }
            if (value is string && type == typeof(Guid)) return new Guid((string) value);
            if (value is string && type == typeof(Version)) return new Version((string) value);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }
    }
}