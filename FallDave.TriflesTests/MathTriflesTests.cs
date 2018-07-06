using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace FallDave.Trifles.Tests
{
    [TestFixture()]
    public partial class MathTriflesTests
    {
        // Normalizes the result of a Compare() operation to one of {-1, 0, 1}.
        private static int Sgn(int n)
        {
            return (n < 0) ? -1 : (n > 0) ? 1 : 0;
        }

        // The ideal behavior for the clamp functions.
        private static BigInteger Clamp(BigInteger value, BigInteger minInclusive, BigInteger maxInclusive)
        {
            if (minInclusive > maxInclusive)
            {
                throw new ArgumentException("minInclusive exceeds maxInclusive");
            }
            return value < minInclusive ? minInclusive : value > maxInclusive ? maxInclusive : value;
        }
    }
}