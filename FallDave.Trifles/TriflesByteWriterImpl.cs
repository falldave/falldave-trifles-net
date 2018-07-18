//-----------------------------------------------------------------------
// <copyright file="TriflesByteReaderImpl.cs" company="falldave">
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
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FallDave.Trifles
{
    internal static class TriflesByteWriterImpl
    {
        // This value must be at least 1.
        private const int DefaultWriteBufferSize = 8192;

        #region Write from enumerable of bytes

        // Calls a write action repeatedly while copying bytes from a source list through a buffer.
        // If the provided source is a byte array, no intermediate buffer is created.
        private static void WriteNoCheck(ITriflesByteWriter sink, IList<byte> source, int index, int count, int bufferSize)
        {
            if (count > 0)
            {
                {
                    var byteArraySource = source as byte[];
                    if (byteArraySource != null)
                    {
                        sink.Write(byteArraySource, index, count);
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

                source.BufferViaArray(index, count, sink.Write, new byte[bufferSize]);
            }
        }

        // Calls a write action repeatedly while copying bytes from a source enumerable through a
        // buffer. If the provided source is an IList<byte>, the algorithm used may be random-access instead.
        private static void WriteNoCheck(ITriflesByteWriter sink, IEnumerable<byte> source, int bufferSize)
        {
            {
                var ilistSource = source as IList<byte>;
                if (ilistSource != null)
                {
                    WriteNoCheck(sink, ilistSource, 0, ilistSource.Count, bufferSize);
                    return;
                }
            }

            if (bufferSize <= 0)
            {
                bufferSize = DefaultWriteBufferSize;
            }

            source.BufferViaArray(sink.Write, new byte[bufferSize]);
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
        internal static void Write(ITriflesByteWriter sink, IList<byte> source, int offset, int count, int bufferSize = 0)
        {
            Checker.NotNull(sink, "sink");
            Checker.NotNull(source, "source");
            Checker.SpanInRange(source.Count, offset, count, "offset", "count");
            WriteNoCheck(sink, source, offset, count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a list.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        internal static void Write(ITriflesByteWriter sink, IList<byte> source, int bufferSize = 0)
        {
            Checker.NotNull(sink, "sink");
            Checker.NotNull(source, "source");
            WriteNoCheck(sink, source, 0, source.Count, bufferSize);
        }

        /// <summary>
        /// Writes blocks of bytes to the current sink using data read from a sequence.
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="source"></param>
        /// <param name="bufferSize">
        /// The size of the temporary buffer used to copy elements from the provided source.
        /// </param>
        internal static void Write(ITriflesByteWriter sink, IEnumerable<byte> source, int bufferSize = 0)
        {
            Checker.NotNull(sink, "sink");
            Checker.NotNull(source, "source");
            WriteNoCheck(sink, source, bufferSize);
        }

        #endregion Write from enumerable of bytes

        #region ToTriflesByteWriter

        internal static ITriflesByteWriter ToTriflesByteWriter(ITriflesByteWriter sink)
        {
            Checker.NotNull(sink, "sink");
            return sink;
        }

        private class StreamTriflesByteWriter : ITriflesByteWriter
        {
            private readonly Stream sink;

            public StreamTriflesByteWriter(Stream sink)
            {
                Checker.NotNull(sink, "sink");
                if (!sink.CanWrite)
                {
                    throw new ArgumentException("This stream does not support writing.", "sink");
                }
                this.sink = sink;
            }

            public void Write(byte[] buffer, int offset, int count)
            {
                sink.Write(buffer, offset, count);
            }
        }

        internal static ITriflesByteWriter ToTriflesByteWriter(Stream sink)
        {
            return new StreamTriflesByteWriter(sink);
        }

        private class BinaryWriterTriflesByteWriter : ITriflesByteWriter
        {
            private readonly BinaryWriter sink;

            public BinaryWriterTriflesByteWriter(BinaryWriter sink)
            {
                Checker.NotNull(sink, "sink");
                this.sink = sink;
            }

            public void Write(byte[] buffer, int offset, int count)
            {
                sink.Write(buffer, offset, count);
            }
        }

        internal static ITriflesByteWriter ToTriflesByteWriter(BinaryWriter sink)
        {
            return new BinaryWriterTriflesByteWriter(sink);
        }

        #endregion ToTriflesByteWriter
    }
}