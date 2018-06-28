//-----------------------------------------------------------------------
// <copyright file="BinaryData.cs" company="falldave">
//
// Written in 2018 by David McFall
//
// To the extent possible under law, the author(s) have dedicated all copyright
// and related and neighboring rights to this software to the public domain
// worldwide. This software is distributed without any warranty.
//
// You should have received a copy of the CC0 Public Domain Dedication along
// with this software. If not, see
// [http://creativecommons.org/publicdomain/zero/1.0/].
//
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace FallDave.Trifles
{
    /// <summary>
    /// Utilities for binary representations of data.
    /// </summary>
    public static class BinaryData
    {
        // Pre-check parameters for Get*Bytes()
        private static void CheckGetNBytes(IList<byte> value, int startIndex, int count)
        {
            Checker.NotNull(value, "value");
            int size = value.Count;

            if (startIndex < 0 || startIndex > size - count)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
        }

        // Pre-check parameters for Swap*Bytes()
        private static void CheckSwapN(byte[] buffer, int startIndex, int length)
        {
            Checker.NotNull(buffer, "buffer");
            int size = buffer.Length;

            if (startIndex < 0 || startIndex > size)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
            if (startIndex > size - length)
            {
                throw new ArgumentOutOfRangeException("length");
            }
        }

        #region Get*Bytes()

        // Retrieve n bytes from the given list as an array. If the list is
        // already an array, return it; otherwise, copy to a new array. This
        // allows some of our conversion operations to be generalized to other
        // IList<byte>s, such as ImmutableArray<byte>.
        //
        // If a new array is created, startIndex is set to 0 to indicate the
        // beginning of the new array. If the value is already an array,
        // startIndex is left unchanged.

        private static byte[] Get2Bytes(IList<byte> value, ref int startIndex)
        {
            CheckGetNBytes(value, startIndex, 2);

            {
                var a = value as byte[];
                if (a != null)
                {
                    return a;
                }
            }

            var r = new byte[]
            {
                value[startIndex],
                value[startIndex + 1]
            };
            startIndex = 0;
            return r;
        }

        private static byte[] Get4Bytes(IList<byte> value, ref int startIndex)
        {
            CheckGetNBytes(value, startIndex, 4);

            {
                var a = value as byte[];
                if (a != null)
                {
                    return a;
                }
            }

            var r = new byte[]
            {
                value[startIndex],
                value[startIndex + 1],
                value[startIndex + 2],
                value[startIndex + 3]
            };
            startIndex = 0;
            return r;
        }

        private static byte[] Get8Bytes(IList<byte> value, ref int startIndex)
        {
            CheckGetNBytes(value, startIndex, 8);

            {
                var a = value as byte[];
                if (a != null)
                {
                    return a;
                }
            }

            var r = new byte[]
            {
                value[startIndex],
                value[startIndex + 1],
                value[startIndex + 2],
                value[startIndex + 3],
                value[startIndex + 4],
                value[startIndex + 5],
                value[startIndex + 6],
                value[startIndex + 7]
            };
            startIndex = 0;
            return r;
        }

        #endregion Get*Bytes()

        #region Pack*()

        /// <summary>
        /// Composes a single 16-bit unsigned integer value from 2 byte values.
        /// </summary>
        /// <param name="b0">A byte value representing the low 8 bits of the result.</param>
        /// <param name="b1">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b0"/>.
        /// </param>
        /// <returns>A 16-bit unsigned value composed from the given byte values.</returns>
        public static UInt16 PackUInt16(byte b0, byte b1)
        {
            return (UInt16)(b0 | (UInt16)(b1 << 8));
        }

        /// <summary>
        /// Composes a single 32-bit unsigned integer value from 4 byte values.
        /// </summary>
        /// <param name="b0">A byte value representing the lowest 8 bits of an integer value.</param>
        /// <param name="b1">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b0"/>.
        /// </param>
        /// <param name="b2">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b1"/>.
        /// </param>
        /// <param name="b3">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b2"/>.
        /// </param>
        /// <returns>A 32-bit unsigned value composed from the given byte values.</returns>
        public static UInt32 PackUInt32(byte b0, byte b1, byte b2, byte b3)
        {
            return (UInt32)(b0 | b1 << 8 | b2 << 16 | b3 << 24);
        }

        /// <summary>
        /// Composes a single 64-bit unsigned integer value from 8 byte values.
        /// </summary>
        /// <param name="b0">A byte value representing the lowest 8 bits of an integer value.</param>
        /// <param name="b1">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b0"/>.
        /// </param>
        /// <param name="b2">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b1"/>.
        /// </param>
        /// <param name="b3">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b2"/>.
        /// </param>
        /// <param name="b4">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b3"/>.
        /// </param>
        /// <param name="b5">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b4"/>.
        /// </param>
        /// <param name="b6">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b5"/>.
        /// </param>
        /// <param name="b7">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b6"/>.
        /// </param>
        /// <returns>A 64-bit unsigned value composed from the given byte values.</returns>
        private static UInt64 PackUInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            return b0 | (UInt64)b1 << 8 | (UInt64)b2 << 16 | (UInt64)b3 << 24 | (UInt64)b4 << 32 | (UInt64)b5 << 40 | (UInt64)b6 << 48 | (UInt64)b7 << 56;
        }

        /// <summary>
        /// Composes a single 16-bit signed integer value from 2 byte values.
        /// </summary>
        /// <param name="b0">A byte value representing the low 8 bits of the result.</param>
        /// <param name="b1">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b0"/>.
        /// </param>
        /// <returns>A 16-bit signed value composed from the given byte values.</returns>
        public static Int16 PackInt16(byte b0, byte b1)
        {
            return (Int16)PackUInt16(b0, b1);
        }

        /// <summary>
        /// Composes a single 32-bit signed integer value from 4 byte values.
        /// </summary>
        /// <param name="b0">A byte value representing the lowest 8 bits of an integer value.</param>
        /// <param name="b1">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b0"/>.
        /// </param>
        /// <param name="b2">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b1"/>.
        /// </param>
        /// <param name="b3">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b2"/>.
        /// </param>
        /// <returns>A 32-bit signed value composed from the given byte values.</returns>
        public static Int32 PackInt32(byte b0, byte b1, byte b2, byte b3)
        {
            return (Int32)PackUInt32(b0, b1, b2, b3);
        }

        /// <summary>
        /// Composes a single 64-bit signed integer value from 8 byte values.
        /// </summary>
        /// <param name="b0">A byte value representing the lowest 8 bits of an integer value.</param>
        /// <param name="b1">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b0"/>.
        /// </param>
        /// <param name="b2">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b1"/>.
        /// </param>
        /// <param name="b3">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b2"/>.
        /// </param>
        /// <param name="b4">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b3"/>.
        /// </param>
        /// <param name="b5">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b4"/>.
        /// </param>
        /// <param name="b6">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b5"/>.
        /// </param>
        /// <param name="b7">
        /// A byte value representing the 8 bits of the result immediately higher than those
        /// represented by <paramref name="b6"/>.
        /// </param>
        /// <returns>A 64-bit signed value composed from the given byte values.</returns>
        private static Int64 PackInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            return (Int64)PackUInt64(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        #endregion Pack*()

        #region ToU*NoCheck(IList<byte>, int, bool)

        // Calls Pack*() with bytes read from a list.

        private static ushort ToUInt16NoCheck(IList<byte> value, int startIndex, bool littleEndian)
        {
            return littleEndian ?
                PackUInt16(
                    value[startIndex],
                    value[startIndex + 1]) :
                PackUInt16(
                    value[startIndex + 1],
                    value[startIndex]);
        }

        private static uint ToUInt32NoCheck(IList<byte> value, int startIndex, bool littleEndian)
        {
            return littleEndian ?
                PackUInt32(
                    value[startIndex],
                    value[startIndex + 1],
                    value[startIndex + 2],
                    value[startIndex + 3]) :
                PackUInt32(
                    value[startIndex + 3],
                    value[startIndex + 2],
                    value[startIndex + 1],
                    value[startIndex]);
        }

        private static ulong ToUInt64NoCheck(IList<byte> value, int startIndex, bool littleEndian)
        {
            return littleEndian ?
                PackUInt64(
                    value[startIndex],
                    value[startIndex + 1],
                    value[startIndex + 2],
                    value[startIndex + 3],
                    value[startIndex + 4],
                    value[startIndex + 5],
                    value[startIndex + 6],
                    value[startIndex + 7]) :
                PackUInt64(
                    value[startIndex + 7],
                    value[startIndex + 6],
                    value[startIndex + 5],
                    value[startIndex + 4],
                    value[startIndex + 3],
                    value[startIndex + 2],
                    value[startIndex + 1],
                    value[startIndex]);
        }

        #endregion ToU*NoCheck(IList<byte>, int, bool)

        #region Swap*NoCheck()

        // Reverse the order of n bytes in the buffer.

        private static void Swap2NoCheck(byte[] buffer, int startIndex = 0)
        {
            byte
                b0 = buffer[startIndex],
                b1 = buffer[startIndex + 1];

            buffer[startIndex] = b1;
            buffer[startIndex + 1] = b0;
        }

        private static void Swap4NoCheck(byte[] buffer, int startIndex = 0)
        {
            byte
                b0 = buffer[startIndex],
                b1 = buffer[startIndex + 1],
                b2 = buffer[startIndex + 2],
                b3 = buffer[startIndex + 3];

            buffer[startIndex] = b3;
            buffer[startIndex + 1] = b2;
            buffer[startIndex + 2] = b1;
            buffer[startIndex + 3] = b0;
        }

        private static void Swap8NoCheck(byte[] buffer, int startIndex = 0)
        {
            byte
                b0 = buffer[startIndex],
                b1 = buffer[startIndex + 1],
                b2 = buffer[startIndex + 2],
                b3 = buffer[startIndex + 3],
                b4 = buffer[startIndex + 4],
                b5 = buffer[startIndex + 5],
                b6 = buffer[startIndex + 6],
                b7 = buffer[startIndex + 7];

            buffer[startIndex] = b7;
            buffer[startIndex + 1] = b6;
            buffer[startIndex + 2] = b5;
            buffer[startIndex + 3] = b4;
            buffer[startIndex + 4] = b3;
            buffer[startIndex + 5] = b2;
            buffer[startIndex + 6] = b1;
            buffer[startIndex + 7] = b0;
        }

        #endregion Swap*NoCheck()

        #region Swapped*()

        // Get the swapped value of the given unsigned integer.

        private static UInt16 Swapped2(UInt16 value)
        {
            return (UInt16)(value << 8 | value >> 8);
        }

        private static UInt32 Swapped4(UInt32 value)
        {
            return value << 24 |
                (value << 8 & 0x00FF0000u) |
                (value >> 8 & 0x0000FF00u) |
                value >> 24;
        }

        private static UInt64 Swapped8(UInt64 value)
        {
            return value << 56 |
                (value << 40 & 0x00FF000000000000ul) |
                (value << 24 & 0x0000FF0000000000ul) |
                (value << 8 & 0x000000FF00000000ul) |
                (value >> 8 & 0x00000000FF000000ul) |
                (value >> 24 & 0x0000000000FF0000ul) |
                (value >> 40 & 0x000000000000FF00ul) |
                value >> 56;
        }

        #endregion Swapped*()

        #region Swap*()

        /// <summary>
        /// Reverses the 2 bytes at the indicated location.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        public static void Swap2(byte[] buffer, int startIndex = 0)
        {
            CheckSwapN(buffer, startIndex, 2);
            Swap2NoCheck(buffer, startIndex);
        }

        /// <summary>
        /// Reverses the 4 bytes at the indicated location.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        public static void Swap4(byte[] buffer, int startIndex = 0)
        {
            CheckSwapN(buffer, startIndex, 4);
            Swap4NoCheck(buffer, startIndex);
        }

        /// <summary>
        /// Reverses the 8 bytes at the indicated location.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        public static void Swap8(byte[] buffer, int startIndex = 0)
        {
            CheckSwapN(buffer, startIndex, 8);
            Swap8NoCheck(buffer, startIndex);
        }

        #endregion Swap*()

        #region Swapped(integer type)

        /// <summary>
        /// Returns the given 64-bit value with the bytes reversed.
        /// </summary>
        /// <param name="value">A 64-bit integer value</param>
        /// <returns>The result of a byte-swap operation on <paramref name="value"/></returns>
        public static UInt64 Swapped(UInt64 value)
        {
            return Swapped8(value);
        }

        /// <summary>
        /// Returns the given 32-bit value with the bytes reversed.
        /// </summary>
        /// <param name="value">A 32-bit integer value</param>
        /// <returns>The result of a byte-swap operation on <paramref name="value"/></returns>
        public static UInt32 Swapped(UInt32 value)
        {
            return Swapped4(value);
        }

        /// <summary>
        /// Returns the given 16-bit value with the bytes reversed.
        /// </summary>
        /// <param name="value">A 16-bit integer value</param>
        /// <returns>The result of a byte-swap operation on <paramref name="value"/></returns>
        public static UInt16 Swapped(UInt16 value)
        {
            return Swapped2(value);
        }

        /// <summary>
        /// Returns the given 64-bit value with the bytes reversed.
        /// </summary>
        /// <param name="value">A 64-bit integer value</param>
        /// <returns>The result of a byte-swap operation on <paramref name="value"/></returns>
        public static Int64 Swapped(Int64 value)
        {
            return (Int64)Swapped8((UInt64)value);
        }

        /// <summary>
        /// Returns the given 32-bit value with the bytes reversed.
        /// </summary>
        /// <param name="value">A 32-bit integer value</param>
        /// <returns>The result of a byte-swap operation on <paramref name="value"/></returns>
        public static Int32 Swapped(Int32 value)
        {
            return (Int32)Swapped4((UInt32)value);
        }

        /// <summary>
        /// Returns the given 16-bit value with the bytes reversed.
        /// </summary>
        /// <param name="value">A 16-bit integer value</param>
        /// <returns>The result of a byte-swap operation on <paramref name="value"/></returns>
        public static Int16 Swapped(Int16 value)
        {
            return (Int16)Swapped2((UInt16)value);
        }

        #endregion Swapped(integer type)

        #region To*(IList<byte>, int, bool)

        /// <summary>
        /// Returns a Unicode character converted from 2 bytes at a specified position in the given
        /// list, using the specified byte order.
        /// </summary>
        /// <param name="value">A list of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A Unicode character formed by the specified data.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref
        /// name="value"/> minus 2.
        /// </exception>
        public static Char ToChar(IList<byte> value, int startIndex, bool littleEndian = true)
        {
            return (Char)ToUInt16(value, startIndex, littleEndian);
        }

        /// <summary>
        /// Returns a 16-bit signed integer converted from 2 bytes at a specified position in the
        /// given list, using the specified byte order.
        /// </summary>
        /// <param name="value">A list of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 16-bit signed integer formed by the specified data.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref
        /// name="value"/> minus 2.
        /// </exception>
        public static Int16 ToInt16(IList<byte> value, int startIndex, bool littleEndian = true)
        {
            return (Int16)ToUInt16(value, startIndex, littleEndian);
        }

        /// <summary>
        /// Returns a 32-bit signed integer converted from 4 bytes at a specified position in the
        /// given list, using the specified byte order.
        /// </summary>
        /// <param name="value">A list of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 32-bit signed integer formed by the specified data.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref
        /// name="value"/> minus 4.
        /// </exception>
        public static Int32 ToInt32(IList<byte> value, int startIndex, bool littleEndian = true)
        {
            return (Int32)ToUInt32(value, startIndex, littleEndian);
        }

        /// <summary>
        /// Returns a 64-bit signed integer converted from 8 bytes at a specified position in the
        /// given list, using the specified byte order.
        /// </summary>
        /// <param name="value">A list of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 64-bit signed integer formed by the specified data.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref
        /// name="value"/> minus 8.
        /// </exception>
        public static Int64 ToInt64(IList<byte> value, int startIndex, bool littleEndian = true)
        {
            return (Int64)ToUInt64(value, startIndex, littleEndian);
        }

        /// <summary>
        /// Returns a 16-bit unsigned integer converted from 2 bytes at a specified position in the
        /// given list, using the specified byte order.
        /// </summary>
        /// <param name="value">A list of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 16-bit unsigned integer formed by the specified data.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref
        /// name="value"/> minus 2.
        /// </exception>
        public static UInt16 ToUInt16(IList<byte> value, int startIndex, bool littleEndian = true)
        {
            CheckGetNBytes(value, startIndex, 2);

            return ToUInt16NoCheck(value, startIndex, littleEndian);
        }

        /// <summary>
        /// Returns a 32-bit unsigned integer converted from 4 bytes at a specified position in the
        /// given list, using the specified byte order.
        /// </summary>
        /// <param name="value">A list of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 32-bit unsigned integer formed by the specified data.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref
        /// name="value"/> minus 4.
        /// </exception>
        public static UInt32 ToUInt32(IList<byte> value, int startIndex, bool littleEndian = true)
        {
            CheckGetNBytes(value, startIndex, 4);

            return ToUInt32NoCheck(value, startIndex, littleEndian);
        }

        /// <summary>
        /// Returns a 64-bit unsigned integer converted from 8 bytes at a specified position in the
        /// given list, using the specified byte order.
        /// </summary>
        /// <param name="value">A list of bytes.</param>
        /// <param name="startIndex">The starting position within <paramref name="value"/>.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 64-bit unsigned integer formed by the specified data.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is less than zero or greater than the length of <paramref
        /// name="value"/> minus 8.
        /// </exception>
        public static UInt64 ToUInt64(IList<byte> value, int startIndex, bool littleEndian = true)
        {
            CheckGetNBytes(value, startIndex, 8);

            return ToUInt64NoCheck(value, startIndex, littleEndian);
        }

        #endregion To*(IList<byte>, int, bool)
    }
}