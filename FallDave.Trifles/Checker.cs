//-----------------------------------------------------------------------
// <copyright file="Checker.cs" company="falldave">
//
// Written in 2015-2016 by David McFall
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
using System.Text;
using System.Threading.Tasks;

namespace FallDave.Trifles
{
    /// <summary>
    /// Utils for parameter validation.
    /// </summary>
    public static class Checker
    {
        /// <summary>
        /// Ensures that the given value is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="paramName">The name of the value parameter to report if the value fails the check.</param>
        /// <returns><paramref name="value"/>, now known to be non-<c>null</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public static T NotNull<T>(T value, string paramName = null)
        {
            if (value == null)
            {
                throw ArgumentNull(paramName);
            }

            return value;
        }

        /// <summary>
        /// Ensures that the specified span would be fully contained in a zero-based sequence of the indicated size.
        /// </summary>
        /// <param name="sequenceLength">The length of a zero-based sequence to test the span against.</param>
        /// <param name="offset">The offset of the span.</param>
        /// <param name="length">The length of the span.</param>
        /// <param name="offsetParamName">The name of the offset parameter to report if the offset fails the check.</param>
        /// <param name="lengthParamName">The name of the length parameter to report if the length fails the check.</param>
        /// <returns>The end index of a slice equivalent to this span.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="sequenceLength"/> is negative.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is negative.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> is negative.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The end index (that is, <paramref name="offset"/> + <paramref name="length"/>) is greater than <paramref name="sequenceLength"/>.</exception>
        public static int SpanInRange(int sequenceLength, int offset, int length, string offsetParamName = "offset", string lengthParamName = "length")
        {
            if (sequenceLength < 0)
            {
                throw SequenceLengthIsNegative(sequenceLength);
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(lengthParamName, length, "Count is negative.");
            }

            if (offset < 0)
            {
                throw StartIndexIsNegative(offset, offsetParamName);
            }

            if (length > sequenceLength - offset)
            {
                throw new ArgumentOutOfRangeException(lengthParamName, length, "Count exceeds count available after start index.");
            }

            // Return end index for slice.
            return offset + length;
        }


        /// <summary>
        /// Ensures that the specified slice would be fully contained in a zero-based sequence of the indicated size.
        /// </summary>
        /// <param name="sequenceLength">The length of a zero-based sequence to test the slice against.</param>
        /// <param name="startIndex">The start index of the specified slice, inclusive.</param>
        /// <param name="endIndex">The end index of the specified slice, exclusive.</param>
        /// <param name="startIndexParamName">The name of the start index parameter to report if the start index fails the check.</param>
        /// <param name="endIndexParamName">The name of the end index parameter to report if the end index fails the check.</param>
        /// <returns>The length of a span equivalent to this slice.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="sequenceLength"/> is negative.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is negative.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="endIndex"/> is greater than <paramref name="sequenceLength"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is greater than <paramref name="endIndex"/>.</exception>
        public static int SliceInRange(int sequenceLength, int startIndex, int endIndex, string startIndexParamName = "startIndex", string endIndexParamName = "endIndex")
        {
            if (sequenceLength < 0)
            {
                throw SequenceLengthIsNegative(sequenceLength);
            }

            if (startIndex < 0)
            {
                throw StartIndexIsNegative(startIndex, startIndexParamName);
            }

            if (endIndex > sequenceLength)
            {
                throw new ArgumentOutOfRangeException(endIndexParamName, endIndex, "End index exceeds available length.");
            }

            if (startIndex > endIndex)
            {
                throw new ArgumentOutOfRangeException(startIndexParamName, startIndex, "Start index exceeds end index.");
            }

            // Return slice length.
            return endIndex - startIndex;
        }

        private static ArgumentOutOfRangeException SequenceLengthIsNegative(int sequenceLength)
        {
            return new ArgumentOutOfRangeException("sequenceLength", sequenceLength, "Sequence length for range test is negative.");
        }

        private static ArgumentOutOfRangeException StartIndexIsNegative(int startIndex, string startIndexParamName)
        {
            return new ArgumentOutOfRangeException(startIndexParamName, startIndex, "Start index is negative.");
        }

        private static ArgumentNullException ArgumentNull(string paramName)
        {
            return (paramName == null) ? new ArgumentNullException() : new ArgumentNullException(paramName);
        }
    }
}
