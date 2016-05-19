/*----------------------------------------------------------------
    原项目地址：http://conditions.codeplex.com/
    文件功能描述：  参数约束检查
    

----------------------------------------------------------------*/


using System.ComponentModel;
using System.Diagnostics;

namespace CS.Conditions
{
    /// <summary>
    /// Enables validation of pre- and postconditions. This class isn't used directly by developers. Instead 
    /// the class should be created by the <see cref="ConditionValidator{T}">Requires</see> and
    /// <see cref="Condition.Ensures{T}(T)">Ensures</see> extension methods.
    /// </summary>
    /// <typeparam name="T">The type of the argument to be validated</typeparam>
    /// <example>
    /// The following example shows how to use <b>CuttingEdge.Conditions</b>.
    /// <code><![CDATA[
    /// using System.Collections;
    /// 
    /// using CuttingEdge.Conditions;
    /// 
    /// public class ExampleClass
    /// {
    ///     private enum StateType { Uninitialized = 0, Initialized };
    ///     
    ///     private StateType currentState;
    /// 
    ///     public ICollection GetData(int? id, string xml, IEnumerable col)
    ///     {
    ///         // Check all preconditions:
    ///         Condition.Requires(id, "id")
    ///             .IsNotNull()          // throws ArgumentNullException on failure
    ///             .IsInRange(1, 999)    // ArgumentOutOfRangeException on failure
    ///             .IsNotEqualTo(128);   // throws ArgumentException on failure
    /// 
    ///         Condition.Requires(xml, "xml")
    ///             .StartsWith("<data>") // throws ArgumentException on failure
    ///             .EndsWith("</data>"); // throws ArgumentException on failure
    /// 
    ///         Condition.Requires(col, "col")
    ///             .IsNotNull()          // throws ArgumentNullException on failure
    ///             .IsEmpty();           // throws ArgumentException on failure
    /// 
    ///         // Do some work
    /// 
    ///         // Example: Call a method that should return a not null ICollection
    ///         object result = BuildResults(xml, col);
    /// 
    ///         // Check all postconditions:
    ///         // A PostconditionException will be thrown at failure.
    ///         Condition.Ensures(result, "result")
    ///             .IsNotNull()
    ///             .IsOfType(typeof(ICollection));
    /// 
    ///         return result as ICollection;
    ///     }
    /// }
    /// ]]></code>
    /// The following code examples shows how to extend the library with your own 'Invariant' entry point
    /// method. The first example shows a class with an Add method that validates the class state (the
    /// class invariants) before adding the <b>Person</b> object to the internal array and that code should
    /// throw an <see cref="InvalidOperationException"/>.
    /// <code><![CDATA[
    /// using CuttingEdge.Conditions;
    /// 
    /// public class Person { }
    /// 
    /// public class PersonCollection 
    /// {
    ///     public PersonCollection(int capicity)
    ///     {
    ///         this.Capacity = capicity;
    ///     }
    /// 
    ///     public void Add(Person person)
    ///     {
    ///         // Throws a ArgumentNullException when person == null
    ///         Condition.Requires(person, "person").IsNotNull();
    ///         
    ///         // Throws an InvalidOperationException on failure
    ///         Invariants.Invariant(this.Count, "Count").IsLessOrEqual(this.Capacity);
    ///         
    ///         this.AddInternal(person);
    ///     }
    ///
    ///     public int Count { get; private set; }
    ///     public int Capacity { get; private set; }
    ///     
    ///     private void AddInternal(Person person)
    ///     {
    ///         // some logic here
    ///     }
    ///     
    ///     public bool Contains(Person person)
    ///     {
    ///         // some logic here
    ///         return false;
    ///     }
    /// }
    /// ]]></code>
    /// The following code example will show the implementation of the <b>Invariants</b> class.
    /// <code><![CDATA[
    /// using System;
    /// using CuttingEdge.Conditions;
    /// 
    /// namespace MyCompanyRootNamespace
    /// {
    ///     public static class Invariants
    ///     {
    ///         public static ConditionValidator<T> Invariant<T>(T value)
    ///         {
    ///             return new InvariantValidator<T>("value", value);
    ///         }
    /// 
    ///         public static ConditionValidator<T> Invariant<T>(T value, string argumentName)
    ///         {
    ///             return new InvariantValidator<T>(argumentName, value);
    ///         }
    /// 
    ///         // Internal class that inherits from ConditionValidator<T>
    ///         sealed class InvariantValidator<T> : ConditionValidator<T>
    ///         {
    ///             public InvariantValidator(string argumentName, T value)
    ///                 : base(argumentName, value)
    ///             {
    ///             }
    /// 
    ///             protected override void ThrowExceptionCore(string condition,
    ///                 string additionalMessage, ConstraintViolationType type)
    ///             {
    ///                 string exceptionMessage = string.Format("Invariant '{0}' failed.", condition);
    /// 
    ///                 if (!String.IsNullOrEmpty(additionalMessage))
    ///                 {
    ///                     exceptionMessage += " " + additionalMessage;
    ///                 }
    /// 
    ///                 // Optionally, the 'type' parameter can be used, but never throw an exception
    ///                 // when the value of 'type' is unknown or unvalid.
    ///                 throw new InvalidOperationException(exceptionMessage);
    ///             }
    ///         }
    ///     }
    /// }
    /// ]]></code>
    /// </example>
    [DebuggerDisplay("{GetType().Name} ( ArgumentName: {ArgumentName}, Value: {Value} )")]
    public abstract class ConditionValidator<T>
    {
        //Note:如果您已重写 ToString()，则您无需使用 DebuggerDisplay。如果同时使用这两者，则 DebuggerDisplay 属性优先于 ToString() 重写。



