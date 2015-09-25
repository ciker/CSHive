using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CS.Validation;

namespace CS.Http
{
    /// <summary>
    ///  参数(Param)的处理方法
    ///  Ver:0.10
    /// </summary>
    /// 
    /// <description class = "CS.ParamMethod">
    ///   将传入的字符串参数转换为目标题类型值
    ///   <para>Note:1. 仅针对字符串进行处理，不能处理其它对象。</para>
    ///   <para>Note:2. 参数为非Valid状态时返回的值为默认值，默认值的目的就是为了在不检测状态时也能获得一个合法的值。</para>
    ///   <para>Note:3. 除Default和Empty状态外，所有的转换都能返回值，但状态不同决定了该值的正确性。</para>
    ///   <para>Note:3. 转换失败的含义也包括超出期望的范围。</para>
    ///   <para>Note:4. 有些的转换是无法设定默认值的，此时直接取值将会引发异常。</para>
    /// </description>
    /// 
    /// <history>
    /// 2010-09-19 , zhouyu , created
    ///  </history>
    public class ParamMethod
    {
        #region 公有属性

        /// <summary>
        /// 原始值
        /// </summary>
        public string Origin { get; private set; }

        /// <summary>
        /// 从原始值转换后的值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// ParamState 状态
        /// </summary>
        public ParamState State { get; private set; }

        #endregion

        /// <summary>
        /// 移去XML文档里的
        /// 修正Utf8文档中的变长字符引起的错误
        /// </summary>
        private static readonly Regex XmlCharReplacePattern = new Regex("[\x00-\x08|\x0b-\x0c|\x0e-\x1f]+",
            RegexOptions.Compiled);

        private readonly int _index;
        private readonly string _key;

        /// <summary>
        /// 使用指定的 index, value 初始化 ParamMethod 对象
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <param name="value">值</param>
        internal ParamMethod(int index, string value)
        {
            _index = index;
            Init(value);
        }

        /// <summary>
        /// 使用指定的 键名key, value 初始化 ParamMethod 对象
        /// </summary>
        /// <param name="key">索引的键名</param>
        /// <param name="value">值</param>
        internal ParamMethod(string key, string value)
        {
            _key = key;
            Init(value);
        }

        /// <summary>
        /// 对字符串进行初始化
        /// </summary>
        /// <param name="value"></param>
        private void Init(string value)
        {
            Origin = String.IsNullOrEmpty(value) ? String.Empty : unchecked((string) value.Clone());
            State = CheckState(ref value);
            Value = value;
        }

        /// <summary>
        /// 检查值的状态
        /// <para>Note:第一次状态检测，不为空串时即为Valid</para>
        /// </summary>
        /// <param name="value">传至服务器的变量集合的String值</param>
        /// <returns>返回对应的 ParamState 状态</returns>
        internal static ParamState CheckState(ref string value)
        {
            if (value == null)
                return ParamState.Default;
            if (value == String.Empty)
                return ParamState.Empty;
            value = value.Trim();
            return value == String.Empty ? ParamState.Empty : ParamState.Valid;
        }

        /// <summary>
        /// 根据当前对象创建 Param 对象.
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <returns>返回转换后的 Param 对象</returns>
        internal Param<T> Create<T>()
        {
            var result = new Param<T> {Key = _key, State = State};
            return result;
        }

        /// <summary>
        /// 通用的转换
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <returns>返回转换后的 Param 对象.</returns>
        public Param<T> To<T>()
        {
            var result = Create<T>();
            result.Key = _key;
            result.State = State;
            result.Value = Value.Convert<T>();
            return result;
        }


        //Todo:可以将下面的方法委托给别的类来处理 , 增加一个方法，可以将转换委托给别的方法处理。

        #region ToBool,ToByte,ToShort,ToInt32 ... 类型转换 

        #region ToBool zhouyu 20100916

        /// <summary>
        /// 转换为 bool 类型
        /// </summary>
        public Param<bool> ToBool(params string[] trueChars)
        {
            return ToBool(false, trueChars);
        }

