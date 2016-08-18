//-----------------------------------------------------------------------
// <copyright file="CountedOpt.cs" company="falldave">
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallDave.Trifles
{
    /// <summary>
    /// An option (<see cref="IOpt{T}"/>) that contains a single value on the
    /// success of a single-returning method, or contains information describing
    /// whether the absence of a singe value is due to the source sequence
    /// having contained too few or too many elements.
    /// </summary>
    /// <para>
    /// As with any <see cref="IOpt{T}"/>, the <see cref="Fix"/> method can be used
    /// to retrieve an ordinary <see cref="Opt{T}"/>.
    /// </para>
    /// <typeparam name="T"></typeparam>
    public struct CountedOpt<T> : IOpt<T>
    {
        private readonly int sourceCount;
        private readonly Opt<T> valueOpt;

        internal CountedOpt(int sourceCount, T value = default(T))
        {
            if (sourceCount < 0)
            {
                throw new ArgumentOutOfRangeException("sourceCount");
            }
            this.sourceCount = sourceCount;
            this.valueOpt = Opt.Create(sourceCount == 1, value);
        }

        /// <summary>
        /// The number of elements in the sequence from which this value was
        /// extracted. A value of 0 indicates that the source sequence was
        /// empty. A value of 1 indicates that the source sequence contained
        /// exactly one element. A value greater than 1 indicates that the
        /// source sequence contained more than one element. Except when 0 or 1,
        /// the value in this property is generally not guaranteed to be the
        /// precise number of elements in the source.
        /// </summary>
        public int SourceCount { get { return sourceCount; } }

        /// <summary>
        /// Value indicating whether the source sequence contained zero
        /// elements.
        /// </summary>
        public bool SourceNone { get { return sourceCount == 0; } }

        /// <summary>
        /// Value indicating whether the source sequence contained more than one
        /// element.
        /// </summary>
        public bool SourceMultiple { get { return sourceCount > 1; } }

        /// <inheritdoc/>
        public Opt<T> Fix()
        {
            return valueOpt;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return Fix().GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable x = Fix();
            return x.GetEnumerator();
        }
    }
}
