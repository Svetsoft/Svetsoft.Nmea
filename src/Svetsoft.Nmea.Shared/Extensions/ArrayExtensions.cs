using System;

namespace Svetsoft.Nmea
{
    internal static class ArrayExtensions
    {
        /// <summary>
        ///     Creates an array from elements in a specified array.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">The array that contains the elements.</param>
        /// <param name="index">A 32-bit integer that represents the index in the sourceArray at which copying begins.</param>
        /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
        /// <returns>The array with the elements.</returns>
        public static TSource[] ToArray<TSource>(this TSource[] source, int index, int length)
        {
            var result = new TSource[length];
            Array.Copy(source, index, result, 0, length);
            return result;
        }
    }
}