        /// <summary>
        ///  转换为 bool 类型的 Param。
        /// <para>1. 转换失败后的Param.Value取决于defaultValue</para>
        /// <para>2. 判定为真的变参默认为{"1", "true", "on"}</para>
        /// </summary>
        /// <param name="defaultValue">默认的值，转换失败时的值。</param>
        /// <param name="trueChars">判定为真的字符串的变参(不区分大小写)如："1", "true", "on","是","真"</param>
        /// <returns>转换后的Param对象</returns>
        public Param<bool> ToBool(bool defaultValue, params string[] trueChars)
        {
            var param = Create<bool>();
            if (param.State != ParamState.Valid)
            {
                param.Value = defaultValue;
                return param;
            }
            var chars = trueChars.Length == 0 ? new[] {"1", "true", "on"} : trueChars;
            bool result;
            var rst = Value.TryBool(defaultValue, out result, chars);
            param.State = rst ? ParamState.Valid : ParamState.ParseError;
            param.Value = result; //Note:一旦直接设定值时则值即可直接取出
            return param;
        }

        #endregion

        #region ToByte

        /// <summary>
        /// 转换为 byte 类型的 Param.
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<byte> ToByte()
        {
            return ToByte(0);
        }

        /// <summary>
        /// 转换为 byte 类型的 Param.
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<byte> ToByte(byte defaultValue)
        {
            return ToByte(defaultValue, byte.MinValue, byte.MaxValue);
        }

        /// <summary>
        /// 转换为 byte 类型的 Param.
        ///  <para>转换失败时返回0</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public Param<byte> ToByte(byte min, byte max)
        {
            return ToByte(0, min, max);
        }

        /// <summary>
        /// 转换为 byte 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<byte> ToByte(byte defaultValue, byte min, byte max)
        {
            var param = Create<byte>();
            if (param.State != ParamState.Valid)
            {
                param.Value = defaultValue;
                return param;
            }

            byte result;
            if (!byte.TryParse(Value, out result))
            {
                param.State = ParamState.ParseError;
                param.Value = defaultValue;
                return param;
            }

            //if ((result < min && min > byte.MinValue) || (result > max && max < byte.MaxValue))
            if ((result < min) || (result > max))
            {
                param.Value = defaultValue;
                param.State = ParamState.OutOfRange;
                return param;
            }

            //获得正确的值
            param.Value = result;
            return param;
        }

        #endregion

        #region ToShort

        /// <summary>
        /// 转换为 short 类型的 Param
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<short> ToShort()
        {
            return ToShort(0);
        }

        /// <summary>
        /// 转换为 short 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<short> ToShort(short defaultValue)
        {
            return ToShort(defaultValue, short.MinValue, short.MaxValue);
        }

        /// <summary>
        /// 转换为 short 类型的 Param
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<short> ToShort(short min, short max)
        {
            return ToShort(0, min, max);
        }

        /// <summary>
        /// 转换为 short 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<short> ToShort(short defaultValue, short min, short max)
        {
            var param = Create<short>();
            if (param.State != ParamState.Valid)
            {
                param.Value = defaultValue;
                return param;
            }

            short result;
            if (!short.TryParse(Value, out result))
            {
                param.State = ParamState.ParseError;
                param.Value = defaultValue;
                return param;
            }

            if ((result < min) || (result > max))
            {
                param.Value = defaultValue;
                param.State = ParamState.OutOfRange;
                return param;
            }

            param.Value = result; //获得正确的值
            return param;
        }

        #endregion

        #region ToInt32

        /// <summary>
        /// 转换为 int 类型的 Param.
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<int> ToInt32()
        {
            return ToInt32(0);
        }

        /// <summary>
        /// 转换为 int 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<int> ToInt32(int defaultValue)
        {
            return ToInt32(defaultValue, int.MinValue, int.MaxValue);
        }

        /// <summary>
        /// 转换为 int 类型的 Param.
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<int> ToInt32(int min, int max)
        {
            return ToInt32(0, min, max);
        }

