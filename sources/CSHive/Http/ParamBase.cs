using System;

namespace CS.Http
{
    /// <summary>
    ///   参数处理抽象类
    ///   参数处理模块:Ver:0.10
    /// </summary>
    /// 
    /// <description class = "CS.Param.ParamBase">
    ///   对参数的再包装,包装后的参数,可以有状态和转换后的值
    /// </description>
    /// 
    /// <history>
    ///   2010-3-3 18:09:49 , zhouyu ,  创建	 
    ///  </history>
    /// <typeparam name="TState">参数状态</typeparam>
    /// <typeparam name="TV">参数的目标(期望)值类型</typeparam>
    public abstract class ParamBase<TState, TV>
    {
        /// <summary>
        /// 受保护的状态
        /// </summary>
        protected TState ProtState;

        /// <summary>
        /// 受保护的值
        /// </summary>
        protected TV ProtValue;

        ///<summary>
        ///</summary>
        protected ParamBase()
        {
        }

        /// <summary>
        /// 根据指定 Value 初始化 Param 对象.
        /// </summary>
        /// <param name="value"></param>
        protected ParamBase(TV value)
        {
            HasValue = true;
            ProtValue = value;
        }

        /// <summary>
        /// 是否有值
        /// </summary>
        public bool HasValue { get; private set; }

        /// <summary>
        /// 当前参数的状态
        /// </summary>
        public virtual TState State
        {
            get { return ProtState; }
            set { ProtState = value; }
        }

        /// <summary>
        /// 获取经过验证的值
        /// <para>get 获取值，未设定值时抛出InvalidOperationException异常。</para>
        /// <para>set 设定参数对象值，不返回异常。</para>
        /// </summary>
        /// <exception cref="InvalidOperationException">枚举转换失败时该异常，该异常应由开发人员避免。无效的的值不可能直接取出。</exception>
        public TV Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException("未设定Value的值(可能是没有获取正确的值)，不可以直接获取该值(如有需要请使用GetValue()方法)。");
                return ProtValue;
            }
            set
            {
                ProtValue = value;
                HasValue = true;
            }
        }

        /// <summary>
        /// 获取值(不抛异常)
        /// </summary>
        /// <returns>返回值</returns>
        public TV GetValue()
        {
            return ProtValue;
        }

        /// <summary>
        /// 获取值(不抛异常), 如果没有值, 就返回默认值
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回值</returns>
        public TV GetValue(TV defaultValue)
        {
            return !HasValue ? defaultValue : ProtValue;
        }

        #region object对象的相关方法重写

        ///<summary>
        ///</summary>
        ///<param name="other"></param>
        ///<returns></returns>
        public override bool Equals(object other)
        {
            if (!HasValue)
            {
                return (other == null);
            }
            return other != null && ProtValue.Equals(other);
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public override int GetHashCode()
        {
            return !HasValue ? 0 : ProtValue.GetHashCode();
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public override string ToString()
        {
            return !HasValue ? "" : ProtValue.ToString();
        }

        #endregion
    }
}