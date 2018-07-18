//-----------------------------------------------------------------------
// <copyright file="ITriflesByteReader.cs" company="falldave">
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
using System.Linq;
using System.Collections.Generic;

namespace FallDave.Trifles
{
    /// <summary>
    /// Interface for classes that support an operation similar to <see
    /// cref="System.IO.Stream.Read(byte[], int, int)"/>.
    /// </summary>
    public interface ITriflesByteReader
    {
        /// <summary>
        /// Reads a sequence of bytes from the current source and advances the position by the number
        /// of bytes read.
        /// </summary>
        /// <remarks>
        /// The general contract for this method is intended to be the same as that of <see
        /// cref="System.IO.Stream.Read(byte[], int, int)"/>.
        /// </remarks>
        /// <param name="buffer">
        /// A byte array whose values from the index <paramref name="offset"/> to the index <paramref
        /// name="offset"/> - <c>actualCount</c> - 1, inclusive, where <c>actualCount</c> is the
        /// value returned by this method, are replaced by the bytes read.
        /// </param>
        /// <param name="offset">
        /// The offset in <paramref name="buffer"/> at which to store the bytes read.
        /// </param>
        /// <param name="count">The maximum number of bytes to be read.</param>
        /// <returns>
        /// The total number of bytes read. This value is <paramref name="count"/> if that many bytes
        /// are immediately available for read; otherwise, the value may be smaller. A value of 0
        /// indicates that the end of the source has been reached.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="offset"/> + <paramref name="count"/> is larger than the length of
        /// <paramref name="buffer"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> or <paramref name="count"/> is less than 0.
        /// </exception>
        int Read(byte[] buffer, int offset, int count);
    }
}