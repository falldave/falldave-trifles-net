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
    public static class OptExtensions
    {
        private static Opt<T> FixSource<T>(this IOpt<T> source)
        {
            return Errors.Require(source, "source").Fix();
        }

        public static bool TryGetValue<T>(this IOpt<T> source, out T value)
        {
            return source.FixSource().TryGetValue(out value);
        }

        public static bool Any<T>(this IOpt<T> source)
        {
            return source.FixSource().Any();
        }

        public static int Count<T>(this IOpt<T> source)
        {
            return source.FixSource().Count();
        }

        public static T Single<T>(this IOpt<T> source)
        {
            return source.FixSource().Single();
        }

        public static T First<T>(this IOpt<T> source)
        {
            return source.FixSource().First();
        }

        public static T Last<T>(this IOpt<T> source)
        {
            return source.FixSource().Last();
        }

        public static T ElementAt<T>(this IOpt<T> source, int index)
        {
            return source.FixSource().ElementAt(index);
        }


    }
}

