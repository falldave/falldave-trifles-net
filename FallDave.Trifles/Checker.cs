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
using System.Linq;
using System.Collections.Generic;

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
        /// <param name="paramName">
        /// The name of the value parameter to report if the value fails the check.
        /// </param>
        /// <returns><paramref name="value"/>, now known to be non- <c>null</c>.</returns>
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
        /// Ensures that the given value is not negative.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">
        /// The name of the value parameter to report is the value fails the check.
        /// </param>
        /// <param name="description">
        /// A human-friendly name for the parameter (for example, "Start index" for a parameter
        /// called "startIndex").
        /// </param>
        /// <returns><paramref name="value"/>, now known to be nonnegative.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
        public static int NotNegative(int value, string paramName = null, string description = null)
        {
            if (value < 0)
            {
                throw ArgumentNegative(value, paramName);
            }

            return value;
        }

        /// <summary>
        /// Ensures that the given value is not negative.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName">
        /// The name of the value parameter to report is the value fails the check.
        /// </param>
        /// <param name="description">
        /// A human-friendly name for the parameter (for example, "Start index" for a parameter
        /// called "startIndex").
        /// </param>
        /// <returns><paramref name="value"/>, now known to be nonnegative.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
        public static long NotNegative(long value, string paramName = null, string description = null)
        {
            if (value < 0)
            {
                throw ArgumentNegative(value, paramName);
            }

            return value;
        }

        /// <summary>
        /// Ensures that the specified span would be fully contained in a zero-based sequence of the
        /// indicated size.
        /// </summary>
        /// <param name="sequenceLength">The length of a zero-based sequence to test the span against.</param>
        /// <param name="offset">The offset of the span.</param>
        /// <param name="length">The length of the span.</param>
        /// <param name="offsetParamName">
        /// The name of the offset parameter to report if the offset fails the check.
        /// </param>
        /// <param name="lengthParamName">
        /// The name of the length parameter to report if the length fails the check.
        /// </param>
        /// <returns>The end index of a slice equivalent to this span.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sequenceLength"/> is negative.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is negative.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> is negative.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The end index (that is, <paramref name="offset"/> + <paramref name="length"/>) is greater
        /// than <paramref name="sequenceLength"/>.
        /// </exception>
        public static int SpanInRange(int sequenceLength, int offset, int length, string offsetParamName = "offset", string lengthParamName = "length")
        {
            NotNegative(sequenceLength, "sequenceLength", "Sequence length for range test");
            NotNegative(length, lengthParamName, "Count");
            NotNegative(offset, offsetParamName, "Start index");

            if (length > sequenceLength - offset)
            {
                throw new ArgumentOutOfRangeException(lengthParamName, length, "Count exceeds count available after start index.");
            }

            // Return end index for slice.
            return offset + length;
        }

        /// <summary>
        /// Ensures that the specified slice would be fully contained in a zero-based sequence of the
        /// indicated size.
        /// </summary>
        /// <param name="sequenceLength">
        /// The length of a zero-based sequence to test the slice against.
        /// </param>
        /// <param name="startIndex">The start index of the specified slice, inclusive.</param>
        /// <param name="endIndex">The end index of the specified slice, exclusive.</param>
        /// <param name="startIndexParamName">
        /// The name of the start index parameter to report if the start index fails the check.
        /// </param>
        /// <param name="endIndexParamName">
        /// The name of the end index parameter to report if the end index fails the check.
        /// </param>
        /// <returns>The length of a span equivalent to this slice.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sequenceLength"/> is negative.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is negative.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="endIndex"/> is greater than <paramref name="sequenceLength"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is greater than <paramref name="endIndex"/>.
        /// </exception>
        public static int SliceInRange(int sequenceLength, int startIndex, int endIndex, string startIndexParamName = "startIndex", string endIndexParamName = "endIndex")
        {
            NotNegative(sequenceLength, "sequenceLength", "Sequence length for range test");
            NotNegative(startIndex, startIndexParamName, "Start index");

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

        private static ArgumentOutOfRangeException ArgumentNegative(object value, string paramName, string description = null)
        {
            if (string.IsNullOrEmpty(description))
            {
                description = "Value";
            }
            return new ArgumentOutOfRangeException(paramName, value, description + " is negative.");
        }

        private static ArgumentNullException ArgumentNull(string paramName)
        {
            return (paramName == null) ? new ArgumentNullException() : new ArgumentNullException(paramName);
        }
    }
}