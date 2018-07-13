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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    /// <summary>
    /// Utility extensions applicable to <see cref="IEnumerable{T}"/> instances.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Opt facilities

        #region *OrValue and *OrResult

        private static T ComputeSingleOrValue<T>(this Opt<T> fixedOpt, T value)
        {
            return fixedOpt.FallbackValueFix(value).Single();
        }

        private static T ComputeSingleOrResult<T>(this Opt<T> fixedOpt, Func<Opt<T>> getResult)
        {
            Checker.NotNull(getResult, "getResult");
            return fixedOpt.FallbackFix(getResult).Single();
        }

        /// <summary>
        /// Returns the single value in this sequence, if any, or the specified fallback value if the
        /// sequence is empty. (Throws if the sequence, after any filtering, contains more than one element.)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T SingleOrValue<T>(this IEnumerable<T> source, T value, Func<T, bool> predicate = null)
        {
            return source.SingleFixOpt(predicate).ComputeSingleOrValue(value);
        }

        /// <summary>
        /// Returns the single value in this sequence, if any, or the result of the specified
        /// function if the sequence is empty. (Throws if the sequence, after any filtering, contains
        /// more than one element.)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getResult"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T SingleOrResult<T>(this IEnumerable<T> source, Func<Opt<T>> getResult, Func<T, bool> predicate = null)
        {
            return source.SingleFixOpt(predicate).ComputeSingleOrResult(getResult);
        }

        /// <summary>
        /// Returns the first value in this sequence, if any, or the specified fallback value if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T FirstOrValue<T>(this IEnumerable<T> source, T value, Func<T, bool> predicate = null)
        {
            return source.FirstFixOpt(predicate).ComputeSingleOrValue(value);
        }

        /// <summary>
        /// Returns the first value in this sequence, if any, or the result of the specified function
        /// if the sequence is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getResult"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T FirstOrResult<T>(this IEnumerable<T> source, Func<Opt<T>> getResult, Func<T, bool> predicate = null)
        {
            return source.FirstFixOpt(predicate).ComputeSingleOrResult(getResult);
        }

        /// <summary>
        /// Returns the last value in this sequence, if any, or the specified fallback value if the
        /// sequence is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T LastOrValue<T>(this IEnumerable<T> source, T value, Func<T, bool> predicate = null)
        {
            return source.LastFixOpt(predicate).ComputeSingleOrValue(value);
        }

        /// <summary>
        /// Returns the last value in this sequence, if any, or the result of the specified function
        /// if the sequence is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getResult"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T LastOrResult<T>(this IEnumerable<T> source, Func<Opt<T>> getResult, Func<T, bool> predicate = null)
        {
            return source.LastFixOpt(predicate).ComputeSingleOrResult(getResult);
        }

        /// <summary>
        /// Returns the element at the specified position in the sequence, if any, or the specified
        /// fallback value if there is no such element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T ElementAtOrValue<T>(this IEnumerable<T> source, T value, int index)
        {
            return source.ElementAtFixOpt(index).ComputeSingleOrValue(value);
        }

        /// <summary>
        /// Returns the element at the specified position in the sequence, if any, or the result of
        /// the specified function if there is no such element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getResult"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T ElementAtOrResult<T>(this IEnumerable<T> source, Func<Opt<T>> getResult, int index)
        {
            return source.ElementAtFixOpt(index).ComputeSingleOrResult(getResult);
        }

        #endregion *OrValue and *OrResult

        #region Opt<T>-returning *FixOpt methods

        /// <summary>
        /// Returns a fixed option containing the only element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty; this method throws an exception if
        /// there is more than one element in the sequence.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="predicate">
        /// A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.
        /// </param>
        /// <returns>
        /// An <see cref="Opt{T}"/> containing the single element of the sequence (after any
        /// filtering), or no elements otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// The sequence contains more than one element.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The sequence contains more than one matching element.
        /// </exception>
        public static Opt<T> SingleFixOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return ComputeSingleFixOpt(source, predicate, usingPredicate);
        }

        /// <summary>
        /// Returns a counted option containing the only element of a sequence (after any filtering),
        /// or an empty counted option if the sequence is empty or there is more than one element in
        /// the sequence. The counted option contains an indication whether the sequence length was
        /// 0, 1, or greater than 1.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="predicate">
        /// A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.
        /// </param>
        /// <returns>
        /// A <see cref="CountedOpt{T}"/> containing the single element of the sequence (after any
        /// filtering), or no elements otherwise, and indicates whether the sequence was empty,
        /// single-element, or multiple-element.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static CountedOpt<T> CountSingleFixOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return ComputeCountSingleFixOpt(source, predicate, usingPredicate);
        }

        /// <summary>
        /// Returns a fixed option containing the first element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="predicate">
        /// A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.
        /// </param>
        /// <returns>
        /// An <see cref="Opt{T}"/> containing the first element of the sequence (after any
        /// filtering), or no elements otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> FirstFixOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return ComputeFirstFixOpt(source, predicate, usingPredicate);
        }

        /// <summary>
        /// Returns a fixed option containing the last element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="predicate">
        /// A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.
        /// </param>
        /// <returns>
        /// An <see cref="Opt{T}"/> containing the last element of the sequence (after any
        /// filtering), or no elements otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> LastFixOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return ComputeLastFixOpt(source, predicate, usingPredicate);
        }

        /// <summary>
        /// Returns a fixed option containing the element at the specified index of a sequence, or an
        /// empty fixed option if the index is out of range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>
        /// An <see cref="Opt{T}"/> containing the last element of the sequence (after any
        /// filtering), or no elements otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> ElementAtFixOpt<T>(this IEnumerable<T> source, int index)
        {
            Checker.NotNull(source, "source");
            return ComputeElementAtFixOpt(source, index);
        }

        /// <summary>
        /// Returns a fixed option containing the only element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty; this method throws an exception if
        /// there is more than one element in the sequence.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerator from which to extract the element.</param>
        /// <param name="predicate">
        /// A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.
        /// </param>
        /// <returns>
        /// An <see cref="Opt{T}"/> containing the single element of the sequence (after any
        /// filtering), or no elements otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// The sequence contains more than one element.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The sequence contains more than one matching element.
        /// </exception>
        public static Opt<T> SingleFixOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return ComputeSingleFixOpt(source, predicate, usingPredicate);
        }

        /// <summary>
        /// Returns a counted option containing the only element of a sequence (after any filtering),
        /// or an empty counted option if the sequence is empty or there is more than one element in
        /// the sequence. The counted option contains an indication whether the sequence length was
        /// 0, 1, or greater than 1.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerable from which to extract the element.</param>
        /// <param name="predicate">
        /// A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.
        /// </param>
        /// <returns>
        /// A <see cref="CountedOpt{T}"/> containing the single element of the sequence (after any
        /// filtering), or no elements otherwise, and indicates whether the sequence was empty,
        /// single-element, or multiple-element.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static CountedOpt<T> CountSingleFixOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return ComputeCountSingleFixOpt(source, predicate, usingPredicate);
        }

        /// <summary>
        /// Returns a fixed option containing the first element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerator from which to extract the element.</param>
        /// <param name="predicate">
        /// A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.
        /// </param>
        /// <returns>
        /// An <see cref="Opt{T}"/> containing the first element of the sequence (after any
        /// filtering), or no elements otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> FirstFixOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return ComputeFirstFixOpt(source, predicate, usingPredicate);
        }

        /// <summary>
        /// Returns a fixed option containing the last element of a sequence (after any filtering),
        /// or an empty fixed option if the sequence is empty.
        /// </summary>
        /// <para>The computation of the result of this method is performed immediately, not deferred.</para>
        /// <typeparam name="T">The type of the contained value of the option.</typeparam>
        /// <param name="source">An enumerator from which to extract the element.</param>
        /// <param name="predicate">
        /// A predicate by which to filter <paramref name="source"/>; if <c>null</c>, no filtering is performed.
        /// </param>
        /// <returns>
        /// An <see cref="Opt{T}"/> containing the last element of the sequence (after any
        /// filtering), or no elements otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> LastFixOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return ComputeLastFixOpt(source, predicate, usingPredicate);
        }

        /// <summary>
        /// Returns a fixed option containing the element at the specified index of a sequence, or an
        /// empty fixed option if the index is out of range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An enumerator from which to extract the element.</param>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>
        /// An <see cref="Opt{T}"/> containing the last element of the sequence (after any
        /// filtering), or no elements otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static Opt<T> ElementAtFixOpt<T>(this IEnumerator<T> source, int index)
        {
            Checker.NotNull(source, "source");
            return ComputeElementAtFixOpt(source, index);
        }

        #endregion Opt<T>-returning *FixOpt methods

        #region TryGet* methods

        /// <summary>
        /// Gets the single value (or single matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">
        /// Set to the single value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.
        /// </param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="source"/> (after any filtering) contains a single element,
        /// or <c>false</c> if empty.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// At the time of evaluation, the source sequence (after any filtering) contains more than
        /// one element.
        /// </exception>
        public static bool TryGetSingle<T>(this IEnumerable<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.SingleFixOpt(predicate).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the first value (or first matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">
        /// Set to the first value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.
        /// </param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="source"/> (after any filtering) contains a first element,
        /// or <c>false</c> if empty.
        /// </returns>
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
        /// <param name="value">
        /// Set to the last value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.
        /// </param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="source"/> (after any filtering) contains a last element,
        /// or <c>false</c> if empty.
        /// </returns>
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
        /// <param name="value">
        /// Set to the value at the given index, if available; otherwise, set to
        /// <c>default(</c><typeparamref name="T"/><c>)</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="source"/> contains an element at the given index, or
        /// <c>false</c> if the index is out of range.
        /// </returns>
        public static bool TryGetElementAt<T>(this IEnumerable<T> source, int index, out T value)
        {
            return source.ElementAtFixOpt(index).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the single value (or single matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">
        /// Set to the single value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.
        /// </param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="source"/> (after any filtering) contains a single element,
        /// or <c>false</c> if empty.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// At the time of evaluation, the source sequence (after any filtering) contains more than
        /// one element.
        /// </exception>
        public static bool TryGetSingle<T>(this IEnumerator<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.SingleFixOpt(predicate).TryGetSingle(out value);
        }

        /// <summary>
        /// Gets the first value (or first matching value) contained in the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="value">
        /// Set to the first value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.
        /// </param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="source"/> (after any filtering) contains a first element,
        /// or <c>false</c> if empty.
        /// </returns>
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
        /// <param name="value">
        /// Set to the last value, if available; otherwise, set to <c>default(</c><typeparamref name="T"/><c>)</c>.
        /// </param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="source"/> (after any filtering) contains a last element,
        /// or <c>false</c> if empty.
        /// </returns>
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
        /// <param name="value">
        /// Set to the value at the given index, if available; otherwise, set to
        /// <c>default(</c><typeparamref name="T"/><c>)</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="source"/> contains an element at the given index, or
        /// <c>false</c> if the index is out of range.
        /// </returns>
        public static bool TryGetElementAt<T>(this IEnumerator<T> source, int index, out T value)
        {
            return source.ElementAtFixOpt(index).TryGetSingle(out value);
        }

        #endregion TryGet* methods

        #region *Opt

        /// <summary>
        /// Returns a deferred option that will contain the source's only element (if the source
        /// contains exactly one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>
        /// An option which, at the point of evaluation, contain the entire source sequence if and
        /// only if the sequence has no more than one element.
        /// </returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <exception cref="InvalidOperationException">
        /// At the time of evaluation, the source sequence contains more than one element.
        /// </exception>
        public static IOpt<T> SingleOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return Opt.Defer(() => source.SingleFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's first element (if the source
        /// contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>
        /// An option which, at the point of evaluation, contains the first element of the source
        /// sequence, if the source is not empty, or no element if the source is empty.
        /// </returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> FirstOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return Opt.Defer(() => source.FirstFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's last element (if the source
        /// contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>
        /// An option which, at the point of evaluation, contains the last element of the source
        /// sequence, if the source is not empty, or no element if the source is empty.
        /// </returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> LastOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return Opt.Defer(() => source.LastFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the element of the source at the given index
        /// (if the index is not out of range) or no elements (if the index is out of range).
        /// </summary>
        /// <returns>
        /// An option which, at the point of evaluation, contains the element of the source sequence
        /// at the given index, if the index is in range (at least zero and no greater than the
        /// element count of the source), or no element if the index is out of range.
        /// </returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="index">The index of the value to retrieve.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> ElementAtOpt<T>(this IEnumerable<T> source, int index)
        {
            return Opt.Defer(() => source.ElementAtFixOpt(index));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's only element (if the source
        /// contains exactly one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>
        /// An option which, at the point of evaluation, contain the entire source sequence if and
        /// only if the sequence has no more than one element.
        /// </returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <exception cref="InvalidOperationException">
        /// At the time of evaluation, the source sequence contains more than one element.
        /// </exception>
        public static IOpt<T> SingleOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            return Opt.Defer(() => source.SingleFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's first element (if the source
        /// contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>
        /// An option which, at the point of evaluation, contains the first element of the source
        /// sequence, if the source is not empty, or no element if the source is empty.
        /// </returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> FirstOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            return Opt.Defer(() => source.FirstFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's last element (if the source
        /// contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>
        /// An option which, at the point of evaluation, contains the last element of the source
        /// sequence, if the source is not empty, or no element if the source is empty.
        /// </returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">
        /// A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.
        /// </param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> LastOpt<T>(this IEnumerator<T> source, Func<T, bool> predicate = null)
        {
            return Opt.Defer(() => source.LastFixOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the element of the source at the given index
        /// (if the index is not out of range) or no elements (if the index is out of range).
        /// </summary>
        /// <returns>
        /// An option which, at the point of evaluation, contains the element of the source sequence
        /// at the given index, if the index is in range (at least zero and no greater than the
        /// element count of the source), or no element if the index is out of range.
        /// </returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="index">The index of the value to retrieve.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> ElementAtOpt<T>(this IEnumerator<T> source, int index)
        {
            return Opt.Defer(() => source.ElementAtFixOpt(index));
        }

        #endregion *Opt

        #region Compute*FixOpt for Single, First, Last, ElementAt.

        // Essentially like *FixOpt without the parameter checks.

        #region For enumerables

        private static Opt<T> ComputeSingleFixOpt<T>(IEnumerable<T> source, Func<T, bool> predicate, bool usingPredicate = false)
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
                        init.Add(item, usingPredicate);
                    }
                }
            }

            return init.AsOpt();
        }

        private static CountedOpt<T> ComputeCountSingleFixOpt<T>(IEnumerable<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new CountedOptInit<T>();

            var list = usingPredicate ? null : source as IList<T>;

            if (list != null)
            {
                init.SetFromList(list);
            }
            else
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        if (!init.Add(item))
                        {
                            break;
                        }
                    }
                }
            }

            return init.AsCountedOpt();
        }

        private static Opt<T> ComputeFirstFixOpt<T>(IEnumerable<T> source, Func<T, bool> predicate, bool usingPredicate = false)
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

            return init.AsOpt();
        }

        private static Opt<T> ComputeLastFixOpt<T>(IEnumerable<T> source, Func<T, bool> predicate, bool usingPredicate = false)
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

            return init.AsOpt();
        }

        private static Opt<T> ComputeElementAtFixOpt<T>(IEnumerable<T> source, int index)
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

            return init.AsOpt();
        }

        #endregion For enumerables

        #region For enumerators

        private static Opt<T> ComputeSingleFixOpt<T>(IEnumerator<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new OptInit<T>();

            while (source.MoveNext())
            {
                var item = source.Current;

                if (predicate(item))
                {
                    init.Add(item, usingPredicate);
                }
            }

            return init.AsOpt();
        }

        private static CountedOpt<T> ComputeCountSingleFixOpt<T>(IEnumerator<T> source, Func<T, bool> predicate, bool usingPredicate = false)
        {
            var init = new CountedOptInit<T>();

            while (source.MoveNext())
            {
                var item = source.Current;

                if (predicate(item))
                {
                    if (!init.Add(item))
                    {
                        break;
                    }
                }
            }

            return init.AsCountedOpt();
        }

        private static Opt<T> ComputeFirstFixOpt<T>(IEnumerator<T> source, Func<T, bool> predicate, bool usingPredicate = false)
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

            return init.AsOpt();
        }

        private static Opt<T> ComputeLastFixOpt<T>(IEnumerator<T> source, Func<T, bool> predicate, bool usingPredicate = false)
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

            return init.AsOpt();
        }

        private static Opt<T> ComputeElementAtFixOpt<T>(IEnumerator<T> source, int index)
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

            return init.AsOpt();
        }

        #endregion For enumerators

        #endregion Compute*FixOpt for Single, First, Last, ElementAt.

        // If the given predicate is null, it is changed to an always-true predicate. Returns whether
        // the initial predicate was non-null. In exception messages this is the difference between
        // say "no elements" and "no matching elements".
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

        // A sort of mutable Opt builder. Initially hasValue = false, valueBuffer = default(T). When
        // Value is set to a value, hasValue becomes true and cannot be reverted to false. Add()ing a
        // value succeeds only when no value has already been set.
        private struct OptInit<T>
        {
            /// <summary>
            /// If true, the value is <see cref="valueBuffer"/>. If false, there is no value.
            /// </summary>
            private bool hasValue;

            /// <summary>
            /// Contains the value of this option, if any. Otherwise, should contain
            /// <c>default(</c><typeparamref name="T"/><c>)</c>.
            /// </summary>
            private T valueBuffer;

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

            // Sets the value iff the value is not already set. Otherwise, throws "more than one element".
            public void Add(T value, bool usingPredicate)
            {
                if (hasValue)
                {
                    throw Errors.MoreThanOneElement(usingPredicate);
                }
                else
                {
                    Value = value;
                }
            }

            public Opt<T> AsOpt()
            {
                return Opt.Create(hasValue, Value);
            }
        }

        private struct CountedOptInit<T>
        {
            private int sourceCount;
            private T value;

            public bool Set(T value)
            {
                sourceCount = 1;
                this.value = value;
                return true;
            }

            public bool SetFromList(IList<T> list)
            {
                var listCount = list.Count;
                var value = (listCount > 0) ? list[0] : default(T);
                return Set(listCount, value);
            }

            public bool Set(int sourceCount, T value)
            {
                if (sourceCount == 1)
                {
                    return Set(value);
                }

                if (sourceCount < 0)
                {
                    throw new ArgumentOutOfRangeException("sourceCount");
                }

                this.sourceCount = sourceCount;
                this.value = default(T);
                return false;
            }

            public bool Add(T value)
            {
                return Set(sourceCount + 1, value);
            }

            public CountedOpt<T> AsCountedOpt()
            {
                return new CountedOpt<T>(sourceCount, value);
            }
        }

        #endregion OptInit

        #endregion Opt facilities

        #region CountUpTo

        /// <summary>
        /// Counts the elements in a sequence until the sequence ends or a maximum count is reached,
        /// whichever occurs first.
        /// </summary>
        /// <para>
        /// This style of counting is used to determine whether the number of elements is greater
        /// than or equal to some number without necessarily traversing the entire sequence.
        /// </para>
        /// <example>
        /// This sample demonstrates how to call the method to determine if a sequence has a given
        /// correct length or is too long or too short.
        /// <code>
        /// <![CDATA[
        /// // Determine whether the sequence contains exactly correctLength elements. The maxCount
        /// // of correctLength + 1 indicates the smallest length that is too long.
        ///
        /// int count = seq.CountUpTo(correctLength + 1);
        ///
        /// if (count < correctLength)
        /// {
        /// Console.WriteLine("Sequence does not contain enough elements (length is {0})", count);
        /// }
        /// else if (count > correctLength)
        /// {
        /// // Equivalently, count == correctLength + 1, since that is the only possible value
        /// // greater than correctLength. But we write it as > for style reasons and because
        /// // that is the way we'd write it using plain count.
        /// Console.WriteLine("Sequence contains too many elements (length is greater than or equal to {0})", count);
        /// }
        /// else
        /// {
        /// Console.WriteLine("Sequence contains the correct number of elements (length is {0})", count);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <para>
        /// Implementation note: If the predicate is <c>null</c> and the source sequence is an <see
        /// cref="IList{T}"/> or <see cref="IList"/>, its count will be retrieved through such
        /// interface rather than by counting the source as an enumerable.
        /// </para>
        /// <typeparam name="T">The type of element in the source sequence.</typeparam>
        /// <param name="source">The source sequence whose elements to count.</param>
        /// <param name="maxCount">The number of elements at which to stop counting.</param>
        /// <param name="predicate">
        /// A condition the element must meet in order to be counted; <c>null</c> specifies no condition.
        /// </param>
        /// <returns>
        /// The number of elements in the source enumerable, after any filtering, or the maximum
        /// count, whichever is reached first.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxCount"/> is less than 0.
        /// </exception>
        public static int CountUpTo<T>(this IEnumerable<T> source, int maxCount, Func<T, bool> predicate = null)
        {
            Checker.NotNull(source, "source");
            if (maxCount < 0) { throw new ArgumentOutOfRangeException("maxCount"); }

            return maxCount - CountRemaining(source, maxCount, predicate);
        }

        // An inversion of CountUpTo() that counts down so that the count is only ever tested as
        // `remaining == 0`. If the source is exhausted before `remaining` is 0, return the minimum
        // number of additional elements that would have been needed to finish the count.
        private static int CountRemaining<T>(IEnumerable<T> source, int remaining, Func<T, bool> predicate)
        {
            if (remaining == 0)
            {
                // Nothing to count
                return 0;
            }
            else if (predicate == null)
            {
                {
                    // Quick count using IList/IList<T> interface

                    int listCount = -1;

                    if (source is IList)
                    {
                        listCount = ((IList)source).Count;
                    }
                    else if (source is IList<T>)
                    {
                        listCount = ((IList<T>)source).Count;
                    }

                    if (listCount >= 0)
                    {
                        var remainingAfterListCount = remaining - listCount;
                        return (remainingAfterListCount < 0) ? 0 : remainingAfterListCount;
                    }
                }

                {
                    // Iterating count

                    foreach (var element in source)
                    {
                        if (--remaining == 0) { return 0; }
                    }

                    return remaining;
                }
            }
            else
            {
                // Iterating count with filter

                foreach (var element in source)
                {
                    if (!predicate(element)) { continue; }
                    if (--remaining == 0) { return 0; }
                }

                return remaining;
            }
        }

        #endregion CountUpTo

        #region CopyTo

        /// <summary>
        /// Copies the specified items in this list to the specified array at the specified index.
        /// </summary>
        /// <remarks>
        /// This method falls back to to <see cref="Array.Copy(Array, int, Array, int, int)"/> if
        /// <paramref name="source"/> is a <c>T[]</c>, or to <see cref="ImmutableArray{T}.CopyTo(int,
        /// T[], int, int)"/> if <paramref name="source"/> is an <see cref="ImmutableArray{T}"/>.
        /// </remarks>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The list to copy from.</param>
        /// <param name="sourceIndex">The index in <paramref name="source"/> where copying begins.</param>
        /// <param name="destination">The array to copy to.</param>
        /// <param name="destinationIndex">
        /// The index in <paramref name="destination"/> where copying begins.
        /// </param>
        /// <param name="length">The number of elements to copy.</param>
        public static void CopyTo<T>(this IList<T> source, int sourceIndex, T[] destination, int destinationIndex, int length)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(destination, "destination");

            if (source is T[])
            {
                var sourceArray = (T[])source;
                Array.Copy(sourceArray, sourceIndex, destination, destinationIndex, length);
            }
            else if (source is ImmutableArray<T>)
            {
                var sourceImmutableArray = (ImmutableArray<T>)source;
                sourceImmutableArray.CopyTo(sourceIndex, destination, destinationIndex, length);
            }
            else
            {
                Checker.SpanInRange(source.Count, sourceIndex, length, "sourceIndex", "length");
                Checker.SpanInRange(destination.Length, destinationIndex, length, "destinationIndex", "length");

                for (int i = 0; i < length; ++i)
                {
                    destination[destinationIndex + i] = source[sourceIndex + i];
                }
            }
        }

        /// <summary>
        /// Copies items from the specified enumerator to an array until the given length has been
        /// read or the sequence is exhausted.
        /// </summary>
        /// <remarks>This method does not manage the lifetime of <paramref name="source"/>.</remarks>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The enumerator to copy from.</param>
        /// <param name="destination">The array to copy to.</param>
        /// <param name="destinationIndex">
        /// The index in <paramref name="destination"/> where copying begins.
        /// </param>
        /// <param name="length">The maximum number of elements to copy.</param>
        /// <returns>
        /// The number of elements actually copied; this may be less than <paramref name="length"/>
        /// if the source sequence is exhausted before <paramref name="length"/> elements could be copied.
        /// </returns>
        public static int CopyTo<T>(this IEnumerator<T> source, T[] destination, int destinationIndex, int length)
        {
            Checker.NotNull(source, "source");
            Checker.NotNull(destination, "destination");
            Checker.SpanInRange(destination.Length, destinationIndex, length, "destinationIndex", "length");

            int i = 0;

            while (i < length && source.MoveNext())
            {
                destination[destinationIndex + i] = source.Current;
                ++i;
            }

            return i;
        }

        #endregion CopyTo

        #region BufferViaArray

        private const int BufferViaArrayDefaultSize = 2048;

        private static ArgumentException BufferArrayIsZeroLength()
        {
            return new ArgumentException("Buffer array for non-empty copy cannot be zero-length.");
        }

        // Action that copies length elements from sourceIndex of a source to destinationIndex of a destination.
        private delegate void SubcopyAction(int sourceIndex, int destinationIndex, int length);

        // Gets an action that copies a specified sublist from the given source to the given array.
        // Note that the non-array, non-ImmutableArray variant does no bounds checking.
        private static SubcopyAction GetSubcopyAction<T>(IList<T> source, T[] destination)
        {
            if (source is T[])
            {
                var sourceArray = (T[])source;
                return (sourceIndex, destinationIndex, length) => Array.Copy(sourceArray, sourceIndex, destination, destinationIndex, length);
            }
            else if (source is ImmutableArray<T>)
            {
                var sourceImmutableArray = ((ImmutableArray<T>)source);
                return (sourceIndex, destinationIndex, length) => sourceImmutableArray.CopyTo(sourceIndex, destination, destinationIndex, length);
            }
            else
            {
                var sourceNonArray = source;
                return (sourceIndex, destinationIndex, length) =>
                {
                    for (int i = 0; i < length; ++i)
                    {
                        destination[destinationIndex + i] = sourceNonArray[sourceIndex + i];
                    }
                };
            }
        }

        /// <summary>
        /// Repeatedly copies elements from the given source list into the given array, producing the
        /// number of elements copied as a sequence.
        /// </summary>
        /// <example>
        /// This sample demonstrates how to copy bytes from a list to a stream using an intermediate array.
        /// <code>
        /// <![CDATA[
        /// private static void BufferViaArraySample(IList<byte> source, Stream destination)
        /// {
        /// var buffer = new byte[2048];
        /// foreach(var length in source.BufferViaArray(0, source.Count, buffer))
        /// {
        /// destination.Write(buffer, 0, length);
        /// }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <typeparam name="T">The type of elements to be buffered.</typeparam>
        /// <param name="source">A list containing the elements to be copied.</param>
        /// <param name="index">The first index of <paramref name="source"/> to be copied.</param>
        /// <param name="count">The total number of elements to copy from <paramref name="source"/>.</param>
        /// <param name="buffer">An array to use as the copy buffer.</param>
        /// <returns>
        /// An enumerable that interleaves copying with yielding the number of elements copied since
        /// the previous yield. (The index in <paramref name="buffer"/> of each copy is always 0.)
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="buffer"/> is <c>null</c> when <paramref name="count"/> is greater than 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="buffer"/> is 0-length when <paramref name="count"/> is greater than 0.
        /// </exception>
        public static IEnumerable<int> BufferViaArray<T>(this IList<T> source, int index, int count, T[] buffer)
        {
            Checker.NotNull(source, "source");
            Checker.SpanInRange(source.Count, index, count, "index", "count");

            if (count > 0)
            {
                Checker.NotNull(buffer, "buffer");
                if (buffer.Length == 0)
                {
                    throw BufferArrayIsZeroLength();
                }
            }

            var subcopy = GetSubcopyAction(source, buffer);

            while (count > 0)
            {
                int countThisPass = Math.Min(count, buffer.Length);
                subcopy(index, 0, countThisPass);
                index += countThisPass;
                count -= countThisPass;
                yield return countThisPass;
            }
        }

        /// <summary>
        /// Repeatedly calls the given action with elements read from the indicated source list into
        /// a buffer.
        /// </summary>
        /// <example>
        /// This sample demonstrates how to copy bytes from a list to a stream using the default
        /// buffer size and a callback.
        /// <code>
        /// <![CDATA[
        /// private static void BufferViaArrayCallbackSample(IList<byte> source, System.IO.Stream destination)
        /// {
        /// source.BufferViaArray(0, source.Count, (buffer, offset, count) => destination.Write(buffer, offset, count));
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <typeparam name="T">The type of elements to be buffered.</typeparam>
        /// <param name="source">A list containing the elements to be copied.</param>
        /// <param name="index">The first index of <paramref name="source"/> to be copied.</param>
        /// <param name="count">The total number of elements to copy from <paramref name="source"/>.</param>
        /// <param name="fillAction">
        /// An <see cref="Action"/> which will be called repeatedly with the copy buffer, the start
        /// index within the copy buffer, and the number of valid elements in the copy buffer.
        /// </param>
        /// <param name="buffer">
        /// An array to use as the copy buffer; if <c>null</c> or omitted, a new array is created.
        /// </param>
        public static void BufferViaArray<T>(this IList<T> source, int index, int count, Action<T[], int, int> fillAction, T[] buffer = null)
        {
            Checker.NotNull(fillAction, "fillAction");

            if (buffer == null && count > 0)
            {
                buffer = new T[Math.Min(count, BufferViaArrayDefaultSize)];
            }

            foreach (var countThisPass in source.BufferViaArray(index, count, buffer))
            {
                fillAction(buffer, 0, countThisPass);
            }
        }

        /// <summary>
        /// Repeatedly calls the given action with elements read from the indicated source sequence
        /// into a buffer.
        /// </summary>
        /// <example>
        /// This sample demonstrates how to copy bytes from a list to a stream using an intermediate array.
        /// <code>
        /// <![CDATA[
        /// private static void BufferViaArraySample(IEnumerable<byte> source, System.IO.Stream destination)
        /// {
        /// var buffer = new byte[2048];
        /// foreach (var length in source.BufferViaArray(buffer))
        /// {
        /// destination.Write(buffer, 0, length);
        /// }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <typeparam name="T">The type of elements to be buffered.</typeparam>
        /// <param name="source">A sequence containing the elements to be copied.</param>
        /// <param name="buffer">
        /// An array to use as the copy buffer; if <c>null</c> or omitted, a new array is created.
        /// </param>
        public static IEnumerable<int> BufferViaArray<T>(this IEnumerable<T> source, T[] buffer)
        {
            Checker.NotNull(source, "source");

            // Defer to list implementation if possible
            if (source is IList<T>)
            {
                var list = (IList<T>)source;
                return BufferViaArray(list, 0, list.Count, buffer);
            }
            else
            {
                Checker.NotNull(buffer, "buffer");
                return BufferEnumerableViaArray(source, buffer);
            }
        }

        private static IEnumerable<int> BufferEnumerableViaArray<T>(IEnumerable<T> source, T[] buffer)
        {
            using (var enumerator = source.GetEnumerator())
            {
                foreach (var item in BufferEnumeratorViaArray(enumerator, buffer))
                {
                    yield return item;
                }
            }
        }

        // Buffer is assumed not null.
        private static IEnumerable<int> BufferEnumeratorViaArray<T>(IEnumerator<T> source, T[] buffer)
        {
            if (buffer.Length == 0)
            {
                // Allowed only if source turns out to be empty.
                if (source.MoveNext())
                {
                    throw BufferArrayIsZeroLength();
                }
            }
            else
            {
                int bufferLength = buffer.Length;
                int i;

                do
                {
                    i = 0;

                    // Copy elements to buffer
                    while (i < bufferLength && source.MoveNext())
                    {
                        buffer[i] = source.Current;
                        ++i;
                    }

                    // At least one element was copied
                    if (i > 0)
                    {
                        yield return i;
                    }
                } while (i == bufferLength); // Buffer was filled before sequence ended (so there may be more elements)
            }
        }

        /// <summary>
        /// Repeatedly calls the given action with elements read from the indicated source sequence
        /// into a buffer.
        /// </summary>
        /// <example>
        /// This sample demonstrates how to copy bytes from a list to a stream using the default
        /// buffer size and a callback.
        /// <code>
        /// <![CDATA[
        /// private static void BufferViaArrayCallbackSample(IEnumerable<byte> source, System.IO.Stream destination)
        /// {
        /// source.BufferViaArray((buffer, offset, count) => destination.Write(buffer, offset, count));
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <typeparam name="T">The type of elements to be buffered.</typeparam>
        /// <param name="source">A sequence containing the elements to be copied.</param>
        /// <param name="fillAction">
        /// An <see cref="Action"/> which will be called repeatedly with the copy buffer, the start
        /// index within the copy buffer, and the number of valid elements in the copy buffer.
        /// </param>
        /// <param name="buffer">
        /// An array to use as the copy buffer; if <c>null</c> or omitted, a new array is created.
        /// </param>
        public static void BufferViaArray<T>(this IEnumerable<T> source, Action<T[], int, int> fillAction, T[] buffer = null)
        {
            Checker.NotNull(fillAction, "fillAction");

            if (buffer == null)
            {
                buffer = new T[BufferViaArrayDefaultSize];
            }

            foreach (var countThisPass in source.BufferViaArray(buffer))
            {
                fillAction(buffer, 0, countThisPass);
            }
        }

        #endregion BufferViaArray

        #region Skip/Take

        private static ulong NonnegativeLong(long count)
        {
            return count < 0 ? 0UL : (ulong)count;
        }

        // Skips up to count elements. Returns true if count elements were skipped, or false if the
        // sequence contained fewer than count elements.
        private static bool SkipEnumerator<T>(IEnumerator<T> enumerator, int count)
        {
            while (count > 0 && enumerator.MoveNext())
            {
                --count;
            }

            return (count <= 0);
        }

        private static bool SkipEnumerator<T>(IEnumerator<T> enumerator, ulong count)
        {
            while (count > 0 && enumerator.MoveNext())
            {
                --count;
            }

            return (count == 0);
        }

        /// <summary>
        /// Omits a specified number of elements from the beginning of a sequence before producing
        /// the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to skip.</param>
        /// <returns>
        /// An enumerable that contains the elements of <paramref name="source"/> after the index
        /// <paramref name="count"/>, or no elements if the sequence contains that number of elements
        /// or fewer.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <seealso cref="System.Linq.Enumerable.Skip{TSource}(IEnumerable{TSource}, int)"/>
        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, long count)
        {
            return Skip(source, NonnegativeLong(count));
        }

        /// <summary>
        /// Omits a specified number of elements from the beginning of a sequence before producing
        /// the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to skip.</param>
        /// <returns>
        /// An enumerable that contains the elements of <paramref name="source"/> after the index
        /// <paramref name="count"/>, or no elements if the sequence contains that number of elements
        /// or fewer.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <seealso cref="System.Linq.Enumerable.Skip{TSource}(IEnumerable{TSource}, int)"/>
        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, ulong count)
        {
            Checker.NotNull(source, "source");

            using (var enumerator = source.GetEnumerator())
            {
                bool skippedAll = SkipEnumerator(enumerator, count);
                if (skippedAll)
                {
                    while (enumerator.MoveNext())
                    {
                        yield return enumerator.Current;
                    }
                }
            }
        }

        /// <summary>
        /// Produces a specified number of elements from the beginning of a sequence, omitting the
        /// remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to take.</param>
        /// <returns>
        /// An enumerable that contains the first <paramref name="count"/> elements of <paramref
        /// name="source"/>, or all elements of <paramref name="source"/> if the sequence contains
        /// fewer than <paramref name="count"/> elements, or no elements if <paramref name="count"/>
        /// is negative.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, long count)
        {
            return Take(source, NonnegativeLong(count));
        }

        /// <summary>
        /// Produces a specified number of elements from the beginning of a sequence, omitting the
        /// remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to take.</param>
        /// <returns>
        /// An enumerable that contains the first <paramref name="count"/> elements of <paramref
        /// name="source"/>, or all elements of <paramref name="source"/> if the sequence contains
        /// fewer than <paramref name="count"/> elements.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, ulong count)
        {
            Checker.NotNull(source, "source");

            if (count > 0)
            {
                foreach (var item in source)
                {
                    yield return item;
                    --count;
                    if (count == 0)
                    {
                        break;
                    }
                }
            }
        }

        private static InvalidOperationException NotEnoughElementsToSkip()
        {
            return new InvalidOperationException("The sequence ended before the specified number of elements could be skipped.");
        }

        private static InvalidOperationException NotEnoughElementsToTake()
        {
            return new InvalidOperationException("The sequence ended before the specified number of elements could be yielded.");
        }

        /// <summary>
        /// Omits a specified number of elements in a sequence before producing the remaining
        /// elements, throwing an exception if there are not enough elements to skip.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to skip.</param>
        /// <returns>
        /// An enumerable that contains the elements of <paramref name="source"/> after the index
        /// <paramref name="count"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        /// <exception cref="InvalidOperationException">
        /// The skip cannot be completed because <paramref name="source"/> contains fewer than
        /// <paramref name="count"/> elements.
        /// </exception>
        public static IEnumerable<T> HardSkip<T>(this IEnumerable<T> source, int count)
        {
            Checker.NotNegative(count, "count");
            Checker.NotNull(source, "source");

            using (var enumerator = source.GetEnumerator())
            {
                bool skippedAll = SkipEnumerator(enumerator, count);
                if (skippedAll)
                {
                    while (enumerator.MoveNext())
                    {
                        yield return enumerator.Current;
                    }
                }
                else
                {
                    throw NotEnoughElementsToSkip();
                }
            }
        }

        /// <summary>
        /// Omits a specified number of elements in a sequence before producing the remaining
        /// elements, throwing an exception if there are not enough elements to skip.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to skip.</param>
        /// <returns>
        /// An enumerable that contains the elements of <paramref name="source"/> after the index
        /// <paramref name="count"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        /// <exception cref="InvalidOperationException">
        /// The skip cannot be completed because <paramref name="source"/> contains fewer than
        /// <paramref name="count"/> elements.
        /// </exception>
        public static IEnumerable<T> HardSkip<T>(this IEnumerable<T> source, long count)
        {
            Checker.NotNegative(count, "count");
            return HardSkip(source, (ulong)count);
        }

        /// <summary>
        /// Omits a specified number of elements in a sequence before producing the remaining
        /// elements, throwing an exception if there are not enough elements to skip.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to skip.</param>
        /// <returns>
        /// An enumerable that contains the elements of <paramref name="source"/> after the index
        /// <paramref name="count"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// The skip cannot be completed because <paramref name="source"/> contains fewer than
        /// <paramref name="count"/> elements.
        /// </exception>
        public static IEnumerable<T> HardSkip<T>(this IEnumerable<T> source, ulong count)
        {
            Checker.NotNull(source, "source");

            using (var enumerator = source.GetEnumerator())
            {
                bool skippedAll = SkipEnumerator(enumerator, count);
                if (skippedAll)
                {
                    while (enumerator.MoveNext())
                    {
                        yield return enumerator.Current;
                    }
                }
                else
                {
                    throw NotEnoughElementsToSkip();
                }
            }
        }

        /// <summary>
        /// Produces a specified number of elements from the beginning of a sequence, omitting the
        /// remaining elements, throwing an exception if there are not enough elements to produce.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to take.</param>
        /// <returns>
        /// An enumerable that contains the first <paramref name="count"/> elements of <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        /// <exception cref="InvalidOperationException">
        /// The take cannot be completed because <paramref name="source"/> contains fewer than
        /// <paramref name="count"/> elements. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<T> HardTake<T>(this IEnumerable<T> source, int count)
        {
            Checker.NotNegative(count, "count");
            Checker.NotNull(source, "source");

            if (count > 0)
            {
                foreach (var item in source)
                {
                    yield return item;
                    --count;
                    if (count == 0)
                    {
                        break;
                    }
                }
            }

            if (count > 0)
            {
                throw NotEnoughElementsToTake();
            }
        }

        /// <summary>
        /// Produces a specified number of elements from the beginning of a sequence, omitting the
        /// remaining elements, throwing an exception if there are not enough elements to produce.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to take.</param>
        /// <returns>
        /// An enumerable that contains the first <paramref name="count"/> elements of <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        /// <exception cref="InvalidOperationException">
        /// The take cannot be completed because <paramref name="source"/> contains fewer than
        /// <paramref name="count"/> elements. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<T> HardTake<T>(this IEnumerable<T> source, long count)
        {
            Checker.NotNegative(count, "count");
            return HardTake(source, (ulong)count);
        }

        /// <summary>
        /// Produces a specified number of elements from the beginning of a sequence, omitting the
        /// remaining elements, throwing an exception if there are not enough elements to produce.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">An enumerable to return elements from.</param>
        /// <param name="count">The number of elements to take.</param>
        /// <returns>
        /// An enumerable that contains the first <paramref name="count"/> elements of <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// The take cannot be completed because <paramref name="source"/> contains fewer than
        /// <paramref name="count"/> elements. (Thrown from enumeration.)
        /// </exception>
        public static IEnumerable<T> HardTake<T>(this IEnumerable<T> source, ulong count)
        {
            Checker.NotNull(source, "source");

            if (count > 0)
            {
                foreach (var item in source)
                {
                    yield return item;
                    --count;
                    if (count == 0)
                    {
                        break;
                    }
                }
            }

            if (count > 0)
            {
                throw NotEnoughElementsToTake();
            }
        }

        #endregion Skip/Take
    }
}