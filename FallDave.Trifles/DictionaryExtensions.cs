//-----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="falldave">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallDave.Trifles
{
    /// <summary>
    /// Utility extensions applicable to <see cref="IDictionary{TKey, TValue}"/> instances.
    /// </summary>
    public static class DictionaryExtensions
    {
        private static Opt<TValue> ValueFixOpt0<TKey, TValue>(IDictionary<TKey, TValue> source, TKey key)
        {
            TValue value;
            bool hasValue = source.TryGetValue(key, out value);
            return Opt.Create(hasValue, value);
        }

        /// <summary>
        /// Returns an immediate, non-deferred option that contains the value of the dictionary at
        /// the given key, if the dictionary contains the key, or an empty option otherwise. Since
        /// this option is not deferred, its value reflects the state of the dictionary at the time
        /// this method is called and will not change.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Opt<TValue> ValueFixOpt<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            Checker.NotNull(source, "source");
            return ValueFixOpt0(source, key);
        }

        /// <summary>
        /// Returns a deferred option that will contain the value of the dictionary at the given key,
        /// if the dictionary contains the key, or an empty option otherwise. Since this option is
        /// deferred, repeated evaluations (of <see cref="IOpt{T}.Fix()"/>, <see
        /// cref="OptExtensions.Single{T}(IOpt{T})"/>, and so forth) reflect the current state of the
        /// dictionary rather than its state as of the call to this method.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IOpt<TValue> ValueOpt<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            Checker.NotNull(source, "source");
            return Opt.Defer(() => ValueFixOpt0(source, key));
        }
    }
}
