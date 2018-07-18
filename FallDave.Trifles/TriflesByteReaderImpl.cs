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
    internal static class TriflesByteReaderImpl
    {
        // This value must be at least 1.
        private const int DefaultReadBufferSize = 8192;

        private static void EnsureBufferNonZero(byte[] buffer)
        {
            if (buffer.Length == 0)
            {
                throw new ArgumentException("Buffer array must not be 0-length.");
            }
        }

        private static int EnsureMaxCountNotNegative(int maxCount)
        {
            return Checker.NotNegative(maxCount, "maxCount");
        }

        private static ulong EnsureMaxCountNotNegative(long maxCount)
        {
            return (ulong)Checker.NotNegative(maxCount, "maxCount");
        }

        #region ReadFully

        // Repeatedly calls a partial read function until a buffer is full or the stream ends.
        private static int ReadAsFullyAsPossibleNoCheck(ITriflesByteReader source, byte[] buffer, int index, int count)
        {
            int totalReadLength = 0;

            while (totalReadLength < count)
            {
                int readLengthThisPass = source.Read(buffer, index + totalReadLength, count - totalReadLength);

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
        private static void ReadFullyNoCheck(ITriflesByteReader source, byte[] buffer, int index, int count)
        {
            int totalReadLength = ReadAsFullyAsPossibleNoCheck(source, buffer, index, count);
            if (totalReadLength < count)
            {
                // Stream ended before full read.
                throw new EndOfStreamException();
            }
        }

        internal static void ReadFully(ITriflesByteReader source, byte[] buffer, int offset, int count)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(buffer, "buffer");
            Checker.SpanInRange(buffer.Length, offset, count, "offset", "count");
            ReadFullyNoCheck(source, buffer, offset, count);
        }

        #endregion ReadFully

        #region ReadViaArray

        // Read to the end of the stream using the given byte array as a buffer.
        private static IEnumerable<int> ReadViaArrayNoCheck(ITriflesByteReader source, byte[] buffer)
        {
            var bufferSize = buffer.Length;

            while (true)
            {
                var countThisPass = ReadAsFullyAsPossibleNoCheck(source, buffer, 0, bufferSize);

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
        private static IEnumerable<int> ReadViaArrayNoCheck(ITriflesByteReader source, int maxCount, byte[] buffer, bool requireFullCount)
        {
            var bufferSize = buffer.Length;

            while (maxCount > 0)
            {
                var targetCountThisPass = Math.Min(bufferSize, maxCount);
                var countThisPass = ReadAsFullyAsPossibleNoCheck(source, buffer, 0, targetCountThisPass);
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

        private static IEnumerable<int> ReadViaArrayNoCheck(ITriflesByteReader source, ulong maxCount, byte[] buffer, bool requireFullCount)
        {
            var bufferSize = buffer.Length;

            while (maxCount > 0)
            {
                var targetCountThisPass = MathTrifles.Min(bufferSize, maxCount);
                var countThisPass = ReadAsFullyAsPossibleNoCheck(source, buffer, 0, targetCountThisPass);
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

        internal static IEnumerable<int> ReadViaArray(ITriflesByteReader source, byte[] buffer)
        {
            Checker.NotNull(buffer, "buffer");
            EnsureBufferNonZero(buffer);
            return ReadViaArrayNoCheck(source, buffer);
        }

        internal static IEnumerable<int> ReadViaArray(ITriflesByteReader source, int maxCount, byte[] buffer, bool requireFullCount)
        {
            Checker.NotNull(buffer, "buffer");
            EnsureMaxCountNotNegative(maxCount);

            if (maxCount > 0)
            {
                EnsureBufferNonZero(buffer);
            }
            return ReadViaArrayNoCheck(source, maxCount, buffer, requireFullCount);
        }

        internal static IEnumerable<int> ReadViaArray(ITriflesByteReader source, ulong maxCount, byte[] buffer, bool requireFullCount)
        {
            Checker.NotNull(buffer, "buffer");
            if (maxCount > 0)
            {
                EnsureBufferNonZero(buffer);
            }
            return ReadViaArrayNoCheck(source, maxCount, buffer, requireFullCount);
        }

        internal static IEnumerable<int> ReadViaArray(ITriflesByteReader source, long maxCount, byte[] buffer, bool requireFullCount)
        {
            return ReadViaArray(source, EnsureMaxCountNotNegative(maxCount), buffer, requireFullCount);
        }

        internal static void ReadViaArray(ITriflesByteReader source, Action<byte[], int, int> fillAction, byte[] buffer = null)
        {
            Checker.NotNull(fillAction, "fillAction");

            if (buffer == null)
            {
                buffer = new byte[DefaultReadBufferSize];
            }

            foreach (var countThisPass in ReadViaArray(source, buffer))
            {
                fillAction(buffer, 0, countThisPass);
            }
        }

        internal static void ReadViaArray(ITriflesByteReader source, int maxCount, Action<byte[], int, int> fillAction, byte[] buffer = null, bool requireFullCount = false)
        {
            Checker.NotNull(fillAction, "fillAction");
            EnsureMaxCountNotNegative(maxCount);

            if (buffer == null)
            {
                buffer = new byte[Math.Min(maxCount, DefaultReadBufferSize)];
            }

            foreach (var countThisPass in ReadViaArray(source, maxCount, buffer, requireFullCount))
            {
                fillAction(buffer, 0, countThisPass);
            }
        }

        internal static void ReadViaArray(ITriflesByteReader source, ulong maxCount, Action<byte[], int, int> fillAction, byte[] buffer = null, bool requireFullCount = false)
        {
            Checker.NotNull(fillAction, "fillAction");

            if (buffer == null)
            {
                buffer = new byte[MathTrifles.Min(maxCount, DefaultReadBufferSize)];
            }

            foreach (var countThisPass in ReadViaArray(source, maxCount, buffer, requireFullCount))
            {
                fillAction(buffer, 0, countThisPass);
            }
        }

        internal static void ReadViaArray(ITriflesByteReader source, long maxCount, Action<byte[], int, int> fillAction, byte[] buffer = null, bool requireFullCount = false)
        {
            ReadViaArray(source, EnsureMaxCountNotNegative(maxCount), fillAction, buffer, requireFullCount);
        }

        #endregion ReadViaArray

        #region ToTriflesByteReader

        internal static ITriflesByteReader ToTriflesByteReader(ITriflesByteReader source)
        {
            Checker.NotNull(source, "source");
            return source;
        }

        private class StreamTriflesByteReader : ITriflesByteReader
        {
            private readonly Stream source;

            public StreamTriflesByteReader(Stream source)
            {
                Checker.NotNull(source, "source");
                if (!source.CanRead)
                {
                    throw new ArgumentException("This stream does not support reading.", "source");
                }
                this.source = source;
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                return source.Read(buffer, offset, count);
            }
        }

        internal static ITriflesByteReader ToTriflesByteReader(Stream source)
        {
            return new StreamTriflesByteReader(source);
        }

        private class BinaryReaderTriflesByteReader : ITriflesByteReader
        {
            private readonly BinaryReader source;

            public BinaryReaderTriflesByteReader(BinaryReader source)
            {
                Checker.NotNull(source, "source");
                this.source = source;
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                return source.Read(buffer, offset, count);
            }
        }

        internal static ITriflesByteReader ToTriflesByteReader(BinaryReader source)
        {
            return new BinaryReaderTriflesByteReader(source);
        }

        internal static void ReadViaArray(ITriflesByteReader triflesByteReader, Action<byte[], int, int> fillAction, int maxCount, byte[] buffer, bool requireFullCount)
        {
            throw new NotImplementedException();
        }

        #endregion ToTriflesByteReader
    }
}