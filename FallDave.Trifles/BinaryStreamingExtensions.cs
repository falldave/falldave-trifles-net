//-----------------------------------------------------------------------
// <copyright file="BinaryStreamingExtensions.cs" company="falldave">
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
using System.IO;
using System.Linq;

namespace FallDave.Trifles
{
    /// <summary>
    /// Extension methods for BinaryReader and BinaryWriter.
    /// </summary>
    public static class BinaryStreamingExtensions
    {
        #region Read*Endian(BinaryReader, bool)

        // Note that BinaryReader.Read*() uses little-endian order.
        // Swapping the result gives the big-endian value.

        /// <summary>
        /// Reads 2 bytes from the current source and interprets them as a 2-byte unsigned integer
        /// using the specified byte order.
        /// </summary>
        /// <remarks>The file position is not restored after an unsuccessful read.</remarks>
        /// <param name="source">The source from which to read.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 2-byte unsigned integer read from this source.</returns>
        /// <seealso cref="BinaryReader.ReadUInt16()"/>
        public static UInt16 ReadUInt16Endian(this BinaryReader source, bool littleEndian)
        {
            var le = source.ReadUInt16();
            return littleEndian ? le : BinaryData.Swapped(le);
        }

        /// <summary>
        /// Reads 4 bytes from the current source and interprets them as a 4-byte unsigned integer
        /// using the specified byte order.
        /// </summary>
        /// <remarks>The file position is not restored after an unsuccessful read.</remarks>
        /// <param name="source">The source from which to read.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 4-byte unsigned integer read from this source.</returns>
        /// <seealso cref="BinaryReader.ReadUInt32()"/>
        public static UInt32 ReadUInt32Endian(this BinaryReader source, bool littleEndian)
        {
            var le = source.ReadUInt32();
            return littleEndian ? le : BinaryData.Swapped(le);
        }

        /// <summary>
        /// Reads 8 bytes from the current source and interprets them as a 8-byte unsigned integer
        /// using the specified byte order.
        /// </summary>
        /// <remarks>The file position is not restored after an unsuccessful read.</remarks>
        /// <param name="source">The source from which to read.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 8-byte unsigned integer read from this source.</returns>
        /// <seealso cref="BinaryReader.ReadUInt64()"/>
        public static UInt64 ReadUInt64Endian(this BinaryReader source, bool littleEndian)
        {
            var le = source.ReadUInt64();
            return littleEndian ? le : BinaryData.Swapped(le);
        }

        /// <summary>
        /// Reads 2 bytes from the current source and interprets them as a 2-byte signed integer
        /// using the specified byte order.
        /// </summary>
        /// <remarks>The file position is not restored after an unsuccessful read.</remarks>
        /// <param name="source">The source from which to read.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 2-byte unsigned integer read from this source.</returns>
        /// <seealso cref="ReadUInt16Endian(BinaryReader, bool)"/>
        public static Int16 ReadInt16Endian(this BinaryReader source, bool littleEndian)
        {
            return (Int16)ReadUInt16Endian(source, littleEndian);
        }

        /// <summary>
        /// Reads 4 bytes from the current source and interprets them as a 4-byte signed integer
        /// using the specified byte order.
        /// </summary>
        /// <remarks>The file position is not restored after an unsuccessful read.</remarks>
        /// <param name="source">The source from which to read.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 4-byte unsigned integer read from this source.</returns>
        /// <seealso cref="ReadUInt32Endian(BinaryReader, bool)"/>
        public static Int32 ReadInt32Endian(this BinaryReader source, bool littleEndian)
        {
            return (Int32)ReadUInt32Endian(source, littleEndian);
        }

        /// <summary>
        /// Reads 8 bytes from the current source and interprets them as a 8-byte signed integer
        /// using the specified byte order.
        /// </summary>
        /// <remarks>The file position is not restored after an unsuccessful read.</remarks>
        /// <param name="source">The source from which to read.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 8-byte unsigned integer read from this source.</returns>
        /// <seealso cref="ReadUInt64Endian(BinaryReader, bool)"/>
        public static Int64 ReadInt64Endian(this BinaryReader source, bool littleEndian)
        {
            return (Int64)ReadUInt64Endian(source, littleEndian);
        }

        /// <summary>
        /// Reads 2 bytes from the current source and interprets them as a 2-byte character using the
        /// specified byte order.
        /// </summary>
        /// <remarks>The file position is not restored after an unsuccessful read.</remarks>
        /// <param name="source">The source from which to read.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to interpret the value in little-endian order, or <c>false</c> to interpret
        /// the value in big-endian order.
        /// </param>
        /// <returns>A 2-byte character read from this source.</returns>
        /// <seealso cref="ReadUInt16Endian(BinaryReader, bool)"/>
        public static Char ReadCharEndian(this BinaryReader source, bool littleEndian)
        {
            return (Char)ReadUInt16Endian(source, littleEndian);
        }

