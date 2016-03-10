using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallDave.Trifles
{
    /// <summary>
    /// Extension methods applicable to the simple types defined by C# ( <c>bool</c>, <c>byte</c>,
    /// <c>char</c>, <c>decimal</c>, <c>double</c>, <c>float</c>, <c>int</c>, <c>long</c>,
    /// <c>sbyte</c>, <c>short</c>, <c>uint</c>, <c>ulong</c>, and <c>ushort</c>), specifically, to
    /// retrieve the value which all that is known about the value is that it is an <see cref="IConvertible"/>.
    /// <para>
    /// To utilize these methods on some arbitrary value or object, cast it (applying any checking
    /// necessary) to <see cref="IConvertible"/>, an interface implemented by all of the basic
    ///            types. If the cast cannot be performed, the value is known not to be of a simple
    /// type (all simple types implement <see cref="IConvertible"/>). If the cast succeeds, the
    /// methods in this class become available.
    /// </para>
    /// </summary>
    public static class SimpleTypeExtensions
    {
        private enum SimpleTypeClassification
        {
            NonSimpleType = 0,
            Boolean,
            UnsignedIntegral,
            SignedIntegral,
            FloatingPoint,
            Decimal
        }
        
        private static SimpleTypeClassification Classify(Type type)
        {
            switch(type.FullName)
            {
                case "System.Boolean":
                    return SimpleTypeClassification.Boolean;

                case "System.Byte":
                case "System.Char":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                    return SimpleTypeClassification.UnsignedIntegral;

                case "System.SByte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    return SimpleTypeClassification.SignedIntegral;
                    
                case "System.Single":
                case "System.Double":
                    return SimpleTypeClassification.FloatingPoint;

                case "System.Decimal":
                    return SimpleTypeClassification.Decimal;
                    
                default:
                    return SimpleTypeClassification.NonSimpleType;
            }
        }

        private static SimpleTypeClassification Classify(IConvertible value)
        {
            return (value == null) ? SimpleTypeClassification.NonSimpleType : Classify(value.GetType());
        }

        /// <summary>
        /// Determines whether the specified value is of one of the simple types defined by C#.
        /// <para>
        /// The simple types defined by C# are <c>bool</c>, <c>byte</c>, <c>char</c>,
        /// <c>decimal</c>, <c>double</c>, <c>float</c>, <c>int</c>, <c>long</c>, <c>sbyte</c>,
        /// <c>short</c>, <c>uint</c>, <c>ulong</c>, and <c>ushort</c>.
        /// </para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasSimpleType(this IConvertible value)
        {
            return Classify(value) != SimpleTypeClassification.NonSimpleType;
        }

        /// <summary>
        /// Determines whether the specified value is one of the simple types defined by C# to
        /// contain unsigned integer values.
        /// </summary>
        /// <para>
        /// This method returns true for a value of any of <c>byte</c>, <c>char</c>, <c>uint</c>,
        /// <c>ulong</c>, and <c>ushort</c>.
        /// </para>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUnsignedIntegral(this IConvertible value)
        {
            return Classify(value) == SimpleTypeClassification.UnsignedIntegral;
        }

        /// <summary>
        /// Determines whether the specified value is one of the simple types defined by C# to
        /// contained unsigned integer values but is not a <c>char</c>.
        /// </summary>
        /// <para>
        /// This method returns true for a value of any of <c>byte</c>, <c>uint</c>, <c>ulong</c>,
        /// and <c>ushort</c>.
        /// </para>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUnsignedIntegralAndNotChar(this IConvertible value)
        {
            return !(value is char) && value.IsUnsignedIntegral();
        }

        /// <summary>
        /// Determines whether the specified value is one of the simple types defined by C# to
        /// contain signed integer values.
        /// </summary>
        /// <para>
        /// This method returns true for a value of any of <c>int</c>, <c>long</c>, <c>sbyte</c>,
        /// and <c>short</c>.
        /// </para>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSignedIntegral(this IConvertible value)
        {
            return Classify(value) == SimpleTypeClassification.SignedIntegral;
        }

        /// <summary>
        /// Determines whether the specified value is one of the simple types defined by C# to
        /// contain integer values.
        /// </summary>
        /// <para>
        /// This method is equivalent to
        /// <code>
        /// value.IsSignedIntegral() || value.IsUnsignedIntegral()
        /// </code>.
        /// </para>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIntegral(this IConvertible value)
        {
            switch (Classify(value))
            {
                case SimpleTypeClassification.SignedIntegral:
                case SimpleTypeClassification.UnsignedIntegral:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines whether the specified value is one of the simple types defined by C# to
        /// contain integer values but is not a <c>char</c>.
        /// </summary>
        /// <para>
        /// This method is equivalent to
        /// <code>
        /// value.IsSignedIntegral() || value.IsUnsignedIntegralAndNotChar()
        /// </code>.
        /// </para>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIntegralAndNotChar(this IConvertible value)
        {
            return !(value is char) && value.IsIntegral();
        }

        /// <summary>
        /// Determines whether the specified value is one of the simple types defined by C# to
        /// contain floating-point values.
        /// </summary>
        /// <para>This method returns true for a value of either <c>float</c> or <c>double</c>.</para>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFloatingPoint(this IConvertible value)
        {
            return Classify(value) == SimpleTypeClassification.FloatingPoint;
        }

        /// <summary>
        /// Determines whether the specified value is one of the simple typed defined by C# to
        /// contain numeric values.
        /// <para>
        /// This method is equivalent to
        /// <code>
        /// value.IsIntegral() || value.IsFloatingPoint() || (value is decimal)
        /// </code>.
        /// </para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this IConvertible value)
        {
            return (value is decimal) || value.IsIntegral() || value.IsFloatingPoint();
        }

        /// <summary> Determines whether the specified value is one of the simple typed defined by
        /// C# to contain numeric values but is not a <c>char</c>. <para> This method is equivalent
        /// to <code><![CDATA[value.IsNumeric() && !(value is char)]]></code>. </para> </summary> <param
        /// name="value"></param> <returns></returns>
        public static bool IsNumericAndNotChar(this IConvertible value)
        {
            return !(value is char) && value.IsNumeric();
        }

        /// <summary>
        /// If the specified value is of a floating-point type, gets the value as a <c>double</c>.
        /// <para>The result is empty if the specified value is not of a floating-point type.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Opt<double> FloatingPointValueOpt(this IConvertible value)
        {
            double v;

            if (value is float) { v = (float)value; }
            else if (value is double) { v = (double)value; }
            else
            {
                return Opt.Empty<double>();
            }

            return Opt.Full(v);
        }

        /// <summary>
        /// If the specified value is of any integral type that is or widens to a <c>long</c>, gets
        /// the value as a <c>long</c>.
        /// <para>The result is empty if the value is of a non-integral type or of <c>ulong</c>.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Opt<long> IntegralValueWidenedToLongOpt(this IConvertible value)
        {
            long v;

            if (value is sbyte) { v = (sbyte)value; }
            else if (value is byte) { v = (byte)value; }
            else if (value is char) { v = (char)value; }
            else if (value is short) { v = (short)value; }
            else if (value is ushort) { v = (ushort)value; }
            else if (value is int) { v = (int)value; }
            else if (value is uint) { v = (uint)value; }
            else if (value is long) { v = (long)value; }
            else
            {
                return Opt.Empty<long>();
            }
            return Opt.Full(v);
        }

        /// <summary>
        /// If the specified value is of any integral type and can be cast losslessly to a
        /// <c>long</c>, gets the value as a <c>long</c>.
        /// <para>
        /// If the value is a <c>ulong</c> but can be converted to a <c>long</c> without losing
        /// data, this method gets the converted value.
        /// </para>
        /// <para>
        /// The result is empty if the specified value is not of an integral type or is of
        /// <c>ulong</c> with a value that cannot be stored losslessly as a <c>long</c>.
        /// </para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Opt<long> SignedIntegralValueOpt(this IConvertible value)
        {
            if (value is ulong)
            {
                long v = (long)value;
                if (v < 0) { return Opt.Empty<long>(); }
                return Opt.Full(v);
            }
            else
            {
                return value.IntegralValueWidenedToLongOpt();
            }
        }

        /// <summary>
        /// If the specified value is of any integral type and can be cast losslessly to a
        /// <c>ulong</c>, gets the value as a <c>ulong</c>.
        /// <para>
        /// If the value is of a signed integral type but can be converted to a <c>ulong</c> without
        /// losing data (i.e., the value is nonnegative), this method gets the converted value.
        /// </para>
        /// <para>
        /// The result is empty if the specified value is not of an integral type or has a negative value.
        /// </para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Opt<ulong> UnsignedIntegralValueOpt(this IConvertible value)
        {
            if (value is ulong)
            {
                return Opt.Full((ulong)value);
            }

            // Returns empty option if the value is negative.
            return value.IntegralValueWidenedToLongOpt()
                .WhereFix(lv => lv >= 0)
                .SelectFix(lv => (ulong)lv);
        }

        /// <summary>
        /// If the specified value is of the type <c>decimal</c>, gets the value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Opt<decimal> DecimalValueOpt(this IConvertible value)
        {
            if (value is decimal)
            {
                return Opt.Full((decimal)value);
            }
            return Opt.Empty<decimal>();
        }

        /// <summary>
        /// If the specified value is of any numeric type, retrieves the value as a <c>double</c>,
        /// casting if necessary.
        /// <para>The result is empty if the specified value is not of a numeric type.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Opt<double> NumericValueAsDoubleOpt(this IConvertible value)
        {
            switch (Classify(value))
            {
                case SimpleTypeClassification.Decimal:
                    {
                        return value
                            .DecimalValueOpt()
                            .SelectFix(decimalValue => (double)decimalValue);
                    }
                case SimpleTypeClassification.FloatingPoint:
                    {
                        return value.FloatingPointValueOpt();
                    }
                case SimpleTypeClassification.SignedIntegral:
                    {
                        return value
                            .SignedIntegralValueOpt()
                            .SelectFix(longValue => (double)longValue);
                    }
                case SimpleTypeClassification.UnsignedIntegral:
                    {
                        return value
                            .UnsignedIntegralValueOpt()
                            .SelectFix(ulongValue => (double)ulongValue);
                    }
                default:
                    return Opt.Empty<double>();
            }
        }

        /// <summary>
        /// If the specified value is of any numeric type, retrieves the value as a <c>decimal</c>,
        /// casting if necessary.
        /// <para>The result is empty if the specified value is not of a numeric type.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Opt<decimal> NumericValueAsDecimalOpt(this IConvertible value)
        {
            switch (Classify(value))
            {
                case SimpleTypeClassification.Decimal:
                    {
                        return value.DecimalValueOpt();
                    }
                case SimpleTypeClassification.FloatingPoint:
                    {
                        return value
                            .FloatingPointValueOpt()
                            .SelectFix(doubleValue => (decimal)doubleValue);
                    }
                case SimpleTypeClassification.SignedIntegral:
                    {
                        return value
                            .SignedIntegralValueOpt()
                            .SelectFix(longValue => (decimal)longValue);
                    }
                case SimpleTypeClassification.UnsignedIntegral:
                    {
                        return value
                            .UnsignedIntegralValueOpt()
                            .SelectFix(ulongValue => (decimal)ulongValue);
                    }
                default:
                    return Opt.Empty<decimal>();
            }
        }
    }
}
