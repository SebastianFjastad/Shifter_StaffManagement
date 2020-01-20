using System;
using System.Collections;
using System.Diagnostics;
using Framework.Properties;

namespace Framework
{
    /// <summary>
    /// Guards are used to give more meaningful exceptions
    /// </summary>
    [DebuggerStepThrough]
    public static class Guard
    {
        /// <summary>
        /// Validates that the <paramref name="argument"/> is not null.
        /// </summary>
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            ArgumentNotEmpty(argumentName, "argumentName");

            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Validates that the <paramref name="value"/> is not null.
        /// </summary>
        public static void InstanceNotNull(object value, string instanceName)
        {
            if (value == null)
            {
                throw new NullReferenceException("Instance expected but not supplied. " + instanceName);
            }
        }

        /// <summary>
        /// Validates that the <paramref name="value"/> is not empty.
        /// </summary>
        public static void ArgumentNotEmpty(Guid value, string argumentName)
        {
            ArgumentNotEmpty(argumentName, "argumentName");

            if (value == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(Resources.ArgumentCannotBeEmpty.FormatString(argumentName));
            }
        }

        /// <summary>
        /// Validates that the <paramref name="collection"/> is not empty.
        /// </summary>
        public static void ArgumentNotEmpty(ICollection collection, string argumentName)
        {
            ArgumentNotNull(collection, "collection");
            ArgumentNotEmpty(argumentName, "argumentName");

            if (collection.Count == 0)
            {
                throw new ArgumentOutOfRangeException(Resources.CollectionCannotBeEmpty.FormatString(argumentName));
            }
        }

        /// <summary>
        /// Validates that the generic <paramref name="collection"/> is not empty.
        /// </summary>
        public static void ArgumentNotEmpty<T>(System.Collections.Generic.ICollection<T> collection, string argumentName)
        {
            ArgumentNotNull(collection, "collection");
            ArgumentNotEmpty(argumentName, "argumentName");

            if (collection.Count == 0)
            {
                throw new ArgumentOutOfRangeException(Resources.CollectionCannotBeEmpty.FormatString(argumentName));
            }
        }

        /// <summary>
        /// Validates that the string <paramref name="argument"/> is not empty.
        /// </summary>
        public static void ArgumentNotEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentOutOfRangeException(Resources.ArgumentCannotBeEmpty.FormatString(argumentName));
            }
        }

        /// <summary>
        /// Validates that an enum <paramref name="value" /> is defined.
        /// </summary>
        public static void EnumValueIsDefined(Type enumType, object value, string argumentName)
        {
            ArgumentNotNull(value, "currentPropertyValue");
            ArgumentNotEmpty(argumentName, "argumentName");

            if (Enum.IsDefined(enumType, value) == false)
            {
                throw new ArgumentException(Resources.InvalidEnumValue.FormatString(argumentName, enumType));
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="test"/> is true.
        /// </summary>
        public static void IsTrue(bool test)
        {
            if (!test)
            {
                throw new ArgumentException(Resources.ResultWasFalse);
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="test"/> is true. Throw the specified exception if false.
        /// </summary>
        public static void IsTrue(bool test, Exception exception)
        {
            if (test)
            {
                throw exception;
            }
        }
    }
}