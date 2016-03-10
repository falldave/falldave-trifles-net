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

    /// <summary>
    /// Extension methods applicable to <see cref="IOpt{T}"/> instances.
    /// </summary>
    public static class OptExtensions
    {
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
            if(index == 0 && f.Any())
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

        // Private abbr for `source.Fix()` with a null check
        private static Opt<T> FixSource<T>(this IOpt<T> source)
        {
            return Checker.NotNull(source, "source").Fix();
        }
    }
}
