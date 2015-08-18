using System;
using System.ComponentModel;

namespace CS.Conditions
{
    /// <summary>
    ///     前置条件检查
    /// </summary>
    /// <typeparam name="T">被验证的参数类型</typeparam>
    internal class RequiresValidator<T> : ConditionValidator<T>
    {
        internal RequiresValidator(string argumentName, T value) : base(argumentName, value)
        {
        }

        internal virtual Exception BuildExceptionBasedOnViolationType(ConstraintViolationType type, string message)
        {
            switch (type)
            {
                case ConstraintViolationType.OutOfRangeViolation:
                    return new ArgumentOutOfRangeException(ArgumentName, message);

                case ConstraintViolationType.InvalidEnumViolation:
                    var enumMessage = BuildInvalidEnumArgumentExceptionMessage(message);
                    return new InvalidEnumArgumentException(enumMessage);

                default:
                    return Value != null
                        ? new ArgumentException(message, ArgumentName)
                        : new ArgumentNullException(ArgumentName, message);
            }
        }

        protected override void ThrowExceptionCore(string condition, string additionalMessage,ConstraintViolationType type)
        {
            var message = BuildExceptionMessage(condition, additionalMessage);

            var exceptionToThrow = BuildExceptionBasedOnViolationType(type, message);

            throw exceptionToThrow;
        }

        private static string BuildExceptionMessage(string condition, string additionalMessage)
        {
            if (!string.IsNullOrEmpty(additionalMessage))
            {
                return condition + ". " + additionalMessage;
            }
            return condition + ".";
        }

        private string BuildInvalidEnumArgumentExceptionMessage(string message)
        {
            var argumentException = new ArgumentException(message, ArgumentName);

            // Returns the message formatted according to the current culture.
            // Note that the 'Parameter name' part of the message is culture sensitive.
            return argumentException.Message;
        }
    }
}