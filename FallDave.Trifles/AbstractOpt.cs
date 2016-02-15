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
    public abstract class AbstractOpt<T> : IOpt<T>
    {
        #region IOpt<T> implementation

        public abstract Opt<T> Fix();

        #endregion

        #region IEnumerable<T> implementation

        public virtual IEnumerator<T> GetEnumerator()
        {
            return Fix().GetEnumerator();
        }

        #endregion

        #region IEnumerable implementation

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            System.Collections.IEnumerable ie = Fix();
            return ie.GetEnumerator();
        }

        #endregion
    }
}

