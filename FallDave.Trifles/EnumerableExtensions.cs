//-----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="falldave">
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
    /// Utility extensions applicable to <see cref="IEnumerable{T}"/> instances.
    /// </summary>
    public static class EnumerableExtensions
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

        #region Opt<T>-returning *FixOpt methods

        /// <summary>
        /// Returns a fixed option containing the only element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="predicate">A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.</param>
        /// <returns>An <see cref="Opt{T}"/> containing the single element of the sequence (after any filtering), or no elements otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">The sequence contains more than one element.</exception>
        /// <exception cref="InvalidOperationException">The sequence contains more than one matching element.</exception>
        public static Opt<T> SingleFixOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return SingleAsInit(source, predicate, usingPredicate).AsOpt();
        }

        /// <summary>
        /// Returns a fixed option containing the first element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="predicate">A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.</param>
        /// <returns>An <see cref="Opt{T}"/> containing the first element of the sequence (after any filtering), or no elements otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> FirstFixOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return FirstAsInit(source, predicate, usingPredicate).AsOpt();
        }

        /// <summary>
        /// Returns a fixed option containing the last element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="predicate">A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.</param>
        /// <returns>An <see cref="Opt{T}"/> containing the last element of the sequence (after any filtering), or no elements otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> LastFixOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return LastAsInit(source, predicate, usingPredicate).AsOpt();
        }

        /// <summary>
        /// Returns a fixed option containing the element at the specified index of a sequence,
        /// or an empty fixed option if the index is out of range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>An <see cref="Opt{T}"/> containing the last element of the sequence (after any filtering), or no elements otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> ElementAtFixOpt<T>(this IEnumerable<T> source, int index)
        {
            Checker.NotNull(source, "source");
            return ElementAtAsInit(source, index).AsOpt();
        }

        /// <summary>
        /// Returns a fixed option containing the only element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty;
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerator from which to extract the element.</param>
        /// <param name="predicate">A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.</param>
        /// <returns>An <see cref="Opt{T}"/> containing the single element of the sequence (after any filtering), or no elements otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">The sequence contains more than one element.</exception>
        /// <exception cref="InvalidOperationException">The sequence contains more than one matching element.</exception>
        public static Opt<T> SingleFixOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return SingleAsInit(source, predicate, usingPredicate).AsOpt();
        }

        /// <summary>
        /// Returns a fixed option containing the first element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerator from which to extract the element.</param>
        /// <param name="predicate">A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.</param>
        /// <returns>An <see cref="Opt{T}"/> containing the first element of the sequence (after any filtering), or no elements otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> FirstFixOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return FirstAsInit(source, predicate, usingPredicate).AsOpt();
        }

        /// <summary>
        /// Returns a fixed option containing the last element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerator from which to extract the element.</param>
        /// <param name="predicate">A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.</param>
        /// <returns>An <see cref="Opt{T}"/> containing the last element of the sequence (after any filtering), or no elements otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> LastFixOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return LastAsInit(source, predicate, usingPredicate).AsOpt();
        }

        /// <summary>
        /// Returns a fixed option containing the element at the specified index of a sequence,
        /// or an empty fixed option if the index is out of range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An enumerator from which to extract the element.</param>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>An <see cref="Opt{T}"/> containing the last element of the sequence (after any filtering), or no elements otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> ElementAtFixOpt<T>(this IEnumerator<T> source, int index)
        {
            Checker.NotNull(source, "source");
            return ElementAtAsInit(source, index).AsOpt();
        }

        #endregion

        #region TryGet* methods

        /// <summary>
        /// Gets the single value (or single matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">Set to the single value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <returns><c>true</c> if <paramref name="source"/> (after any filtering) contains a single element, or <c>false</c> if empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">At the time of evaluation, the source sequence (after any filtering) contains more than one element.</exception>
        public static bool TryGetSingle<T>(this IEnumerable<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.SingleFixOpt(predicate).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the first value (or first matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">Set to the first value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <returns><c>true</c> if <paramref name="source"/> (after any filtering) contains a first element, or <c>false</c> if empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static bool TryGetFirst<T>(this IEnumerable<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.FirstFixOpt(predicate).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the last value (or last matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">Set to the last value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <returns><c>true</c> if <paramref name="source"/> (after any filtering) contains a last element, or <c>false</c> if empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static bool TryGetLast<T>(this IEnumerable<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.LastFixOpt(predicate).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the element at the specified index of the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <param name="value">Set to the value at the given index, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.</param>
        /// <returns><c>true</c> if <paramref name="source"/> contains an element at the given index, or <c>false</c> if the index is out of range.</returns>
        public static bool TryGetElementAt<T>(this IEnumerable<T> source, int index, out T value)
        {
            return source.ElementAtFixOpt(index).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the single value (or single matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">Set to the single value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <returns><c>true</c> if <paramref name="source"/> (after any filtering) contains a single element, or <c>false</c> if empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">At the time of evaluation, the source sequence (after any filtering) contains more than one element.</exception>
        public static bool TryGetSingle<T>(this IEnumerator<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.SingleFixOpt(predicate).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the first value (or first matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">Set to the first value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <returns><c>true</c> if <paramref name="source"/> (after any filtering) contains a first element, or <c>false</c> if empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static bool TryGetFirst<T>(this IEnumerator<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.FirstFixOpt(predicate).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the last value (or last matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">Set to the last value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <returns><c>true</c> if <paramref name="source"/> (after any filtering) contains a last element, or <c>false</c> if empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static bool TryGetLast<T>(this IEnumerator<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.LastFixOpt(predicate).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the element at the specified index of the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <param name="value">Set to the value at the given index, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.</param>
        /// <returns><c>true</c> if <paramref name="source"/> contains an element at the given index, or <c>false</c> if the index is out of range.</returns>
        public static bool TryGetElementAt<T>(this IEnumerator<T> source, int index, out T value)
        {
            return source.ElementAtFixOpt(index).TryGetSingle(out value);
        }

        #endregion

        #region *Opt

        /// <summary>
        /// Returns a deferred option that will contain the source's only element (if the source contains exactly one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contain the entire source sequence if and only if the sequence has no more than one element.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <exception cref="InvalidOperationException">At the time of evaluation, the source sequence contains more than one element.</exception>
        public static IOpt<T> SingleOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.SingleFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's first element (if the source contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the first element of the source sequence, if the source is not empty, or no element if the source is empty.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> FirstOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.FirstFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's last element (if the source contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the last element of the source sequence, if the source is not empty, or no element if the source is empty.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> LastOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.LastFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the element of the source at the given index (if the index is not out of range) or no elements (if the index is out of range).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the element of the source sequence at the given index, if the index is in range (at least zero and no greater than the element count of the source), or no element if the index is out of range.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="index">The index of the value to retrieve.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> ElementAtOpt<T>(this IEnumerable<T> source, int index)
        {
            return new DeferredOpt<T>(() => source.ElementAtFixOpt(index));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's only element (if the source contains exactly one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contain the entire source sequence if and only if the sequence has no more than one element.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <exception cref="InvalidOperationException">At the time of evaluation, the source sequence contains more than one element.</exception>
        public static IOpt<T> SingleOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.SingleFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's first element (if the source contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the first element of the source sequence, if the source is not empty, or no element if the source is empty.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> FirstOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.FirstFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's last element (if the source contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the last element of the source sequence, if the source is not empty, or no element if the source is empty.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> LastOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.LastFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the element of the source at the given index (if the index is not out of range) or no elements (if the index is out of range).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the element of the source sequence at the given index, if the index is in range (at least zero and no greater than the element count of the source), or no element if the index is out of range.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="index">The index of the value to retrieve.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> ElementAtOpt<T>(this IEnumerator<T> source, int index)
        {
            return new DeferredOpt<T>(() => source.ElementAtFixOpt(index));
        }
        
        #endregion

        #region *AsInit for Single, First, Last, ElementAt.

        private static OptInit<T> SingleAsInit<T>(IEnumerable<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new OptInit<T>();
            if (source is IList<T> && !usingPredicate)
            {
                var list = (IList<T>)source;
                switch (list.Count)
                {
                    case 0:
                        // do nothing
                        break;
                    case 1:
                        // use element
                        init.Value = list[0];
                        break;
                    default:
                        throw Errors.MoreThanOneElement();
                }
            }
            else
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        if (init.HasValue)
                        {
                            throw Errors.MoreThanOneElement(usingPredicate);
                        }
                        else
                        {
                            init.Value = item;
                        }
                    }
                }
            }

            return init;
        }

        private static OptInit<T> FirstAsInit<T>(IEnumerable<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new OptInit<T>();
            if (source is IList<T> && !usingPredicate)
            {
                var list = (IList<T>)source;
                if (list.Count > 0)
                {
                    init.Value = list[0];
                }
            }
            else
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        init.Value = item;
                        break;
                    }
                }
            }

            return init;
        }

        private static OptInit<T> LastAsInit<T>(IEnumerable<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new OptInit<T>();
            if (source is IList<T> && !usingPredicate)
            {
                var list = (IList<T>)source;
                var count = list.Count;
                if (count > 0)
                {
                    init.Value = list[count - 1];
                }
            }
            else
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        init.Value = item;
                    }
                }
            }

            return init;
        }

        private static OptInit<T> ElementAtAsInit<T>(IEnumerable<T> source, int index)
        {
            var init = new OptInit<T>();
            if (source is IList<T>)
            {
                var list = (IList<T>)source;
                var count = list.Count;
                if (index > 0 && index < count)
                {
                    init.Value = list[index];
                }
            }
            else
            {
                var leftToSkip = index;

                foreach (var item in source)
                {
                    if (leftToSkip == 0)
                    {
                        init.Value = item;
                        break;
                    }

                    leftToSkip--;
                }
            }

            return init;
        }

        private static OptInit<T> SingleAsInit<T>(IEnumerator<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new OptInit<T>();

            while (source.MoveNext())
            {
                var item = source.Current;

                if (predicate(item))
                {
                    if (init.HasValue)
                    {
                        throw Errors.MoreThanOneElement(usingPredicate);
                    }
                    else
                    {
                        init.Value = item;
                    }
                }
            }

            return init;
        }

        private static OptInit<T> FirstAsInit<T>(IEnumerator<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new OptInit<T>();

            while (source.MoveNext())
            {
                var item = source.Current;

                if (predicate(item))
                {
                    init.Value = item;
                    break;
                }
            }

            return init;
        }

        private static OptInit<T> LastAsInit<T>(IEnumerator<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new OptInit<T>();

            while (source.MoveNext())
            {
                var item = source.Current;

                if (predicate(item))
                {
                    init.Value = item;
                }
            }

            return init;
        }

        private static OptInit<T> ElementAtAsInit<T>(IEnumerator<T> source, int index)
        {
            var init = new OptInit<T>();

            var leftToSkip = index;

            while (source.MoveNext())
            {
                var item = source.Current;

                if (leftToSkip == 0)
                {
                    init.Value = item;
                    break;
                }

                leftToSkip--;
            }

            return init;
        }

        #endregion

        // If the given predicate is null, it is changed to an always-true predicate. Returns
        // whether the initial predicate was non-null. In exception messages this is the difference
        // between say "no elements" and "no matching elements".
        private static bool SetUpPredicate<T>(ref Func<T, bool> predicate)
        {
            var usingPredicate = true;
            if (predicate == null)
            {
                usingPredicate = false;
                predicate = item => true;
            }

            return usingPredicate;
        }

        #region OptInit

        // A sort of mutable Opt builder.
        // Initially hasValue = false, valueBuffer = default(T).
        // When Value is set to a value, hasValue becomes true and cannot be reverted to false.
        private struct OptInit<T>
        {
            /// <summary>
            /// If true, the value is <see cref="valueBuffer"/>. If false, there is no value.
            /// </summary>
            private bool hasValue;

            /// <summary>
            /// Contains the value of this option, if any. Otherwise, should contain <c>default(</c><typeparamref name="T"/><c>)</c>.
            /// </summary>
            private T valueBuffer;

            public bool HasValue
            {
                get { return hasValue; }
            }

            public T Value
            {
                get
                {
                    return hasValue ? valueBuffer : default(T);
                }

                set
                {
                    hasValue = true;
                    valueBuffer = value;
                }
            }

            public Opt<T> AsOpt()
            {
                return Opt.Create(hasValue, Value);
            }
        }

        #endregion
    }
}
