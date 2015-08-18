using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Web.Script.Serialization;

namespace CS.Serialization
{
    /// <summary>
    /// dynamic Json object
    /// <remarks>有待改进</remarks>
    /// <code>
    ///  var serializer = new JavaScriptSerializer();
    /// serializer.RegisterConverters(new[] { new DynamicJsonConverter() }); //注册自定义的转换器
    ///  var jsonString =  serializer.Deserialize(responce, typeof(object));
    /// </code>
    /// </summary>
    public sealed class DynamicJsonConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            //Define the ListItemCollection as a supported type.
            get { return new ReadOnlyCollection<Type>(new List<Type>(new[] {typeof (object)})); }
        }

        /// <summary>
        /// 重写反序列化
        /// </summary>
        /// <returns>dynamic Json 对象</returns>
        public override object Deserialize(IDictionary<string, object> dictionary, Type type,
            JavaScriptSerializer serializer)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
            return type == typeof (object) ? new DynamicJsonObject(dictionary) : null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
            //var dic = obj as Dictionary<string, object>;
            //if (dic != null)
            //{
            //    return dic;
            //}
            //return new Dictionary<string, object>();
        }

        /// <summary>
        /// 自定义动态Json对象
        /// </summary>
        private sealed class DynamicJsonObject : DynamicObject
        {
            private readonly IDictionary<string, object> _dictionary;

            /// <summary>
            /// 初始化仅是修正字典中Key里的非法字符
            /// </summary>
            /// <param name="dictionary"></param>
            public DynamicJsonObject(IDictionary<string, object> dictionary)
            {
                if (dictionary == null) throw new ArgumentNullException(dictionary.ToString());
                //_dictionary = FixInvalidIndentifiers(dictionary);//针对性修正
                _dictionary = dictionary;
            }

            /// <summary>
            /// 取值
            /// </summary>
            /// <returns></returns>
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                //找不到键的值时为null(以免抛出异常，在用到可能会不存在的属性时要注意检查值是否为null)
                if (!_dictionary.TryGetValue(binder.Name, out result))
                {
                    result = null;
                    return true;
                }
                //如果在上面的字典中找到值那么尝试转为字典类型
                var dictionary = result as IDictionary<string, object>;
                if (dictionary != null)
                {
                    result = new DynamicJsonObject(dictionary); //将结果进行递归
                    return true;
                }
                //不能转为字典时，将结果转为数组(如果数组中的第一个元素是字典型那就进行递归，不是时转为对像数组)
                var arrayList = result as ArrayList;
                if (arrayList != null && arrayList.Count > 0)
                {
                    if (arrayList[0] is IDictionary<string, object>)
                        result =
                            new List<object>(
                                arrayList.Cast<IDictionary<string, object>>().Select(x => new DynamicJsonObject(x)));
                    else
                        result = new List<object>(arrayList.Cast<object>());
                }
                //不能转为数组时将结果result 直接返回
                return true;
            }

            /// <summary>
            /// 动态设定值
            /// </summary>
            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                _dictionary[binder.Name] = value;
                return true;
            }

            /// <summary>
            /// 修正非法字符"-","#"
            /// </summary>
            /// <param name="dictionary"></param>
            /// <returns></returns>
            [Obsolete("具有针对性")]
            private static IDictionary<string, object> FixInvalidIndentifiers(
                IEnumerable<KeyValuePair<string, object>> dictionary)
            {
                char[] invalidIdentifiers = {'-', '#'};
                IDictionary<string, object> tempDictionary = new Dictionary<string, object>();
                foreach (var keyValuePair in dictionary)
                {
                    var key = invalidIdentifiers.Aggregate(keyValuePair.Key,
                        (current, @char) => current.Replace(@char, '_'));
                    tempDictionary.Add(key, keyValuePair.Value);
                }
                return tempDictionary;
            }
        }
    }
}