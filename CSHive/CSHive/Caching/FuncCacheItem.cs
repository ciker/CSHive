using System;
using System.Runtime.InteropServices;

namespace CS.Caching
{
    /// <summary>
    /// 自动调用方法更新缓存项
    /// </summary>
    public class FuncCacheItem<TV> : CacheItem<TV> where TV : class
    {
        /// <summary>
        /// 初始化缓存项
        /// </summary>
        /// <param name="function"></param>
        /// <param name="value">可能的话给出初始的值</param>
        public FuncCacheItem(Func<TV> function, TV value = null)
        {
            base.Item = value;
            FuncCallback = function;
        }

        /// <summary>
        /// 当值过期时取值的方法
        /// <remarks>
        /// 该方法返回的Item项目要更新过期时间
        /// </remarks>
        /// </summary>
        public Func<TV> FuncCallback { get; set; }

        /// <summary>
        /// 返回值，如有可能自动更新值
        /// </summary>
        public override TV Item
        {
            get
            {
                if (FuncCallback == null) throw new NullReferenceException("Please use set FuncCacheItem.FuncCallback init.");
                if (base.Item == null || Expired)
                {
                    base.Item = FuncCallback.Invoke();
                }
                return base.Item;
            }
        }

    }
}