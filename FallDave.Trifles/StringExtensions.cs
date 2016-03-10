using System;
using System.Collections.Generic;

namespace FallDave.Trifles
{
    /// <summary>
    /// Extension methods that provide instance-looking invocations of a few static methods from
    /// <see cref="String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Alternate invocation for <see cref="string.Format(string, object)"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <returns></returns>
        public static string FormatStr(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Format(string, object, object)"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public static string FormatStr(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Format(string, object, object, object)"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string FormatStr(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Format(string, object[])"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatStr(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Format(IFormatProvider, string, object[])"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatStr(this string format, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider, format, args);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Intern(string)"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string InternStr(this string str)
        {
            return string.Intern(str);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.IsInterned(string)"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IsInternedStr(this string str)
        {
            return string.IsInterned(str);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyStr(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpaceStr(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Join(string, object[])"/>.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string JoinStr(this string separator, params object[] values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Join(string, string[])"/>.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string JoinStr(this string separator, params string[] values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Join(string, IEnumerable{string})"/>.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string JoinStr(this string separator, IEnumerable<string> values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Join{T}(string, IEnumerable{T})"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string JoinStr<T>(this string separator, IEnumerable<T> values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Alternate invocation for <see cref="string.Join(string, string[], int, int)"/>.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string JoinStr(this string separator, string[] value, int startIndex, int count)
        {
            return string.Join(separator, value, startIndex, count);
        }
    }
}
