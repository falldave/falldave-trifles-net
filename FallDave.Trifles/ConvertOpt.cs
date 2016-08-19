using System;
using System.Collections.Generic;
using System.Linq;

namespace FallDave.Trifles
{
    /// <summary>
    /// Converts a base data type to an option containing another base type, producing an empty option on failure.
    /// </summary>
    public static class ConvertOpt
    {
        private static Opt<T> Optify<T>(Func<T> fn)
        {
            try
            {
                var result = fn();
                return Opt.Full(result);
            }
            catch (FormatException)
            {
                // do nothing
            }
            catch (ArgumentNullException)
            {
                // do nothing
            }
            return Opt.Empty<T>();
        }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Boolean value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Byte value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Char value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(DateTime value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Decimal value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Double value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Int16 value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Int32 value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Int64 value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Object value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Object, IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToBoolean(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(SByte value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(Single value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(String value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(String, IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToBoolean(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(UInt16 value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(UInt32 value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToBoolean(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Boolean"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Boolean> ToBooleanOpt(UInt64 value) { return Optify(() => Convert.ToBoolean(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Boolean value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Byte value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Char value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(DateTime value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Decimal value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Double value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Int16 value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Int32 value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Int64 value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Object value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Object, IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToByte(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(SByte value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(Single value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(String value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(String, IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToByte(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(String,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <param name="fromBase">The base of the numeric value in <paramref name="value"/>, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(String value, int fromBase) { return Optify(() => Convert.ToByte(value, fromBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(UInt16 value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(UInt32 value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToByte(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Byte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Byte> ToByteOpt(UInt64 value) { return Optify(() => Convert.ToByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Boolean value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Byte value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Char value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(DateTime value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Decimal value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Double value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Int16 value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Int32 value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Int64 value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Object value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Object, IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToChar(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(SByte value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(Single value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(String value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(String, IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToChar(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(UInt16 value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(UInt32 value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToChar(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Char"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Char> ToCharOpt(UInt64 value) { return Optify(() => Convert.ToChar(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Boolean value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Byte value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Char value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(DateTime value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Decimal value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Double value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Int16 value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Int32 value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Int64 value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Object value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Object, IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToDateTime(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(SByte value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(Single value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(String value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToDateTime(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(UInt16 value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(UInt32 value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDateTime(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="DateTime"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<DateTime> ToDateTimeOpt(UInt64 value) { return Optify(() => Convert.ToDateTime(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Boolean value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Byte value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Char value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(DateTime value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Decimal value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Double value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Int16 value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Int32 value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Int64 value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Object value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToDecimal(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(SByte value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(Single value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(String value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToDecimal(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(UInt16 value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(UInt32 value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDecimal(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Decimal"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Decimal> ToDecimalOpt(UInt64 value) { return Optify(() => Convert.ToDecimal(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Boolean value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Byte value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Char value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(DateTime value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Decimal value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Double value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Int16 value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Int32 value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Int64 value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Object value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToDouble(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(SByte value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(Single value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(String value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToDouble(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(UInt16 value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(UInt32 value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToDouble(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Double"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Double> ToDoubleOpt(UInt64 value) { return Optify(() => Convert.ToDouble(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Boolean value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Byte value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Char value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(DateTime value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Decimal value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Double value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Int16 value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Int32 value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Int64 value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Object value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToInt16(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(SByte value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(Single value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(String value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(String value, IFormatProvider provider) { return Optify(() => Convert.ToInt16(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(String,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <param name="fromBase">The base of the numeric value in <paramref name="value"/>, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(String value, int fromBase) { return Optify(() => Convert.ToInt16(value, fromBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(UInt16 value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(UInt32 value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt16(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int16> ToInt16Opt(UInt64 value) { return Optify(() => Convert.ToInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Boolean value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Byte value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Char value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(DateTime value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Decimal value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Double value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Int16 value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Int32 value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Int64 value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Object value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToInt32(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(SByte value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(Single value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(String value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(String value, IFormatProvider provider) { return Optify(() => Convert.ToInt32(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(String,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <param name="fromBase">The base of the numeric value in <paramref name="value"/>, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(String value, int fromBase) { return Optify(() => Convert.ToInt32(value, fromBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(UInt16 value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(UInt32 value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt32(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int32> ToInt32Opt(UInt64 value) { return Optify(() => Convert.ToInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Boolean value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Byte value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Char value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(DateTime value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Decimal value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Double value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Int16 value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Int32 value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Int64 value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Object value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToInt64(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(SByte value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(Single value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(String value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(String value, IFormatProvider provider) { return Optify(() => Convert.ToInt64(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(String,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <param name="fromBase">The base of the numeric value in <paramref name="value"/>, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(String value, int fromBase) { return Optify(() => Convert.ToInt64(value, fromBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(UInt16 value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(UInt32 value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToInt64(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Int64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Int64> ToInt64Opt(UInt64 value) { return Optify(() => Convert.ToInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Boolean value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Byte value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Char value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(DateTime value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Decimal value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Double value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Int16 value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Int32 value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Int64 value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Object value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToSByte(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(SByte value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(Single value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(String value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToSByte(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(String,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <param name="fromBase">The base of the numeric value in <paramref name="value"/>, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(String value, int fromBase) { return Optify(() => Convert.ToSByte(value, fromBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(UInt16 value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(UInt32 value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSByte(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="SByte"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<SByte> ToSByteOpt(UInt64 value) { return Optify(() => Convert.ToSByte(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Boolean value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Byte value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Char value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(DateTime value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Decimal value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Double value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Int16 value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Int32 value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Int64 value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Object value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToSingle(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(SByte value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(Single value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(String value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToSingle(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(UInt16 value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(UInt32 value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToSingle(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="Single"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<Single> ToSingleOpt(UInt64 value) { return Optify(() => Convert.ToSingle(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Boolean value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Boolean,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Boolean value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Byte value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Byte,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Byte value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Byte,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="toBase">The base of the resulting numeric representation, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Byte value, int toBase) { return Optify(() => Convert.ToString(value, toBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Char value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Char,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Char value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(DateTime value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(DateTime,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(DateTime value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Decimal value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Decimal,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Decimal value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Double value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Double,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Double value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int16 value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int16,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int16 value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int16,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="toBase">The base of the resulting numeric representation, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int16 value, int toBase) { return Optify(() => Convert.ToString(value, toBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int32 value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int32,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int32 value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int32,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="toBase">The base of the resulting numeric representation, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int32 value, int toBase) { return Optify(() => Convert.ToString(value, toBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int64 value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int64,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int64 value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Int64,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="toBase">The base of the resulting numeric representation, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Int64 value, int toBase) { return Optify(() => Convert.ToString(value, toBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Object value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(SByte value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(SByte,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(SByte value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Single value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(Single,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(Single value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(String value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(String value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(UInt16 value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(UInt16,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(UInt16 value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(UInt32 value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(UInt32,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(UInt32 value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(UInt64 value) { return Optify(() => Convert.ToString(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToString(UInt64,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="String"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<String> ToStringOpt(UInt64 value, IFormatProvider provider) { return Optify(() => Convert.ToString(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Boolean value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Byte value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Char value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(DateTime value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Decimal value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Double value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Int16 value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Int32 value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Int64 value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Object value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToUInt16(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(SByte value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(Single value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(String value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(String value, IFormatProvider provider) { return Optify(() => Convert.ToUInt16(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(String,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <param name="fromBase">The base of the numeric value in <paramref name="value"/>, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(String value, int fromBase) { return Optify(() => Convert.ToUInt16(value, fromBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(UInt16 value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(UInt32 value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt16(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt16"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt16> ToUInt16Opt(UInt64 value) { return Optify(() => Convert.ToUInt16(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Boolean value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Byte value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Char value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(DateTime value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Decimal value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Double value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Int16 value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Int32 value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Int64 value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Object value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToUInt32(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(SByte value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(Single value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(String value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(String value, IFormatProvider provider) { return Optify(() => Convert.ToUInt32(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(String,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <param name="fromBase">The base of the numeric value in <paramref name="value"/>, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(String value, int fromBase) { return Optify(() => Convert.ToUInt32(value, fromBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(UInt16 value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(UInt32 value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt32(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt32"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt32> ToUInt32Opt(UInt64 value) { return Optify(() => Convert.ToUInt32(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Boolean)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Boolean value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Byte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Byte value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Char)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Char value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(DateTime)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(DateTime value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Decimal)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Decimal value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Double)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Double value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Int16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Int16 value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Int32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Int32 value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Int64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Int64 value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Object)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Object value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Object,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Object value, IFormatProvider provider) { return Optify(() => Convert.ToUInt64(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(SByte)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(SByte value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(Single)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(Single value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(String)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(String value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(String,IFormatProvider)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(String value, IFormatProvider provider) { return Optify(() => Convert.ToUInt64(value, provider)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(String,int)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <param name="fromBase">The base of the numeric value in <paramref name="value"/>, which must be 2, 8, 10, or 16</param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(String value, int fromBase) { return Optify(() => Convert.ToUInt64(value, fromBase)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(UInt16)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(UInt16 value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(UInt32)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(UInt32 value) { return Optify(() => Convert.ToUInt64(value)); }

        /// <summary>Returns the result of <see cref="Convert.ToUInt64(UInt64)"/> as an option.</summary>
        /// <param name="value">A value to be converted to <see cref="UInt64"/></param>
        /// <returns>An option containing the result of the conversion, or an empty option if the conversion threw <see cref="FormatException"/> or <see cref="ArgumentNullException"/></returns>
        public static Opt<UInt64> ToUInt64Opt(UInt64 value) { return Optify(() => Convert.ToUInt64(value)); }
    }
}