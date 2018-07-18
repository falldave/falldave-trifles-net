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
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace FallDave.Trifles
{
    /// <summary>
    /// Extension methods for Streams and other stream-like I/O objects.
    /// </summary>
    public static partial class StreamExtensions
    {
        private static Action<byte[], int, int> WriteActionOf(Stream stream)
        {
            return (buffer, offset, count) => stream.Write(buffer, offset, count);
        }

        private static Action<byte[], int, int> WriteActionOf(BinaryWriter writer)
        {
            return (buffer, index, count) => writer.Write(buffer, index, count);
        }

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

        #region Write(... IEnumerable<byte> source ...)

        // This value must be at least 1.
        private const int DefaultWriteBufferSize = 8192;

        // Copies elements from an IList<byte> to a byte array. Special cases exist for source of
        // byte[] or ImmutableArray<byte>.

        private static void CopyToNoCheck(IList<byte> source, int sourceIndex, byte[] destination, int destinationIndex, int length)
        {
            if (source is byte[])
            {
                Array.Copy((byte[])source, sourceIndex, destination, destinationIndex, length);
            }
            else if (source is ImmutableArray<byte>)
            {
                ((ImmutableArray<byte>)source).CopyTo(sourceIndex, destination, destinationIndex, length);
            }
            else
            {
                for (int i = 0; i < length; ++i)
                {
                    destination[destinationIndex + i] = source[sourceIndex + i];
                }
            }
        }

        // Calls a write action repeatedly while copying bytes from a source through a buffer. If the
        // provided source is a byte array, the incremental copy is skipped.
        private static void WriteNoCheck(Action<byte[], int, int> writeAction, IList<byte> source, int index, int count, int bufferSize)
        {
            if (count > 0)
            {
                {
                    var byteArraySource = source as byte[];
                    if (byteArraySource != null)
                    {
                        writeAction(byteArraySource, index, count);
                        return;
                    }
                }

                if (bufferSize <= 0)
                {
                    bufferSize = DefaultWriteBufferSize;
                }

                if (bufferSize > count)
                {
                    bufferSize = count;
                }

                WriteViaArray(source, index, count, writeAction, new byte[bufferSize]);
            }
        }

        // Calls a write action repeatedly while copying bytes from a sequential source through a
        // buffer. If the provided source is an IList<byte>, the algorithm used may be random-access instead.
        private static void WriteNoCheck(Action<byte[], int, int> writeAction, IEnumerable<byte> source, int bufferSize)
        {
            {
                var ilistSource = source as IList<byte>;
                if (ilistSource != null)
                {
                    WriteNoCheck(writeAction, ilistSource, 0, ilistSource.Count, bufferSize);
                    return;
                }
            }

            if (bufferSize <= 0)
            {
                bufferSize = DefaultWriteBufferSize;
            }

            WriteViaArray(source, writeAction, new byte[bufferSize]);
        }

        private static void WriteViaArray(IList<byte> source, int index, int count, Action<byte[], int, int> writeAction, byte[] copyBuffer)
        {
            while (count > 0)
            {
                int countThisPass = Math.Min(count, copyBuffer.Length);
                CopyToNoCheck(source, index, copyBuffer, 0, countThisPass);
                index += countThisPass;
                count -= countThisPass;
                writeAction(copyBuffer, 0, countThisPass);
            }
        }

        private static void WriteViaArray(IEnumerable<byte> source, Action<byte[], int, int> writeAction, byte[] copyBuffer)
        {
            int i = 0;
            int bufferSize = copyBuffer.Length;

            foreach (var b in source)
            {
                copyBuffer[i] = b;
                ++i;

                if (i >= bufferSize)
                {
                    int countThisPass = i;
                    i = 0;
                    writeAction(copyBuffer, 0, countThisPass);
                }
            }

            if (i > 0)
            {
                writeAction(copyBuffer, 0, i);
            }
        }

        /// <summary>
        /// Writes blocks of bytes to the current stream using data read from a list.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="source"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this Stream stream, IList<byte> source, int offset, int count, int bufferSize = 0)
        {
            Checker.NotNull(stream, "stream");
            Checker.NotNull(source, "source");
            Checker.SpanInRange(source.Count, offset, count, "offset", "count");
            WriteNoCheck(WriteActionOf(stream), source, offset, count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current stream using data read from a list.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this Stream stream, IList<byte> source, int bufferSize = 0)
        {
            Checker.NotNull(stream, "stream");
            Checker.NotNull(source, "source");
            WriteNoCheck(WriteActionOf(stream), source, 0, source.Count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current stream using data read from a sequence.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this Stream stream, IEnumerable<byte> source, int bufferSize = 0)
        {
            Checker.NotNull(stream, "stream");
            Checker.NotNull(source, "source");
            WriteNoCheck(WriteActionOf(stream), source, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current writer using data read from a list.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="source"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this BinaryWriter writer, IList<byte> source, int offset, int count, int bufferSize = 0)
        {
            Checker.NotNull(writer, "writer");
            Checker.NotNull(source, "source");
            Checker.SpanInRange(source.Count, offset, count, "offset", "count");
            WriteNoCheck(WriteActionOf(writer), source, offset, count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current writer using data read from a list.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this BinaryWriter writer, IList<byte> source, int bufferSize = 0)
        {
            Checker.NotNull(writer, "writer");
            Checker.NotNull(source, "source");
            WriteNoCheck(WriteActionOf(writer), source, 0, source.Count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current writer using data read from a sequence.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        public static void Write(this BinaryWriter writer, IEnumerable<byte> source, int bufferSize = 0)
        {
            Checker.NotNull(writer, "writer");
            Checker.NotNull(source, "source");
            WriteNoCheck(WriteActionOf(writer), source, bufferSize);
        }

        #endregion Write(... IEnumerable<byte> source ...)
    }
}