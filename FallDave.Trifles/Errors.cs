//-----------------------------------------------------------------------
// <copyright file="Errors.cs" company="falldave">
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
    /// Internal utility for generating exceptions.
    /// </summary>
    internal class Errors
    {
        public static InvalidOperationException NoElements(bool usingPredicate = false)
        {
            return new InvalidOperationException(usingPredicate ? "This sequence contains no matching elements." : "This sequence contains no elements.");
        }

        public static InvalidOperationException MoreThanOneElement(bool usingPredicate = false)
        {
            return new InvalidOperationException(usingPredicate ? "This sequence contains more than one matching element." : "This sequence contains more than one element.");
        }

        public static InvalidOperationException EnumNotStarted()
        {
            return new InvalidOperationException("Enumeration has not yet started.");
        }

        public static InvalidOperationException EnumEnded()
        {
            return new InvalidOperationException("Enumeration has already ended.");
        }
    }
}
