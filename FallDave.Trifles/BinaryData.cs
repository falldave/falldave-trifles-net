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

            if (startIndex < 0 || startIndex > size)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
            if (startIndex > size - count)
            {
                throw new ArgumentOutOfRangeException("count");
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

        // Retrieve n bytes from the given list as an array. If the list is already an array,
        // return it; otherwise, copy to a new array. This allows some of our conversion
        // operations to be generalized to other IList<byte>s, such as ImmutableArray<byte>.

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
            UInt16
                b0 = (byte)(value),
                b1 = (byte)(value >> 8);

            return (UInt16)(
                (b0 << 8) |
                (b1)
                );
        }

        private static UInt32 Swapped4(UInt32 value)
        {
            UInt32
                b0 = (byte)(value),
                b1 = (byte)(value >> 8),
                b2 = (byte)(value >> 16),
                b3 = (byte)(value >> 24);

            return (UInt32)(
                (b0 << 24) |
                (b1 << 16) |
                (b2 << 8) |
                (b3)
                );
        }

        private static UInt64 Swapped8(UInt64 value)
        {
            UInt64
                b0 = (byte)(value),
                b1 = (byte)(value >> 8),
                b2 = (byte)(value >> 16),
                b3 = (byte)(value >> 24),
                b4 = (byte)(value >> 32),
                b5 = (byte)(value >> 40),
                b6 = (byte)(value >> 48),
                b7 = (byte)(value >> 56);

            return (UInt64)(
                (b0 << 56) |
                (b1 << 48) |
                (b2 << 40) |
                (b3 << 32) |
                (b4 << 24) |
                (b5 << 16) |
                (b6 << 8) |
                (b7)
                );
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
            return (Int64)Swapped((UInt64)value);
        }

        /// <summary>
        /// Returns the given 32-bit value with the bytes reversed.
        /// </summary>
        /// <param name="value">A 32-bit integer value</param>
        /// <returns>The result of a byte-swap operation on <paramref name="value"/></returns>
        public static Int32 Swapped(Int32 value)
        {
            return (Int32)Swapped((UInt32)value);
        }

        /// <summary>
        /// Returns the given 16-bit value with the bytes reversed.
        /// </summary>
        /// <param name="value">A 16-bit integer value</param>
        /// <returns>The result of a byte-swap operation on <paramref name="value"/></returns>
        public static Int16 Swapped(Int16 value)
        {
            return (Int16)Swapped((UInt16)value);
        }

        #endregion Swapped(integer type)

        #region Swapped(integer type, bool)

        /// <summary>
        /// Converts the given value from the system byte order to the specified byte order, if needed.
        /// </summary>
        /// <param name="nativeValue">
        /// A 64-bit integer value in the system byte order (as specified by <see cref="BitConverter.IsLittleEndian"/>)
        /// </param>
        /// <param name="littleEndian">
        /// <c>true</c> if the result should be in little-endian order, or <c>false</c> if the result
        /// should be in big-endian order
        /// </param>
        /// <returns>The specified value after any necessary conversion.</returns>
        public static UInt64 Swapped(UInt64 nativeValue, bool littleEndian)
        {
            return (littleEndian == BitConverter.IsLittleEndian) ? nativeValue : Swapped(nativeValue);
        }

        /// <summary>
        /// Converts the given value from the system byte order to the specified byte order, if needed.
        /// </summary>
        /// <param name="nativeValue">
        /// A 64-bit integer value in the system byte order (as specified by <see cref="BitConverter.IsLittleEndian"/>)
        /// </param>
        /// <param name="littleEndian">
        /// <c>true</c> if the result should be in little-endian order, or <c>false</c> if the result
        /// should be in big-endian order
        /// </param>
        /// <returns>The specified value after any necessary conversion.</returns>
        public static UInt32 Swapped(UInt32 nativeValue, bool littleEndian)
        {
            return (littleEndian == BitConverter.IsLittleEndian) ? nativeValue : Swapped(nativeValue);
        }

        /// <summary>
        /// Converts the given value from the system byte order to the specified byte order, if needed.
        /// </summary>
        /// <param name="nativeValue">
        /// A 64-bit integer value in the system byte order (as specified by <see cref="BitConverter.IsLittleEndian"/>)
        /// </param>
        /// <param name="littleEndian">
        /// <c>true</c> if the result should be in little-endian order, or <c>false</c> if the result
        /// should be in big-endian order
        /// </param>
        /// <returns>The specified value after any necessary conversion.</returns>
        public static UInt16 Swapped(UInt16 nativeValue, bool littleEndian)
        {
            return (littleEndian == BitConverter.IsLittleEndian) ? nativeValue : Swapped(nativeValue);
        }

        /// <summary>
        /// Converts the given value from the system byte order to the specified byte order, if needed.
        /// </summary>
        /// <param name="nativeValue">
        /// A 64-bit integer value in the system byte order (as specified by <see cref="BitConverter.IsLittleEndian"/>)
        /// </param>
        /// <param name="littleEndian">
        /// <c>true</c> if the result should be in little-endian order, or <c>false</c> if the result
        /// should be in big-endian order
        /// </param>
        /// <returns>The specified value after any necessary conversion.</returns>
        public static Int64 Swapped(Int64 nativeValue, bool littleEndian)
        {
            return (littleEndian == BitConverter.IsLittleEndian) ? nativeValue : Swapped(nativeValue);
        }

        /// <summary>
        /// Converts the given value from the system byte order to the specified byte order, if needed.
        /// </summary>
        /// <param name="nativeValue">
        /// A 64-bit integer value in the system byte order (as specified by <see cref="BitConverter.IsLittleEndian"/>)
        /// </param>
        /// <param name="littleEndian">
        /// <c>true</c> if the result should be in little-endian order, or <c>false</c> if the result
        /// should be in big-endian order
        /// </param>
        /// <returns>The specified value after any necessary conversion.</returns>
        public static Int32 Swapped(Int32 nativeValue, bool littleEndian)
        {
            return (littleEndian == BitConverter.IsLittleEndian) ? nativeValue : Swapped(nativeValue);
        }

        /// <summary>
        /// Converts the given value from the system byte order to the specified byte order, if needed.
        /// </summary>
        /// <param name="nativeValue">
        /// A 64-bit integer value in the system byte order (as specified by <see cref="BitConverter.IsLittleEndian"/>)
        /// </param>
        /// <param name="littleEndian">
        /// <c>true</c> if the result should be in little-endian order, or <c>false</c> if the result
        /// should be in big-endian order
        /// </param>
        /// <returns>The specified value after any necessary conversion.</returns>
        public static Int16 Swapped(Int16 nativeValue, bool littleEndian)
        {
            return (littleEndian == BitConverter.IsLittleEndian) ? nativeValue : Swapped(nativeValue);
        }

        #endregion Swapped(integer type, bool)
    }
}