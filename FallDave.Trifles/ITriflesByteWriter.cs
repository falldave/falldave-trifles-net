//-----------------------------------------------------------------------
// <copyright file="ITriflesByteWriter.cs" company="falldave">
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
    /// cref="System.IO.Stream.Write(byte[], int, int)"/>.
    /// </summary>
    public interface ITriflesByteWriter
    {
        /// <summary>
        /// Writes a sequence of bytes to the current writer and advances the position by the number
        /// of bytes written.
        /// </summary>
        /// <remarks>
        /// The general contract for this method is intended to be the same as that of <see
        /// cref="System.IO.Stream.Write(byte[], int, int)"/>.
        /// </remarks>
        /// <param name="buffer">
        /// A byte array whose values from the index <paramref name="offset"/> to the index <paramref
        /// name="offset"/> + <paramref name="count"/> - 1, inclusive, are copied to the current writer.
        /// </param>
        /// <param name="offset">The offset in <paramref name="buffer"/> from which to start writing.</param>
        /// <param name="count">The number of bytes to be written.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="offset"/> + <paramref name="count"/> is larger than the length of
        /// <paramref name="buffer"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> or <paramref name="count"/> is less than 0.
        /// </exception>
        void Write(byte[] buffer, int offset, int count);
    }
}