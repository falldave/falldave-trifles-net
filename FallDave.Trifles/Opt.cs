//-----------------------------------------------------------------------
// <copyright file="Opt.cs" company="falldave">
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
    /// Option type that is a value type with immutable instances; a basic implementation of <see cref="IOpt{T}"/>.
    /// </summary>
    public struct Opt<T> : IOpt<T>
    {
        /// <summary>
        /// If true, this option contains <see cref="value"/>. Otherwise, this option contains no value.
        /// </summary>
        private readonly bool hasValue;

        /// <summary>
        /// If <see cref="hasValue"/>, this is the value contained by this option.
        /// Otherwise, this should contain <c>default(</c><typeparamref name="T"/><c>)</c>. 
        /// </summary>
        private readonly T value;

        // The parameterless constructor simply yields an empty Opt.

        /// <summary>
        /// Initializes a new instance of the <see cref="Opt{T}"/> struct containing the given value.
        /// </summary>
        /// <param name="value">The value that will be contained by this new instance.</param>
        public Opt(T value)
            : this(true, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Opt{T}"/> struct optionally containing the given value.
        /// </summary>
        /// <param name="hasValue">If <c>true</c>, the instance contains <paramref name="value"/>; otherwise, the instance contains no value.</param>
        /// <param name="value">The value that will be contained by this new instance if <paramref name="hasValue"/> is <c>true</c>.</param>
        public Opt(bool hasValue, T value = default(T))
        {
            this.hasValue = hasValue;
            this.value = hasValue ? value : default(T);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Opt{T}"/> struct to having the current contents of the given option.
        /// </summary>
        /// <param name="original">An option from which the instance's contents should be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="original"/> is <c>null</c>.</exception>
        public Opt(IOpt<T> original)
        {
            Errors.Require(original, "original");

            T value;
            this.hasValue = original.TryGetValue(out value);
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Opt{T}"/> struct to having the contents of the given sequence.
        /// A sequence of zero elements results in an empty option.
        /// A sequence of one element results in a full option containing the value from the sequence.
        /// </summary>
        /// <param name="original">A sequence from which the instance's contents should be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="original"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="original"/> contains more than one element.</exception>
        public Opt(IEnumerable<T> original)
        {
            Errors.Require(original, "original");

            bool tmpHasValue = false;
            T tmpValue = default(T);

            foreach (var item in original)
            {
                if (tmpHasValue)
                {
                    throw Errors.MoreThanOneElement();
                }

                tmpHasValue = true;
                tmpValue = item;
            }

            hasValue = tmpHasValue;
            value = tmpValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Opt{T}"/> struct to having the contents of the given sequence.
        /// A sequence of zero elements results in an empty option.
        /// A sequence of one element results in a full option containing the value from the sequence.
        /// </summary>
        /// <param name="original">A sequence from which the instance's contents should be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="original"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="original"/> contains more than one element.</exception>
        public Opt(IEnumerator<T> original)
        {
            Errors.Require(original, "original");

            bool tmpHasValue = false;
            T tmpValue = default(T);

            if (original.MoveNext())
            {
                tmpHasValue = true;
                tmpValue = original.Current;

                if (original.MoveNext())
                {
                    throw Errors.MoreThanOneElement();
                }
            }

            hasValue = tmpHasValue;
            value = tmpValue;
        }

        #region IOpt<T> implementation

        /// <inheritdoc/>
        public Opt<T> Fix()
        {
            // Already what was requested.
            return this;
        }

        #endregion

        #region IEnumerable<T> implementation

        public IEnumerator<T> GetEnumerator()
        {
            if (hasValue)
            {
                yield return value;
            }
        }

        #endregion

        #region IEnumerable implementation

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Retrieves the value contained in this option, if any.
        /// </summary>
        /// <returns><c>true</c> (with <paramref name="value"/> set to the contained value) if this option contains a value; <c>false</c> (with <paramref name="value"/> undefined) otherwise.</returns>
        /// <param name="value">Variable to receive the value contained in this option.</param>
        public bool TryGetValue(out T value)
        {
            // All constructors that leave hasValue as false also leaves value as default(T).
            // Therefore, this works nicely.
            value = this.value;
            return hasValue;
        }

        /// <summary>
        /// Returns whether this option contains a value.
        /// </summary>
        /// <returns><c>true</c> if this option contains a value; <c>false</c> otherwise.</returns>
        public bool Any()
        {
            return hasValue;
        }

        /// <summary>
        /// Returns the number of elements contained in this option.
        /// </summary>
        /// <returns><c>1</c> if this option contains a value; <c>0</c> otherwise.</returns>
        public int Count()
        {
            return hasValue ? 1 : 0;
        }

        /// <summary>
        /// Retrieves the (only) value contained in this option, if any.
        /// </summary>
        /// <returns>The value contained in this option.</returns>
        /// <exception cref="InvalidOperationException">This option contains no value.</exception>
        public T Single()
        {
            if (hasValue)
            {
                return value;
            }

            throw Errors.NoElements();
        }

        /// <summary>
        /// Retrieves the first (and only) value contained in this option, if any.
        /// </summary>
        /// <returns>The value contained in this option.</returns>
        /// <exception cref="InvalidOperationException">This option contains no value.</exception>
        public T First()
        {
            return Single();
        }

        /// <summary>
        /// Retrieves the last (and only) value contained in this option, if any.
        /// </summary>
        /// <returns>The value contained in this option.</returns>
        /// <exception cref="InvalidOperationException">This option contains no value.</exception>
        public T Last()
        {
            return Single();
        }

        /// <summary>
        /// Retrieves the value contained at the specified index in this option, if any.
        /// </summary>
        /// <param name="index">The index of the value to retrieve.</param>
        /// <returns>The value contained at the specified index in this option, if this is a full option and <paramref name="index"/> equals <c>0</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count()"/>.</exception>
        public T ElementAt(int index)
        {
            if (hasValue && index == 0)
            {
                return value;
            }

            throw new ArgumentOutOfRangeException("index");
        }
    }
}