        #endregion Read*Endian(BinaryReader, bool)

        // Note that BinaryWriter.Write*() uses little-endian order.

        #region WriteEndian(BinaryWriter, integer type, bool)

        /// <summary>
        /// Converts a 16-bit unsigned integer value to 2 bytes using the specified byte order and
        /// writes the result to the current destination.
        /// </summary>
        /// <param name="destination">The destination to which to write.</param>
        /// <param name="value">The value to be written.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to represent the value in little-endian order, or <c>false</c> to represent
        /// the value in big-endian order.
        /// </param>
        /// <seealso cref="BinaryWriter.Write(UInt16)"/>
        public static void WriteEndian(this BinaryWriter destination, UInt16 value, bool littleEndian)
        {
            destination.Write(littleEndian ? value : BinaryData.Swapped(value));
        }

        /// <summary>
        /// Converts a 32-bit unsigned integer value to 4 bytes using the specified byte order and
        /// writes the result to the current destination.
        /// </summary>
        /// <param name="destination">The destination to which to write.</param>
        /// <param name="value">The value to be written.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to represent the value in little-endian order, or <c>false</c> to represent
        /// the value in big-endian order.
        /// </param>
        /// <seealso cref="BinaryWriter.Write(UInt32)"/>
        public static void WriteEndian(this BinaryWriter destination, UInt32 value, bool littleEndian)
        {
            destination.Write(littleEndian ? value : BinaryData.Swapped(value));
        }

        /// <summary>
        /// Converts a 64-bit unsigned integer value to 8 bytes using the specified byte order and
        /// writes the result to the current destination.
        /// </summary>
        /// <param name="destination">The destination to which to write.</param>
        /// <param name="value">The value to be written.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to represent the value in little-endian order, or <c>false</c> to represent
        /// the value in big-endian order.
        /// </param>
        /// <seealso cref="BinaryWriter.Write(UInt64)"/>
        public static void WriteEndian(this BinaryWriter destination, UInt64 value, bool littleEndian)
        {
            destination.Write(littleEndian ? value : BinaryData.Swapped(value));
        }

        /// <summary>
        /// Converts a 16-bit signed integer value to 2 bytes using the specified byte order and
        /// writes the result to the current destination.
        /// </summary>
        /// <param name="destination">The destination to which to write.</param>
        /// <param name="value">The value to be written.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to represent the value in little-endian order, or <c>false</c> to represent
        /// the value in big-endian order.
        /// </param>
        /// <seealso cref="WriteEndian(BinaryWriter, UInt16, bool)"/>
        public static void WriteEndian(this BinaryWriter destination, Int16 value, bool littleEndian)
        {
            WriteEndian(destination, (UInt16)value, littleEndian);
        }

        /// <summary>
        /// Converts a 32-bit signed integer value to 4 bytes using the specified byte order and
        /// writes the result to the current destination.
        /// </summary>
        /// <param name="destination">The destination to which to write.</param>
        /// <param name="value">The value to be written.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to represent the value in little-endian order, or <c>false</c> to represent
        /// the value in big-endian order.
        /// </param>
        /// <seealso cref="WriteEndian(BinaryWriter, UInt32, bool)"/>
        public static void WriteEndian(this BinaryWriter destination, Int32 value, bool littleEndian)
        {
            WriteEndian(destination, (UInt32)value, littleEndian);
        }

        /// <summary>
        /// Converts a 64-bit signed integer value to 8 bytes using the specified byte order and
        /// writes the result to the current destination.
        /// </summary>
        /// <param name="destination">The destination to which to write.</param>
        /// <param name="value">The value to be written.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to represent the value in little-endian order, or <c>false</c> to represent
        /// the value in big-endian order.
        /// </param>
        /// <seealso cref="WriteEndian(BinaryWriter, UInt64, bool)"/>
        public static void WriteEndian(this BinaryWriter destination, Int64 value, bool littleEndian)
        {
            WriteEndian(destination, (UInt64)value, littleEndian);
        }

        /// <summary>
        /// Converts a 16-bit character value to 2 bytes using the specified byte order and writes
        /// the result to the current destination.
        /// </summary>
        /// <param name="destination">The destination to which to write.</param>
        /// <param name="value">The value to be written.</param>
        /// <param name="littleEndian">
        /// <c>true</c> to represent the value in little-endian order, or <c>false</c> to represent
        /// the value in big-endian order.
        /// </param>
        /// <seealso cref="WriteEndian(BinaryWriter, UInt16, bool)"/>
        public static void WriteEndian(this BinaryWriter destination, Char value, bool littleEndian)
        {
            WriteEndian(destination, (UInt16)value, littleEndian);
        }

        #endregion WriteEndian(BinaryWriter, integer type, bool)
    }
}