using System.Xml.Serialization;
using CS.Serialization;

namespace System
{
    /// <summary>
    /// 对象扩展
    /// </summary>
    public static class ObjectExtension
    {


        #region ToNullDateTime() DateTime 类型处理

        /// <summary>
        /// 默认值 null
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <returns></returns>
        public static DateTime? ToNullDateTime(object obj)
        {
            return obj.ToNullDateTime(null);
        }

        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static DateTime? ToNullDateTime(this object obj, DateTime? defaultValue)
        {
            if (obj == null) return defaultValue;
            if (obj is DateTime) return (DateTime)obj;
            DateTime val;
            return DateTime.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime? ToNullDateTime(this object obj, DateTime? defalutValue, DateTime min, DateTime max)
        {
            var r = obj.ToNullDateTime(defalutValue);
            return r.HasValue ? r.ToNullDateTime(defalutValue, min, max) : defalutValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为 null
        /// </summary>
        /// <param name="o"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static DateTime? ToNullDateTime(this object o, DateTime min, DateTime max)
        {
            return o.ToNullDateTime(null, min, max);
        }

        #endregion


        #region ToDateTime() DateTime 类型处理

        /// <summary>
        /// 默认值 0001/1/1 0:00:00
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            return obj.ToDateTime(DateTime.MinValue);
        }

        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static DateTime ToDateTime(this object obj, DateTime defaultValue)
        {
            if (obj == null) return defaultValue;
            if (obj is DateTime) return (DateTime)obj;
            DateTime val;
            return DateTime.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime ToDateTime(this object obj, DateTime defalutValue, DateTime min, DateTime max)
        {
            var r = obj.ToDateTime(defalutValue);
            return r.ToDateTime(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0001/1/1 0:00:00
        /// </summary>
        /// <param name="o"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object o, DateTime min, DateTime max)
        {
            return o.ToDateTime(DateTime.MinValue, min, max);
        }

        #endregion


        #region ToDouble() double Double 类型处理

        /// <summary>
        /// 默认值(null,转换失败时)为0
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <returns></returns>
        public static double ToDouble(object obj)
        {
            return obj.ToDouble(0);
        }

        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static double ToDouble(this object obj, double defaultValue)
        {
            if (obj == null) return defaultValue;
            if (obj is double) return (double)obj;
            double val;
            return double.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static double ToDouble(this object obj, double defalutValue, double min, double max)
        {
            var r = obj.ToDouble(defalutValue);
            return r.ToDouble(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="o"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static double ToDouble(this object o, double min, double max)
        {
            return o.ToDouble(0, min, max);
        }

        #endregion


        #region ToDecimal() decimal Decimal 类型处理

        /// <summary>
        /// 默认值(null,转换失败时)为0
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj)
        {
            return obj.ToDecimal(0);
        }

        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue)
        {
            if (obj == null) return defaultValue;
            if (obj is decimal) return (decimal)obj;
            decimal val;
            return decimal.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static decimal ToDecimal(this object obj, decimal defalutValue, decimal min, decimal max)
        {
            var r = obj.ToDecimal(defalutValue);
            return r.ToDecimal(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="o"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object o, decimal min, decimal max)
        {
            return o.ToDecimal(0, min, max);
        }

        #endregion


        #region ToLong() Int64 类型处理

        /// <summary>
        /// 默认值(null,转换失败时)为0
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <returns></returns>
        public static long ToLong(object obj)
        {
            return obj.ToLong(0);
        }

        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static long ToLong(this object obj, long defaultValue)
        {
            if (obj == null) return defaultValue;
            if (obj is long) return (long)obj;
            long val;
            return long.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static long ToLong(this object obj, long defalutValue, long min, long max)
        {
            var r = obj.ToLong(defalutValue);
            return r.ToLong(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="o"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long ToLong(this object o, long min, long max)
        {
            return o.ToLong(0, min, max);
        }

        #endregion


        #region ToInt() Int32  IsInt32类型处理

        /// <summary>
        /// 是否为Int32
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsInt32(this object o)
        {
            if (o == null) return false;
            if (o is Int32) return true;
            int val;
            return Int32.TryParse(o.ToString(), out val);
        }

        /// <summary>
        /// 默认值(null,转换失败时)为0
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            return obj.ToInt(0);
        }

        /// <summary>
        /// 空对象或转换失败时为默认值
        /// </summary>
        /// <param name="obj">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static int ToInt(this object obj, int defaultValue)
        {
            if (obj == null) return defaultValue;
            if (obj is int) return (int)obj;
            int val;
            return int.TryParse(obj.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static int ToInt(this object obj, int defalutValue, int min, int max)
        {
            var r = obj.ToInt(defalutValue);
            return r.ToInt(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="o"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int ToInt(this object o, int min, int max)
        {
            return o.ToInt(0, min, max);
        }

        #endregion


        #region ToUShort() ushort  UInt16 类型处理

        /// <summary>
        /// 默认值0
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ushort ToUShort(this object p)
        {
            return p.ToUShort(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">需要转换的 object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static ushort ToUShort(this object param, ushort defaultValue)
        {
            if (param == null) return defaultValue;
            if (param is ushort) return (ushort)param;
            ushort val;
            return ushort.TryParse(param.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ushort ToUShort(this object obj, ushort defaultValue, ushort min, ushort max)
        {
            var r = obj.ToUShort(defaultValue);
            return r.ToUShort(defaultValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static ushort ToUShort(this object obj, ushort min, ushort max)
        {
            return obj.ToUShort(0, min, max);
        }

        #endregion


        #region ToShort() short  int16 类型处理

        /// <summary>
        /// 默认值0
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static short ToShort(this object p)
        {
            return p.ToShort(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">需要转换的 object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static short ToShort(this object param, short defaultValue)
        {
            if (param == null) return defaultValue;
            if (param is short) return (short)param;
            short val;
            return short.TryParse(param.ToString(), out val) ? val : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="obj">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static short ToShort(this object obj, short defaultValue, short min, short max)
        {
            var r = obj.ToShort(defaultValue);
            return r.ToShort(defaultValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static short ToShort(this object obj, short min, short max)
        {
            return obj.ToShort(0, min, max);
        }

        #endregion


        #region ToByte() Byte byte   类型处理(Byte为8位长的一个字节，类似ToByte(defalut,min,max)的操作基本不存在)


        #endregion


        #region ToBool 字符串转为Bool

        /// <summary>
        /// 判断对象是否为空
        /// </summary>
        /// <param name="p">对像引用</param>
        /// <returns>非空为真，否则为假</returns>
        public static bool ToBool(this object p)
        {
            return p.ToBool(false);
        }

        /// <summary>
        /// null或转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static bool ToBool(this object p, bool defaultValue)
        {
            if (p == null) return defaultValue;
            if (p is bool) return (bool)p;
            bool val;
            return bool.TryParse(p.ToString(), out val) ? val : defaultValue;
        }

        #endregion

        #region ToXml,FromXml

        /// <summary>
        /// 序列化成XML
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T o)
        {
            // xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"
            var rst =  XmlSerializor.Serialize(o);
            return rst.Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"","");
        }
        /// <summary>
        /// 将XML序列化成目标对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T FromXml<T>(this string xml)
        {
            return XmlSerializor.Deserialize<T>(xml);
        }

        #endregion


        #region others tests

        ////static MethodInfo byteArrayToHexString;

        ///// <summary>
        ///// 反射调用该方法, 并且缓存.
        ///// </summary>
        ///// <returns></returns>
        //static MethodInfo ByteArrayToHexStringMethod()
        //{
        //    if (byteArrayToHexString == null)
        //    {
        //        Type type = typeof(System.Web.Configuration.MachineKeySection);
        //        byteArrayToHexString = type.GetMethod("ByteArrayToHexString", BindingFlags.Static | BindingFlags.NonPublic);
        //    }
        //    return byteArrayToHexString;
        //}



        //public static K[] ParseIDArray<T, K>(ComponentCollection<T> obj, ParseAction<T, K> action) where T : IComponent
        //{
        //    if (obj.Count == 0) return null;
        //    if (obj.Count == 1) return new K[] { action(obj[0]) };
        //    List<K> result = new List<K>();
        //    foreach (T item in obj)
        //        result.Add(action(item));
        //    return result.ToArray();
        //}

        #endregion


    }
}