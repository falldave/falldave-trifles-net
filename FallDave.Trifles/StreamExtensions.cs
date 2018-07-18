//-----------------------------------------------------------------------
// <copyright file="StreamExtensions.cs" company="falldave">
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
    /// Extension methods for Streams and other stream-like I/O objects.
    /// </summary>
    public static partial class StreamExtensions
    {
        #region ToTriflesByteWriter

        /// <summary>
        /// Returns this sink as an <see cref="ITriflesByteWriter"/>.
        /// </summary>
        /// <param name="sink">A sink to convert.</param>
        /// <returns><paramref name="sink"/> as an <see cref="ITriflesByteWriter"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sink"/> is <c>null</c>.</exception>
        public static ITriflesByteWriter ToTriflesByteWriter(this ITriflesByteWriter sink)
        {
            return TriflesByteWriterImpl.ToTriflesByteWriter(sink);
        }

        /// <summary>
        /// Creates a new <see cref="ITriflesByteWriter"/> backed by this sink.
        /// </summary>
        /// <param name="sink">A sink to convert.</param>
        /// <returns>
        /// A new <see cref="ITriflesByteWriter"/> whose <see cref="ITriflesByteWriter.Write(byte[],
        /// int, int)"/> method is implemented directly by the sink's <see cref="Stream.Write(byte[],
        /// int, int)"/> method.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="sink"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="sink"/> does not support writing.</exception>
        public static ITriflesByteWriter ToTriflesByteWriter(this Stream sink)
        {
            return TriflesByteWriterImpl.ToTriflesByteWriter(sink);
        }

        /// <summary>
        /// Creates a new <see cref="ITriflesByteWriter"/> backed by this sink.
        /// </summary>
        /// <param name="sink">A sink to convert.</param>
        /// <returns>
        /// A new <see cref="ITriflesByteWriter"/> whose <see cref="ITriflesByteWriter.Write(byte[],
        /// int, int)"/> method is implemented directly by the sink's <see
        /// cref="BinaryWriter.Write(byte[], int, int)"/> method.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="sink"/> is <c>null</c>.</exception>
        public static ITriflesByteWriter ToTriflesByteWriter(this BinaryWriter sink)
        {
            return TriflesByteWriterImpl.ToTriflesByteWriter(sink);
        }

        #endregion ToTriflesByteWriter

        #region ToTriflesByteReader

        /// <summary>
        /// Returns this source as an <see cref="ITriflesByteReader"/>.
        /// </summary>
        /// <param name="source">A source to convert.</param>
        /// <returns><paramref name="source"/> as an <see cref="ITriflesByteReader"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static ITriflesByteReader ToTriflesByteReader(this ITriflesByteReader source)
        {
            return TriflesByteReaderImpl.ToTriflesByteReader(source);
        }

        /// <summary>
        /// Creates a new <see cref="ITriflesByteReader"/> backed by this source.
        /// </summary>
        /// <param name="source">A source to convert.</param>
        /// <returns>
        /// A new <see cref="ITriflesByteReader"/> whose <see cref="ITriflesByteReader.Read(byte[],
        /// int, int)"/> method is implemented directly by the source's <see
        /// cref="Stream.Read(byte[], int, int)"/> method.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="source"/> does not support reading.
        /// </exception>
        public static ITriflesByteReader ToTriflesByteReader(this Stream source)
        {
            return TriflesByteReaderImpl.ToTriflesByteReader(source);
        }

        /// <summary>
        /// Creates a new <see cref="ITriflesByteReader"/> backed by this source.
        /// </summary>
        /// <param name="source">A source to convert.</param>
        /// <returns>
        /// A new <see cref="ITriflesByteReader"/> whose <see cref="ITriflesByteReader.Read(byte[],
        /// int, int)"/> method is implemented directly by the source's <see
        /// cref="BinaryReader.Read(byte[], int, int)"/> method.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static ITriflesByteReader ToTriflesByteReader(this BinaryReader source)
        {
            return TriflesByteReaderImpl.ToTriflesByteReader(source);
        }

        #endregion ToTriflesByteReader

        #region Write from byte enumerable

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a list.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this ITriflesByteWriter sink, IList<byte> source, int offset, int count, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, offset, count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a list.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this ITriflesByteWriter sink, IList<byte> source, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a sequence.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this ITriflesByteWriter sink, IEnumerable<byte> source, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a list.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this Stream sink, IList<byte> source, int offset, int count, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, offset, count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a list.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this Stream sink, IList<byte> source, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a sequence.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this Stream sink, IEnumerable<byte> source, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a list.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this BinaryWriter sink, IList<byte> source, int offset, int count, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, offset, count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a list.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this BinaryWriter sink, IList<byte> source, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a sequence.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this BinaryWriter sink, IEnumerable<byte> source, int bufferSize = 0)
        {
            TriflesByteWriterImpl.Write(sink.ToTriflesByteWriter(), source, bufferSize);
        }

        #endregion Write from byte enumerable
    }
}