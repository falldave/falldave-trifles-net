using System;
using System.Linq;
using System.Collections.Generic;

namespace FallDave.Trifles
{
    /// <summary>
    /// Utilities for math clamping, comparisons, and conversions.
    /// </summary>
    public static class MathTrifles
    {
        private static ArgumentException ClampRangeMustNotBeEmpty()
        {
            return new ArgumentException("Clamp range must not be empty.", "maxInclusive");
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int32"/> operand and returns an
        /// indication of their relative values.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of <paramref name="val1"/> and
        /// <paramref name="val2"/>.
        /// <list type="table">
        /// <item>
        /// <term>Less than 0</term>
        /// <description><paramref name="val1"/> is less than <paramref name="val2"/>.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description><paramref name="val1"/> is equal to <paramref name="val2"/>.</description>
        /// </item>
        /// <item>
        /// <term>Greater than 0</term>
        /// <description><paramref name="val1"/> is greater than <paramref name="val2"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static int Compare(UInt64 val1, Int32 val2)
        {
            return (val1 > Int32.MaxValue) ? 1 : ((Int32)val1).CompareTo(val2);
        }

        /// <summary>
        /// Compares an <see cref="Int32"/> operand to a <see cref="UInt64"/> operand and returns an
        /// indication of their relative values.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of <paramref name="val1"/> and
        /// <paramref name="val2"/>.
        /// <list type="table">
        /// <item>
        /// <term>Less than 0</term>
        /// <description><paramref name="val1"/> is less than <paramref name="val2"/>.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description><paramref name="val1"/> is equal to <paramref name="val2"/>.</description>
        /// </item>
        /// <item>
        /// <term>Greater than 0</term>
        /// <description><paramref name="val1"/> is greater than <paramref name="val2"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static int Compare(Int32 val1, UInt64 val2)
        {
            return (val2 > Int32.MaxValue) ? -1 : val1.CompareTo((Int32)val2);
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int32"/> operand, returning true
        /// if the first operand is smaller.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is less than <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool LessThan(UInt64 val1, Int32 val2)
        {
            return (val1 > Int32.MaxValue) ? false : (((Int32)val1) < val2);
        }

        /// <summary>
        /// Compares an <see cref="Int32"/> operand to a <see cref="Int32"/> operand, returning true
        /// if the first operand is smaller.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is less than <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool LessThan(Int32 val1, UInt64 val2)
        {
            return (val2 > Int32.MaxValue) ? true : (val1 < ((Int32)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int32"/> operand, returning true
        /// if the first operand is smaller or the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is less than or equal to <paramref name="val2"/>, or
        /// false otherwise.
        /// </returns>
        public static bool LessThanOrEqual(UInt64 val1, Int32 val2)
        {
            return (val1 > Int32.MaxValue) ? false : (((Int32)val1) <= val2);
        }

        /// <summary>
        /// Compares an <see cref="Int32"/> operand to a <see cref="UInt64"/> operand, returning true
        /// if the first operand is smaller or the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is less than or equal to <paramref name="val2"/>, or
        /// false otherwise.
        /// </returns>
        public static bool LessThanOrEqual(Int32 val1, UInt64 val2)
        {
            return (val2 > Int32.MaxValue) ? true : (val1 <= ((Int32)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int32"/> operand, returning true
        /// if the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is equal to <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool Equality(UInt64 val1, Int32 val2)
        {
            return (val1 > Int32.MaxValue) ? false : (((Int32)val1) == val2);
        }

        /// <summary>
        /// Compares an <see cref="Int32"/> operand to a <see cref="UInt64"/> operand, returning true
        /// if the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is equal to <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool Equality(Int32 val1, UInt64 val2)
        {
            return (val2 > Int32.MaxValue) ? false : (val1 == ((Int32)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int32"/> operand, returning true
        /// if the first operand is larger or the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is greater than or equal to <paramref name="val2"/>, or
        /// false otherwise.
        /// </returns>
        public static bool GreaterThanOrEqual(UInt64 val1, Int32 val2)
        {
            return (val1 > Int32.MaxValue) ? true : (((Int32)val1) >= val2);
        }

        /// <summary>
        /// Compares an <see cref="Int32"/> operand to a <see cref="UInt64"/> operand, returning true
        /// if the first operand is larger or the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is greater than or equal to <paramref name="val2"/>, or
        /// false otherwise.
        /// </returns>
        public static bool GreaterThanOrEqual(Int32 val1, UInt64 val2)
        {
            return (val2 > Int32.MaxValue) ? false : (val1 >= ((Int32)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int32"/> operand, returning true
        /// if the first operand is larger.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is greater than <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool GreaterThan(UInt64 val1, Int32 val2)
        {
            return (val1 > Int32.MaxValue) ? true : (((Int32)val1) > val2);
        }

        /// <summary>
        /// Compares an <see cref="Int32"/> operand to a <see cref="UInt64"/> operand, returning true
        /// if the first operand is larger.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int32.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int32"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is greater than <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool GreaterThan(Int32 val1, UInt64 val2)
        {
            return (val2 > Int32.MaxValue) ? false : (val1 > ((Int32)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int64"/> operand and returns an
        /// indication of their relative values.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of <paramref name="val1"/> and
        /// <paramref name="val2"/>.
        /// <list type="table">
        /// <item>
        /// <term>Less than 0</term>
        /// <description><paramref name="val1"/> is less than <paramref name="val2"/>.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description><paramref name="val1"/> is equal to <paramref name="val2"/>.</description>
        /// </item>
        /// <item>
        /// <term>Greater than 0</term>
        /// <description><paramref name="val1"/> is greater than <paramref name="val2"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static int Compare(UInt64 val1, Int64 val2)
        {
            return (val1 > Int64.MaxValue) ? 1 : ((Int64)val1).CompareTo(val2);
        }

        /// <summary>
        /// Compares an <see cref="Int64"/> operand to a <see cref="UInt64"/> operand and returns an
        /// indication of their relative values.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of <paramref name="val1"/> and
        /// <paramref name="val2"/>.
        /// <list type="table">
        /// <item>
        /// <term>Less than 0</term>
        /// <description><paramref name="val1"/> is less than <paramref name="val2"/>.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description><paramref name="val1"/> is equal to <paramref name="val2"/>.</description>
        /// </item>
        /// <item>
        /// <term>Greater than 0</term>
        /// <description><paramref name="val1"/> is greater than <paramref name="val2"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static int Compare(Int64 val1, UInt64 val2)
        {
            return (val2 > Int64.MaxValue) ? -1 : val1.CompareTo((Int64)val2);
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int64"/> operand, returning true
        /// if the first operand is smaller.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is less than <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool LessThan(UInt64 val1, Int64 val2)
        {
            return (val1 > Int64.MaxValue) ? false : (((Int64)val1) < val2);
        }

        /// <summary>
        /// Compares an <see cref="Int64"/> operand to a <see cref="Int64"/> operand, returning true
        /// if the first operand is smaller.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is less than <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool LessThan(Int64 val1, UInt64 val2)
        {
            return (val2 > Int64.MaxValue) ? true : (val1 < ((Int64)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int64"/> operand, returning true
        /// if the first operand is smaller or the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is less than or equal to <paramref name="val2"/>, or
        /// false otherwise.
        /// </returns>
        public static bool LessThanOrEqual(UInt64 val1, Int64 val2)
        {
            return (val1 > Int64.MaxValue) ? false : (((Int64)val1) <= val2);
        }

        /// <summary>
        /// Compares an <see cref="Int64"/> operand to a <see cref="UInt64"/> operand, returning true
        /// if the first operand is smaller or the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is less than or equal to <paramref name="val2"/>, or
        /// false otherwise.
        /// </returns>
        public static bool LessThanOrEqual(Int64 val1, UInt64 val2)
        {
            return (val2 > Int64.MaxValue) ? true : (val1 <= ((Int64)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int64"/> operand, returning true
        /// if the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is equal to <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool Equality(UInt64 val1, Int64 val2)
        {
            return (val1 > Int64.MaxValue) ? false : (((Int64)val1) == val2);
        }

        /// <summary>
        /// Compares an <see cref="Int64"/> operand to a <see cref="UInt64"/> operand, returning true
        /// if the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is equal to <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool Equality(Int64 val1, UInt64 val2)
        {
            return (val2 > Int64.MaxValue) ? false : (val1 == ((Int64)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int64"/> operand, returning true
        /// if the first operand is larger or the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is greater than or equal to <paramref name="val2"/>, or
        /// false otherwise.
        /// </returns>
        public static bool GreaterThanOrEqual(UInt64 val1, Int64 val2)
        {
            return (val1 > Int64.MaxValue) ? true : (((Int64)val1) >= val2);
        }

        /// <summary>
        /// Compares an <see cref="Int64"/> operand to a <see cref="UInt64"/> operand, returning true
        /// if the first operand is larger or the operands are equal.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is greater than or equal to <paramref name="val2"/>, or
        /// false otherwise.
        /// </returns>
        public static bool GreaterThanOrEqual(Int64 val1, UInt64 val2)
        {
            return (val2 > Int64.MaxValue) ? false : (val1 >= ((Int64)val2));
        }

        /// <summary>
        /// Compares a <see cref="UInt64"/> operand to an <see cref="Int64"/> operand, returning true
        /// if the first operand is larger.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is greater than <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool GreaterThan(UInt64 val1, Int64 val2)
        {
            return (val1 > Int64.MaxValue) ? true : (((Int64)val1) > val2);
        }

        /// <summary>
        /// Compares an <see cref="Int64"/> operand to a <see cref="UInt64"/> operand, returning true
        /// if the first operand is larger.
        /// </summary>
        /// <remarks>
        /// This method works around the ambiguity reported when comparing a <see cref="UInt64"/>
        /// operand with a signed operand. If the <see cref="UInt64"/> operand is greater than <see
        /// cref="Int64.MaxValue"/>, it is automatically known to be greater than the signed operand;
        /// otherwise, it is safely cast to the type <see cref="Int64"/> and compared natively to the
        /// signed operand.
        /// </remarks>
        /// <param name="val1">The first value to compare.</param>
        /// <param name="val2">The second value to compare.</param>
        /// <returns>
        /// True if <paramref name="val1"/> is greater than <paramref name="val2"/>, or false otherwise.
        /// </returns>
        public static bool GreaterThan(Int64 val1, UInt64 val2)
        {
            return (val2 > Int64.MaxValue) ? false : (val1 > ((Int64)val2));
        }

        // Implements the clamp operation from Int32 to SByte.
        private static SByte ClampToSByteNoCheck(Int32 value, SByte minInclusive, SByte maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (SByte)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int32"/> to the specified <see cref="SByte"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static SByte Clamp(Int32 value, SByte minInclusive, SByte maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToSByteNoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int64 to SByte.
        private static SByte ClampToSByteNoCheck(Int64 value, SByte minInclusive, SByte maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (SByte)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int64"/> to the specified <see cref="SByte"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static SByte Clamp(Int64 value, SByte minInclusive, SByte maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToSByteNoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from UInt64 to SByte.
        private static SByte ClampToSByteNoCheck(UInt64 value, SByte minInclusive, SByte maxInclusive)
        {
            return LessThan(value, minInclusive) ? minInclusive : GreaterThan(value, maxInclusive) ? maxInclusive : (SByte)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="UInt64"/> to the specified <see cref="SByte"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static SByte Clamp(UInt64 value, SByte minInclusive, SByte maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToSByteNoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int32 to Byte.
        private static Byte ClampToByteNoCheck(Int32 value, Byte minInclusive, Byte maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (Byte)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int32"/> to the specified <see cref="Byte"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Byte Clamp(Int32 value, Byte minInclusive, Byte maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToByteNoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int64 to Byte.
        private static Byte ClampToByteNoCheck(Int64 value, Byte minInclusive, Byte maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (Byte)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int64"/> to the specified <see cref="Byte"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Byte Clamp(Int64 value, Byte minInclusive, Byte maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToByteNoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from UInt64 to Byte.
        private static Byte ClampToByteNoCheck(UInt64 value, Byte minInclusive, Byte maxInclusive)
        {
            return LessThan(value, minInclusive) ? minInclusive : GreaterThan(value, maxInclusive) ? maxInclusive : (Byte)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="UInt64"/> to the specified <see cref="Byte"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Byte Clamp(UInt64 value, Byte minInclusive, Byte maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToByteNoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int32 to Int16.
        private static Int16 ClampToInt16NoCheck(Int32 value, Int16 minInclusive, Int16 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (Int16)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int32"/> to the specified <see cref="Int16"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Int16 Clamp(Int32 value, Int16 minInclusive, Int16 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToInt16NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int64 to Int16.
        private static Int16 ClampToInt16NoCheck(Int64 value, Int16 minInclusive, Int16 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (Int16)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int64"/> to the specified <see cref="Int16"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Int16 Clamp(Int64 value, Int16 minInclusive, Int16 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToInt16NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from UInt64 to Int16.
        private static Int16 ClampToInt16NoCheck(UInt64 value, Int16 minInclusive, Int16 maxInclusive)
        {
            return LessThan(value, minInclusive) ? minInclusive : GreaterThan(value, maxInclusive) ? maxInclusive : (Int16)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="UInt64"/> to the specified <see cref="Int16"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Int16 Clamp(UInt64 value, Int16 minInclusive, Int16 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToInt16NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int32 to UInt16.
        private static UInt16 ClampToUInt16NoCheck(Int32 value, UInt16 minInclusive, UInt16 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (UInt16)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int32"/> to the specified <see cref="UInt16"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static UInt16 Clamp(Int32 value, UInt16 minInclusive, UInt16 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToUInt16NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int64 to UInt16.
        private static UInt16 ClampToUInt16NoCheck(Int64 value, UInt16 minInclusive, UInt16 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (UInt16)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int64"/> to the specified <see cref="UInt16"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static UInt16 Clamp(Int64 value, UInt16 minInclusive, UInt16 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToUInt16NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from UInt64 to UInt16.
        private static UInt16 ClampToUInt16NoCheck(UInt64 value, UInt16 minInclusive, UInt16 maxInclusive)
        {
            return LessThan(value, minInclusive) ? minInclusive : GreaterThan(value, maxInclusive) ? maxInclusive : (UInt16)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="UInt64"/> to the specified <see
        /// cref="UInt16"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static UInt16 Clamp(UInt64 value, UInt16 minInclusive, UInt16 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToUInt16NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int32 to Int32.
        private static Int32 ClampToInt32NoCheck(Int32 value, Int32 minInclusive, Int32 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (Int32)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int32"/> to the specified range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Int32 Clamp(Int32 value, Int32 minInclusive, Int32 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToInt32NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int64 to Int32.
        private static Int32 ClampToInt32NoCheck(Int64 value, Int32 minInclusive, Int32 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (Int32)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int64"/> to the specified <see cref="Int32"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Int32 Clamp(Int64 value, Int32 minInclusive, Int32 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToInt32NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from UInt64 to Int32.
        private static Int32 ClampToInt32NoCheck(UInt64 value, Int32 minInclusive, Int32 maxInclusive)
        {
            return LessThan(value, minInclusive) ? minInclusive : GreaterThan(value, maxInclusive) ? maxInclusive : (Int32)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="UInt64"/> to the specified <see cref="Int32"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Int32 Clamp(UInt64 value, Int32 minInclusive, Int32 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToInt32NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int32 to UInt32.
        private static UInt32 ClampToUInt32NoCheck(Int32 value, UInt32 minInclusive, UInt32 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (UInt32)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int32"/> to the specified <see cref="UInt32"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static UInt32 Clamp(Int32 value, UInt32 minInclusive, UInt32 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToUInt32NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int64 to UInt32.
        private static UInt32 ClampToUInt32NoCheck(Int64 value, UInt32 minInclusive, UInt32 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (UInt32)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int64"/> to the specified <see cref="UInt32"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static UInt32 Clamp(Int64 value, UInt32 minInclusive, UInt32 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToUInt32NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from UInt64 to UInt32.
        private static UInt32 ClampToUInt32NoCheck(UInt64 value, UInt32 minInclusive, UInt32 maxInclusive)
        {
            return LessThan(value, minInclusive) ? minInclusive : GreaterThan(value, maxInclusive) ? maxInclusive : (UInt32)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="UInt64"/> to the specified <see
        /// cref="UInt32"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static UInt32 Clamp(UInt64 value, UInt32 minInclusive, UInt32 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToUInt32NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int64 to Int64.
        private static Int64 ClampToInt64NoCheck(Int64 value, Int64 minInclusive, Int64 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (Int64)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int64"/> to the specified range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Int64 Clamp(Int64 value, Int64 minInclusive, Int64 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToInt64NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from UInt64 to Int64.
        private static Int64 ClampToInt64NoCheck(UInt64 value, Int64 minInclusive, Int64 maxInclusive)
        {
            return LessThan(value, minInclusive) ? minInclusive : GreaterThan(value, maxInclusive) ? maxInclusive : (Int64)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="UInt64"/> to the specified <see cref="Int64"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static Int64 Clamp(UInt64 value, Int64 minInclusive, Int64 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToInt64NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from Int64 to UInt64.
        private static UInt64 ClampToUInt64NoCheck(Int64 value, UInt64 minInclusive, UInt64 maxInclusive)
        {
            return LessThan(value, minInclusive) ? minInclusive : GreaterThan(value, maxInclusive) ? maxInclusive : (UInt64)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="Int64"/> to the specified <see cref="UInt64"/> range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static UInt64 Clamp(Int64 value, UInt64 minInclusive, UInt64 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToUInt64NoCheck(value, minInclusive, maxInclusive);
        }

        // Implements the clamp operation from UInt64 to UInt64.
        private static UInt64 ClampToUInt64NoCheck(UInt64 value, UInt64 minInclusive, UInt64 maxInclusive)
        {
            return (value < minInclusive) ? minInclusive : (value > maxInclusive) ? maxInclusive : (UInt64)value;
        }

        /// <summary>
        /// Clamps an integer value of type <see cref="UInt64"/> to the specified range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <param name="minInclusive">The minimum value, inclusive, of the result.</param>
        /// <param name="maxInclusive">The maximum value, inclusive, of the result.</param>
        /// <returns>
        /// The result of clamping the given value to the specified range.
        /// <list type="table">
        /// <item>
        /// <term><paramref name="minInclusive"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <paramref name="minInclusive"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <paramref name="minInclusive"/> to
        /// <paramref name="maxInclusive"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><paramref name="maxInclusive"/></term>
        /// <description>
        /// If <paramref name="value"/> is greater than or equal to <paramref name="maxInclusive"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>,
        /// indicating an empty range.
        /// </exception>
        public static UInt64 Clamp(UInt64 value, UInt64 minInclusive, UInt64 maxInclusive)
        {
            if (minInclusive > maxInclusive) throw ClampRangeMustNotBeEmpty();
            return ClampToUInt64NoCheck(value, minInclusive, maxInclusive);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="SByte"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="SByte"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="SByte.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="SByte.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="SByte.MinValue"/> to <see
        /// cref="SByte.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="SByte.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="SByte.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static SByte ClampToSByte(Int32 value)
        {
            return ClampToSByteNoCheck(value, SByte.MinValue, SByte.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="SByte"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="SByte"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="SByte.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="SByte.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="SByte.MinValue"/> to <see
        /// cref="SByte.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="SByte.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="SByte.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static SByte ClampToSByte(Int64 value)
        {
            return ClampToSByteNoCheck(value, SByte.MinValue, SByte.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="SByte"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="SByte"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="SByte.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="SByte.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="SByte.MinValue"/> to <see
        /// cref="SByte.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="SByte.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="SByte.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static SByte ClampToSByte(UInt64 value)
        {
            return ClampToSByteNoCheck(value, SByte.MinValue, SByte.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Byte"/>, clamping the value if it
        /// is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Byte"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Byte.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Byte.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Byte.MinValue"/> to <see
        /// cref="Byte.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Byte.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Byte.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Byte ClampToByte(Int32 value)
        {
            return ClampToByteNoCheck(value, Byte.MinValue, Byte.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Byte"/>, clamping the value if it
        /// is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Byte"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Byte.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Byte.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Byte.MinValue"/> to <see
        /// cref="Byte.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Byte.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Byte.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Byte ClampToByte(Int64 value)
        {
            return ClampToByteNoCheck(value, Byte.MinValue, Byte.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Byte"/>, clamping the value if it
        /// is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Byte"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Byte.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Byte.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Byte.MinValue"/> to <see
        /// cref="Byte.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Byte.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Byte.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Byte ClampToByte(UInt64 value)
        {
            return ClampToByteNoCheck(value, Byte.MinValue, Byte.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Int16"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Int16"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Int16.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Int16.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Int16.MinValue"/> to <see
        /// cref="Int16.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Int16.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Int16.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Int16 ClampToInt16(Int32 value)
        {
            return ClampToInt16NoCheck(value, Int16.MinValue, Int16.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Int16"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Int16"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Int16.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Int16.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Int16.MinValue"/> to <see
        /// cref="Int16.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Int16.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Int16.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Int16 ClampToInt16(Int64 value)
        {
            return ClampToInt16NoCheck(value, Int16.MinValue, Int16.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Int16"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Int16"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Int16.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Int16.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Int16.MinValue"/> to <see
        /// cref="Int16.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Int16.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Int16.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Int16 ClampToInt16(UInt64 value)
        {
            return ClampToInt16NoCheck(value, Int16.MinValue, Int16.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="UInt16"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="UInt16"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="UInt16.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="UInt16.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="UInt16.MinValue"/> to <see
        /// cref="UInt16.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="UInt16.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="UInt16.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static UInt16 ClampToUInt16(Int32 value)
        {
            return ClampToUInt16NoCheck(value, UInt16.MinValue, UInt16.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="UInt16"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="UInt16"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="UInt16.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="UInt16.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="UInt16.MinValue"/> to <see
        /// cref="UInt16.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="UInt16.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="UInt16.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static UInt16 ClampToUInt16(Int64 value)
        {
            return ClampToUInt16NoCheck(value, UInt16.MinValue, UInt16.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="UInt16"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="UInt16"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="UInt16.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="UInt16.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="UInt16.MinValue"/> to <see
        /// cref="UInt16.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="UInt16.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="UInt16.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static UInt16 ClampToUInt16(UInt64 value)
        {
            return ClampToUInt16NoCheck(value, UInt16.MinValue, UInt16.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Int32"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Int32"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Int32.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Int32.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Int32.MinValue"/> to <see
        /// cref="Int32.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Int32.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Int32.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Int32 ClampToInt32(Int64 value)
        {
            return ClampToInt32NoCheck(value, Int32.MinValue, Int32.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Int32"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Int32"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Int32.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Int32.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Int32.MinValue"/> to <see
        /// cref="Int32.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Int32.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Int32.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Int32 ClampToInt32(UInt64 value)
        {
            return ClampToInt32NoCheck(value, Int32.MinValue, Int32.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="UInt32"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="UInt32"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="UInt32.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="UInt32.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="UInt32.MinValue"/> to <see
        /// cref="UInt32.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="UInt32.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="UInt32.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static UInt32 ClampToUInt32(Int32 value)
        {
            return ClampToUInt32NoCheck(value, UInt32.MinValue, UInt32.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="UInt32"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="UInt32"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="UInt32.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="UInt32.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="UInt32.MinValue"/> to <see
        /// cref="UInt32.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="UInt32.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="UInt32.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static UInt32 ClampToUInt32(Int64 value)
        {
            return ClampToUInt32NoCheck(value, UInt32.MinValue, UInt32.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="UInt32"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="UInt32"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="UInt32.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="UInt32.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="UInt32.MinValue"/> to <see
        /// cref="UInt32.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="UInt32.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="UInt32.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static UInt32 ClampToUInt32(UInt64 value)
        {
            return ClampToUInt32NoCheck(value, UInt32.MinValue, UInt32.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="Int64"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="Int64"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="Int64.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="Int64.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="Int64.MinValue"/> to <see
        /// cref="Int64.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="Int64.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="Int64.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static Int64 ClampToInt64(UInt64 value)
        {
            return ClampToInt64NoCheck(value, Int64.MinValue, Int64.MaxValue);
        }

        /// <summary>
        /// Converts an integer value to a value of type <see cref="UInt64"/>, clamping the value if
        /// it is outside the valid range.
        /// </summary>
        /// <param name="value">An integer value to be clamped.</param>
        /// <returns>
        /// The result of clamping the given value to the valid range of <see cref="UInt64"/>.
        /// <list type="table">
        /// <item>
        /// <term><see cref="UInt64.MinValue"/></term>
        /// <description>If <paramref name="value"/> is less than or equal to <see cref="UInt64.MinValue"/>.</description>
        /// </item>
        /// <item>
        /// <term><paramref name="value"/></term>
        /// <description>
        /// If <paramref name="value"/> is within the range <see cref="UInt64.MinValue"/> to <see
        /// cref="UInt64.MaxValue"/>, inclusive.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="UInt64.MaxValue"/></term>
        /// <description>If <paramref name="value"/> is greater than or equal to <see cref="UInt64.MaxValue"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static UInt64 ClampToUInt64(Int64 value)
        {
            return ClampToUInt64NoCheck(value, UInt64.MinValue, UInt64.MaxValue);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="SByte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="SByte"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static SByte Min(SByte val1, Byte val2)
        {
            return (val2 > (Byte)SByte.MaxValue) ? val1 : Math.Min(val1, (SByte)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="SByte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="SByte"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static SByte Min(Byte val1, SByte val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="Byte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Byte"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static Byte Max(Byte val1, SByte val2)
        {
            return (val2 < (SByte)Byte.MinValue) ? val1 : Math.Max(val1, (Byte)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="Byte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Byte"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static Byte Max(SByte val1, Byte val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="SByte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="SByte"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static SByte Min(SByte val1, UInt16 val2)
        {
            return (val2 > (UInt16)SByte.MaxValue) ? val1 : Math.Min(val1, (SByte)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="SByte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="SByte"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static SByte Min(UInt16 val1, SByte val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt16"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt16 Max(UInt16 val1, SByte val2)
        {
            return (val2 < (SByte)UInt16.MinValue) ? val1 : Math.Max(val1, (UInt16)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt16"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt16 Max(SByte val1, UInt16 val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="SByte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="SByte"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static SByte Min(SByte val1, UInt32 val2)
        {
            return (val2 > (UInt32)SByte.MaxValue) ? val1 : Math.Min(val1, (SByte)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="SByte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="SByte"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static SByte Min(UInt32 val1, SByte val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt32"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt32 Max(UInt32 val1, SByte val2)
        {
            return (val2 < (SByte)UInt32.MinValue) ? val1 : Math.Max(val1, (UInt32)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt32"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt32 Max(SByte val1, UInt32 val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="SByte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="SByte"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static SByte Min(SByte val1, UInt64 val2)
        {
            return (val2 > (UInt64)SByte.MaxValue) ? val1 : Math.Min(val1, (SByte)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="SByte"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="SByte"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static SByte Min(UInt64 val1, SByte val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt64"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt64 Max(UInt64 val1, SByte val2)
        {
            return (val2 < (SByte)UInt64.MinValue) ? val1 : Math.Max(val1, (UInt64)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt64"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt64 Max(SByte val1, UInt64 val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int16"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int16 Min(Int16 val1, UInt16 val2)
        {
            return (val2 > (UInt16)Int16.MaxValue) ? val1 : Math.Min(val1, (Int16)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int16"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int16 Min(UInt16 val1, Int16 val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt16"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt16 Max(UInt16 val1, Int16 val2)
        {
            return (val2 < (Int16)UInt16.MinValue) ? val1 : Math.Max(val1, (UInt16)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt16"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt16 Max(Int16 val1, UInt16 val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int16"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int16 Min(Int16 val1, UInt32 val2)
        {
            return (val2 > (UInt32)Int16.MaxValue) ? val1 : Math.Min(val1, (Int16)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int16"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int16 Min(UInt32 val1, Int16 val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt32"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt32 Max(UInt32 val1, Int16 val2)
        {
            return (val2 < (Int16)UInt32.MinValue) ? val1 : Math.Max(val1, (UInt32)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt32"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt32 Max(Int16 val1, UInt32 val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int16"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int16 Min(Int16 val1, UInt64 val2)
        {
            return (val2 > (UInt64)Int16.MaxValue) ? val1 : Math.Min(val1, (Int16)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int16"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int16"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int16 Min(UInt64 val1, Int16 val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt64"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt64 Max(UInt64 val1, Int16 val2)
        {
            return (val2 < (Int16)UInt64.MinValue) ? val1 : Math.Max(val1, (UInt64)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt64"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt64 Max(Int16 val1, UInt64 val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int32"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int32 Min(Int32 val1, UInt32 val2)
        {
            return (val2 > (UInt32)Int32.MaxValue) ? val1 : Math.Min(val1, (Int32)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int32"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int32 Min(UInt32 val1, Int32 val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt32"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt32 Max(UInt32 val1, Int32 val2)
        {
            return (val2 < (Int32)UInt32.MinValue) ? val1 : Math.Max(val1, (UInt32)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt32"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt32 Max(Int32 val1, UInt32 val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int32"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int32 Min(Int32 val1, UInt64 val2)
        {
            return (val2 > (UInt64)Int32.MaxValue) ? val1 : Math.Min(val1, (Int32)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int32"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int32"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int32 Min(UInt64 val1, Int32 val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt64"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt64 Max(UInt64 val1, Int32 val2)
        {
            return (val2 < (Int32)UInt64.MinValue) ? val1 : Math.Max(val1, (UInt64)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt64"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt64 Max(Int32 val1, UInt64 val2)
        {
            return Max(val2, val1);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int64"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int64 Min(Int64 val1, UInt64 val2)
        {
            return (val2 > (UInt64)Int64.MaxValue) ? val1 : Math.Min(val1, (Int64)val2);
        }

        /// <summary>
        /// Returns the smaller of two integers, safely narrowing to <see cref="Int64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="Int64"/> since the smaller of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is smaller</returns>
        public static Int64 Min(UInt64 val1, Int64 val2)
        {
            return Min(val2, val1);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt64"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt64 Max(UInt64 val1, Int64 val2)
        {
            return (val2 < (Int64)UInt64.MinValue) ? val1 : Math.Max(val1, (UInt64)val2);
        }

        /// <summary>
        /// Returns the larger of two integers, safely narrowing to <see cref="UInt64"/> if necessary.
        /// </summary>
        /// <remarks>
        /// This operation returns a value of type <see cref="UInt64"/> since the larger of the two
        /// operands is guaranteed to be within the valid range for that type.
        /// </remarks>
        /// <param name="val1">The first of two operands to compare</param>
        /// <param name="val2">The second of two operands to compare</param>
        /// <returns><paramref name="val1"/> or <paramref name="val2"/>, whichever is larger</returns>
        public static UInt64 Max(Int64 val1, UInt64 val2)
        {
            return Max(val2, val1);
        }
    }
}