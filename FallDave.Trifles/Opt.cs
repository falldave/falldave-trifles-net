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


        #region Generated zipping methods



        // Equivalent to ZipFix, but with no null check for selector
        private static Opt<TResult> ZipFixUnchecked<T1, TResult>(IOpt<T1> item1Opt, Func<T1, TResult> selector)
        {
            T1 item1;

            if (item1Opt.TryGetSingle(out item1))
            {
                return Opt.Full(selector(item1));
            }
            else
            {
                return Opt.Empty<TResult>();
            }
        }

        /// <summary>
        /// Returns a deferred option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<Tuple<T1>> Zip<T1>(IOpt<T1> item1Opt)
        {
            return Zip(item1Opt, (item1) => Tuple.Create(item1));
        }
        /// <summary>
        /// Returns a deferred option that contains the result of
        /// calling the selector on all the values of the given options, or an empty option if at
        /// least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained values of the option
        /// parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<TResult> Zip<T1, TResult>(IOpt<T1> item1Opt, Func<T1, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(selector, "selector");
            return Opt.Defer(() => ZipFixUnchecked(item1Opt, selector));
        }

        /// <summary>
        /// Returns (immediately) a full option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <returns>
        /// If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static Opt<Tuple<T1>> ZipFix<T1>(IOpt<T1> item1Opt)
        {
            return ZipFix(item1Opt, (item1) => Tuple.Create(item1));
        }


        /// <summary>
        /// Returns (immediately) a full option containing the result of
        /// calling the selector on all the values of the given options, or
        /// an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained
        /// values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c>.</exception>
        public static Opt<TResult> ZipFix<T1, TResult>(IOpt<T1> item1Opt, Func<T1, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(selector, "selector");
            return ZipFixUnchecked(item1Opt, selector);
        }

        // Equivalent to ZipFix, but with no null check for selector
        private static Opt<TResult> ZipFixUnchecked<T1, T2, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, Func<T1, T2, TResult> selector)
        {
            T1 item1;
            T2 item2;

            if (item1Opt.TryGetSingle(out item1) &&
            item2Opt.TryGetSingle(out item2))
            {
                return Opt.Full(selector(item1, item2));
            }
            else
            {
                return Opt.Empty<TResult>();
            }
        }

        /// <summary>
        /// Returns a deferred option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<Tuple<T1, T2>> Zip<T1, T2>(IOpt<T1> item1Opt, IOpt<T2> item2Opt)
        {
            return Zip(item1Opt, item2Opt, (item1, item2) => Tuple.Create(item1, item2));
        }
        /// <summary>
        /// Returns a deferred option that contains the result of
        /// calling the selector on all the values of the given options, or an empty option if at
        /// least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained values of the option
        /// parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<TResult> Zip<T1, T2, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, Func<T1, T2, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(selector, "selector");
            return Opt.Defer(() => ZipFixUnchecked(item1Opt, item2Opt, selector));
        }

        /// <summary>
        /// Returns (immediately) a full option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <returns>
        /// If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static Opt<Tuple<T1, T2>> ZipFix<T1, T2>(IOpt<T1> item1Opt, IOpt<T2> item2Opt)
        {
            return ZipFix(item1Opt, item2Opt, (item1, item2) => Tuple.Create(item1, item2));
        }


        /// <summary>
        /// Returns (immediately) a full option containing the result of
        /// calling the selector on all the values of the given options, or
        /// an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained
        /// values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c>.</exception>
        public static Opt<TResult> ZipFix<T1, T2, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, Func<T1, T2, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(selector, "selector");
            return ZipFixUnchecked(item1Opt, item2Opt, selector);
        }

        // Equivalent to ZipFix, but with no null check for selector
        private static Opt<TResult> ZipFixUnchecked<T1, T2, T3, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, Func<T1, T2, T3, TResult> selector)
        {
            T1 item1;
            T2 item2;
            T3 item3;

            if (item1Opt.TryGetSingle(out item1) &&
            item2Opt.TryGetSingle(out item2) &&
            item3Opt.TryGetSingle(out item3))
            {
                return Opt.Full(selector(item1, item2, item3));
            }
            else
            {
                return Opt.Empty<TResult>();
            }
        }

        /// <summary>
        /// Returns a deferred option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<Tuple<T1, T2, T3>> Zip<T1, T2, T3>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt)
        {
            return Zip(item1Opt, item2Opt, item3Opt, (item1, item2, item3) => Tuple.Create(item1, item2, item3));
        }
        /// <summary>
        /// Returns a deferred option that contains the result of
        /// calling the selector on all the values of the given options, or an empty option if at
        /// least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained values of the option
        /// parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<TResult> Zip<T1, T2, T3, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, Func<T1, T2, T3, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(selector, "selector");
            return Opt.Defer(() => ZipFixUnchecked(item1Opt, item2Opt, item3Opt, selector));
        }

        /// <summary>
        /// Returns (immediately) a full option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <returns>
        /// If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static Opt<Tuple<T1, T2, T3>> ZipFix<T1, T2, T3>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt)
        {
            return ZipFix(item1Opt, item2Opt, item3Opt, (item1, item2, item3) => Tuple.Create(item1, item2, item3));
        }


        /// <summary>
        /// Returns (immediately) a full option containing the result of
        /// calling the selector on all the values of the given options, or
        /// an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained
        /// values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c>.</exception>
        public static Opt<TResult> ZipFix<T1, T2, T3, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, Func<T1, T2, T3, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(selector, "selector");
            return ZipFixUnchecked(item1Opt, item2Opt, item3Opt, selector);
        }

        // Equivalent to ZipFix, but with no null check for selector
        private static Opt<TResult> ZipFixUnchecked<T1, T2, T3, T4, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, Func<T1, T2, T3, T4, TResult> selector)
        {
            T1 item1;
            T2 item2;
            T3 item3;
            T4 item4;

            if (item1Opt.TryGetSingle(out item1) &&
            item2Opt.TryGetSingle(out item2) &&
            item3Opt.TryGetSingle(out item3) &&
            item4Opt.TryGetSingle(out item4))
            {
                return Opt.Full(selector(item1, item2, item3, item4));
            }
            else
            {
                return Opt.Empty<TResult>();
            }
        }

        /// <summary>
        /// Returns a deferred option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<Tuple<T1, T2, T3, T4>> Zip<T1, T2, T3, T4>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt)
        {
            return Zip(item1Opt, item2Opt, item3Opt, item4Opt, (item1, item2, item3, item4) => Tuple.Create(item1, item2, item3, item4));
        }
        /// <summary>
        /// Returns a deferred option that contains the result of
        /// calling the selector on all the values of the given options, or an empty option if at
        /// least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained values of the option
        /// parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<TResult> Zip<T1, T2, T3, T4, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, Func<T1, T2, T3, T4, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(selector, "selector");
            return Opt.Defer(() => ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, selector));
        }

        /// <summary>
        /// Returns (immediately) a full option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <returns>
        /// If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static Opt<Tuple<T1, T2, T3, T4>> ZipFix<T1, T2, T3, T4>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt)
        {
            return ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, (item1, item2, item3, item4) => Tuple.Create(item1, item2, item3, item4));
        }


        /// <summary>
        /// Returns (immediately) a full option containing the result of
        /// calling the selector on all the values of the given options, or
        /// an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained
        /// values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c>.</exception>
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, Func<T1, T2, T3, T4, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(selector, "selector");
            return ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, selector);
        }

        // Equivalent to ZipFix, but with no null check for selector
        private static Opt<TResult> ZipFixUnchecked<T1, T2, T3, T4, T5, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, Func<T1, T2, T3, T4, T5, TResult> selector)
        {
            T1 item1;
            T2 item2;
            T3 item3;
            T4 item4;
            T5 item5;

            if (item1Opt.TryGetSingle(out item1) &&
            item2Opt.TryGetSingle(out item2) &&
            item3Opt.TryGetSingle(out item3) &&
            item4Opt.TryGetSingle(out item4) &&
            item5Opt.TryGetSingle(out item5))
            {
                return Opt.Full(selector(item1, item2, item3, item4, item5));
            }
            else
            {
                return Opt.Empty<TResult>();
            }
        }

        /// <summary>
        /// Returns a deferred option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <param name="item5Opt">An option containing the fifth item to be collected into a tuple.</param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<Tuple<T1, T2, T3, T4, T5>> Zip<T1, T2, T3, T4, T5>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt)
        {
            return Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, (item1, item2, item3, item4, item5) => Tuple.Create(item1, item2, item3, item4, item5));
        }
        /// <summary>
        /// Returns a deferred option that contains the result of
        /// calling the selector on all the values of the given options, or an empty option if at
        /// least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item5Opt">An option containing the fifth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained values of the option
        /// parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<TResult> Zip<T1, T2, T3, T4, T5, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, Func<T1, T2, T3, T4, T5, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(item5Opt, "item5Opt");
            Checker.NotNull(selector, "selector");
            return Opt.Defer(() => ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, selector));
        }

        /// <summary>
        /// Returns (immediately) a full option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <param name="item5Opt">An option containing the fifth item to be collected into a tuple.</param>
        /// <returns>
        /// If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static Opt<Tuple<T1, T2, T3, T4, T5>> ZipFix<T1, T2, T3, T4, T5>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt)
        {
            return ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, (item1, item2, item3, item4, item5) => Tuple.Create(item1, item2, item3, item4, item5));
        }


        /// <summary>
        /// Returns (immediately) a full option containing the result of
        /// calling the selector on all the values of the given options, or
        /// an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item5Opt">An option containing the fifth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained
        /// values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c>.</exception>
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, T5, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, Func<T1, T2, T3, T4, T5, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(item5Opt, "item5Opt");
            Checker.NotNull(selector, "selector");
            return ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, selector);
        }

        // Equivalent to ZipFix, but with no null check for selector
        private static Opt<TResult> ZipFixUnchecked<T1, T2, T3, T4, T5, T6, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, Func<T1, T2, T3, T4, T5, T6, TResult> selector)
        {
            T1 item1;
            T2 item2;
            T3 item3;
            T4 item4;
            T5 item5;
            T6 item6;

            if (item1Opt.TryGetSingle(out item1) &&
            item2Opt.TryGetSingle(out item2) &&
            item3Opt.TryGetSingle(out item3) &&
            item4Opt.TryGetSingle(out item4) &&
            item5Opt.TryGetSingle(out item5) &&
            item6Opt.TryGetSingle(out item6))
            {
                return Opt.Full(selector(item1, item2, item3, item4, item5, item6));
            }
            else
            {
                return Opt.Empty<TResult>();
            }
        }

        /// <summary>
        /// Returns a deferred option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <param name="item5Opt">An option containing the fifth item to be collected into a tuple.</param>
        /// <param name="item6Opt">An option containing the sixth item to be collected into a tuple.</param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<Tuple<T1, T2, T3, T4, T5, T6>> Zip<T1, T2, T3, T4, T5, T6>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt)
        {
            return Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, (item1, item2, item3, item4, item5, item6) => Tuple.Create(item1, item2, item3, item4, item5, item6));
        }
        /// <summary>
        /// Returns a deferred option that contains the result of
        /// calling the selector on all the values of the given options, or an empty option if at
        /// least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item5Opt">An option containing the fifth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item6Opt">An option containing the sixth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained values of the option
        /// parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<TResult> Zip<T1, T2, T3, T4, T5, T6, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, Func<T1, T2, T3, T4, T5, T6, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(item5Opt, "item5Opt");
            Checker.NotNull(item6Opt, "item6Opt");
            Checker.NotNull(selector, "selector");
            return Opt.Defer(() => ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, selector));
        }

        /// <summary>
        /// Returns (immediately) a full option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <param name="item5Opt">An option containing the fifth item to be collected into a tuple.</param>
        /// <param name="item6Opt">An option containing the sixth item to be collected into a tuple.</param>
        /// <returns>
        /// If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static Opt<Tuple<T1, T2, T3, T4, T5, T6>> ZipFix<T1, T2, T3, T4, T5, T6>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt)
        {
            return ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, (item1, item2, item3, item4, item5, item6) => Tuple.Create(item1, item2, item3, item4, item5, item6));
        }


        /// <summary>
        /// Returns (immediately) a full option containing the result of
        /// calling the selector on all the values of the given options, or
        /// an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item5Opt">An option containing the fifth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item6Opt">An option containing the sixth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained
        /// values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c>.</exception>
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, T5, T6, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, Func<T1, T2, T3, T4, T5, T6, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(item5Opt, "item5Opt");
            Checker.NotNull(item6Opt, "item6Opt");
            Checker.NotNull(selector, "selector");
            return ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, selector);
        }

        // Equivalent to ZipFix, but with no null check for selector
        private static Opt<TResult> ZipFixUnchecked<T1, T2, T3, T4, T5, T6, T7, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
        {
            T1 item1;
            T2 item2;
            T3 item3;
            T4 item4;
            T5 item5;
            T6 item6;
            T7 item7;

            if (item1Opt.TryGetSingle(out item1) &&
            item2Opt.TryGetSingle(out item2) &&
            item3Opt.TryGetSingle(out item3) &&
            item4Opt.TryGetSingle(out item4) &&
            item5Opt.TryGetSingle(out item5) &&
            item6Opt.TryGetSingle(out item6) &&
            item7Opt.TryGetSingle(out item7))
            {
                return Opt.Full(selector(item1, item2, item3, item4, item5, item6, item7));
            }
            else
            {
                return Opt.Empty<TResult>();
            }
        }

        /// <summary>
        /// Returns a deferred option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="T7">The contained type of the option <paramref name="item7Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <param name="item5Opt">An option containing the fifth item to be collected into a tuple.</param>
        /// <param name="item6Opt">An option containing the sixth item to be collected into a tuple.</param>
        /// <param name="item7Opt">An option containing the seventh item to be collected into a tuple.</param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<Tuple<T1, T2, T3, T4, T5, T6, T7>> Zip<T1, T2, T3, T4, T5, T6, T7>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt)
        {
            return Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, (item1, item2, item3, item4, item5, item6, item7) => Tuple.Create(item1, item2, item3, item4, item5, item6, item7));
        }
        /// <summary>
        /// Returns a deferred option that contains the result of
        /// calling the selector on all the values of the given options, or an empty option if at
        /// least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="T7">The contained type of the option <paramref name="item7Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item5Opt">An option containing the fifth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item6Opt">An option containing the sixth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item7Opt">An option containing the seventh parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained values of the option
        /// parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(item5Opt, "item5Opt");
            Checker.NotNull(item6Opt, "item6Opt");
            Checker.NotNull(item7Opt, "item7Opt");
            Checker.NotNull(selector, "selector");
            return Opt.Defer(() => ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, selector));
        }

        /// <summary>
        /// Returns (immediately) a full option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="T7">The contained type of the option <paramref name="item7Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <param name="item5Opt">An option containing the fifth item to be collected into a tuple.</param>
        /// <param name="item6Opt">An option containing the sixth item to be collected into a tuple.</param>
        /// <param name="item7Opt">An option containing the seventh item to be collected into a tuple.</param>
        /// <returns>
        /// If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static Opt<Tuple<T1, T2, T3, T4, T5, T6, T7>> ZipFix<T1, T2, T3, T4, T5, T6, T7>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt)
        {
            return ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, (item1, item2, item3, item4, item5, item6, item7) => Tuple.Create(item1, item2, item3, item4, item5, item6, item7));
        }


        /// <summary>
        /// Returns (immediately) a full option containing the result of
        /// calling the selector on all the values of the given options, or
        /// an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="T7">The contained type of the option <paramref name="item7Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item5Opt">An option containing the fifth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item6Opt">An option containing the sixth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item7Opt">An option containing the seventh parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained
        /// values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c>.</exception>
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, T5, T6, T7, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(item5Opt, "item5Opt");
            Checker.NotNull(item6Opt, "item6Opt");
            Checker.NotNull(item7Opt, "item7Opt");
            Checker.NotNull(selector, "selector");
            return ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, selector);
        }

        // Equivalent to ZipFix, but with no null check for selector
        private static Opt<TResult> ZipFixUnchecked<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
        {
            T1 item1;
            T2 item2;
            T3 item3;
            T4 item4;
            T5 item5;
            T6 item6;
            T7 item7;
            T8 item8;

            if (item1Opt.TryGetSingle(out item1) &&
            item2Opt.TryGetSingle(out item2) &&
            item3Opt.TryGetSingle(out item3) &&
            item4Opt.TryGetSingle(out item4) &&
            item5Opt.TryGetSingle(out item5) &&
            item6Opt.TryGetSingle(out item6) &&
            item7Opt.TryGetSingle(out item7) &&
            item8Opt.TryGetSingle(out item8))
            {
                return Opt.Full(selector(item1, item2, item3, item4, item5, item6, item7, item8));
            }
            else
            {
                return Opt.Empty<TResult>();
            }
        }

        /// <summary>
        /// Returns a deferred option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="T7">The contained type of the option <paramref name="item7Opt"/>.</typeparam>
        /// <typeparam name="T8">The contained type of the option <paramref name="item8Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <param name="item5Opt">An option containing the fifth item to be collected into a tuple.</param>
        /// <param name="item6Opt">An option containing the sixth item to be collected into a tuple.</param>
        /// <param name="item7Opt">An option containing the seventh item to be collected into a tuple.</param>
        /// <param name="item8Opt">An option containing the eighth item to be collected into a tuple.</param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>> Zip<T1, T2, T3, T4, T5, T6, T7, T8>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt)
        {
            return Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, item8Opt, (item1, item2, item3, item4, item5, item6, item7, item8) => Tuple.Create(item1, item2, item3, item4, item5, item6, item7, item8));
        }
        /// <summary>
        /// Returns a deferred option that contains the result of
        /// calling the selector on all the values of the given options, or an empty option if at
        /// least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="T7">The contained type of the option <paramref name="item7Opt"/>.</typeparam>
        /// <typeparam name="T8">The contained type of the option <paramref name="item8Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item5Opt">An option containing the fifth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item6Opt">An option containing the sixth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item7Opt">An option containing the seventh parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item8Opt">An option containing the eighth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// A deferred option which evaluates as follows: If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained values of the option
        /// parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static IOpt<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(item5Opt, "item5Opt");
            Checker.NotNull(item6Opt, "item6Opt");
            Checker.NotNull(item7Opt, "item7Opt");
            Checker.NotNull(item8Opt, "item8Opt");
            Checker.NotNull(selector, "selector");
            return Opt.Defer(() => ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, item8Opt, selector));
        }

        /// <summary>
        /// Returns (immediately) a full option that contains the a tuple of the values of the given options,
        /// or an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="T7">The contained type of the option <paramref name="item7Opt"/>.</typeparam>
        /// <typeparam name="T8">The contained type of the option <paramref name="item8Opt"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first item to be collected into a tuple.</param>
        /// <param name="item2Opt">An option containing the second item to be collected into a tuple.</param>
        /// <param name="item3Opt">An option containing the third item to be collected into a tuple.</param>
        /// <param name="item4Opt">An option containing the fourth item to be collected into a tuple.</param>
        /// <param name="item5Opt">An option containing the fifth item to be collected into a tuple.</param>
        /// <param name="item6Opt">An option containing the sixth item to be collected into a tuple.</param>
        /// <param name="item7Opt">An option containing the seventh item to be collected into a tuple.</param>
        /// <param name="item8Opt">An option containing the eighth item to be collected into a tuple.</param>
        /// <returns>
        /// If all option parameters are full, a full option containing a new tuple
        /// whose contents are contained values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c> (determined at the time of this call).</exception>
        public static Opt<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>> ZipFix<T1, T2, T3, T4, T5, T6, T7, T8>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt)
        {
            return ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, item8Opt, (item1, item2, item3, item4, item5, item6, item7, item8) => Tuple.Create(item1, item2, item3, item4, item5, item6, item7, item8));
        }


        /// <summary>
        /// Returns (immediately) a full option containing the result of
        /// calling the selector on all the values of the given options, or
        /// an empty option if at least one of the given options is empty.
        /// </summary>
        /// <typeparam name="T1">The contained type of the option <paramref name="item1Opt"/>.</typeparam>
        /// <typeparam name="T2">The contained type of the option <paramref name="item2Opt"/>.</typeparam>
        /// <typeparam name="T3">The contained type of the option <paramref name="item3Opt"/>.</typeparam>
        /// <typeparam name="T4">The contained type of the option <paramref name="item4Opt"/>.</typeparam>
        /// <typeparam name="T5">The contained type of the option <paramref name="item5Opt"/>.</typeparam>
        /// <typeparam name="T6">The contained type of the option <paramref name="item6Opt"/>.</typeparam>
        /// <typeparam name="T7">The contained type of the option <paramref name="item7Opt"/>.</typeparam>
        /// <typeparam name="T8">The contained type of the option <paramref name="item8Opt"/>.</typeparam>
        /// <typeparam name="TResult">The return type for <paramref name="selector"/>.</typeparam>
        /// <param name="item1Opt">An option containing the first parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item2Opt">An option containing the second parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item3Opt">An option containing the third parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item4Opt">An option containing the fourth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item5Opt">An option containing the fifth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item6Opt">An option containing the sixth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item7Opt">An option containing the seventh parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="item8Opt">An option containing the eighth parameter to be passed to <paramref name="selector"/>.</param>
        /// <param name="selector">
        /// A function to be called on the contained values of the option parameters, if all are present.
        /// </param>
        /// <returns>
        /// If all option parameters are full, a full option containing the
        /// result of calling <paramref name="selector"/> on the contained
        /// values of the option parameters; otherwise, an empty option.
        /// </returns>
        /// <exception cref="ArgumentNullException">If any of the option parameters is <c>null</c>.</exception>
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
        {
            Checker.NotNull(item1Opt, "item1Opt");
            Checker.NotNull(item2Opt, "item2Opt");
            Checker.NotNull(item3Opt, "item3Opt");
            Checker.NotNull(item4Opt, "item4Opt");
            Checker.NotNull(item5Opt, "item5Opt");
            Checker.NotNull(item6Opt, "item6Opt");
            Checker.NotNull(item7Opt, "item7Opt");
            Checker.NotNull(item8Opt, "item8Opt");
            Checker.NotNull(selector, "selector");
            return ZipFixUnchecked(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, item8Opt, selector);
        }

        #endregion Generated zipping methods




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