        /// <summary>
        /// 转换为 int 类型的 Param.
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<int> ToInt32(int defaultValue, int min, int max)
        {
            var param = Create<int>();
            if (param.State != ParamState.Valid)
            {
                param.Value = defaultValue;
                return param;
            }

            int result;
            if (!int.TryParse(Value, out result))
            {
                param.State = ParamState.ParseError;
                param.Value = defaultValue;
                return param;
            }

            if ((result < min) || (result > max))
            {
                param.Value = defaultValue;
                param.State = ParamState.OutOfRange;
                return param;
            }

            param.Value = result; //获得正确的值
            return param;
        }

        #endregion

        #region ToLong

        /// <summary>
        /// 转换为 long 类型的 Param
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<long> ToLong()
        {
            return ToLong(0);
        }

        /// <summary>
        /// 转换为 long 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<long> ToLong(long defaultValue)
        {
            return ToLong(defaultValue, long.MinValue, long.MaxValue);
        }

        /// <summary>
        /// 转换为 long 类型的 Param
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<long> ToLong(long min, long max)
        {
            return ToLong(0, min, max);
        }

        /// <summary>
        /// 转换为 long 类型的 Param.
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<long> ToLong(long defaultValue, long min, long max)
        {
            var param = Create<long>();
            if (param.State != ParamState.Valid)
            {
                param.Value = defaultValue;
                return param;
            }

            long result;
            if (!long.TryParse(Value, out result))
            {
                param.State = ParamState.ParseError;
                param.Value = defaultValue;
                return param;
            }

            if ((result < min) || (result > max))
            {
                param.Value = defaultValue;
                param.State = ParamState.OutOfRange;
                return param;
            }

            param.Value = result; //获得正确的值
            return param;
        }

        #endregion

        #region ToDecimal

        /// <summary>
        /// 转换为 decimal 类型的 Param
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<decimal> ToDecimal()
        {
            return ToDecimal(0);
        }

        /// <summary>
        /// 转换为 decimal 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<decimal> ToDecimal(decimal defaultValue)
        {
            return ToDecimal(defaultValue, decimal.MinValue, decimal.MaxValue);
        }

        /// <summary>
        /// 转换为 decimal 类型的 Param
        /// <para>转换失败时返回0</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<decimal> ToDecimal(decimal min, decimal max)
        {
            return ToDecimal(0, min, max);
        }

        /// <summary>
        /// 转换为 decimal 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<decimal> ToDecimal(decimal defaultValue, decimal min, decimal max)
        {
            var param = Create<decimal>();
            if (param.State != ParamState.Valid)
            {
                param.Value = defaultValue;
                return param;
            }

            decimal result;
            if (!decimal.TryParse(Value, out result))
            {
                param.State = ParamState.ParseError;
                param.Value = defaultValue;
                return param;
            }

            if ((result < min) || (result > max))
            {
                param.Value = defaultValue;
                param.State = ParamState.OutOfRange;
                return param;
            }

            param.Value = result; //获得正确的值
            return param;
        }

        #endregion

        #region ToDateTime

        /// <summary>
        /// 转换为 DateTime 类型的 Param
        /// <para>转换失败时返回一个当前时间</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<DateTime?> ToDateTime()
        {
            return ToDateTime(DateTime.Now);
        }

