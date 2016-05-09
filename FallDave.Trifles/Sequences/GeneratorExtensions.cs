using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallDave.Trifles.Sequences
{
    /// <summary>
    /// A generator function that attempts to advance to the next value in a sequence.
    /// </summary>
    /// <typeparam name="T">The type of element in the generated sequence</typeparam>
    /// <param name="value">Variable to receive the next value in the sequence, if any</param>
    /// <returns><c>true</c> if the next value in the sequence was retrieved and the generator advanced, or <c>false</c> if the sequence has ended</returns>
    public delegate bool GeneratorFunction<T>(out T value);

    /// <summary>
    /// Extension methods for <see cref="IGenerator{T}"/>.
    /// </summary>
    public static class GeneratorExtensions
    {
        /// <summary>
        /// Wraps this generator function into an <see cref="IGenerator{T}"/> object.
        /// </summary>
        /// <typeparam name="T">The type of element in the generated sequence</typeparam>
        /// <param name="source">The generator function for a sequence</param>
        /// <returns>A new generator object that defers to this generator function</returns>
        public static IGenerator<T> AsGenerator<T>(this GeneratorFunction<T> source)
        {
            Checker.NotNull(source, "source");
            return new FunctionBasedGenerator<T>(source);
        }

        /// <summary>
        /// Attempts to advance this generator and, if successful, provides the next value in the sequence.
        /// </summary>
        /// <para>The behavior of this method when called after the end of the sequence is undefined.</para>
        /// <typeparam name="T">The type of element in the generated sequence</typeparam>
        /// <param name="source">The generator for a sequence</param>
        /// <returns>
        /// A full <see cref="Opt{T}"/> containing the next value in the sequence, if the generator
        /// has been advanced, or an empty <see cref="Opt{T}"/> if the sequence has ended.
        /// </returns>
        /// <seealso cref="IGenerator{T}.TryGetNext(out T)"/>
        public static Opt<T> GetNext<T>(this IGenerator<T> source)
        {
            Checker.NotNull(source, "source");

            T value;
            bool hasValue = source.TryGetNext(out value);
            return Opt.Create(hasValue, value);
        }

        /// <summary>
        /// Adapts this generator to work as an <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <para>
        /// When <see cref="IEnumerator.MoveNext()"/> is called, the enumerator calls <see
        /// cref="IGenerator{T}.TryGetNext(out T)"/>, caches the current value, and returns the
        /// boolean value. <see cref="IEnumerator{T}.Current"/> then simply returns the cached value.
        /// </para>
        /// <para>The operation <see cref="IEnumerator.Reset()"/> is not supported.</para>
        /// <para>
        /// The enumerable is disposed when <see cref="IEnumerator.MoveNext()"/> returns <c>false</c>
        /// for the first time, or when <see cref="IDisposable.Dispose"/> is called, whichever occurs
        /// first. When the enumerable is disposed, the reference to the generator and the cached
        /// current value are unset. If this generator is also an <see cref="IDisposable"/>, having
        /// set <paramref name="shouldDispose"/> to <c>true</c> will cause this generator to be
        /// disposed before its reference is unset. When using this functionality, ensure that the
        /// enumerator is disposed, such as by way of a <c>foreach</c> or <c>using</c> block or by
        /// calling <see cref="IDisposable.Dispose"/> explicitly. As a rule, an <see
        /// cref="IEnumerator{T}"/> used directly is an object that must be properly disposed after use.
        /// </para>
        /// <typeparam name="T">The type of element in the generated sequence</typeparam>
        /// <param name="source">The generator for a sequence</param>
        /// <param name="shouldDispose">
        /// <c>true</c> to dispose <paramref name="source"/> when the enumerator is disposed, or
        /// <c>false</c> to leave it undisposed. (This parameter has no effect unless <paramref
        /// name="source"/> is also an <see cref="IDisposable"/>.)
        /// </param>
        /// <returns>An enumerator representing this generator</returns>
        public static IEnumerator<T> AsEnumerator<T>(this IGenerator<T> source, bool shouldDispose = true)
        {
            Checker.NotNull(source, "source");
            return new GeneratorEnumerator<T>(source, shouldDispose);
        }

        /// <summary>
        /// Adapts this enumerator to work as a disposable <see cref="IGenerator{T}"/>.
        /// </summary>
        /// <para>
        /// The created generator implements <see cref="IDisposable"/>. The generator is disposed
        /// when <see cref="IGenerator{T}.TryGetNext(out T)"/> returns <c>false</c> for the first
        /// time, or when <see cref="IDisposable.Dispose"/> is called, whichever occurs first. When
        /// the generator is disposed, the reference to the enumerator is unset. Having set <paramref
        /// name="shouldDispose"/> to <c>true</c> will cause the enumerable to be disposed before its
        /// reference is unset.
        /// </para>
        /// <para>
        /// When using this functionality, ensure that the generator is disposed, such as by way of a
        /// <c>using</c> block or by calling <see cref="IDisposable.Dispose"/> explicitly. As a rule,
        /// an <see cref="IEnumerator{T}"/> used directly is an object that must be properly disposed
        /// after use.
        /// </para>
        /// <typeparam name="T">The type of element in the enumerated sequence</typeparam>
        /// <param name="source">The enumerator for a sequence</param>
        /// <param name="shouldDispose">
        /// <c>true</c> to dispose <paramref name="source"/> when the disposable generator is
        /// disposed, or <c>false</c> to leave it undisposed.
        /// </param>
        /// <returns></returns>
        public static DisposableGenerator<T> AsGenerator<T>(this IEnumerator<T> source, bool shouldDispose = true)
        {
            return new EnumeratorGenerator<T>(Checker.NotNull(source, "source"), shouldDispose);
        }


        /// <summary>
        /// Non-specific class for objects implementing both <see cref="IDisposable"/> and <see cref="IGenerator{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract class DisposableGenerator<T> : IDisposable, IGenerator<T>
        {
            internal DisposableGenerator() { }

            /// <inheritDoc/>
            public abstract void Dispose();

            /// <inheritDoc/>
            public abstract bool TryGetNext(out T value);
        }

        private class FunctionBasedGenerator<T> : IGenerator<T>
        {
            private readonly GeneratorFunction<T> source;

            internal FunctionBasedGenerator(GeneratorFunction<T> source)
            {
                this.source = Checker.NotNull(source, "source");
            }

            public bool TryGetNext(out T value)
            {
                return source(out value);
            }
        }

        private class GeneratorEnumerator<T> : IEnumerator<T>
        {
            private enum EnumeratorState
            {
                InProgress,
                BeforeStart,
                AfterEnd,
            };

            private IGenerator<T> source;
            private bool shouldDispose;
            private T current = default(T);
            private EnumeratorState state = EnumeratorState.BeforeStart;

            internal GeneratorEnumerator(IGenerator<T> source, bool shouldDispose)
            {
                this.source = Checker.NotNull(source, "source");
                this.shouldDispose = shouldDispose;
            }

            public T Current
            {
                get
                {
                    switch (state)
                    {
                        case EnumeratorState.BeforeStart: throw Errors.EnumNotStarted();
                        case EnumeratorState.AfterEnd: throw Errors.EnumEnded();
                        default:
                            return current;
                    }
                }
            }

            object IEnumerator.Current { get { return Current; } }

            public void Dispose()
            {
                var disposable = source as IDisposable;
                source = null;
                current = default(T);

                if (shouldDispose)
                {
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                    shouldDispose = false;
                }
            }

            public bool MoveNext()
            {
                switch (state)
                {
                    case EnumeratorState.AfterEnd:
                        {
                            return false;
                        }
                    default:
                        {
                            if (source == null) { throw new ObjectDisposedException("source"); }

                            T value;
                            bool hasValue = source.TryGetNext(out value);
                            if (hasValue)
                            {
                                current = value;
                                state = EnumeratorState.InProgress;
                                return true;
                            }
                            else
                            {
                                current = default(T);
                                state = EnumeratorState.AfterEnd;
                                Dispose();
                                return false;
                            }
                        }
                }
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }
        
        private class EnumeratorGenerator<T> : DisposableGenerator<T>
        {
            private IEnumerator<T> source;
            private bool shouldDispose;

            internal EnumeratorGenerator(IEnumerator<T> source, bool shouldDispose)
            {
                this.source = Checker.NotNull(source, "source");
                this.shouldDispose = shouldDispose;
            }

            public override void Dispose()
            {
                var disposable = source as IDisposable;
                source = null;
                if (shouldDispose)
                {
                    disposable.Dispose();
                    shouldDispose = false;
                }
            }

            public override bool TryGetNext(out T value)
            {
                if (source == null) { throw new ObjectDisposedException("source"); }
                var response = source.MoveNext();
                value = response ? source.Current : default(T);
                return response;
            }
        }
    }
}
