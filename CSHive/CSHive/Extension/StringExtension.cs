using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace System
{
    /// <summary>
    ///     针对字符串的扩展
    /// </summary>
    public static class StringExtension
    {



        /// <summary>
        ///  采用UTF8 编码URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string EncodeUrl(this string url) => HttpUtility.UrlEncode(url, Encoding.UTF8);

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string EncodeUrl(this string url, Encoding e) => HttpUtility.UrlEncode(url, e);

        #region ToEnum  T ToEnum<T>(string)

        /// <summary>
        /// 将字符串转换为 枚举
        /// <remarks>
        /// 如果是数字字符串时，枚举转换失败时输出原输入的字符串。
        /// 如果是非纯数字字符串时，有默认值时输出默认值，无时输出枚举中的第一项。
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="param">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static T ToEnum<T>(this string param)
        {
            return param.ToEnum(default(T));
        }

        /// <summary>
        /// 将字符串转换为 枚举
        /// <remarks>
        /// 如果是数字字符串时，枚举转换失败时输出原输入的字符串。
        /// 如果是非纯数字字符串时，有默认值时输出默认值，无时输出枚举中的第一项。
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="param">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static T ToEnum<T>(this string param, T defaultValue)
        {
            T result;
            try
            {
                result = (T)Enum.Parse(typeof(T), param, true);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 将字符串转换为 枚举
        /// <remarks>
        /// 当mustDefined==False时:
        /// 如果是数字字符串时，枚举转换失败时输出原输入的字符串。
        /// 如果是非纯数字字符串时，有默认值时输出默认值，无时输出枚举中的第一项。
        /// 如果是多个枚举值用，号隔开，那么只返回最后一个枚举值,非Flags时(Flags并且成员已经赋值1，2，4，8....时返回所有值)
        /// http://msdn.microsoft.com/zh-cn/library/vstudio/kxydatf9.aspx
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="param">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="mustDefined">是否必须为内部定义成员</param>
        /// <returns>转换结果</returns>
        public static T ToEnum<T>(this string param, T defaultValue, bool mustDefined)
        {
            T result;
            try
            {
                result = (T)Enum.Parse(typeof(T), param, true);
                if (mustDefined)
                {
                    var isDefined = Enum.IsDefined(typeof(T), result);
                    return isDefined ? result : defaultValue;
                }
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }



        #endregion


        #region long[] ToLongArray(string)  转为数组

        /// <summary>
        /// 可使用条件来返回结果
        /// </summary>
        /// <param name="s"></param>
        /// <param name="predicate"></param>
        /// <param name="separator">分隔符,exp: ,|W+-。即每个Char都将做为分隔符使用</param>
        /// <returns></returns>
        public static IEnumerable<long> ToLongArray(this string s, Predicate<long> predicate, string separator = ",")
        {
            var result = new List<long>();
            if (string.IsNullOrEmpty(s))
                return result;
            string[] intsrearray = s.Split(separator.ToCharArray());
            if (intsrearray.Length > 0)
            {
                for (long i = 0; i < intsrearray.Length; i++)
                {
                    long t;
                    if (long.TryParse(intsrearray[i], out t) && predicate(t))
                    {
                        result.Add(t);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 将字符串转为可枚举的数组
        /// </summary>
        /// <param name="param">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static IEnumerable<long> ToLongArray(this string param)
        {
            return param.ToLongArray(",");
        }

        ///<summary>
        /// 将字符串转为可枚举的数组
        ///</summary>
        ///<param name="str">参数</param>
        ///<param name="spliter">分隔符,exp: ,|W+-</param>
        ///<returns></returns>
        public static IEnumerable<long> ToLongArray(this string str, string spliter)
        {
            return String.IsNullOrEmpty(str) ? new long[0] : str.Split(spliter.ToCharArray()).ToLongArray();
        }

        /// <summary>
        /// 将字符串转为可枚举的数组
        /// </summary>
        /// <param name="param">需要转换的 string[]</param>
        /// <returns>转换结果</returns>
        public static IEnumerable<long> ToLongArray(this string[] param)
        {
            return Array.ConvertAll(param, ToLong);
        }


        #endregion


        #region int[] ToIntArray(string)  转为数组

        /// <summary>
        /// 可使用条件来返回结果
        /// </summary>
        /// <param name="s"></param>
        /// <param name="predicate"></param>
        /// <param name="separator">分隔符,exp: ,|W+-。即每个Char都将做为分隔符使用</param>
        /// <returns></returns>
        public static IEnumerable<int> ToIntArray(this string s, Predicate<int> predicate, string separator = ",")
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(s))
                return result;
            string[] intsrearray = s.Split(separator.ToCharArray());
            if (intsrearray.Length > 0)
            {
                for (int i = 0; i < intsrearray.Length; i++)
                {
                    int t;
                    if (int.TryParse(intsrearray[i], out t) && predicate(t))
                    {
                        result.Add(t);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 将字符串转为可枚举的数组
        /// </summary>
        /// <param name="param">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static IEnumerable<int> ToIntArray(this string param)
        {
            return param.ToIntArray(",");
        }

        ///<summary>
        /// 将字符串转为可枚举的数组
        ///</summary>
        ///<param name="str">参数</param>
        ///<param name="spliter">分隔符,exp: ,|W+-</param>
        ///<returns></returns>
        public static IEnumerable<int> ToIntArray(this string str, string spliter)
        {
            return String.IsNullOrEmpty(str) ? new int[0] : str.Split(spliter.ToCharArray()).ToIntArray();
        }

        /// <summary>
        /// 将字符串转为可枚举的数组
        /// </summary>
        /// <param name="param">需要转换的 string[]</param>
        /// <returns>转换结果</returns>
        public static IEnumerable<int> ToIntArray(this string[] param)
        {
            return Array.ConvertAll(param, ToInt);
        }


        #endregion


        #region ToNullDateTime() DateTime 类型处理

        /// <summary>
        /// 默认值为 默认值为 null
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static DateTime? ToNullDateTime(this string str)
        {
            return str.ToNullDateTime(null);
        }

        /// <summary>
        /// 转换失败时为 返回默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static DateTime? ToNullDateTime(this string p, DateTime? defaultValue)
        {
            DateTime result;
            return DateTime.TryParse(p, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime? ToNullDateTime(this string p, DateTime? defalutValue, DateTime min, DateTime max)
        {
            var r = p.ToNullDateTime(defalutValue);
            return r.HasValue ? r.Value.ToNullDateTime(defalutValue, min, max) : defalutValue;
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为 null
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime? ToNullDateTime(this string p, DateTime min, DateTime max)
        {
            return p.ToNullDateTime(null, min, max);
        }


        #endregion


        #region ToDateTime() DateTime 类型处理

        /// <summary>
        /// 默认值为 默认值为 0001/1/1 0:00:00
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static DateTime ToDateTime(this string str)
        {
            return str.ToDateTime(DateTime.MinValue);
        }

        /// <summary>
        /// 默认值为 0001/1/1 0:00:00
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static DateTime ToDateTime(this string p, DateTime defaultValue)
        {
            DateTime result;
            if (!DateTime.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime ToDateTime(this string p, DateTime defalutValue, DateTime min, DateTime max)
        {
            var r = p.ToDateTime(defalutValue);
            return r.ToDateTime(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为 0001/1/1 0:00:00
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static DateTime ToDateTime(this string p, DateTime min, DateTime max)
        {
            return p.ToDateTime(DateTime.MinValue, min, max);
        }


        #endregion


        #region ToFloat() Float float 类型处理

        /// <summary>
        /// 默认值为0
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static float ToFloat(this string str)
        {
            return str.ToFloat(0);
        }

        /// <summary>
        /// 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static float ToFloat(this string p, float defaultValue)
        {
            float result;
            if (!float.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static float ToFloat(this string p, float defalutValue, float min, float max)
        {
            var r = p.ToFloat(defalutValue);
            return r.ToFloat(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static float ToFloat(this string p, float min, float max)
        {
            return p.ToFloat(0, min, max);
        }


        #endregion


        #region ToDouble() Double double 类型处理

        /// <summary>
        /// 默认值为0
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static double ToDouble(this string str)
        {
            return str.ToDouble(0);
        }

        /// <summary>
        /// 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static double ToDouble(this string p, double defaultValue)
        {
            double result;
            if (!double.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static double ToDouble(this string p, double defalutValue, double min, double max)
        {
            var r = p.ToDouble(defalutValue);
            return r.ToDouble(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static double ToDouble(this string p, double min, double max)
        {
            return p.ToDouble(0, min, max);
        }


        #endregion


        #region ToDecimal() decimal Decimal 类型处理

        /// <summary>
        /// 默认值为0
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static decimal ToDecimal(this string str)
        {
            return str.ToDecimal(0);
        }

        /// <summary>
        /// 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static decimal ToDecimal(this string p, decimal defaultValue)
        {
            decimal result;
            if (!decimal.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static decimal ToDecimal(this string p, decimal defalutValue, decimal min, decimal max)
        {
            var r = p.ToDecimal(defalutValue);
            return r.ToDecimal(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static decimal ToDecimal(this string p, decimal min, decimal max)
        {
            return p.ToDecimal(0, min, max);
        }


        #endregion


        #region ToLong() long Int64 类型处理

        /// <summary>
        /// 默认值为0
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static long ToLong(this string str)
        {
            return str.ToLong(0);
        }

        /// <summary>
        /// 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static long ToLong(this string p, long defaultValue)
        {
            long result;
            if (!long.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static long ToLong(this string p, long defalutValue, long min, long max)
        {
            var r = p.ToLong(defalutValue);
            return r.ToLong(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static long ToLong(this string p, long min, long max)
        {
            return p.ToLong(0, min, max);
        }


        #endregion


        #region ToInt() Int32 类型处理

        /// <summary>
        /// 将字符串转换为int ，默认值为0
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static int ToInt(this string str)
        {
            return str.ToInt(0);
        }

        /// <summary>
        /// 将字符串转换为 int ， 转换失败时为默认值
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static int ToInt(this string p, int defaultValue)
        {
            int result;
            if (!int.TryParse(p, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="defalutValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static int ToInt(this string p, int defalutValue, int min, int max)
        {
            var r = p.ToInt(defalutValue);
            return r.ToInt(defalutValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) 默认值为0
        /// </summary>
        /// <param name="p">参数值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回值</returns>
        public static int ToInt(this string p, int min, int max)
        {
            return p.ToInt(0, min, max);
        }

        #endregion


        #region ToUShort() ushort  UInt16 类型处理

        /// <summary>
        /// 将字符串转换为 ushort , 默认值0
        /// </summary>
        /// <param name="param">需要转换的 文本</param>
        /// <returns>转换结果</returns>
        public static ushort ToUShort(this string param)
        {
            return param.ToUShort(0);
        }

        /// <summary>
        /// 将字符串转换为 short
        /// </summary>
        /// <param name="param">需要转换的 文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static ushort ToUShort(this string param, ushort defaultValue)
        {
            ushort result;
            if (!ushort.TryParse(param, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ushort ToUShort(this string p, ushort defaultValue, ushort min, ushort max)
        {
            var r = p.ToUShort(defaultValue);
            return r.ToUShort(defaultValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) , 默认值0
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static ushort ToUShort(this string p, ushort min, ushort max)
        {
            return p.ToUShort(0, min, max);
        }

        #endregion


        #region ToShort() short  Int16 类型处理

        /// <summary>
        /// 将字符串转换为 short , 默认值0
        /// </summary>
        /// <param name="param">需要转换的 文本</param>
        /// <returns>转换结果</returns>
        public static short ToShort(this string param)
        {
            return param.ToShort(0);
        }

        /// <summary>
        /// 将字符串转换为 short
        /// </summary>
        /// <param name="param">需要转换的 文本</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public static short ToShort(this string param, short defaultValue)
        {
            short result;
            if (!short.TryParse(param, out result)) result = defaultValue;
            return result;
        }

        /// <summary>
        /// 范围约束(闭区间)
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static short ToShort(this string p, short defaultValue, short min, short max)
        {
            var r = p.ToShort(defaultValue);
            return r.ToShort(defaultValue, min, max);
        }

        /// <summary>
        /// 范围约束(闭区间) , 默认值0
        /// </summary>
        /// <param name="p">输入的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>不介于最小最大之间时返回默认值</returns>
        public static short ToShort(this string p, short min, short max)
        {
            return p.ToShort(0, min, max);
        }

        #endregion


        #region HexToByte 16制字符串转为Byte数组

        /// <summary>
        /// 16制字符串转为Byte数组
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexToByte(this string hex)
        {
            string fixedHex = hex.Replace("-", string.Empty);
            // array to put the result in
            byte[] bytes = new byte[fixedHex.Length / 2];
            // variable to determine shift of high/low nibble
            int shift = 4;
            // offset of the current byte in the array
            int offset = 0;
            // loop the characters in the string
            foreach (char c in fixedHex)
            {
                // get character code in range 0-9, 17-22
                // the % 32 handles lower case characters
                int b = (c - '0') % 32;
                // correction for a-f
                if (b > 9) b -= 7;
                // store nibble (4 bits) in byte array
                bytes[offset] |= (byte)(b << shift);
                // toggle the shift variable between 0 and 4
                shift ^= 4;
                // move to next byte
                if (shift != 0) offset++;
            }
            return bytes;
        }

        #endregion


        #region ToByte() ToPackageByte() btye[] 字符串转为byte[]

        /// <summary>
        /// 将字符串按UTF8格式转为byte[]
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string param)
        {
            return Encoding.UTF8.GetBytes(param);
        }

        /// <summary>
        /// 将字符串按某种编码格式转换为字节流
        /// </summary>
        /// <param name="param"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string param, Encoding encoding)
        {
            return encoding.GetBytes(param);
        }

        /// <summary>
        /// 将字符串转为UTF8的字节数组并在头上加上长度
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] ToBytesPackage(this string message)
        {
            return message.ToBytes().ToPackage();
        }

        /// <summary>
        /// 将字符串转为某种编码的字节数组并在头上加上长度
        /// </summary>
        /// <param name="message"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytesPackage(this string message, Encoding encoding)
        {
            return message.ToBytes(encoding).ToPackage();
        }


        #endregion


        #region ToBool 字符串转为Bool

        /// <summary>
        /// 参数P是否被成功转换,类似于TryParse
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认值，转换失败时输出的值</param>
        /// <param name="result">输出转换的结果</param>
        /// <param name="trueChars">判定为真的字符串的设定字符集</param>
        /// <returns>是否转换成功</returns>
        public static bool TryBool(this string p, bool defaultValue, out bool result, params string[] trueChars)
        {
            var rst = false; //默认转换失败
            result = defaultValue;  //默认结果为默认值
            if (p == null) return defaultValue;
            if (trueChars.Any(s => p.Equals(s, StringComparison.CurrentCultureIgnoreCase)))
            {
                rst = true;
                result = true;
            }
            if (!rst)
                rst = Boolean.TryParse(p, out result);
            return rst;
        }

        /// <summary>
        /// 将参数p转换为bool
        /// <remarks>"1"默认为真</remarks>
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">转换失败时的值</param>
        /// <param name="trueChars">判定为真时的字符串变参，默认为真的是:"1","true"</param>
        /// <returns>转换结果</returns>
        public static bool ToBoolWithTrueChars(this string p, bool defaultValue, params string[] trueChars)
        {
            bool result;
            var chars = trueChars.Length == 0 ? new[] { "1", "true" } : trueChars;
            TryBool(p, defaultValue, out result, chars);
            return result;
        }

        /// <summary>
        /// 将参数p转换为bool
        /// </summary>
        /// <param name="p"></param>
        /// <param name="trueChars">判定为真时的字符串变参，默认为真的是:"1","true"</param>
        /// <returns>转换时为false</returns>
        public static bool ToBoolWithTrueChars(this string p, params string[] trueChars)
        {
            return ToBoolWithTrueChars(p, false, trueChars);
        }


        /// <summary>
        /// 将参数p转换为bool
        /// </summary>
        /// <param name="p">需要转换的文本</param>
        /// <param name="defaultValue">默认结果，转换失败时的值</param>
        /// <returns>转换结果</returns>
        public static bool ToBool(this string p, bool defaultValue)
        {
            bool val;
            var rst = bool.TryParse(p, out val);
            return rst ? val : defaultValue;
        }
        /// <summary>
        /// 将参数p转换为bool类型结果
        /// </summary>
        /// <param name="p"></param>
        /// <returns>转换失败时为</returns>
        public static bool ToBool(this string p)
        {
            return ToBool(p, false);
        }

        #endregion


        #region  将字符串进行简单的类型转换 ToType

        /// <summary>
        /// 扩展方法，获取字符串对应的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type ToType(this  string type)
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
            }
        }

        #endregion


        #region 字符串剪切

        /// <summary>
        ///     按字节数左剪切
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ByteLeft(this string s, int count)
        {
            int bytecount = 0;
            int charcount = 0;
            foreach (char c in s)
            {
                bytecount += (c > 255 ? 2 : 1);
                if (bytecount >= count)
                    break;
                charcount++;
            }
            return charcount >= s.Length ? s : s.Substring(0, charcount);
        }

        /// <summary>
        ///     按字符数左剪切
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string s, int length)
        {
            return s.Length <= length ? s : s.Substring(0, length);
        }

        /// <summary>
        ///     按字节数右剪切
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ByteRight(this string s, int count)
        {
            int bytecount = 0;
            int charcount = 0;
            foreach (char c in s)
            {
                bytecount += (c > 255 ? 2 : 1);
                if (bytecount >= count)
                    break;
                charcount++;
            }
            return charcount >= s.Length ? s : s.Substring(s.Length - charcount, charcount);
        }

        /// <summary>
        ///     按字符数右剪切
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string s, int length)
        {
            return s.Length <= length ? s : s.Substring(s.Length - length, length);
        }

        #endregion


        #region 字符串统计

        /// <summary>
        ///  字符串的字节长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ByteLength(this string s)
        {
            var l = 0;
            foreach (char c in s)
            {
                if (c > 255)
                    l = l + 2;
                else
                    l++;
            }
            return l;
        }

        #endregion


        #region Html 安全字符串处理

        /// <summary>
        /// 转换为安全的HTML字符串
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ToSafeHtml(this string html)
        {
            var sf = html.Replace("<", "&#60");
            sf = sf.Replace(">", "&#62");
            return sf;
        }

        /// <summary>
        /// 先截取后转为安全HTML字符串
        /// </summary>
        /// <param name="html"></param>
        /// <param name="maxLength"></param>
        /// <param name="omitStr">默认省略字符串为空串</param>
        /// <returns></returns>
        public static string CutToSafeHtml(this string html, int maxLength, string omitStr = "")
        {
            var val = string.Format("{0}{1}", html.ByteLeft(maxLength), omitStr);
            return val.ToSafeHtml();
        }

        #endregion


        #region T  Convert<T>(string)

        /// <summary>
        /// 将字符串转换为 目标类型 (有装箱拆箱的损失)
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="p">需要转换的文本</param>
        /// <returns>转换结果</returns>
        public static T Convert<T>(this string p)
        {
            T result;
            switch (Type.GetTypeCode(typeof(T)))
            {
                //case TypeCode.Byte:
                //    result = (T)(object)Byte(p);
                //    break;
                //case TypeCode.Int32:
                //    result = (T)(object)Int(p);
                //    break;
                default:
                    result = default(T);
                    break;
            }
            return result;
        }

        #endregion


    }
}