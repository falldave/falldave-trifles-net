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
        /// Returns (immediately, non-deferred) an option which contains the value from this option, if any, or another option containing the specified value, otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Opt<T> FillWithValueFix<T>(this IOpt<T> source, T value)
        {
            var opt = source.FixSource();
            return opt.Any() ? opt : Opt.Full(value);
        }

        /// <summary>
        /// Returns (immediately, non-deferred) an option which contains the value from this option, if any, or another option containing the result of the specified function, otherwise.
        /// The specified function is not called unless <paramref name="source"/> is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getResult"></param>
        /// <returns></returns>
        public static Opt<T> FillWithResultFix<T>(this IOpt<T> source, Func<Opt<T>> getResult)
        {
            Checker.NotNull(getResult, "getResult");
            var opt = source.FixSource();
            return opt.Any() ? opt : getResult();
        }

        /// <summary>
        /// Returns (immediately, non-deferred) an option which contains the value from this option, if any, or the value from the specified fallback option, otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public static Opt<T> SubstituteIfEmptyFix<T>(this IOpt<T> source, IOpt<T> fallback)
        {
            Checker.NotNull(fallback, "fallback");
            var opt = source.FixSource();
            return opt.Any() ? opt : fallback.Fix();
        }

        /// <summary>
        /// Returns (in a deferred fashion) an option which contains the value from this option, if any, or another option containing the specified value, otherwise.
        /// The value of <paramref name="source"/> is reevaluated every time the returned option's value is retrieved.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IOpt<T> FillWithValue<T>(this IOpt<T> source, T value)
        {
            Checker.NotNull(source, "source");
            return Opt.Defer(() => source.FillWithValueFix(value));
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
        public static IOpt<T> FillWithResult<T>(this IOpt<T> source, Func<Opt<T>> getResult)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(getResult, "getResult");
            return Opt.Defer(() => source.FillWithResultFix(getResult));
        }

        /// <summary>
        /// Returns (in a deferred fashion) an option which contains the value from this option, if any, or the value from the specified fallback option, otherwise.
        /// The value of <paramref name="source"/> (and of <paramref name="fallback"/>, if <paramref name="source"/> is currently empty) is reevaluated every time the returned option's value is retrieved.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public static IOpt<T> SubstituteIfEmpty<T>(this IOpt<T> source, IOpt<T> fallback)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(fallback, "fallback");
            return Opt.Defer(() => source.SubstituteIfEmptyFix(fallback));
        }

        // Private abbr for `source.Fix()` with a null check
        private static Opt<T> FixSource<T>(this IOpt<T> source)
        {
            return Checker.NotNull(source, "source").Fix();
        }
    }
}