        /// <summary>
        /// 转换为 DateTime 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<DateTime?> ToDateTime(DateTime defaultValue)
        {
            return ToDateTime(defaultValue, DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// 转换为 DateTime 类型的 Param
        /// <para>转换失败时返回一个当前时间</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<DateTime?> ToDateTime(DateTime min, DateTime max)
        {
            return ToDateTime(DateTime.Now, min, max);
        }

        /// <summary>
        /// 转换为 DateTime 类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>转换结果</returns>
        public Param<DateTime?> ToDateTime(DateTime? defaultValue, DateTime min, DateTime max)
        {
            //var param = Create<DateTime>();
            //DateTime result;
            //if (!DateTime.TryParse(Value, out result))
            //{
            //    param.State = ParamState.ParseError;
            //    param.Value = defaultValue;
            //    return param;
            //}
            //param.Value = result;
            //if (min > DateTime.MinValue && result < min)
            //    param.State = ParamState.OutOfRange;
            //if (max < DateTime.MaxValue && result > max)
            //    param.State = ParamState.OutOfRange;

            //return param;


            //新写法
            var param = Create<DateTime?>();
            if (param.State != ParamState.Valid)
            {
                param.Value = defaultValue;
                return param;
            }

            DateTime result;
            if (!DateTime.TryParse(Value, out result))
            {
                param.State = ParamState.ParseError;
                param.Value = defaultValue;
                return param;
            }

            if ((result < min) || (result > max))
            {
                param.Value = defaultValue;
                param.State = ParamState.OutOfRange;
                return param;
            }

            param.Value = result; //获得正确的值
            return param;
        }

        #endregion

        #region ToEnum

        /// <summary>
        /// 转换为 枚举类型的 Param
        /// <para>转换失败时将无法取得枚举值，抛出异常。</para>
        /// </summary>
        /// <remarks>
        /// 无法自动设定默认值
        /// </remarks>
        /// <typeparam name="T">指定的枚举类型</typeparam>
        /// <returns>转换结果</returns>
        public Param<T> ToEnum<T>()
        {
            var param = Create<T>();
            if (param.State != ParamState.Valid)
            {
                return param;
            }

            try
            {
                param.Value = (T) Enum.Parse(typeof (T), Value, true);
            }
            catch
            {
                param.State = ParamState.ParseError;
            }

            return param;
        }

        /// <summary>
        /// 转换为 枚举类型的 Param
        /// <para>转换失败时返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <typeparam name="T">指定的枚举类型</typeparam>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<T> ToEnum<T>(T defaultValue)
        {
            var param = Create<T>();
            if (param.State != ParamState.Valid)
            {
                param.Value = defaultValue;
                return param;
            }

            try
            {
                param.Value = (T) Enum.Parse(typeof (T), Value, true);
            }
            catch
            {
                param.State = ParamState.ParseError;
                param.Value = defaultValue;
            }

            return param;
        }

        #endregion

        #region ToId , ToLongId  均为大于0的正整数

        /// <summary>
        /// 转换为 Id 类型的 Param, 即 Id &gt; 0
        /// <para>Note:转换失败时将无法获取值，最大值:2147483647   。</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<int> ToId()
        {
            var param = Create<int>();
            if (param.State != ParamState.Valid)
                return param;

            int result;
            if (!int.TryParse(Value, out result))
            {
                param.State = ParamState.ParseError;
                return param;
            }

            if (result < 1)
            {
                param.State = ParamState.OutOfRange;
                return param;
            }

            param.Value = result;
            return param;
        }

        /// <summary>
        /// 转换为 Id 类型的 Param, 即 Id &gt; 0
        /// <para>Note:转换失败时将无法获取值，最大值:79228162514264337593543950335 (128bit，29位数)  。</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<decimal> ToLongId()
        {
            var param = Create<decimal>();
            if (param.State != ParamState.Valid)
                return param;

            decimal result;
            if (!decimal.TryParse(Value, out result))
            {
                param.State = ParamState.ParseError;
                return param;
            }

            if (result < 1)
            {
                param.State = ParamState.OutOfRange;
                return param;
            }

            param.Value = result;
            return param;
        }

        #endregion

        #region ToGuid

        /// <summary>
        /// 转换为 Guid 类型的 Param
        /// <para>转换失败时返回一个NewGuild</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<Guid> ToGuid()
        {
            var param = Create<Guid>();
            try
            {
                param.Value = new Guid(Value);
            }
            catch
            {
                param.Value = Guid.NewGuid();
                param.State = ParamState.ParseError;
            }
            return param;
        }

        #endregion

        #region ToIntArray & ToIntList

        /// <summary>
        /// 转换为 int[] 类型的 Param, , 使用 , 作为默认的分割
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<int[]> ToIntArray()
        {
            return ToIntArray(",");
        }

        /// <summary>
        /// 转换为 int[] 类型的 Param
        /// </summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <returns>转换结果</returns>
        public Param<int[]> ToIntArray(string separator)
        {
            return ToIntArray(separator, null);
        }

        /// <summary>
        /// 转换为 int[] 类型的 Param
        /// </summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <param name="action">委托，用以判断元素是否符合要求</param>
        /// <returns>转换结果</returns>
        public Param<int[]> ToIntArray(string separator, Predicate<int> action)
        {
            var param = Create<int[]>();
            var result = ToIntList(separator, action);

            param.State = result.State;
            if (param.State == ParamState.Valid)
                param.Value = result.Value.ToArray();
            return param;
        }

        /// <summary>
        /// 转换为泛型 List[int] 类型的 Param, 使用<b>,</b>作为默认的分割
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<List<int>> ToIntList()
        {
            return ToIntList(",");
        }

        /// <summary>
        ///  转换为泛型 List[int] 类型的 Param
        /// </summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <returns>转换结果</returns>
        public Param<List<int>> ToIntList(string separator)
        {
            return ToIntList(separator, null);
        }

        /// <summary>
        ///  转换为泛型 List[int] 类型的 Param
        /// <para>转换失败或结果为空Empty时，无法取得Value的值，此时取值将抛出异常。</para>
        /// </summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <param name="action">委托，用以判断元素是否符合要求</param>
        /// <returns>转换结果</returns>
        public Param<List<int>> ToIntList(string separator, Predicate<int> action)
        {
            var param = Create<List<int>>();
            if (param.State != ParamState.Valid)
                return param;

            if (!Valid.IdStringValidator(Value))
            {
                param.State = ParamState.ParseError;
                return param;
            }

            var list = new List<int>();
            var ids = Value.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries); //移去空元素

            if (action != null)
            {
                for (var i = 0; i < ids.Length; i++)
                {
                    int result;
                    if (!int.TryParse(ids[i], out result)) continue;
                    if (action(result))
                        list.Add(result);
                }
            }
            else
            {
                for (var i = 0; i < ids.Length; i++)
                {
                    int result;
                    if (int.TryParse(ids[i], out result))
                        list.Add(result);
                }
            }

            if (list.Count == 0)
            {
                param.State = ParamState.Empty;
                return param;
            }

            param.Value = list;
            return param;
        }

        #endregion

        #region ToIdArray & ToIdList (Id) > 0

        /// <summary>
        /// 转换为 int[] 类型的 Param, 即 Id > 0,  使用<b>,</b>作为默认的分割
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<int[]> ToIdArray()
        {
            return ToIdArray(",");
        }

        /// <summary>
        /// 转换为 int[] 类型的 Param, 即 Id > 0
        /// </summary>
        /// <param name="separator">分割字符</param>
        /// <returns>转换结果</returns>
        public Param<int[]> ToIdArray(string separator)
        {
            var param = Create<int[]>();
            var result = ToIntList(separator, x => x > 0);

            param.State = result.State;
            if (param.State == ParamState.Valid)
                param.Value = result.Value.ToArray();
            return param;
        }


        ///<summary>
        /// 对含有,的可分割数字字符串数值自动求和
        ///</summary>
        ///<returns>求合的结果</returns>
        public Param<int> SumIntArray()
        {
            return SumIntArray(",");
        }

        /// <summary>
        /// 可分割数字字符串数值自动求和
        /// <para>状态一定为Valid，求合失败时直接返回结果0</para>
        /// </summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <returns>求合的结果</returns>
        public Param<int> SumIntArray(string separator)
        {
            var param = Create<int>();
            var result = ToIntList(separator);
            param.State = result.State;
            if (param.State == ParamState.Valid)
            {
                var rst = result.Value.ToArray();
                var sum = 0;
                foreach (var t in rst)
                {
                    sum += t;
                }
                param.Value = sum;
            }
            else
            {
                param.State = ParamState.Valid;
                param.Value = 0;
            }
            return param;
        }

        /// <summary>
        /// 转换为泛型 List[int] 类型的 Param, 即 Id > 0, 使用<b>,</b>作为默认的分割
        /// </summary>
        /// <returns></returns>
        public Param<List<int>> ToIdList()
        {
            return ToIdList(",");
        }

        /// <summary>
        /// 转换为泛型 List[int] 类型的 Param, 即 Id > 0
        /// </summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <returns>转换结果</returns>
        public Param<List<int>> ToIdList(string separator)
        {
            return ToIntList(separator, x => x > 0);
        }

        #endregion

        #region ToPositiveIntArray & ToPositiveIntList  0 ~ int32.MaxValue 之间的数值集合

        /// <summary>
        /// 不小于0 ,  使用<b>,</b>作为默认的分割
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<int[]> ToPositiveArray()
        {
            return ToPositiveArray(",");
        }

        /// <summary>
        /// 不小于0
        /// </summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <returns>转换结果</returns>
        public Param<int[]> ToPositiveArray(string separator)
        {
            var param = Create<int[]>();
            var result = ToIntList(separator, x => x >= 0);

            param.State = result.State;
            if (param.State == ParamState.Valid)
                param.Value = result.Value.ToArray();
            return param;
        }

        /// <summary>
        /// 不小于0 ,  使用<b>,</b>作为默认的分割
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<List<int>> ToPositiveList()
        {
            return ToPositiveList(",");
        }

        /// <summary>
        /// 不小于0
        /// </summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <returns>转换结果</returns>
        public Param<List<int>> ToPositiveList(string separator)
        {
            return ToIntList(separator, x => x >= 0);
        }

        #endregion

        #region ToString

        /// <summary>
        /// 转换为 string 类型的 Param
        /// <para>未通过第一次状态检测的返回null，此时无法直接获取值，会抛出异常</para>
        /// </summary>
        /// <returns>转换结果</returns>
        public new Param<string> ToString()
        {
            return ToString(null, null, null);
        }

        /// <summary>
        /// 转换为 string 类型的 Param
        /// <para>未通过第一次状态检测的返回默认值<paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<string> ToString(string defaultValue)
        {
            return ToString(defaultValue, null, null);
        }

        /// <summary>
        /// 转换为 string 类型的 Param
        /// <para>验证委托为null时直接返回原始值</para>
        /// </summary>
        /// <param name="verifyMatch">验证委托</param>
        /// <returns>转换结果</returns>
        public Param<string> ToString(Predicate<string> verifyMatch)
        {
            return ToString(null, null, verifyMatch);
        }

        /// <summary>
        /// 转换为 string 类型的 Param
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="verifyMatch">验证委托</param>
        /// <returns></returns>
        public Param<string> ToString(string defaultValue, Predicate<string> verifyMatch)
        {
            return ToString(defaultValue, null, verifyMatch);
        }

        /// <summary>
        /// 转换为 string 类型的 Param
        /// <para>不在验证范围，不符合验证时返回默认值</para>
        /// <para>如果范围或验证委托为null时将返回为原始值，即隐含意义为默认是通过全部验证的</para>
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="rangeMatch">范围委托</param>
        /// <param name="verifyMatch">验证委托</param>
        /// <returns>泛型类 Param[string]</returns>
        public Param<string> ToString(string defaultValue, Predicate<string> rangeMatch, Predicate<string> verifyMatch)
        {
            var param = Create<string>();


            if (param.State != ParamState.Valid)
            {
                if (defaultValue != null)
                {
                    param.Value = defaultValue;
                }
                return param;
            }

            if (rangeMatch != null && !rangeMatch(Value))
            {
                param.State = ParamState.OutOfRange;
                param.Value = defaultValue;
                return param;
            }

            if (verifyMatch != null && !verifyMatch(Value))
            {
                param.State = ParamState.InValid;
                param.Value = defaultValue;
                return param;
            }

            param.Value = Value;
            return param;
        }

        /// <summary>
        /// 转换为 string 类型的 Param
        /// </summary>
        /// <param name="min">字符串最小长度</param>
        /// <param name="max">字符串最大长度</param>
        /// <returns>转换结果</returns>
        public Param<string> ToString(int min, int max)
        {
            return ToString(null, min, max, null);
        }

        /// <summary>
        /// 转换为 string 类型的 Param.
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">字符串最小长度</param>
        /// <param name="max">字符串最大长度</param>
        /// <returns>转换结果</returns>
        public Param<string> ToString(string defaultValue, int min, int max)
        {
            return ToString(defaultValue, min, max, null);
        }

        /// <summary>
        /// 转换为 string 类型的 Param
        /// </summary>
        /// <param name="min">字符串最小长度</param>
        /// <param name="max">字符串最大长度</param>
        /// <param name="verifyMatch">验证委托</param>
        /// <returns>转换结果</returns>
        public Param<string> ToString(int min, int max, Predicate<string> verifyMatch)
        {
            return ToString(null, min, max, verifyMatch);
        }

        /// <summary>
        /// 转换为 string 类型的 Param
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">字符串最小长度</param>
        /// <param name="max">字符串最大长度</param>
        /// <param name="verifyMatch">验证委托</param>
        /// <returns>转换结果</returns>
        public Param<string> ToString(string defaultValue, int min, int max, Predicate<string> verifyMatch)
        {
            return ToString(defaultValue,
                //delegate(string x)
                //{
                //    if (min > int.MinValue && x.Length < min)
                //        return false;

                //    return max >= int.MaxValue || x.Length <= max;
                //}
                x => x.Length >= min && x.Length <= max
                , verifyMatch
                );
        }

        #endregion

        #region ToSqlSafeString 不推荐使用，最好是使用直接传参的方式

        /// <summary>
        /// 转为 Sql 安全类型.
        /// </summary>
        /// <param name="param">string 类型的 Param</param>
        private static void ToSqlSafeString(ParamBase<ParamState, string> param)
        {
            if (!String.IsNullOrEmpty(param.Value))
            {
                param.Value = param.Value.Replace("'", "''");
            }
        }

        /// <summary>
        /// 转换为 Sql 安全的 string 类型的 Param
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<string> ToSqlSafeString()
        {
            var param = ToString();
            ToSqlSafeString(param);
            return param;
        }

        /// <summary>
        /// 转换为 Sql 安全的 string 类型的 Param
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<string> ToSqlSafeString(string defaultValue)
        {
            var param = ToString(defaultValue);
            ToSqlSafeString(param);
            return param;
        }

        /// <summary>
        /// 转换为 Sql 安全的 string 类型的 Param
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小长度</param>
        /// <param name="max">最大长度</param>
        /// <returns>转换结果</returns>
        public Param<string> ToSqlSafeString(string defaultValue, int min, int max)
        {
            var param = ToString(defaultValue, min, max);
            ToSqlSafeString(param);
            return param;
        }

        #endregion

        #region ToXmlSafeString

        /// <summary>
        /// 转换为 Xml 安全的 string 类型的 Param
        /// </summary>
        /// <param name="param">string 类型的 HttpParam</param>
        private static void ToXmlSafeString(ParamBase<ParamState, string> param)
        {
            if (param.State == ParamState.Valid || param.State == ParamState.OutOfRange)
            {
                param.Value = XmlCharReplacePattern.Replace(param.Value, String.Empty);
            }
        }

        /// <summary>
        /// 转换为 Xml 安全的 string 类型的 Param
        /// </summary>
        /// <returns>转换结果</returns>
        public Param<string> ToXmlSafeString()
        {
            var param = ToString();
            ToXmlSafeString(param);
            return param;
        }

        /// <summary>
        /// 转换为 Xml 安全的 string 类型的 Param
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<string> ToXmlSafeString(string defaultValue)
        {
            var param = ToString(defaultValue);
            ToXmlSafeString(param);
            return param;
        }

        ///<summary>
        /// 转换为 Xml 安全的 string 类型的 Param.
        ///</summary>
        /// <param name="min">最小长度</param>
        /// <param name="max">最大长度</param>
        ///<returns>返回值</returns>
        public Param<string> ToXmlSafeString(int min, int max)
        {
            return ToXmlSafeString(null, min, max);
        }

        /// <summary>
        /// 转换为 Xml 安全的 string 类型的 Param.
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小长度</param>
        /// <param name="max">最大长度</param>
        /// <returns>转换结果</returns>
        public Param<string> ToXmlSafeString(string defaultValue, int min, int max)
        {
            var param = ToString(defaultValue, min, max);
            ToXmlSafeString(param);
            return param;
        }

        #endregion

        #region ToSafeString Sql与XML的安全

        /// <summary>
        /// 转换为 安全的 string 类型的 Param
        /// </summary>
        /// <param name="param">string 类型的 HttpParam</param>
        private static void ToSafeString(ParamBase<ParamState, string> param)
        {
            if (param.State != ParamState.Valid && param.State != ParamState.OutOfRange) return;
            ToSqlSafeString(param);
            ToXmlSafeString(param);
        }

        /// <summary>
        /// 转换为 安全的 string 类型的 Param
        /// </summary>
        public Param<string> ToSafeString()
        {
            var param = ToString();
            ToSafeString(param);
            return param;
        }

        /// <summary>
        /// 转换为 安全的 string 类型的 Param
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换结果</returns>
        public Param<string> ToSafeString(string defaultValue)
        {
            var param = ToString(defaultValue);
            ToSafeString(param);
            return param;
        }

        /// <summary>
        /// 转换为 安全的 string 类型的 Param
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="min">最小长度</param>
        /// <param name="max">最大长度</param>
        /// <returns>转换结果</returns>
        public Param<string> ToSafeString(string defaultValue, int min, int max)
        {
            var param = ToString(defaultValue, min, max);
            ToSafeString(param);
            return param;
        }

        #endregion

        #region ToStringList

        ///<summary>
        /// 转为字符串数组
        ///</summary>
        ///<returns>符串数组组成的Param</returns>
        public Param<string[]> ToStringArray()
        {
            return ToStringArray(",", null);
        }

        ///<summary>
        /// 转为字符串数组
        ///</summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <param name="action">委托，用以判断元素是否符合要求</param>
        ///<returns>符串数组组成的Param</returns>
        public Param<string[]> ToStringArray(string separator, Predicate<string> action)
        {
            var param = Create<string[]>();
            var result = ToStringList(separator, action);

            param.State = result.State;
            if (param.State == ParamState.Valid)
                param.Value = result.Value.ToArray();
            return param;
        }

        ///<summary>
        /// 转为List&lt;string&gt;的Param
        ///</summary>
        ///<returns>List字符串成的Param</returns>
        public Param<List<string>> ToStringList()
        {
            return ToStringList(",", null);
        }

        ///<summary>
        /// 转为List&lt;string&gt;的Param
        /// <para>若转换失败则不能获得值，直接获取会抛出异常</para>
        ///</summary>
        /// <param name="separator">分割字符组合串，如:,.$，每一个字符元素都将做为可用的分割符</param>
        /// <param name="action">委托，用以判断元素是否符合要求</param>
        ///<returns>换转后的结果Param对象</returns>
        public Param<List<string>> ToStringList(string separator, Predicate<string> action)
        {
            var param = Create<List<string>>();
            if (param.State != ParamState.Valid)
                return param;

            var list = new List<string>();
            var ids = Value.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (action != null)
            {
                for (var i = 0; i < ids.Length; i++)
                {
                    if (action(ids[i]))
                        list.Add(ids[i]);
                }
            }
            else
            {
                list.AddRange(ids);
            }

            if (list.Count == 0)
            {
                param.State = ParamState.Empty;
                return param;
            }

            param.Value = list;
            return param;
        }

        #endregion

        #endregion
    }
}