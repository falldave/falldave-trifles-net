//-----------------------------------------------------------------------
// <copyright file="IOpt.cs" company="falldave">
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

namespace FallDave.Trifles
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An option, which represents the presence or absence of a value of a given type.
    /// </summary>
    /// <typeparam name="T">The type of the contained value of the option.</typeparam>
    public interface IOpt<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets an immutable option reflecting the current state of this option.
        /// </summary>
        /// <returns>An <see cref="Opt{T}"/> containing an unmodifiable copy of the state of this option.</returns>
        Opt<T> Fix();
    }
}
