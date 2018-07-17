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
    public static class StreamExtensions
    {
        private static Action<byte[], int, int> WriteActionOf(Stream stream)
        {
            return (buffer, offset, count) => stream.Write(buffer, offset, count);
        }

        private static Action<byte[], int, int> WriteActionOf(BinaryWriter writer)
        {
            return (buffer, index, count) => writer.Write(buffer, index, count);
        }

        private static Func<byte[], int, int, int> ReadFunctionOf(Stream stream)
        {
            return (buffer, offset, count) => stream.Read(buffer, offset, count);
        }

        private static Func<byte[], int, int, int> ReadFunctionOf(BinaryReader reader)
        {
            return (buffer, index, count) => reader.Read(buffer, index, count);
        }

        #region ReadFully

        // Repeatedly calls a partial read function until a buffer is full or the stream ends.
        private static int ReadAsFullyAsPossibleNoCheck(Func<byte[], int, int, int> readFunction, byte[] buffer, int index, int count)
        {
            int totalReadLength = 0;

            while (totalReadLength < count)
            {
                int readLengthThisPass = readFunction(buffer, index + totalReadLength, count - totalReadLength);

                if (readLengthThisPass > 0)
                {
                    // At least one byte was read.
                    totalReadLength += readLengthThisPass;
                }
                else
                {
                    // Stream ended.
                    break;
                }
            }

            return totalReadLength;
        }

        // Repeatedly calls a partial read function until a buffer is full.
        private static void ReadFullyNoCheck(Func<byte[], int, int, int> readFunction, byte[] buffer, int index, int count)
        {
            int totalReadLength = ReadAsFullyAsPossibleNoCheck(readFunction, buffer, index, count);
            if (totalReadLength < count)
            {
                // Stream ended before full read.
                throw new EndOfStreamException();
            }
        }

        /// <summary>
        /// Reads the entire specified buffer segment or throws an exception.
        /// </summary>
        /// <remarks>
        /// The buffer is filled by reading the source repeatedly until the buffer is full, which may
        /// require multiple reads. The stream position is not restored after an unsuccessful read.
        /// </remarks>
        /// <param name="source">The source to be read</param>
        /// <param name="buffer">The buffer to read data into</param>
        /// <param name="offset">The starting point in the buffer</param>
        /// <param name="count">The number of bytes to read</param>
        /// <exception cref="EndOfStreamException">
        /// The end of the stream was reached before the read could be fully completed.
        /// </exception>
        public static void ReadFully(this Stream source, byte[] buffer, int offset, int count)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(buffer, "buffer");
            Checker.SpanInRange(buffer.Length, offset, count, "offset", "count");
            ReadFullyNoCheck(ReadFunctionOf(source), buffer, offset, count);
        }

        /// <summary>
        /// Reads the entire specified buffer segment or throws an exception.
        /// </summary>
        /// <remarks>
        /// The buffer is filled by reading the source repeatedly until the buffer is full, which may
        /// require multiple reads. The stream position is not restored after an unsuccessful read.
        /// </remarks>
        /// <param name="source">The source to be read</param>
        /// <param name="buffer">The buffer to read data into</param>
        /// <param name="index">The starting point in the buffer</param>
        /// <param name="count">The number of bytes to read</param>
        /// <exception cref="EndOfStreamException">
        /// The end of the stream was reached before the read could be fully completed.
        /// </exception>
        public static void ReadFully(this BinaryReader source, byte[] buffer, int index, int count)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(buffer, "buffer");
            Checker.SpanInRange(buffer.Length, index, count, "index", "count");
            ReadFullyNoCheck(ReadFunctionOf(source), buffer, index, count);
        }

        #endregion ReadFully

        #region ReadViaArray

        // This value must be at least 1.
        private const int DefaultReadBufferSize = 8192;

        private static void EnsureBufferNonZero(byte[] buffer)
        {
            if (buffer.Length == 0)
            {
                throw new ArgumentException("Buffer array must not be 0-length.");
            }
        }

        // Read to the end of the stream using the given byte array as a buffer.
        private static IEnumerable<int> ReadViaArrayNoCheck(Func<byte[], int, int, int> readFunction, byte[] buffer)
        {
            var bufferSize = buffer.Length;

            while (true)
            {
                var countThisPass = ReadAsFullyAsPossibleNoCheck(readFunction, buffer, 0, bufferSize);

                if (countThisPass > 0)
                {
                    yield return countThisPass;
                }

                if (countThisPass < bufferSize)
                {
                    break;
                }
            }
        }

        // Read up to maxCount bytes or to the end of the stream, whichever comes first, using the
        // given byte array as a buffer. If requireFullCount is true, throw EndOfStreamException if
        // fewer than maxCount bytes are read.
        private static IEnumerable<int> ReadViaArrayNoCheck(Func<byte[], int, int, int> readFunction, int maxCount, byte[] buffer, bool requireFullCount)
        {
            var bufferSize = buffer.Length;

            while (maxCount > 0)
            {
                var targetCountThisPass = Math.Min(bufferSize, maxCount);
                var countThisPass = ReadAsFullyAsPossibleNoCheck(readFunction, buffer, 0, targetCountThisPass);
                if (countThisPass > 0)
                {
                    yield return countThisPass;
                    maxCount -= countThisPass;
                }
            }

            if (requireFullCount && maxCount > 0)
            {
                throw new EndOfStreamException();
            }
        }

        private static IEnumerable<int> ReadViaArrayNoCheck(Func<byte[], int, int, int> readFunction, ulong maxCount, byte[] buffer, bool requireFullCount)
        {
            var bufferSize = buffer.Length;

            while (maxCount > 0)
            {
                var targetCountThisPass = MathTrifles.Min(bufferSize, maxCount);
                var countThisPass = ReadAsFullyAsPossibleNoCheck(readFunction, buffer, 0, targetCountThisPass);
                if (countThisPass > 0)
                {
                    yield return countThisPass;
                    maxCount -= (ulong)countThisPass;
                }
            }

            if (requireFullCount && maxCount > 0)
            {
                throw new EndOfStreamException();
            }
        }

        private static IEnumerable<int> ReadViaArray(Func<byte[], int, int, int> readFunction, byte[] buffer)
        {
            Checker.NotNull(buffer, "buffer");
            EnsureBufferNonZero(buffer);
            return ReadViaArrayNoCheck(readFunction, buffer);
        }

        private static IEnumerable<int> ReadViaArray(Func<byte[], int, int, int> readFunction, int maxCount, byte[] buffer, bool requireFullCount)
        {
            Checker.NotNull(buffer, "buffer");
            if (maxCount > 0)
            {
                EnsureBufferNonZero(buffer);
            }
            return ReadViaArrayNoCheck(readFunction, maxCount, buffer, requireFullCount);
        }

        private static IEnumerable<int> ReadViaArray(Func<byte[], int, int, int> readFunction, ulong maxCount, byte[] buffer, bool requireFullCount)
        {
            Checker.NotNull(buffer, "buffer");
            if (maxCount > 0)
            {
                EnsureBufferNonZero(buffer);
            }
            return ReadViaArrayNoCheck(readFunction, maxCount, buffer, requireFullCount);
        }

        private static void ReadViaArray(Func<byte[], int, int, int> readFunction, Action<byte[], int, int> fillAction, byte[] buffer = null)
        {
            Checker.NotNull(fillAction, "fillAction");

            if (buffer == null)
            {
                buffer = new byte[DefaultReadBufferSize];
            }

            foreach (var countThisPass in ReadViaArray(readFunction, buffer))
            {
                fillAction(buffer, 0, countThisPass);
            }
        }

        private static void ReadViaArray(Func<byte[], int, int, int> readFunction, int maxCount, Action<byte[], int, int> fillAction, byte[] buffer = null, bool requireFullCount = false)
        {
            Checker.NotNull(fillAction, "fillAction");

            if (buffer == null)
            {
                buffer = new byte[Math.Min(maxCount, DefaultReadBufferSize)];
            }

            foreach (var countThisPass in ReadViaArray(readFunction, maxCount, buffer, requireFullCount))
            {
                fillAction(buffer, 0, countThisPass);
            }
        }

        private static void ReadViaArray(Func<byte[], int, int, int> readFunction, ulong maxCount, Action<byte[], int, int> fillAction, byte[] buffer = null, bool requireFullCount = false)
        {
            Checker.NotNull(fillAction, "fillAction");

            if (buffer == null)
            {
                buffer = new byte[MathTrifles.Min(maxCount, DefaultReadBufferSize)];
            }

            foreach (var countThisPass in ReadViaArray(readFunction, maxCount, buffer, requireFullCount))
            {
                fillAction(buffer, 0, countThisPass);
            }
        }

        private static ulong NonnegativeLong(long count)
        {
            return count < 0 ? 0UL : (ulong)count;
        }

        #region ReadViaArray(this Stream ...)

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends.
        /// </summary>
        /// <remarks>
        /// As long as the end of the source has not been reached, <paramref name="buffer"/> will be
        /// repeatedly filled with data read from the source, with the returned enumerable producing
        /// the number of bytes read each time.
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="buffer">An array to receive data from the source.</param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException"><paramref name="buffer"/> is 0-length.</exception>
        public static IEnumerable<int> ReadViaArray(this Stream source, byte[] buffer)
        {
            return ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), buffer);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, <paramref name="buffer"/> will be repeatedly filled with
        /// data read from the source, with the returned enumerable producing the number of bytes
        /// read each time.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source.</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<int> ReadViaArray(this Stream source, int maxCount, byte[] buffer, bool requireFullCount = false)
        {
            return ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), maxCount, buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, <paramref name="buffer"/> will be repeatedly filled with
        /// data read from the source, with the returned enumerable producing the number of bytes
        /// read each time.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source.</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<int> ReadViaArray(this Stream source, long maxCount, byte[] buffer, bool requireFullCount = false)
        {
            return ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), NonnegativeLong(maxCount), buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, <paramref name="buffer"/> will be repeatedly filled with
        /// data read from the source, with the returned enumerable producing the number of bytes
        /// read each time.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source.</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<int> ReadViaArray(this Stream source, ulong maxCount, byte[] buffer, bool requireFullCount = false)
        {
            return ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), maxCount, buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends, calling the given
        /// action each time the buffer is filled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached, the buffer will be repeatedly
        /// filled with data read from the source, with <paramref name="fillAction"/> being called
        /// with the buffer, an offset, and a length as parameters each time the buffer is filled.
        /// </para>
        /// <para>
        /// If <paramref name="buffer"/> is present, it is used as the buffer. If <paramref
        /// name="buffer"/> is omitted or <c>null</c>, a new array is allocated.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="fillAction">
        /// An action with buffer, offset, and length parameters that is called each time the buffer
        /// is filled.
        /// </param>
        /// <param name="buffer">An array to receive data from the source (may be omitted).</param>
        /// <exception cref="ArgumentException"><paramref name="buffer"/> is 0-length.</exception>
        public static void ReadViaArray(this Stream source, Action<byte[], int, int> fillAction, byte[] buffer = null)
        {
            ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), fillAction, buffer);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first, calling the given action each time the buffer is filled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, the buffer will be repeatedly filled with data read from
        /// the source, with <paramref name="fillAction"/> being called with the buffer, an offset,
        /// and a length as parameters each time the buffer is filled.
        /// </para>
        /// <para>
        /// If <paramref name="buffer"/> is present, it is used as the buffer. If <paramref
        /// name="buffer"/> is omitted or <c>null</c>, a new array is allocated.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="fillAction">
        /// An action with buffer, offset, and length parameters that is called each time the buffer
        /// is filled.
        /// </param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source (may be omitted).</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static void ReadViaArray(this Stream source, Action<byte[], int, int> fillAction, int maxCount, byte[] buffer = null, bool requireFullCount = false)
        {
            ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), maxCount, buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first, calling the given action each time the buffer is filled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, the buffer will be repeatedly filled with data read from
        /// the source, with <paramref name="fillAction"/> being called with the buffer, an offset,
        /// and a length as parameters each time the buffer is filled.
        /// </para>
        /// <para>
        /// If <paramref name="buffer"/> is present, it is used as the buffer. If <paramref
        /// name="buffer"/> is omitted or <c>null</c>, a new array is allocated.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="fillAction">
        /// An action with buffer, offset, and length parameters that is called each time the buffer
        /// is filled.
        /// </param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source (may be omitted).</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static void ReadViaArray(this Stream source, Action<byte[], int, int> fillAction, long maxCount, byte[] buffer = null, bool requireFullCount = false)
        {
            ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), NonnegativeLong(maxCount), buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first, calling the given action each time the buffer is filled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, the buffer will be repeatedly filled with data read from
        /// the source, with <paramref name="fillAction"/> being called with the buffer, an offset,
        /// and a length as parameters each time the buffer is filled.
        /// </para>
        /// <para>
        /// If <paramref name="buffer"/> is present, it is used as the buffer. If <paramref
        /// name="buffer"/> is omitted or <c>null</c>, a new array is allocated.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="fillAction">
        /// An action with buffer, offset, and length parameters that is called each time the buffer
        /// is filled.
        /// </param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source (may be omitted).</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static void ReadViaArray(this Stream source, Action<byte[], int, int> fillAction, ulong maxCount, byte[] buffer = null, bool requireFullCount = false)
        {
            ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), maxCount, buffer, requireFullCount);
        }

        #endregion ReadViaArray(this Stream ...)

        #region ReadViaArray(this BinaryReader ...)

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends.
        /// </summary>
        /// <remarks>
        /// As long as the end of the source has not been reached, <paramref name="buffer"/> will be
        /// repeatedly filled with data read from the source, with the returned enumerable producing
        /// the number of bytes read each time.
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="buffer">An array to receive data from the source.</param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException"><paramref name="buffer"/> is 0-length.</exception>
        public static IEnumerable<int> ReadViaArray(this BinaryReader source, byte[] buffer)
        {
            return ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), buffer);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, <paramref name="buffer"/> will be repeatedly filled with
        /// data read from the source, with the returned enumerable producing the number of bytes
        /// read each time.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source.</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<int> ReadViaArray(this BinaryReader source, int maxCount, byte[] buffer, bool requireFullCount = false)
        {
            return ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), maxCount, buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, <paramref name="buffer"/> will be repeatedly filled with
        /// data read from the source, with the returned enumerable producing the number of bytes
        /// read each time.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source.</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<int> ReadViaArray(this BinaryReader source, long maxCount, byte[] buffer, bool requireFullCount = false)
        {
            return ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), NonnegativeLong(maxCount), buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, <paramref name="buffer"/> will be repeatedly filled with
        /// data read from the source, with the returned enumerable producing the number of bytes
        /// read each time.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source.</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<int> ReadViaArray(this BinaryReader source, ulong maxCount, byte[] buffer, bool requireFullCount = false)
        {
            return ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), maxCount, buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends, calling the given
        /// action each time the buffer is filled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached, the buffer will be repeatedly
        /// filled with data read from the source, with <paramref name="fillAction"/> being called
        /// with the buffer, an offset, and a length as parameters each time the buffer is filled.
        /// </para>
        /// <para>
        /// If <paramref name="buffer"/> is present, it is used as the buffer. If <paramref
        /// name="buffer"/> is omitted or <c>null</c>, a new array is allocated.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="fillAction">
        /// An action with buffer, offset, and length parameters that is called each time the buffer
        /// is filled.
        /// </param>
        /// <param name="buffer">An array to receive data from the source (may be omitted).</param>
        /// <exception cref="ArgumentException"><paramref name="buffer"/> is 0-length.</exception>
        public static void ReadViaArray(this BinaryReader source, Action<byte[], int, int> fillAction, byte[] buffer = null)
        {
            ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), fillAction, buffer);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first, calling the given action each time the buffer is filled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, the buffer will be repeatedly filled with data read from
        /// the source, with <paramref name="fillAction"/> being called with the buffer, an offset,
        /// and a length as parameters each time the buffer is filled.
        /// </para>
        /// <para>
        /// If <paramref name="buffer"/> is present, it is used as the buffer. If <paramref
        /// name="buffer"/> is omitted or <c>null</c>, a new array is allocated.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="fillAction">
        /// An action with buffer, offset, and length parameters that is called each time the buffer
        /// is filled.
        /// </param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source (may be omitted).</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static void ReadViaArray(this BinaryReader source, Action<byte[], int, int> fillAction, int maxCount, byte[] buffer = null, bool requireFullCount = false)
        {
            ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), maxCount, buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first, calling the given action each time the buffer is filled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, the buffer will be repeatedly filled with data read from
        /// the source, with <paramref name="fillAction"/> being called with the buffer, an offset,
        /// and a length as parameters each time the buffer is filled.
        /// </para>
        /// <para>
        /// If <paramref name="buffer"/> is present, it is used as the buffer. If <paramref
        /// name="buffer"/> is omitted or <c>null</c>, a new array is allocated.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="fillAction">
        /// An action with buffer, offset, and length parameters that is called each time the buffer
        /// is filled.
        /// </param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source (may be omitted).</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static void ReadViaArray(this BinaryReader source, Action<byte[], int, int> fillAction, long maxCount, byte[] buffer = null, bool requireFullCount = false)
        {
            ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), NonnegativeLong(maxCount), buffer, requireFullCount);
        }

        /// <summary>
        /// Reads bytes repeatedly from a source stream until the stream ends or a given count has
        /// been read, whichever happens first, calling the given action each time the buffer is filled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As long as the end of the source has not been reached and <paramref name="maxCount"/>
        /// bytes have not yet been read, the buffer will be repeatedly filled with data read from
        /// the source, with <paramref name="fillAction"/> being called with the buffer, an offset,
        /// and a length as parameters each time the buffer is filled.
        /// </para>
        /// <para>
        /// If <paramref name="buffer"/> is present, it is used as the buffer. If <paramref
        /// name="buffer"/> is omitted or <c>null</c>, a new array is allocated.
        /// </para>
        /// <para>
        /// If <paramref name="requireFullCount"/> is <c>true</c>, the enumeration will throw an <see
        /// cref="EndOfStreamException"/> if the end of the source has been reached before <paramref
        /// name="maxCount"/> bytes have been read. If <c>false</c>, reaching the end of the source
        /// simply causes the enumeration to end early.
        /// </para>
        /// </remarks>
        /// <param name="source">A stream to read data from.</param>
        /// <param name="fillAction">
        /// An action with buffer, offset, and length parameters that is called each time the buffer
        /// is filled.
        /// </param>
        /// <param name="maxCount">The maximum total number of bytes to read from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to receive data from the source (may be omitted).</param>
        /// <param name="requireFullCount">
        /// If <c>true</c>, <see cref="EndOfStreamException"/> will be thrown if the end of the
        /// source is reached before <paramref name="maxCount"/> bytes are read.
        /// </param>
        /// <returns>An enumerable that produces the number of bytes read each pass.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="buffer"/> is 0-length while <paramref name="maxCount"/> is greater than 0.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// The end of the source was reached before <paramref name="maxCount"/> bytes could be read
        /// while <paramref name="requireFullCount"/> is <c>true</c>. (Thrown from enumeration.)
        /// </exception>
        public static void ReadViaArray(this BinaryReader source, Action<byte[], int, int> fillAction, ulong maxCount, byte[] buffer = null, bool requireFullCount = false)
        {
            ReadViaArray(ReadFunctionOf(Checker.NotNull(source, "source")), maxCount, buffer, requireFullCount);
        }

        #endregion ReadViaArray(this BinaryReader ...)

        #endregion ReadViaArray

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