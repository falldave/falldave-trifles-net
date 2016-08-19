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
    /// Static methods for creating and manipulating <see cref="Opt{T}"/> option values.
    /// </summary>
    public static class Opt
    {
        /// <summary>
        /// Produces a fixed option that is empty.
        /// </summary>
        /// <typeparam name="T">The value type for the new fixed option.</typeparam>
        /// <param name="typeExample">Ignored value; can be used to infer <typeparamref name="T"/> instead of specifying explicitly.</param>
        /// <returns>An empty option value.</returns>
        public static Opt<T> Empty<T>(T typeExample = default(T))
        {
            return new Opt<T>();
        }

        /// <summary>
        /// Produces a fixed option that contains the given value.
        /// </summary>
        /// <typeparam name="T">The value type for the new fixed option.</typeparam>
        /// <param name="value">The value to be contained in the new fixed option.</param>
        /// <returns>An option value containing <paramref name="value"/>.</returns>
        public static Opt<T> Full<T>(T value)
        {
            return new Opt<T>(value);
        }

        /// <summary>
        /// Produces a fixed option that contains the given value, or is empty if the given value is null.
        /// </summary>
        /// <typeparam name="T">The value type for the new fixed option.</typeparam>
        /// <param name="value">The value to be contained in the new fixed option, if not null.</param>
        /// <returns>An option value that contains <paramref name="value"/>, if not <c>null</c>, or nothing, if <paramref name="value"/> is <c>null</c>.</returns>
        public static Opt<T> FullIfNotNull<T>(T value)
        {
            return Create(value != null, value);
        }

        /// <summary>
        /// Creates a new fixed option whose element count is set by the given flag.
        /// </summary>
        /// <typeparam name="T">The value type for the new fixed option.</typeparam>
        /// <param name="hasValue">
        /// <c>true</c> if the new fixed option will contain a value, or <c>false</c> if it will be empty.
        /// </param>
        /// <param name="value">
        /// The value to be contained in the new fixed option (ignored if <paramref
        /// name="hasValue"/> is <c>false</c>).
        /// </param>
        /// <returns>
        /// A new fixed option equivalent to <c>Opt.Full(value)</c> if <paramref name="hasValue"/>
        /// is <c>true</c>, or <c><![CDATA[Opt.Empty<T>()]]></c> otherwise.
        /// </returns>
        public static Opt<T> Create<T>(bool hasValue, T value = default(T))
        {
            return hasValue ? Full(value) : Empty<T>();
        }

        /// <summary>
        /// Creates a new fixed option whose element count is set by the given
        /// flag and whose value is only computed if the flag is <c>true</c>.
        /// </summary>
        /// <typeparam name="T">The value type for the new fixed option.</typeparam>
        /// <param name="hasValue">
        /// <c>true</c> if the new fixed option will contain a value, or
        /// <c>false</c> if it will be empty.
        /// </param>
        /// <param name="getValue">
        /// A callback to retrieve the value to be contained in the new fixed
        /// option (ignored if <paramref name="hasValue"/> is <c>false</c>).
        /// </param>
        /// <returns>
        /// A new fixed option equivalent to <c>Opt.Full(getValue())</c> if
        /// <paramref name="hasValue"/> is <c>true</c>, or
        /// <c><![CDATA[Opt.Empty<T>()]]></c> otherwise.
        /// </returns>
        public static Opt<T> CreateFromResult<T>(bool hasValue, Func<T> getValue)
        {
            return hasValue ? Full(getValue()) : Empty<T>();
        }

        /// <summary>
        /// Creates a new option whose contents are evaluated on demand using the given function.
        /// </summary>
        /// <typeparam name="T">The value type for the new deferred option.</typeparam>
        /// <param name="getCurrentFixedValue">
        /// A function that returns, as a fixed option, the current value of this option.
        /// </param>
        /// <returns>
        /// A new options whose contents are reevaluated each time they are retrieved by calling the
        /// given function.
        /// </returns>
        public static IOpt<T> Defer<T>(Func<Opt<T>> getCurrentFixedValue)
        {
            return new DeferredOpt<T>(getCurrentFixedValue);
        }
    }

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

        /// <summary>
        /// Selects (immediately, non-deferred) a projection of this option using the given selector and returns a new fixed option containing the result.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Opt<TResult> SelectFix<TResult>(Func<T, TResult> selector)
        {
            if (selector == null) { throw new ArgumentNullException("selector"); }

            if (!hasValue)
            {
                return new Opt<TResult>();
            }
            else
            {
                return new Opt<TResult>(selector(value));
            }
        }

        /// <summary>
        /// Filters (immediately, non-deferred) this option using the given predicate and returns a new fixed option containing the result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Opt<T> WhereFix(Func<T, bool> predicate)
        {
            if (predicate == null) { throw new ArgumentNullException("predicate"); }

            if (hasValue)
            {
                // Full, so return self for full.
                return predicate(value) ? this : new Opt<T>();
            }
            else
            {
                // Empty; cannot test predicate
                return this;
            }
        }





        // The parameterless constructor simply yields an empty Opt.

        /// <summary>
        /// Initializes a new instance of the <see cref="Opt{T}"/> struct containing the given value.
        /// </summary>
        /// <param name="value">The value that will be contained by this new instance.</param>
        public Opt(T value)
        {
            this.hasValue = true;
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
            Checker.NotNull(original, "original");

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
            Checker.NotNull(original, "original");

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
        
        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator()"/>
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
        public bool TryGetSingle(out T value)
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
        /// Retrieves the (only) value contained in this option, if any, or the default value for its value type otherwise.
        /// </summary>
        /// <returns>The value contained in this option, if any, or <c>default</c>(<typeparamref name="T"/>) otherwise.</returns>
        public T SingleOrDefault()
        {
            return hasValue ? value : default(T);
        }        
    }
}