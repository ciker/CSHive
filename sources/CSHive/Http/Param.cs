namespace CS.Http
{
    /// <summary>
    ///   参数处理
    ///   参数处理模块:Ver:0.10
    /// </summary>
    /// 
    /// <description class = "CS.Param">
    ///  键值对的参数包装
    /// </description>
    /// 
    /// <history>
    ///   2010-9-19 18:13:18 , zhouyu ,  创建	     
    ///  </history>
    /// <typeparam name="TValueType">结果参数的值的类型</typeparam>
    public class Param<TValueType> : ParamBase<ParamState, TValueType>
    {
        #region Constructor

        ///<summary>
        /// 默认构造
        ///</summary>
        public Param()
        {
        }

        /// <summary>
        /// 使用指定的 value 初始化 Param 对象
        /// </summary>
        /// <param name="value">指定值</param>
        public Param(TValueType value) : base(value)
        {
        }

        /// <summary>
        /// 使用指定的 key, value 初始化 Param 对象.
        /// </summary>
        /// <param name="key">指定键</param>
        /// <param name="value">指定值</param>
        public Param(string key, TValueType value)
            : base(value)
        {
            Key = key;
        }

        #endregion

        #region Properies

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        ///<summary>
        /// 是否为回传参数,标识Param对象是否做为回传至客户端使用(要手动附加至相关对象上,此处仅标记一下该对象)
        ///</summary>
        public bool IsParam { get; set; }

        ///// <summary>
        ///// 对父类State的重写
        ///// <para>通过该状态更新时如果可更新为通过验证的</para>
        ///// </summary>
        //public override ParamState State
        //{
        //    get
        //    {
        //        return base.State;
        //    }
        //    set
        //    {
        //        if (value == ParamState.Valid)
        ////            PassedVerify = true;

        //        base.State = value;
        //    }
        //}

        #endregion

        #region Valid 手动构造一个通过验证的Param对象

        ///<summary>
        /// 手动构造一个新的通过验证的仅有值的Param对象 [不是回传的参数]
        ///</summary>
        ///<param name="value">参数的值</param>
        ///<returns>通过验证的Param对象</returns>
        public static Param<TValueType> Valid(TValueType value)
        {
            return Valid(null, value);
        }

        ///<summary>
        /// 手动构造一个新的通过验证的Param对象 [不是回传的参数]
        ///</summary>
        ///<param name="key">参数的键</param>
        ///<param name="value">参数的值</param>
        ///<returns>通过验证的Param对象</returns>
        public static Param<TValueType> Valid(string key, TValueType value)
        {
            return Valid(key, value, false);
        }

        ///<summary>
        /// 手动构造一个新的,可直接取值[有效合法的Param对象]
        ///</summary>
        ///<param name="key">键</param>
        ///<param name="value">值</param>
        ///<param name="isParam">是否回传</param>
        ///<returns>通过验证的Param对象</returns>
        public static Param<TValueType> Valid(string key, TValueType value, bool isParam)
        {
            var result = new Param<TValueType>(key, value) {ProtState = ParamState.Valid, IsParam = isParam};
            return result;
        }

        /// <summary>
        /// 将当前对象转换为 Param&lt;string&gt; 对象。
        /// string类型时无作用，非string类型时才显现
        /// </summary>
        /// <returns>返回转换后的 Param&lt;string&gt; 对象</returns>
        public Param<string> ToStringParam()
        {
            var result = new Param<string> {Key = Key, ProtState = State, Value = Value.ToString()};
            return result;
        }

        /// <summary>
        /// 设置为Param对象为回传类型并返回该Param对象
        /// <para>例如:将本身附加至HttpContext.Current.Params上，以备回传至客户端之用</para>
        /// </summary>
        public Param<TValueType> WithParam()
        {
            IsParam = true;
            return this;
        }

        #endregion

        #region implicit & explicit 转换

        /// <summary>
        /// 隐式转换 值为 Param 对象 [可以得到未经转化的值]
        /// <example> Param &lt;string&gt; p = ("this is a test");</example>
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>返回转换后的 Param 对象</returns>
        public static implicit operator Param<TValueType>(TValueType value)
        {
            return new Param<TValueType>(value);
        }

        /// <summary>
        /// 显示转换 Param 对象为其值
        /// </summary>
        /// <param name="value">Param 对象</param>
        /// <returns>Param的值</returns>
        public static explicit operator TValueType(Param<TValueType> value)
        {
            return value.Value;
        }

        #endregion
    }

    #region ParamState Enum

    /// <summary>
    /// Param 转换的状态
    /// </summary>
    public enum ParamState
    {
        /// <summary>
        /// 未经过检测与转换的 ，初始的未指向的 (null) 。
        /// </summary>
        Default,

        /// <summary>
        /// 空 ""
        /// </summary>
        Empty,

        ///// <summary>
        ///// 有值，可以进行ToX()进行转换
        ///// </summary>
        //HasValue,

        /// <summary>
        /// 转换出错, 类型错误
        /// </summary>
        ParseError,

        /// <summary>
        /// 超过范围
        /// </summary>
        OutOfRange,

        /// <summary>
        /// 无效的，非法的，验证失败的
        /// </summary>
        InValid,

        /// <summary>
        /// 有效，Note:默认是通过验证(此时的意义为字符串不为空)的。通过ToX()方法后Valid是指是否符合该约束的验证。
        /// </summary>
        Valid
    }

    #endregion
}