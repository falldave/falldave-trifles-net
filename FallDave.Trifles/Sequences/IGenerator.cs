using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallDave.Trifles.Sequences
{
    /// <summary>
    /// Interface for an object representing a sequence.
    /// </summary>
    /// <para>
    /// Like an <see cref="IEnumerator{T}"/>, an object implementing this interface retrieves and
    /// presents a single value at a time. Differences from <see cref="IEnumerator{T}"/> include:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Advancing in the sequence and retrieving the value are the same operation, <see
    /// cref="TryGetNext(ref T)"/>. <see cref="IEnumerator{T}"/> separates these tasks.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// There is no notion of a "current" value; a value in the sequence is only produced when advancing.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// This interface provides no means to restart the generation (though one can be implemented
    /// separately from this interface).
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// This interface does not require <see cref="IDisposable"/> (though it can be implemented
    /// separately from this interface, if needed).
    /// </description>
    /// </item>
    /// </list>
    /// </para>
    /// <typeparam name="T">The type of element in the generated sequence</typeparam>
    public interface IGenerator<T>
    {
        /// <summary>
        /// Attempts to advance this generator and, if successful, provides the next value in the sequence.
        /// </summary>
        /// <para>The behavior of this method when called after the end of the sequence is undefined.</para>
        /// <param name="value">
        /// Variable to set to the next value in the sequence (only defined when <c>true</c> is returned)
        /// </param>
        /// <returns>
        /// <c>true</c> if the generator has been advanced and <paramref name="value"/> has been set
        /// to the next value, or <c>false</c> if the sequence has ended.
        /// </returns>
        bool TryGetNext(out T value);
    }
}
