//-----------------------------------------------------------------------
// <copyright file="DeferredOpt.cs" company="falldave">
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
    /// An option (<see cref="IOpt{T}"/>) whose <see cref="Fix()"/> method is implemented in terms of a given function.
    /// </summary>
    /// <typeparam name="T">The type of the contained value of the option.</typeparam>
    public class DeferredOpt<T> : AbstractOpt<T>
    {
        /// <summary>
        /// A function that returns the current value of the option as an <see cref="Opt{T}"/>. 
        /// </summary>
        private readonly Func<Opt<T>> getCurrentFixedValue;

        /// <summary>
        /// Creates an option whose value is the result of calling the given function.
        /// </summary>
        /// <param name="getCurrentFixedValue">A function that returns the current value of the option as an <see cref="Opt{T}"/>.</param>
        public DeferredOpt(Func<Opt<T>> getCurrentFixedValue)
        {
            this.getCurrentFixedValue = Errors.Require(getCurrentFixedValue, "getCurrentFixedValue");
        }

        #region IOpt<T> implementation

        /// <inheritdoc />
        public override Opt<T> Fix()
        {
            return getCurrentFixedValue();
        }

        #endregion
    }
}
