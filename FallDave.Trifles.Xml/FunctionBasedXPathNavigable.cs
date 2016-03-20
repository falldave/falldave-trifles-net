//-----------------------------------------------------------------------
// <copyright file="FunctionBasedXPathNavigable.cs" company="falldave">
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
using System.Xml.XPath;

namespace FallDave.Trifles.Xml
{
    /// <summary>
    /// Wraps a navigator-creating function as an IXPathNavigable.
    /// </summary>
    internal class FunctionBasedXPathNavigable : IXPathNavigable
    {
        private readonly Func<XPathNavigator> createNavigatorFunction;

        public FunctionBasedXPathNavigable(Func<XPathNavigator> createNavigatorFunction)
        {
            this.createNavigatorFunction = Checker.NotNull(createNavigatorFunction, "createNavigatorFunction");
        }

        public XPathNavigator CreateNavigator()
        {
            return createNavigatorFunction();
        }
    }
}
