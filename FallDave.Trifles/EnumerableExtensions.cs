/*
 * Written in 2015-2016 by David McFall
 *
 * To the extent possible under law, the author(s) have dedicated all copyright
 * and related and neighboring rights to this software to the public domain
 * worldwide. This software is distributed without any warranty.
 *
 * You should have received a copy of the CC0 Public Domain Dedication along
 * with this software. If not, see
 * <http://creativecommons.org/publicdomain/zero/1.0/>.
 */

using System;
using System.Collections.Generic;

namespace FallDave.Trifles
{
    public static class EnumerableExtensions
    {
        // A sort of mutable Opt builder.
        // Initially hasValue = false, valueBuffer = default(T).
        // When Value is set to a value, hasValue becomes true and cannot be reverted to false.
        private struct OptInit<T>
        {
            private bool hasValue;
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
                return new Opt<T>(hasValue, Value);
            }
        }

        // If the given predicate is null, it is changed to an always-true predicate.
        // Returns whether the initial predicate was non-null. In exception messages
        // this is the difference between say "no elements" and "no matching elements".
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

        #endregion

        #region Opt<T>-returning *AsFixedOpt methods

        public static Opt<T> SingleAsFixedOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Errors.Require(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return SingleAsInit(source, predicate, usingPredicate).AsOpt();
        }

        public static Opt<T> FirstAsFixedOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Errors.Require(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return FirstAsInit(source, predicate, usingPredicate).AsOpt();
        }

        public static Opt<T> LastAsFixedOpt<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            Errors.Require(source, "source");
            var usingPredicate = SetUpPredicate(ref predicate);
            return LastAsInit(source, predicate, usingPredicate).AsOpt();
        }

        public static Opt<T> ElementAtAsFixedOpt<T>(this IEnumerable<T> source, int index)
        {
            Errors.Require(source, "source");
            return ElementAtAsInit(source, index).AsOpt();
        }

        #endregion

        #region TryGet* methods

        public static bool TryGetSingle<T>(this IEnumerable<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.SingleAsFixedOpt(predicate).TryGetValue(out value);
        }

        public static bool TryGetFirst<T>(this IEnumerable<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.FirstAsFixedOpt(predicate).TryGetValue(out value);
        }

        public static bool TryGetLast<T>(this IEnumerable<T> source, out T value, Func<T, bool> predicate = null)
        {
            return source.LastAsFixedOpt(predicate).TryGetValue(out value);
        }

        public static bool TryGetElementAt<T>(this IEnumerable<T> source, int index, out T value)
        {
            return source.ElementAtAsFixedOpt(index).TryGetValue(out value);
        }

        #endregion

        #region Take*AsOpt

        /// <summary>
        /// Returns a deferred option that will contain the source's only element (if the source contains exactly one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contain the entire source sequence if and only if the sequence has no more than one element.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        /// <exception cref="InvalidOperationException">At the time of evaluation, the source sequence contains more than one element.</exception>
        public static IOpt<T> TakeSingleAsOpt<T>(IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.SingleAsFixedOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's first element (if the source contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the first element of the source sequence, if the source is not empty, or no element if the source is empty.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> TakeFirstAsOpt<T>(IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.FirstAsFixedOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the source's last element (if the source contains at least one element) or no elements (if the source is empty).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the last element of the source sequence, if the source is not empty, or no element if the source is empty.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="predicate">A predicate function by which to filter the source sequence. If <c>null</c>, no filter is used.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> TakeLastAsOpt<T>(IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            return new DeferredOpt<T>(() => source.LastAsFixedOpt(predicate));
        }

        /// <summary>
        /// Returns a deferred option that will contain the element of the source at the given index (if the index is not out of range) or no elements (if the index is out of range).
        /// </summary>
        /// <returns>An option which, at the point of evaluation, contains the element of the source sequence at the given index, if the index is in range (at least zero and no greater than the element count of the source), or no element if the index is out of range.</returns>
        /// <param name="source">The sequence to use as the source.</param>
        /// <param name="index">The index of the value to retrieve.</param>
        /// <typeparam name="T">The type of value contained by the sequence.</typeparam>
        public static IOpt<T> TakeElementAtAsOpt<T>(IEnumerable<T> source, int index)
        {
            return new DeferredOpt<T>(() => source.ElementAtAsFixedOpt(index));
        }

        #endregion

    }
}




