using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CS.Diagnostics;

namespace CS.Utils
{

    /// <summary>
    /// 类型辅助
    /// </summary>
    public class TypeHelper
    {

        /// <summary>
        /// 返回某了父类下所有子类的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetSubclassObjects<T>()
        {
            var types = GetSubclasses(typeof (T));
            return types.Select(tp => (T) Activator.CreateInstance(tp)).Where(instance => instance != null).ToList();
        }

        /// <summary>
        /// 返回某了父类下所有子类的类型<see cref="Type"/>集合
        /// </summary>
        /// <returns></returns>
        public static List<Type> GetSubclasses(Type baseType)
        {
            var types = Assembly.GetAssembly(baseType).GetTypes().Where(x => x.IsSubclassOf(baseType)).ToList();
            return types;
            //try
            //{
            //    var types = Assembly.GetAssembly(baseType).GetTypes().Where(x => x.IsSubclassOf(baseType)).ToList();
            //    return types;
            //}
            //catch (ReflectionTypeLoadException ex)
            //{
            //    foreach (var exception in ex.LoaderExceptions)
            //    {
            //        Tracer.Error("LoaderExceptions[]", ex);
            //    }
            //    Tracer.Error("InnerException", ex.InnerException);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

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


        /// <summary>
        /// 扩展方法，获取字符串对应的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type ToType(string type)
        {
            switch (type.ToLower())
            {
                case "bool":
                    return Type.GetType("System.Boolean", true, true);
                case "byte":
                    return Type.GetType("System.Byte", true, true);
                case "sbyte":
                    return Type.GetType("System.SByte", true, true);
                case "char":
                    return Type.GetType("System.Char", true, true);
                case "decimal":
                    return Type.GetType("System.Decimal", true, true);
                case "double":
                    return Type.GetType("System.Double", true, true);
                case "float":
                    return Type.GetType("System.Single", true, true);
                case "int":
                    return Type.GetType("System.Int32", true, true);
                case "uint":
                    return Type.GetType("System.UInt32", true, true);
                case "long":
                    return Type.GetType("System.Int64", true, true);
                case "ulong":
                    return Type.GetType("System.UInt64", true, true);
                case "object":
                    return Type.GetType("System.Object", true, true);
                case "short":
                    return Type.GetType("System.Int16", true, true);
                case "ushort":
                    return Type.GetType("System.UInt16", true, true);
                case "string":
                    return Type.GetType("System.String", true, true);
                case "date":
                case "datetime":
                    return Type.GetType("System.DateTime", true, true);
                case "guid":
                    return Type.GetType("System.Guid", true, true);
                default:
                    return Type.GetType(type, true, true);
                    //return GetTypeFromAssembly(type);
            }
        }



    }
}