        /// <summary>初始化 <see cref="ConditionValidator{T}" /> 的新实例</summary>
        /// <param name="argumentName">参数名</param>
        /// <param name="value">参数值</param>
        protected ConditionValidator(string argumentName, T value)
        {
            // This constructor is internal. It is not useful for a user to inherit from this class.
            // When this ctor is made protected, so should be the BuildException method.
            Value = value;
            ArgumentName = argumentName;
        }

        /// <summary>参数名称</summary>
        [EditorBrowsable(EditorBrowsableState.Never)] // see top of page for note on this attribute.
        public string ArgumentName { get; private set; }

        /// <summary>参数的值</summary>
        [EditorBrowsable(EditorBrowsableState.Never)] // see top of page for note on this attribute.
        public readonly T Value;

        /// <summary>抛出异常</summary>
        /// <param name="condition">违反约束时的异常提示信息，如：参数不能为null</param>
        /// <param name="additionalMessage">异常附加消息，如参数值为3</param>
        /// <param name="type">异常类型</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ThrowException(string condition, string additionalMessage, ConstraintViolationType type)
        {
            ThrowExceptionCore(condition, additionalMessage, type);
        }

        /// <summary>抛出异常</summary>
        /// <param name="condition">违反约束时的异常提示信息，如：参数不能为null</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ThrowException(string condition)
        {
            ThrowExceptionCore(condition, null, ConstraintViolationType.Default);
        }

        internal void ThrowException(string condition, string additionalMessage)
        {
            ThrowExceptionCore(condition, additionalMessage, ConstraintViolationType.Default);
        }

        internal void ThrowException(string condition, ConstraintViolationType type)
        {
            ThrowExceptionCore(condition, null, type);
        }

        /// <summary>抛出异常</summary>
        /// <param name="condition">
        ///     违反约束时的异常提示信息，如：参数不能为null
        /// </param>
        /// <param name="additionalMessage">异常附加消息，如参数值为3</param>
        /// <param name="type">异常类型</param>
        /// <remarks>
        ///     继承 <see cref="ConditionValidator{T}" /> 类时必须实现
        ///     参数都是可选的，并且要注意处理未知类型的<see cref="ConstraintViolationType" />的处理情况
        /// </remarks>
        /// <example>
        ///     示例文档请查看 <see cref="ConditionValidator{T}" />.
        /// </example>
        protected abstract void ThrowExceptionCore(string condition, string additionalMessage,
            ConstraintViolationType type);
    }

    /// <summary>
    ///     违反约束类型
    ///     <remarks>将会抛出对应的异常</remarks>
    /// </summary>
    public enum ConstraintViolationType
    {
        /// <summary>默认异常</summary>
        Default = 0,

        /// <summary>
        ///     超出约定范围异常.
        /// </summary>
        OutOfRangeViolation,

        /// <summary>
        ///     枚举无效异常 <see cref="System.ComponentModel.InvalidEnumArgumentException" />.
        /// </summary>
        InvalidEnumViolation
    }
}