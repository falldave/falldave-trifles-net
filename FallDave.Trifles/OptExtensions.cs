//-----------------------------------------------------------------------
// <copyright file="OptExtensions.cs" company="falldave">
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
    using System.Linq;

    /// <summary>
    /// Extension methods applicable to <see cref="IOpt{T}"/> instances.
    /// </summary>
    public static class OptExtensions
    {
        /// <summary>
        /// If the source option contains a value, calls an action with that value as a parameter;
        /// otherwise, does nothing.
        /// </summary>
        /// <para>
        /// This method forces evaluation by making a fixed copy of the source option. The copy is
        /// returned, so this call may be chained with e.g. <see cref="Opt{T}.ForNone(Action)"/> to
        /// implement type-safe if-else semantics.
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action">An action to be executed if this option contains a value.</param>
        /// <returns>A fixed copy of <paramref name="source"/>.</returns>
        public static Opt<T> ForAny<T>(this IOpt<T> source, Action<T> action)
        {
            Checker.NotNull(action, "action");
            return source.FixSource().ForAny(action);
        }

        /// <summary>
        /// If the source option contains a value, calls an action with that value as a parameter;
        /// otherwise, calls a different action.
        /// </summary>
        /// <para>
        /// This method forces evaluation by making a fixed copy of the source option. The copy is returned.
        /// </para>
        /// <para>
        /// This method implements type-safe if-else semantics in one call. However, it may help
        /// readability to chain a <see cref="ForAny{T}(IOpt{T}, Action{T})"/> with a <see
        /// cref="Opt{T}.ForNone(Action)"/> call instead.
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action">An action to be executed if this option contains a value.</param>
        /// <param name="actionIfNone">An action to be executed if this option is empty.</param>
        /// <returns>A fixed copy of <paramref name="source"/>.</returns>
        public static Opt<T> ForAny<T>(this IOpt<T> source, Action<T> action, Action actionIfNone)
        {
            Checker.NotNull(action, "action");
            Checker.NotNull(actionIfNone, "actionIfNone");
            return source.FixSource().ForAny(action, actionIfNone);
        }

        /// <summary>
        /// If the source option contains no value, calls an action; otherwise, does nothing.
        /// </summary>
        /// <para>
        /// This method forces evaluation by making a fixed copy of the source option. The copy is
        /// returned, so it may be chained with e.g. <see cref="Opt{T}.ForAny(Action{T})"/> to
        /// implement type-safe if-else semantics.
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="actionIfNone">An action to be executed if this option is empty.</param>
        /// <returns>A fixed copy of <paramref name="source"/>.</returns>
        public static Opt<T> ForNone<T>(this IOpt<T> source, Action actionIfNone)
        {
            Checker.NotNull(actionIfNone, "actionIfNone");
            return source.FixSource().ForNone(actionIfNone);
        }

        /// <summary>
        /// Produces an option whose contents are the result of applying, in a deferred fashion, the specified selector function to the contents of this option.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IOpt<TResult> Select<TSource, TResult>(this IOpt<TSource> source, Func<TSource, TResult> selector)
        {
            IEnumerable<TSource> sourcex = source;
            return sourcex.Select(selector).SingleOpt();
        }

        /// <summary>
        /// Produces a fixed option whose contents are the result of eagerly applying the specified selector function to the contents of this option.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Opt<TResult> SelectFix<TSource, TResult>(this IOpt<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Fix().SelectFix(selector);
        }

        /// <summary>
        /// Produces an option whose contents are the result of filtering, in a deferred fashion, the contents of this option according to the specified predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IOpt<T> Where<T>(this IOpt<T> source, Func<T, bool> predicate)
        {
            IEnumerable<T> sourcex = source;
            return sourcex.SingleOpt(predicate);
        }

        /// <summary>
        /// Produces a fixed option whose contents are the result of eagerly filtering the contents of this option according to the specified predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Opt<T> WhereFix<T>(this IOpt<T> source, Func<T, bool> predicate)
        {
            return source.Fix().WhereFix(predicate);
        }

        /// <summary>
        /// Returns whether this option contains a value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static bool Any<T>(this IOpt<T> source)
        {
            return source.FixSource().Any();
        }

        /// <summary>
        /// Returns the number of values contained by this option (which is either 0 or 1).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static int Count<T>(this IOpt<T> source)
        {
            return source.FixSource().Count();
        }

        /// <summary>
        /// Returns the single value contained in this option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>The value contained in this option.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">This option is empty.</exception>
        public static T Single<T>(this IOpt<T> source)
        {
            return source.FixSource().Single();
        }

        /// <summary>
        /// Returns the single value contained in this option, if any, or the default value for its value type otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>The value contained in this option, if any, or <c>default</c>(<typeparamref name="T"/>) otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static T SingleOrDefault<T>(this IOpt<T> source)
        {
            return source.FixSource().SingleOrDefault();
        }

        /// <summary>
        /// Returns the first (and only) value contained in this option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>The value contained in this option.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">This option is empty.</exception>
        public static T First<T>(this IOpt<T> source)
        {
            return source.FixSource().First();
        }

        /// <summary>
        /// Returns the first (and only) value contained in this option, if any, or the default value for its value type otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>The value contained in this option, if any, or <c>default</c>(<typeparamref name="T"/>) otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static T FirstOrDefault<T>(this IOpt<T> source)
        {
            return source.FixSource().SingleOrDefault();
        }

        /// <summary>
        /// Returns the last (and only) value contained in this option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>The value contained in this option.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">This option is empty.</exception>
        public static T Last<T>(this IOpt<T> source)
        {
            return source.FixSource().Last();
        }

        /// <summary>
        /// Returns the last (and only) value contained in this option, if any, or the default value for its value type otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>The value contained in this option, if any, or <c>default</c>(<typeparamref name="T"/>) otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static T LastOrDefault<T>(this IOpt<T> source)
        {
            return source.FixSource().SingleOrDefault();
        }

        /// <summary>
        /// Returns the value contained at the given index in this option, if any.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than or equal to the number of elements in this option.</exception>
        public static T ElementAt<T>(this IOpt<T> source, int index)
        {
            var f = source.FixSource();
            if (index == 0 && f.Any())
            {
                return f.Single();
            }
            throw new ArgumentOutOfRangeException("index");
        }

        /// <summary>
        /// Returns the value contained at the given index in this option, if any, or the default value for its value type otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns>The value contained at the given index in this option, if any, or <c>default</c>(<typeparamref name="T"/>) if the given index is out of range.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static T ElementAtOrDefault<T>(this IOpt<T> source, int index)
        {
            var f = source.FixSource();
            return index == 0 ? f.SingleOrDefault() : default(T);
        }

        /// <summary>
        /// Returns an empty option having the same value type as this option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Opt<T> Cleared<T>(this IOpt<T> source)
        {
            return Opt.Empty<T>();
        }

        /// <summary>
        /// Obsolete; replace with <see cref="FallbackValueFix{T}(IOpt{T}, T)"/>.
        /// </summary>
        [Obsolete("Use FallbackValueFix() instead.")]
        public static Opt<T> FillWithValueFix<T>(this IOpt<T> source, T value)
        {
            return FallbackValueFix(source, value);
        }

        /// <summary>
        /// Returns (immediately, non-deferred) an option which contains the value from this option, if any, or another option containing the specified value, otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Opt<T> FallbackValueFix<T>(this IOpt<T> source, T value)
        {
            var opt = source.FixSource();
            return opt.Any() ? opt : Opt.Full(value);
        }

        /// <summary>
        /// Obsolete; replace with <see cref="FallbackFix{T}(IOpt{T}, Func{Opt{T}})"/>.
        /// </summary>
        [Obsolete("Use FallbackFix() instead.")]
        public static Opt<T> FillWithResultFix<T>(this IOpt<T> source, Func<Opt<T>> getResult)
        {
            return FallbackFix(source, getResult);
        }

        /// <summary>
        /// Returns (immediately, non-deferred) an option which contains the value from this option, if any, or another option containing the result of the specified function, otherwise.
        /// The specified function is not called unless <paramref name="source"/> is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getResult"></param>
        /// <returns></returns>
        public static Opt<T> FallbackFix<T>(this IOpt<T> source, Func<Opt<T>> getResult)
        {
            Checker.NotNull(getResult, "getResult");
            var opt = source.FixSource();
            return opt.Any() ? opt : getResult();
        }

        /// <summary>
        /// Obsolete; replace with <see cref="FallbackFix{T}(IOpt{T}, IOpt{T})"/>.
        /// </summary>
        [Obsolete("Use FallbackFix() instead.")]
        public static Opt<T> SubstituteIfEmptyFix<T>(this IOpt<T> source, IOpt<T> fallback)
        {
            return FallbackFix(source, fallback);
        }

        /// <summary>
        /// Returns (immediately, non-deferred) an option which contains the value from this option, if any, or the value from the specified fallback option, otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Opt<T> FallbackFix<T>(this IOpt<T> source, IOpt<T> option)
        {
            Checker.NotNull(option, "option");
            var opt = source.FixSource();
            return opt.Any() ? opt : option.Fix();
        }

        /// <summary>
        /// Obsolete; replace with <see cref="FallbackValue{T}(IOpt{T}, T)"/>.
        /// </summary>
        [Obsolete("Use FallbackValue() instead.")]
        public static IOpt<T> FillWithValue<T>(this IOpt<T> source, T value)
        {
            return FallbackValue(source, value);
        }

        /// <summary>
        /// Returns (in a deferred fashion) an option which contains the value from this option, if any, or another option containing the specified value, otherwise.
        /// The value of <paramref name="source"/> is reevaluated every time the returned option's value is retrieved.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IOpt<T> FallbackValue<T>(this IOpt<T> source, T value)
        {
            Checker.NotNull(source, "source");
            return Opt.Defer(() => source.FallbackValueFix(value));
        }

        /// <summary>
        /// Obsolete; replace with <see cref="Fallback{T}(IOpt{T}, Func{Opt{T}})"/>.
        /// </summary>
        [Obsolete("Use Fallback() instead.")]
        public static IOpt<T> FillWithResult<T>(this IOpt<T> source, Func<Opt<T>> getResult)
        {
            return Fallback(source, getResult);
        }

        /// <summary>
        /// Returns (in a deferred fashion) an option which contains the value from this option, if any, or another option containing the result of the specified function, otherwise.
        /// The specified function is not called unless <paramref name="source"/> is empty.
        /// The value of <paramref name="source"/> (and the result of getResult(), where applicable) is reevaluated every time the returned option's value is retrieved.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getResult"></param>
        /// <returns></returns>
        public static IOpt<T> Fallback<T>(this IOpt<T> source, Func<Opt<T>> getResult)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(getResult, "getResult");
            return Opt.Defer(() => source.FallbackFix(getResult));
        }

        /// <summary>
        /// Obsolete; replace with <see cref="Fallback{T}(IOpt{T}, IOpt{T})"/>.
        /// </summary>
        [Obsolete("Use Fallback() instead.")]
        public static IOpt<T> SubstituteIfEmpty<T>(this IOpt<T> source, IOpt<T> fallback)
        {
            return Fallback(source, fallback);
        }

        /// <summary>
        /// Returns (in a deferred fashion) an option which contains the value from this option, if any, or the value from the specified fallback option, otherwise.
        /// The value of <paramref name="source"/> (and of <paramref name="option"/>, if <paramref name="source"/> is currently empty) is reevaluated every time the returned option's value is retrieved.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IOpt<T> Fallback<T>(this IOpt<T> source, IOpt<T> option)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(option, "option");
            return Opt.Defer(() => source.FallbackFix(option));
        }

        /// <summary>
        /// Collapses an option-containing option into an option representing
        /// the contents of the inner option, if any. If the outer and inner options are both full,
        /// the result is full; otherwise, the result is empty.
        /// </summary>
        /// <para>
        /// The result is not deferred but computed immediately.
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Opt<T> FlattenFixOpt<T>(this IOpt<IOpt<T>> source)
        {
            Checker.NotNull(source, "source");
            IOpt<T> inner;
            return source.TryGetSingle(out inner) ? inner.Fix() : Opt.Empty<T>();
        }

        /// <summary>
        /// Collapses an option-containing option into an option representing
        /// the contents of the inner option, if any. If the outer and inner options are both full,
        /// the result is full; otherwise, the result is empty.
        /// </summary>
        /// <para>
        /// The result is not deferred but computed immediately.
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Opt<T> FlattenFixOpt<T>(this IOpt<Opt<T>> source)
        {
            Checker.NotNull(source, "source");
            Opt<T> inner;
            return source.TryGetSingle(out inner) ? inner.Fix() : Opt.Empty<T>();
        }

        /// <summary>
        /// Collapses an option-containing option into an option representing
        /// the contents of the inner option, if any. If the outer and inner options are both full,
        /// the result is full; otherwise, the result is empty.
        /// </summary>
        /// <para>
        /// The result is deferred.
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IOpt<T> FlattenOpt<T>(this IOpt<IOpt<T>> source)
        {
            Checker.NotNull(source, "source");
            return Opt.Defer<T>(() => source.FlattenFixOpt());
        }

        /// <summary>
        /// Collapses an option-containing option into an option representing
        /// the contents of the inner option, if any. If the outer and inner options are both full,
        /// the result is full; otherwise, the result is empty.
        /// </summary>
        /// <para>
        /// The result is deferred.
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IOpt<T> FlattenOpt<T>(this IOpt<Opt<T>> source)
        {
            Checker.NotNull(source, "source");
            return Opt.Defer<T>(() => source.FlattenFixOpt());
        }

        // Private abbr for `source.Fix()` with a null check
        private static Opt<T> FixSource<T>(this IOpt<T> source)
        {
            return Checker.NotNull(source, "source").Fix();
        }

        #region Generated zipping methods

        // The methods in this region are generated for use with up to 8 parameters.

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
		public static IOpt<Tuple<T1>> Zip<T1>(this IOpt<T1> item1Opt)
        {
            return Opt.Zip(item1Opt);
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
        public static IOpt<TResult> Zip<T1, TResult>(this IOpt<T1> item1Opt, Func<T1, TResult> selector)
        {
            return Opt.Zip(item1Opt, selector);
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
        public static Opt<Tuple<T1>> ZipFix<T1>(this IOpt<T1> item1Opt)
        {
            return Opt.ZipFix(item1Opt);
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
        public static Opt<TResult> ZipFix<T1, TResult>(this IOpt<T1> item1Opt, Func<T1, TResult> selector)
        {
            return Opt.ZipFix(item1Opt, selector);
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
		public static IOpt<Tuple<T1, T2>> Zip<T1, T2>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt)
        {
            return Opt.Zip(item1Opt, item2Opt);
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
        public static IOpt<TResult> Zip<T1, T2, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, Func<T1, T2, TResult> selector)
        {
            return Opt.Zip(item1Opt, item2Opt, selector);
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
        public static Opt<Tuple<T1, T2>> ZipFix<T1, T2>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt)
        {
            return Opt.ZipFix(item1Opt, item2Opt);
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
        public static Opt<TResult> ZipFix<T1, T2, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, Func<T1, T2, TResult> selector)
        {
            return Opt.ZipFix(item1Opt, item2Opt, selector);
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
		public static IOpt<Tuple<T1, T2, T3>> Zip<T1, T2, T3>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt);
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
        public static IOpt<TResult> Zip<T1, T2, T3, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, Func<T1, T2, T3, TResult> selector)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, selector);
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
        public static Opt<Tuple<T1, T2, T3>> ZipFix<T1, T2, T3>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt);
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
        public static Opt<TResult> ZipFix<T1, T2, T3, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, Func<T1, T2, T3, TResult> selector)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, selector);
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
		public static IOpt<Tuple<T1, T2, T3, T4>> Zip<T1, T2, T3, T4>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt);
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
        public static IOpt<TResult> Zip<T1, T2, T3, T4, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, Func<T1, T2, T3, T4, TResult> selector)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, selector);
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
        public static Opt<Tuple<T1, T2, T3, T4>> ZipFix<T1, T2, T3, T4>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt);
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
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, Func<T1, T2, T3, T4, TResult> selector)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, selector);
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
		public static IOpt<Tuple<T1, T2, T3, T4, T5>> Zip<T1, T2, T3, T4, T5>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt);
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
        public static IOpt<TResult> Zip<T1, T2, T3, T4, T5, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, Func<T1, T2, T3, T4, T5, TResult> selector)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, selector);
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
        public static Opt<Tuple<T1, T2, T3, T4, T5>> ZipFix<T1, T2, T3, T4, T5>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt);
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
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, T5, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, Func<T1, T2, T3, T4, T5, TResult> selector)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, selector);
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
		public static IOpt<Tuple<T1, T2, T3, T4, T5, T6>> Zip<T1, T2, T3, T4, T5, T6>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt);
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
        public static IOpt<TResult> Zip<T1, T2, T3, T4, T5, T6, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, Func<T1, T2, T3, T4, T5, T6, TResult> selector)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, selector);
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
        public static Opt<Tuple<T1, T2, T3, T4, T5, T6>> ZipFix<T1, T2, T3, T4, T5, T6>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt);
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
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, T5, T6, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, Func<T1, T2, T3, T4, T5, T6, TResult> selector)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, selector);
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
		public static IOpt<Tuple<T1, T2, T3, T4, T5, T6, T7>> Zip<T1, T2, T3, T4, T5, T6, T7>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt);
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
        public static IOpt<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, selector);
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
        public static Opt<Tuple<T1, T2, T3, T4, T5, T6, T7>> ZipFix<T1, T2, T3, T4, T5, T6, T7>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt);
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
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, T5, T6, T7, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, selector);
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
		public static IOpt<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>> Zip<T1, T2, T3, T4, T5, T6, T7, T8>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, item8Opt);
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
        public static IOpt<TResult> Zip<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
        {
            return Opt.Zip(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, item8Opt, selector);
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
        public static Opt<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>> ZipFix<T1, T2, T3, T4, T5, T6, T7, T8>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, item8Opt);
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
        public static Opt<TResult> ZipFix<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IOpt<T1> item1Opt, IOpt<T2> item2Opt, IOpt<T3> item3Opt, IOpt<T4> item4Opt, IOpt<T5> item5Opt, IOpt<T6> item6Opt, IOpt<T7> item7Opt, IOpt<T8> item8Opt, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
        {
            return Opt.ZipFix(item1Opt, item2Opt, item3Opt, item4Opt, item5Opt, item6Opt, item7Opt, item8Opt, selector);
        }

        #endregion Generated zipping methods
    }
}