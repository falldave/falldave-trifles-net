using FallDave.TriflesTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FallDave.Trifles.Tests
{
    [TestFixture()]
    public class EnumerableExtensionsTests
    {
        private static IEnumerable<ulong> GenerateFibo()
        {
            ulong n0 = 0;
            ulong n1 = 1;

            while (true)
            {
                {
                    ulong n2 = n0 + n1;
                    n0 = n1;
                    n1 = n2;
                }

                yield return n0;
            }
        }

        private static IEnumerable<ulong> Fibo { get { return GenerateFibo(); } }

        private static IEnumerable<ulong> FiniteFibo { get { return Fibo.Take(20); } }

        private static IEnumerable<byte> FiboLow { get { return Fibo.Select(n => (byte)n); } }

        private static T[] GetArray<T>(IEnumerable<T> source)
        {
            return source.ToArray();
        }

        private static List<T> GetList<T>(IEnumerable<T> source)
        {
            return new List<T>(source);
        }

        private static IList<T> GetIList<T>(IEnumerable<T> source)
        {
            return WrapIList(GetList(source));
        }

        private static IList<T> WrapIList<T>(IList<T> q)
        {
            return new WrappedList<T>(q);
        }

        private static IEnumerable<T> WrapIEnumerable<T>(IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                yield return item;
            }
        }

        // The elements to expect after doing source.CopyTo(buffer, offset, length) for buffer = new T[arraySize].
        private static IEnumerable<T> SubarrayCopyExpect<T>(IEnumerable<T> source, int offset, int length, int arraySize, T arrayDefaultValue = default(T))
        {
            if (offset < 0) { throw new ArgumentException("offset"); }
            if (length < 0) { throw new ArgumentException("length"); }
            if (arraySize - length < offset) { throw new ArgumentException("arraySize"); }

            var buffer = Enumerable.Repeat(arrayDefaultValue, arraySize).ToArray();
            var copySource = new List<T>(source.Take(length));
            copySource.CopyTo(buffer, offset);
            return buffer;
        }

        private void HardSkipOrTakeFailsDuringEnumerationTestImpl<T>(Func<IEnumerable<T>> callHardSkipOrTake)
        {
            IEnumerable<T> seq = null;

            try
            {
                seq = callHardSkipOrTake();
            }
            catch (Exception)
            {
                Assert.Fail("Hard skip/take failed during initialization instead of traversal");
            }

            try
            {
                Console.WriteLine("Starting iteration");
                foreach (var item in seq)
                {
                    Console.WriteLine("Got item " + item);
                }
            }
            catch (InvalidOperationException)
            {
                Assert.Pass();
            }
            catch (Exception)
            {
                Assert.Fail("Hard skip/take failed during traversal, but with wrong exception");
            }

            Assert.Fail("Hard skip/take did not fail as expected");
        }

        [Test()]
        public void HardTakeFailsDuringEnumerationTest()
        {
            HardSkipOrTakeFailsDuringEnumerationTestImpl(() => Fibo.Take(5).HardTake(10));
            HardSkipOrTakeFailsDuringEnumerationTestImpl(() => Fibo.Take(5).HardTake(10L));
            HardSkipOrTakeFailsDuringEnumerationTestImpl(() => Fibo.Take(5).HardTake(10UL));
        }

        [Test()]
        public void HardSkipFailsDuringEnumerationTest()
        {
            HardSkipOrTakeFailsDuringEnumerationTestImpl(() => Fibo.Take(5).HardSkip(10));
            HardSkipOrTakeFailsDuringEnumerationTestImpl(() => Fibo.Take(5).HardSkip(10L));
            HardSkipOrTakeFailsDuringEnumerationTestImpl(() => Fibo.Take(5).HardSkip(10UL));
        }

        private IEnumerable<T> NativeTake<T>(IEnumerable<T> source, int count)
        {
            return Enumerable.Take(source, count);
        }

        private IEnumerable<T> NativeSkip<T>(IEnumerable<T> source, int count)
        {
            return Enumerable.Skip(source, count);
        }

        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void LongTakeActsLikeNativeTakeTest(int count)
        {
            var expected = NativeTake(Fibo, count);
            Assert.That(Fibo.Take((long)count), Is.EquivalentTo(expected));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void ULongTakeActsLikeNativeTakeTest(int count)
        {
            var expected = NativeTake(Fibo, count);
            Assert.That(Fibo.Take((ulong)count), Is.EquivalentTo(expected));
        }

        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void LongSkipActsLikeNativeSkipTest(int count)
        {
            var expected = NativeSkip(FiniteFibo, count);
            Assert.That(FiniteFibo.Skip((long)count), Is.EquivalentTo(expected));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void ULongSkipActsLikeNativeSkipTest(int count)
        {
            var expected = NativeSkip(FiniteFibo, count);
            Assert.That(FiniteFibo.Skip((ulong)count), Is.EquivalentTo(expected));
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 2)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(10, 0)]
        [TestCase(10, 1)]
        [TestCase(10, 2)]
        public void HardSkipActsLikeNativeSkipOnSufficientSequenceTest(int count, int leftoverCount)
        {
            var seq = NativeTake(Fibo, count + leftoverCount);
            var expected = NativeSkip(seq, count);
            Assert.That(seq.HardSkip(count), Is.EquivalentTo(expected));
            Assert.That(seq.HardSkip((long)count), Is.EquivalentTo(expected));
            Assert.That(seq.HardSkip((ulong)count), Is.EquivalentTo(expected));
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 2)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(10, 0)]
        [TestCase(10, 1)]
        [TestCase(10, 2)]
        public void HardTakeActsLikeNativeTakeOnSufficientSequenceTest(int count, int leftoverCount)
        {
            var seq = NativeTake(Fibo, count + leftoverCount);
            var expected = NativeTake(seq, count);
            Assert.That(seq.HardTake(count), Is.EquivalentTo(expected));
            Assert.That(seq.HardTake((long)count), Is.EquivalentTo(expected));
            Assert.That(seq.HardTake((ulong)count), Is.EquivalentTo(expected));
        }

        [Test()]
        public void BasicCopyToTest()
        {
            RunBasicCopyToTestsOnAllSourceTypes(NativeTake(Fibo, 100), 100);
            RunBasicCopyToTestsOnAllSourceTypes(NativeTake(Fibo, 3), 3);
        }

        private static void RunBasicCopyToTestsOnAllSourceTypes<T>(IEnumerable<T> source, int sourceLength)
        {
            RunBasicCopyToTestsOn(source, sourceLength);
            RunBasicCopyToTestsOn(GetArray(source), sourceLength);
            RunBasicCopyToTestsOn(GetList(source), sourceLength);
            RunBasicCopyToTestsOn(GetIList(source), sourceLength);
        }

        private static void RunBasicCopyToTestsOn<T>(IEnumerable<T> source, int sourceLength)
        {
            RunBasicCopyToTestOn(source, 0, 0, 0, sourceLength);
            RunBasicCopyToTestOn(source, 0, 0, 5, sourceLength);
            RunBasicCopyToTestOn(source, 0, 5, 5, sourceLength);
            RunBasicCopyToTestOn(source, 0, 5, 10, sourceLength);
            RunBasicCopyToTestOn(source, 5, 0, 5, sourceLength);
            RunBasicCopyToTestOn(source, 5, 0, 10, sourceLength);
            RunBasicCopyToTestOn(source, 5, 5, 10, sourceLength);
            RunBasicCopyToTestOn(source, 5, 5, 15, sourceLength);
        }

        private static void RunBasicCopyToTestOn<T>(IEnumerable<T> source, int offset, int length, int arraySize, int sourceLength)
        {
            var actualLength = Math.Min(length, sourceLength);

            var buffer = new T[arraySize];

            var copyCount = source.CopyTo(buffer, offset, length);
            Assert.That(copyCount, Is.EqualTo(actualLength));

            var expected = SubarrayCopyExpect(source, offset, actualLength, buffer.Length);
            Assert.That(buffer, Is.EquivalentTo(expected));
        }

        [Test()]
        public void BasicBufferViaArrayTest()
        {
            var listConversions = new Func<IList<byte>, IList<byte>>[]
            {
                fullList => GetList(fullList),
                fullList => GetIList(fullList),
                fullList => GetArray(fullList)
            };

            var enumerableConversions = new Func<IList<byte>, IEnumerable<byte>>[]
            {
                fullList => GetList(fullList),
                fullList => GetIList(fullList),
                fullList => GetArray(fullList),
                fullList => WrapIEnumerable(fullList)
            };

            var bufferSizes = new[] { 1, 200, 1024, 2048 };

            var fullSequenceSize = 1024;

            foreach (var bufferSize in bufferSizes)
            {
                foreach (var listConversion in listConversions)
                {
                    RunBufferViaArrayTestUsingMemoryStream(fullSequenceSize, 0, fullSequenceSize, bufferSize, listConversion);
                    RunBufferViaArrayTestUsingMemoryStream(fullSequenceSize, 100, fullSequenceSize - 100, bufferSize, listConversion);
                    RunBufferViaArrayTestUsingMemoryStream(fullSequenceSize, 0, fullSequenceSize - 100, bufferSize, listConversion);
                    RunBufferViaArrayTestUsingMemoryStream(fullSequenceSize, 100, fullSequenceSize - 200, bufferSize, listConversion);
                }

                foreach (var enumerableConversion in enumerableConversions)
                {
                    RunBufferViaArrayTestUsingMemoryStream(fullSequenceSize, bufferSize, enumerableConversion);
                }
            }
        }

        private static void RunBufferViaArrayTestUsingMemoryStream(int fullSequenceCount, int bufferSize, Func<IList<byte>, IEnumerable<byte>> convertToSource)
        {
            var fullSequenceList = GetList(FiboLow.Take(fullSequenceCount));

            var source = convertToSource(fullSequenceList);

            {
                var buffer = new byte[bufferSize];
                var actualResult = new System.IO.MemoryStream();
                foreach (var countThisPass in source.BufferViaArray(buffer))
                {
                    Console.WriteLine("Iteration copying pass of: " + countThisPass);
                    actualResult.Write(buffer, 0, countThisPass);
                }

                Assert.That(actualResult.ToArray(), Is.EquivalentTo(fullSequenceList));
            }

            {
                var buffer = new byte[bufferSize];
                var actualResult = new System.IO.MemoryStream();
                source.BufferViaArray((array, offset, count) =>
               {
                   Console.WriteLine("Callback copying pass of: " + count);
                   actualResult.Write(array, offset, count);
               }, buffer);

                Assert.That(actualResult.ToArray(), Is.EquivalentTo(fullSequenceList));
            }
        }

        private static void RunBufferViaArrayTestUsingMemoryStream(int fullSequenceCount, int subsequenceOffset, int subsequenceCount, int bufferSize, Func<IList<byte>, IList<byte>> convertToSource)
        {
            var fullSequenceList = GetList(FiboLow.Take(fullSequenceCount));
            var expectedResult = fullSequenceList.GetRange(subsequenceOffset, subsequenceCount);

            var source = convertToSource(fullSequenceList);

            {
                var buffer = new byte[bufferSize];
                var actualResult = new System.IO.MemoryStream();
                foreach (var countThisPass in source.BufferViaArray(subsequenceOffset, subsequenceCount, buffer))
                {
                    Console.WriteLine("Iteration copying pass of: " + countThisPass);
                    actualResult.Write(buffer, 0, countThisPass);
                }

                Assert.That(actualResult.ToArray(), Is.EquivalentTo(expectedResult));
            }

            {
                var buffer = new byte[bufferSize];
                var actualResult = new System.IO.MemoryStream();
                source.BufferViaArray(subsequenceOffset, subsequenceCount, (array, offset, count) =>
                {
                    Console.WriteLine("Callback copying pass of: " + count);
                    actualResult.Write(array, offset, count);
                }, buffer);

                Assert.That(actualResult.ToArray(), Is.EquivalentTo(expectedResult));
            }
        }
    }
}